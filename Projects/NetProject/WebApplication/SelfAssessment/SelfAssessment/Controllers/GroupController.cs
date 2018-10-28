using SelfAssessment.Business;
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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class GroupController : Controller
    {
        private readonly IBusinessContract businessContract;
        public GroupController(IBusinessContract businessContract)
        {
            this.businessContract = businessContract;
        }
        // GET: Group
        public ActionResult Index()
        {
            var lmodel = new List<UIGroup>();
            var group = new List<Group>();
            var question = new List<Questions>();

            using (Repository<Group> repository = new Repository<Group>())
            {
                group = repository.All().ToList();
                question = repository.AssessmentContext.questions.ToList();
            }

            var model = group.Select(q => new UIGroup() { GroupName = q.Name, GroupDescription = q.Description, Id = q.Id, NoOfQuestions = question.Where(t => t.GroupId == q.Id).Count() }).ToList();
            int i = 1;
            model.ForEach(t =>
            {
                lmodel.Add(new UIGroup() { Id=t.Id, GroupName=t.GroupName, OrderId=i, NoOfQuestions=t.NoOfQuestions, GroupDescription=t.GroupDescription, questions=t.questions });
                i = i + 1;
            });
            return View(lmodel);
        }

        public List<Questions> GetQuestions(int groupId)
        {
            var lQuestion = new List<Questions>();
            using (var repository = new Repository<Questions>())
            {
                lQuestion = repository.Filter(q => q.GroupId == groupId).ToList(); 
            }
            return lQuestion;
        }

        public JsonResult GetQuestionById(string id)
        {
            var question = new Questions();
            using (var repository = new Repository<Questions>())
            {
                question = repository.Filter(q => q.QuestionCode == id).FirstOrDefault();
            }
            return Json(question, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllQByGroupId(int id)
        {
            var questions = GetQuestions(id);
            var group = new Group();

            using (Repository<Group> repository = new Repository<Group>())
            {
                group = repository.Filter(q => q.Id == id).FirstOrDefault();
            }
            var uiGroup = new UIGroup() {Id=id, GroupName=group.Name, GroupDescription= group.Description, questions= questions };
            return Json(uiGroup, JsonRequestBehavior.AllowGet);
        }
       
        // POST: Group/Create
        [HttpPost]
        public JsonResult SaveGroup(UIGroup uiGroup)
        {
            try
            {
                if (uiGroup.Id != 0)
                {
                    using (Repository<Group> repository = new Repository<Group>())
                    {
                        var group = repository.Filter(q => q.Id == uiGroup.Id).FirstOrDefault();
                        if (group != null)
                        {
                            group.Name = uiGroup.GroupName;
                            group.Description = uiGroup.GroupDescription;
                            group.UpdateDate = DateTime.Now;

                            repository.Update(group);
                            repository.SaveChanges();
                        }
                    }
                }
                else
                {
                    using (Repository<Group> repository = new Repository<Group>())
                    {
                        var group = new Group() { Name = uiGroup.GroupName, Description = uiGroup.GroupDescription, CreateDate = DateTime.Now };
                        repository.Create(group);
                        repository.SaveChanges();
                    }
                }

                // TODO: Add insert logic here
                return Json(Utilities.Success, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json(Utilities.Failiure, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Group/Create   
        [HttpPost]
        public JsonResult CreateQuestion(Questions question)
        {
            var first = new Questions();

            if (!string.IsNullOrEmpty(question.QuestionCode))
            {
                using (var repository = new Repository<Questions>())
                {
                    first = repository.Filter(q => q.QuestionCode == question.QuestionCode).FirstOrDefault();
                    if(first!=null)
                    {
                        first.Groups = repository.AssessmentContext.groups.FirstOrDefault(q => q.Id == first.GroupId);
                        first.QuestionText = question.QuestionText;
                        first.QHint = question.QHint;
                        //first.TimerValue = question.TimerValue;
                        first.Answer = question.Answer;
                        first.Mandatory = question.Mandatory;
                        first.Option1 = question.Option1;
                        first.Option2 = question.Option2;
                        first.Option3 = question.Option3;
                        first.Option4 = question.Option4;
                        first.Option5 = question.Option5;
                        first.QType = question.QType;
                    }
                    repository.Update(first);
                    repository.SaveChanges();
                }
            }
            else
            {

                var qCode = Utilities.QuestionCode + Guid.NewGuid().ToString().Replace("-","");                

                var quest = new Questions()
                {
                    QuestionCode = qCode,
                    QuestionText = question.QuestionText,
                    QHint = question.QHint,
                    //TimerValue = question.TimerValue,
                    Answer = question.Answer,
                    Mandatory = question.Mandatory,
                    Option1 = question.Option1,
                    Option2 = question.Option2,
                    Option3 = question.Option3,
                    Option4 = question.Option4,
                    Option5 = question.Option5,
                    GroupId = question.GroupId,
                    QType = question.QType
                };

                using (var repository = new Repository<Questions>())
                {
                    quest.Groups = repository.AssessmentContext.groups.FirstOrDefault(q => q.Id == quest.GroupId);
                    repository.Create(quest);
                    repository.SaveChanges();
                    first = repository.Filter(q => q.QuestionCode == quest.QuestionCode).FirstOrDefault();
                }
            }

            return Json(first, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteQuestionById(string id)
        {
            using (var repository = new Repository<Questions>())
            {
                var delQuestion = repository.Filter(q => q.QuestionCode == id).FirstOrDefault();
                if (delQuestion != null)
                {
                    repository.Delete(delQuestion);
                    repository.SaveChanges();
                }
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteGroupById(int id)
        {
            using (var repository = new Repository<Questions>())
            {
                var delQuestions = repository.Filter(q => q.GroupId == id).ToList();
                if (delQuestions != null)
                {
                    repository.DeleteRange(delQuestions);
                    repository.SaveChanges();
                }
            }
            using (var repository = new Repository<Group>())
            {
                var delGroup = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (delGroup != null)
                {
                    repository.Delete(delGroup);
                    repository.SaveChanges();
                }
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }
    }
}
