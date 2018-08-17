using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}