using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.Class.Travel;

namespace TravelOnline.CruisesOrder
{
    public partial class AdjustOther : System.Web.UI.Page
    {
        public string TitleName, AutoId, OrderName, hide, ChangeName, RebateFlag, Original, ChangeInfos, QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, shipid, RoomList, CruisesRoomString;
        //public string RB1, RB2, RB3, RB4, BranchOption, Preference, BranchMap;
        public string Flag;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            Flag = Request.QueryString["flag"];
            if (Convert.ToString(Session["Online_UserDept"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                switch (Flag)
                {
                    case "DinnerChange":
                        TitleName = "用餐时间变更";
                        break;
                    case "Settlement":
                        TitleName = "结算方式变更";
                        break;
                    case "Combine":
                        TitleName = "订单关联操作";
                        break;
                    default:
                        Response.Write("传递的参数不正确");
                        Response.End();
                        break;
                }
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText;
            SqlQueryText = string.Format("select id from OL_OrderApply where flag='0' and orderid='{0}'", QueryId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("您已经提交过申请，在审核通过之前，同一订单只能申请一次");
                Response.End();
            }

            SqlQueryText = string.Format("select *,(select top 1 cancel from CR_Confirm where lineid=OL_Order.lineid) as cancel from OL_Order where OrderId='{0}'", QueryId, Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9" || DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "8")
                {
                    Response.Write("已经删除的订单不能操作！");
                    Response.End();
                }
                if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
                {
                }
                else
                {
                    if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                }
                //AutoId
                AutoId = DS.Tables[0].Rows[0]["AutoId"].ToString();
                OrderName = DS.Tables[0].Rows[0]["OrderName"].ToString();
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
                NumsInfo = string.Format(" 预订人数：{0}成人", Adults);
                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
                switch (Flag)
                {
                    case "DinnerChange":
                        LoadDinner(DS.Tables[0].Rows[0]["LineID"].ToString());
                        break;
                    case "Settlement":
                        LoadRebate(DS.Tables[0].Rows[0]["LineID"].ToString(), RebateFlag);
                        break;
                    case "Combine":
                        //TitleName = "用餐时间变更";
                        LoadCombine();
                        break;
                    default:
                        Response.Write("传递的参数不正确");
                        Response.End();
                        break;
                }
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected void LoadDinner(string LineId)
        {
            string OldDinner = MyDataBaseComm.getScalar("select ExtContent from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='" + OrderId + "'");
            ChangeInfos = "原用餐时间：" + OldDinner.Replace("All", "晚餐时间均可安排") + "<br><br>请选择需变更的用餐时间：";
            string dinner = MyDataBaseComm.getScalar("select dinner from OL_Line where MisLineId='" + LineId + "'");

            string Dtime1 = "";
            string Dtime2 = "";
            string Dnum1 = "";
            string Dnum2 = "";
            string chk = "";
            string[] Dtime = dinner.Split('|');
            if (Dtime.Length > 1)
            {

                Dtime1 = Dtime[0].Split("@".ToCharArray())[0];
                Dnum1 = Dtime[0].Split("@".ToCharArray())[1];
                Dtime2 = Dtime[1].Split("@".ToCharArray())[0];
                Dnum2 = Dtime[1].Split("@".ToCharArray())[1];
                //.Split("/".ToCharArray())[0]
                ChangeInfos += string.Format("<input tgs=\"{1}\" id=\"Radio1\" type=\"radio\" name=\"dinner\" value=\"1@{0}\" checked=\"checked\" /><label for=\"Radio1\" class=\"radiobtn\">{0}</label>", Dtime1, MyConvert.ConToInt(Dnum1));
                ChangeInfos += string.Format("<input tgs=\"{1}\" id=\"Radio2\" type=\"radio\" name=\"dinner\" value=\"2@{0}\" /><label for=\"Radio2\" class=\"radiobtn\">{0}</label>", Dtime2, MyConvert.ConToInt(Dnum2));
            }
            else
            {
                chk = "checked=\"checked\"";
            }

            ChangeInfos += string.Format("<input tgs=\"0\" id=\"Radio3\" type=\"radio\" name=\"dinner\" value=\"3@All\" {1} /><label for=\"Radio3\" class=\"radiobtn\">{0}</label>", "以上时间均可", chk);
        }

        protected void LoadRebate(string LineId, string Rflag)
        {
            //string RebateFlag = Request.QueryString["PayType"];
            if (Rflag == "1")
            {
                ChangeInfos = "原结算方式：按订单结算价付款<br><br>请选择需变更的结算方式：";
            }
            else
            {
                ChangeInfos = "原结算方式：按订单全额付款<br><br>请选择需变更的结算方式：";
            }
            
            ChangeInfos += "<input id=\"Radio1\" type=\"radio\" name=\"RebateFlag\" value=\"0\" checked=\"checked\" /><label for=\"Radio1\" class=radiobtn>按订单全额付款</label>";
            ChangeInfos += "<input id=\"Radio2\" type=\"radio\" name=\"RebateFlag\" value=\"1\" /><label for=\"Radio2\" class=radiobtn>按订单结算价付款</label>";
            
        }

        protected void LoadCombine()
        {
            ChangeInfos = "<br>请输入与 " + AutoId + " " + OrderName + " 关联(合并)的订单号：";
            ChangeInfos += "<input id=\"CombineId\" name=\"CombineId\" type=\"text\" class=\"ipt easyui-numberbox\" precision=\"0\" max=\"999999\" style=\"width: 80px;text-align:center;\" maxlength=\"6\" /><br><br>";
        }

    }
}