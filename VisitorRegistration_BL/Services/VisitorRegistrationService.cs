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
    public class VisitorRegistrationService : IVisitorRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VisitorRegistrationErrorDescriber Describer = new VisitorRegistrationErrorDescriber();

        public VisitorRegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult> CheckIn(string firstName, string lastName, string email, int employeeId)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetById(employeeId);

            if (employee != null)
            {
                var registration = new Registration
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Employee = employee
                };

                _unitOfWork.RegistrationRepository.Create(registration);

                if (await _unitOfWork.Save() == 1)
                {
                    return ApiResult.Success;
                }
            }

            return ApiResult.Failed();
        }

        public async Task<ApiResult> CheckOut(string email)
        {
            var registrations = await _unitOfWork.RegistrationRepository.GetByEmail(email);

            if (registrations.Count > 0)
            {
                foreach (var registration in registrations)
                {
                    if (registration.EndDate == null)
                        registration.EndDate = DateTime.Now;
                }

                if (await _unitOfWork.Save() != 1)
                {
                    return ApiResult.Failed(Describer.DbError());
                }
            }
            else
            {
                return ApiResult.Failed(Describer.AlreadyCheckedOut());
            }

            return ApiResult.Success;
        }

        public async Task<IPagedList<Registration>> GetActiveRegistrations(int pageNumber = 1, int pageSize = -1, Expression<Func<Registration, bool>> predicate = null)
        {
            var query = _unitOfWork.RegistrationRepository.GetActiveRegistrations();

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

        public async Task<IEnumerable<Registration>> GetRegistrationsById(IEnumerable<int> ids)
        {
            return await _unitOfWork.RegistrationRepository.GetAll()
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();
        }

        public async Task<ApiDbResult> DeleteRegistrations(IEnumerable<Registration> registrations)
        {
            _unitOfWork.RegistrationRepository.DeleteRange(registrations);

            var result = new ApiDbResult
            {
                AffectedRows = await _unitOfWork.Save()
            };

            if (result.AffectedRows == registrations.Count())
            {
                result.Succeeded = true;
            }

            return result;
        }

        public async Task<int> GetActiveRegistrationsCount()
        {
            return await _unitOfWork.RegistrationRepository.GetActiveRegistrations().CountAsync();
        }
    }
}
