using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UIAssignGroup
    {
        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<UIAssignGroupQuestion> uIAssignGroupQuestions { get; set; }
    }
}