using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using SelfAssessment.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;

namespace SelfAssessment.Controllers
{
    public class ManageUserController : BaseController
    {      
        private GroupQuizManager groupQuizManager;
        private QuizManager quizManager;

        public ManageUserController()
        {
            //this.groupQuizManager = new GroupQuizManager(this.UserId);
        }
        // GET: ManageUser
        public ActionResult Index()
        {
            var uiOrganization = new UIOrganization();

            using (var org = new Repository<Organization>())
            {
                var selectOrg = org.Filter(q => q.Id == this.UserId).FirstOrDefault();

                uiOrganization = org.AssessmentContext.UserInfo.Where(q => q.Id == this.UserId).ToList().Join(org.AssessmentContext.cities, og => og.CityId, cy => cy.Id, (og, cy) => new { og, cy }).
                    Join(org.AssessmentContext.states, s => s.og.StateId, st => st.Id, (s, st) => new { s, st, }).Join(org.AssessmentContext.serviceTypes,
                    sty => sty.s.og.TypeOfServiceId, stype => stype.Id, (sty,stype)=> new UIOrganization()
                    {
                        Name = sty.s.og.Name,
                        Address = sty.s.og.Address,
                        City = sty.s.cy.CityName,
                        Email = sty.s.og.Email,
                        MobileNo = sty.s.og.MobileNo,
                        State = sty.st.StateName,
                        Designation = sty.s.og.Designation,
                        TypeOfService = stype.Name
                    }).FirstOrDefault();
            }           
            return View(uiOrganization);
        }


        private UIAssessment AssessmentMapping(string type,int noOfGroup,int noOfQuestion,string status)
        {
            return new UIAssessment()
            {                
                Type = type,
                NoOfGroup = noOfGroup,
                NoOfQuestion = noOfQuestion,
                AssignmentStatus = status
            };
        }

        public ActionResult ListAssessment()
        {
            var lAssessment = new List<UIAssessment>();
            var userInfo = new Repository<Organization>();
            int sectorValue = int.Parse(SelfAssessment.Business.Utilities.SectorValue);
            var user = userInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
            if (user != null)
            {
                var assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(q => q.Sector == user.SectorId);
                if (assessment == null)
                    assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(t => t.Sector == sectorValue);

                ViewBag.WelComeMessage = assessment.WelcomeMessage;
                ViewBag.Description = assessment.Description;
                ViewBag.Url = assessment.AssessmentFormat == "2" ? "QuizGroup" : assessment.AssessmentFormat == "1" ? "QuizOne" : "QuizGroup";
                ViewBag.AssessmentName = assessment.Name;

                if (assessment.Id != 0)
                {
                    //Completion Level details
                    //var compList = userInfo.AssessmentContext.organizationLevelHistories.Where(q => q.OrgId == this.UserId).GroupBy(t=>t.OrgId).ToList();
                    //if(compList !=null && compList.Count > 0)
                    //{
                        //compList.ForEach(q =>
                        //{
                        
                        var completedList = userInfo.AssessmentContext.assessmentLevelMappings.Where(t => t.AssessmentId == assessment.Id).GroupBy(t=>t.Level).ToList();
                        completedList.ForEach(v => 
                        {
                            if(v.Key == user.CurrentAssignmentType)
                                lAssessment.Add(AssessmentMapping(v.Key, v.GroupBy(t => t.GroupId).Count(), v.Count(), user.CurrentAssignmentStatus));
                            else
                                lAssessment.Add(AssessmentMapping(v.Key, v.GroupBy(t => t.GroupId).Count(), v.Count(), Utilities.AssessmentCompletedStatus));
                        });
                           
                       // });
                   // }

                    //Current Level details
                    //var mappings = userInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == assessment.Id && q.Level == user.CurrentAssignmentType).ToList();
                    //if (mappings != null)
                    //{
                    //    lAssessment.Add(AssessmentMapping(user.CurrentAssignmentType, mappings.GroupBy(q => q.GroupId).Count(), mappings.Count(), user.CurrentAssignmentStatus));                      
                    //}
                }
            }
            return View(lAssessment);
        }
        public ActionResult CustomerAssessment()
        {

            var userInfo = new Repository<Organization>();
            int sectorValue = int.Parse(SelfAssessment.Business.Utilities.SectorValue);
            var user = userInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
            if (user != null)
            {
                var assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(q => q.Sector == user.SectorId);
                if (assessment == null)
                    assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(t => t.Sector == sectorValue);
                ViewBag.AssessmentName = assessment.Name;
            }
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAssessment(QuestionOnePost question)
        {
            // Get Report
            string companyName=string.Empty, address=string.Empty, contactName=string.Empty;
            var userInfo = new Repository<Organization>();
            var user = userInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
            if (user != null)
            {
                companyName = user.Name;
                address = user.Address;
                contactName = user.ContactName;
            }

            if (question.hdnaction != string.Empty && (question.hdnaction.Contains("PDF") || question.hdnaction.Contains("CSV")))
            {
                var level = question.hdnaction.Split('-')[1];
                if(question.hdnaction.Contains("PDF"))
                {
                    return PdfExport(companyName,address,contactName,level);
                }
                else
                {
                    return CsvExport(companyName, address, contactName, level);
                }
            }
            else
            {
                GroupQuiz groupQuiz = null;
                int groupId = int.Parse(question.QInfo);
                if (question.hdnaction == "Previous")
                {
                    groupId = groupId - 1;
                    if (groupId <= 0)
                        groupId = 1;
                }
                else
                {
                    groupId = groupId + 1;
                }
                this.groupQuizManager = new GroupQuizManager(this.UserId);
                this.groupQuizManager.AllQuestions = (List<GroupQuiz>)Session["AllGroupQuestions"];
                if (this.groupQuizManager.MoveToNextGroup(groupId))
                {
                    groupQuiz = this.groupQuizManager.LoadQuiz(groupId);
                }
                else
                {
                    groupQuiz = this.groupQuizManager.LoadQuiz(--groupId);
                }

                return PartialView("QuizGroupPartial", groupQuiz);
            }
        }

        public ActionResult GetFirstGroup(string id)
        {
            return PartialView("QuizGroupPartial", GetGroupQuiz(id));
        }
        public ActionResult AssessmentReport()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            ViewBag.Error = string.Empty;
            return View();
        }            

