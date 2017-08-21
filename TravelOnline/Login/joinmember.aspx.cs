using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TravelOnline.LoginUsers;

namespace TravelOnline.Login
{
    public partial class joinmember : System.Web.UI.Page
    {
        public Guid ucode;
        public string UserInfos, UserEmail, UserName, Sex1, Sex2, Tel, Mobile, Address, ZipCode, Remark, Marriage, Income, Career, UserBirthDay;
        public string buttonhide = "hide", infos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                ucode = System.Guid.NewGuid();
                LoadUserInfo();
            }
            else
            {
                Response.Redirect("/login/login.aspx", true);
            }

        }

        protected void LoadUserInfo()
        {
            if (Convert.ToString(Session["Online_UserCompany"]).Length > 0)
            {
                infos = "您的账号（门市或同业）不能加入积分会员！";
                return;
            }

            if (MyDataBaseComm.getScalar("select id from OL_Member where uid='" + Convert.ToString(Session["Online_UserId"]) + "'") != null)
            {
                infos = "您已经成功加入积分会员！";
                return;
            }

            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("Id='{0}'", Convert.ToString(Session["Online_UserId"]));

            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser != null)
            {
                UserEmail = RUser.UserEmail;
                UserName = RUser.UserName;
                try
                {
                    UserBirthDay = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(RUser.BirtyDay));
                }
                catch
                { }

                if (RUser.Sex == 1)
                {
                    Sex1 = "checked=\"checked\"";
                }
                else
                {
                    Sex2 = "checked=\"checked\"";
                }
                Tel = RUser.Tel;
                Mobile = RUser.Mobile;
                Address = RUser.Address;
                ZipCode = RUser.ZipCode;
                Remark = RUser.Hobby;
                buttonhide = "";
            }
            else
            {
                Response.Redirect("/Login/Login.aspx", true);
            }

            RUser = null;
        }
    }
}