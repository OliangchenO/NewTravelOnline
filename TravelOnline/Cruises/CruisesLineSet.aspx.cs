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
    public partial class CruisesLineSet : System.Web.UI.Page
    {
        public string Cid, shipid, planid, typename, PlanDate, AgeLimit, VisitSell, CruisesReport;
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
                else
                {
                    Response.Write("没有操作权限！");
                    Response.End();
                }
            }
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select CruisesReport,VisitSell,MisLineId,PlanDate,Shipid,Planid,AgeLimit from OL_Line where MisLineId='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Cid = DS.Tables[0].Rows[0]["MisLineId"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                planid = DS.Tables[0].Rows[0]["planid"].ToString();
                AgeLimit = DS.Tables[0].Rows[0]["AgeLimit"].ToString();
                CruisesReport = DS.Tables[0].Rows[0]["CruisesReport"].ToString();
                PlanDate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["PlanDate"]);
                typename = MyDataBaseComm.getScalar("select cname from CR_Ship where id='" + shipid + "'");
                if (DS.Tables[0].Rows[0]["VisitSell"].ToString() == "1") VisitSell = "checked=checked";
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}