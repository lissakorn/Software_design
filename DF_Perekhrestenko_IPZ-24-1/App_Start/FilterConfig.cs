using System.Web;
using System.Web.Mvc;

namespace DF_Perekhrestenko_IPZ_24_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
