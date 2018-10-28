using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class QuestionQuiz
    {
        public int QuestionId { get; set; }
        public int UIQId { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public int UserOptionId { get; set; }
        public int GroupId { get; set; }
        public string GroupText { get; set; }
        public int UIGroupId { get; set; }
        public bool Mandatory { get; set; }
    }
}