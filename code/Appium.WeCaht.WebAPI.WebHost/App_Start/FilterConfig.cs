using System.Web;
using System.Web.Mvc;

namespace Appium.WeChat.WebAPI.WebHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
