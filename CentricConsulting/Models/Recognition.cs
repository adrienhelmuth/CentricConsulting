using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentricConsulting.Models
{
    public class Recognition
    {
        [Key]
        public int EmployeeRecognitionID { get; set; }

        [Display(Name = "Date Recognition is Given")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> CurentDateTime { get; set; }


        [Required]
        [Display(Name = "Comments")]
        public string RecognitionComments { get; set; }

        [Display(Name = "Employee Giving Recognition")]
        [Required]
        public Guid EmployeeGivingRecog { get; set; }

        [ForeignKey("EmployeeGivingRecog")]
        public virtual userDetails Giver { get; set; }

        [Required]

        [Display(Name = "Employee Being Recognized")]
        public Guid ID { get; set; }

        [ForeignKey("ID")]
        public virtual userDetails userDetails { get; set; }

        public CoreValue award { get; set; }

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
    