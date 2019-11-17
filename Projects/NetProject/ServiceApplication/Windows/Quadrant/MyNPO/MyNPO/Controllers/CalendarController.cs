using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHTMLX.Scheduler;
using DHTMLX.Common;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Controls;

using MyNPO.Models;
using System.Reflection;
using MyNPO.DataAccess;
using System.Data.Entity.Migrations;

namespace MyNPO.Controllers
{
    public class CalendarController : BaseController
    {
        public ActionResult Index()
        {
            //https://www.c-sharpcorner.com/article/using-dhtmlx-scheduler-in-mvc/

            //Being initialized in that way, scheduler will use CalendarController.Data as a the datasource and CalendarController.Save to process changes
            var scheduler = new DHXScheduler(this);

            /*
             * It's possible to use different actions of the current controller
             *      var scheduler = new DHXScheduler(this);     
             *      scheduler.DataAction = "ActionName1";
             *      scheduler.SaveAction = "ActionName2";
             * 
             * Or to specify full paths
             *      var scheduler = new DHXScheduler();
             *      scheduler.DataAction = Url.Action("Data", "Calendar");
             *      scheduler.SaveAction = Url.Action("Save", "Calendar");
             */

            /*
             * The default codebase folder is ~/Scripts/dhtmlxScheduler. It can be overriden:
             *      scheduler.Codebase = Url.Content("~/customCodebaseFolder");
             */

            var currentDate = DateTime.Now;
            scheduler.InitialDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;

            return View(scheduler);
        }

        public ContentResult Data()
        {
            var entityContext = new EntityContext();
            var cEvent = new List<CalendarEvent>();
            try
            {
                var data = entityContext.calendarInfo.Where(t => t.Type == "Room").ToList();
                data.ForEach(q =>
                {
                    cEvent.Add(new CalendarEvent() { id = q.Id, text = q.Text, start_date = q.StartDate, end_date = q.EndDate });
                });
            }
            catch (Exception ex)
            {
                throw;
            }

            var data1 = new SchedulerAjaxData(cEvent);
            return (ContentResult)data1;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var entityContext = new EntityContext();
            var action = new DataAction(actionValues);
            var cInfo = new CalendarInfo();
            int Id = 0;
            string typeOfAction = string.Empty;
            try
            {
                var maxData = entityContext.calendarInfo.Where(q => q.Type == "Room").ToList();
                if (maxData != null && maxData.Count > 0)
                {
                    var maxId = maxData?.Max(q => q.Id);
                    Id = maxId.Value;
                }
                var changedEvent = (CalendarEvent)DHXEventsHelper.Bind(typeof(CalendarEvent), actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        {
                            cInfo.Id = ++Id; cInfo.Text = changedEvent.text; cInfo.StartDate = changedEvent.start_date; cInfo.EndDate = changedEvent.end_date;cInfo.Type = "Room";
                            entityContext.calendarInfo.Add(cInfo);
                            typeOfAction = "New Room Booked";
                        }
                        break;
                    case DataActionTypes.Delete:
                        {
                            cInfo = entityContext.calendarInfo.FirstOrDefault(q => q.Id == changedEvent.id);
                            entityContext.calendarInfo.Remove(cInfo);
                            typeOfAction = "Cancel the Room";
                        }
                        break;
                    default:// "update"
                        {
                            cInfo = entityContext.calendarInfo.FirstOrDefault(q => q.Id == changedEvent.id);
                            cInfo.Id = changedEvent.id; cInfo.Text = changedEvent.text; cInfo.StartDate = changedEvent.start_date; cInfo.EndDate = changedEvent.end_date; cInfo.Type = "Room";
                            entityContext.calendarInfo.AddOrUpdate(cInfo);
                            typeOfAction = "Modified the Room";
                        }
                        break;
                }
                entityContext.SaveChanges();
                Notifications.NotificationToAdmins(typeOfAction, cInfo);
            }
            catch(Exception ex)
            {
                action.Type = DataActionTypes.Error;
            }
            return (ContentResult)new AjaxSaveResponse(action);
        }
    }
}

