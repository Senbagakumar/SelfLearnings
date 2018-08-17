using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WelcomeMessage { get; set; }
        public string EndMessage { get; set; }
        public string AdminEmail { get; set; }
        public string AssessmentFormat { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ShowWelcomeScreen { get; set; }
        public bool ShowNextButton { get; set; }
        public bool ShowQuestionIndex { get; set; }
        public bool ShowProgressBar { get; set; }
        public bool ParticipantsMayPrintAnswer { get; set; }
        public bool PublicStatistics { get; set; }
        public bool LineAssessmentPublically { get; set; }
        public bool AllowPublicRegistration { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}