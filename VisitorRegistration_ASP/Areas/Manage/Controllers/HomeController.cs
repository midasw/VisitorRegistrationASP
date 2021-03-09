using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_ASP.Areas.Manage.Models;
using VisitorRegistration_BLL.Services;

namespace VisitorRegistration_ASP.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Route("Manage")]
    public class HomeController : Controller
    {
        private readonly IVisitorRegistrationService _registrationService;

        public HomeController(IVisitorRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [Route("")]
        [Breadcrumb("Manage", AreaName = "Manage")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                VisitorsCount = await _registrationService.GetActiveRegistrationsCount()
            };

            return View(viewModel);
        }
    }
}
