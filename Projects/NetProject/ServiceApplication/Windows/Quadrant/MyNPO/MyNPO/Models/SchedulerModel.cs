using System;
using System.ComponentModel.DataAnnotations;

namespace MyNPO.Models
{
    public class CalendarEvent
    {
        //id, text, start_date and end_date properties are mandatory
        public int id { get; set; }
        public string text { get; set; }

        [Display(Name = "Start Date")]
        public DateTime start_date { get; set; }

        [Display(Name = "End Date")]
        public DateTime end_date { get; set; }
    }
}