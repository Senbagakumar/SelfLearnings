using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Business
{
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
            AllQuestions.ForEach(q =>
            {
                var isAnswer = uInfo.Filter(a => a.QuestionId == q.Questions.QuestionId && a.UserId == UserId).FirstOrDefault();
                if (isAnswer != null)
                {
                    q.AnswerChoices.ForEach(t =>
                    {
                        t.IsChecked = false;
                    });
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
                aAnswer.Groups = ans.AssessmentContext.groups.FirstOrDefault(t => t.Id == aAnswer.GroupId);
                aAnswer.Organization = ans.AssessmentContext.UserInfo.FirstOrDefault(t => t.Id == aAnswer.UserId);
                aAnswer.Questiones = ans.AssessmentContext.questions.FirstOrDefault(t => t.Id == aAnswer.QuestionId);

                ans.Create(aAnswer);
            }
            else
            {
                eAnswer.Groups = ans.AssessmentContext.groups.FirstOrDefault(t => t.Id == eAnswer.GroupId);
                eAnswer.Organization = ans.AssessmentContext.UserInfo.FirstOrDefault(t => t.Id == eAnswer.UserId);
                eAnswer.Questiones = ans.AssessmentContext.questions.FirstOrDefault(t => t.Id == eAnswer.QuestionId);

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
                organization.CurrentAssignmentStatus = Utilities.AssessmentCompletedStatus;

            uInfo.Update(organization);
            uInfo.SaveChanges();
        }
    }
}