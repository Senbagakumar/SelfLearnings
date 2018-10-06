using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public virtual List<QuestionQuiz> Questions { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndTime { get; set; }
        public int Score { get; set; }

    }
}