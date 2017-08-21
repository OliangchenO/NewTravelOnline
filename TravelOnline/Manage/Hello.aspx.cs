using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Manage
{
    public partial class Hello : System.Web.UI.Page
    {
        public string Infos = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                Infos = GetTimeString() + "，" + GetNickName() + "欢迎您来到青旅商城后台管理！<a href=\"/Management/EditPassWord.aspx\">[修改密码]</a> <a href=\"/login/logout.aspx\">[退出]</a>";
                Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");
            }
            else
            {
                if (Request.Cookies["username"] != null)
                {
                    Infos = GetNickName() + "欢迎来到青旅商城后台管理！请登录后再操作！";
                    Response.Write("(function(){var id=document.getElementById('loginfo');if(id){id.innerHTML='" + Infos + "';}})();");
                }
            }
            Response.End();
        }

        protected string GetNickName()
        {
            if (Request.Cookies["username"] != null)
            {
                return HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["username"].Value)) + "！";
            }
            else
            {
                return "";
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