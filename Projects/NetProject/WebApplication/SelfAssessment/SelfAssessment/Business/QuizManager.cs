using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{
    public class AnswerChoice
    {
        public int AnswerChoiceId { get; set; }
        public string Choices { get; set; }
        public bool IsChecked { get; set; }

    }

    public class QuestionOnePost
    {
        public string hdnaction { get; set; }
        public string QInfo { get; set; }
    }
    public class QuestionQuiz
    {
        public int QuestionId { get; set; }
        public int UIQId { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public int  UserOptionId { get; set; }
        public int GroupId { get; set; }
        public string GroupText { get; set; }
        public int UIGroupId { get; set; }
        //public virtual List<AnswerChoice> AnswerChoices { get; set; }
    }

    public class QuestionAnswer
    {
        public virtual QuestionQuiz Questions { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }
        public int NoOfQuestions { get; set; }
        public int NoOfGroups { get; set; }
        public int NoOfCompletedQuestions { get; set; }

    }
    interface ISaveQuiz
    {
        void SaveAnswer(QuestionQuiz answers);
        void CompleteQuiz();
        bool MoveToNextQuestion(int questionId);
        int CalculateScore();
    }
    interface IQuizManager : ISaveQuiz
    {
        QuestionAnswer LoadQuiz(int questionId);
        List<QuestionAnswer> GetAllQuestions();

    }
    public class Quiz
    {
        public int QuizId { get; set; }
        public virtual List<QuestionQuiz> Questions { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndTime { get; set; }
        public int Score { get; set; }

    }
    public class QuizManager : IQuizManager
    {
        private readonly int UserId;
        public List<QuestionAnswer> AllQuestions { get; set; }
        public QuizManager(int userId)
        {
            UserId = userId;
        }

        public List<QuestionAnswer> GetAllQuestions()
        {
            var listOfQuestions = new List<QuestionAnswer>();
            var uInfo = new Repository<Organization>();

            var details = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q => q.u.Id == UserId).FirstOrDefault();

            var questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == details.a.Id && q.Level == details.u.CurrentAssignmentType).Select(q => q.QuestionId).ToList();
            var lQuestions = uInfo.AssessmentContext.questions.Where(q => questionIds.Contains(q.Id)).GroupBy(q => q.GroupId).Select(q => new { Questions = q.ToList(), Type = q.Key }).OrderBy(t => t.Type).ToList();

            int i = 1;
            lQuestions.ForEach(t =>
            {

                t.Questions.ForEach(v =>
                {
                    var question = new QuestionAnswer();
                    
                    question.Questions = new QuestionQuiz() { UIQId = i, QuestionCode="Q"+i, QuestionId = v.Id, QuestionText = v.QuestionText, GroupId=v.GroupId };

                    question.Questions.GroupText = uInfo.AssessmentContext.groups.Where(q => q.Id == t.Type).FirstOrDefault().Name;

                    var answerChoice = new List<AnswerChoice>();

                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 1, Choices = v.Option1 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 2, Choices = v.Option2 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 3, Choices = v.Option3 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 4, Choices = v.Option4 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 5, Choices = v.Option5 });

                    question.AnswerChoices = answerChoice;

                    listOfQuestions.Add(question);
                    i++;
                });
              
            });
            return listOfQuestions;
        }

        private List<QuestionAnswer> PrePopulateAnswers()
        {
            var uInfo = new Repository<Answer>();
            AllQuestions.ForEach(q=>
            {
                var isAnswer = uInfo.Filter(a => a.QuestionId == q.Questions.QuestionId &&  a.UserId == UserId).FirstOrDefault();
                if (isAnswer != null)
                {
                    var selectedChoice = q.AnswerChoices.Where(ans => ans.AnswerChoiceId == isAnswer.UserOptionId).FirstOrDefault();
                    selectedChoice.IsChecked = true;
                }

            });
            return AllQuestions;
        }


        public QuestionAnswer LoadQuiz(int questionId)
        {
            var currentQuesions= PrePopulateAnswers().Where(q => q.Questions.UIQId == questionId).First();
            return GetCountDetails(AllQuestions,currentQuesions);
        }

        private QuestionAnswer GetCountDetails(List<QuestionAnswer> allQuestionAnswer, QuestionAnswer currentQuestion)
        {            
            currentQuestion.NoOfQuestions = allQuestionAnswer.Count();
            currentQuestion.NoOfGroups = allQuestionAnswer.GroupBy(q => q.Questions.GroupId).Count();
            currentQuestion.NoOfCompletedQuestions = allQuestionAnswer.Select(q => q.AnswerChoices.Any(t => t.IsChecked)).Count();
            return currentQuestion;
        }


        public void SaveAnswer(QuestionQuiz questionQuiz)
        {
            var ans = new Repository<Answer>();
            var eAnswer = ans.Filter(q => q.UserId == UserId && q.QuestionId == questionQuiz.QuestionId && q.GroupId == questionQuiz.GroupId).FirstOrDefault();
            if (eAnswer == null)
            {
                var aAnswer = new Answer() { GroupId = questionQuiz.GroupId, QuestionId = questionQuiz.QuestionId, UserId = UserId, UserOptionId = questionQuiz.UserOptionId };
                ans.Create(aAnswer);
            }
            else
            {
                eAnswer.UserOptionId = questionQuiz.UserOptionId;
                ans.Update(eAnswer);
            }
            ans.SaveChanges();
        }

        public int CalculateScore()
        {
            int score = 0;
            var uInfo = new Repository<Organization>();
            AllQuestions.ForEach(v =>
            {
                var isAnswer = uInfo.AssessmentContext.answers.Where(a => a.QuestionId == v.Questions.QuestionId && a.GroupId == v.Questions.GroupId && a.UserId == UserId).FirstOrDefault();
                if (isAnswer != null)
                {
                    score += CalculateScoreByAns(isAnswer.UserOptionId);
                }
                
            });
            return score;
        }

        private int CalculateScoreByAns(int answerId)
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

        public bool MoveToNextQuestion(int questionId)
        {
            return AllQuestions.Count >= questionId ? true : false;
        }

        public void CompleteQuiz()
        {
            var uInfo = new Repository<Organization>();
            var organization = uInfo.Filter(q => q.Id == UserId).FirstOrDefault();
            if (organization != null)
                organization.CurrentAssignmentStatus = "Completed";

            uInfo.Update(organization);
            uInfo.SaveChanges();
        }
    }
}