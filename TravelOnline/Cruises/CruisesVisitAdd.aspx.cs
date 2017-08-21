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
    public partial class CruisesVisitAdd : System.Web.UI.Page
    {
        public string hide, Cid, lineid, days, vtitle, visitname, stay, sight, dinner, intro, price, nums, vdate, vmemo;
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
            string SqlQueryText = string.Format("select * from CR_Visit where id='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                hide = "hide";
                Cid = DS.Tables[0].Rows[0]["Id"].ToString();
                //lineid, days, vtitle, visitname, stay, sight, dinner, intro, price
                lineid = DS.Tables[0].Rows[0]["lineid"].ToString();
                days = DS.Tables[0].Rows[0]["days"].ToString();
                vtitle = DS.Tables[0].Rows[0]["vtitle"].ToString();
                visitname = DS.Tables[0].Rows[0]["visitname"].ToString();
                stay = DS.Tables[0].Rows[0]["stay"].ToString();
                sight = DS.Tables[0].Rows[0]["sight"].ToString();
                dinner = DS.Tables[0].Rows[0]["dinner"].ToString();
                intro = DS.Tables[0].Rows[0]["intro"].ToString();
                price = DS.Tables[0].Rows[0]["price"].ToString();
                nums = DS.Tables[0].Rows[0]["nums"].ToString();
                vdate = DS.Tables[0].Rows[0]["vdate"].ToString();
                vmemo = DS.Tables[0].Rows[0]["vmemo"].ToString();
                
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}