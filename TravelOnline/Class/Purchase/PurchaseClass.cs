using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Configuration;
using TravelOnline.TravelMisWebService;
using System.Text.RegularExpressions;
using TravelOnline.GetCombineKeys;
using TravelOnline.NewPage.erp;

namespace TravelOnline.Class.Purchase
{
    public class PurchaseClass
    {
        public class LineClass
        {
            public string LineId { get; set; }
            public string LineType { get; set; }
            public string LinesClass { get; set; }
            public string LineName { get; set; }
            public string LinePrice { get; set; }
            public string Deptid { get; set; }
            public string LineDays { get; set; }
            public string Planid { get; set; }
            public string Shipid { get; set; }
        }

        public class OrderExtendInfo
        {
            public string Contract { get; set; }
            public string Invoice { get; set; }
            public string Dinner { get; set; }
        }

        public class CruisesPlanInfo
        {
            public string CruisesPlanInfo_PlanList { get; set; }
            public string CruisesPlanInfo_RoomList { get; set; }
        }

        public static string GetContractType(int i)
        {
            string[] Contract = { "", "传真签约", "快递签约", "在线签约", "门店签约" };
            return Contract[i];
        }

        //从分团号批量取消游客
        public static string MisCruisesPlanNoCancel(string LineId, string GroupPlanId)
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            string Result = rsp.CruisesPlanNoCancel(UpPassWord, LineId, GroupPlanId);
            return Result;
        }

