using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.NewPage
{
    public partial class IndexHeader : System.Web.UI.UserControl
    {
        public string url0, url1, url2, url3, url4, url5, logininfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            url0 = "/index.html";
            url1 = "/outbound.html";
            url2 = "/inland.html";
            url3 = "/freetour.html";
            url4 = "/cruise.html";
            url5 = "/visa.html";
            if (Convert.ToString(Session["Online_UserId"]).Length == 0)
            {
                logininfo = "<a rel=\"nofollow\" href=\"/member/login.html\">请登陆</a><span>|</span><a rel=\"nofollow\" href=\"/member/register.html\">免费注册</a>";
            }
            else
            {
                logininfo = "您好，" + Convert.ToString(Session["Online_UserName"]) + "<span>|</span><a rel=\"nofollow\" href=\"/login/logout.aspx\">退出</a>";
            }
            //url1 = "/newpage/outbound/outbound.html";
            //url2 = "/newpage/inland/inland.html";
            //url3 = "/newpage/freetour/freetour.html";
            //url4 = "/newpage/cruise/cruise.html";
            //url5 = "/newpage/visa/visa.html";
            //url0 = "/new_index.html";
            //url1 = "/new_outbound.html";
            //url2 = "/new_inland.html";
            //url3 = "/new_freetour.html";
            //url4 = "/new_cruise.html";
            //url5 = "/new_visa.html";
            //if (TravelOnline.Class.Common.PublicPageKeyWords.NewPage == "1")
            //{
            //    url0 = "/index.html";
            //    url1 = "/outbound.html";
            //    url2 = "/inland.html";
            //    url3 = "/freetour.html";
            //    url4 = "/cruise.html";
            //    url5 = "/visa.html";
            //}
        }
    }
}