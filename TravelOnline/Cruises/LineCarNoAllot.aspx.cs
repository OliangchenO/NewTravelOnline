using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.Cruises
{
    public partial class LineCarNoAllot : System.Web.UI.Page
    {
        public string lineid, flag, hide1, hide2, memo;
        public string BusNums;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            BusNums = "";
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

            lineid = Request.QueryString["lineid"];
            flag = Request.QueryString["flag"];
            if (flag == "PlanNo")
            {
                hide1 = "hide";
            }
            else {
                hide2 = "hide";
            }

            if (Request.QueryString["id"] != null)
            {
                flag = "EditPlanNo";
                lineid = Request.QueryString["id"];

                string SqlQueryText;
                SqlQueryText = string.Format("select memo,Berth from CR_PlanNo where Id='{0}'", lineid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    memo = DS.Tables[0].Rows[0]["memo"].ToString();
                    BusNums = DS.Tables[0].Rows[0]["Berth"].ToString();
                }
            }

            if (Request.QueryString["carid"] != null)
            {
                flag = "EditCarNo";
                lineid = Request.QueryString["carid"];

                string SqlQueryText;
                SqlQueryText = string.Format("select Berth from CR_BusNo where Id='{0}'", lineid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    BusNums = DS.Tables[0].Rows[0]["Berth"].ToString();
                }
            }
        }
    }
}