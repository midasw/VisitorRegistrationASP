using System;
using X.PagedList;

namespace VisitorRegistration_ASP.Models
{
    public class PaginationViewModel
    {
        public IPagedList List { get; set; }
        public Func<int, string> GeneratePageUrl { get; set; }
    }
}
