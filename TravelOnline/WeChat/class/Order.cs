using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using TravelOnline.Class.Travel;
using TravelOnline.TravelMisWebService;
using System.Text.RegularExpressions;
using TravelOnline.Class.Purchase;
using TravelOnline.GetCombineKeys;
using TravelOnline.LoginUsers;
using TravelOnline.WeChat.freetrip.interfaces;
using Belinda.Jasp;

namespace TravelOnline.WeChat
{
    public class WeChatOrder
    {
        public static string TempOrder(string lineid, string planid, string begindate, string adults, string childs)
        {
            Guid ucode = CombineKeys.NewComb();
            PlanSeats GetPlan = new PlanSeats();
            int allnums = Convert.ToInt32(adults) + Convert.ToInt32(childs);



            //浦发人数限制
            if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf("," + planid + ",") > -1)
            {
                if (allnums > 2)
                {
                    return "{\"error\":\"浦发信用卡立减活动，最多只能报名2人！\"}";
                }
            }

            if (Convert.ToString(ConfigurationManager.AppSettings["pdyhQianZheng"]).IndexOf("," + lineid + ",") > -1)
            {

                if (allnums > 1)
                {
                    return "{\"error\":\"浦发信用卡签证活动，最多只能报名1人！\"}";
                }
            }
            //浦发人数限制


