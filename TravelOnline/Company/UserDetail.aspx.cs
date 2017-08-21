using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Company
{
    public partial class UserDetail : System.Web.UI.Page
    {
        public string Cid, hide, companyid, company, deptid, deptname, useremail, misid, username, mobile;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@1") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            Cid = Request.QueryString["Cid"];

            if (!IsPostBack)
            {
                if (Cid != null)
                {
                    LoadInfo();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("SELECT id,useremail,misid,username,mobile,companyid,deptid,(select companyname from company where id=OL_LoginUser.companyid) as company,(select deptname from DeptInfo where id=OL_LoginUser.deptid) as deptname FROM OL_LoginUser where Id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                hide = "hide";
                Cid = DS.Tables[0].Rows[0]["id"].ToString();
                companyid = DS.Tables[0].Rows[0]["companyid"].ToString();
                company = DS.Tables[0].Rows[0]["company"].ToString();
                deptid = DS.Tables[0].Rows[0]["deptid"].ToString();
                deptname = DS.Tables[0].Rows[0]["deptname"].ToString();
                useremail = DS.Tables[0].Rows[0]["useremail"].ToString();
                username = DS.Tables[0].Rows[0]["username"].ToString();
                mobile = DS.Tables[0].Rows[0]["mobile"].ToString();
                misid = DS.Tables[0].Rows[0]["misid"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}