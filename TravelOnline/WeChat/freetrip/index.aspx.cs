using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeChat.freetrip.interfaces;
using TravelOnline.WeChat.freetrip.model;

namespace TravelOnline.WeChat.freetrip
{
    public partial class index : System.Web.UI.Page
    {
        public string bannerList,lineList;
        protected void Page_Load(object sender, EventArgs e)
        {
            bannerList = LineInfoService.GetFreetripBannerList();
            LineSelecter selecter = new LineSelecter();
            lineList = LineInfoService.GetFreetripLineList(selecter);
        }
    }
}