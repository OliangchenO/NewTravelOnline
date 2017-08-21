using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Member
{
    public partial class RegisterSuccess : System.Web.UI.Page
    {
        public string UserName="";
        public string Url="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Online_UserEmail"] != null)
            {
                UserName = Convert.ToString(HttpContext.Current.Session["Online_UserEmail"]);
            }
            if (Session["UserType"].ToString() == "ShYinHangRegUser")
            {
                Url = "../../Users/UserInfo.aspx";
            }
            else
            {
                Url = "../../indexNew.html";
            }
            
        }
    }
}