using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class ManageAssessmentController : Controller
    {
        // GET: ManageAssessment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AssignOrganization()
        {
            return View();
        }

        public ActionResult ListAssessment()
        {
            return View();
        }

        public ActionResult AssignQuestion()
        {
            return View();
        }
        // GET: ManageAssessment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageAssessment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageAssessment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: ManageAssessment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManageAssessment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ManageAssessment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageAssessment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
