using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistration_Models;

namespace VisitorRegistration_DAL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new IEnumerable<Employee> GetAll()
        {
            return _context.Employees
                .OrderBy(e => e.LastName)
                .OrderBy(e => e.FirstName)
                .ToList();
        }

        public override async Task<Employee> GetById(int id)
        {
            return await _context.Employees
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
