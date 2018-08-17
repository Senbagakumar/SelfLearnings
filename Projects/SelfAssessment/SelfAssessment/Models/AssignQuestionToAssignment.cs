using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class AssignQuestionToAssignment
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
        public int QuestionId { get; set; }
        public int Level { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}