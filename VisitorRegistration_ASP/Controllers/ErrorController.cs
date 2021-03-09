using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VisitorRegistration_ASP.Models;

namespace VisitorRegistration_ASP.Controllers
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
