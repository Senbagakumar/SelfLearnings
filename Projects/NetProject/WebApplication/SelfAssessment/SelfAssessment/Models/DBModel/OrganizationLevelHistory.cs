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
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Level { get; set; }      
        public int TypeId { get; set; }       
        public int AssessmentId { get; set; } // ForeignKey
        public int TypeOfServiceId { get; set; } //ForeignKey
        public int CityId { get; set; } // ForeignKey
        public int StateId { get; set; } // ForeignKey
        public int SectorId { get; set; } //ForeignKey
        public int SubSectorId { get; set; } // ForeignKey
        public int RevenueId { get; set; } // ForeignKey
        public DateTime PromoteDate { get; set; }
        public string Status { get; set; }
    }
}