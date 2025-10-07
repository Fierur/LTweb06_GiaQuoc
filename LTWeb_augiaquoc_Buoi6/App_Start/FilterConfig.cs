using System.Web;
using System.Web.Mvc;

namespace LTWeb_augiaquoc_Buoi6
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
