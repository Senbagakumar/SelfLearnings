using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DHTMLX.Scheduler.GoogleCalendar
{
    public class SchedulerEvent
    {
        public string text { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public string id { get; set; }

        public string description { get; set; }

        public static SchedulerEvent Convert(ICalEvent ev)
        {
            return new SchedulerEvent()
            {
                id = ev.ICalUID,
                text = ev.Summary,
                start_date = ev.SDate,
                end_date = ev.EDate,
                description = ev.Description
            };
        }
    }
    public class RequestHelper
    {
        public string Send(string url)
        {
            return new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
    public class ICalEvent
    {
        public string Summary { get; set; }

        public DateTime SDate { get; set; }

        public DateTime EDate { get; set; }

        public string Description { get; set; }

        public string ICalUID { get; set; }
    }

    public class ICalHelper
    {
        public List<SchedulerEvent> GetFromFeed(string url)
        {
            return this.Parse(new RequestHelper().Send(url));
        }

        public List<SchedulerEvent> Parse(string ics)
        {
            List<SchedulerEvent> schedulerEventList = new List<SchedulerEvent>();
            string pattern = "BEGIN:VEVENT(.*?)END:VEVENT";
            Regex regex1 = new Regex("DTSTART(;VALUE=DATE)?:([0-9T]*)");
            Regex regex2 = new Regex("DTEND(;VALUE=DATE)?:([0-9T]*)");
            Regex regex3 = new Regex("SUMMARY:(.*)");
            Regex regex4 = new Regex("RECURRENCE:");
            Regex regex5 = new Regex("DESCRIPTION:(.*?)[A-Z]+(:|;)");
            Regex.Match(ics, pattern, RegexOptions.Singleline);
            string[] formats = new string[2]
            {
        "yyyyMMddTHHmmss",
        "yyyyMMdd"
            };
            DateTime result = new DateTime();
            foreach (Match match1 in new Regex(pattern, RegexOptions.Singleline).Matches(ics))
            {
                if (!regex4.IsMatch(match1.Groups[1].Value))
                {
                    SchedulerEvent schedulerEvent = new SchedulerEvent();
                    Match match2 = regex1.Match(match1.Groups[1].Value);
                    if (match2.Success && DateTime.TryParseExact(match2.Groups[2].Value, formats, (IFormatProvider)null, DateTimeStyles.None, out result))
                        schedulerEvent.start_date = result;
                    Match match3 = regex2.Match(match1.Groups[1].Value);
                    if (match3.Success && DateTime.TryParseExact(match3.Groups[2].Value, formats, (IFormatProvider)null, DateTimeStyles.None, out result))
                        schedulerEvent.end_date = result;
                    Match match4 = regex3.Match(match1.Groups[1].Value);
                    if (match4.Success)
                        schedulerEvent.text = match4.Groups[1].Value;
                    Match match5 = regex5.Match(match1.Groups[1].Value);
                    if (match5.Success)
                        schedulerEvent.description = match5.Groups[1].Value;
                    schedulerEventList.Add(schedulerEvent);
                }
            }
            return schedulerEventList;
        }
    }

}
