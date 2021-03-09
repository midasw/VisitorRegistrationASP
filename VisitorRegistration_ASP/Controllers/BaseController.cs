using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP.Controllers
{
    public class BaseController : Controller
    {
        protected string GetRefererUrl()
        {
            return Request.GetTypedHeaders().Referer.ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                ViewData["ReturnUrl"] = ReturnUrl;
            }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public string ReturnUrl
        {
            get
            {
                var obj = TempData.Peek("ReturnUrl");
                return obj != null ? obj.ToString() : null;
            }
            set
            {
                ViewData["ReturnUrl"] = value;
                TempData["ReturnUrl"] = value;
            }
        }
    }
}
