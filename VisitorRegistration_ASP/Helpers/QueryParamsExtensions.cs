using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorRegistration_ASP.Helpers
{
    // Taken from: https://stackoverflow.com/questions/43229707/append-querystring-to-href-in-asp-net-core-anchor-helper-tag

    public static class QueryParamsExtensions
    {
        public static QueryParameters GetQueryParameters(this HttpContext context)
        {
            var dictionary = context.Request.Query.ToDictionary(d => d.Key, d => d.Value.ToString());
            return new QueryParameters(dictionary);
        }
    }

    public class QueryParameters : Dictionary<string, string>
    {
        public QueryParameters() : base() { }
        public QueryParameters(int capacity) : base(capacity) { }
        public QueryParameters(IDictionary<string, string> dictionary) : base(dictionary) { }

        public QueryParameters WithRoute(string routeParam, object routeValue)
        {
            //if (!ContainsKey(routeParam))
            this[routeParam] = routeValue.ToString();

            return this;
        }
    }

}
