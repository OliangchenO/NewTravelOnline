using System;
using System.Data;
using System.Configuration;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Aspose.Cells;
using System.IO;
using System.Text;
using Sunrise.Spell;

namespace TravelOnline.CruisesOrder
{
    public partial class ExcelOutPut : System.Web.UI.Page
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
                case "JapanCar"://日本车号表
                    JapanCar();
                    break;
                case "AllCar"://车号表
                    AllCar();
                    break;
                case "AllChuJing"://出境名单表
                    AllChuJing();
                    break;
                case "AllPlan"://所有分团客人信息
                    AllPlan();
                    break;
                case "CruisesPlanGuest"://邮轮分团客人信息
                    CruisesPlanGuest();
                    break;
                case "CruisesCarGuest"://邮轮分车客人信息
                    CruisesCarGuest();
                    break;
                case "CharterManifest"://1皇家总表、2歌诗达总表
                    if (Request.QueryString["report"] == "1") CharterManifest();
                    if (Request.QueryString["report"] == "2") AtlanticRoomList();
                    break;
                case "Dinning"://餐桌表
                    Dinning();
                    break;
                case "AllCharterManifest":
                    //CharterManifest();
                    if (Request.QueryString["report"] == "1") CharterManifest();
                    if (Request.QueryString["report"] == "2") AtlanticRoomList();
                    break;
                case "ManifestUploadExcel":
                    ManifestUploadExcel();//量子号表格
                    break;
                case "AllDinning":
                    Dinning();
                    break;
                case "KingSize":
                    KingSize();
                    break;
                case "DestinationOutPut":
                    DestinationOutPut();
                    break;
                default:
                    Response.Write("没有登陆，不能操作");
                    Response.End();
                    break;
            }
        }

        protected void ManifestUploadExcel()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' order by RoomNoid,rankno", Request.QueryString["lineid"]);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            DataTable dt = DS1.Tables[0];

            SqlQueryText = string.Format("SELECT * from CR_RoomNo where Lineid={0} order by RoomNo", Request.QueryString["lineid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\ManifestUploadExcel_v3.0.3.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                //Put a string value into the cell using its name 
                //cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                string OrderId = "", CombingId = "";

                int ii = 0;
                string OCCUPANCY = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    OrderId = "";
                    CombingId = "";

                    //cells[2 + i, 0].PutValue(OCCUPANCY);
                    cells[2 + i, 1].PutValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                    cells[2 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());

                    DataRow[] drs = dt.Select("RoomNoid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    ii = 0;
                    if (drs.Count() > 0)
                    {
                        switch (drs.Count())
                        {
                            case 0:
                                OCCUPANCY = "";
                                break;
                            case 1:
                                OCCUPANCY = "S";
                                break;
                            case 2:
                                OCCUPANCY = "D";
                                break;
                            case 3:
                                OCCUPANCY = "T";
                                break;
                            case 4:
                                OCCUPANCY = "Q";
                                break;
                            default:
                                OCCUPANCY = "Q";
                                break;
                        }
                        cells[2 + i, 0].PutValue(OCCUPANCY);

                        foreach (DataRow dr in drs)
                        {
                            if (dr["AutoId"].ToString().Length > 2) OrderId = dr["AutoId"].ToString();
                            if (dr["combineid"].ToString().Length > 2) CombingId = dr["combineid"].ToString();

                            try
                            {
                                cells[2 + i, ii + 3].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[0]);
                                cells[2 + i, ii + 4].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[1]);
                            }
                            catch
                            {
                                cells[2 + i, ii + 3].PutValue(dr["GuestEnName"].ToString());
                            }
                            cells[2 + i, ii + 5].PutValue(dr["DinnerTime"].ToString());
                            cells[2 + i, ii + 6].PutValue("C/O");
                            cells[2 + i, ii + 7].PutValue("CHN");

                            //20140326调整表格顺序
                            cells[2 + i, ii + 8].PutValue(string.Format("{0:yyyy}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:dd}", dr["BirthDay"]));
                            cells[2 + i, ii + 9].PutValue(dr["age"].ToString());
                            //cells[2 + i, ii + 10].PutValue(dr["DinnerClaim"].ToString());
                            cells[2 + i, ii + 11].PutValue(dr["Sex"].ToString());

                            //cells[2 + i, ii + 8].PutValue(dr["age"].ToString());
                            //if (dr["DinnerTime"].ToString().Length > 2)
                            //{
                            //    cells[2 + i, ii + 9].PutValue(dr["DinnerTime"].ToString());
                            //}
                            //else
                            //{
                            //    cells[2 + i, ii + 9].PutValue(dr["DinnerClaim"].ToString());
                            //}

                            //cells[2 + i, ii + 9].PutValue(dr["DinnerTime"].ToString()); DinnerClaim
                            //cells[2 + i, ii + 9].PutValue(dr["DinnerClaim"].ToString());

                            //cells[2 + i, ii + 10].PutValue(dr["Sex"].ToString());
                            //cells[2 + i, ii + 11].PutValue(string.Format("{0:dd}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:yyyy}", dr["BirthDay"]));
                            //cells[2 + i, ii + 11].PutValue(string.Format("{0:dd/MM/yyyy}", dr["BirthDay"]));
                            //cells[2 + ii, 10].PutValue(string.Format("{0:dd}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:yyyy}", dr["BirthDay"]));

                            cells[2 + i, ii + 12].PutValue(dr["IdNumber"].ToString());
                            //cells[2 + i, ii + 13].PutValue(string.Format("{0:yyyy-MM-dd}", dr["PassBgn"]));
                            //cells[2 + i, ii + 13].PutValue(string.Format("{0:dd/MM/yyyy}", dr["PassEnd"]));
                            cells[2 + i, ii + 13].PutValue(string.Format("{0:dd}", dr["PassEnd"]) + "/" + string.Format("{0:MM}", dr["PassEnd"]) + "/" + string.Format("{0:yyyy}", dr["PassEnd"]));
                            cells[2 + i, ii + 14].PutValue(dr["Mobile"].ToString());
                            ii = ii + 12;
                        }

                        cells[2 + i, 51].PutValue(OrderId);
                        if (CombingId != OrderId) cells[2 + i, 52].PutValue(CombingId);
                    }
                }

                workbook.Save(HttpContext.Current.Response, "ManifestUploadExcel_v3.0.3.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }


        protected void JapanCar()
        {
            string SqlQueryText;
            string BusNo = "", ViewName = "";
            SqlQueryText = string.Format("SELECT * from View_CR_BusNo where Lineid='{0}' and Nums>0 and id in ({1})", Request.QueryString["lineid"], Cid);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                if (DS1.Tables[0].Rows.Count > 100)
                {
                    Response.Write("不能导出超过100个团");
                    Response.End();
                }

                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\JapanCar.xls", AppDomain.CurrentDomain.BaseDirectory));

                for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                {
                    BusNo = DS1.Tables[0].Rows[ii]["BusNo"].ToString();
                    ViewName = DS1.Tables[0].Rows[ii]["visitname"].ToString();

                    SqlQueryText = "SELECT *,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
                    SqlQueryText = string.Format("{0} from View_GuestRoomInfo where id in (select guestid from CR_VisitList where Busid='{1}' and flag='0') order by IsLeader desc,autoid", SqlQueryText, DS1.Tables[0].Rows[ii]["id"].ToString());
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Worksheet worksheet = workbook.Worksheets[ii];
                        worksheet.Name = BusNo + "车 " + ViewName;
                        Cells cells = worksheet.Cells;
                        cells["B1"].PutValue("实际来日团队-" + BusNo + "团");

                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            cells[2 + i, 2].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                            cells[2 + i, 3].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                            cells[2 + i, 4].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                            cells[2 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                            cells[2 + i, 6].PutValue(DS.Tables[0].Rows[i]["Home"].ToString());
                            cells[2 + i, 7].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            if (DS.Tables[0].Rows[i]["IsLeader"].ToString() == "1")
                            {
                                cells[2 + i, 8].PutValue("领队");
                            }
                            else
                            {
                                cells[2 + i, 8].PutValue(DS.Tables[0].Rows[i]["Vocation"].ToString());
                            }
                            
                        }


                    }

                }
                if (DS1.Tables[0].Rows.Count < 100)
                {
                    for (int i = 100; i >= DS1.Tables[0].Rows.Count; i--)
                    {
                        workbook.Worksheets.RemoveAt(i);
                    }
                }
                workbook.Save(HttpContext.Current.Response, "JapanCarNo.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();

            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }


        protected void AllCar()
        {
            string SqlQueryText;
            string BusNo = "", ViewName = "", ViewTitle = "";
            SqlQueryText = string.Format("SELECT * from View_CR_BusNo where Lineid='{0}' and Nums>0 and id in ({1})", Request.QueryString["lineid"], Cid);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                if (DS1.Tables[0].Rows.Count > 100)
                {
                    Response.Write("不能导出超过100个团");
                    Response.End();
                }

                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\AllGroupCheckin.xls", AppDomain.CurrentDomain.BaseDirectory));
                        
                for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                {
                    BusNo = DS1.Tables[0].Rows[ii]["BusNo"].ToString();
                    ViewName = DS1.Tables[0].Rows[ii]["visitname"].ToString();
                    ViewTitle = DS1.Tables[0].Rows[ii]["vtitle"].ToString();

                    SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
                    SqlQueryText = string.Format("{0} from View_GuestRoomInfo where IsLeader='0' and id in (select guestid from CR_VisitList where Busid='{1}' and flag='0') order by autoid", SqlQueryText, DS1.Tables[0].Rows[ii]["id"].ToString());
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        Worksheet worksheet = workbook.Worksheets[ii];
                        worksheet.Name = BusNo + "车 " + ViewTitle + " " + ViewName + " " + (ii+1).ToString();
                        Cells cells = worksheet.Cells;
                        cells["B1"].PutValue(BusNo + " 车游客名单表");
                        cells["B2"].PutValue(ViewName);

                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                            cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                            cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                            cells[4 + i, 6].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                            cells[4 + i, 7].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                            cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
                            cells[4 + i, 9].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
                            cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
                            cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                        }

                        
                    }

                }
                if (DS1.Tables[0].Rows.Count < 100)
                {
                    for (int i = 100; i >= DS1.Tables[0].Rows.Count; i--)
                    {
                        workbook.Worksheets.RemoveAt(i);
                    }
                }
                workbook.Save(HttpContext.Current.Response, "AllCarNo.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
                
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }

            //SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
            //SqlQueryText = string.Format("{0} from View_GuestRoomInfo where IsLeader='0' and id in (select guestid from CR_VisitList where Busid='{1}' and flag='0') order by autoid", SqlQueryText, Cid);
            //DataSet DS = new DataSet();
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
            //    Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\GroupCheckin.xls", AppDomain.CurrentDomain.BaseDirectory));
            //    Worksheet worksheet = workbook.Worksheets[0];
            //    Cells cells = worksheet.Cells;

            //    //Put a string value into the cell using its name  //  + " 领队：" + Leader
            //    cells["B1"].PutValue(BusNo + " 车游客名单表");
            //    cells["B2"].PutValue(ViewName);

            //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //    {
            //        cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
            //        cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
            //        cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
            //        cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
            //        cells[4 + i, 6].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
            //        cells[4 + i, 7].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
            //        cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
            //        cells[4 + i, 9].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
            //        cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
            //        cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
            //    }

            //    workbook.Save(HttpContext.Current.Response, "CarNo" + BusNo + " Group Manifest.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
            //    HttpContext.Current.Response.End();
            //}
            //else
            //{
            //    Response.Write("没有任何数据可导出");
            //    Response.End();
            //}
        }


        protected void AllChuJing()
        {
            string SqlQueryText;
            //string shipname="", plandate="";
            string titlename = "邮轮名称： 出境日期： 组团旅行社：上海中国青年旅行社 领队：";
            SqlQueryText = string.Format("SELECT id,PlanDate,(select cname from CR_Ship where id=OL_Line.Shipid) as shipname FROM OL_Line where MisLineId='{0}'", Request.QueryString["lineid"]);
            DataSet DS2 = new DataSet();
            DS2.Clear();
            DS2 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                //shipname = DS2.Tables[0].Rows[0]["shipname"].ToString();
                //plandate = string.Format("{0:yyyy年MM月dd日}", DS2.Tables[0].Rows[0]["PlanDate"]);
                titlename = "邮轮名称：" + DS2.Tables[0].Rows[0]["shipname"].ToString() + " 出境日期：" + string.Format("{0:yyyy年MM月dd日}", DS2.Tables[0].Rows[0]["PlanDate"]) + " 组团旅行社：上海中国青年旅行社 领队：";
            }
            SqlQueryText = string.Format("SELECT id FROM View_CR_PlanNo where Lineid='{0}' and Nums>0 and id in ({1})", Request.QueryString["lineid"], Cid);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                if (DS1.Tables[0].Rows.Count > 100)
                {
                    Response.Write("不能导出超过100个团");
                    Response.End();
                }
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\chujingnew.xls", AppDomain.CurrentDomain.BaseDirectory));
                //Worksheet worksheet_bak = workbook.Worksheets[0];

                for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                {
                    SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select RoomName from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomName,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
                    SqlQueryText = string.Format("{0} from View_GuestRoomInfo where PlanAllotid='{1}' order by ranks", SqlQueryText, DS1.Tables[0].Rows[ii]["id"].ToString());
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                        //workbook.Worksheets.AddCopy(0);
                        Worksheet worksheet = workbook.Worksheets[ii];
                        worksheet.Name = DS.Tables[0].Rows[0]["PlanNo"].ToString() + "团";
                        Cells cells = worksheet.Cells;

                        //Put a string value into the cell using its name 
                        cells["A1"].PutValue("邮轮旅游团团体出境名单样表（边检专用） " + DS.Tables[0].Rows[0]["PlanNo"].ToString() + "团");
                        int orders = 0, rooms = 0;
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            //cells[3 + i, 0].PutValue(i+1);
                            if (i > 0)
                            {
                                if (DS.Tables[0].Rows[i]["OrderId"].ToString() == DS.Tables[0].Rows[i - 1]["OrderId"].ToString())
                                {
                                    orders++;
                                }
                                else
                                {
                                    if (orders > 0) cells.Merge(3 + i - orders-1, 1, orders + 1, 1);
                                    orders = 0;
                                }

                                if (DS.Tables[0].Rows[i]["RoomNo"].ToString() == DS.Tables[0].Rows[i - 1]["RoomNo"].ToString())
                                {
                                    rooms++;
                                }
                                else
                                {
                                    if (rooms > 0) cells.Merge(3 + i - rooms-1, 2, rooms + 1, 1);
                                    rooms = 0;
                                }
                            }

                            if (i == DS.Tables[0].Rows.Count - 1)
                            {
                                if (orders > 0) cells.Merge(3 + i - orders, 1, orders + 1, 1);
                                if (rooms > 0) cells.Merge(3 + i - rooms, 2, rooms + 1, 1);
                            }

                            //cells.Merge(0, 0, 1, 1);//合并单元格
                            cells[3 + i, 1].PutValue(DS.Tables[0].Rows[i]["OrderName"].ToString());
                            cells[3 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[3 + i, 3].PutValue(DS.Tables[0].Rows[i]["RoomName"].ToString());
                            cells[3 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                            if (DS.Tables[0].Rows[i]["Sex"].ToString() == "F")
                            {
                                cells[3 + i, 5].PutValue("女");
                            }
                            else
                            {
                                cells[3 + i, 5].PutValue("男");
                            }
                            cells[3 + i, 6].PutValue(string.Format("{0:yyyyMMdd}", DS.Tables[0].Rows[i]["BirthDay"]));
                            cells[3 + i, 7].PutValue(DS.Tables[0].Rows[i]["Home"].ToString());
                            cells[3 + i, 8].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                            cells[3 + i, 9].PutValue(DS.Tables[0].Rows[i]["Sign"].ToString());
                            cells[3 + i, 10].PutValue(DS.Tables[0].Rows[i]["Address"].ToString());
                            cells[3 + i, 11].PutValue(DS.Tables[0].Rows[i]["Company"].ToString());
                            cells[3 + i, 12].PutValue(string.Format("{0:yyyyMMdd}", DS.Tables[0].Rows[i]["PassBgn"]));
                            //cells[3 + i, 14].PutValue(string.Format("{0:yyyyMMdd}", DS.Tables[0].Rows[i]["PassEnd"]));
                            cells[3 + i, 13].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());

                            cells[3 + i, 15].PutValue(DS.Tables[0].Rows[i]["TongXing"].ToString());
                            cells[3 + i, 16].PutValue(DS.Tables[0].Rows[i]["firstcj"].ToString());
                            cells[3 + i, 17].PutValue(DS.Tables[0].Rows[i]["cjdate"].ToString());
                            cells[3 + i, 18].PutValue(DS.Tables[0].Rows[i]["cjmdd"].ToString());
                            cells[3 + i, 19].PutValue(DS.Tables[0].Rows[i]["cjsy"].ToString());



                            if (DS.Tables[0].Rows[i]["IsLeader"].ToString() == "1")
                            {
                                cells["A2"].PutValue(titlename + DS.Tables[0].Rows[i]["GuestName"].ToString() + " 联系方式：" + DS.Tables[0].Rows[i]["Mobile"].ToString());
                            }
                            else
                            {
                                cells["A2"].PutValue(titlename);
                            }
                            //cells[3 + i, 0].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            //cells[3 + i, 1].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                            //cells[3 + i, 2].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                            //cells[3 + i, 3].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                            //cells[3 + i, 4].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                            //cells[3 + i, 5].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                            //cells[3 + i, 6].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
                            //cells[3 + i, 7].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
                            //cells[3 + i, 8].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
                            //cells[3 + i, 9].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                        }
                    }
                }
                if (DS1.Tables[0].Rows.Count < 100)
                {
                    for (int i = 100; i >= DS1.Tables[0].Rows.Count; i--)
                    {
                        workbook.Worksheets.RemoveAt(i);
                    }
                }
                workbook.Save(HttpContext.Current.Response, "chujing.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }

        }


        protected void AllPlan()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("SELECT id FROM View_CR_PlanNo where Lineid='{0}' and Nums>0 and id in ({1})", Request.QueryString["lineid"], Cid);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                if (DS1.Tables[0].Rows.Count > 100)
                {
                    Response.Write("不能导出超过100个团");
                    Response.End();
                }
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\AllGroupCheckin.xls", AppDomain.CurrentDomain.BaseDirectory));
                //Worksheet worksheet_bak = workbook.Worksheets[0];
                
                for (int ii = 0; ii < DS1.Tables[0].Rows.Count; ii++)
                {
                    SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
                    SqlQueryText = string.Format("{0} from View_GuestRoomInfo where PlanAllotid='{1}' ", SqlQueryText, DS1.Tables[0].Rows[ii]["id"].ToString());
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                        //workbook.Worksheets.AddCopy(0);
                        Worksheet worksheet = workbook.Worksheets[ii];
                        worksheet.Name = DS.Tables[0].Rows[0]["PlanNo"].ToString() + "团";
                        Cells cells = worksheet.Cells;

                        //Put a string value into the cell using its name 
                        cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                            cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                            cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                            cells[4 + i, 6].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                            cells[4 + i, 7].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                            cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
                            cells[4 + i, 9].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
                            cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
                            cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                        }
                    }
                }
                if (DS1.Tables[0].Rows.Count < 100)
                {
                    for (int i = 100; i >= DS1.Tables[0].Rows.Count; i--)
                    {
                        workbook.Worksheets.RemoveAt(i);
                    }
                    //for (int i = 100; i < 60; i--)
                    //{
                    //    workbook.Worksheets.RemoveAt(i);
                    //}
                }
                workbook.Save(HttpContext.Current.Response, "AllGroupCheckIn.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }

            //SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
            //SqlQueryText = string.Format("{0} from View_GuestRoomInfo where PlanAllotid='{1}' ", SqlQueryText, Cid);
            //DataSet DS = new DataSet();
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
            //    Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\GroupCheckin.xls", AppDomain.CurrentDomain.BaseDirectory));
            //    Worksheet worksheet = workbook.Worksheets[0];
            //    Cells cells = worksheet.Cells;

            //    //Put a string value into the cell using its name 
            //    cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
            //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //    {
            //        cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
            //        cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
            //        cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
            //        cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
            //        cells[4 + i, 6].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
            //        cells[4 + i, 7].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
            //        cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
            //        cells[4 + i, 9].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
            //        cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
            //        cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
            //    }

            //    workbook.Save(HttpContext.Current.Response, "No" + DS.Tables[0].Rows[0]["PlanNo"].ToString() + " Group Manifest Template check in.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
            //    HttpContext.Current.Response.End();
            //}
            //else
            //{
            //    Response.Write("没有任何数据可导出");
            //    Response.End();
            //}
        }

        protected void DestinationOutPut()
        {
            string SqlQueryText = "SELECT id,DestinationName,ClassLevel from OL_Destination ORDER BY ClassList";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            SqlQueryText = "SELECT id,desid,viewname from OL_View ORDER BY desid";
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            int ClassLevel=0;
            int ii = 0;
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataTable dt = DS1.Tables[0];
                
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\BlankExcel.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;
                cells["A1"].PutValue("目的地及景点层级");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    ii++;
                    ClassLevel = MyConvert.ConToInt(DS.Tables[0].Rows[i]["ClassLevel"].ToString());
                    DataRow[] drs = dt.Select("desid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    cells[ii, ClassLevel - 1].PutValue(DS.Tables[0].Rows[i]["DestinationName"].ToString());
                    if (drs.Count() > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            ii++;
                            cells[ii, ClassLevel].PutValue(dr["viewname"].ToString());
                        }
                    }
                }

                workbook.Save(HttpContext.Current.Response, "DestinationOutPut.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        protected void KingSize()
        {
            string SqlQueryText = string.Format("SELECT RoomNo,Nums,(select top 1 bedtype from cr_roomlist where id=CR_RoomNo.listid) as bedtype FROM CR_RoomNo where Lineid='{0}' and id in (select roomnoid from cr_roomlist where bedtype='2' and orderflag='0' and Lineid='{0}' group by roomnoid)", Request.QueryString["lineid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\BlankExcel.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;
                cells["A1"].PutValue("大床房房号列表");
                cells["A2"].PutValue("序号");
                cells["B2"].PutValue("房号");
                cells["C2"].PutValue("已入住");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    cells[2 + i, 0].PutValue(i+1);
                    cells[2 + i, 1].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                    cells[2 + i, 2].PutValue(DS.Tables[0].Rows[i]["Nums"].ToString());
                }

                workbook.Save(HttpContext.Current.Response, "KingSize.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        protected void CruisesCarGuest()
        {
            string SqlQueryText;
            string BusNo="", ViewName="";
            SqlQueryText = string.Format("SELECT * from CR_BusNo where id ='{0}'", Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                BusNo = DS.Tables[0].Rows[0]["BusNo"].ToString();
                ViewName = DS.Tables[0].Rows[0]["visitname"].ToString();
            }
            
            SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
            SqlQueryText = string.Format("{0} from View_GuestRoomInfo where IsLeader='0' and id in (select guestid from CR_VisitList where Busid='{1}' and flag='0') order by autoid", SqlQueryText, Cid);
            //DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\GroupCheckinSpecial.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                //Put a string value into the cell using its name  //  + " 领队：" + Leader
                cells["B1"].PutValue(BusNo + " 车游客名单表");
                cells["B2"].PutValue(ViewName);

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                    cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                    cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                    if (DS.Tables[0].Rows[i]["IdNumber"].ToString().Contains("/"))
                    {
                        string[] idNumbers = DS.Tables[0].Rows[i]["IdNumber"].ToString().Split('/');
                        cells[4 + i, 5].PutValue(idNumbers[0]);
                        cells[4 + i, 6].PutValue(idNumbers[1]);
                    }
                    else
                    {
                        cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                    }
                    cells[4 + i, 7].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                    cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                    cells[4 + i, 9].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
                    cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
                    cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
                    cells[4 + i, 12].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                }

                workbook.Save(HttpContext.Current.Response, "CarNo" + BusNo + " Group Manifest.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        protected void Dinning()
        {
            string SqlQueryText;
            SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select Berth from CR_DinnerNo where id=View_GuestRoomInfo.DinnerId) as Berth";
            if (Action == "AllDinning")
            {
                SqlQueryText = string.Format("{0} from View_GuestRoomInfo where LineID='{1}' order by TabelNo,DinnerTime", SqlQueryText, Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("{0} from View_GuestRoomInfo where PlanAllotid in ({1}) order by TabelNo", SqlQueryText, Cid);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\DinningSheet.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;
                //Put a string value into the cell using its name 
                //cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    cells[2 + i, 1].PutValue(DS.Tables[0].Rows[i]["TabelNo"].ToString());
                    cells[2 + i, 2].PutValue(DS.Tables[0].Rows[i]["Berth"].ToString());
                    cells[2 + i, 3].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                    cells[2 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                    cells[2 + i, 5].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                    cells[2 + i, 6].PutValue(DS.Tables[0].Rows[i]["DinnerTime"].ToString());
                }

                workbook.Save(HttpContext.Current.Response, "Dinning Assignment Spreadsheet.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        protected void CruisesPlanGuest()
        {
            string SqlQueryText;
            SqlQueryText = "SELECT *,(select BookingNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as BookingNo,(select RoomNo from CR_RoomNo where id=View_GuestRoomInfo.RoomNoid) as RoomNo,(select top 1 BusNo from CR_VisitList where flag='0' and guestid=View_GuestRoomInfo.id) as BusNo";
            SqlQueryText = string.Format("{0} from View_GuestRoomInfo where PlanAllotid='{1}' ", SqlQueryText, Cid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\GroupCheckinSpecial.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                //Put a string value into the cell using its name 
                cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    cells[4 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                    cells[4 + i, 3].PutValue(DS.Tables[0].Rows[i]["GuestEnName"].ToString());
                    cells[4 + i, 4].PutValue(DS.Tables[0].Rows[i]["GuestName"].ToString());
                    if (DS.Tables[0].Rows[i]["IdNumber"].ToString().Contains("/"))
                    {
                        string[] idNumbers = DS.Tables[0].Rows[i]["IdNumber"].ToString().Split('/');
                        cells[4 + i, 5].PutValue(idNumbers[0]);
                        cells[4 + i, 6].PutValue(idNumbers[1]);
                    }
                    else
                    {
                        cells[4 + i, 5].PutValue(DS.Tables[0].Rows[i]["IdNumber"].ToString());
                    }
                    cells[4 + i, 7].PutValue(DS.Tables[0].Rows[i]["Sex"].ToString());
                    cells[4 + i, 8].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" +string.Format("{0:MM}", DS.Tables[0].Rows[i]["BirthDay"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["BirthDay"]));
                    cells[4 + i, 9].PutValue(string.Format("{0:dd}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:MM}", DS.Tables[0].Rows[i]["PassEnd"]) + "/" + string.Format("{0:yyyy}", DS.Tables[0].Rows[i]["PassEnd"]));
                    cells[4 + i, 10].PutValue(DS.Tables[0].Rows[i]["Mobile"].ToString());
                    cells[4 + i, 11].PutValue(DS.Tables[0].Rows[i]["AutoId"].ToString());
                    cells[4 + i, 12].PutValue(DS.Tables[0].Rows[i]["BookingNo"].ToString());
                }

                workbook.Save(HttpContext.Current.Response, "No" + DS.Tables[0].Rows[0]["PlanNo"].ToString() + " Group Manifest Template check in.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        protected void CharterManifest()
        {
            string SqlQueryText;
            if (Action == "AllCharterManifest")
            {
                SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' order by RoomNoid,rankno", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) order by RoomNoid,rankno", Request.QueryString["lineid"], Cid);
            }
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            DataTable dt = DS1.Tables[0];

            if (Action == "AllCharterManifest")
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where Lineid={0} order by RoomNo", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where id in (select RoomNoid from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) group by RoomNoid) order by RoomNo", Request.QueryString["lineid"], Cid);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\CharterManifestForm.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[1];
                Cells cells = worksheet.Cells;

                //Put a string value into the cell using its name 
                //cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                string OrderId="",CombingId="";

                int ii = 0;
                string OCCUPANCY = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    OrderId = "";
                    CombingId = "";
                    //switch (DS.Tables[0].Rows[i]["berth"].ToString())
                    //{
                    //    case "1":
                    //        OCCUPANCY = "S";
                    //        break;
                    //    case "2":
                    //        OCCUPANCY = "D";
                    //        break;
                    //    case "3":
                    //        OCCUPANCY = "T";
                    //        break;
                    //    case "4":
                    //        OCCUPANCY = "Q";
                    //        break;
                    //    default:
                    //        OCCUPANCY = "Q";
                    //        break;
                    //}

                    //cells[2 + i, 0].PutValue(OCCUPANCY);
                    cells[2 + i, 1].PutValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                    cells[2 + i, 2].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());

                    DataRow[] drs = dt.Select("RoomNoid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    ii = 0;
                    if (drs.Count() > 0)
                    {
                        switch (drs.Count())
                        {
                            case 0:
                                OCCUPANCY = "";
                                break;
                            case 1:
                                OCCUPANCY = "S";
                                break;
                            case 2:
                                OCCUPANCY = "D";
                                break;
                            case 3:
                                OCCUPANCY = "T";
                                break;
                            case 4:
                                OCCUPANCY = "Q";
                                break;
                            default:
                                OCCUPANCY = "Q";
                                break;
                        }
                        cells[2 + i, 0].PutValue(OCCUPANCY);

                        foreach (DataRow dr in drs)
                        {
                            if (dr["AutoId"].ToString().Length > 2) OrderId = dr["AutoId"].ToString();
                            if (dr["combineid"].ToString().Length > 2) CombingId = dr["combineid"].ToString();
                            
                            try
                            {
                                cells[2 + i, ii + 3].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[0]);
                                cells[2 + i, ii + 4].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[1]);
                            }
                            catch
                            {
                                cells[2 + i, ii + 3].PutValue(dr["GuestEnName"].ToString());
                            }
                            cells[2 + i, ii + 5].PutValue(dr["DinnerTime"].ToString());
                            cells[2 + i, ii + 6].PutValue("C/O");
                            cells[2 + i, ii + 7].PutValue("CHN");

                            //20140326调整表格顺序
                            cells[2 + i, ii + 8].PutValue(string.Format("{0:yyyy}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:dd}", dr["BirthDay"]));
                            cells[2 + i, ii + 9].PutValue(dr["age"].ToString());
                            //cells[2 + i, ii + 10].PutValue(dr["DinnerClaim"].ToString());
                            cells[2 + i, ii + 11].PutValue(dr["Sex"].ToString());

                            //cells[2 + i, ii + 8].PutValue(dr["age"].ToString());
                            //if (dr["DinnerTime"].ToString().Length > 2)
                            //{
                            //    cells[2 + i, ii + 9].PutValue(dr["DinnerTime"].ToString());
                            //}
                            //else
                            //{
                            //    cells[2 + i, ii + 9].PutValue(dr["DinnerClaim"].ToString());
                            //}

                            //cells[2 + i, ii + 9].PutValue(dr["DinnerTime"].ToString()); DinnerClaim
                            //cells[2 + i, ii + 9].PutValue(dr["DinnerClaim"].ToString());

                            //cells[2 + i, ii + 10].PutValue(dr["Sex"].ToString());
                            //cells[2 + i, ii + 11].PutValue(string.Format("{0:dd}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:yyyy}", dr["BirthDay"]));
                            //cells[2 + i, ii + 11].PutValue(string.Format("{0:dd/MM/yyyy}", dr["BirthDay"]));
                            //cells[2 + ii, 10].PutValue(string.Format("{0:dd}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:yyyy}", dr["BirthDay"]));
                            
                            cells[2 + i, ii + 12].PutValue(dr["IdNumber"].ToString());
                            //cells[2 + i, ii + 13].PutValue(string.Format("{0:yyyy-MM-dd}", dr["PassBgn"]));
                            //cells[2 + i, ii + 13].PutValue(string.Format("{0:dd/MM/yyyy}", dr["PassEnd"]));
                            cells[2 + i, ii + 13].PutValue(string.Format("{0:dd}", dr["PassEnd"]) + "/" + string.Format("{0:MM}", dr["PassEnd"]) + "/" + string.Format("{0:yyyy}", dr["PassEnd"]));
                            cells[2 + i, ii + 14].PutValue(dr["Mobile"].ToString());
                            ii = ii + 12;
                        }

                        cells[2 + i, 51].PutValue(OrderId);
                        if (CombingId != OrderId) cells[2 + i, 52].PutValue(CombingId);
                    }
                }

                workbook.Save(HttpContext.Current.Response, "Charter Manifest form.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }


        protected void AtlanticRoomList()
        {
            string SqlQueryText;
            if (Action == "AllCharterManifest")
            {
                SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' order by RoomNoid,rankno", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) order by RoomNoid,rankno", Request.QueryString["lineid"], Cid);
            }
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            DataTable dt = DS1.Tables[0];

            if (Action == "AllCharterManifest")
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where Lineid={0} order by id", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where id in (select RoomNoid from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) group by RoomNoid) order by id", Request.QueryString["lineid"], Cid);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //AutoId = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                Workbook workbook = new Workbook(string.Format(@"{0}OfficeFiles\CruisesOut\AtlanticRoomList.xls", AppDomain.CurrentDomain.BaseDirectory));
                Worksheet worksheet = workbook.Worksheets[0];
                Cells cells = worksheet.Cells;

                int ii = 0, berth = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    berth = 0;
                    berth = Convert.ToInt32(DS.Tables[0].Rows[i]["berth"].ToString());
                    DataRow[] drs = dt.Select("RoomNoid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    if (drs.Count() > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            cells[2 + ii, 0].PutValue(DS.Tables[0].Rows[i]["berth"].ToString());
                            cells[2 + ii, 1].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[2 + ii, 2].PutValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                            try
                            {
                                cells[2 + ii, 3].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[0]);
                                cells[2 + ii, 4].PutValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[1]);
                            }
                            catch
                            {
                                cells[2 + ii, 3].PutValue(dr["GuestEnName"].ToString());
                            }
                            cells[2 + ii, 5].PutValue(dr["Sex"].ToString());
                            cells[2 + ii, 6].PutValue(string.Format("{0:dd}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:yyyy}", dr["BirthDay"]));
                            cells[2 + ii, 7].PutValue(Spell.MakeSpellCode(dr["Home"].ToString(), SpellOptions.EnableUnicodeLetter));
                            cells[2 + ii, 8].PutValue("CN");
                            cells[2 + ii, 9].PutValue(dr["IdNumber"].ToString());
                            cells[2 + ii, 10].PutValue(string.Format("{0:dd}", dr["PassBgn"]) + "/" + string.Format("{0:MM}", dr["PassBgn"]) + "/" + string.Format("{0:yyyy}", dr["PassBgn"]));
                            cells[2 + ii, 11].PutValue(Spell.MakeSpellCode(dr["Sign"].ToString(), SpellOptions.EnableUnicodeLetter));
                            cells[2 + ii, 12].PutValue(string.Format("{0:dd}", dr["PassEnd"]) + "/" + string.Format("{0:MM}", dr["PassEnd"]) + "/" + string.Format("{0:yyyy}", dr["PassEnd"]));
                            cells[2 + ii, 13].PutValue(dr["GuestName"].ToString());
                            cells[2 + ii, 14].PutValue("No.2 HengShan Rd.ShangHai P.R.China");
                            cells[2 + ii, 15].PutValue(dr["Mobile"].ToString());
                            cells[2 + ii, 16].PutValue("CHEN");
                            cells[2 + ii, 17].PutValue("KAN");
                            cells[2 + ii, 18].PutValue("15000338019");
                            cells[2 + ii, 19].PutValue(dr["DinnerTime"].ToString());
                            cells[2 + ii, 20].PutValue(dr["TabelNo"].ToString());
                            ii += 1;
                        }

                        for (int s = 0; s < (berth - drs.Count()); s++)
                        {
                            cells[2 + ii, 0].PutValue(DS.Tables[0].Rows[i]["berth"].ToString());
                            cells[2 + ii, 1].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[2 + ii, 2].PutValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                            cells[2 + ii, 3].PutValue("");
                            ii += 1;
                        }
                    }
                    else
                    {
                        for (int s = 0; s < berth; s++)
                        {
                            cells[2 + ii, 0].PutValue(DS.Tables[0].Rows[i]["berth"].ToString());
                            cells[2 + ii, 1].PutValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            cells[2 + ii, 2].PutValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                            cells[2 + ii, 3].PutValue("");
                            ii += 1;
                        }
                        
                    }
                }

                workbook.Save(HttpContext.Current.Response, "Atlantic Room List.xls", ContentDisposition.Attachment, new XlsSaveOptions(SaveFormat.Excel97To2003));
                HttpContext.Current.Response.End();
            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }
        }

        
    }
}