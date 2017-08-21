using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Travel;
using System.Data;
using TravelOnline.Class.Common;
using System.Text;
using TravelOnline.Class.Purchase;
using System.Collections;

namespace TravelOnline.Cruises
{
    public partial class AjaxService : System.Web.UI.Page
    {
        public List<string> Sql;
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
                Response.Write("{\"info\":\"尚未登录\"}");
                Response.End();
            }
            switch (Request.QueryString["action"])
            {
                case "VisitGroupDo":
                    VisitGroupDo();
                    break;
                case "CancelRoomNo":
                    CancelRoomNo();
                    break;
                case "CruisesCompany":
                    CruisesCompany();
                    break;
                case "CruisesShip":
                    CruisesShip();
                    break;
                case "CruisesRoom":
                    CruisesRoom();
                    break;
                case "DeleteCruisesRoom":
                    DeleteCruisesRoom();
                    break;
                case "DeleteCruisesCompany":
                    DeleteCruisesCompany();
                    break;
                case "DeleteCruisesShip":
                    DeleteCruisesShip();
                    break;
                case "CruisesPic":
                    CruisesPic();
                    break;
                case "DeleteCruisesPic":
                    DeleteCruisesPic();
                    break;
                case "CruisesLineSet":
                    CruisesLineSet();
                    break;
                case "LineRoomAllot":
                    LineRoomAllot();
                    break;
                case "DeleteCruisesRoomAllot":
                    DeleteCruisesRoomAllot();
                    break;
                case "CruisesRoute":
                    CruisesRoute();
                    break;
                case "CruisesVisit":
                    CruisesVisit();
                    break;
                case "DeleteCruisesVisit":
                    DeleteCruisesVisit();
                    break;
                case "CruisesRebate":
                    CruisesRebate();
                    break;
                case "DeleteCruisesRebate":
                    DeleteCruisesRebate();
                    break;
                case "CruisesDamage":
                    CruisesDamage();
                    break;
                case "DeleteCruisesDamage":
                    DeleteCruisesDamage();
                    break;
                case "CruisesConfirm":
                    CruisesConfirm();
                    break;
                case "CruisesRoomNoDelete":
                    CruisesRoomNoDelete();
                    break;
                case "CruisesCheckInCancel":
                    CruisesCheckInCancel();
                    break;
                case "CruisesCarNoAllot":
                    CruisesCarNoAllot();
                    break;
                case "CruisesCarNoDelete":
                    CruisesCarNoDelete();
                    break;
                case "CruisesCarSeatCancel":
                    CruisesCarSeatCancel();
                    break;
                case "CruisesDinnerNoDelete":
                    CruisesDinnerNoDelete();
                    break;
                case "CruisesDinnerSeatCancel":
                    CruisesDinnerSeatCancel();
                    break;
                case "CruisesPlanNoAllot":
                    CruisesPlanNoAllot();
                    break;
                case "EditPlanNoAllot":
                    EditPlanNoAllot();
                    break;
                case "EditCarNoAllot":
                    EditCarNoAllot();
                    break;
                case "CruisesPlanNoDelete":
                    CruisesPlanNoDelete();
                    break;
                case "CruisesPlanSeatCancel":
                    CruisesPlanSeatCancel();
                    break;
                case "ClearCache":
                    ClearCache();
                    break;
                case "EditRoomNo":
                    EditRoomNo();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }

        protected void EditRoomNo()
        {
            //Response.Write("({\"success\":\"OK\"})");
            //Response.End();
            string SqlQueryText = string.Format("update CR_RoomNo set RoomName='{1}',berth='{2}' where id ='{0}'", Request.Form["roomid"], Request.Form["roomname"].Trim(), Request.Form["berth"].Trim());

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void CancelRoomNo()
        {
            Sql = new List<string>();
            string SqlQueryText;
            DataSet DS = new DataSet();
            SqlQueryText = string.Format("select * from CR_RoomNo where Flag='2' and id='{0}'", Request.QueryString["roomnoid"].Trim());
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Listid"].ToString()) == 0)
                {
                    Response.Write("{\"success\":\"您选择的房号不是拼房，不能操作!\"}");
                    Response.End();
                }

                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Mergeid"].ToString()) == 0)
                {
                    Response.Write("{\"success\":\"您选择的房号不是拼房，不能操作!\"}");
                    Response.End();
                }

                Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Mergeid='0',Nums='0',DoDate=NULL where Id='{0}'",
                    DS.Tables[0].Rows[0]["id"].ToString()
                ));

