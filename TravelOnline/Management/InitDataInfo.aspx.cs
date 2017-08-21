using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TravelOnline.Management
{
    public partial class InitDataInfo : System.Web.UI.Page
    {
        public string id, ftype, fname, dataname, sort;
        public string buttoninfo, pbdate, pedate, sellnums, pre_no;
        public string type;
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

            id = Request.QueryString["id"];
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
            string SqlQueryText = string.Format("SELECT * FROM InitData where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                ftype = DS.Tables[0].Rows[0]["ftype"].ToString();
                fname = DS.Tables[0].Rows[0]["fname"].ToString();
                dataname = DS.Tables[0].Rows[0]["dataname"].ToString();
                sort = DS.Tables[0].Rows[0]["sort"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}