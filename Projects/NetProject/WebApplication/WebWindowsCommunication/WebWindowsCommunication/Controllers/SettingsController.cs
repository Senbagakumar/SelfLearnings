using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWindowsCommunication.DataAccess;
using WebWindowsCommunication.Models;

namespace WebWindowsCommunication.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var list = new EntityContext().settings.ToList();
            list.ForEach(t => { t.Type = Utility.GetValue(t.Type); });
            return View(list);
        }

        // GET: Settings/Details/5
        public ActionResult Details(int id)
        {
            var details = new EntityContext().settings.FirstOrDefault(t => t.Id == id);
            details.Type = Utility.GetValue(details.Type);
            return View(details);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            var item = new List<SelectListItem>();
            var listOfItems = Utility.GetTypeValues();
            
            foreach(var key in listOfItems.Keys)
            {
                item.Add(new SelectListItem() { Text = listOfItems[key], Value = key });
            }
            ViewBag.TypeList =item;
            return View();
        }

        // POST: Settings/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                EntityContext entityContext = new EntityContext();
                var gross = new Settings() { Minimum = collection["Minimum"], Maximum = collection["Maximum"], ConstWt = collection["ConstWt"], Factor = collection["Factor"], Type=collection["Type"]};
                entityContext.settings.Add(gross);
                entityContext.SaveChanges();

                var type = Utility.GetTypeValues()[gross.Type];

                var dataformat = $"{type}${gross.Minimum}${ gross.Maximum}${ gross.ConstWt}${ gross.Factor}";

                var folderPath = Server.MapPath("~/OutputFiles");
                var fileName = $"{type}.txt";
                var filePath = $@"{folderPath}\{fileName}";

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                System.IO.File.AppendAllText(filePath, dataformat);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Settings/Edit/5
        public ActionResult Edit(int id)
        {
            var item = new List<SelectListItem>();
            var listOfItems = Utility.GetTypeValues();

            foreach (var key in listOfItems.Keys)
            {
                item.Add(new SelectListItem() { Text = listOfItems[key], Value = key });
            }


            var editdetails = new EntityContext().settings.FirstOrDefault(t => t.Id == id);

            item.FirstOrDefault(q => q.Value == editdetails.Type).Selected = true;

            ViewBag.TypeList = item;

            return View(editdetails);
        }

        // POST: Settings/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                EntityContext entityContext = new EntityContext();
                var gross = new Settings() { Minimum = collection["Minimum"], Maximum = collection["Maximum"], ConstWt = collection["ConstWt"], Factor = collection["Factor"], Type=collection["Type"] };

                var upDateDetails = new EntityContext().settings.FirstOrDefault(t => t.Id == id);
                upDateDetails.Minimum = gross.Minimum;
                upDateDetails.Maximum = gross.Maximum;
                upDateDetails.ConstWt = gross.ConstWt;
                upDateDetails.Factor = gross.Factor;
                upDateDetails.Type = gross.Type;

                entityContext.Entry(upDateDetails).State = System.Data.Entity.EntityState.Modified;
                entityContext.settings.AddOrUpdate(upDateDetails);

                entityContext.SaveChanges();

                var type = Utility.GetTypeValues()[gross.Type];
                var dataformat = $"{type}${gross.Minimum}${ gross.Maximum}${ gross.ConstWt}${ gross.Factor}";

                var folderPath = Server.MapPath("~/OutputFiles");
                var fileName = $"{type}.txt";
                var filePath = $@"{folderPath}\{fileName}";

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                System.IO.File.AppendAllText(filePath, dataformat);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Settings/Delete/5
        public ActionResult Delete(int id)
        {
            var deleteDetails = new EntityContext().settings.FirstOrDefault(t => t.Id == id);
            deleteDetails.Type = Utility.GetValue(deleteDetails.Type);
            return View(deleteDetails);
        }

        // POST: Settings/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var ec = new EntityContext();
                var item=ec.settings.FirstOrDefault(t => t.Id == id);
                ec.settings.Remove(item);
                ec.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
