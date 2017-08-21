using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.LoginUsers;
using System.Text.RegularExpressions;

namespace TravelOnline.CruisesOrder
{
    public partial class SecondStep : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice, FaxNumber, HTurl;
        public string User_Name, User_Mobile, User_Tel, User_Email, User_Address, User_Fax, User_Memo, GuestListInfo, BranchOption;
        public string RB1, RB2, RB3, RB4, RC1, RC2, hide1, hide2, CompanyAddress, BeginDateInfo, AgeLimit, VisitSell;
        public string FpInfo1, FpInfo2, FpInfo3, InvoiceShow, rb_hide1, rb_hide3, rc_hide1, RoomOrder, SaveInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
            //LoadTempOrder();
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                LoadTempOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

        }

        protected void LoadTempOrder()
        {
            string SqlQueryText = string.Format("select *,(select top 1 AgeLimit from OL_Line where MisLineId=OL_TempOrder.LineID) as AgeLimit,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='contract' and ExtId='2') as address,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='contract' and ExtId='4') as branch,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='invoice') as fpinfo,(select top 1 VisitSell from OL_Line where MisLineId=OL_TempOrder.lineid) as VisitSell from OL_TempOrder where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                hide1 = "btn-link btn-personal";
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9") hide1 = "hide";
                CompanyAddress = "上海市徐汇区衡山路2号";
                //:021-64742928(出境) 021-64670982(国内) 
                switch (DS.Tables[0].Rows[0]["ProductType"].ToString())
                {
                    case "InLand":
                        CompanyAddress = "上海市桃江路1号4楼";
                        FaxNumber = "021-64670982(国内)";
                        HTurl = "<A href=\"/Upload/上海市国内旅游合同示范文本.doc\">上海市国内旅游合同范本</A>";
                        break;
                    case "OutBound":
                        FaxNumber = "021-64742928(出境)";
                        HTurl = "<A href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A>";
                        break;
                    case "Cruises":
                        FaxNumber = "021-64742928(出境)";
                        HTurl = "<A href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A>";
                        break;
                    case "FreeTour":
                        FaxNumber = "021-64742928(出境)";
                        HTurl = "<A href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A>";
                        if (DS.Tables[0].Rows[0]["ProductClass"].ToString() == "1062")
                        {
                            HTurl = "<A href=\"/Upload/上海市国内旅游合同示范文本.doc\">上海市国内旅游合同范本</A>";
                            FaxNumber = "021-64670982(国内)";
                        }
                        break;
                    case "Visa":
                        FaxNumber = "021-64742928(出境)";
                        HTurl = "<A href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A>";
                        break;
                    default:
                        break;
                }
                OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Adults = DS.Tables[0].Rows[0]["Adults"].ToString();
                Childs = DS.Tables[0].Rows[0]["Childs"].ToString();

                AgeLimit = DS.Tables[0].Rows[0]["AgeLimit"].ToString();
                VisitSell = DS.Tables[0].Rows[0]["VisitSell"].ToString();

                //SaveInfo = "checked=checked";
                if (Convert.ToString(Session["Online_UserDept"]).Length > 0)
                {
                    hide2 = "hide";
                    SaveInfo = "";
                }

                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2") hide2 = "hide";

                /////////////
                DateTime LimitDate = Convert.ToDateTime(DS.Tables[0].Rows[0]["BeginDate"]);
                LimitDate = LimitDate.AddDays(-1);
                LimitDate = LimitDate.AddYears(-MyConvert.ConToInt(AgeLimit));

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDateInfo = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
                BeginDate = string.Format("{0:yyyy-MM-dd}", LimitDate);
                BranchOption = PurchaseClass.GetBranch(0, "Option");
                if (DS.Tables[0].Rows[0]["OrderName"].ToString().Trim().Length > 0)
                {
                    User_Email = DS.Tables[0].Rows[0]["OrderEmail"].ToString();
                    User_Name = DS.Tables[0].Rows[0]["OrderName"].ToString();
                    User_Tel = DS.Tables[0].Rows[0]["OrderTel"].ToString();
                    User_Mobile = DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                    User_Fax = DS.Tables[0].Rows[0]["OrderFax"].ToString();
                    User_Memo = DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                    switch (DS.Tables[0].Rows[0]["Contract"].ToString())
                    {
                        case "1":
                            RB1 = "checked=\"checked\"";
                            break;
                        case "2":
                            User_Address = DS.Tables[0].Rows[0]["address"].ToString();
                            RB2 = "checked=\"checked\"";
                            break;
                        case "3":
                            RB3 = "checked=\"checked\"";
                            break;
                        case "4":
                            BranchOption = PurchaseClass.GetBranch(Convert.ToInt32(DS.Tables[0].Rows[0]["branch"].ToString()), "PayOption");
                            RB4 = "checked=\"checked\"";
                            break;
                        default:
                            break;
                    }
                    string invoice = DS.Tables[0].Rows[0]["Invoice"].ToString();
                    switch (invoice)
                    {
                        case "1":
                            RC1 = "checked=\"checked\"";
                            break;
                        case "3":
                            RC2 = "checked=\"checked\"";
                            break;
                        case "0":
                            InvoiceShow = "hide";
                            break;
                        default:
                            RC1 = "checked=\"checked\"";
                            InvoiceShow = "hide";
                            break;
                    }
                    if (invoice == "1" || invoice == "3")
                    {
                        string[] FP = Regex.Split(DS.Tables[0].Rows[0]["fpinfo"].ToString(), @"\|\|", RegexOptions.IgnoreCase);
                        FpInfo1 = FP[0];
                        FpInfo2 = FP[1];
                        FpInfo3 = FP[2];
                    }
                    rb_hide3 = "hide";
                    if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "InLand" && DS.Tables[0].Rows[0]["PayType"].ToString() == "1")
                    {
                        rb_hide1 = "hide";
                        rc_hide1 = "hide";
                        rb_hide3 = "";
                    }
                }
                else
                {
                    rb_hide3 = "hide";
                    if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "InLand" && DS.Tables[0].Rows[0]["PayType"].ToString() == "1")
                    {
                        rb_hide1 = "hide";
                        rc_hide1 = "hide";
                        rb_hide3 = "";
                        RB3 = "checked=\"checked\"";
                        RC2 = "checked=\"checked\"";
                    }
                    else
                    {
                        if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "InLand")
                        {
                            RB3 = "checked=\"checked\"";
                        }
                        else
                        {
                            RB1 = "checked=\"checked\"";
                        }

                        RC1 = "checked=\"checked\"";
                    }
                    InvoiceShow = "hide";
                    LoadUserInfo();
                }

                GuestListInfo = PurchaseClass.GetCruisesGuestList(
                    DS.Tables[0].Rows[0]["ProductType"].ToString(), 
                    DS.Tables[0].Rows[0]["ProductClass"].ToString(), 
                    Convert.ToInt32(Nums),
                    OrderId
                );

                RoomOrder = GetRoomOrder();
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }

        protected string GetRoomOrder()
        {
            StringBuilder Stings = new StringBuilder();
            string SqlQueryText = string.Format("select * from CR_RoomOrder where OrderId='{0}'", QueryId);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                {
                    Stings.Append("{");
                    Stings.Append(string.Format("'roomid': '{0}',", DS1.Tables[0].Rows[i]["roomid"].ToString()));
                    Stings.Append(string.Format("'roomname': '{0}',", DS1.Tables[0].Rows[i]["roomname"].ToString()));
                    Stings.Append(string.Format("'adult': {0},", DS1.Tables[0].Rows[i]["adult"].ToString()));
                    Stings.Append(string.Format("'childs': {0}", DS1.Tables[0].Rows[i]["childs"].ToString()));
                    Stings.Append("},");
                }
            }
            string infos = Stings.ToString();
            infos = infos.Substring(0, infos.Length - 1);
            return infos;
        }

        protected void LoadUserInfo()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            string SqlQueryText;
            SqlQueryText = string.Format("Id='{0}'", Convert.ToString(Session["Online_UserId"]));

            RUser = LoginUser.LoginUseDetail(SqlQueryText);
            if (RUser != null)
            {
                User_Email = RUser.UserEmail;
                User_Name = RUser.UserName;
                User_Tel = RUser.Tel;
                User_Mobile = RUser.Mobile;
                User_Address = RUser.Address;
                if (User_Email.Split("@".ToCharArray())[0] == User_Name) User_Name = "";
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }
        }
    }
}