            if (MyConvert.ConToInt(planid) == 0)
            {
                GetPlan.Seats = "99";
                GetPlan.Route = "99";
                GetPlan.StopDate = "";
            }
            else
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, begindate);
                }
                catch
                {
                    GetPlan.Seats = "0";
                    GetPlan.Route = "99";
                    GetPlan.StopDate = "";
                }
            }

            if (GetPlan.StopDate.Length > 0)
            {
                DateTime rp_times = DateTime.Now;
                if (Convert.ToDateTime(GetPlan.StopDate) < DateTime.Today)
                {

                    return "{\"error\":\"已报名截止\"}";
                }
                if (Convert.ToDateTime(GetPlan.StopDate) == DateTime.Today)
                {

                    if (rp_times.Hour >= 17)
                    {
                        return "{\"error\":\"已报名截止\"}";
                    }
                }
            }

            if (MyConvert.ConToInt(GetPlan.Seats) < allnums)
            {
                return "{\"error\":\"余位不足，剩" + GetPlan.Seats + "人\"}";
            }

            string routeflag;
            if (GetPlan.Route == "0")
            {
                routeflag = "0";
            }
            else
            {
                routeflag = "1";
            }

            PurchaseClass.LineClass LineInfos = new PurchaseClass.LineClass();
            LineInfos = PurchaseClass.LineDetail(lineid);
            if (LineInfos != null)
            {
                List<string> Sql = new List<string>();
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_TempOrder (OrderId,ProductType,ProductClass,LineID,PlanId,LineName,BeginDate,OrderNums,Adults,Childs,OrderTime,OrderFlag,DeptId,LineDays,RouteFlag,PlanNo,UserName) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                    ucode,
                    LineInfos.LineType,
                    LineInfos.LinesClass,
                    LineInfos.LineId,
                    planid,
                    LineInfos.LineName,
                    MyConvert.ConToDate(begindate),
                    allnums,
                    adults,
                    childs,
                    DateTime.Now.ToString(),
                    "0",
                    LineInfos.Deptid,
                    LineInfos.LineDays,
                    routeflag,
                    GetPlan.PlanNo,
                    ""
                );
                Sql.Add(SqlQueryText);

                if (Convert.ToString(ConfigurationManager.AppSettings["prebook"]).IndexOf("," + lineid + ",") > -1)
                {
                    string TableName = "OL_ActivityOrder";
                    string[] Fil = new string[5];
                    string[] Val = new string[5];
                    Fil[0] = "OrderId"; Val[0] = ucode.ToString();
                    Fil[1] = "AType"; Val[1] = "1";//预付活动
                    Fil[2] = "APrice"; Val[2] = "99";
                    Fil[3] = "ABeginDate"; Val[3] = Convert.ToDateTime("2015-11-11 00:00:00").ToString();
                    Fil[4] = "AEndDate"; Val[4] = Convert.ToDateTime("2015-11-11 23:59:59").ToString();
                    Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                }

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    HttpContext.Current.Session["OrderUid"] = ucode;
                    return "{\"success\":0,\"orderuid\":\"" + ucode + "\"}";
                }
                else
                {
                    return "{\"error\":\"报名失败\"}";
                }
            }
            else
            {
                return "{\"error\":\"报名失败\"}";
            }

        }

        public static string OrderFirstStep(string orderid)
        {
            List<string> lstPriceType = new List<string>();

            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;
            int OrderNums, Adults, Childs;
            string Lineid, Planid, BeginDate;
            string PriceId = "0";
            decimal groupDiscount = 0;
            SqlQueryText = string.Format("select * from OL_TempOrder where OrderFlag='0' and OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                Planid = DS.Tables[0].Rows[0]["PlanId"].ToString();
                BeginDate = DS.Tables[0].Rows[0]["BeginDate"].ToString();
                OrderNums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["OrderNums"].ToString());
                Adults = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Adults"].ToString());
                Childs = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Childs"].ToString());

                //取拼团信息
                groupDiscount = MyConvert.ConToDec(MyDataBaseComm.getScalar(string.Format("select discount from ol_groupplan where MisLineId='{0}' and GroupDate='{1:yyyy-MM-dd}'", DS.Tables[0].Rows[0]["LineId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"])));

                string DefaultOption = string.Format("value=0 max={0} min=0", OrderNums);
                string DefaultChildOption = string.Format("value=0 max={0} min=0", Childs);
                string AdultOption = string.Format("value={0} max={1} min=0", Adults, OrderNums);
                string ChildOption = string.Format("value={0} max={1} min=0", Childs, Childs);

                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                PlanPrices GetPlan = new PlanPrices();

                try
                {
                    if (Planid == "0" && BeginDate.Length > 5)
                    {
                        GetPlan = rsp.GetLinePrices(UpPassWord, Lineid, BeginDate);
                    }
                    else
                    {
                        GetPlan = rsp.GetPlanPrices(UpPassWord, Lineid, Planid, PriceId);
                    }
                }
                catch
                {
                    Strings.Append("<div class=\"sub_view info\" id=\"first_view\">");
                    Strings.Append("订单不存在，不能继续预订！");
                    Strings.Append("</div>");
                    return Strings.ToString();
                }

                Strings.Append("<div class=\"sub_view\" id=\"first_view\">");
                Strings.Append("<div class=\"product_wrap\">");
                Strings.Append(string.Format("<h3>{0}</h3>", DS.Tables[0].Rows[0]["LineName"].ToString()));
                if (MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["BeginDate"].ToString()) >= DateTime.Today)
                {
                    Strings.Append(string.Format("<p class=\"xdesc\"><h2><i class=\"fa fa-calendar\"></i> 出发日期: {0:yyyy-MM-dd}</h2></p>", DS.Tables[0].Rows[0]["BeginDate"]));
                }
                Strings.Append("<div class=\"price_box\" style=\"font-size: 16px;\"><i class=\"fa fa-cny\"></i> <span class=\"allprice price\"></span></div>");
                Strings.Append(string.Format("<input id=\"s_ordernums\" type=\"hidden\" value=\"{0}\"/>", OrderNums));
                Strings.Append(string.Format("<input id=\"s_adults\" type=\"hidden\" value=\"{0}\"/>", Adults));
                Strings.Append(string.Format("<input id=\"s_childs\" type=\"hidden\" value=\"{0}\"/>", Childs));
                Strings.Append("</div>");

                Strings.Append("<div class=\"recommend_wrap\">");

                //price_01 基本费用， price_02 必选费用， price_03 可选费用
                if (GetPlan.PlanStaPrice != null)
                {
                    Strings.Append("<div class=\"recommend_detail\">");
                    Strings.Append("<div class=\"recommend_txt price_01\">");
                    Strings.Append("<h3>基本费用</h3>");
                    Strings.Append("");

                    for (int j = 0; j < GetPlan.PlanStaPrice.Length; j++)
                    {
                        lstPriceType.Add(GetPlan.PlanStaPrice[j].PriceType);
                    }

                    for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                    {
                        if (GetPlan.PlanStaPrice[i].PriceType == "儿童价" && Childs == 0)
                        { }
                        else
                        {
                            //Strings.Append("<div class=\"row sellprice\">");//row 开始
                            Strings.Append(string.Format("<div class=\"row sellprice\" id=\"S{0}\">", GetPlan.PlanStaPrice[i].PriceId));

                            Strings.Append("<div class=\"col-xs-7\">");
                            Strings.Append(string.Format("<div><span class=\"pricename\">{0}</span><span class=\"sprice\"><i class=\"fa fa-cny\"></i> {1}</span></div>", GetPlan.PlanStaPrice[i].PriceType, MyConvert.ConToDec(GetPlan.PlanStaPrice[i].Price)- groupDiscount));
                            Strings.Append(string.Format("<div class=\"pricememo\">{0}</div>", GetPlan.PlanStaPrice[i].PriceName));
                            Strings.Append("</div>");

                            Strings.Append("<div class=\"col-xs-5\">");
                            Strings.Append("<div style=\"width:100px\">");
                            Strings.Append(string.Format("<input tps=SellPrice tagid=\"{0}\" price=\"{1}\" type=\"text\" class=\"form-control touch\" readonly ", GetPlan.PlanStaPrice[i].PriceId, MyConvert.ConToDec(GetPlan.PlanStaPrice[i].Price)-groupDiscount));
                            if (!lstPriceType.Contains("儿童价"))
                            {
                                if (!lstPriceType.Contains("儿童价") && i > 0)
                                {
                                    Strings.Append(DefaultOption);
                                }
                                else
                                {
                                    AdultOption = string.Format("value={0} max={1} min=0", Adults + Childs, OrderNums);
                                    Strings.Append(AdultOption);
                                    AdultOption = DefaultOption;
                                }
                            }
                            else
                            {
                                switch (GetPlan.PlanStaPrice[i].PriceType)
                                {
                                    case "成人价":
                                        Strings.Append(AdultOption);
                                        AdultOption = DefaultOption;
                                        break;
                                    case "儿童价":
                                        Strings.Append(ChildOption);
                                        ChildOption = DefaultChildOption;
                                        break;
                                    default:
                                        Strings.Append(DefaultOption);
                                        break;
                                }
                            }
                            Strings.Append(" /></div>");
                            Strings.Append("</div>");

                            Strings.Append("</div>");//row 结束
                        }
                    }
                    Strings.Append("</div></div>");
                    lstPriceType.Clear();
                }

                if (GetPlan.PlanExtPrice != null)
                {
                    string ExtType = "";
                    int NeedPrice = 0, NoNeedPrice = 0;
                    for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                    {
                        if (GetPlan.PlanExtPrice[i].OnlineNeeds == "0")
                        {
                            NoNeedPrice++;
                        }
                        else
                        {
                            NeedPrice++;
                        }
                    }

                    if (NeedPrice > 0)
                    {
                        Strings.Append("<div class=\"recommend_detail\">");
                        Strings.Append("<div class=\"recommend_txt price_02\">");
                        Strings.Append("<h3>必选费用</h3>");
                        Strings.Append("");
                        for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                        {
                            switch (GetPlan.PlanExtPrice[i].InfoId)
                            {
                                case "1":
                                    ExtType = "单房差";
                                    break;
                                case "2":
                                    ExtType = "自费项目";
                                    break;
                                case "3":
                                    ExtType = "小费";
                                    break;
                                case "4":
                                    ExtType = "其他费用";
                                    break;
                                case "5":
                                    ExtType = "保险费用";
                                    break;
                                default:
                                    ExtType = "";
                                    break;
                            }
                            if (GetPlan.PlanExtPrice[i].OnlineNeeds != "0")
                            {
                                //Strings.Append("<div class=\"row sellprice\">");//row 开始
                                Strings.Append(string.Format("<div class=\"row sellprice\" id=\"P{0}\">", GetPlan.PlanExtPrice[i].PriceId));

                                Strings.Append("<div class=\"col-xs-7\">");
                                //Strings.Append(string.Format("<div class=\"pricename\">{0}</div>", ExtType));
                                Strings.Append(string.Format("<div><span class=\"pricename\">{0}</span><span class=\"sprice\"><i class=\"fa fa-cny\"></i> {1}</span></div>", ExtType, GetPlan.PlanExtPrice[i].Price));
                                Strings.Append(string.Format("<div class=\"pricememo\">{0} {1}</div>", GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName));
                                Strings.Append("</div>");

                                Strings.Append("<div class=\"col-xs-5\">");
                                Strings.Append("<div style=\"width:100px\">");
                                Strings.Append(string.Format("<input tps=ExtPrice tagid=\"{0}\" price=\"{1}\" type=\"text\" class=\"form-control touch\"", GetPlan.PlanExtPrice[i].PriceId, GetPlan.PlanExtPrice[i].Price));

                                switch (GetPlan.PlanExtPrice[i].OnlineNeeds)
                                {
                                    case "1":
                                        Strings.Append(string.Format("value={0} max={0} min={0}", Adults));
                                        break;
                                    case "2":
                                        Strings.Append(string.Format("value={0} max={0} min={0}", Childs));
                                        break;
                                    case "3":
                                        Strings.Append(string.Format("value={0} max={0} min={0}", OrderNums));
                                        break;
                                    default:
                                        Strings.Append(string.Format("value={0} max={0} min={0}", OrderNums));
                                        break;
                                }
                                Strings.Append(" /></div>");
                                Strings.Append("</div>");

                                Strings.Append("</div>"); //row 结束
                            }

                        }
                        Strings.Append("</div></div>");
                    }

                    if (NoNeedPrice > 0)
                    {
                        Strings.Append("<div class=\"recommend_detail\">");
                        Strings.Append("<div class=\"recommend_txt price_03\">");
                        Strings.Append("<h3>可选费用</h3>");
                        Strings.Append("");
                        for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                        {
                            switch (GetPlan.PlanExtPrice[i].InfoId)
                            {
                                case "1":
                                    ExtType = "单房差";
                                    break;
                                case "2":
                                    ExtType = "自费项目";
                                    break;
                                case "3":
                                    ExtType = "小费";
                                    break;
                                case "4":
                                    ExtType = "其他费用";
                                    break;
                                case "5":
                                    ExtType = "保险费用";
                                    break;
                                default:
                                    ExtType = "";
                                    break;
                            }
                            if (GetPlan.PlanExtPrice[i].OnlineNeeds == "0")
                            {
                                //Strings.Append("<div class=\"row sellprice\">");//row 开始
                                Strings.Append(string.Format("<div class=\"row sellprice\" id=\"P{0}\">", GetPlan.PlanExtPrice[i].PriceId));

                                Strings.Append("<div class=\"col-xs-7\">");
                                Strings.Append(string.Format("<div><span class=\"pricename\">{0}</span><span class=\"sprice\"><i class=\"fa fa-cny\"></i> {1}</span></div>", ExtType, GetPlan.PlanExtPrice[i].Price));
                                Strings.Append(string.Format("<div class=\"pricememo\">{0} {1}</div>", GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName));
                                Strings.Append("</div>");

                                Strings.Append("<div class=\"col-xs-5\">");
                                Strings.Append("<div style=\"width:100px\">");
                                Strings.Append(string.Format("<input tps=ExtPrice tagid=\"{0}\" price=\"{1}\" type=\"text\" class=\"form-control touch\" readonly value=0 max={2} min=0 /></div>", GetPlan.PlanExtPrice[i].PriceId, GetPlan.PlanExtPrice[i].Price, OrderNums));
                                Strings.Append("");
                                Strings.Append("</div>");

                                Strings.Append("</div>"); //row 结束
                            }

                        }
                        Strings.Append("</div></div>");
                    }

                }
                string sql = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
                string Integral = MyDataBaseComm.getScalar(sql);
                if (!string.IsNullOrEmpty(Integral))
                {
                    Strings.Append("<div class=\"recommend_detail\">");
                    Strings.Append("<div class=\"recommend_txt price_04\">");
                    Strings.Append("<h3>积分抵扣</h3>");
                    Strings.Append("");
                    Strings.Append("<div class=\"row sellprice\">");
                    Strings.Append("<div class=\"col-xs-5\">");
                    //Strings.Append(string.Format("<div><span class=\"pricename\">{0}</span><span class=\"sprice\">{1}积分</span><input id=\"sumIntegral\" type=\"hidden\" value=\"{1}\" /><input id=\"Integral_ratio\" type=\"hidden\" value=\"{2}\" /></div>", "可用积分", Integral, ConfigurationManager.AppSettings["Integral_ratio"]));
                    Strings.Append(string.Format("<div><span class=\"pricename\">{0}</span></div>", "使用积分", Integral));//<input id=\"integral\" onblur=\"Validate()\" style=\"border-style:none; width:80px;\" type=\"text\" placeholder=\"可用积分：{1}\" />
                    Strings.Append(string.Format("<div class=\"pricememo\">{1}积分=1元<input id=\"sumIntegral\" type=\"hidden\" value=\"{0}\" /><input id=\"Integral_ratio\" type=\"hidden\" value=\"{1}\" /></div>", Integral, ConfigurationManager.AppSettings["Integral_ratio"]));
                    Strings.Append("</div>");
                    Strings.Append("<div class=\"col-xs-5\">");
                    Strings.Append("<div style=\"width:100px\">");
                    //Strings.Append(string.Format("<input id=\"integral\" onblur=\"Validate()\" style=\"border-style:none;\" type=\"text\" placeholder=\"输入积分({0}积分=1元)\" />", ConfigurationManager.AppSettings["Integral_ratio"]));
                    Strings.Append(string.Format("<span class=\"pricename\"><input id=\"integral\" onblur=\"Validate()\" style=\"border-style:none; width:150px;\" type=\"text\" placeholder=\"可用积分{0}\" /></span>", Integral));
                    Strings.Append("</div>");
                    Strings.Append("</div>");
                    Strings.Append("</div></div>");
                }

                Strings.Append("");
                Strings.Append("</div>"); //product_wrap end

                Strings.Append("<div class=\"pre-footer order-footer\"  style=\"position: fixed; bottom: -1px; left: 0px;width:101%\">");
                Strings.Append("<div class=\"container\">");
                Strings.Append("<div class=\"row\">");
                Strings.Append("<div class=\"col-xs-8\" style=\"text-align:left;\"><i class=\"fa fa-cny\"></i> <span class=\"allprice\">0</span></div>");
                Strings.Append("<div class=\"col-xs-4\" style=\"text-align:center\"><a class=\"yd cur\" href=\"javascript:;\" id=\"ordernow\"><i class=\"fa fa-chevron-circle-right\"></i> 下一步</a></div>");
                Strings.Append("</div></div></div>");

                Strings.Append("</div>"); //sub_view end
            }
            else
            {
                Strings.Append("<div class=\"sub_view info\" id=\"first_view\">");
                Strings.Append("订单不存在，不能继续预订！");
                Strings.Append("</div>");
            }

            return Strings.ToString();
        }

        #region OrderFirstStep_New
        public static object OrderFirstStep_New(string orderid)
        {
            JSONObject ObJson = new JSONObject();
            StringBuilder Strings = new StringBuilder();
            int OrderNums, Adults, Childs;
            string Lineid, Planid, BeginDate;
            string PriceId = "0";

            string sql = string.Format("select * from OL_TempOrder where OrderFlag='0' and OrderId='{0}'", orderid);
            DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                JSONArray ArrJson = new JSONArray();
                dt.Columns.Add(new DataColumn("EqualToday", typeof(bool)));
                dt.Columns.Add(new DataColumn("data", typeof(string)));
                dt.Rows[0]["EqualToday"] = false;
                Lineid = dt.Rows[0]["LineID"].ToString();
                Planid = dt.Rows[0]["PlanId"].ToString();
                BeginDate = dt.Rows[0]["BeginDate"].ToString();
                OrderNums = MyConvert.ConToInt(dt.Rows[0]["OrderNums"].ToString());
                Adults = MyConvert.ConToInt(dt.Rows[0]["Adults"].ToString());
                Childs = MyConvert.ConToInt(dt.Rows[0]["Childs"].ToString());

                string DefaultOption = string.Format("value=0 max={0} min=0", OrderNums);
                string DefaultChildOption = string.Format("value=0 max={0} min=0", Childs);
                string AdultOption = string.Format("value={0} max={1} min=0", Adults, OrderNums);
                string ChildOption = string.Format("value={0} max={1} min=0", Childs, Childs);
                dt.Rows[0]["OrderNums"] = OrderNums;
                dt.Rows[0]["Adults"] = Adults;
                dt.Rows[0]["Childs"] = Childs;
                dt.Rows[0]["data"] = string.Format("{0:yyyy-MM-dd}", dt.Rows[0]["BeginDate"]);

                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                PlanPrices GetPlan = new PlanPrices();
                ObJson.Add("DefaultOption", DefaultOption);
                ObJson.Add("DefaultChildOption", DefaultChildOption);
                ObJson.Add("AdultOption", AdultOption);
                ObJson.Add("ChildOption", ChildOption);
                try
                {
                    if (Planid == "0" && BeginDate.Length > 5)
                    {
                        GetPlan = rsp.GetLinePrices(UpPassWord, Lineid, BeginDate);
                    }
                    else
                    {
                        GetPlan = rsp.GetPlanPrices(UpPassWord, Lineid, Planid, PriceId);
                    }
                    if (GetPlan.PlanStaPrice != null)
                    {
                        ObJson.Add("PlanStaPrice", GetPlan.PlanStaPrice);
                    }
                    if (GetPlan.PlanExtPrice != null)
                    {
                        ObJson.Add("PlanExtPrice", GetPlan.PlanExtPrice);
                    }
                }
                catch
                {
                    ObJson.Add("PlanStaPrice", string.Empty);
                    ObJson.Add("PlanExtPrice", string.Empty);
                }
                if (MyConvert.ConToDateTime(dt.Rows[0]["BeginDate"].ToString()) >= DateTime.Today)
                {
                    dt.Rows[0]["EqualToday"] = true;
                }
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
                string sql1 = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
                string Integral = MyDataBaseComm.getScalar(sql1);
                if (!string.IsNullOrEmpty(Integral))
                {
                    ObJson.Add("Integral", Integral);
                    ObJson.Add("Integral_ratio", ConfigurationManager.AppSettings["Integral_ratio"]);
                }
            }
            else
            {
                ObJson.Add("rows", null);
            }
            return json.SerializeObject(ObJson);
        }
        #endregion

        public static string OrderSecondStep(string orderid)
        {
            string SqlQueryText = string.Format("select *,(select sum(SumPrice) from OL_OrderPrice where orderid='{0}') as SumPrice,(select wwwyh from OL_Line where MisLineId=OL_TempOrder.lineid) as wwwyh from OL_TempOrder where OrderFlag='0' and OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) != MyConvert.ConToDec(DS.Tables[0].Rows[0]["SumPrice"].ToString())) return "<div class=\"sub_view\" id=\"second_view\">订单费用错误，不能继续预订</div>";

                StringBuilder Strings = new StringBuilder();
                int wwwyh = MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString());
                int preferAmount = 0;
                decimal groupDiscount = 0;

                SqlQueryText = string.Format("select preferAmount from OL_Preferential where Lineid='{0}' and startDate<='{1}' and endDate>='{1}' and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate())", DS.Tables[0].Rows[0]["LineId"].ToString(), Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"].ToString()));
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    preferAmount = MyConvert.ConToInt(DS1.Tables[0].Rows[0]["preferAmount"].ToString());
                }
                //groupDiscount = MyConvert.ConToDec(MyDataBaseComm.getScalar(string.Format("select discount from ol_groupplan where MisLineId='{0}' and GroupDate='{1:yyyy-MM-dd}'", DS.Tables[0].Rows[0]["LineId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"])));
                //string User_Name = "", User_Mobile = "", User_Tel = "", User_Email = "";
                //if (Convert.ToString(HttpContext.Current.Session["Online_UserId"]) != "")
                //{
                //    LoginUser.RegistUser RUser = new LoginUser.RegistUser();
                //    SqlQueryText = string.Format("Id='{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
                //    RUser = LoginUser.LoginUseDetail(SqlQueryText);
                //    if (RUser != null)
                //    {
                //        User_Email = RUser.UserEmail;
                //        User_Name = RUser.UserName;
                //        User_Tel = RUser.Tel;
                //        User_Mobile = RUser.Mobile;
                //    }
                //}

                Strings.Append("<div class=\"sub_view\" id=\"second_view\">");
                Strings.Append("<div class=\"recommend_wrap\">");

                //联系人 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>联系人信息</h3>");
                Strings.Append("<form id=\"form1\" onsubmit=\"return false;\" method=\"post\">");
                Strings.Append(string.Format("<input maxlength=\"30\" id=\"ordername\" type=\"text\" name=\"ordername\" class=\"form-control ordertext\" placeholder=\"联系人姓名\" value=\"{0}\">", Convert.ToString(HttpContext.Current.Session["Online_UserName"])));
                Strings.Append(string.Format("<input maxlength=\"11\" id=\"orderphone\" type=\"text\" name=\"orderphone\" class=\"form-control ordertext\" placeholder=\"手机号码\" value=\"{0}\">", Convert.ToString(HttpContext.Current.Session["Online_WeChatPhone"])));
                Strings.Append(string.Format("<input maxlength=\"50\" id=\"orderemail\" type=\"text\" name=\"orderemail\" class=\"form-control ordertext\" placeholder=\"电子邮件\" value=\"{0}\">", Convert.ToString(HttpContext.Current.Session["Online_WeChatEmail"])));
                Strings.Append(string.Format("<input maxlength=\"100\" id=\"ordermemo\" type=\"text\" name=\"ordermemo\" class=\"form-control ordertext\" placeholder=\"订单特别说明\">", ""));
                Strings.Append(string.Format("<input id=\"Preference\" type=\"hidden\" value=\"{0}\">", wwwyh));
                Strings.Append(string.Format("<input id=\"preferAmount\" type=\"hidden\" value=\"{0}\">", preferAmount));
                Strings.Append(string.Format("<input id=\"groupDiscount\" type=\"hidden\" value=\"{0}\">", groupDiscount));
                
                Strings.Append("</form>");
                Strings.Append("</div></div>");
                //联系人 end

                //付款方式 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3 class=\"pay\">");
                Strings.Append("<input value=\"1\" type=\"radio\" id=\"radio-1\" name=\"iCheck\" checked><label for=\"radio-1\" class=\"hovers\">在线支付</label>&nbsp;&nbsp;&nbsp;&nbsp;");
                //Strings.Append("<input value=\"2\" type=\"radio\" id=\"radio-2\" name=\"iCheck\"><label for=\"radio-2\" class=\"hovers\">门店支付</label>");
                Strings.Append("</h3>");

                Strings.Append("<div id=\"Pre1\">");
                if (wwwyh > 0) Strings.Append(string.Format("<div class=\"pre\">在线支付每人立减 {0} 元</div>", wwwyh));
                if (preferAmount > 0) Strings.Append(string.Format("<div class=\"pre\">早定早优惠每人立减 {0} 元</div>", preferAmount));
                if (groupDiscount>0) Strings.Append(string.Format("<div class=\"pre\">拼团优惠每人立减 {0} 元</div>", groupDiscount));
                Strings.Append("<div>请于订单确认后24小时之内，通过网上支付方式付清全部费用！</div>");
                Strings.Append("</div>");

                Strings.Append("<select id=\"Pre2\" class=\"form-control Branch\" style=\"display:none\">");
                Strings.Append(PurchaseClass.GetBranch(1, "PayOption"));
                Strings.Append("</select>");
                Strings.Append("");
                Strings.Append("</div></div>");
                //付款方式 end

                Strings.Append("");
                Strings.Append("</div>"); //product_wrap end

                Strings.Append("<div class=\"pre-footer order-footer\"  style=\"position: fixed; bottom: -1px; left: 0px;width:101%\">");
                Strings.Append("<div class=\"container\">");
                Strings.Append("<div class=\"row\">");
                Strings.Append("<div class=\"col-xs-8\" style=\"text-align:left;\">");
                if (Convert.ToString(HttpContext.Current.Session["Online_UserId"]) == "")
                {
                    Strings.Append("<a class=\"yd cur\" href=\"javascript:;\" id=\"loginnow\"><i class=\"fa fa-user\"></i> 注册或登录</a>");
                }
                Strings.Append("</div>");

                Strings.Append("<div class=\"col-xs-4\" style=\"text-align:center\"><a class=\"yd cur\" href=\"javascript:;\" id=\"submitorder\"><i class=\"fa fa-chevron-circle-right\"></i> 下一步</a></div>");
                Strings.Append("</div></div></div>");

                Strings.Append("</div>"); //sub_view end

                return Strings.ToString();
            }
            else
            {
                return "<div class=\"sub_view info\" id=\"second_view\">订单不存在，不能继续预订！</div>";
            }

        }

        #region OrderSecondStep_New
        public static object OrderSecondStep_New(string orderid)
        {
            JSONObject ObJson = new JSONObject();
            //(select wwwyh from OL_Line where MisLineId=OL_TempOrder.lineid) as wwwyh
            string sql = string.Format("select *,(select sum(SumPrice) from OL_OrderPrice where orderid='{0}') as SumPrice from OL_TempOrder where OrderFlag='0' and OrderId='{0}'", orderid);
            DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                JSONArray ArrJson = new JSONArray();
                dt.Columns.Add(new DataColumn("PriceFlag", typeof(bool)));
                dt.Rows[0]["PriceFlag"] = false;
                if (MyConvert.ConToDec(dt.Rows[0]["Price"].ToString()) != MyConvert.ConToDec(dt.Rows[0]["SumPrice"].ToString()))
                {
                    dt.Rows[0]["PriceFlag"] = true;
                }
                StringBuilder Strings = new StringBuilder();
                int wwwyh = MyConvert.ConToInt(dt.Rows[0]["wwwyh"].ToString());
                int preferAmount = 0;

                sql = string.Format("select preferAmount from OL_Preferential where Lineid='{0}' and startDate<='{1}' and endDate>='{1}' and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate())", dt.Rows[0]["LineId"].ToString(), Convert.ToDateTime(dt.Rows[0]["BeginDate"].ToString()));
                DataTable dt1 = MyDataBaseComm.getDataSet(sql).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    preferAmount = MyConvert.ConToInt(dt1.Rows[0]["preferAmount"].ToString());
                    ObJson.Add("preferAmount", preferAmount);
                }
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
                ObJson.Add("PayOption", PurchaseClass.GetBranch(1, "PayOption"));
                ObJson.Add("Online_UserName", Convert.ToString(HttpContext.Current.Session["Online_UserName"]));
                ObJson.Add("Online_WeChatPhone", Convert.ToString(HttpContext.Current.Session["Online_WeChatPhone"]));
                ObJson.Add("Online_WeChatEmail", Convert.ToString(HttpContext.Current.Session["Online_WeChatEmail"]));
                ObJson.Add("Online_UserId", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
            }
            else
            {
                ObJson.Add("rows", null);
            }
            return json.SerializeObject(ObJson);
        }
        #endregion

        public static string OrderThirdStep(string orderid)
        {
            string SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string AdjustTime = "24";
                //InLand  ProductType
                if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "InLand")
                {
                    AdjustTime = "12";
                }
                //return DS.Tables[0].Rows[0]["autoid"].ToString();
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=\"sub_view\" id=\"third_view\">");
                Strings.Append("<div class=\"recommend_wrap\">");

                //预订成功 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1") Strings.Append("<h3>预订成功（已占位）</h3>");
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "0") Strings.Append("<h3>预订成功（待确认）</h3>");
                Strings.Append("<div style=\"font-size: 14px;line-height:30px\">");
                //        
                Strings.Append(string.Format("您的订单号：<span class=\"orderid\">{0}</span><br/>", DS.Tables[0].Rows[0]["autoid"].ToString()));
                Strings.Append("我们的工作人员将会尽快通过电话或者EMAIL和您联系。如果您所填写的信息有误，此订单将被取消。<br/>");
                //Strings.Append(string.Format("即时占位订单，请您在预订后<span class=\"orderid\">{0}</span>小时以内付款；非占位订单，请您在订单确认以后<span class=\"orderid\">{0}</span>小时内付款（紧急情况或特殊情况除外）。<br/>", AdjustTime));

                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1") Strings.Append(string.Format("请您在<span class=\"orderid\">预订后{0}小时</span>以内付款（紧急情况或特殊情况除外）。<br/>", AdjustTime));
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "0") Strings.Append(string.Format("请您在<span class=\"orderid\">订单确认以后{0}小时</span>以内付款（紧急情况或特殊情况除外）。<br/>", AdjustTime));

                Strings.Append("如果因特殊原因无法成行，我们也会及时通知您。");
                Strings.Append("</div>");
                Strings.Append("</div></div>");
                //预订成功 end

                //付款方式 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>付款方式</h3>");
                Strings.Append("<div style=\"font-size: 14px;line-height:30px\">");
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1" && DS.Tables[0].Rows[0]["PayFlag"].ToString() == "0")
                {
                    //Strings.Append("");
                    Strings.Append(string.Format("<A class=\"btn yellow\" href=\"/wechat/pay.aspx?OrderId={0}\" target=\"_blank\">在线支付</A>", orderid));
                }
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2")
                {
                    Strings.Append("<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchName") + "</DIV>");
                    Strings.Append("<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchMap") + "</DIV>");
                }
                Strings.Append("</div>");
                Strings.Append("</div></div>");
                //付款方式 end

                //联系方式 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>联系方式</h3>");

                Strings.Append("<ul>");
                Strings.Append("<li>电话：<a class='margin-right-20' href='tel:02164747595'>021-64747595、64747596</a></li>");
                //Strings.Append("<li>传真：021-64742928(出境)&nbsp;&nbsp;021-64670982(国内)</li>");
                Strings.Append("<li>上海中国青年旅行社有限公司</li>");
                Strings.Append("<li>联系地址：上海市徐汇区衡山路2号（200031）</li>");
                Strings.Append("<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>");
                Strings.Append("</ul>");

                Strings.Append("</div></div>");
                //联系方式 end

                Strings.Append("");
                Strings.Append("</div>"); //product_wrap end

                Strings.Append("<div class=\"pre-footer order-footer\"  style=\"position: fixed; bottom: -1px; left: 0px;width:101%\">");
                Strings.Append("<div class=\"container\">");
                Strings.Append("<div class=\"row\">");
                Strings.Append("<div class=\"col-xs-7\" style=\"text-align:left;\"></div>");
                Strings.Append("<div class=\"col-xs-5\" style=\"text-align:center\"><a class=\"yd cur\" href=\"app/main\"><i class=\"fa fa-home\"></i> 返回首页</a></div>");
                Strings.Append("</div></div></div>");

                Strings.Append("</div>"); //sub_view end

                return Strings.ToString();
            }
            else
            {
                return "<div class=\"sub_view info\" id=\"third_view\">订单不存在，不能继续预订！</div>";
            }
        }

        #region OrderThirdStep_New
        public static object OrderThirdStep_New(string orderid)
        {
            JSONObject ObJson = new JSONObject();
            string SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", orderid);
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                JSONArray ArrJson = new JSONArray();
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
                ObJson.Add("orderid", orderid);
                ObJson.Add("BranchName", PurchaseClass.GetBranch(MyConvert.ConToInt(dt.Rows[0]["BranchId"].ToString()), "BranchName"));
                ObJson.Add("BranchMap", PurchaseClass.GetBranch(MyConvert.ConToInt(dt.Rows[0]["BranchId"].ToString()), "BranchMap"));
            }
            else
            {
                ObJson.Add("rows", null);
            }
            return json.SerializeObject(ObJson);
        }
        #endregion

        public static string OrderDetail(string orderid)
        {
            string SqlQueryText = string.Format("select *,(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=\"sub_view\" id=\"orderdetail_view\">");
                Strings.Append("<div class=\"recommend_wrap\">");

                //订单详情 begin
                string Pics = "";
                string nums = "";
                string pay = "";
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Childs"].ToString()) > 0)
                {
                    nums = string.Format("{0}成人 {1}儿童", DS.Tables[0].Rows[0]["Adults"].ToString(), DS.Tables[0].Rows[0]["Childs"].ToString());
                }
                else
                {
                    nums = string.Format("{0}成人", DS.Tables[0].Rows[0]["Adults"].ToString());
                }
                switch (DS.Tables[0].Rows[0]["OrderFlag"].ToString())
                {
                    case "0":
                        Pics = "待确认";
                        break;
                    case "1":
                        Pics = "占位";
                        break;
                    case "2":
                        Pics = "确认";
                        break;
                    case "3":
                        Pics = "完成";
                        break;
                    case "8":
                        Pics = "取消";
                        break;
                    default:
                        break;
                }
                pay = DS.Tables[0].Rows[0]["pay"].ToString();
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append(string.Format("<h3>订单号：{0}</h3>", DS.Tables[0].Rows[0]["AutoId"].ToString()));
                Strings.Append(string.Format("<div>线路：{0}</div>", DS.Tables[0].Rows[0]["LineName"].ToString()));
                Strings.Append(string.Format("<div>日期：{0:yyyy-MM-dd}</div>", DS.Tables[0].Rows[0]["BeginDate"]));
                Strings.Append(string.Format("<div>人数：{0}</div>", nums));
                Strings.Append(string.Format("<div>费用：<span class=\"o-price\">¥{0}</span></div>", DS.Tables[0].Rows[0]["Price"].ToString()));
                Strings.Append(string.Format("<div>已付：{0}</div>", pay));
                Strings.Append(string.Format("<div>状态：{0}</div>", Pics));
                Strings.Append("</div></div>");
                //订单详情 end

                //游客信息begin
                string GuestList = PurchaseClass.GetOrderGuest(orderid);
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>联系人信息</h3>");
                Strings.Append(string.Format("<h3>姓名：{0}</h3>", DS.Tables[0].Rows[0]["OrderName"].ToString()));
                if (DS.Tables[0].Rows[0]["OrderTel"].ToString().Length > 0) Strings.Append(string.Format("<h3>电话：{0}</h3>", DS.Tables[0].Rows[0]["OrderTel"].ToString()));
                if (DS.Tables[0].Rows[0]["OrderMobile"].ToString().Length > 0) Strings.Append(string.Format("<h3>手机：{0}</h3>", DS.Tables[0].Rows[0]["OrderMobile"].ToString()));
                if (DS.Tables[0].Rows[0]["OrderFax"].ToString().Length > 0) Strings.Append(string.Format("<h3>传真：{0}</h3>", DS.Tables[0].Rows[0]["OrderFax"].ToString()));
                if (DS.Tables[0].Rows[0]["OrderEmail"].ToString().Length > 0) Strings.Append(string.Format("<h3>邮箱：{0}</h3>", DS.Tables[0].Rows[0]["OrderEmail"].ToString()));
                Strings.Append(string.Format("<span style = \"color: #e84d1c\">客服会联系您确认游客信息</span>"));
                Strings.Append("</div></div>");

                //费用清单 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>费用清单</h3>");

                SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}'", orderid);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("");
                    for (int i = 0; i <= DS1.Tables[0].Rows.Count - 1; i++)
                    {
                        Strings.Append("<div class=\"col-price\">");
                        Strings.Append(string.Format("<div><span class=\"pricename\">{0} ({1} × {2})</span><span class=\"sprice\"><i class=\"fa fa-cny\"></i> {3}</span></div>", DS1.Tables[0].Rows[i]["PriceName"].ToString(), DS1.Tables[0].Rows[i]["SellPrice"].ToString().Replace(".00", ""), DS1.Tables[0].Rows[i]["OrderNums"].ToString(), DS1.Tables[0].Rows[i]["SumPrice"].ToString().Replace(".00", "")));
                        Strings.Append(string.Format("<div class=\"pricememo\">{0}</div>", DS1.Tables[0].Rows[i]["PriceMemo"].ToString()));
                        Strings.Append("</div>");
                    }
                    string sql = string.Format("select top 1 couponAmount from OL_Order where OrderId='{0}'", orderid);
                    DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string couponAmount = dt.Rows[0]["couponAmount"].ToString();
                        if (!string.IsNullOrEmpty(couponAmount) && Convert.ToDecimal(couponAmount) > 0)
                        {
                            Strings.Append("<div class=\"col-price\">");
                            Strings.Append(string.Format("<div><span class=\"pricename\">积分抵扣 ({0} / {1})</span><span class=\"sprice\"><i class=\"fa fa-cny\"></i> -{2}</span></div>", Convert.ToInt32(couponAmount.Replace(".00", "")) * Convert.ToInt32(ConfigurationManager.AppSettings["Integral_ratio"]), ConfigurationManager.AppSettings["Integral_ratio"], couponAmount.Replace(".00", "")));
                            Strings.Append(string.Format("<div class=\"pricememo\">{0}</div>", ConfigurationManager.AppSettings["Integral_ratio"] + "积分=1元"));
                            Strings.Append("</div>");
                        }
                    }
                }
                Strings.Append("</div></div>");
                //费用清单 end

                //付款方式 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>付款方式</h3>");
                Strings.Append("<div style=\"font-size: 14px;line-height:30px\">");
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "1" && DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "1" && DS.Tables[0].Rows[0]["PayFlag"].ToString() == "0")
                {
                    //Strings.Append("");
                    Strings.Append(string.Format("<A class=\"btn yellow\" href=\"/wechat/pay.aspx?OrderId={0}\" target=\"_blank\">在线支付</A>", orderid));
                }
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2")
                {
                    Strings.Append("<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchName") + "</DIV>");
                    Strings.Append("<div class=\"infos\">" + PurchaseClass.GetBranch(MyConvert.ConToInt(DS.Tables[0].Rows[0]["BranchId"].ToString()), "BranchMap") + "</DIV>");
                }
                Strings.Append("</div>");
                Strings.Append("</div></div>");
                //付款方式 end

                //联系方式 begin
                Strings.Append("<div class=\"recommend_detail\">");
                Strings.Append("<div class=\"recommend_txt\">");
                Strings.Append("<h3>联系方式</h3>");

                Strings.Append("<ul>");
                Strings.Append("<li>电话：<a class=\"margin-right-20\" href=\"tel:02164747596\">021-64747596</a></li>");
                Strings.Append("<li>传真：021-64742928(出境)&nbsp;&nbsp;021-64670982(国内)</li>");
                Strings.Append("<li>上海中国青年旅行社有限公司</li>");
                Strings.Append("<li>联系地址：上海市徐汇区衡山路2号（200031）</li>");
                Strings.Append("<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>");
                Strings.Append("</ul>");

                Strings.Append("</div></div>");
                //联系方式 end

                Strings.Append("");
                Strings.Append("</div>"); //product_wrap end

                //Strings.Append("<div class=\"pre-footer order-footer\"  style=\"position: fixed; bottom: -1px; left: 0px;width:101%\">");
                //Strings.Append("<div class=\"container\">");
                //Strings.Append("<div class=\"row\">");
                //Strings.Append("<div class=\"col-xs-7\" style=\"text-align:left;\"></div>");
                //Strings.Append("<div class=\"col-xs-5\" style=\"text-align:center\"><a class=\"yd cur\" href=\"app/main\"><i class=\"fa fa-home\"></i> 返回首页</a></div>");
                //Strings.Append("</div></div></div>");

                Strings.Append("</div>"); //sub_view end

                return Strings.ToString();
            }
            else
            {
                return "<div class=\"sub_view info\" id=\"third_view\">订单不存在，不能继续预订！</div>";
            }
        }

        #region OrderDetail_New
        public static object OrderDetail_New(string orderid)
        {
            JSONObject ObJson = new JSONObject();
            //(select yfk from ol_line where MisLineId=OL_Order.LineID) as yfk,
            string sql = string.Format("select *,(select ISNULL(sum(PayPrice),0) from OL_PayMent where OrderId=OL_Order.OrderId) as pay from OL_Order where OrderId='{0}'", orderid);
            DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                JSONArray ArrJson = new JSONArray();
                dt.Columns.Add(new DataColumn("nums", typeof(string)));
                dt.Columns.Add(new DataColumn("Pics", typeof(string)));
                dt.Columns.Add(new DataColumn("date", typeof(string)));
                if (MyConvert.ConToInt(dt.Rows[0]["Childs"].ToString()) > 0)
                {
                    dt.Rows[0]["nums"] = string.Format("{0}成人 {1}儿童", dt.Rows[0]["Adults"].ToString(), dt.Rows[0]["Childs"].ToString());
                }
                else
                {
                    dt.Rows[0]["nums"] = string.Format("{0}成人", dt.Rows[0]["Adults"].ToString());
                }
                switch (dt.Rows[0]["OrderFlag"].ToString())
                {
                    case "0":
                        dt.Rows[0]["Pics"] = "待确认";
                        break;
                    case "1":
                        dt.Rows[0]["Pics"] = "占位";
                        break;
                    case "2":
                        dt.Rows[0]["Pics"] = "确认";
                        break;
                    case "3":
                        dt.Rows[0]["Pics"] = "完成";
                        break;
                    case "8":
                        dt.Rows[0]["Pics"] = "取消";
                        break;
                    default:
                        break;
                }
                string sql1 = string.Format("select top 1 couponAmount from OL_Order where OrderId='{0}'", orderid);
                DataTable dt2 = MyDataBaseComm.getDataSet(sql1).Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    string couponAmount = dt2.Rows[0]["couponAmount"].ToString();
                    if (!string.IsNullOrEmpty(couponAmount) && Convert.ToDecimal(couponAmount) > 0)
                    {
                        ObJson.Add("couponAmount", couponAmount.Replace(".00", ""));
                        ObJson.Add("Integral_ratio", ConfigurationManager.AppSettings["Integral_ratio"]);
                    }
                }
                dt.Rows[0]["date"] = string.Format("{0:yyyy-MM-dd}", dt.Rows[0]["BeginDate"]);
                string GuestList = PurchaseClass.GetOrderGuest(orderid);
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
                ObJson.Add("orderid", orderid);
                JSONArray ArrJson1 = new JSONArray();
                sql = string.Format("select * from OL_OrderPrice where OrderId='{0}'", orderid);
                DataTable dt1 = MyDataBaseComm.getDataSet(sql).Tables[0];
                ArrJson1 = Data.GetJsonList(dt1);
                ObJson.Add("row1", ArrJson1);
                ObJson.Add("BranchName", PurchaseClass.GetBranch(MyConvert.ConToInt(dt.Rows[0]["BranchId"].ToString()), "BranchName"));
                ObJson.Add("BranchMap", PurchaseClass.GetBranch(MyConvert.ConToInt(dt.Rows[0]["BranchId"].ToString()), "BranchMap"));
            }
            return json.SerializeObject(ObJson);
        }
        #endregion

        public static string OrderList(int pages)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("OrderUser='{0}' and ", Convert.ToString(HttpContext.Current.Session["Online_UserId"])));

            string fieldlist = "*";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "AutoId";
            string sortflag = "";
            string sortname = "AutoId desc";
            string tablename = "OL_Order";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            string SqlQueryText = "", ListResult = "非常抱歉，没有找到您的订单";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = CreateLineListString(SqlQueryText);
            }
            if (pages == 1)
            {
                liststring.Append("<div class=\\\"sub_view\\\" id=\\\"orderlist_view\\\">");
                liststring.Append("<div class=\\\"recommend_wrap\\\"  id=\\\"orderlist\\\">");
                liststring.Append(ListResult);
                liststring.Append("</div><div class=\\\"text-center\\\" style=\\\"margin-bottom: 50px;margin-top: -10px;\\\">");
                liststring.Append("<a style=\\\"display:none;width:150px;\\\" id=\\\"serch-next\\\" href=\\\"javascript:;\\\" class=\\\"btn btn-default\\\"></a>");
                liststring.Append("</div></div>");
            }
            else
            {
                liststring.Append(ListResult);
            }


            Strings.Clear();
            Strings.Append("({");
            Strings.Append(string.Format("\"success\":0,\"pages\":{0},\"pagecount\":{1},\"content\":\"{2}\"", pages + 1, PageCount, liststring.ToString()));
            Strings.Append("})");
            return Strings.ToString();
            //return "<div class=\"sub_view info\" id=\"orderlist_view\">功能开发中</div>";
        }

        #region OrderList_New
        public static string OrderList_New(int pages)
        {
            JSONObject ObJson = new JSONObject();
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("OrderUser='{0}' and ", Convert.ToString(HttpContext.Current.Session["Online_UserId"])));
            string fieldlist = "*";
            Strings.Append("1=1 ");
            string condition = Strings.ToString();
            string pkey = "AutoId";
            string sortflag = "";
            string sortname = "AutoId desc";
            string tablename = "OL_Order";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            ObJson.Add("rowcount", rowcount);
            ObJson.Add("pages", pages);
            ObJson.Add("PageCount", PageCount);
            ObJson.Add("Fx_UserId", Convert.ToString(HttpContext.Current.Session["Fx_UserId"]));
            ObJson.Add("Fx_Login", Convert.ToString(HttpContext.Current.Session["Fx_Login"]));
            string SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
            var query = CreateLineListString_New(SqlQueryText);
            JSONArray ArrJson = new JSONArray();
            ArrJson = Data.GetJsonList(query);
            ObJson.Add("rows", ArrJson);
            return json.SerializeObject(ObJson);
        }
        #endregion
        #region CreateLineListString_New
        public static DataTable CreateLineListString_New(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("nums", typeof(string)));
                dt.Columns.Add(new DataColumn("PhotoPath", typeof(string)));
                dt.Columns.Add(new DataColumn("date", typeof(string)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (MyConvert.ConToInt(dt.Rows[i]["Childs"].ToString()) > 0)
                    {
                        dt.Rows[i]["nums"] = string.Format("（{0}成人 {1}儿童）", dt.Rows[i]["Adults"].ToString(), dt.Rows[i]["Childs"].ToString());
                    }
                    else
                    {
                        dt.Rows[i]["nums"] = string.Format("（{0}成人）", dt.Rows[i]["Adults"].ToString());
                    }
                    switch (dt.Rows[i]["OrderFlag"].ToString())
                    {
                        case "0":
                            dt.Rows[i]["PhotoPath"] = "待确认";
                            break;
                        case "1":
                            dt.Rows[i]["PhotoPath"] = "占位";
                            break;
                        case "2":
                            dt.Rows[i]["PhotoPath"] = "确认";
                            break;
                        case "3":
                            dt.Rows[i]["PhotoPath"] = "完成";
                            break;
                        case "8":
                            dt.Rows[i]["PhotoPath"] = "取消";
                            break;
                        default:
                            break;
                    }
                    dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                    dt.Rows[i]["date"] = string.Format("{0:yyyy-MM-dd}", dt.Rows[i]["BeginDate"]).ToString();
                }
                return dt;
            }
            return new DataTable();
        }
        #endregion
        public static string CreateLineListString(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string Pics = "";
                string nums = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["Childs"].ToString()) > 0)
                    {
                        nums = string.Format("（{0}成人 {1}儿童）", DS.Tables[0].Rows[i]["Adults"].ToString(), DS.Tables[0].Rows[i]["Childs"].ToString());
                    }
                    else
                    {
                        nums = string.Format("（{0}成人）", DS.Tables[0].Rows[i]["Adults"].ToString());
                    }
                    switch (DS.Tables[0].Rows[i]["OrderFlag"].ToString())
                    {
                        case "0":
                            Pics = "待确认";
                            break;
                        case "1":
                            Pics = "占位";
                            break;
                        case "2":
                            Pics = "确认";
                            break;
                        case "3":
                            Pics = "完成";
                            break;
                        case "8":
                            Pics = "取消";
                            break;
                        default:
                            break;
                    }

                    Strings.Append("<div class=\\\"recommend_detail\\\">");
                    Strings.Append("<div class=\\\"recommend_txt\\\">");
                    Strings.Append(string.Format("<h3>订单号：{0}{1}</h3>", DS.Tables[0].Rows[i]["AutoId"].ToString(), nums));
                    Strings.Append(string.Format("<div>线路：{0}</div>", DS.Tables[0].Rows[i]["LineName"].ToString()));
                    Strings.Append(string.Format("<div>日期：{0:yyyy-MM-dd}</div>", DS.Tables[0].Rows[i]["BeginDate"]));
                    Strings.Append(string.Format("<div>费用：<span class=\\\"o-price\\\">¥{0}</span></div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    Strings.Append(string.Format("<div>状态：{0}</div>", Pics));
                    if (Convert.ToString(HttpContext.Current.Session["Fx_UserId"]).Length > 0 && Convert.ToString(HttpContext.Current.Session["Fx_Login"]).Length > 0)
                    {
                        Strings.Append(string.Format("<div>下单人：{0}</div>", DS.Tables[0].Rows[i]["OrderName"].ToString()));
                        Strings.Append(string.Format("<div>联系电话：<a href='tel:{0}'>{0}</a></div>", DS.Tables[0].Rows[i]["OrderMobile"].ToString()));
                    }
                    Strings.Append(string.Format("<a id=\\\"btnOrderDetail\\\" class=\\\"btn yellow\\\" href=\\\"javascript:;\\\" tag=\\\"{0}\\\">查看订单</a>", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    if (Convert.ToInt32(DS.Tables[0].Rows[i]["PayFlag"]) == 0 && DS.Tables[0].Rows[i]["OrderFlag"].ToString().Contains("1"))
                    {
                        Strings.Append(string.Format("&nbsp;&nbsp;<a id=\\\"btnCancelOrder\\\" class=\\\"btn yellow\\\" href=\\\"javascript:CancelOrder('{0}');\\\">订单取消</a>", DS.Tables[0].Rows[i]["OrderId"].ToString()));
                    }
                    Strings.Append("</div></div>");
                }
            }
            return Strings.ToString();
        }


        public static string CouponList(int pages)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("userid='{0}' and ", Convert.ToString(HttpContext.Current.Session["Online_UserId"])));

            string fieldlist = "*,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "Id";
            string sortflag = "";
            string sortname = "inputdate desc";
            string tablename = "Pre_Ticket";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            string SqlQueryText = "", ListResult = "非常抱歉，没有找到您的优惠券";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = CreateCouponListString(SqlQueryText);
            }
            if (pages == 1)
            {
                liststring.Append("<div class=\\\"sub_view\\\" id=\\\"coupon_view\\\">");
                liststring.Append("<div class=\\\"recommend_wrap\\\"  id=\\\"orderlist\\\">");
                liststring.Append(ListResult);
                liststring.Append("</div><div class=\\\"text-center\\\" style=\\\"margin-bottom: 50px;margin-top: -10px;\\\">");
                liststring.Append("<a style=\\\"display:none;width:150px;\\\" id=\\\"serch-next\\\" href=\\\"javascript:;\\\" class=\\\"btn btn-default\\\"></a>");
                liststring.Append("</div></div>");
            }
            else
            {
                liststring.Append(ListResult);
            }


            Strings.Clear();
            Strings.Append("({");
            Strings.Append(string.Format("\"success\":0,\"pages\":{0},\"pagecount\":{1},\"content\":\"{2}\"", pages + 1, PageCount, liststring.ToString()));
            Strings.Append("})");
            return Strings.ToString();
            //return "<div class=\"sub_view info\" id=\"orderlist_view\">功能开发中</div>";
        }

        #region CouponList_New
        public static object CouponList_New(int pages)
        {
            JSONObject ObJson = new JSONObject();
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("userid='{0}' and ", Convert.ToString(HttpContext.Current.Session["Online_UserId"])));

            string fieldlist = "*,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo";
            Strings.Append("1=1 ");
            string condition = Strings.ToString();
            string pkey = "Id";
            string sortflag = "";
            string sortname = "inputdate desc";
            string tablename = "Pre_Ticket";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            ObJson.Add("rowcount", rowcount);
            ObJson.Add("pages", pages);
            ObJson.Add("PageCount", PageCount);
            string SqlQueryText = "";
            JSONArray ArrJson = new JSONArray();
            SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
            var query = CreateCouponListString_New(SqlQueryText);
            ArrJson = Data.GetJsonList(query);
            ObJson.Add("rows", ArrJson);
            return json.SerializeObject(ObJson);
        }
        #endregion

        #region CreateCouponListString_New
        public static DataTable CreateCouponListString_New(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("PhotoPath", typeof(string)));
                dt.Columns.Add(new DataColumn("bDate", typeof(string)));
                dt.Columns.Add(new DataColumn("eDate", typeof(string)));
                string PreDate;
                PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["flag"].ToString())
                    {
                        case "0":
                            dt.Rows[i]["PhotoPath"] = "<font color=blue>未使用</font>";
                            if (Convert.ToDateTime(dt.Rows[i]["enddate"].ToString()) < DateTime.Today)
                            {
                                dt.Rows[i]["PhotoPath"] = "<font color=red>已过期</font>";
                            }
                            break;
                        case "1":
                            dt.Rows[i]["PhotoPath"] = "<font color=green>已使用</font>";
                            break;
                        default:
                            break;
                    }
                    dt.Rows[i]["bDate"] = string.Format("{0:yyyy-MM-dd}", dt.Rows[i]["BeginDate"]);
                    dt.Rows[i]["eDate"] = string.Format("{0:yyyy-MM-dd}", dt.Rows[i]["enddate"]);
                }
                return dt;
            }
            return new DataTable();
        }
        #endregion

        public static string CreateCouponListString(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string Pics = "";
                string PreDate;
                PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    switch (DS.Tables[0].Rows[i]["flag"].ToString())
                    {
                        case "0":
                            Pics = "<font color=blue>未使用</font>";
                            if (Convert.ToDateTime(DS.Tables[0].Rows[i]["enddate"].ToString()) < DateTime.Today)
                            {
                                Pics = "<font color=red>已过期</font>";
                            }
                            break;
                        case "1":
                            Pics = "<font color=green>已使用</font>";
                            break;
                        default:
                            break;
                    }

                    Strings.Append("<div class=\\\"recommend_detail\\\">");
                    Strings.Append("<div class=\\\"recommend_txt\\\">");
                    Strings.Append(string.Format("<h3>{1}：{0}</h3>", DS.Tables[0].Rows[i]["uno"].ToString(), Pics));
                    Strings.Append(string.Format("<div>有效日期：{0:yyyy-MM-dd} 至 {1:yyyy-MM-dd}</div>", DS.Tables[0].Rows[i]["BeginDate"], DS.Tables[0].Rows[i]["enddate"]));
                    Strings.Append(string.Format("<div>抵用金额：<span class=\\\"o-price\\\">¥{0}</span></div>", DS.Tables[0].Rows[i]["par"].ToString()));
                    Strings.Append(string.Format("<div>使用说明：{0}</div>", DS.Tables[0].Rows[i]["memo"].ToString()));
                    Strings.Append("</div></div>");
                }
            }
            return Strings.ToString();
        }

        public static string OrderPrice(string orderid, string allprice, string PriceStrings)
        {
            string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", orderid));
                Sql.Add(string.Format("UPDATE OL_TempOrder set Price='{0}',PayType='{1}',BranchId='{2}',rebate='{3}' where OrderId='{4}'", MyConvert.ConToDec(allprice), "1", "0", "0", orderid));


                string[] AllInfo = Regex.Split(PriceStrings.Trim(), @"\|\|", RegexOptions.IgnoreCase);
                string PriceSql;

                if (AllInfo.Length > 0)
                {
                    for (int i = 0; i < AllInfo.Length; i++)
                    {
                        string[] PriceInfo = Regex.Split(AllInfo[i], @"\@\@", RegexOptions.IgnoreCase);  //AllInfo[i].Split("@@".ToArray());
                        if (PriceInfo.Contains("undefined"))
                        {
                            continue;
                        }
                        if (PriceInfo.Length > 0)
                        {
                            PriceSql = string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                    orderid,
                                    PriceInfo[0],
                                    PriceInfo[1],
                                    PriceInfo[2],
                                    PriceInfo[3],
                                    PriceInfo[4],
                                    PriceInfo[5],
                                    PriceInfo[6],
                                    DateTime.Now.ToString()
                            );
                            Sql.Add(PriceSql);
                        }
                    }

                    string[] SqlQuery = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuery) == true)
                    {
                        return "{\"success\":\"OK\"}";
                    }
                    else
                    {
                        return "{\"error\":\"报名失败\"}";
                    }
                }
                else
                {
                    return "{\"error\":\"报名失败\"}";
                }
            }
            else
            {
                return "{\"error\":\"报名失败\"}";
            }
        }

        public static string OrderSubmit(string orderid, string paytype, string integral, string oname, string ophone, string oemail, string omemo)
        {
            string SqlQueryText = string.Format("select *,(select wwwyh from OL_Line where MisLineId=OL_TempOrder.lineid) as wwwyh from OL_TempOrder where OrderFlag='0' and OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {

                List<string> Sql = new List<string>();
                //载入用户
                string User_Name = "临时用户", User_Id = "cc68b789-0d20-4ab5-a07b-9f4c002b7b96";
                if (Convert.ToString(HttpContext.Current.Session["Online_UserId"]) != "")
                {
                    User_Id = Convert.ToString(HttpContext.Current.Session["Online_UserId"]);
                    User_Name = Convert.ToString(HttpContext.Current.Session["Online_UserName"]);

                    //新用户保存联系方式
                    //Sql.Add(string.Format("UPDATE OL_LoginUser set UserName='{1}',Mobile='{2}',Tel='{3}' where id='{0}'",
                    //    User_Id,
                    //    oname,
                    //    ophone,
                    //    OrderInfo[2]
                    //    )
                    //);
                }

                int Nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["OrderNums"].ToString());
                int wwwyh = MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString());
                string AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                int SumPre_Price = 0;
                int preferAmount = 0;
                int SumPreferAmount = 0;
                decimal groupDiscount = 0;
                decimal SumGroupdiscount = 0;

                SqlQueryText = string.Format("select preferAmount from OL_Preferential where Lineid='{0}' and startDate<='{1}' and endDate>='{1}' and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate())", DS.Tables[0].Rows[0]["LineId"].ToString(), Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"].ToString()));
                SaveErrorToLog("OL_Preferential sql:", SqlQueryText);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    preferAmount = MyConvert.ConToInt(DS1.Tables[0].Rows[0]["preferAmount"].ToString());
                    SaveErrorToLog("OL_Preferential amount:", preferAmount.ToString());
                }

                //groupDiscount = MyConvert.ConToDec(MyDataBaseComm.getScalar(string.Format("select discount from ol_groupplan where MisLineId='{0}' and GroupDate='{1:yyyy-MM-dd}'", DS.Tables[0].Rows[0]["LineId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"])));

                string PayType, BranchId, Pre_Price;
                PayType = "2";
                BranchId = "0";
                Pre_Price = "0";

                string[] PayInfo = Regex.Split(paytype, @"\@", RegexOptions.IgnoreCase);
                PayType = PayInfo[0];
                if (PayType == "2")
                {
                    BranchId = PayInfo[1];
                }
                else
                {
                    Pre_Price = PayInfo[1];
                    SumPre_Price = Nums * wwwyh;
                    SumPreferAmount = Nums * preferAmount;
                    SumGroupdiscount = Nums * groupDiscount;
                }

                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

                OrderInfos Sorder = new OrderInfos();
                Sorder.adult = DS.Tables[0].Rows[0]["Adults"].ToString();
                Sorder.begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]);
                Sorder.childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Sorder.days = DS.Tables[0].Rows[0]["LineDays"].ToString();
                Sorder.deptid = DS.Tables[0].Rows[0]["DeptId"].ToString();
                Sorder.email = DS.Tables[0].Rows[0]["OrderEmail"].ToString();
                Sorder.gathering = DS.Tables[0].Rows[0]["Price"].ToString();
                Sorder.infoid = DS.Tables[0].Rows[0]["ProductClass"].ToString();
                Sorder.lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                Sorder.linename = DS.Tables[0].Rows[0]["LineName"].ToString();
                Sorder.mobile = ophone;
                Sorder.orderdate = DateTime.Now.ToString(); //DS.Tables[0].Rows[0]["OrderTime"].ToString();
                Sorder.orderflag = DS.Tables[0].Rows[0]["OrderFlag"].ToString();
                Sorder.orderid = DS.Tables[0].Rows[0]["OrderId"].ToString();
                Sorder.ordermemo = omemo;
                Sorder.ordername = oname;
                Sorder.ordernumber = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Sorder.planid = DS.Tables[0].Rows[0]["PlanId"].ToString();
                Sorder.tel = DS.Tables[0].Rows[0]["OrderTel"].ToString();
                Sorder.orderno = DS.Tables[0].Rows[0]["AutoId"].ToString();
                Sorder.contract = DS.Tables[0].Rows[0]["Contract"].ToString();
                Sorder.invoice = DS.Tables[0].Rows[0]["Invoice"].ToString();
                Sorder.SellDept = BranchId;
                Sorder.ordertypes = DS.Tables[0].Rows[0]["ProductType"].ToString();

                Sorder.CruisesFlag = "0";
                Sorder.ccid = "0";
                Decimal gathering = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString());

                if (SumPre_Price > 0)
                {
                    Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            orderid,
                            "Preference",
                            "0",
                            "在线支付优惠",
                            "每人立减" + Pre_Price + "元",
                            Pre_Price,
                            Nums,
                            -SumPre_Price,
                            DateTime.Now.ToString()
                    ));

                    gathering = gathering - MyConvert.ConToDec(SumPre_Price.ToString());
                }

                if (SumPreferAmount > 0)
                {
                    Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            orderid,
                            "Preference",
                            "0",
                            "早定早优惠",
                            "每人立减" + preferAmount + "元",
                            -preferAmount,
                            Nums,
                            -SumPreferAmount,
                            DateTime.Now.ToString()
                    ));

                    gathering = gathering - MyConvert.ConToDec(SumPreferAmount.ToString());
                }

                if (SumGroupdiscount > 0)
                {
                    Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            orderid,
                            "Preference",
                            "0",
                            "拼团优惠",
                            "每人立减" + groupDiscount + "元",
                            groupDiscount,
                            Nums,
                            -SumGroupdiscount,
                            DateTime.Now.ToString()
                    ));

                    gathering = gathering - MyConvert.ConToDec(SumGroupdiscount.ToString());
                }
                Sorder.gathering = Convert.ToString(gathering);

                string OrderFlag = "0";//预订状态，不占位订单和无位置订单为0，畅游占位成功为1，提交错误返回9
                try
                {
                    //OrderFlag = "1";
                    OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
                }
                catch
                {
                    OrderFlag = "9";
                }

                //保存游客信息
                string gueststring;
                for (int i = 0; i < Nums; i++)
                {
                    gueststring = string.Format("insert into OL_GuestInfo (OrderId,GuestName,GuestEnName,Sex,IdType,rankno) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                        orderid,
                        oname,
                        "",
                        "M",
                        "11",
                        i + 1
                    );
                    Sql.Add(gueststring);
                }

                if (OrderFlag == "9")
                {
                    Sql.Add(string.Format("update OL_TempOrder set OrderFlag='9',PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4} where OrderId='{1}'", OrderFlag, orderid, DateTime.Now.ToString(), "0", SumPre_Price));
                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", orderid, DateTime.Now.ToString(), "您暂存了预订单"));
                }
                else
                {
                    Sql.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", orderid));
                    //分销订单插入
                    if (Convert.ToString(HttpContext.Current.Session["Fx_UserId"]).Length > 0)
                    {
                        Sql.Add(string.Format("INSERT INTO OL_FxOrder (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", orderid));
                        Sql.Add(string.Format("UPDATE OL_FxOrder set ccid='0',PayFlag='0',Price=Price-{1},PayType='{2}',BranchId='{3}',OrderName='{4}',OrderMobile='{5}',OrderEmail='{6}',OrderMemo='{7}',UserName='{8}',OrderUser='{9}',OrderTime='{10}',OrderFlag='{11}',FxUserId='{12}' where OrderId='{0}'",
                        orderid,
                        SumPre_Price + SumPreferAmount,
                        PayType,
                        BranchId,
                        oname,
                        ophone,
                        oemail,
                        omemo,
                        User_Name,
                        User_Id,
                        DateTime.Now.ToString(),
                        OrderFlag,
                        HttpContext.Current.Session["Fx_UserId"]
                        ));
                    }
                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", orderid, DateTime.Now.ToString(), "您在微信提交了预订单"));
                    Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", orderid));
                    int i = MyConvert.ConToInt(MyDataBaseComm.getScalar(string.Format("select count(1) from ol_groupplan where MisLineId='{0}' and GroupDate='{1:yyyy-MM-dd}'", DS.Tables[0].Rows[0]["LineId"].ToString(), DS.Tables[0].Rows[0]["BeginDate"])));
                    if (i > 0)
                    {
                        Sql.Add(string.Format("UPDATE OL_Order set ccid='0',PayFlag='0',Price=Price-{1},PayType='{2}',BranchId='{3}',OrderName='{4}',OrderMobile='{5}',OrderEmail='{6}',OrderMemo='{7}',UserName='{8}',OrderUser='{9}',OrderTime='{10}',OrderFlag='{11}',GroupOrder=1 where OrderId='{0}'",
                        orderid,
                        SumPre_Price + SumPreferAmount + SumGroupdiscount,
                        PayType,
                        BranchId,
                        oname,
                        ophone,
                        oemail,
                        omemo,
                        User_Name,
                        User_Id,
                        DateTime.Now.ToString(),
                        OrderFlag
                        ));
                    }
                    else
                    {
                        Sql.Add(string.Format("UPDATE OL_Order set ccid='0',PayFlag='0',Price=Price-{1},PayType='{2}',BranchId='{3}',OrderName='{4}',OrderMobile='{5}',OrderEmail='{6}',OrderMemo='{7}',UserName='{8}',OrderUser='{9}',OrderTime='{10}',OrderFlag='{11}' where OrderId='{0}'",
                        orderid,
                        SumPre_Price + SumPreferAmount,
                        PayType,
                        BranchId,
                        oname,
                        ophone,
                        oemail,
                        omemo,
                        User_Name,
                        User_Id,
                        DateTime.Now.ToString(),
                        OrderFlag
                        ));
                    }
                    

                }

                #region 积分抵扣
                if (!string.IsNullOrEmpty(integral))
                {
                    int integralAmount = Convert.ToInt32(integral) / Convert.ToInt32(ConfigurationManager.AppSettings["Integral_ratio"]);
                    string sql = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
                    string sumIntegral = MyDataBaseComm.getScalar(sql);
                    if (Convert.ToInt32(sumIntegral) < Convert.ToInt32(integral))
                    {
                        return "{\"error\":\"积分不足\"}";
                    }
                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", orderid, DateTime.Now.ToString(), "您使用了" + Convert.ToInt32(integral) + "点积分"));
                    //Sql.Add(string.Format("update OL_Order set couponAmount=isnull(couponAmount,0)+{0} where OrderId='{1}'", (Convert.ToInt32(integral) / 20), orderid));
                    Sql.Add(string.Format("UPDATE OL_Order set ccid='0',PayFlag='0',Price=Price-{1},PayType='{2}',BranchId='{3}',OrderName='{4}',OrderMobile='{5}',OrderEmail='{6}',OrderMemo='{7}',UserName='{8}',OrderUser='{9}',OrderTime='{10}',OrderFlag='{11}',couponAmount=isnull(couponAmount,0)+{1} where OrderId='{0}'",
                        orderid,
                        integralAmount,
                        PayType,
                        BranchId,
                        oname,
                        ophone,
                        oemail,
                        omemo,
                        User_Name,
                        User_Id,
                        DateTime.Now.ToString(),
                        OrderFlag
                    ));
                    Sql.Add(string.Format("INSERT INTO [dbo].[OL_Integral] ([uid],[orderid],[lineid],[integral],[getdate],[flag],[dept],[enddate]) " +
                                                                            "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')"
                                    , User_Id
                                    , orderid
                                    , AutoId
                                    , Convert.ToInt32(integral) * -1
                                    , DateTime.Now.ToString()
                                    , '1'
                                    , '0'
                                    , DateTime.Now.AddYears(2).ToString()));
                }
                #endregion

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    try
                    {
                        CommentInfoService.insertOrderComment(AutoId.ToString());
                    }
                    catch (Exception ex)
                    {
                        SaveErrorToLog("订单：" + AutoId + "初始化点评失败！失败原因：", ex.Message);
                    }
                    if (OrderFlag == "9")
                    {
                        return "{\"success\":\"Save\"}";
                    }
                    else
                    {
                        return "{\"success\":\"OK\"}";
                    }
                }
                else
                {
                    return "{\"error\":\"报名失败\"}";
                }
            }
            else
            {
                return "{\"error\":\"订单不存在\"}";
            }
        }

        private static void SaveErrorToLog(string inErrorlog, string inSQL)
        {
            //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\ErrorLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(DateTime.Now.ToString() + ":");
                writer.WriteLine(inErrorlog);
                writer.WriteLine(inSQL);
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }


        public static String FxOrderList(int pages)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("FxUserId='{0}' and ", Convert.ToString(HttpContext.Current.Session["Fx_UserId"])));

            string fieldlist = "*";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "AutoId";
            string sortflag = "";
            string sortname = "AutoId desc";
            string tablename = "OL_FxOrder";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            string SqlQueryText = "", ListResult = "非常抱歉，没有找到您的订单";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = CreateLineListString(SqlQueryText);
            }
            if (pages == 1)
            {
                liststring.Append("<div class=\\\"sub_view\\\" id=\\\"orderlist_view\\\">");
                liststring.Append("<div class=\\\"recommend_wrap\\\"  id=\\\"orderlist\\\">");
                liststring.Append(ListResult);
                liststring.Append("</div><div class=\\\"text-center\\\" style=\\\"margin-bottom: 50px;margin-top: -10px;\\\">");
                liststring.Append("<a style=\\\"display:none;width:150px;\\\" id=\\\"serch-next\\\" href=\\\"javascript:;\\\" class=\\\"btn btn-default\\\"></a>");
                liststring.Append("</div></div>");
            }
            else
            {
                liststring.Append(ListResult);
            }


            Strings.Clear();
            Strings.Append("({");
            Strings.Append(string.Format("\"success\":0,\"pages\":{0},\"pagecount\":{1},\"content\":\"{2}\"", pages + 1, PageCount, liststring.ToString()));
            Strings.Append("})");
            return Strings.ToString();
        }

        #region FxOrderList_New
        public static String FxOrderList_New(int pages)
        {
            JSONObject ObJson = new JSONObject();
            StringBuilder Strings = new StringBuilder();
            StringBuilder liststring = new StringBuilder();
            Strings.Append(string.Format("FxUserId='{0}' and ", Convert.ToString(HttpContext.Current.Session["Fx_UserId"])));

            string fieldlist = "*";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "AutoId";
            string sortflag = "";
            string sortname = "AutoId desc";
            string tablename = "OL_FxOrder";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));
            ObJson.Add("rowcount", rowcount);
            ObJson.Add("pages", pages);
            ObJson.Add("PageCount", PageCount);
            ObJson.Add("Fx_UserId", Convert.ToString(HttpContext.Current.Session["Fx_UserId"]));
            ObJson.Add("Fx_Login", Convert.ToString(HttpContext.Current.Session["Fx_Login"]));
            string SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
            var query = CreateLineListString_New(SqlQueryText);
            JSONArray ArrJson = new JSONArray();
            ArrJson = Data.GetJsonList(query);
            ObJson.Add("rows", ArrJson);
            return json.SerializeObject(ObJson);
        }
        #endregion

        #region 订单取消
        public static string CancelOrder(string orderid)
        {
            StringBuilder Strings = new StringBuilder();

            string SqlQueryText = string.Format("select * from OL_Order where OrderId= '{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "1")
                {
                    Strings.Append("只有占位订单才可以取消！");
                    return "{\"success\":\"" + Strings.ToString() + "\"}";
                }

                if (DS.Tables[0].Rows[0]["OrderUser"].ToString() != Convert.ToString(HttpContext.Current.Session["Online_UserId"]))
                {
                    Strings.Append("只有您预订的订单才可以取消！");
                    return "{\"success\":\"" + Strings.ToString() + "\"}";
                }
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    string Result = rsp.OnlineOrderAdjust(UpPassWord, DS.Tables[0].Rows[0]["OrderId"].ToString());
                    if (Result == "OK")
                    {
                        List<string> Sql = new List<string>();
                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS.Tables[0].Rows[0]["OrderId"].ToString(), DateTime.Now.ToString(), "您取消了订单！"));
                        Sql.Add(string.Format("update OL_Order set OrderFlag='8' where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        //取消订单时，把对应使用优惠券和积分一起取消
                        Sql.Add(string.Format("update Pre_Ticket set flag=0 where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        Sql.Add(string.Format("delete OL_Integral where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                        string[] SqlQuery = Sql.ToArray();
                        MyDataBaseComm.Transaction(SqlQuery);
                        Strings.Append("OK");
                    }
                }
                catch
                {
                    Strings.Append("订单取消失败，请稍后再试！");
                }
            }
            else
            {
                Strings.Append("订单取消失败，您的订单不存在！");
            }
            return "{\"success\":\"" + Strings.ToString() + "\"}";
        }
        #endregion

        #region 我的积分
        public static string IntegralDetail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"sub_view\" id=\"integral_view\">");
            sb.Append("<div class=\"recommend_wrap\">");
            string sql = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
            string sumIntegral = MyDataBaseComm.getScalar(sql);
            if (!string.IsNullOrEmpty(sumIntegral))
            {
                sb.Append(string.Format("<div class=\"price_box\" style=\"font-size: 16px;\"><span class=\"allprice price\">当前可用积分为：{0}</span></div>", sumIntegral));
            }
            string SqlQueryText = string.Format("select a.AutoId,a.LineName,a.BeginDate,a.Adults,a.Childs,a.Price,a.CouponAmount,b.integral from OL_Order as a inner join OL_Integral as b on a.OrderId=b.orderid where OrderUser='{0}' and CouponAmount is not null", HttpContext.Current.Session["Online_UserId"]);
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string nums = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["Childs"]) > 0)
                    {
                        nums = string.Format("{0}成人 {1}儿童", dt.Rows[i]["Adults"], dt.Rows[i]["Childs"]);
                    }
                    else
                    {
                        nums = string.Format("{0}成人", dt.Rows[i]["Adults"]);
                    }
                    sb.Append("<div class=\"recommend_detail\">");
                    sb.Append("<div class=\"recommend_txt\">");
                    sb.Append(string.Format("<h3>订单号：{0}</h3>", dt.Rows[i]["AutoId"]));
                    sb.Append(string.Format("<div>线路：{0}</div>", dt.Rows[i]["LineName"]));
                    sb.Append(string.Format("<div>日期：{0}</div>", dt.Rows[i]["BeginDate"]));
                    sb.Append(string.Format("<div>人数：{0}</div>", nums));
                    sb.Append(string.Format("<div>费用：<span class=\"o-price\">¥{0}</span></div>", dt.Rows[i]["Price"]));
                    sb.Append(string.Format("<div>积分抵扣：{0}</div>", dt.Rows[i]["integral"]));
                    sb.Append("</div></div>");
                }
            }
            sb.Append("</div></div>");
            return sb.ToString();
        }

        public static object IntegralDetail_New()
        {
            JSONObject ObJson = new JSONObject();
            string sql = string.Format("SELECT isnull(sum(integral),0) integral FROM [OL_Integral] where uid = '{0}'", Convert.ToString(HttpContext.Current.Session["Online_UserId"]));
            string sumIntegral = MyDataBaseComm.getScalar(sql);
            if (!string.IsNullOrEmpty(sumIntegral))
            {
                ObJson.Add("sumIntegral", sumIntegral);
            }
            string SqlQueryText = string.Format("select a.AutoId,a.LineName,a.BeginDate,a.Adults,a.Childs,a.Price,a.CouponAmount,b.integral from OL_Order as a inner join OL_Integral as b on a.OrderId=b.orderid where OrderUser='{0}' and CouponAmount is not null", HttpContext.Current.Session["Online_UserId"]);
            DataTable dt = MyDataBaseComm.getDataSet(SqlQueryText).Tables[0];
            if (dt.Rows.Count > 0)
            {
                JSONArray ArrJson = new JSONArray();
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
            }
            return json.SerializeObject(ObJson);
        }
        #endregion
    }
}