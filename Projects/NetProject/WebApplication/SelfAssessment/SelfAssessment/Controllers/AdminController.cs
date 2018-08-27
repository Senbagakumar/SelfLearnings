using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerWiseReport()
        {
            return View();
        }

        public ActionResult SectorWiseReport()
        {
            return View();
        }
        public ActionResult CustomerDetails()
        {
            return View();
        }
    }
}
