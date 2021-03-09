using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP
{
    //public class WebApiOptions
    //{
    //    public string BasePath { get; set; }
    //}

    //public class UserOptions
    //{
    //    public int PageSize
    //    {
    //        get => GetPageSize();
    //        set => SetPageSize(value);
    //    }

    //    private void SetPageSize(int size)
    //    {
    //        if (size <= 0) return;

    //        _httpContextAccessor.HttpContext.Response.Cookies.Append("PageSize", size.ToString());
    //    }

    //    private int GetPageSize()
    //    {
    //        if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("PageSize", out string value) && int.TryParse(value, out int size))
    //        {
    //            return size;
    //        }

    //        return 10;
    //    }
    //}

    public class AppSettings : IAppSettings
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AppSettings(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            BaseApiPath = configuration["BaseApiPath"];
        }

        public int PageSize
        {
            get => GetPageSize();
            set => SetPageSize(value);
        }

        public string BaseApiPath { get; set; }

        private void SetPageSize(int size)
        {
            if (size <= 0) return;

            _httpContextAccessor.HttpContext.Response.Cookies.Append("PageSize", size.ToString());
        }

        private int GetPageSize()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("PageSize", out string value) && int.TryParse(value, out int size))
            {
                return size;
            }

            return 10;
        }
    }
}
