using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class GroupQuiz
    {
        public string GroupText { get; set; }
        public List<QuestionAnswer> listOfQuestions { get; set; }
        public int UIGroupId { get; set; }
        public int GroupId { get; set; }
        public int NoOfQuestions { get; set; }
        public int NoOfGroups { get; set; }
        public int NoOfCompletedQuestions { get; set; }
        public int UserId { get; set; }
        public string AssessmentName { get; set; }

    }
}