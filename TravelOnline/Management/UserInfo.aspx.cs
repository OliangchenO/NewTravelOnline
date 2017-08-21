using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class UserInfo : System.Web.UI.Page
    {
        public string id;
        public string OldPassWord;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            
            id = Request.QueryString["Id"];
            if (!IsPostBack)
            {

                LoadDropInfo();
                if (id != null) {
                    LoadUserInfo();
                }
                else {
                    //Response.Write("aaaabbb");
                }
            }
        }

        protected void LoadDropInfo()
        {
            string sqlstr = "select id,RightName from OL_UserRight where RightFlag='Menu'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();

            sqlstr = "select id,DeptName from OL_Dept";
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList2.DataSource = DS.Tables[0].DefaultView;
            DropDownList2.DataBind();
        }

        protected void LoadUserInfo()
        {
            ManageUsers.UserClass RUser = new ManageUsers.UserClass();
            RUser = ManageUsers.ManageUseDetail(string.Format("Id='{0}'", id));
            if (RUser != null)
            {
                TB_LoginName.Text = RUser.LoginName;
                TB_UserName.Text = RUser.UserName;
                TB_LoginName.Text = RUser.LoginName;
                OldPassWord = RUser.LoginPassWord;

                try
                {
                    DropDownList1.Text = RUser.UserRight.ToString();
                    DropDownList2.Text = RUser.UserDept.ToString();
                }
                catch
                {}
            }
            RUser = null;
        }
    }
}