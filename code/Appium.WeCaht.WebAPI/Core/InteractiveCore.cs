using Appium.WeChat.WebAPI.AppiumModel.Result;
using Appium.WeChat.WebAPI.Config;
using Appium.WeChat.WebAPI.Tools;
using RestSharp;
using System;
using System.Threading;

namespace Appium.WeChat.WebAPI.Core
{
    public static class InteractiveCore
    {
        /// <summary>
        /// 查找元素
        /// </summary>
        /// <param name="sessionId"></param>
        public static string FindElement(string sessionId, string usingType, string value)
        {
            var request = new RestRequest("session/{sessionId}/element", Method.POST);
            request.AddUrlSegment("sessionId", sessionId);
            request.AddJsonBody(new { @using = usingType, value = value });

            try
            {
                var response = RequestSender.Send<Result_FindElement>(request);
                return response.Value.ELEMENT;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 点击元素
        /// </summary>
        /// <param name="sessionId"></param>
        public static bool Clicklement(string sessionId, string element)
        {
            var request = new RestRequest("session/{sessionId}/element/{element}/click", Method.POST);
            request.AddUrlSegment("sessionId", sessionId);
            request.AddUrlSegment("element", element);

            try
            {
                var response = RequestSender.Send<Result_ClickElement>(request);

                //点击后暂停一下，等待设备界面响应
                Thread.Sleep(500);

                return response != null && response.Value;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置元素值
        /// </summary>
        /// <param name="sessionId"></param>
        public static bool SetElementVal(string sessionId, string element, string[] text)
        {
            var request = new RestRequest("session/{sessionId}/element/{element}/value", Method.POST);
            request.AddUrlSegment("sessionId", sessionId);
            request.AddUrlSegment("element", element);

            request.AddJsonBody(new { value = text });

            try
            {
                var response = RequestSender.Send<Result_SendKeys>(request);
                return response != null && response.Status == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static void DeviceBack(string sessionId, string element)
        {
            var request = new RestRequest("/session/{sessionId}/appium/device/press_keycode", Method.POST);
            request.AddUrlSegment("sessionId", sessionId);

            request.AddJsonBody(new { keycode = 4, metastate = string.Empty, flags = string.Empty });

            try
            {
                var response = RequestSender.Send<object>(request);

                //点击后暂停一下，等待设备界面响应
                Thread.Sleep(500);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        public static void Quit(string sessionId)
        {
            var request = new RestRequest("session/{sessionId}", Method.DELETE);
            request.AddUrlSegment("sessionId", sessionId);

            try
            {
                var response = RequestSender.Send<object>(request);
                Loger.Error($"Quit---> {sessionId}");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="message"></param>
        public static void SendMsg(string appiumSessionId, string contact, string message)
        {
            if (!string.IsNullOrWhiteSpace(appiumSessionId))
            {
                //Q:查找联系人
                string element = string.Empty;
                element = FindElement(appiumSessionId, "xpath", $"//android.view.View[@resource-id=\"{InitDeviceConfig.element.Home_Contact}\" and @text=\"{contact}\"]");
                if (!string.IsNullOrWhiteSpace(element))
                {
                    //C:点击联系人
                    if (Clicklement(appiumSessionId, element))
                    {
                        //Q:查找消息输入框
                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Input_Message);
                        if (!string.IsNullOrWhiteSpace(element))
                        {
                            //S:输入消息内容
                            if (SetElementVal(appiumSessionId, element, new string[] { message }))
                            {
                                //Q:查找发送按钮
                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Btn_Send);

                                if (!string.IsNullOrWhiteSpace(element))
                                {
                                    //C:点击发送按钮
                                    if (Clicklement(appiumSessionId, element))
                                    {
                                        //Q:查找返回按钮
                                        //element = FindElement(appiumSessionId, "accessibility id", "返回");
                                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Back);
                                        if (!string.IsNullOrWhiteSpace(element))
                                        {
                                            //C:点击返回按钮
                                            if (Clicklement(appiumSessionId, element))
                                            {
                                                //Done
                                                Loger.Info("OK---> 消息发送完成");
                                            }
                                            else
                                            {
                                                Loger.Error("ERROR---> 点击返回按钮");
                                            }
                                        }
                                        else
                                        {
                                            Loger.Error("ERROR---> 查找返回按钮");
                                        }
                                    }
                                    else
                                    {
                                        Loger.Error("ERROR---> 点击发送按钮");
                                    }
                                }
                                else
                                {
                                    Loger.Error("ERROR---> 查找发送按钮");
                                }
                            }
                            else
                            {
                                Loger.Error("ERROR---> 输入消息内容");
                            }
                        }
                        else
                        {
                            Loger.Error("ERROR---> 查找消息输入框");
                        }
                    }
                    else
                    {
                        Loger.Error("ERROR---> 点击联系人");
                    }
                }
                else
                {
                    Loger.Error("ERROR---> 查找联系人");
                }
            }
            else
            {
                Loger.Error("ERROR---> 获取sessionId");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appiumSessionId"></param>
        /// <param name="contact"></param>
        /// <param name="message"></param>
        public static void SendMsgWithContacts(string appiumSessionId, string contact, string message)
        {
            //S:切换至“通讯录”
            if (SwitchTabBar(appiumSessionId, "通讯录"))
            {
                //Q:查找“联系人”
                string element = string.Empty;
                //2种方法都行
                //element = FindElement(appiumSessionId, "xpath", $"//android.view.View[@resource-id=\"{InitDeviceConfig.element.Contact_Person}\" and @content-desc=\"{contact}\"]");
                element = FindElement(appiumSessionId, "accessibility id", contact);
                if (!string.IsNullOrWhiteSpace(element))
                {
                    //C:点击“联系人”
                    if (Clicklement(appiumSessionId, element))
                    {
                        //Q:查找“发消息”
                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Contact_Person_SendMsg);
                        if (!string.IsNullOrWhiteSpace(element))
                        {
                            //C:点击“发消息”
                            if (Clicklement(appiumSessionId, element))
                            {
                                //Q:查找“消息输入框”
                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Input_Message);

                                if (!string.IsNullOrWhiteSpace(element))
                                {
                                    //S:输入消息内容
                                    if (SetElementVal(appiumSessionId, element, new string[] { message }))
                                    {
                                        //Q:查找“发送按钮”
                                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Btn_Send);

                                        if (!string.IsNullOrWhiteSpace(element))
                                        {
                                            //C:点击“发送按钮”
                                            if (Clicklement(appiumSessionId, element))
                                            {
                                                //Q:查找“返回按钮”
                                                //element = FindElement(appiumSessionId, "accessibility id", "返回");
                                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Back);
                                                if (!string.IsNullOrWhiteSpace(element))
                                                {
                                                    //C:点击“返回按钮”
                                                    if (Clicklement(appiumSessionId, element))
                                                    {
                                                        //Done
                                                        Loger.Info("OK---> 消息发送完成");
                                                    }
                                                    else
                                                    {
                                                        Loger.Error("ERROR---> 点击返回按钮");
                                                    }
                                                }
                                                else
                                                {
                                                    Loger.Error("ERROR---> 查找返回按钮");
                                                }
                                            }
                                            else
                                            {
                                                Loger.Error("ERROR---> 点击发送按钮");
                                            }
                                        }
                                        else
                                        {
                                            Loger.Error("ERROR---> 查找发送按钮");
                                        }
                                    }
                                    else
                                    {
                                        Loger.Error("ERROR---> 输入消息内容");
                                    }
                                }
                                else
                                {
                                    Loger.Error("ERROR---> 查找消息输入框");
                                }
                            }
                            else
                            {
                                Loger.Error("ERROR---> 点击联系人");
                            }
                        }
                        else
                        {
                            Loger.Error("ERROR---> 从通讯录点击“发消息”");
                        }
                    }
                    else
                    {
                        Loger.Error("ERROR---> 从通讯录点击“发消息”");
                    }
                }
                else
                {
                    Loger.Error("ERROR---> 查找联系人");
                }
            }
            else
            {
                Loger.Error("ERROR---> 切换至通讯录");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appiumSessionId"></param>
        /// <param name="contact"></param>
        /// <param name="message"></param>
        public static void SendMsgWithCreateGroup(string appiumSessionId, string[] contacts, string message)
        {
            if (!string.IsNullOrWhiteSpace(appiumSessionId))
            {
                //Q:查找右上+
                string element = string.Empty;
                //element = FindElement(appiumSessionId, "accessibility id", "更多功能按钮");
                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Right_Top_Plus);
                if (!string.IsNullOrWhiteSpace(element))
                {
                    //C:点击右上+
                    if (Clicklement(appiumSessionId, element))
                    {
                        //Q:查找发起群聊
                        element = FindElement(appiumSessionId, "xpath", $"//android.widget.TextView[@resource-id=\"{InitDeviceConfig.element.Right_Top_Plus_GroupChat}\" and @text=\"发起群聊\"]");
                        if (!string.IsNullOrWhiteSpace(element))
                        {
                            //C:点击发起群聊
                            if (Clicklement(appiumSessionId, element))
                            {
                                //勾选所有联系人
                                for (int i = 0; i < contacts.Length; i++)
                                {
                                    string elContact = FindElement(appiumSessionId, "xpath", $"//android.widget.TextView[@resource-id=\"{InitDeviceConfig.element.Right_Top_Plus_GroupChat_Person}\" and @text=\"{contacts[i]}\"]");
                                    if (!string.IsNullOrWhiteSpace(element))
                                    {
                                        Clicklement(appiumSessionId, elContact);
                                    }
                                }

                                //Q:查找右上确定
                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Right_Top_Plus_GroupChat_Ok);
                                if (!string.IsNullOrWhiteSpace(element))
                                {
                                    //C:点击右上确定
                                    if (Clicklement(appiumSessionId, element))
                                    {
                                        //Q:查找“消息输入框”
                                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Input_Message);

                                        if (!string.IsNullOrWhiteSpace(element))
                                        {
                                            //S:输入消息内容
                                            if (SetElementVal(appiumSessionId, element, new string[] { message }))
                                            {
                                                //Q:查找“发送按钮”
                                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Btn_Send);

                                                if (!string.IsNullOrWhiteSpace(element))
                                                {
                                                    //C:点击“发送按钮”
                                                    if (Clicklement(appiumSessionId, element))
                                                    {
                                                        //Q:查找“返回按钮”
                                                        //element = FindElement(appiumSessionId, "accessibility id", "返回");
                                                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Back);
                                                        if (!string.IsNullOrWhiteSpace(element))
                                                        {
                                                            //C:点击“返回按钮”
                                                            if (Clicklement(appiumSessionId, element))
                                                            {
                                                                //Done
                                                                Loger.Info("OK---> 消息发送完成");
                                                            }
                                                            else
                                                            {
                                                                Loger.Error("ERROR---> 点击返回按钮");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Loger.Error("ERROR---> 查找返回按钮");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Loger.Error("ERROR---> 点击发送按钮");
                                                    }
                                                }
                                                else
                                                {
                                                    Loger.Error("ERROR---> 查找发送按钮");
                                                }
                                            }
                                            else
                                            {
                                                Loger.Error("ERROR---> 输入消息内容");
                                            }
                                        }
                                        else
                                        {
                                            Loger.Error("ERROR---> 查找消息输入框");
                                        }
                                    }
                                    else
                                    {
                                        Loger.Error("ERROR---> 点击右上确定");
                                    }
                                }
                                else
                                {
                                    Loger.Error("ERROR---> 查找右上确定");
                                }
                            }
                            else
                            {
                                Loger.Error("ERROR---> 点击发起群聊");
                            }
                        }
                        else
                        {
                            Loger.Error("ERROR---> 查找发起群聊");
                        }
                    }
                    else
                    {
                        Loger.Error("ERROR---> 点击右上+");
                    }
                }
                else
                {
                    Loger.Error("ERROR---> 查找右上+");
                }
            }
            else
            {
                Loger.Error("ERROR---> 获取sessionId");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appiumSessionId"></param>
        /// <param name="contact"></param>
        /// <param name="message"></param>
        public static void SendMgsWithContactsGroup(string appiumSessionId, string groupname, string message)
        {
            //S:切换至通讯录
            if (SwitchTabBar(appiumSessionId, "通讯录"))
            {
                //查找元素
                string element = string.Empty;

                //Q:查找群聊
                element = FindElement(appiumSessionId, "xpath", $"//android.widget.TextView[@resource-id=\"{InitDeviceConfig.element.Contact_Group}\" and @text=\"群聊\"]");
                if (!string.IsNullOrWhiteSpace(element))
                {
                    //C:点击群聊
                    if (Clicklement(appiumSessionId, element))
                    {
                        //Q:查找群聊分组
                        element = FindElement(appiumSessionId, "xpath", $"//android.widget.TextView[@resource-id=\"{InitDeviceConfig.element.Contact_Group_Name}\" and @text=\"{groupname}\"]");
                        if (!string.IsNullOrWhiteSpace(element))
                        {
                            //C:点击群聊分组
                            if (Clicklement(appiumSessionId, element))
                            {
                                //Q:查找消息输入框
                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Input_Message);
                                if (!string.IsNullOrWhiteSpace(element))
                                {
                                    //S:输入消息内容
                                    if (SetElementVal(appiumSessionId, element, new string[] { message }))
                                    {
                                        //Q:查找发送按钮
                                        element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Chat_Btn_Send);
                                        if (!string.IsNullOrWhiteSpace(element))
                                        {
                                            //C:点击“发送按钮”
                                            if (Clicklement(appiumSessionId, element))
                                            {
                                                //Q:查找“返回按钮”
                                                //element = FindElement(appiumSessionId, "accessibility id", "返回");
                                                element = FindElement(appiumSessionId, "id", InitDeviceConfig.element.Back);
                                                if (!string.IsNullOrWhiteSpace(element))
                                                {
                                                    //C:点击“返回按钮”
                                                    if (Clicklement(appiumSessionId, element))
                                                    {
                                                        //Done
                                                        Loger.Info("OK---> 消息发送完成");
                                                    }
                                                    else
                                                    {
                                                        Loger.Error("ERROR---> 点击返回按钮");
                                                    }
                                                }
                                                else
                                                {
                                                    Loger.Error("ERROR---> 查找返回按钮");
                                                }
                                            }
                                            else
                                            {
                                                Loger.Error("ERROR---> 点击发送按钮");
                                            }
                                        }
                                        else
                                        {
                                            Loger.Error("ERROR---> 查找发送按钮");
                                        }
                                    }
                                    else
                                    {
                                        Loger.Error("ERROR---> 输入消息内容");
                                    }
                                }
                                else
                                {
                                    Loger.Error("ERROR---> 查找消息输入框");
                                }
                            }
                            else
                            {
                                Loger.Error("ERROR---> 点击群聊分组");
                            }
                        }
                        else
                        {
                            Loger.Error("ERROR---> 查找群聊分组");
                        }
                    }
                    else
                    {
                        Loger.Error("ERROR---> 点击群聊");
                    }
                }
                else
                {
                    Loger.Error("ERROR---> 查找群聊");
                }
            }
            else
            {
                Loger.Error("ERROR---> 切换至通讯录");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appiumSessionId"></param>
        /// <param name="content"></param>
        public static void PostMoments(string appiumSessionId, string content)
        {
            //朋友圈发送流程

            //1.切换tabBar至“发现”
            //if (SwitchTabBar(appiumSessionId, "发现"))

            //2.查找、点击 “朋友圈”
            ////android.widget.TextView[@resource-id=\"android:id/title\" and @text=\"朋友圈\"]

            //3.查找、点击 “相机”
            //accessibility id    拍照分享
            //id  com.tencent.mm:id / hh
            //xpath	//android.widget.ImageButton[@content-desc="拍照分享"]

            //4.可发送纯文本、图片、视频等...暂时不做研究，大致方法就是查找、点击；
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appiumSessionId"></param>
        /// <param name="tabBarName">微信\通讯录\发现\我</param>
        /// <returns></returns>
        public static bool SwitchTabBar(string appiumSessionId, string tabBarName)
        {
            if (!string.IsNullOrWhiteSpace(appiumSessionId))
            {
                //Q:查找切换TabBar
                string element = string.Empty;
                element = FindElement(appiumSessionId, "xpath", $"//android.widget.TextView[@resource-id=\"{InitDeviceConfig.element.NavBar}\" and @text=\"{tabBarName}\"]");
                if (!string.IsNullOrWhiteSpace(element))
                {
                    //C:点击“tabBarName”
                    if (Clicklement(appiumSessionId, element))
                    {
                        Loger.Info("OK---> 点击通讯录");

                        return true;
                    }
                    else
                    {
                        Loger.Error("ERROR---> 点击通讯录");
                    }
                }
                else
                {
                    Loger.Error("ERROR---> 查找切换TabBar");
                }
            }
            else
            {
                Loger.Error("ERROR---> 获取sessionId");
            }

            return false;
        }

    }
}
