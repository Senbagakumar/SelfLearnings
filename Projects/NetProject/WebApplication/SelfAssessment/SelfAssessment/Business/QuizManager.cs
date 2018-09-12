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
    public class QuestionQuiz
    {
        public int QuestionId { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public int  UserOptionId { get; set; }
        public int GroupId { get; set; }
        public string GroupText { get; set; }

        //public virtual List<AnswerChoice> AnswerChoices { get; set; }
    }

    public class QuestionAnswer
    {
        public virtual QuestionQuiz Questions { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }       

    }
    interface ISaveQuiz
    {
        void SaveAnswer(string answers);
        bool MoveToNextQuestion();
        bool PreviosQuestion();
    }
    interface IQuizManager : ISaveQuiz
    {
        QuestionAnswer LoadQuiz(int defaultQuestionId);

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
        static QuizManager instance;
        //private QuizContext db = new QuizContext();
        static string Type="Level 1";
        static List<QuestionAnswer> listOfQuestions = new List<QuestionAnswer>();
        int questionId = 1;
        public bool IsComplete = false;
        public Quiz quiz;

        private QuizManager()
        {
            quiz = new Quiz();
            quiz.StartTime = DateTime.Now;
            quiz.QuizId = 1;
            quiz.Score = 0;
        }

        public static QuizManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuizManager();
                if (listOfQuestions == null)
                    listOfQuestions = AllQuestions();
                return instance;
            }
        }


        private static List<QuestionAnswer> AllQuestions()
        {
            var listOfQuestions = new List<QuestionAnswer>();

            var qContext = new Repository<Questions>();
            var qAssessment = new Repository<Assessment>();         
            var levelType = qAssessment.Filter(q => q.AssessmentType == Type).FirstOrDefault().Id;
            var lQuestions = qContext.Filter(q => q.AssignmentId == levelType).ToList();

            int i = 1;
            lQuestions.ForEach(t => 
            {
                var answerChoice = new List<AnswerChoice>();

                answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 1, Choices = t.Option1 });
                answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 2, Choices = t.Option2 });
                answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 3, Choices = t.Option3 });
                answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 4, Choices = t.Option4 });
                answerChoice.Add(new AnswerChoice() { AnswerChoiceId = 5, Choices = t.Option5 });

                listOfQuestions.Add(new QuestionAnswer() { Questions = new QuestionQuiz() { QuestionId=i, GroupId=1, GroupText="MyGroup", QuestionCode = t.QuestionCode, QuestionText = t.QuestionText}, AnswerChoices = answerChoice });
                i++;
            });
            return listOfQuestions;
        }

        public QuestionAnswer LoadQuiz(int defaultQuestionId = 0)
        {
            if (defaultQuestionId != 0)
            {
                questionId = defaultQuestionId;
                quiz.Score = 0;
            }

            var currentQuesions= AllQuestions().Where(q => q.Questions.QuestionId == questionId).First();
            return currentQuesions;
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

        public bool MoveToNextQuestion()
        {
            bool canMove = false;
            if (AllQuestions().Count > questionId)
            {
                questionId++;
                canMove = true;
            }

            return canMove;
        }

        public bool PreviosQuestion()
        {
            bool canMove = false;

            if (questionId > 1)
            {
                questionId--;
                canMove = true;
            }

            return canMove;
        }
    }
}