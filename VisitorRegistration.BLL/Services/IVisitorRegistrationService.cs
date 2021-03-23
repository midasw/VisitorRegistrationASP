using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VisitorRegistration.BLL.Models;
using VisitorRegistration_Models;
using X.PagedList;

namespace VisitorRegistration.BLL.Services
{
    public interface IVisitorRegistrationService
    {
        Task<ApiResult> CheckIn(string firstName, string lastName, string email, int employeeId);
        Task<ApiResult> CheckOut(string email);
        Task<IPagedList<Registration>> GetActiveRegistrations(int pageNumber = 1, int pageSize = -1, Expression<Func<Registration, bool>> predicate = null);
        Task<int> GetActiveRegistrationsCount();
        Task<IEnumerable<Registration>> GetRegistrationsById(IEnumerable<int> ids);
        Task<ApiDbResult> DeleteRegistrations(IEnumerable<Registration> registrations);
    }
}
