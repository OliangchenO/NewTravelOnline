using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Saving;
using System.Xml;

namespace TravelOnline.CruisesOrder
{
    public partial class WordOutPut : System.Web.UI.Page
    {
        public string Cid, Action;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
            }
            else
            {
                Response.Write("没有登陆，不能操作");
                Response.End();
            }

            Action = Request.QueryString["action"];
            Cid = Request.QueryString["cid"];

            switch (Action)
            {
                case "SinglePlanVisit"://岸上观光
                    Visit();
                    break;
                case "PlanSelectVisit"://岸上观光
                    Visit();
                    break;
                default:
                    Response.Write("没有登陆，不能操作");
                    Response.End();
                    break;
            }
        }

        protected void Visit()
        {
            string LineName = "", viewsconfirm = "", PlanNo = "多";
            string SqlQueryText;

            if (Action == "SinglePlanVisit")
            {
                SqlQueryText = string.Format("select * from View_VisitReport where guestid in (select id from View_GuestRoomInfo where IsLeader='0' and LineID='{0}' and PlanAllotid='{1}') order by guestid,days", Request.QueryString["lineid"], Cid);
            }
            else
            {
                SqlQueryText = string.Format("select * from View_VisitReport where guestid in (select id from View_GuestRoomInfo where IsLeader='0' and LineID='{0}' and PlanAllotid in ({1})) order by guestid,days", Request.QueryString["lineid"], Cid);
            }
            //Response.Write(SqlQueryText);
            //Response.End();
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            DataTable dt = DS1.Tables[0];

            SqlQueryText = string.Format("select LineName,(select top 1 views from CR_Confirm where lineid=OL_Line.MisLineId) as views from OL_Line where MisLineId='{0}'", Request.QueryString["lineid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                viewsconfirm = DS.Tables[0].Rows[0]["views"].ToString();
            }


            SqlQueryText = "SELECT id,PlanNo,GuestName,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,'' as vtitle,'' as visitname,'' as vmemo,'' as vdate,'' as stay,'' as dinner,'' as bus,'' as vtitle1,'' as visitname1,'' as vmemo1,'' as vdate1,'' as stay1,'' as dinner1,'' as bus1,'' as vtitle2,'' as visitname2,'' as vmemo2,'' as vdate2,'' as stay2,'' as dinner2,'' as bus2";
            if (Action == "SinglePlanVisit")
            {
                SqlQueryText = string.Format("{0} from View_GuestRoomInfo where IsLeader='0' and LineID='{1}' and PlanAllotid='{2}' and visitcount>0 order by PlanAllotid", SqlQueryText, Request.QueryString["lineid"], Cid);
            }
            else
            {
                SqlQueryText = string.Format("{0} from View_GuestRoomInfo where IsLeader='0' and LineID='{1}' and PlanAllotid in ({2}) and visitcount>0 order by PlanAllotid", SqlQueryText, Request.QueryString["lineid"], Cid);
            }
            //Response.Write(SqlQueryText);
            //Response.End();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                PlanNo = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                int ii = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ii = 0;
                    DataRow[] drs = dt.Select("guestid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    if (drs.Count() > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            if (ii == 0)
                            {
                                DS.Tables[0].Rows[i]["vtitle"] = dr["vtitle"].ToString();
                                DS.Tables[0].Rows[i]["visitname"] = dr["visitname"].ToString();
                                DS.Tables[0].Rows[i]["vmemo"] = dr["vmemo"].ToString();
                                DS.Tables[0].Rows[i]["vdate"] = dr["vdate"].ToString();
                                DS.Tables[0].Rows[i]["stay"] = dr["stay"].ToString();
                                DS.Tables[0].Rows[i]["dinner"] = dr["dinner"].ToString();
                                DS.Tables[0].Rows[i]["bus"] = dr["BusNo"].ToString();
                            }
                            if (ii == 1)
                            {
                                DS.Tables[0].Rows[i]["vtitle1"] = dr["vtitle"].ToString();
                                DS.Tables[0].Rows[i]["visitname1"] = dr["visitname"].ToString();
                                DS.Tables[0].Rows[i]["vmemo1"] = dr["vmemo"].ToString();
                                DS.Tables[0].Rows[i]["vdate1"] = dr["vdate"].ToString();
                                DS.Tables[0].Rows[i]["stay1"] = dr["stay"].ToString();
                                DS.Tables[0].Rows[i]["dinner1"] = dr["dinner"].ToString();
                                DS.Tables[0].Rows[i]["bus1"] = dr["BusNo"].ToString();
                            }

                            if (ii == 2)
                            {
                                DS.Tables[0].Rows[i]["vtitle2"] = dr["vtitle"].ToString();
                                DS.Tables[0].Rows[i]["visitname2"] = dr["visitname"].ToString();
                                DS.Tables[0].Rows[i]["vmemo2"] = dr["vmemo"].ToString();
                                DS.Tables[0].Rows[i]["vdate2"] = dr["vdate"].ToString();
                                DS.Tables[0].Rows[i]["stay2"] = dr["stay"].ToString();
                                DS.Tables[0].Rows[i]["dinner2"] = dr["dinner"].ToString();
                                DS.Tables[0].Rows[i]["bus2"] = dr["BusNo"].ToString();
                            }
                            
                            ii += 1;
                        }
                    }
                }

            }
            //Response.End();

            //SELECT days FROM  CR_Visit WHERE  (lineid = 12916) GROUP BY days
            string filename = "";
            SqlQueryText = string.Format("SELECT days FROM  CR_Visit WHERE lineid ={0} GROUP BY days", Request.QueryString["lineid"]);
            DataSet DS2 = new DataSet();
            DS2.Clear();
            DS2 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                filename = "Visit" + DS2.Tables[0].Rows.Count;
            }
            else
            {
                filename = "Visit";
            }

            Document doc = new Document(string.Format(@"{0}OfficeFiles\CruisesOut\{1}.doc", AppDomain.CurrentDomain.BaseDirectory, filename));
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.PageSetup.PaperSize = PaperSize.A4;

            //doc.MailMerge.Execute(new String[] { "合同编号", "合同日期", "游客姓名", "联系电话", "性别", "年龄" }, new Object[] { AutoId, AutoDate, TourstName, Tel, Sex, Age });
            //doc.MailMerge.Execute(new String[] { "线路名称", "天数", "出发日期", "返回日期" }, new Object[] { LineName, LineDays, BeginDate, EndDate });
            doc.MailMerge.Execute(new String[] { "LineName", "viewsconfirm" }, new Object[] { LineName, viewsconfirm });
            doc.MailMerge.Execute(DS.Tables[0]);
            doc.MailMerge.DeleteFields();
            doc.Save(Response, PlanNo + "团岸上观光确认单.pdf", ContentDisposition.Attachment, SaveOptions.CreateSaveOptions(SaveFormat.Pdf));

        }



    }
}