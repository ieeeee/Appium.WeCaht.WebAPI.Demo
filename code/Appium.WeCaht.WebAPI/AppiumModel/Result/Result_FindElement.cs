using Newtonsoft.Json;


namespace Appium.WeChat.WebAPI.AppiumModel.Result
{
    public class Valuee
    {

        [JsonProperty("ELEMENT")]
        public string ELEMENT { get; set; }
    }

    public class Result_FindElement
    {

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("value")]
        public Valuee Value { get; set; }
    }
}
