using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appium.WeChat.WebAPI.Controllers
{
    public class WelComeController : BaseController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return BuildStringResult("WelCome Appium .Net WebApi Client.");
        }
    }
}
