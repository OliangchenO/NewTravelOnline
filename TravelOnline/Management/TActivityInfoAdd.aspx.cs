using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class TActivityInfoAdd : System.Web.UI.Page
    {
        public string id, name, context, title, color, tradingAreaId, buttoninfo;
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

            id = Request.QueryString["Cid"];
            tradingAreaId = Request.QueryString["tradingAreaId"];
            if (!IsPostBack)
            {
                buttoninfo = "<a id=\"SaveInfo\" onclick=\"check_null()\" class=\"easyui-linkbutton\" plain=\"true\" iconCls=\"icon-save\" style=\"margin-left: 50px;margin-top: 10px;\">保存</a>";
                if (id != null)
                {
                    LoadInfo();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_TActivity where id= '{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                name = DS.Tables[0].Rows[0]["name"].ToString();
                context = DS.Tables[0].Rows[0]["context"].ToString();
                title = DS.Tables[0].Rows[0]["title"].ToString();
                color = DS.Tables[0].Rows[0]["color"].ToString();
                tradingAreaId = DS.Tables[0].Rows[0]["tradingAreaId"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}