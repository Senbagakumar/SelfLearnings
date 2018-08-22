using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAssessment()
        {
            return View();
        }
        public ActionResult CustomerAssessment()
        {
            return View();
        }
        public ActionResult AssessmentReport()
        {
            return View();
        }
    }
}
