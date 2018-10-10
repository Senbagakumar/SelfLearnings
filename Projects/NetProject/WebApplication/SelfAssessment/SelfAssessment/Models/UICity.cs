using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UICity
    {
        public int Id { get; set; }     
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }

    }

    public class Graph
    {
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
}