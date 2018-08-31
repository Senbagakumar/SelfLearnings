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
}