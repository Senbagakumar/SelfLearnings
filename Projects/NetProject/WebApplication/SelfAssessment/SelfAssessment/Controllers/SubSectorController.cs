﻿using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class SubSectorController : Controller
    {
        // GET: SubSector
        public ActionResult Index()
        {
            var lmodel = new List<UISubSector>();
            var subSector = new List<SubSector>();
            var sector = new List<Sector>();

            using (var repository = new Repository<SubSector>())
            {
                subSector = repository.All().ToList();
            }

            using (var repository = new Repository<Sector>())
            {
                sector = repository.All().ToList();
            }

            var list = sector.Join(subSector, r => r.Id, t => t.SectorId, (r, t) => new UISubSector() { Id=t.Id, SectorId=t.SectorId, SectorName=r.SectorName, SubSectorName=t.SubSectorName }).ToList();

            var lmod = new List<SelectListItem>();
            lmod = sector.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
            lmod.Insert(0, new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true });
           
            ViewBag.SectorList = lmod;
            return View(list);
        }

        // POST: SubSector/Create
        [HttpPost]
        public JsonResult Create(UISubSector uISubSector)
        {
            try
            {
                using (var repository = new Repository<SubSector>())
                {
                    if (uISubSector.Id != 0)
                    {
                        var subSector = repository.Filter(q => q.Id == uISubSector.Id).FirstOrDefault();
                        if (subSector != null && !string.IsNullOrEmpty(subSector.SubSectorName))
                        {
                            subSector.SubSectorName = uISubSector.SubSectorName;
                            subSector.SectorId = uISubSector.SectorId;
                            subSector.UpdateDate = DateTime.Now;
                            repository.Update(subSector);
                        }
                    }
                    else
                    {
                        repository.Create(new SubSector() { SubSectorName = uISubSector.SubSectorName, SectorId=uISubSector.SectorId, CreateDate = DateTime.Now });
                    }
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch
            {
                return Json("Failiure", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }       

        // GET: SubSector/Delete/5
        public JsonResult Delete(int id)
        {
            using (var repository = new Repository<SubSector>())
            {
                var deleteSubSector = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteSubSector != null && !string.IsNullOrEmpty(deleteSubSector.SubSectorName))
                {
                    repository.Delete(deleteSubSector);
                }
                repository.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }       
    }
}