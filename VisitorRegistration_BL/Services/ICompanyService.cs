using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VisitorRegistration_BLL.Models;
using VisitorRegistration_Models;
using X.PagedList;

namespace VisitorRegistration_BLL.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanies(Expression<Func<Company, bool>> predicate = null);
        Task<IPagedList<Company>> GetAllCompaniesPaged(int pageNumber = 1, int pageSize = -1, Expression<Func<Company, bool>> predicate = null);
        Task<Company> GetCompanyById(int id);
        Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<int> ids);
        Task<ApiDbResult> CreateCompany(Company company);
        Task<ApiDbResult> UpdateCompany(Company company);
        Task<ApiDbResult> DeleteCompanies(IEnumerable<Company> companies);
        Task<ApiDbResult> DeleteCompany(Company company);
        Task<Employee> GetEmployeeById(int id);
        Task<ApiDbResult> CreateEmployee(Employee employee);
        Task<ApiDbResult> UpdateEmployee(Employee employee);
        Task<ApiDbResult> DeleteEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId);
    }
}
