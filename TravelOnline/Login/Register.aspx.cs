using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Login
{
    public partial class Register : System.Web.UI.Page
    {
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            //Response.Write(u);
            Response.Cookies.Add(new HttpCookie("CheckCode", ""));
        }
    }
}