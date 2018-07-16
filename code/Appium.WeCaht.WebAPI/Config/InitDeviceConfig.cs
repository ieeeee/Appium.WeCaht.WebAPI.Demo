using Appium.WeChat.WebAPI.AppiumModel.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WeChat.WebAPI.Config
{
    /// <summary>
    /// 
    /// </summary>
    public static class InitDeviceConfig
    {
        public static readonly string DeviceName = System.Configuration.ConfigurationManager.AppSettings["RoBotDevice"] ?? "S_G900F";
        public static WeChatElements element;
        public static Params_CreateSession capsinfo;

        /// <summary>
        /// 
        /// </summary>
        static InitDeviceConfig()
        {
            switch (DeviceName)
            {
                case "Redmi_Note_4X":
                    capsinfo = new Params_CreateSession()
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
                            unicodeKeyboard = true,
                            resetKeyboard = true
                            //automationName = "UiAutomator2",
                        }
                    };
                    element = new WeChatElements()
                    {
                        Back = "com.tencent.mm:id/hl",
                        NavBar = "com.tencent.mm:id/cdj",

                        Home_Contact = "com.tencent.mm:id/as6",

                        Contact_Group = "com.tencent.mm:id/k3",
                        Contact_Group_Name = "com.tencent.mm:id/aaq",

                        Contact_Person = "com.tencent.mm:id/k9",
                        Contact_Person_SendMsg = "com.tencent.mm:id/ap1",

                        Chat_Input_Message = "com.tencent.mm:id/ac8",
                        Chat_Btn_Send = "com.tencent.mm:id/acd",

                        Right_Top_Plus = "com.tencent.mm:id/gd",
                        Right_Top_Plus_GroupChat = "com.tencent.mm:id/ge",
                        Right_Top_Plus_GroupChat_Person = "com.tencent.mm:id/lp",
                        Right_Top_Plus_GroupChat_Ok = "com.tencent.mm:id/hg"
                    };
                    break;

                case "S_G900F":
                default:
                    capsinfo = new Params_CreateSession()
                    {
                        desiredCapabilities = new DesiredCapabilities()
                        {
                            platformName = "Android",
                            platformVersion = "4.4.2",
                            deviceName = "SM-G900F",
                            udid = "emulator-5554",
                            appActivity = "com.tencent.mm.ui.LauncherUI",
                            appPackage = " com.tencent.mm",
                            noReset = true,
                            unicodeKeyboard = true,
                            resetKeyboard = true
                            //automationName = "UiAutomator2",
                            //app = ""
                        }
                    };
                    element = new WeChatElements()
                    {
                        Back = "com.tencent.mm:id/hi",
                        NavBar = "com.tencent.mm:id/ca5",

                        Home_Contact = "com.tencent.mm:id/apx",

                        Contact_Group = "com.tencent.mm:id/j5",
                        Contact_Group_Name = "com.tencent.mm:id/a9v",

                        Contact_Person = "com.tencent.mm:id/ja",
                        Contact_Person_SendMsg = "com.tencent.mm:id/anc",

                        Chat_Input_Message = "com.tencent.mm:id/aac",
                        Chat_Btn_Send = "com.tencent.mm:id/aai",

                        Right_Top_Plus = "com.tencent.mm:id/g_",
                        Right_Top_Plus_GroupChat = "com.tencent.mm:id/ga",
                        Right_Top_Plus_GroupChat_Person = "com.tencent.mm:id/kr",
                        Right_Top_Plus_GroupChat_Ok = "com.tencent.mm:id/hd"
                    };
                    break;
            }
        }
    }
}
