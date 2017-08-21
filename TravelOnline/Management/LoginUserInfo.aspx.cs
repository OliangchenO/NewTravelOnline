using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TravelOnline.LoginUsers;

namespace TravelOnline.Management
{
    public partial class LoginUserInfo : System.Web.UI.Page
    {
        public string UserEmail, UserName, Sex1, Sex2, Tel, Mobile, Address, ZipCode, Remark, Marriage, Income, Career, UserBirthDay, Company;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            //{
            //    Response.Write("尚未登录");
            //    Response.End();
            //}
            LoadUserInfo();
        }

        protected void LoadUserInfo()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("Id='{0}'", Request.QueryString["UserId"]);

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
                Company = RUser.CompanyName;
                Mobile = RUser.Mobile;
                Address = RUser.Address;
                ZipCode = RUser.ZipCode;
                Remark = RUser.Hobby;
                Marriage = RUser.Marriage.ToString();
                Income = RUser.Income.ToString();
                Career = RUser.Career.ToString();

            }
            RUser = null;
        }
    }
}