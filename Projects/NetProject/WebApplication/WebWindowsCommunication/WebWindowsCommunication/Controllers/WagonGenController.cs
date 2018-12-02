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
    public class WagonGenController : Controller
    {
        // GET: WagonGen
        public ActionResult Index()
        {
            var entity = new EntityContext();
            var list = entity.wagons.ToList();
            return View(list);
        }

        // GET: WagonGen/Details/5
        public ActionResult Details(int id)
        {
            var entity = new EntityContext();
            var details = entity.wagons.Where(q => q.Id == id).FirstOrDefault();
            return View(details);
        }

        // GET: WagonGen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WagonGen/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                var entity = new EntityContext();
                // TODO: Add insert logic here

                var inputValues = collection["sno"];
                foreach(string iput in inputValues.Split(','))
                {
                    if (!string.IsNullOrEmpty(iput) && iput.Contains("$"))
                    {
                        var sv = iput.Split('$');
                        var wagon = new Wagon() { Slno=sv[0], WagonWeight=sv[1], Direction=sv[2], ErrorValue=sv[3] };
                        entity.wagons.Add(wagon);
                    }
                }
                entity.SaveChanges();

                var list = entity.wagons.ToList();
                FileGeneration(list);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WagonGen/Edit/5
        public ActionResult Edit(int id)
        {
            var entity = new EntityContext();
            var editDetails = entity.wagons.Where(q => q.Id == id).FirstOrDefault();
            return View(editDetails);
        }

        // POST: WagonGen/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                var entity = new EntityContext();

                var editdetails = new EntityContext().wagons.FirstOrDefault(t => t.Id == id);

                var slno = collection["Slno"];
                if (!string.IsNullOrEmpty(slno))
                {
                    editdetails.Slno = slno;
                    editdetails.WagonWeight = collection["WagonWeight"];
                    editdetails.Direction = collection["Direction"];
                    editdetails.ErrorValue = collection["ErrorValue"];

                    entity.Entry(editdetails).State = System.Data.Entity.EntityState.Modified;
                    entity.wagons.AddOrUpdate(editdetails);
                }
                
                entity.SaveChanges();

                //

                var list = entity.wagons.ToList();
                FileGeneration(list);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WagonGen/Delete/5
        public ActionResult Delete(int id)
        {
            var entity = new EntityContext();
            var deleteDetails = entity.wagons.Where(q => q.Id == id).FirstOrDefault();
            return View(deleteDetails);
        }

        // POST: WagonGen/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var entity = new EntityContext();
                var deleteDetails = entity.wagons.FirstOrDefault(q => q.Id == id);
                entity.wagons.Remove(deleteDetails);
                entity.SaveChanges();

                var list = entity.wagons.ToList();
                FileGeneration(list);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void FileGeneration(List<Wagon> wagons)
        {
            var folderPath = Server.MapPath("~/OutputFiles");
            var fileName = $"wagons.txt";
            var filePath = $@"{folderPath}\{fileName}";

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            var dataformat = string.Empty;

            foreach(Wagon wan in wagons)
             dataformat+= $"{wan.Slno}${ wan.WagonWeight}${wan.Direction}${wan.ErrorValue}";

            System.IO.File.AppendAllText(filePath, dataformat);
        }
    }
}
