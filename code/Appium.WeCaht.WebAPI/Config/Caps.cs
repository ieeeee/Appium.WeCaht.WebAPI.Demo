using Appium.WeChat.WebAPI.AppiumModel.Params;

namespace Appium.WeChat.WebAPI.Config
{
    public static class Caps
    {
        public static Params_CreateSession android
        {
            get
            {
                return new Params_CreateSession()
                {
                    desiredCapabilities = new DesiredCapabilities()
                    {
                        platformName = "Android",
                        platformVersion = "7.0",
                        deviceName = "Redmi_Note_4X",
                        udid = "99bee08a0604",
                        appActivity = "com.tencent.mm.ui.LauncherUI",
                        appPackage = " com.tencent.mm",
                        noReset = true,
                        automationName = "UiAutomator2",
                        app = ""
                    }
                };
            }
        }

        public static void GetCaps()
        {
        }
    }
}
