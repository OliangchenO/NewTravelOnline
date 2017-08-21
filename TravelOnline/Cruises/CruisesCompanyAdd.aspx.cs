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
    public partial class CruisesCompanyAdd : System.Web.UI.Page
    {
        public string id, cname, ename, intro, ThumbSrc, logourl, hide;
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

            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                if (id != null)
                {
                    LoadInfo();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from CR_Company where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                hide = "hide";
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                cname = DS.Tables[0].Rows[0]["cname"].ToString();
                ename = DS.Tables[0].Rows[0]["ename"].ToString();
                intro = DS.Tables[0].Rows[0]["intro"].ToString();
                logourl = DS.Tables[0].Rows[0]["picurl"].ToString();
                if (logourl.Length > 10)
                {
                    string[] sArray = DS.Tables[0].Rows[0]["picurl"].ToString().Split('/');
                    ThumbSrc = string.Format("src=\"/Upload/Cruises/Thumb_{0}\"", sArray[3].ToString());
                }

            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}