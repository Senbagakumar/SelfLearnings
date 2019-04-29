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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class ManageAssessmentController : AdminBaseController
    {
        // GET: ManageAssessment
        public ActionResult Index()
        {
            var lAssessment = new List<Assessment>();
            var ulAssessment = new List<UAssessment>();
            var sector = new List<SelectListItem>();
            //var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            using (var repository = new Repository<Sector>())
            {
                sector = BaseHelper.GetSectorValues();
                lAssessment = repository.AssessmentContext.assessments.ToList();
            }
            int i = 1;
            lAssessment.ForEach(t =>
            {
                ulAssessment.Add(new UAssessment() { AdminEmail=t.AdminEmail, AssessmentFormat=t.AssessmentFormat, AssessmentType=t.AssessmentType,CreateDate=t.CreateDate
                , Description=t.Description, EndMessage=t.EndMessage, Id=t.Id, Name=t.Name,Sector=t.Sector, SubSector=t.SubSector, ShowProgressBar=t.ShowProgressBar,
                ShowWelcomeScreen=t.ShowWelcomeScreen, WelcomeMessage=t.WelcomeMessage, OrderId=i});
                i = i + 1;
            });
            //sector.Insert(0, firstItem);
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = new List<SelectListItem>();
            return View(ulAssessment);
        }

        [HttpGet]
        public JsonResult GetSubSector(int id)
        {
            var subSector = BaseHelper.GetSubSectorValues(id);
            return Json(subSector, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection userValues)
        {
            var assessment = new Assessment();

            assessment.Id = !string.IsNullOrWhiteSpace(userValues["txt-id-input"]) ? Convert.ToInt16(userValues["txt-id-input"]) : 0;
            assessment.AdminEmail = userValues["txt-AEmail-input"];
            assessment.AssessmentFormat = userValues["st-AFormat"];
            assessment.Description = userValues["txt-ADes-input"];
            assessment.WelcomeMessage = userValues["txt-AWMsg-input"];
            assessment.EndMessage = userValues["txt-AEMsg-input"];
            assessment.Name = userValues["txt-AName-input"];
            assessment.ShowProgressBar = userValues["chk-SWScreen"] == "on" ? true : false;
            assessment.ShowWelcomeScreen = userValues["chk-SPbar"] == "on" ? true : false;
            assessment.Sector = Convert.ToInt16(userValues["Sector"]);
            assessment.SubSector = Convert.ToInt16(userValues["SubSector"]);
                
            
            using (var assessmentRepo = new Repository<Assessment>())
            {
                if (assessment.Id != 0)
                {
                    var updateAssessment = assessmentRepo.Filter(q => q.Id == assessment.Id).FirstOrDefault();
                    if (updateAssessment != null)
                    {
                        updateAssessment.AdminEmail = assessment.AdminEmail;
                        //updateAssessment.AllowPublicRegistration = assessment.AllowPublicRegistration;
                        updateAssessment.AssessmentFormat = assessment.AssessmentFormat;
                        updateAssessment.Description = assessment.Description;
                        //updateAssessment.EndDate = assessment.EndDate;
                        updateAssessment.EndMessage = assessment.EndMessage;
                        //updateAssessment.LineAssessmentPublically = assessment.LineAssessmentPublically;
                        updateAssessment.Name = assessment.Name;
                        //updateAssessment.ParticipantsMayPrintAnswer = assessment.ParticipantsMayPrintAnswer;
                        //updateAssessment.PublicStatistics = assessment.PublicStatistics;
                        //updateAssessment.ShowNextButton = assessment.ShowNextButton;
                        updateAssessment.ShowProgressBar = assessment.ShowProgressBar;
                        //updateAssessment.ShowQuestionIndex = assessment.ShowQuestionIndex;
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
            //return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
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
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
           
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();

            var assessMent = new List<SelectListItem>();

            using (var repo = new Repository<Assessment>())
            {
                assessMent = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                //sector = repo.AssessmentContext.sectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
                sector = BaseHelper.GetSectorValues();
                states = repo.AssessmentContext.states.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
                revenue = repo.AssessmentContext.revenues.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                typeOfService = repo.AssessmentContext.serviceTypes.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
            }

            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = Utilities.OthersValue };

            //sector.Insert(0, firstItem);           
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            states.Insert(0, firstItem);
            assessMent.Insert(0, firstItem);

            //sector.Add(lastItem);

            ViewBag.AssessMent = assessMent;
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = new List<SelectListItem>();
            ViewBag.City = new List<SelectListItem>();
            ViewBag.State = states;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;
            return View();
        }

        public JsonResult MoveToNextLevel(int id, string level)
        {
            using (var repo = new Repository<Organization>())
            {
                var org = repo.Filter(q => q.Id == id).FirstOrDefault();
                string type = org.CurrentAssignmentType;

                if(org.CurrentAssignmentType != level)//&& org.CurrentAssignmentStatus == Utilities.AssessmentCompletedStatus
                    return Json("IsAlreadyMoved", JsonRequestBehavior.AllowGet);

                if (org != null)
                {
                    if (org.CurrentAssignmentType == Utilities.AssessmentTypeLevel1)
                        org.CurrentAssignmentType = Utilities.AssessmentTypeLevel2;
                    else
                        org.CurrentAssignmentType = Utilities.AssessmentTypeLevel3;

                    org.CurrentAssignmentStatus = Utilities.AssessmentPendingStatus;
                    repo.Update(org);

                    repo.AssessmentContext.organizationLevelHistories.Add(new OrganizationLevelHistory()
                    {
                        Level = type, OrgId = id, PromoteDate = DateTime.Now, Status = Utilities.AssessmentCompletedStatus, Id = id,
                        AssessmentId=org.AssessmentId, CityId=org.CityId, RevenueId=org.RevenueId, SectorId=org.SectorId,
                        StateId=org.StateId, SubSectorId=org.SubSectorId, TypeId=org.TypeId, TypeOfServiceId=org.TypeOfServiceId
                    });
                    repo.SaveChanges();

                    Repository<Template> template = new Repository<Template>();
                    var registrationTemplate = template.Filter(q => q.Name.StartsWith(Utilities.MoveUserTemplate)).FirstOrDefault();
                    if (registrationTemplate != null && !string.IsNullOrWhiteSpace(registrationTemplate.Description))
                        RegistrationSendMail.SendMail(registrationTemplate.Description, Utilities.MoveUserSubject, org.Email);


                }
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignOrganizationByFilter(Organization org)
        {
            var lmodel = new List<UIOrganization>();
            try
            {
                lmodel = Helper.AssignOrganizationByFilter(org);
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
                var pendingCount = repo.All().ToList().GroupBy(q => new { q.CurrentAssignmentType, q.CurrentAssignmentStatus }).Select(q => new { count = q.Count(), Type = q.Key.CurrentAssignmentType, status=q.Key.CurrentAssignmentStatus }).ToList();

                level1PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel1 && t.status == Utilities.AssessmentPendingStatus)?.count??0;
                level2PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel2 && t.status == Utilities.AssessmentPendingStatus)?.count??0;
                level3PenCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel3 && t.status == Utilities.AssessmentPendingStatus)?.count??0;

                level1ComCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel1 && t.status == Utilities.AssessmentCompletedStatus)?.count ?? 0;
                level2ComCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel2 && t.status == Utilities.AssessmentCompletedStatus)?.count ?? 0;
                level3ComCount = pendingCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel3 && t.status == Utilities.AssessmentCompletedStatus)?.count ?? 0;

            }

            using (var repo = new Repository<OrganizationLevelHistory>())
            {
                var completedCount = repo.All().ToList().GroupBy(q => q.Level).Select(q => new { count = q.Count(), Type = q.Key }).ToList();

                level1ComCount += completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel1)?.count??0;
                level2ComCount += completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel2)?.count??0;
                level3ComCount += completedCount.FirstOrDefault(t => t.Type == Utilities.AssessmentTypeLevel3)?.count??0;
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

            if(uIQuest.QuestionId == null)
                return Json(Utilities.Failiure, JsonRequestBehavior.AllowGet);

            try
            {
                using (var rep = new Repository<Questions>())
                {

                    var mapping = new Repository<AssessmentLevelMapping>();

                    var list = rep.Filter(q => uIQuest.QuestionId.Contains(q.QuestionCode)).Select(q => new QuestionGroup() { QuestionId = q.Id, GroupId = q.GroupId }).ToList();

                    var mappingList = mapping.Filter(q => q.AssessmentId == uIQuest.AssessmentId && q.Level == uIQuest.Level).Select(q => new QuestionGroup() { QuestionId = q.QuestionId, GroupId = q.GroupId, MapperId = q.Id }).ToList();

                    var addingMapper = list.Except(mappingList, (t, t1) => t.QuestionId == t1.QuestionId).ToList();
                    var removingMapper = mappingList.Except(list, (t, t1) => t.QuestionId == t1.QuestionId).ToList();

                    addingMapper.ForEach(q =>
                    {
                        if (mapping.Filter(t => t.QuestionId == q.QuestionId && t.AssessmentId == uIQuest.AssessmentId).FirstOrDefault() == null)
                        {
                            var assesmentLevel = new AssessmentLevelMapping()
                            {
                                AssessmentId = uIQuest.AssessmentId,
                                Level = uIQuest.Level,
                                GroupId = q.GroupId,
                                QuestionId = q.QuestionId,
                                Assessments = mapping.AssessmentContext.assessments.FirstOrDefault(v => v.Id == uIQuest.AssessmentId),
                                Groups = mapping.AssessmentContext.groups.FirstOrDefault(v => v.Id == q.GroupId),
                                Questiones = mapping.AssessmentContext.questions.FirstOrDefault(v => v.Id == q.QuestionId)
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

                    if (removelist.Count > 0)
                        mapping.DeleteRange(removelist);

                    mapping.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCities(int id)
        {
            var cities = new List<SelectListItem>();
            //var firstItem = new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true };            

            using (var repository = new Repository<City>())
            {
                cities = repository.Filter(q => q.StateId == id).Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
            }
            //subSector.Insert(0, firstItem);
            return Json(cities, JsonRequestBehavior.AllowGet);
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
