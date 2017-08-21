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
    public partial class CruisesRebate : System.Web.UI.Page
    {
        public string Cid, lineid, roomid, allotid, begindate, enddate, price, infos, flag, Rebateflag;
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
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            Cid = Request.QueryString["Cid"];
            lineid = Request.QueryString["lineid"];
            flag = Request.QueryString["flag"];
            roomid = Request.QueryString["roomid"];
            allotid = Request.QueryString["allotid"];

            if (!IsPostBack)
            {
                if (Cid != null)
                {
                    LoadInfo();
                }
                else
                {
                    Rebateflag = "0";
                    begindate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    enddate = string.Format("{0:yyyy-MM-dd}", DateTime.Today.AddMonths(1));
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from CR_Rebate where id={0}", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["id"].ToString();
                roomid = DS.Tables[0].Rows[0]["roomid"].ToString();
                begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["begindate"]);
                enddate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["enddate"]);
                price = DS.Tables[0].Rows[0]["price"].ToString();
                infos = DS.Tables[0].Rows[0]["infos"].ToString();
                Rebateflag = DS.Tables[0].Rows[0]["flag"].ToString();
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}