        //从游客分团号批量取消
        public static string MisCruisesGuestPlanNoCancel(string LineId, string GroupGuestId)
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            string Result = rsp.CruisesGuestPlanNoCancel(UpPassWord, LineId, GroupGuestId);
            return Result;
        }

        //游客分团号变更
        public static string MisCruisesPlanNoChange(string LineId, string GroupGuestId, string PlanId, string PlanNo)
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            string Result = rsp.CruisesPlanNoChange(UpPassWord, LineId, GroupGuestId, PlanId, PlanNo);
            return Result;
        }

        public static string CruisesOrderAdjust(string orderid, string flag, string SaveFlag)
        {
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";

            OrderInfos Sorder = new OrderInfos();
            string SqlQueryText, Result;
            Result = "";
            SqlQueryText = string.Format("select * from OL_Order where OrderId='{0}'", orderid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            string allprice = "0";
            string iweboutid = "";
            if (DS.Tables[0].Rows.Count > 0)
            {
                //orders.CruisesFlag 1,CancelCruisesRoom退舱房 2 AdjustCruisesRoom舱房变更 3 AdjustVisit观光调整 4 OrderRetreat取消订单 5 AdjustPrice费用变更 6 Settlement 结算方式变更
                Sorder.ordernumber = DS.Tables[0].Rows[0]["OrderNums"].ToString();
                Sorder.adult = DS.Tables[0].Rows[0]["Adults"].ToString();
                Sorder.childs = DS.Tables[0].Rows[0]["Childs"].ToString();
                Sorder.gathering = DS.Tables[0].Rows[0]["Price"].ToString();
                allprice = DS.Tables[0].Rows[0]["Price"].ToString();
                Sorder.planid = DS.Tables[0].Rows[0]["PlanId"].ToString();
                Sorder.orderno = DS.Tables[0].Rows[0]["AutoId"].ToString();
                iweboutid = DS.Tables[0].Rows[0]["AutoId"].ToString();
                Sorder.CruisesFlag = flag;

                string RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
                if (RebateFlag == "1")
                {
                    Sorder.gathering = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["rebate"].ToString())).ToString();
                    allprice = (MyConvert.ConToDec(DS.Tables[0].Rows[0]["Price"].ToString()) - MyConvert.ConToDec(DS.Tables[0].Rows[0]["rebate"].ToString())).ToString();
                }
                //游客rankno
                string rankno = "";
                SqlQueryText = string.Format("select rankno from OL_GuestInfo where flag='1' and OrderId='{0}'", orderid);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        rankno += DS.Tables[0].Rows[i]["rankno"].ToString() + ",";
                    }
                    if (rankno.Length > 1) rankno = rankno.Substring(0, rankno.Length - 1);
                }
                Sorder.ordername = rankno;

                //Result = rsp.CruisesOrderAdjust(UpPassWord, Sorder);

                Result = ErpUtil.UpdateOrderAccount(iweboutid, allprice);
                if (Result != "0")
                {
                    //orderid,doflag,inputtime,infos
                    //畅游同步失败，将本次操作保存到相应表，可事后操作
                    if (SaveFlag == "Yes")
                    {
                        SqlQueryText = string.Format("insert into CR_MisDoError (orderid,adjustflag,inputtime,infos) values ('{0}','{1}','{2}','{3}')",
                            orderid,
                            flag,
                            DateTime.Now.ToString(),
                            Result
                        );
                        MyDataBaseComm.ExcuteSql(SqlQueryText);
                    }
                }
                return Result;
            }
            else
            {
                return "订单未找到，畅游同步失败";
            }
            
        }

        public static string GetIdType(string ids, string flag)
        {
            //if (ProductType == "InLand")
            //{
            //    Strings.Append("<option value=\"1\">身份证</option><option value=\"2\">护照</option><option value=\"5\">军官证</option><option value=\"6\">回乡证</option>");
            //    Strings.Append("<option value=\"7\">台胞证</option><option value=\"8\">国际海员证</option><option value=\"9\">外国人永久居住证</option>");
            //    Strings.Append("<option value=\"98\">其他</option><option value=\"99\">稍后提供</option>");
            //}
            //else
            //{
            //    switch (ProductClass)
            //    {
            //        case "227":
            //            Strings.Append("<option value=\"3\">港澳通行证</option>");
            //            Strings.Append("<option value=\"2\">护照</option>");
            //            Strings.Append("<option value=\"99\">稍后提供</option>");
            //            break;
            //        case "851":
            //            Strings.Append("<option value=\"4\">台湾通行证</option>");
            //            Strings.Append("<option value=\"2\">护照</option>");
            //            Strings.Append("<option value=\"99\">稍后提供</option>");
            //            break;
            //        default:
            //            Strings.Append("<option value=\"2\">护照</option>");
            //            Strings.Append("<option value=\"99\">稍后提供</option>");
            //            break;
            //    }
            //}

            StringBuilder Strings = new StringBuilder();
            int a = Convert.ToInt32(ids);
            string[] Options = { "", "", "", "", "", "", "", "", "", "", "", "" };
            Options[a] = "selected=\"selected\"";

            string[] IdType = { "", 
                                "身份证", 
                                "护照",
                                "港澳通行证", 
                                "台湾通行证",
                                "军官证",
                                "回乡证",
                                "台胞证",
                                "国际海员证",
                                "外国人永久居住证",
                                "其他",
                                "稍后提供"};

            switch (flag)
            {
                case "SingleName":
                    Strings.Append(IdType[a]);
                    break;
                case "AllType":
                    for (int i = 1; i <= IdType.ToArray().Length - 1; i++)
                    {
                        Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", i, Options[i],IdType[i]));
                    }
                    break;
                case "HongKong":
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 3, Options[3], IdType[3]));
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 2, Options[2], IdType[2]));
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 11, Options[11], IdType[11]));
                    break;
                case "TaiWan":
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 4, Options[4], IdType[4]));
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 2, Options[2], IdType[2]));
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 11, Options[11], IdType[11]));
                    break;
                case "OutBound":
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 2, Options[2], IdType[2]));
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 11, Options[11], IdType[11]));
                    break;
                case "Cruises":
                    Strings.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 2, Options[2], IdType[2]));
                    break;
                default:
                    break;
            }

            return Strings.ToString();
        
        }

        public static string GetBranch(int a,string flag)
        {
            StringBuilder Strings = new StringBuilder();
            string[] Branch = { "", 
                                "营业总部 衡山路2号", 
                                "卢湾营业部 成都南路124号",
                                "张杨路营业部 张杨路1363号(国际华城楼下)", 
                                "普陀营业部 武宁路225号西宫内（河边大道4号）",
                                "徐汇营业部 虹桥路808号",
                                "虹口营业部 四川北路1755号",
                                "莘庄营业部 莘建东路216号",
                                "五角场营业部 四平路2500号东方商厦底楼103（近黄兴路口）",
                                "天山营业部 天山路762号泓鑫时尚广场2楼20室（肯德基旁）",
                                "人民广场营业部 黄陂北路228号"
                              };
            string[] BranchId = { "", 
                                "45", 
                                "58",
                                "88", 
                                "61",
                                "62",
                                "63",
                                "64",
                                "81",
                                "86",
                                "60"
                                };
            string[] Links = { "", 
                                "联系人：张震，电话：64747476、64338054，邮编：200031，营业时间：09:00-19:00", 
                                "联系人：张燕娟，电话：63875890、53510881，邮编：200021，营业时间：09:00-20:30",
                                "联系人：朱凤，电话：58605121、58515033，邮编：200122，营业时间：09:00-20:00", 
                                "联系人：秦钢，电话：32130227、62441490，邮编：200063，营业时间：08:30-19:30",
                                "联系人：郑婕，电话：64481609、64481610，邮编：200030，营业时间：09:00-20:00",
                                "联系人：时冬妹，电话：65405058、65403882，邮编：200080，营业时间：09:30-20:30",
                                "联系人：张燕，电话：54172321、64986808，邮编：201100，营业时间：08:30-19:30",
                                "联系人：丁乐益，电话：55059775、55059715，邮编：200433，营业时间：09:00-20:00",
                                "联系人：邹爱珍，电话：62412783、62412785，邮编：200050，营业时间：09:00-20:00",
                                "联系人：严海燕，电话：53752762、53752763，邮编：200001，营业时间：09:00-20:00"
                             };
            string[] bus = { "", 
                                " ", 
                                " ",
                                " ",
                                " ",
                                " ",
                                " ",
                                " ",
                                " ",
                                " ",
                                " ",
                                " "
                           };

            switch (flag)
            {
                case "Option":
                    for (int i = 1; i <= Branch.ToArray().Length - 1; i++)
                    {
                        if (a == i)
                        {
                            Strings.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", i, Branch[i]));
                        }
                        else
                        {
                            Strings.Append(string.Format("<option value=\"{0}\">{1}</option>", i, Branch[i]));
                        }
                    }
                    break;
                case "PayOption":
                    for (int i = 1; i <= BranchId.ToArray().Length - 1; i++)
                    {
                        if (a == Convert.ToInt32(BranchId[i]))
                        {
                            Strings.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", BranchId[i], Branch[i]));
                        }
                        else
                        {
                            Strings.Append(string.Format("<option value=\"{0}\">{1}</option>", BranchId[i], Branch[i]));
                        }
                    }
                    break;

                case "BranchName":
                    if (a > 0)
                    {
                        for (int i = 1; i <= BranchId.ToArray().Length - 1; i++)
                        {
                            if (a == Convert.ToInt32(BranchId[i]))
                            {
                                Strings.Append(Branch[i]);
                            }
                        }
                    }
                    else
                    {
                        Strings.Append(Branch[1]);
                    }
                    
                    break;
                case "BranchMap":
                    if (a > 0)
                    {
                        for (int i = 1; i <= BranchId.ToArray().Length - 1; i++)
                        {
                            if (a == Convert.ToInt32(BranchId[i]))
                            {
                                //<div class=oname></div><div class=oinfo>联系人：张震，电话：64747476、64338054，邮编：200031，营业时间：09:00-18:30</div>
                                //<div class=oname>门店地图：</div><div class=oinfo></div>
                                Strings.Append(string.Format("<div class=oname></div><div class=oinfo>{0}</div>", Links[i]));
                                Strings.Append(string.Format("<div class=oname>门店地图：</div><div class=mapoinfo><IMG src='/Images/BranchMap/{0}.jpg' /></div>", BranchId[i]));
                            }
                        }
                    }
                    else
                    {
                        Strings.Append(string.Format("<div class=oname></div><div class=oinfo>{0}</div>", Links[1]));
                        Strings.Append(string.Format("<div class=oname>门店地图：</div><div class=mapoinfo><IMG src='/Images/BranchMap/{0}.jpg' /></div>", BranchId[1]));
                    }
                    break;
                default:
                    for (int i = 1; i <= BranchId.ToArray().Length - 1; i++)
                    {
                        if (a == Convert.ToInt32(BranchId[i]))
                        {
                            Strings.Append(string.Format("<li><div class=oname>门店签约：</div><div class=oinfo>{0}</div></li>", Branch[i]));
                            Strings.Append(string.Format("<li><div class=oname>联系方式：</div><div class=oinfo>{0}</div></li>", Links[i]));
                            //Strings.Append(string.Format("<div class=oname></div><div class=oinfo>{0}</div>", Links[i]));
                            //Strings.Append(string.Format("<div class=oname>门店地图：</div><div class=mapoinfo><IMG src='/Images/BranchMap/{0}.jpg' /></div>", BranchId[i]));
                        }
                    }
                    if (a < 30)
                    {
                        Strings.Append(string.Format("<li><div class=oname>门店签约：</div><div class=oinfo>{0}</div></li>", Branch[1]));
                        Strings.Append(string.Format("<li><div class=oname>联系方式：</div><div class=oinfo>{0}</div></li>", Links[1]));
                    }
                    break;
            }
            //if (flag == "Option")
            //{
            //    for (int i = 1; i <= Branch.ToArray().Length - 1; i++)
            //    {
            //        if (a == i)
            //        {
            //            Strings.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{1}</option>", i, Branch[i]));
            //        }
            //        else
            //        { 
            //            Strings.Append(string.Format("<option value=\"{0}\">{1}</option>", i, Branch[i]));
            //        }                    
            //    }            
            //}
            //else
            //{
            //    Strings.Append(string.Format("<li><div class=oname>门店签约：</div><div class=oinfo>{0}</div></li>", Branch[a]));
            //    Strings.Append(string.Format("<li><div class=oname>联系方式：</div><div class=oinfo>{0}</div></li>", Links[a]));
            //    //Strings.Append(string.Format("<li><div class=oname>交通信息：</div><div class=oinfo>{0}</div></li>", bus[a]));
            //}
            return Strings.ToString();
        }

        public static OrderExtendInfo GetOrderExtInfo(string OrderId,string ProType,string ProClass)
        {
            OrderExtendInfo ExtInfo = new OrderExtendInfo();
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_OrderExtend where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (DS.Tables[0].Rows[i]["ExtType"].ToString() == "contract")
                    {
                        string Cinfo = "";
                        switch (DS.Tables[0].Rows[i]["ExtId"].ToString())
                        {
                                //"传真签约", "快递签约", "上门签约", "门店签约" 
                            case "1":
                                if (ProType == "InLand") {
                                    Cinfo = "请将旅游合同打印、填写并签字盖章后，传真到021-64670982(国内)，我们的客服人员会及时进行处理。";
                                }
                                else {
                                    Cinfo = "请将旅游合同打印、填写并签字盖章后，传真到021-64742928(出境)，我们的客服人员会及时进行处理。";
                                }
                                if (ProClass == "1062") Cinfo = "请将旅游合同打印、填写并签字盖章后，传真到021-64670982(国内)，我们的客服人员会及时进行处理。";
                                ExtInfo.Contract = string.Format("<li><div class=oname>传真签约：</div><div class=oinfo>{0}</div></li>", Cinfo);
                                break;
                            case "2":
                                if (ProType == "InLand")
                                {
                                    Cinfo = "请将旅游合同打印、填写并签字盖章后，快递到：上海市桃江路1号4楼 电子商务部收，邮编：200031。";
                                }
                                else
                                {
                                    Cinfo = "请将旅游合同打印、填写并签字盖章后，快递到：上海市徐汇区衡山路2号 电子商务部收，邮编：200031。";
                                }
                                
                                ExtInfo.Contract = string.Format("<li><div class=oname>快递签约：</div><div class=oinfo>{0}</div></li>", Cinfo);
                                break;
                            case "3":
                                //Cinfo = DS.Tables[0].Rows[i]["ExtContent"].ToString();
                                Cinfo = "在线支付全额团费后，在我的订单中自动生成合同文本，请打印保存即可。";
                                ExtInfo.Contract = string.Format("<li><div class=oname>在线签约：</div><div class=oinfo>{0}</div></li>", Cinfo);
                                break;
                            case "4":
                                Cinfo = GetBranch(Convert.ToInt32(DS.Tables[0].Rows[i]["ExtContent"]), "OrderInfo");
                                ExtInfo.Contract = Cinfo; //string.Format("<li><div class=oname>门店签约：</div><div class=oinfo>{0}</div></li>", Cinfo);
                                break;
                            default:
                                break;
                        }
                    }

                    if (DS.Tables[0].Rows[i]["ExtType"].ToString() == "invoice")
                    {
                        string[] AllInfo = Regex.Split(DS.Tables[0].Rows[i]["ExtContent"].ToString(), @"\|\|", RegexOptions.IgnoreCase);
                        ExtInfo.Invoice += string.Format("<li><div class=oname>发票抬头：</div><div class=oinfo>{0}</div></li>", AllInfo[0]);
                        ExtInfo.Invoice += string.Format("<li><div class=oname>发票内容：</div><div class=oinfo>{0}</div></li>", AllInfo[1]);
                        if (DS.Tables[0].Rows[i]["ExtId"].ToString() == "3")
                        {
                            ExtInfo.Invoice += string.Format("<li><div class=oname>快递信息：</div><div class=oinfo>{0}</div></li>", AllInfo[2]);
                        }
                    }
                    if (DS.Tables[0].Rows[i]["ExtType"].ToString() == "CruisesDinner")
                    {
                        if (DS.Tables[0].Rows[i]["ExtId"].ToString() == "3")
                        {
                            ExtInfo.Dinner += "晚餐时间均可安排";
                        }
                        else
                        {
                            ExtInfo.Dinner += DS.Tables[0].Rows[i]["ExtContent"].ToString();
                        }
                        
                    }
                }
            }
            return ExtInfo;
        }

        public static LineClass LineDetail(string QueryText)
        {
            string SqlQueryText;
            //if (QueryText.Length == 0) QueryText = "1=1";
            SqlQueryText = string.Format("select top 1 MisLineId,LineName,Price,LineType,LineClass,LineDays,DeptId,Shipid,Planid from OL_Line where MisLineId='{0}'", QueryText);
            LineClass Details = new LineClass();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //string url = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["LineType"], DS.Tables[0].Rows[0]["LineClass"], DS.Tables[0].Rows[0]["MisLineId"]);
                Details.LineId = DS.Tables[0].Rows[0]["MisLineId"].ToString();
                Details.LineType = DS.Tables[0].Rows[0]["LineType"].ToString();
                Details.LinesClass = DS.Tables[0].Rows[0]["LineClass"].ToString();
                Details.LineName = DS.Tables[0].Rows[0]["LineName"].ToString();
                Details.LinePrice = DS.Tables[0].Rows[0]["Price"].ToString();
                Details.Deptid = DS.Tables[0].Rows[0]["DeptId"].ToString();
                Details.LineDays = DS.Tables[0].Rows[0]["LineDays"].ToString();
                Details.Shipid = DS.Tables[0].Rows[0]["Shipid"].ToString();
                Details.Planid = DS.Tables[0].Rows[0]["Planid"].ToString();
                //Details.LineUrl = string.Format("/{0}/{1}/{2}.html", DS.Tables[0].Rows[0]["LineType"], DS.Tables[0].Rows[0]["LineClass"], DS.Tables[0].Rows[0]["MisLineId"]);
                return Details;
            }
            else
            {
                return null;
            }
        }

        public static CruisesPlanInfo GetCrusesPlanList(string Lineid)
        {
            //Lineid = "3873";
            CruisesPlanInfo PlanInfo = new CruisesPlanInfo();
            StringBuilder PlanString = new StringBuilder();
            StringBuilder RoomString = new StringBuilder();

            //PlanString.Append("</div>");
            //RoomString.Append("</div>");

            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            CruisesLine Cruises = new CruisesLine();

            Cruises = rsp.GetCruisesRoom(UpPassWord, Lineid);

            if (Cruises.CruisesPlans != null)
            {
                PlanString.Append(string.Format("<input id=\"PlanId\" type=\"hidden\" value=\"{0}\"/><input id=\"BeginDate\" type=\"hidden\" value=\"{1}\"/>", Cruises.CruisesPlans[0].PlanId, Cruises.CruisesPlans[0].BeginDate));
                for (int i = 0; i < Cruises.CruisesPlans.Length; i++)
                {
                    //PlanString.Append("<div class="CruisesDate" tag="01">2011-12-11</div>");
                    //if (i == 0)
                    //{
                    //    PlanString.Append(string.Format("<input id=\"PlanId\" type=\"hidden\" value=\"{0}\"/><input id=\"BeginDate\" type=\"hidden\" value=\"{1}\"/><div class=\"CruisesDate CruisesSelect\" tag=\"{0}\">{1}</div>", Cruises.CruisesPlans[i].PlanId, Cruises.CruisesPlans[i].BeginDate));
                    //}
                    //else
                    //{
                    //    PlanString.Append(string.Format("<div class=\"CruisesDate\" tag=\"{0}\">{1}</div>", Cruises.CruisesPlans[i].PlanId, Cruises.CruisesPlans[i].BeginDate));
                    //}

                    PlanString.Append(string.Format("<div id=\"Date_{0}\"class=\"CruisesDate\" tag=\"{0}\">{1}</div>", Cruises.CruisesPlans[i].PlanId, Cruises.CruisesPlans[i].BeginDate));

                    RoomString.Append(string.Format("<div id=\"{0}\" class=\"hide\">", Cruises.CruisesPlans[i].PlanId));
                    if (Cruises.CruisesPlans[i].CruisesRooms != null)
                    {
                        //if (i == 0)
                        //{
                        //    RoomString.Append(string.Format("<div id=\"{0}\">", Cruises.CruisesPlans[i].PlanId));                        
                        //}
                        //else
                        //{
                        //    RoomString.Append(string.Format("<div id=\"{0}\" class=\"hide\">", Cruises.CruisesPlans[i].PlanId));                        
                        //}

                        //RoomString.Append("<ul id=SellPrice_ class=Cruises>");
                        RoomString.Append("<div class=\"mc tabcon borders01\">");
                        RoomString.Append(string.Format("<ul id=SellPrice_{0} class=Cruises>", Cruises.CruisesPlans[i].PlanId));
                        RoomString.Append("<li class=cur><div class=ttype>类型</div><div class=tname>名称</div><div class=tsname>配置</div><div class=tprice>价格</div><div class=tnum>预订</div><div class=tpic></div></li>");
                        int rooms, beds;

                        int colornum = 1;
                        string color;
                        for (int r = 0; r < Cruises.CruisesPlans[i].CruisesRooms.Length; r++)
                        {
                            rooms = MyConvert.ConToInt(Cruises.CruisesPlans[i].CruisesRooms[r].RoomNum);
                            beds = MyConvert.ConToInt(Cruises.CruisesPlans[i].CruisesRooms[r].RoomBed);
                            if (rooms > 9) rooms = 9;
                            if (rooms < 0) rooms = 0;
                            if (r > 0)
                            {
                                if (Cruises.CruisesPlans[i].CruisesRooms[r].RoomType != Cruises.CruisesPlans[i].CruisesRooms[r - 1].RoomType)
                                {
                                    if (colornum == 1)
                                    {
                                        colornum = 2;
                                    }
                                    else
                                    {
                                        colornum = 1;
                                    }
                                }
                            }
                            color = "color" + colornum;

                            if (rooms > 0)
                            {
                                RoomString.Append(string.Format("<li class=\"priceli {5}\" tps=roomlist roomid={0} id={1} rooms={2} beds={3} price={4}>", Cruises.CruisesPlans[i].CruisesRooms[r].CruisesId, Cruises.CruisesPlans[i].CruisesRooms[r].PriceId, rooms, beds, Cruises.CruisesPlans[i].CruisesRooms[r].RoomPrice, color));
                                RoomString.Append(string.Format("<div class=ftype>{0}</div><div class=fname>{1}</div>", Cruises.CruisesPlans[i].CruisesRooms[r].RoomType, Cruises.CruisesPlans[i].CruisesRooms[r].RoomName));
                                RoomString.Append(string.Format("<div class=fsname>{0}</div><div class=fprice>&yen;<span class=sellprice>{1}</span></div>", Cruises.CruisesPlans[i].CruisesRooms[r].RoomStand, Cruises.CruisesPlans[i].CruisesRooms[r].RoomPrice));
                                RoomString.Append(string.Format("<div class=fnum><select class=psel>{0}</select>间 &nbsp;<select class=ddlnums>{1}</select>成人 &nbsp;<select class=ddlnums>{1}</select>儿童</div><div id=pic class=fnpic></div></li>", GetDropList(rooms), GetDropList(rooms * beds)));//
                            }
                            //RoomString.Append("<li class=priceli tps=SellPrice tag=125750 id=P125750><div class=ftype>内仓</div><div class=fname>舱位Q二人间</div><div class=fsname>人数：2人<br>面积：20平米<br>楼层：甲板2，3，4，5层</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value=\"0\">0</option></select>间 &nbsp;<select class=psel><option value=\"0\">0</option></select>成人 &nbsp;<select class=psel><option value=\"0\">0</option></select>儿童</div><div id=pic class=fnpic></div></li>");
                        }
                        RoomString.Append("</ul>");
                        RoomString.Append("</div>");
                        RoomString.Append(string.Format("<div><a href=\"javascript:void(0);\" class=ShowMore onclick=\"ShowMoreCruises('{0}')\"><img src=\"/images/mbi_down.gif\">查看更多舱位...</a></div>", Cruises.CruisesPlans[i].PlanId));
                    }
                    RoomString.Append("</div>");
                }
            }

            //PlanString.Append("<div id=CruisesDateList>");
            //RoomString.Append("<div id=CruisesRoomList>");
            PlanInfo.CruisesPlanInfo_PlanList = PlanString.ToString();
            PlanInfo.CruisesPlanInfo_RoomList = RoomString.ToString();
            return PlanInfo;
        }

        public static string GetDropList(int nums)
        {
            StringBuilder Strings = new StringBuilder();
            for (int i = 0; i <= nums; i++)
            {
                Strings.Append(string.Format("<option value=\"{0}\">{0}</option>", i));
            }
            return Strings.ToString();
        }

        public static string GetPreferenceList(int OrderNums, string Lineid)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;
            DataSet DS = new DataSet();
            DS.Clear();

            SqlQueryText = string.Format("select wwwyh from OL_Line where MisLineId='{0}'", Lineid);
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) > 0)
                {
                    Strings.Append("<li id=Pre1 class=hide><ul id=PrePrice class=price>");
                    Strings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>优惠合计</div><div class=tpic></div></li>");
                    Strings.Append(string.Format("<li class=pre_priceli tps=PrePrice tag=PrePrice id=Preference><div class=ftype>在线支付</div><div class=fname>每人立减{0}元</div><div class=fprice><span class=PrePrice>&yen;</span> <span id=Pre_Price class=PrePrice>-{0}</span></div>", DS.Tables[0].Rows[0]["wwwyh"].ToString()));
                    Strings.Append("<div class=fnum>");
                    Strings.Append(string.Format("<select class=psel><option value=\"{0}\">{0}</option></select>", OrderNums));
                    Strings.Append(string.Format("</div><div class=fsum><span class=PrePrice>&yen;</span> <span class=PrePrice id=SumPre_Price>-{0}</span></div><div id=pic class=fpic></div></li>", MyConvert.ConToInt(DS.Tables[0].Rows[0]["wwwyh"].ToString()) * OrderNums));
                    Strings.Append("</ul></li>");

                }
                else
                {
                    Strings.Append("<li id=Pre1 class=\"hide order\"><div class=oname>支付说明：</div><div class=oinfo>请于订单确认后24小时之内，通过网上支付方式付清全部费用！</div>");
                    Strings.Append("<ul id=PrePrice class=hide>");
                    Strings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>优惠合计</div><div class=tpic></div></li>");
                    Strings.Append(string.Format("<li class=pre_priceli tps=PrePrice tag=PrePrice id=Preference><div class=ftype>在线支付优惠</div><div class=fname>每人立减{0}元</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice id=Pre_Price>{0}</span></div>", "0"));
                    Strings.Append("<div class=fnum>");
                    Strings.Append(string.Format("<select class=psel><option value=\"{0}\">{0}</option></select>", OrderNums));
                    Strings.Append(string.Format("</div><div class=fsum><span class=PrePrice>&yen;</span> <span class=PrePrice id=SumPre_Price>{0}</span></div><div id=pic class=fpic></div></li>", "0"));
                    Strings.Append("</ul></li>");
                }
            }
            return string.Format("{0}", Strings.ToString());
        }

        public static string GetPriceList(int OrderNums, int Adults, int Childs, string Lineid, string Planid, string BeginDate, string OrderId, string OrderType, string PreCount, string UserId, int shipid, string VisitSell)
        {
            StringBuilder Strings = new StringBuilder();
            StringBuilder ExtStrings = new StringBuilder();
            StringBuilder NeedStrings = new StringBuilder();
            StringBuilder RebateStrings = new StringBuilder();
            string NeedPrice = "0";
            string PriceId = "0";

            DataSet DS = new DataSet();
            DS.Clear();

            //基本费用开始
            //////////////////////////////////////

            if (OrderType == "Cruises" && shipid==0)
            {
                string SqlQueryText = string.Format("select * from OL_CuisesRoom where OrderId='{0}'", OrderId);                
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                    {
                        PriceId += "," + DS.Tables[0].Rows[a]["PriceId"].ToString();
                    }
                }
            }

            string NoPrice = "<div class=\"m detail\"><UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的基本费用！</div></div><div class=\"m detail\"><UL class=tab><LI class=curr>可选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的可选费用！</div></div>";
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            PlanPrices GetPlan = new PlanPrices();

            try
            {
                if (Planid == "0")
                {
                    //GetPlan = rsp.GetLinePrices(UpPassWord, Lineid, BeginDate);
                    GetPlan = ErpUtil.getPriceInfo(Lineid, BeginDate);
                }
                else
                {
                    //GetPlan = rsp.GetPlanPrices(UpPassWord, Lineid, Planid, PriceId);
                    GetPlan = ErpUtil.getPriceInfo(Lineid, BeginDate);
                }
            }
            catch
            {
                //Strings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的基本费用！</div></div>");
                //Strings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>可选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的可选费用！</div></div>");
                return NoPrice;
            }

            if (GetPlan.PlanStaPrice != null)
            {
                string OptionHtml = "";
                string AdultOption = "";
                string ChildOption = "";
                string ChildOptionHtml = "";
                for (int i = 0; i <= OrderNums; i++)
                {
                    OptionHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
                    if (i==Adults)
                    {
                        AdultOption += string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                    }
                    else
                    {
                        AdultOption += string.Format("<option value=\"{0}\">{0}</option>", i);
                    }                    
                }
                for (int i = 0; i <= Childs; i++)
                {
                    ChildOptionHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
                    if (i == Childs)
                    {
                        ChildOption += string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                    }
                    else
                    {
                        ChildOption += string.Format("<option value=\"{0}\">{0}</option>", i);
                    }
                }

                if (shipid == 0)
                {
                    Strings.Append("<input id='Room_Gather' name='Room_Gather' type='hidden' value='0'/>");
                    Strings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    Strings.Append("<ul id=SellPrice class=price>");
                    if (OrderType == "Cruises")
                    {
                        //Strings.Append("<li class=cur><div class=\"ttype hide\">费用类型</div><div class=roomname>舱位名称</div><div class=troom>房间数</div><div class=tprice>价格</div><div class=tprice>第三人成人</div><div class=tprice>第三人儿童</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                        Strings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>费用说明</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    }
                    else
                    {
                        Strings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>费用说明</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    }
                    Strings.Append("");
                    Strings.Append("");

                    if (OrderType == "Cruises" && DS.Tables[0].Rows.Count > 0)
                    {

                        //for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                        //{
                        //    for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                        //    {
                        //        if (DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanStaPrice[i].PriceId)
                        //        {
                        //            Strings.Append(string.Format("<li class=priceli tps=SellPrice tag={0} id=P{0} Cuises=1 AdultNums={1} ChildNums={2} Rooms={3}>", GetPlan.PlanStaPrice[i].PriceId, DS.Tables[0].Rows[a]["AdultNum"].ToString(), DS.Tables[0].Rows[a]["ChildNum"].ToString(), DS.Tables[0].Rows[a]["OrderNum"].ToString()));

                        //            Strings.Append(string.Format("<div class=\"ftype hide\">{0}</div><div class=froomname>{1}</div><div class=froom>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div><div class=fprice>&yen;<span class=adultprice>{4}</span></div><div class=fprice>&yen;<span class=childprice>{5}</span></div>", GetPlan.PlanStaPrice[i].PriceType, GetPlan.PlanStaPrice[i].CruiseName, DS.Tables[0].Rows[a]["RoomNum"].ToString(), GetPlan.PlanStaPrice[i].Price, GetPlan.PlanStaPrice[i].Thd_adultprice, GetPlan.PlanStaPrice[i].Thd_childprice));
                        //            Strings.Append("<div class=fnum><select class=psel>");
                        //            Strings.Append(string.Format("<option value=\"{0}\">{0}</option>", DS.Tables[0].Rows[a]["OrderNum"].ToString()));
                        //            // 
                        //            Strings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        //        }
                        //    }
                        //}
                        for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                        {
                            if (GetPlan.PlanStaPrice[i].PriceType == "儿童价" && Childs == 0)
                            { }
                            else
                            {
                                Strings.Append(string.Format("<li class=priceli tps=SellPrice tag={0} id=P{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", GetPlan.PlanStaPrice[i].PriceId, GetPlan.PlanStaPrice[i].PriceType, GetPlan.PlanStaPrice[i].PriceName, GetPlan.PlanStaPrice[i].Price));
                                Strings.Append("<div class=fnum><select class=psel>");
                                switch (GetPlan.PlanStaPrice[i].PriceType)
                                {
                                    case "成人价":
                                        Strings.Append(AdultOption);
                                        AdultOption = OptionHtml;
                                        break;
                                    case "儿童价":
                                        Strings.Append(ChildOption);
                                        ChildOption = ChildOptionHtml;
                                        break;
                                    default:
                                        Strings.Append(OptionHtml);
                                        break;
                                }
                                Strings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                        {
                            if (GetPlan.PlanStaPrice[i].PriceType == "儿童价" && Childs == 0)
                            { }
                            else
                            {
                                Strings.Append(string.Format("<li class=priceli tps=SellPrice tag={0} id=P{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", GetPlan.PlanStaPrice[i].PriceId, GetPlan.PlanStaPrice[i].PriceType, GetPlan.PlanStaPrice[i].PriceName, GetPlan.PlanStaPrice[i].Price));
                                Strings.Append("<div class=fnum><select class=psel>");
                                switch (GetPlan.PlanStaPrice[i].PriceType)
                                {
                                    case "成人价":
                                        Strings.Append(AdultOption);
                                        AdultOption = OptionHtml;
                                        break;
                                    case "儿童价":
                                        Strings.Append(ChildOption);
                                        ChildOption = ChildOptionHtml;
                                        break;
                                    default:
                                        Strings.Append(OptionHtml);
                                        break;
                                }
                                Strings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                            }
                        }
                    }

                    Strings.Append("</ul>");
                    Strings.Append("</div></div>");
                }
                else
                {
                    //邮轮舱位预定列表
                    string roomgather = MyDataBaseComm.getScalar("select ISNULL(sum(gather),0) from CR_RoomOrder where OrderId='" + OrderId + "' and lineid='" + Lineid + "'");
                    Strings.Append("<input id='Room_Gather' name='Room_Gather' type='hidden' value='" + roomgather + "'/>");
                    Strings.Append("<div id=Div1 class=\"m detail\">");
                    Strings.Append("<UL class=tab><LI class=curr>房间和人数<SPAN></SPAN></LI></UL>");
                    Strings.Append("<div class=\"mc tabcon borders01\">");
                    Strings.Append("<div class=roomdivlist>");
                    Strings.Append("<DIV class=roomHead>双人间最少入住2人，不满2人需要补房差；三人间或四人间同舱的第3、第4位可享受价格优惠；</DIV>");
                    Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
                    Strings.Append("<tr class=tit1>");
                    Strings.Append("<td width=\"30%\">房间类型</td>");
                    Strings.Append("<td width=\"5%\">成人</td>");
                    Strings.Append("<td width=\"5%\">儿童</td>");
                    Strings.Append("<td width=\"5%\">房间数</td>");
                    Strings.Append("<td width=\"10%\">第1、2人价格</td>");
                    Strings.Append("<td width=\"10%\">第3成人价</td>");
                    Strings.Append("<td width=\"10%\">第3儿童价</td>");
                    Strings.Append("<td width=\"10%\">价格小计</td>");
                    Strings.Append("</tr>");
                    

                    string SqlQueryText = string.Format("select * from CR_RoomOrder where OrderId='{0}'", OrderId);
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            //str += "<tr><td>{0}</td><td>{1}</td><td>{0}2</td><td>{3}间</td><td class=tdn>&yen;{4}</td><td class=tdn>&yen;{5}</td><td class=tdn>&yen;{6}</td><td class=tds>&yen;{7}</td></tr>"
                            Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}间</td><td class=tdn>&yen;{4}</td><td class=tdn>&yen;{5}</td><td class=tdn>&yen;{6}</td><td class=tds>&yen;{7}</td></tr>",
                                DS.Tables[0].Rows[i]["roomname"].ToString(),
                                DS.Tables[0].Rows[i]["adult"].ToString(),
                                DS.Tables[0].Rows[i]["childs"].ToString(),
                                DS.Tables[0].Rows[i]["rooms"].ToString(),
                                DS.Tables[0].Rows[i]["price"].ToString(),
                                DS.Tables[0].Rows[i]["thirdprice"].ToString(),
                                DS.Tables[0].Rows[i]["childprice"].ToString(),
                                DS.Tables[0].Rows[i]["gather"].ToString()
                            ));
                            
                        }
                    }

                    Strings.Append("</table>");
                    Strings.Append("</div>");
                    Strings.Append("</div>");
                    Strings.Append("</div>");
                }
                


                ///////////////////////////////////////////////////////
                //基本费用结束

                if (GetPlan.PlanExtPrice != null)
                {

                    ExtStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>可选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    ExtStrings.Append("<ul class=price>");
                    ExtStrings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    ExtStrings.Append("");

                    NeedStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>必选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    NeedStrings.Append("<ul id=ExtPrice class=price>");
                    NeedStrings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    NeedStrings.Append("");
                    
                    string ExtType = "";
                    for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                    {
                        switch (GetPlan.PlanExtPrice[i].InfoId)
                        {
                            case "1":
                                ExtType = "单房差";
                                break;
                            case "2":
                                ExtType = "自费项目";
                                break;
                            case "3":
                                ExtType = "小费";
                                break;
                            case "4":
                                ExtType = "其他费用";
                                break;
                            case "5":
                                ExtType = "保险费用";
                                break;
                            case "6":
                                ExtType = "机票税金";
                                break;
                            default:
                                break;
                        }

                        if (GetPlan.PlanExtPrice[i].OnlineNeeds == "0")
                        {
                            ExtStrings.Append(string.Format("<li class=priceli tps=ExtPrice tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname>{2} {3}</div><div class=fprice>&yen;<span class=sellprice>{4}</span></div>", GetPlan.PlanExtPrice[i].PriceId, ExtType, GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName, GetPlan.PlanExtPrice[i].Price));
                            ExtStrings.Append("<div class=fnum><select class=psel>");
                            ExtStrings.Append(OptionHtml);
                            ExtStrings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        }
                        else
                        {
                            NeedPrice = "1";
                            NeedStrings.Append(string.Format("<li class=priceli tps=ExtPrice tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2} {3}</div><div class=fprice>&yen;<span class=sellprice>{4}</span></div>", GetPlan.PlanExtPrice[i].PriceId, "必选费用", GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName, GetPlan.PlanExtPrice[i].Price));
                            NeedStrings.Append("<div class=fnum><select class=psel>");
                            switch (GetPlan.PlanExtPrice[i].OnlineNeeds)
                            {
                                case "1":
                                    NeedStrings.Append(string.Format("<option value=\"{0}\">{0}</option>", Adults));
                                    break;
                                case "2":
                                    NeedStrings.Append(string.Format("<option value=\"{0}\">{0}</option>", Childs));
                                    break;
                                case "3":
                                    NeedStrings.Append(string.Format("<option value=\"{0}\">{0}</option>", OrderNums));
                                    break;
                                default:
                                    break;
                            }
                            NeedStrings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        }                        
                    }

                    ExtStrings.Append("</ul>");
                    ExtStrings.Append("</div></div>");

                    NeedStrings.Append("</ul>");
                    NeedStrings.Append("</div></div>");
                    if (NeedPrice == "0") NeedStrings.Clear();
                }

                //岸上观光
                if (VisitSell == "1")
                {
                    if (shipid > 0)
                    {
                        string SqlQueryText;
                        SqlQueryText = "select * from CR_Visit where sellflag='0' and lineid='" + Lineid + "' order by days,id";
                        DS = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            //ExtStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>岸上观光<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                            //ExtStrings.Append("<ul class=\"price Visit\">");
                            //ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                            //ExtStrings.Append("");
                            int rows = 0;
                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                rows = i;
                                if (i == 0)
                                {
                                    ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
                                    ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
                                    ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                                    ExtStrings.Append("");

                                    ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                                    ExtStrings.Append("<div class=fnum><select class=psel>");
                                    ExtStrings.Append(OptionHtml);
                                    ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                                }
                                else
                                {
                                    if (DS.Tables[0].Rows[i]["days"].ToString() != DS.Tables[0].Rows[i - 1]["days"].ToString())
                                    {
                                        ExtStrings.Append("</ul>");
                                        ExtStrings.Append("</div></div>");
                                        ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
                                        ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
                                        ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                                        ExtStrings.Append("");

                                        ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                                        ExtStrings.Append("<div class=fnum><select class=psel>");
                                        ExtStrings.Append(OptionHtml);
                                        ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                                    }
                                    else
                                    {
                                        ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                                        ExtStrings.Append("<div class=fnum><select class=psel>");
                                        ExtStrings.Append(OptionHtml);
                                        ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                                    }
                                    if (i == DS.Tables[0].Rows.Count - 1)
                                    {
                                        ExtStrings.Append("</ul>");
                                        ExtStrings.Append("</div></div>");
                                    }

                                }

                                //ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["vtitle"].ToString(), DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

                                //ExtStrings.Append("<div class=fnum><select class=psel>");
                                //ExtStrings.Append(OptionHtml);
                                //ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                            }
                            if (rows == 0)
                            {
                                ExtStrings.Append("</ul>");
                                ExtStrings.Append("</div></div>");
                            }
                            //ExtStrings.Append("</ul>");
                            //ExtStrings.Append("</div></div>");
                        }
                    }
                }
                //岸上观光结束

                int OldNum = OrderNums;
                int CanRebate = 0;
                //优惠政策
                if (shipid == 0)
                {
                    //普通线路优惠
                    if (GetPlan.PlanRebatePrice != null)
                    {
                        CanRebate = 1;
                        RebateStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>促销优惠<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                        RebateStrings.Append("<ul class=price>");
                        RebateStrings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                        RebateStrings.Append("");

                        string ExtType = "";
                        int EditNum = 0;
                        for (int i = 0; i < GetPlan.PlanRebatePrice.Length; i++)
                        {
                            OrderNums = OldNum;
                            switch (GetPlan.PlanRebatePrice[i].PriceType)
                            {
                                case "1":
                                    ExtType = "下单立减";
                                    OrderNums = 1;
                                    EditNum = 1;
                                    break;
                                case "2":
                                    ExtType = "每人立减";
                                    EditNum = OrderNums;
                                    break;
                                case "3":
                                    ExtType = "立享优惠";
                                    double aa = (OrderNums / MyConvert.ConToInt(GetPlan.PlanRebatePrice[i].Nums));
                                    OrderNums = MyConvert.ConToInt(Math.Floor(aa).ToString());
                                    EditNum = OrderNums;
                                    break;
                                default:
                                    break;
                            }
                            //Strings.Append(string.Format("<li class=pre_priceli tps=PrePrice tag=PrePrice id=Preference><div class=ftype>在线支付</div><div class=fname>每人立减{0}元</div><div class=fprice><span class=PrePrice>&yen;</span> <span id=Pre_Price class=PrePrice>-{0}</span></div>", DS.Tables[0].Rows[0]["wwwyh"].ToString()));

                            RebateStrings.Append(string.Format("<li class=priceli tps=Rebate tag={0} id=RA{0}><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice>-{3}</span><span class=\"sellprice hide\">-{3}</span></div>", GetPlan.PlanRebatePrice[i].PriceId, ExtType, GetPlan.PlanRebatePrice[i].PriceName, GetPlan.PlanRebatePrice[i].Price));
                            RebateStrings.Append("<div class=fnum><select class=psel>");
                            if (i > 0) EditNum = 0;
                            RebateStrings.Append(EditPrceDropList(OrderNums, EditNum));
                            RebateStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

                        }

                        RebateStrings.Append("</ul>");
                        RebateStrings.Append("</div></div>");
                    }
                    //普通线路优惠结束
                }
                else
                {
                    //邮轮舱房优惠 flag=0 舱房优惠标志
                    string SqlQueryText = "";
                    SqlQueryText += string.Format("select *,(select top 1 ISNULL(price,0) from CR_Rebate where allotid=CR_RoomOrder.allotid and begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' and flag='0' order by id) as rebateprice,", DateTime.Today);
                    SqlQueryText += string.Format("(select top 1 infos from CR_Rebate where allotid=CR_RoomOrder.allotid and begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' and flag='0' order by id) as rebateinfos,", DateTime.Today);
                    SqlQueryText += string.Format("(select top 1 ISNULL(price,0) from CR_Rebate where allotid=CR_RoomOrder.allotid and begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' and flag='1' order by id) as viewrebateprice", DateTime.Today);
                    SqlQueryText += " from CR_RoomOrder where rooms>0 ";
                    SqlQueryText += string.Format(" and OrderId='{0}'", OrderId); 
                    
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        int viewsrebate = 0;
                        string CR_Rebate = "0";
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["rebateprice"].ToString()) > 0)
                            {
                                CR_Rebate = "1";
                            }
                            viewsrebate += MyConvert.ConToInt(DS.Tables[0].Rows[i]["viewrebateprice"].ToString()) * MyConvert.ConToInt(DS.Tables[0].Rows[i]["peoples"].ToString());
                        }

                        if (viewsrebate > 0) CR_Rebate = "1";

                        if (CR_Rebate == "1")
                        {
                            RebateStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>促销优惠<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                            RebateStrings.Append("<ul class=price>");
                            RebateStrings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>数量</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                            RebateStrings.Append("");

                            for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                            {
                                if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["rebateprice"].ToString()) > 0)
                                {
                                    RebateStrings.Append(string.Format("<li class=priceli tps=CruisesRebate tag={0} id=RC{0}><div class=ftype>舱房优惠</div><div class=fname title='{1} {2}'>{1} {2}</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice>-{3}</span><span class=\"sellprice hide\">-{3}</span></div>", DS.Tables[0].Rows[i]["allotid"].ToString(), DS.Tables[0].Rows[i]["roomname"].ToString(), DS.Tables[0].Rows[i]["rebateinfos"].ToString(), DS.Tables[0].Rows[i]["rebateprice"].ToString()));
                                    RebateStrings.Append("<div class=fnum><select class=psel>");
                                    RebateStrings.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>",DS.Tables[0].Rows[i]["rooms"].ToString()));
                                    RebateStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                                }
                            }

                            //岸上观光优惠
                            if (viewsrebate > 0)
                            {
                                RebateStrings.Append(string.Format("<li class=priceli tps=ViewsRebate tag={0} id=VR0><div class=ftype>观光优惠</div><div class=fname title='岸上观光线路促销优惠'>岸上观光线路促销优惠</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=\"PrePrice viewrebate\">0</span><span class=\"sellprice hide\">0</span></div>", viewsrebate));
                                RebateStrings.Append("<div class=fnum><select class=psel>");
                                RebateStrings.Append(string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", "1"));
                                RebateStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                            }

                            RebateStrings.Append("</ul>");
                            RebateStrings.Append("</div></div>");
                        }
                        
                    }
                }
                

                //优惠券
                if (PreCount != "0")
                {
                    string SqlQueryText, PreDate;
                    PreDate = string.Format("{0:yyyy-MM-dd}", DateTime.Today);
                    SqlQueryText = "select pid,count(pid) as precount,(select deduction from Pre_Policy where id=Pre_Ticket.pid) as deduction,(select par from Pre_Policy where id=Pre_Ticket.pid) as par,(select memo from Pre_Policy where id=Pre_Ticket.pid) as memo";
                    SqlQueryText += " from Pre_Ticket where sellflag in (1,2) and flag='0' and begindate<='" + PreDate + "' and enddate>='" + PreDate + "' ";
                    SqlQueryText += "and pbdate<='" + BeginDate + "' and pedate>='" + BeginDate + "' and userid='" + UserId + "' ";
                    SqlQueryText += " and range in (1,";
                    switch (OrderType)
                    {
                        case "OutBound":
                            SqlQueryText += "2,";
                            break;
                        case "InLand":
                            SqlQueryText += "3,";
                            break;
                        case "Visa":
                            SqlQueryText += "4,";
                            break;
                        case "Cruises":
                            SqlQueryText += "5,";
                            break;
                        default:
                            break;
                    }
                    SqlQueryText += "8,9)";
                    SqlQueryText += " group by pid";
                    //RebateStrings.Append(SqlQueryText);  BeginDate
                    DS = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        RebateStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>优惠券<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                        RebateStrings.Append("<ul class=price>");
                        RebateStrings.Append("<li class=cur><div class=ttype>优惠券类型</div><div class=tname>优惠券说明</div><div class=tprice>优惠金额</div><div class=tnum>可用份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                        RebateStrings.Append("");

                        string ExtType = "";
                        int EditNum = 0;
                        int precount = 0;
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            precount = MyConvert.ConToInt(DS.Tables[0].Rows[i]["precount"].ToString());
                            OrderNums = OldNum;
                            switch (DS.Tables[0].Rows[i]["deduction"].ToString())
                            {
                                case "1":
                                    ExtType = "整单优惠券";
                                    OrderNums = 1;
                                    EditNum = 1;
                                    break;
                                case "2":
                                    ExtType = "每人优惠券";
                                    if (OrderNums > precount) OrderNums = precount;
                                    EditNum = OrderNums;
                                    break;
                                default:
                                    break;
                            }

                            RebateStrings.Append(string.Format("<li class=priceli tps=Coupon tag={0} id=RB{0}><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice>-{3}</span><span class=\"sellprice hide\">-{3}</span></div>", DS.Tables[0].Rows[i]["pid"].ToString(), ExtType, DS.Tables[0].Rows[i]["memo"].ToString(), DS.Tables[0].Rows[i]["par"].ToString()));
                            RebateStrings.Append("<div class=fnum><select class=psel>");
                            if (CanRebate == 0)
                            {
                                if (i > 0) EditNum = 0;
                            }
                            else
                            {
                                EditNum = 0;
                            }
                            RebateStrings.Append(EditPrceDropList(OrderNums, EditNum));
                            RebateStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        }
                        RebateStrings.Append("</ul>");
                        RebateStrings.Append("</div></div>");
                    }
                }
            }
            else
            {
                return NoPrice;
            }
            
            //NeedStrings.
            return string.Format("{0}{1}{2}{3}", Strings.ToString(), NeedStrings.ToString(), ExtStrings.ToString(), RebateStrings.ToString());
        }

        public static string GetEditPriceList(string OrderId, int OrderNums, int Adults, int Childs, string Lineid, string Planid, string BeginDate, string OrderType)
        {
            string SqlQueryText;
            string PriceId = "0";
            DataSet DS1 = new DataSet();
            DS1.Clear();

            if (OrderType == "Cruises")
            {
                SqlQueryText = string.Format("select * from OL_CuisesRoom where OrderId='{0}'", OrderId);
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    for (int a = 0; a < DS1.Tables[0].Rows.Count; a++)
                    {
                        PriceId += "," + DS1.Tables[0].Rows[a]["PriceId"].ToString();
                    }
                }
            }


            int EditNum = -1;
            SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}'", OrderId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    LoadPriceFlag = "1";
            //}

            StringBuilder Strings = new StringBuilder();
            StringBuilder ExtStrings = new StringBuilder();
            StringBuilder NeedStrings = new StringBuilder();
            StringBuilder RebateStrings = new StringBuilder();
            string NeedPrice = "0";
            
            string NoPrice = "<div class=\"m detail\"><UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的基本费用！</div></div><div class=\"m detail\"><UL class=tab><LI class=curr>可选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">没有任何可选择的可选费用！</div></div>";
            string UpPassWord = Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]);
            TravelOnlineService rsp = new TravelOnlineService();
            rsp.Url = Convert.ToString(ConfigurationManager.AppSettings["TravelMisWebService"]) + "/WebService/TravelOnline.asmx";
            PlanPrices GetPlan = new PlanPrices();

            try
            {
                if (Planid == "0" && BeginDate.Length > 5)
                {
                    GetPlan = rsp.GetLinePrices(UpPassWord, Lineid, BeginDate);
                }
                else
                {
                    GetPlan = rsp.GetPlanPrices(UpPassWord, Lineid, Planid, PriceId);

                }
            }
            catch
            {
                return NoPrice;
            }

            if (GetPlan.PlanStaPrice != null)
            {
                Strings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>基本费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                Strings.Append("<ul id=SellPrice class=price>");
                if (OrderType == "Cruises")
                {
                    Strings.Append("<li class=cur><div class=\"ttype hide\">费用类型</div><div class=roomname>舱位名称</div><div class=troom>房间数</div><div class=tprice>价格</div><div class=tprice>第三人成人</div><div class=tprice>第三人儿童</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                }
                else
                {
                    Strings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                }
                //Strings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                Strings.Append("");
                Strings.Append("");
                if (OrderType == "Cruises" && DS1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                    {
                        for (int a = 0; a < DS1.Tables[0].Rows.Count; a++)
                        {
                            if (DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanStaPrice[i].PriceId)
                            {
                                Strings.Append(string.Format("<li class=priceli tps=SellPrice tag={0} id=P{0} Cuises=1 AdultNums={1} ChildNums={2} Rooms={3}>", GetPlan.PlanStaPrice[i].PriceId, DS1.Tables[0].Rows[a]["AdultNum"].ToString(), DS1.Tables[0].Rows[a]["ChildNum"].ToString(), DS1.Tables[0].Rows[a]["OrderNum"].ToString()));

                                Strings.Append(string.Format("<div class=\"ftype hide\">{0}</div><div class=froomname>{1}</div><div class=froom>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div><div class=fprice>&yen;<span class=adultprice>{4}</span></div><div class=fprice>&yen;<span class=childprice>{5}</span></div>", GetPlan.PlanStaPrice[i].PriceType, GetPlan.PlanStaPrice[i].CruiseName, DS1.Tables[0].Rows[a]["RoomNum"].ToString(), GetPlan.PlanStaPrice[i].Price, GetPlan.PlanStaPrice[i].Thd_adultprice, GetPlan.PlanStaPrice[i].Thd_childprice));
                                Strings.Append("<div class=fnum><select class=psel>");
                                Strings.Append(string.Format("<option value=\"{0}\">{0}</option>", DS1.Tables[0].Rows[a]["OrderNum"].ToString()));
                                // 
                                Strings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                            }
                        }
                    }   
                }
                else
                {
                    for (int i = 0; i < GetPlan.PlanStaPrice.Length; i++)
                    {
                        Strings.Append(string.Format("<li class=priceli tps=SellPrice tag={0} id=P{0}><div class=ftype>{1}</div><div class=fname>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", GetPlan.PlanStaPrice[i].PriceId, GetPlan.PlanStaPrice[i].PriceType, GetPlan.PlanStaPrice[i].PriceName, GetPlan.PlanStaPrice[i].Price));
                        Strings.Append("<div class=fnum><select class=psel>");
                        EditNum = -1;
                        for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                        {
                            if (DS.Tables[0].Rows[a]["PriceType"].ToString() == "SellPrice" && DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanStaPrice[i].PriceId) EditNum = Convert.ToInt32(DS.Tables[0].Rows[a]["OrderNums"].ToString());
                        }
                        Strings.Append(EditPrceDropList(OrderNums, EditNum));
                        Strings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                    }
                }
                
                Strings.Append("</ul>");
                Strings.Append("</div></div>");

                ////////////////////////////////////////////////////////////

                if (GetPlan.PlanExtPrice != null)
                {

                    ExtStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>可选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    ExtStrings.Append("<ul id=ExtPrice class=price>");
                    ExtStrings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    ExtStrings.Append("");

                    NeedStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>必选费用<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    NeedStrings.Append("<ul id=ExtPrice class=price>");
                    NeedStrings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    NeedStrings.Append("");

                    string ExtType = "";
                    for (int i = 0; i < GetPlan.PlanExtPrice.Length; i++)
                    {
                        switch (GetPlan.PlanExtPrice[i].InfoId)
                        {
                            case "1":
                                ExtType = "单房差";
                                break;
                            case "2":
                                ExtType = "自费项目";
                                break;
                            case "3":
                                ExtType = "小费";
                                break;
                            case "4":
                                ExtType = "其他费用";
                                break;
                            case "5":
                                ExtType = "保险费用";
                                break;
                            case "6":
                                ExtType = "机票税金";
                                break;
                            default:
                                break;
                        }

                        if (GetPlan.PlanExtPrice[i].OnlineNeeds == "0")
                        {
                            ExtStrings.Append(string.Format("<li class=priceli tps=ExtPrice tag={0} id=E{0}><div class=ftype>{1}</div><div class=fname>{2} {3}</div><div class=fprice>&yen;<span class=sellprice>{4}</span></div>", GetPlan.PlanExtPrice[i].PriceId, ExtType, GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName, GetPlan.PlanExtPrice[i].Price));
                            ExtStrings.Append("<div class=fnum><select class=psel>");
                            EditNum = -1;
                            for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                            {
                                if (DS.Tables[0].Rows[a]["PriceType"].ToString() == "ExtPrice" && DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanExtPrice[i].PriceId) EditNum = Convert.ToInt32(DS.Tables[0].Rows[a]["OrderNums"].ToString());
                            }
                            ExtStrings.Append(EditPrceDropList(OrderNums,EditNum));
                            ExtStrings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        }
                        else
                        {
                            NeedPrice = "1";
                            NeedStrings.Append(string.Format("<li class=priceli tps=ExtPrice tag={0} id=E{0}><div class=ftype>{1}</div><div class=fname>{2} {3}</div><div class=fprice>&yen;<span class=sellprice>{4}</span></div>", GetPlan.PlanExtPrice[i].PriceId, "必选费用", GetPlan.PlanExtPrice[i].PriceType, GetPlan.PlanExtPrice[i].PriceName, GetPlan.PlanExtPrice[i].Price));
                            NeedStrings.Append("<div class=fnum><select class=psel>");
                            EditNum = -1;
                            for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                            {
                                if (DS.Tables[0].Rows[a]["PriceType"].ToString() == "ExtPrice" && DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanExtPrice[i].PriceId) EditNum = Convert.ToInt32(DS.Tables[0].Rows[a]["OrderNums"].ToString());
                            }
                            NeedStrings.Append(EditPrceDropList(OrderNums, EditNum));
                            NeedStrings.Append("</select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        }
                    }

                    ExtStrings.Append("</ul>");
                    ExtStrings.Append("</div></div>");

                    NeedStrings.Append("</ul>");
                    NeedStrings.Append("</div></div>");
                    if (NeedPrice == "0") NeedStrings.Clear();
                }

                //优惠政策
                if (GetPlan.PlanRebatePrice != null)
                {

                    RebateStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>促销优惠<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
                    RebateStrings.Append("<ul id=ExtPrice class=price>");
                    RebateStrings.Append("<li class=cur><div class=ttype>优惠类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>份数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
                    RebateStrings.Append("");
                    
                    string ExtType = "";
                    EditNum = 0;
                    int OldNum = OrderNums;
                    for (int i = 0; i < GetPlan.PlanRebatePrice.Length; i++)
                    {
                        OrderNums = OldNum;
                        switch (GetPlan.PlanRebatePrice[i].PriceType)
                        {
                            case "1":
                                ExtType = "下单立减";
                                OrderNums = 1;
                                EditNum = 1;
                                break;
                            case "2":
                                ExtType = "每人立减";
                                EditNum = OrderNums;
                                break;
                            case "3":
                                ExtType = "立享优惠";
                                double aa = (OrderNums / MyConvert.ConToInt(GetPlan.PlanRebatePrice[i].Nums));
                                OrderNums = MyConvert.ConToInt(Math.Floor(aa).ToString());
                                EditNum = OrderNums;
                                break;
                            default:
                                break;
                        }
                        RebateStrings.Append(string.Format("<li class=priceli tps=Rebate tag={0} id=R{0}><div class=ftype>{1}</div><div class=fname>{2}</div><div class=fprice><span class=PrePrice>&yen;</span> <span class=PrePrice>-{3}</span><span class=\"sellprice hide\">-{3}</span></div>", GetPlan.PlanRebatePrice[i].PriceId, ExtType, GetPlan.PlanRebatePrice[i].PriceName, GetPlan.PlanRebatePrice[i].Price));
                        //RebateStrings.Append(string.Format("<li class=priceli tps=ExtPrice tag={0} id=E{0}><div class=ftype>{1}</div><div class=fname>{2} {3}</div><div class=fprice>&yen;<span class=sellprice>{4}</span></div>", "0", ExtType, GetPlan.PlanRebatePrice[i].PriceType, GetPlan.PlanRebatePrice[i].PriceName, GetPlan.PlanRebatePrice[i].Price));
                        RebateStrings.Append("<div class=fnum><select class=psel>");
                        EditNum = -1;
                        for (int a = 0; a < DS.Tables[0].Rows.Count; a++)
                        {
                            if (DS.Tables[0].Rows[a]["PriceType"].ToString() == "Rebate" && DS.Tables[0].Rows[a]["PriceId"].ToString() == GetPlan.PlanRebatePrice[i].PriceId) EditNum = Convert.ToInt32(DS.Tables[0].Rows[a]["OrderNums"].ToString());
                        }
                        RebateStrings.Append(EditPrceDropList(OrderNums, EditNum));
                        RebateStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
                        
                    }

                    RebateStrings.Append("</ul>");
                    RebateStrings.Append("</div></div>");
                }
            }
            else
            {
                return NoPrice;
            }

            //NeedStrings.
            return string.Format("{0}{1}{2}{3}", Strings.ToString(), NeedStrings.ToString(), ExtStrings.ToString(), RebateStrings.ToString());
        }

        public static string EditPrceDropList(int OrderNums,int EditNum)
        {
            string OptionHtml = "";
            for (int i = 0; i <= OrderNums; i++)
            {
                if (i == EditNum)
                {
                    OptionHtml += string.Format("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                }
                else
                {
                    OptionHtml += string.Format("<option value=\"{0}\">{0}</option>", i);
                }
            }
            return OptionHtml;
        }

        public static string GetGuestList(string ProductType, string ProductClass, int OrderNums, string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select * from OL_GuestInfo where OrderId='{0}'", OrderId);
            string OptionHtml;

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string sex1, sex2;
                string pass1, pass2, pass3;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    sex1 = "";
                    sex2 = "";
                    pass1 = "";
                    pass2 = "";
                    pass3 = "";
                    if (DS.Tables[0].Rows[i]["Sex"].ToString() == "M")
                    { sex1 = "selected=\"selected\""; }
                    else
                    { sex2 = "selected=\"selected\""; }
                    switch (DS.Tables[0].Rows[i]["PassType"].ToString())
                    {
                        case "P":
                            pass1 = "selected=\"selected\"";
                            break;
                        case "I":
                            pass2 = "selected=\"selected\"";
                            break;
                        case "D":
                            pass3 = "selected=\"selected\"";
                            break;
                        default:
                            break;
                    }
                    OptionHtml = GetDrpOption(DS.Tables[0].Rows[i]["IdType"].ToString(), ProductType, ProductClass);
                    Strings.Append("<div class=guest>");
                    Strings.Append(string.Format("<ul id=GuestInfo{0} class=order><li class=cur>第{0}位游客 <a class=icon_del href=\"javascript:void(0);\">清空填写内容</a></li>", i + 1));
                    Strings.Append(string.Format("<li><div class=oname><span class=xh>*</span>姓名：</div><div class=ginfo><input type=\"text\" class=Guest_Name name='ipt_name{1}' maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["GuestName"].ToString(),i));
                    Strings.Append(string.Format("<div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style=\"width: 60px\"><option value=\"M\" {0}>男</option><option value=\"F\" {1}>女</option></select>&nbsp;</div></li>",sex1,sex2));

                    Strings.Append(string.Format("<li><div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType name='ipt_IdType{1}' style=\"width: 155px\">{0}</select>&nbsp;</div>", OptionHtml,i));
                    Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件号码：</div><div class=ginfo><input  type=\"text\" class=Guest_IdNum name='ipt_IdNum{1}' maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div></li>", DS.Tables[0].Rows[i]["IdNumber"].ToString(),i));

                    Strings.Append(string.Format("<li><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Tel"].ToString(),i));
                    Strings.Append(string.Format("<div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type=\"text\" name='ipt_birthday{1}' maxlength=\"10\" class=Guest_BirthDay style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["BirthDay"],i));

                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" name='ipt_ename{1}' class=Guest_EnName maxlength=\"30\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["GuestEnName"].ToString(),i));
                    Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件有效期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" name='ipt_passend{1}' class=Guest_PassEnd style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["PassEnd"],i));
                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\" {0}>因私护照(P)</option><option value=\"I\" {1}>因公护照(I)</option><option value=\"D\" {2}>外交护照(D)</option></select>&nbsp;</div>",pass1,pass2,pass3));
                    Strings.Append(string.Format("<div class=oname><span class=xh>*</span>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" name='ipt_passbgn{1}' class=Guest_PassBgn style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["PassBgn"],i));

                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Sign"].ToString(),i));
                    Strings.Append(string.Format("<div class=oname><span class=xh>*</span>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{1}' type=\"text\" maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div></li>", DS.Tables[0].Rows[i]["Home"].ToString(),i));
                    Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid type=\"hidden\" value=\"0\"/><input class=Guest_Roomid name='ipt_roomid{0}' type=\"hidden\" value=\"0\"/><input class=Guest_Listid name='ipt_listidid{0}' type=\"hidden\" value=\"0\"/></li>",i));
                    Strings.Append("</ul></div>");
                }
            }
            else
            { 
                OptionHtml = GetDrpOption("0",ProductType, ProductClass);
                for (int i = 1; i <= OrderNums; i++)
                {
                    Strings.Append("<div class=guest>");
                    Strings.Append(string.Format("<ul id=GuestInfo{0} class=order><li class=cur>第{0}位游客 <a class=icon_del href=\"javascript:void(0);\">清空填写内容</a></li>", i));
                    Strings.Append(string.Format("<li><div class=oname><span class=xh>*</span>姓名：</div><div class=ginfo><input type=\"text\" class=Guest_Name name='ipt_name{0}' maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style=\"width: 60px\"><option value=\"M\">男</option><option value=\"F\">女</option></select>&nbsp;</div></li>",i));
                    Strings.Append(string.Format("<li><div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType name='ipt_idtype{1}' style=\"width: 155px\">{0}</select>&nbsp;</div><div class=oname><span class=xh>*</span>证件号码：</div><div class=ginfo><input  type=\"text\" class=Guest_IdNum name='ipt_idnum{1}' maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div></li>",OptionHtml,i));
                    Strings.Append(string.Format("<li><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{0}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_BirthDay name='ipt_birthday{0}' style=\"width: 80px\"/>&nbsp;<span class=hs>示例:1977-01-31</span></div></li>",i));
                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_EnName name='ipt_enname{0}' maxlength=\"30\" style=\"width: 150px\" />&nbsp;</div><div class=oname><span class=xh>*</span>证件有效期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassEnd name='ipt_passend{0}' style=\"width: 80px\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>",i));
                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\">因私护照(P)</option><option value=\"I\">因公护照(I)</option><option value=\"D\">外交护照(D)</option></select>&nbsp;</div><div class=oname><span class=xh>*</span>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassBgn name='ipt_passbgn{0}' style=\"width: 80px\"/>&nbsp;<span class=hs>示例:1977-01-31</span></div></li>",i));
                    Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{0}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname><span class=xh>*</span>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{0}' type=\"text\" maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div></li>",i));
                    Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid type=\"hidden\" value=\"0\"/><input class=Guest_Roomid name='ipt_roomid{0}' type=\"hidden\" value=\"0\"/><input class=Guest_Listid name='ipt_listid{0}' type=\"hidden\" value=\"0\"/></li>",i));
                    Strings.Append("</ul></div>");                
                }
            }            
            return Strings.ToString();
        }

        public static string GetCruisesGuestList(string ProductType, string ProductClass, int OrderNums, string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            string OptionHtml;
            int Row = 1;
            string BedType = "";//大床或双床选择
            string SqlQueryText = string.Format("select * from CR_RoomList where OrderId='{0}'", OrderId);
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                string css = "";
                SqlQueryText = string.Format("select * from OL_GuestInfo where OrderId='{0}'", OrderId);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    string sex1, sex2;
                    string pass1, pass2, pass3;
                    string selectit1, selectit2;
                    int cssflag;
                    for (int a = 0; a < DS1.Tables[0].Rows.Count; a++)
                    {
                        cssflag = 0;
                        css = "";
                        selectit1 = "";
                        selectit2 = "";
                        BedType = "";
                        //if (DS1.Tables[0].Rows[a]["BedType"].ToString() == "0")
                        //{
                        //    BedType = "<select class=\"bedtype\" name=\"BedType\" class=\"hide\"><option selected=\"selected\" value=\"0\">不选</option></select>";
                        //}
                        //else
                        //{
                        //    if (DS1.Tables[0].Rows[a]["BedType"].ToString() == "1")
                        //    {
                        //        selectit1 = "selected=\"selected\"";
                        //    }
                        //    else
                        //    {
                        //        selectit2 = "selected=\"selected\"";
                        //    }
                        //    BedType = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color=\"#E56700\">床型要求：</font><select class=\"bedtype\" name=\"BedType\" style=\"width:50px;\"><option " + selectit1 + " value=\"1\">双床</option><option " + selectit2 + " value=\"2\">大床</option></select>";
                        //}

                        if (DS1.Tables[0].Rows[a]["BedType"].ToString() == "1")
                        {
                            selectit1 = "selected=\"selected\"";
                        }
                        else
                        {
                            selectit2 = "selected=\"selected\"";
                        }
                        BedType = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color=\"#E56700\">第一、二人床型要求：</font><select class=\"bedtype\" name=\"BedType\" style=\"width:60px;\"><option " + selectit1 + " value=\"1\">双床</option><option " + selectit2 + " value=\"2\">大床</option></select>";
                       

                        Strings.Append("<div class=guest>");
                        Strings.Append(string.Format("<ul class=order id=GuestInfo{0}><li class=cur id=li{2} tgs=\"{2}|{3}\">第{0}间 {1}{4}</li>", a + 1, DS1.Tables[0].Rows[a]["roomname"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), DS1.Tables[0].Rows[a]["peoples"].ToString(), BedType));
                        
                        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                        {
                            if (DS.Tables[0].Rows[i]["listid"].ToString() == DS1.Tables[0].Rows[a]["id"].ToString())
                            {
                                sex1 = "";
                                sex2 = "";
                                pass1 = "";
                                pass2 = "";
                                pass3 = "";
                                if (DS.Tables[0].Rows[i]["Sex"].ToString() == "M")
                                { sex1 = "selected=\"selected\""; }
                                else
                                { sex2 = "selected=\"selected\""; }
                                switch (DS.Tables[0].Rows[i]["PassType"].ToString())
                                {
                                    case "P":
                                        pass1 = "selected=\"selected\"";
                                        break;
                                    case "I":
                                        pass2 = "selected=\"selected\"";
                                        break;
                                    case "D":
                                        pass3 = "selected=\"selected\"";
                                        break;
                                    default:
                                        break;
                                }
                                OptionHtml = GetDrpOption(DS.Tables[0].Rows[i]["IdType"].ToString(), ProductType, "Cruises");

                                //Strings.Append(string.Format("<span class=GuestDiv id=GuestDiv{0}>", DS.Tables[0].Rows[i]["id"].ToString()));

                                //Strings.Append(string.Format("<li id=GuestInfo{0} class=iptchk><div class=oname1>姓名：</div><div class=ginfo1><input name='ipt_name{2}' type=\"text\" class=Guest_Name maxlength=\"20\" style=\"width: 90px\" value=\"{1}\" />&nbsp;&nbsp;&nbsp;", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["GuestName"].ToString(),i));
                                //Strings.Append(string.Format("证件号码：<input  type=\"text\" class=Guest_IdNum name='ipt_idnum{1}' maxlength=\"20\" style=\"width: 130px\" value=\"{0}\"/></div>", DS.Tables[0].Rows[i]["IdNumber"].ToString(),i));
                                //Strings.Append(string.Format("<div class=oname1>性别：</div><div class=ginfo1><select class=Guest_Sex style=\"width: 40px\"><option value=\"M\" {0}>男</option><option value=\"F\" {1}>女</option></select>&nbsp;&nbsp;&nbsp;", sex1, sex2));
                                //Strings.Append(string.Format("出生日期：<input type=\"text\" maxlength=\"10\" class=\"Guest_BirthDay li{2} RoomChk{3}\" name='ipt_birthday{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" /></div></li>", DS.Tables[0].Rows[i]["BirthDay"], i, DS1.Tables[0].Rows[a]["id"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString()));

                                ////Strings.Append(string.Format("<li><div class=oname1>姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_Name maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["GuestName"].ToString()));
                                ////Strings.Append(string.Format("<div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style=\"width: 60px\"><option value=\"M\" {0}>男</option><option value=\"F\" {1}>女</option></select>&nbsp;</div></li>", sex1, sex2));
                                ////Strings.Append(string.Format("<li><div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_BirthDay style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div>", DS.Tables[0].Rows[i]["BirthDay"]));
                                ////Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件号码：</div><div class=ginfo><input  type=\"text\" class=Guest_IdNum maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div></li>", DS.Tables[0].Rows[i]["IdNumber"].ToString()));

                                //Strings.Append(string.Format("<li class=hide><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Tel"].ToString(),i));
                                //Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType style=\"width: 155px\">{0}</select>&nbsp;</div></li>", OptionHtml));

                                //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_EnName name='ipt_enname{1}' maxlength=\"30\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["GuestEnName"].ToString(),i));
                                //Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件有效期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassEnd name='ipt_passend{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["PassEnd"],i));
                                //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\" {0}>因私护照(P)</option><option value=\"I\" {1}>因公护照(I)</option><option value=\"D\" {2}>外交护照(D)</option></select>&nbsp;</div>", pass1, pass2, pass3));
                                //Strings.Append(string.Format("<div class=oname><span class=xh>*</span>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassBgn name='ipt_passbgn{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["PassBgn"],i));

                                //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Sign"].ToString(),i));
                                //Strings.Append(string.Format("<div class=oname><span class=xh>*</span>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{1}' type=\"text\" maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div></li>", DS.Tables[0].Rows[i]["Home"].ToString(),i));

                                //Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid type=\"hidden\" value=\"{0}\"/><input class=Guest_Roomid name='ipt_roomid{3}' type=\"hidden\" value=\"{1}\"/><input class=Guest_Listid name='ipt_listid{3}' type=\"hidden\" value=\"{2}\"/></li>", DS1.Tables[0].Rows[a]["allotid"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), i));
                                //Strings.Append("</span>");
                                if (cssflag>0) css = "iptchk";
                                Strings.Append(string.Format("<span class=GuestDiv id=GuestDiv{0}>", DS.Tables[0].Rows[i]["id"].ToString()));

                                Strings.Append(string.Format("<li id=GuestInfo{0} class={3}><div class=oname><span class=xh>*</span><b>姓名：</b></div><div class=ginfo><input name='ipt_name{2}' type=\"text\" class=Guest_Name maxlength=\"20\" style=\"width: 150px\" value=\"{1}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["GuestName"].ToString(), i, css));
                                Strings.Append(string.Format("<div class=oname>护照号码：</div><div class=ginfo><input  type=\"text\" class=Guest_IdNum name='ipt_idnum{1}' maxlength=\"20\" style=\"width: 150px\" value=\"{0}\"/></div></li>", DS.Tables[0].Rows[i]["IdNumber"].ToString(), i));
                                Strings.Append(string.Format("<li><div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style=\"width: 40px\"><option value=\"M\" {0}>男</option><option value=\"F\" {1}>女</option></select>&nbsp;</div>", sex1, sex2));
                                Strings.Append(string.Format("<div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=\"Guest_BirthDay li{2} RoomChk{3}\" name='ipt_birthday{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["BirthDay"], i, DS1.Tables[0].Rows[a]["id"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString()));

                                Strings.Append(string.Format("<li class=hide><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Tel"].ToString(), i));
                                Strings.Append(string.Format("<div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType style=\"width: 155px\">{0}</select>&nbsp;</div></li>", OptionHtml));

                                Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_EnName name='ipt_enname{1}' maxlength=\"30\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["GuestEnName"].ToString(), i));
                                Strings.Append(string.Format("<div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\" {0}>因私护照(P)</option><option value=\"I\" {1}>因公护照(I)</option><option value=\"D\" {2}>外交护照(D)</option></select>&nbsp;</div></li>", pass1, pass2, pass3));
                                Strings.Append(string.Format("<li><div class=oname>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassBgn name='ipt_passbgn{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div>", DS.Tables[0].Rows[i]["PassBgn"], i));
                                Strings.Append(string.Format("<div class=oname>有效期止：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassEnd name='ipt_passend{1}' style=\"width: 80px\" value=\"{0:yyyy-MM-dd}\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS.Tables[0].Rows[i]["PassEnd"], i));

                                Strings.Append(string.Format("<li ><div class=oname>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div>", DS.Tables[0].Rows[i]["Sign"].ToString(), i));
                                Strings.Append(string.Format("<div class=oname>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{1}' type=\"text\" maxlength=\"20\" style=\"width: 150px\" value=\"{0}\" />&nbsp;</div></li>", DS.Tables[0].Rows[i]["Home"].ToString(), i));

                                Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid type=\"hidden\" value=\"{0}\"/><input class=Guest_Roomid name='ipt_roomid{3}' type=\"hidden\" value=\"{1}\"/><input class=Guest_Listid name='ipt_listid{3}' type=\"hidden\" value=\"{2}\"/></li>", DS1.Tables[0].Rows[a]["allotid"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), i));
                                Strings.Append("</span>");
                                cssflag++;
                            }

                        }
                        Strings.Append("</ul></div>");
                        Row++;
                    }
                    
                }
                else
                {
                    OptionHtml = GetDrpOption("0", ProductType, "Cruises");
                    for (int a = 0; a < DS1.Tables[0].Rows.Count; a++)
                    {
                        BedType = "";
                        //if (DS1.Tables[0].Rows[a]["berth"].ToString() == "2")
                        //{
                        //    BedType = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color=\"#E56700\">第一、二人床型要求：</font><select class=\"bedtype\" name=\"BedType\" style=\"width:50px;\"><option selected=\"selected\" value=\"1\">双床</option><option value=\"2\">大床</option></select>";
                        //}
                        //else
                        //{
                        //    BedType = "<select class=\"bedtype\" name=\"BedType\" class=\"hide\"><option selected=\"selected\" value=\"0\">不选</option></select>";
                        //}
                        BedType = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color=\"#E56700\">第一、二人床型要求：</font><select class=\"bedtype\" name=\"BedType\" style=\"width:60px;\"><option selected=\"selected\" value=\"1\">双床</option><option value=\"2\">大床</option></select>";
                        
                        Strings.Append("<div class=guest>");
                        Strings.Append(string.Format("<ul class=order><li class=cur id=li{2} tgs=\"{2}|{3}\">第{0}间 {1}{4}</li>", a + 1, DS1.Tables[0].Rows[a]["roomname"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), DS1.Tables[0].Rows[a]["peoples"].ToString(), BedType));
                        for (int i = 1; i <= Convert.ToInt32(DS1.Tables[0].Rows[a]["peoples"].ToString()); i++)
                        {
                            //Strings.Append(string.Format("<span class=GuestDiv id=GuestDiv{0}{1}>", DS1.Tables[0].Rows[a]["id"].ToString(), i));

                            //Strings.Append(string.Format("<li id=GuestInfo{0}{1} class=iptchk><div class=oname1>姓名：</div><div class=ginfo1><input  type=\"text\" class=Guest_Name name='ipt_name{1}' maxlength=\"20\" style=\"width: 90px\"/>&nbsp;&nbsp;&nbsp;证件号码：<input  type=\"text\" class=Guest_IdNum name='ipt_idnum{1}' maxlength=\"20\" style=\"width: 130px\"/></div><div class=oname1>性别：</div><div class=ginfo1><select class=Guest_Sex style=\"width: 40px\"><option value=\"M\">男</option><option value=\"F\">女</option></select>&nbsp;&nbsp;&nbsp;出生日期：<input type=\"text\" maxlength=\"10\" class=\"Guest_BirthDay li{0} RoomChk{2}\" name='ipt_birthday{1}' style=\"width: 80px\"/></div></li>", DS1.Tables[0].Rows[a]["id"].ToString(), i, DS1.Tables[0].Rows[a]["roomid"].ToString()));

                            //Strings.Append(string.Format("<li class=hide><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType style=\"width: 155px\">{0}</select>&nbsp;</div></li>", OptionHtml,i));
                            //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_EnName name='ipt_enname{0}' maxlength=\"30\" style=\"width: 150px\" />&nbsp;</div><div class=oname><span class=xh>*</span>证件有效期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassEnd name='ipt_passend{0}' style=\"width: 80px\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>",i));
                            //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\">因私护照(P)</option><option value=\"I\">因公护照(I)</option><option value=\"D\">外交护照(D)</option></select>&nbsp;</div><div class=oname><span class=xh>*</span>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassBgn name='ipt_passbgn{0}' style=\"width: 80px\"/>&nbsp;<span class=hs>示例:1977-01-31</span></div></li>",i));
                            //Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{0}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname><span class=xh>*</span>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{0}' type=\"text\" maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div></li>",i));
                            //Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid name='ipt_allotid{3}' type=\"hidden\" value=\"{0}\"/><input class=Guest_Roomid name='ipt_roomid{3}' type=\"hidden\" value=\"{1}\"/><input class=Guest_Listid name='ipt_listid{3}' type=\"hidden\" value=\"{2}\"/></li>", DS1.Tables[0].Rows[a]["allotid"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), i));
                            //Strings.Append("</span>");
                            css = "iptchk";
                            if (i == 1) css = "";
                            Strings.Append(string.Format("<span class=GuestDiv id=GuestDiv{0}{1}>", DS1.Tables[0].Rows[a]["id"].ToString(), i));
                            Strings.Append(string.Format("<li id=GuestInfo{0}{1} class={3}><div class=oname><span class=xh>*</span><b>姓名：</b></div><div class=ginfo><input  type=\"text\" class=Guest_Name name='ipt_name{1}' maxlength=\"20\" style=\"width: 150px\"/></div><div class=oname>护照号码：</div><div class=ginfo><input  type=\"text\" class=Guest_IdNum name='ipt_idnum{1}' maxlength=\"20\" style=\"width: 150px\"/></div></li><li><div class=oname>性别：</div><div class=ginfo><select class=Guest_Sex style=\"width: 40px\"><option value=\"M\">男</option><option value=\"F\">女</option></select>&nbsp;</div><div class=oname><span class=xh>*</span>出生日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=\"Guest_BirthDay li{0} RoomChk{2}\" name='ipt_birthday{1}' style=\"width: 80px\"/>&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", DS1.Tables[0].Rows[a]["id"].ToString(), i, DS1.Tables[0].Rows[a]["roomid"].ToString(),css));

                            Strings.Append(string.Format("<li class=hide><div class=oname>联系电话：</div><div class=ginfo><input class=Guest_Tel name='ipt_tel{1}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname><span class=xh>*</span>证件类型：</div><div class=ginfo><select class=Guest_IdType style=\"width: 155px\">{0}</select>&nbsp;</div></li>", OptionHtml, i));
                            Strings.Append(string.Format("<li class=hide><div class=oname><span class=xh>*</span>拼音姓名：</div><div class=ginfo><input  type=\"text\" class=Guest_EnName name='ipt_enname{0}' maxlength=\"30\" style=\"width: 150px\" />&nbsp;</div><div class=oname>护照类型：</div><div class=ginfo><select class=Guest_PassType style=\"width: 155px\"><option value=\"P\">因私护照(P)</option><option value=\"I\">因公护照(I)</option><option value=\"D\">外交护照(D)</option></select>&nbsp;</div></li>", i));
                            Strings.Append(string.Format("<li><div class=oname>签发日期：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassBgn name='ipt_passbgn{0}' style=\"width: 80px\"/>&nbsp;<span class=hs>示例:1977-01-31</span></div><div class=oname>有效期止：</div><div class=ginfo><input type=\"text\" maxlength=\"10\" class=Guest_PassEnd name='ipt_passend{0}' style=\"width: 80px\" />&nbsp;<span class=hs>示例:1977-01-31</span></div></li>", i));
                            Strings.Append(string.Format("<li><div class=oname>签发地：</div><div class=ginfo><input class=Guest_Sign name='ipt_sign{0}' type=\"text\"  maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div><div class=oname>出生地：</div><div class=ginfo><input class=Guest_Home name='ipt_home{0}' type=\"text\" maxlength=\"20\" style=\"width: 150px\"/>&nbsp;</div></li>", i));
                            Strings.Append(string.Format("<li class=hide><input class=Guest_Allotid name='ipt_allotid{3}' type=\"hidden\" value=\"{0}\"/><input class=Guest_Roomid name='ipt_roomid{3}' type=\"hidden\" value=\"{1}\"/><input class=Guest_Listid name='ipt_listid{3}' type=\"hidden\" value=\"{2}\"/></li>", DS1.Tables[0].Rows[a]["allotid"].ToString(), DS1.Tables[0].Rows[a]["roomid"].ToString(), DS1.Tables[0].Rows[a]["id"].ToString(), i));
                            Strings.Append("</span>");
                        }
                        Strings.Append("</ul></div>");
                    }
                }

            }
            return Strings.ToString();
        }

        public static string GetDrpOption(string a, string ProductType, string ProductClass)
        {
            
            StringBuilder Strings = new StringBuilder();
            if (ProductType == "InLand")
            {
                Strings.Append(GetIdType(a, "AllType"));
            }
            else {
                switch (ProductClass)
                {
                    case "227":
                        Strings.Append(GetIdType(a, "HongKong"));
                        break;
                    case "851":
                        Strings.Append(GetIdType(a, "TaiWan"));
                        break;
                    case "1062":
                        Strings.Append(GetIdType(a, "AllType"));
                        break;
                    case "Cruises":
                        Strings.Append(GetIdType(a, "Cruises"));
                        break;
                    default:
                        Strings.Append(GetIdType(a, "OutBound"));
                        break;
                }            
            }
            return Strings.ToString();
        }

        public static string GetOrderPrice(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_OrderPrice where OrderId='{0}' order by InputDate", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul class=price>");
                Strings.Append("<li class=cur><div class=ttype>费用类型</div><div class=tsname>费用说明</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div></li>");
                Strings.Append("");
                Strings.Append("");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div class=ftype>{0}</div><div class=fsname>{1}&nbsp;</div><div class=fprice>&yen;<span class=sellprice>{2}</span></div>", DS.Tables[0].Rows[i]["PriceName"].ToString(), DS.Tables[0].Rows[i]["PriceMemo"].ToString(), DS.Tables[0].Rows[i]["SellPrice"].ToString()));
                    Strings.Append(string.Format("<div class=fnum>{0}</div><div class=fsum>&yen;<span class=sumprice>{1}</span></div></li>", DS.Tables[0].Rows[i]["OrderNums"].ToString(), DS.Tables[0].Rows[i]["SumPrice"].ToString()));
                }
                Strings.Append("</ul>");
            }
            return Strings.ToString();
        }

        public static string GetOrderGuest(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_GuestInfo where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<ul class=guests>");
                Strings.Append("<li class=cur><div class=fname>姓名</div><div class=fename>英文姓名</div><div class=fbirth>出生日期</div><div class=ftype>证件类型</div><div class=fnum>证件号码</div></li>");
                Strings.Append("");
                Strings.Append("");
                string PType="";
                string gclass = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    gclass = "fname";
                    //if (DS.Tables[0].Rows[i]["flag"].ToString() == "1") gclass = "fname_t";
                    
                    PType = GetIdType(DS.Tables[0].Rows[i]["IdType"].ToString(), "SingleName");
                    Strings.Append(string.Format("<li><div class={3}>{0}</div><div class=fename>{1}</div><div class=fbirth>{2:yyyy-MM-dd}</div>", DS.Tables[0].Rows[i]["GuestName"].ToString(), DS.Tables[0].Rows[i]["GuestEnName"].ToString(), DS.Tables[0].Rows[i]["BirthDay"], gclass));
                    Strings.Append(string.Format("<div class=ftype>{0}</div><div id=pic class=fnum>{1}</div></li>", PType, DS.Tables[0].Rows[i]["IdNumber"].ToString()));
                }
                Strings.Append("</ul>");
            }
            return Strings.ToString();
        }

        public static string GetCuisesOrderGuest(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select flag,GuestName,Sex,IdNumber,BirthDay,visitid from OL_GuestInfo where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("select PriceMemo,PriceId,PriceType from OL_OrderPrice where OrderId='{0}' and PriceType='ShipVisit'", OrderId);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);

                Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
                Strings.Append("<tr class=tit1>");
                Strings.Append("<td width=\"15%\">姓名</td>");
                Strings.Append("<td width=\"5%\">性别</td>");
                Strings.Append("<td width=\"12%\">出生日期</td>");
                Strings.Append("<td width=\"23%\">证件号码</td>");
                Strings.Append("<td width=\"45%\">观光线路</td>");
                Strings.Append("</tr>");
                string gclass = "";
                string gcancel = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    gclass = "";
                    gcancel = "";
                    if (DS.Tables[0].Rows[i]["flag"].ToString() == "1")
                    {
                        gclass = " class=tit2";
                        gcancel = "*";
                    }

                    Strings.Append(string.Format("<tr{5}><td>{0}</td><td>{1}</td><td>{2:yyyy-MM-dd}</td><td>{3}</td><td><span class=Visit_Span>{4}</span></td></tr>",
                        gcancel + DS.Tables[0].Rows[i]["GuestName"].ToString(),
                        GetSex(DS.Tables[0].Rows[i]["Sex"].ToString()),
                        DS.Tables[0].Rows[i]["BirthDay"],
                        DS.Tables[0].Rows[i]["IdNumber"].ToString(),
                        GetVisitName(DS.Tables[0].Rows[i]["visitid"].ToString(), DS1.Tables[0]),
                        gclass
                    ));
                }
                Strings.Append("</table>");
            }
            return Strings.ToString();
        }

        public static string GetSex(string sex)
        {
            if (sex == "M")
            {
                return "男";
            }
            else
            {
                return "女";
            }
        }

        public static string GetVisitName(string ids, DataTable dt)
        {
            string visit = "";
            DataRow[] drs = dt.Select("PriceType='ShipVisit'");
            foreach (DataRow dr in drs)
            {
                if (ids.IndexOf("," + dr["PriceId"].ToString() + ",") > 0)
                {
                    visit += dr["PriceMemo"].ToString() + "、";
                }
            }
            if (visit.Length > 2) visit = visit.Substring(0, visit.Length - 1);
            return visit;
        }

        public static string GetVisitNameAndBusNo(string ids, DataTable dt)
        {
            string visit = "";
            DataRow[] drs = dt.Select("guestid='" + ids + "'");
            foreach (DataRow dr in drs)
            {
                visit += dr["visitname"].ToString() + " <b>" + dr["BusNo"].ToString() + "</b>、";
            }
            if (visit.Length > 2) visit = visit.Substring(0, visit.Length - 1);
            return visit;
        }

        public static string GetRoomName(string ids, DataTable dt)
        {
            string rooms = "";
            string bedtype = "";
            DataRow[] drs = dt.Select("id='" + ids + "'");
            foreach (DataRow dr in drs)
            {
                bedtype = "";
                //if (dr["roomno"].ToString() == "1") bedtype = "";
                if (dr["BedType"].ToString() == "2") bedtype = "(大床)";
                if (dr["roomno"].ToString().Length > 0)
                {
                    rooms += dr["roomname"].ToString() + bedtype + "<br>房号：<b>" + dr["roomno"].ToString() + "</b>";
                    if (dr["BookingNo"].ToString().Length > 0) rooms += "<br>Booking：<b>" + dr["BookingNo"].ToString() + "</b>";
                }
                else
                {
                    rooms += dr["roomname"].ToString() + bedtype;
                }
            }
            return rooms;
        }

        public static string GetOrderCuises(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_CuisesRoom where OrderId='{0}'", OrderId);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<div class=\"m detail\">");
                Strings.Append("<UL class=tab><LI class=curr>邮轮舱位<SPAN></SPAN></LI></UL>");
                Strings.Append("<div class=\"mc tabcon borders01\">");                

                Strings.Append("<ul class=guests>");
                Strings.Append("<li class=cur><div class=fename>舱位名称</div><div class=fname>间数</div><div class=fbirth>预订人数</div><div class=ftype>成人</div><div class=fnum>儿童</div></li>");
                Strings.Append("");
                Strings.Append("");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<li><div class=fename>{0}</div><div class=fname>{1}</div><div class=fbirth>{2}</div>", DS.Tables[0].Rows[i]["RoomName"].ToString(), DS.Tables[0].Rows[i]["RoomNum"].ToString(), DS.Tables[0].Rows[i]["OrderNum"]));
                    Strings.Append(string.Format("<div class=ftype>{0}</div><div id=pic class=fnum>{1}</div></li>", DS.Tables[0].Rows[i]["AdultNum"].ToString(), DS.Tables[0].Rows[i]["ChildNum"].ToString()));
                }
                Strings.Append("</ul>");
                Strings.Append("</div>");
                Strings.Append("</div>");
            }
            return Strings.ToString();
        }

        public static string GetOrderCuisesRoom(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");

            Strings.Append("<div id=Div1 class=\"m detail\">");
            Strings.Append("<UL class=tab><LI class=curr>房间和人数<SPAN></SPAN></LI></UL>");
            Strings.Append("<div class=\"mc tabcon borders01\">");
            Strings.Append("<div class=roomdivlist>");
            //Strings.Append("<DIV class=roomlistHead>双人间最少入住2人，不满2人需要补房差；三人间或四人间同舱的第3、第4位可享受价格优惠；</DIV>");
            Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
            Strings.Append("<tr class=tit1>");
            Strings.Append("<td width=\"30%\">房间类型</td>");
            Strings.Append("<td width=\"5%\">成人</td>");
            Strings.Append("<td width=\"5%\">儿童</td>");
            Strings.Append("<td width=\"5%\">房间数</td>");
            Strings.Append("<td width=\"10%\">第1、2人价格</td>");
            Strings.Append("<td width=\"10%\">第3成人价</td>");
            Strings.Append("<td width=\"10%\">第3儿童价</td>");
            Strings.Append("<td width=\"10%\">价格小计</td>");
            Strings.Append("</tr>");


            string SqlQueryText = string.Format("select * from CR_RoomOrder where OrderId='{0}'", OrderId);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}间</td><td class=tdn>&yen;{4}</td><td class=tdn>&yen;{5}</td><td class=tdn>&yen;{6}</td><td class=tds>&yen;{7}</td></tr>",
                        DS.Tables[0].Rows[i]["roomname"].ToString(),
                        DS.Tables[0].Rows[i]["adult"].ToString(),
                        DS.Tables[0].Rows[i]["childs"].ToString(),
                        DS.Tables[0].Rows[i]["rooms"].ToString(),
                        DS.Tables[0].Rows[i]["price"].ToString(),
                        DS.Tables[0].Rows[i]["thirdprice"].ToString(),
                        DS.Tables[0].Rows[i]["childprice"].ToString(),
                        DS.Tables[0].Rows[i]["gather"].ToString()
                    ));

                }
            }

            Strings.Append("</table>");
            Strings.Append("</div>");
            Strings.Append("</div>");
            Strings.Append("</div>");

            return Strings.ToString();
        }

        public static string GetTimePoint(string OrderId)
        {
            StringBuilder Strings = new StringBuilder();
            Strings.Append("");
            string SqlQueryText = string.Format("select * from OL_OrderLog where OrderId='{0}' order by LogTime", OrderId);
            string tp="";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("var JSONData = [");
                Strings.Append("");
                Strings.Append("");
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Strings.Append("{ ");
                    Strings.Append(string.Format("'date': '', 'title': '{0} &nbsp;&nbsp; {1}'", DS.Tables[0].Rows[i]["LogTime"].ToString(), DS.Tables[0].Rows[i]["LogContent"].ToString()));
                    Strings.Append("},");
                }


                //if (DS.Tables[0].Rows.Count < 4)
                //{
                //    for (int i = DS.Tables[0].Rows.Count + 1; i < 5; i++)
                //    {
                //        Strings.Append("{ ");
                //        Strings.Append("'date': '', 'title': '等待下一步订单业务处理'");
                //        Strings.Append("},");
                //    }
                //}
                //Strings.Append("{ ");
                //Strings.Append("'date': '', 'title': '完成订单，评价本次预订'");
                //Strings.Append("}");
                //Strings.Append("];");
                string aa = Strings.ToString();
                tp = aa.Substring(0, aa.Length - 1);
                tp += "];";

            }
            return tp;
        }

        public static void CouponGetAfterBuy(string UserId,string ProductClass,int OrderNums)
        {
            string SqlQueryText = "select * from View_Prefer where Id='" + UserId + "'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);

            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("select * from Pre_Policy where id='{0}'", ProductClass);

                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    List<string> Sql = new List<string>();
                    
                    for (int i = 0; i < OrderNums; i++)
                    {
                        String AutoId = Convert.ToString(CombineKeys.NewComb());
                        Sql.Add(string.Format("insert into Pre_Ticket (pid,uid,uno,par,amount,userid,begindate,enddate,inputdate,flag,deduction,range,product,UserEmail,UserName,pbdate,pedate,sellflag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
                            DS1.Tables[0].Rows[0]["id"].ToString(),
                            AutoId,
                            MyConvert.CreateVerifyCode(11)+i,
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
                            DS1.Tables[0].Rows[0]["sellflag"].ToString()
                        ));
                    }
                    string[] SqlQuery = Sql.ToArray();
                    MyDataBaseComm.Transaction(SqlQuery);
                }
            }
        }

        public static string GetCruisesGuestRoomSelect(string listid, string gno, DataTable dt)
        {
            StringBuilder Strings = new StringBuilder();
            DataRow[] drs = dt.Select("rankno>0");
            string guestname = "";
            string guestno = ",," + gno + ",";
            string chks = "";
            foreach (DataRow dr in drs)
            {
                chks = "";
                if (guestno.IndexOf("," + dr["rankno"].ToString() + ",") > 0)
                {
                    chks = "checked=\"checked\"";
                    guestname += dr["GuestName"].ToString() + "，";
                }
                Strings.Append(string.Format("<div><input class=\"ChkIt\" type=checkbox id=\"CB{0}-{1}\" name=CheckBox{0} tgs=\"{2}\" gid=\"{4}\" onclick=\"SelectIts(this,{0})\" value=\"{1}\" {3} />{2}</div>", listid, dr["rankno"].ToString(), dr["GuestName"].ToString(), chks, dr["id"].ToString()));
            }
            if (guestname.Length > 2) guestname = guestname.Substring(0, guestname.Length - 1);
            return string.Format("<div><input type='checkbox' onclick='chkall(this,{0})' name='chk_all' id='chk_all'>全选</div>", listid) + Strings.ToString();
            //return "";
        }

        public static string GetCruisesGuestNameList(string gno, DataTable dt)
        {
            StringBuilder Strings = new StringBuilder();
            DataRow[] drs = dt.Select("rankno>0");
            string guestname = "";
            string guestno = ",," + gno + ",";
            foreach (DataRow dr in drs)
            {
                if (guestno.IndexOf("," + dr["rankno"].ToString() + ",") > 0)
                {
                    guestname += dr["GuestName"].ToString() + "，";
                    //visit += dr["PriceMemo"].ToString() + "，";
                }
            }
            if (guestname.Length > 2) guestname = guestname.Substring(0, guestname.Length - 1);
            return guestname;
            //return "";
        }

        public static string GetCruisesGuestIdList(string gno, DataTable dt)
        {
            StringBuilder Strings = new StringBuilder();
            DataRow[] drs = dt.Select("rankno>0");
            string guestname = "";
            string guestno = ",," + gno + ",";
            foreach (DataRow dr in drs)
            {
                if (guestno.IndexOf("," + dr["rankno"].ToString() + ",") > 0)
                {
                    guestname += dr["id"].ToString() + "@";
                }
            }
            return guestname;
        }

        public static string GetCruisesVisitList(string OrderId,string flag)
        {
            //<div class=\"m detail\" ><UL class=tab><LI class=curr>岸上观光<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\"><div class=VisitList>
            //</div></div></div>

            StringBuilder Strings = new StringBuilder();
            string SqlQueryText;// = string.Format("select *,(select vtitle from CR_Visit where id=OL_OrderPrice.PriceId) as vtitle,(select days from CR_Visit where id=OL_OrderPrice.PriceId) as days from OL_OrderPrice where OrderId='{0}' and PriceType='ShipVisit'", OrderId);
            if (flag == "temp")
            {
                SqlQueryText = string.Format("select *,(select vtitle from CR_Visit where id=OL_TempPrice.PriceId) as vtitle,(select days from CR_Visit where id=OL_TempPrice.PriceId) as days from OL_TempPrice where OrderId='{0}' and PriceType='ShipVisit'", OrderId);
            }
            else
            {
                SqlQueryText = string.Format("select *,(select vtitle from CR_Visit where id=OL_OrderPrice.PriceId) as vtitle,(select days from CR_Visit where id=OL_OrderPrice.PriceId) as days from OL_OrderPrice where OrderId='{0}' and PriceType='ShipVisit'", OrderId);
            }
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("select id,GuestName,rankno,visitid from OL_GuestInfo where flag='0' and OrderId='{0}'", OrderId);
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                DataTable dt = DS1.Tables[0];
                int rows = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    rows = i;
                    if (i == 0)
                    {
                        Strings.Append(string.Format("<div class=\"m detail\" ><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\"><div class=\"VisitList Day{1}\" tgs=\"{0}\" tids=\"{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), DS.Tables[0].Rows[i]["days"].ToString()));
                        Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
                        Strings.Append("<tr class=tit1>");
                        Strings.Append("<td width=\"40%\">观光线路</td>");
                        Strings.Append("<td width=\"5%\">人数</td>");
                        Strings.Append("<td width=\"45%\">参加人员</td>");
                        Strings.Append("<td width=\"10%\">&nbsp;</td>");
                        Strings.Append("<td width=\"1%\">&nbsp;</td>");
                        Strings.Append("</tr>");

                        Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td><span class=Visit_Span id=\"Visit_Span{2}\">{5}</span></td><td><A class=order href=\"javascript:void(0)\" onclick=\"SelectGuest('{2}','0')\">选择人员</A></td><td><input id=\"V_GID{2}\" name=\"V_GID\" type=\"hidden\" value=\"{6}\"/><input id=\"V_ID{2}\" name=\"V_ID\" type=\"hidden\" value=\"{2}\"/><input id=\"V_NO{2}\" name=\"V_NO\" type=\"hidden\" value=\"{3}\"/><input id=\"V_NM{2}\" name=\"V_NM\" type=\"hidden\" value=\"{5}\"/><input id=\"V_Nums{2}\" name=\"V_Nums\" type=\"hidden\" value=\"{1}\"/></td></tr><tr class=\"hide htr\" id=h{2}><td colspan=\"5\"><div tgs={2} tns=\"{0}\" id=show{2} class=\"CheckBoxList\">{4}</div></td></tr>",
                            DS.Tables[0].Rows[i]["PriceMemo"].ToString(),
                            DS.Tables[0].Rows[i]["OrderNums"].ToString(),
                            DS.Tables[0].Rows[i]["PriceId"].ToString(),
                            DS.Tables[0].Rows[i]["guestnostring"].ToString(),
                            GetCruisesGuestRoomSelect(DS.Tables[0].Rows[i]["PriceId"].ToString(), DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                            GetCruisesGuestNameList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                            GetCruisesGuestIdList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt)
                        ));

                    }
                    else
                    {
                        if (DS.Tables[0].Rows[i]["days"].ToString() != DS.Tables[0].Rows[i - 1]["days"].ToString())
                        {
                            Strings.Append("</table></div></div></div>");
                            Strings.Append(string.Format("<div class=\"m detail\" ><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\"><div class=\"VisitList Day{1}\" tgs=\"{0}\" tids=\"{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), DS.Tables[0].Rows[i]["days"].ToString()));
                            Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
                            Strings.Append("<tr class=tit1>");
                            Strings.Append("<td width=\"40%\">观光线路</td>");
                            Strings.Append("<td width=\"5%\">人数</td>");
                            Strings.Append("<td width=\"44%\">参加人员</td>");
                            Strings.Append("<td width=\"10%\">&nbsp;</td>");
                            Strings.Append("<td width=\"1%\">&nbsp;</td>");
                            Strings.Append("</tr>");

                            Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td><span class=Visit_Span id=\"Visit_Span{2}\">{5}</span></td><td><A class=order href=\"javascript:void(0)\" onclick=\"SelectGuest('{2}','0')\">选择人员</A></td><td><input id=\"V_GID{2}\" name=\"V_GID\" type=\"hidden\" value=\"{6}\"/><input id=\"V_ID{2}\" name=\"V_ID\" type=\"hidden\" value=\"{2}\"/><input id=\"V_NO{2}\" name=\"V_NO\" type=\"hidden\" value=\"{3}\"/><input id=\"V_NM{2}\" name=\"V_NM\" type=\"hidden\" value=\"{5}\"/><input id=\"V_Nums{2}\" name=\"V_Nums\" type=\"hidden\" value=\"{1}\"/></td></tr><tr class=\"hide htr\" id=h{2}><td colspan=\"5\"><div tgs={2} tns=\"{0}\" id=show{2} class=\"CheckBoxList\">{4}</div></td></tr>",
                                DS.Tables[0].Rows[i]["PriceMemo"].ToString(),
                                DS.Tables[0].Rows[i]["OrderNums"].ToString(),
                                DS.Tables[0].Rows[i]["PriceId"].ToString(),
                                DS.Tables[0].Rows[i]["guestnostring"].ToString(),
                                GetCruisesGuestRoomSelect(DS.Tables[0].Rows[i]["PriceId"].ToString(), DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                                GetCruisesGuestNameList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                                GetCruisesGuestIdList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt)
                            ));

                        }
                        else
                        {
                            Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td><span class=Visit_Span id=\"Visit_Span{2}\">{5}</span></td><td><A class=order href=\"javascript:void(0)\" onclick=\"SelectGuest('{2}','0')\">选择人员</A></td><td><input id=\"V_GID{2}\" name=\"V_GID\" type=\"hidden\" value=\"{6}\"/><input id=\"V_ID{2}\" name=\"V_ID\" type=\"hidden\" value=\"{2}\"/><input id=\"V_NO{2}\" name=\"V_NO\" type=\"hidden\" value=\"{3}\"/><input id=\"V_NM{2}\" name=\"V_NM\" type=\"hidden\" value=\"{5}\"/><input id=\"V_Nums{2}\" name=\"V_Nums\" type=\"hidden\" value=\"{1}\"/></td></tr><tr class=\"hide htr\" id=h{2}><td colspan=\"5\"><div tgs={2} tns=\"{0}\" id=show{2} class=\"CheckBoxList\">{4}</div></td></tr>",
                                DS.Tables[0].Rows[i]["PriceMemo"].ToString(),
                                DS.Tables[0].Rows[i]["OrderNums"].ToString(),
                                DS.Tables[0].Rows[i]["PriceId"].ToString(),
                                DS.Tables[0].Rows[i]["guestnostring"].ToString(),
                                GetCruisesGuestRoomSelect(DS.Tables[0].Rows[i]["PriceId"].ToString(), DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                                GetCruisesGuestNameList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt),
                                GetCruisesGuestIdList(DS.Tables[0].Rows[i]["guestno"].ToString(), dt)
                            ));

                        }
                        if (i == DS.Tables[0].Rows.Count - 1)
                        {
                            Strings.Append("</table></div></div></div>");
                        }

                    }
                    if (DS.Tables[0].Rows.Count == 1)
                    {
                        Strings.Append("</table></div></div></div>");
                    }
                }
            }

            return Strings.ToString();


            ///////////////////////////////////////////
            //StringBuilder Strings = new StringBuilder();
            //string names, ids;
            //string SqlQueryText = string.Format("select *,(select days from CR_Visit where id=OL_OrderPrice.PriceId) as days from OL_OrderPrice where OrderId='{0}' and PriceType='ShipVisit'", OrderId);
            //DataSet DS = new DataSet();
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    SqlQueryText = string.Format("select id,GuestName,rankno,visitid from OL_GuestInfo where OrderId='{0}'", OrderId);
            //    DataSet DS1 = new DataSet();
            //    DS1.Clear();
            //    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            //    DataTable dt = DS1.Tables[0];

            //    //Strings.Append("<div style=\"width: 780px;PADDING:5px 10px 5px 10px;\" class=roomdiv>");
            //    Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"RoomSelectList\" style=\"width: 100%;\">");
            //    Strings.Append("<tr class=tit1>");
            //    Strings.Append("<td width=\"40%\">观光线路</td>");
            //    Strings.Append("<td width=\"5%\">人数</td>");
            //    Strings.Append("<td width=\"45%\">参加人员</td>");
            //    Strings.Append("<td width=\"10%\">&nbsp;</td>");
            //    Strings.Append("</tr>");

            //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //    {
            //        names = "";
            //        ids = "";
            //        DataRow[] drs = dt.Select("visitid=" + DS.Tables[0].Rows[0]["PriceId"].ToString());
            //        foreach (DataRow dr in drs)
            //        {
            //            names += dr["GuestName"].ToString() + ",";
            //            ids += dr["rankno"].ToString() + "@";
            //        }
            //        if (names.Length>1) names = names.Substring(0, names.Length - 1);
            //        //<input id="V_ID" name="V_ID" type="hidden" value="{2}"/><input id="V_NO" name="V_NO" type="hidden" value="{3}"/><input id="V_NM" name="V_NM" type="hidden" value="{4}"/>
            //        Strings.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td><span id=\"Visit_Span{2}\">{4}</span></td><td><A class=order href=\"javascript:void(0)\" onclick=\"SelectGuest('{2}')\">选择人员</A><input id=\"V_ID{2}\" name=\"V_ID\" type=\"hidden\" value=\"{2}\"/><input id=\"V_NO{2}\" name=\"V_NO\" type=\"hidden\" value=\"{3}\"/><input id=\"V_NM{2}\" name=\"V_NM\" type=\"hidden\" value=\"{4}\"/><input id=\"V_Nums{2}\" name=\"V_Nums\" type=\"hidden\" value=\"{1}\"/></td></tr><tr class=\"hide htr\" id=h{2}><td colspan=\"4\"><div tgs={2} tns=\"{0}\" id=show{2} class=\"CheckBoxList\"></div></td></tr>",
            //            DS.Tables[0].Rows[i]["PriceMemo"].ToString(),
            //            DS.Tables[0].Rows[i]["OrderNums"].ToString(),
            //            DS.Tables[0].Rows[i]["PriceId"].ToString(),
            //            ids,
            //            names
            //        ));
            //    }
            //    Strings.Append("</table>");
            //    //Strings.Append("</div>");
            //}
            //return Strings.ToString();




            //岸上观光
            //if (shipid > 0)
            //{
            //    string SqlQueryText;
            //    SqlQueryText = "select * from CR_Visit where lineid='" + Lineid + "' order by days";
            //    DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //    if (DS.Tables[0].Rows.Count > 0)
            //    {
            //        //ExtStrings.Append("<div class=\"m detail\"><UL class=tab><LI class=curr>岸上观光<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">");
            //        //ExtStrings.Append("<ul class=\"price Visit\">");
            //        //ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
            //        //ExtStrings.Append("");

            //        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //        {
            //            if (i == 0)
            //            {
            //                ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
            //                ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
            //                ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
            //                ExtStrings.Append("");

            //                ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

            //                ExtStrings.Append("<div class=fnum><select class=psel>");
            //                ExtStrings.Append(OptionHtml);
            //                ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

            //            }
            //            else
            //            {
            //                if (DS.Tables[0].Rows[i]["days"].ToString() != DS.Tables[0].Rows[i - 1]["days"].ToString())
            //                {
            //                    ExtStrings.Append("</ul>");
            //                    ExtStrings.Append("</div></div>");
            //                    ExtStrings.Append(string.Format("<div class=\"m detail\"><UL class=tab><LI class=curr>{0}<SPAN></SPAN></LI></UL><div class=\"mc tabcon borders01\">", DS.Tables[0].Rows[i]["vtitle"].ToString()));
            //                    ExtStrings.Append(string.Format("<ul class=\"price Visit\" tag=\"{0}\" id=\"vt{1}\">", DS.Tables[0].Rows[i]["vtitle"].ToString(), i));
            //                    ExtStrings.Append("<li class=cur><div class=ttype>类型</div><div class=tname>观光线路</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li>");
            //                    ExtStrings.Append("");

            //                    ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

            //                    ExtStrings.Append("<div class=fnum><select class=psel>");
            //                    ExtStrings.Append(OptionHtml);
            //                    ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

            //                }
            //                else
            //                {
            //                    ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=V{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), "岸上观光", DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

            //                    ExtStrings.Append("<div class=fnum><select class=psel>");
            //                    ExtStrings.Append(OptionHtml);
            //                    ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");

            //                }
            //                if (i == DS.Tables[0].Rows.Count - 1)
            //                {
            //                    ExtStrings.Append("</ul>");
            //                    ExtStrings.Append("</div></div>");
            //                }

            //            }

            //            //ExtStrings.Append(string.Format("<li class=priceli tps=ShipVisit tag={0} id=E{0} Cuises=0><div class=ftype>{1}</div><div class=fname title='{2}'>{2}</div><div class=fprice>&yen;<span class=sellprice>{3}</span></div>", DS.Tables[0].Rows[i]["id"].ToString(), DS.Tables[0].Rows[i]["vtitle"].ToString(), DS.Tables[0].Rows[i]["visitname"].ToString(), DS.Tables[0].Rows[i]["price"].ToString()));

            //            //ExtStrings.Append("<div class=fnum><select class=psel>");
            //            //ExtStrings.Append(OptionHtml);
            //            //ExtStrings.Append("</select></div><div class=fsum>&yen; <span class=sumprice>0</span></div><div id=pic class=fnpic></div></li>");
            //        }
            //        //ExtStrings.Append("</ul>");
            //        //ExtStrings.Append("</div></div>");
            //    }
            //}
            ////岸上观光结束
        }
    
    
    }
}