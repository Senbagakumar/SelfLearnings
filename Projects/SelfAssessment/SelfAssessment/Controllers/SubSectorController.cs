using SelfAssessment.Models;
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

            for (int i = 1; i <= 20; i++)
            {
                var model = new UISubSector();
                model.Id = i;
                model.SubSectorName = "SubSectorName"+ i;
                model.SectorName = "SectorName" + i;
                lmodel.Add(model);
            }

            var lmod = new List<SelectListItem>();
            lmod.Add(new SelectListItem() { Text = "-- Select --", Value = "-- Select --", Selected = true });

            for (int i=1; i <=20; i++)
            {
                var mod = new SelectListItem();
                mod.Text = "SectorName" + i;
                mod.Value = "SectorName" + i;
                lmod.Add(mod);
            }
           
            ViewBag.SectorList = lmod;
            return View(lmodel);
        }

        // POST: SubSector/Create
        [HttpPost]
        public ActionResult Create(UISubSector uISubSector)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }       

        // GET: SubSector/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }       
    }
}
