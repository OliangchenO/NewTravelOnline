using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class RightInfo : System.Web.UI.Page
    {
        public string MenuCheckBoxString, OperationCheckBoxString;
        public string id, flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }
            
            id = Request.QueryString["Id"];
            flag = Request.QueryString["flag"];
            if (id != null)
            {
                LoadRighInfo();
            }
            else
            {
                id = "0";
                MenuCheckBoxString = UserRight.MenuCheckBox("");
                OperationCheckBoxString = UserRight.MenuOpCheckBox("");
            }
        }

        protected void LoadRighInfo()
        {
            UserRight.UserRightClass RUser = new UserRight.UserRightClass();
            RUser = UserRight.UserRightDetail(string.Format("Id='{0}'", id));

            if (RUser != null)
            {
                this.TextBox1.Text = RUser.RightName;
                OperationCheckBoxString = UserRight.MenuOpCheckBox(RUser.RightCode);
                MenuCheckBoxString = UserRight.MenuCheckBox(RUser.RightCode);
            }
            RUser = null;
        }
    }
}