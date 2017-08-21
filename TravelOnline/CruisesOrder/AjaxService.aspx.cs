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
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Common;
using TravelOnline.Class.Purchase;

namespace TravelOnline.CruisesOrder
{
    public partial class AjaxService : System.Web.UI.Page
    {
        public Guid ucode;
        public decimal AllDamages; //退舱房或订单损失费
        public List<string> Sql;
        //public Decimal NewPrice, OldPrice;
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
                case "OrderSubmit":
                    OrderSubmit();
                    break;
                case "LoadVisitGuest":
                    LoadVisitGuest();
                    break;
                case "ThirdStep":
                    ThirdStep();
                    break;
                case "VisitSelect":
                    VisitSelect();
                    break;
                case "CancelCruisesRoom":
                    CancelCruisesRoom();
                    break;
                case "AdjustCruisesRoom":
                    AdjustCruisesRoom();
                    break;
                case "CruisesOrderCancel":
                    CruisesOrderCancel();
                    break;
                case "CruisesRoomDoCancel":
                    CruisesRoomDoCancel();
                    break;
                case "CruisesRoomDoAdjust":
                    CruisesRoomDoAdjust();
                    break;
                case "DoMisError":
                    DoMisError();
                    break;
                case "CancelMisError":
                    CancelMisError();
                    break;
                case "SetCruisesRoomNo":
                    SetCruisesRoomNo();
                    break;
                case "CruisesCheckInCancel":
                    CruisesCheckInCancel();
                    break;
                case "SetCruisesCarNo":
                    SetCruisesCarNo();
                    break;
                case "SelectCruisesBusNo":
                    SelectCruisesBusNo();
                    break;
                case "CruisesCarSeatCancel":
                    CruisesCarSeatCancel();
                    break;
                case "SetCruisesDinnerNo":
                    SetCruisesDinnerNo();
                    break;
                case "CruisesDinnerNoCancel":
                    CruisesDinnerNoCancel();
                    break;
                case "CruisesGroupAllotDinnerNo":
                    CruisesGroupAllotDinnerNo();
                    break;
                case "SetCruisesPlanNo":
                    SetCruisesPlanNo();
                    break;
                case "CruisesGroupAllotPlan":
                    CruisesGroupAllotPlan();
                    break;
                case "CruisesPlanNoCancel":
                    CruisesPlanNoCancel();
                    break;
                case "DeleteOrder":
                    DeleteOrder();
                    break;
                case "DinnerChange":
                    DinnerChange();
                    break;
                case "Settlement":
                    Settlement();
                    break;
                case "Combine":
                    Combine();
                    break;
                case "OrderApplyDoAdjust":
                    OrderApplyDoAdjust();
                    break;
                case "VisaFlag":
                    VisaFlag();
                    break;
                case "SaveGuestOtherInfo":
                    SaveGuestOtherInfo();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        protected void SaveGuestOtherInfo()
        {
            string SqlQueryText;

            SqlQueryText = string.Format("update OL_GuestInfo set otherinfo='{1}' where id='{0}'",
                Request.QueryString["cid"],
                Request.QueryString["otherinfo"].Trim()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void VisaFlag()
        {
            string visaflag = "";
            if (Request.QueryString["flag"] == "Visa_Tuan") visaflag = "1";
            if (Request.QueryString["flag"] == "Visa_Ger") visaflag = "2";
            string SqlQueryText = string.Format("update OL_GuestInfo set visaflag='{1}' where id in ({0})", Request.QueryString["Id"], visaflag);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"签证状态设置失败，请稍后再试!\"}");
            }
        }

        protected void Combine()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{0}'", Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次!\"})");
                Response.End();
            }

            string line1 = "", line2 = "", autoid1 = "", autoid2 = "", ordername1 = "", ordername2 = "", OrderId2 = "", CombineId1 = "", CombineId2 = "";

            //CombineId1 = MyDataBaseComm.getScalar("select combineid from CR_Combine where uid='" + Request.QueryString["TempOrderId"].Trim() + "'");
            //CombineId2 = MyDataBaseComm.getScalar("select combineid from CR_Combine where autoid='" + Request.QueryString["DoString"] + "'");

            SqlQueryText = string.Format("select OrderId,LineID,AutoId,OrderName,orderdept,(select top 1 combineid from CR_Combine where uid=OL_Order.orderid) as combineid from OL_Order where AutoId='{0}'", Request.QueryString["DoString"]);
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                line2 = DS.Tables[0].Rows[0]["LineID"].ToString();
                CombineId2 = DS.Tables[0].Rows[0]["combineid"].ToString();
                autoid2 = DS.Tables[0].Rows[0]["AutoId"].ToString();
                ordername2 = DS.Tables[0].Rows[0]["OrderName"].ToString();
                OrderId2 = DS.Tables[0].Rows[0]["OrderId"].ToString();
                if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
                {
                    if (Convert.ToString(Session["Online_UserDept"]) == DS.Tables[0].Rows[0]["orderdept"].ToString())
                    {
                    }
                    else
                    {
                        Response.Write("({\"error\":\"您没有权限操作，" + autoid2 + "不是您预订的订单\"})");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Write("({\"error\":\"您输入的订单号不存在，请检查！\"})");
                Response.End();
            }

            string infos = "", peoples = "", nums = "0";
            SqlQueryText = string.Format("select AutoId,LineID,OrderName, RebateFlag,OrderNums,adults,childs,(select top 1 combineid from CR_Combine where uid=OL_Order.orderid) as combineid from OL_Order where OrderId='{0}'", Request.QueryString["TempOrderId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                line1 = DS.Tables[0].Rows[0]["LineID"].ToString();
                CombineId1 = DS.Tables[0].Rows[0]["combineid"].ToString();
                autoid1 = DS.Tables[0].Rows[0]["AutoId"].ToString();
                ordername1 = DS.Tables[0].Rows[0]["OrderName"].ToString();
                infos = "本订单与 " + autoid2 + " 订单合并关联";//string.Format("<a class=order href=\"/OrderView/{0}.html\" target=_blank>{1} {2}</a>", OrderId2, autoid2, ordername2)
                peoples = DS.Tables[0].Rows[0]["OrderNums"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
                nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
            }

            if (autoid1 == autoid2)
            {
                Response.Write("({\"error\":\"同一个订单不能合并关联！\"})");
                Response.End();
            }

            if (line1 != line2)
            {
                Response.Write("({\"error\":\"不是同一旅游线路的订单不能合并关联！\"})");
                Response.End();
            }

            if (CombineId1.Length > 2 && CombineId2.Length > 2)
            {
                Response.Write("({\"error\":\"您将操作的两个订单都已做过关联，请重新输入!\"})");
                Response.End();
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,infos,peoples,OrderNums,ChangInfo) values ('{0}','{1}','{2}','{3}','Combine','{4}','{5}','{6}','{7}','{8}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                autoid2,
                infos,
                peoples,
                nums,
                OrderId2
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"订单合并关联申请失败\"})");
            }

        }

        protected void Settlement()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{0}'", Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次\"})");
                Response.End();
            }

            string infos = "", peoples = "", nums = "0";
            SqlQueryText = string.Format("select RebateFlag,OrderNums,adults,childs from OL_Order where OrderId='{0}'", Request.QueryString["TempOrderId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string rname = "";
                if (Request.QueryString["DoString"] == "0")
                {
                    rname = "订单全额";
                }
                else
                {
                    rname = "订单结算价";
                }
                if (DS.Tables[0].Rows[0]["RebateFlag"].ToString() == "1")
                {
                    infos = "结算方式变更 订单结算价 -> " + rname;
                }
                else
                {
                    infos = "结算方式变更 订单全额 -> " + rname;
                }
                peoples = DS.Tables[0].Rows[0]["OrderNums"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
                nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,infos,peoples,OrderNums,ChangInfo) values ('{0}','{1}','{2}','{3}','Settlement','{4}','{5}','{6}','{7}','{8}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                "0",
                infos,
                peoples,
                nums,
                Request.QueryString["DoString"]
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"结算方式变更申请失败\"})");
            }

        }

        protected void OrderApplyDoAdjust()
        {
            switch (Request.QueryString["doflag"])
            {
                case "DinnerChange":
                    DoOtherChange();
                    break;
                case "Settlement":
                    DoOtherChange();
                    break;
                case "Combine":
                    DoOtherChange();
                    break;
                default:
                    CruisesRoomDoAdjust();
                    break;
            }
        }

        protected void DoOtherChange()
        {
            string userid = "", username = "";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                //username = Convert.ToString(Session["Online_UserName"]);
                //userid = Convert.ToString(Session["Online_UserId"]);
                Response.Write("({\"error\":\"您没有权限操作\"})");
                Response.End();
            }
            DataSet DS = new DataSet();
            DataSet DS1 = new DataSet();
            DataSet DS2 = new DataSet();

            string SqlQueryText;
            SqlQueryText = string.Format("select *,(select LineID from OL_Order where OrderId=OL_OrderApply.OrderId) as LineID,(select OrderFlag from OL_Order where OrderId=OL_OrderApply.OrderId) as OrderFlag from OL_OrderApply where id='{0}'", Request.QueryString["ApplyId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["flag"].ToString() != "0")
                {
                    Response.Write("({\"error\":\"您选择的申请不能重复操作\"})");
                    Response.End();
                }
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9")
                {
                    Response.Write("({\"error\":\"已经删除的订单不能操作\"})");
                    Response.End();
                }
            }
            else {
                Response.Write("({\"error\":\"申请不存在！\"})");
                Response.End();
            }

            string OrderId = DS.Tables[0].Rows[0]["orderid"].ToString();
            string AdjustFlag = DS.Tables[0].Rows[0]["applyflag"].ToString();
            string LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
            string ChangInfo = DS.Tables[0].Rows[0]["ChangInfo"].ToString();
            decimal OrderNums = MyConvert.ConToDec(DS.Tables[0].Rows[0]["OrderNums"].ToString());

            Sql = new List<string>();

            if (AdjustFlag == "Combine")
            {
                string autoid1 = "", autoid2 = "", OrderId2 = "", CombineId1 = "", CombineId2 = "";
                autoid1 = DS.Tables[0].Rows[0]["originalid"].ToString();
               
                SqlQueryText = string.Format("select OrderId,AutoId,(select top 1 combineid from CR_Combine where uid=OL_Order.orderid) as combineid from OL_Order where AutoId='{0}'", autoid1);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    CombineId2 = DS.Tables[0].Rows[0]["combineid"].ToString();
                    autoid2 = DS.Tables[0].Rows[0]["AutoId"].ToString();
                    OrderId2 = DS.Tables[0].Rows[0]["OrderId"].ToString();
                }
                else
                {
                    Response.Write("({\"error\":\"订单不存在，不能操作!\"})");
                    Response.End();
                }

                SqlQueryText = string.Format("select AutoId,(select top 1 combineid from CR_Combine where uid=OL_Order.orderid) as combineid from OL_Order where OrderId='{0}'", OrderId);
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    CombineId1 = DS.Tables[0].Rows[0]["combineid"].ToString();
                    autoid1 = DS.Tables[0].Rows[0]["AutoId"].ToString();
                }
                else
                {
                    Response.Write("({\"error\":\"订单不存在，不能操作!\"})");
                    Response.End();
                }

