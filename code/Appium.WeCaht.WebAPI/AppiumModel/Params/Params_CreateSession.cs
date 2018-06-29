using Newtonsoft.Json;

namespace Appium.WeChat.WebAPI.AppiumModel.Params
{
    public class Params_CreateSession
    {
        [JsonProperty("desiredCapabilities")]
        public DesiredCapabilities desiredCapabilities { get; set; }
    }

    public class DesiredCapabilities
    {
        [JsonProperty("platformName")]
        public string platformName { get; set; }

        [JsonProperty("platformVersion")]
        public string platformVersion { get; set; }

        [JsonProperty("deviceName")]
        public string deviceName { get; set; }

        [JsonProperty("udid")]
        public string udid { get; set; }

        [JsonProperty("appActivity")]
        public string appActivity { get; set; }

        [JsonProperty("appPackage")]
        public string appPackage { get; set; }

        [JsonProperty("noReset")]
        public bool noReset { get; set; }

        [JsonProperty("automationName")]
        public string automationName { get; set; }

        [JsonProperty("newCommandTimeout")]
        public int newCommandTimeout { get; set; } = 60 * 5;

        [JsonProperty("app")]
        public string app { get; set; }
    }
}
