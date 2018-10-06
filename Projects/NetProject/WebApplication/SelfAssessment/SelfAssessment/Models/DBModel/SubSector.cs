using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class SubSector
    {
        [Key]
        public int Id { get; set; }
        public int SectorId { get; set; }
        public string SubSectorName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [ForeignKey("Id")]
        public virtual Sector Sectors { get; set; }
    }
}