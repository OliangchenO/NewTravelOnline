﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline.Users
{
    public partial class UserOrder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length == 0) Response.Redirect("/login/login.aspx", true);
            if (!IsPostBack)
            {
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            string sqlstr = string.Format("SELECT *,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderFlag<>'9' and OrderUser='{0}'", Convert.ToString(Session["Online_UserId"]));
            //if (TB_Name.Text.Trim().Length > 0) sqlstr = string.Format("{0} and AfficheName like '%{1}%' ", sqlstr, TB_Name.Text.Trim());
            DateTime date = DateTime.Today;
            date = date.AddMonths(-1);
            if (DropDownList1.Text == "1")
            {
                sqlstr = string.Format("{0} and OrderTime >= '{1}' ", sqlstr, date.ToString());
            }
            else
            {
                sqlstr = string.Format("{0} and OrderTime < '{1}' ", sqlstr, date.ToString());
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
                e.Row.Cells[3].Text = string.Format("<a href=\"/line/{2}.html\" target=_blank>{3}</a>", DataBinder.Eval(e.Row.DataItem, "ProductType"), DataBinder.Eval(e.Row.DataItem, "ProductClass"), DataBinder.Eval(e.Row.DataItem, "LineID"), DataBinder.Eval(e.Row.DataItem, "LineName"));
                
                //<A href="/OrderView/.html" style="color: #159ce9"><%=AutoId %></A>
                switch (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString())
                {
                    case "10":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">新订单</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                    case "30":
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">已确认</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;          
                    default:
                        e.Row.Cells[1].Text = string.Format("<a class=tip tag={0} href=\"javascript:void(0);\">新订单</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        break;
                }                

                if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "10" || DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "30")
                {
                    decimal Fee = MyConvert.ConToDec(DataBinder.Eval(e.Row.DataItem, "Price").ToString());
                    decimal Pay = MyConvert.ConToDec(DataBinder.Eval(e.Row.DataItem, "Pay").ToString());
                    decimal Yue = Fee - Pay;

                    if (DataBinder.Eval(e.Row.DataItem, "PayType").ToString() == "2")
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "10" || DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "30")
                        {
                            if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "shipid").ToString()) == 0)
                            {
                                e.Row.Cells[8].Text = string.Format("<a class=pay href=\"javascript:CancelOrder('{0}');void(0);\" >取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                            }
                        }
                     }
                    else
                    {
                        if (Yue > 0)
                        {
                            if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() == "Coupon")
                            {
                                e.Row.Cells[8].Text = string.Format("<a class=pay1 href=\"/Pay/PayNow.aspx?CouponId={0}\" target=_blank>付款</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                            }
                            else
                            {
                                e.Row.Cells[8].Text = string.Format("<a class=pay1 href=\"/NewPage/pay/Pay.aspx?OrderId={0}\" target=_blank>付款</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                            }
                            
                        }

                        if ((DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "10"|| DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "30") && Pay == 0)
                        {
                            if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "shipid").ToString()) == 0)
                            {
                                e.Row.Cells[8].Text += string.Format(" <a class=pay href=\"javascript:CancelOrder('{0}');void(0);\" >取消</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                            }
                        }
                    }
                    
                    //else
                    //{
                    //    //e.Row.Cells[7].Text += " " + string.Format("<a class=pay href=\"/Pay/Contract.aspx?OrderId={0}\" target=_blank>合同</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                    //}

                    if (Pay > 0)
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() != "Coupon")
                        {
                            e.Row.Cells[8].Text += " " + string.Format(" <a class=pay href=\"/Order/Contract/{0}.html\" target=_blank>合同</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        }
                    }

                    if (MyConvert.ConToInt(DataBinder.Eval(e.Row.DataItem, "shipid").ToString()) > 0)
                    {
                        e.Row.Cells[8].Text += " " + string.Format(" <a class=pay href=\"/CruisesOrder/OrderPrint.aspx?OrderId={0}\" target=_blank>确认单</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                    }
                    //if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() == "Cruises" && DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() != "2")
                    //{
                    //    e.Row.Cells[8].Text = "";
                    //}

                    //if (DataBinder.Eval(e.Row.DataItem, "PayFlag").ToString() == "1")
                    //{
                    //    e.Row.Cells[7].Text = "已付款";
                    //    e.Row.Cells[7].Text += " " + string.Format("<a class=pay href=\"/Pay/Contract.aspx?OrderId={0}\" target=_blank>合同</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        
                    //}
                    //else if (DataBinder.Eval(e.Row.DataItem, "PayFlag").ToString() == "0")
                    //{
                    //    e.Row.Cells[7].Text = string.Format("<a class=pay href=\"/Pay/PayNow.aspx?OrderId={0}\" target=_blank>付款</a>", DataBinder.Eval(e.Row.DataItem, "OrderId"));
                        
                    //    //散拼未确认暂时不能付款
                    //    if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() == "FreeTour" && DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() != "2")
                    //    {
                    //        e.Row.Cells[7].Text = "";
                    //    }
                    //    if (DataBinder.Eval(e.Row.DataItem, "ProductType").ToString() == "Cruises" && DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() != "2")
                    //    {
                    //        e.Row.Cells[7].Text = "";
                    //    }
                    //}
                }
                else
                {
                    //if (DataBinder.Eval(e.Row.DataItem, "PayFlag").ToString() == "1")
                    //{
                    //    e.Row.Cells[7].Text = "已付款";
                    //}
                }
                if (DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "8" || DataBinder.Eval(e.Row.DataItem, "OrderFlag").ToString() == "9")
                {
                    e.Row.Cells[8].Text = "";
                }
                
            }
        }

        protected void GridView_Serch(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
            this.GridView_DataBind();
        }
    }
}