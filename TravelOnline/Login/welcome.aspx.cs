using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class welcome : System.Web.UI.Page
    {
        public string Infos = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                Infos = "<ul style=\"FLOAT: right;\">";
                Infos += "<li>";
                Infos += Session["Online_UserName"] + "！您好，欢迎来到青旅" + " &nbsp;&nbsp;|&nbsp;&nbsp;";
                Infos += "</li>";
                Infos += "<li id=\"topbar_dropmenu\">";
                Infos += "<div id=\"topbar_dropmenu_a\" class=\"mod_dropmenu_hd\"> <a id=\"topbar_usercenter\" href=\"/users/userhome.aspx\" target=\"_blank\">会员中心<i style=\"margin-left: 5px;\" class=\"icon-chevron-down\"></i></a></div>";
                Infos += "<div class=\"mod_dropmenu_pop\">";
                Infos += "<div><i class=\"icon-th-list\"></i> <a id=\"topbar_usercenter\" href=\"/users/userorder.aspx\">我的订单</a></div>";
                Infos += "<div><i class=\"icon-flag\"></i> <a id=\"topbar_usercenter\" href=\"/users/journals.aspx\">我的游记</a></div>";
                Infos += "<div><i class=\"icon-user\"></i> <a id=\"topbar_usercenter\" href=\"/users/userinfo.aspx\">个人信息</a></div>";
                Infos += "<div><i class=\"icon-wrench\"></i> <a id=\"topbar_usercenter\" href=\"/users/changepassword.aspx\">修改密码</a></div>";
                Infos += "</div>";

                Infos += "</li>";
                Infos += "<li>";
                Infos += "&nbsp;&nbsp;|&nbsp;&nbsp; <a href=\"/login/logout.aspx\"><i class=\"icon-off\"></i> 退出</a>";
                Infos += "</li>";
                Infos += "</ul>";
            }
            else
            {
                string NickName = "";
                if (Request.Cookies["UserLoginMail"] != null)
                {
                    NickName = GetNickName()+ " ";
                }
                Infos = NickName + GetTimeString() + "，" + "欢迎您来到青旅！ &nbsp; <a href=\"javascript:login();\"><i class=\"icon-user\"></i> 登录</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href=\"javascript:regist();\"><i class=\"icon-cog\"></i> 注册</a>";
            }
            Response.Write("(function(){var id=document.getElementById('loginarea');if(id){id.innerHTML='" + Infos + "';}})();");
            //Response.Write("function () {$('#loginarea').html(" + Infos + ")});");
            Response.End();
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