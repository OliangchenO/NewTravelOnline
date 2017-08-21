using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.CruisesOrder
{
    public partial class CruisesApply : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@4") == -1)
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
            string sqlstr = "SELECT *,(select AutoId from OL_Order where OrderId=OL_OrderApply.OrderId) as AutoId,(select OrderFlag from OL_Order where OrderId=OL_OrderApply.OrderId) as OrderFlag ";
            //sqlstr += "(select roomname from CR_RoomList where id=OL_OrderApply.originalid) as roomname,";
            //sqlstr += "(select CONVERT(VARCHAR(1), peoples) + ' (' + CONVERT(VARCHAR(1), adults) + ',' + CONVERT(VARCHAR(1), childs) + ')'  from CR_RoomList where id=OL_OrderApply.originalid) as peoples,";
            //sqlstr += "(select roomname from CR_RoomAllot where id=OL_OrderApply.updateid) as newroomname ";
            sqlstr += " from OL_OrderApply where 1=1 ";
            //if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            DateTime date = DateTime.Today;
            date = date.AddMonths(-1);
            if (DropDownList1.Text == "1")
            {
                sqlstr = string.Format("{0} and inputdate >= '{1}' ", sqlstr, date.ToString());
            }
            else
            {
                sqlstr = string.Format("{0} and inputdate < '{1}' ", sqlstr, date.ToString());
            }

            if (DropDownList2.Text != "all")
            {
                sqlstr = string.Format("{0} and applyflag = '{1}' ", sqlstr, DropDownList2.Text);
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
            //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1}</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"), DataBinder.Eval(e.Row.DataItem, "AutoId")); //"<a href=\"javascript:void(0);\" onclick=\"EditInfo({0})\">修改</a>";
                //e.Row.Cells[2].Text = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["ProductType"], DS.Tables[0].Rows[0]["ProductClass"], DS.Tables[0].Rows[0]["LineID"]);
                
                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "0":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">待确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "1":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">占位</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "2":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "3":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">完成</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "8":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "9":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">已删除</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    default:
                        break;
                }

                switch (DataBinder.Eval(e.Row.DataItem, "applyflag").ToString())
                {
                    case "AdjustCruisesRoom":
                        e.Row.Cells[2].Text = "舱房变更";
                        //e.Row.Cells[3].Text = DataBinder.Eval(e.Row.DataItem, "roomname").ToString() + " -> " + DataBinder.Eval(e.Row.DataItem, "newroomname").ToString();
                        break;
                    case "CancelCruisesRoom":
                        e.Row.Cells[2].Text = "舱房取消";
                        break;
                    case "OrderRetreat":
                        e.Row.Cells[2].Text = "订单取消";
                        break;
                    case "DinnerChange":
                        e.Row.Cells[2].Text = "用餐变更";
                        break;
                    case "Settlement":
                        e.Row.Cells[2].Text = "结算方式";
                        break;
                    case "Combine":
                        e.Row.Cells[2].Text = "关联合并";
                        e.Row.Cells[3].Text = "本订单与 " + string.Format("<a class=order href=\"/OrderView/{1}.html\" target=_blank>{0}</a>", DataBinder.Eval(e.Row.DataItem, "originalid").ToString(), DataBinder.Eval(e.Row.DataItem, "ChangInfo").ToString()) + " 合并关联";//DataBinder.Eval(e.Row.DataItem, "ChangInfo").ToString().Split("$".ToCharArray())[0];
                        //string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1} {2}</a>", DataBinder.Eval(e.Row.DataItem, "ChangInfo").ToString().Split("$".ToCharArray())[1], DataBinder.Eval(e.Row.DataItem, "ChangInfo").ToString().Split("$".ToCharArray())[0], DataBinder.Eval(e.Row.DataItem, "ChangInfo").ToString().Split("$".ToCharArray())[2])
                        break;
                    default:
                        break;
                }

                switch (DataBinder.Eval(e.Row.DataItem, "flag").ToString())
                {
                    case "0":
                        e.Row.Cells[7].Text = string.Format("<A class=\"order Doit\" href=\"javascript:void(0)\" tgs={0} flag={1}>操作</A> ", DataBinder.Eval(e.Row.DataItem, "id"), DataBinder.Eval(e.Row.DataItem, "applyflag"));
                        break;
                    case "1":
                        e.Row.ForeColor = System.Drawing.Color.Blue;
                        break;
                    case "2":
                        e.Row.ForeColor = System.Drawing.Color.Red;
                        break;
                    default:
                        break;
                }
                //if (DataBinder.Eval(e.Row.DataItem, "Sale").ToString() == "1") e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}