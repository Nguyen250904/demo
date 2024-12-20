using System.Web;
using System.Web.Mvc;

namespace nguyennhatnguyen2122110318
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
