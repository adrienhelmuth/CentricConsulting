using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricConsulting.Models
{
    public class userDetails
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Primary Phone")]
        [Phone]
        public string PhoneNumber { get; set; }

        //[Display(Name = "Office")]
        //public string Office { get; set; }

        public Office office { get; set; }

        public enum Office
        {
            Boston = 1,
            Charlotte = 2,
            Chicago = 3,
            Cincinnati = 4,
            Cleveland = 5,
            Columbus = 6,
            India = 7,
            Indianapolis = 8,
            Louisville = 9,
            Miami = 10,
            Seattle = 11,
            Tampa = 12
        }


        [Display(Name = "Current position")]
        public string Position { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> hireDate { get; set; }


        public string fullName { get { return lastName + ", " + firstName; } }
    }
}