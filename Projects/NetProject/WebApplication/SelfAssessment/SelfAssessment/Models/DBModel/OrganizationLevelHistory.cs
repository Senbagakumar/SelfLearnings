using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class OrganizationLevelHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrgId { get; set; } //ForeginKey
        public string Level { get; set; }
        public string Status { get; set; }
        public DateTime PromoteDate { get; set; }
        [ForeignKey("Id")]
        public virtual Organization Organizations { get; set; }
    }
}