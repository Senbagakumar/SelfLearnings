using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class UIGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int NoOfQuestions { get; set; }
        public List<Questions> questions { get; set; }

    }
}