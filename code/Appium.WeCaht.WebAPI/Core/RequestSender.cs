using Appium.WeChat.WebAPI.Config;
using Appium.WeChat.WebAPI.Tools;
using RestSharp;
using System;

namespace Appium.WeChat.WebAPI.Core
{
    public static class RequestSender
    {
        private static RestClient client;

        static RequestSender()
        {
            var serverConfig = new AppiumServer();
            client = new RestClient($"{serverConfig.host}:{serverConfig.port}/wd/hub");
        }

        public static T Send<T>(RestRequest request) where T : new()
        {
            var response = client.Execute<T>(request);
            return response.Data;
        }
    }
}
