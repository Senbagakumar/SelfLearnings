using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class SectorController : Controller
    {
        // GET: Sector
        public ActionResult Index()
        {
            var lmodel = new List<Sector>();

            for(int i=1; i<=20; i++)
            {
                var model = new Sector();
                model.Id = i;
                model.SectorName = "SectorName" + i;                 
                lmodel.Add(model);
            }
            return View(lmodel);
        }

        [HttpPost]
        public ActionResult Create(Sector sector)
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
        
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }
    }
}