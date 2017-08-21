using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Class.Travel
{
    public class LinePreferences
    {
        public static void CreatePreferencesJs()
        {
            BuildPreferences("Index");
            BuildPreferences("OutBound");
            BuildPreferences("InLand");
            BuildPreferences("FreeTour");
            BuildPreferences("Cruises");
        }

        public static string JournalLine(string FirstDestination)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;
            SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and PV>0 and  Price>0 and PlanDate>='{1}' and Destinationid like ',%{0}%,' order by PV desc, EditTime desc", FirstDestination, DateTime.Today.ToString());
                    
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                Strings.Append(string.Format("<LI><span>{4}</span><div class=p-name><A href=\"/line/{2}.html\" target=_blank>{3}</A></div></LI>", DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"], i + 1));
            }
            return Strings.ToString();
        }

        public static string LineJournal(string FirstDestination)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;
            SqlQueryText = string.Format("select top 9 id,title from OL_Journal where flag='1' and Destinationid like ',%{0}%,' order by id desc", FirstDestination);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            {
                ///News/46.html
                Strings.Append(string.Format("<LI><span>{2}</span><div class=p-name><A href=\"/journal/{1}.html\" target=_blank>{0}</A></div></LI>", DS.Tables[0].Rows[i]["title"], DS.Tables[0].Rows[i]["id"], i + 1));
            }
            return Strings.ToString();
        }

        public static void BuildPreferences(string LineType)
        {
            StringBuilder Strings = new StringBuilder(); //and PlanDate>'{0}' 
            string SqlQueryText;
            if (LineType == "Index")
            {
                SqlQueryText = string.Format("select top 15 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and Preferences=1 and Price>0 and PlanDate>='{0}' order by EditTime desc", DateTime.Today.ToString());
            }
            else
            {
                SqlQueryText = string.Format("select top 15 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and Preferences=1 and LineType='{0}' and Price>0 and PlanDate>='{1}' order by EditTime desc", LineType, DateTime.Today.ToString());
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int rows = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (rows == 0) Strings.Append("[{");
                    string Pics = "/Images/none.gif";
                    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length ==24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    
                    Strings.Append(string.Format("n:\"{0}\",p:\"{1}\",i:\"{2}\",l:\"{3}/{4}/{5}\"", DS.Tables[0].Rows[i]["LineName"], DS.Tables[0].Rows[i]["Price"], Pics, DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"]));
                    if (rows == 2)
                    {
                        Strings.Append("}],");
                    }
                    else
                    {
                        if (i == DS.Tables[0].Rows.Count-1)
                        {
                            Strings.Append("}],");
                        }
                        else
                        {
                            Strings.Append("},{");
                        }
                    }
                    rows++;
                    if (rows == 3) rows = 0;
                }
                SqlQueryText = Strings.ToString();
                SaveScriptToFile.SaveScript(string.Format("var rushdata=[{0}];", SqlQueryText.Substring(0, SqlQueryText.Length - 1)), "Preferences", LineType);
            }
        }

        public static string LineOnHotSale(string LineType)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineOnHotSale{0}", LineType)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                //Strings.Append("<div class=pleft>");//and PlanDate>'{1}' 
                string SqlQueryText;
                if (LineType == "Index")
                {
                    SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and Price>0 and PlanDate>='{0}' order by PV desc, EditTime desc", DateTime.Today.ToString());
                }
                else
                {
                    if (MyConvert.ConToInt(LineType) > 0)
                    {
                        SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and PV>0 and LineClass='{0}' and Price>0 and PlanDate>='{1}' order by PV desc, EditTime desc", LineType, DateTime.Today.ToString());
                    }
                    else
                    {
                        SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineType='{0}' and Price>0 and PlanDate>='{1}' order by PV desc, EditTime desc", LineType, DateTime.Today.ToString());
                    }
                }
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<LI><span>{4}</span><div class=p-name><A href=\"/line/{2}.html\" target=_blank>{3}</A></div></LI>", DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"], i + 1));
                }
                //Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineOnHotSale{0}", LineType), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineOnHotSale{0}", LineType)]);
        }

        public static string LineSendAll(string LineType)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("LineSendAll{0}", LineType)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                //Strings.Append("<div class=pleft>");//and PlanDate>'{1}' 
                string SqlQueryText;
                SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and LineType='{0}' and Price>0 and PlanDate>='{1}' order by EditTime desc", LineType, DateTime.Today.ToString());
                if (LineType == "Index") SqlQueryText = string.Format("select top 9 LineType,LineClass,MisLineId,LineName,Price,Pics from OL_Line where Sale='0' and Price>0 and PlanDate>='{0}' order by EditTime desc", DateTime.Today.ToString());
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<LI><span>{4}</span><div class=p-name><A href=\"/line/{2}.html\" target=_blank>{3}</A></div></LI>", DS.Tables[0].Rows[i]["LineType"], DS.Tables[0].Rows[i]["LineClass"], DS.Tables[0].Rows[i]["MisLineId"], DS.Tables[0].Rows[i]["LineName"], i + 1));
                }
                //Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("LineSendAll{0}", LineType), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("LineSendAll{0}", LineType)]);
        }

        public static string CruisesRouteString(string lineid)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesRouteString{0}", lineid)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                //Strings.Append("<div class=pleft>");//and PlanDate>'{1}' 
                string SqlQueryText;
                SqlQueryText = string.Format("select * from CR_Route where lineid='{0}'", lineid);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<DIV class=tripsItem><SPAN class=terms>第{0}天</SPAN> <SPAN class=port>{1}</SPAN><SPAN class=arrive>{2}&nbsp;</SPAN><SPAN class=startUp>{3}&nbsp;</SPAN></DIV>", DS.Tables[0].Rows[i]["days"], DS.Tables[0].Rows[i]["harbour"], DS.Tables[0].Rows[i]["arrive"], DS.Tables[0].Rows[i]["sail"]));
                }
                //Strings.Append("</div>");
                HttpContext.Current.Cache.Insert(string.Format("CruisesRouteString{0}", lineid), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesRouteString{0}", lineid)]);
        }

        public static string CruisesPicUrl(string shipid)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesPicUrl{0}", shipid)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                StringBuilder Strings1 = new StringBuilder();
                string SqlQueryText = "SELECT * from CR_Pic where shipid='" + shipid + "' and (pictype='ship' or pictype='others')";

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<div align=\"center\"><div id=\"photos\" class=\"galleryview\">");
                    Strings1.Append("<ul class=\"filmstrip\">");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<div class=\"panel\"><img class=img6-4 src=\"{0}\" /><div class=\"panel-overlay\"><h2>{1}</h2></div></div>",
                            DS.Tables[0].Rows[i]["picurl"].ToString(),
                            DS.Tables[0].Rows[i]["cname"].ToString()
                        ));

                        Strings1.Append(string.Format("<li><img width=60 height=40 src=\"/Upload/Cruises/{2}/Thumb_{0}\" alt=\"{1}\" title=\"{1}\" /></li>",
                            DS.Tables[0].Rows[i]["picurl"].ToString().Split("/".ToCharArray())[4],
                            DS.Tables[0].Rows[i]["cname"].ToString(),
                            DS.Tables[0].Rows[i]["shipid"].ToString()
                        ));
                    }
                    Strings1.Append("</ul>");

                    Strings.Append(Strings1.ToString());
                    Strings.Append("</div></div>");
                }
                HttpContext.Current.Cache.Insert(string.Format("CruisesPicUrl{0}", shipid), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesPicUrl{0}", shipid)]);
        }

        public static string NewCruisesPicUrl(string shipid)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("NewCruisesPicUrl{0}", shipid)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                string SqlQueryText = "SELECT * from CR_Pic where shipid='" + shipid + "' and (pictype='ship' or pictype='others')";

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                Strings.Append("<ul>");
                if (DS.Tables[0].Rows.Count > 0)
                {
                    int ranks = 1;

                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        //<li><a href="#1"><img src="/img/1m.jpg" thumb="" alt="图片1来源于网络，版权属于作者" text="图片1更详细的描述文字" /></a></li>
                        Strings.Append(string.Format("<li><a href=\"#{0}\"><img src=\"{1}\" thumb=\"/Upload/Cruises/{4}/Thumb_{2}\" alt=\"{3}\" text=\"{3}\" /></a></li>",
                            ranks + i,
                            DS.Tables[0].Rows[i]["picurl"].ToString(),
                            DS.Tables[0].Rows[i]["picurl"].ToString().Split("/".ToCharArray())[4],
                            DS.Tables[0].Rows[i]["cname"].ToString(),
                            DS.Tables[0].Rows[i]["shipid"].ToString()
                        ));
                    }


                }
                Strings.Append("</ul>");
                HttpContext.Current.Cache.Insert(string.Format("NewCruisesPicUrl{0}", shipid), Strings.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("NewCruisesPicUrl{0}", shipid)]);
        }


        public static string CruisesRoomStringGet(string lineid)
        {
            if (Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesRoomString{0}", lineid)]) == "")
            {
                StringBuilder Strings = new StringBuilder();
                StringBuilder Strings1 = new StringBuilder();
                string recommend = ""; //(nums-sellroom)>=0 and 
                string SqlQueryText = "SELECT * from View_CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' order by typeid";
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    Strings1.Append("<UL class=hoteltab>");
                    //Strings.Append("<table style=\\\"width: 99%;\\\" border=\\\"0\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\"><tr class=tit><td width=\\\"20%\\\">房间类型 Room Type</td><td width=\\\"15%\\\">床型 Bed Type</td><td width=\\\"10%\\\">早餐 Breakfast</td><td width=\\\"10%\\\">宽带 Broadband</td><td class=al width=\\\"12%\\\">挂牌价 List Price</td><td class=al width=\\\"12%\\\">预定价 Order Price</td><td width=\\\"8%\\\">&nbsp;</td></tr>");
                    Strings.Append("<div id=tabdiv>");
                    string T_Price, C_Price;
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        recommend = "";
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["recommend"].ToString()) > 0) recommend = "<IMG class=yltags src=\"/images/Cruises/" + DS.Tables[0].Rows[i]["recommend"].ToString() + ".jpg\">";
                        T_Price = "--";// &yen;
                        C_Price = "--";
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                        if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();
                        if (i == 0)
                        {
                            Strings1.Append(string.Format("<LI class=curr>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                            Strings.Append("<div class=\"tabcon borders\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">&nbsp;</td></tr>");
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price,
                                recommend
                            ));
                            if (i == DS.Tables[0].Rows.Count - 1)
                            {
                                Strings.Append("</table></div>");
                            }
                        }
                        else
                        {
                            if (DS.Tables[0].Rows[i]["typeid"].ToString() != DS.Tables[0].Rows[i - 1]["typeid"].ToString())
                            {
                                Strings1.Append(string.Format("<LI>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                                Strings.Append("</table></div>");
                                //Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"28%\">客舱类型</td><td width=\"13%\">客舱面积</td><td width=\"15%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"12%\">第1、2人价格</td><td class=al width=\"12%\">第3、4人价格</td><td width=\"8%\">&nbsp;</td></tr>");
                                Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">&nbsp;</td></tr>");
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                    DS.Tables[0].Rows[i]["roomname"].ToString(),
                                    DS.Tables[0].Rows[i]["area"].ToString(),
                                    DS.Tables[0].Rows[i]["deck"].ToString(),
                                    DS.Tables[0].Rows[i]["berth"].ToString(),
                                    DS.Tables[0].Rows[i]["price"].ToString(),
                                    T_Price,
                                    DS.Tables[0].Rows[i]["id"].ToString(),
                                    C_Price,
                                    recommend
                                ));
                            }
                            else
                            {
                                Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\" title=\"{0}\">{8}{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                    DS.Tables[0].Rows[i]["roomname"].ToString(),
                                    DS.Tables[0].Rows[i]["area"].ToString(),
                                    DS.Tables[0].Rows[i]["deck"].ToString(),
                                    DS.Tables[0].Rows[i]["berth"].ToString(),
                                    DS.Tables[0].Rows[i]["price"].ToString(),
                                    T_Price,
                                    DS.Tables[0].Rows[i]["id"].ToString(),
                                    C_Price,
                                    recommend
                                ));
                            }
                            if (i == DS.Tables[0].Rows.Count - 1)
                            {
                                Strings.Append("</table></div>");
                            }
                        }
                        
                    }
                    
                    Strings1.Append("</UL>");
                    Strings.Append("</div>");
                    Strings1.Append(Strings.ToString());
                }
                HttpContext.Current.Cache.Insert(string.Format("CruisesRoomString{0}", lineid), Strings1.ToString());
            }
            return Convert.ToString(HttpContext.Current.Cache[string.Format("CruisesRoomString{0}", lineid)]);
        }


        public static string CompanyCruisesRoomGet(string lineid,string companyid)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder Strings1 = new StringBuilder();
            string SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";

            //if (MyConvert.ConToInt(companyid) == 0)
            //{
            //    SqlQueryText = "SELECT * from CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' order by typeid";
            //}
            //else
            //{
            //    SqlQueryText = "SELECT * from CR_RoomAllot where companyid='" + companyid + "' and lineid='" + lineid + "' and allotflag='1' order by typeid";
            //}
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings1.Append("<UL class=hoteltab>");
                Strings.Append("<div id=tabdiv>");
                string T_Price, C_Price;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    T_Price = "--";// &yen;
                    C_Price = "--";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();
                    if (i == 0)
                    {
                        Strings1.Append(string.Format("<LI class=curr>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                        Strings.Append("<div class=\"tabcon borders\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">&nbsp;</td></tr>");
                        Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                            DS.Tables[0].Rows[i]["roomname"].ToString(),
                            DS.Tables[0].Rows[i]["area"].ToString(),
                            DS.Tables[0].Rows[i]["deck"].ToString(),
                            DS.Tables[0].Rows[i]["berth"].ToString(),
                            DS.Tables[0].Rows[i]["price"].ToString(),
                            T_Price,
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            C_Price
                        ));
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Strings.Append("</table></div>");
                        }
                    }
                    else
                    {
                        if (DS.Tables[0].Rows[i]["typeid"].ToString() != DS.Tables[0].Rows[i - 1]["typeid"].ToString())
                        {
                            Strings1.Append(string.Format("<LI>{0}<span></span></LI>", DS.Tables[0].Rows[i]["typename"].ToString()));
                            Strings.Append("</table></div>");
                            //Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"28%\">客舱类型</td><td width=\"13%\">客舱面积</td><td width=\"15%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"12%\">第1、2人价格</td><td class=al width=\"12%\">第3、4人价格</td><td width=\"8%\">&nbsp;</td></tr>");
                            Strings.Append("<div class=\"tabcon borders hide\"><table style=\"width: 100%;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr class=tit><td width=\"25%\">客舱类型</td><td width=\"10%\">客舱面积</td><td width=\"13%\">甲板层</td><td width=\"10%\">最多可住</td><td class=al width=\"11%\">第1、2人价格</td><td class=al width=\"10%\">第3成人价</td><td class=al width=\"10%\">第3儿童价</td><td width=\"10%\">&nbsp;</td></tr>");
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price
                            ));
                        }
                        else
                        {
                            Strings.Append(string.Format("<tr class=tir><td class=al><a class=roomname href=\"javascript:;\" onclick=\"RoomClick({6})\">{0}</a></td><td class=ac>{1}</td><td class=ac>{2}层</td><td class=ac>{3}人</td><td class=al><dfn> &yen;{4}</dfn></td><td class=al><dfn>{5}</dfn></td><td class=al><dfn>{7}</dfn></td><td align=\"center\"><a class=btns href=\"javascript:void(0);\" onclick=\"RoomClick({6})\">选择房间</a></td></tr><tr class=\"hide htr\" id=h{6}><td colspan=\"8\"><div id=show{6}></div></td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["area"].ToString(),
                                DS.Tables[0].Rows[i]["deck"].ToString(),
                                DS.Tables[0].Rows[i]["berth"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                T_Price,
                                DS.Tables[0].Rows[i]["id"].ToString(),
                                C_Price
                            ));
                        }
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Strings.Append("</table></div>");
                        }
                    }

                }

                Strings1.Append("</UL>");
                Strings.Append("</div>");
                Strings1.Append(Strings.ToString());
            }
            else
            {
                Strings1.Append("None");
            }
            return Strings1.ToString();
        }
       


    }
}