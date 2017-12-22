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
using Belinda.Jasp;
using TravelOnline.BLL;
using TravelOnline.Models;
using TravelOnline.NewPage.erp;

namespace TravelOnline.WeChat
{
    public class WeChatClass
    {
        public static string LineInfoStringCreate(string lineid)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_LineDetail_" + lineid]);
            infos = "";
            if (infos == "")
            {
                StringBuilder AllStrings = new StringBuilder();
                StringBuilder Strings = new StringBuilder();
                StringBuilder Pics = new StringBuilder();
                StringBuilder PicUrl = new StringBuilder();
                StringBuilder Routes = new StringBuilder();
                StringBuilder VisaString = new StringBuilder();
                string Days = "";
                string linePrice = "";
                string tuanDiscount = "";
                string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", lineid);
                //return SqlQueryText;
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string FirstPic = "/images/none.gif";
                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) FirstPic = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Days = DS.Tables[0].Rows[0]["LineDays"].ToString();

                    decimal lijian;
                    if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + lineid + ",") > -1)
                    {
                        lijian = MyConvert.ConToDec(ConfigurationManager.AppSettings["specialpfqje"].ToString());
                    }
                    else
                    {
                        lijian = MyConvert.ConToDec(ConfigurationManager.AppSettings["pfqje"].ToString());
                    }

                    //LineFlag 1、2、3  1为正常线路 2为邮轮包船 3为签证
                    string LineFlag = "1";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Shipid"].ToString()) > 0) LineFlag = "2";
                    if (DS.Tables[0].Rows[0]["LineType"].ToString() == "Visa") LineFlag = "3";
                    Strings.Append("<div id=\"inputs_detail\" style=\"DISPLAY:none\">");
                    Strings.Append(string.Format("<input id=\"s_lineflag\" type=\"hidden\" value=\"{0}\"/>", LineFlag));
                    //Strings.Append(string.Format("<script type=\"text/javascript\">{0}</script>", CreatePlanDateJason(lineid)));
                    Strings.Append("</div>");

                    Strings.Append("<div class=\"product_wrap\">");
                    Strings.Append(string.Format("<h3>{0}</h3>", MyConvert.TeShuChar(DS.Tables[0].Rows[0]["LineName"].ToString())));
                    Strings.Append(string.Format("<p class=\"xdesc\">{0}</p>", DS.Tables[0].Rows[0]["LineFeature"].ToString()));
                    Strings.Append(string.Format("<h2>编号:{0}</h2>", DS.Tables[0].Rows[0]["MisLineId"].ToString()));
                    linePrice = DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "");
                    Strings.Append(string.Format("<div class=\"price_box\"><p class=\"price\"><dfn>¥</dfn>{0}<em>起</em></p></div>", DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", ""), lijian.ToString()));
                    string SqlQueryText1 = string.Format("select top 1 * from OL_GroupPlan where MisLineId='{0}'", lineid);
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText1);
                    bool isPintuan = false;
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        isPintuan = true;
                        
                        string orderNums = MyDataBaseComm.getScalar(string.Format("select SUM(OrderNums) from ol_order where LineID='{0}' and PayFlag=1 and GroupOrder=1 and BeginDate='{1}'", lineid, DS1.Tables[0].Rows[0]["GroupDate"].ToString()));
                        string groupNums = DS1.Tables[0].Rows[0]["Num"].ToString();
                        tuanDiscount = DS1.Tables[0].Rows[0]["discount"].ToString();
                        HttpContext.Current.Session["Group_Discount_" + lineid] = tuanDiscount;
                        HttpContext.Current.Session["Group_Date_" + lineid] = string.Format("{0:yyyy-MM-dd}", DS1.Tables[0].Rows[0]["GroupDate"]);
                        
                        int leaveNum = (MyConvert.ConToInt(groupNums) - MyConvert.ConToInt(orderNums))% MyConvert.ConToInt(groupNums);
                        if (leaveNum >= 0)
                        {
                            Strings.Append(string.Format("<div id='g-local-groups'><div class='local-groups-title'>{0}人已参团</div>", MyConvert.ConToInt(orderNums)));
                            Strings.Append(string.Format("<div class='local-group-item'><img class='local-group-img' src='/Images/headImage.jpg'/><div class='local-group-detial'><div class='local-group-detial-row1'>"));
                            if (MyConvert.ConToInt(orderNums)==0)
                            {
                                Strings.Append(string.Format("<span class='local-group-name'>还未有人开团，开来加入</span></div><div class='local-group-detial-row2'><span class='local-group-timer'>还差{0}人</span></div></div>", groupNums));
                            }
                            else
                            {
                                string orderName = MyDataBaseComm.getScalar(string.Format("select top 1 OrderName from ol_order where LineID='{0}' and PayFlag=1 order by OrderTime desc", lineid));
                                Strings.Append(string.Format("<span class='local-group-name'>{0} 等已参团</span></div><div class='local-group-detial-row2'><span class='local-group-timer'>还差{1}人</span></div></div>", orderName, leaveNum));
                            }
                            
                            Strings.Append(string.Format("<div class='local-group-btn-border BeginOrder'>去参团</div></div></div>"));
                        }
                        
                    }

                    if (null != HttpContext.Current.Session["Fx_UserId"])
                    {
                        Strings.Append(string.Format("<div class='shopInfo'>"));
                        if (null != HttpContext.Current.Session["Fx_Storename"])
                        {
                            Strings.Append(string.Format("<p>{0}</p>", HttpContext.Current.Session["Fx_Storename"].ToString()));
                        }
                        else
                        {
                            Strings.Append(string.Format("<p>分销店铺</p>"));
                        }
                        Strings.Append(string.Format("<a href='/WeChat/main_fx.aspx'>进店逛逛</a>"));
                        Strings.Append("</div>");
                    }

                    string pdates = DS.Tables[0].Rows[0]["Pdates"].ToString();


                    switch (LineFlag)
                    {
                        case "2":
                            if (pdates.Length > 24) pdates = pdates.Substring(0, 23) + "...";
                            Strings.Append("<div class=\"corp_box\">");
                            Strings.Append(string.Format("<a href=\"javascript:;\"><i class=\"fa fa-calendar\"></i> 出发日期: {0}</a>", pdates.Replace(",", "、")));
                            Strings.Append("</div>");
                            break;
                        case "3":

                            break;
                        default:
                            if (pdates.Length > 24) pdates = pdates.Substring(0, 23) + "...";
                            Strings.Append("<div class=\"corp_box showdate\">");
                            Strings.Append(string.Format("<a href=\"javascript:;\"><i class=\"fa fa-calendar\"></i> 出发日期: {0}<i style=\"font-size:16px\" class=\"fa fa-chevron-circle-right pull-right\"></i></a>", pdates.Replace(",", "、")));
                            Strings.Append("</div>");
                            break;
                    }

                    Strings.Append("</div>");

                    //Strings.Append("");
                    //Strings.Append("");

                    string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, lineid);
                    if (System.IO.File.Exists(path) == true)
                    {
                        XmlDocument XmlDoc = new XmlDocument();
                        XmlDoc.Load(path);
                        XmlNode x = XmlDoc.SelectSingleNode("//Route");
                        if (x != null)
                        {
                            //签证信息
                            if (LineFlag == "3")
                            {
                                //特色开始
                                Strings.Append("<div class=\"recommend_wrap\">");
                                if (pdates.Length > 3)
                                {
                                    //线路特色推荐
                                    Strings.Append("<div class=\"recommend_detail\">");
                                    Strings.Append("<div class=\"recommend_txt\">");
                                    Strings.Append("<h3>签证办理说明</h3>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("有效期：{0}&nbsp;&nbsp;&nbsp;&nbsp;停留时间：{1}&nbsp;&nbsp;&nbsp;&nbsp;工作日：{2}", pdates.Split("$".ToCharArray())[0], pdates.Split("$".ToCharArray())[1], pdates.Split("$".ToCharArray())[2]));
                                    Strings.Append("</ul>");
                                    Strings.Append("</div>");
                                    Strings.Append("</div>");
                                }

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
                                Strings.Append("<div class=\"recommend_detail\">");
                                Strings.Append("<div class=\"recommend_txt\">");
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

                                //展示图片
                                Pics.Append("<div class=\"addWrap\" style=\"height:240px;\">");
                                string FirstImg = "";
                                if (DS.Tables[0].Rows[0]["Pics"].ToString().Length > 5)
                                {
                                    FirstImg = "/images/shadow/" + DS.Tables[0].Rows[0]["Pics"].ToString();
                                }
                                else
                                {
                                    FirstImg = "/Images/none.gif";
                                }
                                Pics.Append("<div class=\"swipe\" id=\"mySwipe\">");
                                Pics.Append("<div class=\"swipe-wrap\">");
                                Pics.Append("<div><a><img class=\"img-responsive img-h250\" style=\"height:240px\" src=\"" + FirstImg + "\"/></a></div>");
                                Pics.Append("</div></div>");
                                Pics.Append("<ul id=\"position\"><li class=\"cur\"></li>");
                                Pics.Append("</ul>");
                                Pics.Append("</div>");

                                AllStrings.Append(Pics.ToString());
                                AllStrings.Append("<div class=\"clearfix\"><div class=\"portlet-body\" style=\"margin-bottom: 50px;width:100%\">");
                                AllStrings.Append("<div class=\"nav-area\">");
                                AllStrings.Append("<ul class=\"nav nav-tabs\">");
                                AllStrings.Append("<li style=\"width:100%\" class=\"active\">");
                                AllStrings.Append("<a href=\"#tab_1_1\" data-toggle=\"tab\">" + MyConvert.TeShuChar(DS.Tables[0].Rows[0]["LineName"].ToString()) + "</a>");
                                AllStrings.Append("</li>");
                                AllStrings.Append("</ul>");
                                AllStrings.Append("</div>");
                                AllStrings.Append("<div class=\"tab-content\">");
                                AllStrings.Append("<div class=\"tab-pane fade active in\" id=\"tab_1_1\">");
                                AllStrings.Append(Strings.ToString().Replace("<li></li>", ""));
                                AllStrings.Append("</div>");
                                AllStrings.Append("</div>");
                                AllStrings.Append("</div>");

                            }
                            else //常规线路信息
                            {
                                //特色开始
                                Strings.Append("<div class=\"recommend_wrap\">");

                                //线路特色推荐
                                Strings.Append("<div class=\"recommend_detail\">");
                                Strings.Append("<div class=\"recommend_txt\">");
                                Strings.Append("<h3>线路特色推荐</h3>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", x.SelectSingleNode("Feature").InnerText.Replace("\n", "</li><li>")));
                                Strings.Append("</ul>");
                                Strings.Append("</div>");
                                Strings.Append("</div>");


                                //线路特色推荐
                                Strings.Append("<div class=\"recommend_detail\">");
                                Strings.Append("<div class=\"recommend_txt\">");
                                Strings.Append("<h3>费用说明</h3>");
                                string PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</li><li>");
                                if (PriceIn.Length > 5)
                                {
                                    Strings.Append("<div><strong>报价包含</strong></div>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("<li>{0}</li>", PriceIn));
                                    Strings.Append("</ul>");
                                }
                                string PriceOut = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "</li><li>");
                                PriceOut = PriceOut.Replace("<li></li>", "aa");
                                if (PriceOut.Length > 5)
                                {
                                    Strings.Append("<div><strong>报价不含</strong></div>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("<li>{0}</li>", PriceOut));
                                    Strings.Append("</ul>");
                                }
                                string OwnExpense = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "</li><li>");
                                if (OwnExpense.Length > 5)
                                {
                                    Strings.Append("<div><strong>自费项目</strong></div>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("<li>{0}</li>", OwnExpense));
                                    Strings.Append("</ul>");
                                }
                                Strings.Append("</div>");
                                Strings.Append("</div>");


                                //温馨提醒
                                Strings.Append("<div class=\"recommend_detail\">");
                                Strings.Append("<div class=\"recommend_txt\">");
                                Strings.Append("<h3>温馨提醒</h3>");
                                string Attentions = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</li><li>");
                                if (Attentions.Length > 5)
                                {
                                    Strings.Append("<div><strong>注意事项</strong></div>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("<li>{0}</li>", Attentions));
                                    Strings.Append("</ul>");
                                }
                                string Shopping = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "</li><li>");
                                if (Shopping.Length > 5)
                                {
                                    Strings.Append("<div><strong>购物商店</strong></div>");
                                    Strings.Append("<ul>");
                                    Strings.Append(string.Format("<li>{0}</li>", Shopping));
                                    Strings.Append("</ul>");
                                }
                                Strings.Append("</div>");
                                Strings.Append("</div>");


                                Strings.Append("</div>");
                                //特色结束

                                XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                                int PicCount = 0;
                                string route = "";
                                for (int i = 0; i < elemList.Count; i++)
                                {

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
                                                PicUrl.Append(string.Format("<div><a><img class=\"img-responsive img-h250\" style=\"height:240px\" src=\"{0}\"/></a></div>", pic2));
                                                PicCount = PicCount + 1;
                                            }
                                        }
                                        catch
                                        { }

                                    }

                                    Routes.Append("<div class=\"recommend_detail\">");
                                    Routes.Append("<div class=\"recommend_txt route\">");
                                    Routes.Append(string.Format("<h3><i class=\"fa fa-calendar\"></i> {0}</h3>", elemList[i].SelectSingleNode("daterank").InnerText));
                                    Routes.Append(string.Format("<h2><i class=\"fa fa-map-marker\"></i> {0}</h2>", elemList[i].SelectSingleNode("rname").InnerText));
                                    route = elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>");
                                    route = route.Replace("\"", "");
                                    Routes.Append(string.Format("<div class=\"routeinfo\">{0}</div>", route));
                                    Routes.Append(string.Format("<h1><i class=\"glyphicon glyphicon-plane\"></i> 交通：{0}</h1>", elemList[i].SelectSingleNode("bus").InnerText));
                                    Routes.Append(string.Format("<h1><i class=\"glyphicon glyphicon-glass\"></i> 用餐：{0}</h1>", elemList[i].SelectSingleNode("dinner").InnerText));
                                    Routes.Append(string.Format("<h1><i class=\"glyphicon glyphicon-home\"></i> 住宿：{0}</h1>", elemList[i].SelectSingleNode("room").InnerText));
                                    Routes.Append("</div>");
                                    Routes.Append("</div>");

                                }

                                //展示图片
                                Pics.Append("<div class=\"addWrap\" style=\"height:240px;\">");
                                if (PicCount > 0)
                                {
                                    Pics.Append("<div class=\"swipe\" id=\"mySwipe\">");
                                    Pics.Append("<div class=\"swipe-wrap\">");
                                    Pics.Append(PicUrl.ToString());
                                    Pics.Append("</div></div>");
                                    Pics.Append("<ul id=\"position\"><li class=\"cur\"></li>");
                                    for (int i = 0; i < PicCount - 1; i++)
                                    {
                                        Pics.Append("<li></li>");
                                    }
                                    Pics.Append("</ul>");
                                }
                                else
                                {
                                    Pics.Append("<div class=\"swipe\" id=\"mySwipe\">");
                                    Pics.Append("<div class=\"swipe-wrap\">");
                                    Pics.Append(string.Format("<div><a><img class=\"img-responsive img-h250\" style=\"height:240px\" src=\"{0}\"/></a></div>", "/Images/none.gif"));
                                    Pics.Append("</div></div>");
                                    Pics.Append("<ul id=\"position\"><li class=\"cur\"></li>");
                                    Pics.Append("</ul>");
                                }
                                Pics.Append("</div>");

                                AllStrings.Append(Pics.ToString());

                                if (LineFlag == "2")
                                {
                                    AllStrings.Append("<div class=\"clearfix\"></div><div class=\"portlet-body\" style=\"margin-bottom: 50px;width:100%\">");
                                    AllStrings.Append("<div class=\"nav-area\">");
                                    AllStrings.Append("<ul class=\"nav nav-tabs\">");
                                    AllStrings.Append("<li style=\"width:33%\" class=\"active\">");
                                    AllStrings.Append("<a href=\"#tab_1_1\" data-toggle=\"tab\">线路概述</a>");
                                    AllStrings.Append("</li>");
                                    AllStrings.Append("<li style=\"width:33%\" >");
                                    AllStrings.Append("<a href=\"#tab_1_2\" data-toggle=\"tab\">" + Days + "天行程</a>");
                                    AllStrings.Append("</li>");
                                    AllStrings.Append("<li style=\"width:34%\" >");
                                    AllStrings.Append("<a href=\"#tab_1_3\" data-toggle=\"tab\">舱型及价格</a>");
                                    AllStrings.Append("</li>");
                                    AllStrings.Append("</ul>");
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("<div class=\"tab-content\">");

                                    AllStrings.Append("<div class=\"tab-pane fade active in\" id=\"tab_1_1\">");
                                    AllStrings.Append(Strings.ToString().Replace("<li></li>", ""));
                                    AllStrings.Append("</div>");

                                    AllStrings.Append("<div class=\"tab-pane fade\" id=\"tab_1_2\">");
                                    AllStrings.Append("<div class=\"recommend_wrap\">");
                                    AllStrings.Append(Routes.ToString());
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");

                                    AllStrings.Append("<div class=\"tab-pane fade\" id=\"tab_1_3\">");
                                    AllStrings.Append("<div class=\"recommend_wrap\">");
                                    AllStrings.Append(CreateRoomList(lineid));
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");

                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");
                                }
                                else
                                {
                                    AllStrings.Append("<div class=\"clearfix\"></div><div class=\"portlet-body\" style=\"margin-bottom: 50px;width:100%\">");
                                    AllStrings.Append("<div class=\"nav-area\">");
                                    AllStrings.Append("<ul class=\"nav nav-tabs\">");
                                    AllStrings.Append("<li style=\"width:50%\" class=\"active\">");
                                    AllStrings.Append("<a href=\"#tab_1_1\" data-toggle=\"tab\">线路概述</a>");
                                    AllStrings.Append("</li>");
                                    AllStrings.Append("<li style=\"width:50%\" >");
                                    AllStrings.Append("<a href=\"#tab_1_2\" data-toggle=\"tab\">" + Days + "天行程</a>");
                                    AllStrings.Append("</li>");
                                    AllStrings.Append("</ul>");
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("<div class=\"tab-content\">");
                                    AllStrings.Append("<div class=\"tab-pane fade active in\" id=\"tab_1_1\">");
                                    AllStrings.Append(Strings.ToString().Replace("<li></li>", ""));
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("<div class=\"tab-pane fade\" id=\"tab_1_2\">");
                                    AllStrings.Append("<div class=\"recommend_wrap\">");
                                    AllStrings.Append(Routes.ToString());
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");
                                    AllStrings.Append("</div>");
                                }
                            }
                        }
                    }
                    if (isPintuan == true)
                    {
                        AllStrings.Append("<div class='goods-bottom-bar'><div class='goods-home-button'><a href='/app/main'><span>首页</span></a></div>");
                        AllStrings.Append("<div class='goods-unlike-button' id='share'><span>分享</span></div>");
                        AllStrings.Append("<div class='goods-chat-button'><a href='tel:4006777666'><span>咨询</span></a></div>");
                        decimal tuanPrice = (MyConvert.ConToDec(linePrice) - MyConvert.ConToDec(tuanDiscount));
                        AllStrings.Append(string.Format("<div class='goods-direct-btn'><span class='goods-buy-price'><i>￥</i>{0}</span><span>单独购买</span></div>", linePrice));
                        AllStrings.Append(string.Format("<div class='goods-group-btn BeginOrder'><span class='goods-buy-price'><i>￥</i>{0}</span><span>一键参团</span></div></div>", tuanPrice));
                    }
                    else
                    {
                        AllStrings.Append("<div class='clearfix'></div><div class='pre-footer order-footer' style='position: fixed; bottom: -1px; left: 0px;width:101%'><div class='container'><div class='row'>");
                        if (HttpContext.Current.Session["Fx_Storename"] != null)
                        {
                            AllStrings.Append(string.Format("<div class='col-xs-6' style='text-align:center'><a class='yd cur' href='tel:{0}'><i class='fa fa-phone-square'></i> 电话咨询</a></div>", HttpContext.Current.Session["Fx_Storename"].ToString()));
                        }
                        else
                        {
                            AllStrings.Append("<div class='col-xs-6' style='text-align:center'><a class='yd cur' href='tel:4006777666' ><i class='fa fa-phone-square'></i> 电话咨询</a></div>");
                        }
                        AllStrings.Append("<div class='col-xs-6 BeginOrder' style='text-align:center'><a class='yd cur' href='javascript:;'><i class='fa fa-shopping-cart'></i> 开始预订</a></div></div></div></div>");
                    }
                }

                infos = AllStrings.ToString();
                //infos = "aasfa";
                AllStrings.Clear();
                Pics.Clear();
                Strings.Clear();
                Routes.Clear();
                PicUrl.Clear();
                HttpContext.Current.Cache.Insert("WeChat_LineDetail_" + lineid, infos);
            }
            return infos;
        }

        #region LineInfoStringCreate_New
        public static object LineInfoStringCreate_New(string lineid)
        {
            JSONObject ObJson = new JSONObject();
            string Days = "";
            string sql = string.Format("select top 1 * from tbLine where ErpID='{0}'", lineid);
            DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("LineFlag", typeof(string)));
                string FirstPic = "/images/none.gif";
                if (dt.Rows[0]["PhotoPath"].ToString().Length == 24)
                    FirstPic = string.Format("/Images/Views/{0}/S_{1}", dt.Rows[0]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[0]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);
                Days = dt.Rows[0]["Days"].ToString();
                //LineFlag 1、2、3  1为正常线路 2为邮轮包船 3为签证
                dt.Rows[0]["LineFlag"] = "1";
                if (dt.Rows[0]["Types"].ToString() == "Visa") dt.Rows[0]["LineFlag"] = "3";
                dt.Rows[0]["Cname"] = MyConvert.TeShuChar(dt.Rows[0]["Cname"].ToString());
                dt.Rows[0]["Price"] = dt.Rows[0]["Price"].ToString().Replace(".00", "");
                dt.Rows[0]["LineName"] = MyConvert.TeShuChar(dt.Rows[0]["LineName"].ToString());
                JSONArray ArrJson = new JSONArray();
                ArrJson = Data.GetJsonList(dt);
                ObJson.Add("rows", ArrJson);
                ObJson.Add("Fx_UserId", HttpContext.Current.Session["Fx_UserId"]);
                ObJson.Add("Fx_Storename", HttpContext.Current.Session["Fx_Storename"]);

                string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, lineid);
                if (System.IO.File.Exists(path) == true)
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlDoc.Load(path);
                    XmlNode x = XmlDoc.SelectSingleNode("//Route");
                    if (x != null)
                    {
                        DataTable dt3 = new DataTable();
                        dt3.Columns.Add(new DataColumn("Feature", typeof(string)));
                        dt3.Columns.Add(new DataColumn("PriceIn", typeof(string)));
                        dt3.Columns.Add(new DataColumn("PriceOut", typeof(string)));
                        dt3.Columns.Add(new DataColumn("OwnExpense", typeof(string)));
                        dt3.Columns.Add(new DataColumn("Attentions", typeof(string)));
                        dt3.Columns.Add(new DataColumn("Shopping", typeof(string)));
                        DataRow dr2 = dt3.NewRow();
                        dr2["Feature"] = x.SelectSingleNode("Feature").InnerText.Replace("\n", "</li><li>");
                        dr2["PriceIn"] = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</li><li>");
                        dr2["PriceOut"] = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "</li><li>");
                        dr2["OwnExpense"] = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "</li><li>");
                        dr2["Attentions"] = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</li><li>");
                        dr2["Shopping"] = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "</li><li>");
                        dt3.Rows.Add(dr2);
                        JSONArray ArrJson3 = new JSONArray();
                        ArrJson3 = Data.GetJsonList(dt3);
                        ObJson.Add("XmlRoute", ArrJson3);
                        //签证信息
                        if (dt.Rows[0]["LineFlag"].ToString() == "3")
                        {
                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            DataTable dt2 = new DataTable();
                            dt2.Columns.Add(new DataColumn("VisaName", typeof(string)));
                            dt2.Columns.Add(new DataColumn("VisaContent", typeof(string)));
                            dt2.Columns.Add(new DataColumn("V1", typeof(string)));
                            dt2.Columns.Add(new DataColumn("V2", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Flag", typeof(string)));
                            DataRow dr;
                            for (int i = 0; i < elemList.Count; i++)
                            {
                                dr = dt2.NewRow();
                                dr["VisaName"] = elemList[i].SelectSingleNode("VisaName").InnerText;
                                dr["VisaContent"] = elemList[i].SelectSingleNode("VisaContent").InnerText.Replace("\n", "<br>");
                                dr["V1"] = elemList[i].SelectSingleNode("V1").InnerText;
                                dr["V2"] = elemList[i].SelectSingleNode("V2").InnerText;
                                dr["Flag"] = elemList[i].SelectSingleNode("Flag").InnerText;
                                dt2.Rows.Add(dr);
                            }
                            JSONArray ArrJson2 = new JSONArray();
                            ArrJson2 = Data.GetJsonList(dt2);
                            ObJson.Add("XmlRouteInfos", ArrJson2);
                        }
                        else //常规线路信息
                        {
                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            int PicCount = 0;
                            string route = "";
                            DataTable dt4 = new DataTable();
                            DataRow dr3;
                            dt4.Columns.Add(new DataColumn("Pics", typeof(string)));
                            dt4.Columns.Add(new DataColumn("pic2", typeof(string)));
                            dt4.Columns.Add(new DataColumn("daterank", typeof(string)));
                            dt4.Columns.Add(new DataColumn("rname", typeof(string)));
                            dt4.Columns.Add(new DataColumn("route", typeof(string)));
                            dt4.Columns.Add(new DataColumn("bus", typeof(string)));
                            dt4.Columns.Add(new DataColumn("dinner", typeof(string)));
                            dt4.Columns.Add(new DataColumn("room", typeof(string)));
                            dt4.Columns.Add(new DataColumn("ispicpath", typeof(bool)));

                            for (int i = 0; i < elemList.Count; i++)
                            {
                                dr3 = dt4.NewRow();
                                string picpath, pic2 = string.Empty;
                                if (elemList[i].SelectSingleNode("Pics").InnerText.Length > 10 && i < 6)
                                {
                                    dr3["ispicpath"] = false;
                                    pic2 = "/Images/none.gif";
                                    picpath = string.Format(@"{0}\Images\Views\{1}", AppDomain.CurrentDomain.BaseDirectory, elemList[i].SelectSingleNode("Pics").InnerText);
                                    pic2 = string.Format("/Images/Views/{0}/M_{1}", elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[0], elemList[i].SelectSingleNode("Pics").InnerText.Split("/".ToCharArray())[1]);
                                    if (System.IO.File.Exists(picpath) == true)
                                    {
                                        dr3["ispicpath"] = true;
                                        PicCount = PicCount + 1;
                                    }
                                }

                                dr3["Pics"] = elemList[i].SelectSingleNode("Pics").InnerText;
                                dr3["pic2"] = pic2;
                                dr3["daterank"] = elemList[i].SelectSingleNode("daterank").InnerText;
                                dr3["rname"] = elemList[i].SelectSingleNode("rname").InnerText;
                                dr3["route"] = elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>").Replace("\"", "");
                                dr3["bus"] = elemList[i].SelectSingleNode("bus").InnerText;
                                dr3["dinner"] = elemList[i].SelectSingleNode("dinner").InnerText;
                                dr3["room"] = elemList[i].SelectSingleNode("room").InnerText;
                                dt4.Rows.Add(dr3);
                            }
                            JSONArray ArrJson4 = new JSONArray();
                            ArrJson4 = Data.GetJsonList(dt4);
                            ObJson.Add("XmlRouteInfos", ArrJson4);
                            ObJson.Add("PicCount", PicCount);
                        }
                    }
                }
            }
            return json.SerializeObject(ObJson);
        }
        #endregion

        #region CreateRoomList_New
        public static DataTable CreateRoomList_New(string lineid)
        {
            StringBuilder Rooms = new StringBuilder();
            string sql = "SELECT * from View_CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' and (nums-sellroom)>2 order by typeid";
            DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("T_Price", typeof(string)));
                dt.Columns.Add(new DataColumn("C_Price", typeof(string)));
                dt.Columns.Add(new DataColumn("istypeid", typeof(bool)));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["istypeid"] = false;
                    dt.Rows[i]["T_Price"] = " --";
                    dt.Rows[i]["C_Price"] = " --";
                    if (MyConvert.ConToInt(dt.Rows[i]["thirdprice"].ToString()) > 0) dt.Rows[i]["T_Price"] = " &yen;" + dt.Rows[i]["thirdprice"].ToString();
                    if (MyConvert.ConToInt(dt.Rows[i]["childprice"].ToString()) > 0) dt.Rows[i]["C_Price"] = " &yen;" + dt.Rows[i]["childprice"].ToString();

                    if (i == 0)
                    {
                    }
                    else
                    {
                        if (dt.Rows[i]["typeid"].ToString() != dt.Rows[i - 1]["typeid"].ToString())
                        {
                            dt.Rows[i]["istypeid"] = true;
                        }
                    }
                }

            }
            return dt;
        }
        #endregion

        public static string CreateRoomList(string lineid)
        {
            StringBuilder Rooms = new StringBuilder();
            string SqlQueryText = "SELECT * from View_CR_RoomAllot where lineid='" + lineid + "' and allotflag='0' and (nums-sellroom)>2 order by typeid";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string T_Price, C_Price;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    T_Price = " --";
                    C_Price = " --";
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["thirdprice"].ToString()) > 0) T_Price = " &yen;" + DS.Tables[0].Rows[i]["thirdprice"].ToString();
                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["childprice"].ToString()) > 0) C_Price = " &yen;" + DS.Tables[0].Rows[i]["childprice"].ToString();

                    if (i == 0)
                    {
                        Rooms.Append("<div class=\"recommend_detail\">");
                        Rooms.Append("<div class=\"recommend_txt room\">");
                        Rooms.Append(string.Format("<h3><i class=\"fa fa-anchor\"></i> {0}</h3>", DS.Tables[0].Rows[i]["typename"].ToString()));

                        Rooms.Append(string.Format("<h2><i class=\"fa fa-user\"></i> {0}</h2>", DS.Tables[0].Rows[i]["roomname"].ToString()));
                        Rooms.Append(string.Format("<h1>面积：{0} 甲板：{1} 最多可住：{2}</h1>", DS.Tables[0].Rows[i]["area"].ToString(), DS.Tables[0].Rows[i]["deck"].ToString(), DS.Tables[0].Rows[i]["berth"].ToString()));
                        Rooms.Append(string.Format("<div class=\"roominfo\">第1、2人价格： &yen;{0}<br>第3、4成人价：{1}<br>第3、4儿童价：{2}<br>", DS.Tables[0].Rows[i]["price"].ToString(), T_Price, C_Price));
                        Rooms.Append(string.Format("<input value=\"{0}\" type=\"radio\" id=\"radio-{0}\" name=\"iCheck\"><label for=\"radio-{0}\" class=\"hovers\">预订此舱型</label></div>", DS.Tables[0].Rows[i]["id"].ToString()));

                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Rooms.Append("</div>");
                            Rooms.Append("</div>");
                        }
                    }
                    else
                    {
                        if (DS.Tables[0].Rows[i]["typeid"].ToString() != DS.Tables[0].Rows[i - 1]["typeid"].ToString())
                        {
                            Rooms.Append("</div>");
                            Rooms.Append("</div>");
                            Rooms.Append("<div class=\"recommend_detail\">");
                            Rooms.Append("<div class=\"recommend_txt room\">");
                            Rooms.Append(string.Format("<h3><i class=\"fa fa-anchor\"></i> {0}</h3>", DS.Tables[0].Rows[i]["typename"].ToString()));

                        }
                        Rooms.Append(string.Format("<h2><i class=\"fa fa-user\"></i> {0}</h2>", DS.Tables[0].Rows[i]["roomname"].ToString()));
                        Rooms.Append(string.Format("<h1>面积：{0} 甲板：{1} 最多可住：{2}</h1>", DS.Tables[0].Rows[i]["area"].ToString(), DS.Tables[0].Rows[i]["deck"].ToString(), DS.Tables[0].Rows[i]["berth"].ToString()));
                        Rooms.Append(string.Format("<div class=\"roominfo\">第1、2人价格： &yen;{0}<br>第3、4成人价：{1}<br>第3、4儿童价：{2}<br>", DS.Tables[0].Rows[i]["price"].ToString(), T_Price, C_Price));
                        Rooms.Append(string.Format("<input value=\"{0}\" type=\"radio\" id=\"radio-{0}\" name=\"iCheck\"><label for=\"radio-{0}\" class=\"hovers\">预订此舱型</label></div>", DS.Tables[0].Rows[i]["id"].ToString()));

                        //Rooms.Append(string.Format("<div class=\"roominfo\">{0}</div>", ""));
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Rooms.Append("</div>");
                            Rooms.Append("</div>");
                        }
                    }
                }

            }
            return Rooms.ToString();
        }

        //创建出发日期的jason
        public static string CreatePlanDateJason(string lineid)
        {
            //string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            //TravelOnlineService rsp = new TravelOnlineService();
            //rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            string ss = "";
            try
            {
                //string[] ListInfo = Regex.Split(rsp.WeChatPlanDateCreate(UpPassWord, lineid), @"\@\@", RegexOptions.IgnoreCase);
                string ListInfo = ErpUtil.getWechatTeamInfo(string.Format("{0:yyyy-MM-dd}", DateTime.Now), string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(4)), lineid);
                ss = ListInfo;
                if(null!=HttpContext.Current.Session["Group_Date_"+ lineid])
                {
                    ss = ss + " var Group_Date ='" + HttpContext.Current.Session["Group_Date_" + lineid] + "'; var Group_Discount = " + HttpContext.Current.Session["Group_Discount_" + lineid] +";";　　　　　　　　　　　　　　　
                }
            }
            catch
            {
            }
            return (string.Format("<script type=\"text/javascript\">{0}</script>", ss));
            //return ss;
        }

        //json 目前不使用
        public static string LineInfoCreate(string lineid)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_LineDetail_" + lineid]);
            if (infos == "")
            {
                StringBuilder Strings = new StringBuilder();
                StringBuilder Pics = new StringBuilder();
                StringBuilder PicUrl = new StringBuilder();
                StringBuilder Routes = new StringBuilder();
                string Days = "";
                string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", lineid);
                //return SqlQueryText;
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string FirstPic = "/images/none.gif";
                    if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) FirstPic = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                    Days = DS.Tables[0].Rows[0]["LineDays"].ToString();

                    Strings.Append("<div class=\\\"product_wrap\\\">");
                    Strings.Append(string.Format("<h3>{0}</h3>", MyConvert.TeShuChar(DS.Tables[0].Rows[0]["LineName"].ToString())));
                    Strings.Append(string.Format("<p class=\\\"xdesc\\\">{0}</p>", DS.Tables[0].Rows[0]["LineFeature"].ToString()));
                    Strings.Append(string.Format("<h2>编号:{0}</h2>", DS.Tables[0].Rows[0]["MisLineId"].ToString()));
                    Strings.Append(string.Format("<div class=\\\"price_box\\\"><p class=\\\"price\\\"><dfn>¥</dfn>{0}<em>起</em></p></div>", DS.Tables[0].Rows[0]["Price"].ToString().Replace(".00", "")));
                    Strings.Append("<div class=\\\"corp_box\\\">");
                    string pdates = DS.Tables[0].Rows[0]["Pdates"].ToString();
                    if (pdates.Length > 18) pdates = pdates.Substring(0, 17) + "...";
                    Strings.Append(string.Format("<a href=\\\"javascript:;\\\"><i class=\\\"fa fa-calendar\\\"></i> 出发日期: {0}<i style=\\\"font-size:16px\\\" class=\\\"fa fa-chevron-circle-right pull-right\\\"></i></a>", pdates.Replace(",", "、")));
                    Strings.Append("</div>");
                    Strings.Append("</div>");

                    //Strings.Append("");
                    //Strings.Append("");

                    string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, lineid);
                    if (System.IO.File.Exists(path) == true)
                    {
                        XmlDocument XmlDoc = new XmlDocument();
                        XmlDoc.Load(path);
                        XmlNode x = XmlDoc.SelectSingleNode("//Route");
                        if (x != null)
                        {
                            //特色开始
                            Strings.Append("<div class=\\\"recommend_wrap\\\">");

                            //线路特色推荐
                            Strings.Append("<div class=\\\"recommend_detail\\\">");
                            Strings.Append("<div class=\\\"recommend_txt\\\">");
                            Strings.Append("<h3>线路特色推荐</h3>");
                            Strings.Append("<ul>");
                            Strings.Append(string.Format("<li>{0}</li>", x.SelectSingleNode("Feature").InnerText.Replace("\n", "</li><li>")));
                            Strings.Append("</ul>");
                            Strings.Append("</div>");
                            Strings.Append("</div>");

                            //线路特色推荐
                            Strings.Append("<div class=\\\"recommend_detail\\\">");
                            Strings.Append("<div class=\\\"recommend_txt\\\">");
                            Strings.Append("<h3>费用说明</h3>");
                            string PriceIn = x.SelectSingleNode("PriceIn").InnerText.Replace("\n", "</li><li>");
                            if (PriceIn.Length > 5)
                            {
                                Strings.Append("<div><strong>报价包含</strong></div>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", PriceIn));
                                Strings.Append("</ul>");
                            }
                            string PriceOut = x.SelectSingleNode("PriceOut").InnerText.Replace("\n", "</li><li>");
                            PriceOut = PriceOut.Replace("<li></li>", "aa");
                            if (PriceOut.Length > 5)
                            {
                                Strings.Append("<div><strong>报价包含</strong></div>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", PriceOut));
                                Strings.Append("</ul>");
                            }
                            string OwnExpense = x.SelectSingleNode("OwnExpense").InnerText.Replace("\n", "</li><li>");
                            if (OwnExpense.Length > 5)
                            {
                                Strings.Append("<div><strong>自费项目</strong></div>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", OwnExpense));
                                Strings.Append("</ul>");
                            }
                            Strings.Append("</div>");
                            Strings.Append("</div>");


                            //温馨提醒
                            Strings.Append("<div class=\\\"recommend_detail\\\">");
                            Strings.Append("<div class=\\\"recommend_txt\\\">");
                            Strings.Append("<h3>温馨提醒</h3>");
                            string Attentions = x.SelectSingleNode("Attentions").InnerText.Replace("\n", "</li><li>");
                            if (Attentions.Length > 5)
                            {
                                Strings.Append("<div><strong>注意事项</strong></div>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", Attentions));
                                Strings.Append("</ul>");
                            }
                            string Shopping = x.SelectSingleNode("Shopping").InnerText.Replace("\n", "</li><li>");
                            if (Shopping.Length > 5)
                            {
                                Strings.Append("<div><strong>购物商店</strong></div>");
                                Strings.Append("<ul>");
                                Strings.Append(string.Format("<li>{0}</li>", Shopping));
                                Strings.Append("</ul>");
                            }
                            Strings.Append("</div>");
                            Strings.Append("</div>");


                            Strings.Append("</div>");
                            //特色结束

                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("RouteInfos");
                            int PicCount = 0;
                            string route = "";
                            for (int i = 0; i < elemList.Count; i++)
                            {

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
                                            PicUrl.Append(string.Format("<div><a><img class=\\\"img-responsive img-h250\\\" src=\\\"{0}\\\"/></a></div>", pic2));
                                            PicCount = PicCount + 1;
                                        }
                                    }
                                    catch
                                    { }

                                }

                                Routes.Append("<div class=\\\"recommend_detail\\\">");
                                Routes.Append("<div class=\\\"recommend_txt route\\\">");
                                Routes.Append(string.Format("<h3><i class=\\\"fa fa-calendar\\\"></i> {0}</h3>", elemList[i].SelectSingleNode("daterank").InnerText));
                                Routes.Append(string.Format("<h2><i class=\\\"fa fa-map-marker\\\"></i> {0}</h2>", elemList[i].SelectSingleNode("rname").InnerText));
                                route = elemList[i].SelectSingleNode("route").InnerText.Replace("\n", "<br>");
                                route = route.Replace("\"", "");
                                Routes.Append(string.Format("<div class=\\\"routeinfo\\\">{0}</div>", route));
                                Routes.Append(string.Format("<h1><i class=\\\"glyphicon glyphicon-plane\\\"></i> 交通：{0}</h1>", elemList[i].SelectSingleNode("bus").InnerText));
                                Routes.Append(string.Format("<h1><i class=\\\"glyphicon glyphicon-glass\\\"></i> 用餐：{0}</h1>", elemList[i].SelectSingleNode("dinner").InnerText));
                                Routes.Append(string.Format("<h1><i class=\\\"glyphicon glyphicon-home\\\"></i> 住宿：{0}</h1>", elemList[i].SelectSingleNode("room").InnerText));
                                Routes.Append("</div>");
                                Routes.Append("</div>");

                            }

                            //展示图片
                            if (PicCount > 0)
                            {
                                Pics.Append("<div class=\\\"swipe\\\" id=\\\"mySwipe\\\">");
                                Pics.Append("<div class=\\\"swipe-wrap\\\">");
                                Pics.Append(PicUrl.ToString());
                                Pics.Append("</div></div>");
                                Pics.Append("<ul id=\\\"position\\\"><li class=\\\"cur\\\"></li>");
                                for (int i = 0; i < PicCount - 1; i++)
                                {
                                    Pics.Append("<li class=\"\"></li>");
                                }
                                Pics.Append("</ul>");
                            }
                            else
                            {
                                Pics.Append("<div class=\\\"swipe\\\" id=\\\"mySwipe\\\">");
                                Pics.Append("<div class=\\\"swipe-wrap\\\">");
                                Pics.Append(string.Format("<div><a><img class=\\\"img-responsive img-h250\\\" src=\\\"{0}\\\"/></a></div>", "/Images/none.gif"));
                                Pics.Append("</div></div>");
                                Pics.Append("<ul id=\\\"position\\\"><li class=\\\"cur\\\"></li>");
                                Pics.Append("</ul>");
                            }


                        }
                    }

                }
                infos = "{\"success\":0,\"Days\":\"" + Days + "\",\"Pics\":\"" + Pics.ToString() + "\",\"line\":\"" + Strings.ToString().Replace("<li></li>", "") + "\",\"Routes\":\"" + Routes.ToString() + "\"}";
                Pics.Clear();
                Strings.Clear();
                Routes.Clear();
                PicUrl.Clear();
                HttpContext.Current.Cache.Insert("WeChat_LineDetail_" + lineid, infos);
            }
            return infos;
        }

        public static string DestinationListCreate(string lineclass, string LineType)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_DestinationList" + lineclass]);
            if (infos == "")
            {
                string desids = "0", pname = "";
                string SqlQueryText = string.Format("select Destinationid,ProductName from OL_ProductType where MisClassId='{0}'and ProductType='{1}'", lineclass, LineType);
                //return SqlQueryText;
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    if (DS.Tables[0].Rows[0]["Destinationid"].ToString().Length > 1) desids = "0" + DS.Tables[0].Rows[0]["Destinationid"].ToString() + "0";
                    pname = DS.Tables[0].Rows[0]["ProductName"].ToString();
                }

                StringBuilder Strings = new StringBuilder();
                //<li><dl><dt>澳门</dt><dd>34543543 电饭锅</dd></dl></li>
                Strings.Append("<li><dl><dt>");
                Strings.Append(string.Format("<a tag=\\\"0\\\">{0}</a>", pname));
                Strings.Append("</dt><dd>");
                SqlQueryText = string.Format("SELECT id,ParentId,DestinationName,ClassLevel from OL_Destination where id<>'112' and (ClassLevel='2' or ClassLevel='3') and id in ({0}) order by ClassLevel,id", desids);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Strings.Append(string.Format("<a tag=\\\"{0}\\\">{1}</a>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["DestinationName"].ToString()));
                    }
                }
                Strings.Append("</dd></dl></li>");
                //infos = Strings.ToString();
                //Strings.Append("{");
                //Strings.Append(string.Format("\"success\":0,\"content\":\"{0}\"", infos));
                //Strings.Append("}");
                infos = "{\"success\":0,\"area\":\"" + pname + "\",\"content\":\"" + Strings.ToString() + "\"}";
                Strings.Clear();
                HttpContext.Current.Cache.Insert("WeChat_DestinationList" + lineclass, Strings.ToString());
            }
            return infos;
        }

        public static string GetWeChatFlashAd()
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_FlashAd_" + HttpContext.Current.Session["Fx_UserId"]]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 5 * from OL_FlashAD where AdFlag='{0}' and HideFlag='0' order by AdSort", "WeChat");
                StringBuilder Strings = new StringBuilder();
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<div class=\"swipe\" id=\"mySwipe\">");
                    Strings.Append("<div class=\"swipe-wrap\">");
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            Strings.Append(string.Format("<div><a href=\"{0}\"><img class=\"img-responsive img-h250\" src=\"{1}\"/></a></div>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString() + "?userId=" + HttpContext.Current.Session["Fx_UserId"], DS.Tables[0].Rows[i]["AdPicUrl"].ToString()));
                        }
                        else
                        {
                            Strings.Append(string.Format("<div><a href=\"{0}\"><img class=\"img-responsive img-h250\" src=\"{1}\"/></a></div>", DS.Tables[0].Rows[i]["AdPageUrl"].ToString(), DS.Tables[0].Rows[i]["AdPicUrl"].ToString()));
                        }

                    }
                    Strings.Append("</div></div>");
                    Strings.Append("<ul id=\"position\"><li class=\"cur\"></li> \r\n");
                    for (int i = 0; i < DS.Tables[0].Rows.Count - 1; i++)
                    {
                        Strings.Append("<li class=\"\"></li> \r\n");
                    }
                    Strings.Append("</ul>");
                }
                Strings.Append(" \r\n");
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("WeChat_FlashAd_" + HttpContext.Current.Session["Fx_UserId"], Strings.ToString());
            }
            return infos;
        }

        #region <% =TravelOnline.WeChat.WeChatClass.GetWeChatFlashAd()%>
        public static object GetWeChatFlashAd_New()
        {
            try
            {
                var query = Query.GetOL_FlashAD("WeChat", 5);
                JSONObject ObJson = new JSONObject();
                if (query != null && query.Count > 0)
                {
                    JSONArray ArrJson = new JSONArray();
                    ArrJson = Data.GetJsonList(UIHelper.ListToDataTable(query));
                    ObJson.Add("rows", ArrJson);
                    ObJson.Add("total", query.Count);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public static string GetWeChatIndexRecom(string flag)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_IndexRecom_" + HttpContext.Current.Session["Fx_UserId"]]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("SELECT top 6 MisLineId,LineName,Price,Pics,BigPics,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination FROM OL_Line where Sale='0' and Price>0 and WeChat='1' and PlanDate>='{0}'order by WeChatSortTime desc", DateTime.Today.ToString());//PlanDate>'{1}'

                StringBuilder Strings = new StringBuilder();
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string Pics = "/Images/none.gif", url = "";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);

                        Strings.Append("<li>");
                        Strings.Append("<div class=\"product-item product-list\">");
                        Strings.Append("<div class=\"pi-img-wrapper\">");//<a href=\"/app/line{0}\"></a>

                        //Strings.Append(string.Format("<a href=\"/app/line/{0}\"><img src=\"{1}\" class=\"img-responsive\" alt=\"{2}\"></a>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        //Strings.Append("</div>");

                        //Strings.Append(string.Format("<h3><a href=\"/app/line/{0}\">{1}</a></h3>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                        //Strings.Append(string.Format("<div class=\"pi-price\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        //Strings.Append(string.Format("<a href=\"/app/line/{0}\" class=\"btn btn-default add2cart\">去看看</a>", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", DS.Tables[0].Rows[i]["MisLineId"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString());
                        }
                        Strings.Append(string.Format("<a href=\"{0}\"><img src=\"{1}\" class=\"img-responsive\" alt=\"{2}\"></a>", url, Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append("</div>");
                        Strings.Append(string.Format("<h3><a href=\"{0}\">{1}</a></h3>", url, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append(string.Format("<div class=\"pi-price\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        Strings.Append(string.Format("<a href=\"{0}\" class=\"btn btn-default add2cart\">去看看</a>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString()));

                        //if (flag == "sticker") Strings.Append("<div class=\"sticker sticker-new\"></div>");
                        Strings.Append("</div></li>");
                    }
                }
                //Strings.Append(" \r\n");
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("WeChat_IndexRecom_" + HttpContext.Current.Session["Fx_UserId"], Strings.ToString());
            }
            return infos;
        }

        #region <% =TravelOnline.WeChat.WeChatClass.GetWeChatIndexRecom("sticker")%>
        public static object GetWeChatIndexRecom_New(string flag)
        {
            try
            {
                //string sql= string.Format("SELECT top 6 MisLineId,LineName,Price,Pics,BigPics,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination FROM OL_Line where Sale='0' and Price>0 and WeChat='1' and PlanDate>='{0}'order by WeChatSortTime desc", DateTime.Today.ToString());
                string sql = string.Format("SELECT top 6 Id,Cname,Price,PhotoPath,(select FullName from tbDestination where id=tbLine.Destination) as Destination FROM tbLine where SalesTag='0' and Price>0 and BeginDate>='{0}'", DateTime.Today.ToString());
                JSONObject ObJson = new JSONObject();
                DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    JSONArray ArrJson = new JSONArray();
                    string Pics = "/Images/none.gif";
                    dt.Columns.Add(new DataColumn("url", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (dt.Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", dt.Rows[i]["Id"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", dt.Rows[i]["Id"].ToString());
                        }
                        dt.Rows[i]["PhotoPath"] = Pics;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string GetWeChatRecom(string flag)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_Recom_" + HttpContext.Current.Session["Fx_UserId"]]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("SELECT top 30 MisLineId,LineName,Price,Pics,BigPics,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination,CASE LineType WHEN 'Cruises' THEN '邮轮' WHEN 'FreeTour' THEN '自由行' WHEN 'Visa' THEN '签证' WHEN 'OutBound' THEN '跟团游' WHEN 'InLand' THEN '跟团游' END AS LineType FROM OL_Line where Sale='0' and Price>0 and WeChat='1' and PlanDate>='{0}'order by WeChatSortTime desc", DateTime.Today.ToString());//PlanDate>'{1}'

                StringBuilder Strings = new StringBuilder();
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string Pics = "/Images/none.gif", url = "";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);

                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", DS.Tables[0].Rows[i]["MisLineId"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString());
                        }
                        Strings.Append(string.Format("<div class='row b-color'><a href='{0}'><div class='col-xs-3 pic'>", url));
                        Strings.Append(string.Format("<div class='tile' style='height:60px;overflow:hidden;'><img src='{0}' style='width:100%; height:60px;'/>", Pics));
                        Strings.Append(string.Format("<div class='span4 text-center pos50' style='top:0;font-size:12px;'><p class='colorFFF'>{0}</p></div></div></div>", DS.Tables[0].Rows[i]["LineType"].ToString()));
                        Strings.Append(string.Format("<div class='col-xs-6 name'><p>{0}</p></div>", DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append(string.Format("<div class='col-xs-2 cos'><p class='text-right price'><span>￥</span>{0}</p></div></a></div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    }
                }
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("WeChat_Recom_" + HttpContext.Current.Session["Fx_UserId"], Strings.ToString());
            }
            return infos;
        }

        #region <% =TravelOnline.WeChat.WeChatClass.GetWeChatRecom("sticker")%>
        public static object GetWeChatRecom_New(string flag)
        {
            try
            {
                //string sql = string.Format("SELECT top 30 MisLineId,LineName,Price,Pics,BigPics,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination,CASE LineType WHEN 'Cruises' THEN '邮轮' WHEN 'FreeTour' THEN '自由行' WHEN 'Visa' THEN '签证' WHEN 'OutBound' THEN '跟团游' WHEN 'InLand' THEN '跟团游' END AS LineType FROM OL_Line where Sale='0' and Price>0 and WeChat='1' and PlanDate>='{0}'order by WeChatSortTime desc", DateTime.Today.ToString());
                string sql = string.Format("SELECT top 30 Id,Cname,Price,PhotoPath,(select FullName from tbDestination where id=tbLine.Destination) as Destination,CASE Types WHEN 'Cruises' THEN '邮轮' WHEN 'FreeTour' THEN '自由行' WHEN 'Visa' THEN '签证' WHEN 'OutBound' THEN '跟团游' WHEN 'InLand' THEN '跟团游' END AS Types FROM tbLine where SalesTag='0' and Price>0 and BeginDate>='{0}'", DateTime.Today.ToString());
                DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                JSONObject ObJson = new JSONObject();
                if (dt.Rows.Count > 0)
                {
                    JSONArray ArrJson = new JSONArray();
                    string Pics = "/Images/none.gif";
                    dt.Columns.Add(new DataColumn("url", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (dt.Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);

                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", dt.Rows[i]["Id"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", dt.Rows[i]["Id"].ToString());
                        }
                        dt.Rows[i]["PhotoPath"] = Pics;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string GetWeChatFlashSale(string flag)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_FlashSale_" + HttpContext.Current.Session["Fx_UserId"]]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("select top 1 * from SpecialTopic where Types='{0}' order by SortNum,EditTime desc", "WeChat_FlashSale");
                StringBuilder String1 = new StringBuilder();
                DataSet DS = new DataSet();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string typeid, destid;
                    typeid = DS.Tables[0].Rows[0]["LineType"].ToString();
                    destid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    if (null != HttpContext.Current.Session["Fx_UserId"])
                    {
                        String1.Append(string.Format("<div class='row'><div class='col-xs-12'><h3 class='row-tit row-h3'>限时抢购<a class='more' href='{0}'>更多></a></h3></div></div>", DS.Tables[0].Rows[0]["Url"].ToString() + "?userId=" + HttpContext.Current.Session["Fx_UserId"]));
                    }
                    else
                    {
                        String1.Append(string.Format("<div class='row'><div class='col-xs-12'><h3 class='row-tit row-h3'>限时抢购<a class='more' href='{0}'>更多></a></h3></div></div>", DS.Tables[0].Rows[0]["Url"].ToString()));
                    }

                    //SqlQueryText = string.Format("select top 3 * from View_SpecialLine where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    SqlQueryText = string.Format("select top 3 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", DS.Tables[0].Rows[0]["ID"].ToString());
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);

                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            //string sqlstr = "SELECT top 3 * FROM View_SpecialLineTemp where 1=1";
                            string sqlstr = "SELECT top 3 * FROM View_SpecialLineTemp_New where 1=1";
                            if (typeid.Length > 2)
                            {
                                //sqlstr += " and lineclass in (" + typeid + ")";
                                sqlstr += " and LineType in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                //sqlstr += " and MisLineId in (select lineid from linedest where destid in (0" + destid + "0))";
                                sqlstr += " and Destination =" + destid + "";
                            }
                            //sqlstr += " order by TopEnd desc,EditTime desc";
                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(sqlstr);
                        }
                    }

                    string Pics = "/Images/none.gif";
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", DS.Tables[0].Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);
                        if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["PhotoPath"].ToString());
                        string url = "";
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", DS.Tables[0].Rows[i]["Id"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", DS.Tables[0].Rows[i]["Id"].ToString());
                        }
                        String1.Append(string.Format("<div class='row'><a href='{0}'><div class='col-xs-7 overflow'><img src='{1}' alt='{2}', title='{2}'/></div>", url, Pics, DS.Tables[0].Rows[i]["Cname"].ToString()));
                        String1.Append(string.Format("<div class='col-xs-5 product'><p>{0}</p><strong>￥<span>{1}</span></strong></div>", DS.Tables[0].Rows[i]["Cname"].ToString(), DS.Tables[0].Rows[i]["Price"].ToString()));

                        String1.Append("<div class='col-xs-5 product' style='border:0;'><form name='form1'><input type='text' name='left' size='30' style='font-size:12px;'></form></div></a></div>");
                    }
                }
                infos = String1.ToString();
                HttpContext.Current.Cache.Insert("WeChat_FlashSale_" + HttpContext.Current.Session["Fx_UserId"], infos);
            }
            return infos;
        }

        #region <%= TravelOnline.WeChat.WeChatClass.GetWeChatFlashSale("")%>
        public static object GetWeChatFlashSale_New(string flag)
        {
            try
            {
                var query = Query.GetSpecialTopic("WeChat_FlashSale", 1);
                JSONObject ObJson = new JSONObject();
                if (query != null && query.Count > 0)
                {
                    JSONArray ArrJson1 = new JSONArray();
                    ArrJson1 = Data.GetJsonList(UIHelper.ListToDataTable(query));
                    ObJson.Add("row1", ArrJson1);
                    string typeid = query[0].LineType;
                    string destid = query[0].Destinationid;
                    string sql = string.Format("select top 3 * from View_SpecialLine_New where Stid='{0}' order by SortNum,EditTime desc", query[0].Id.ToString());
                    DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                    if (dt.Rows.Count.Equals(0))
                    {
                        if (typeid.Length > 2 || destid.Length > 3)
                        {
                            string sqlstr = "SELECT top 3 * FROM View_SpecialLineTemp_New where 1=1";
                            if (typeid.Length > 2)
                            {
                                sqlstr += " and LineType in (" + typeid + ")";
                            }
                            if (destid.Length > 3)
                            {
                                sqlstr += " and Destination =" + destid + "";
                            }
                            dt.Clear();
                            dt = MyDataBaseComm.getDataSet(sqlstr).Tables[0];
                        }
                    }
                    dt.Columns.Add(new DataColumn("url", typeof(string)));
                    string Pics = "/Images/none.gif";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (dt.Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/images/views/{0}/m_{1}", dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);
                        if (dt.Rows[i]["Types"].ToString() == "Visa") Pics = string.Format("/images/shadow/{0}", dt.Rows[i]["PhotoPath"].ToString());
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", dt.Rows[i]["Id"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", dt.Rows[i]["Id"].ToString());
                        }
                        dt.Rows[i]["PhotoPath"] = Pics;
                    }
                    JSONArray ArrJson = new JSONArray();
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string GetWeChatLineListRecom(string flag)
        {
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_LineListRecom_" + flag + HttpContext.Current.Session["Fx_UserId"]]);
            if (infos == "")
            {
                string SqlQueryText = string.Format("SELECT top 30 MisLineId,LineName,Price,Pics FROM OL_Line where Sale='0' and Price>0 and WeChat='2' and LineType='{0}' and PlanDate>='{1}' order by WeChatSortTime desc", flag, DateTime.Today.ToString());//PlanDate>'{1}'
                if (flag == "recommend") SqlQueryText = string.Format("SELECT top 30 MisLineId,LineName,Price,Pics FROM OL_Line where Sale='0' and Price>0 and WeChat='2' and PlanDate>='{1}' order by WeChatSortTime desc", flag, DateTime.Today.ToString());

                StringBuilder Strings = new StringBuilder();
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                string Pics = "/Images/none.gif", url = "";
                if (DS.Tables[0].Rows.Count > 0)
                {

                    //for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    //{
                    //    Pics = "/images/none.gif";
                    //    if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);

                    //    Strings.Append("<li>");
                    //    Strings.Append("<div class=\"product-item product-list\">");
                    //    Strings.Append("<div class=\"pi-img-wrapper\">");//<a href=\"/app/line{0}\"></a>
                    //    //Strings.Append(string.Format("<a href=\"/app/line/{0}\"><img src=\"{1}\" class=\"img-responsive\" alt=\"{2}\"></a>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                    //    //Strings.Append("</div>");

                    //    //Strings.Append(string.Format("<h3><a href=\"/app/line/{0}\">{1}</a></h3>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    //    //Strings.Append(string.Format("<div class=\"pi-price\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    //    //Strings.Append(string.Format("<a href=\"/app/line/{0}\" class=\"btn btn-default add2cart\">去看看</a>", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                    //    url = string.Format("line?{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString());
                    //    Strings.Append(string.Format("<a tag=\\\"{0}\\\" lineid=\\\"{2}\\\" linename=\\\"{3}\\\"><img src=\\\"{1}\\\" class=\\\"img-responsive\\\" alt=\\\"{3}\\\"></a>", url, Pics, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    //    Strings.Append("</div>");
                    //    Strings.Append(string.Format("<h3><a tag=\\\"{0}\\\" lineid=\\\"{1}\\\" linename=\\\"{2}\\\">{2}</a></h3>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    //    Strings.Append(string.Format("<div class=\\\"pi-price\\\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    //    Strings.Append(string.Format("<a tag=\\\"{0}\\\" class=\\\"btn btn-default add2cart\\\" lineid=\\\"{1}\\\" linename=\\\"{2}\\\">去看看</a>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));

                    //    Strings.Append("</div></li>");
                    //}
                }
                else
                {
                    SqlQueryText = string.Format("SELECT top 30 MisLineId,LineName,Price,Pics,BigPics,(select DestinationName from OL_Destination where id=OL_Line.FirstDestination) as Destination FROM OL_Line where Sale='0' and Price>0 and WeChat='1' and PlanDate>='{0}'order by WeChatSortTime desc", DateTime.Today.ToString());
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);

                        Strings.Append("<li>");
                        Strings.Append("<div class=\"product-item product-list\">");
                        Strings.Append("<div class=\"pi-img-wrapper\">");//<a href=\"/app/line{0}\"></a>
                        //Strings.Append(string.Format("<a href=\"/app/line/{0}\"><img src=\"{1}\" class=\"img-responsive\" alt=\"{2}\"></a>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        //Strings.Append("</div>");

                        //Strings.Append(string.Format("<h3><a href=\"/app/line/{0}\">{1}</a></h3>", DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                        //Strings.Append(string.Format("<div class=\"pi-price\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        //Strings.Append(string.Format("<a href=\"/app/line/{0}\" class=\"btn btn-default add2cart\">去看看</a>", DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", DS.Tables[0].Rows[i]["MisLineId"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            url = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString());
                        }

                        Strings.Append(string.Format("<a href=\"{0}\"><img src=\"{1}\" class=\"img-responsive\" alt=\"{2}\"></a>", url, Pics, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append("</div>");
                        Strings.Append(string.Format("<h3><a href=\"{0}\">{1}</a></h3>", url, DS.Tables[0].Rows[i]["LineName"].ToString()));
                        Strings.Append(string.Format("<div class=\"pi-price\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                        Strings.Append(string.Format("<a href=\"{0}\" class=\"btn btn-default add2cart\">去看看</a>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString()));


                        Strings.Append("</div></li>");
                    }
                }
                //Strings.Append(" \r\n");
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("WeChat_LineListRecom_" + flag + HttpContext.Current.Session["Fx_UserId"], Strings.ToString());
            }
            return infos;
        }

        #region WeChatClass.GetWeChatLineListRecom(linetype)
        private static IDictionary<string, string> dicTypes()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("101", "inland");
            dic.Add("102", "outbound");
            return dic;
        }
        private static string GetKey(string value, IDictionary<string, string> dic)
        {
            try
            {
                foreach (string strKey in dic.Keys)
                {
                    if (dic[strKey] == value)
                    {
                        return strKey;
                    }
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetValue(string key, IDictionary<string, string> dic)
        {
            try
            {
                if (dic.Keys.Contains(key))
                {
                    return dic[key];
                }
                return key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static object GetWeChatLineListRecom_New(string flag)
        {
            try
            {
                //string sql = string.Format("SELECT top 30 Id,Cname,Price,PhotoPath FROM tbLine where SalesTag='0' and Price>0 and Types='{0}' and BeginDate>='{1}'", GetKey(flag, dicTypes()), DateTime.Today.ToString());//PlanDate>'{1}'
                string sql = string.Format("SELECT top 30 Id,Cname,Price,PhotoPath FROM tbLine where SalesTag='0' and Price>0 and Types='{0}'", GetKey(flag, dicTypes()));//PlanDate>'{1}'
                if (flag == "recommend")
                {
                    sql = string.Format("SELECT top 30 Id,Cname,Price,PhotoPath FROM tbLine where SalesTag='0' and Price>0 and BeginDate>='{1}'", flag, DateTime.Today.ToString());
                }
                DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                string Pics = "/Images/none.gif";
                if (dt.Rows.Count == 0)
                {
                    sql = string.Format("SELECT top 30 Id,Cname,Price,PhotoPath,(select DestinationName from OL_Destination where id=tbLine.Destination) as Destination FROM tbLine where SalesTag='0' and Price>0 and BeginDate>='{0}'", DateTime.Today.ToString());
                    dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                }
                JSONObject ObJson = new JSONObject();
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("url", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Pics = "/images/none.gif";
                        if (dt.Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);

                        if (null != HttpContext.Current.Session["Fx_UserId"])
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}&userId={1}", dt.Rows[i]["Id"].ToString(), HttpContext.Current.Session["Fx_UserId"]);
                        }
                        else
                        {
                            dt.Rows[i]["url"] = string.Format("/WeChat/linelist.aspx?linetype=line?{0}", dt.Rows[i]["Id"].ToString());
                        }
                        dt.Rows[i]["PhotoPath"] = Pics;
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                    }
                    JSONArray ArrJson = new JSONArray();
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string GetWeChatLineClassList(string linetype)
        {
            string typename = "", css = "", tilediv = "tile-div", style = "background-position: 0 0;";
            string fa = "fa-globe";
            string infos = Convert.ToString(HttpContext.Current.Cache["WeChat_LineClass_" + linetype]);
            if (infos == "")
            {
                switch (linetype)
                {
                    case "outbound":
                        typename = "团队游";
                        fa = "fa-globe";
                        break;
                    case "inland":
                        typename = "团队游";
                        fa = "fa-flag";
                        break;
                    case "freetour":
                        typename = "自由行";
                        fa = "fa-briefcase";
                        break;
                    case "cruises":
                        typename = "邮轮";
                        tilediv = "tile-div2";
                        fa = "fa-anchor";
                        break;
                    case "visa":
                        typename = "签证";
                        fa = "fa-shield";
                        break;
                    default:
                        typename = "出境旅游";
                        fa = "fa-globe";
                        break;
                }
                string SqlQueryText = string.Format("select * from OL_ProductType where ProductType='{0}' and MisClassId not in (1051)", linetype);
                StringBuilder Strings = new StringBuilder();
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        string type = DS.Tables[0].Rows[i]["ProductName"].ToString();
                        switch (type)
                        {
                            case "港澳":
                                style = "background-position: 0 -64px";
                                break;
                            case "日韩":
                                style = "background-position: -32px -64px";
                                break;
                            case "澳新":
                                style = "background-position: -64px -64px";
                                break;
                            case "FIT":
                                style = "background-position: -288px -64px";
                                break;
                            case "欧洲":
                                style = "background-position: -96px -64px";
                                break;
                            case "东南亚":
                                style = "background-position: -128px -64px";
                                break;
                            case "中东非洲":
                                style = "background-position: -160px -64px";
                                break;
                            case "美加":
                                style = "background-position: -192px -64px";
                                break;
                            case "台湾":
                                style = "background-position: -224px -64px";
                                break;
                            case "南美":
                                style = "background-position: -256px -64px";
                                break;
                            case "华东":
                                style = "background-position: 0 0";
                                break;
                            case "华南":
                                style = "background-position: -32px 0";
                                break;
                            case "华中":
                                style = "background-position: -64px 0";
                                break;
                            case "北方":
                                style = "background-position: -96px 0";
                                break;
                            case "西南":
                                style = "background-position: -128px 0";
                                break;
                            case "西北":
                                style = "background-position: -160px 0";
                                break;
                            case "港澳台":
                                style = "background-position: 0 -64px";
                                break;
                            case "国内":
                                style = "background-position: -96px 0";
                                break;
                            case "中东非":
                                style = "background-position: -160px -64px";
                                break;
                            case "亚洲签证":
                                style = "background-position: 0 -32px";
                                break;
                            case "欧洲签证":
                                style = "background-position: -32px -32px";
                                break;
                            case "美洲签证":
                                style = "background-position: -64px -32px";
                                break;
                            case "中东非签证":
                                style = "background-position: -96px -32px";
                                break;
                            case "大洋洲签证":
                                style = "background-position: -32px -32px";
                                break;
                            case "入台证":
                                style = "background-position: -160px -32px";
                                break;
                            case "日韩航线":
                                style = "background-position: -32px -64px";
                                break;
                            case "台湾航线":
                                style = "background-position: -224px -64px";
                                break;
                            case "欧美航线":
                                style = "background-position: -192px -64px";
                                break;
                            case "东南亚航线":
                                style = "background-position: -128px -64px";
                                break;
                            default:
                                style = "background-position: 0 0";
                                break;
                        }
                        Strings.Append("<div class=\"" + tilediv + "\">");// tarea=\"{1}\"
                        Strings.Append(string.Format("<a tag=\"{0}\" tname=\"{1}{2}\">", DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS.Tables[0].Rows[i]["ProductName"].ToString(), typename));
                        Strings.Append("<div class=\"tile\">");
                        Strings.Append("<div class=\"tile-body\">");
                        Strings.Append("<i style=\"" + style + "\"></i>");
                        Strings.Append("</div>");
                        Strings.Append("<div class=\"tile-object\">");
                        Strings.Append(string.Format("<div class=\"number\">{0}</div>", DS.Tables[0].Rows[i]["ProductName"].ToString()));
                        Strings.Append("</div>");
                        Strings.Append("</div>");
                        Strings.Append("<a>");
                        Strings.Append("</div>");
                        Strings.Append("");
                    }
                }
                infos = Strings.ToString();
                HttpContext.Current.Cache.Insert("WeChat_LineClass_" + linetype, Strings.ToString());
            }
            return infos;
        }

        #region WeChatClass.GetWeChatLineClassList(linetype)
        public static object GetWeChatLineClassList_New(string linetype)
        {
            try
            {
                string sql = string.Format("select * from OL_ProductType where ProductType='{0}' and MisClassId not in (1051)", linetype);
                JSONObject ObJson = new JSONObject();
                DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("tilediv", typeof(string)));
                    dt.Columns.Add(new DataColumn("typename", typeof(string)));
                    dt.Columns.Add(new DataColumn("style", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string type = dt.Rows[i]["ProductName"].ToString();
                        dt.Rows[i]["style"] = GetStyleByType(type);
                        dt.Rows[i]["typename"] = GetTypeNameByLineType(linetype);
                        dt.Rows[i]["tilediv"] = GetTileDiv(linetype);
                    }
                    JSONArray ArrJson = new JSONArray();
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetTileDiv(string linetype)
        {
            string tilediv = "tile-div";
            try
            {
                if (!string.IsNullOrEmpty(linetype) && linetype.Equals("cruises"))
                {
                    tilediv = "tile-div2";
                }
                return tilediv;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetTypeNameByLineType(string linetype)
        {
            string typename = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(linetype))
                {
                    switch (linetype)
                    {
                        case "outbound":
                            typename = "团队游";
                            break;
                        case "inland":
                            typename = "团队游";
                            break;
                        case "freetour":
                            typename = "自由行";
                            break;
                        case "cruises":
                            typename = "邮轮";
                            break;
                        case "visa":
                            typename = "签证";
                            break;
                        default:
                            typename = "出境旅游";
                            break;
                    }
                }
                return typename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GetStyleByType(string type)
        {
            string style = "background-position: 0 0;";
            try
            {
                if (!string.IsNullOrEmpty(type))
                {
                    switch (type)
                    {
                        case "港澳":
                            style = "background-position: 0 -64px";
                            break;
                        case "日韩":
                            style = "background-position: -32px -64px";
                            break;
                        case "澳新":
                            style = "background-position: -64px -64px";
                            break;
                        case "FIT":
                            style = "background-position: -288px -64px";
                            break;
                        case "欧洲":
                            style = "background-position: -96px -64px";
                            break;
                        case "东南亚":
                            style = "background-position: -128px -64px";
                            break;
                        case "中东非洲":
                            style = "background-position: -160px -64px";
                            break;
                        case "美加":
                            style = "background-position: -192px -64px";
                            break;
                        case "台湾":
                            style = "background-position: -224px -64px";
                            break;
                        case "南美":
                            style = "background-position: -256px -64px";
                            break;
                        case "华东":
                            style = "background-position: 0 0";
                            break;
                        case "华南":
                            style = "background-position: -32px 0";
                            break;
                        case "华中":
                            style = "background-position: -64px 0";
                            break;
                        case "北方":
                            style = "background-position: -96px 0";
                            break;
                        case "西南":
                            style = "background-position: -128px 0";
                            break;
                        case "西北":
                            style = "background-position: -160px 0";
                            break;
                        case "港澳台":
                            style = "background-position: 0 -64px";
                            break;
                        case "国内":
                            style = "background-position: -96px 0";
                            break;
                        case "中东非":
                            style = "background-position: -160px -64px";
                            break;
                        case "亚洲签证":
                            style = "background-position: 0 -32px";
                            break;
                        case "欧洲签证":
                            style = "background-position: -32px -32px";
                            break;
                        case "美洲签证":
                            style = "background-position: -64px -32px";
                            break;
                        case "中东非签证":
                            style = "background-position: -96px -32px";
                            break;
                        case "大洋洲签证":
                            style = "background-position: -32px -32px";
                            break;
                        case "入台证":
                            style = "background-position: -160px -32px";
                            break;
                        case "日韩航线":
                            style = "background-position: -32px -64px";
                            break;
                        case "台湾航线":
                            style = "background-position: -224px -64px";
                            break;
                        case "欧美航线":
                            style = "background-position: -192px -64px";
                            break;
                        case "东南亚航线":
                            style = "background-position: -128px -64px";
                            break;
                        default:
                            style = "background-position: 0 0";
                            break;
                    }
                }
                return style;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //线路列表结果及页码导航生成
        public static string LineListCreate(string navbar, string linetype, string lineclass, string lineclassname, int filter, string dest, int pages, string searchval)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("Sale='0' and Price>0 and PlanDate>='{0}' and ", DateTime.Today.ToString()));
            if (searchval != "")
            {
                string dest_id = "0";
                dest_id = MyDataBaseComm.getScalar("select Id from OL_Destination where DestinationName like '%" + searchval + "%'");

                Strings.Append(string.Format("(LineName like '%{0}%' ", searchval));
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
                if (linetype != "") Strings.Append(string.Format("LineType='{0}' and ", linetype));
                if (lineclass != "") Strings.Append(string.Format("(lineclass='{0}' or LineName like '%{1}%') and ", lineclass, lineclassname));
                if (dest != "0") Strings.Append(string.Format("Destinationid like '%,{0},%' and ", dest));
            }

            string fieldlist = "*,(select max(preferAmount) from OL_Preferential where (Lineid=dbo.OL_Line.MisLineid and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate()))) AS preferAmount";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "id";
            string sortflag = "";
            string sortname = "Price";
            string tablename = "OL_Line";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            switch (filter)
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

            string SqlQueryText = "", ListResult = "非常抱歉，没有找到您需要的内容";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = CreateLineListString(SqlQueryText, navbar);
            }
            Strings.Clear();
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"pages\":{0},\"pagecount\":{1},\"content\":\"{2}\"", pages + 1, PageCount, ListResult));
            Strings.Append("}");
            return Strings.ToString();
        }
        #region LineListCreate_New
        public static object LineListCreate_New(string navbar, string linetype, string lineclass, string lineclassname, int filter, string dest, int pages, string searchval)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append(string.Format("SaleTag='0' and Price>0 and BeginDate>='{0}' and ", DateTime.Today.ToString()));
            if (searchval != "")
            {
                string dest_id = "0";
                dest_id = MyDataBaseComm.getScalar("select Id from OL_Destination where DestinationName like '%" + searchval + "%'");

                Strings.Append(string.Format("(Cname like '%{0}%' ", searchval));
                if (MyConvert.ConToInt(dest_id) != 0)
                {
                    Strings.Append(string.Format(" or Destination like '%,{0},%' ) and ", dest_id));
                }
                else
                {
                    Strings.Append(string.Format(") and ", dest_id));
                }

            }
            else
            {
                if (linetype != "") Strings.Append(string.Format("Types='{0}' and ", linetype));
                if (lineclass != "") Strings.Append(string.Format("(LineType='{0}' or Cname like '%{1}%') and ", lineclass, lineclassname));
                if (dest != "0") Strings.Append(string.Format("Destination like '%,{0},%' and ", dest));
            }

            string fieldlist = "*,(select max(preferAmount) from OL_Preferential where (Lineid=dbo.tbLine.Id and (pStartDate is null or pStartDate<=getdate()) and (pEndDate is null or pEndDate>=getdate()))) AS preferAmount";
            Strings.Append("1=1 ");
            //查询条件结束

            //int SortType = filter;
            string condition = Strings.ToString();
            string pkey = "Uid";
            string sortflag = "";
            string sortname = "Price";
            string tablename = "tbLine";
            int pagesize = 10;
            int currpage = pages;
            int rowcount = MyConvert.ConToInt(LineListPageSerch.GetPagesCounts(pkey, tablename, condition));
            int PageCount = Convert.ToInt32(Math.Ceiling((double)rowcount / (double)pagesize));

            switch (filter)
            {
                case 2:
                    sortname = "Days";
                    sortflag = "asc";
                    break;
                case 3:
                    sortname = "Days";
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
                    sortname = "CreateTime desc";
                    break;
            }

            string SqlQueryText = ""; object ListResult = "非常抱歉，没有找到您需要的内容";
            if (rowcount != 0)
            {
                SqlQueryText = LineListPageSerch.GetPagesSqlQueryText(fieldlist, condition, pkey, tablename, sortflag, sortname, pagesize, currpage);
                ListResult = CreateLineListString_New(SqlQueryText, navbar, pages + 1, PageCount);
            }
            //Strings.Clear();
            //Strings.Append("{");
            //Strings.Append(string.Format("\"success\":0,\"pages\":{0},\"pagecount\":{1},\"content\":\"{2}\"", pages + 1, PageCount, ListResult));
            //Strings.Append("}");
            //return Strings.ToString();
            return ListResult;
        }
        #endregion
        #region CreateLineListString_New
        public static object CreateLineListString_New(string sql, string navbar, int pages, int pagecount)
        {
            JSONObject ObJson = new JSONObject();
            try
            {
                DataTable dt = MyDataBaseComm.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add(new DataColumn("isspecialpfq", typeof(bool)));
                    JSONArray ArrJson = new JSONArray();
                    string Pics = "/Images/none.gif";
                    dt.Columns.Add(new DataColumn("url", typeof(string)));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Types"].ToString() == "Visa")
                        {
                            if (dt.Rows[i]["PhotoPath"].ToString().Length > 5) Pics = string.Format("/images/shadow/{0}", dt.Rows[i]["PhotoPath"].ToString());
                        }
                        else
                        {
                            if (dt.Rows[i]["PhotoPath"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[0], dt.Rows[i]["PhotoPath"].ToString().Split("/".ToCharArray())[1]);
                        }
                        dt.Rows[i]["url"] = string.Format("line?{0}", dt.Rows[i]["Id"].ToString());
                        dt.Rows[i]["Price"] = dt.Rows[i]["Price"].ToString().Replace(".00", "");
                        if (Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                        {
                            string LineId = dt.Rows[i]["Id"].ToString();
                            int price = Convert.ToInt32(dt.Rows[i]["Price"]);
                            if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && price > 1500)
                            {
                                dt.Rows[i]["isspecialpfq"] = true;
                            }
                            else
                            {
                                dt.Rows[i]["isspecialpfq"] = false;
                            }
                        }
                    }
                    ArrJson = Data.GetJsonList(dt);
                    ObJson.Add("rows", ArrJson);
                    ObJson.Add("pages", pages);
                    ObJson.Add("pagecount", pagecount);
                    ObJson.Add("navbar", navbar);
                    ObJson.Add("showBanklj", ConfigurationManager.AppSettings["showBanklj"].ToString());
                    ObJson.Add("specialpfq", ConfigurationManager.AppSettings["specialpfq"].ToString());
                    ObJson.Add("specialBanklj", ConfigurationManager.AppSettings["specialBanklj"].ToString());
                    ObJson.Add("normalBanklj", ConfigurationManager.AppSettings["normalBanklj"].ToString());
                }
                return json.SerializeObject(ObJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static string CreateLineListString(string SqlQueryText, string navbar)
        {
            StringBuilder Strings = new StringBuilder();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string Pics = "/Images/none.gif";
                string url = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {

                    if (DS.Tables[0].Rows[i]["LineType"].ToString() == "Visa")
                    {
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length > 5) Pics = string.Format("/images/shadow/{0}", DS.Tables[0].Rows[i]["Pics"].ToString());
                        }
                        catch
                        { }
                    }
                    else
                    {
                        try
                        {
                            if (DS.Tables[0].Rows[i]["Pics"].ToString().Length == 24) Pics = string.Format("/Images/Views/{0}/M_{1}", DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[i]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        }
                        catch
                        { }
                    }

                    Strings.Append("<li>");
                    if (navbar == "1")
                    {
                        Strings.Append("<div class=\\\"product-item\\\">");
                    }
                    else
                    {
                        Strings.Append("<div class=\\\"product-item product-list\\\">");
                    }



                    Strings.Append("<div class=\\\"pi-img-wrapper\\\">");
                    url = string.Format("line?{0}", DS.Tables[0].Rows[i]["MisLineId"].ToString());
                    Strings.Append(string.Format("<a tag=\\\"{0}\\\" lineid=\\\"{2}\\\" linename=\\\"{3}\\\"><img src=\\\"{1}\\\" class=\\\"img-responsive\\\" alt=\\\"{3}\\\"></a>", url, Pics, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    Strings.Append("</div>");
                    Strings.Append(string.Format("<h3><a tag=\\\"{0}\\\" lineid=\\\"{1}\\\" linename=\\\"{2}\\\">{2}</a></h3>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    ; Strings.Append(string.Format("<div class=\\\"pi-price\\\">&#165;{0}起</div>", DS.Tables[0].Rows[i]["Price"].ToString().Replace(".00", "")));
                    Strings.Append(string.Format("<a tag=\\\"{0}\\\" class=\\\"btn btn-default add2cart\\\" lineid=\\\"{1}\\\" linename=\\\"{2}\\\">去看看</a>", url, DS.Tables[0].Rows[i]["MisLineId"].ToString(), DS.Tables[0].Rows[i]["LineName"].ToString()));
                    if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0 || (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0")) || Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                    {
                        Strings.Append("<div class='discount'><div id='js_down' class='down'>");

                        if (Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Length > 0 && !Convert.ToString(DS.Tables[0].Rows[i]["wwwyh"]).Equals("0"))
                        {
                            Strings.Append(string.Format("<div class='type'><em>优惠</em>网上支付立减{0}元</div>", DS.Tables[0].Rows[i]["wwwyh"].ToString()));
                        }
                        if (Convert.ToString(DS.Tables[0].Rows[i]["preferAmount"]).Length > 0)
                        {
                            Strings.Append(string.Format("<div class='type'><em class='cu'>促销</em>早定早优惠立减{0}元</div>", DS.Tables[0].Rows[i]["preferAmount"].ToString()));
                        }
                        if (Convert.ToString(ConfigurationManager.AppSettings["showBanklj"]).Equals("Y"))
                        {
                            string LineId = DS.Tables[0].Rows[i]["MisLineId"].ToString();
                            int price = Convert.ToInt32(DS.Tables[0].Rows[i]["Price"]);
                            if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && price > 1500)
                            {

                                Strings.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["specialBanklj"])));
                            }
                            else
                            {
                                Strings.Append(string.Format("<div class='type'><em class='yin'>银行</em>{0}</div>", Convert.ToString(ConfigurationManager.AppSettings["normalBanklj"])));
                            }
                        }
                        string.Format("</div></div>");
                    }
                    //Strings.Append(string.Format("<div class='discount'><div id='js_down' class='down'><div class='type'><em>优惠</em>美妙的云南之旅丽江</div><div class='type'><em class='cu'>促销</em>美妙的云南之旅丽江、西双版纳精</div><div class='type'><em class='yin'>银行</em>美妙的云南之旅丽江</div></div></div>"));
                    Strings.Append("</div></li>");
                }
            }
            return Strings.ToString();
        }


    }
}