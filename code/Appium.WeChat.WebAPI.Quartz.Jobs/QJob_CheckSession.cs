using Appium.WeChat.WebAPI.Core;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WeChat.WebAPI.Quartz.Jobs
{
    public class QJob_CheckSession : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            AppiumSession.KeepSessionLive();
        }
    }
}
