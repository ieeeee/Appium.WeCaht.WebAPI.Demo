using Appium.WeChat.WebAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appium.WeChat.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public readonly string sessionId = AppiumSession.sessionId;

        public HttpResponseMessage BuildStringResult(object obj, ResponseMediaType contentType = ResponseMediaType.JSON)
        {
            try
            {
                HttpResponseMessage res = Request.CreateResponse(HttpStatusCode.OK);

                StringBuilder sbResult = new StringBuilder();

                if (obj is string)
                {
                    sbResult.Append(obj);
                }
                else
                {
                    sbResult.Append(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                }

                string strMediaType = string.IsNullOrWhiteSpace(supportMediaType[contentType]) ? supportMediaType[ResponseMediaType.JSON] : supportMediaType[contentType];

                res.Content = new StringContent(sbResult.ToString(), Encoding.UTF8, strMediaType);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Dictionary<ResponseMediaType, string> supportMediaType
        {
            get
            {
                return new Dictionary<ResponseMediaType, string>() {
                    { ResponseMediaType.JSON,"application/json" },
                    { ResponseMediaType.TEXT,"text/plain" },
                    { ResponseMediaType.HTML,"text/html" },
                    { ResponseMediaType.XML,"text/xml" }
                };
            }
        }

        public enum ResponseMediaType
        {
            JSON,
            TEXT,
            HTML,
            XML
        }

    }
}
