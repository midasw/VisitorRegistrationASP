using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VisitorRegistration_Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
