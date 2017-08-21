using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Travel
{
    public partial class Show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string categorys, cid, pid;
            categorys = Request.QueryString["category"];
            cid = Request.QueryString["cid"];
            pid = Request.QueryString["pid"];
            Response.Write("Show.aspx categorys=" + categorys + " cid=" + cid + " pid=" + pid);
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string categorys;
            categorys = Request.QueryString["category"];
            Response.Write(categorys);
        }
    }
}