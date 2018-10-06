
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{  
    public class QuestionGroup
    {
        public int QuestionId { get; set; }
        public int GroupId { get; set; }
        public int MapperId { get; set; }
    }
}