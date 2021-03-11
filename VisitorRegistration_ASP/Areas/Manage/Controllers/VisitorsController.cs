using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_ASP.Controllers;
using VisitorRegistration_ASP.Helpers;
using VisitorRegistration_BLL.Services;
using VisitorRegistration_ASP.Options;
using VisitorRegistration_Models;
using X.PagedList;

namespace VisitorRegistration_ASP.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class VisitorsController : BaseController
    {
        private readonly IVisitorRegistrationService _registrationService;
        private readonly UserOptions _userOptions;

        public VisitorsController(IVisitorRegistrationService registrationService, UserOptions userOptions)
        {
            _registrationService = registrationService;
            _userOptions = userOptions;
        }

        private BreadcrumbNode BuildRootBreadcrumbNode()
        {
            return new MvcBreadcrumbNode("Index", "Visitors", "Visitors", false, null, "Manage")
            {
                Parent = new MvcBreadcrumbNode("Index", "Home", "Manage", false, null, "Manage")
            };
        }

        private IActionResult HandleReturn()
        {
            if (ReturnUrl != null)
                return Redirect(ReturnUrl);

            return RedirectToAction(nameof(Index));
        }

        [Breadcrumb("Visitors", AreaName = "Manage", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> Index(string q, int page = 1, int? pageSize = null)
        {
            int size = pageSize ?? _userOptions.PageSize;

            if (pageSize != null)
            {
                _userOptions.PageSize = size;
            }

            IPagedList<Registration> registrations;

            if (!string.IsNullOrWhiteSpace(q))
            {
                registrations = await _registrationService.GetActiveRegistrations(page, size, r =>
                    r.FirstName.Contains(q) ||
                    r.LastName.Contains(q) ||
                    r.Email.Contains(q) ||
                    r.Employee.Company.Name.Contains(q) ||
                    r.Employee.FirstName.Contains(q) ||
                    r.Employee.LastName.Contains(q)
                );

                ViewData["q"] = q;
                ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("", "", $"Search: \"{q}\"") { Parent = BuildRootBreadcrumbNode() };
            }
            else
            {
                registrations = await _registrationService.GetActiveRegistrations(page, size);
            }

            if (registrations.PageCount > 0 && page > registrations.PageCount)
            {
                return RedirectToAction(nameof(Index), HttpContext.GetQueryParameters().WithRoute("page", registrations.PageCount));
            }

            ViewData["PageSize"] = size;

            return View(registrations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string button, List<int> checkedIds)
        {
            if (checkedIds.Any())
            {
                switch (button)
                {
                    case "delete":
                        return await DeleteMultiple(checkedIds);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMultiple(List<int> checkedIds, bool confirmed = false)
        {
            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Delete", "Visitors", "Delete") { Parent = BuildRootBreadcrumbNode() };

            var registrations = await _registrationService.GetRegistrationsById(checkedIds);

            if (confirmed)
            {
                var result = await _registrationService.DeleteRegistrations(registrations);

                if (result.Succeeded)
                {
                    StatusMessage = $"Successfully deleted {result.AffectedRows} registrations";
                }

                return HandleReturn();
            }
            else
            {
                ReturnUrl = Request.Headers["Referer"];

                ViewData["ReturnUrl"] = ReturnUrl;

                return View("DeleteMultiple", registrations);
            }
        }
    }
}
