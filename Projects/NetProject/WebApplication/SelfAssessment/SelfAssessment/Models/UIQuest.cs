using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UIQuest
    {
        public List<string> QuestionId { get; set; }
        public string Level { get; set; }
        public int AssessmentId { get; set; }
    }
}