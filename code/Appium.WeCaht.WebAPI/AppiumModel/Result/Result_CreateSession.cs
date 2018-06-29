using Newtonsoft.Json;


namespace Appium.WeChat.WebAPI.AppiumModel.Result
{
    public class Warnings
    {
    }

    public class Desired
    {
        [JsonProperty("platformName")]
        public string PlatformName { get; set; }

        [JsonProperty("platformVersion")]
        public string PlatformVersion { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("udid")]
        public string Udid { get; set; }

        [JsonProperty("appActivity")]
        public string AppActivity { get; set; }

        [JsonProperty("appPackage")]
        public string AppPackage { get; set; }

        [JsonProperty("noReset")]
        public bool NoReset { get; set; }

        [JsonProperty("automationName")]
        public string AutomationName { get; set; }

        [JsonProperty("app")]
        public string App { get; set; }
    }

    public class ViewportRect
    {
        [JsonProperty("left")]
        public int Left { get; set; }

        [JsonProperty("top")]
        public int Top { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class Value
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("webStorageEnabled")]
        public bool WebStorageEnabled { get; set; }

        [JsonProperty("takesScreenshot")]
        public bool TakesScreenshot { get; set; }

        [JsonProperty("javascriptEnabled")]
        public bool JavascriptEnabled { get; set; }

        [JsonProperty("databaseEnabled")]
        public bool DatabaseEnabled { get; set; }

        [JsonProperty("networkConnectionEnabled")]
        public bool NetworkConnectionEnabled { get; set; }

        [JsonProperty("locationContextEnabled")]
        public bool LocationContextEnabled { get; set; }

        [JsonProperty("warnings")]
        public Warnings Warnings { get; set; }

        [JsonProperty("desired")]
        public Desired Desired { get; set; }

        [JsonProperty("platformName")]
        public string PlatformName { get; set; }

        [JsonProperty("platformVersion")]
        public string PlatformVersion { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("udid")]
        public string Udid { get; set; }

        [JsonProperty("appActivity")]
        public string AppActivity { get; set; }

        [JsonProperty("appPackage")]
        public string AppPackage { get; set; }

        [JsonProperty("noReset")]
        public bool NoReset { get; set; }

        [JsonProperty("automationName")]
        public string AutomationName { get; set; }

        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("deviceUDID")]
        public string DeviceUDID { get; set; }

        [JsonProperty("deviceScreenSize")]
        public string DeviceScreenSize { get; set; }

        [JsonProperty("deviceScreenDensity")]
        public int DeviceScreenDensity { get; set; }

        [JsonProperty("deviceModel")]
        public string DeviceModel { get; set; }

        [JsonProperty("deviceManufacturer")]
        public string DeviceManufacturer { get; set; }

        [JsonProperty("deviceApiLevel")]
        public int DeviceApiLevel { get; set; }

        [JsonProperty("pixelRatio")]
        public int PixelRatio { get; set; }

        [JsonProperty("statBarHeight")]
        public int StatBarHeight { get; set; }

        [JsonProperty("viewportRect")]
        public ViewportRect ViewportRect { get; set; }
    }

    public class Result_CreateSession
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}
