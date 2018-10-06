using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class AnswerChoice
    {
        public int AnswerChoiceId { get; set; }
        public string Choices { get; set; }
        public bool IsChecked { get; set; }

    }
}