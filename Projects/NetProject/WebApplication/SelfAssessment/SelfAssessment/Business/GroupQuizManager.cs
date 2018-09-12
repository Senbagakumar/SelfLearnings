using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{


    public class GroupQuiz
    {
        public string GroupText { get; set; }
        public List<QuestionAnswer> listOfQuestions { get; set; }
        public int UIGroupId { get; set; }
        public int GroupId { get; set; }
    }

    interface IGroupSaveQuiz
    {
        void SaveAnswer(List<QuestionQuiz> answers, int userId);
        bool MoveToNextGroup(int userId);
        bool PreviosGroup(int userId);
    }
    interface IGroupQuizManager : IGroupSaveQuiz
    {
        GroupQuiz LoadQuiz(int defaultGroupId, int userid);

    }
    public class GroupQuizModel
    {
        public int QuizGroupId { get; set; }
        public virtual List<GroupQuiz> GroupQuestions { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndTime { get; set; }
        public int Score { get; set; }

    }
    public class GroupQuizManager : IGroupQuizManager
    {
        static GroupQuizManager instance;
        static string Type="Level 1";
        static List<QuestionAnswer> listOfQuestions = new List<QuestionAnswer>();
        int groupId = 1;
        public bool IsComplete = false;
        public GroupQuizModel quiz;

        private GroupQuizManager()
        {
            quiz = new GroupQuizModel();
            quiz.StartTime = DateTime.Now;
            quiz.QuizGroupId = 1;
            quiz.Score = 0;
        }

        public static GroupQuizManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GroupQuizManager();              
                return instance;
            }
        }


        private static List<GroupQuiz> AllGroupQuestions(int userId)
        {
            var listOfGroupQuestions = new List<GroupQuiz>();
            var uInfo = new Repository<Organization>();

            var details=uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).FirstOrDefault();

            var questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == details.a.Id && q.Level == details.u.CurrentAssignmentType).Select(q => q.QuestionId).ToList();
            var lQuestions = uInfo.AssessmentContext.questions.Where(q=> questionIds.Contains(q.Id)).GroupBy(q=> q.GroupId).Select(q => new { Questions = q.ToList(), Type = q.Key }).OrderBy(t => t.Type).ToList();

            int i = 1;
            lQuestions.ForEach(t => 
            {
                var listOfGroupQuiz = new GroupQuiz();
                listOfGroupQuiz.UIGroupId = i;
                listOfGroupQuiz.GroupId = t.Type;
                listOfGroupQuiz.GroupText = uInfo.AssessmentContext.groups.Where(q => q.Id == t.Type).FirstOrDefault().Name;
                listOfGroupQuiz.listOfQuestions = new List<QuestionAnswer>();
                int k = 1;

                t.Questions.ForEach(v => {

                    var question = new QuestionAnswer();
                    question.Questions = new QuestionQuiz() { QuestionCode= "Q"+ k, QuestionId=v.Id, QuestionText=v.QuestionText };                    

                    var answerChoice = new List<AnswerChoice>();                    

                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 1, Choices = v.Option1 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 2, Choices = v.Option2 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 3, Choices = v.Option3 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 4, Choices = v.Option4 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 5, Choices = v.Option5 });                    

                    var isAnswer=uInfo.AssessmentContext.answers.Where(q => q.QuestionId == v.Id && q.GroupId == t.Type && q.UserId == userId).FirstOrDefault();
                    if(isAnswer!=null)
                    {
                        var selectedChoice=answerChoice.Where(q => q.AnswerChoiceId == isAnswer.UserOptionId).FirstOrDefault();
                        selectedChoice.IsChecked = true;
                    }

                    question.AnswerChoices = answerChoice;

                    listOfGroupQuiz.listOfQuestions.Add(question);
                    k++;
                });
                i++;
                listOfGroupQuestions.Add(listOfGroupQuiz);
            });
            return listOfGroupQuestions;
        }

        public GroupQuiz LoadQuiz(int defaultGroupId = 0, int userId=0)
        {
            //var question = db.Questions.Find(questionId);
            //return question;
            if (defaultGroupId != 0)
            {
                groupId = defaultGroupId;
                quiz.Score = 0;
            }

            var currentGroup= AllGroupQuestions(userId).Where(q => q.UIGroupId == groupId).First();
            return currentGroup;
        }

        public void SaveAnswer(List<QuestionQuiz> answers, int userId)//QueryWindowInMins
        {
            var dbAnswer = new List<Answer>();
            var ans = new Repository<Answer>();
            bool isSave = false;
            foreach(var questionQuiz in answers)
            {
                isSave = true;
                //SaveLogic

                var eAnswer = ans.Filter(q => q.UserId == userId && q.QuestionId == questionQuiz.QuestionId && q.GroupId == questionQuiz.GroupId).FirstOrDefault();
                if (eAnswer == null)
                {
                    var aAnswer = new Answer() { GroupId = questionQuiz.GroupId, QuestionId = questionQuiz.QuestionId, UserId = userId, UserOptionId = questionQuiz.UserOptionId };
                    ans.Create(aAnswer);
                }
                else
                {
                    eAnswer.UserOptionId = questionQuiz.UserOptionId;
                    ans.Update(eAnswer);
                }
                quiz.Score += CalculateScore(questionQuiz.UserOptionId);
            }
            if(isSave)
                ans.SaveChanges();
        }

        private int CalculateScore(int answerId)
        {
            int score = 0;
            switch (answerId)
            {
                case 1:
                    score = 5;
                    break;
                case 2:
                    score = 25;
                    break;
                case 3:
                    score = 50;
                    break;
                case 4:
                    score = 75;
                    break;
                case 5:
                    score = 95;
                    break;
                default:
                    score = 5;
                    break;

            }
            return score;
        }

        public bool MoveToNextGroup(int userId)
        {
            bool canMove = false;
            if (AllGroupQuestions(userId).Count > groupId)
            {
                groupId++;
                canMove = true;
            }
            else
            {
                var uInfo = new Repository<Organization>();
                var organization = uInfo.Filter(q => q.Id == userId).FirstOrDefault();
                if (organization != null)
                    organization.CurrentAssignmentStatus = "Completed";
                uInfo.Update(organization);
                uInfo.SaveChanges();
            }
            return canMove;
        }

        public bool PreviosGroup(int userId)
        {
            bool canMove = false;

            if (groupId > 1)
            {
                groupId--;
                canMove = true;
            }

            return canMove;
        }
    }
}