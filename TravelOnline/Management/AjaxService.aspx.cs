using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Travel;
using System.Data;
using TravelOnline.Class.Common;
using System.Text;
using TravelOnline.Class.Purchase;
using Sunrise.Spell;
using System.IO;
using TravelOnline.WeChat.freetrip.interfaces;
using TravelOnline.WeChat.freetrip.model;

namespace TravelOnline.Management
{
    public partial class AjaxService : System.Web.UI.Page
    {
        //public DataSet DS;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("{\"info\":\"尚未登录\"}");
                Response.End();
            }
            switch (Request.QueryString["action"])
            {
                case "FuJia":
                    FuJia(Convert.ToInt32(Request.QueryString["autoid"]), Request.QueryString["Order_lxr"], Request.QueryString["Order_Memo"]);
                    break;
                case "NewPageCacheReset":
                    PurchaseAutoRun.DeleteSelectCache("NewPage_");
                    PurchaseAutoRun.DeleteSelectCache("OTA_");
                    Response.Write("{\"success\":0}");
                    break;
                case "WeChatReset":
                    PurchaseAutoRun.DeleteSelectCache("WeChat_");
                    Response.Write("{\"success\":0}");
                    break;
                case "WeChatcancel":
                    WeChatcancel();
                    break;
                case "SetHotDestination":
                    SetHotDestination();
                    break;
                case "DeleteDestinationClass":
                    DeleteDestinationClass();
                    break;
                case "CreateDestinationSelect":
                    GetProductClass.BindDestinationClassName();
                    break;
                case "GetPinYin":
                    GetPinYin();
                    break;
                case "SetTopAffiche":
                    SetTopAffiche();
                    break;
                case "ManageLine":
                    ManageLine(Request.QueryString["DoFlag"]);
                    break;
                case "NewRecom":
                    NewRecom(Request.QueryString["DoFlag"]);
                    break;
                case "SaveUserRight":
                    //保存权限
                    UserRightEdit(Request.QueryString["action"]);
                    break;
                case "DeleteRightInfos":
                    DeleteSelectInfos("OL_UserRight");
                    break;
                case "DeleteDeptInfos":
                    DeleteSelectInfos("OL_Dept");
                    break;
                case "DeleteUserInfos":
                    DeleteSelectInfos("OL_ManageUser");
                    break;
                case "DeleteAdInfos":
                    DeleteSelectInfos("OL_FlashAD");
                    break;
                case "DeleteAfficheInfos":
                    DeleteSelectInfos("OL_Affiche");
                    break;
                case "DeleteProductClass":
                    DeleteProductClass();
                    break;
                case "DeleteProductType":
                    DeleteSelectInfos("OL_ProductType");
                    break;
                case "CreateProductClassSelect":
                    GetProductClass.BindClassName();
                    break;
                case "CreateProductClassSort":
                    GetProductClass.BindSortList();
                    Response.Write("{\"success\":0}");
                    Response.End();
                    break;
                case "SaveDept":
                    SaveDept();
                    break;
                case "CheckUser":
                    CheckUser();
                    break;
                case "SaveUser":
                    SaveUser();
                    break;
                case "SaveFlashAd":
                    SaveFlashAd();
                    break;
                case "SaveProductClass":
                    SaveProductClass();
                    break;
                case "SaveDestinationClass":
                    SaveDestinationClass();
                    break;
                case "SaveProductType":
                    SaveProductType();
                    break;
                case "SaveInitDataInfo":
                    SaveInitDataInfo();
                    break;
                case "ChangePassWord":
                    EditPass_CheckAuthcode();
                    EditPass_CheckPassWord();
                    EditPass_Change();
                    break;
                case "AdjustPrice":
                    AdjustPrice();
                    break;
                case "SavePrefer":
                    SavePrefer();
                    break;
                case "SaveIntegral":
                    SaveIntegral();
                    break;
                case "SavePreferNums":
                    SavePreferNums();
                    break;
                case "PreferSerch":
                    PreferSerch();
                    break;
                case "PreferSend":
                    PreferSend();
                    break;
                case "InitData":
                    InitData();
                    break;
                case "DeleteInitData":
                    DeleteSelectInfos("InitData");
                    break;
                case "DeleteTempOrder":
                    DeleteTempOrder();
                    break;
                case "FriendLink":
                    FriendLink();
                    break;
                case "DeleteFriendLink":
                    DeleteSelectInfos("OL_FriendLink");
                    HttpContext.Current.Cache.Insert("FriendLink", "");
                    break;
                case "LineDestination":
                    LineDestination();
                    break;
                case "CacheReset":
                    CacheReset();
                    break;
                case "SaveView":
                    SaveView();
                    break;
                case "SaveViewPic":
                    SaveViewPic();
                    break;
                case "SiteMapCreate":
                    SiteMapCreate();
                    break;
                case "SaveSeoLink":
                    SaveSeoLink();
                    break;
                case "DeleteSeoLink":
                    DeleteSelectInfos("SeoLink");
                    break;
                case "SaveSpecialTopic":
                    SaveSpecialTopic();
                    break;
                case "SpecialTopicSetTop":
                    SpecialTopicSetTop();
                    break;
                case "SaveSpecialLine":
                    SaveSpecialLine();
                    break;
                case "DelSpecialLine":
                    DeleteSelectInfos("SpecialLine");
                    break;
                case "DeleteSpecialTopic":
                    DeleteSelectInfos("SpecialTopic");
                    break;
                case "SpecialLineSetTop":
                    SpecialLineSetTop();
                    break;
                case "SavePreferInfo":
                    SavePreferInfo();
                    break;
                case "DeletePreferInfo":
                    DeleteSelectInfos("OL_Preferential");
                    break;
                case "SaveTradingAreaInfo":
                    SaveTradingAreaInfo();
                    break;
                case "DeleteTradingAreaInfo":
                    DeleteSelectInfos("OL_TradingArea");
                    break;
                case "SaveTActivityInfo":
                    SaveTActivityInfo();
                    break;
                case "DeleteTActivityInfo":
                    DeleteSelectInfos("OL_TActivity");
                    break;
                case "SaveTCouponInfo":
                    SaveTCouponInfo();
                    break;
                case "DeleteTCouponInfo":
                    DeleteSelectInfos("OL_TCoupon");
                    break;
                case "SaveTStoreInfo":
                    SaveTStoreInfo();
                    break;
                case "DeleteTStoreInfo":
                    DeleteSelectInfos("OL_TStore");
                    break;
                case "AuditCommentInfo":
                    AuditCommentInfo();
                    break;
                case "RecomJournals":
                    RecomJournals();
                    break;
                case "SaveGroupInfo":
                    SaveGroupInfo();
                    break;
                case "DeleteGroupInfo":
                    DeleteSelectInfos("OL_GroupPlan");
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }

        protected void FuJia(int AutoId, string OrderName, string OrderMemo)
        {
            string sql = string.Format("update OL_Order set OrderName='{0}',OrderMemo='{1}' where AutoId={2}", OrderName, OrderMemo, AutoId);
            if (MyDataBaseComm.ExcuteSql(sql) == true)
            {
                Response.Write("{\"success\":\"OK\"}");
            }
            else
            {
                Response.Write("{\"error\":\"保存失败\"}");
            }
            Response.End();
        }

