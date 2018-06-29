using Appium.WeChat.WebAPI.Core;
using Appium.WeChat.WebAPI.Model;
using Appium.WeChat.WebAPI.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Appium.WeChat.WebAPI.Controllers
{
    public class MessageController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "WelCome Appium .Net WebApi Client.";
        }

        /// <summary>
        /// 发送内容相同
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SendWithSameContent(Params_IN_Message_Send1 param)
        {
            Loger.Debug($"Message->Send1-[param]:{param.ToJson()}");

            ReturnModel result = new ReturnModel();
            if (param != null && param.check())
            {
                if (param.createGroup && param.contacts.Length > 1)
                {
                    InteractiveCore.SendMsgWithCreateGroup(sessionId, param.contacts, param.content);
                }
                else
                {
                    for (int i = 0; i < param.contacts.Length; i++)
                    {
                        InteractiveCore.SendMsgWithContacts(sessionId, param.contacts[i], param.content);
                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                result.ErrorMessage = "参数无效";
                result.ReturnCode = ReturnCode.Error;
            }
            return BuildStringResult(result);
        }

        /// <summary>
        /// 发送内容不同
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SendWithDiffContent(List<Params_IN_Message_Send2> param)
        {
            Loger.Debug($"Message->Send2-[param]:{param.ToJson()}");

            ReturnModel result = new ReturnModel();
            if (param != null)
            {
                Params_IN_Message_Send2 tmp;
                for (int i = 0; i < param.Count; i++)
                {
                    tmp = param[i];
                    if (tmp.check())
                    {
                        InteractiveCore.SendMsgWithContacts(sessionId, tmp.contact, tmp.content);
                        Thread.Sleep(500);
                    }
                }
            }
            else
            {
                result.ErrorMessage = "参数无效";
                result.ReturnCode = ReturnCode.Error;
            }
            return BuildStringResult(result);
        }

        /// <summary>
        /// 发送已存在的群聊
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SendWithGroup(Params_IN_Message_Send3 param)
        {
            Loger.Debug($"Message->Send3-[param]:{param.ToJson()}");

            ReturnModel result = new ReturnModel();
            if (param != null && param.check())
            {
                InteractiveCore.SendMgsWithContactsGroup(sessionId, param.group, param.content);
            }
            else
            {
                result.ErrorMessage = "参数无效";
                result.ReturnCode = ReturnCode.Error;
            }
            return BuildStringResult(result);
        }
    }
}
