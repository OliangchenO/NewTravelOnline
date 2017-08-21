using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeiXinPay
{
    public class WxPayException : Exception 
	{
        public WxPayException(string msg)
            : base(msg)
        {

        }
	}
}