using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Text;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ServiceModel.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml;
using TravelOnline.TravelMisWebService;
using System.Web.Services.Protocols;
using TravelOnline.GetCombineKeys;


namespace TravelOnline.WebService
{
    /// <summary>
    /// ota 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[SoapRpcService(RoutingStyle = SoapServiceRoutingStyle.SoapAction)] 
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ota : System.Web.Services.WebService
    {
        public class LineInfos
        {
            public string Lineid { get; set; }
            public string EditTime { get; set; }
            public string LineName { get; set; }
            public string LineDays { get; set; }
            public string LineType { get; set; }
            public string LineClass { get; set; }
            public string Price { get; set; }
            public string PlanDate { get; set; }
            public string Destination { get; set; }
            public string Pics { get; set; }
            public string LineFeature { get; set; }
            public PlanInfos[] Plans { get; set; }
        }

        public class PlanInfos
        {
            public string PlanId { get; set; }
            public string BgnDate { get; set; }
            public string Price { get; set; }
        }

        public class LineRoute
        {
            public string Feature { get; set; } //行程特色
            public string Attentions { get; set; } //注意事项
            public string PriceIn { get; set; } //价格包含
            public string PriceOut { get; set; } //价格不含
            public string OwnExpense { get; set; } //自费项目
            public string Shopping { get; set; } //购物商店
            public RouteInfos[] Routes { get; set; }
        }

        public class RouteInfos
        {
            public string daterank { get; set; }
            public string rname { get; set; }
            public string dinner { get; set; }
            public string bus { get; set; }
            public string room { get; set; }
            public string Pics { get; set; }
            public string route { get; set; }
        }

        public class Prices
        {
            public string PriceId { get; set; }
            public string PriceFlag { get; set; }
            public string PriceType { get; set; }
            public string PriceName { get; set; }
            public string Price { get; set; }
        }


        public class OTA_Order
        {
            public string lineid { get; set; }
            public string planid { get; set; }
            public string linename { get; set; }
            public string begindate { get; set; }
            public string ordernums { get; set; }
            public string adults { get; set; }
            public string childs { get; set; }
            public string price { get; set; }
            public string ordername { get; set; }
            public string orderemail { get; set; }
            public string ordermobile { get; set; }
            public string ordertel { get; set; }
            public string ordermemo { get; set; }
            public string orderota { get; set; }
            public OTA_Price[] pricelist { get; set; }
            public OTA_Guest[] guestlist { get; set; }
            public string sign { get; set; }
        }

        public class OTA_Price
        {
            public string priceid { get; set; }
            public string priceflag { get; set; }
            public string pricetype { get; set; }
            public string pricename { get; set; }
            public string nums { get; set; }
            public string price { get; set; }
            public string allprice { get; set; }
        }

        public class OTA_Guest
        {
            public string guestname { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string tel { get; set; }
            public string memo { get; set; }
            public string idcard { get; set; }
            public string cardtype { get; set; }
        }

        public class OTA_Pay
        {
            public string orderid { get; set; }
            public string lineid { get; set; }
            public string planid { get; set; }
            public string paytime { get; set; }
            public string price { get; set; }
            public string tradeno { get; set; }
            public string memo { get; set; }
            public string sign { get; set; }
        }

        public class OTA_PriceCheck
        {
            public string lineid { get; set; }
            public string planid { get; set; }
            public string price { get; set; }
            public string sign { get; set; }
            public OTA_Price[] pricelist { get; set; }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string line(string id)
        {
            SaveErrorToLog("line 线路列表访问，专题id：" + id);

            string infos = Convert.ToString(HttpContext.Current.Cache["OTA_LineList_" + id]);
            if (infos == "")
            {
                List<LineInfos> list = new List<LineInfos>();
                string SqlQueryText = string.Format("select * from SpecialTopic where Types='OTA' and id='{0}'", id);
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    string sid, typeid, destid;
                    sid = DS1.Tables[0].Rows[0]["id"].ToString();
                    typeid = DS1.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS1.Tables[0].Rows[0]["Destinationid"].ToString();

                    SqlQueryText = string.Format("select * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", sid);
                    DataSet DS = new DataSet();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT Lineid,EditTime,LineName,Price,LineClass,PlanDate,Destination,Pics,LineType,LineFeature FROM View_SpecialLineTemp where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and lineclass in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";

                            }
                            sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string Pics = "/Images/none.gif";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            Pics = "/images/none.gif";
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                            if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                            DS.Tables[0].Rows[i]["Pics"] = Pics;
                        }

                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            LineInfos lines = new LineInfos();
                            //Model.att a = new Model.att();
                            lines.Lineid = dr["Lineid"].ToString();
                            lines.EditTime = dr["EditTime"].ToString();
                            lines.LineName = dr["LineName"].ToString();
                            lines.LineDays = dr["LineDays"].ToString();
                            lines.LineType = dr["LineType"].ToString();
                            lines.LineClass = dr["LineClass"].ToString();
                            lines.Price = dr["Price"].ToString();
                            lines.PlanDate = dr["PlanDate"].ToString();
                            lines.Destination = dr["Destination"].ToString();
                            lines.Pics = dr["Pics"].ToString();
                            lines.LineFeature = dr["LineFeature"].ToString();

                            SqlQueryText = string.Format("select * from OL_Plan where lineid='{0}' order by begindate", dr["Lineid"].ToString());
                            DS1 = new DataSet();
                            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                            lines.Plans = new PlanInfos[DS1.Tables[0].Rows.Count];
                            for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                            {
                                lines.Plans[i] = new PlanInfos();
                                lines.Plans[i].PlanId = DS1.Tables[0].Rows[i]["planid"].ToString();
                                lines.Plans[i].BgnDate = string.Format("{0:yyyy-MM-dd}", DS1.Tables[0].Rows[i]["begindate"]);
                                lines.Plans[i].Price = DS1.Tables[0].Rows[i]["price"].ToString();
                            }
                            list.Add(lines);
                        }
                        //return MyConvert.DtSerializeJson(DS.Tables[0]);
                        infos = JsonConvert.SerializeObject(list);
                    }
                    else
                    {
                        infos = "{\"error\":\"没有数据\"}";
                    }
                }
                HttpContext.Current.Cache.Insert("OTA_LineList_" + id, infos);
            }
            return infos;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string plans(string lineid)
        {
            SaveErrorToLog("plans 计划列表访问，计划id：" + lineid);
            string infos = Convert.ToString(HttpContext.Current.Cache["OTA_PlanList_" + lineid]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select * from OL_Plan where lineid='{0}' order by begindate", lineid);
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    List<PlanInfos> list = new List<PlanInfos>();
                    foreach (DataRow dr in DS1.Tables[0].Rows)
                    {
                        PlanInfos lines = new PlanInfos();
                        lines.PlanId = dr["planid"].ToString();
                        lines.BgnDate = string.Format("{0:yyyy-MM-dd}", dr["begindate"]);
                        lines.Price = dr["Price"].ToString();
                        list.Add(lines);
                    }
                    infos = JsonConvert.SerializeObject(list);
                }
                else
                {
                    infos =  "{\"error\":\"没有数据\"}";
                }
                HttpContext.Current.Cache.Insert("OTA_PlanList_" + lineid, infos);
            }
            return infos;

            
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string lineroute(string lineid)
        {
            SaveErrorToLog("lineroute 线路行程访问，线路id：" + lineid);

            //string infos = Convert.ToString(HttpContext.Current.Cache["OTA_PlanList_" + lineid]);
            //if (infos == "")
            //{

            //    HttpContext.Current.Cache.Insert("OTA_PlanList_" + lineid, infos);
            //}
            //return infos;

            string infos = Convert.ToString(HttpContext.Current.Cache["OTA_LineRoute_" + lineid]);
            if (infos == "")
            {
                string linetype = "", LineName = "";
                string SqlQueryText = string.Format("select LineName,linetype from OL_Line where MisLineId='{0}'", lineid);
                DataSet DS1 = new DataSet();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    linetype = DS1.Tables[0].Rows[0]["linetype"].ToString();
                    LineName = DS1.Tables[0].Rows[0]["LineName"].ToString();
                }
                string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, lineid);
                if (System.IO.File.Exists(path) == true)
                {
                    StringBuilder Strings = new StringBuilder();
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.Load(path);
                    XmlNode x = XmlDoc.SelectSingleNode("//Route");
                    List<LineRoute> list = new List<LineRoute>();
                    LineRoute lineroute = new LineRoute();

                    if (x != null)
                    {
                        if (linetype == "Visa") //签证所需材料
                        {
                            StringBuilder VisaString = new StringBuilder();
                            //特色开始
                            Strings.Append("<div class=recommend_wrap>");

                            string Visa1 = "", Visa2 = "", Visa3 = "", Visa4 = "", Visa5 = "", Visa6 = "";
                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            for (int i = 0; i < elemList.Count; i++)
                            {
                                VisaString.Clear();
                                VisaString.Append(string.Format("<dl><dt>{0}：</dt><dd>", elemList[i].SelectSingleNode("VisaName").InnerText));
                                VisaString.Append(string.Format("<div>{0}</div>", elemList[i].SelectSingleNode("VisaContent").InnerText.Replace("\n", "<br>")));
                                if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0 || elemList[i].SelectSingleNode("V2").InnerText.Length != 0)
                                {
                                    VisaString.Append("<div class=files>");
                                    if (elemList[i].SelectSingleNode("V1").InnerText.Length != 0) VisaString.Append(string.Format("原件：{0} &nbsp;&nbsp;&nbsp;&nbsp;", elemList[i].SelectSingleNode("V1").InnerText));
                                    if (elemList[i].SelectSingleNode("V2").InnerText.Length != 0) VisaString.Append(string.Format("复印件：{0}", elemList[i].SelectSingleNode("V2").InnerText));
                                    VisaString.Append("</div>");
                                }
                                VisaString.Append("</dd></dl>");
                                VisaString.Append("");
                                switch (elemList[i].SelectSingleNode("Flag").InnerText)
                                {
                                    case "1":
                                        Visa1 += VisaString.ToString();
                                        break;
                                    case "2":
                                        Visa2 += VisaString.ToString();
                                        break;
                                    case "3":
                                        Visa3 += VisaString.ToString();
                                        break;
                                    case "4":
                                        Visa4 += VisaString.ToString();
                                        break;
                                    case "5":
                                        Visa5 += VisaString.ToString();
                                        break;
                                    case "6":
                                        Visa6 += VisaString.ToString();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            //签证所需材料
                            Strings.Append("<!DOCTYPE HTML><html><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1' /><title>" + LineName + "</title><style>body { font: normal 100% Helvetica, Arial, sans-serif; }h3 { font-size: 1.5em; }strong{font-size: 1.2em;}.visa_info{ font-size:1em;}</style></head><body>");

                            Strings.Append("<div class=recommend_detail>");
                            Strings.Append("<div class=recommend_txt>");
                            Strings.Append("<h3>签证所需材料</h3>");
                            if (Visa1.Length > 5)
                            {
                                Strings.Append("<div><strong>身份证明</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa1));
                                Strings.Append("</div>");
                            }
                            if (Visa2.Length > 5)
                            {
                                Strings.Append("<div><strong>资产证明</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa2));
                                Strings.Append("</div>");
                            }
                            if (Visa3.Length > 5)
                            {
                                Strings.Append("<div><strong>工作证明</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa3));
                                Strings.Append("</div>");
                            }
                            if (Visa4.Length > 5)
                            {
                                Strings.Append("<div><strong>学生及儿童</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa4));
                                Strings.Append("</div>");
                            }
                            if (Visa5.Length > 5)
                            {
                                Strings.Append("<div><strong>老人</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa5));
                                Strings.Append("</div>");
                            }
                            if (Visa6.Length > 5)
                            {
                                Strings.Append("<div><strong>其他</strong></div>");
                                Strings.Append("<div class=visa_info>");
                                Strings.Append(string.Format("{0}", Visa6));
                                Strings.Append("</div>");
                            }

                            Strings.Append("</div>");
                            Strings.Append("</div>");

                            Strings.Append("</div>");
                            //特色结束
                            Strings.Append("</body></html>");
                            return "{\"visa\":\"" + Strings.ToString().Replace("\"", "'") + "\"}";
                        }
                        else
                        {
                            lineroute.Feature = x.SelectSingleNode("Feature").InnerText;
                            lineroute.PriceIn = x.SelectSingleNode("PriceIn").InnerText;
                            lineroute.PriceOut = x.SelectSingleNode("PriceOut").InnerText;
                            lineroute.OwnExpense = x.SelectSingleNode("OwnExpense").InnerText;
                            lineroute.Attentions = x.SelectSingleNode("Attentions").InnerText;
                            lineroute.Shopping = x.SelectSingleNode("Shopping").InnerText;

                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            lineroute.Routes = new RouteInfos[elemList.Count];

                            string Pics = "/Images/none.gif";
                            for (int i = 0; i < elemList.Count; i++)
                            {
                                lineroute.Routes[i] = new RouteInfos();
                                Pics = "/Images/none.gif";
                                if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 5)
                                {
                                    Pics = string.Format("/images/views/{0}/m_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                }
                                
                                lineroute.Routes[i].daterank = elemList[i].SelectSingleNode("daterank").InnerText;
                                lineroute.Routes[i].rname = elemList[i].SelectSingleNode("rname").InnerText.Replace("\"", "'");
                                lineroute.Routes[i].bus = elemList[i].SelectSingleNode("bus").InnerText.Replace("\"", "'");
                                lineroute.Routes[i].dinner = elemList[i].SelectSingleNode("dinner").InnerText.Replace("\"", "'");
                                lineroute.Routes[i].room = elemList[i].SelectSingleNode("room").InnerText.Replace("\"", "'");
                                lineroute.Routes[i].route = elemList[i].SelectSingleNode("route").InnerText.Replace("\"", "'");
                                lineroute.Routes[i].Pics = Pics;
                            }
                        }

                        list.Add(lineroute);
                        infos = JsonConvert.SerializeObject(lineroute);
                    }
                    else
                    {
                        infos = "{\"error\":\"没有数据\"}";
                    }

                }
                else
                {
                    infos = "{\"error\":\"没有数据\"}";
                }
                HttpContext.Current.Cache.Insert("OTA_LineRoute_" + lineid, infos);
            }
            return infos;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string planseats(string lineid,string planid)
        {
            SaveErrorToLog("planseats 计划剩余人数访问，线路id：" + lineid + "，计划id：" + planid);
            PlanSeats GetPlan = new PlanSeats();
            if (MyConvert.ConToInt(planid) == 0)
            {
                GetPlan.Seats = "99";
                GetPlan.StopDate = "";
            }
            else
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, "");
                }
                catch
                {
                    GetPlan.Seats = "0";
                    GetPlan.StopDate = "";
                }
            }
            return "{\"Seats\":\"" + GetPlan.Seats + "\",\"StopDate\":\"" + GetPlan.StopDate + "\"}";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string price(string lineid, string planid)
        {
            SaveErrorToLog("price 费用明细列表访问，线路id：" + lineid + "，计划id：" + planid);

            string infos = Convert.ToString(HttpContext.Current.Cache["OTA_PriceList_" + lineid + "_" + planid]);
            if (infos == "")
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                PlanPrices GetPlan = new PlanPrices();

                List<Prices> list = new List<Prices>();
                try
                {
                    if (MyConvert.ConToInt(planid) == 0)
                    {
                        GetPlan = rsp.GetLinePrices(UpPassWord, lineid, "");
                    }
                    else
                    {
                        GetPlan = rsp.GetPlanPrices(UpPassWord, lineid, planid, "");

                    }
                }
                catch
                {
                    return "{\"error\":\"没有数据\"}";
                }

                if (GetPlan.PlanStaPrice != null)
                {
                    for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                    {
                        Prices price = new Prices();
                        price.PriceId = GetPlan.PlanStaPrice[i].PriceId;
                        price.PriceFlag = "基本团费";
                        price.PriceType = GetPlan.PlanStaPrice[i].PriceType;
                        price.PriceName = GetPlan.PlanStaPrice[i].PriceName;
                        price.Price = GetPlan.PlanStaPrice[i].Price;
                        list.Add(price);
                    }
                }
                if (GetPlan.PlanExtPrice != null)
                {
                    for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                    {
                        Prices price = new Prices();
                        string ExtType = "";
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
                            case "6":
                                ExtType = "机票税金";
                                break;
                            default:
                                break;
                        }
                        price.PriceId = GetPlan.PlanExtPrice[i].PriceId;
                        price.PriceFlag = ExtType;
                        price.PriceType = GetPlan.PlanExtPrice[i].PriceType;
                        price.PriceName = GetPlan.PlanExtPrice[i].PriceName;
                        price.Price = GetPlan.PlanExtPrice[i].Price;
                        list.Add(price);
                    }
                }
                infos = JsonConvert.SerializeObject(list);
                HttpContext.Current.Cache.Insert("OTA_PriceList_" + lineid + "_" + planid, infos);
            }
            return infos;
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = true)]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string OrderSubmit(OTA_Order orders)
        {
            
            if (orders == null) return "{\"error\":\"没有任何订单数据\"}";

            SaveErrorToLog("OrderSubmit 订单提交，线路id：" + orders.lineid + "，计划id：" + orders.planid + "，联系人：" + orders.ordername);
            
            //string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            //TravelOnlineService rsp = new TravelOnlineService();
            //rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            //string MisOrder = "";

            //try
            //{
            //    MisOrder = rsp.Ota_Order(UpPassWord, orders.lineid, orders.planid, orders.ordernums, orders.ordername, orders.ordermobile + orders.ordertel, orders.orderota);
            //}
            //catch
            //{
            //    return "{\"error\":\"青旅内部业务系统通讯失败\"}";
            //}

            //if (MisOrder != "ok") return "{\"error\":\"" + MisOrder + "\"}";

            //string TableName = "Ota_Order";
            string TableName = "OL_TempOrder";
            string Id = "0";
            string[] Fil = new string[21];
            string[] Val = new string[21];

            //Fil[0] = "lineid"; Val[0] = orders.lineid;
            //Fil[1] = "planid"; Val[1] = orders.planid;
            //Fil[2] = "linename"; Val[2] = orders.linename;
            //Fil[3] = "begindate"; Val[3] = orders.begindate;
            //Fil[4] = "ordernums"; Val[4] = orders.ordernums;
            //Fil[5] = "adults"; Val[5] = orders.adults;
            //Fil[6] = "childs"; Val[6] = orders.childs;
            //Fil[7] = "price"; Val[7] = orders.price;
            //Fil[8] = "ordername"; Val[8] = orders.ordername;
            //Fil[9] = "orderemail"; Val[9] = orders.orderemail;
            //Fil[10] = "ordermobile"; Val[10] = orders.ordermobile;
            //Fil[11] = "ordertel"; Val[11] = orders.ordertel;
            //Fil[12] = "ordermemo"; Val[12] = orders.ordermemo;
            //Fil[13] = "orderota"; Val[13] = orders.orderota;

            string ProductType = "0", ProductClass = "0", LineDays = "0";
            string SqlQueryText = string.Format("select * from OL_Line where MisLineId='{0}'", orders.lineid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ProductType = DS.Tables[0].Rows[0]["LineType"].ToString();
                ProductClass = DS.Tables[0].Rows[0]["LineClass"].ToString();
                LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
            }
            else
            {
                return "{\"error\":\"订单提交失败，旅游线路不存在，请稍后再试\"}";
            }

            string orderid = CombineKeys.NewComb().ToString();
            Fil[0] = "OrderId"; Val[0] = orderid;
            Fil[1] = "ProductType"; Val[1] = ProductType;
            Fil[2] = "ProductClass"; Val[2] = ProductClass;
            Fil[3] = "LineID"; Val[3] = orders.lineid;
            Fil[4] = "PlanId"; Val[4] = orders.planid;
            Fil[5] = "LineName"; Val[5] = orders.linename;
            Fil[6] = "BeginDate"; Val[6] = orders.begindate;
            Fil[7] = "OrderNums"; Val[7] = orders.ordernums;
            Fil[8] = "Adults"; Val[8] = orders.adults;
            Fil[9] = "Childs"; Val[9] = orders.childs;
            Fil[10] = "Price"; Val[10] = orders.price;
            Fil[11] = "OrderName"; Val[11] = orders.ordername;
            Fil[12] = "OrderEmail"; Val[12] = orders.orderemail;
            Fil[13] = "OrderMobile"; Val[13] = orders.ordermobile;
            Fil[14] = "OrderTel"; Val[14] = orders.ordertel;
            Fil[15] = "OrderMemo"; Val[15] = "浦发银行活动订单" + " " + orders.ordermemo;
            Fil[16] = "ota"; Val[16] = orders.orderota;
            Fil[17] = "OrderTime"; Val[17] = DateTime.Now.ToString();
            Fil[18] = "OrderUser"; Val[18] = "453a3c3e-e479-408d-ae97-9f4c002b7bce";
            Fil[19] = "OrderFlag"; Val[19] = "1";
            Fil[20] = "DeptId"; Val[20] = "0";


            string Md5Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("lineid=" + orders.lineid + "&planid=" + orders.planid + "&begindate=" + orders.begindate + "&ordernums=" + orders.ordernums + "&price=" + orders.price + "&ordername=" + orders.ordername + "&key=Shpfyh_Scyts_2015", "MD5");
            string ssign = "lineid=" + orders.lineid + "&planid=" + orders.planid + "&begindate=" + orders.begindate + "&ordernums=" + orders.ordernums + "&price=" + orders.price + "&ordername=" + orders.ordername + "&key=Shpfyh_Scyts_2015" + "||" + orders.sign;
            SaveErrorToLog("OrderSubmit 订单提交，Md5Sign：" + ssign);
            
            //if (orders.sign != Md5Sign) return "{\"error\":\"验证失败，请稍后再试\"}";


            Id = MyDataBaseComm.InsertDataStrGetReturn(TableName, Fil, Val);

            SaveErrorToLog("OrderSubmit 订单成功步骤1，线路id：" + orders.lineid + "，计划id：" + orders.planid + "，订单id：" + Id);

            if (MyConvert.ConToInt(Id) == 0)
            {
                SaveErrorToLog("OrderSubmit 订单成功步骤2，线路id：" + orders.lineid + "，计划id：" + orders.planid + "，失败，订单id没有成功返回");
                return "{\"error\":\"订单提交失败，请稍后再试" + Id + "\"}";
            }
            else
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

                OrderInfos Sorder = new OrderInfos();
                Sorder.adult = orders.adults;
                Sorder.begindate = orders.begindate;
                Sorder.childs = orders.childs;
                Sorder.days = LineDays;
                Sorder.deptid = "0";
                Sorder.email = orders.orderemail;
                Sorder.gathering = orders.price;

                Sorder.infoid = ProductClass;
                Sorder.ordertypes = ProductType;
                Sorder.lineid = orders.lineid;
                Sorder.linename = orders.linename;
                Sorder.mobile = orders.ordermobile;
                Sorder.orderdate = DateTime.Now.ToString();
                if (MyConvert.ConToInt(orders.planid) > 0)
                {
                    Sorder.orderflag = "1";
                }
                else
                {
                    Sorder.orderflag = "0";
                } 
                Sorder.orderid = orderid;
                Sorder.ordermemo = "浦发银行活动订单" + " " + orders.ordermemo;
                Sorder.ordername = orders.ordername;
                Sorder.ordernumber = orders.ordernums;
                Sorder.planid = orders.planid;
                Sorder.tel = orders.ordertel;
                Sorder.orderno = Id;
                Sorder.contract = "0";
                Sorder.invoice = "0";
                Sorder.SellDept = "0";

                Sorder.CruisesFlag = "0";
                Sorder.ccid = "0";
                Sorder.acom = "0";
                Sorder.adept = "0";
                Sorder.SellUser = "0";

                string OrderFlag = "1";
                string message = "";
                try
                {
                    OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
                }
                catch (Exception exception)
                {
                    message = exception.Message;
                    OrderFlag = "9";
                }

                if (OrderFlag == "8")
                {
                    return "{\"error\":\"计划不存在或剩余人数不足" + message + "\"}";
                }

                if (OrderFlag == "9")
                {
                    return "{\"error\":\"青旅内部业务系统通讯失败，请稍后再试" + message + "\"}";
                }

                SaveErrorToLog("OrderSubmit 订单成功步骤3，线路id：" + orders.lineid + "，计划id：" + orders.planid + "，青旅内部业务系统成功返回，OrderFlag=" + OrderFlag);

                List<string> Sql = new List<string>();

                Sql.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", orderid));
                Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", orderid));
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", orderid, DateTime.Now.ToString(), "浦发银行活动预订单"));
                if (orders.pricelist != null)
                {
                    TableName = "OL_OrderPrice";
                    for (int i = 0; i < orders.pricelist.Length; i++)
                    {
                        if (MyConvert.ConToDec(orders.pricelist[i].nums) > 0)
                        {
                            Fil = new string[8];
                            Val = new string[8];

                            Fil[0] = "OrderId"; Val[0] = orderid;
                            if (orders.pricelist[i].priceflag == "基本团费")
                            {
                                Fil[1] = "PriceType"; Val[1] = "SellPrice";
                                Fil[2] = "PriceName"; Val[2] = orders.pricelist[i].pricetype;
                                Fil[2] = "PriceMemo"; Val[2] = orders.pricelist[i].pricename;
                            }
                            else
                            {
                                Fil[1] = "PriceType"; Val[1] = "ExtPrice";
                                Fil[2] = "PriceName"; Val[2] = orders.pricelist[i].priceflag;
                                Fil[2] = "PriceMemo"; Val[2] = orders.pricelist[i].pricename + " " + orders.pricelist[i].pricetype;
                            }
                            Fil[3] = "OrderNums"; Val[3] = orders.pricelist[i].nums;
                            Fil[4] = "SellPrice"; Val[4] = orders.pricelist[i].price;
                            Fil[5] = "SumPrice"; Val[5] = orders.pricelist[i].allprice;
                            Fil[6] = "InputDate"; Val[6] = DateTime.Now.ToString();
                            Fil[7] = "PriceId"; Val[7] = orders.pricelist[i].priceid;
                            Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                        }
                    }
                }

                if (orders.guestlist != null)
                {
                    TableName = "OL_GuestInfo";
                    for (int i = 0; i < orders.guestlist.Length; i++)
                    {
                        Fil = new string[8];
                        Val = new string[8];

                        Fil[0] = "OrderId"; Val[0] = orderid;
                        Fil[1] = "GuestName"; Val[1] = orders.guestlist[i].guestname;
                        //Fil[2] = "email"; Val[2] = orders.guestlist[i].email;
                        Fil[3] = "Mobile"; Val[3] = orders.guestlist[i].mobile;
                        Fil[4] = "Tel"; Val[4] = orders.guestlist[i].tel;
                        //Fil[5] = "otherinfo"; Val[5] = orders.guestlist[i].memo;
                        Fil[6] = "IdNumber"; Val[6] = orders.guestlist[i].idcard;
                        Fil[7] = "IdType"; Val[7] = "1";
                        Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));
                    }
                }

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    SaveErrorToLog("OrderSubmit 订单成功步骤4，线路id：" + orders.lineid + "，计划id：" + orders.planid + "，预订结束");
                    return "{\"success\":\"" + Id + "\"}";
                }
            }

            return "{\"error\":\"订单提交失败，请稍后再试\"}";
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = true)]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string OrderPay(OTA_Pay pays)
        {
            if (pays == null) return "{\"error\":\"没有任何支付数据\"}";

            SaveErrorToLog("OrderPay 在线支付信息，订单id：" + pays.orderid + "，交易id：" + pays.tradeno + "，金额：" + pays.price);

            string uid = "";
            string SqlQueryText = string.Format("select * from OL_Order where AutoId='{0}'", pays.orderid);
            DataSet DS1 = new DataSet();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                uid = DS1.Tables[0].Rows[0]["OrderId"].ToString();
            }
            else
            {
                return "{\"error\":\"订单不存在，不能提交支付数据\"}";
            }

            string TableName = "Ota_Pay";
            string[] Fil = new string[5];
            string[] Val = new string[5];

            Fil[0] = "orderid"; Val[0] = pays.orderid;
            Fil[1] = "tradeno"; Val[1] = pays.tradeno;
            Fil[2] = "payprice"; Val[2] = pays.price;
            Fil[3] = "paytime"; Val[3] = pays.paytime;
            Fil[4] = "paycontent"; Val[4] = pays.memo;

            //string Md5Sign = "orderid=" + pays.orderid + "&tradeno=" + pays.tradeno + "&price=" + pays.price + "&paytime=" + pays.paytime + "&key=Shpfyh_Scyts_2015";
            //TravelOnline.EncryptCode.SecurityCode.Md5_Encrypt("orderid=" + pays.orderid + "&tradeno=" + pays.tradeno + "&price=" + pays.price + "&paytime=" + pays.paytime + "&key=Shpfyh_Scyts_2015", 32);
            string Md5Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("orderid=" + pays.orderid + "&tradeno=" + pays.tradeno + "&price=" + pays.price + "&paytime=" + pays.paytime + "&key=Shpfyh_Scyts_2015", "MD5");
            if (pays.sign != Md5Sign) return "{\"error\":\"验证失败，请稍后再试\"}";

            List<string> Sql = new List<string>();
            Sql.Add(MyDataBaseComm.InsertDataStr(TableName, Fil, Val, ""));

            Sql.Add(string.Format("UPDATE OL_Order set PayFlag='1' where OrderId='{0}'", uid));

            Sql.Add(string.Format("insert into OL_PayMent (OrderId,TradeNo,Buyer,PayPrice,PayTime,PayContent,PayType) values ('{0}','{1}','{2}','{3}','{4}','{5}','spdb')",
                uid,
                pays.tradeno,
                "",
                pays.price,
                DateTime.Now.ToString(),
                pays.memo
                )
            );

            Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','在线支付成功，付款金额：{2}，浦发交易号：{3}')",
                uid,
                DateTime.Now.ToString(),
                pays.price,
                pays.tradeno
                )
            );

            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                PayInfo Pays = new PayInfo();
                Pays.OrderId = uid;
                Pays.TradeNo = pays.tradeno;
                Pays.PayPrice = pays.price;
                Pays.PayTime = DateTime.Now.ToString();
                Pays.PayContent = "浦发信用卡支付";

                string Result;
                try
                {
                    Result = rsp.PayInfoSave(UpPassWord, Pays);
                }
                catch
                {
                    SaveErrorToLog("OrderId为" + uid + "的订单，向畅游系统支付失败！");
                }
                return "{\"success\":\"" + pays.orderid + "\"}";
            }
            else
            {
                return "{\"error\":\"支付信息提交失败，请稍后再试\"}";
            }
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = true)]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string PriceCheck(OTA_PriceCheck priceinfo)
        {
            if (priceinfo == null) return "{\"error\":\"没有任何费用数据 priceinfo为空\"}";
            if (priceinfo.pricelist.Length == 0) return "{\"error\":\"没有任何费用数据 pricelist为空\"}";
            string lineid = priceinfo.lineid, planid = priceinfo.planid;
            if (MyConvert.ConToInt(lineid) == 0) return "{\"error\":\"线路id参数错误\"}";

            SaveErrorToLog("PriceCheck 费用核对，线路id：" + lineid + "，计划id：" + planid + "，金额：" + priceinfo.price);

            string Md5Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("lineid=" + priceinfo.lineid + "&planid=" + priceinfo.planid + "&price=" + priceinfo.price + "&key=Shpfyh_Scyts_2015", "MD5");
            if (priceinfo.sign != Md5Sign) return "{\"error\":\"验证失败，请稍后再试\"}";


            double AllPrice = 0;

            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            PlanPrices GetPlan = new PlanPrices();

            List<Prices> list = new List<Prices>();
            try
            {
                if (MyConvert.ConToInt(planid) == 0)
                {
                    GetPlan = rsp.GetLinePrices(UpPassWord, lineid, "");
                }
                else
                {
                    GetPlan = rsp.GetPlanPrices(UpPassWord, lineid, planid, "");

                }
            }
            catch
            {
                return "{\"error\":\"没有查询到价格数据\"}";
            }

            if (GetPlan.PlanStaPrice != null)
            {
                for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                {
                    for (int ii = 0; ii < priceinfo.pricelist.Length; ii++)
                    {
                        if (priceinfo.pricelist[ii].priceflag == "基本团费" && priceinfo.pricelist[ii].priceid == GetPlan.PlanStaPrice[i].PriceId)
                        {
                            AllPrice += MyConvert.ConToDouble(GetPlan.PlanStaPrice[i].Price) * MyConvert.ConToDouble(priceinfo.pricelist[ii].nums);
                        }
                    }
                }
            }
            if (GetPlan.PlanExtPrice != null)
            {
                for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                {
                    for (int ii = 0; ii < priceinfo.pricelist.Length; ii++)
                    {
                        if (priceinfo.pricelist[ii].priceflag != "基本团费" && priceinfo.pricelist[ii].priceid == GetPlan.PlanExtPrice[i].PriceId)
                        {
                            AllPrice += MyConvert.ConToDouble(GetPlan.PlanExtPrice[i].Price) * MyConvert.ConToDouble(priceinfo.pricelist[ii].nums);
                        }
                    }
                }
            }

            return "{\"success\":\"" + AllPrice + "\"}";

            //if (AllPrice == MyConvert.ConToDouble(priceinfo.price))
            //{
            //    return "{\"success\":\"" + AllPrice + "\"}";
            //}
            //else
            //{
            //    return "{\"error\":\"传递的合计价格计算错误\"}";
            //}
        }


        [WebMethod]
        //[ScriptMethod(UseHttpGet = true)]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "GET")]
        public string OrderCancel(string orderid,string sign)
        {
            SaveErrorToLog("OrderCancel 订单取消，订单id：" + orderid);
            string Md5Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("orderid=" + orderid + "&key=Shpfyh_Scyts_2015", "MD5");
            if (sign != Md5Sign) return "{\"error\":\"验证失败，请稍后再试\"}";

            string SqlQueryText = string.Format("select * from OL_Order where AutoId='{0}'", orderid);
            DataSet DS1 = new DataSet();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("update OL_Order set orderflag='8' where AutoId='{0}'", orderid));
                Sql.Add(string.Format("insert into Ota_Cancel (orderid,dotime) values ('{0}','{1}')", orderid, DateTime.Now.ToString()));
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS1.Tables[0].Rows[0]["OrderId"].ToString(), DateTime.Now.ToString(), "浦发银行通过ota接口 取消订单"));
                
                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    return "{\"success\":\"订单取消成功\"}";
                }
                else
                {
                    return "{\"error\":\"订单取消失败，数据库操作错误\"}";
                }
                
            }
            else
            {
                return "{\"error\":\"订单取消失败，没有查询到要取消的订单\"}";
            }
            
        }

        private static void SaveErrorToLog(string infos)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\PfyhLog.txt";

            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(DateTime.Now.ToString() + "，访问IP：" + GetIpaddress());
                writer.WriteLine(infos);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception exception)
            {
                string message = exception.Message;
            }
        }

        public static string GetIpaddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result)) result = request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result)) result = request.UserHostAddress;
            if (string.IsNullOrEmpty(result)) result = "0.0.0.0";
            return result;
        }


    }
}
