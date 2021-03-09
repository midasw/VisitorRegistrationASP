using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP.Models
{
    public class CheckInViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }

        public SelectList Companies { get; set; }
        public SelectList Employees { get; set; }
    }
}
