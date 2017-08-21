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
    public partial class AjaxService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //强制刷新页面，不允许从缓存中读取
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            switch (Request.QueryString["action"])
            {
                case "ManageLine":
                    
                    break;
                case "LoadCruisesRoom":
                    LoadCruisesRoom();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        protected void LoadCruisesRoom()
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select (select count(id) from CR_RoomList where orderflag='0' and allotid=CR_RoomAllot.id) as sellroom,(select AgeLimit from OL_Line where MisLineId=CR_RoomAllot.lineid)as AgeLimit,CR_ShipRoom.configure,CR_ShipRoom.intro,CR_RoomAllot.*,(select top 1 infos from CR_Rebate where allotid=CR_RoomAllot.id and begindate <= '{1:yyyy-MM-dd}' AND enddate >= '{1:yyyy-MM-dd}' and flag='0' order by id) as RoomRebateInfos,(select top 1 infos from CR_Rebate where allotid=CR_RoomAllot.id and begindate <= '{1:yyyy-MM-dd}' AND enddate >= '{1:yyyy-MM-dd}' and flag='1' order by id) as ViewRebateInfos from CR_RoomAllot,CR_ShipRoom where CR_RoomAllot.id='{0}' and CR_RoomAllot.roomid=CR_ShipRoom.id", 
                Request.QueryString["Id"],
                DateTime.Today
            );
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string AgeLimit = "";
                if (DS.Tables[0].Rows[0]["AgeLimit"].ToString().Length > 0) AgeLimit = "(" + DS.Tables[0].Rows[0]["AgeLimit"].ToString() + "周岁以下)";

                Strings.Append("<div class=roomlist>");
                SqlQueryText = "SELECT top 5 * from CR_Pic where roomid='" + DS.Tables[0].Rows[0]["roomid"].ToString() + "' and pictype='room'";
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Strings.Append("<div id=roompic>");
                    string Pics = "";
                    string PicsBig = "";

                    ///Upload/Hotel/2/2012071916171459746945.jpg  DS1.Tables[0].Rows[i]["roomtype"].ToString();

                    for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                    {
                        Pics = string.Format("/Upload/Cruises/{0}/Thumb_{1}", DS1.Tables[0].Rows[i]["shipid"].ToString(), DS1.Tables[0].Rows[i]["picurl"].ToString().Split("/".ToCharArray())[4]);
                        PicsBig = DS1.Tables[0].Rows[i]["picurl"].ToString();
                        Strings.Append(string.Format("<a class=tip tag={0} href=\\\"javascript:void(0);\\\"><IMG onerror=\\\"this.src='/Images/none.gif'\\\" src=\\\"{1}\\\" width=100 height=75 title={2}></a>", PicsBig, Pics, DS1.Tables[0].Rows[i]["roomtype"].ToString()));
                    }
                    Strings.Append("</div>");
                }

                int HaveRoom = MyConvert.ConToInt(DS.Tables[0].Rows[0]["nums"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellroom"].ToString());
                int DefaultOrderNums = MyConvert.ConToInt(ConfigurationManager.AppSettings["DefaultOrderNums"]);
                int OrderNums = 0;
                int berth = MyConvert.ConToInt(DS.Tables[0].Rows[0]["berth"].ToString());
                if (HaveRoom < 0) HaveRoom = 0;

                OrderNums = berth * HaveRoom;
                if (OrderNums > DefaultOrderNums) OrderNums = DefaultOrderNums;
                //if (HaveRoom < 15)
                //{
                    
                //}

                string DeckString = "";
                if (DS.Tables[0].Rows[0]["deck"].ToString().Length > 0)
                {
                    try
                    {
                        string[] arr = DS.Tables[0].Rows[0]["deck"].ToString().Split(',');
                        for (int l = 0; l < arr.Length; l++)
                        {
                            DeckString += string.Format("<A class=deck href='javascript:void(0)' onclick=\\\"ShowDeck('{0}','{1}')\\\">{1}</A>", DS.Tables[0].Rows[0]["shipid"].ToString(), arr[l]);
                        }
                    }
                    catch
                    { }
                }
                Strings.Append(string.Format("<div><span>床位配置：{0}</span><span>甲板示意图：{1}</span></div>", DS.Tables[0].Rows[0]["configure"].ToString(), DeckString));
                Strings.Append(string.Format("<div><span>设施简介：{0}</span></div>", DS.Tables[0].Rows[0]["intro"].ToString().Replace("\r\n", "<br>")));

                //显示舱房优惠
                if (DS.Tables[0].Rows[0]["RoomRebateInfos"].ToString().Length > 2) Strings.Append(string.Format("<div><span style=\\\"color: red;\\\">舱房优惠：{0}</span></div>", DS.Tables[0].Rows[0]["RoomRebateInfos"].ToString()));
                //显示观光优惠
                if (DS.Tables[0].Rows[0]["ViewRebateInfos"].ToString().Length > 2) Strings.Append(string.Format("<div><span style=\\\"color: red;\\\">观光优惠：{0}</span></div>", DS.Tables[0].Rows[0]["ViewRebateInfos"].ToString()));

                string allotid = DS.Tables[0].Rows[0]["id"].ToString();
                Strings.Append(string.Format("<div class=roomsell tps='{1}' haveroom='{2}' mnum='{3}' p1='{4}' p2='{5}' p3='{9}' tag={0} id=RoomSell{0}><span>成人：{6}</span><span>儿童{10}：{7} </span><span>房间数：{8} </span>价格小计：<span class=pcount>&yen;0</span><span class=RoomSelectButton><strong id=RB_{0} class=radioBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong>选择此舱</span><span class=hide>0</span></div>", 
                    allotid, 
                    DS.Tables[0].Rows[0]["roomname"].ToString(), 
                    HaveRoom,
                    berth, 
                    DS.Tables[0].Rows[0]["price"].ToString(), 
                    DS.Tables[0].Rows[0]["thirdprice"].ToString(),
                    GetOrderDropList(OrderNums, 0, "cr", allotid),
                    GetOrderDropList(OrderNums, 0, "et", allotid),
                    "<select class=fjs style='width: 45px'><option value='0'>0</option></select>",
                    DS.Tables[0].Rows[0]["childprice"].ToString(),
                    AgeLimit
                ));
                Strings.Append("</div>");
            }

            string countent = Strings.ToString();
            Strings.Clear();
            //Strings.Append(string.Format("select count({0}) from {1} where {2} ", pkey, tablename, condition));
            Strings.Append("{");
            Strings.Append(string.Format("\"success\":0,\"content\":\"{0}\"", countent));
            Strings.Append("}");
            Response.Write(Strings.ToString());
            Response.End();
        }

        protected string GetOrderDropList(int OrderNums, int EditNum, string dropname, string allotid)
        {
            string OptionHtml = "";
            OptionHtml += string.Format("<select class=psel name='{0}' style='width: 45px;'>", dropname, allotid);
            for (int i = 0; i <= OrderNums; i++)
            {
                if (i == 0)
                {
                    OptionHtml += string.Format("<option value='{0}' selected='selected'>{0}</option>", i);
                }
                else
                {
                    OptionHtml += string.Format("<option value='{0}'>{0}</option>", i);
                }
            }
            OptionHtml += "</select>";

            if (OrderNums == 0)
            {
                OptionHtml = string.Format("<select class=psel name='{0}' style='width: 45px;'>", dropname, allotid);
                OptionHtml += "<option value='{0}' selected='selected'>满</option>";
                OptionHtml += "</select>";
            }
            return OptionHtml;
        }



    }
}