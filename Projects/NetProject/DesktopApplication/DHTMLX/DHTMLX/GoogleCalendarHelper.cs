using DHTMLX.Scheduler.GoogleCalendar;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Linq;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;

namespace DHTMLX.Scheduler
{
    public enum Logic
    {
        AND,
        OR,
        XOR,
        NOT,
    }
    public class GoogleCalendarHelper
    {
        protected string UserName;
        protected string Password;

        public GoogleCalendarHelper(string login, string password)
        {
            this.UserName = login;
            this.Password = password;
        }

        public void ExportToGoogleCalendar(string url, string ics)
        {
            List<SchedulerEvent> schedulerEventList = new ICalHelper().Parse(ics);
            this.ExportToGoogleCalendar(url, (IEnumerable)schedulerEventList);
        }

        public void ExportToGoogleCalendar(string url, IEnumerable events)
        {
            object obj1 = (object)null;
            List<ICalEvent> icalEventList = new List<ICalEvent>();
            foreach (object obj2 in events)
            {
                if (obj2 is ICalEvent)
                {
                    icalEventList.Add(obj2 as ICalEvent);
                }
                else
                {
                    obj1 = obj2;
                    break;
                }
            }
            if (obj1 == null)
                return;
            PropertyInfo[] properties = obj1.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "text"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "start_date"));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "end_date"));
            PropertyInfo propertyInfo4 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "description"));
            if (propertyInfo2 == null && propertyInfo1 == null)
                return;
            foreach (object obj2 in events)
            {
                ICalEvent icalEvent = new ICalEvent();
                icalEvent.Summary = (string)propertyInfo1.GetValue(obj2, (object[])null);
                icalEvent.SDate = (DateTime)propertyInfo2.GetValue(obj2, (object[])null);
                if (propertyInfo3 != null)
                    icalEvent.EDate = (DateTime)propertyInfo3.GetValue(obj2, (object[])null);
                if (propertyInfo4 != null)
                    icalEvent.Description = (string)propertyInfo4.GetValue(obj2, (object[])null);
                icalEventList.Add(icalEvent);
            }
            this.ExportToGoogleCalendar(url, (IEnumerable<ICalEvent>)icalEventList);
        }

        public void ExportToGoogleCalendar(string url, IEnumerable<ICalEvent> ics)
        {
            CalendarService calendarService = new CalendarService("dhtmlxScheduler .Net");
            ((Service)calendarService).setUserCredentials(this.UserName, this.Password);
            EventQuery eventQuery = new EventQuery(url);
            EventFeed eventFeed1 = calendarService.Query(eventQuery);
            AtomFeed atomFeed = new AtomFeed((AtomFeed)eventFeed1);
            foreach (ICalEvent ic in ics)
            {
                EventEntry eventEntry = new EventEntry();
                ((AtomEntry)eventEntry).Title.Text = ic.Summary;
                ((AtomEntry)eventEntry).Content.Content= ic.Description;
                When when = new When(ic.SDate, ic.EDate);
                eventEntry.Times.Add(when);
                ((AtomEntry)eventEntry).BatchData = (new GDataBatchEntryData((GDataBatchOperationType)0));
                atomFeed.Entries.Add((AtomEntry)eventEntry);
            }
            EventFeed eventFeed2 = (EventFeed)((Service)calendarService).Batch(atomFeed, new Uri(((AtomFeed)eventFeed1).Batch));
        }
    }
}

