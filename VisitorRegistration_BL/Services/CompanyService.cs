using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistration_BLL.Models;
using VisitorRegistration_DAL.UnitOfWork;
using VisitorRegistration_Models;
using X.PagedList;

namespace VisitorRegistration_BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiDbResult> CreateCompany(Company company)
        {
            _unitOfWork.CompanyRepository.Create(company);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<ApiDbResult> DeleteCompanies(IEnumerable<Company> companies)
        {
            _unitOfWork.CompanyRepository.DeleteRange(companies);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == companies.Count())
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<ApiDbResult> DeleteCompany(Company company)
        {
            _unitOfWork.CompanyRepository.Delete(company);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(Expression<Func<Company, bool>> predicate = null)
        {
            var query = _unitOfWork.CompanyRepository.GetAll();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesById(IEnumerable<int> ids)
        {
            return await _unitOfWork.CompanyRepository.GetAll()
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _unitOfWork.CompanyRepository.GetById(id);
        }

        public async Task<ApiDbResult> UpdateCompany(Company company)
        {
            _unitOfWork.CompanyRepository.Update(company);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<ApiDbResult> CreateEmployee(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Create(employee);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.EmployeeRepository.GetById(id);
        }

        public async Task<ApiDbResult> UpdateEmployee(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Update(employee);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<ApiDbResult> DeleteEmployee(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Delete(employee);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == 1)
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<IPagedList<Company>> GetAllCompaniesPaged(int pageNumber = 1, int pageSize = -1, Expression<Func<Company, bool>> predicate = null)
        {
            var query = _unitOfWork.CompanyRepository.GetAll();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (pageSize == -1)
            {
                pageNumber = 1;
                pageSize = query.Count();
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId)
        {
            return await _unitOfWork.EmployeeRepository
                .GetAll()
                .Where(e => e.CompanyId == companyId)
                .ToListAsync();
        }
    }
}
