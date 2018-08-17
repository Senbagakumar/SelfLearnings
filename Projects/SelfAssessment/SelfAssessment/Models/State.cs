using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}