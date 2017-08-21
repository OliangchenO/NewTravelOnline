using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.LoginUsers;
using TravelOnline.WeChat.jssdk;

namespace TravelOnline.WeChat
{
    public partial class main_fx : System.Web.UI.Page
    {
        public LoginUser.RegistUser user;
        public string config;
        public string Fx_UserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Request.QueryString["userId"])
            {
                if (null == Session["Fx_UserId"])
                {
                    Response.Redirect("/WeChat/WeiXinLogin.aspx?state=Fx_regedit", true);
                }
                else
                {
                    user = LoginUser.LoginFxUser("Id='" + Session["Fx_UserId"].ToString() + "'");
                }
            }
            else
            {
                user = LoginUser.LoginFxUser("Id='" + Request.QueryString["userId"].ToString() + "'");
                if (null != user)
                {
                    Session["Fx_UserId"] = user.Id;
                    Session["Fx_Mobile"] = user.Mobile;
                    Session["Fx_UserEmail"] = user.UserEmail;
                    Session["Fx_UserName"] = user.UserName;
                    Session["Fx_Wxid"] = user.Wxid;
                    Session["Fx_Storename"] = user.Storename;
                    Session["Fx_Tel"] = user.Tel;
                    Session["Fx_Address"] = user.Address;
                    Session["Fx_Headimgurl"] = user.headimgurl;
                    Session["ThirdPartyID"] = user.ThirdPartyID;
                    Session.Remove("Fx_Login");
                }
            }
            JsSDKTools tool = new JsSDKTools();
            config = tool.getSignPackage();

            if (Session["Fx_UserId"] != null)
            {
                Fx_UserId = Session["Fx_UserId"].ToString();
            }
            else
            {
                Fx_UserId = null;
            }
        }
    }
}