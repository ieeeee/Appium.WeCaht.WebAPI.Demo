using Newtonsoft.Json;


namespace Appium.WeChat.WebAPI.AppiumModel.Result
{
    public class Result_SendKeys
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
