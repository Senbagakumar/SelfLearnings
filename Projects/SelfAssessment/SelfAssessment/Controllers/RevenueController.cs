using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class RevenueController : Controller
    {
        // GET: Revenue
        public ActionResult Index()
        {
            var lmodel = new List<Revenue>();

            for (int i = 1; i <= 20; i++)
            {
                var model = new Revenue();
                model.Id = i;
                model.Name = "Revenue" + i;
                lmodel.Add(model);
            }
            return View(lmodel);
        }

        // POST: Revenue/Create
        [HttpPost]
        public ActionResult Create(Revenue revenue)
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

       // GET: Revenue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
