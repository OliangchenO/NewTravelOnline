using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.LoginUsers;

namespace TravelOnline.CruisesOrder
{
    public partial class OrderPrint : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice, CuisesList;
        public string hide, User_Info, User_Memo, GuestList, Contract, Invoice, AutoId, JSONData, DinnerInfo;
        public string lineid, visit, pay, cancel, visa, change, other, rebate;

        public string flag, OrderInfo, LinkInfo, RoomInfo, PriceInfo, GuestInfo, RebateInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["flag"];
            QueryId = Request.QueryString["OrderId"];
            if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                hide = "hide";
                LoadTempOrder();
            }
            else
            {
                Response.Write("您没有权限查看！");
                Response.End();
            }
        }

        protected void LoadTempOrder()
        {
            string SqlQueryText;

            SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", QueryId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserDept"]) == DS.Tables[0].Rows[0]["orderdept"].ToString() || Convert.ToString(Session["Manager_UserId"]).Length > 0 || Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                }
                else
                {
                    Response.Write("您没有权限查看！");
                    Response.End();
                }

                StringBuilder Strings = new StringBuilder();
                string names = " 确认单";
                if (flag == "agent") names = " 同行确认单";
                //if (Request.QueryString["DeptId"] == null)
                //{
                //}
                lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString() + names;
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                rebate = DS.Tables[0].Rows[0]["rebate"].ToString();

                string ProType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                string ProClass = DS.Tables[0].Rows[0]["ProductClass"].ToString();

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                //Strings.Append(string.Format("<tr><td class=linename height=35px colspan=6>{0}{1}</td></tr>", DS.Tables[0].Rows[0]["LineName"].ToString(), names));
                Strings.Append(string.Format("<tr><td width=10%>订单号</td><td width=23%>{0}</td><td width=10%>预定时间</td><td width=23%>{1}</td><td width=10%>出发日期</td><td width=23%><b>{2:yyyy-MM-dd}</b></td></tr>", DS.Tables[0].Rows[0]["AutoId"].ToString(), DS.Tables[0].Rows[0]["OrderTime"].ToString(), DS.Tables[0].Rows[0]["BeginDate"]));
                Strings.Append(string.Format("<tr><td>预定人数</td><td>{0}人 ({1}+{2})</td><td>费用合计</td><td><b>¥ {3}</b></td><td>人均费用</td><td><b>¥ {4}</b></td></tr>", Nums, Adults, Childs, AllPrice, AvePrice));

                if (flag == "agent")
                {
                    if (MyConvert.ConToInt(rebate) > 0)
                    {
                        if (DS.Tables[0].Rows[0]["RebateFlag"].ToString() == "1")
                        {
                            Strings.Append(string.Format("<tr><td>结算方式</td><td>按订单结算价</td><td>结算费用</td><td colspan=3><b>¥ {0} ({1})</b></td></tr>", (price - MyConvert.ConToInt(rebate)).ToString(), price.ToString() + " - " + rebate));
                        }
                        else
                        {
                            Strings.Append(string.Format("<tr><td>结算方式</td><td>按订单全额</td><td>结算费用</td><td colspan=3><b>¥ {0}</b></td></tr>", price.ToString()));
                        }
                    }
                    Strings.Append("<tr><td>收款人</td><td>上海中国青年旅行社有限公司</td><td>开户行</td><td>上海银行淮海支行</td><td>银行帐号</td><td>31682900005125964</td></tr>");

                }
                

                OrderInfo = Strings.ToString();

                Strings.Clear();
                Strings.Append(string.Format("<tr><td width=10%>联系人</td><td width=23%>{0}</td><td width=10%>联系电话</td><td width=23%>{1}&nbsp;</td><td width=10%>移动电话</td><td width=23%>{2}&nbsp;</td></tr>", DS.Tables[0].Rows[0]["OrderName"].ToString(), DS.Tables[0].Rows[0]["OrderTel"].ToString(), DS.Tables[0].Rows[0]["OrderMobile"].ToString()));
                Strings.Append(string.Format("<tr><td>特别说明</td><td colspan=5>{0}&nbsp;</td></tr>", DS.Tables[0].Rows[0]["OrderMemo"].ToString()));

                LinkInfo = Strings.ToString();

                Strings.Clear();
                SqlQueryText = string.Format("select * from CR_RoomOrder where OrderId='{0}'", QueryId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<tr class=trt>");
                    Strings.Append("<td width=\"20%\">房间类型</td>");
                    Strings.Append("<td width=\"6%\">成人</td>");
                    Strings.Append("<td width=\"6%\">儿童</td>");
                    Strings.Append("<td width=\"8%\">房间数</td>");
                    Strings.Append("<td width=\"12%\">第1,2人价格</td>");
                    Strings.Append("<td width=\"12%\">第3成人价</td>");
                    Strings.Append("<td width=\"12%\">第3儿童价</td>");
                    Strings.Append("<td width=\"12%\">价格小计</td>");
                    Strings.Append("</tr>");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<tr><td>{0}</td><td class=ac>{1}</td><td class=ac>{2}</td><td class=ac>{3}间</td><td class=ar>{4}</td><td class=ar>{5}</td><td class=ar>{6}</td><td class=ar>{7}</td></tr>",
                            DS.Tables[0].Rows[i]["roomname"].ToString(),
                            DS.Tables[0].Rows[i]["adult"].ToString(),
                            DS.Tables[0].Rows[i]["childs"].ToString(),
                            DS.Tables[0].Rows[i]["rooms"].ToString(),
                            DS.Tables[0].Rows[i]["price"].ToString(),
                            DS.Tables[0].Rows[i]["thirdprice"].ToString(),
                            DS.Tables[0].Rows[i]["childprice"].ToString(),
                            DS.Tables[0].Rows[i]["gather"].ToString()
                        ));
                    }
                    string AllRoom = DS.Tables[0].Compute("sum(rooms)", "true").ToString();
                    string RoomPrice = DS.Tables[0].Compute("sum(gather)", "true").ToString();

                    Strings.Append(string.Format("<tr><td class=ac>{0}</td><td class=ac>{1}</td><td class=ac>{2}</td><td class=ac>{3}间</td><td class=ar>{4}</td><td class=ar>{5}</td><td class=ar>{6}</td><td class=ar><b>&yen; {7:N}</b></td></tr>",
                        "<b>合计</b>",
                        Adults,
                        Childs,
                        AllRoom,
                        "&nbsp",
                        "&nbsp",
                        "&nbsp",
                        RoomPrice
                    ));
                    RoomInfo = Strings.ToString();

                    if (MyConvert.ConToInt(rebate) > 0)
                    {
                        if (flag == "agent")
                        {
                            Strings.Clear();
                            hide = "";
                            Strings.Append("<tr class=trt>");
                            Strings.Append("<td width=\"20%\">房间类型</td>");
                            Strings.Append("<td width=\"6%\">成人</td>");
                            Strings.Append("<td width=\"6%\">儿童</td>");
                            Strings.Append("<td width=\"8%\">房间数</td>");
                            Strings.Append("<td width=\"12%\">第1,2人返利</td>");
                            Strings.Append("<td width=\"12%\">第3成人返</td>");
                            Strings.Append("<td width=\"12%\">第3儿童返</td>");
                            Strings.Append("<td width=\"12%\">返利小计</td>");
                            Strings.Append("</tr>");
                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                Strings.Append(string.Format("<tr><td>{0}</td><td class=ac>{1}</td><td class=ac>{2}</td><td class=ac>{3}间</td><td class=ar>{4}</td><td class=ar>{5}</td><td class=ar>{6}</td><td class=ar>{7}</td></tr>",
                                    DS.Tables[0].Rows[i]["roomname"].ToString(),
                                    DS.Tables[0].Rows[i]["adult"].ToString(),
                                    DS.Tables[0].Rows[i]["childs"].ToString(),
                                    DS.Tables[0].Rows[i]["rooms"].ToString(),
                                    DS.Tables[0].Rows[i]["rebate"].ToString(),
                                    DS.Tables[0].Rows[i]["thirdrebate"].ToString(),
                                    DS.Tables[0].Rows[i]["childrebate"].ToString(),
                                    DS.Tables[0].Rows[i]["AllRebate"].ToString()
                                ));
                            }

                            string RoomRebate = DS.Tables[0].Compute("sum(AllRebate)", "true").ToString();

                            Strings.Append(string.Format("<tr><td class=ac>{0}</td><td class=ac>{1}</td><td class=ac>{2}</td><td class=ac>{3}间</td><td class=ar>{4}</td><td class=ar>{5}</td><td class=ar>{6}</td><td class=ar><b>&yen; {7:N}</b></td></tr>",
                                "<b>合计</b>",
                                Adults,
                                Childs,
                                AllRoom,
                                "&nbsp",
                                "&nbsp",
                                "&nbsp",
                                RoomRebate
                            ));

                            RebateInfo = Strings.ToString();
                        }
                    }
                }

                Strings.Clear();
                SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}' order by InputDate", QueryId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<tr class=trt>");
                    Strings.Append("<td width=\"16%\">费用类型</td>");
                    Strings.Append("<td width=\"42%\">费用说明</td>");
                    Strings.Append("<td width=\"12%\">价格</td>");
                    Strings.Append("<td width=\"6%\">数量</td>");
                    Strings.Append("<td width=\"12%\">价格小计</td>");
                    Strings.Append("</tr>");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td class=ar>{2}</td><td class=ac>{3}</td><td class=ar>{4}</td></tr>",
                            DS.Tables[0].Rows[i]["PriceName"].ToString(),
                            DS.Tables[0].Rows[i]["PriceMemo"].ToString(),
                            DS.Tables[0].Rows[i]["SellPrice"].ToString(),
                            DS.Tables[0].Rows[i]["OrderNums"].ToString(),
                            DS.Tables[0].Rows[i]["SumPrice"].ToString()
                        ));
                    }
                    string PriceCount = DS.Tables[0].Compute("sum(SumPrice)", "true").ToString();

                    Strings.Append(string.Format("<tr><td class=ac>{0}</td><td>{1}</td><td class=ar>{2}</td><td class=ac>{3}</td><td class=ar><b>&yen; {4:N}</b></td></tr>",
                        "<b>合计</b>",
                        "&nbsp",
                        "&nbsp",
                        "&nbsp",
                        PriceCount
                    ));
                }
                PriceInfo = Strings.ToString();

                Strings.Clear();
                SqlQueryText = string.Format("select id,flag,GuestName,Sex,IdNumber,BirthDay,visitid,listid,(select TabelNo from CR_DinnerNo where id=OL_GuestInfo.DinnerId) as TabelNo,(select DinnerTime from CR_DinnerNo where id=OL_GuestInfo.DinnerId) as DinnerTime,(select peoples from CR_RoomList where id=OL_GuestInfo.listid) as peoples from OL_GuestInfo where flag='0' and OrderId='{0}' order by listid", QueryId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    SqlQueryText = string.Format("select guestid,BusNo,(select visitname from CR_Visit where id=CR_VisitList.visitid) as visitname from CR_VisitList where OrderId='{0}' and flag='0'", QueryId);
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                    SqlQueryText = string.Format("select id,roomname,roomno,roomcode,BedType,(select BookingNo from CR_RoomNo where id=CR_RoomList.roomnoid) as BookingNo from CR_RoomList where OrderId='{0}' and orderflag='0'", QueryId);
                    DataSet DS2 = new DataSet();
                    DS2.Clear();
                    DS2 = MyDataBaseComm.getDataSet(SqlQueryText);

                    Strings.Append("<tr class=trt>");
                    Strings.Append("<td width=\"20%\">入住舱房及房号</td>");
                    Strings.Append("<td width=\"12%\">姓名</td>");
                    Strings.Append("<td width=\"6%\">性别</td>");
                    Strings.Append("<td width=\"12%\">出生日期</td>");
                    Strings.Append("<td width=\"35%\">观光线路及车号</td>");
                    Strings.Append("<td width=\"10%\">用餐</td>");
                    Strings.Append("</tr>");

                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            Strings.Append(string.Format("<tr><td rowspan=\"{0}\">{1}</td>",
                                DS.Tables[0].Rows[i]["peoples"].ToString(),
                                PurchaseClass.GetRoomName(DS.Tables[0].Rows[i]["listid"].ToString(), DS2.Tables[0])
                            ));
                        }
                        else
                        {
                            if (DS.Tables[0].Rows[i]["listid"].ToString() != DS.Tables[0].Rows[i-1]["listid"].ToString())
                            {
                                Strings.Append(string.Format("<tr><td rowspan=\"{0}\">{1}</td>",
                                    DS.Tables[0].Rows[i]["peoples"].ToString(),
                                    PurchaseClass.GetRoomName(DS.Tables[0].Rows[i]["listid"].ToString(), DS2.Tables[0])
                                ));
                            }
                            else
                            {
                                Strings.Append("<tr>");
                            }
                        }
                        Strings.Append(string.Format("<td class=ac>{0}</td><td class=ac>{1}</td><td class=ac>{2:yyyy-MM-dd}</td><td>{3}&nbsp;</td><td>{4}</td></tr>",
                            DS.Tables[0].Rows[i]["GuestName"].ToString(),
                            PurchaseClass.GetSex(DS.Tables[0].Rows[i]["Sex"].ToString()),
                            DS.Tables[0].Rows[i]["BirthDay"],
                            PurchaseClass.GetVisitNameAndBusNo(DS.Tables[0].Rows[i]["id"].ToString(), DS1.Tables[0]),
                            "<b>" + DS.Tables[0].Rows[i]["TabelNo"].ToString() + "</b> " + DS.Tables[0].Rows[i]["DinnerTime"].ToString()
                        ));
                    }
                }
                GuestInfo = Strings.ToString();

                SqlQueryText = string.Format("select * from CR_Confirm where lineid='{0}'", lineid);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    visit = DS.Tables[0].Rows[0]["visit"].ToString().Replace("\n", "<br>");
                    pay = DS.Tables[0].Rows[0]["pay"].ToString().Replace("\n", "<br>");
                    cancel = DS.Tables[0].Rows[0]["cancel"].ToString().Replace("\n", "<br>");
                    visa = DS.Tables[0].Rows[0]["visa"].ToString().Replace("\n", "<br>");
                    change = DS.Tables[0].Rows[0]["change"].ToString().Replace("\n", "<br>");
                    other = DS.Tables[0].Rows[0]["other"].ToString().Replace("\n", "<br>");
                }

                PurchaseClass.OrderExtendInfo ExtendInfo = new PurchaseClass.OrderExtendInfo();
                ExtendInfo = PurchaseClass.GetOrderExtInfo(QueryId, ProType, ProClass);
                DinnerInfo = ExtendInfo.Dinner;
            }
            else
            {
                Response.Write("订单不存在！");
                Response.End();
            }
        }

    }
}