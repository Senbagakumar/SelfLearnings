using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class AssessmentLevelMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AssessmentId { get; set; } //ForeignKey
        public int QuestionId { get; set; } // ForeignKey
        public int GroupId { get; set; } //ForeignKey
        public string Level { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Groups { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Questiones { get; set; }
        [ForeignKey("AssessmentId")]
        public Assessment Assessments { get; set; }
    }
}