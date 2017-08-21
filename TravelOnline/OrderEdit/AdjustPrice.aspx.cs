using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.OrderEdit
{
    public partial class AdjustPrice : System.Web.UI.Page
    {
        public string OrderId,flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            OrderId = Request.QueryString["OrderId"];
            flag = Request.QueryString["flag"];

            string SqlQueryText = string.Format("select OrderFlag from OL_Order where OrderId='{0}'", OrderId);
            //Response.Write(SqlQueryText);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9")
                {
                    Response.Write("已经删除的订单不能操作！");
                    Response.End();
                }
            }
        }
    }
}