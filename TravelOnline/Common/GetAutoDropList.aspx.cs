using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

using System.IO;
using System.Text;

namespace TravelOnline.Common
{
    public partial class GetAutoDropList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {

            }
            else
            {
                Response.Write("");
                Response.End();
            }

            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            string SqlQueryText = "";
            string SerchName = Request.QueryString["SerchName"].ToString();
            string Do = "Y";
            StringBuilder Strings = new StringBuilder();
            Strings.Append("<NewDataSet><MisERP><listid></listid><listname></listname></MisERP></NewDataSet>");
            switch (Request.QueryString["action"])
            {
                case "SummaryType":
                    SqlQueryText = "select id as listid,dataname as listname,'' as e1,'' as e2,'' as e3 from InitData where ftype='Summary' order by sort,id";
                    if (Request.QueryString["flag"] == "1") SqlQueryText = "select id as listid,dataname as listname,'' as e1,'' as e2,'' as e3 from InitData where ftype='Traffic' order by sort,id";
                    break;
                case "JournalDestination":
                    SqlQueryText = string.Format("select top 10 id as listid,DestinationName as listname,'' as e1,'' as e2,'' as e3 from OL_Destination where DestinationName like '%{0}%' or SortPinYin like '%{0}%' or PinYin like '%{0}%'", SerchName);
                    break;
                case "DestinationView":
                    SqlQueryText = string.Format("select top 10 id as listid,viewname as listname,'' as e1,'' as e2,'' as e3 from OL_View where desid in (0{1}0) and (viewname like '%{0}%' or SortPinYin like '%{0}%' or PinYin like '%{0}%')", SerchName, Request.QueryString["desid"]);
                    break;
                case "InitData":
                    SqlQueryText = string.Format("select id as listid,dataname as listname,'' as e1,'' as e2,'' as e3 from InitData where ftype='{0}' order by sort", SerchName);
                    break;
                case "AllInitData":
                    Do = "N";
                    string path = string.Format(@"{0}XML\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, "IntData");
                    if (System.IO.File.Exists(path) == true)
                    {
                        XmlDocument XmlDoc = new XmlDocument();
                        XmlDoc.Load(path);
                        XmlNode x = XmlDoc.SelectSingleNode("//InitData");
                        if (x != null)
                        {
                            Strings.Clear();
                            Strings.Append("<NewDataSet>");
                            StringBuilder PicString = new StringBuilder();
                            XmlNodeList elemList = XmlDoc.GetElementsByTagName("Data");
                            for (int i = 0; i < elemList.Count; i++)
                            {
                                Strings.Append(string.Format("<MisERP><listid>{0}</listid><listname>{1}</listname><e1 /><e2 /><e3 /></MisERP>", elemList[i].Attributes["id"].Value, elemList[i].Attributes["name"].Value));
                            }
                            Strings.Append("</NewDataSet>");
                        }
                    }
                    
                    break;
                case "MeetngInitData":
                    SqlQueryText = string.Format("select id as listid,dataname as listname,'' as e1,'' as e2,'' as e3 from MeetingInit where meetingid='{1}' and ftype='{0}'", SerchName, Request.QueryString["meetingid"]);
                    break;
                case "CruisesRoom":
                    SqlQueryText = "select id as listid,roomname as listname,' - ' + typename as e1,'' as e2,'' as e3 from CR_ShipRoom where shipid='" + SerchName + "' order by typename";
                    break;
                case "CruisesShip":
                    SqlQueryText = "select id as listid,cname as listname,' - ' + seriesname as e1,'' as e2,'' as e3 from CR_Ship  order by comid,series";
                    break;
                case "CruisesVisit":
                    SqlQueryText = "select days as listid,visit as listname,' - 第' + CONVERT(VARCHAR,days) + '天' as e1,'' as e2,'' as e3 from CR_Route where visit <> '' and lineid='" + SerchName + "'";
                    break;
                case "CompanyInfo":
                    SqlQueryText = "select top 50 id as listid,companyname as listname,'' as e1,'' as e2,'' as e3 from Company where companyname like '%" + SerchName + "%' or sortpy like '%" + SerchName + "%'";
                    break;
                case "DeptInfo":
                    SqlQueryText = "select top 50 id as listid,deptname as listname,'' as e1,'' as e2,'' as e3 from DeptInfo where companyid = (select uid from company where  id= '" + SerchName + "')";
                    break;
                case "ShowCruisesRoomNo":
                    SqlQueryText = "select top 50 id as listid,RoomNo as listname,' &nbsp;&nbsp;' + CONVERT(VARCHAR,berth) + '人间' as e1,' ' + roomcode as e2,' 已住' + CONVERT(VARCHAR,Nums) as e3 from CR_RoomNo where lineid=" + SerchName + " and roomcode='" + Request.QueryString["roomcode"] + "' ";//and roomcode='" + Request.QueryString["roomcode"] + "' 
                    if (Request.QueryString["floor"] != "")
                    {
                        SqlQueryText += " and RoomNo like '" + Request.QueryString["floor"] + "%' and berth>=" + Request.QueryString["berth"] + "";
                        if (Request.QueryString["Merge"] == "1")
                        {
                            SqlQueryText += " and Flag='1' and Nums>0 and Nums<>berth and Mergeid=0";
                        }
                        else
                        {
                            SqlQueryText += " and Flag='0'";
                        }  
                    }
                    else
                    {
                        SqlQueryText += "  and Flag='0' and roomcode='" + Request.QueryString["roomcode"] + "' and berth=" + Request.QueryString["berth"] + "";
                    }
                    break;
                case "ShowCruisesBusNo":
                    SqlQueryText = "select top 20 id as listid,BusNo as listname,' &nbsp;&nbsp;' + CONVERT(VARCHAR,Berth) + '座 余:' + CONVERT(VARCHAR,Berth-Nums) as e1,'' as e2,'' as e3 from View_CR_BusNo where Berth>Nums and lineid=" + SerchName + " and Visitid='" + Request.QueryString["visitid"] + "'";
                    break;
                case "ShowCruisesPlanNo":
                    SqlQueryText = "select id as listid,PlanNo as listname,' &nbsp;&nbsp;' + CONVERT(VARCHAR,Berth) + '人 余:' + CONVERT(VARCHAR,Berth-Nums) as e1,'' as e2,'' as e3 from View_CR_PlanNo where Berth>Nums and lineid='" + SerchName + "'";
                    break;
                case "ShowCruisesDinnerNo":
                    string times, sql;
                    int berth = 0;
                    sql = "";
                    berth = MyConvert.ConToInt(Request.QueryString["nums"]);
                    if (berth <= 6)
                    {
                        sql += " and Berth<=6";
                    }
                    if (berth > 6)
                    {
                        sql += " and Berth>6";
                    }
                    times = Request.QueryString["Claim"];
                    if (times != "All")
                    {
                        sql += " and DinnerTime='" + times + "'";
                    }

                    SqlQueryText = "select top 50 id as listid,TabelNo as listname,' &nbsp;&nbsp;' + DinnerTime as e1,' &nbsp;&nbsp;' + CONVERT(VARCHAR,Berth) + '座 余:' + CONVERT(VARCHAR,Berth-Nums) as e2,'' as e3 from View_CR_DinnerNo where Berth>Nums and lineid=" + SerchName + sql + " order by Berth desc";
                    break;
                case "CruisesLineVisit":
                    SqlQueryText = "select id as listid,visitname as listname,' - 第' + CONVERT(VARCHAR,days) + '天' as e1,'' as e2,'' as e3 from CR_Visit where lineid='" + SerchName + "' order by days,id";
                    break;
                default:
                    Response.Write("<NewDataSet><listid>0</listid><listname>没有查询到任何数据</listname></NewDataSet>");
                    Response.End();
                    break;
            }

            if (Do == "Y")
            { 
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                XmlTextWriter xml = new XmlTextWriter(Response.OutputStream, Response.ContentEncoding);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DS.WriteXml(xml);
                    //将缓冲区的内容写进xml文件
                    xml.Flush();
                    xml.Close();
                }
                else
                {
                    Response.Write("<NewDataSet><MisERP><listid></listid><listname></listname></MisERP></NewDataSet>");
                }
            }
            else
            {
                Response.Write(Strings.ToString());
            }
            
            Response.ContentType = "text/xml";
            Response.End();

        }
    }
}