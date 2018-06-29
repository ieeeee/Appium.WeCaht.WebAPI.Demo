using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Mvc;

namespace System
{
    public static class ExtendObject
    {
        private static IsoDateTimeConverter timeFormat = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

        private static string SerializeObject(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, timeFormat);
            }
            catch (Exception ex)
            {
                var strex = ex.Message;
                return string.Empty;
            }
        }

        public static string ToJson(this object obj)
        {
            return SerializeObject(obj);
        }

        public static ContentResult ToJsonResult(this object obj)
        {
            return new ContentResult() { Content = obj.ToJson(), ContentType = "application/json", ContentEncoding = Encoding.UTF8 };
        }
    }
}
