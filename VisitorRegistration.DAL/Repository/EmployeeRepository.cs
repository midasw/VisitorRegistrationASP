using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_Models;

namespace VisitorRegistration.DAL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new IEnumerable<Employee> GetAll()
        {
            return _entities
                .OrderBy(e => e.LastName)
                .OrderBy(e => e.FirstName)
                .ToList();
        }

        public override async Task<Employee> GetById(int id)
        {
            return await _entities
                .Include(e => e.Company)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
