using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class FavoriteLine : BasePage
    {
        public string LineType, ClassType, Types;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);

            if (!IsPostBack)
            {
                //LoadDropListInfo();
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
            string sqlstr = "select Isnull((select count(1) from OL_Favorite where lineid=l.mislineid group by lineid),0) as Num , mislineid , lineName, sale,LineType,LineClass, (select top 1 ProductName from OL_ProductType where MisClassId=l.LineClass) as TypeName from ol_line l where 1=1";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and l.LineName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (tb_mislineid.Text.Trim().Length > 0) sqlstr = string.Format("{0} and l.mislineid = '{1}' ", sqlstr, tb_mislineid.Text.Trim());
            if (DropDownList1.Text != "全部") sqlstr = string.Format("{0} and l.LineType = '{1}' ", sqlstr, DropDownList1.Text);
            if (DropDownList2.Text != "全部") sqlstr = string.Format("{0} and l.Sale = '{1}' ", sqlstr, DropDownList2.Text);
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
                e.Row.Cells[2].Text += "<br>" + DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString();
                if (DataBinder.Eval(e.Row.DataItem, "Sale").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[3].Text = string.Format("<A href=\"/line/{2}.html\" target=_blank>{3}</A>", DataBinder.Eval(e.Row.DataItem, "LineType").ToString(), DataBinder.Eval(e.Row.DataItem, "LineClass").ToString(), DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "LineName").ToString());
                

                //if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "BigPics").ToString()) > 0)
                //{
                //    e.Row.Cells[8].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">换图</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
                //}
                //else
                //{
                //    e.Row.Cells[8].Text = string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"EditDes({0})\">增加</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
                //}

            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}