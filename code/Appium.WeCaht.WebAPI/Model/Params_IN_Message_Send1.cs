using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WeChat.WebAPI.Model
{
    public class Params_IN_Message_Send1
    {
        public bool createGroup { get; set; } = false;

        public string type { get; set; } = "text";

        public string[] contacts { get; set; }

        public string content { get; set; }

        public bool check()
        {
            if (string.IsNullOrWhiteSpace(this.type))
            {
                return false;
            }

            if (this.contacts == null || this.contacts.Length <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.content))
            {
                return false;
            }

            return true;
        }
    }
}