        [HttpPost]
        public JsonResult ChangePassword(ChangePassword changePassword)
        {
            string message = string.Empty;
            var sc = new StringCipher();
            var org = new Repository<Organization>();
            var user = org.Filter(q => q.Id == this.UserId).FirstOrDefault();

            if(user!=null && user.TempPassword == changePassword.OldPassword || StringCipher.Decrypt(user.Password) == changePassword.OldPassword)
            {
                user.Password = StringCipher.Encrypt(changePassword.NewPassword);
                user.TempPassword = string.Empty;
                org.Update(user);
                org.SaveChanges();
                message = "Successfully Updated";

                Repository<Template> template = new Repository<Template>();
                var registrationTemplate = template.Filter(q => q.Name.StartsWith(Utilities.ChangePasswordTemplate)).FirstOrDefault();

                if(registrationTemplate!=null && !string.IsNullOrWhiteSpace(registrationTemplate.Description))
                    RegistrationSendMail.SendMail(registrationTemplate.Description, Utilities.ChangePasswordSubject, user.Email,user.Name);

            }
            else
            {
                message = "Please Enter Correct Details";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        public FileResult PdfExport(string companyName,string address, string contactName, string level = null)
        {
            Utilities.DeleteOldFiles(Server.MapPath("~/Downloads"));
            var dt = Utilities.GetReport(this.UserId,level);
            var dynamicName = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var fileName = Server.MapPath($"~/Downloads/{dynamicName}.pdf");
            Utilities.CreatePdf(fileName, dt, companyName, address, contactName);
            return File(fileName, "application/pdf", $"{dynamicName}.pdf");

        }

        public FileResult CsvExport(string companyName, string address, string contactName, string level = null)
        {
            Utilities.DeleteOldFiles(Server.MapPath("~/Downloads"));
            var dt = Utilities.GetReport(this.UserId,level);
            var dynamicName = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var fileName = Server.MapPath($"~/Downloads/{dynamicName}.csv");
            var s=Utilities.CreateCsv(dt, companyName, address, contactName);
            System.IO.File.AppendAllText(fileName, s);
            return File(fileName, "application/text", $"{dynamicName}.csv");

        }

        public ActionResult QuizOne(string id = null)
        {
            int qid = 1;
            this.quizManager = new QuizManager(this.UserId);
            var listOfQuestions = this.quizManager.GetAllQuestions();
            Session["AllQuestions"] = this.quizManager.AllQuestions = listOfQuestions;

            if (!string.IsNullOrWhiteSpace(id))
                qid = listOfQuestions.Count;

            var question = this.quizManager.LoadQuiz(qid);
            ViewBag.AssessmentName = question.AssessmentName;
            return View(question);
        }

        [HttpPost]
        public ActionResult QuizOne(FormCollection questionCode)
        {
            bool isAction = false;
            var quiz = new QuestionQuiz();

            foreach (string user in questionCode.Keys)
            {
                if (user == "hdnaction")
                {
                    isAction = questionCode["hdnaction"].Equals("Previous", StringComparison.OrdinalIgnoreCase) ? true : false;
                    continue;
                }
                if (user == "Questions.UIQId")
                {
                    quiz.UIQId = Convert.ToInt16(questionCode["Questions.UIQId"]);
                    continue;
                }

                string[] values = questionCode[user].Split('~');
                quiz.GroupId = Convert.ToInt16(values[0]);
                quiz.QuestionId = Convert.ToInt16(values[1]);
                quiz.UserOptionId = Convert.ToInt16(values[2]);
                quiz.UIQId = Convert.ToInt16(values[3]);

            }

            int qId = 0;

            this.quizManager = new QuizManager(this.UserId);
            this.quizManager.AllQuestions = (List<QuestionAnswer>)Session["AllQuestions"];
            bool isAnswered = quiz.UserOptionId > 0 ? true : false;

            this.quizManager.SaveAnswer(quiz);
            bool isMandatory = true;
            isMandatory = this.quizManager.IsMandatoryQuestion(quiz.UIQId);

            if(isMandatory && quiz.UserOptionId == 0)
            {
                qId = quiz.UIQId;
                ViewBag.Msg = "Please Fill the Mandatroy Questions";
            }
            else
            {
                qId = quiz.UIQId;

                if (!isAction)
                    qId = qId + 1;
                else
                    qId = qId - 1;

                if (qId == 0)
                    qId = 1;
            }

                
            if (this.quizManager.MoveToNextQuestion(qId))
            {
                var question = this.quizManager.LoadQuiz(qId);
                return View(question);
            }

            return RedirectToAction("ShowResults", new { id=qId, origin= "Questions" });
        }
        public ActionResult ShowResults(string origin)
        {
            ViewBag.Origin = origin;
            return View();
        }

        public ActionResult EndMsg()
        {
            var uInfo= new Repository<Organization>();
            int sectorValue = int.Parse(Business.Utilities.SectorValue);
            var userSectorId = uInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
            var assessmentDetails = uInfo.AssessmentContext.assessments.Where(q => q.Sector == userSectorId.SectorId).FirstOrDefault();
            if (assessmentDetails == null || assessmentDetails.Sector == 0)
                assessmentDetails = uInfo.AssessmentContext.assessments.Where(q => q.Sector == sectorValue).FirstOrDefault();

            ViewBag.EndMessage = assessmentDetails.EndMessage;
            return View();
        }

        public JsonResult Complete()
        {
            CompleteAssignment();
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuizGroup(string id = null)
        {
            GroupQuiz groupQuiz = null;
            if (string.IsNullOrWhiteSpace(id))
            {
                groupQuiz = GetGroupQuiz();
            }
            else
            {
                groupQuiz = GetLastGroupQuiz();
            }
            ViewBag.AssessmentName = groupQuiz.AssessmentName;
            return View(groupQuiz);
        }

        [HttpPost]
        public ActionResult QuizGroup(FormCollection questionCode)
        {
            int groupId = 0;
            //string result = string.Empty;
            //if (questionCode["Origin"] != null && questionCode["Origin"].Equals("Result"))
            //{
            //    groupId = int.Parse(questionCode["Id"].ToString());
            //    var groupQuiz = GetGroupQuiz(--groupId);
            //    return RedirectToAction("QuizGroup", new { quiz = groupQuiz });
            //}
            //else
            //{


                bool isAction = false;
                var groupQuiz = new List<QuestionQuiz>();
                foreach (string user in questionCode.Keys)
                {
                    if (user == "hdnaction")
                    {
                        isAction = questionCode["hdnaction"].Equals("Previous", StringComparison.OrdinalIgnoreCase) ? true : false;
                        continue;
                    }
                    if (user == "UIGroupId")
                        continue;

                    string[] values = questionCode[user].Split('~');
                    var quiz = new QuestionQuiz();
                    quiz.GroupId = Convert.ToInt16(values[0]);
                    quiz.QuestionId = Convert.ToInt16(values[1]);
                    quiz.UserOptionId = Convert.ToInt16(values[2]);
                    quiz.UIGroupId = Convert.ToInt16(values[3]);
                    groupQuiz.Add(quiz);
                }
                this.groupQuizManager = new GroupQuizManager(this.UserId);
                if (groupQuiz.Count == 0)
                    groupId = int.Parse(questionCode[1]);
                else
                    groupId = groupQuiz[0].GroupId;

                this.groupQuizManager.AllQuestions = (List<GroupQuiz>)Session["AllGroupQuestions"];
                var lQuestions = this.groupQuizManager.GetMandatoryQuestion(groupId);

                this.groupQuizManager.SaveAnswer(groupQuiz);

                bool isMandatoryAnswerd = true;
                foreach (var q in lQuestions)
                {
                    if (groupQuiz == null || groupQuiz.Count == 0 || !groupQuiz.Exists(t => t.QuestionId == q.Questions.QuestionId))
                    {
                        ViewBag.AssessmentName = q.AssessmentName;
                        isMandatoryAnswerd = false;
                        break;
                    }
                }

                if (isMandatoryAnswerd)
                {
                    groupId = groupQuiz.First().UIGroupId;

                    if (!isAction)
                        groupId = groupId + 1;
                    else
                    {
                        groupId = Convert.ToInt16(questionCode["UIGroupId"].ToString());
                        groupId = groupId - 1;
                    }

                    if (groupId == 0)
                        groupId = 1;
                }
                else
                {
                    groupId = groupQuiz.Count > 0 ? groupQuiz.First().UIGroupId : groupId;
                    ViewBag.Msg = "Please Fill the Mandatroy Questions";
                }

                this.groupQuizManager.AllQuestions = (List<GroupQuiz>)Session["AllGroupQuestions"];
                if (this.groupQuizManager.MoveToNextGroup(groupId))
                {
                    var question = this.groupQuizManager.LoadQuiz(groupId);
                    ViewBag.AssessmentName = question.AssessmentName;
                    return View(question);
                }
            //}
            return RedirectToAction("ShowResults", new { origin = "Group" });
        }

        private void CompleteAssignment()
        {
            string name = string.Empty;
            string email = string.Empty;
            using (var repo = new Repository<Organization>())
            {
                var org = repo.Filter(q => q.Id == this.UserId).FirstOrDefault();
                org.CurrentAssignmentStatus = Business.Utilities.AssessmentCompletedStatus;
                repo.Update(org);
                repo.SaveChanges();
                name = org.Name;
                email = org.Email;
            }
            
            using (var template = new Repository<Template>())
            {
                var registrationTemplate = template.Filter(q => q.Name.StartsWith(Utilities.AssessmentCompletionMail)).FirstOrDefault();

                if (registrationTemplate != null && !string.IsNullOrWhiteSpace(registrationTemplate.Description))
                    RegistrationSendMail.SendMail(registrationTemplate.Description, Utilities.AssessmentCompletionSubject, email, name);
            }

        }
        private GroupQuiz GetGroupQuiz(string level=null)
        {
            this.groupQuizManager = new GroupQuizManager(this.UserId);
            var listOfGroup = this.groupQuizManager.GetAllQuestions(level);
            Session["AllGroupQuestions"] = this.groupQuizManager.AllQuestions = listOfGroup;
            return this.groupQuizManager.LoadQuiz(1);
        }
        private GroupQuiz GetLastGroupQuiz()
        {
            this.groupQuizManager = new GroupQuizManager(this.UserId);
            var listOfGroup = this.groupQuizManager.GetAllQuestions(null);
            Session["AllGroupQuestions"] = this.groupQuizManager.AllQuestions = listOfGroup;
            var groupId = listOfGroup.Count;
            return this.groupQuizManager.LoadQuiz(groupId);
        }

    }
   
}
