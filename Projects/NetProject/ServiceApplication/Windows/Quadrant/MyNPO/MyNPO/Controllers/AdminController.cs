using MyNPO.DataAccess;
using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class AdminController : BaseController
    {
        EntityContext entityContext = new EntityContext();
        // GET: Admin
        public ActionResult Index()
        {
            var listUser = entityContext.adminUser.ToList();
            return View(listUser);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            var detailsUser = entityContext.adminUser.FirstOrDefault(q => q.Id == id);
            return View(detailsUser);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(AdminUser adminUser)
        {
            try
            {
                // TODO: Add insert logic here

                var aUser = new AdminUser() { Email=adminUser.Email, Name=adminUser.Name, PhoneNumber=adminUser.PhoneNumber };
                entityContext.adminUser.Add(aUser);
                entityContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var eUser = entityContext.adminUser.FirstOrDefault(q => q.Id == id);
            return View(eUser);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AdminUser adminUser)
        {
            try
            {
                var eUser = entityContext.adminUser.FirstOrDefault(q => q.Id == id);

                eUser.Name = adminUser.Name; eUser.Email = adminUser.Email; eUser.PhoneNumber = adminUser.PhoneNumber;
                entityContext.Entry(eUser).State = EntityState.Modified;
                entityContext.adminUser.AddOrUpdate(eUser);
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
            var dUser = entityContext.adminUser.FirstOrDefault(q => q.Id == id);
            return View(dUser);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AdminUser adminUser)
        {
            try
            {
                // TODO: Add delete logic here
                var dCUser = entityContext.adminUser.FirstOrDefault(q => q.Id == id);
                entityContext.Entry(dCUser).State = EntityState.Deleted;
                entityContext.adminUser.Remove(dCUser);
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
