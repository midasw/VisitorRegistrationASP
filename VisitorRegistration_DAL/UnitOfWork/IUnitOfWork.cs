using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistration_DAL.Repository;
using VisitorRegistration_Models;

namespace VisitorRegistration_DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Company> CompanyRepository { get; }
        IGenericRepository<Employee> EmployeeRepository { get; }
        RegistrationRepository RegistrationRepository { get; }
        Task<int> Save();
    }
}
