using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;

namespace TravelOnline.CruisesOrder
{
    public partial class VisitEdit : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, shipid, PriceList;
        public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap;
        public string OptionHtml;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            
            if (Convert.ToString(Session["Online_UserDept"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", QueryId);
            //Response.Write(SqlQueryText);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "8" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9")
                {
                    Response.Write("已经取消的订单不能操作！");
                    Response.End();
                }
                if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
                {
                }
                else
                {
                    if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                }
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                NumsInfo = string.Format(" 预订人数：{0}成人", Adults);
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                PriceList = GetPriceList(Convert.ToInt32(Nums), Convert.ToInt32(Adults), Convert.ToInt32(Childs), DS.Tables[0].Rows[0]["LineID"].ToString(), DS.Tables[0].Rows[0]["PlanId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"].ToString(), DS.Tables[0].Rows[0]["OrderId"].ToString(), DS.Tables[0].Rows[0]["ProductType"].ToString(), Convert.ToString(Session["Online_UserId"]), Convert.ToInt32(shipid));
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        
        protected string GetPriceList(int OrderNums, int Adults, int Childs, string Lineid, string Planid, string BeginDate, string OrderId, string OrderType, string UserId, int shipid)
        {
            StringBuilder ExtStrings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();

            DataSet DS1 = new DataSet();
            DS1.Clear();

            string SqlQueryText;
            SqlQueryText = "select * from  View_CR_Visit where sellflag='0' and nums>=(orders+" + OrderNums + ") and lineid='" + Lineid + "' order by days";
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //SqlQueryText = "select * from OL_OrderPrice where PriceType='ShipVisit' and OrderId='" + QueryId + "'";
                SqlQueryText = "select count(1) as OrderNums,visitid from CR_VisitList where flag='0' and OrderId='" + QueryId + "' group by visitid";
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                OptionHtml = "";
                for (int i = 0; i <= OrderNums; i++)
                {
                    OptionHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
                }

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
                        ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
                        ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tnamelong>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                        ExtStrings.Append("");

                        ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fnamelong title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                        ExtStrings.Append("<div class=fnum><select class=psel>");
                        ExtStrings.Append(GetOptionHtml(DS.Tables[0].Rows[i]["id"].ToString(), OrderNums, DS1.Tables[0]));
                        ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                    }
                    else
                    {
                        if (DS.Tables[0].Rows[i]["days"].ToString() != DS.Tables[0].Rows[i - 1]["days"].ToString())
                        {
                            ExtStrings.Append("</ul>");
                            ExtStrings.Append("</div></div>");
                            ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
                            ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
                            ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tnamelong>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                            ExtStrings.Append("");

                            ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fnamelong title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                            ExtStrings.Append("<div class=fnum><select class=psel>");
                            ExtStrings.Append(GetOptionHtml(DS.Tables[0].Rows[i]["id"].ToString(), OrderNums, DS1.Tables[0]));
                            ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                        }
                        else
                        {
                            ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fnamelong title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                            ExtStrings.Append("<div class=fnum><select class=psel>");
                            ExtStrings.Append(GetOptionHtml(DS.Tables[0].Rows[i]["id"].ToString(), OrderNums, DS1.Tables[0]));
                            ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                        }
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            ExtStrings.Append("</ul>");
                            ExtStrings.Append("</div></div>");
                        }
                    }
                }
                
            }
            return ExtStrings.ToString();
        }

        protected string GetOptionHtml(string ids, int OrderNums, DataTable dt)
        {
            string OptionSelectHtml = "";
            //DataRow[] drs = dt.Select("visitid='ShipVisit'");
            DataRow[] drs = dt.Select("visitid=" + ids);
            foreach (DataRow dr in drs)
            {
                //if (dr["PriceId"].ToString() == ids)
                //{
                    
                //}
                for (int i = 0; i <= OrderNums; i++)
                {
                    if (i == Convert.ToInt32(dr["OrderNums"].ToString()))
                    {
                        OptionSelectHtml += string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                    }
                    else
                    {
                        OptionSelectHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
                    }
                }
            }
            //OptionSelectHtml += string.Format("<option value=\"{0}\">{0}</option>", ids + "/" + drs.Count());
            if (OptionSelectHtml.Length < 3) OptionSelectHtml = OptionHtml;
            return OptionSelectHtml;
        }
    }
}