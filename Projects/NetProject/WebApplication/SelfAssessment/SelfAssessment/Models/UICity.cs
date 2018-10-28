using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UICity
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }

    }

    public class Graph
    {
        public string[] Groups { get; set; }
        public int[] OtherOrg { get; set; }
        public int[] Org { get; set; }
    }

    public class ScoreCalc
    {
        public int UserId { get; set; }
        public List<GraphDynam> Scores {get; set;}
    }

    public class GraphDynam
    {
        public string GroupName { get; set; }
        public int MyScore { get; set; }
    }

    public class UIRevenue
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class UIServiceType
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class UIState
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string StateName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class UISector
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string SectorName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class UIQuestions
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
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
        //public string TimerValue { get; set; }
        public bool Mandatory { get; set; }
    }

}