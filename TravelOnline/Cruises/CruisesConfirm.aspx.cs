using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Cruises
{
    public partial class CruisesConfirm : System.Web.UI.Page
    {
        public string Cid, lineid, visit, pay, cancel, visa, change, other, views;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            lineid = Request.QueryString["lineid"];
            if (!IsPostBack)
            {
                LoadInfo();
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from CR_Confirm where lineid='{0}'", lineid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                lineid = DS.Tables[0].Rows[0]["lineid"].ToString();
                visit = DS.Tables[0].Rows[0]["visit"].ToString();
                pay = DS.Tables[0].Rows[0]["pay"].ToString();
                cancel = DS.Tables[0].Rows[0]["cancel"].ToString();
                visa = DS.Tables[0].Rows[0]["visa"].ToString();
                change = DS.Tables[0].Rows[0]["change"].ToString();
                other = DS.Tables[0].Rows[0]["other"].ToString();
                views = DS.Tables[0].Rows[0]["views"].ToString();
            }
        }
    }
}