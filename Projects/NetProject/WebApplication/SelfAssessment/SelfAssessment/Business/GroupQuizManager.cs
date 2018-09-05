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
        public int GroupId { get; set; }
    }

    interface IGroupSaveQuiz
    {
        void SaveAnswer(string answers);
        bool MoveToNextGroup();
        bool PreviosGroup();
    }
    interface IGroupQuizManager : IGroupSaveQuiz
    {
        GroupQuiz LoadQuiz(int defaultGroupId);

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


        private static List<GroupQuiz> AllGroupQuestions()
        {
            var listOfGroupQuestions = new List<GroupQuiz>();

            var qContext = new Repository<Questions>();
            var qAssessment = new Repository<Assessment>();
            var group = new Repository<Group>();

            var levelType = qAssessment.Filter(q => q.AssessmentType == Type).FirstOrDefault().Id;
            var lQuestions = qContext.Filter(q => q.AssignmentId == levelType).GroupBy(q=> q.GroupId).Select(q => new { Questions = q.ToList(), Type = q.Key }).OrderBy(t=> t.Type).ToList(); ;
            
            int i = 1;
            lQuestions.ForEach(t => 
            {
                var listOfGroupQuiz = new GroupQuiz();
                listOfGroupQuiz.GroupId = i;
                listOfGroupQuiz.GroupText = group.Filter(q => q.Id == t.Type).FirstOrDefault().Name;
                listOfGroupQuiz.listOfQuestions = new List<QuestionAnswer>();
                int k = 1;
                t.Questions.ForEach(v => {

                    var question = new QuestionAnswer();
                    question.Questions = new QuestionQuiz() { QuestionCode="Q"+k, QuestionId=v.Id, QuestionText=v.QuestionText };                    

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

        public GroupQuiz LoadQuiz(int defaultGroupId = 0)
        {
            //var question = db.Questions.Find(questionId);
            //return question;
            if (defaultGroupId != 0)
            {
                groupId = defaultGroupId;
                quiz.Score = 0;
            }

            var currentGroup= AllGroupQuestions().Where(q => q.GroupId == groupId).First();
            return currentGroup;
        }

        public void SaveAnswer(string answers)
        {
            int optionId = Convert.ToInt16(answers.Substring(0, 1));
            quiz.Score += CalculateScore(optionId);
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

        public bool MoveToNextGroup()
        {
            bool canMove = false;
            if (AllGroupQuestions().Count > groupId)
            {
                groupId++;
                canMove = true;
            }

            return canMove;
        }

        public bool PreviosGroup()
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