                if (CombineId1.Length > 2 && CombineId2.Length > 2)
                {
                    Response.Write("({\"error\":\"申请合并的两个订单都已做过关联，不能操作!\"})");
                    Response.End();
                }

                if (CombineId1.Length == 0 && CombineId2.Length == 0)
                {
                    Sql.Add(string.Format("insert into CR_Combine (combineid,autoid,uid,dotime) values ('{0}','{1}','{2}','{3}')", autoid1, autoid1, OrderId, DateTime.Now.ToString()));
                    Sql.Add(string.Format("insert into CR_Combine (combineid,autoid,uid,dotime) values ('{0}','{1}','{2}','{3}')", autoid1, autoid2, OrderId2, DateTime.Now.ToString()));
                }
                else
                {
                    if (CombineId1.Length > 2)
                    {
                        Sql.Add(string.Format("insert into CR_Combine (combineid,autoid,uid,dotime) values ('{0}','{1}','{2}','{3}')", CombineId1, autoid2, OrderId2, DateTime.Now.ToString()));
                    }

                    if (CombineId2.Length > 2)
                    {
                        Sql.Add(string.Format("insert into CR_Combine (combineid,autoid,uid,dotime) values ('{0}','{1}','{2}','{3}')", CombineId2, autoid1, OrderId, DateTime.Now.ToString()));
                    }
                }
            }

            switch (AdjustFlag)
            {
                case "DinnerChange":
                    Sql.Add(string.Format("delete from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='{0}'", OrderId));
                    Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", OrderId, "CruisesDinner", ChangInfo.Split("@".ToCharArray())[0], ChangInfo.Split("@".ToCharArray())[1], DateTime.Now.ToString()));
                    break;
                case "Settlement":
                    Sql.Add(string.Format("update OL_Order set RebateFlag='{0}' where OrderId='{1}'", ChangInfo, OrderId));
                    break;
                case "Combine":
                    break;
                default:
                    break;
            }
            
            //变更申请为已操作
            Sql.Add(string.Format("update OL_OrderApply set flag='1',douser='{0}',dotime='{1}' where id='{2}'",
                username,
                DateTime.Now.ToString(),
                Request.QueryString["ApplyId"].Trim()
            ));

            string[] SqlQuerys = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuerys) == true)
            {
                if (AdjustFlag == "Settlement")
                {
                    string result = PurchaseClass.CruisesOrderAdjust(OrderId, AdjustFlag, "Yes");
                    if (result == "OK")
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"error\":\"操作已成功完成，但畅游同步失败（" + result + "），请到同步记录中查看！\"})");
                    }
                }
                else
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"订单申请操作失败\"})");
            }
        }

        protected void DinnerChange()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{0}'", Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次\"})");
                Response.End();
            }

            string infos = "", peoples = "", nums = "0";
            SqlQueryText = string.Format("select OrderNums,adults,childs from OL_Order where OrderId='{0}'", Request.QueryString["TempOrderId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string OldDinner = MyDataBaseComm.getScalar("select ExtContent from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='" + Request.QueryString["TempOrderId"].Trim() + "'");

                infos = "用餐变更 " + OldDinner.Replace("All", "晚餐时间均可安排") + " -> " + Request.QueryString["DoString"].Split("@".ToCharArray())[1].Replace("All", "晚餐时间均可安排");
                peoples = DS.Tables[0].Rows[0]["OrderNums"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
                nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,infos,peoples,OrderNums,ChangInfo) values ('{0}','{1}','{2}','{3}','DinnerChange','{4}','{5}','{6}','{7}','{8}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                "0",
                infos,
                peoples,
                nums,
                Request.QueryString["DoString"].Trim()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"订单用餐时间变更申请失败\"})");
            }

        }

        protected void DeleteOrder()
        {
            string userid="", username="";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                Response.Write("{\"success\":\"只有后台操作人员才能删除!\"}");
                Response.End();
            }
            string SqlQueryText = string.Format("select OrderId,OrderFlag from OL_Order where OrderId='{0}'", Request.QueryString["OrderId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() != "8")
                {
                    Response.Write("{\"success\":\"只有已经取消的订单才能删除!\"}");
                    Response.End();
                }
                List<string> Sql = new List<string>();
                Sql.Add(string.Format("update OL_Order set OrderFlag='9' where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                //Sql.Add(string.Format("delete from OL_Order where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_RoomList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_RoomOrder where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_GuestInfo where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_OrderExtend where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));
                Sql.Add(string.Format("delete from OL_OrderPrice where OrderId='{0}'", DS.Tables[0].Rows[0]["OrderId"].ToString()));

                //取消申请记录
                Sql.Add(string.Format("update OL_OrderApply set flag='2',douser='{1}',dotime='{2}' where flag='0' and OrderId='{0}'",
                    DS.Tables[0].Rows[0]["OrderId"].ToString(),
                    username,
                    DateTime.Now.ToString()
                ));

                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", DS.Tables[0].Rows[0]["OrderId"].ToString(), DateTime.Now.ToString(), username + "删除了订单"));

                string[] SqlQueryList = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryList) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":\"订单删除失败，请稍后再试!\"}");
                }
            }

        }

        protected void CruisesPlanNoCancel()
        {
            string result = PurchaseClass.MisCruisesGuestPlanNoCancel(Request.QueryString["LineId"], Request.QueryString["Id"]);
            if (result != "OK")
            {
                Response.Write("{\"success\":\"畅游分团号同步失败，请稍后再试!\"}");
                Response.End();
            }

            string SqlQueryText = string.Format("update OL_GuestInfo set PlanAllotid=null where id in ({0})", Request.QueryString["Id"]);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"分团号分配取消失败，请稍后再试!\"}");
            }
        }

        protected void CruisesGroupAllotPlan()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from View_CR_PlanNo where id='{0}'", Request.Form["SelectId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int SelectNums = Request.Form["GuestId"].Split(',').Length;
                int Nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Berth"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[0]["nums"].ToString());
                if (Nums < SelectNums)
                {
                    Response.Write("({\"error\":\"团号分配失败，所选分团剩余人数不足!\"})");
                    Response.End();
                }

                string result = PurchaseClass.MisCruisesPlanNoChange(Request.Form["LineId"], Request.Form["GuestId"], DS.Tables[0].Rows[0]["id"].ToString(), DS.Tables[0].Rows[0]["PlanNo"].ToString());
                if (result != "OK")
                {
                    Response.Write("{\"success\":\"畅游分团号同步失败，请稍后再试!\"}");
                    Response.End();
                }

                Sql.Add(string.Format("update OL_GuestInfo set PlanAllotid='{1}' where id in ({0})",
                    Request.Form["GuestId"],
                    Request.Form["SelectId"]
                ));

                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"分团号分配失败，请重稍后再试!\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"没有查询到分团号，请重新选择!\"})");
            }
        }

        protected void SetCruisesPlanNo()
        {
            Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from View_CR_PlanNo where Berth>Nums and id='{0}'", Request.QueryString["PlanAllotid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                string result = PurchaseClass.MisCruisesPlanNoChange(Request.QueryString["LineId"], Request.QueryString["GuestId"], DS.Tables[0].Rows[0]["id"].ToString(), DS.Tables[0].Rows[0]["PlanNo"].ToString());
                if (result != "OK")
                {
                    Response.Write("{\"success\":\"畅游分团号同步失败，请稍后再试!\"}");
                    Response.End();
                }

                SqlQueryText = string.Format("update OL_GuestInfo set PlanAllotid='{1}' where id='{0}'", Request.QueryString["GuestId"], Request.QueryString["PlanAllotid"]);

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":\"分团号分配失败，请稍后再试!\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"您选择的分团已满员!\"}");
                Response.End();
            }
        }

        protected void SelectCruisesBusNo()
        {
            Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from View_CR_BusNo where Berth>Nums and id='{0}'", Request.QueryString["BusNoId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("update CR_VisitList set Busid='{1}',BusNo='{2}' where id='{0}'", 
                    Request.QueryString["ListId"],
                    DS.Tables[0].Rows[0]["id"].ToString(),
                    DS.Tables[0].Rows[0]["BusNo"].ToString()
                );
                
                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":\"车号分配取消失败，请稍后再试!\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"您选择的车号已满员!\"}");
                Response.End();
            }
        }

        protected void CruisesGroupAllotDinnerNo()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from View_CR_DinnerNo where id='{0}'", Request.Form["SelectId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int SelectNums = Request.Form["GuestId"].Split(',').Length;
                int Nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Berth"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[0]["nums"].ToString());
                if (Nums < SelectNums)
                {
                    Response.Write("({\"error\":\"餐桌号分配失败，所选餐桌剩余人数不足!\"})");
                    Response.End();
                }

                Sql.Add(string.Format("update OL_GuestInfo set DinnerId='{1}' where id in ({0})",
                    Request.Form["GuestId"],
                    Request.Form["SelectId"]
                ));

                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"餐桌号分配失败，请重稍后再试!\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"没有查询到餐桌号，请重新选择!\"})");
            }
        }

        protected void CruisesDinnerNoCancel()
        {
            string SqlQueryText = string.Format("update OL_GuestInfo set DinnerId=null where id in ({0})", Request.QueryString["Id"]);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"餐桌号分配取消失败，请稍后再试!\"}");
            }
        }

        protected void SetCruisesDinnerNo()
        {
            Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from View_CR_DinnerNo where Berth>Nums and id='{0}'", Request.QueryString["DinnerId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                SqlQueryText = string.Format("update OL_GuestInfo set DinnerId='{1}' where id='{0}'", Request.QueryString["GuestId"], Request.QueryString["DinnerId"]);

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("{\"success\":0,\"DinnerTime\":\"" + DS.Tables[0].Rows[0]["DinnerTime"].ToString() + "\"}");
                }
                else
                {
                    Response.Write("{\"success\":\"餐桌号分配失败，请稍后再试!\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"您选择的餐桌已满员!\"}");
                Response.End();
            }
        }

        protected void CruisesCarSeatCancel()
        {
            string SqlQueryText = string.Format("update CR_VisitList set Busid=null,BusNo=null where id in ({0})", Request.QueryString["Id"]);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"车号分配取消失败，请稍后再试!\"}");
            }
        }

        protected void SetCruisesCarNo()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("SELECT *,(select count(1) from CR_VisitList where flag='0' and busid=CR_BusNo.id) as nums FROM CR_BusNo where id='{0}'", Request.Form["CarId"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int SelectNums = Request.Form["VisitListId"].Split(',').Length;
                int Nums = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Berth"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[0]["nums"].ToString());
                if (Nums < SelectNums)
                {
                    Response.Write("({\"error\":\"车号分配失败，所选车号剩余人数不足!\"})");
                    Response.End();
                }

                Sql.Add(string.Format("update CR_VisitList set Busid='{1}',BusNo='{2}' where id in ({0})",
                    Request.Form["VisitListId"],
                    DS.Tables[0].Rows[0]["id"].ToString(),
                    DS.Tables[0].Rows[0]["BusNo"].ToString()
                ));

                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"车号分配失败，请重稍后再试!\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"没有查询到车号，请重新选择!\"})");
            }
        }

        protected void CruisesCheckInCancel()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from CR_RoomList where roomnoid>0 and (MergeFlag='0' or MergeFlag is null) and id in ({0})", Request.QueryString["Id"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    //取消房号入住信息
                    Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Mergeid='0',Nums='0',DoDate=NULL where Id='{0}'",
                        DS.Tables[0].Rows[i]["roomnoid"].ToString()
                    ));

                    //取消客人入住
                    Sql.Add(string.Format("update CR_RoomList set roomnoid='0',roomno='' where Id='{0}'",
                        DS.Tables[0].Rows[i]["id"].ToString()
                    ));
                }
                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    Response.Write("{\"success\":0}");
                }
                else
                {
                    Response.Write("{\"success\":\"批量取消客人入住失败，请重稍后再试!\"}");
                }
            }
            else
            {
                Response.Write("{\"success\":\"没有查询到客人入住信息，请重新选择!\"}");
            }
        }

        protected void SetCruisesRoomNo()
        {
            Sql = new List<string>();
            string SqlQueryText, peoples="0";
            SqlQueryText = string.Format("select *,(select Mergeid from CR_RoomNo where id=CR_RoomList.roomnoid) as Mergeid from CR_RoomList where id='{0}'", Request.QueryString["ListId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Mergeid"].ToString()) > 0)
                {
                    Response.Write("{\"success\":\"您选择的待入住房间，之前的房号是拼房，不能操作!\"}");
                    Response.End();
                }
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["roomnoid"].ToString()) > 0)
                {
                    Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Nums='0',DoDate=NULL where Id='{0}'",
                        DS.Tables[0].Rows[0]["roomnoid"].ToString()
                    ));
                }
                peoples = DS.Tables[0].Rows[0]["peoples"].ToString();
            }
            else
            {
                Response.Write("{\"success\":\"您选择的游客待入住房间不存在!\"}");
                Response.End();
            }

            SqlQueryText = string.Format("select * from CR_RoomNo where Flag='0' and id='{0}'", Request.QueryString["RoomNoId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Mergeid"].ToString()) > 0)
                {
                    Response.Write("{\"success\":\"您选择的房号是拼房，不能操作!\"}");
                    Response.End();
                }

                Sql.Add(string.Format("update CR_RoomNo set Flag='1',Listid='{1}',Nums='{2}',DoDate='{3}' where Id='{0}'",
                    DS.Tables[0].Rows[0]["id"].ToString(),
                    Request.QueryString["ListId"].Trim(),
                    peoples,
                    DateTime.Now.ToString()
                ));

                Sql.Add(string.Format("update CR_RoomList set roomnoid='{1}',roomno='{2}' where Id='{0}'",
                    Request.QueryString["ListId"].Trim(),
                    DS.Tables[0].Rows[0]["id"].ToString(),
                    DS.Tables[0].Rows[0]["RoomNo"].ToString()
                ));
            }
            else
            {
                SqlQueryText = string.Format("select *,(berth-Nums) as seat from CR_RoomNo where id='{0}' and Flag='1' and nums>0 and listid>0 and Mergeid=0", Request.QueryString["RoomNoId"].Trim());
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    int haveseats = MyConvert.ConToInt(DS.Tables[0].Rows[0]["seat"].ToString());

                    if (haveseats < MyConvert.ConToInt(peoples))
                    {
                        Response.Write("{\"success\":\"您选择的房号剩余可住人数不足!\"}");
                        Response.End();
                    }

                    Sql.Add(string.Format("update CR_RoomNo set Flag='2',Mergeid='{1}',Nums=Nums+{2},DoDate='{3}' where Id='{0}'",
                        DS.Tables[0].Rows[0]["id"].ToString(),
                        Request.QueryString["ListId"].Trim(),
                        peoples,
                        DateTime.Now.ToString()
                    ));

                    Sql.Add(string.Format("update CR_RoomList set roomnoid='{1}',roomno='{2}',MergeFlag='1' where Id='{0}'",
                        Request.QueryString["ListId"].Trim(),
                        DS.Tables[0].Rows[0]["id"].ToString(),
                        DS.Tables[0].Rows[0]["RoomNo"].ToString()
                    ));

                    Sql.Add(string.Format("update CR_RoomList set MergeFlag='1' where Id='{0}'",
                         DS.Tables[0].Rows[0]["Listid"].ToString()
                    ));
                }
                else
                {
                    Response.Write("{\"success\":\"您选择的房号不存在!\"}");
                    Response.End();
                }
                
            }

            string[] SqlQuerys = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuerys) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"房号分配失败，请稍后再试!\"}");
            }
        }

        protected void CruisesOrderCancel()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{1}'", Request.QueryString["OldId"].Trim(), Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次\"})");
                Response.End();
            }

            string infos = "", peoples = "", nums = "0";
            SqlQueryText = string.Format("select OrderNums,adults,childs from OL_Order where OrderId='{0}'", Request.QueryString["TempOrderId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                infos = "订单取消";
                peoples = DS.Tables[0].Rows[0]["OrderNums"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
                nums = DS.Tables[0].Rows[0]["OrderNums"].ToString();
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,infos,peoples,OrderNums) values ('{0}','{1}','{2}','{3}','OrderRetreat','{4}','{5}','{6}','{7}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                "0",
                infos,
                peoples,
                nums
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"订单取消申请失败\"})");
            }
        }

        protected void DoMisError()
        {
            string userid = "", username = "";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                Response.Write("({\"error\":\"您没有权限操作\"})");
                Response.End();
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from CR_MisDoError where id='{0}'", Request.QueryString["ApplyId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {

                if (DS.Tables[0].Rows[0]["flag"].ToString() != "0")
                {
                    Response.Write("({\"error\":\"您选择的记录不能重复操作\"})");
                    Response.End();
                }

                string result = PurchaseClass.CruisesOrderAdjust(DS.Tables[0].Rows[0]["orderid"].ToString(), DS.Tables[0].Rows[0]["adjustflag"].ToString(),"No");
                if (result == "OK")
                {
                    SqlQueryText = string.Format("update CR_MisDoError set flag='1',douser='{0}',dotime='{1}' where id={2}",
                        username,
                        DateTime.Now.ToString(),
                        Request.QueryString["ApplyId"].Trim()
                    );

                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"error\":\"同步失败\"})");
                    }
                }
                else
                {
                    Response.Write("({\"error\":\"同步失败，" + result + "\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"记录不存在\"})");
            }
        }

        protected void CancelMisError()
        {
            string userid = "", username = "";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                Response.Write("({\"error\":\"您没有权限操作\"})");
                Response.End();
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from CR_MisDoError where id='{0}'", Request.QueryString["ApplyId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {

                if (DS.Tables[0].Rows[0]["flag"].ToString() != "0")
                {
                    Response.Write("({\"error\":\"您选择的记录不能重复操作\"})");
                    Response.End();
                }

                SqlQueryText = string.Format("update CR_MisDoError set flag='2',douser='{0}',dotime='{1}' where id={2}",
                    username,
                    DateTime.Now.ToString(),
                    Request.QueryString["ApplyId"].Trim()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"取消失败\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"记录不存在\"})");
            }
        }

        protected void CruisesRoomDoAdjust()
        {

            string userid = "", username = "";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                //username = Convert.ToString(Session["Online_UserName"]);
                //userid = Convert.ToString(Session["Online_UserId"]);
                Response.Write("({\"error\":\"您没有权限操作\"})");
                Response.End();
            }

            DataSet DS = new DataSet();
            DataSet DS1 = new DataSet();
            DataSet DS2 = new DataSet();
            DataSet DS3 = new DataSet();

            string SqlQueryText;
            SqlQueryText = string.Format("select *,(select allotid from cr_roomlist where id=OL_OrderApply.originalid) as OldAllotid,(select Price from OL_Order where OrderId=OL_OrderApply.OrderId) as AllPrice,(select LineID from OL_Order where OrderId=OL_OrderApply.OrderId) as LineID,(select OrderTime from OL_Order where OrderId=OL_OrderApply.OrderId) as OrderTime,(select orderdept from OL_Order where OrderId=OL_OrderApply.OrderId) as orderdept,(select ordercompany from OL_Order where OrderId=OL_OrderApply.OrderId) as ordercompany,(select OrderFlag from OL_Order where OrderId=OL_OrderApply.OrderId) as OrderFlag from OL_OrderApply where id='{0}'", Request.QueryString["ApplyId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["flag"].ToString() != "0")
                {
                    Response.Write("({\"error\":\"您选择的申请不能重复操作\"})");
                    Response.End();
                }
                if (DS.Tables[0].Rows[0]["OrderFlag"].ToString() == "9")
                {
                    Response.Write("({\"error\":\"已经删除的订单不能操作\"})");
                    Response.End();
                }
                string RoomListId = DS.Tables[0].Rows[0]["originalid"].ToString();
                string OldAllotId = DS.Tables[0].Rows[0]["OldAllotid"].ToString();
                string AllotId = DS.Tables[0].Rows[0]["updateid"].ToString();
                string OrderId = DS.Tables[0].Rows[0]["orderid"].ToString();
                string GuestRankNo = "0";
                string AdjustFlag = DS.Tables[0].Rows[0]["applyflag"].ToString();
                string LineId = DS.Tables[0].Rows[0]["LineID"].ToString();
                string orderdept = DS.Tables[0].Rows[0]["orderdept"].ToString();
                string ordercompany = DS.Tables[0].Rows[0]["ordercompany"].ToString();

                DateTime ApplyDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["inputdate"].ToString());
                DateTime OrderDate = MyConvert.ConToDateTime(DS.Tables[0].Rows[0]["OrderTime"].ToString());
                
                decimal Retreat = MyConvert.ConToDec(DS.Tables[0].Rows[0]["AllPrice"].ToString());
                decimal OrderNums = MyConvert.ConToDec(DS.Tables[0].Rows[0]["OrderNums"].ToString());
                AllDamages = 0;
                //string results = PurchaseClass.CruisesOrderAdjust(OrderId, AdjustFlag);
                //if (results == "OK")
                //{
                //    Response.Write("({\"success\":\"OK\"})");
                //}
                //else
                //{
                //    Response.Write("({\"error\":\"操作已成功完成，但畅游同步失败（" + results + "），请到同步记录中查看！\"})");
                //}
                //Response.End();
                
                Sql = new List<string>();

                if (AdjustFlag == "OrderRetreat")
                {
                    //检查是否有拼房，如果有则不允许取消
                    //取消房间号分配
                    SqlQueryText = string.Format("select *,(select Mergeid from CR_RoomNo where id=CR_RoomList.roomnoid) as Mergeid from CR_RoomList where roomnoid>0 and orderflag='0' and OrderId='{0}'", OrderId);
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                        {
                            if (DS1.Tables[0].Rows[i]["MergeFlag"].ToString() == "1")
                            {
                                Response.Write("({\"error\":\"您选择的订单，有拼房记录，请将拼房房号取消再操作\"})");
                                Response.End();
                            }
                            if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["Mergeid"].ToString()) > 0)
                            {
                                Response.Write("({\"error\":\"您选择的订单，有拼房记录，请将拼房房号取消再操作\"})");
                                Response.End();
                            }
                            //取消房号入住信息
                            Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Mergeid='0',Nums='0',DoDate=NULL where Id='{0}'",
                                DS1.Tables[0].Rows[i]["roomnoid"].ToString()
                            ));
                        }
                    }

                    //舱房取消
                    Sql.Add(string.Format("update CR_RoomList set orderflag='1',roomnoid='0',roomno='' where OrderId='{0}'",
                        OrderId
                    ));

                    //取消游客
                    Sql.Add(string.Format("update OL_GuestInfo set flag='1' where OrderId='{0}'",
                        OrderId
                    ));

                    //变更观光明细为取消状态
                    Sql.Add(string.Format("update CR_VisitList set flag='1' where OrderId='{0}'",
                        OrderId
                    ));

                    //订单金额全退
                    Sql.Add(string.Format("update OL_Order set OrderFlag='8',Price=0,rebate=0,allmdjs=0 where OrderId='{0}'",
                        OrderId
                    ));

                    //补退款明细金额
                    Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        OrderId,
                        "OrderRetreat",
                        "0",
                        "订单取消",
                        "订单取消退款",
                        0 - Retreat,
                        "1",
                        0 - Retreat,
                        DateTime.Now.ToString()
                    ));

                    AllDamages = Retreat;
                }
                else
                {
                    //是否有舱房优惠，有则退优惠
                    string RebatePrice = MyDataBaseComm.getScalar("select ISNULL(sum(SumPrice),0) from OL_OrderPrice where PriceType='CruisesRebate' and PriceId='" + OldAllotId + "' and OrderId='" + OrderId + "'");
                    //Response.Write(RebatePrice);
                    //Response.End();
                    if (Convert.ToDecimal(RebatePrice) < 0)
                    {
                        //有优惠，退优惠
                        SqlQueryText = string.Format("SELECT top 1 *,(select roomname from CR_RoomAllot where id=CR_Rebate.allotid) as roomname from CR_Rebate where allotid='{1}' and begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' order by id", OrderDate, OldAllotId);
                        DS3.Clear();
                        DS3 = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS3.Tables[0].Rows.Count > 0)
                        {
                            AllDamages += 0 - MyConvert.ConToDec(DS3.Tables[0].Rows[0]["price"].ToString());
                            Sql.Add(string.Format("update OL_Order set Price=Price+{1} where OrderId='{0}'",
                                OrderId,
                                DS3.Tables[0].Rows[0]["price"].ToString()
                            ));

                            Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                OrderId,
                                "CruisesRebate",
                                DS3.Tables[0].Rows[0]["allotid"].ToString(),
                                "退舱房优惠",
                                "舱房取消，退" + DS3.Tables[0].Rows[0]["roomname"].ToString() + "优惠",
                                DS3.Tables[0].Rows[0]["price"].ToString(),
                                "1",
                                DS3.Tables[0].Rows[0]["price"].ToString(),
                                DateTime.Now.ToString()
                            ));
                        }
                    }

                    //舱房取消
                    SqlQueryText = "SELECT *,(select Mergeid from CR_RoomNo where id=CR_RoomList.roomnoid) as Mergeid,(select shipid from CR_RoomAllot where id=CR_RoomList.allotid) as shipid from CR_RoomList where id='" + RoomListId + "' and orderflag='0'";
                    DS1.Clear();
                    DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                    if (DS1.Tables[0].Rows.Count > 0)
                    {
                        if (DS1.Tables[0].Rows[0]["MergeFlag"].ToString() == "1")
                        {
                            Response.Write("({\"error\":\"您选择的订单，有拼房记录，请将拼房房号取消再操作\"})");
                            Response.End();
                        }
                        if (MyConvert.ConToInt(DS1.Tables[0].Rows[0]["Mergeid"].ToString()) > 0)
                        {
                            Response.Write("{\"success\":\"您选择的待入住房间，之前的房号是拼房，不能操作!\"}");
                            Response.End();
                        }
                        //取消房号入住信息
                        if (MyConvert.ConToInt(DS1.Tables[0].Rows[0]["roomnoid"].ToString()) > 0)
                        {
                            Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Mergeid='0',Nums='0',DoDate=NULL where Id='{0}'",
                                DS1.Tables[0].Rows[0]["roomnoid"].ToString()
                            ));
                        }

                        GuestRankNo = DS1.Tables[0].Rows[0]["guestids"].ToString();
                        //SqlQueryText = "SELECT * from CR_RoomAllot where id='" + DS1.Tables[0].Rows[0]["allotid"].ToString() + "'";
                        //DS2.Clear();
                        //DS2 = MyDataBaseComm.getDataSet(SqlQueryText);
                        //取消原来预定的舱房
                        AdjustRoomSqlstr(DS1.Tables[0], "Cancel", OrderId, DS1.Tables[0].Rows[0]["allotid"].ToString(), 1, DS1.Tables[0].Rows[0]["peoples"].ToString(), DS1.Tables[0].Rows[0]["adults"].ToString(), DS1.Tables[0].Rows[0]["childs"].ToString(), RoomListId, orderdept, ordercompany);
                    }
                    else
                    {
                        Response.Write("({\"error\":\"您选择的舱房预定信息不存在\"})");
                        Response.End();
                    }

                    //舱房变更
                    if (AdjustFlag == "AdjustCruisesRoom")
                    {
                        SqlQueryText = "SELECT *,id as allotid from View_CR_RoomAllot where (nums-sellroom)>0 and id='" + AllotId + "'";
                        DS2.Clear();
                        DS2 = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS2.Tables[0].Rows.Count > 0)
                        {
                            AdjustRoomSqlstr(DS2.Tables[0], "Adjust", OrderId, AllotId, 1, DS1.Tables[0].Rows[0]["peoples"].ToString(), DS1.Tables[0].Rows[0]["adults"].ToString(), DS1.Tables[0].Rows[0]["childs"].ToString(), RoomListId, orderdept, ordercompany);
                        }
                        else
                        {
                            Response.Write("({\"error\":\"您选择的舱房，剩余房间数量不足\"})");
                            Response.End();
                        }

                        //有优惠，补优惠
                        SqlQueryText = string.Format("SELECT top 1 *,(select roomname from CR_RoomAllot where id=CR_Rebate.allotid) as roomname from CR_Rebate where allotid='{1}' and begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' order by id", DateTime.Today, AllotId);
                        DS3.Clear();
                        DS3 = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS3.Tables[0].Rows.Count > 0)
                        {
                            Sql.Add(string.Format("update OL_Order set Price=Price-{1} where OrderId='{0}'",
                                OrderId,
                                DS3.Tables[0].Rows[0]["price"].ToString()
                            ));

                            Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                OrderId,
                                "CruisesRebate",
                                DS3.Tables[0].Rows[0]["allotid"].ToString(),
                                "舱房优惠",
                                DS3.Tables[0].Rows[0]["roomname"].ToString() + " " + DS3.Tables[0].Rows[0]["infos"].ToString(),
                                "-" + DS3.Tables[0].Rows[0]["price"].ToString(),
                                "1",
                                "-" + DS3.Tables[0].Rows[0]["price"].ToString(),
                                DateTime.Now.ToString()
                            ));
                        }

                        //游客舱房分配id变更
                        Sql.Add(string.Format("update OL_GuestInfo set allotid='{2}',roomid='{3}' where OrderId='{0}' and rankno in ({1})",
                            OrderId,
                            GuestRankNo,
                            AllotId,
                            DS2.Tables[0].Rows[0]["roomid"].ToString()
                        ));

                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), username + "操作了舱房变更申请"));

                    }
                    else
                    {
                        //变更舱房为取消
                        Sql.Add(string.Format("update CR_RoomList set orderflag='1',roomnoid='0',roomno='' where id='{0}'",
                            RoomListId
                        ));

                        //取消游客
                        Sql.Add(string.Format("update OL_GuestInfo set flag='1' where OrderId='{0}' and rankno in ({1})",
                            OrderId,
                            GuestRankNo
                        ));

                        //增加负金额观光项目
                        string VisitPrice = MyDataBaseComm.getScalar(string.Format("select ISNULL(sum(price),0) from View_CR_VisitList where OrderId='{0}' and rankno in ({1})",
                            OrderId,
                            GuestRankNo
                        ));

                        if (MyConvert.ConToInt(VisitPrice) > 0)
                        {
                            AllDamages += MyConvert.ConToDec(VisitPrice);
                            
                            Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                OrderId,
                                "CancelVisit",
                                "0",
                                "岸上观光",
                                "舱房取消，退" + DS1.Tables[0].Rows[0]["peoples"].ToString() + "人岸上观光费用",
                                "-" + VisitPrice,
                                "1",
                                "-" + VisitPrice,
                                DateTime.Now.ToString()
                            ));

                            Sql.Add(string.Format("update OL_Order set Price=Price-{1} where OrderId='{0}'",
                                OrderId,
                                VisitPrice
                            ));
                        }

                        //变更观光明细为取消状态
                        Sql.Add(string.Format("update CR_VisitList set flag='1' where OrderId='{0}' and rankno in ({1})",
                            OrderId,
                            GuestRankNo
                        ));

                        //退必选费用
                        decimal bxfy = 0;
                        SqlQueryText = string.Format("SELECT * from OL_OrderPrice where PriceType='ExtPrice' and PriceName = '必选费用' and OrderId = '{0}' ", OrderId);
                        DS3.Clear();
                        DS3 = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS3.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < DS3.Tables[0].Rows.Count; i++)
                            {
                                bxfy = 0;
                                bxfy = MyConvert.ConToDec(DS3.Tables[0].Rows[i]["SellPrice"].ToString()) * MyConvert.ConToDec(DS1.Tables[0].Rows[0]["peoples"].ToString());
                                AllDamages += bxfy;

                                Sql.Add(string.Format("update OL_Order set Price=Price-{1} where OrderId='{0}'",
                                     OrderId,
                                     bxfy
                                 ));

                                Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                    OrderId,
                                    "ExtPrice",
                                    DS3.Tables[0].Rows[i]["PriceId"].ToString(),
                                    "退必选费用",
                                    "退" + DS3.Tables[0].Rows[i]["PriceMemo"].ToString(),
                                    0 - MyConvert.ConToDec(DS3.Tables[0].Rows[i]["SellPrice"].ToString()),
                                    DS1.Tables[0].Rows[0]["peoples"].ToString(),
                                    0 - bxfy,
                                    DateTime.Now.ToString()
                                ));
                            }
                        }

                        Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", OrderId, DateTime.Now.ToString(), username + "操作了舱房取消申请"));
                    }
                }

                //增加退舱房损失费
                if (AdjustFlag == "OrderRetreat" || AdjustFlag == "CancelCruisesRoom")
                {
                    string OrderFlag = MyDataBaseComm.getScalar("select OrderFlag from OL_Order where OrderId='" + OrderId + "'");
                    string RetreatName = "舱房取消或订单取消收取损失费";
                        
                    if (OrderFlag == "2" || OrderFlag == "3")
                    {
                        //提取损失费比例，并按比例折算
                        SqlQueryText = string.Format("SELECT top 1 *,(select yfk from ol_line where MisLineId=CR_Damage.lineid) as yfk from CR_Damage where begindate <= '{0:yyyy-MM-dd}' AND enddate >= '{0:yyyy-MM-dd}' and lineid='{1}' order by begindate", ApplyDate, LineId);
                        DS1.Clear();
                        DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                        if (DS1.Tables[0].Rows.Count > 0)
                        {
                            RetreatName = DS1.Tables[0].Rows[0]["infos"].ToString();
                            if (DS1.Tables[0].Rows[0]["flag"].ToString() == "1")
                            {
                                AllDamages = OrderNums * MyConvert.ConToDec(DS1.Tables[0].Rows[0]["yfk"].ToString());
                            }
                            else
                            {
                                AllDamages = Math.Round(AllDamages * (MyConvert.ConToDec(DS1.Tables[0].Rows[0]["rate"].ToString()) / 100), 2);
                            }
                        }
                        else
                        {
                            AllDamages = 0;
                        }
                    }
                    else
                    {
                        AllDamages = 0;
                    }
                    

                    if (AllDamages > 0)
                    {
                        Sql.Add(string.Format("update OL_Order set Price=Price+{1} where OrderId='{0}'",
                            OrderId,
                            AllDamages
                        ));

                        Sql.Add(string.Format("insert into OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                            OrderId,
                            "OrderRetreat",
                            "0",
                            "退团损失",
                            RetreatName,
                            AllDamages,
                            "1",
                            AllDamages,
                            DateTime.Now.ToString()
                        ));
                    }
                }
                //损失费End

                //变更申请为已操作
                Sql.Add(string.Format("update OL_OrderApply set flag='1',douser='{0}',dotime='{1}' where id='{2}'",
                    username,
                    DateTime.Now.ToString(),
                    Request.QueryString["ApplyId"].Trim()
                ));

                string[] SqlQuerys = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                {
                    string result = PurchaseClass.CruisesOrderAdjust(OrderId, AdjustFlag,"Yes");
                    if (result == "OK")
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"error\":\"操作已成功完成，但畅游同步失败（" + result + "），请到同步记录中查看！\"})");
                    }
                }
                else
                {
                    Response.Write("({\"error\":\"舱房申请操作失败\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"舱房申请记录不存在\"})");
            }
        }

        protected void AdjustRoomSqlstr(DataTable dt, string AdjustFlag, string OrderId, string allotid, int allroom, string allmen, string adult, string childs, string listid, string OrderDept, string OrderCompany)
        {
            int allprice, price1, price2, price3, rebate1, rebate2, rebate3, adultnm, childnum, allPeple, allrebate;
            adultnm = MyConvert.ConToInt(adult);
            childnum = MyConvert.ConToInt(childs);
            allPeple = MyConvert.ConToInt(allmen);

            //string sql = "";
            DataRow[] drs = dt.Select("allotid=" + allotid);
            if (drs.Count() > 0)
            {
                foreach (DataRow dr in drs)
                {
                    allrebate = 0;
                    price1 = Convert.ToInt32(dr["price"].ToString());
                    price2 = Convert.ToInt32(dr["thirdprice"].ToString());
                    price3 = Convert.ToInt32(dr["childprice"].ToString());

                    rebate1 = Convert.ToInt32(dr["rebate"].ToString());
                    rebate2 = Convert.ToInt32(dr["thirdrebate"].ToString());
                    rebate3 = Convert.ToInt32(dr["childrebate"].ToString());
                    int berth = Convert.ToInt32(dr["berth"].ToString());

                    switch (berth)
                    {
                        case 1:
                            allprice = allPeple * price1;
                            break;
                        case 2:
                            allprice = allroom * price1 * 2;
                            break;
                        default:
                            allprice = allroom * price1 * 2;
                            if (childnum >= allroom * (berth - 2))
                            {
                                allprice += allroom * (berth - 2) * price3;
                            }
                            else
                            {
                                allprice += childnum * price3;
                                allprice += (allroom * (berth - 2) - childnum) * price2;
                            }
                            break;
                    }

                    switch (berth)
                    {
                        case 1:
                            allrebate = allPeple * rebate1;
                            break;
                        case 2:
                            allrebate = allroom * rebate1 * 2;
                            break;
                        default:
                            allrebate = allroom * rebate1 * 2;
                            if (childnum >= allroom * (berth - 2))
                            {
                                allrebate += allroom * (berth - 2) * rebate3;
                            }
                            else
                            {
                                allrebate += childnum * price3;
                                allrebate += (allroom * (berth - 2) - childnum) * rebate2;
                            }
                            break;
                    }

                    //非同行预定，返利为零
                    if (MyConvert.ConToInt(OrderDept) == 0) allrebate = 0;
                    //青旅内部预定，返利为零
                    if (MyConvert.ConToInt(OrderCompany) == 1) allrebate = 0;


                    if (AdjustFlag == "Cancel")
                    {
                        AllDamages += MyConvert.ConToDec(allprice.ToString());
                        Sql.Add(string.Format("update OL_Order set OrderNums=OrderNums-{1},Adults=Adults-{2},Childs=Childs-{3},Price=Price-{4},rebate=rebate-{5} where OrderId='{0}'",
                            OrderId,
                            allPeple,
                            adultnm,
                            childnum,
                            allprice,
                            allrebate
                        ));

                        Sql.Add(string.Format("insert into CR_RoomOrder (OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,adult,childs,peoples,rooms,gather,roomname,berth,AllRebate) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                            OrderId,
                            dr["lineid"].ToString(),
                            dr["shipid"].ToString(),
                            dr["allotid"].ToString(),
                            dr["roomid"].ToString(),
                            dr["price"].ToString(),
                            dr["thirdprice"].ToString(),
                            dr["childprice"].ToString(),
                            dr["rebate"].ToString(),
                            dr["thirdrebate"].ToString(),
                            dr["childrebate"].ToString(),
                            "-" + adult,
                            "-" + childs,
                            "-" + allmen,
                            0-allroom,
                            0-allprice,
                            dr["roomname"].ToString()+" 取消",
                            dr["berth"].ToString(),
                            0-allrebate
                        ));
                    }

                    if (AdjustFlag == "Adjust")
                    {
                        Sql.Add(string.Format("update OL_Order set OrderNums=OrderNums+{1},Adults=Adults+{2},Childs=Childs+{3},Price=Price+{4},rebate=rebate+{5} where OrderId='{0}'",
                            OrderId,
                            allPeple,
                            adultnm,
                            childnum,
                            allprice,
                            allrebate
                        ));

                        Sql.Add(string.Format("insert into CR_RoomOrder (OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,adult,childs,peoples,rooms,gather,roomname,berth,AllRebate) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                            OrderId,
                            dr["lineid"].ToString(),
                            dr["shipid"].ToString(),
                            dr["id"].ToString(),
                            dr["roomid"].ToString(),
                            dr["price"].ToString(),
                            dr["thirdprice"].ToString(),
                            dr["childprice"].ToString(),
                            dr["rebate"].ToString(),
                            dr["thirdrebate"].ToString(),
                            dr["childrebate"].ToString(),
                            adult,
                            childs,
                            allmen,
                            allroom,
                            allprice,
                            dr["roomname"].ToString(),
                            dr["berth"].ToString(),
                            allrebate
                        ));

                        Sql.Add(string.Format("update CR_RoomList set allotid='{1}',typeid='{2}',roomid='{3}',price='{4}',thirdprice='{5}',childprice='{6}',rebate='{7}',thirdrebate='{8}',childrebate='{9}',roomname='{10}',berth='{11}',roomcode='{12}',roomnoid='0',roomno='' where id='{0}'",
                            listid,
                            dr["id"].ToString(),
                            dr["typeid"].ToString(),
                            dr["roomid"].ToString(),
                            dr["price"].ToString(),
                            dr["thirdprice"].ToString(),
                            dr["childprice"].ToString(),
                            dr["rebate"].ToString(),
                            dr["thirdrebate"].ToString(),
                            dr["childrebate"].ToString(),
                            dr["roomname"].ToString(),
                            dr["berth"].ToString(),
                            dr["roomcode"].ToString()
                        ));
                    }
                    
                }
            }
            else
            {
                Response.Write("({\"error\":\"您选择的房型不存在，请重新选择\"})");
                Response.End();
            }
        }

        protected void CruisesRoomDoCancel()
        {
            string userid="", username="";
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                //username = Convert.ToString(Session["Online_UserName"]);
                //userid = Convert.ToString(Session["Online_UserId"]);
                Response.Write("({\"error\":\"您没有权限操作\"})");
                Response.End();
            }

            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where id='{0}'", Request.QueryString["ApplyId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {

                if (DS.Tables[0].Rows[0]["flag"].ToString() != "0")
                {
                    Response.Write("({\"error\":\"您选择的申请不能重复操作\"})");
                    Response.End();
                }

                SqlQueryText = string.Format("update OL_OrderApply set flag='2',douser='{0}',dotime='{1}' where id={2}",
                    username,
                    DateTime.Now.ToString(),
                    Request.QueryString["ApplyId"].Trim()
                );

                if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                {
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"舱房申请取消失败\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"舱房申请记录不存在\"})");
            }
        }

        protected void AdjustCruisesRoom()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }
            
            string SqlQueryText;
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{1}'", Request.QueryString["OldId"].Trim(), Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                // and applyflag='AdjustCruisesRoom' and originalid='{0}'
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次\"})");
                Response.End();
            }

            //string SqlQueryText = string.Format("select *,(select count(id) from CR_RoomList where orderflag='0' and allotid=CR_RoomAllot.id) as sellroom from CR_RoomAllot where id in ({0}) and lineid='{1}'", Request.Form["RS_ID"], Request.Form["TB_LineId"]);
            SqlQueryText = "SELECT * from View_CR_RoomAllot where (nums-sellroom)>0 and id='" + Request.QueryString["NewId"].Trim() + "'";
            
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count == 0)
            {
                Response.Write("({\"error\":\"您选择的舱房，剩余房间数量不足\"})");
                Response.End();
            }

            string infos = "", peoples = "";
            SqlQueryText = string.Format("select *,(select roomname from CR_RoomAllot where id={1}) as newroom from CR_RoomList where id='{0}'", Request.QueryString["OldId"].Trim(), Request.QueryString["NewId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                infos = DS.Tables[0].Rows[0]["roomname"].ToString() + " -> " + DS.Tables[0].Rows[0]["newroom"].ToString();
                peoples = DS.Tables[0].Rows[0]["peoples"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,updateid,infos,peoples) values ('{0}','{1}','{2}','{3}','AdjustCruisesRoom','{4}','{5}','{6}','{7}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                Request.QueryString["OldId"].Trim(),
                Request.QueryString["NewId"].Trim(),
                infos,
                peoples
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"舱房变更申请失败\"})");
            }
        }

        protected void CancelCruisesRoom()
        {
            string userid, username;
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
                userid = Convert.ToString(Session["Manager_UserId"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
                userid = Convert.ToString(Session["Online_UserId"]);
            }

            string SqlQueryText;
            //SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and applyflag='CancelCruisesRoom' and originalid='{0}' and orderid='{1}'", Request.QueryString["OldId"].Trim(), Request.QueryString["TempOrderId"].Trim());
            //DataSet DS = new DataSet();
            //DS.Clear();
            //DS = MyDataBaseComm.getDataSet(SqlQueryText);
            //if (DS.Tables[0].Rows.Count > 0)
            //{
            //    Response.Write("({\"error\":\"同一间房间，只能申请一次\"})");
            //    Response.End();
            //}
            SqlQueryText = string.Format("select * from OL_OrderApply where flag='0' and orderid='{1}'", Request.QueryString["OldId"].Trim(), Request.QueryString["TempOrderId"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("({\"error\":\"您已经提交过申请，在审核通过之前，同一订单只能申请一次\"})");
                Response.End();
            }

            string infos = "", peoples = "",nums= "0";
            SqlQueryText = string.Format("select * from CR_RoomList where id='{0}'", Request.QueryString["OldId"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                infos = DS.Tables[0].Rows[0]["roomname"].ToString();
                peoples = DS.Tables[0].Rows[0]["peoples"].ToString() + " (" + DS.Tables[0].Rows[0]["adults"].ToString() + "+" + DS.Tables[0].Rows[0]["childs"].ToString() + ")";
                nums = DS.Tables[0].Rows[0]["peoples"].ToString();
            }

            SqlQueryText = string.Format("insert into OL_OrderApply (orderid,username,OrderUser,inputdate,applyflag,originalid,infos,peoples,OrderNums) values ('{0}','{1}','{2}','{3}','CancelCruisesRoom','{4}','{5}','{6}','{7}')",
                Request.QueryString["TempOrderId"].Trim(),
                username,
                userid,
                DateTime.Now.ToString(),
                Request.QueryString["OldId"].Trim(),
                infos,
                peoples,
                nums
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"舱房取消申请失败\"})");
            }
        }

        protected void VisitSelect()
        {
            string username;
            //if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            //{
            //    username = Convert.ToString(Session["Online_UserName"]);
            //}
            //else
            //{
            //    username = Convert.ToString(Session["Manager_UserName"]);
            //}
            if (Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {
                username = Convert.ToString(Session["Manager_UserName"]);
            }
            else
            {
                username = Convert.ToString(Session["Online_UserName"]);
            }
            Sql = new List<string>();
            string[] vistid = Request.Form["V_ID"].Trim().Split(',');
            string[] guestno = Request.Form["V_NO"].Trim().Split(',');
            string[] guestname = Request.Form["V_NM"].Trim().Split(',');
            string[] AllGuestId = Request.Form["V_GID"].Trim().Split(',');
            //string b = a.Replace("-", "");  
            var GuestNos = "";
            var buling = "";
            if (vistid.Length > 0)
            {
                string OldPrice = MyDataBaseComm.getScalar("select ISNULL(sum(price),0) from View_CR_VisitList where flag='0' and OrderId='" + Request.QueryString["TempOrderId"] + "'");
                //string OldPrice = MyDataBaseComm.getScalar("select ISNULL(sum(SumPrice),0) from OL_OrderPrice where (PriceType='ShipVisit' or PriceType='CancelVisit') and OrderId='" + Request.QueryString["TempOrderId"] + "'");
                Sql.Add(string.Format("delete from OL_OrderPrice where PriceType='ShipVisit' and OrderId='{0}'", Request.QueryString["TempOrderId"]));
                Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", Request.QueryString["TempOrderId"]));
                
                Sql.Add(string.Format("UPDATE OL_GuestInfo set visitid = NULL where OrderId='{0}'",
                    Request.QueryString["TempOrderId"]
                ));

                for (int i = 0; i < vistid.Length; i++)
                {
                    GuestNos = guestno[i];
                    GuestNos = GuestNos.Replace("@", ",");
                    GuestNos = GuestNos.Substring(0, GuestNos.Length - 1);
                    if (i == vistid.Length - 1)
                    {
                        buling = ",0";
                    }
                    else
                    {
                        buling = ",";
                    }


                    Sql.Add(string.Format("UPDATE OL_TempPrice set infos='{2}',guestno='{3}',guestnostring='{4}' where OrderId='{0}' and PriceId='{1}' and PriceType='ShipVisit'",
                        Request.QueryString["TempOrderId"],
                        vistid[i],
                        guestname[i],
                        GuestNos,
                        guestno[i]
                    ));

                    Sql.Add(string.Format("UPDATE OL_GuestInfo set visitid = ISNULL(visitid, '0,') + '{1}' where OrderId='{0}' and rankno in ({2})",
                        Request.QueryString["TempOrderId"],
                        vistid[i] + buling,
                        GuestNos
                    ));

                    //保存观光列表
                    string[] GuestId = AllGuestId[i].Split('@');
                    string[] GuestRankNo = guestno[i].Split('@');
                    for (int ii = 0; ii < GuestId.Length - 1; ii++)
                    {
                        Sql.Add(string.Format("insert into CR_VisitList (orderid,visitid,guestid,rankno) values ('{0}','{1}','{2}','{3}')", Request.QueryString["TempOrderId"], vistid[i], GuestId[ii], GuestRankNo[ii]));
                    }

                }

                Sql.Add(string.Format("INSERT INTO OL_OrderPrice (OrderId,PriceType,PriceId,PriceName,PriceMemo,SellPrice,OrderNums,SumPrice,InputDate,infos,guestno,guestnostring) SELECT * FROM OL_TempPrice WHERE PriceType='ShipVisit' and OrderId='{0}'", Request.QueryString["TempOrderId"]));
                Sql.Add(string.Format("insert into OL_OrderLog (OrderId,LogTime,LogContent) values ('{0}','{1}','{2}')", Request.QueryString["TempOrderId"], DateTime.Now.ToString(), username + "修改了岸上观光项目"));

                string NewPrice = MyDataBaseComm.getScalar("select ISNULL(sum(SumPrice),0) from OL_TempPrice where PriceType='ShipVisit' and OrderId='" + Request.QueryString["TempOrderId"] + "'");

                Sql.Add(string.Format("UPDATE OL_Order set Price=Price + {0} - {1} where OrderId='{2}'", NewPrice, OldPrice, Request.QueryString["TempOrderId"]));
            

                string[] SqlQueryText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryText) == true)
                {
                    string result = PurchaseClass.CruisesOrderAdjust(Request.QueryString["TempOrderId"], "AdjustVisit", "Yes");
                    if (result == "OK")
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"success\":\"操作已成功完成，但畅游同步失败（" + result + "），请到同步记录中查看！\"})");
                    }
                }
                else
                {
                    Response.Write("({\"error\":\"订单提交失败，请稍后重试！\"})");
                }
            }

        }


        protected void ThirdStep()
        {
            string TempOrderid = MyDataBaseComm.getScalar("select OrderId from OL_TempOrder where OrderId='" + Request.QueryString["TempOrderId"] + "'");
            if (TempOrderid.ToString().Length < 5)
            {
                Response.Write("({\"error\":\"信息保存失败，订单已经提交！\"})");
                Response.End();
            }

            //Response.Write("({\"success\":\"" + Request.Form["V_ID"] + " // " + Request.Form["V_NO"] + " // " + Request.Form["V_NM"] + "\"})");
            Sql = new List<string>();

            if (Request.Form["V_ID"] != null)
            {
                string[] vistid = Request.Form["V_ID"].Trim().Split(',');
                string[] guestno = Request.Form["V_NO"].Trim().Split(',');
                string[] guestname = Request.Form["V_NM"].Trim().Split(',');
                string[] AllGuestId = Request.Form["V_GID"].Trim().Split(',');
                //string b = a.Replace("-", "");  
                var GuestNos = "";
                var buling = "";
                if (vistid.Length > 0)
                {
                    Sql.Add(string.Format("delete from CR_VisitList where OrderId='{0}'", Request.QueryString["TempOrderId"]));
                    //Sql.Add(string.Format("delete from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='{0}'", Request.QueryString["TempOrderId"]));
                    //Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["TempOrderId"], "CruisesDinner", Request.Form["dinner"].Split("@".ToCharArray())[0], Request.Form["dinner"].Split("@".ToCharArray())[1], DateTime.Now.ToString()));

                    Sql.Add(string.Format("UPDATE OL_GuestInfo set visitid = NULL where OrderId='{0}'",
                        Request.QueryString["TempOrderId"]
                    ));

                    for (int i = 0; i < vistid.Length; i++)
                    {
                        GuestNos = guestno[i];
                        GuestNos = GuestNos.Replace("@", ",");
                        GuestNos = GuestNos.Substring(0, GuestNos.Length - 1);
                        if (i == vistid.Length - 1)
                        {
                            buling = ",0";
                        }
                        else
                        {
                            buling = ",";
                        }

                        Sql.Add(string.Format("UPDATE OL_OrderPrice set infos='{2}',guestno='{3}',guestnostring='{4}' where OrderId='{0}' and PriceId='{1}' and PriceType='ShipVisit'",
                            Request.QueryString["TempOrderId"],
                            vistid[i],
                            guestname[i],
                            GuestNos,
                            guestno[i]
                        ));

                        Sql.Add(string.Format("UPDATE OL_GuestInfo set visitid = ISNULL(visitid, '0,') + '{1}' where OrderId='{0}' and rankno in ({2})",
                            Request.QueryString["TempOrderId"],
                            vistid[i] + buling,
                            GuestNos
                        ));

                        //保存观光列表
                        string[] GuestId = AllGuestId[i].Split('@');
                        string[] GuestRankNo = guestno[i].Split('@');
                        for (int ii = 0; ii < GuestId.Length - 1; ii++)
                        {
                            Sql.Add(string.Format("insert into CR_VisitList (orderid,visitid,guestid,rankno) values ('{0}','{1}','{2}','{3}')", Request.QueryString["TempOrderId"], vistid[i], GuestId[ii], GuestRankNo[ii]));
                        }
                    }
                }
            }
            else
            {
                //Sql.Add(string.Format("delete from OL_OrderExtend where ExtType='CruisesDinner' and OrderId='{0}'", Request.QueryString["TempOrderId"]));
                //Sql.Add(string.Format("insert into OL_OrderExtend (OrderId,ExtType,ExtId,ExtContent,InputTime) values ('{0}','{1}','{2}','{3}','{4}')", Request.QueryString["TempOrderId"], "CruisesDinner", Request.Form["dinner"].Split("@".ToCharArray())[0], Request.Form["dinner"].Split("@".ToCharArray())[1], DateTime.Now.ToString()));
                Response.Write("({\"error\":\"观光线路提取失败，请稍后重试！\"})");
                Response.End();
            }


            string[] SqlQueryText = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"订单提交失败，请稍后重试！\"})");
            }

        }

        protected void LoadVisitGuest()
        {
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = string.Format("select id,GuestName,rankno,visitid from OL_GuestInfo where OrderId='{0}'", Request.QueryString["orderid"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //checked=checked 
                string chks = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    //if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@1@4") == -1)
                    //{
                    //    Response.Write("没有操作权限！");
                    //    Response.End();
                    //}
                    //Request.QueryString["VNO"].IndexOf("@"+DS.Tables[0].Rows[i]["rankno"].ToString()+"@") DS.Tables[0].Rows[i]["rankno"].ToString()
                    //pic1 = string.Format("/Images/Views/{0}", elemList[i].SelectSingleNode("Pics").InnerText);
                    if (Request.QueryString["Id"] == DS.Tables[0].Rows[i]["visitid"].ToString() || Request.QueryString["VNO"].IndexOf("@" + DS.Tables[0].Rows[i]["rankno"].ToString() + "@") > 0)
                    {
                        chks = "checked=checked";
                    }
                    else
                    {
                        chks = "";
                    }
                    Strings.Append(string.Format("<DIV><input class=\"ChkIt\" type=checkbox id=\"CB{0}-{1}\" name=CheckBox{0} tgs=\"{2}\" onclick=\"SelectIts(this,{0})\" value=\"{1}\" {3}/>{2}</DIV>", Request.QueryString["Id"], DS.Tables[0].Rows[i]["rankno"].ToString(), DS.Tables[0].Rows[i]["GuestName"].ToString(), chks));
                }
            }
            //Response.Write(Strings.ToString());
            Response.Write(string.Format("<DIV><input type='checkbox' onclick='chkall(this,{0})' name='chk_all' id='chk_all'>全选</DIV>", Request.QueryString["Id"]) + Strings.ToString());

        }

        protected void OrderSubmit()
        {
            //Response.Write("({\"success\":\"" + Request.Form["RS_ID"] + "\"})");
            //Response.End();
            //string userid,userdept,usercompany,username;
            //userid = Convert.ToString(Session["Online_UserId"]);
            //userdept = Convert.ToString(Session["Online_UserDept"]);
            //usercompany = Convert.ToString(Session["Online_UserCompany"]);
            //username = Convert.ToString(Session["Online_UserName"]);
            //if (userid == "") userid = "0fe604b1-97f8-4b50-bda8-a15e01121760";
            //if (userdept == "") userdept = "0";
            //if (usercompany == "") usercompany = "0";
            

            //青旅呼叫中心订单
            string CcId = "0";
            if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1)
            {
                if (Request.Cookies["CallCenterOrderId"] != null)
                {
                    string CookieCcid = HttpUtility.UrlDecode(Convert.ToString(Request.Cookies["CallCenterOrderId"].Value));
                    CcId = MyConvert.ConToInt(CookieCcid).ToString();
                }
            }

            ucode = CombineKeys.NewComb();
            Sql = new List<string>();

            PurchaseClass.LineClass LineInfos = new PurchaseClass.LineClass();
            LineInfos = PurchaseClass.LineDetail(Request.Form["TB_LineId"]);
            if (LineInfos != null)
            {
                string SqlQueryText = string.Format("select * from View_CR_RoomAllot where id in ({0}) and lineid='{1}'", Request.Form["RS_ID"], Request.Form["TB_LineId"]);
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);

                //TB_LineId=2043&TB_BeginDate=2011-09-11&AllPeople=3&AllAdult=2&AllChilds=1&AllRoom=1&AllPrice=700&RS_ID=11&RS_CR=2&RS_ET=1&RS_ROOM=1&RS_PRICE=700
                string[] AllotId = Request.Form["RS_ID"].Split(',');
                string[] AllRoom = Request.Form["RS_ROOM"].Split(',');
                string[] AllMen = Request.Form["RS_NUM"].Split(',');
                string[] AllAdult = Request.Form["RS_CR"].Split(',');
                string[] AllChild = Request.Form["RS_ET"].Split(',');
                string[] AllPrice = Request.Form["RS_PRICE"].Split(',');

                //判断房型和价格是否符合，重要！！！！

                //判断是否有舱位,计算舱位价格，保存到舱位价格表
                for (int i = 0; i < AllotId.Length; i++)
                {
                    //Sql.Add(GetRoomSqlstr(DS.Tables[0], AllotId[i], AllPrice[i], Convert.ToInt32(AllRoom[i]), AllMen[i], AllAdult[i], AllChild[i]));
                    SqlQueryText = "";
                    SqlQueryText = GetRoomSqlstr(DS.Tables[0], AllotId[i], AllPrice[i], Convert.ToInt32(AllRoom[i]), AllMen[i], AllAdult[i], AllChild[i]);
                    if (SqlQueryText.Length > 0)
                    {
                        Sql.Add(SqlQueryText);
                    }
                    else
                    {
                        Response.Write("({\"error\":\"房间检查失败，请稍后再试\"})");
                    }
                }

                //Response.Write("({\"success\":\"" + SqlQueryText + "\"})");
                //Response.End();
                //保存订单信息，返回订单uid
                Sql.Add(string.Format("insert into OL_TempOrder (OrderId,ProductType,ProductClass,LineID,PlanId,LineName,BeginDate,OrderNums,Adults,Childs,OrderTime,OrderUser,DeptId,LineDays,RouteFlag,PlanNo,shipid,orderdept,ordercompany,ProductNum,UserName,ccid) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')",
                    ucode,
                    LineInfos.LineType,
                    LineInfos.LinesClass,
                    LineInfos.LineId,
                    LineInfos.Planid,
                    LineInfos.LineName,
                    MyConvert.ConToDate(Request.Form["TB_BeginDate"]),
                    Request.Form["AllPeople"],
                    Request.Form["AllAdult"],
                    Request.Form["AllChilds"],
                    DateTime.Now.ToString(),
                    Convert.ToString(Session["Online_UserId"]),
                    LineInfos.Deptid,
                    LineInfos.LineDays,
                    "1",
                    "",
                    LineInfos.Shipid,
                    Convert.ToString(Session["Online_UserDept"]),
                    Convert.ToString(Session["Online_UserCompany"]),
                    Request.Form["AllRoom"],
                    Convert.ToString(Session["Online_UserName"]),
                    CcId
                ));

                string[] SqlQueryList = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryList) == true)
                {
                    Response.Write("({\"success\":\"" + ucode + "\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"订单预定失败，请稍后再试\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"旅游线路加载错误\"})");
            }

        }

        protected string GetRoomSqlstr(DataTable dt, string allotid, string price, int allroom, string allmen, string adult, string childs)
        {
            int price1, price2, price3, adultnm, childnum, allPeple, allrebate;
            adultnm = MyConvert.ConToInt(adult);
            childnum = MyConvert.ConToInt(childs);
            allPeple = MyConvert.ConToInt(allmen);

            string sql = "";
            DataRow[] drs = dt.Select("id=" + allotid);
            if (drs.Count() > 0)
            {
                foreach (DataRow dr in drs)
                {
                    int haveroom = Convert.ToInt32(dr["nums"].ToString()) - Convert.ToInt32(dr["sellroom"].ToString());

                    if (haveroom < allroom)
                    {
                        Response.Write("({\"error\":\"" + dr["roomname"].ToString() + "房间剩余数量不足\"})");
                        Response.End();
                    }
                    else
                    {
                        allrebate = 0;
                        price1 = Convert.ToInt32(dr["rebate"].ToString());
                        price2 = Convert.ToInt32(dr["thirdrebate"].ToString());
                        price3 = Convert.ToInt32(dr["childrebate"].ToString());
                        int berth = Convert.ToInt32(dr["berth"].ToString());

                        switch (berth)
                        {
                            case 1:
                                allrebate = allPeple * price1;
                                break;
                            case 2:
                                allrebate = allroom * price1 * 2;
                                break;
                                //case 3:
                                //    allrebate = allroom * price1 * 2;
                                //    if (childnum >= allroom) allrebate += allroom * price3;
                                //    if (childnum < allroom)
                                //    {
                                //        allrebate += childnum * price3;
                                //        allrebate += (allroom - childnum) * price2;
                                //    }
                                //    break;
                                //case 4:
                                //    allrebate = allroom * price1 * 2;
                                //    if (childnum >= allroom)
                                //    {
                                //        allrebate += allroom * price2;
                                //        allrebate += allroom * price3;
                                //    }
                                //    if (childnum < allroom)
                                //    {
                                //        allrebate += childnum * price3;
                                //        allrebate += (allroom * 2 - childnum) * price2;
                                //    }
                                //    break;
                            default:
                                allrebate = allroom * price1 * 2;
                                if (childnum >= allroom * (berth - 2))
                                {
                                    allrebate += allroom * (berth - 2) * price3;
                                }
                                else
                                {
                                    allrebate += childnum * price3;
                                    allrebate += (allroom * (berth - 2) - childnum) * price2;
                                }
                                break;
                        }

                        //计算总价是否相等
                        int allprice = 0;
                        int price_1, price_2, price_3;
                        price_1 = Convert.ToInt32(dr["price"].ToString());
                        price_2 = Convert.ToInt32(dr["thirdprice"].ToString());
                        price_3 = Convert.ToInt32(dr["childprice"].ToString());
                        
                        switch (berth)
                        {
                            case 1:
                                allprice = allPeple * price_1;
                                break;
                            case 2:
                                allprice = allroom * price_1 * 2;
                                break;
                                //case 3:
                                //    allprice = allroom * price_1 * 2;
                                //    if (childnum >= allroom) allprice += allroom * price_3;
                                //    if (childnum < allroom)
                                //    {
                                //        allprice += childnum * price_3;
                                //        allprice += (allroom - childnum) * price_2;
                                //    }
                                //    break;
                                //case 4:
                                //    allprice = allroom * price_1 * 2;
                                //    if (childnum >= allroom)
                                //    {
                                //        allprice += allroom * price_2;
                                //        allprice += allroom * price_3;
                                //    }
                                //    if (childnum < allroom)
                                //    {
                                //        allprice += childnum * price_3;
                                //        allprice += (allroom * 2 - childnum) * price_2;
                                //    }
                                //    break;
                            default:
                                allprice = allroom * price_1 * 2;
                                if (childnum >= allroom * (berth - 2))
                                {
                                    allprice += allroom * (berth - 2) * price_3;
                                }
                                else
                                {
                                    allprice += childnum * price_3;
                                    allprice += (allroom * (berth - 2) - childnum) * price_2;
                                }
                                break;
                        }

                        if (allprice != Convert.ToInt32(price))
                        {
                            Response.Write("({\"error\":\"舱房价格计算错误\"})");
                            Response.End();
                        }

                        //非同行预定，返利为零
                        if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserDept"])) == 0) allrebate = 0;
                        //青旅内部预定，返利为零
                        if (MyConvert.ConToInt(Convert.ToString(Session["Online_UserCompany"])) == 1) allrebate = 0;

                        sql = string.Format("insert into CR_RoomOrder (OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,adult,childs,peoples,rooms,gather,roomname,berth,AllRebate) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                            ucode,
                            dr["lineid"].ToString(),
                            dr["shipid"].ToString(),
                            dr["id"].ToString(),
                            dr["roomid"].ToString(),
                            dr["price"].ToString(),
                            dr["thirdprice"].ToString(),
                            dr["childprice"].ToString(),
                            dr["rebate"].ToString(),
                            dr["thirdrebate"].ToString(),
                            dr["childrebate"].ToString(),
                            adult,
                            childs,
                            allmen,
                            allroom,
                            price,
                            dr["roomname"].ToString(),
                            dr["berth"].ToString(),
                            allrebate
                        );
                        
                        int DoubleRoom = 0;
                        int SingleRoom = 0;
                        if (berth == 2)
                        {
                            //房间数*2-人数=只住1人房间数
                            SingleRoom = allroom * 2 - Convert.ToInt32(allmen);
                            DoubleRoom = allroom - SingleRoom;
                        }
                        else
                        {
                            DoubleRoom = allroom;
                            SingleRoom = 0;
                        }

                        for (int i = 0; i < DoubleRoom; i++)
                        {
                            Sql.Add(string.Format("insert into CR_RoomList (OrderId,lineid,allotid,typeid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,roomname,berth,peoples,roomcode) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                                ucode,
                                dr["lineid"].ToString(),
                                dr["id"].ToString(),
                                dr["typeid"].ToString(),
                                dr["roomid"].ToString(),
                                dr["price"].ToString(),
                                dr["thirdprice"].ToString(),
                                dr["childprice"].ToString(),
                                dr["rebate"].ToString(),
                                dr["thirdrebate"].ToString(),
                                dr["childrebate"].ToString(),
                                dr["roomname"].ToString(),
                                dr["berth"].ToString(),
                                dr["berth"].ToString(),
                                dr["roomcode"].ToString()
                            ));
                        }

                        for (int i = 0; i < SingleRoom; i++)
                        {
                            Sql.Add(string.Format("insert into CR_RoomList (OrderId,lineid,allotid,typeid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,roomname,berth,peoples,roomcode) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                                ucode,
                                dr["lineid"].ToString(),
                                dr["id"].ToString(),
                                dr["typeid"].ToString(),
                                dr["roomid"].ToString(),
                                dr["price"].ToString(),
                                dr["thirdprice"].ToString(),
                                dr["childprice"].ToString(),
                                dr["rebate"].ToString(),
                                dr["thirdrebate"].ToString(),
                                dr["childrebate"].ToString(),
                                dr["roomname"].ToString(),
                                dr["berth"].ToString(),
                                "1",
                                dr["roomcode"].ToString()
                            ));
                        }
                        //OrderId,lineid,shipid,allotid,roomid,price,thirdprice,childprice,rebate,thirdrebate,childrebate,
                        //adult,childs,peoples,rooms,gather
                    }
                }
            }
            else
            {
                Response.Write("({\"error\":\"您选择的房型不存在，请重新选择\"})");
                Response.End();
            }

            return sql;
        }


    }
}