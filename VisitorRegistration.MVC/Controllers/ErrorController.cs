using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisitorRegistration.MVC.Models;

namespace VisitorRegistration.MVC.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("500")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AppError()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
