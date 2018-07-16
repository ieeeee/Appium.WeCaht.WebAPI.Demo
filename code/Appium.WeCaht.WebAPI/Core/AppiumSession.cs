using Appium.WeChat.WebAPI.AppiumModel.Params;
using Appium.WeChat.WebAPI.AppiumModel.Result;
using Appium.WeChat.WebAPI.Config;
using Appium.WeChat.WebAPI.Tools;
using RestSharp;
using System;

namespace Appium.WeChat.WebAPI.Core
{
    public static class AppiumSession
    {
        /// <summary>
        /// 全局共享sessionId
        /// </summary>
        public static string sessionId { get; set; }

        static AppiumSession()
        {
            sessionId = GetSessionId();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetSessionId()
        {
            try
            {
                var request = new RestRequest("session", Method.POST);
                request.AddJsonBody(InitDeviceConfig.capsinfo);

                var session = RequestSender.Send<Result_CreateSession>(request);
                return session.SessionId;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void KeepSessionLive()
        {
            try
            {
                var request = new RestRequest("session/{sessionId}", Method.GET);
                request.AddUrlSegment("sessionId", sessionId);

                var capabilities = RequestSender.Send<Result_CreateSession>(request);

                Loger.Debug("AppiumSession->KeepSessionLive-[capabilities]:");
                Loger.Debug(capabilities.ToJson());

                if (capabilities == null || capabilities.Status != 0)
                {
                    //尝试重新获取sessionId,3次
                    for (int i = 0; i < 3; i++)
                    {
                        sessionId = GetSessionId();
                        if (!string.IsNullOrWhiteSpace(sessionId))
                        {
                            //成功获取sessionId后退出循环
                            return;
                        }
                    }

                    //尝试重新获取sessionId失败，3次
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
