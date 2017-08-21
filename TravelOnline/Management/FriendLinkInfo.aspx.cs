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
    public partial class FriendLinkInfo : System.Web.UI.Page
    {
        public string hide, Cid, LinkName, LinkUrl, rankid, LinkType;
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

            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@4@4") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            Cid = Request.QueryString["Id"];
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
            string SqlQueryText = string.Format("select * from OL_FriendLink where id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                hide = "hide";
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                LinkName = DS.Tables[0].Rows[0]["LinkName"].ToString();
                LinkUrl = DS.Tables[0].Rows[0]["LinkUrl"].ToString();
                rankid = DS.Tables[0].Rows[0]["rankid"].ToString();
                LinkType = DS.Tables[0].Rows[0]["LinkType"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}