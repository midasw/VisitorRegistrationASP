using System;
using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration_Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Check in date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime BeginDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        [Display(Name = "First name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Appointment with")]
        public Employee Employee { get; set; }
    }
}
