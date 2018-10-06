using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }     
        public int TypeId { get; set; }       
        public string TempPassword { get; set; }
        public string Password { get; set; }
        public string CurrentAssignmentType { get; set; }
        public string CurrentAssignmentStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public int AssessmentId { get; set; } // ForeignKey
        public int TypeOfServiceId { get; set; } //ForeignKey
        public int CityId { get; set; } // ForeignKey
        public int StateId { get; set; } // ForeignKey
        public int SectorId { get; set; } //ForeignKey
        public int SubSectorId { get; set; } // ForeignKey
        public int RevenueId { get; set; } // ForeignKey
        [ForeignKey("Id")]
        public virtual State States { get; set; }
        [ForeignKey("Id")]
        public virtual City Cities { get; set; }
        [ForeignKey("Id")]
        public virtual Sector Sectors { get; set; }
        [ForeignKey("Id")]
        public virtual SubSector SubSectors { get; set; }
        [ForeignKey("Id")]
        public virtual Revenue Revenues { get; set; }
        [ForeignKey("Id")]
        public virtual ServiceType TypesOfService { get; set; }
        [ForeignKey("Id")]
        public virtual Assessment Assessments { get; set; }

    }   
}