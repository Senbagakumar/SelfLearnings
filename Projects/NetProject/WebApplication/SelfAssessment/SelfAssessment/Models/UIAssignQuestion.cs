using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UIAssignQuestion
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public string AssignMentName { get; set; }
        public string Level { get; set; }
        public int NoOfGroup { get; set; }
        public int NoOfQuestions { get; set; }
        public List<UIAssignGroup> uIAssignGroups { get; set; }
    }  
}