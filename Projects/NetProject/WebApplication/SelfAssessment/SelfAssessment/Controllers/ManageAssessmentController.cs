using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
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
            var sector = new List<SelectListItem>();
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            using (var repository = new Repository<Sector>())
            {
                sector = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
                lAssessment = repository.AssessmentContext.assessments.ToList();
            }
            sector.Insert(0, firstItem);
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = new List<SelectListItem>();
            return View(lAssessment);
        }

        [HttpGet]
        public JsonResult GetSubSector(int id)
        {
            var firstItem = new SelectListItem() { Text = Utilities.All, Value = "0", Selected = true };
            var subSector = new List<SelectListItem>();                      
            using (var repository = new Repository<SubSector>())
            {
                subSector = repository.Filter(q => q.SectorId == id).Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
            }
            subSector.Insert(0, firstItem);
            return Json(subSector, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAssessment(Assessment assessment)
        {          
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
                        updateAssessment.Sector = assessment.Sector;
                        updateAssessment.SubSector = assessment.SubSector;
                        assessment.UpdateDate = DateTime.Now;
                        assessmentRepo.Update(updateAssessment);
                    }
                }
                else
                {
                    assessment.CreateDate = DateTime.Now;
                    assessmentRepo.Create(assessment);
                }                
                assessmentRepo.SaveChanges();
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
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
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            var subSector = new List<SelectListItem>();
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
            var cities = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();

            var assessMent = new List<SelectListItem>();

            using (var repo = new Repository<Assessment>())
            {
                assessMent = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                subSector =  repo.AssessmentContext.subSectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
                sector = repo.AssessmentContext.sectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
                states = repo.AssessmentContext.states.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
                revenue = repo.AssessmentContext.revenues.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                typeOfService = repo.AssessmentContext.serviceTypes.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                cities = repo.AssessmentContext.cities.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
            }

            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = Utilities.OthersValue };

            sector.Insert(0, firstItem);
            subSector.Insert(0, firstItem);
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            cities.Insert(0, firstItem);
            states.Insert(0, firstItem);
            assessMent.Insert(0, firstItem);

            sector.Add(lastItem);
            subSector.Add(lastItem);

            ViewBag.AssessMent = assessMent;
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = subSector;
            ViewBag.City = cities;
            ViewBag.State = states;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;
            return View();
        }

        public JsonResult MoveToNextLevel(int id)
        {

            OrganizationLevelHistory tempOrg = null;
            using (var repo = new Repository<Organization>())
            {
                var org = repo.Filter(q => q.Id == id).FirstOrDefault();
                tempOrg = new OrganizationLevelHistory() { Level = org.CurrentAssignmentType, OrgId = org.Id, Status = Utilities.AssessmentCompletedStatus, PromoteDate = DateTime.Now };
                if (org != null)
                {
                    if (org.CurrentAssignmentType == Utilities.AssessmentTypeLevel1)
                        org.CurrentAssignmentType = Utilities.AssessmentTypeLevel2;
                    else
                        org.CurrentAssignmentType = Utilities.AssessmentTypeLevel3;

                    org.CurrentAssignmentStatus = Utilities.AssessmentPendingStatus;
                    repo.Update(org);
                    repo.SaveChanges();
                }
            }

            using (var rep = new Repository<OrganizationLevelHistory>())
            {
                rep.Create(tempOrg);
                rep.SaveChanges();
            }

            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignOrganizationByFilter(Organization org)
        {
            var lmodel = new List<UIOrganization>();
            var listOrganization = new List<Organization>();
            using (var repo = new Repository<Organization>())
            {
                listOrganization = repo.AssessmentContext.UserInfo.Join(repo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => u).Where(q=> q.CurrentAssignmentType == org.CurrentAssignmentType).ToList();
            }

            if (org.CityId > 0)
                listOrganization = listOrganization.Where(q => q.CityId == org.CityId).ToList();
            if (org.StateId > 0)
                listOrganization = listOrganization.Where(q => q.StateId == org.StateId).ToList();
            if (org.SectorId > 0 && org.SectorId != 1000)
                listOrganization = listOrganization.Where(q => q.SectorId == org.SectorId).ToList();
            if (org.RevenueId > 0)
                listOrganization = listOrganization.Where(q => q.RevenueId == org.RevenueId).ToList();
            if (org.SubSectorId > 0 && org.SubSectorId != 1000)
                listOrganization = listOrganization.Where(q => q.SubSectorId == org.SubSectorId).ToList();
            if(org.TypeOfServiceId > 0)
                listOrganization = listOrganization.Where(q => q.TypeOfServiceId == org.TypeOfServiceId).ToList();

            var city = new Repository<City>();

            try
            {
                listOrganization.ForEach(q =>
                {
                    UIOrganization model = new UIOrganization();
                    model.Id = q.Id;
                    model.Name = q.Name;
                    model.City = city.Filter(c => c.Id == q.CityId).FirstOrDefault().CityName;
                    model.Revenue = city.AssessmentContext.revenues.FirstOrDefault(r => r.Id == q.RevenueId).Name;
                    model.Sector = (q.SectorId == 0 || q.SectorId == 1000) ? Utilities.Others : city.AssessmentContext.sectors.FirstOrDefault(r => r.Id == q.SectorId).SectorName;
                    model.SubSector = (q.SectorId == 0 || q.SubSectorId == 1000) ? Utilities.Others : city.AssessmentContext.subSectors.FirstOrDefault(r => r.Id == q.SubSectorId).SubSectorName;
                    model.State = q.StateId > 0 ? city.AssessmentContext.states.FirstOrDefault(s => s.Id == q.StateId).StateName : "";
                    model.TypeOfService = q.TypeOfServiceId > 0 ? city.AssessmentContext.serviceTypes.FirstOrDefault(ty => ty.Id == q.TypeOfServiceId).Name : "";
                    model.Type = org.CurrentAssignmentType;
                    lmodel.Add(model);
                });
            }
            catch (Exception ex)
            {
                //throw;
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

                level1PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel1)?.count??0;
                level2PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel2)?.count??0;
                level3PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel3)?.count??0;               
            }

            using (var repo = new Repository<OrganizationLevelHistory>())
            {
                var completedCount = repo.All().ToList().GroupBy(q => q.Level).Select(q => new { count = q.Count(), Type = q.Key }).ToList();

                level1ComCount = completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel1)?.count??0;
                level2ComCount = completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel2)?.count??0;
                level3ComCount = completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel3)?.count??0;
            }

            var questionRepo = new Repository<Questions>();
            var assessmentRepo = new Repository<AssessmentLevelMapping>();
            var assessmentList=assessmentRepo.All().ToList().GroupBy(q => new { q.AssessmentId, q.Level}).Select(q => new { id = q.Key.AssessmentId, type=q.Key.Level }).ToList();


            var groupMap = assessmentRepo.All().GroupBy(q => new { q.AssessmentId, q.Level }).ToList().Select(t => t).ToList();
            foreach (var grp in groupMap)
            {
                if (grp.Key.Level == Utilities.AssessmentTypeLevel1)
                {
                    level1QCount = grp.Count();
                    level1GCount = grp.GroupBy(q => q.GroupId).Count();
                }

                if (grp.Key.Level == Utilities.AssessmentTypeLevel2)
                {
                    level2QCount = grp.Count();
                    level2GCount = grp.GroupBy(q => q.GroupId).Count();
                }

                if (grp.Key.Level == Utilities.AssessmentTypeLevel3)
                {
                    level3QCount = grp.Count();
                    level3GCount = grp.GroupBy(q => q.GroupId).Count();
                }
            }         


            var level1AssignMent = new UIAssessment() { Type = Utilities.AssessmentTypeLevel1, NoOfPending = level1PenCount.Value, NoOfGroup = level1GCount, NoOfQuestion= level1QCount, NoOfCompleted = level1ComCount.Value, NoOfParticipants = (level1ComCount.Value + level1PenCount.Value) };
            var level2AssignMent = new UIAssessment() { Type = Utilities.AssessmentTypeLevel2, NoOfPending = level2PenCount.Value, NoOfGroup = level2GCount, NoOfQuestion = level2QCount, NoOfCompleted = level2ComCount.Value, NoOfParticipants = (level2ComCount.Value + level2PenCount.Value) };
            var level3AssignMent = new UIAssessment() { Type = Utilities.AssessmentTypeLevel3, NoOfPending = level3PenCount.Value, NoOfGroup = level3GCount, NoOfQuestion = level3QCount, NoOfCompleted = level3ComCount.Value, NoOfParticipants = (level3ComCount.Value + level3PenCount.Value) };



            var lAssessment = new List<UIAssessment>();
            lAssessment.Add(level1AssignMent);
            lAssessment.Add(level2AssignMent);
            lAssessment.Add(level3AssignMent);

            
            return View(lAssessment);
        }

        public ActionResult AssignQuestion()
        {
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            var assessMent = new List<SelectListItem>();

            using (var assessment = new Repository<Assessment>())
            {
                assessMent = assessment.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
            }
            assessMent.Insert(0, firstItem);
            ViewBag.AssessMent = assessMent;


            var lAssignment = new List<UIAssignQuestion>();

            using (var levelMapping = new Repository<AssessmentLevelMapping>())
            {
                var groupMap = levelMapping.All().GroupBy(q => new { q.AssessmentId, q.Level }).ToList().Select(t=> t).ToList();
                foreach(var grp in groupMap)
                {
                    var uiassign = new UIAssignQuestion();
                    int questionId=grp.First().QuestionId;
                    uiassign.Id = levelMapping.AssessmentContext.questions.Where(t => t.Id == questionId).First().Id;
                    uiassign.AssignMentName = levelMapping.AssessmentContext.assessments.Where(t => t.Id == grp.Key.AssessmentId).First().Name;
                    uiassign.Level = grp.Key.Level;
                    uiassign.AssignmentId = grp.Key.AssessmentId;
                    uiassign.NoOfQuestions = grp.Count();
                    uiassign.NoOfGroup = grp.GroupBy(q => q.GroupId).Count();

                    lAssignment.Add(uiassign);
                }

            }

            var lQroup = new List<UIAssignGroup>();
            using (var assessmentRepository = new Repository<Group>())
            {
                lQroup = assessmentRepository.All().ToList().Select(q => new UIAssignGroup() { GroupId=q.Id, GroupName=q.Name }).ToList();

                lQroup.ForEach(q =>
                {
                    q.uIAssignGroupQuestions = assessmentRepository.AssessmentContext.questions.Where(t => t.GroupId == q.GroupId).ToList().Select(s => new UIAssignGroupQuestion() { GroupId = s.GroupId, QuestionId = s.QuestionCode, QuestionName = s.QuestionText }).ToList();

                });
            }

            if(lAssignment.Count ==0)
                lAssignment.Add(new UIAssignQuestion());
            lAssignment[0].uIAssignGroups = new List<UIAssignGroup>();
            lAssignment[0].uIAssignGroups.AddRange(lQroup);
            
            return View(lAssignment);
        }      

        public JsonResult GetQuestionByAssignmentId(int id,string level)
        {

            var lAssignment = new List<AssessmentLevelMapping>();
            var lQuestions = new List<Questions>();

            using (var assessmentRepository = new Repository<AssessmentLevelMapping>())
            {
                lAssignment = assessmentRepository.Filter(q => q.AssessmentId == id && q.Level == level).ToList();
            }

            var lQroup = new List<UIAssignGroup>();
            using (var assessmentRepository = new Repository<Group>())
            {
                lAssignment.ForEach(q =>
                {
                    if(lQroup.FirstOrDefault(t=> t.GroupId == q.GroupId)==null)
                        lQroup.AddRange(assessmentRepository.Filter(v => v.Id == q.GroupId).Select(t => new UIAssignGroup() { GroupId = q.GroupId, GroupName = t.Name }).ToList());

                    lQuestions.Add(assessmentRepository.AssessmentContext.questions.FirstOrDefault(v => v.Id == q.QuestionId));
                });                
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
            var removelist = new List<AssessmentLevelMapping>();

            using (var rep = new Repository<Questions>())
            {

                var mapping = new Repository<AssessmentLevelMapping>();

                var list = rep.Filter(q => uIQuest.QuestionId.Contains(q.QuestionCode)).Select(q => new QuestionGroup() { QuestionId=q.Id, GroupId=q.GroupId }).ToList();

                var mappingList = mapping.Filter(q => q.AssessmentId == uIQuest.AssessmentId && q.Level == uIQuest.Level).Select(q => new QuestionGroup() { QuestionId=q.QuestionId, GroupId = q.GroupId, MapperId=q.Id }).ToList();

                var addingMapper = list.Except(mappingList, (t, t1) => t.QuestionId == t1.QuestionId).ToList();
                var removingMapper = mappingList.Except(list, (t, t1) => t.QuestionId == t1.QuestionId).ToList();

                addingMapper.ForEach(q =>
                {
                    if (mapping.Filter(t => t.QuestionId == q.QuestionId).FirstOrDefault() == null)
                    {
                        var assesmentLevel = new AssessmentLevelMapping()
                        {
                            AssessmentId = uIQuest.AssessmentId,
                            Level = uIQuest.Level,
                            GroupId = q.GroupId,
                            QuestionId = q.QuestionId,
                            Assessments = mapping.AssessmentContext.assessments.FirstOrDefault(v => v.Id == uIQuest.AssessmentId),
                            Groups = mapping.AssessmentContext.groups.FirstOrDefault(v=> v.Id == q.GroupId),
                            Questiones = mapping.AssessmentContext.questions.FirstOrDefault(v=> v.Id == q.QuestionId)                            
                        };
                        mapping.Create(assesmentLevel);
                    }
                });

                removingMapper.ForEach(q =>
                {
                    var removeMapper = mapping.Filter(t => t.Id == q.MapperId).FirstOrDefault();
                    if (removeMapper != null)
                    {
                        removelist.Add(removeMapper);
                    }
                });

                if(removelist.Count > 0)
                    mapping.DeleteRange(removelist);

                mapping.SaveChanges();
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
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
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }
    }
}
