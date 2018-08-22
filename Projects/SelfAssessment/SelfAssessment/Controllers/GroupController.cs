using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            var lmodel = new List<UIGroup>();

            for (int i = 1; i <= 20; i++)
            {
                var model = new UIGroup();
                model.Id = i;
                model.GroupName = "GroupName" + i;
                model.NoOfQuestions = 10;
                lmodel.Add(model);
            }

            return View(lmodel);
        }

        public List<Questions> GetQuestions()
        {
            var lQuestion = new List<Questions>();
            for(int i=1; i<5; i++)
            {
                var question = new Questions();
                question.Id = i;
                question.GroupId = i;
                question.QuestionCode = "QC000" + i;
                question.QuestionText = "QuestionText";
                question.QHint = "Hint" + i;
                question.TimerValue = "01:00 Hrs";
                lQuestion.Add(question);
            }
            return lQuestion;
        }

        public Questions GetQById()
        {
            return new Questions(){ Id=1, GroupId=1, Mandatory=false, QuestionCode="QC0001", QuestionText="QuestionText", QHint="Question Hint", Option1="Opt1", Option2="Opt2", Option3="Opt3", Option4="Opt4", Option5="Opt5", Answer="Answer", TimerValue="05:00 Hrs" };
        }

        public JsonResult GetQuestionById(string id)
        {
            var question = GetQById();
            return Json(question, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllQByGroupId(int id)
        {
            var questions = GetQuestions();
            var uiGroup = new UIGroup() {Id=id, GroupName="MyGroupName", GroupDescription= "MyGroupDescription", questions= questions };
            return Json(uiGroup, JsonRequestBehavior.AllowGet);
        }
       
        // POST: Group/Create
        [HttpPost]
        public ActionResult SaveGroup(UIGroup uiGroup)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Group/Create   
        [HttpPost]
        public JsonResult CreateQuestion(Questions question)
        {
            var model = new Questions() { Id=6, QuestionCode="QC0006", QuestionText="NewQuestion", QHint="QHint", TimerValue="01:02 Hrs" };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }      
    }
}
