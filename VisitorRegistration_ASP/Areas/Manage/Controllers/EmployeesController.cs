using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_ASP.Controllers;
using VisitorRegistration_BLL.Services;
using VisitorRegistration_DAL.UnitOfWork;
using VisitorRegistration_Models;

namespace VisitorRegistration_ASP.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class EmployeesController : BaseController
    {
        private readonly ICompanyService _companyService;

        private BreadcrumbNode BuildRootBreadcrumbNode(Company company)
        {
            return new MvcBreadcrumbNode("View", "Companies", company.Name, false, null, "Manage")
            {
                RouteValues = new { id = company.Id },
                Parent = new MvcBreadcrumbNode("Index", "Companies", "Companies", false, null, "Manage")
                {
                    Parent = new MvcBreadcrumbNode("Index", "Home", "Manage", false, null, "Manage")
                }
            };
        }

        public EmployeesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int companyId)
        {
            Company company = await _companyService.GetCompanyById(companyId);
            if (company == null)
            {
                return NotFound();
            }

            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Index", "Companies", "Create employee") { Parent = BuildRootBreadcrumbNode(company) };

            ViewBag.CompanyId = companyId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int companyId, [Bind("Id,FirstName,LastName")] Employee employee)
        {
            Company company = await _companyService.GetCompanyById(companyId);

            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Index", "Companies", "Create employee") { Parent = BuildRootBreadcrumbNode(company) };

            if (ModelState.IsValid)
            {
                if (company == null)
                {
                    return NotFound();
                }

                employee.Company = company;

                var result = await _companyService.CreateEmployee(employee);

                if (result.Succeeded)
                {
                    StatusMessage = "Successfully added one employee";

                    return RedirectToAction("View", "Companies", new { @id = employee.Company.Id });
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CompanyId = companyId;

            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await _companyService.GetEmployeeById((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Index", "Companies", "Edit employee") { Parent = BuildRootBreadcrumbNode(employee.Company) };

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Employee model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Employee employee = await _companyService.GetEmployeeById(id);

                ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Index", "Companies", "Edit employee") { Parent = BuildRootBreadcrumbNode(employee.Company) };

                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;

                    var result = await _companyService.UpdateEmployee(employee);

                    if (result.Succeeded)
                    {
                        StatusMessage = "Successfully updated one employee";

                        return RedirectToAction("View", "Companies", new { @id = employee.Company.Id });
                    }
                }
                else
                {
                    return NotFound();
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee employee = await _companyService.GetEmployeeById((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["BreadcrumbNode"] = new MvcBreadcrumbNode("Index", "Companies", "Delete employee") { Parent = BuildRootBreadcrumbNode(employee.Company) };

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await _companyService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var result = await _companyService.DeleteEmployee(employee);

            if (result.Succeeded)
            {
                StatusMessage = "Successfully deleted one employee";

                return RedirectToAction("View", "Companies", new { @id = employee.Company.Id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
