using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class SubSector
    {
        public int Id { get; set; }
        public int SectorId { get; set; }
        public string SubSectorName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}