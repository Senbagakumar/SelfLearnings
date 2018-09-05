using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UIAssessment
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public int NoOfParticipants { get; set; }
        public int NoOfCompleted { get; set; }
        public int NoOfPending { get; set; }
        public int NoOfGroup { get; set; }
        public int NoOfQuestion { get; set; }
        public string AssignmentStatus { get; set; }
    }

}