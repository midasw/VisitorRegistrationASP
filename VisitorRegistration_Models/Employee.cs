using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
