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

    public class UAssessment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WelcomeMessage { get; set; }
        public string EndMessage { get; set; }
        public string AdminEmail { get; set; }
        public string AssessmentFormat { get; set; }
        public bool ShowWelcomeScreen { get; set; }    
        public bool ShowProgressBar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AssessmentType { get; set; }
        public int Sector { get; set; }
        public int SubSector { get; set; }
    }

}