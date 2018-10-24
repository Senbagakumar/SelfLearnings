using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.Business;

namespace SelfAssessment.Business
{
    public class GroupQuizManager : IGroupQuizManager
    {
        private readonly int UserId;
        public List<GroupQuiz> AllQuestions { get; set; }
        public GroupQuizManager(int userId)
        {
            UserId = userId;
        }

        public List<GroupQuiz> GetAllQuestions()
        {
            var listOfGroupQuestions = new List<GroupQuiz>();
            var uInfo = new Repository<Organization>();
            var details = uInfo.AssessmentContext.UserInfo.Join(uInfo.AssessmentContext.assessments, u => u.SectorId, a => a.Sector, (u, a) => new { u, a }).Where(q=> q.u.Id == UserId).FirstOrDefault();

            var questionIds = uInfo.AssessmentContext.assessmentLevelMappings.Where(q => q.AssessmentId == details.a.Id && q.Level == details.u.CurrentAssignmentType).Select(q => q.QuestionId).ToList();
            var lQuestions = uInfo.AssessmentContext.questions.Where(q => questionIds.Contains(q.Id)).GroupBy(q => q.GroupId).Select(q => new { Questions = q.ToList(), Type = q.Key }).OrderBy(t => t.Type).ToList();

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
                    question.Questions = new QuestionQuiz() { QuestionCode = "Q" + k, QuestionId = v.Id, QuestionText = v.QuestionText };

                    var answerChoice = new List<AnswerChoice>();

                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 1, Choices = v.Option1 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 2, Choices = v.Option2 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 3, Choices = v.Option3 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 4, Choices = v.Option4 });
                    answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 5, Choices = v.Option5 });

                    question.AnswerChoices = answerChoice;

                    listOfGroupQuiz.listOfQuestions.Add(question);
                    k++;
                });
                i++;
                listOfGroupQuestions.Add(listOfGroupQuiz);
            });
            return listOfGroupQuestions;
        }


        private List<GroupQuiz> AllGroupQuiz()
        {
            var uInfo = new Repository<Answer>();

            AllQuestions.ForEach(v =>
            {
                v.listOfQuestions.ForEach(q =>
                {
                    var isAnswer = uInfo.Filter(a => a.QuestionId ==q.Questions.QuestionId  && a.GroupId == v.GroupId && a.UserId == UserId).FirstOrDefault();
                    if (isAnswer != null)
                    {
                        q.AnswerChoices.ForEach(s => { s.IsChecked = false; });
                        var selectedChoice = q.AnswerChoices.Where(ans => ans.AnswerChoiceId == isAnswer.UserOptionId).FirstOrDefault();
                        selectedChoice.IsChecked = true;
                    }
                });
            });
            return AllQuestions;
          
        }

        public GroupQuiz LoadQuiz(int groupId)
        { 
            var allGroup = AllGroupQuiz();
            var currentGroup=allGroup.Where(q => q.UIGroupId == groupId).First();          
            return GetCountDetails(allGroup, currentGroup);
        }

        private GroupQuiz GetCountDetails(List<GroupQuiz> allGroupQuiz, GroupQuiz currentQuiz)
        {
            int answeredCount = 0;
            int noQuestions = 0;
            allGroupQuiz.ForEach(q => noQuestions+= q.listOfQuestions.Count());
            currentQuiz.NoOfQuestions = noQuestions;
            currentQuiz.NoOfGroups = allGroupQuiz.Count();
            allGroupQuiz.ForEach(v =>
            {
                v.listOfQuestions.ForEach(s =>
                {                   
                    if (s.AnswerChoices.Any(t => t.IsChecked))
                        answeredCount++;
                });

            });
            currentQuiz.NoOfCompletedQuestions = answeredCount;
            return currentQuiz;
        }

        public void SaveAnswer(List<QuestionQuiz> answers)//QueryWindowInMins
        {
            var ans = new Repository<Answer>();
            bool isSave = false;
            foreach(var questionQuiz in answers)
            {
                isSave = true;
                //SaveLogic

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
                //quiz.Score += CalculateScore(questionQuiz.UserOptionId);
            }
            if(isSave)
                ans.SaveChanges();
        }

        public int CalculateScore()
        {
            int score = 0;
            var uInfo = new Repository<Organization>();
            AllQuestions.ForEach(v =>
            {
                v.listOfQuestions.ForEach(q =>
                {
                    var isAnswer = uInfo.AssessmentContext.answers.Where(a => a.QuestionId == q.Questions.QuestionId && a.GroupId == v.GroupId && a.UserId == UserId).FirstOrDefault();
                    if (isAnswer != null)
                    {
                        score += Utilities.CalculateScoreByAns(isAnswer.UserOptionId);
                    }
                });
            });
            return score;
        }      

        public bool MoveToNextGroup(int groupId)
        {
           return AllGroupQuiz().Count >= groupId ? true : false;  
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