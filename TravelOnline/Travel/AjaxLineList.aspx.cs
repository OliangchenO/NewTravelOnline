using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using TravelOnline.Class.Travel;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using System.Text.RegularExpressions;

namespace TravelOnline.Travel
{
    public partial class AjaxLineList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.QueryString["action"])
            {
                case "LoadLineList":
                    LoadLineList();
                    break;
                case "LoadNewsList":
                    LoadNewsList();
                    break;
                case "LoadJournalList":
                    LoadJournalList();
                    break;
                case "LoadCruisesList":
                    LoadCruisesList();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }

        protected void LoadJournalList()
        {
            //AjaxLineList.aspx?action=LoadLineList&Topic=101&Area=&Price=&Days=&Sort=&Pages= RowCount  LineType
            // 增加字段Pdates nvarchar20  Preferences 默认值改为0
            //UPDATE OL_Line SET  AreaId = ',' + AreaId
            //UPDATE OL_Line SET  Preferences = 0 where Preferences is null
            string fieldlist = "id,title,inputdate";
            string condition = " Flag='1'";
            string pkey = "id";
            string sort = "";
            string sortname = "inputdate";
            sort = "desc";
            string tablename = "OL_Journal";
            int pagesize = 20;
            int currpage = MyConvert.ConToInt(Request.QueryString["Pages"]);
            int rowcount = MyConvert.ConToInt(Request.QueryString["RowCount"]);
            if (rowcount == 0) rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            //int a = 5; int b = 2; lbl.Text = Convert.ToString(Math.Ceiling((double)a / (double)b)); 
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            string TopPages = PageNumbersCreate.CreateTopPage(rowcount, currpage, PageCount);
            string BottomPages = PageNumbersCreate.CreateBottomPage(rowcount, currpage, PageCount);

            string LineLst = "<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>";
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                LineLst = LineListPageSerch.GetJournalPageList(SqlQueryText);
            }

            StringBuilder Strings = new StringBuilder();
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"rows\":{0},\"content\":\"{1}\",\"bottompages\":\"{2}\"", rowcount, LineLst, BottomPages));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }

        protected void LoadCruisesList()
        {
            string PlanId = Request.QueryString["PlanId"];
            StringBuilder RoomString = new StringBuilder();
            StringBuilder Strings = new StringBuilder();

            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            CruisesPlan Cruises = new CruisesPlan();

            Cruises = rsp.GetPlanCruisesRoom(UpPassWord, PlanId);
            if (Cruises.CruisesRooms != null)
            {
                RoomString.Append(string.Format("<div id=\\\"{0}\\\">", Cruises.PlanId));
                RoomString.Append("<div class=\\\"mc tabcon borders01\\\">");
                //RoomString.Append("<ul id=SellPrice_ class=Cruises>");
                RoomString.Append(string.Format("<ul id=SellPrice_{0} class=Cruises>", Cruises.PlanId));
                RoomString.Append("<li class=cur><div class=ttype>类型</div><div class=tname>名称</div><div class=tsname>配置</div><div class=tprice>价格</div><div class=tnum>预订</div><div class=tpic></div></li>");
                int rooms, beds;

                int colornum = 1;
                string color;
                for (int r = 0; r < Cruises.CruisesRooms.Length; r++)
                {
                    rooms = MyConvert.ConToInt(Cruises.CruisesRooms[r].RoomNum);
                    beds = MyConvert.ConToInt(Cruises.CruisesRooms[r].RoomBed);
                    if (rooms > 12) rooms = 12;
                    if (rooms < 0) rooms = 0;

                    if (r > 0)
                    {
                        if (Cruises.CruisesRooms[r].RoomType != Cruises.CruisesRooms[r-1].RoomType)
                        {
                            if (colornum == 1)
                            {
                                colornum = 2;
                            }
                            else
                            {
                                colornum = 1;
                            }
                        }
                    }
                    color = "color" + colornum;

                    if (rooms > 0)
                    {
                        RoomString.Append(string.Format("<li class=\\\"priceli {5}\\\" tps=roomlist roomid={0} id={1} rooms={2} beds={3} price={4}>", Cruises.CruisesRooms[r].CruisesId, Cruises.CruisesRooms[r].PriceId, rooms, beds, Cruises.CruisesRooms[r].RoomPrice, color));
                        RoomString.Append(string.Format("<div class=ftype>{0}</div><div class=fname>{1}</div>", Cruises.CruisesRooms[r].RoomType, Cruises.CruisesRooms[r].RoomName));
                        RoomString.Append(string.Format("<div class=fsname>{0}</div><div class=fprice>&yen;<span class=sellprice>{1}</span></div>", Cruises.CruisesRooms[r].RoomStand, Cruises.CruisesRooms[r].RoomPrice));
                        RoomString.Append(string.Format("<div class=fnum><select class=psel>{0}</select>间 &nbsp;<select class=ddlnums>{1}</select>成人 &nbsp;<select class=ddlnums>{1}</select>儿童</div><div id=pic class=fnpic></div></li>", GetDropList(rooms), GetDropList(rooms * beds)));//
                    }
                    //RoomString.Append("<li class=priceli tps=SellPrice tag=125750 id=P125750><div class=ftype>内仓</div><div class=fname>舱位Q二人间</div><div class=fsname>人数：2人<br>面积：20平米<br>楼层：甲板2，3，4，5层</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value=\"0\">0</option></select>间 &nbsp;<select class=psel><option value=\"0\">0</option></select>成人 &nbsp;<select class=psel><option value=\"0\">0</option></select>儿童</div><div id=pic class=fnpic></div></li>");
                }
                RoomString.Append("</ul>");
                RoomString.Append("</div>");
                //RoomString.Append(string.Format("<div><a href=\"javascript:void(0);\" class=ShowMore onclick=\"ShowMoreCruises('{0}')\"><img src=\"/images/mbi_down.gif\">查看更多舱位...</a></div>", Cruises.PlanId));
                RoomString.Append("</div>");
            }

            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"content\":\"{0}\"", RoomString.ToString()));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }

        protected static string GetDropList(int nums)
        {
            StringBuilder Strings = new StringBuilder();
            for (int i = 0; i <= nums; i++)
            {
                Strings.Append(string.Format("<option value=\\\"{0}\\\">{0}</option>", i));
            }
            return Strings.ToString();
        }

        protected void LoadLineList()
        {
            //AjaxLineList.aspx?action=LoadLineList&Topic=101&Area=&Price=&Days=&Sort=&Pages= RowCount  LineType
            // 增加字段Pdates nvarchar20  Preferences 默认值改为0
            //UPDATE OL_Line SET  AreaId = ',' + AreaId
            //UPDATE OL_Line SET  Preferences = 0 where Preferences is null
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}' and ", DateTime.Today.ToString()));
            if (MyConvert.ConToInt(Request.QueryString["Topic"]) != 0) Strings.Append(string.Format("Topic='{0}' and ", Request.QueryString["Topic"]));
            int days = MyConvert.ConToInt(Request.QueryString["Days"]);
            if (days != 0)
            {
                if (days == 11)
                {
                    Strings.Append(string.Format("LineDays >= '{0}' and ", days));
                }
                else
                {
                    Strings.Append(string.Format("LineDays = '{0}' and ", days));
                }                   
                    
            }

            if (Request.QueryString["LineType"] != "") Strings.Append(string.Format("LineType = '{0}' and ", Request.QueryString["LineType"]));
            if (MyConvert.ConToInt(Request.QueryString["LineClass"]) != 0) Strings.Append(string.Format("LineClass='{0}' and ", Request.QueryString["LineClass"]));

            if (MyConvert.ConToInt(Request.QueryString["VisaId"]) != 0) Strings.Append(string.Format("Standard = '{0}' and ", Request.QueryString["VisaId"]));
            
            if (MyConvert.ConToInt(Request.QueryString["Area"]) != 0) Strings.Append(string.Format("AreaId like '%,{0},%' and ", Request.QueryString["Area"]));

            if (MyConvert.ConToDate(Request.QueryString["pdate"]) != "null")
            {
                DateTime begindate = Convert.ToDateTime(Request.QueryString["pdate"]);
                Strings.Append(string.Format("Pdates like '%{0}/{1}%' and ", begindate.ToString("MM"), begindate.ToString("dd")));            
            }

            if (Request.QueryString["KeyWord"] != "")
            {
                string keyword = Request.QueryString["KeyWord"];
                keyword = keyword.Replace("#", "");
                switch (keyword)
                {
                    case "InLand_NewYear":
                        Strings.Append("LineType='InLand' and Tags like '%,1108,%' and LineDays IN (2, 3) and ");
                        break;
                    case "Topic":
                        break;
                    case "Third":
                        string ThirdType = Request.QueryString["ThirdType"];
                        Strings.Append("Tags like '%," + ThirdType + ",%' and ");
                        break;
                    default:
                        if (keyword == "") Strings.Append(string.Format("1=2 and ", keyword));
                        string area = MyDataBaseComm.getScalar(string.Format("select top 1 MisClassId from OL_ProductClass where ClassLevel='3' and ProductName='{0}'", keyword));
                        if (area != null)
                        {
                            Strings.Append(string.Format("(AreaId like '%,{0},%' or LineName like '%{1}%') and ", area, keyword));
                        }
                        else
                        {
                            if (MyConvert.ConToInt(keyword) == 0)
                            {
                                Strings.Append(string.Format("LineName like '%{0}%' and ", keyword));
                            }
                            else
                            {
                                Strings.Append(string.Format("MisLineId = '{0}' and ", keyword));
                            }

                        }
                        break;
                }
            }

            switch (Request.QueryString["Price"])
            {
                case "0":
                    break;
                case "1":
                    Strings.Append("Price<500 and ");
                    break;
                case "2":
                    Strings.Append("Price>=500 and Price<1000 and ");
                    break;
                case "3":
                    Strings.Append("Price>=1000 and Price<2000 and ");
                    break;
                case "4":
                    Strings.Append("Price>=2000 and Price<4000 and ");
                    break;
                case "5":
                    Strings.Append("Price>=4000 and Price<6000 and ");
                    break;
                case "6":
                    Strings.Append("Price>=6000 and Price<8000 and ");
                    break;
                case "7":
                    Strings.Append("Price>=8000 and Price<10000 and ");
                    break;
                case "8":
                    Strings.Append("Price>=10000 and ");
                    break;
                default:
                    break;
            }
            
            string fieldlist = "*";
            Strings.Append("1=1 ");

            int SortType = MyConvert.ConToInt(Request.QueryString["Sort"]);
            string condition = Strings.ToString();
            string pkey = "id";
            string sort = "";
            string sortname = "Price";
            string tablename = "OL_Line";
            int pagesize = 12;
            int currpage = MyConvert.ConToInt(Request.QueryString["Pages"]);
            int rowcount = MyConvert.ConToInt(Request.QueryString["RowCount"]);
            if (rowcount == 0) rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            //int a = 5; int b = 2; lbl.Text = Convert.ToString(Math.Ceiling((double)a / (double)b)); 
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            switch (SortType)
            {
                case 1:
                    sortname = "Price";
                    sort = "asc";
                    break;
                case 2:
                    sortname = "Price";
                    sort = "desc";
                    break;
                case 3:
                    sortname = "LineDays";
                    break;
                case 4:
                    sortname = "LineDays";
                    sort = "desc";
                    break;
                default:
                    sortname = "pv,id desc";// desc,EditTime desc
                    break;
            }

            string Sorts = LineSortCreate.CreateSort(SortType);
            string TopPages = PageNumbersCreate.CreateTopPage(rowcount, currpage, PageCount);
            string BottomPages = PageNumbersCreate.CreateBottomPage(rowcount, currpage, PageCount);
            string Filter = string.Format("<DIV class=\\\"fore item\\\">排序&nbsp;&nbsp;</DIV>{0}{1}<SPAN class=clr></SPAN><DIV class=extra><SPAN class=clr></SPAN></DIV>", Sorts, TopPages);

            string LineLst = "<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>";
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                LineLst = LineListPageSerch.GetLinesPageList(SqlQueryText);
            }
            //LineLst = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);

            Strings.Clear();
            //Strings.Append(string.Format("select count({0}) from {1} where {2} ", pkey, tablename, condition));
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"rows\":{0},\"filter\":\"{1}\",\"content\":\"{2}\",\"bottompages\":\"{3}\"", rowcount, Filter, LineLst, BottomPages));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }

        protected void LoadNewsList()
        {
            //AjaxLineList.aspx?action=LoadLineList&Topic=101&Area=&Price=&Days=&Sort=&Pages= RowCount  LineType
            // 增加字段Pdates nvarchar20  Preferences 默认值改为0
            //UPDATE OL_Line SET  AreaId = ',' + AreaId
            //UPDATE OL_Line SET  Preferences = 0 where Preferences is null
            string fieldlist = "id,AfficheName,EditTime";            
            string condition = " AfficheFlag='1'";
            string pkey = "id";
            string sort = "";
            string sortname = "EditTime";
            sort = "desc";
            string tablename = "OL_Affiche";
            int pagesize = 20;
            int currpage = MyConvert.ConToInt(Request.QueryString["Pages"]);
            int rowcount = MyConvert.ConToInt(Request.QueryString["RowCount"]);
            if (rowcount == 0) rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            //int a = 5; int b = 2; lbl.Text = Convert.ToString(Math.Ceiling((double)a / (double)b)); 
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            string TopPages = PageNumbersCreate.CreateTopPage(rowcount, currpage, PageCount);
            string BottomPages = PageNumbersCreate.CreateBottomPage(rowcount, currpage, PageCount);
            
            string LineLst = "<SPAN style=\\\"LINE-HEIGHT: 25px; PADDING-LEFT: 50px; color: #009900; font-size: 16px; font-weight: bold;\\\">没有查询到任何数据，请重新搜索！</SPAN>";
            string SqlQueryText = "";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sort, sortname, pagesize, currpage);
                LineLst = LineListPageSerch.GetNewsPageList(SqlQueryText);
            }
            
            StringBuilder Strings = new StringBuilder();
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"rows\":{0},\"content\":\"{1}\",\"bottompages\":\"{2}\"", rowcount, LineLst, BottomPages));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }
        
    }
}