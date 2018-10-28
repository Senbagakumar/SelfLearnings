using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using SelfAssessment.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SelfAssessment.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
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
            var user = userInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
            if (user != null)
            {
                var assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(q => q.Sector == user.SectorId);
                ViewBag.WelComeMessage = assessment.WelcomeMessage;
                ViewBag.Description = assessment.Description;
                ViewBag.Url = assessment.AssessmentFormat == "2" ? "/manageuser/QuizGroup" : assessment.AssessmentFormat == "1" ? "/manageuser/QuizOne" : "/manageuser/QuizGroup";
                if (assessment.Id != 0)
                {
                    //Completion Level details
                    var compList = userInfo.AssessmentContext.organizationLevelHistories.Where(q => q.OrgId == this.UserId).ToList();
                    if(compList !=null && compList.Count > 0)
                    {
                        compList.ForEach(q =>
                        {
                            var completedList = userInfo.AssessmentContext.assessmentLevelMappings.Where(t => t.AssessmentId == assessment.Id && q.Level == q.Level).ToList();
                            lAssessment.Add(AssessmentMapping(q.Level, completedList.GroupBy(t => t.GroupId).Count(), completedList.Count(), q.Status));
                        });                       
                    }

                    //Current Level details
                    var mappings = userInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == assessment.Id && q.Level == user.CurrentAssignmentType).ToList();
                    if (mappings != null)
                    {
                        lAssessment.Add(AssessmentMapping(user.CurrentAssignmentType, mappings.GroupBy(q => q.GroupId).Count(), mappings.Count(), user.CurrentAssignmentStatus));                      
                    }
                }
            }
            return View(lAssessment);
        }
        public ActionResult CustomerAssessment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAssessment(QuestionOnePost question)
        {
            GroupQuiz groupQuiz=null;
            int groupId = int.Parse(question.QInfo);
            if(question.hdnaction== "Previous")
            {
                if (groupId == 1)
                    groupId = 1;
                else
                    groupId = groupId + 1;
            }
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

        public ActionResult GetFirstGroup(string id)
        {
            return PartialView("QuizGroupPartial", GetGroupQuiz());
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
            }
            else
            {
                message = "Please Enter Correct Details";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuizOne()
        {
            return View();
        }

        public ActionResult GetFirstQuestion(string id)
        {
            this.quizManager = new QuizManager(this.UserId);
            var listOfQuestions = this.quizManager.GetAllQuestions();
            Session["AllQuestions"] = this.quizManager.AllQuestions = listOfQuestions;
            var question = this.quizManager.LoadQuiz(1);
            return PartialView("QuizOnePartial", question);

        }

        [HttpPost]
        public ActionResult QuizOne(QuestionOnePost questionCode)
        {
            bool isAction = false;
            var quiz = new QuestionQuiz();
            isAction = questionCode.hdnaction.Equals("Previous", StringComparison.OrdinalIgnoreCase) ? true : false;

            string[] values = questionCode.QInfo.Split('~');
            quiz.GroupId = Convert.ToInt16(values[0]);
            quiz.QuestionId = Convert.ToInt16(values[1]);
            quiz.UserOptionId = Convert.ToInt16(values[2]);
            quiz.UIQId = Convert.ToInt16(values[3]);

            this.quizManager = new QuizManager(this.UserId);
            bool isAnswered = false;
            if(this.quizManager.IsMandatoryQuestion(quiz.QuestionId))
            {
                isAnswered = quiz.UserOptionId > 0 ? true : false;
            }          

            int qId = 0;

            if (isAnswered)
            {
                this.quizManager.SaveAnswer(quiz);
                qId = quiz.UIQId;

                if (!isAction)
                    qId = qId + 1;
                else
                {
                    qId = quiz.UIQId;
                    qId = qId - 1;
                }

                if (qId == 0)
                    qId = 1;
            }
            else
            {
                qId = quiz.UIQId;
                ViewBag.Msg = "Please Fill the Mandatroy Questions";
            }

            this.quizManager.AllQuestions = (List<QuestionAnswer>)Session["AllQuestions"];
            if (this.quizManager.MoveToNextQuestion(qId))
            {
                var question = this.quizManager.LoadQuiz(qId);
                return PartialView("QuizOnePartial", question);
            }
            return RedirectToAction("ShowResults", new { score = this.quizManager.CalculateScore() });
        }
        public ActionResult ShowResults(int score)
        {
            CompleteAssignment();
            ViewBag.Score = score;
            return View();
        }

        public ActionResult ShowGroupResults(int score)
        {
            CompleteAssignment();
            ViewBag.Score = score;
            return View();
        }

        public ActionResult QuizGroup()
        {           
            return View(GetGroupQuiz());
        }

        [HttpPost]
        public ActionResult QuizGroup(FormCollection questionCode)
        {
            bool isAction = false;
            var groupQuiz = new List<QuestionQuiz>();
            foreach(string user in questionCode.Keys)
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
                quiz.GroupId =Convert.ToInt16(values[0]);
                quiz.QuestionId = Convert.ToInt16(values[1]);
                quiz.UserOptionId = Convert.ToInt16(values[2]);
                quiz.UIGroupId = Convert.ToInt16(values[3]);
                groupQuiz.Add(quiz);
            }

            int groupId = 0;
            this.groupQuizManager = new GroupQuizManager(this.UserId);
            if (groupQuiz.Count == 0)
                groupId = int.Parse(questionCode[1]);
            else
                groupId = groupQuiz[0].GroupId;

            this.groupQuizManager.AllQuestions = (List<GroupQuiz>)Session["AllGroupQuestions"];
            var lQuestions=this.groupQuizManager.GetMandatoryQuestion(groupId);

            bool isMandatoryAnswerd = false;
            lQuestions.ForEach(q =>
            {
                if (groupQuiz == null || groupQuiz.Count ==0 || groupQuiz.Any(t => t.QuestionId == q.Questions.QuestionId && t.UserOptionId == 0))
                    isMandatoryAnswerd = false;
                else
                    isMandatoryAnswerd = true;
            });
            

            if (isMandatoryAnswerd)
            {
                this.groupQuizManager.SaveAnswer(groupQuiz);
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
                return View(question);
            }           
            return RedirectToAction("ShowGroupResults", new { score=this.groupQuizManager.CalculateScore()});
        }

        private void CompleteAssignment()
        {
            using (var repo = new Repository<Organization>())
            {
                var org = repo.Filter(q => q.Id == this.UserId).FirstOrDefault();
                org.CurrentAssignmentStatus = Utilities.AssessmentCompletedStatus;
                repo.Update(org);
                repo.SaveChanges();
            }
        }
        private GroupQuiz GetGroupQuiz()
        {
            this.groupQuizManager = new GroupQuizManager(this.UserId);
            var listOfGroup = this.groupQuizManager.GetAllQuestions();
            Session["AllGroupQuestions"] = this.groupQuizManager.AllQuestions = listOfGroup;
            return this.groupQuizManager.LoadQuiz(1);
        }
    }
   
}
