using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SelfAssessment.Controllers
{
    public class BaseController : Controller
    {
        private int userId;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            userId = Convert.ToInt16(Session["UserId"].ToString());
        }

        public int UserId => userId;

    }
    public class ManageUserController : BaseController
    {      
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

        public ActionResult ListAssessment()
        {
            var lAssessment = new List<UIAssessment>();
 
            //if (!string.IsNullOrEmpty(UserId))
            //{
                var userInfo = new Repository<Organization>();
                var user = userInfo.Filter(q => q.Id == this.UserId).FirstOrDefault();
                if (user != null)
                {
                    var assessment = userInfo.AssessmentContext.assessments.FirstOrDefault(q => q.Sector == user.SectorId);
                    if(assessment !=null)
                    {
                        var mappings = userInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == assessment.Id && q.Level == user.CurrentAssignmentType).ToList();
                        if(mappings !=null)
                        {
                            var assess = new UIAssessment();
                            assess.Type = user.CurrentAssignmentType;
                            assess.NoOfGroup = mappings.GroupBy(q=> q.GroupId).Count();
                            assess.NoOfQuestion = mappings.Count();
                            assess.AssignmentStatus = userInfo.AssessmentContext.UserInfo.Where(q => q.Id == this.UserId).FirstOrDefault()?.CurrentAssignmentStatus;
                            lAssessment.Add(assess);
                        }
                    }
                }
            //}
          

            //for (int i = 1; i <= 3; i++)
            //{
            //    var assessment = new UIAssessment();
            //    assessment.Type = "Level " + i;
            //    assessment.NoOfGroup = 10;
            //    assessment.NoOfQuestion = 10;
            //    assessment.AssignmentStatus = "Pending";
            //    lAssessment.Add(assessment);
            //}
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

        public ActionResult QuizOne()
        {
            var question = QuizManager.Instance.LoadQuiz(1);
            QuizManager.Instance.IsComplete = false;
            return View(question);
        }

        [HttpPost]
        public ActionResult QuizOne(string answer)
        {
            if (QuizManager.Instance.IsComplete) // Prevent score increase if backbutton is clicked and choice made
                return RedirectToAction("ShowResults");

            QuizManager.Instance.SaveAnswer(answer);
            if (QuizManager.Instance.MoveToNextQuestion())
            {
                var question = QuizManager.Instance.LoadQuiz();
                return View(question);
            }
            QuizManager.Instance.IsComplete = true;
            return RedirectToAction("ShowResults");
        }
        public ActionResult ShowResults()
        {
            return View(QuizManager.Instance.quiz);
        }

        public ActionResult ShowGroupResults()
        {
            return View(GroupQuizManager.Instance.quiz);
        }

        public ActionResult QuizGroup()
        {
            var group = GroupQuizManager.Instance.LoadQuiz(1, this.UserId);
            GroupQuizManager.Instance.IsComplete = false;
            return View(group);

            //var group = new GroupQuiz();
            //group.listOfQuestions = new List<QuestionAnswer>();            
            //return View(group);
        }

        [HttpPost]
        public ActionResult QuizGroup(FormCollection QuestionCode)
        {
            var groupQuiz = new List<QuestionQuiz>();
            foreach(string user in QuestionCode.Keys)
            {
                string[] values = QuestionCode[user].Split('~');
                var quiz = new QuestionQuiz();
                quiz.GroupId =Convert.ToInt16(values[0]);
                quiz.QuestionId = Convert.ToInt16(values[1]);
                quiz.UserOptionId = Convert.ToInt16(values[2]);
                groupQuiz.Add(quiz);
            }

            if (GroupQuizManager.Instance.IsComplete) // Prevent score increase if backbutton is clicked and choice made
                return RedirectToAction("ShowGroupResults");

            GroupQuizManager.Instance.SaveAnswer(groupQuiz, this.UserId);
            if (GroupQuizManager.Instance.MoveToNextGroup(this.UserId))
            {
                var question = GroupQuizManager.Instance.LoadQuiz(0,this.UserId);
                return View(question);
            }
            GroupQuizManager.Instance.IsComplete = true;
            return RedirectToAction("ShowGroupResults");
        }
    }
   
}
