using Newtonsoft.Json;


namespace Appium.WeChat.WebAPI.AppiumModel.Result
{
    public class Result_ClickElement
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("value")]
        public bool Value { get; set; }
    }
}
