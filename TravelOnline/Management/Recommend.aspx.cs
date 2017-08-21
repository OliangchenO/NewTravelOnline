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
    public partial class Recommend : BasePage
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
            //Types = Request.QueryString["LineType"];
            //switch (Request.QueryString["LineType"])
            //{
            //    case "OutBound":
            //        LineType = "LineType='OutBound'";
            //        ClassType = "ProductType='OutBound'";
            //        break;
            //    case "InLand":
            //        LineType = "LineType='InLand'";
            //        ClassType = "ProductType='InLand'";
            //        break;
            //    case "FreeTour":
            //        LineType = "LineType='FreeTour'";
            //        ClassType = "ProductType='FreeTour'";
            //        break;
            //    case "Cruises":
            //        LineType = "LineType='Cruises'";
            //        ClassType = "ProductType='Cruises'";
            //        break;
            //    case "Visa":
            //        LineType = "LineType='Visa'";
            //        ClassType = "ProductType='Visa'";
            //        break;
            //    default:
            //        LineType = "LineType='OutBound'";
            //        ClassType = "ProductType='OutBound'";
            //        break;
            //}
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
            string sqlstr = "SELECT TopBegin,TopEnd,WeChat,BigPics,NewRecom,IndexRecom,famous,Destination,FirstDestination,Shipid,MisLineId,Sale,LineType,LineName,LineClass,Preferences,Recommend,EditTime,PlanDate,Price,(select top 1 ProductName from OL_ProductType where MisClassId=OL_Line.LineClass) as TypeName FROM OL_Line where 1=1 ";
            if (tb_cname.Text.Trim().Length > 0) sqlstr = string.Format("{0} and LineName like '%{1}%' ", sqlstr, tb_cname.Text.Trim());
            if (tb_MisLineId.Text.Trim().Length > 0) sqlstr = string.Format("{0} and MisLineId = '{1}' ", sqlstr, tb_MisLineId.Text.Trim());
            if (DropDownList1.Text != "全部") sqlstr = string.Format("{0} and LineType = '{1}' ", sqlstr, DropDownList1.Text);
            if (DropDownList2.Text != "全部") sqlstr = string.Format("{0} and Sale = '{1}' ", sqlstr, DropDownList2.Text);
            if (DropDownList3.Text != "全部") sqlstr = string.Format("{0} and (IndexRecom = '{1}' or NewRecom = '{1}') ", sqlstr, DropDownList3.Text);
            if (DropDownList4.Text != "全部") sqlstr = string.Format("{0} and WeChat = '{1}' ", sqlstr, DropDownList4.Text);
            
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
                switch (DataBinder.Eval(e.Row.DataItem, "IndexRecom").ToString())
                {
                    case "1":
                        e.Row.Cells[6].Text = "名牌";
                        break;
                    case "2":
                        e.Row.Cells[6].Text = "特价";
                        break;
                    case "3":
                        e.Row.Cells[6].Text = "推荐";
                        break;
                    case "4":
                        e.Row.Cells[6].Text = "热卖";
                        break;
                }
                switch (DataBinder.Eval(e.Row.DataItem, "famous").ToString())
                {
                    case "1":
                        e.Row.Cells[6].Text += "看天下";
                        break;
                    case "2":
                        e.Row.Cells[6].Text += "品天下";
                        break;
                    case "3":
                        e.Row.Cells[6].Text += "走天下";
                        break;
                }
                switch (DataBinder.Eval(e.Row.DataItem, "NewRecom").ToString())
                {
                    case "1":
                        e.Row.Cells[7].Text = "名牌";
                        break;
                    case "2":
                        e.Row.Cells[7].Text = "特价";
                        break;
                    case "3":
                        e.Row.Cells[7].Text = "推荐";
                        break;
                    case "4":
                        e.Row.Cells[7].Text = "热卖";
                        break;
                }

                switch (DataBinder.Eval(e.Row.DataItem, "WeChat").ToString())
                {
                    case "1":
                        e.Row.Cells[8].Text = "首页推荐";
                        break;
                    case "2":
                        e.Row.Cells[8].Text = "当季推荐";
                        break;
                }
                //if (DataBinder.Eval(e.Row.DataItem, "Preferences").ToString() == "1") e.Row.Cells[6].Text = "√";
                //if (DataBinder.Eval(e.Row.DataItem, "Recommend").ToString() == "1") e.Row.Cells[7].Text = "√";
                if (DataBinder.Eval(e.Row.DataItem, "Sale").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[3].Text = string.Format("<A href=\"/line/{2}.html\" target=_blank>{3}</A>", DataBinder.Eval(e.Row.DataItem, "LineType").ToString(), DataBinder.Eval(e.Row.DataItem, "LineClass").ToString(), DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString(), DataBinder.Eval(e.Row.DataItem, "LineName").ToString());
                //if (DataBinder.Eval(e.Row.DataItem, "LineType").ToString() == "Cruises") e.Row.Cells[3].Text += string.Format(" <A class=order href=\"javascript:void(0)\" onclick=\"Cruises({0})\">【包船设置】</A> ", DataBinder.Eval(e.Row.DataItem, "MisLineId").ToString());
                e.Row.Cells[3].Text += "<br>置顶：" + string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "TopBegin")) + " - " + string.Format("{0:yyyy-MM-dd}", DataBinder.Eval(e.Row.DataItem, "TopEnd"));
                if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "Shipid").ToString()) > 0) e.Row.Cells[8].Text = "包船";

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