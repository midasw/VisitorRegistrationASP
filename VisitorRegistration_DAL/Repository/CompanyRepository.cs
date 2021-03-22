using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistration_Models;

namespace VisitorRegistration_DAL.Repository
{
    public class CompanyRepository : GenericRepository<Company>
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<Company> GetAll()
        {
            return _entities
                .Include(c => c.Employees)
                .OrderByDescending(c => c.Id);
        }

        public override async Task<Company> GetById(int id)
        {
            Company company = await _entities
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(c => c.Id == id);

            //Company company = await _context.Companies
            //    .Where(c => c.Id == id)
            //    .Include(c => c.Employees)
            //    .FirstOrDefaultAsync();

            company.Employees = company.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToList();

            return company;
        }
    }
}
