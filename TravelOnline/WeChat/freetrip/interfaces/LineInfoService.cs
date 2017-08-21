using LitJson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using TravelOnline.Class.Travel;
using TravelOnline.WeChat.freetrip.model;

namespace TravelOnline.WeChat.freetrip.interfaces
{
	public class LineInfoService
	{
        public static string GetFreetripBannerList()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_BannerList"]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 5 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", "freetrip");
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                List<FlashAD> bannerList = new List<FlashAD>();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        FlashAD banner = new FlashAD();
                        banner.AdPageUrl = DS.Tables[0].Rows[i]["AdPageUrl"].ToString();
                        banner.AdPicUrl = DS.Tables[0].Rows[i]["AdPicUrl"].ToString();
                        bannerList.Add(banner);
                    }
                }
                infos = JsonMapper.ToJson(bannerList);
                HttpContext.Current.Cache.Insert("WeChat_BannerList", infos);
            }
            return infos;
        }

        public static string GetFreetripLineList(LineSelecter select) {
            string infos = "";
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}' and lineName like '%【游淘】%' and ", "2013-03-01"));//DateTime.Today.ToString()
            if (select.searchval != null)
            {
                string dest_id = "0";
                dest_id = MyDataBaseComm.getScalar("select Id from OL_Destination where DestinationName like '%" + select.searchval + "%'");

                Strings.Append(string.Format("(LineName like '%{0}%' ", select.searchval));
                if (MyConvert.ConToInt(dest_id) != 0)
                {
                    Strings.Append(string.Format(" or Destinationid like '%,{0},%' ) and ", dest_id));
                }
                else
                {
                    Strings.Append(string.Format(") and ", dest_id));
                }
            }
            else
            {
                if (select.linetype != null) Strings.Append(string.Format("LineType='{0}' and ", select.linetype));
                if (select.lineclass != null) Strings.Append(string.Format("(lineclass='{0}' or LineName like '%{1}%') and ", select.lineclass, select.lineclassname));
                if (select.dest != null) Strings.Append(string.Format("Destinationid like '%,{0},%' and ", select.dest));
            }
            string fieldlist = "*,(select max(preferAmount) from OL_Preferential where (Lineid=dbo.OL_Line.MisLineid and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate()))) AS preferAmount";
            Strings.Append("1=1 ");

            string condition = Strings.ToString();
            string pkey = "id";
            string sortflag = "";
            string sortname = "Price";
            string tablename = "OL_Line";
            int pagesize = select.pagesize == 0 ? 20 : select.pagesize;
            int currpage = select.pages == 0 ? 1 : select.pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = MyConvert.ConToInt(Math.Ceiling((double)rowcount / (double)pagesize).ToString());

            switch (select.filter)
            {
                case 2:
                    sortname = "LineDays";
                    sortflag = "asc";
                    break;
                case 3:
                    sortname = "LineDays";
                    sortflag = "desc";
                    break;
                case 4:
                    sortname = "Price";
                    sortflag = "desc";
                    break;
                case 5:
                    sortname = "Price";
                    sortflag = "asc";
                    break;
                default:
                    sortname = "EditTime desc";
                    break;
            }

            string SqlQueryText = "";

            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                List<LineList> lineList = SearchLineList(SqlQueryText);
                LineListRS rs = new LineListRS();
                rs.lineList = lineList;
                rs.pageCount = PageCount;
                rs.currpage = currpage;
                infos = JsonMapper.ToJson(rs);
            }    
            return infos;
        }

        public static List<LineList> SearchLineList(string SqlQueryText)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            List<LineList> lineInfoList = new List<LineList>();
            if (DS.Tables[0].Rows.Count > 0)
            {
                string Pics = "/Images/none.gif";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    LineList lineInfo = new LineList();
                    try
                    {
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    }
                    catch
                    { }
                    lineInfo.MisLineId = MyConvert.ConToInt(DS.Tables[0].Rows[i]["MisLineId"].ToString());
                    lineInfo.LineName = DS.Tables[0].Rows[i]["LineName"].ToString();
                    lineInfo.Price = MyConvert.ConToDec(DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", ""));
                    lineInfo.wwwyh = MyConvert.ConToInt(DS.Tables[0].Rows[i]["wwwyh"].ToString());
                    lineInfo.Pics = Pics;
                    lineInfo.Preferences = MyConvert.ConToInt(DS.Tables[0].Rows[i]["Preferences"].ToString());
                    lineInfo.LineDays = MyConvert.ConToInt(DS.Tables[0].Rows[i]["LineDays"].ToString());
                    lineInfo.LineFeature = DS.Tables[0].Rows[i]["LineFeature"].ToString();
                    string destid = DS.Tables[0].Rows[i]["Destinationid"].ToString();
                    if (destid != null && destid.Length > 2)
                    {
                        destid = destid.Substring(1, destid.Length - 2);
                        string tradingAreaListInfos = Convert.ToString(HttpContext.Current.Cache["WeChat_TradingAreaList_" + destid]);
                        if (tradingAreaListInfos == "")
                        {
                            string SqlQueryText1 = string.Format("select * from OL_TradingArea where destid in ({0})", destid);
                            DataSet DS1 = new DataSet();
                            DS1.Clear();
                            DS1 = MyDataBaseComm.getDataSet(SqlQueryText1);
                            if (DS1.Tables[0].Rows.Count > 0)
                            {
                                List<TradingArea> tradingAreaList = new List<TradingArea>();
                                for (int j = 0; j < DS1.Tables[0].Rows.Count; j++)
                                {
                                    TradingArea tradingArea = new TradingArea();
                                    tradingArea.id = MyConvert.ConToInt(DS1.Tables[0].Rows[j]["id"].ToString());
                                    tradingArea.name = DS1.Tables[0].Rows[j]["name"].ToString();
                                    tradingAreaList.Add(tradingArea);
                                }
                                lineInfo.tradingArea = tradingAreaList;
                                tradingAreaListInfos = JsonMapper.ToJson(tradingAreaList);
                                HttpContext.Current.Cache.Insert("WeChat_TradingAreaList_" + destid, tradingAreaListInfos);
                            }
                        }
                        else
                        {
                            List<TradingArea> tradingAreaList = JsonMapper.ToObject<List<TradingArea>>(tradingAreaListInfos);
                            lineInfo.tradingArea = tradingAreaList;
                        }
                    }
                    
                    
                    lineInfoList.Add(lineInfo);
                }
            }
            return lineInfoList;
        }

        public static string GetLineDetail(string lineid) {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_FreeLineDetail_" + lineid]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", lineid);
                string Days = "";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string FirstPic = "/images/none.gif";
                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) FirstPic = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    List<string> picList = new List<string>();
                    Days = DS.Tables[0].Rows[0]["LineDays"].ToString();
                    LineDetail lineDetail = new LineDetail();
                    lineDetail.Id = DS.Tables[0].Rows[0]["Id"].ToString();
                    lineDetail.LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                    switch(DS.Tables[0].Rows[0]["LineType"].ToString()){
                        case "inLand":
                            lineDetail.LineType = "国内旅游";
                            break;
                        case "FreeTour":
                            lineDetail.LineType = "自由行";
                            break;
                        case "OutBound":
                            lineDetail.LineType = "出境旅游";
                            break;
                        default:
                            lineDetail.LineType = "自由行";
                            break;
                    }
                    lineDetail.LineDays = MyConvert.ConToInt(Days);
                    lineDetail.LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                    lineDetail.MisLineId = MyConvert.ConToInt(DS.Tables[0].Rows[0]["MisLineId"].ToString());
                    lineDetail.Price = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", ""));
                    string pdates = DS.Tables[0].Rows[0]["Pdates"].ToString();
                    if (pdates.Length > 24) pdates = pdates.Substring(0, 23) + "...";
                    lineDetail.Destination = DS.Tables[0].Rows[0]["Destination"].ToString();
                    lineDetail.Destinationid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    getTradingAreaInfo(lineDetail);//获取商圈信息
                    lineDetail.Pdates = pdates.Replace(",", "、");
                    picList.Add(FirstPic);
                    //解析行程信息
                    string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, lineid);
                    StringBuilder PicUrl = new StringBuilder();
                    if (System.IO.File.Exists(path) == true)
                    {
                        XmlDocument XmlDoc = new XmlDocument();
                        XmlDoc.Load(path);
                        XmlNode x = XmlDoc.SelectSingleNode("//Route");
                        if (x != null)
                        {
                            if (x.SelectSingleNode("Feature") != null)
                            {
                                string LineFeatureStr = x.SelectSingleNode("Feature").InnerText.Replace("\n  \n", "\n\n");
                                string[] lineFeatures = LineFeatureStr.Split(new string[] { "\n\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                                if (lineFeatures.Length >= 0)
                                {
                                    string Feature = lineFeatures[0].Replace("\n", "</p><p>");
                                    lineDetail.Feature = Feature;
                                    foreach (string str in lineFeatures)
                                    {
                                        if (str.Contains("目的地概览"))
                                        {
                                            string[] destinfos = str.Split('：');
                                            if (destinfos.Length > 1)
                                            {
                                                lineDetail.DestinationInfo = "<p>" + destinfos[1].Replace("\n", "</p><p>") + "</p>"; 
                                            }
                                        }
                                        if (str.Contains("交通信息"))
                                        {
                                            string[] trafficlinfos = str.Split(new string[] { "：\n" }, StringSplitOptions.RemoveEmptyEntries);
                                            List<TrafficInfo> trafficInfoList = new List<TrafficInfo>();
                                            if (trafficlinfos.Length > 1)
                                            {
                                                string[] traffics = trafficlinfos[1].Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                for (var j = 0; j < traffics.Count(); j++)
                                                {
                                                    TrafficInfo traffic = new TrafficInfo();
                                                    string[] trafficStr = traffics[j].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                    traffic.name = trafficStr[0];
                                                    traffic.from = trafficStr[1].Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                                    traffic.to = trafficStr[1].Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)[1];
                                                    traffic.context = trafficStr[2];
                                                    trafficInfoList.Add(traffic);
                                                }

                                                lineDetail.TrafficInfo = trafficInfoList;
                                            }
                                        }
                                        if (str.Contains("酒店信息"))
                                        {
                                            string[] hotelinfos = str.Split(new string[] { "：\n" }, StringSplitOptions.RemoveEmptyEntries);
                                            List<string> hotellist = new List<string>();
                                            if (hotelinfos.Length > 1)
                                            {
                                                string hotelstr = hotelinfos[1];
                                                string[] hotels = hotelstr.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
                                                
                                                for (int i = 0; i < hotels.Length; i++)
                                                {
                                                    string hotelinfostr = hotels[i].Replace("\n",";;");
                                                    hotellist.Add(hotelinfostr);
                                                }
                                                lineDetail.hotelInfo = hotellist;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string Feature = LineFeatureStr.Replace("\n", "</p><p>");
                                    lineDetail.Feature = string.Format("<p>{0}</p>", Feature);
                                }
                                
                                
                            }
                            if (x.SelectSingleNode("PriceIn") != null)
                            {
                                string PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</p><p>");
                                lineDetail.PriceIn = string.Format("<p>{0}</p>", PriceIn);
                            }
                            if (x.SelectSingleNode("PriceOut") != null)
                            {
                                string PriceOut = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "</p><p>");
                                lineDetail.PriceOut = string.Format("<p>{0}</p>", PriceOut);
                            }
                            if (x.SelectSingleNode("OwnExpense") != null)
                            {
                                string OwnExpense = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "</p><p>");
                                lineDetail.OwnExpense = string.Format("<p>{0}</p>", OwnExpense);
                            }
                            if (x.SelectSingleNode("Attentions") != null)
                            {
                                string Attentions = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</p><p>");
                                lineDetail.Attentions = string.Format("<p>{0}</p>", Attentions);
                            }
                            if (x.SelectSingleNode("Shopping") != null)
                            {
                                string Shopping = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "</p><p>");
                                lineDetail.Shopping = string.Format("<p>{0}</p>", Shopping);
                            }

                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            List<RouteInfo> routeInfoList = new List<RouteInfo>();
                            for (int i = 0; i < elemList.Count; i++)
                            {
                                RouteInfo routeInfo = new RouteInfo();
                                string picpath, pic2;
                                if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 10 && i < 6)
                                {
                                    pic2 = "/Images/none.gif";
                                    try
                                    {
                                        picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, elemList[i].SelectSingleNode("Pics").InnerText);
                                        pic2 = string.Format("/Images/Views/{0}/M_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                        if (System.IO.File.Exists(picpath) == true)
                                        {
                                            routeInfo.pic = pic2;
                                            picList.Add(pic2);
                                        }
                                    }
                                    catch
                                    { }
                                }
                                if (elemList[i].SelectSingleNode("daterank") != null)
                                {
                                    routeInfo.daterank = elemList[i].SelectSingleNode("daterank").InnerText;
                                }
                                if (elemList[i].SelectSingleNode("route") != null)
                                {
                                    routeInfo.route = elemList[i].SelectSingleNode("route").InnerText;
                                }
                                if (elemList[i].SelectSingleNode("rname") != null)
                                {
                                    routeInfo.rname = elemList[i].SelectSingleNode("rname").InnerText;
                                }
                                if (elemList[i].SelectSingleNode("bus") != null)
                                {
                                    routeInfo.bus = elemList[i].SelectSingleNode("bus").InnerText;
                                }
                                if (elemList[i].SelectSingleNode("dinner") != null)
                                {
                                    routeInfo.dinner = elemList[i].SelectSingleNode("dinner").InnerText;
                                }
                                if (elemList[i].SelectSingleNode("room") != null)
                                {
                                    routeInfo.room = elemList[i].SelectSingleNode("room").InnerText;
                                }
                                routeInfoList.Add(routeInfo);
                            }
                            lineDetail.routeInfoList = routeInfoList;
                            lineDetail.Pics = picList;
                        }
                    }
                    infos = JsonMapper.ToJson(lineDetail);
                    HttpContext.Current.Cache.Insert("WeChat_FreeLineDetail_" + lineid, infos);
                }
            }
            return infos;
        }

        private static void getTradingAreaInfo(LineDetail lineDetail)
        {
            string destid = lineDetail.Destinationid;
            if (destid != ""&& null!=destid)
            {
                destid = destid.Substring(1, destid.Length - 2);
                string tradingAreaId = "";
                string SqlQueryText = string.Format("select * from OL_TradingArea where destid in ({0})", destid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    List<TradingArea> tradingAreaList = new List<TradingArea>();
                    
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        TradingArea tradingArea = new TradingArea();
                        tradingArea.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                        if (i == DS.Tables[0].Rows.Count-1)
                        {
                            tradingAreaId = tradingAreaId + tradingArea.id;
                        }
                        else
                        {
                            tradingAreaId = tradingAreaId + tradingArea.id + ",";
                        }
                        
                        tradingArea.name = DS.Tables[0].Rows[i]["name"].ToString();
                        tradingArea.detail = DS.Tables[0].Rows[i]["detail"].ToString();
                        tradingArea.destid = DS.Tables[0].Rows[i]["destid"].ToString();
                        tradingArea.flag = DS.Tables[0].Rows[i]["flag"].ToString();
                        tradingArea.pic = DS.Tables[0].Rows[i]["pic"].ToString();
                        tradingAreaList.Add(tradingArea);
                    }
                    lineDetail.tradingArea = tradingAreaList;
                }
                if (tradingAreaId != null && tradingAreaId != "")
                {
                    SqlQueryText = string.Format("select * from OL_TActivity where tradingAreaId in ({0})", tradingAreaId.ToString());
                    DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        List<TActivity> activityList = new List<TActivity>();
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            TActivity activity = new TActivity();
                            activity.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                            activity.tradingAreaId = DS.Tables[0].Rows[i]["tradingAreaId"].ToString();
                            activity.name = DS.Tables[0].Rows[i]["name"].ToString();
                            activity.context = DS.Tables[0].Rows[i]["context"].ToString();
                            activity.color = DS.Tables[0].Rows[i]["color"].ToString();
                            activity.key = DS.Tables[0].Rows[i]["title"].ToString();
                            activityList.Add(activity);
                        }
                        lineDetail.activity = activityList;
                    }

                    SqlQueryText = string.Format("select * from OL_TCoupon where tradingAreaId in ({0})", tradingAreaId.ToString());
                    DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        List<TCoupon> couponList = new List<TCoupon>();
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            TCoupon coupon = new TCoupon();
                            coupon.id = MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString());
                            coupon.barCode = DS.Tables[0].Rows[i]["barCode"].ToString();
                            if (coupon.barCode != null && coupon.barCode != "")
                            {
                                coupon.barCodeImg = TradingAreaService.createBarCode(coupon.barCode);
                            }
                            coupon.context = DS.Tables[0].Rows[i]["context"].ToString();
                            coupon.tradingAreaId = DS.Tables[0].Rows[i]["tradingAreaId"].ToString();
                            coupon.endDate = Convert.ToDateTime(DS.Tables[0].Rows[i]["endDate"]);
                            coupon.starDate = Convert.ToDateTime(DS.Tables[0].Rows[i]["starDate"]);
                            couponList.Add(coupon);
                        }
                        lineDetail.coupon = couponList;
                        TStoreRS rs = new TStoreRS();
                        rs.tradingAreaId = tradingAreaId.ToString();
                        List<TStore> storeList = TradingAreaService.getTStore(rs);
                        lineDetail.store = storeList;
                    }
                }
                }
                
        }
	}
}