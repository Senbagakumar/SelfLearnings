using SelfAssessment.Business;
using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SelfAssessment.Controllers
{
    //public static class HtmlExtension
    //{
    //    public static MvcHtmlString RadioButton<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object value, object htmlAttributes, bool checkedState)
    //    {
    //        var htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

    //        if (checkedState)
    //        {
    //            htmlAttributeDictionary.Add("checked", "checked");
    //        }

    //        return htmlHelper.RadioButtonFor(expression, value, htmlAttributeDictionary);
    //    }
    //}
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

        public ActionResult QuizGroup()
        {
            var group = GroupQuizManager.Instance.LoadQuiz(1);
            GroupQuizManager.Instance.IsComplete = false;
            return View(group);
        }

        [HttpPost]
        public ActionResult QuizGroup(FormCollection QuestionCode)
        {
            if (GroupQuizManager.Instance.IsComplete) // Prevent score increase if backbutton is clicked and choice made
                return RedirectToAction("ShowResults");

            //GroupQuizManager.Instance.SaveAnswer(answer);
            if (GroupQuizManager.Instance.MoveToNextGroup())
            {
                var question = GroupQuizManager.Instance.LoadQuiz();
                return View(question);
            }
            GroupQuizManager.Instance.IsComplete = true;
            return RedirectToAction("ShowResults");
        }
    }
   
}