                Sql.Add(string.Format("update CR_RoomList set roomnoid='0',roomno='',MergeFlag='0' where Id='{0}'",
                    DS.Tables[0].Rows[0]["ListId"].ToString()
                ));

                Sql.Add(string.Format("update CR_RoomList set roomnoid='0',roomno='',MergeFlag='0' where Id='{0}'",
                    DS.Tables[0].Rows[0]["Mergeid"].ToString()
                ));
            }
            else
            {
                Response.Write("{\"success\":\"您选择的房号不存在!\"}");
                Response.End();
            }

            string[] SqlQuerys = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuerys) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"房号取消失败，请稍后再试!\"}");
            }
        }

        protected void VisitGroupDo()
        {
            string flag = "0";
            if (Request.QueryString["flag"] == "VisitStopSell") flag = "1";

            string SqlQueryText = string.Format("update CR_Visit set sellflag='{1}' where id in ({0})", Request.QueryString["Id"], flag);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"操作失败，请稍后再试!\"}");
            }
        }

        protected void ClearCache()
        {

            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            string flag = "0";
            foreach (string key in al)
            {
                flag = "0";
                if (key.IndexOf("CruisesRoomList_") > -1) flag = "1";
                if (flag == "1") _cache.Remove(key);
            } 
            Response.Write("{\"success\":0}");
        }

        protected void EditPlanNoAllot()
        {
            string SqlQueryText = string.Format("update CR_PlanNo set Berth='{1}',memo='{2}' where id='{0}'",
                Request.Form["lineid"].Trim(),
                Request.Form["Berth"].Trim(),
                Request.Form["memo"].Trim()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"分团号修改失败，请重稍后再试!\"})");
            }
        }

        protected void EditCarNoAllot()
        {
            string SqlQueryText = string.Format("update CR_BusNo set Berth='{1}' where id='{0}'",
                Request.Form["lineid"].Trim(),
                Request.Form["nums"].Trim()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"车号修改失败，请重稍后再试!\"})");
            }
        }

        protected void CruisesPlanSeatCancel()
        {
            string result = PurchaseClass.MisCruisesPlanNoCancel(Request.QueryString["Lineid"], Request.QueryString["Id"]);
            if (result != "OK")
            {
                Response.Write("{\"success\":\"畅游分团号同步失败，请稍后再试!\"}");
                Response.End();
            }

            string SqlQueryText = string.Format("update OL_GuestInfo set PlanAllotid=null where roomid>0 and PlanAllotid in ({0})", Request.QueryString["Id"]);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"分团号分配取消失败，请稍后再试!\"}");
            }
        }

        protected void CruisesPlanNoDelete()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_GuestInfo where flag='0' and PlanAllotid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择的分团号，已有分配记录，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_PlanNo");
            }
        }

        protected void CruisesPlanNoAllot()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            int num1 = MyConvert.ConToInt(Request.Form["Plan1"].Trim()), num2 = MyConvert.ConToInt(Request.Form["Plan2"].Trim());

            SqlQueryText = string.Format("select top 1 id from CR_PlanNo where Lineid='{0}' and PlanNo>={1} and PlanNo<={2}", Request.Form["lineid"].Trim(), num1, num2);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("({\"error\":\"团号已存在，请重新输入!\"})");
                Response.End();
            }
            for (int i = num1; i <= num2; i++)
            {
                Sql.Add(string.Format("insert into CR_PlanNo (lineid,PlanNo,Berth,memo) values ('{0}','{1}','{2}','{3}')",
                    Request.Form["lineid"].Trim(),
                    i,
                    Request.Form["Berth"].Trim(),
                    Request.Form["memo"].Trim()
                ));
            }
            string[] SqlQuerys = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuerys) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"团号分配失败，请重稍后再试!\"})");
            }
        }

        protected void CruisesDinnerSeatCancel()
        {
            string SqlQueryText = string.Format("update OL_GuestInfo set DinnerId=null where roomid>0 and DinnerId in ({0})", Request.QueryString["Id"]);

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"餐桌号分配取消失败，请稍后再试!\"}");
            }
        }

        protected void CruisesDinnerNoDelete()
        {
            string SqlQueryText = string.Format("select top 1 id from OL_GuestInfo where flag='0' and DinnerId in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择的餐桌号，已有分配记录，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_DinnerNo");
            }
        }

        protected void CruisesCarSeatCancel()
        {
            string SqlQueryText = string.Format("update CR_VisitList set Busid=null,BusNo=null where Busid in ({0})", Request.QueryString["Id"]);
            
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"车号分配取消失败，请稍后再试!\"}");
            }
        }


        protected void CruisesCarNoDelete()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_VisitList where flag='0' and Busid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择的车号，已有分配记录，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_BusNo");
            }
        }

        protected void CruisesCarNoAllot()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from CR_Visit where lineid='{0}' and id='{1}'", Request.Form["lineid"].Trim(), Request.Form["visitid"].Trim());
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                int num1 = MyConvert.ConToInt(Request.Form["No1"].Trim()), num2 = MyConvert.ConToInt(Request.Form["No2"].Trim());

                SqlQueryText = string.Format("select top 1 id from CR_BusNo where Lineid='{0}' and Days='{1}' and BusNo>={2} and BusNo<={3}", Request.Form["lineid"].Trim(), DS.Tables[0].Rows[0]["days"].ToString(), num1, num2);
                if (MyDataBaseComm.getScalar(SqlQueryText) != null)
                {
                    Response.Write("({\"error\":\"车号已存在，请重新输入!\"})");
                    Response.End();
                }
                for (int i = num1; i <= num2; i++)
                {
                    Sql.Add(string.Format("insert into CR_BusNo (lineid,BusNo,Berth,Days,Visitid,vtitle,visitname) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        Request.Form["lineid"].Trim(),
                        i,
                        Request.Form["nums"].Trim(),
                        DS.Tables[0].Rows[0]["days"].ToString(),
                        DS.Tables[0].Rows[0]["id"].ToString(),
                        DS.Tables[0].Rows[0]["vtitle"].ToString(),
                        DS.Tables[0].Rows[0]["visitname"].ToString()
                    ));
                }
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
                Response.Write("({\"error\":\"没有查询到观光线路，请重新选择!\"})");
            }
        }

        protected void CruisesCheckInCancel()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            SqlQueryText = string.Format("select * from CR_RoomNo where flag<>'0' and Listid>0 and id in ({0})", Request.QueryString["Id"]);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    //取消房号入住信息
                    Sql.Add(string.Format("update CR_RoomNo set Flag='0',Listid='0',Mergeid='0',Nums='0',DoDate=NULL where Id='{0}'",
                        DS.Tables[0].Rows[i]["id"].ToString()
                    ));

                    //取消客人入住
                    Sql.Add(string.Format("update CR_RoomList set roomnoid='0',roomno='' where Id='{0}'",
                        DS.Tables[0].Rows[i]["Listid"].ToString()
                    ));

                    if (MyConvert.ConToInt(DS.Tables[0].Rows[i]["Mergeid"].ToString()) > 0)
                    {
                        Sql.Add(string.Format("update CR_RoomList set roomnoid='0',roomno='' where Id='{0}'",
                            DS.Tables[0].Rows[i]["Mergeid"].ToString()
                        ));
                    }
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

        protected void CruisesRoomNoDelete()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_RoomNo where (flag<>'0' or Nums>0 or Listid>0) and id in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择的分配记录，房号已入住，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_RoomNo");
            }
        }

        protected void CruisesConfirm()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into CR_Confirm (lineid,visit,pay,cancel,visa,change,other,views) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                    Request.Form["lineid"].Trim(),
                    Request.Form["visit"].Trim(),
                    Request.Form["pay"].Trim(),
                    Request.Form["cancel"].Trim(),
                    Request.Form["visa"].Trim(),
                    Request.Form["change"].Trim(),
                    Request.Form["other"].Trim(),
                    Request.Form["views"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Confirm set visit='{1}',pay='{2}',cancel='{3}',visa='{4}',change='{5}',other='{6}',views='{7}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["visit"].Trim(),
                    Request.Form["pay"].Trim(),
                    Request.Form["cancel"].Trim(),
                    Request.Form["visa"].Trim(),
                    Request.Form["change"].Trim(),
                    Request.Form["other"].Trim(),
                    Request.Form["views"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void DeleteCruisesDamage()
        {
            DeleteSelectInfos("CR_Damage");
        }

        protected void CruisesDamage()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into CR_Damage (lineid,begindate,enddate,flag,rate,infos) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    Request.Form["lineid"].Trim(),
                    Request.Form["begindate"].Trim(),
                    Request.Form["enddate"].Trim(),
                    Request.Form["flag"].Trim(),
                    Request.Form["rate"].Trim(),
                    Request.Form["infos"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Damage set begindate='{1}',enddate='{2}',flag='{3}',rate='{4}',infos='{5}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["begindate"].Trim(),
                    Request.Form["enddate"].Trim(),
                    Request.Form["flag"].Trim(),
                    Request.Form["rate"].Trim(),
                    Request.Form["infos"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void DeleteCruisesRebate()
        {
            DeleteSelectInfos("CR_Rebate");
        }

        protected void DeleteCruisesVisit()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_VisitList where flag='0' and visitid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择删除的岸上观光，已经预定过，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_Visit");
            }
        }

        protected void DeleteCruisesRoomAllot()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_RoomList where orderflag='0' and allotid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择删除的舱房，已经预定过，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_RoomAllot");
            }
        }

        protected void DeleteCruisesRoom()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_RoomAllot where roomid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择删除的舱房，已经在邮轮线路中分配过，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_ShipRoom");
            }
        }

        protected void DeleteCruisesPic()
        {
            DeleteSelectInfos("CR_Pic");
        }

        protected void CruisesRebate()
        {
            List<string> Sql = new List<string>();
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                if (Request.Form["flag"] == "group")
                {
                    SqlQueryText = string.Format("select id,roomid,lineid from CR_RoomAllot where lineid='{0}' and roomid in ({1})", Request.Form["lineid"], Request.Form["roomid"]);
                }
                else
                {
                    SqlQueryText = string.Format("select id,roomid,lineid from CR_RoomAllot where id in ({0})", Request.Form["allotid"]);
                }
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Sql.Add(string.Format("insert into CR_Rebate (lineid,roomid,allotid,begindate,enddate,infos,price,flag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            DS.Tables[0].Rows[i]["lineid"].ToString(),
                            DS.Tables[0].Rows[i]["roomid"].ToString(),
                            DS.Tables[0].Rows[i]["id"].ToString(),
                            Request.Form["begindate"].Trim(),
                            Request.Form["enddate"].Trim(),
                            Request.Form["infos"].Trim(),
                            Request.Form["price"].Trim(),
                            Request.Form["Rebateflag"].Trim()
                        ));
                    }
                    string[] SqlQuerys = Sql.ToArray();
                    if (MyDataBaseComm.Transaction(SqlQuerys) == true)
                    {
                        Response.Write("({\"success\":\"OK\"})");
                    }
                    else
                    {
                        Response.Write("({\"error\":\"信息保存失败\"})");
                    }
                }
                else
                {
                    Response.Write("({\"error\":\"增加失败，没有查询到任何舱房分配记录\"})");
                }
            }
            else
            {
                SqlQueryText = string.Format("update CR_Rebate set begindate='{1}',enddate='{2}',infos='{3}',price='{4}',flag='{5}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["begindate"].Trim(),
                    Request.Form["enddate"].Trim(),
                    Request.Form["infos"].Trim(),
                    Request.Form["price"].Trim(),
                    Request.Form["Rebateflag"].Trim()
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
        }

        protected void CruisesVisit()
        {
            string SqlQueryText;

            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into CR_Visit (lineid,days,vtitle,visitname,stay,sight,dinner,intro,price,nums,vdate,vmemo) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                    Request.Form["lineid"].Trim(),
                    Request.Form["days"].Trim(),
                    Request.Form["vtitle"].Trim(),
                    Request.Form["visitname"].Trim(),
                    Request.Form["stay"].Trim(),
                    Request.Form["sight"].Trim(),
                    Request.Form["dinner"].Trim(),
                    Request.Form["intro"].Trim(),
                    MyConvert.ConToInt(Request.Form["price"].Trim()),
                    MyConvert.ConToInt(Request.Form["nums"].Trim()),
                    Request.Form["vdate"].Trim(),
                    Request.Form["vmemo"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Visit set days='{1}',vtitle='{2}',visitname='{3}',stay='{4}',sight='{5}',dinner='{6}',intro='{7}',price='{8}',nums='{9}',vdate='{10}',vmemo='{11}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["days"].Trim(),
                    Request.Form["vtitle"].Trim(),
                    Request.Form["visitname"].Trim(),
                    Request.Form["stay"].Trim(),
                    Request.Form["sight"].Trim(),
                    Request.Form["dinner"].Trim(),
                    Request.Form["intro"].Trim(),
                    MyConvert.ConToInt(Request.Form["price"].Trim()),
                    MyConvert.ConToInt(Request.Form["nums"].Trim()),
                    Request.Form["vdate"].Trim(),
                    Request.Form["vmemo"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void CruisesRoute()
        {
            List<string> Sql = new List<string>();
            Sql.Add(string.Format("delete from CR_Route where lineid='{0}'", Request.Form["lineid"].Trim()));

            Sql.Add(string.Format("update OL_Line set dinner='{1}' where MisLineId='{0}'",
                Request.Form["lineid"].Trim(),
                Request.Form["Dtime1"].Trim() + "@" + Request.Form["Dnum1"].Trim() + "|" + Request.Form["Dtime2"].Trim() + "@" + Request.Form["Dnum2"].Trim()
            ));
            
            string[] harbour = Request.Form["harbour"].Trim().Split(',');
            string[] arrive = Request.Form["arrive"].Trim().Split(',');
            string[] sail = Request.Form["sail"].Trim().Split(',');
            string[] visit = Request.Form["visit"].Trim().Split(',');

            //SqlQueryText = Request.Form["harbour"].Trim() + " / " + Request.Form["arrive"].Trim() + " / " + Request.Form["sail"].Trim();
            //Response.Write("({\"success\":\"" + SqlQueryText + "\"})");
            if (harbour.Length > 0)
            {
                for (int i = 0; i < harbour.Length; i++)
                {
                    Sql.Add(string.Format("insert into CR_Route (lineid,days,harbour,arrive,sail,visit) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                        Request.Form["lineid"].Trim(),
                        i + 1,
                        harbour[i],
                        arrive[i],
                        sail[i],
                        visit[i]
                        )
                    );
                }

                string[] SqlQueryText = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryText) == true)
                {
                    //清空包船线路图片和港口缓存内容
                    HttpContext.Current.Cache.Insert(string.Format("CruisesRouteString{0}", Request.Form["lineid"].Trim()), "");
                    HttpContext.Current.Cache.Insert(string.Format("CruisesPicUrl{0}", Request.Form["shipid"].Trim()), "");
                    Response.Write("({\"success\":\"OK\"})");
                }
                else
                {
                    Response.Write("({\"error\":\"信息保存失败\"})");
                }
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void LineRoomAllot()
        {
            string SqlQueryText;
            
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("select * from CR_RoomAllot where allotflag='{0}' and roomid='{1}' and lineid='{2}' and companyid='{3}'", Request.Form["allotflag"].Trim(), Request.Form["roomid"].Trim(), Request.Form["lineid"].Trim(), MyConvert.ConToInt(Request.Form["companyid"].Trim()));
                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    Response.Write("({\"error\":\"同一间房间，相同的销售方式只能增加一次\"})");
                    Response.End();
                }

                string typeid = "", typename = "", roomname = "", roomcode = "", berth = "", area = "", deck = "";
                SqlQueryText = string.Format("select * from CR_ShipRoom where Id='{0}'", Request.Form["roomid"].Trim());
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    typeid = DS.Tables[0].Rows[0]["typeid"].ToString();
                    typename = DS.Tables[0].Rows[0]["typename"].ToString();
                    roomname = DS.Tables[0].Rows[0]["roomname"].ToString();
                    roomcode = DS.Tables[0].Rows[0]["roomcode"].ToString();
                    berth = DS.Tables[0].Rows[0]["berth"].ToString();
                    area = DS.Tables[0].Rows[0]["area"].ToString();
                    deck = DS.Tables[0].Rows[0]["deck"].ToString();
                }
                else
                {
                    Response.Write("({\"error\":\"信息保存失败，没找到任何数据\"})");
                    Response.End();
                }

                SqlQueryText = string.Format("insert into CR_RoomAllot (allotflag,lineid,shipid,roomid,companyid,company,typeid,typename,roomname,roomcode,berth,nums,price,thirdprice,rebate,area,deck,childprice,thirdrebate,childrebate,sellflag,recommend) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')",
                    Request.Form["allotflag"].Trim(),
                    Request.Form["lineid"].Trim(),
                    Request.Form["shipid"].Trim(),
                    Request.Form["roomid"].Trim(),
                    MyConvert.ConToInt(Request.Form["companyid"].Trim()),
                    Request.Form["company"].Trim(),
                    typeid,
                    typename,
                    roomname,
                    roomcode,
                    berth,
                    MyConvert.ConToInt(Request.Form["nums"].Trim()),
                    MyConvert.ConToInt(Request.Form["price"].Trim()),
                    MyConvert.ConToInt(Request.Form["thirdprice"].Trim()),
                    MyConvert.ConToInt(Request.Form["rebate"].Trim()),
                    area,
                    deck,
                    MyConvert.ConToInt(Request.Form["childprice"].Trim()),
                    MyConvert.ConToInt(Request.Form["thirdrebate"].Trim()),
                    MyConvert.ConToInt(Request.Form["childrebate"].Trim()),
                    Request.Form["sellflag"].Trim(),
                    Request.Form["recommend"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_RoomAllot set nums='{1}',price='{2}',thirdprice='{3}',rebate='{4}',childprice='{5}',thirdrebate='{6}',childrebate='{7}',sellflag='{8}',recommend='{9}' where id={0}",
                    Request.Form["Cid"],
                    MyConvert.ConToInt(Request.Form["nums"].Trim()),
                    MyConvert.ConToInt(Request.Form["price"].Trim()),
                    MyConvert.ConToInt(Request.Form["thirdprice"].Trim()),
                    MyConvert.ConToInt(Request.Form["rebate"].Trim()),
                    MyConvert.ConToInt(Request.Form["childprice"].Trim()),
                    MyConvert.ConToInt(Request.Form["thirdrebate"].Trim()),
                    MyConvert.ConToInt(Request.Form["childrebate"].Trim()),
                    Request.Form["sellflag"].Trim(),
                    Request.Form["recommend"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                HttpContext.Current.Cache.Insert(string.Format("CruisesRoomString{0}", Request.Form["lineid"].Trim()), "");
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void CruisesLineSet()
        {
            string SqlQueryText;

            //检查是否有做过包船的舱位分配
            string VisitSell = "0";
            if (Request.Form["VisitSell"] != null) VisitSell = "1";
            SqlQueryText = string.Format("update OL_Line set Shipid='{1}',Planid='{2}',PlanDate='{3}',AgeLimit='{4}',VisitSell='{5}',CruisesReport='{6}' where MisLineId={0}",
                Request.Form["Cid"],
                Request.Form["shipid"].Trim(),
                Request.Form["planid"].Trim(),
                Request.Form["PlanDate"].Trim(),
                Request.Form["AgeLimit"].Trim(),
                VisitSell,
                Request.Form["CruisesReport"].Trim()
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

        protected void CruisesPic()
        {
            string SqlQueryText;

            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into CR_Pic (shipid,roomid,deck,pictype,roomtype,cname,picurl) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    Request.Form["shipid"].Trim(),
                    Request.Form["roomid"].Trim(),
                    Request.Form["deck"].Trim(),
                    Request.Form["pictype"].Trim(),
                    Request.Form["roomtype"].Trim(),
                    Request.Form["cname"].Trim(),
                    Request.Form["picurl"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Pic set roomid='{1}',deck='{2}',pictype='{3}',roomtype='{4}',cname='{5}',picurl='{6}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["roomid"].Trim(),
                    Request.Form["deck"].Trim(),
                    Request.Form["pictype"].Trim(),
                    Request.Form["roomtype"].Trim(),
                    Request.Form["cname"].Trim(),
                    Request.Form["picurl"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void DeleteCruisesShip()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_ShipRoom where shipid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择删除的船队，已经录入了舱房，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_Ship");
            }
        }

        protected void DeleteCruisesCompany()
        {
            string SqlQueryText = string.Format("select top 1 id from CR_Ship where comid in ({0})", Request.QueryString["Id"]);
            if (MyDataBaseComm.getScalar(SqlQueryText) != null)
            {
                Response.Write("{\"success\":\"您选择删除的邮轮公司，已经录入了船队，请重新选择!\"}");
                Response.End();
            }
            else
            {
                DeleteSelectInfos("CR_Company");
            }
        }

        protected void DeleteSelectInfos(string DbTableName)
        {
            if (MyDataBaseComm.DeleteExcuteSql(DbTableName, "", string.Format("{0}", Request.QueryString["Id"])) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":\"信息删除失败，请稍后再试!\"}");
            }
            Response.End();
        }

        protected void CruisesRoom()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                Guid ucode = CombineKeys.NewComb();
                SqlQueryText = string.Format("insert into CR_ShipRoom (uid,comid,shipid,typeid,typename,roomname,roomcode,configure,deck,area,berth,intro,rooms) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                    ucode,
                    Request.Form["comid"].Trim(),
                    Request.Form["shipid"].Trim(),
                    Request.Form["typeid"].Trim(),
                    Request.Form["typename"].Trim(),
                    Request.Form["roomname"].Trim(),
                    Request.Form["roomcode"].Trim(),
                    Request.Form["configure"].Trim(),
                    Request.Form["deck"].Trim(),
                    Request.Form["area"].Trim(),
                    MyConvert.ConToInt(Request.Form["berth"].Trim()),
                    Request.Form["intro"].Trim(),
                    Request.Form["rooms"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_ShipRoom set typeid='{1}',typename='{2}',roomname='{3}',roomcode='{4}',configure='{5}',deck='{6}',area='{7}',berth='{8}',intro='{9}',rooms='{10}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["typeid"].Trim(),
                    Request.Form["typename"].Trim(),
                    Request.Form["roomname"].Trim(),
                    Request.Form["roomcode"].Trim(),
                    Request.Form["configure"].Trim(),
                    Request.Form["deck"].Trim(),
                    Request.Form["area"].Trim(),
                    MyConvert.ConToInt(Request.Form["berth"].Trim()),
                    Request.Form["intro"].Trim(),
                    Request.Form["rooms"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void CruisesShip()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                Guid ucode = CombineKeys.NewComb();
                SqlQueryText = string.Format("insert into CR_Ship (uid,series,seriesname,cname,ename,tonnage,native,capacity,length,width,waterline,deck,speed,firstseaway,rooms,voltage,feature,restaurant,collection,meeting,bar,amusement,others,free,charge,userid,username,comid) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')",
                    ucode,
                    Request.Form["series"].Trim(),
                    Request.Form["seriesname"].Trim(),
                    Request.Form["cname"].Trim(),
                    Request.Form["ename"].Trim(),
                    Request.Form["tonnage"].Trim(),
                    Request.Form["native"].Trim(),
                    Request.Form["capacity"].Trim(),
                    Request.Form["length"].Trim(),
                    Request.Form["width"].Trim(),
                    Request.Form["waterline"].Trim(),
                    MyConvert.ConToInt(Request.Form["deck"].Trim()),
                    Request.Form["speed"].Trim(),
                    Request.Form["firstseaway"].Trim(),
                    MyConvert.ConToInt(Request.Form["rooms"].Trim()),
                    Request.Form["voltage"].Trim(),
                    Request.Form["feature"].Trim(),
                    Request.Form["restaurant"].Trim(),
                    Request.Form["collection"].Trim(),
                    Request.Form["meeting"].Trim(),
                    Request.Form["bar"].Trim(),
                    Request.Form["amusement"].Trim(),
                    Request.Form["others"].Trim(),
                    Request.Form["free"].Trim(),
                    Request.Form["charge"].Trim(),
                    Session["Manager_UserId"],
                    Session["Manager_UserName"],
                    Request.Form["comid"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Ship set series='{1}',seriesname='{2}',cname='{3}',ename='{4}',tonnage='{5}',native='{6}',capacity='{7}',length='{8}',width='{9}',waterline='{10}',deck='{11}',speed='{12}',firstseaway='{13}',rooms='{14}',voltage='{15}',feature='{16}',restaurant='{17}',collection='{18}',meeting='{19}',bar='{20}',amusement='{21}',others='{22}',free='{23}',charge='{24}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["series"].Trim(),
                    Request.Form["seriesname"].Trim(),
                    Request.Form["cname"].Trim(),
                    Request.Form["ename"].Trim(),
                    Request.Form["tonnage"].Trim(),
                    Request.Form["native"].Trim(),
                    Request.Form["capacity"].Trim(),
                    Request.Form["length"].Trim(),
                    Request.Form["width"].Trim(),
                    Request.Form["waterline"].Trim(),
                    MyConvert.ConToInt(Request.Form["deck"].Trim()),
                    Request.Form["speed"].Trim(),
                    Request.Form["firstseaway"].Trim(),
                    MyConvert.ConToInt(Request.Form["rooms"].Trim()),
                    Request.Form["voltage"].Trim(),
                    Request.Form["feature"].Trim(),
                    Request.Form["restaurant"].Trim(),
                    Request.Form["collection"].Trim(),
                    Request.Form["meeting"].Trim(),
                    Request.Form["bar"].Trim(),
                    Request.Form["amusement"].Trim(),
                    Request.Form["others"].Trim(),
                    Request.Form["free"].Trim(),
                    Request.Form["charge"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }

        protected void CruisesCompany()
        {
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                Guid ucode = CombineKeys.NewComb();
                SqlQueryText = string.Format("insert into CR_Company (uid,cname,ename,intro,picurl,userid,username) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                    ucode,
                    Request.Form["cname"].Trim(),
                    Request.Form["ename"].Trim(),
                    Request.Form["intro"].Trim(),
                    Request.Form["logourl"].Trim(),
                    Session["Manager_UserId"],
                    Session["Manager_UserName"]
                );
            }
            else
            {
                SqlQueryText = string.Format("update CR_Company set cname='{1}',ename='{2}',intro='{3}',picurl='{4}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["cname"].Trim(),
                    Request.Form["ename"].Trim(),
                    Request.Form["intro"].Trim(),
                    Request.Form["logourl"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"信息保存失败\"})");
            }
        }


    }
}