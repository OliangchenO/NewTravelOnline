using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TravelOnline.LoginUsers;

namespace TravelOnline.Users
{
    public partial class UserInfo : System.Web.UI.Page
    {
        public Guid ucode;
        public string UserInfos, UserEmail, UserName, Sex1, Sex2, Tel, Mobile, Address, ZipCode, Remark, Marriage, Income, Career,UserBirthDay;
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
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("Id='{0}'", Convert.ToString(Session["Online_UserId"]));

            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser != null)
            {
                //UserEmail, UserName, Sex1, Sex2, Tel, Mobile, Address, ZipCode, Remark, Marriage, Income, Career,UserBirthDay;

                UserEmail = RUser.UserEmail;
                UserName = RUser.UserName;
                try
                {
                    UserBirthDay = string.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(RUser.BirtyDay));
                }
                catch
                {}
                                
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
                Marriage = RUser.Marriage.ToString();
                Income = RUser.Income.ToString();
                Career = RUser.Career.ToString();

                if (RUser.DeptName.ToString() != "")
                {
                    UserInfos = "<DIV class=item><SPAN class=label>会员类型：</SPAN><DIV class=fl><SPAN class=labelb>公司用户</SPAN></DIV></DIV>";
                    UserInfos += "<DIV class=item><SPAN class=label>公司：</SPAN><DIV class=fl><SPAN class=labelb>" + RUser.CompanyName.ToString() + "</SPAN></DIV></DIV>";
                    UserInfos += "<DIV class=item><SPAN class=label>部门：</SPAN><DIV class=fl><SPAN class=labelb>" + RUser.DeptName.ToString() + "</SPAN></DIV></DIV>";
                
                }
                else
                {
                    UserInfos = "<DIV class=item><SPAN class=label>会员类型：</SPAN><DIV class=fl><SPAN class=labelb>普通用户</SPAN></DIV></DIV>";
                }
            }
            else
            {
                Response.Redirect("/Login/Login.aspx", true);
            }

            RUser = null;
        }
    }
}