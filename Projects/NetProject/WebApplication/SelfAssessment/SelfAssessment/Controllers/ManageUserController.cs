using SelfAssessment.Models;
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
            var uiOrganization = new UIOrganization();
            uiOrganization.Name = "Senba";
            uiOrganization.Address = "156th AVE NE, APT 287";
            uiOrganization.City = "Redmond";
            uiOrganization.Email = "abc@gmail.com";
            uiOrganization.MobileNo = "123456789";
            uiOrganization.State = "TamilNadu";
            uiOrganization.Designation = "Manager";
            uiOrganization.TypeOfService = "Service";
            return View(uiOrganization);
        }

        public ActionResult ListAssessment()
        {
            var lAssessment = new List<UIAssessment>();
            for (int i = 1; i <= 3; i++)
            {
                var assessment = new UIAssessment();
                assessment.Type = "Level " + i;               
                assessment.NoOfGroup = 10;             
                assessment.NoOfQuestion = 10;
                assessment.AssignmentStatus = "Pending";
                lAssessment.Add(assessment);
            }
            return View(lAssessment);
        }
        public ActionResult CustomerAssessment()
        {
            return View();
        }
        public ActionResult AssessmentReport()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            return View();
        }
    }
}
