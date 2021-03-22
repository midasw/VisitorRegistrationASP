using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Resource = VisitorRegistration_ASP.Resources.Models.CheckInViewModel;

namespace VisitorRegistration_ASP.Models
{
    public class CheckInViewModel
    {
        [Required]
        [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resource.VisitingCompanyRequired), ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = nameof(Resource.VisitingCompany), ResourceType = typeof(Resource))]
        public int? CompanyId { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resource.AppointmentWithRequired), ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = nameof(Resource.AppointmentWith), ResourceType = typeof(Resource))]
        public int? EmployeeId { get; set; }

        public SelectList Companies { get; set; }
        public SelectList Employees { get; set; }
    }
}
