using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class hello : System.Web.UI.Page
    {
        public string Infos = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            //{ }

            //if (Request.Cookies["UserLoginMail"] != null)
            //{ }

            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                Infos = GetTimeString() + "，" + GetNickName() + "欢迎您来到青旅商城！ <a href=\"/Users/UserHome.aspx\">[会员中心]</a> &nbsp;&nbsp; <a href=\"/login/logout.aspx\">[退出]</a>";
                Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");
            }
            else
            {
                if (Request.Cookies["UserLoginMail"] != null)
                {
                    Infos = GetNickName() + "欢迎来到青旅在线商城！<span><A href=\"javascript:login();\">[请登录]</A>，新用户？<A class=link-regist href=\"javascript:regist();\">[免费注册]</A>";
                    Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");
                   
                    //if (Request.Cookies["UserLoginInfos"] != null)
                    //{
                    //    //根据用户email和密码提取身份验证，验证失败，清空UserLoginInfos
                    //    LoadUserInfos();
                    //}
                    //else
                    //{
                    //    Infos = GetNickName() + "欢迎来到青旅在线商城！<span><A href=\"javascript:login();\">[请登录]</A>，新用户？<A class=link-regist href=\"javascript:regist();\">[免费注册]</A>";
                    //    Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");
                    //}
                }
            }
            Response.End();
        }

        protected void LoadUserInfos()
        {
            Session["Online_UserId"] = "1211";

            Infos = GetTimeString() + "，" + GetNickName() + "欢迎您来到青旅商城！<a href=\"/login/logout.aspx\">[退出]</a>";
            Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");

        }
        
        protected string GetNickName()
        {
            if (Request.Cookies["MemberName"] != null)
            {
                return HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["MemberName"].Value)) + "！";
            }
            else
            {
                if (Request.Cookies["UserLoginMail"] != null)
                {
                    return HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["UserLoginMail"].Value)).Split("@".ToCharArray())[0] + "！";
                }
                else
                {
                    return "";
                }
            }            
        }

        protected string GetTimeString()
        {
            //System.DateTime currentTime = new System.DateTime();
            int nowtime = DateTime.Now.Hour; 
            if (nowtime <= 3)
            {
                return "凌晨好";
            }
            else if (nowtime <= 6)
            {
                return "清晨好";
            }
            else if (nowtime <= 8)
            {
                return "早晨好";
            }
            else if (nowtime <= 11)
            {
                return "上午好";
            }
            else if (nowtime <= 13)
            {
                return "中午好";
            }
            else if (nowtime <= 17)
            {
                return "下午好";
            }
            else if (nowtime <= 19)
            {
                return "傍晚好";
            }
            else
            {
                return "晚上好";
            }

        }

    }
}