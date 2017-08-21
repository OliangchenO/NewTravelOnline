using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using TravelOnline.Class.Common;

namespace TravelOnline.Cruises
{
    public partial class LinePlanNo : BasePage
    {
        public string Cid, lineid, CruisesShip, ReportFlag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            lineid = Request.QueryString["lineid"];
            if (!IsPostBack)
            {
                CruisesShip = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + lineid + "'");
                ReportFlag = MyDataBaseComm.getScalar("select CruisesReport from OL_Line where MisLineId='" + lineid + "'");
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT * FROM View_CR_PlanNo where Lineid='" + lineid + "' ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and PlanNo ='{1}' ", sqlstr, MyConvert.ConToInt(tb_cname.Text.Trim()));
            switch (DropDownList2.SelectedValue)
            {
                case "all":
                    break;
                case "0":
                    sqlstr += " and Nums=0";
                    break;
                case "1":
                    sqlstr += " and Nums>0";
                    break;
                case "2":
                    sqlstr += " and Nums>0 and Nums<>berth";
                    break;
                case "3":
                    sqlstr += " and Nums>0 and Nums=berth";
                    break;
                default:
                    break;
            }

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
                e.Row.Cells[6].Text = string.Format("<A class=order href=\"javascript:edit('{0}');void(0)\">修改</A> ", DataBinder.Eval(e.Row.DataItem, "id").ToString());
                e.Row.Cells[6].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/CruisesGuest.aspx?id={0}&lineid={1}\" target=_blank>名单</a>", DataBinder.Eval(e.Row.DataItem, "id"), DataBinder.Eval(e.Row.DataItem, "Lineid"));
                e.Row.Cells[6].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/ExcelOutPut.aspx?cid={0}&action=CruisesPlanGuest\" target=_blank>名单导出</a>", DataBinder.Eval(e.Row.DataItem, "id"));
                e.Row.Cells[6].Text += " " + string.Format(" <a class=order href=\"/CruisesOrder/WordOutPut.aspx?cid={0}&lineid={1}&action=SinglePlanVisit\" target=_blank>观光导出</a>", DataBinder.Eval(e.Row.DataItem, "id"), DataBinder.Eval(e.Row.DataItem, "Lineid"));
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}