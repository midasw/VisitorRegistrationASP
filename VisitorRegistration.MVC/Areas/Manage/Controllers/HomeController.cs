using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Threading.Tasks;
using VisitorRegistration.MVC.Areas.Manage.Models;
using VisitorRegistration.BLL.Services;

namespace VisitorRegistration.MVC.Areas.Manage.Controllers
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
