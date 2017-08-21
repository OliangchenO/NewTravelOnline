using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelOnline.WeChat.freetrip.model
{
	public class FlashAD
	{
        public string id { get; set; }
        public string AdName { get; set; }
        public string AdPicUrl { get; set; }//图片地址
        public string AdSecPicUrl { get; set; }
        public string AdPageUrl { get; set; }//链接地址
        public string AdFlag { get; set; }
        public int AdSort { get; set; }
        public DateTime EditTime { get; set; }
        public int MisClassId { get; set; }
        public string HideFlag { get; set; }
        public string BackGround { get; set; }
	}
}