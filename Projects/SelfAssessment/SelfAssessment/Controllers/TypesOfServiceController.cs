using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class TypesOfServiceController : Controller
    {
        // GET: TypesOfService
        public ActionResult Index()
        {
            var lmodel = new List<ServiceType>();

            for (int i = 1; i <= 20; i++)
            {
                var model = new ServiceType();
                model.Id = i;
                model.Name = "TypesOfService" + i;
                lmodel.Add(model);
            }

            return View(lmodel);
        }

        // POST: TypesOfService/Create
        [HttpPost]
        public ActionResult Create(ServiceType serviceType)
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
        // GET: TypesOfService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
