using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SmartBreadcrumbs.Nodes;
using System.Threading.Tasks;
using VisitorRegistration.MVC.Controllers;
using VisitorRegistration.BLL.Services;
using VisitorRegistration_Models;

namespace VisitorRegistration.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class EmployeesController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly IStringLocalizer Localizer;

        public EmployeesController(
            ICompanyService companyService,
            IStringLocalizer<SharedResources> localizer)
        {
            _companyService = companyService;
            Localizer = localizer;
        }

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
                    StatusMessage = Localizer["Successfully added one employee"];

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
                        StatusMessage = Localizer["Successfully updated one employee"];

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
                StatusMessage = Localizer["Successfully deleted one employee"];

                return RedirectToAction("View", "Companies", new { @id = employee.Company.Id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
