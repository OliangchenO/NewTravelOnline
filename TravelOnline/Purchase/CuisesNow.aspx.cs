using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using TravelOnline.GetCombineKeys;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace TravelOnline.Purchase
{
    public partial class CuisesNow : System.Web.UI.Page
    {
        public string lineid, planid, begindate, nums, etnums, Seats, OrderInfo, routeflag, planno;
        public string step1, step2, step3, Btn1;
        public Guid ucode;
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            step2 = "style=\"DISPLAY:none\"";
            step3 = "style=\"DISPLAY:none\"";
            lineid = Request.QueryString["lineid"];
            planid = Request.QueryString["planid"];
            begindate = Request.QueryString["begindate"];
            ucode = CombineKeys.NewComb(); //System.Guid.NewGuid();

            string SqlQueryText = string.Format("select top 1 Sale from OL_Line where MisLineId='{0}'", lineid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["Sale"].ToString() == "1")
                {
                    step1 = "style=\"DISPLAY:none\"";
                    Btn1 = "style=\"DISPLAY:none\"";
                    step3 = "";
                    return;
                }
            }
            else
            {
                step1 = "style=\"DISPLAY:none\"";
                Btn1 = "style=\"DISPLAY:none\"";
                step3 = "";
                return;
            }

            //保存邮轮舱位
            StringBuilder RoomString = new StringBuilder();
            List<string> Sql = new List<string>();
            string[] AllInfo = Regex.Split(Request.QueryString["Parms"], @"\|\|", RegexOptions.IgnoreCase); //Request.QueryString["PriceStrings"].Split("||".ToArray());
            string PriceSql;
            int Anums, Cnums;
            Anums = 0;
            Cnums = 0;
            if (AllInfo.Length > 0)
            {
                RoomString.Append("<ul id=SellPrice class=Cruises>");
                RoomString.Append("<li class=title><div class=t1>舱位名称</div><div class=t2>价格</div><div class=t3>房间数</div><div class=t4>成人</div><div class=t5>儿童</div></li>");
                       
                for (int i = 0; i < AllInfo.Length; i++)
                {
                    string[] PriceInfo = Regex.Split(AllInfo[i], @"\@\@", RegexOptions.IgnoreCase);  //AllInfo[i].Split("@@".ToArray());
                    if (PriceInfo.Length > 0)
                    {

                        PriceSql = string.Format("insert into OL_CuisesRoom (OrderId,PriceId,CuisesRoomId,RoomName,BedNum,RoomNum,AdultNum,ChildNum,price,InputDate,OrderNum) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                                    ucode,
                                    PriceInfo[0],
                                    PriceInfo[1],
                                    PriceInfo[2],
                                    PriceInfo[3],
                                    PriceInfo[4],
                                    PriceInfo[5],
                                    PriceInfo[6],
                                    PriceInfo[7],
                                    DateTime.Now.ToString(),
                                    MyConvert.ConToInt(PriceInfo[5]) + MyConvert.ConToInt(PriceInfo[6])
                        );
                        RoomString.Append(string.Format("<li class=roomli><div class=t1>{0}</div><div class=r2>&yen;{1}</div><div class=t3>{2}间</div><div class=t4>{3}</div><div class=t5>{4}</div></li>", PriceInfo[2], PriceInfo[7], PriceInfo[4], PriceInfo[5], PriceInfo[6]));
                        //RoomString.Append(string.Format("<li class=roomli><div class=t1>{0}</div><div class=r2>&yen;{1}</div><div class=t3>{2}间</div><div class=t4>{3}</div><div class=t5>{4}</div></li>", PriceInfo[2], PriceInfo[7], PriceInfo[4], PriceInfo[5], PriceInfo[6]));
                        //RoomString.Append(string.Format("<li class=roomli><div class=t1>{0}</div><div class=r2>&yen;{1}</div><div class=t3>{2}间</div><div class=t4>{3}</div><div class=t5>{4}</div></li>", PriceInfo[2], PriceInfo[7], PriceInfo[4], PriceInfo[5], PriceInfo[6]));
                        Anums += MyConvert.ConToInt(PriceInfo[5]);
                        Cnums += MyConvert.ConToInt(PriceInfo[6]);
                        Sql.Add(PriceSql);
                    }
                }
                RoomString.Append("</ul>");

                string[] SqlQuerysText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerysText) != true)
                {
                    step1 = "style=\"DISPLAY:none\"";
                    Btn1 = "style=\"DISPLAY:none\"";
                    step3 = "";
                    return;
                }
            }
            else
            {
                step1 = "style=\"DISPLAY:none\"";
                Btn1 = "style=\"DISPLAY:none\"";
                step3 = "";
                return;
            }
            step2 = RoomString.ToString();
            nums = Anums.ToString();
            etnums = Cnums.ToString();

            PlanSeats GetPlan = new PlanSeats();

            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            try
            {
                GetPlan = rsp.GetPlanSeats(UpPassWord, lineid, planid, begindate);
            }
            catch
            {
                step1 = "style=\"DISPLAY:none\"";
                Btn1 = "style=\"DISPLAY:none\"";
                step3 = "";
                return;
            }

            DateTime today = DateTime.Today;
            planno = GetPlan.PlanNo;

            if (GetPlan.StopDate.Length > 0)
            {
                if (Convert.ToDateTime(GetPlan.StopDate) < today)
                {
                    step1 = "style=\"DISPLAY:none\"";
                    Btn1 = "style=\"DISPLAY:none\"";
                    step3 = "";
                    return;
                }
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Seats = "0";
        }
    }
}