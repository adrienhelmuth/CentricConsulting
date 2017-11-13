using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricConsulting.Models
{
    public class Recognition
    {



        public int ID { get; set; }
        [Display(Name = "Core value recognized")]
        public CoreValue award { get; set; }
        [Display(Name = "Person giving the recognition")]
        public string recognizor { get; set; }
        [Display(Name = "Person receiving the recognition")]
        public string recognized { get; set; }
        [Display(Name = "Date recognition given")]
        public DateTime recognizationDate { get; set; }
        public enum CoreValue
        {
            Commit_to_Delivery_Excellence = 1,
            Inveset_in_an_Exceptional_Culture = 2,
            Embrace_Integrity_and_Openness = 3,
            Practice_Responsible_Stewardship = 4,
            Strive_to_Innovate = 5,
            Ignite_Passion_for_the_Greater_Good = 6,
            Live_a_Balanced_Life = 7

        }
    }
}
    