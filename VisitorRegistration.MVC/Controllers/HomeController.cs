using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SmartBreadcrumbs.Attributes;
using System;
using System.Threading.Tasks;
using VisitorRegistration.MVC.Models;
using VisitorRegistration.BLL.Models;
using VisitorRegistration.BLL.Services;
using VisitorRegistration.MVC.Resources;

namespace VisitorRegistration.MVC.Controllers
{
    [DefaultBreadcrumb]
    public class HomeController : BaseController
    {
        private readonly IVisitorRegistrationService _registrationService;
        private readonly ICompanyService _companyService;
        private readonly IStringLocalizer Localizer;

        public HomeController(
            IVisitorRegistrationService registrationService,
            ICompanyService companyService,
            IStringLocalizer<ValidationMessages> localizer)
        {
            _registrationService = registrationService;
            _companyService = companyService;
            Localizer = localizer;
        }

        private SelectList PopulateDropdown()
        {
            var companies = _companyService.GetAllCompanies(c => c.Employees.Count > 0).Result;

            return new SelectList(companies, "Id", "Name");
        }

        public IActionResult Index()
        {
            return View(new CheckInViewModel
            {
                Companies = PopulateDropdown()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _registrationService.CheckIn(viewModel.FirstName, viewModel.LastName, viewModel.Email, (int)viewModel.EmployeeId);

                if (result.Succeeded)
                {
                    StatusMessage = "Check in successfull!";
                    return RedirectToAction(nameof(Index));
                }

                ViewData["ErrorMessage"] = "An unexpected error occured";
            }
            else if (viewModel.CompanyId != null)
            {
                var company = await _companyService.GetCompanyById((int)viewModel.CompanyId);

                if (company != null)
                {
                    viewModel.Employees = new SelectList(company.Employees, "Id", "FullName");
                }
            }

            viewModel.Companies = PopulateDropdown();

            return View(viewModel);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string email)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var result = await _registrationService.CheckOut(email);

                    if (result.Succeeded)
                    {
                        StatusMessage = "Check out successfull. Thanks and goodbye!";
                        return RedirectToAction(nameof(Checkout));
                    }
                    else
                    {
                        if (result.Error.Code == nameof(VisitorRegistrationErrorDescriber.AlreadyCheckedOut))
                        {
                            ViewData["StatusMessage"] = Localizer["You have already been checked out."];
                        }
                        else
                        {
                            ViewData["ErrorMessage"] = Localizer["An unexpected error occured."];
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", Localizer["Please enter a valid email address."]);
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
