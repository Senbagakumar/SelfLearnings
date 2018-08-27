using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class Answers
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Type { get; set; }
        public int QuestionId { get; set; }
        public int GroupId { get; set; }
        public string OrganizationAnswers { get; set; }
    }
}