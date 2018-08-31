using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
 

    public class UISubSector
    {
        public int Id { get; set; }     
        public string SubSectorName { get; set; }
        public int SectorId { get; set; }
        public string SectorName { get; set; }

    }
}