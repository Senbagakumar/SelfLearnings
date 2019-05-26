using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class QuestionAnswer
    {
        public virtual QuestionQuiz Questions { get; set; }
        public virtual List<AnswerChoice> AnswerChoices { get; set; }
        public int NoOfQuestions { get; set; }
        public int NoOfGroups { get; set; }
        public int NoOfCompletedQuestions { get; set; }
        public string AssessmentName { get; set; }

    }
}