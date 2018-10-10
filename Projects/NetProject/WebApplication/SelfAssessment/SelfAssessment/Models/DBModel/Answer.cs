using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; } //ForeignKey
        public int QuestionId { get; set; } //ForeginKey
        public int UserOptionId { get; set; }
        public int GroupId { get; set; } //ForeginKey
        [ForeignKey("UserId")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Questions Questiones { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Groups { get; set; }
    }
}