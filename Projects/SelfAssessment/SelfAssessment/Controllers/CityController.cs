using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            var lmodel = new List<UICity>();

            for (int i = 1; i <= 20; i++)
            {
                var model = new UICity();
                model.Id = i;
                model.CityName = "CityName" + i;
                model.StateName = "StateName" + i;
                lmodel.Add(model);
            }

            var lmod = new List<SelectListItem>();
            lmod.Add(new SelectListItem() { Text = "-- Select --", Value = "-- Select --", Selected = true });

            for (int i = 1; i <= 20; i++)
            {
                var mod = new SelectListItem();
                mod.Text = "StateName" + i;
                mod.Value = "StateName" + i;
                lmod.Add(mod);
            }

            ViewBag.StateList = lmod;

            return View(lmodel);
        }

        
        // POST: City/Create
        [HttpPost]
        public ActionResult Create(UICity uICity)
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

        
        // GET: City/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

       
    }
}
