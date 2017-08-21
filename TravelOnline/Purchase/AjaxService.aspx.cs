using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Purchase;
using System.Text.RegularExpressions;
using Sunrise.Spell;
using System.Data;
using TravelOnline.TravelMisWebService;
using System.Configuration;
using System.Text;
using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using System.Net;
using System.IO;
using TravelOnline.EncryptCode;

namespace TravelOnline.Purchase
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
            if (Request.QueryString["action"] == "GetCouponNums")
            { }
            else
            {
                if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
                { }
                else
                {
                    Response.Write("{\"success\":1}");
                    Response.End();
                }
            }
            

            switch (Request.QueryString["action"])
            {
                case "TempOrder":
                    TempOrder();
                    break;
                case "FirdtStep":
                    FirdtStep();
                    break;
                case "SecondStep":
                    SecondStep();
                    break;
                case "ThirdStep":
                    ThirdStep();
                    break;
                case "VisitEdit":
                    VisitEdit();
                    break;
                case "GetPinYin":
                    GetPinYin();
                    break;
                case "OrderLogInfo":
                    OrderLogInfo();
                    break;
                case "CouponUse":
                    CouponUse();
                    break;
                case "CouponBuy":
                    CouponBuy();
                    break;
                case "CouponReceive":
                    CouponReceive();
                    break;
                case "GetCouponNums":
                    GetCouponNums();
                    break;
                case "ShowMyCoupon":
                    ShowMyCoupon();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        protected void GetCouponNums()
        {
            string SqlQueryText = string.Format("select sellnums,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff from Pre_Policy where uid='{0}'", Request.QueryString["Uid"]);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellnums"].ToString());
                int ff = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ff"].ToString());
                if ((nums - ff) >= 0)
                {
                    Response.Write("{\"success\":\"" + (nums - ff) + "\"}");
                }
                else
                {
                    Response.Write("{\"success\":\"0\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"0\"}");
            }
        }

        protected void ShowMyCoupon()
        {
            string SqlQueryText = string.Format("select id,uno,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}' and flag='0' and sellflag in (4,5) and userid='{1}' and pre_no in (select allotid from CR_RoomOrder where OrderId='{0}')", Request.QueryString["TempOrderId"], Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                StringBuilder Strings = new StringBuilder();
                Strings.Append("<div class=roomdivlist><table border='0' cellpadding='0' cellspacing='0' style='width: 100%;'>");
                Strings.Append("<tr class=tit1>");
                Strings.Append("<td width='20%'>券号</td>");
                Strings.Append("<td width='70%'>使用说明</td>");
                Strings.Append("<td width='10%'>&nbsp;</td>");
                Strings.Append("</tr>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td><a style='color: #159ce9' href='javascript:' onclick=\\\"showid('{0}')\\\">使用</a></td></tr>",
                            DS.Tables[0].Rows[i]["uno"].ToString(),
                            DS.Tables[0].Rows[i]["memo"].ToString()
                        ));
                }
                Strings.Append("</table><div>");
                Response.Write("{\"success\":\"" + Strings .ToString() + "\"}");
            }
            else
            {
                Response.Write("{\"success\":\"没有任何可用优惠券\"}");
            }
        }

        protected void CouponReceive()
        {
            if (Convert.ToString(Session["Online_UserCompany"]).Length > 0)
            {
                Response.Write("{\"error\":\"您的账号（门市或同业）不能领用\"}");
                Response.End();
            }

            //if (String.Compare(Session["CheckCode"].ToString(), Request.QueryString["authcode"].Trim(), true) != 0)
            //{
            //    Response.Write("{\"error\":\"验证码错误\"}");
            //    Response.End();
            //}

            string SqlQueryText = "select * from OL_LoginUser where Id='" + Convert.ToString(Session["Online_UserId"]) + "'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("select *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff,(select top 1 id from Pre_Ticket where pid=Pre_Policy.id and userid='{1}' and flag=0) as buyid from Pre_Policy where uid='{0}'", Request.QueryString["Uid"], Convert.ToString(Session["Online_UserId"]));

                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    if (MyConvert.ConToInt(DS1.Tables[0].Rows[0]["buyid"].ToString()) > 0)
                    {
                        Response.Write("{\"error\":\"您不能重复领用\"}");
                        Response.End();
                    }

                    int nums = MyConvert.ConToInt(DS1.Tables[0].Rows[0]["sellnums"].ToString());
                    int ff = MyConvert.ConToInt(DS1.Tables[0].Rows[0]["ff"].ToString());
                    if (ff >= nums)
                    {
                        Response.Write("{\"error\":\"优惠券已领完\"}");
                        Response.End();
                    }

                    String AutoId = Convert.ToString(CombineKeys.NewComb());
                    SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag,pre_no) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                        DS1.Tables[0].Rows[0]["id"].ToString(),
                        AutoId,
                        MyConvert.CreateVerifyCode(12),
                        DS1.Tables[0].Rows[0]["par"].ToString(),
                        DS1.Tables[0].Rows[0]["amount"].ToString(),
                        DS.Tables[0].Rows[0]["Id"].ToString(),
                        DS1.Tables[0].Rows[0]["begindate"].ToString(),
                        DS1.Tables[0].Rows[0]["enddate"].ToString(),
                        DateTime.Now.ToString(),
                        "0",
                        DS1.Tables[0].Rows[0]["deduction"].ToString(),
                        DS1.Tables[0].Rows[0]["range"].ToString(),
                        DS1.Tables[0].Rows[0]["product"].ToString(),
                        DS.Tables[0].Rows[0]["UserEmail"].ToString(),
                        DS.Tables[0].Rows[0]["UserName"].ToString(),
                        DS1.Tables[0].Rows[0]["pbdate"].ToString(),
                        DS1.Tables[0].Rows[0]["pedate"].ToString(),
                        DS1.Tables[0].Rows[0]["sellflag"].ToString(),
                        DS1.Tables[0].Rows[0]["pre_no"].ToString()
                    );
                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        Response.Write("{\"success\":\"ok\"}");
                    }
                    else
                    {
                        Response.Write("{\"error\":\"优惠券领用失败\"}");
                    }
                }
                else
                {
                    Response.Write("{\"error\":\"优惠券已领完\"}");
                }
            }
            else
            {
                Response.Write("{\"error\":\"优惠券领用失败\"}");
            }
        }

        protected void CouponBuy()
        {
            List<string> Sql = new List<string>();

            string SqlText = string.Format("select *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as ff from Pre_Policy where uid='{0}'", Request.QueryString["Uid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellnums"].ToString());
                int ff = MyConvert.ConToInt(DS.Tables[0].Rows[0]["ff"].ToString());
                int buys = MyConvert.ConToInt(Request.QueryString["Nums"]);
                if ((ff + buys) > nums)
                {
                    Response.Write("{\"error\":\"优惠券数量不足，只剩余"+ (nums - ff) + "张\"}");
                    Response.End();
                }


                Guid ucode = CombineKeys.NewComb();
                Sql.Add(string.Format("insert into OL_TempOrder (OrderId,ProductType,ProductClass,LineID,PlanId,LineName,BeginDate,OrderNums,Price,Adults,Childs,OrderTime,OrderUser,OrderFlag,OrderName,UserName) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{14}')",
                        ucode,
                        "Coupon",
                        DS.Tables[0].Rows[0]["id"].ToString(),
                        "0",
                        "0",
                        "购买优惠券",
                        DS.Tables[0].Rows[0]["begindate"].ToString(),
                        Request.QueryString["Nums"],
                        MyConvert.ConToInt(Request.QueryString["Nums"]) * MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellprice"].ToString()),
                        Request.QueryString["Nums"],
                        "0",
                        DateTime.Now.ToString(),
                        Convert.ToString(Session["Online_UserId"]),
                        "0",
                        Convert.ToString(Session["Online_UserName"])
                ));

                Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        ucode,
                        "CouponBuy",
                        DS.Tables[0].Rows[0]["id"].ToString(),
                        "购买优惠券",
                        DS.Tables[0].Rows[0]["memo"].ToString(),
                        DS.Tables[0].Rows[0]["sellprice"].ToString(),
                        MyConvert.ConToInt(Request.QueryString["Nums"]),
                        MyConvert.ConToInt(Request.QueryString["Nums"]) * MyConvert.ConToInt(DS.Tables[0].Rows[0]["sellprice"].ToString()),
                        DateTime.Now.ToString()
                ));

                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", ucode, DateTime.Now.ToString(), Convert.ToString(Session["Online_UserName"]) + "购买了优惠券"));
                //Sql.Add(SqlText);
                string[] SqlQueryText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":\"" + ucode + "\"}");
                }
                else
                {
                    Response.Write("{\"error\":\"优惠券购买失败，请稍后再试\"}");
                }
            }
            else
            {
                Response.Write("{\"error\":\"优惠券购买失败，请稍后再试\"}");
            }
        }

        protected void CouponUse()
        {
            string SqlQueryText = string.Format("select ProductType,ProductClass,LineID from OL_TempOrder where OrderId='{0}'", Request.QueryString["TempOrderId"]);
            //Convert.ToString(Session["Online_UserId"]), DateTime.Today
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string ProductType, ProductClass, LineID;
                string range, product, amount, price;
                ProductType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                ProductClass = DS.Tables[0].Rows[0]["ProductClass"].ToString();
                LineID = DS.Tables[0].Rows[0]["LineID"].ToString();

                SqlQueryText = string.Format("select * from Pre_Policy where Id='{0}'", Request.QueryString["CouponId"]);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    range = DS.Tables[0].Rows[0]["range"].ToString();
                    product = DS.Tables[0].Rows[0]["product"].ToString();
                    amount = DS.Tables[0].Rows[0]["amount"].ToString();
                    switch (DS.Tables[0].Rows[0]["deduction"].ToString())
                    {
                        case "1":
                            price = Request.QueryString["Amount"];
                            CheckCoupon(ProductType, ProductClass, LineID, range, product, amount, price);
                            break;
                        case "2":
                            price = Request.QueryString["Ave"];
                            CheckCoupon(ProductType, ProductClass, LineID, range, product, amount, price);
                            break;
                        default:
                            Response.Write("{\"error\":\"没有查询到任何优惠券信息\"}");
                            break;
                    }
                }
                else
                {
                    Response.Write("{\"error\":\"没有查询到任何优惠券信息\"}");
                }
            }
            else
            {
                Response.Write("{\"error\":\"没有查询到您当前的订单，请稍后再试\"}");
            }
            
        }

        protected void CheckCoupon(string ProductType, string ProductClass, string LineID, string range, string product, string amount, string price)
        {
            if (MyConvert.ConToInt(amount)>0)
            {
                if (MyConvert.ConToInt(amount) >= MyConvert.ConToInt(price))
                {
                    Response.Write("{\"error\":\"您选择的优惠券不满足需消费" + amount + "元的使用要求\"}");
                    return;
                }
            }

            switch (range)
            {
                case "1":
                    
                    break;
                case "2":
                    if (ProductType != "OutBound")
                    {
                        Response.Write("{\"error\":\"您选择的优惠券只能出境旅游产品使用\"}");
                        Response.End();
                    }
                    break;
                case "3":
                    if (ProductType != "InLand")
                    {
                        Response.Write("{\"error\":\"您选择的优惠券只能国内旅游产品使用\"}");
                        Response.End();
                    }
                    break;
                case "4":
                    if (ProductType != "Visa")
                    {
                        Response.Write("{\"error\":\"您选择的优惠券只能单项服务旅游产品使用\"}");
                        Response.End();
                    }
                    break;
                case "5":
                    if (ProductType != "Cruises")
                    {
                        Response.Write("{\"error\":\"您选择的优惠券只能邮轮旅游产品使用\"}");
                        Response.End();
                    }
                    break;
                case "8":
                    if (product.IndexOf("," + ProductClass + ",") < 0)
                    {
                        Response.Write("{\"error\":\"您选择的优惠券不满足可使用范围\"}");
                        Response.End();
                    }
                    break;
                case "9":
                    if (product.IndexOf("," + LineID + ",") < 0)
                    {
                        Response.Write("{\"error\":\"您选择的优惠券不能在当前旅游线路使用\"}");
                        Response.End();
                    }
                    break;
                default:
                    Response.Write("{\"error\":\"您选择的优惠券不能在当前旅游线路使用\"}");
                    Response.End();
                    break;
            }

            //string SqlQueryText = string.Format("select ProductType,ProductClass,LineID from OL_TempOrder where OrderId='{0}'", Request.QueryString["TempOrderId"]);
            string SqlQueryText = string.Format("select id from Pre_Ticket where pid='{0}' and userid='{1}' and flag='0' and begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}'", Request.QueryString["CouponId"], Convert.ToString(Session["Online_UserId"]), DateTime.Today);
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows.Count < MyConvert.ConToInt(Request.QueryString["CouponNums"]))
                {
                    Response.Write("{\"error\":\"您选择的优惠券数量不足，只有" + DS.Tables[0].Rows.Count + "份可使用\"}");
                    return;
                }
            }
            else
            {
                Response.Write("{\"error\":\"您选择的优惠券已使用或已过期\"}");
                return;
            }
            
        }

        protected void GetPinYin()
        {
            string CnName = Request.QueryString["CnName"];
            if (CnName.Length > 1 && CnName.Length < 5)
            {
                string FirstName = CnName.Substring(0, 1);
                string LastName = CnName.Substring(1, CnName.Length - 1);
                string result1 = Spell.MakeSpellCode(FirstName, SpellOptions.EnableUnicodeLetter);
                string result2 = Spell.MakeSpellCode(LastName, SpellOptions.EnableUnicodeLetter);
                Response.Write("{\"success\":\"" + result1 + "/" + result2 + "\"}");
            }
            else
            {
                Response.Write("{\"success\":\"" + CnName  + "\"}");
            }
            
        }

        public void OrderLogInfo()
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_OrderLog where OrderId='{0}' order by LogTime", Request.QueryString["OrderId"]);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul id=logul>");
                Strings.Append("<li>订单状态记录</li>");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li class=logtit>{0}</li><li>{1}</li>", DS.Tables[0].Rows[i]["LogTime"].ToString(), DS.Tables[0].Rows[i]["LogContent"].ToString()));
                }
                Strings.Append("</ul>");
            }
            Response.Write("{\"success\":\"" + Strings.ToString() + "\"}");
        }

        protected void TempOrder()
        {
            if (Convert.ToString(ConfigurationManager.AppSettings["pdyh2000"]).IndexOf(Request.QueryString["planid"]) > -1)
            {
                if (MyConvert.ConToInt(Request.QueryString["nums"]) > 2)
                {
                    Response.Write("{\"error\":\"浦发信用卡立减活动，最多只能报名2人！\"}");
                    Response.End();
                }
            }
            if (Convert.ToString(ConfigurationManager.AppSettings["pdyhQianZheng"]).IndexOf(Request.QueryString["planid"]) > -1)
            {
                if (MyConvert.ConToInt(Request.QueryString["nums"]) > 1)
                {
                    Response.Write("{\"error\":\"浦发信用卡签证活动，最多只能报名1人！\"}");
                    Response.End();
                }
            }
            PurchaseClass.LineClass LineInfos = new PurchaseClass.LineClass();
            LineInfos = PurchaseClass.LineDetail(Request.QueryString["lineid"]);
            if (LineInfos != null)
            {
                //var url = "AjaxService.aspx?action=TempOrder&TempOrderId=" + $('#TempOrderId').val() + "&begindate=" + $('#BeginDate').val() + "&planid=" + $('#PlanId').val() + "&lineid=" + $('#LineId').val() + "&nums=" + nums + "&adults=" + Number($('#AdultNums').val()) + "&childs=" + Number($('#ChildNums').val()) + "&r=" + Math.random();
            
                string SqlQueryText;
                SqlQueryText = string.Format("insert into OL_TempOrder (OrderId,ProductType,ProductClass,LineID,PlanId,LineName,BeginDate,OrderNums,Adults,Childs,OrderTime,OrderUser,DeptId,LineDays,RouteFlag,PlanNo,UserName) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",
                    Request.QueryString["TempOrderId"].Trim(),
                    LineInfos.LineType,
                    LineInfos.LinesClass,
                    LineInfos.LineId,
                    Request.QueryString["planid"],
                    LineInfos.LineName,
                    MyConvert.ConToDate(Request.QueryString["begindate"]),
                    Request.QueryString["nums"],
                    Request.QueryString["adults"],
                    Request.QueryString["childs"],
                    DateTime.Now.ToString(),
                    Convert.ToString(Session["Online_UserId"]),
                    LineInfos.Deptid,
                    LineInfos.LineDays,
                    Request.QueryString["routeflag"],
                    Request.QueryString["PlanNo"],
                    Convert.ToString(Session["Online_UserName"])
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"error\":\"报名失败，请稍后重试！\"}");
                }
            }
            else
            {
                Response.Write("{\"error\":\"报名失败，请稍后重试！\"}");
            }
            
        }

        protected void VisitEdit()
        {
            
            List<string> Sql = new List<string>();
            Sql.Add(string.Format("delete from OL_TempPrice where OrderId='{0}'", Request.QueryString["TempOrderId"]));

            string[] AllInfo = Regex.Split(Request.Form["PriceStrings"].Trim(), @"\|\|", RegexOptions.IgnoreCase); //Request.QueryString["PriceStrings"].Split("||".ToArray());
            string PriceSql;
            if (AllInfo.Length > 0)
            {
                for (int i = 0; i < AllInfo.Length; i++)
                {
                    string[] PriceInfo = Regex.Split(AllInfo[i], @"\@\@", RegexOptions.IgnoreCase);  //AllInfo[i].Split("@@".ToArray());
                    if (PriceInfo.Length > 0)
                    {
                        PriceSql = string.Format("insert into OL_TempPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                Request.QueryString["TempOrderId"],
                                PriceInfo[0],
                                PriceInfo[1],
                                PriceInfo[2],
                                PriceInfo[3],
                                PriceInfo[4],
                                PriceInfo[5],
                                PriceInfo[6],
                                DateTime.Now.ToString()
                        );
                        Sql.Add(PriceSql);
                    }
                }

                string[] SqlQueryText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"岸上观光失败，请稍后重试！\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"岸上观光失败，请稍后重试！\"})");
            }
        }



        protected void VisitNumsCheck(DataTable dt, string visitid, string visitname, string price, string nums)
        {
            DataRow[] drs = dt.Select("id=" + visitid);
            if (drs.Count() > 0)
            {
                foreach (DataRow dr in drs)
                {
                    int haveroom = Convert.ToInt32(dr["nums"].ToString()) - Convert.ToInt32(dr["orders"].ToString());

                    if (haveroom < MyConvert.ConToInt(nums))
                    {
                        Response.Write("({\"error\":\"您选择的岸上观光 " + dr["visitname"].ToString() + " 剩余数量不足，请重新选择\"})");//"/" + haveroom + "/" + MyConvert.ConToInt(nums) + 
                        Response.End();
                    }

                    if (dr["price"].ToString() != price)
                    {
                        Response.Write("({\"error\":\"您选择的岸上观光 " + dr["visitname"].ToString() + " 价格发生了变化，请刷新页面后重新选择\"})");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Write("({\"error\":\"您选择的岸上观光 " + visitname + " 不存在，请重新选择\"})");
                Response.End();
            }
        }

        protected void FirdtStep()
        {
            //string TempOrderid = MyDataBaseComm.getScalar("select OrderId from OL_TempOrder where OrderId='" + Request.QueryString["TempOrderId"] + "'");
            //if (TempOrderid.Length < 5)
            //{
            //    Response.Write("({\"error\":\"信息保存失败，订单已经提交！\"})");
            //    Response.End();
            //}
            Int32 ShipId = 0;
            string SqlQueryText, LineID="0";

            SqlQueryText = string.Format("select LineID,shipid,OrderNums,(select dinner from OL_Line where MisLineId=OL_TempOrder.LineID) as dinner from OL_TempOrder where OrderId='{0}'", Request.QueryString["TempOrderId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ShipId = MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString());
                LineID = DS.Tables[0].Rows[0]["LineID"].ToString();
                string dinner = DS.Tables[0].Rows[0]["dinner"].ToString();
                string OrderNums = DS.Tables[0].Rows[0]["OrderNums"].ToString();

                //检查用餐人数
                if (Request.Form["DinnerStrings"].Length > 1)
                {
                    if (Request.Form["DinnerStrings"].Split("@".ToCharArray())[0] != "3")
                    {
                        string Dtime1 = "", Dtime2 = "", Dtime3 = "", Dnum1 = "", Dnum2 = "", Dnum3 = "";
                        Dtime3 = Request.Form["DinnerStrings"].Split("@".ToCharArray())[1];
                        string[] Dtime = dinner.Split('|');
                        if (Dtime.Length > 1)
                        {

                            Dtime1 = Dtime[0].Split("@".ToCharArray())[0];
                            Dnum1 = Dtime[0].Split("@".ToCharArray())[1];
                            Dtime2 = Dtime[1].Split("@".ToCharArray())[0];
                            Dnum2 = Dtime[1].Split("@".ToCharArray())[1];
                            if (Dtime1 == Dtime3)
                            {
                                Dnum3 = Dnum1;
                            }
                            else
                            {
                                Dtime3 = Dtime2;
                                Dnum3 = Dnum2;
                            }

                            string DinnerNums = MyDataBaseComm.getScalar("select isnull(sum(OrderNums),0) from View_OrderDinnerTime where lineid='" + LineID + "' and ExtContent='" + Dtime3 + "' and OrderFlag in (0,1,2,3) ");
                            if (MyConvert.ConToInt(OrderNums) + MyConvert.ConToInt(DinnerNums) > MyConvert.ConToInt(Dnum3))
                            {
                                Response.Write("({\"error\":\"您选择的 " + Dtime3 + " 用餐时间剩余位置不足！\"})");
                                Response.End();
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败，订单已经提交！\"})");
                Response.End();
            }

            string rebate = "0";
            if (ShipId > 0)
            { 
                rebate = MyDataBaseComm.getScalar("select ISNULL(sum(AllRebate),0) from CR_RoomOrder where OrderId='" + Request.QueryString["TempOrderId"] + "'");
                SqlQueryText = string.Format("select * from View_CR_Visit where lineid='{0}'", LineID);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
            }
            
            List<string> Sql = new List<string>();
            string PayType, BranchId, Pre_Price, SumPre_Price, Nums;
            PayType = "2";
            BranchId = "0";
            Pre_Price = "0";
            Nums = "0";
            SumPre_Price = "0";
            string[] PayInfo = Regex.Split(Request.QueryString["PayType"], @"\@", RegexOptions.IgnoreCase);
            PayType = PayInfo[0];
            if (PayType == "2")
            {
                BranchId = PayInfo[1];
                if (LineID == "12509")
                {
                    Nums = PayInfo[2];
                    Pre_Price = PayInfo[3];
                    SumPre_Price = PayInfo[4];
                }
            }
            else
            {
                Nums = PayInfo[1];
                Pre_Price = PayInfo[2];
                SumPre_Price = PayInfo[3];
            }

            if (Request.QueryString["EditFlag"] == "Manage")
            {
                Sql.Add(string.Format("UPDATE OL_Order set Price='{0}',PayType='{1}',BranchId='{2}' where OrderId='{3}'", Request.QueryString["Price"], PayType, BranchId, Request.QueryString["TempOrderId"]));
            }
            else
            {
                Sql.Add(string.Format("UPDATE OL_TempOrder set Price='{0}',PayType='{1}',BranchId='{2}',rebate='{3}' where OrderId='{4}'", Request.QueryString["Price"], PayType, BranchId, rebate, Request.QueryString["TempOrderId"]));
            }
            Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", Request.QueryString["TempOrderId"]));

            string[] AllInfo = Regex.Split(Request.Form["PriceStrings"].Trim(), @"\|\|", RegexOptions.IgnoreCase); //Request.QueryString["PriceStrings"].Split("||".ToArray());
            string PriceSql;
            if (AllInfo.Length > 0)
            {
                for (int i = 0; i < AllInfo.Length; i++)
                {
                    string[] PriceInfo = Regex.Split(AllInfo[i], @"\@\@", RegexOptions.IgnoreCase);  //AllInfo[i].Split("@@".ToArray());
                    if (PriceInfo.Length > 0)
                    {
                        //检查观光人数
                        if (PriceInfo[0].ToString() == "ShipVisit")
                        {
                            VisitNumsCheck(DS.Tables[0], PriceInfo[1], PriceInfo[3], PriceInfo[4], PriceInfo[5]);
                        }
                        //检查结束
                    
                        PriceSql = string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                Request.QueryString["TempOrderId"],
                                PriceInfo[0],
                                PriceInfo[1],
                                PriceInfo[2],
                                PriceInfo[3],
                                PriceInfo[4],
                                PriceInfo[5],
                                PriceInfo[6],
                                DateTime.Now.ToString()
                        );
                        Sql.Add(PriceSql);
                    }
                }

                if (Pre_Price != "0")
                {
                    Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            Request.QueryString["TempOrderId"],
                            "Preference",
                            "0",
                            "在线支付优惠",
                            "每人立减" + Pre_Price + "元",
                            Pre_Price,
                            Nums,
                            SumPre_Price,
                            DateTime.Now.ToString()
                    ));
                }

                if (Request.Form["DinnerStrings"].Length > 1)
                {
                    Sql.Add(string.Format("delete from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='{0}'", Request.QueryString["TempOrderId"]));
                    Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["TempOrderId"], "CruisesDinner", Request.Form["DinnerStrings"].Split("@".ToCharArray())[0], Request.Form["DinnerStrings"].Split("@".ToCharArray())[1], DateTime.Now.ToString()));
                }

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"订单保存失败，请稍后重试！\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"订单保存失败，请稍后重试！\"})");
            }
        }

        protected void SecondStep()
        {
            string TempOrderid = MyDataBaseComm.getScalar("select OrderId from OL_TempOrder where OrderId='" + Request.QueryString["TempOrderId"] + "'");
            if (TempOrderid.ToString().Length < 5)
            {
                Response.Write("({\"error\":\"信息保存失败，订单已经提交！\"})");
                Response.End();
            }
            
            List<string> Sql = new List<string>();
                     
            Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", Request.QueryString["TempOrderId"]));
            Sql.Add(string.Format("delete from OL_OrderExtend where ExtType<>'CruisesDinner' and OrderId='{0}'", Request.QueryString["TempOrderId"]));

            string[] AllInfo = Regex.Split(Request.Form["GuestInfo"].Trim(), @"\|\|", RegexOptions.IgnoreCase); 
            string PriceSql;
            if (AllInfo.Length > 0)
            {
                //邮轮预定，客人顺序号保存到房间明细表
                if (Request.Form["CruisesFlag"] == "1")
                {
                    string[] rooms = Request.Form["RoomListInfos"].Split('@');
                    string[] peoples = Request.Form["PeopleListInfos"].Split('@');
                    string GuestNo = "";
                    int berth = 0;
                    int sort = 1;
                    
                    for (int i = 0; i < rooms.Length - 1; i++)
                    {
                        GuestNo = "";
                        berth = MyConvert.ConToInt(rooms[i].Split("|".ToCharArray())[1]);
                        
                        for (int ii = 0; ii < berth; ii++)
                        {
                            GuestNo += sort.ToString() + ",";
                            sort += 1;
                        }
                        Sql.Add(string.Format("UPDATE CR_RoomList set guestids='{1}',adults='{2}',childs='{3}',BedType='{4}' where id='{0}'",
                            rooms[i].Split("|".ToCharArray())[0],
                            "0," + GuestNo + "0",
                            MyConvert.ConToInt(peoples[i].Split("|".ToCharArray())[0]),
                            MyConvert.ConToInt(peoples[i].Split("|".ToCharArray())[1]),
                            rooms[i].Split("|".ToCharArray())[2]
                        ));
                    }
                }

                string IdType = "", IdentityCard = "";
                for (int i = 0; i < AllInfo.Length; i++)
                {
                    
                    string[] PriceInfo = Regex.Split(AllInfo[i], @"\@\@", RegexOptions.IgnoreCase);
                    if (PriceInfo.Length > 0)
                    {
                        IdType = PriceInfo[3];
                        IdentityCard = "";
                        if (PriceInfo[4].ToString().Length == 15 || PriceInfo[4].ToString().Length == 18)
                        { 
                            IdType = "1";
                            IdentityCard = PriceInfo[4];
                        }


                        PriceSql = string.Format("insert into OL_GuestInfo (OrderId,GuestName,GuestEnName,Sex,IdType,IdNumber,BirthDay,PassType,PassBgn,PassEnd,Sign,Home,Tel,allotid,roomid,listid,rankno,IdentityCard) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                                Request.QueryString["TempOrderId"],
                                PriceInfo[0],
                                PriceInfo[1],
                                PriceInfo[2],
                                IdType,
                                PriceInfo[4],
                                MyConvert.ConToDate(PriceInfo[5]),
                                PriceInfo[6],
                                MyConvert.ConToDate(PriceInfo[7]),
                                MyConvert.ConToDate(PriceInfo[8]),
                                PriceInfo[9],
                                PriceInfo[10],
                                PriceInfo[11],
                                PriceInfo[12],
                                PriceInfo[13],
                                PriceInfo[14],
                                i+1,
                                IdentityCard
                        );
                        Sql.Add(PriceSql);
                    }
                }

                string htid = "0";
                string fpid = "0";
                string[] HT = Regex.Split(Request.QueryString["HT"], @"\@\@", RegexOptions.IgnoreCase);
                if (HT.Length > 0)
                {
                    htid = HT[0];
                    Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["TempOrderId"], "contract", HT[0], HT[1], DateTime.Now.ToString()));
                }

                if (Request.QueryString["NFP"] == "1")
                { 
                    string[] FP = Regex.Split(Request.QueryString["FP"], @"\@\@", RegexOptions.IgnoreCase);
                    if (FP.Length > 0)
                    {
                        fpid = FP[0];
                        Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["TempOrderId"], "invoice", FP[0], FP[1], DateTime.Now.ToString()));
                    }
                }

                string[] OrderInfo = Regex.Split(Request.Form["OrderInfos"].Trim(), @"\@\@", RegexOptions.IgnoreCase);
                string DbName = "OL_TempOrder";
                if (Request.QueryString["EditFlag"] == "Manage") DbName = "OL_Order";
                
                if (OrderInfo.Length > 0)
                {
                    Sql.Add(string.Format("UPDATE {9} set OrderName='{1}',OrderMobile='{2}',OrderTel='{3}',OrderFax='{4}',OrderEmail='{5}',OrderMemo='{6}',Contract='{7}',Invoice='{8}' where OrderId='{0}'",
                        Request.QueryString["TempOrderId"],
                        OrderInfo[0],
                        OrderInfo[1],
                        OrderInfo[2],
                        OrderInfo[3],
                        OrderInfo[4],
                        OrderInfo[5],
                        htid,
                        fpid,
                        DbName
                    ));
                }

                if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserDept"])) == 0)
                {
                    if (Request.QueryString["SaveFlag"] == "1")
                    {
                        Sql.Add(string.Format("UPDATE OL_LoginUser set UserName='{1}',Mobile='{2}',Tel='{3}' where id='{0}'",
                            Convert.ToString(Session["Online_UserId"]),
                            OrderInfo[0],
                            OrderInfo[1],
                            OrderInfo[2]
                            )
                        );
                    }
                }
                
                string[] SqlQueryText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"订单保存失败，请稍后重试！\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"订单保存失败，请稍后重试！\"})");
            }
        }

        protected void ThirdStep()
        {
            string TempOrderid = MyDataBaseComm.getScalar("select OrderId from OL_TempOrder where OrderId='" + Request.QueryString["TempOrderId"] + "'");
            if (TempOrderid.ToString().Length < 5)
            {
                Response.Write("{\"success\":1}");
                Response.End();
            }
            int CouponId = 0;
            int CouponNums = 0;
            int AutoId;
            string CouponTicketId = "";
            string SqlQueryText;
            SqlQueryText = "select *,(select PriceId from OL_OrderPrice where OrderId=OL_TempOrder.OrderId and PriceType='Coupon') as CouponId,";
            SqlQueryText += "(select OrderNums from OL_OrderPrice where OrderId=OL_TempOrder.OrderId and PriceType='Coupon') as CouponNums,";
            SqlQueryText += "(select misid from OL_LoginUser where Id=OL_TempOrder.OrderUser) as auser,";
            SqlQueryText += "(select misid from DeptInfo where id=OL_TempOrder.orderdept) as adept,";
            SqlQueryText += "(select misid from Company where id=OL_TempOrder.ordercompany) as acom";
            SqlQueryText += string.Format(" from OL_TempOrder where OrderId='{0}'", Request.QueryString["TempOrderId"]);
            string RouteFlag = "";
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                AutoId = MyConvert.ConToInt(DS.Tables[0].Rows[0]["AutoId"].ToString());
                CouponId = MyConvert.ConToInt(DS.Tables[0].Rows[0]["CouponId"].ToString());
                CouponNums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["CouponNums"].ToString());
                if (CouponId > 0)
                {
                    SqlQueryText = string.Format("select top {3} id from Pre_Ticket where pid='{0}' and userid='{1}' and flag='0' and begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}'", CouponId, Convert.ToString(Session["Online_UserId"]), DateTime.Today, CouponNums);

                    DataSet Coupon = new DataSet();
                    Coupon.Clear();
                    Coupon = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (Coupon.Tables[0].Rows.Count > 0)
                    {
                        if (Coupon.Tables[0].Rows.Count < CouponNums)
                        {
                            Response.Write("{\"success\":2,\"info\":\"您选择的优惠券数量不足，只有" + Coupon.Tables[0].Rows.Count + "份可使用，不能提交订单\"}");
                            Response.End();
                        }
                        else
                        {
                            for (int i = 0; i < Coupon.Tables[0].Rows.Count; i++)
                            {
                                CouponTicketId += Coupon.Tables[0].Rows[i]["id"].ToString() + ",";
                                
                            }
                            CouponTicketId += "0";
                        }
                        //Response.Write("{\"success\":2,\"info\":\"可用" + CouponTicketId + "\"}");
                    }
                    else
                    {
                        Response.Write("{\"success\":2,\"info\":\"您选择的优惠券已使用或已过期，不能提交订单\"}");
                        Response.End();
                    }
                    //Response.Write("{\"success\":2,\"info\":\"" + DS.Tables[0].Rows[0]["CouponNums"].ToString() + "\"}");

                }

                //邮轮优惠券
                string Policy = Request.QueryString["Policy"];
                string PolicyUid = "";
                decimal PolicyNum = 0, PolicyGather = 0, PolicyPar = 0;
                string PolicyRoom = "";
                if (Policy.Length > 3)
                {
                    //string SqlQueryText = string.Format("select id,uno,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo from Pre_Ticket where begindate<='{2:yyyy-MM-dd}' and enddate>='{2:yyyy-MM-dd}' and flag='0' and sellflag='4' and userid='{1}' and pre_no in (select allotid from CR_RoomOrder where OrderId='{0}')", Request.QueryString["TempOrderId"], Convert.ToString(Session["Online_UserId"]), DateTime.Today);
                    //string pre_allotid = "";
                    string SqlText = string.Format("select Pre_Ticket.id,Pre_Ticket.uid,Pre_Ticket.uno,Pre_Ticket.par,Pre_Ticket.deduction,CR_RoomOrder.peoples,CR_RoomOrder.rooms,CR_RoomOrder.roomname from Pre_Ticket,CR_RoomOrder where Pre_Ticket.begindate<='{3:yyyy-MM-dd}' and Pre_Ticket.enddate>='{3:yyyy-MM-dd}' and Pre_Ticket.pre_no=CR_RoomOrder.allotid and Pre_Ticket.flag='0' and CR_RoomOrder.orderid='{0}' and Pre_Ticket.sellflag in (4,5) and Pre_Ticket.userid='{1}' and Pre_Ticket.uno='{2}'", Request.QueryString["TempOrderId"], Convert.ToString(Session["Online_UserId"]), Policy, DateTime.Today);
                    DataSet DS2 = new DataSet();
                    DS2.Clear();
                    DS2 = MyDataBaseComm.getDataSet(SqlText);
                    if (DS2.Tables[0].Rows.Count > 0)
                    {
                        PolicyUid = DS2.Tables[0].Rows[0]["uid"].ToString();
                        PolicyPar = MyConvert.ConToDec(DS2.Tables[0].Rows[0]["par"].ToString());
                        PolicyRoom = DS2.Tables[0].Rows[0]["roomname"].ToString();
                        if (DS2.Tables[0].Rows[0]["deduction"].ToString() == "1")
                        {
                            PolicyNum = 1;
                            PolicyGather = PolicyPar;
                        }

                        if (DS2.Tables[0].Rows[0]["deduction"].ToString() == "2")
                        {
                            PolicyNum = MyConvert.ConToDec(DS2.Tables[0].Rows[0]["peoples"].ToString());
                            PolicyGather = PolicyPar * PolicyNum;
                        }

                        if (DS2.Tables[0].Rows[0]["deduction"].ToString() == "3")
                        {
                            PolicyNum = MyConvert.ConToDec(DS2.Tables[0].Rows[0]["rooms"].ToString());
                            PolicyGather = PolicyPar * PolicyNum;
                        }
                    }
                    else
                    {
                        SqlText = string.Format("select *,(select count(id) from Pre_Ticket where pid=Pre_Policy.id) as nums from Pre_Policy where pre_no='{0}' and begindate<='{1:yyyy-MM-dd}' and enddate>='{1:yyyy-MM-dd}'", Policy, DateTime.Today);
                        DS2.Clear();
                        DS2 = MyDataBaseComm.getDataSet(SqlText);
                        if (DS2.Tables[0].Rows.Count > 0)
                        {
                            string DoFlag = "0";
                            if (DS2.Tables[0].Rows[0]["range"].ToString() == "5")
                            {
                                DoFlag = "1";
                            }

                            if (DS2.Tables[0].Rows[0]["range"].ToString() == "1")
                            {
                                DoFlag = "1";
                            }

                            if (DS2.Tables[0].Rows[0]["range"].ToString() == "9")
                            {
                                if (DS2.Tables[0].Rows[0]["product"].ToString().IndexOf(DS.Tables[0].Rows[0]["LineID"].ToString()) > -1)
                                {
                                    DoFlag = "1";
                                }
                            }
                            if (MyConvert.ConToDec(DS2.Tables[0].Rows[0]["sellnums"].ToString()) <= MyConvert.ConToDec(DS2.Tables[0].Rows[0]["nums"].ToString()))
                            {
                                DoFlag = "0";
                                PolicyPar = 0;
                            }

                            if (DoFlag == "1")
                            {
                                PolicyPar = MyConvert.ConToDec(DS2.Tables[0].Rows[0]["par"].ToString());
                                string deduction = DS2.Tables[0].Rows[0]["deduction"].ToString();

                                SqlText = string.Format("select sum(peoples) as peoples,sum(rooms) as rooms from CR_RoomOrder where CR_RoomOrder.orderid='{0}'", Request.QueryString["TempOrderId"]);
                                DataSet DS3 = new DataSet();
                                DS3.Clear();
                                DS3 = MyDataBaseComm.getDataSet(SqlText);
                                if (DS3.Tables[0].Rows.Count > 0)
                                {
                                    PolicyRoom = "";
                                    if (deduction == "1")
                                    {
                                        PolicyNum = 1;
                                        PolicyGather = PolicyPar;
                                    }

                                    if (deduction == "2")
                                    {
                                        PolicyNum = MyConvert.ConToDec(DS3.Tables[0].Rows[0]["peoples"].ToString());
                                        PolicyGather = PolicyPar * PolicyNum;
                                    }

                                    if (deduction == "3")
                                    {
                                        PolicyNum = MyConvert.ConToDec(DS3.Tables[0].Rows[0]["rooms"].ToString());
                                        PolicyGather = PolicyPar * PolicyNum;
                                    }

                                    PolicyUid = Convert.ToString(CombineKeys.NewComb());
                                    SqlQueryText = string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag,pre_no) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                                        DS2.Tables[0].Rows[0]["id"].ToString(),
                                        PolicyUid,
                                        Policy,
                                        DS2.Tables[0].Rows[0]["par"].ToString(),
                                        DS2.Tables[0].Rows[0]["amount"].ToString(),
                                        DS.Tables[0].Rows[0]["OrderUser"].ToString(),
                                        DS2.Tables[0].Rows[0]["begindate"].ToString(),
                                        DS2.Tables[0].Rows[0]["enddate"].ToString(),
                                        DateTime.Now.ToString(),
                                        "0",
                                        DS2.Tables[0].Rows[0]["deduction"].ToString(),
                                        DS2.Tables[0].Rows[0]["range"].ToString(),
                                        DS2.Tables[0].Rows[0]["product"].ToString(),
                                        DS.Tables[0].Rows[0]["OrderEmail"].ToString(),
                                        DS.Tables[0].Rows[0]["UserName"].ToString(),
                                        DS2.Tables[0].Rows[0]["pbdate"].ToString(),
                                        DS2.Tables[0].Rows[0]["pedate"].ToString(),
                                        DS2.Tables[0].Rows[0]["sellflag"].ToString(),
                                        DS2.Tables[0].Rows[0]["pre_no"].ToString()
                                    );
                                    MyDataBaseComm.ExcuteSql(SqlQueryText);
                                }
                                else
                                {
                                    PolicyPar = 0;
                                }
                            
                            }
                        }
                    }

                    if (PolicyPar == 0)
                    {
                        Response.Write("{\"success\":2,\"info\":\"您选择的优惠券不能使用，不能提交订单\"}");
                        Response.End();
                    }
                    
                }

                //Response.Write("{\"success\":2,\"info\":\"您选择的优惠券不能使用，" + PolicyPar + "\"}");
                //Response.End();
                //else
                //{
                //    Response.Write("{\"success\":2,\"info\":\"没用优惠券\"}");
                //    Response.End();
                //}
                //Response.End();
                //Response.Write("{\"success\":2,\"info\":\"" + PolicyNum + " ：" + PolicyGather + "\"}");
                //Response.End();
                string OrderFlag = "0";//预订状态，不占位订单和无位置订单为0，畅游占位成功为1，提交错误返回9
                RouteFlag = DS.Tables[0].Rows[0]["RouteFlag"].ToString();//行程标志，是否取线路的或者计划有自己行程，为0时与planid同时判断

                string yyyyMM = string.Format("{0:yyyy-MM}", DateTime.Today);
                string Directory = string.Format(@"{0}XML\OrderRoute\{1}", AppDomain.CurrentDomain.BaseDirectory, yyyyMM);

                if (!System.IO.Directory.Exists(Directory))
                {
                    System.IO.Directory.CreateDirectory(Directory);
                }

                string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                TravelOnlineService rsp = new TravelOnlineService();
                rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

                if (RouteFlag == "0")
                {
                    LineInfos UpLoadInfo = new LineInfos();

                    try
                    {
                        StringBuilder Stings = new StringBuilder();
                        UpLoadInfo = rsp.GetPlanRoute(UpPassWord, DS.Tables[0].Rows[0]["PlanId"].ToString());

                        //行程Xml生成
                        if (UpLoadInfo.RouteDetail != null)
                        {
                            Stings.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                            Stings.Append("<Route>");
                            Stings.Append(string.Format("<Feature>{0}</Feature>", UpLoadInfo.RouteServices.Feature));
                            Stings.Append(string.Format("<Attentions>{0}</Attentions>", UpLoadInfo.RouteServices.Attentions));
                            Stings.Append(string.Format("<PriceIn>{0}</PriceIn>", UpLoadInfo.RouteServices.PriceIn));
                            Stings.Append(string.Format("<PriceOut>{0}</PriceOut>", UpLoadInfo.RouteServices.PriceOut));
                            Stings.Append(string.Format("<OwnExpense>{0}</OwnExpense>", UpLoadInfo.RouteServices.OwnExpense));
                            Stings.Append(string.Format("<Shopping>{0}</Shopping>", UpLoadInfo.RouteServices.Shopping));
                            Stings.Append(string.Format("<TravelAgency>{0}</TravelAgency>", UpLoadInfo.RouteServices.TravelAgency));
                            Stings.Append(string.Format("<Scenery>{0}</Scenery>", UpLoadInfo.RouteServices.Scenery));
                            Stings.Append(string.Format("<Traffic>{0}</Traffic>", UpLoadInfo.RouteServices.Traffic));
                            Stings.Append(string.Format("<Hotel>{0}</Hotel>", UpLoadInfo.RouteServices.Hotel));
                            Stings.Append(string.Format("<Foods>{0}</Foods>", UpLoadInfo.RouteServices.Foods));
                            Stings.Append(string.Format("<Guide>{0}</Guide>", UpLoadInfo.RouteServices.Guide));
                            Stings.Append(string.Format("<Insure>{0}</Insure>", UpLoadInfo.RouteServices.Insure));
                            Stings.Append(string.Format("<Others>{0}</Others>", UpLoadInfo.RouteServices.Others));
                            Stings.Append("<RouteDetail>");

                            for (int i = 0; i < UpLoadInfo.RouteDetail.Length; i++)
                            {
                                Stings.Append("<RouteInfos>");
                                Stings.Append(string.Format("<daterank>{0}</daterank>", UpLoadInfo.RouteDetail[i].daterank));
                                Stings.Append(string.Format("<rname>{0}</rname>", UpLoadInfo.RouteDetail[i].rname));
                                Stings.Append(string.Format("<route>{0}</route>", UpLoadInfo.RouteDetail[i].route));
                                Stings.Append(string.Format("<dinner>{0}</dinner>", UpLoadInfo.RouteDetail[i].dinner));
                                Stings.Append(string.Format("<bus>{0}</bus>", UpLoadInfo.RouteDetail[i].bus));
                                Stings.Append(string.Format("<room>{0}</room>", UpLoadInfo.RouteDetail[i].room));
                                Stings.Append(string.Format("<Pics>{0}</Pics>", UpLoadInfo.RouteDetail[i].Pics));
                                Stings.Append("</RouteInfos>");
                            }
                            Stings.Append("</RouteDetail>");
                            Stings.Append("</Route>");
                            Stings.Append("");
                            string RouteUrl = string.Format(@"OrderRoute\{0}", yyyyMM);
                            SaveScriptToFile.SaveXml(Stings.ToString(), RouteUrl, DS.Tables[0].Rows[0]["OrderId"].ToString());
                        }
                    }
                    catch
                    { }

                }
                else //保存线路行程
                {
                    string path = string.Format(@"{0}XML\Route\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, DS.Tables[0].Rows[0]["LineID"].ToString());
                    if (System.IO.File.Exists(path) == true)
                    {
                        string topath = string.Format(@"{0}\{1}.xml", Directory, DS.Tables[0].Rows[0]["OrderId"].ToString());
                        System.IO.File.Copy(path, topath, true);
                    }
                }

                OrderInfos Sorder = new OrderInfos();
                Sorder.adult = DS.Tables[0].Rows[0]["Adults"].ToString();
                Sorder.begindate = string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]); //string.Format("{0:yyyy-MM-dd}", DS.Tables[0].Rows[0]["BeginDate"]);
                Sorder.childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Sorder.days = DS.Tables[0].Rows[0]["LineDays"].ToString();
                Sorder.deptid = DS.Tables[0].Rows[0]["DeptId"].ToString();
                Sorder.email = DS.Tables[0].Rows[0]["OrderEmail"].ToString();

                decimal OrderGathering = MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString());

                string RebateFlag = Request.QueryString["PayType"];
                if (RebateFlag == "1")
                {
                    //Sorder.gathering = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["rebate"].ToString())).ToString();
                    OrderGathering = OrderGathering - MyConvert.ConToDec(DS.Tables[0].Rows[0]["rebate"].ToString());
                }

                if (PolicyGather > 0)
                {
                    OrderGathering = OrderGathering - PolicyGather;
                }

                Sorder.gathering = OrderGathering.ToString();
                //switch (DS.Tables[0].Rows[0]["ProductType"].ToString())
                //{
                //    case "FreeTour":
                //        Sorder.infoid = "852";
                //        break;
                //    case "Cruises":
                //        Sorder.infoid = "753";
                //        break;
                //    default:
                //        Sorder.infoid = DS.Tables[0].Rows[0]["ProductClass"].ToString();
                //        break;
                //}
                Sorder.infoid = DS.Tables[0].Rows[0]["ProductClass"].ToString();

                Sorder.lineid = DS.Tables[0].Rows[0]["LineID"].ToString();
                Sorder.linename = DS.Tables[0].Rows[0]["LineName"].ToString();
                Sorder.mobile = DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                Sorder.orderdate = DateTime.Now.ToString(); //DS.Tables[0].Rows[0]["OrderTime"].ToString();
                Sorder.orderflag = DS.Tables[0].Rows[0]["OrderFlag"].ToString();
                Sorder.orderid = DS.Tables[0].Rows[0]["OrderId"].ToString();
                Sorder.ordermemo = DS.Tables[0].Rows[0]["OrderMemo"].ToString();
                Sorder.ordername = DS.Tables[0].Rows[0]["OrderName"].ToString();
                Sorder.ordernumber = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Sorder.planid = DS.Tables[0].Rows[0]["PlanId"].ToString();
                Sorder.tel = DS.Tables[0].Rows[0]["OrderTel"].ToString();
                Sorder.orderno = DS.Tables[0].Rows[0]["AutoId"].ToString();
                Sorder.contract = DS.Tables[0].Rows[0]["Contract"].ToString();
                Sorder.invoice = DS.Tables[0].Rows[0]["Invoice"].ToString();
                Sorder.SellDept = DS.Tables[0].Rows[0]["BranchId"].ToString();

                Sorder.CruisesFlag = "0";
                Sorder.ccid = "0";
                Sorder.acom = DS.Tables[0].Rows[0]["acom"].ToString();
                Sorder.adept = DS.Tables[0].Rows[0]["adept"].ToString();
                Sorder.SellUser = DS.Tables[0].Rows[0]["auser"].ToString(); //"auser";

                //公司id为1，表示内部部门预定
                if (DS.Tables[0].Rows[0]["ordercompany"].ToString() == "1")
                {
                    Sorder.SellDept = DS.Tables[0].Rows[0]["adept"].ToString();
                    Sorder.acom = "0";
                    Sorder.adept = "0";
                }
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["shipid"].ToString()) > 0)
                {
                    Sorder.CruisesFlag = "1";
                    Sorder.ccid = DS.Tables[0].Rows[0]["ccid"].ToString();
                }

                string OrderId = DS.Tables[0].Rows[0]["OrderId"].ToString();
                Sorder.ordertypes = DS.Tables[0].Rows[0]["ProductType"].ToString();


                //if (DS.Tables[0].Rows[0]["ProductType"].ToString() == "Cruises")
                //{
                //    DataSet DS1 = new DataSet();
                //    DS1.Clear();
                //    SqlQueryText = string.Format("select * from OL_CuisesRoom where OrderId='{0}'", OrderId);
                //    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                //    if (DS1.Tables[0].Rows.Count > 0)
                //    {
                //        Sorder.CruiseOrderRooms = new CruiseOrderRoom[DS1.Tables[0].Rows.Count];
                //        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                //        {
                //            Sorder.CruiseOrderRooms[i] = new CruiseOrderRoom();
                //            Sorder.CruiseOrderRooms[i].priceid = DS1.Tables[0].Rows[i]["PriceId"].ToString();
                //            Sorder.CruiseOrderRooms[i].rooms = DS1.Tables[0].Rows[i]["RoomNum"].ToString();
                //            Sorder.CruiseOrderRooms[i].nums = DS1.Tables[0].Rows[i]["OrderNum"].ToString();
                //            Sorder.CruiseOrderRooms[i].adults = DS1.Tables[0].Rows[i]["AdultNum"].ToString();
                //            Sorder.CruiseOrderRooms[i].childs = DS1.Tables[0].Rows[i]["ChildNum"].ToString();
                //            //PriceId += "," + DS.Tables[0].Rows[a]["PriceId"].ToString();
                //        }
                //    }
                //}

                //string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
                //TravelOnlineService rsp = new TravelOnlineService();
                //rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
                try
                {
                    OrderFlag = rsp.SaveOrder(UpPassWord, Sorder);
                    //自由行和邮轮不占位方式取消 20111018
                    //switch (DS.Tables[0].Rows[0]["ProductType"].ToString())
                    //{
                    //    case "FreeTour":
                    //        OrderFlag = "0";
                    //        break;
                    //    case "Cruises":
                    //        OrderFlag = "0";
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                catch
                {
                    OrderFlag = "9";
                }

                decimal Integral = MyConvert.ConToDec(Request.QueryString["Integral"]);
                if (Integral > MyConvert.ConToDec(Sorder.gathering)) Integral = MyConvert.ConToDec(Sorder.gathering);

                string Allintegral = "0";
                if (Integral > 0)
                {
                    Allintegral = MyDataBaseComm.getScalar("select isnull(sum(integral),0) from OL_Integral where uid='" + Convert.ToString(Session["Online_UserId"]) + "'");
                    if (Integral > MyConvert.ConToDec(Allintegral)) Integral = MyConvert.ConToDec(Allintegral);
                }

                

                //Response.Write(Integral.ToString() + " / " + Sorder.lineid + " / " + Sorder.gathering);
                //Response.End();

                List<string> Sql = new List<string>();

                if (OrderFlag == "9")
                {
                    Sql.Add(string.Format("update OL_TempOrder set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), RebateFlag, Integral));
                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您暂存了预订单"));
                    if (Integral > 0)
                    {
                        Sql.Add(string.Format("insert into OL_Integral (uid,orderid,lineid,integral,getdate,flag,dept,enddate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            Convert.ToString(Session["Online_UserId"]),
                            OrderId,
                            Sorder.lineid,
                            "-" + Integral.ToString(),
                            DateTime.Today,
                            "1",
                            Sorder.deptid,
                            DateTime.Today
                        ));

                        Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            OrderId,
                            "Integral",
                            "0",
                            "积分抵扣",
                            "使用积分抵扣团费",
                            "-" + Integral.ToString(),
                            "1",
                            "-" + Integral.ToString(),
                            DateTime.Now.ToString()
                        ));
                    }
                }
                else
                {
                    Sql.Add(string.Format("INSERT INTO OL_Order (OrderId, ProductType, ProductClass, LineID, PlanId, LineName, BeginDate, OrderNums, Adults, Childs, Price, OrderName, OrderEmail, OrderMobile, OrderTel,OrderFax, OrderMemo, OrderTime, OrderUser, DeptId, OrderFlag, Contract, Invoice, AutoId, LineDays, PayFlag, RouteFlag, PlanNo,PayType,BranchId,shipid,orderdept,ordercompany,ProductNum,rebate,UserName,ccid,RebateFlag,allmdjs,ota) SELECT * FROM OL_TempOrder WHERE OrderId='{0}'", OrderId));
                    Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), "您提交了预订单"));
                    Sql.Add(string.Format("delete from OL_TempOrder where OrderId='{0}'", OrderId));
                    Sql.Add(string.Format("update OL_Order set PayFlag='0',OrderFlag='{0}',OrderTime='{2}',RebateFlag='{3}',Price=Price-{4}-{5} where OrderId='{1}'", OrderFlag, OrderId, DateTime.Now.ToString(), RebateFlag, Integral, PolicyGather));
                    if (CouponId > 0) Sql.Add(string.Format("update Pre_Ticket set flag='1',usedate='{2}',AutoId='{4}',OrderId='{5}' where pid='{0}' and userid='{1}' and flag='0' and id in ({3})", CouponId, Convert.ToString(Session["Online_UserId"]), DateTime.Now.ToString(), CouponTicketId, AutoId, OrderId));
                    if (Integral > 0)
                    {
                        Sql.Add(string.Format("insert into OL_Integral (uid,orderid,lineid,integral,getdate,flag,dept,enddate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            Convert.ToString(Session["Online_UserId"]),
                            OrderId,
                            Sorder.lineid,
                            "-" + Integral.ToString(),
                            DateTime.Today,
                            "1",
                            Sorder.deptid,
                            DateTime.Today
                        ));

                        Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            OrderId,
                            "Integral",
                            "0",
                            "积分抵扣",
                            "使用积分抵扣团费",
                            "-" + Integral.ToString(),
                            "1",
                            "-" + Integral.ToString(),
                            DateTime.Now.ToString()
                        ));
                    }
                    
                    if (PolicyGather > 0)
                    {
                        Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                OrderId,
                                "Policy",
                                "0",
                                "优惠券",
                                "使用邮轮优惠券抵扣 " + PolicyRoom,
                                "-" + PolicyPar.ToString(),
                                PolicyNum,
                                "-" + PolicyGather.ToString(),
                                DateTime.Now.ToString()
                            ));

                        Sql.Add(string.Format("update Pre_Ticket set Flag='1',AutoId='{1}',OrderId='{2}',usedate='{3}' where Uid='{0}'", PolicyUid, AutoId, OrderId, DateTime.Today));
                    
                    }
                }

                string[] SqlQuery = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuery) == true)
                {
                    if (OrderFlag == "9")
                    {
                        Response.Write("{\"success\":9}");
                    }
                    else
                    {
                        if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1)
                        {
                            //清空呼叫中心订单id
                            HttpCookie cookie = default(HttpCookie);
                            cookie = new HttpCookie("CallCenterOrderId", "");
                            cookie.Expires = DateTime.Now.AddDays(-10);
                            Response.Cookies.Add(cookie);
                        }

                        if (Request.Cookies["XiRong"] != null)
                        {
                            string XiRong_Lineid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["XiRong"].Value));
                            string XiRong_userid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["XiRongUser"].Value));
                            string XiRong_orderid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["XiRongOrder"].Value));
                            SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", Request.QueryString["TempOrderId"]);

                            DS.Clear();
                            DS = MyDataBaseComm.getDataSet(SqlQueryText);
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                SqlQueryText = string.Format("insert into XiRongOrder (xruserid,xrorderid,autoid,lineid,orderdate) values ('{0}','{1}','{2}','{3}','{4}')",
                                    MyConvert.ConToInt(XiRong_userid),
                                    MyConvert.ConToInt(XiRong_orderid),
                                    DS.Tables[0].Rows[0]["AutoId"].ToString(),
                                    XiRong_Lineid,
                                    DateTime.Now.ToString());
                                MyDataBaseComm.ExcuteSql(SqlQueryText);

                                if (Sorder.lineid == XiRong_Lineid)
                                {
                                    //访问喜荣网站，提交订单信息
                                    //
                                    string url = "";
                                    url = "is_success=1";
                                    url += "&notify_time=" + MyConvert.GetTimeStamp(true);
                                    url += "&total_fee=" + DS.Tables[0].Rows[0]["Price"].ToString();
                                    url += "&trade_no=" + DS.Tables[0].Rows[0]["AutoId"].ToString();
                                    url += "&out_trade_no=" + XiRong_orderid;
                                    //url += "&UserID=" + XiRong_userid;
                                    //url += "&UserAcc=" + DS.Tables[0].Rows[0]["OrderName"].ToString();
                                    string Sign = SecurityCode.Md5_Encrypt(url + "&key=scyts_xirong", 32).ToLower();
                                    //提交数据库
                                    url += "&Sign=" + Sign;

                                    string result = GetPageContent("http://www.sdxjr.com/order/scytscallback.aspx?"+ url, "");
                                    if (result == "success")
                                    {
                                        SqlQueryText = string.Format("update XiRongOrder set flag='1' where autoid='{0}'", DS.Tables[0].Rows[0]["AutoId"].ToString());
                                        MyDataBaseComm.ExcuteSql(SqlQueryText);
                                    }
                                    else
                                    {
                                        MyConvert.SaveErrorToLogFile("禧荣订单提交错误：" + result + "，回调地址：" + "http://www.sdxjr.com/order/scytscallback.aspx?" + url, "ErrorLog");
                                    }

                                    HttpCookie cookie = default(HttpCookie);
                                    cookie = new HttpCookie("XiRong", "");
                                    cookie.Expires = DateTime.Now.AddDays(-10);
                                    Response.Cookies.Add(cookie);

                                    cookie = new HttpCookie("XiRongUser", "");
                                    cookie.Expires = DateTime.Now.AddDays(-10);
                                    Response.Cookies.Add(cookie);

                                    cookie = new HttpCookie("XiRongOrder", "");
                                    cookie.Expires = DateTime.Now.AddDays(-10);
                                    Response.Cookies.Add(cookie);
                                }

                            }
                        }
                        Response.Write("{\"success\":0}");
                    }
                }
                else
                {
                    Response.Write("{\"success\":1}");
                }
            }
            else
            {
                Response.Write("{\"success\":1}");
            }        
        }

        


        private readonly static int TIMEOUT = 15000;
        private CookieContainer _cookieCon = new CookieContainer();
        private CredentialCache _credentials = new CredentialCache();

        /// <summary>
        /// 通过url请求数据
        /// </summary>
        /// <param >被请求页面的url</param>
        /// <param >代理服务器</param>

        /// <returns>返回页面内容</returns>
        public string GetPageContent(string url, string proxyServer)
        {
            StringBuilder ret = new StringBuilder("");
            HttpWebResponse rsp = null;

            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                if (!string.IsNullOrEmpty(proxyServer))
                {


                    req.Proxy = new WebProxy(proxyServer);
                }
                req.CookieContainer = this._cookieCon;
                req.Headers.Add("Accept-Language: zh-cn");
                req.AllowAutoRedirect = true;
                req.Timeout = TIMEOUT;

                if (this._credentials != null)
                {
                    req.PreAuthenticate = true;
                    req.Credentials = this._credentials;
                }
                rsp = (HttpWebResponse)req.GetResponse();

                Stream rspStream = rsp.GetResponseStream();
                StreamReader sr = new StreamReader(rspStream, System.Text.Encoding.Default);

                //获取数据
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    ret.Append(read, 0, count);
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {
                ret.Append(e.Message);
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return ret.ToString();
        }

        /// <summary>
        /// 通过url请求数据(Post方法)
        /// </summary>
        /// <param >被请求页面的url</param>
        /// <param >POST的内容</param>
        /// <param >代理</param>
        /// <returns>返回页面内容</returns>
        public string GetPageContent(string url, string param, string proxyServer)
        {
            StringBuilder ret = new StringBuilder("");
            HttpWebResponse rsp = null;

            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                if (!string.IsNullOrEmpty(proxyServer))
                {
                    req.Proxy = new WebProxy(proxyServer);
                }
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Add("Accept-Language: zh-cn");
                req.CookieContainer = _cookieCon;
                req.Timeout = TIMEOUT;
                req.AllowAutoRedirect = true;
                if (_credentials != null)
                {
                    req.PreAuthenticate = true;
                    req.Credentials = _credentials;
                }

                //传入POST参数的分析
                if (param != null)
                {
                    string temp = EncodeParams(param, System.Text.Encoding.Default);
                    byte[] bytes = Encoding.UTF8.GetBytes(temp);
                    req.ContentLength = bytes.Length;
                    Stream rspStream = req.GetRequestStream();
                    rspStream.Write(bytes, 0, bytes.Length);
                    rspStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }

                //取得请求后返回的的数据
                rsp = (HttpWebResponse)(req.GetResponse());
                Stream ReceiveStream = rsp.GetResponseStream();
                StreamReader sr = new StreamReader(ReceiveStream, System.Text.Encoding.Default);

                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    ret.Append(read, 0, count);
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
                rsp.Close();
            }
            finally
            {
                if (rsp != null)
                {
                    rsp.Close();
                }
            }
            return ret.ToString();
        }

        private string EncodeParams(string param, Encoding enc)
        {
            StringBuilder ret = new StringBuilder();
            char[] reserved = { '?', '=', '&', '%', '+' };

            if (param != null)
            {
                int i = 0, j;
                while (i < param.Length)
                {
                    j = param.IndexOfAny(reserved, i);
                    if (j == -1)
                    {
                        ret.Append(HttpUtility.UrlEncode(param.Substring(i, param.Length - i), enc));
                        break;
                    }
                    ret.Append(HttpUtility.UrlEncode(param.Substring(i, j - i), enc));
                    ret.Append(param.Substring(j, 1));
                    i = j + 1;
                }
            }
            return ret.ToString();
        }
    }
}