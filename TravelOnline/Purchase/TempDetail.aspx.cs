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

namespace TravelOnline.Purchase
{
    public partial class TempDetail : System.Web.UI.Page
    {
        public string QueryId, OrderId, LineName, BeginDate, NumsInfo, Nums, Adults, Childs, PriceList, AllPrice, AvePrice,FaxNumber,HTurl;
        public string User_Name, User_Mobile, User_Tel, User_Email, User_Address, User_Fax, User_Memo, GuestListInfo, BranchOption;
        public string RB1, RB2, RB3, RB4, RC1, RC2, hide1,CompanyAddress;
        public string FpInfo1, FpInfo2, FpInfo3, InvoiceShow, rb_hide1, rb_hide3, rc_hide1, mendian_hide;
        public string huifeng, huifeng1 = "hide", huifengid, tebieshuoming = "特别说明", dianziyoujian="电子邮件";
        protected void Page_Load(object sender, EventArgs e)
        {
            QueryId = Request.QueryString["OrderId"];
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
            string SqlQueryText = string.Format("select *,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='contract' and ExtId='2') as address,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='contract' and ExtId='4') as branch,(select top 1 ExtContent from OL_OrderExtend where OrderId=OL_TempOrder.OrderId and ExtType='invoice') as fpinfo from OL_TempOrder where OrderId='{0}'", QueryId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(Session["Online_UserId"]) != DS.Tables[0].Rows[0]["OrderUser"].ToString()) Response.Redirect("~/index.html", true);
                
                hide1 = "btn-link btn-personal";
                if (DS.Tables[0].Rows[0]["PayType"].ToString() == "2") mendian_hide = "hide";
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
                        //ProductClass
                        HTurl = "<A href=\"/Upload/上海市出境旅游合同示范文本.doc\">上海市出境旅游合同范本</A>";
                        if (DS.Tables[0].Rows[0]["ProductClass"].ToString() == "851") HTurl = "<A href=\"/Upload/大陆居民赴台湾地区旅游合同.doc\">大陆居民赴台湾地区旅游合同</A>";
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

                int price = Convert.ToInt32(DS.Tables[0].Rows[0]["Price"]);
                AllPrice = price.ToString();
                AvePrice = (price / Convert.ToInt32(Nums)).ToString();

                if (Convert.ToInt32(Childs) > 0) NumsInfo = string.Format(" 预订人数：{0}成人 {1}儿童", Adults, Childs);
                BeginDate = string.Format("出发日期：{0:yyyy年MM月dd日}", DS.Tables[0].Rows[0]["BeginDate"]);
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
                GuestListInfo = PurchaseClass.GetGuestList(DS.Tables[0].Rows[0]["ProductType"].ToString(), DS.Tables[0].Rows[0]["ProductClass"].ToString(), Convert.ToInt32(Nums), OrderId);
            }
            else
            {
                Response.Redirect("~/index.html", true);
            }

            if (Request.Cookies["HuiFeng2014"] != null)
            {
                huifeng = "hide";
                huifeng1 = "";
                mendian_hide = "hide";
                huifengid = "1";
                tebieshuoming = "会员工号及特别说明<br>(有几位会员须填写几位工号)";
                dianziyoujian = "汇丰内部邮箱";
            }
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