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
    public partial class comment : System.Web.UI.Page
    {
        string commentstr;
        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Convert.ToString(HttpContext.Current.Session["Online_UserId"]);
            //int currpage = MyConvert.ConToInt(Request.QueryString["currpage"]);
            //CommentRS rs = new CommentRS();
            //rs.userId = userId;
            //rs.currpage = currpage;
            //commentstr = CommentInfoService.getComments(rs);

            if (string.IsNullOrEmpty(userId)) {
                Response.Redirect("/wechat/freetriporder.aspx#login");
            }
        }
    }
}