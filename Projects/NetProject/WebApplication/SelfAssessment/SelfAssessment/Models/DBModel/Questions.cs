using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }  // ForeignKey
        public string QuestionCode { get; set; }
        public string QuestionText { get; set; }
        public string QType { get; set; }
        public string QHint { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option5 { get; set; }
        public string Answer { get; set; }
        public string TimerValue { get; set; }
        public bool Mandatory { get; set; }
        [ForeignKey("Id")]
        public virtual Group Groups { get; set; }

    }
}