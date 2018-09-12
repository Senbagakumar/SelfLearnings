
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class TempQuestions : Questions
    {

    }
    public class Questions
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public string QType { get; set; }
        public string QHint { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Answer { get; set; }
        public string TimerValue { get; set; }
        public bool Mandatory { get; set; }
    }

    public class QuestionGroup
    {
        public int QuestionId { get; set; }
        public int GroupId { get; set; }
        public int MapperId { get; set; }
    }
}