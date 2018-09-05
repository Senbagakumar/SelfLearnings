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
            using (var assessmentRepo = new Repository<Assessment>())
            {
                //lAssessment = assessmentRepo.All().Select(q=> new Assessment() { Id=q.Id, Name=q.Name,Description=q.Description,AdminEmail=q.AdminEmail,AssessmentFormat=q.AssessmentFormat}).ToList();
                lAssessment = assessmentRepo.All().ToList();
            }
            return View(lAssessment);
        }

        [HttpPost]
        public JsonResult SaveAssessment(Assessment assessment)
        {
            assessment.CreateDate = DateTime.Now;

            using (var assessmentRepo = new Repository<Assessment>())
            {
                if(assessment.Id!=0)
                {
                    var updateAssessment = assessmentRepo.Filter(q => q.Id == assessment.Id).FirstOrDefault();
                    if (updateAssessment != null)
                    {
                        updateAssessment.AdminEmail = assessment.AdminEmail;
                        updateAssessment.AllowPublicRegistration = assessment.AllowPublicRegistration;
                        updateAssessment.AssessmentFormat = assessment.AssessmentFormat;
                        updateAssessment.Description = assessment.Description;
                        //updateAssessment.EndDate = assessment.EndDate;
                        updateAssessment.EndMessage = assessment.EndMessage;
                        updateAssessment.LineAssessmentPublically = assessment.LineAssessmentPublically;
                        updateAssessment.Name = assessment.Name;
                        updateAssessment.ParticipantsMayPrintAnswer = assessment.ParticipantsMayPrintAnswer;
                        updateAssessment.PublicStatistics = assessment.PublicStatistics;
                        updateAssessment.ShowNextButton = assessment.ShowNextButton;
                        updateAssessment.ShowProgressBar = assessment.ShowProgressBar;
                        updateAssessment.ShowQuestionIndex = assessment.ShowQuestionIndex;
                        updateAssessment.ShowWelcomeScreen = assessment.ShowWelcomeScreen;
                        updateAssessment.WelcomeMessage = assessment.WelcomeMessage;
                        assessment.UpdateDate = DateTime.Now;
                        assessmentRepo.Update(updateAssessment);
                    }
                }
                else
                {
                    assessmentRepo.Create(assessment);
                }                
                assessmentRepo.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssessMentDetails(int id)
        {
            var model = new Assessment();
            using (var assessmentRepo = new Repository<Assessment>())
            {
                model = assessmentRepo.Filter(q => q.Id == id).FirstOrDefault();
            }
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

        public JsonResult MoveToNextLevel(int id)
        {

            TempOrg tempOrg = null;
            using (var repo = new Repository<Organization>())
            {
                var org = repo.Filter(q => q.Id == id).FirstOrDefault();
                tempOrg = new TempOrg() { Level = org.CurrentAssignmentType, OrgId = org.Id, Status = "Completed", PromoteDate = DateTime.Now };
                if (org != null)
                {
                    if (org.CurrentAssignmentType == "Level 1")
                        org.CurrentAssignmentType = "Level 2";
                    else
                        org.CurrentAssignmentType = "Level 3";
                    repo.Update(org);
                    repo.SaveChanges();
                }
            }

            using (var rep = new Repository<TempOrg>())
            {
                rep.Create(tempOrg);
                rep.SaveChanges();
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignOrganizationByFilter(Organization org)
        {
            var lmodel = new List<UIOrganization>();
            using (var repo = new Repository<Organization>())
            {
                var listOrganization = repo.Filter(q => q.CityId == org.CityId
                                                        && q.StateId == org.StateId
                                                        && q.SectorId == org.SectorId
                                                        && q.RevenueId == org.RevenueId
                                                        && q.SubSectorId == org.SubSectorId
                                                        && q.CurrentAssignmentType == org.CurrentAssignmentType
                                                        && q.TypeId == org.TypeId).ToList();

                var states = new Repository<State>();
                var sector = new Repository<Sector>();
                var city = new Repository<City>();
                var subSector = new Repository<SubSector>();
                var typeofservice = new Repository<ServiceType>();
                var revenue = new Repository<Revenue>();

                listOrganization.ForEach(q=>
                {
                    UIOrganization model = new UIOrganization();
                    model.Id = q.Id;
                    model.Name = q.Name;
                    model.City = city.Filter(c => c.Id == q.CityId).FirstOrDefault().CityName;
                    model.Revenue = revenue.Filter(r => r.Id == q.RevenueId).FirstOrDefault().Name;
                    model.Sector = org.SectorId == 1000 ? "OTHERS" : sector.Filter(r => r.Id == q.SectorId).FirstOrDefault().SectorName;
                    model.SubSector = org.SubSectorId == 1000 ? "OTHERS" : subSector.Filter(r => r.Id == q.SubSectorId).FirstOrDefault().SubSectorName;
                    model.State = states.Filter(s => s.Id == q.Id).FirstOrDefault().StateName;
                    model.TypeOfService = typeofservice.Filter(ty => ty.Id == q.Id).FirstOrDefault().Name;
                    model.Type = "Level 1";
                    lmodel.Add(model);
                });
            }
            return Json(lmodel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListAssessment()
        {
            int? level1PenCount = 0;
            int? level2PenCount = 0;
            int? level3PenCount = 0;
            int? level1ComCount = 0;
            int? level2ComCount = 0;
            int? level3ComCount = 0;

            int level1QCount = 0;
            int level2QCount = 0;
            int level3QCount = 0;

            int level1GCount = 0;
            int level2GCount = 0;
            int level3GCount = 0;

            using (var repo = new Repository<Organization>())
            {
                var pendingCount = repo.All().ToList().GroupBy(q => q.CurrentAssignmentType).Select(q => new { count = q.Count(), Type = q.Key }).ToList();

                level1PenCount = pendingCount.FirstOrDefault(t => t.Type == "Level 1")?.count??0;
                level2PenCount = pendingCount.FirstOrDefault(t => t.Type == "Level 2")?.count??0;
                level3PenCount = pendingCount.FirstOrDefault(t => t.Type == "Level 3")?.count??0;               
            }

            using (var repo = new Repository<TempOrg>())
            {
                var completedCount = repo.All().ToList().GroupBy(q => q.Level).Select(q => new { count = q.Count(), Type = q.Key }).ToList();

                level1ComCount = completedCount.FirstOrDefault(t => t.Type == "Level 1")?.count??0;
                level2ComCount = completedCount.FirstOrDefault(t => t.Type == "Level 2")?.count??0;
                level3ComCount = completedCount.FirstOrDefault(t => t.Type == "Level 3")?.count??0;
            }

            var questionRepo = new Repository<Questions>();
            var assessmentRepo = new Repository<Assessment>();
            var assessmentList=assessmentRepo.All().ToList().GroupBy(q => new { q.AssessmentType, q.Id }).Select(q => new { id = q.Key.Id, type=q.Key.AssessmentType }).ToList();

            assessmentList.ForEach(q => 
            {
                level1QCount = questionRepo.Filter(t => t.AssignmentId == q.id && q.type == "Level 1").Count();
                level2QCount = questionRepo.Filter(t => t.AssignmentId == q.id && q.type == "Level 2").Count();
                level3QCount = questionRepo.Filter(t => t.AssignmentId == q.id && q.type == "Level 3").Count();
            });

            assessmentList.ForEach(q =>
            {
                level1GCount=questionRepo.All().ToList().Where(t => t.AssignmentId == q.id && q.type == "Level 1").GroupBy(v => v.GroupId).ToList().Count();
                level2GCount = questionRepo.All().ToList().Where(t => t.AssignmentId == q.id && q.type == "Level 2").GroupBy(v => v.GroupId).ToList().Count();
                level3GCount = questionRepo.All().ToList().Where(t => t.AssignmentId == q.id && q.type == "Level 3").GroupBy(v => v.GroupId).ToList().Count();

            });


            var level1AssignMent = new UIAssessment() { Type = "Level 1", NoOfPending = level1PenCount.Value, NoOfGroup = level1GCount, NoOfQuestion= level1QCount, NoOfCompleted = level1ComCount.Value, NoOfParticipants = (level1ComCount.Value + level1PenCount.Value) };
            var level2AssignMent = new UIAssessment() { Type = "Level 2", NoOfPending = level2PenCount.Value, NoOfGroup = level2GCount, NoOfQuestion = level2QCount, NoOfCompleted = level2ComCount.Value, NoOfParticipants = (level2ComCount.Value + level2PenCount.Value) };
            var level3AssignMent = new UIAssessment() { Type = "Level 3", NoOfPending = level3PenCount.Value, NoOfGroup = level3GCount, NoOfQuestion = level3QCount, NoOfCompleted = level3ComCount.Value, NoOfParticipants = (level3ComCount.Value + level3PenCount.Value) };



            var lAssessment = new List<UIAssessment>();
            lAssessment.Add(level1AssignMent);
            lAssessment.Add(level2AssignMent);
            lAssessment.Add(level3AssignMent);

            
            return View(lAssessment);
        }

        public ActionResult AssignQuestion()
        {

            var lAssignment = new List<UIAssignQuestion>();

            using (var assessmentRepository = new Repository<Assessment>())
            {
                lAssignment = assessmentRepository.All().ToList().Select(q => new UIAssignQuestion(){ Id=q.Id, AssignmentId=q.Id, AssignMentName=q.Name  }).ToList();
            }

            using (var assessmentRepo = new Repository<Questions>())
            {
                lAssignment.ForEach(q =>
                {
                    q.NoOfQuestions = assessmentRepo.Filter(t => t.AssignmentId == q.Id).Count();
                });
            }


            var lQroup = new List<UIAssignGroup>();
            using (var assessmentRepository = new Repository<Group>())
            {
                lQroup = assessmentRepository.All().ToList().Select(q => new UIAssignGroup() { GroupId=q.Id, GroupName=q.Name }).ToList();
            }

            using (var assessmentRepo = new Repository<Questions>())
            {
                lQroup.ForEach(q => 
                {
                    q.uIAssignGroupQuestions = assessmentRepo.Filter(t => t.GroupId == q.GroupId).ToList().Select(s=> new UIAssignGroupQuestion() { GroupId=s.GroupId, QuestionId=s.QuestionCode, QuestionName=s.QuestionText }).ToList();

                });                   
            }          

            lAssignment[0].uIAssignGroups = new List<UIAssignGroup>();
            lAssignment[0].uIAssignGroups.AddRange(lQroup);
            
            return View(lAssignment);
        }      

        public JsonResult GetQuestionByAssignmentId(int id)
        {          

            var lAssignment = new UIAssignQuestion();

            var assignment = new Assessment();
            var lQuestions = new List<Questions>();

            using (var assessmentRepository = new Repository<Assessment>())
            {
                assignment = assessmentRepository.Filter(q => q.Id == id).FirstOrDefault();
            }

            var lQroup = new List<UIAssignGroup>();
            using (var assessmentRepository = new Repository<Group>())
            {
                lQroup = assessmentRepository.All().ToList().Select(q => new UIAssignGroup() { GroupId = q.Id, GroupName = q.Name }).ToList();
            }

            using (var assessmentRepo = new Repository<Questions>())
            {
                lQuestions = assessmentRepo.Filter(t => t.AssignmentId == assignment.Id).ToList();
            }

            lQroup.ForEach(q =>
            {
                q.uIAssignGroupQuestions = lQuestions.Where(t => t.GroupId == q.GroupId).ToList().Select(s => new UIAssignGroupQuestion() { GroupId = s.GroupId, QuestionId = s.QuestionCode, QuestionName = s.QuestionText }).ToList();
                if (q.uIAssignGroupQuestions.Count == 0)
                    q.GroupName = "";
            });

            return Json(lQroup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveQuestion(UIQuest uIQuest)
        {
            using (var rep = new Repository<Questions>())
            {
                var list = rep.Filter(q => uIQuest.QuestionId.Contains(q.QuestionCode)).ToList();
                list.ForEach(q =>
                {
                    q.AssignmentId = uIQuest.AssignmentId;
                    rep.Update(q);
                });
                rep.SaveChanges();              
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAssessMentById(int id)
        {
            using (var assessmentRepo = new Repository<Assessment>())
            {
                var deleteAssesment = assessmentRepo.Filter(q => q.Id == id).FirstOrDefault();
                if(deleteAssesment!=null)
                {
                    assessmentRepo.Delete(deleteAssesment);
                    assessmentRepo.SaveChanges();
                }
            }
            return Json("Deleted", JsonRequestBehavior.AllowGet);
        }
    }
}
