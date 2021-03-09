using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistration_Models;

namespace VisitorRegistration_DAL.Repository
{
    public class RegistrationRepository : GenericRepository<Registration>
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Registration>> GetByEmail(string email)
        {
            return await _context.Registrations
                .Where(r => r.Email == email && r.EndDate == null)
                .ToListAsync();
        }

        public IQueryable<Registration> GetActiveRegistrations()
        {
            return _context.Registrations
                .Include(r => r.Employee)
                .ThenInclude(e => e.Company)
                .Where(r => r.EndDate == null)
                .OrderByDescending(r => r.BeginDate);
        }
    }
}
