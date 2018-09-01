using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
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
            var type = new List<SelectListItem>();

            var firstItem = new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true };
            type.Add(firstItem);

            type.Add(new SelectListItem() { Text = "Small", Value = "1" });
            type.Add(new SelectListItem() { Text = "Large", Value = "2" });
            type.Add(new SelectListItem() { Text = "Operating Unit", Value = "3" });

            var subSector = new List<SelectListItem>();
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
            var cities = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();


            using (var repository = new Repository<SubSector>())
            {
                subSector = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
            }

            using (var repository = new Repository<Sector>())
            {
                sector = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
            }

            using (var repository = new Repository<State>())
            {
                states = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
            }

            using (var repository = new Repository<Revenue>())
            {
                revenue = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
            }

            using (var repository = new Repository<ServiceType>())
            {
                typeOfService = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
            }

            using (var repository = new Repository<City>())
            {
                cities = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
            }

            var lastItem = new SelectListItem() { Text = "OTHERS", Value = "1000" };

            sector.Insert(0, firstItem);
            subSector.Insert(0, firstItem);
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            cities.Insert(0, firstItem);
            states.Insert(0, firstItem);

            sector.Add(lastItem);
            subSector.Add(lastItem);

            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = subSector;
            ViewBag.City = cities;
            ViewBag.State = states;
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
