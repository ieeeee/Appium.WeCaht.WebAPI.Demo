using Appium.WeChat.WebAPI.Core;
using Appium.WeChat.WebAPI.Quartz.Jobs;
using Appium.WeChat.WebAPI.Tools;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Appium.WeChat.WebAPI.WebHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Loger.Info("Application_Start...");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //init sessionId
            var appiumSessionId = AppiumSession.sessionId;

            InitQuartz();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            try
            {
                if (scheduler != null)
                {
                    scheduler.Shutdown(true);
                }

                //InteractiveCore.Quit(AppiumSession.sessionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Quartz
        IScheduler scheduler;
        ISchedulerFactory factory;

        /// <summary>
        /// 
        /// </summary>
        protected void InitQuartz()
        {
            Loger.Info("Init Quartz Config...");

            //1.创建调度器
            factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
            scheduler.Start();

            //2.创建一个任务
            IJobDetail job = JobBuilder.Create<QJob_CheckSession>().WithIdentity("joob1", "group1").Build();

            //3.创建触发器
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0 0/1 * * * ?")
                .StartAt(new DateTimeOffset(DateTime.Now.AddMinutes(1d)))
                .Build();

            //4.绑定任务+触发器到调度器
            scheduler.ScheduleJob(job, trigger);

            //5.开始调度
            scheduler.Start();
        }
        #endregion
    }
}
