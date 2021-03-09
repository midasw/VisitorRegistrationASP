using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP
{
    public interface IAppSettings
    {
        int PageSize { get; set; }
        string BaseApiPath { get; set; }
    }
}
