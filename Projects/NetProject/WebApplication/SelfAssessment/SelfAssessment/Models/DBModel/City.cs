using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public int StateId { get; set; } // Foreign Key
        public string CityName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        [ForeignKey("Id")]
        public virtual State States { get; set; }
    } 
}