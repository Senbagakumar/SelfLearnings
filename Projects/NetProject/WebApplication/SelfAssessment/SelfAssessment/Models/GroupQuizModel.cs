using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class GroupQuizModel
    {
        public int QuizGroupId { get; set; }
        public virtual List<GroupQuiz> GroupQuestions { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? EndTime { get; set; }
        public int Score { get; set; }

    }
}