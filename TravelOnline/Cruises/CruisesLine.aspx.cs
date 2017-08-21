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
    public partial class CruisesLine : BasePage
    {
        public string LineType, ClassType, Types;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            Types = Request.QueryString["LineType"];
            LineType = "LineType='Cruises'";
            ClassType = "ProductType='Cruises'";
            if (!IsPostBack)
            {
                LoadDropListInfo();
                this.GridView_DataBind();
            }
        }

        protected void LoadDropListInfo()
        {
            string sqlstr = string.Format("select MisClassId,ProductName from OL_ProductType where {0} order by ProductType,ProductSort", ClassType);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(sqlstr);
            DropDownList1.DataSource = DS.Tables[0].DefaultView;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "全部类型");
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = "SELECT CruisesReport,Shipid,MisLineId,Sale,LineType,LineName,LineClass,Preferences,Recommend,EditTime,PlanDate,Price,(select top 1 ProductName from OL_ProductType where MisClassId=OL_Line.LineClass) as TypeName,(select top 1 cname from CR_Ship where Id=OL_Line.shipid) as shipName FROM OL_Line where shipid>0 ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LineName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            sqlstr = string.Format("{0} and {1} ", sqlstr, LineType);
            if (DropDownList1.Text != "全部类型") sqlstr = string.Format("{0} and LineClass = '{1}' ", sqlstr, DropDownList1.Text);
            if (DropDownList2.Text != "all") sqlstr = string.Format("{0} and Sale = '{1}' ", sqlstr, DropDownList2.Text);
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
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Sale").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].Text = string.Format("<A href=\"/ship/{1}/{2}.html\" target=_blank>{3}</A>", DataBinder.Eval(e.Row.DataItem, "LineType").ToString(), DataBinder.Eval(e.Row.DataItem, "LineClass").ToString(), DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "LineName").ToString());
                //e.Row.Cells[7].Text += string.Format("<A class=order href=\"CruisesLineRoom.aspx?id={0}&shipid={1}\" target=\"_blank\">房型</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "shipid").ToString());
                //e.Row.Cells[7].Text += string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"Route({0})\">行程</A>", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
                //e.Row.Cells[7].Text += string.Format(" <A class=order href=\"CruisesVisit.aspx?id={0}\" target=\"_blank\">观光</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
                e.Row.Cells[7].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} rep={2} ship={1}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "shipid").ToString(), DataBinder.Eval(e.Row.DataItem, "CruisesReport").ToString());
                e.Row.Cells[7].Text += string.Format(" <A class=\"order\" href=\"/CruisesOrder/CruisesLineOrder.aspx?id={0}\" target=\"_blank\">订单</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}