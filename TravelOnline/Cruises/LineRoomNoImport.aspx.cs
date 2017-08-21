using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using System.Diagnostics;
using Excel;
using System.Text;

namespace TravelOnline.Cruises
{
    public partial class LineRoomNoImport : System.Web.UI.Page
    {
        public string lineid, flag, InputResult, RoomName, AllotId, AllotBerth,ImportFlag;
        public string hide1, hide2, hide3;
        public int RepeatCount, ImportCount;
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
                Response.Write("尚未登录");
                Response.End();
            }
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            lineid = Request.QueryString["lineid"];
            flag = Request.QueryString["flag"];
            hide1 = "hide";
            hide2 = "hide";
            hide3 = "hide";
            switch (flag)
            {
                case "RoomNo":
                    hide1 = "";
                    break;
                case "Dinner":
                    hide2 = "";
                    break;
                case "BookingNo":
                    hide3 = "";
                    break;
                default:
                    break;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            StringBuilder Strings = new StringBuilder();

            //导入文件是否存在
            if (!filePath.HasFile)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('请选择文件');", true);
                filePath.Focus();
                return;
            }
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
            string toFilePath = Path.Combine(serverPath, string.Format(@"Upload\{0}\", "Excel"));

            string fileSavePath = toFilePath + DateTime.Now.Ticks.ToString() + filePath.FileName;
            //保存文件
            filePath.SaveAs(fileSavePath);

            ////清空导入时间
            //lbImportTime.Text = "";

            Stopwatch myWatch = Stopwatch.StartNew();

            string excelVersion = ExcelVersion.Excel8;
            //确定导入文件的版本
            if (filePath.FileName.EndsWith(".xlsx", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                excelVersion = ExcelVersion.Excel12;
            }

            //关键语句：调用ExcelFile组件执行导入操作。
            DataTable[] dtExcelDatas = ExcelFile.GetData(fileSavePath, excelVersion, HDRType.Yes, true);

            //执行处理
            if (dtExcelDatas[0].Rows.Count > 0)
            {
                int Row = 0;
                RepeatCount = 0;
                ImportCount = 0;
                string CanImport = "YES";

                //舱房判断
                if (flag == "RoomNo")
                {
                    try
                    {
                        switch (DropDownList1.Text)
                        {
                            case "1":
                                if (dtExcelDatas[0].Columns[0].Caption.ToString() != "舱型" && dtExcelDatas[0].Columns[1].Caption.ToString() != "房号")
                                {
                                    CanImport = "NO";
                                }
                                Row = 0;
                                break;
                            case "2":
                                if (dtExcelDatas[0].Columns[1].Caption.ToString() != "舱号" && dtExcelDatas[0].Columns[2].Caption.ToString() != "目录")
                                {
                                    CanImport = "NO";
                                }
                                Row = 1;
                                break;
                            case "3":
                                if (dtExcelDatas[0].Columns[0].Caption.ToString() != "房型" && dtExcelDatas[0].Columns[1].Caption.ToString() != "房号")
                                {
                                    CanImport = "NO";
                                }
                                Row = 0;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception exp)
                    {
                        Response.Write(exp.Message);
                        CanImport = "NO";
                    }
                }

                //餐桌号判断
                if (flag == "Dinner")
                {
                    try
                    {
                        if (dtExcelDatas[0].Columns[0].Caption.ToString() != "餐桌号" && dtExcelDatas[0].Columns[1].Caption.ToString() != "每桌人数")
                        {
                            CanImport = "NO";
                        }
                        Row = 0;
                    }
                    catch (Exception exp)
                    {
                        Response.Write(exp.Message);
                        CanImport = "NO";
                    }
                }

                //BookingNo判断
                if (flag == "BookingNo")
                {
                    try
                    {
                        if (dtExcelDatas[0].Columns[0].Caption.ToString() != "Booking Id" && dtExcelDatas[0].Columns[1].Caption.ToString() != "Cabin Number")
                        {
                            CanImport = "NO";
                        }
                        Row = 0;
                    }
                    catch (Exception exp)
                    {
                        Response.Write(exp.Message);
                        CanImport = "NO";
                    }
                }
                
                
                if (CanImport == "NO")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('上传的文件格式不对，不能导入！');", true);
                    return;
                }

                //////////////////////////////
                //导入开始
                //////////////////////////////

                string SqlQueryText;
                List<string> Sql = new List<string>();

                if (flag == "RoomNo")
                {
                    SqlQueryText = string.Format("select id,roomcode,roomname,berth from CR_ShipRoom where Shipid=(select top 1 shipid from OL_Line where MisLineId={0})", lineid);
                    DataSet DS = new DataSet();
                    DS.Clear();
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('查询错误，您选择的包船线路没找到任何房型分配数据，不能导入！');", true);
                        return;
                    }

                    SqlQueryText = string.Format("select RoomNo from CR_RoomNo where lineid={0}", lineid);
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                    //    ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('您选择的包船线路已经导入过房型分配数据，不能再次导入！');", true);
                    //    return;
                    }

                    string OldRoomNo = "", RoomNo = "", Roomcode = "", Berth = "";

                    for (int i = Row; i < dtExcelDatas[0].Rows.Count; i++)
                    {
                        OldRoomNo = "";
                        RoomName = "";
                        AllotId = "";
                        RoomNo = "";
                        Roomcode = "";
                        Berth = "";
                        AllotBerth = "";
                        ImportFlag = "YES";
                        switch (DropDownList1.Text)
                        {
                            case "1":
                                if (i == 0)
                                {
                                    OldRoomNo = "";
                                }
                                else
                                {
                                    OldRoomNo = dtExcelDatas[0].Rows[i - 1]["房号"].ToString().Trim().Replace("'", "");
                                }
                                //DS.Tables[0].Rows[0]["visit"].ToString().Replace("'", "");
                                RoomNo = dtExcelDatas[0].Rows[i]["房号"].ToString().Trim().Replace("'", "");
                                Roomcode = dtExcelDatas[0].Rows[i]["舱型"].ToString().Trim().Replace("'", "");
                                Berth = dtExcelDatas[0].Rows[i]["最大入住人数"].ToString().Trim().Replace("'", "");
                                break;
                            case "2":
                                if (i == 1)
                                {
                                    OldRoomNo = "";
                                }
                                else
                                {
                                    OldRoomNo = dtExcelDatas[0].Rows[i - 1]["舱号"].ToString().Trim().Replace("'", "");
                                }

                                RoomNo = dtExcelDatas[0].Rows[i]["舱号"].ToString().Trim().Replace("'", "");
                                Roomcode = dtExcelDatas[0].Rows[i]["目录"].ToString().Trim().Replace("'", "");
                                Berth = dtExcelDatas[0].Rows[i]["最大入住_人数"].ToString().Trim().Replace("'", "");
                                break;
                            case "3":
                                if (i == 0)
                                {
                                    OldRoomNo = "";
                                }
                                else
                                {
                                    OldRoomNo = dtExcelDatas[0].Rows[i - 1]["房号"].ToString().Trim().Replace("'", "");
                                }
                                //DS.Tables[0].Rows[0]["visit"].ToString().Replace("'", "");
                                RoomNo = dtExcelDatas[0].Rows[i]["房号"].ToString().Trim().Replace("'", "");
                                Roomcode = dtExcelDatas[0].Rows[i]["房型"].ToString().Trim().Replace("'", "");
                                Berth = dtExcelDatas[0].Rows[i]["入住人数"].ToString().Trim().Replace("'", "");
                                break;
                            default:
                                break;
                        }

                        if (OldRoomNo != RoomNo)
                        {
                            if (MyConvert.ConToInt(RoomNo) > 0)
                            {
                                GetRoomName(DS.Tables[0], DS1.Tables[0], Roomcode, Berth, RoomNo);
                                if (ImportFlag == "YES")
                                {
                                    ImportCount += 1;
                                    Sql.Add(string.Format("insert into CR_RoomNo (Lineid,RoomId,RoomNo,roomcode,RoomName,berth,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                                        lineid,
                                        AllotId,
                                        RoomNo,
                                        Roomcode,
                                        RoomName,
                                        Berth,
                                        DateTime.Now.ToString()
                                    ));
                                }
                                else
                                {
                                    if (ImportFlag == "No")
                                    {
                                        Strings.Append((i + 2) + "行 " + Roomcode + " " + RoomNo);
                                        Strings.Append("：该舱型在船队房型表中不存在，不能导入！<br>");
                                    }
                                    else
                                    {
                                        Strings.Append((i + 2) + "行 " + Roomcode + " " + RoomNo);
                                        Strings.Append("：该舱型房号已导入，不能重复操作！<br>");
                                    }
                                }

                            }
                            else
                            {
                                RepeatCount += 1;
                                Strings.Append((i + 2) + "行 " + Roomcode + " " + RoomNo);
                                Strings.Append("：舱房号有误！<br>");
                            }
                        }
                    }
                }


                if (flag == "Dinner")
                {
                    SqlQueryText = string.Format("select TabelNo from CR_DinnerNo where lineid={0}", lineid);
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                    string TabelNo = "", DinnerTime = "", Berth = "";

                    for (int i = Row; i < dtExcelDatas[0].Rows.Count; i++)
                    {
                        TabelNo = "";
                        DinnerTime = "";
                        Berth = "";
                        ImportFlag = "YES";

                        TabelNo = dtExcelDatas[0].Rows[i]["餐桌号"].ToString().Trim().Replace("'", "");
                        Berth = dtExcelDatas[0].Rows[i]["每桌人数"].ToString().Trim().Replace("'", "");
                        DinnerTime = dtExcelDatas[0].Rows[i]["用餐时间"].ToString().Trim().Replace("'", "");

                        if (MyConvert.ConToInt(Berth) > 0)
                        {
                            GetTableName(DS1.Tables[0], TabelNo);
                            if (ImportFlag == "YES")
                            {
                                ImportCount += 1;
                                Sql.Add(string.Format("insert into CR_DinnerNo (Lineid,TabelNo,Berth,DinnerTime) values ('{0}','{1}','{2}','{3}')",
                                    lineid,
                                    TabelNo,
                                    Berth,
                                    DinnerTime
                                ));
                            }
                            else
                            {
                                Strings.Append((i + 2) + "行 " + TabelNo);
                                Strings.Append("：该餐桌号已导入，不能重复操作！<br>");
                            }
                        }
                        else
                        {
                            RepeatCount += 1;
                            Strings.Append((i + 2) + "行 " + TabelNo);
                            Strings.Append("：每桌人数有误！<br>");
                        }
                        
                    }
                }

                if (flag == "BookingNo")
                {
                    SqlQueryText = string.Format("select RoomNo from CR_RoomNo where lineid={0}", lineid);
                    DataSet DS1 = new DataSet();
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    DataTable dt = DS1.Tables[0];

                    string BookingNo = "", RoomNo = "";

                    for (int i = Row; i < dtExcelDatas[0].Rows.Count; i++)
                    {
                        BookingNo = "";
                        RoomNo = "";
                        ImportFlag = "YES";

                        BookingNo = dtExcelDatas[0].Rows[i]["Booking Id"].ToString().Trim().Replace("'", "");
                        RoomNo = dtExcelDatas[0].Rows[i]["Cabin Number"].ToString().Trim().Replace("'", "");

                        DataRow[] drs = dt.Select("RoomNo='" + RoomNo + "'");
                        if (drs.Count() > 0)
                        {
                            ImportCount += 1;
                            Sql.Add(string.Format("update CR_RoomNo set BookingNo='{2}' where lineid='{0}' and roomno='{1}'",
                                lineid,
                                RoomNo,
                                BookingNo
                            ));
                        }
                        else
                        {
                            RepeatCount += 1;
                            Strings.Append((i + 2) + "行 房号" + RoomNo);
                            Strings.Append("：分配记录里不存在，不能导入！<br>");
                        }

                    }
                }
                

                InputResult = "本次导入 " + ImportCount + " 条记录<br>共有 " + RepeatCount + " 条记录没有导入<br><br>" + Strings.ToString();
                
                string[] SqlQueryList = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryList) == true)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('导入完成！');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('导入失败！');", true);
                }
                
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "clientScipt", "alert('没有查询到任何要导入的数据！');", true);
            }
        }


        protected void GetRoomName(DataTable dt, DataTable dt1, string code, string nums, string rno)
        {
            DataRow[] drs = dt.Select("roomcode='" + code + "' and berth='" + nums + "'");
            DataRow[] drn = dt1.Select("RoomNo='" + rno + "'");
            if (drn.Count() > 0)
            {
                ImportFlag = "Repeat";
                RepeatCount += 1;
            }
            else
            {
                if (drs.Count() > 0)
                {
                    AllotId = drs[0]["id"].ToString();
                    RoomName = drs[0]["roomname"].ToString();
                    AllotBerth = drs[0]["berth"].ToString();
                    //foreach (DataRow dr in drs)
                    //{
                    //    RoomName = dr["id"].ToString();
                    //    AllotBerth = dr["id"].ToString();
                    //    AllotId = dr["id"].ToString();
                    //}
                }
                else
                {
                    ImportFlag = "No";
                    RepeatCount += 1;
                }
            }
        }

        protected void GetTableName(DataTable dt,string rno)
        {
            DataRow[] drs = dt.Select("TabelNo='" + rno + "'");
            if (drs.Count() > 0)
            {
                ImportFlag = "Repeat";
                RepeatCount += 1;
            }
            else
            {
                ImportFlag = "YES";
            }
        }

    }
}