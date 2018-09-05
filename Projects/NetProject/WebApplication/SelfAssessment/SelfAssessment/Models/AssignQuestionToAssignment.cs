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

    public class UIAssignGroup
    {
        public int AssignmentId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<UIAssignGroupQuestion> uIAssignGroupQuestions { get; set; }

    }
    public class UIAssignGroupQuestion
    {
        public int GroupId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionName { get; set; }
    }

    public class UIQuest
    {
        public List<string> QuestionId { get; set; }
        public string Level { get; set; }
        public int AssignmentId { get; set; }
    }
}