        protected void SpecialTopicSetTop()
        {
            string SqlQueryText = string.Format("update SpecialTopic set SortNum='0',EditTime='{0}' where id in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"].Trim());

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }


        protected void SpecialLineSetTop()
        {
            string SqlQueryText = string.Format("update SpecialLine set SortNum='0',EditTime='{0}' where id in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"].Trim());

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void SaveSpecialLine()
        {
            string linename = MyDataBaseComm.getScalar("select LineName from OL_Line where MisLineId='" + Request.QueryString["lined"].Trim() + "'");
            //string linename = MyDataBaseComm.getScalar("select LineName from tbLine where Id='" + Request.QueryString["lined"].Trim() + "'");
            if (linename == null)
            {
                Response.Write("{\"success\":\"线路不存在\"}");
                Response.End();
            }
            linename = MyDataBaseComm.getScalar("select Lineid from SpecialLine where Lineid='" + Request.QueryString["lined"].Trim() + "' and Stid='" + Request.QueryString["Stid"].Trim() + "'");
            if (linename != null)
            {
                Response.Write("{\"success\":\"线路已经添加过\"}");
                Response.End();
            }
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into SpecialLine (Stid,Lineid,SortNum,EditTime) values ('{0}','{1}','{2}','{3}')", Request.QueryString["Stid"].Trim(), Request.QueryString["lined"].Trim(), MyConvert.ConToInt(Request.QueryString["SortNum"].Trim()), DateTime.Now.ToString());
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":\"0\"}");
            }
            else
            {
                Response.Write("{\"success\":\"信息保存失败，请稍后再试!\"}");
            }
            Response.End();
        }

        protected void SaveSpecialTopic()
        {
            string SqlQueryText = "";

            if (MyConvert.ConToInt(Request.Form["stid"]) == 0)
            {
                SqlQueryText = string.Format("insert into SpecialTopic (Types,Cname,SortNum,EditTime,Url,LineType,Destinationid,Destination,DestinationList) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    Request.Form["DropDownList1"],
                    Request.Form["Cname"].Trim(),
                    MyConvert.ConToInt(Request.Form["SortNum"].Trim()),
                    DateTime.Now.ToString(),
                    Request.Form["Url"].Trim(),
                    Request.Form["LineType"].Trim(),
                    Request.Form["Destinationid"],
                    Request.Form["DestinationName"],
                    Request.Form["Des_List"]
                );
            }
            else
            {
                SqlQueryText = string.Format("update SpecialTopic set DestinationList='{1}',Cname='{2}',SortNum='{3}',EditTime='{4}',Url='{5}',LineType='{6}',Destinationid='{7}',Destination='{8}' where id={0}",
                    Request.Form["stid"],
                    Request.Form["Des_List"],
                    Request.Form["Cname"].Trim(),
                    MyConvert.ConToInt(Request.Form["SortNum"].Trim()),
                    DateTime.Now.ToString(),
                    Request.Form["Url"].Trim(),
                    Request.Form["LineType"].Trim(),
                    Request.Form["Destinationid"],
                    Request.Form["DestinationName"]
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":1})");
            }
            else
            {
                Response.Write("({\"error\":1})");
            }
            Response.End();
        }

        protected void WeChatcancel()
        {
            string SqlQueryText = "", DoText = "";
            SqlQueryText = string.Format("update OL_Line set WeChatSortTime=null,WeChat=null where MisLineId in ({2})", DateTime.Now.ToString(), DoText, Request.QueryString["Id"]);
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                //清空缓存
                PurchaseAutoRun.DeleteSelectCache("WeChat_");
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }

        }

        protected void SaveSeoLink()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into seolink (keyword,url,serchnum,rank,keylength) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["keyword"].Trim(), Request.QueryString["url"].Trim(), MyConvert.ConToInt(Request.QueryString["serchnum"].Trim()), MyConvert.ConToInt(Request.QueryString["rank"].Trim()), Request.QueryString["keyword"].Trim().Length);
            }
            else
            {
                SqlQueryText = string.Format("update seolink set keyword='{1}',url='{2}',serchnum='{3}',rank='{4}',keylength='{5}' where id={0}", Request.QueryString["Id"], Request.QueryString["keyword"].Trim(), Request.QueryString["url"].Trim(), MyConvert.ConToInt(Request.QueryString["serchnum"].Trim()), MyConvert.ConToInt(Request.QueryString["rank"].Trim()), Request.QueryString["keyword"].Trim().Length);
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        public void SiteMapCreate()
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;
            DataSet DS = new DataSet();
            DataSet DS1 = new DataSet();
            DataSet DS2 = new DataSet();

            Strings.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\r\n");

            //一级页面
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/index.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/outbound.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/inland.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/freetour.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/cruises.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/visa.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));

            //新闻页面
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/index_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/outbound_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/inland_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/freetour_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/cruises_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/info/visa_news.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));

            SqlQueryText = string.Format("select id from OL_Affiche", "");
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/showinfo/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                }
            }

            //关于我们
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/service/aboutus.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/service/contactus.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/service/joinus.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/service/partner.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));
            Strings.Append(string.Format("<url><loc>http://www.scyts.com/service/advertising.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now));

            //二级大类旅游线路
            SqlQueryText = "SELECT * FROM OL_ProductType where Destinationid is not null";
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/{1}/{2}-0-0-0-0-0-0-0-0-1.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["ProductType"].ToString().ToLower(), DS.Tables[0].Rows[i]["MisClassId"].ToString()));

                    SqlQueryText = string.Format("select id from View_Destination where linecount>0 and ClassLevel='2' and id in (0{0}0)", DS.Tables[0].Rows[i]["Destinationid"].ToString());
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                        {
                            Strings.Append(string.Format("<url><loc>http://www.scyts.com/{1}/{2}-{3}-0-0-0-0-0-0-0-1.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["ProductType"].ToString().ToLower(), DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS1.Tables[0].Rows[ii]["id"].ToString()));

                            if (DS.Tables[0].Rows[i]["ProductType"].ToString() == "OutBound" || DS.Tables[0].Rows[i]["ProductType"].ToString() == "InLand" || DS.Tables[0].Rows[i]["ProductType"].ToString() == "FreeTour")
                            {
                                SqlQueryText = string.Format("select id from View_Destination where linecount>0 and ClassLevel='3' and ParentId='{0}'", DS1.Tables[0].Rows[ii]["id"].ToString());
                                DS2.Clear();
                                DS2 = MyDataBaseComm.getDataSet(SqlQueryText);
                                if (DS2.Tables[0].Rows.Count > 0)
                                {
                                    for (int iii = 0; iii < DS2.Tables[0].Rows.Count; iii++)
                                    {
                                        Strings.Append(string.Format("<url><loc>http://www.scyts.com/{1}/{2}-{3}-{4}-0-0-0-0-0-0-1.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["ProductType"].ToString().ToLower(), DS.Tables[0].Rows[i]["MisClassId"].ToString(), DS1.Tables[0].Rows[ii]["id"].ToString(), DS2.Tables[0].Rows[iii]["id"].ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //旅游线路
            SqlQueryText = string.Format("select MisLineId from OL_Line where Sale='0' and Price>0 and PlanDate>='{0}' order by MisLineId", DateTime.Today.ToString());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/line/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/routeprint/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["MisLineId"].ToString()));
                }
            }

            //游记
            SqlQueryText = "select id from OL_Journal where flag='1'";
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/showjournal/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                }
            }

            //目的地
            SqlQueryText = "select id from OL_Destination where ClassLevel>1";
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/place/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/summary/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/sight/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/traffic/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/journals/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/placemap/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                }
            }

            //景点
            SqlQueryText = "select id from OL_View";
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<url><loc>http://www.scyts.com/sightdetail/{1}.html</loc><lastmod>{0:yyyy-MM-dd}</lastmod><changefreq>Always</changefreq></url>\r\n", DateTime.Now, DS.Tables[0].Rows[i]["id"].ToString()));
                }
            }

            Strings.Append("</urlset>");

            string path = string.Format(@"{0}sitemap.xml", AppDomain.CurrentDomain.BaseDirectory);

            try
            {
                StreamWriter writer = new StreamWriter(path, false, Encoding.GetEncoding("UTF-8"));
                writer.WriteLine(Strings.ToString());
                writer.Close();
                Response.Write("{\"success\":0}");
            }
            catch
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        public void SetHotDestination()
        {
            string SqlQueryText = "";
            SqlQueryText = string.Format("update OL_Destination set hotflag='1' where id='{0}'",
                Request.QueryString["Id"]
            );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        public void SaveViewPic()
        {
            string SqlQueryText = "";
            if (Request.Form["id"] == "")
            {
                SqlQueryText = string.Format("insert into OL_ViewPic (desid,viewid,picname,picmemo,picurl) values ('{0}','{1}','{2}','{3}','{4}')",
                    Request.Form["desid"].Trim(),
                    Request.Form["viewid"].Trim(),
                    Request.Form["picname"].Trim(),
                    Request.Form["picmemo"].Trim(),
                    Request.Form["PicUrl1"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update OL_ViewPic set picname='{1}',picmemo='{2}',picurl='{3}' where id='{0}'",
                    Request.Form["id"].Trim(),
                    Request.Form["picname"].Trim(),
                    Request.Form["picmemo"].Trim(),
                    Request.Form["PicUrl1"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"success\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
        }

        public void SaveView()
        {
            string ucode = "";
            string SqlQueryText = "";
            if (Request.Form["uid"] == "")
            {
                Guid ucodes = CombineKeys.NewComb();
                ucode = ucodes.ToString();
                SqlQueryText = string.Format("insert into OL_View (uid,desid,viewname,tags,tel,address,ticket,ticketmemo,opentime,map_x,map_y,visitseason,visittime,intro,viewpoint,traffic,memo,map_size,PinYin,SortPinYin,SeoName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                    ucode,
                    Request.Form["desid"].Trim(),
                    Request.Form["viewname"].Trim(),
                    Request.Form["tag"].Trim(),
                    Request.Form["tel"].Trim(),
                    Request.Form["address"].Trim(),
                    Request.Form["ticket"].Trim(),
                    Request.Form["ticketmemo"].Trim(),
                    Request.Form["opentime"].Trim(),
                    MyConvert.ConToDouble(Request.Form["map_x"].Trim()),
                    MyConvert.ConToDouble(Request.Form["map_y"].Trim()),
                    Request.Form["visitseason"].Trim(),
                    Request.Form["visittime"].Trim(),
                    Request.Form["intro"].Trim(),
                    Request.Form["viewpoint"].Trim(),
                    Request.Form["traffic"].Trim(),
                    Request.Form["memo"].Trim(),
                    MyConvert.ConToInt(Request.Form["map_size"].Trim()),
                    Request.Form["PinYin"].Trim(),
                    Request.Form["SortPinYin"].Trim(),
                    Request.Form["SeoName"].Trim()
                );
            }
            else
            {
                ucode = Request.Form["uid"];
                SqlQueryText = string.Format("update OL_View set viewname='{1}',tags='{2}',tel='{3}',address='{4}',ticket='{5}',ticketmemo='{6}',opentime='{7}',map_x='{8}',map_y='{9}',visitseason='{10}',visittime='{11}',intro='{12}',viewpoint='{13}',traffic='{14}',memo='{15}',map_size='{16}',PinYin='{17}',SortPinYin='{18}',SeoName='{19}' where uid='{0}'",
                    ucode,
                    Request.Form["viewname"].Trim(),
                    Request.Form["tag"].Trim(),
                    Request.Form["tel"].Trim(),
                    Request.Form["address"].Trim(),
                    Request.Form["ticket"].Trim(),
                    Request.Form["ticketmemo"].Trim(),
                    Request.Form["opentime"].Trim(),
                    MyConvert.ConToDouble(Request.Form["map_x"].Trim()),
                    MyConvert.ConToDouble(Request.Form["map_y"].Trim()),
                    Request.Form["visitseason"].Trim(),
                    Request.Form["visittime"].Trim(),
                    Request.Form["intro"].Trim(),
                    Request.Form["viewpoint"].Trim(),
                    Request.Form["traffic"].Trim(),
                    Request.Form["memo"].Trim(),
                    MyConvert.ConToInt(Request.Form["map_size"].Trim()),
                    Request.Form["PinYin"].Trim(),
                    Request.Form["SortPinYin"].Trim(),
                    Request.Form["SeoName"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"" + ucode + "\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
        }

        protected void CacheReset()
        {
            string SqlQueryText = string.Format("select LineClass from OL_Line where MisLineId in ({0}) group by LineClass", Request.QueryString["Id"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Recommend_" + DS.Tables[0].Rows[i]["LineClass"].ToString(), "");
                }
                HttpContext.Current.Cache.Insert("Index_" + Request.QueryString["LineType"], "");
                HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Preferences", "");
                HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Hot", "");
            }
            Response.Write("{\"success\":0}");
        }

        protected void NewRecom(string DoFlag)
        {
            string SqlQueryText = "", DoText = "";
            DateTime date = DateTime.Today;

            switch (DoFlag)
            {
                case "S1":
                    DoText = ",IndexRecom='1'";
                    HttpContext.Current.Cache.Insert("Index_Famous", "");
                    break;
                case "S2":
                    DoText = ",IndexRecom='2'";
                    HttpContext.Current.Cache.Insert("Index_Preferences", "");
                    break;
                case "S3":
                    DoText = ",IndexRecom='3'";
                    break;
                case "S4":
                    DoText = ",IndexRecom='4'";
                    HttpContext.Current.Cache.Insert("Index_Hot", "");
                    break;
                case "E1":
                    DoText = ",NewRecom='1'";
                    break;
                case "E2":
                    DoText = ",NewRecom='2'";
                    break;
                case "E3":
                    DoText = ",NewRecom='3'";
                    break;
                case "E4":
                    DoText = ",NewRecom='4'";
                    break;
                case "F1":
                    DoText = ",famous='1'";
                    HttpContext.Current.Cache.Insert("Index_Famous", "");
                    break;
                case "F2":
                    DoText = ",famous='2'";
                    HttpContext.Current.Cache.Insert("Index_Famous", "");
                    break;
                case "F3":
                    DoText = ",famous='3'";
                    HttpContext.Current.Cache.Insert("Index_Famous", "");
                    break;
                case "cancel":
                    DoText = ",IndexRecom='0',NewRecom='0',famous=''";
                    break;
                case "W1":
                    DoText = ",WeChat='1'";
                    break;
                case "W2":
                    DoText = ",WeChat='2'";
                    break;
                case "top1":
                    DoText = ",TopEnd='" + date.AddDays(7).ToString() + "'";
                    break;
                case "top2":
                    DoText = ",TopEnd='" + date.AddDays(15).ToString() + "'";
                    break;
                case "top3":
                    DoText = ",TopEnd='" + date.AddDays(30).ToString() + "'";
                    break;
                case "top4":
                    DoText = ",TopEnd='" + date.AddDays(60).ToString() + "'";
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
            if (DoFlag == "top1" || DoFlag == "top2" || DoFlag == "top3" || DoFlag == "top4")
            {
                SqlQueryText = string.Format("update OL_Line set TopBegin='{0}'{1} where MisLineId in ({2})", DateTime.Now.ToString(), DoText, Request.QueryString["Id"]);
                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    //清空缓存

                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":1}");
                }
            }
            else
            {
                if (DoFlag == "W1" || DoFlag == "W2")
                {
                    SqlQueryText = string.Format("update OL_Line set WeChatSortTime='{0}'{1} where MisLineId in ({2})", DateTime.Now.ToString(), DoText, Request.QueryString["Id"]);
                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        //清空缓存
                        PurchaseAutoRun.DeleteSelectCache("WeChat_");
                        Response.Write("{\"success\":0}");
                    }
                    else
                    {
                        Response.Write("{\"success\":1}");
                    }
                }
                else
                {
                    SqlQueryText = string.Format("update OL_Line set NewSortTime='{0}'{1} where MisLineId in ({2})", DateTime.Now.ToString(), DoText, Request.QueryString["Id"]);
                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        SqlQueryText = string.Format("select LineClass from OL_Line where MisLineId in ({0}) group by LineClass", Request.QueryString["Id"]);
                        DataSet DS = new DataSet();
                        DS.Clear();
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Recommend_" + DS.Tables[0].Rows[i]["LineClass"].ToString(), "");
                            }
                            HttpContext.Current.Cache.Insert("Index_" + Request.QueryString["LineType"], "");
                            HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Preferences", "");
                            HttpContext.Current.Cache.Insert(Request.QueryString["LineType"] + "_Hot", "");
                        }
                        Response.Write("{\"success\":0}");
                    }
                    else
                    {
                        Response.Write("{\"success\":1}");
                    }
                }
            }


        }

        protected void DeleteDestinationClass()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_Summary where desid='{0}'", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":1}");
                Response.End();
            }
            SqlQueryText = string.Format("select top 1 id from OL_View where desid='{0}'", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":1}");
                Response.End();
            }
            string ClassList = GetProductClass.getParentDestinationById(Request.QueryString["Id"]);
            if (ClassList == null)
            {
                Response.Write("{\"success\":1}");
                Response.End();
            }
            if (MyDataBaseComm.DeleteExcuteSql("OL_Destination", string.Format("ClassList like '{0}%'", ClassList), "") == true)
            {
                GetProductClass.BindDestinationClassName();
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        public void LineDestination()
        {
            List<string> Sql = new List<string>();
            decimal xianzhi = MyConvert.ConToDec("0.03");
            if (MyConvert.ConToDec(Request.Form["Integral"]) > xianzhi)
            {
                Response.Write("({\"xianzhi\":\"error\"})");
                Response.End();
            }
            string DbName = "OL_Line";
            string DbId = "MisLineId";
            string SqlQueryText = "";
            if (Request.Form["flag"] == "view")
            {
                Sql.Add(string.Format("update OL_Line set viewids='{1}',viewname='{2}',viewlist='{3}',Integral='{4}'  where MisLineId='{0}'",
                    Request.Form["Cid"],
                    Request.Form["Destinationid"].Trim(),
                    Request.Form["DestinationName"].Trim(),
                    Request.Form["Des_List"].Trim(),
                    MyConvert.ConToDec(Request.Form["Integral"])
                ));
                Sql.Add(SqlQueryText = string.Format("delete from ViewDest where lineid='{0}'",
                    Request.Form["Cid"]
                ));
                if (Request.Form["Day_List"].Trim().Length > 0)
                {
                    string[] DestId = Request.Form["Destinationid"].Trim().Split(',');
                    string[] DayId = Request.Form["Day_List"].Trim().Split(',');
                    for (int i = 1; i < DestId.Length - 1; i++)
                    {
                        Sql.Add(SqlQueryText = string.Format("insert into ViewDest (lineid,viewid,days) values ('{0}','{1}','{2}')",
                            Request.Form["Cid"],
                            DestId[i],
                            DayId[i]
                        ));
                    }
                }
            }
            else
            {
                if (Request.Form["flag"] == "Destination")
                {
                    DbName = "OL_ProductType";
                    DbId = "id";
                    Sql.Add(SqlQueryText = string.Format("update {5} set Destinationid='{1}',Destination='{2}',DestinationList='{3}',FirstDestination='{4}'  where {6}='{0}'",
                        Request.Form["Cid"],
                        Request.Form["Destinationid"].Trim(),
                        Request.Form["DestinationName"].Trim(),
                        Request.Form["Des_List"].Trim(),
                        Request.Form["FirstDestination"].Trim(),
                        DbName, DbId
                    ));
                }
                else
                {
                    Sql.Add(SqlQueryText = string.Format("update {5} set Destinationid='{1}',Destination='{2}',DestinationList='{3}',FirstDestination='{4}',Integral='{7}'  where {6}='{0}'",
                        Request.Form["Cid"],
                        Request.Form["Destinationid"].Trim(),
                        Request.Form["DestinationName"].Trim(),
                        Request.Form["Des_List"].Trim(),
                        Request.Form["FirstDestination"].Trim(),
                        DbName, DbId,
                        MyConvert.ConToDec(Request.Form["Integral"])
                    ));
                    Sql.Add(SqlQueryText = string.Format("delete from LineDest where lineid='{0}'",
                        Request.Form["Cid"]
                    ));

                    if (Request.Form["Destinationid"].Trim().Length > 3)
                    {
                        string[] DestId = Request.Form["Destinationid"].Trim().Split(',');
                        for (int i = 1; i < DestId.Length - 1; i++)
                        {
                            Sql.Add(SqlQueryText = string.Format("insert into LineDest (lineid,destid) values ('{0}','{1}')",
                                Request.Form["Cid"],
                                DestId[i]
                            ));
                        }
                    }
                }

            }


            string[] SqlQueryList = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQueryList) == true)
            {
                Response.Write("({\"success\":\"ok\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
        }

        protected void DeleteFriendLink(string DbTableName)
        {
            if (MyDataBaseComm.DeleteExcuteSql(DbTableName, "", string.Format("{0}", Request.QueryString["Id"])) == true)
            {
                AfterDeleteInfo();
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void FriendLink()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into OL_FriendLink (LinkName,LinkUrl,rankid,LinkType) values ('{0}','{1}','{2}','{3}')",
                    Request.Form["LinkName"].Trim(),
                    Request.Form["LinkUrl"].Trim(),
                    MyConvert.ConToInt(Request.Form["rankid"]),
                    Request.Form["Select1"].Trim());
            }
            else
            {
                SqlQueryText = string.Format("update OL_FriendLink set LinkName='{1}',LinkUrl='{2}',rankid='{3}',LinkType='{4}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["LinkName"].Trim(),
                    Request.Form["LinkUrl"].Trim(),
                    MyConvert.ConToInt(Request.Form["rankid"]),
                    Request.Form["Select1"].Trim());
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                HttpContext.Current.Cache.Insert("SlidePic_FirendLink", "");
                Response.Write("({\"success\":0})");
            }
            else
            {
                Response.Write("({\"success\":1})");
            }
            Response.End();
        }

        protected void GetPinYin()
        {
            string CnName = Request.QueryString["CnName"];
            if (CnName.Length > 1 && CnName.Length < 50)
            {
                Response.Write("{\"py\":\"" + Spell.MakeSpellCode(CnName, SpellOptions.EnableUnicodeLetter).ToLower() + "\",\"sortpy\":\"" + AllPinYin.FirstIndexCode(CnName).ToLower() + "\"}");

                //Spell.MakeSpellCode(CnName, SpellOptions.EnableUnicodeLetter),
                //AllPinYin.FirstIndexCode(CnName),

            }
            else
            {
                Response.Write("{\"py\":\"\",\"sortpy\":\"\"}");
            }

        }


        protected void DeleteTempOrder()
        {
            string userid = "", username = "";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                Response.Write("{\"success\":\"只有后台操作人员才能删除!\"}");
                Response.End();
            }
            string SqlQueryText = string.Format("select * from OL_TempOrder where OrderId='{0}'", Request.QueryString["OrderId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {

                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) == 0)
                {
                    Response.Write("{\"success\":\"只有邮轮临时订单才能删除!\"}");
                    Response.End();
                }
                List<string> Sql = new List<string>();
                //Sql.Add(string.Format("update OL_Order set OrderFlag='9' where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from OL_Order where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from CR_RoomList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from CR_RoomOrder where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from OL_OrderExtend where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));

                Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_RoomList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_RoomOrder where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_OrderExtend where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_OrderLog where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));

                string[] SqlQueryList = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryList) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":\"订单删除失败，请稍后再试!\"}");
                }
            }

        }

        protected void InitData()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into InitData (ftype,fname,dataname,sort) values ('{0}','{1}','{2}','{3}')", Request.QueryString["ftype"].Trim(), Request.QueryString["fname"].Trim(), Request.QueryString["dataname"].Trim(), MyConvert.ConToInt(Request.QueryString["sort"]));
            }
            else
            {
                SqlQueryText = string.Format("update InitData set dataname='{1}',sort='{2}' where id={0}", Request.QueryString["Id"], Request.QueryString["dataname"].Trim(), MyConvert.ConToInt(Request.QueryString["sort"]));
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void PreferSend()
        {
            //string SqlQueryText = "";
            //SqlQueryText = string.Format("update OL_Affiche set EditTime='{0}' where Id in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
            int SendCount = 0;
            string SqlQueryText = "select * from View_Prefer where 1=1";

            if (Request.Form["CheckBox1"] != null)
            {
                SqlQueryText += " and LastLoginTime>=" + MyConvert.ConToDate(Request.Form["begindate"].Trim()) + " and LastLoginTime<=" + MyConvert.ConToDate(Request.Form["enddate"].Trim()) + "";
            }

            if (Request.Form["CheckBox2"] != null)
            {
                SqlQueryText += " and price>='" + MyConvert.ConToDec(Request.Form["price1"].Trim()) + "' and price<='" + MyConvert.ConToDec(Request.Form["price2"].Trim()) + "'";
            }

            if (Request.Form["CheckBox3"] != null)
            {
                SqlQueryText += " and age>='" + MyConvert.ConToDec(Request.Form["age1"].Trim()) + "' and age<='" + MyConvert.ConToDec(Request.Form["age2"].Trim()) + "'";
            }

            if (Request.Form["CheckBox4"] != null)
            {
                SqlQueryText += " and UserEmail='" + Request.Form["email"].Trim() + "'";
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {

                SqlQueryText = string.Format("select * from Pre_Policy where id='{0}'", Request.Form["Cid"].Trim());

                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {

                        String AutoId = Convert.ToString(CombineKeys.NewComb());

                        SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                            DS1.Tables[0].Rows[0]["id"].ToString(),
                            AutoId,
                            MyConvert.CreateVerifyCode(12),
                            DS1.Tables[0].Rows[0]["par"].ToString(),
                            DS1.Tables[0].Rows[0]["amount"].ToString(),
                            DS.Tables[0].Rows[i]["Id"].ToString(),
                            DS1.Tables[0].Rows[0]["begindate"].ToString(),
                            DS1.Tables[0].Rows[0]["enddate"].ToString(),
                            DateTime.Now.ToString(),
                            "0",
                            DS1.Tables[0].Rows[0]["deduction"].ToString(),
                            DS1.Tables[0].Rows[0]["range"].ToString(),
                            DS1.Tables[0].Rows[0]["product"].ToString(),
                            DS.Tables[0].Rows[i]["UserEmail"].ToString(),
                            DS.Tables[0].Rows[i]["UserName"].ToString(),
                            DS1.Tables[0].Rows[0]["pbdate"].ToString(),
                            DS1.Tables[0].Rows[0]["pedate"].ToString(),
                            DS1.Tables[0].Rows[0]["sellflag"].ToString()
                        );

                        if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                        {
                            if (SendEmail(DS.Tables[0].Rows[i]["UserName"].ToString(), DS.Tables[0].Rows[i]["UserEmail"].ToString(), DS1.Tables[0].Rows[0]["par"].ToString()) == "0") SendCount += 1;

                        }
                        //AdScriptr.Append(string.Format("<LI>· <A href=\"/News/{0}.html\" target=_blank>{1}</A></LI>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["AfficheName"].ToString()));
                    }
                    Response.Write("({\"success\":\"发放" + DS.Tables[0].Rows.Count + "张优惠券，邮件发送成功" + SendCount + "封\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"指定发送的优惠券不存在\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"没有查询到任何数据\"})");
            }
        }

        protected string SendEmail(string UserName, string EmailAdd, string Infos)
        {
            StringBuilder Strings = new StringBuilder();

            Strings.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            Strings.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><title>青旅网上商城优惠券</title></head>");
            Strings.Append("<body>");
            Strings.Append("<table width=\"650\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border: 1px solid #999999;\">");
            Strings.Append("<tr>");
            Strings.Append("<td style=\"font-size: 12px; line-height: 20px; text-align: left;\">");
            Strings.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style='width: 465.0pt; height: 72px; margin-left: 7.5pt; border: none; border-bottom: solid #333333 1.0pt'>");
            Strings.Append("<tr>");
            Strings.Append("<td width=\"192\" style='width: 144.0pt; height: 40px; border: none; padding: 20px 0px 0px 0px;'>");
            Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\"><img border=\"0\" id=\"_x0000_i1025\" src=\"http://www.scyts.com/Images/logo.gif\" alt=\"上海中国青年旅行社\" /></a>");
            Strings.Append("</td>");
            Strings.Append("<td width=\"428\" height=\"40\" valign=\"bottom\" align=\"right\" style='width: 321.0pt; border: none; padding: 0cm 0cm 2.5pt 0cm; font-size: 12px;'>");
            Strings.Append("<a href=\"http://www.scyts.com\" target=\"_blank\">上海中国青年旅行社首页</a>");
            Strings.Append("</td>");
            Strings.Append("</tr>");
            Strings.Append("</table>");
            Strings.Append("<table width=\"620\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-left: 10px;\">");
            Strings.Append("<tr>");
            Strings.Append("<td style=\"font-size: 12px; line-height: 25px; padding-top: 10px;\">");
            Strings.Append(string.Format("<strong>尊敬的{0}，您好:</strong>", UserName));
            Strings.Append("</td>");
            Strings.Append("</tr>");

            //Strings.Append("<tr>");
            //Strings.Append("<td style=\"line-height: 20px; padding-top: 0px; font-size: 12px;\">");
            //Strings.Append("您在上海中国青年旅行社网站（www.scyts.com）点击了“忘记密码”按钮，故系统自动为您发送了这封邮件。您可以点击以下链接修改您的密码：<br />");
            //Strings.Append(string.Format("<a href=\"http://www.scyts.com/login/ResetPassword.aspx?UID={0}\" target=\"_blank\">http://www.scyts.com/login/ResetPassword.aspx?UID={0}</a>", AutoId));
            //Strings.Append("</td>");
            //Strings.Append("</tr>");

            Strings.Append("<tr>");
            Strings.Append("<td style=\"line-height: 20px; padding-top: 8px; font-size: 12px;\">");
            Strings.Append("您在上海中国青年旅行社网站（www.scyts.com）收到了一张" + Infos + "元优惠券，请尽快登陆使用，谢谢您再次光临");
            Strings.Append("<br />");
            Strings.Append("</td>");
            Strings.Append("</tr>");

            Strings.Append("<tr>");
            Strings.Append("<td style=\"line-height: 20px; padding-top: 2px; font-size: 12px;\">");
            Strings.Append("<p><br>");
            Strings.Append("如有任何疑问，请联系上海中国青年旅行社客服，客服热线：<span lang=\"EN-US\" xml:lang=\"EN-US\">4006-777-666</span></p>");
            Strings.Append("</td>");
            Strings.Append("</tr>");
            Strings.Append("<tr>");
            Strings.Append("<td style=\"line-height: 40px; font-size: 12px\">");
            Strings.Append("<strong>欢迎您再次到上海中国青年旅行社预订旅游产品，祝您旅途愉快！<span lang=\"EN-US\" xml:lang=\"EN-US\"></span></strong>");
            Strings.Append("</td>");
            Strings.Append("</tr>");
            Strings.Append("<tr>");
            Strings.Append("<td align=\"left\" style='border: none; padding: 1.5pt 0cm 7.5pt 0cm; border-top: dashed #999999 1.0pt'>");
            Strings.Append("<p style='line-height: 15.0pt'>");
            Strings.Append("<span style=\"font-size: 9.0pt; color: #999999\">您之所以收到这封邮件，是因为您曾经注册成为上海中国青年旅行社网站的用户。<br />");
            Strings.Append("本邮件由系统自动发出，请勿直接回复！</p>");
            Strings.Append("</td>");
            Strings.Append("</tr>");
            Strings.Append("</table>");
            Strings.Append("</td>");
            Strings.Append("</tr>");
            Strings.Append("</table>");
            Strings.Append("</body>");
            Strings.Append("</html>");

            List<string> MailTo = new List<string>();
            MailTo.Add(EmailAdd);
            string result = SendEmailClass.SendMail(MailTo.ToArray(), "青旅网上商城优惠券(www.scyts.com)", Strings.ToString(), 1, 2, "null");

            return result;
            //if (result == "0")
            //{
            //    //Response.Write("({\"success\":\"" + AutoId + "\"})");
            //}
            //else
            //{
            //    //Response.Write("({\"info\":\"提交失败\"})");
            //}
        }

        protected void PreferSerch()
        {
            //string SqlQueryText = "";
            //SqlQueryText = string.Format("update OL_Affiche set EditTime='{0}' where Id in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);

            string SqlQueryText = "select count(1) as nums from View_Prefer where 1=1";

            if (Request.Form["CheckBox1"] != null)
            {
                SqlQueryText += " and LastLoginTime>=" + MyConvert.ConToDate(Request.Form["begindate"].Trim()) + " and LastLoginTime<=" + MyConvert.ConToDate(Request.Form["enddate"].Trim()) + "";
            }

            if (Request.Form["CheckBox2"] != null)
            {
                SqlQueryText += " and price>='" + MyConvert.ConToDec(Request.Form["price1"].Trim()) + "' and price<='" + MyConvert.ConToDec(Request.Form["price2"].Trim()) + "'";
            }

            if (Request.Form["CheckBox3"] != null)
            {
                SqlQueryText += " and age>='" + MyConvert.ConToDec(Request.Form["age1"].Trim()) + "' and age<='" + MyConvert.ConToDec(Request.Form["age2"].Trim()) + "'";
            }
            if (Request.Form["CheckBox4"] != null)
            {
                SqlQueryText += " and UserEmail='" + Request.Form["email"].Trim() + "'";
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"success\":\"查询到" + DS.Tables[0].Rows[0]["nums"].ToString() + "条记录\"})");
            }
            else
            {
                Response.Write("({\"error\":\"0\"})");
            }
        }

        protected void SavePrefer()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                Guid ucode = CombineKeys.NewComb();
                SqlQueryText = string.Format("insert into Pre_Policy (uid,sellflag,deduction,range,product,begindate,enddate,sellprice,par,amount,memo,userid,username,inputdate,picurl,pbdate,pedate,sellnums,pre_no) values ('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}')",
                    ucode,
                    Request.Form["sellflag"].Trim(),
                    Request.Form["deduction"].Trim(),
                    Request.Form["range"].Trim(),
                    Request.Form["product"].Trim(),
                    MyConvert.ConToDate(Request.Form["begindate"].Trim()),
                    MyConvert.ConToDate(Request.Form["enddate"].Trim()),
                    MyConvert.ConToDec(Request.Form["sellprice"].Trim()),
                    MyConvert.ConToDec(Request.Form["par"].Trim()),
                    MyConvert.ConToDec(Request.Form["amount"].Trim()),
                    Request.Form["memo"].Trim(),
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    DateTime.Now.ToString(),
                    Request.Form["logourl"].Trim(),
                    MyConvert.ConToDate(Request.Form["pbdate"].Trim()),
                    MyConvert.ConToDate(Request.Form["pedate"].Trim()),
                    MyConvert.ConToInt(Request.Form["sellnums"].Trim()),
                    Request.Form["pre_no"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update Pre_Policy set sellflag='{1}',deduction='{2}',range='{3}',product='{4}',begindate={5},enddate={6},sellprice='{7}',par='{8}',amount='{9}',memo='{10}',picurl='{11}',pbdate={12},pedate={13},sellnums='{14}',pre_no='{15}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["sellflag"].Trim(),
                    Request.Form["deduction"].Trim(),
                    Request.Form["range"].Trim(),
                    Request.Form["product"].Trim(),
                    MyConvert.ConToDate(Request.Form["begindate"].Trim()),
                    MyConvert.ConToDate(Request.Form["enddate"].Trim()),
                    MyConvert.ConToDec(Request.Form["sellprice"].Trim()),
                    MyConvert.ConToDec(Request.Form["par"].Trim()),
                    MyConvert.ConToDec(Request.Form["amount"].Trim()),
                    Request.Form["memo"].Trim(),
                    Request.Form["logourl"].Trim(),
                    MyConvert.ConToDate(Request.Form["pbdate"].Trim()),
                    MyConvert.ConToDate(Request.Form["pedate"].Trim()),
                    MyConvert.ConToInt(Request.Form["sellnums"].Trim()),
                    Request.Form["pre_no"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void SavePreferNums()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
                Response.End();
            }
            SqlQueryText = string.Format("update Pre_Policy set sellnums='{1}',begindate={2},enddate={3} where id={0}",
                Request.Form["Cid"],
                MyConvert.ConToInt(Request.Form["sellnums"].Trim()),
                MyConvert.ConToDate(Request.Form["begindate"].Trim()),
                MyConvert.ConToDate(Request.Form["enddate"].Trim())
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void SaveIntegral()
        {
            string SqlQueryText = "";
            try
            {
                if (Convert.ToInt32(Request.Form["integral"]) > 0)
                {
                    SqlQueryText = string.Format("INSERT INTO [dbo].[OL_Integral] ([uid],[integral],[getdate],[flag],[dept],[enddate]) " +
                                                                                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')"
                                        , Request.Form["Uid"]
                                        , Request.Form["integral"]
                                        , DateTime.Now.ToString()
                                        , '0'
                                        , '0'
                                        , DateTime.Now.AddYears(-1).ToString());

                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"error\":\"信息保存失败\"})");
                    }
                }
                else
                {
                    Response.Write("({\"error\":\"信息保存失败\"})");
                }

            }
            catch (Exception)
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void SaveInitDataInfo()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.Form["id"]) == 0)
            {
                SqlQueryText = string.Format("insert into InitData (ftype,fname,dataname,sort) values ('{0}','{1}','{2}','{3}') "
                                    , Request.Form["ftype"].Split('|')[0]
                                    , Request.Form["ftype"].Split('|')[1]
                                    , Request.Form["dataname"]
                                    , Request.Form["sort"]);
            }
            else
            {
                SqlQueryText = string.Format("update InitData set dataname = '{1}',sort = '{2}' where id = '{0}' "
                                    , Request.Form["id"]
                                    , Request.Form["dataname"]
                                    , Request.Form["sort"]);
            }
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void AdjustPrice()
        {
            string SqlQueryText = string.Format("select OrderFlag from OL_Order where OrderId='{0}'", Request.QueryString["OrderId"].Trim());
            //Response.Write(SqlQueryText);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9")
                {
                    Response.Write("{\"success\":\"已经删除的订单不能操作\"}");
                    Response.End();
                }
            }

            int price = 0;
            int nums = MyConvert.ConToInt(Request.QueryString["Nums"].Trim());
            int allprice = 0;
            
            if (Request.QueryString["Flag"] == "1")
            {
                price = MyConvert.ConToInt(Request.QueryString["Price"].Trim());
            }
            else
            {
                price = 0 - MyConvert.ConToInt(Request.QueryString["Price"].Trim());
            }
            allprice = price * nums;
            List<string> Sql = new List<string>();

            Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    Request.QueryString["OrderId"].Trim(),
                    "ExtPrice", "0", "费用调整",
                    Request.QueryString["PriceName"].Trim(),
                    price,
                    Request.QueryString["Nums"].Trim(),
                    allprice,
                    DateTime.Now.ToString()
                )
            );

            Sql.Add(string.Format("update OL_Order set price=price + {1} where OrderId='{0}'",
                    Request.QueryString["OrderId"].Trim(),
                    allprice
                )
            );

            Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", Request.QueryString["OrderId"].Trim(), DateTime.Now.ToString(), Session["Manager_UserName"] + " 调整订单费用项目"));
            
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                if (Request.QueryString["Cruises"].Trim() == "Cruises")
                {
                    string result = PurchaseClass.CruisesOrderAdjust(Request.QueryString["OrderId"], "AdjustPrice", "Yes");
                    if (result == "OK")
                    {
                        Response.Write("{\"success\":\"OK\"}");
                    }
                    else
                    {
                        Response.Write("{\"success\":\"操作已成功完成，但畅游同步失败（" + result + "），请到同步记录中查看！\"}");
                    }
                }
                else
                {
                    Response.Write("{\"success\":\"OK\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"信息保存失败，请稍后再试!\"}");
            }
        }

        protected void ManageLine(string DoFlag)
        {
            string SqlQueryText="";
            switch (DoFlag)
            {
                case "Top":
                    SqlQueryText = string.Format("update OL_Line set EditTime='{0}' where MisLineId in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
                    break;
                case "Preferences":
                    SqlQueryText = string.Format("update OL_Line set Preferences='1' where MisLineId in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
                    break;
                case "Recommend":
                    LineClass.ClearRecommendCache(Request.QueryString["LineClass"]);
                    SqlQueryText = string.Format("update OL_Line set Recommend='1' where MisLineId in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
                    break;
                case "CancelPreferences":
                    SqlQueryText = string.Format("update OL_Line set Preferences='0' where MisLineId in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
                    break;
                case "CancelRecommend":
                    LineClass.ClearRecommendCache(Request.QueryString["LineClass"]);
                    SqlQueryText = string.Format("update OL_Line set Recommend='0' where MisLineId in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
                    break;
                
                case "CreatePreferencesJs":
                    LinePreferences.CreatePreferencesJs();
                    Response.Write("{\"success\":0}");
                    Response.End();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }

            if (Request.QueryString["LineType"] == "Visa")
            {
                LineClass.CreateVisaList(Request.QueryString["LineClass"]);
                HttpContext.Current.Cache.Insert("IndexVisa", "");
            }
            else
            { 
                LineClass.CreateLineList(Request.QueryString["LineType"], Request.QueryString["LineClass"]);
                if (Request.QueryString["LineType"] == "Cruises") HttpContext.Current.Cache.Insert("IndexCruises", "");
            
            }
                
        }

        protected void SetTopAffiche()
        {
            string SqlQueryText = "";
            SqlQueryText = string.Format("update OL_Affiche set EditTime='{0}' where Id in ({1})", DateTime.Now.ToString(), Request.QueryString["Id"]);
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
        }

        protected void SaveProductClass()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                if (Request.QueryString["sel1"] == "")
                {
                    SqlQueryText = string.Format("insert into OL_ProductClass (ProductName,ProductUrl,ProductSort,MisClassId,ClassList) values ('{0}','{1}','{2}','{3}','{4},')",
                        Request.QueryString["ProductName"].Trim(),
                        Request.QueryString["ProductUrl"].Trim().Replace("@", "/"),
                        Request.QueryString["ProductSort"].Trim(),
                        Request.QueryString["MisClassId"].Trim(),
                        MyConvert.GetTimeRandomCode()
                    );
                }
                else
                {
                    string Parentid = Request.QueryString["sel2"];
                    if (Parentid == "") Parentid = Request.QueryString["sel1"];

                    GetProductClass.ProductParent ParentInfo = new GetProductClass.ProductParent();
                    ParentInfo = GetProductClass.GetParentInfo(Parentid);
                    if (ParentInfo == null)
                    {
                        Response.Write("{\"success\":1}");
                        Response.End();
                    }
                    SqlQueryText = string.Format("insert into OL_ProductClass (ParentId,ProductName,ProductUrl,ProductSort,MisClassId,ClassPath,ClassList,ClassLevel,ProductType) values ('{0}','{1}','{2}','{3}','{4}','{5},{6}','{7}{8},','{9}','{10}')",
                        Parentid,
                        Request.QueryString["ProductName"].Trim(),
                        Request.QueryString["ProductUrl"].Trim().Replace("@", "/"),
                        Request.QueryString["ProductSort"].Trim(),
                        Request.QueryString["MisClassId"].Trim(),
                        ParentInfo.ClassPath,
                        Parentid,
                        ParentInfo.ClassList,
                        MyConvert.GetTimeRandomCode(),
                        ParentInfo.ClassLevel+1,
                        Request.QueryString["ProductType"]
                    );
                }
            }
            else
            {
                SqlQueryText = string.Format("update OL_ProductClass set ProductName='{1}',ProductUrl='{2}',ProductSort='{3}',MisClassId='{4}',ProductType='{5}' where id={0}",
                    Request.QueryString["Id"],
                    Request.QueryString["ProductName"].Trim(),
                    Request.QueryString["ProductUrl"].Trim().Replace("@", "/"),
                    Request.QueryString["ProductSort"].Trim(),
                    Request.QueryString["MisClassId"].Trim(),
                    Request.QueryString["ProductType"]
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                LineClass.ClearCache( Request.QueryString["MisClassId"].Trim());
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
        }

        protected void SaveDestinationClass()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                if (Request.QueryString["sel1"] == "")
                {
                    //Spell.MakeSpellCode(Request.QueryString["DestinationName"].Trim(), SpellOptions.EnableUnicodeLetter),
                    //AllPinYin.FirstIndexCode(Request.QueryString["DestinationName"].Trim()),
                    SqlQueryText = string.Format("insert into OL_Destination (Dtype,DestinationName,Ename,PinYin,SortPinYin,SortNum,ClassList,map_x,map_y,map_size) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6},','{7}','{8}','{9}')",
                        Request.QueryString["Dtype"].Trim(),
                        Request.QueryString["DestinationName"].Trim(),
                        Request.QueryString["Ename"].Trim(),
                        Request.QueryString["PinYin"].Trim(),
                        Request.QueryString["SortPinYin"].Trim(),
                        Request.QueryString["SortNum"].Trim(),
                        MyConvert.GetTimeRandomCode(),
                        Request.QueryString["map_x"].Trim(),
                        Request.QueryString["map_y"].Trim(),
                        Request.QueryString["map_size"].Trim()
                    );
                }
                else
                {
                    string Parentid = Request.QueryString["sel3"];
                    if (Parentid == "") Parentid = Request.QueryString["sel2"];
                    if (Parentid == "") Parentid = Request.QueryString["sel1"];

                    GetProductClass.ProductParent ParentInfo = new GetProductClass.ProductParent();
                    ParentInfo = GetProductClass.GetDestinationParentInfo(Parentid);
                    if (ParentInfo == null)
                    {
                        Response.Write("{\"success\":1}");
                        Response.End();
                    }
                    SqlQueryText = string.Format("insert into OL_Destination (ParentId,ClassPath,ClassList,ClassLevel,Dtype,DestinationName,Ename,PinYin,SortPinYin,SortNum,map_x,map_y,map_size) values ('{0}','{1},{2}','{3}{4},','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                        Parentid,
                        ParentInfo.ClassPath,
                        Parentid,
                        ParentInfo.ClassList,
                        MyConvert.GetTimeRandomCode(),
                        ParentInfo.ClassLevel + 1,
                        Request.QueryString["Dtype"],
                        Request.QueryString["DestinationName"].Trim(),
                        Request.QueryString["Ename"].Trim(),
                        Request.QueryString["PinYin"].Trim(),
                        Request.QueryString["SortPinYin"].Trim(),
                        Request.QueryString["SortNum"].Trim(),
                        Request.QueryString["map_x"].Trim(),
                        Request.QueryString["map_y"].Trim(),
                        Request.QueryString["map_size"].Trim()
                    );
                }
            }
            else
            {
                SqlQueryText = string.Format("update OL_Destination set DestinationName='{1}',Ename='{2}',PinYin='{3}',SortPinYin='{4}',SortNum='{5}',map_x='{6}',map_y='{7}',map_size='{8}' where id={0}",
                    Request.QueryString["Id"],
                    Request.QueryString["DestinationName"].Trim(),
                    Request.QueryString["Ename"].Trim(),
                    Request.QueryString["PinYin"].Trim(),
                    Request.QueryString["SortPinYin"].Trim(),
                    Request.QueryString["SortNum"],
                    Request.QueryString["map_x"].Trim(),
                    Request.QueryString["map_y"].Trim(),
                    Request.QueryString["map_size"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                //LineClass.ClearCache(Request.QueryString["MisClassId"].Trim());
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
        }

        protected void SaveProductType()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into OL_ProductType (ProductName,ProductType,ProductSort,MisClassId) values ('{0}','{1}','{2}','{3}')",
                        Request.QueryString["ProductName"].Trim(),
                        Request.QueryString["ProductType"].Trim(),
                        Request.QueryString["ProductSort"].Trim(),
                        Request.QueryString["MisClassId"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update OL_ProductType set ProductName='{1}',ProductType='{2}',ProductSort='{3}',MisClassId='{4}' where id={0}",
                    Request.QueryString["Id"],
                    Request.QueryString["ProductName"].Trim(),
                    Request.QueryString["ProductType"].Trim(),
                    Request.QueryString["ProductSort"].Trim(),
                    Request.QueryString["MisClassId"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                //HttpContext.Current.Cache.Insert(string.Format("Index{0}Html", Request.QueryString["ProductType"]), "");
                CreateProductClass.ClearCache(Request.QueryString["ProductType"]);
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (HttpContext.Current.Server.GetLastError() is HttpRequestValidationException)
            {
                HttpContext.Current.Response.Write("请输入合法的字符串返回");
                HttpContext.Current.Server.ClearError();
            }
        }

        protected void SaveFlashAd()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into OL_FlashAD (AdName,AdSort,AdPicUrl,AdSecPicUrl,AdPageUrl,AdFlag,EditTime,MisClassId,HideFlag,BackGround) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                    Request.QueryString["AdName"].Trim(),
                    Request.QueryString["AdSort"].Trim(),
                    Request.QueryString["PicUrl1"].Trim(),
                    Request.QueryString["PicUrl2"].Trim(),
                    Request.QueryString["AdUrl"].Trim().Replace("@", "/"),
                    Request.QueryString["AdFlag"].Trim(), 
                    DateTime.Now.ToString(),
                    Request.QueryString["MisClassId"].Trim(),
                    Request.QueryString["HideFlag"],
                    Request.QueryString["BackGround"]
                ); 
            }
            else
            {
                SqlQueryText = string.Format("update OL_FlashAD set AdName='{1}',AdPicUrl='{2}',AdSecPicUrl='{3}',AdPageUrl='{4}',EditTime='{5}',AdSort='{6}',MisClassId='{7}',HideFlag='{8}',BackGround='{9}' where id={0}", 
                    Request.QueryString["Id"], 
                    Request.QueryString["AdName"].Trim(),
                    Request.QueryString["PicUrl1"].Trim(),
                    Request.QueryString["PicUrl2"].Trim(),
                    Request.QueryString["AdUrl"].Trim().Replace("@", "/"),
                    DateTime.Now.ToString(),
                    Request.QueryString["AdSort"].Trim(),
                    Request.QueryString["MisClassId"].Trim(),
                    Request.QueryString["HideFlag"],
                    Request.QueryString["BackGround"]
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                //CreateAdScript.AdScriptCreateNow(Request.QueryString["AdFlag"]);
                HttpContext.Current.Cache.Insert("SlidePic_" + Request.QueryString["AdFlag"], "");
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void UserRightEdit(string DoFlag)
        {
            UserRight.UserRightClass RUser = new UserRight.UserRightClass { Id = Request.QueryString["Id"], RightName = Request.QueryString["RightName"].Trim(), RightCode = Request.QueryString["RightCode"], RightFlag = Request.QueryString["RightFlag"] };

            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                if (UserRight.UserRight_Sql(RUser, "AddNew") != true)
                {
                    Response.Write("{\"success\":1}");
                    Response.End();
                }
            }
            else
            {
                if (UserRight.UserRight_Sql(RUser, "EditInfo") != true)
                {
                    Response.Write("{\"success\":1}");
                    Response.End();
                }
            }

            Response.Write("{\"success\":0}");
            Response.End();

        }

        protected void DeleteSelectInfos(string DbTableName)
        {
            if (MyDataBaseComm.DeleteExcuteSql(DbTableName, "", string.Format("'{0}'", Request.QueryString["Id"])) == true)
            {
                AfterDeleteInfo();
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void DeleteProductClass()
        {
            string ClassList = GetProductClass.getParentClassListById(Request.QueryString["Id"]);
            if (MyDataBaseComm.DeleteExcuteSql("OL_ProductClass", string.Format("ClassList like '{0}%'", ClassList), "") == true)
            {
                AfterDeleteInfo();
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        //删除事件执行之后，做一些事情
        protected void AfterDeleteInfo()
        {
            switch (Request.QueryString["action"])
            {
                case "DeleteAdInfos":
                    CreateAdScript.AdScriptCreateNow(Request.QueryString["AdFlag"]);
                    break;
                case "DeleteAfficheInfos":
                    HttpContext.Current.Cache.Insert(string.Format("AfficheHtml{0}", Request.QueryString["AfficheFlag"]), "");
                    HttpContext.Current.Cache.Insert("Announcement", "");
                    //CreateAfficheHtml.CreateAffiche(Request.QueryString["AfficheFlag"]);
                    break;
                case "DeleteProductClass":
                    //LineClass.ClearCache( Request.QueryString["MisClassId"].Trim());
                    GetProductClass.BindClassName();
                    break;
                case "DeleteProductType":
                    //HttpContext.Current.Cache.Insert(string.Format("Index{0}Html", Request.QueryString["ProductType"]), "");
                    CreateProductClass.ClearCache(Request.QueryString["ProductType"]);
                    break;
                default:
                    break;
            }
        }

        protected void SaveDept()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["Id"]) == 0)
            {
                SqlQueryText = string.Format("insert into OL_Dept (DeptName,ErpId) values ('{0}','{1}')", Request.QueryString["DeptName"].Trim(), Request.QueryString["ErpId"].Trim());
            }
            else
            {
                SqlQueryText = string.Format("update OL_Dept set DeptName='{1}',ErpId='{2}' where id={0}", Request.QueryString["Id"], Request.QueryString["DeptName"].Trim(), Request.QueryString["ErpId"].Trim());
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void CheckUser()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_ManageUser where LoginName='{0}'", Request.QueryString["LoginName"].Trim());
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":1}");
                Response.End();
            }
            else
            {
                Response.Write("{\"success\":0}");
                Response.End();
            }
        }

        protected void SaveUser()
        {
            ManageUsers.UserClass RUser = new ManageUsers.UserClass();
            if (Request.QueryString["Id"] != "")
            {
                RUser.Id = Request.QueryString["Id"];
            }
            else 
            {
                RUser.Id = Convert.ToString(CombineKeys.NewComb());
            }

            if (Request.QueryString["LoginPassWord"] != "")
            {
                RUser.LoginPassWord = SecurityCode.Md5_Encrypt(Request.QueryString["LoginPassWord"].Trim(), 32);
            }
            else
            {
                RUser.LoginPassWord = Request.QueryString["OldPassWord"];
            }
            RUser.LoginName = Request.QueryString["LoginName"].Trim();
            RUser.UserName = Request.QueryString["UserName"].Trim();
            RUser.UserRight = MyConvert.ConToInt(Request.QueryString["UserRight"]);
            RUser.UserDept = MyConvert.ConToInt(Request.QueryString["UserDept"]);

            if (Request.QueryString["Id"] != "")
            {
                if (ManageUsers.LoginUser_Sql(RUser, "EditInfo") != true)
                {
                    Response.Write("{\"success\":1}");
                    Response.End();
                }
            }
            else
            {
                if (ManageUsers.LoginUser_Sql(RUser, "Regist") != true)
                {
                    Response.Write("{\"success\":1}");
                    Response.End();
                }
            }
            Response.Write("{\"success\":0}");
            Response.End();

        }

        protected void EditPass_CheckAuthcode()
        {
            if (String.Compare(Request.Cookies["CheckCode"].Value, Request.Form["authcode"].Trim(), true) != 0)
            {
                Response.Write("({\"authcode\":\"验证码错误\"})");
                Response.End();
            }
        }

        protected void EditPass_CheckPassWord()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_ManageUser where Id='{0}' and LoginPassWord='{1}'", Convert.ToString(Session["Manager_UserId"]), SecurityCode.Md5_Encrypt(Request.Form["loginpwd"].Trim(), 32));
            if (MyDataBaseComm.getScalar(SqlQueryText) == null)
            {
                Response.Write("({\"pwd\":\"旧登录密码不正确\"})");
                Response.End();
            }
        }

        protected void EditPass_Change()
        {
            ManageUsers.UserClass RUser = new ManageUsers.UserClass { Id = Convert.ToString(Session["Manager_UserId"]), LoginPassWord = SecurityCode.Md5_Encrypt(Request.Form["pwd"].Trim(), 32) };
            if (ManageUsers.LoginUser_Sql(RUser, "PassWord") != true)
            {
                Response.Write("({\"info\":\"密码修改失败\"})");
            }
            else
            {
                Response.Write("({\"success\":\"密码修改成功\"})");
            }
            RUser = null;
            Response.End();

        }

        protected void SavePreferInfo()
        {
            //校验线路编号是否正确
            string SqlQueryText = string.Format("select * from ol_line where misLineId = {0}", Request.Form["LineId"].Trim());
            string lineName ="", lineType="";
            DateTime startDate = MyConvert.ConToDateTime(Request.Form["startDate"].Trim());
            DateTime endDate = MyConvert.ConToDateTime(Request.Form["endDate"].Trim());

            DataSet DS = new DataSet();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                lineType = DS.Tables[0].Rows[0]["lineType"].ToString();
            }
            else
            {
                Response.Write("({\"error\":\"未找到线路编号对应的旅游线路\"})");
                return;
            }

            if (lineType.Equals("Cruises") && lineType.Equals("Visa"))
            {
                Response.Write("({\"error\":\"无法修改邮轮或签证产品\"})");
                return;
            }

            if (lineType.Equals("OutBound") && Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@10") == -1)
            {
                Response.Write("({\"error\":\"您没有权限修改出境旅游线路\"})");
                return;
            }

            if (lineType.Equals("InLand") && Convert.ToString(Session["Manager_UserRight"]).IndexOf("@5@9") == -1)
            {
                Response.Write("({\"error\":\"您没有权限修改国内旅游线路\"})");
                return;
            }

            int Cid = MyConvert.ConToInt(Request.Form["Cid"]);
            //校验日期是否重复
            SqlQueryText = string.Format("select * from OL_Preferential where LineId = {0}", Request.Form["LineId"].Trim());
            DS = new DataSet();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (Cid == 0 || Cid != MyConvert.ConToInt(DS.Tables[0].Rows[i]["id"].ToString()))
                    {
                        DateTime startDateFromDB = MyConvert.ConToDateTime(DS.Tables[0].Rows[i]["startDate"].ToString());
                        DateTime endDateFromDB = MyConvert.ConToDateTime(DS.Tables[0].Rows[i]["endDate"].ToString());
                        if ((startDate.CompareTo(startDateFromDB) >= 0 && startDate.CompareTo(endDateFromDB) <= 0) || (endDate.CompareTo(startDateFromDB) >= 0 && endDate.CompareTo(endDateFromDB) <= 0))
                        {
                            Response.Write("({\"error\":\"日期重复请核对优惠日期\"})");
                            return;
                        }
                    }
                }
            }

            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into dbo.OL_Preferential (lineId,lineName,lineType,startDate,endDate,preferAmount,EditTime,EditUserId,EditUserName,pStartDate,pEndDate) values ('{0}','{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},{10})",
                    Request.Form["LineId"].Trim(),
                    lineName,
                    lineType,
                    MyConvert.ConToDate(Request.Form["startDate"].Trim()),
                    MyConvert.ConToDate(Request.Form["endDate"].Trim()),
                    MyConvert.ConToDouble(Request.Form["preferAmount"].Trim()),
                    DateTime.Now,
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    MyConvert.ConToDate(Request.Form["pStartDate"].Trim()),
                    MyConvert.ConToDate(Request.Form["pEndDate"].Trim())
                );
            }
            else
            {
                SqlQueryText = string.Format("update dbo.OL_Preferential set lineId='{1}',lineName='{2}',lineType='{3}',startDate={4},endDate={5},preferAmount={6},EditTime='{7}',EditUserId='{8}',EditUserName='{9}',pStartDate={10},pEndDate={11} where id={0}",
                    Request.Form["Cid"],
                    Request.Form["LineId"].Trim(),
                    lineName,
                    lineType,
                    MyConvert.ConToDate(Request.Form["startDate"].Trim()),
                    MyConvert.ConToDate(Request.Form["endDate"].Trim()),
                    MyConvert.ConToDouble(Request.Form["preferAmount"].Trim()),
                    DateTime.Now,
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    MyConvert.ConToDate(Request.Form["pStartDate"].Trim()),
                    MyConvert.ConToDate(Request.Form["pEndDate"].Trim())
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        public void SaveTradingAreaInfo()
        {
            Boolean result = false;
            TradingArea tradingArea = new TradingArea();
            tradingArea.id = Convert.ToInt16(Request.Form["Cid"]);
            tradingArea.name = Convert.ToString(Request.Form["name"]);
            tradingArea.flag = Convert.ToString(Request.Form["flag"]);
            tradingArea.pic = Convert.ToString(Request.Form["pic"]);
            tradingArea.detail = Convert.ToString(Request.Form["detail"]);
            tradingArea.destid = Convert.ToString(Request.Form["destid"]);
            tradingArea.destname = Convert.ToString(Request.Form["destname"]);
            if (Request.Form["Cid"] != "")
            {
                result = TradingAreaService.updateTradingArea(tradingArea);
            }else{
                result = TradingAreaService.insertTradingArea(tradingArea);
            }
            if (result)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        public void SaveTActivityInfo()
        {
            Boolean result = false;
            TActivity activity = new TActivity();
            
            activity.name = Convert.ToString(Request.Form["name"]);
            activity.context = Convert.ToString(Request.Form["context"]);
            activity.key = Convert.ToString(Request.Form["title"]);
            activity.color = Convert.ToString(Request.Form["color"]);
            activity.tradingAreaId = Convert.ToString(Request.Form["tradingAreaId"]);
            if (Request.Form["Cid"] != "")
            {
                activity.id = Convert.ToInt16(Request.Form["Cid"]);
                result = TradingAreaService.updateTActivity(activity);
            }
            else
            {
                result = TradingAreaService.insertTActivity(activity);
            }
            
            if (result)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        public void SaveTCouponInfo()
        {
            Boolean result = false;
            TCoupon coupon = new TCoupon();
            coupon.starDate = Convert.ToDateTime(Request.Form["starDate"]);
            coupon.endDate = Convert.ToDateTime(Request.Form["endDate"]);
            coupon.context = Convert.ToString(Request.Form["context"]);
            coupon.barCode = Convert.ToString(Request.Form["barcode"]);
            coupon.tradingAreaId = Convert.ToString(Request.Form["tradingAreaId"]);
            if (Request.Form["Cid"] != "")
            {
                coupon.id = Convert.ToInt16(Request.Form["Cid"]);
                result = TradingAreaService.updateTCoupon(coupon);
            }
            else
            {
                result = TradingAreaService.insertTCoupon(coupon);
            }
            
            if (result)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        public void SaveTStoreInfo()
        {
            Boolean result = false;
            TStore store = new TStore();
            store.name = Convert.ToString(Request.Form["name"]);
            store.link = Convert.ToString(Request.Form["link"]);
            store.context = Convert.ToString(Request.Form["context"]);
            store.pic = Convert.ToString(Request.Form["pic"]);
            store.tradingAreaId = Convert.ToString(Request.Form["tradingAreaId"]);
            if (Request.Form["Cid"] != "")
            {
                store.id = Convert.ToInt16(Request.Form["Cid"]);
                result = TradingAreaService.updateTStore(store);
            }
            else
            {
                result = TradingAreaService.insertTStore(store);
            }
            
            if (result)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        public void AuditCommentInfo()
        {
            string id = Request.Form["Cid"].ToString();
            if (CommentInfoService.AuditCommentInfo(id))
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息审核失败\"})");
            }
        }

        public void RecomJournals()
        {
            string id = Request.QueryString["Id"].ToString();
            string DoFlag = Request.QueryString["DoFlag"].ToString();
            string SqlQueryText = "";
            SqlQueryText = string.Format("update OL_Journal set Recom='{1}',EditDate='{2}' where uid='{0}'",
                    id,
                    DoFlag,
                    DateTime.Now.ToString()
            );
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
        }

        protected void SaveGroupInfo()
        {
            //校验线路编号是否正确
            string SqlQueryText = "";
            string exist = MyDataBaseComm.getScalar(string.Format("select count(1) from ol_line where misLineId = {0}", Request.Form["MisLineId"].Trim()));

            if (MyConvert.ConToInt(exist) == 0)
            {
                Response.Write("({\"error\":\"未找到线路编号对应的旅游线路\"})");
                return;
            }

            int Cid = MyConvert.ConToInt(Request.Form["Cid"]);

            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into dbo.OL_GroupPlan (MisLineId,Discount,Num,GroupDate,EditTime,EditUserId,EditUserName,pre_price) values ('{0}','{1}','{2}',{3},'{4}','{5}','{6}',{7})",
                    Request.Form["MisLineId"].Trim(),
                    Request.Form["Discount"].Trim(),
                    Request.Form["Num"].Trim(),
                    MyConvert.ConToDate(Request.Form["GroupDate"].Trim()),
                    DateTime.Now.ToString(),
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    Request.Form["pre_price"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update dbo.OL_GroupPlan set MisLineId={1},Discount={2},Num={3},GroupDate={4},EditTime='{5}',EditUserId='{6}',EditUserName='{7}',pre_price={8} where id={0}",
                    Request.Form["Cid"],
                    Request.Form["MisLineId"].Trim(),
                    Request.Form["Discount"].Trim(),
                    Request.Form["Num"].Trim(),
                    MyConvert.ConToDate(Request.Form["GroupDate"].Trim()),
                    DateTime.Now.ToString(),
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    Request.Form["pre_price"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }
    }
}