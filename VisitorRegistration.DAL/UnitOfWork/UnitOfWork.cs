using System.Threading.Tasks;
using VisitorRegistration.DAL.Repository;
using VisitorRegistration_Models;

namespace VisitorRegistration.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private CompanyRepository companyRepository;
        private EmployeeRepository employeeRepository;
        private RegistrationRepository registrationRepository;

        public IGenericRepository<Company> CompanyRepository
        {
            get
            {
                if (companyRepository == null)
                {
                    companyRepository = new CompanyRepository(_context);
                }
                return companyRepository;
            }
        }

        public IGenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new EmployeeRepository(_context);
                }
                return employeeRepository;
            }
        }

        public RegistrationRepository RegistrationRepository
        {
            get
            {
                if (registrationRepository == null)
                {
                    registrationRepository = new RegistrationRepository(_context);
                }
                return registrationRepository;
            }
        }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
