using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WeChat.WebAPI.Model
{
    public class ReturnModel
    {
        public bool IsSuccess
        {
            get
            {
                return true;
            }
        }

        public ReturnCode ReturnCode { get; set; } = ReturnCode.Success;

        public string ErrorMessage { get; set; } = "";

        public string ErrorCode { get; set; } = "";
    }

    public sealed class ReturnModel<T> : ReturnModel
    {
        private T data;

        public ReturnModel() : this(default(T))
        {
        }

        public ReturnModel(T defaultValue)
        {

        }

        public T ReturnData
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public static implicit operator T(ReturnModel<T> data)
        {
            return data.ReturnData;
        }
    }

    public enum ReturnCode
    {
        Error = 0,
        Success = 1
    }

}
