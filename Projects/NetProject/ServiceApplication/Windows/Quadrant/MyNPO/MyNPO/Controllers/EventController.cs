using MyNPO.DataAccess;
using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class EventController : BaseController
    {
        EntityContext entityContext = new EntityContext();

        // GET: Event
        public ActionResult Index()
        {
            entityContext = new EntityContext();
            var lEvents = entityContext.eventInfos.ToList();
            return View(lEvents);
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GmailShare(int id)
        {
            var entityContext = new EntityContext();
            var eventInfo = entityContext.eventInfos.Where(q => q.Id == id).FirstOrDefault();
            var email = entityContext.familyInfos.Select(q => q.Email).ToList();

            foreach(string mail in email)
                MailSender.EventSendMail(new EventInfo() { Email = mail, Event = eventInfo });

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase postedFile, FormCollection eventCreation)
        {
            try
            {
                string path = Server.MapPath("~/Images/Events/");
                string fileName = string.Empty;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (!string.IsNullOrEmpty(postedFile?.FileName))
                {
                    fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + fileName);
                }

                var createEvent = new Event()
                {
                    Name = eventCreation["EventName"],
                    Location = eventCreation["EventLocation"],
                    Details = eventCreation["EventDescription"],
                    StartDate = Convert.ToDateTime(eventCreation["EventStartDate"]),
                    EndDate = Convert.ToDateTime(eventCreation["EventEndDate"]),
                    UploadFileName = fileName
                };
                var entityContext = new EntityContext();
                entityContext.eventInfos.Add(createEvent);
                entityContext.SaveChanges();
                ViewBag.Status = "Successfully Created";
                return View();
            }
            catch (Exception ex)
            {
                return View();

            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var eUser = entityContext.eventInfos.FirstOrDefault(q => q.Id == id);
            return View(eUser);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event eventInfos)
        {
            try
            {
                var eventId = entityContext.eventInfos.FirstOrDefault(q => q.Id == id);

                eventId.Name = eventInfos.Name; eventId.Location = eventInfos.Location; eventId.Details = eventInfos.Details;
                eventId.StartDate = eventInfos.StartDate; eventId.EndDate = eventInfos.EndDate; eventId.UploadFileName = eventInfos.UploadFileName;
                entityContext.Entry(eventId).State = EntityState.Modified;
                entityContext.eventInfos.AddOrUpdate(eventId);
                // TODO: Add update logic here
                entityContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            var eventId = entityContext.eventInfos.FirstOrDefault(q => q.Id == id);
            return View(eventId);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event eventInfo)
        {
            try
            {
                // TODO: Add delete logic here
                var eventId = entityContext.eventInfos.FirstOrDefault(q => q.Id == id);
                entityContext.Entry(eventId).State = EntityState.Deleted;
                entityContext.eventInfos.Remove(eventId);
                entityContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}