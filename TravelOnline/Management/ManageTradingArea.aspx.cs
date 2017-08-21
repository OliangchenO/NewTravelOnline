using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class ManageTradingArea : BasePage
    {
        public string LineType, ClassType, Types;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM OL_TradingArea where 1=1 ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and name like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);

            string sortExpression = this.GridView1.Attributes["SortExpression"];
            string sortDirection = this.GridView1.Attributes["SortDirection"];
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                DS.Tables[0].DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }

            this.GridView1.DataSource = DS.Tables[0].DefaultView;
            this.GridView1.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] sArray = DataBinder.Eval(e.Row.DataItem, "pic").ToString().Split('/');
                e.Row.Cells[5].Text = string.Format("<img width='250px' height='75px' src=\"/Upload/AdImage/Thumb_{0}\" />", sArray[3].ToString());
                e.Row.Cells[6].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">修改</A> <A class=order href=\"ManageTActivity.aspx?tradingAreaId={0}\" target=\"_blank\">活动</A> <A class=order href=\"ManageTCoupon.aspx?tradingAreaId={0}\" target=\"_blank\">优惠券</A> <A class=order href=\"ManageTStore.aspx?tradingAreaId={0}\" target=\"_blank\">店铺</A>", DataBinder.Eval(e.Row.DataItem, "id").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}