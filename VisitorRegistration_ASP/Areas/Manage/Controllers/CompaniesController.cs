using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_ASP.Controllers;
using VisitorRegistration_ASP.Helpers;
using VisitorRegistration_ASP.Options;
using VisitorRegistration_BLL.Services;
using VisitorRegistration_Models;

namespace VisitorRegistration_ASP.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class CompaniesController : BaseController
    {
        private readonly ICompanyService _service;
        private readonly UserOptions _userOptions;

        public CompaniesController(ICompanyService service, UserOptions userOptions)
        {
            _service = service;
            _userOptions = userOptions;
        }

        private static BreadcrumbNode BuildRootBreadcrumbNode()
        {
            return new MvcBreadcrumbNode("Index", "Companies", "Companies", false, null, "Manage")
            {
                Parent = new MvcBreadcrumbNode("Index", "Home", "Manage", false, null, "Manage")
            };
        }

        private void UpdateReturnUrl()
        {
            ReturnUrl = GetRefererUrl();
        }

        private IActionResult HandleReturn()
        {
            if (ReturnUrl != null)
                return Redirect(ReturnUrl);

            return RedirectToAction(nameof(Index));
        }

        [Breadcrumb("Companies", AreaName = "Manage", FromAction = "Index", FromController = typeof(HomeController))]
        public async Task<IActionResult> Index(int page = 1, int? pageSize = null)
        {
            int size = pageSize ?? _userOptions.PageSize;

            if (pageSize != null)
            {
                _userOptions.PageSize = size;
            }

            var companies = await _service.GetAllCompaniesPaged(page, size);

            companies = await _service.GetAllCompaniesPaged(pageSize: 10);

            if (companies.PageCount > 0 && page > companies.PageCount)
            {
                return RedirectToAction(nameof(Index), HttpContext.GetQueryParameters().WithRoute("page", companies.PageCount));
            }

            ViewData["PageSize"] = size;

            return View(companies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Companies", AreaName = "Manage", FromAction = "Index", FromController = typeof(HomeController))]
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

        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company company = await _service.GetCompanyById((int)id);
            if (company == null)
            {
                return NotFound();
            }

            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("View", "Companies", company.Name, false, null, "Manage") { Parent = BuildRootBreadcrumbNode() };

            UpdateReturnUrl();

            return View(company);
        }

        [Breadcrumb("Create company", FromAction = "Index", FromController = typeof(CompaniesController))]
        public IActionResult Create()
        {
            UpdateReturnUrl();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Create company", FromAction = "Index", FromController = typeof(CompaniesController))]
        public async Task<IActionResult> Create([Bind("Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateCompany(company);

                if (result.Succeeded)
                {
                    StatusMessage = "Successfully created one company";

                    return HandleReturn();
                }
            }

            return View(company);
        }

        [Breadcrumb("Edit company", FromAction = "Index", FromController = typeof(CompaniesController))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company company = await _service.GetCompanyById((int)id);
            if (company == null)
            {
                return NotFound();
            }

            UpdateReturnUrl();

            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Edit company", FromAction = "Index", FromController = typeof(CompaniesController))]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _service.UpdateCompany(company);

                if (result.Succeeded)
                {
                    StatusMessage = "Successfully updated one company";

                    return HandleReturn();
                }
            }

            return View(company);
        }

        [Breadcrumb("Delete company", FromAction = "Index", FromController = typeof(CompaniesController))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Company company = await _service.GetCompanyById((int)id);
            if (company == null)
            {
                return NotFound();
            }

            UpdateReturnUrl();

            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Company company = await _service.GetCompanyById(id);
            if (company == null)
            {
                return NotFound();
            }

            var result = await _service.DeleteCompany(company);

            if (result.Succeeded)
            {
                StatusMessage = "Successfully deleted one company";
            }

            return HandleReturn();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMultiple(List<int> checkedIds, bool confirmed = false)
        {
            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Delete", "Companies", "Delete") { Parent = BuildRootBreadcrumbNode() };

            var companies = await _service.GetCompaniesById(checkedIds);

            if (confirmed)
            {
                var result = await _service.DeleteCompanies(companies);

                if (result.Succeeded)
                {
                    StatusMessage = $"Successfully deleted {result.AffectedRows} companies";
                }

                return HandleReturn();
            }
            else
            {
                UpdateReturnUrl();

                return View("DeleteMultiple", companies);
            }
        }
    }
}
