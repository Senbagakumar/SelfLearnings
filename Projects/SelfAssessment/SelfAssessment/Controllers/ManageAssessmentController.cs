using SelfAssessment.Models;
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
            var lAssessment = new List<Assessment>();
            for(int i=1; i<10; i++)
            {
                var assessment = new Assessment();
                assessment.Id = i;
                assessment.Name = "Assessment" + i;
                assessment.Description = "Assessment Des" + i;
                assessment.AdminEmail = "Ass@gmail.com";
                assessment.AssessmentFormat = "System Architect";
                lAssessment.Add(assessment);
            }
            return View(lAssessment);
        }

        [HttpPost]
        public ActionResult SaveAssessment(Assessment assessment)
        {
            return RedirectToAction("Index");
        }

        public JsonResult GetAssessMentDetails(int id)
        {
            var model = new Assessment()
            {
                Name = "AssessmentName",
                Description = "AssessmentDescription",
                AdminEmail = "Ademin@gmail.com",
                Id = 1,
                WelcomeMessage = "Welcome",
                EndMessage = "Thank you",
                LineAssessmentPublically = true,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                AssessmentFormat = "QuestionByQuestion",
                AllowPublicRegistration = true,
                ParticipantsMayPrintAnswer = true,
                PublicStatistics = true,
                ShowNextButton = true,
                ShowProgressBar = true,
                ShowQuestionIndex = true,
                ShowWelcomeScreen = true
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssignOrganization()
        {
            var sectorList = new List<SelectListItem>();
            var subSectorList = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();
            var type = new List<SelectListItem>();
            var city = new List<SelectListItem>();
            var state = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();

            type.Add(new SelectListItem() { Text = "Small", Value = "Small" });
            type.Add(new SelectListItem() { Text = "Large", Value = "Large" });
            type.Add(new SelectListItem() { Text = "Operating Unit", Value = "Operating Unit" });

            var first = new SelectListItem() { Text = "-- Select --", Value = "-- Select --", Selected = true };
            sectorList.Add(first);


            for (int i = 1; i <= 10; i++)
            {
                var mod = new SelectListItem();
                mod.Text = "SectorName" + i;
                mod.Value = "SectorName" + i;
                sectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "SubSectorName" + i;
                mod.Value = "SubSectorName" + i;
                subSectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "City" + i;
                mod.Value = "City" + i;
                city.Add(mod);

                mod = new SelectListItem();
                mod.Text = "State" + i;
                mod.Value = "State" + i;
                state.Add(mod);

                mod = new SelectListItem();
                mod.Text = "Revenue" + i;
                mod.Value = "Revenue" + i;
                revenue.Add(mod);

                mod = new SelectListItem();
                mod.Text = "TypesOfService" + i;
                mod.Value = "TypesOfService" + i;
                typeOfService.Add(mod);

            }

            sectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "OTHERS" });
            subSectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "OTHERS" });

            ViewBag.SectorList = sectorList;
            ViewBag.SubSectorList = subSectorList;
            ViewBag.City = city;
            ViewBag.State = state;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;
            ViewBag.Type = type;
            return View();
        }

        [HttpPost]
        public JsonResult AssignOrganizationByFilter(UIOrganization organization)
        {
            var model = new UIOrganization()
            {
                Id=1,
                Name="Name",
                City="City",
                Revenue="Revenue",
                Sector="Sector",
                SubSector="SubSector",
                State="State",
                TypeOfService="TypeOfService",
                Type="Level1"                
            };
            var lmodel = new List<UIOrganization>();
            lmodel.Add(model);
            return Json(lmodel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAssessment()
        {
            var lAssessment = new List<UIAssessment>();
            for (int i=1; i<=3; i++)
            {
                var assessment = new UIAssessment();
                assessment.Type = "Level "+i;
                assessment.NoOfCompleted = 10;
                assessment.NoOfGroup = 10;
                assessment.NoOfParticipants = 10;
                assessment.NoOfPending = 10;
                assessment.NoOfQuestion = 10;
                lAssessment.Add(assessment);
            }
            return View(lAssessment);
        }

        public ActionResult AssignQuestion()
        {

            var lAssignment = new List<UIAssignQuestion>();
            for(int i=1; i<4; i++)
            {
                var assignment = new UIAssignQuestion();
                assignment.Id = i;
                assignment.AssignmentId = i;
                assignment.Level = "Level" + i;
                assignment.AssignMentName = "Assignment" + i;                
                assignment.NoOfGroup = i;
                assignment.NoOfQuestions = i;
                lAssignment.Add(assignment);
            }
            var lQroup = new List<UIAssignGroup>();

            int k = 1;
            for(int i=1; i<3; i++)
            {

                var qroup = new UIAssignGroup();                
                qroup.GroupId = i;                
                qroup.uIAssignGroupQuestions = new List<UIAssignGroupQuestion>();
                for(int j=1; j<3;j++)
                {
                    var question = new UIAssignGroupQuestion();
                    question.GroupId = i;
                    question.QuestionId = k;
                    if (i == 1)
                    {
                        qroup.GroupName = "ShortNameQuestion";
                        if (j == 1)
                            question.QuestionName = "ShortFirstName";
                        else
                            question.QuestionName = "ShortLastName";
                    }
                    else
                    {
                        qroup.GroupName = "LongNameQuestion";
                        if (j == 1)
                            question.QuestionName = "LongFirstName";
                        else
                            question.QuestionName = "LongLastName";
                    }
                    qroup.uIAssignGroupQuestions.Add(question);
                    k++;
                }
                lQroup.Add(qroup);

                lAssignment[0].uIAssignGroups = new List<UIAssignGroup>();
                lAssignment[0].uIAssignGroups.AddRange(lQroup);
            }
            return View(lAssignment);
        }
      

        public JsonResult GetQuestionByAssignmentId(int id)
        {
            var lqroup = new List<UIAssignGroup>();
            for (int i = 1; i < 2; i++)
            {
                var qroup = new UIAssignGroup();
                qroup.GroupId = i;
                qroup.uIAssignGroupQuestions = new List<UIAssignGroupQuestion>();
                for (int j = 1; j < 3; j++)
                {
                    var question = new UIAssignGroupQuestion();
                    question.GroupId = i;
                    question.QuestionId = j;

                    qroup.GroupName = "ShortNameQuestion";
                    if (j == 1)
                        question.QuestionName = "ShortFirstName";
                    else
                        question.QuestionName = "ShortLastName";

                    qroup.uIAssignGroupQuestions.Add(question);
                }
                lqroup.Add(qroup);
            }           
            return Json(lqroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveQuestion(UIQuest uIQuest)
        {
            return RedirectToAction("Index");
        }
    }
}
