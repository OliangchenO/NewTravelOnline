using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.CruisesOrder
{
    public partial class FuJia : System.Web.UI.Page
    {
        public string autoid, OrderId, Order_Memo, fj_ly, fj_bmb, fj_bx, fj_zp, Order_lxr;
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
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@3") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            OrderId = Request.QueryString["OrderId"];

            if (!IsPostBack)
            {
                if (OrderId != null)
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
            string SqlQueryText = string.Format("select autoid,OrderMemo,OrderName from OL_Order where OrderId='{0}'", OrderId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                autoid = DS.Tables[0].Rows[0]["autoid"].ToString();
                Order_Memo = DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                Order_lxr = DS.Tables[0].Rows[0]["OrderName"].ToString();
            }
        }
    }
}