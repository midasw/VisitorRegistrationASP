using System;
using X.PagedList;

namespace VisitorRegistration.MVC.Models
{
    public class PaginationViewModel
    {
        public IPagedList List { get; set; }
        public Func<int, string> GeneratePageUrl { get; set; }
    }
}
