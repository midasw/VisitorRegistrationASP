using System.Threading.Tasks;
using VisitorRegistration.DAL.Repository;
using VisitorRegistration_Models;

namespace VisitorRegistration.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        RegistrationRepository RegistrationRepository { get; }
        Task<int> Save();
    }
}
