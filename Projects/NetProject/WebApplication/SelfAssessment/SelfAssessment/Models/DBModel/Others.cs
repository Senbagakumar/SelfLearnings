using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class Others
    {
        [Key]
        public string OrganizationId { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        //[ForeignKey("Id")]
        //public virtual Organization Organizations { get; set; }
    }
}