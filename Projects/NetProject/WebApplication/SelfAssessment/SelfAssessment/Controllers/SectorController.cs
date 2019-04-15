using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class SectorController : AdminBaseController
    {
        // GET: Sector
        public ActionResult Index()
        {
            var lmodel = new List<Sector>();
            var uilmodel = new List<UISector>();

            using (Repository<Sector> repository = new Repository<Sector>())
            {
                lmodel = repository.All().ToList();
            }

            int i = 1;
            lmodel.ForEach(q =>
            {
                uilmodel.Add(new UISector() { Id = q.Id, CreateDate = q.CreateDate, SectorName = q.SectorName, OrderId = i });
                i = i + 1;
            });
            return View(uilmodel);
        }

        [HttpPost]
        public ActionResult Create(Sector sector)
        {
            try
            {
                using (var repository = new Repository<Sector>())
                {
                    if (sector.Id != 0)
                    {
                        var updateSector = repository.Filter(q => q.Id == sector.Id).FirstOrDefault();
                        if (updateSector != null && !string.IsNullOrEmpty(updateSector.SectorName))
                        {
                            updateSector.SectorName = sector.SectorName;
                            updateSector.UpdateDate = DateTime.Now;
                            repository.Update(updateSector);
                        }
                    }
                    else
                    {
                        var count = repository.All().Count(); 
                        repository.Create(new Sector() { Id = ++count, SectorName = sector.SectorName, CreateDate = DateTime.Now });
                    }
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                UserException.LogException(ex);
                return Json("Failiure", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Delete(int id)
        {
            using (Repository<Sector> repository = new Repository<Sector>())
            {
                var deleteSector = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteSector != null && !string.IsNullOrEmpty(deleteSector.SectorName))
                {
                    repository.Delete(deleteSector);
                }
                repository.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}