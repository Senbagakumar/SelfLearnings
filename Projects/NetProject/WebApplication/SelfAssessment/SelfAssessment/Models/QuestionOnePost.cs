using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class QuestionOnePost
    {
        public string hdnaction { get; set; }
        public string QInfo { get; set; }
        public int UserId { get; set; }
        public string Level { get; set; }
    }
}