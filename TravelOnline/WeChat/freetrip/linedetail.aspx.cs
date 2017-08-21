using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.WeChat.freetrip.interfaces;

namespace TravelOnline.WeChat.freetrip
{
    public partial class linedetail : System.Web.UI.Page
    {
        public string lineInfoDetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            string lineid = Request.QueryString["id"];
            lineInfoDetail = LineInfoService.GetLineDetail(lineid);
        }
    }
}