using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.jssdk
{
	public class JSTicket
	{
        public string jsapi_ticket { get; set; }
        public double expire_time { get; set; }
	}
}