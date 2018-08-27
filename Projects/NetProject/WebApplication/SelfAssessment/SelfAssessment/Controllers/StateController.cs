using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public ActionResult Index()
        {
            var lmodel = new List<State>();

            for (int i = 1; i <= 15; i++)
            {
                var model = new State();
                model.Id = i;
                model.StateName = "StateName" + i;
                lmodel.Add(model);
            }
            return View(lmodel);
        }      

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(State state)
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
