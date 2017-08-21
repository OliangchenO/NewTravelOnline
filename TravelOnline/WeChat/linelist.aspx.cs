using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TravelOnline.WeChat.jssdk;
using TravelOnline.LoginUsers;
using Belinda.Jasp;
using System.Xml;

namespace TravelOnline.WeChat
{
    public partial class linelist : System.Web.UI.Page
    {
        public string viewflag = "page", titlename, linetype, lineclass, lineclasslist, lineid, FirstPic, LineFeature;
        public string typename, areaname, page_url, linerecomm,searchstring,config;
        public object lineclasslist_New, linerecomm_New;
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断是否为微信浏览器
            string userAgent = Request.UserAgent;
            if (userAgent.ToLower().Contains("micromessenger"))
            {
                JsSDKTools tool = new JsSDKTools();
                config = tool.getSignPackage();
            }
            if (null != Request.QueryString["userId"] && Convert.ToString(Session["Fx_Login"]).Length == 0)
            {
                LoginUser.RegistUser user = LoginUser.LoginFxUser("Id='" + Request.QueryString["userId"].ToString() + "'");
                if (null != user)
                {
                    Session["Fx_UserId"] = user.Id;
                    Session["Fx_Mobile"] = user.Mobile;
                    Session["Fx_UserEmail"] = user.UserEmail;
                    Session["Fx_UserName"] = user.UserName;
                    Session["Fx_Wxid"] = user.Wxid;
                    Session["Fx_Storename"] = user.Storename;
                    Session["Fx_Tel"] = user.Tel;
                    Session["Fx_Address"] = user.Address;
                    Session["Fx_Headimgurl"] = user.headimgurl;
                    Session["ThirdPartyID"] = user.ThirdPartyID;
                }
            }
            linetype = Request.QueryString["linetype"];
            page_url = Request.QueryString["linetype"];
            searchstring = Request.Form["search"];
            //lineclass = Request.QueryString["linetype"];
            //Response.Write(linetype);
            if (!string.IsNullOrEmpty(linetype))
            {
                if (linetype.IndexOf("?") > -1)
                {
                    lineclass = linetype.Split("?".ToCharArray())[0];
                    lineid = linetype.Split("?".ToCharArray())[1];
                }
                if (MyConvert.ConToDec(linetype) > 0)
                {
                    viewflag = "list";
                    lineclass = linetype;
                    string SqlQueryText = string.Format("select ProductType,ProductName from OL_ProductType where MisClassId='{0}'", lineclass);
                    //return SqlQueryText;
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        linetype = DS.Tables[0].Rows[0]["ProductType"].ToString().ToLower();
                        areaname = DS.Tables[0].Rows[0]["ProductName"].ToString();
                    }
                }
                if (MyConvert.ConToDec(lineid) > 0)
                {
                    viewflag = "line";
                    string SqlQueryText = string.Format("select LineName,LineType,LineClass,Pics,LineFeature from OL_Line where MisLineId='{0}'", lineid);
                    //string SqlQueryText = string.Format("select Cname,Types,LineType from tbLine where Id='{0}'", lineid);
                    //return SqlQueryText;
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        linetype = DS.Tables[0].Rows[0]["LineType"].ToString().ToLower();
                        titlename = DS.Tables[0].Rows[0]["LineName"].ToString();
                        lineclass = DS.Tables[0].Rows[0]["LineClass"].ToString();
                        if (DS.Tables[0].Rows[0]["Pics"].ToString().Length == 24) FirstPic = string.Format("/Images/Views/{0}/S_{1}", DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[0], DS.Tables[0].Rows[0]["Pics"].ToString().Split("/".ToCharArray())[1]);
                        LineFeature = DS.Tables[0].Rows[0]["LineFeature"].ToString();
                        //linetype = DS.Tables[0].Rows[0]["Types"].ToString().ToLower();
                        //titlename = DS.Tables[0].Rows[0]["Cname"].ToString();
                        //lineclass = DS.Tables[0].Rows[0]["LineType"].ToString();
                    }
                }
            }
            
            switch (linetype)
            {
                case "outbound":
                    typename = "出境旅游";
                    break;
                case "inland":
                    typename = "国内旅游";
                    break;
                case "freetour":
                    typename = "自由行";
                    break;
                case "cruises":
                    typename = "邮轮旅游";
                    break;
                case "visa":
                    typename = "单办签证";
                    break;
                case "recommend":
                    typename = "当季推荐";
                    break;
                case "search":
                    typename = "线路搜索";
                    viewflag = "search";
                    break;
                default:
                    break;
            }
            

            if (MyConvert.ConToDec(lineclass) > 0)
            {
                switch (linetype)
                {
                    case "outbound":
                        typename = areaname + "团队游";
                        break;
                    case "inland":
                        typename = areaname + "团队游";
                        break;
                    case "freetour":
                        typename = areaname + "自由行";
                        break;
                    case "cruises":
                        typename = areaname + "邮轮";
                        break;
                    case "visa":
                        typename = areaname + "签证";
                        break;
                    default:
                        typename = "团队游";
                        break;
                }
            }

            if (MyConvert.ConToDec(lineid) > 0)
            {
                typename = "线路详情";
            }
            if (null == titlename)
            {
                titlename = typename;
            }
            lineclasslist = WeChatClass.GetWeChatLineClassList(linetype);
            linerecomm = WeChatClass.GetWeChatLineListRecom(linetype);
            lineclasslist_New = WeChatClass.GetWeChatLineClassList_New(linetype);
            //linerecomm_New = WeChatClass.GetWeChatLineListRecom_New(linetype);
            //Response.Write(lineid);
        }

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
                        if (dt.Rows[0]["LineFlag"] == "3")
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

    }
}