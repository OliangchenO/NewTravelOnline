using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Sunrise.Spell;
using System.Data;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace TravelOnline.CruisesOrder
{
    public partial class NuoWeiZhenHao : System.Web.UI.Page
    {
        public string Cid, Action, flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            Action = Request.QueryString["action"];
            Cid = Request.QueryString["cid"];
            flag = Request.QueryString["flag"];
            if (flag == "1")
            {
                ManifestUploadExcel();//量子号表格
            }
            else
            {
                TaoFangExcel();//套房量子号表格
            }
            
        }

        protected void TaoFangExcel()
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
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where berth>4 and Lineid={0} order by id", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where berth>4 and id in (select RoomNoid from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) group by RoomNoid) order by id", Request.QueryString["lineid"], Cid);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                FileStream file = new FileStream(string.Format(@"{0}OfficeFiles\CruisesOut\ManifestUploadExcel_v3.0.3.xls", AppDomain.CurrentDomain.BaseDirectory), FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new HSSFWorkbook(file);
                ISheet sheet1 = workbook.GetSheetAt(2);

                //Put a string value into the cell using its name 
                //cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                string OrderId = "", CombingId = "", GuestName = "", GuestName1 = "";

                int ii = 0, t = 0, guest = 0;
                string OCCUPANCY = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {


                    OrderId = "";
                    CombingId = "";

                    IRow row1 = sheet1.CreateRow(i + t + 2);
                    ICell cel0 = row1.CreateCell(0); //入住人数
                    ICell cel1 = row1.CreateCell(1); //船舱类型
                    ICell cel2 = row1.CreateCell(2); //船舱号码

                    cel1.SetCellValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                    cel2.SetCellValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());

                    DataRow[] drs = dt.Select("RoomNoid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    ii = 0;
                    guest = 0;
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
                        cel0.SetCellValue(OCCUPANCY);

                        foreach (DataRow dr in drs)
                        {
                            if (guest == 4 || guest == 8)
                            {
                                t = t + 1;
                                ii = 0;
                                row1 = sheet1.CreateRow(i + t + 2);
                                cel0 = row1.CreateCell(0); //入住人数
                                cel1 = row1.CreateCell(1); //船舱类型
                                cel2 = row1.CreateCell(2); //船舱号码

                                cel0.SetCellValue(OCCUPANCY);
                                cel1.SetCellValue(DS.Tables[0].Rows[i]["roomcode"].ToString());
                                cel2.SetCellValue(DS.Tables[0].Rows[i]["RoomNo"].ToString());
                            }

                            if (dr["AutoId"].ToString().Length > 2) OrderId = dr["AutoId"].ToString();
                            if (dr["combineid"].ToString().Length > 2) CombingId = dr["combineid"].ToString();

                            ICell cel4 = row1.CreateCell(4 + ii); //客人姓的拼音
                            ICell cel5 = row1.CreateCell(5 + ii); //客人名的拼音

                            try
                            {
                                cel4.SetCellValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[0]);
                                cel5.SetCellValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[1]);
                            }
                            catch
                            {
                                cel4.SetCellValue(dr["GuestEnName"].ToString());
                            }

                            GuestName = dr["GuestName"].ToString();
                            if (GuestName.Length < 5)
                            {
                                GuestName1 = GuestName.Substring(1, GuestName.Length - 1);
                                GuestName = GuestName.Substring(0, 1);

                                ICell cel8 = row1.CreateCell(8 + ii); //客人姓的拼音
                                ICell cel9 = row1.CreateCell(9 + ii); //客人名的拼音
                                cel8.SetCellValue(GuestName);
                                cel9.SetCellValue(GuestName1);
                            }
                            else
                            {
                                GuestName = "";
                            }

                            ICell cel13 = row1.CreateCell(13 + ii); //客人的用餐批次
                            cel13.SetCellValue(dr["DinnerTime"].ToString());

                            ICell cel14 = row1.CreateCell(14 + ii); //客人的船票类型(只放C/O)
                            cel14.SetCellValue("C/O");

                            ICell cel15 = row1.CreateCell(15 + ii); //客人的国籍
                            cel15.SetCellValue("CHN");

                            ICell cel16 = row1.CreateCell(16 + ii); //客人的国籍
                            cel16.SetCellValue(string.Format("{0:yyyy}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:dd}", dr["BirthDay"]));


                            ICell cel17 = row1.CreateCell(17 + ii); //客人的性别
                            cel17.SetCellValue(dr["Sex"].ToString());


                            ICell cel19 = row1.CreateCell(19 + ii); //领队
                            if (dr["IsLeader"].ToString() == "1")
                            {
                                cel19.SetCellValue("Y");
                            }
                            else
                            {
                                cel19.SetCellValue("N");
                            }

                            ICell cel20 = row1.CreateCell(20 + ii); //客人的护照号
                            ICell cel21 = row1.CreateCell(21 + ii); //客人的护照有效期
                            ICell cel22 = row1.CreateCell(22 + ii); //紧急联系人号码

                            cel20.SetCellValue(dr["IdNumber"].ToString());
                            if (MyConvert.ConToDateTime(dr["PassEnd"].ToString()) > DateTime.Today) cel21.SetCellValue(string.Format("{0:dd}", dr["PassEnd"]) + "/" + string.Format("{0:MM}", dr["PassEnd"]) + "/" + string.Format("{0:yyyy}", dr["PassEnd"]));
                            cel22.SetCellValue(dr["Mobile"].ToString());

                            ii = ii + 20;
                            guest = guest + 1;
                        }
                        //if (guest >= 4) t = t + 1;
                    }
                }

                string timestring = GetNewName();// string.Format("{0:yyMMddHHmmss}", DateTime.Now);
                string filepath = string.Format(@"{0}OfficeFiles\CruisesOut\ManifestUploadExcel_{1}.xls", AppDomain.CurrentDomain.BaseDirectory, timestring);

                FileStream file1 = new FileStream(filepath, FileMode.Create);
                workbook.Write(file1);
                file.Close();
                file1.Close();

                string fileName = "Manifest_TaoFang_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";

                FileInfo info = new FileInfo(filepath);
                long fileSize = info.Length;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                Response.Charset = "utf-8 ";
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie") > -1)
                {
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                }
                if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
                {
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);


                //指定文件大小   
                HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                HttpContext.Current.Response.WriteFile(filepath, 0, fileSize);
                HttpContext.Current.Response.Flush();

                System.IO.File.Delete(filepath);

            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }

        }


        protected void ManifestUploadExcel()
        {
            //workbook.CreateSheet("Sheet1");
            //workbook.CreateSheet("Sheet2");
            //workbook.CreateSheet("Sheet3");

            //HSSFWorkbook hssfworkbook = new HSSFWorkbook(file); 
            //HSSFSheet sheet1 = hssfworkbook.GetSheet("Sheet1"); 
            //sheet1.GetRow(1).GetCell(1).SetCellValue(200200);
            //sheet1.GetRow(2).GetCell(1).SetCellValue(300);
            //sheet1.GetRow(3).GetCell(1).SetCellValue(500050);

            //ICell cell = sheet1.CreateRow(12).CreateCell(5);
            //cell.SetCellValue(150);
            
            //Force excel to recalculate all the formula while open
            //sheet1.ForceFormulaRecalculation = true;   
            
            
            //FileStream file1 = new FileStream(string.Format(@"{0}OfficeFiles\CruisesOut\ManifestUploadExcel_v3.xls", AppDomain.CurrentDomain.BaseDirectory), FileMode.Create);
            //workbook.Write(file1);
            //file.Close();
            //file1.Close();


            string SqlQueryText;
            //SqlQueryText = string.Format("select *,DATEDIFF(yy, birthday, GETDATE()) AS age from View_GuestRoomInfo where lineid='{0}' order by RoomNoid,rankno", Request.QueryString["lineid"]);
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

            //SqlQueryText = string.Format("SELECT * from CR_RoomNo where berth<5 and Lineid={0} order by RoomNo", Request.QueryString["lineid"]);
            if (Action == "AllCharterManifest")
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where berth<7 and Lineid={0} order by id", Request.QueryString["lineid"]);
            }
            else
            {
                SqlQueryText = string.Format("SELECT * from CR_RoomNo where berth<7 and id in (select RoomNoid from View_GuestRoomInfo where lineid='{0}' and PlanAllotid in ({1}) group by RoomNoid) order by id", Request.QueryString["lineid"], Cid);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                FileStream file = new FileStream(string.Format(@"{0}OfficeFiles\CruisesOut\blank manifest format.xls", AppDomain.CurrentDomain.BaseDirectory), FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new HSSFWorkbook(file);
                ISheet sheet1 = workbook.GetSheetAt(0);
                
                //Put a string value into the cell using its name 
                //cells["B1"].PutValue(DS.Tables[0].Rows[0]["PlanNo"].ToString() + " 团队名单表");
                string OrderId = "", CombingId = "", GuestName = "", GuestName1 = "";

                int rowNum = 1;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    OrderId = "";
                    CombingId = "";
                    IRow row1 = null;
                    string roomcode = DS.Tables[0].Rows[i]["roomcode"].ToString();
                    string RoomNo = DS.Tables[0].Rows[i]["RoomNo"].ToString();
                    
                    DataRow[] drs = dt.Select("RoomNoid=" + DS.Tables[0].Rows[i]["id"].ToString());
                    if (drs.Count() > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            row1 = sheet1.CreateRow(rowNum);
                            ICell cel2 = row1.CreateCell(2); //船舱类型
                            ICell cel3 = row1.CreateCell(3); //船舱号码
                            cel2.SetCellValue(roomcode);
                            cel3.SetCellValue(RoomNo);
                            if (dr["AutoId"].ToString().Length > 2) OrderId = dr["AutoId"].ToString();
                            if (dr["combineid"].ToString().Length > 2) CombingId = dr["combineid"].ToString();

                            ICell cel4 = row1.CreateCell(4); //客人姓的拼音
                            ICell cel5 = row1.CreateCell(5); //客人名的拼音

                            try
                            {
                                cel4.SetCellValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[0]);
                                cel5.SetCellValue(dr["GuestEnName"].ToString().Split("/".ToCharArray())[1]);
                            }
                            catch
                            {
                                cel4.SetCellValue(dr["GuestEnName"].ToString());
                            }

                            GuestName = dr["GuestName"].ToString();
                            if (GuestName.Length < 5)
                            {
                                GuestName1 = GuestName.Substring(1, GuestName.Length - 1);
                                GuestName = GuestName.Substring(0, 1);

                                ICell cel6 = row1.CreateCell(6); //客人姓的拼音
                                ICell cel7 = row1.CreateCell(7); //客人名的拼音
                                cel6.SetCellValue(GuestName);
                                cel7.SetCellValue(GuestName1);
                            }
                            else
                            {
                                GuestName = "";
                            }

                            ICell cel8 = row1.CreateCell(8); //客人的性别
                            cel8.SetCellValue(dr["Sex"].ToString());

                            ICell cel9 = row1.CreateCell(9); //客人的国籍
                            cel9.SetCellValue("CHN");

                            ICell cel10 = row1.CreateCell(10); //客人的国籍
                            cel10.SetCellValue(string.Format("{0:yyyy}", dr["BirthDay"]) + "/" + string.Format("{0:MM}", dr["BirthDay"]) + "/" + string.Format("{0:dd}", dr["BirthDay"]));
                            
                            ICell cel11 = row1.CreateCell(11); //客人的护照号
                            ICell cel12 = row1.CreateCell(12); //客人的护照签发期
                            ICell cel13 = row1.CreateCell(13); //客人的护照有效期
                            ICell cel14 = row1.CreateCell(14); //客人的护照签发地
                            if (dr["IdNumber"].ToString().Contains("/"))
                            {
                                string[] idNumbers = dr["IdNumber"].ToString().Split('/');
                                cel11.SetCellValue(idNumbers[0]);
                            }
                            else
                            {
                                cel11.SetCellValue(dr["IdNumber"].ToString());
                            }
                            if (MyConvert.ConToDateTime(dr["PassBgn"].ToString()) < DateTime.Today) cel12.SetCellValue(string.Format("{0:yyyy}", dr["PassBgn"]) + "/" + string.Format("{0:MM}", dr["PassBgn"]) + "/" + string.Format("{0:dd}", dr["PassBgn"]));
                            if (MyConvert.ConToDateTime(dr["PassEnd"].ToString()) > DateTime.Today) cel13.SetCellValue(string.Format("{0:yyyy}", dr["PassEnd"]) + "/" + string.Format("{0:MM}", dr["PassEnd"]) + "/" + string.Format("{0:dd}", dr["PassEnd"]));
                            cel14.SetCellValue(dr["sign"].ToString());
                            rowNum += 1;
                        }
                    }
                }

                string timestring = GetNewName();// string.Format("{0:yyMMddHHmmss}", DateTime.Now);
                string filepath = string.Format(@"{0}OfficeFiles\CruisesOut\ManifestUploadExcel_{1}.xls", AppDomain.CurrentDomain.BaseDirectory, timestring);

                FileStream file1 = new FileStream(filepath, FileMode.Create);
                workbook.Write(file1);
                file.Close();
                file1.Close();

                string fileName = "ManifestUploadExcel_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xls";

                FileInfo info = new FileInfo(filepath);
                long fileSize = info.Length;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                Response.Charset = "utf-8 ";
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("msie") > -1)
                {
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                }
                if (HttpContext.Current.Request.UserAgent.ToLower().IndexOf("firefox") > -1)
                {
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);


                //指定文件大小   
                HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                HttpContext.Current.Response.WriteFile(filepath, 0, fileSize);
                HttpContext.Current.Response.Flush();

                System.IO.File.Delete(filepath);

            }
            else
            {
                Response.Write("没有任何数据可导出");
                Response.End();
            }

        }

        private string GetNewName()
        {
            Random rd = new Random();
            StringBuilder serial = new StringBuilder();
            serial.Append(DateTime.Now.ToString("yyMMddHHmmssff"));
            serial.Append(DateTime.Now.Millisecond.ToString());
            serial.Append(rd.Next(10000, 99999).ToString());

            return serial.ToString();
        }

    }
}