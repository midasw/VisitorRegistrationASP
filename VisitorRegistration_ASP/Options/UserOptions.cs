using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP.Options
{
    public class UserOptions
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserOptions(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int PageSize
        {
            get => GetPageSize();
            set => SetPageSize(value);
        }

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
