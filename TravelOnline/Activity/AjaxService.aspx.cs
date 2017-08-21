using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using System.Data;
using TravelOnline.Class.Common;
using System.Text;
using Sunrise.Spell;
using System.IO;

namespace TravelOnline.Activity
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

            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("{\"info\":\"尚未登录\"}");
                Response.End();
            }
            switch (Request.QueryString["action"])
            {
                case "SaveAct_Add":
                    SaveAct();
                    break;
                case "DisableActInfoMain":
                    DisableActInfoMain("Act_ActInfoMain");
                    break;
                case "DisableActOrder":
                    DisableActOrder("Act_Order");
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (HttpContext.Current.Server.GetLastError() is HttpRequestValidationException)
            {
                HttpContext.Current.Response.Write("请输入合法的字符串返回");
                HttpContext.Current.Server.ClearError();
            }
        }

        protected void SaveAct()
        {
            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.QueryString["ActInfoMain_ID"]) == 0)
            {
                SqlQueryText = "INSERT INTO [dbo].[Act_ActInfoMain]([ActivityName],[ActivityStartTime],[ActivityEndTime],[Start],[MaxNum],[MinNum],[JoinNum],[Numbers],[MaxAge],[MinAge],[Remark],[Place],[ActivityRunSTime],[ActivityRunETime]) VALUES (";
                SqlQueryText += string.Format("'{0}',", Request.QueryString["ActivityName"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["ActivityStartTime"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["ActivityEndTime"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["Start"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["MaxNum"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["MinNum"].Trim());
                SqlQueryText += string.Format("'{0}',", "0");//joinNum
                SqlQueryText += string.Format("'{0}',", Request.QueryString["MaxNum"].Trim());//Numbers=MaxNum
                SqlQueryText += string.Format("'{0}',", Request.QueryString["MaxAge"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["MinAge"].Trim());
                SqlQueryText += string.Format("'{0}',", "");//空备注
                SqlQueryText += string.Format("'{0}',", Request.QueryString["Place"].Trim());
                SqlQueryText += string.Format("'{0}',", Request.QueryString["ActivityRunSTime"].Trim());
                SqlQueryText += string.Format("'{0}')", Request.QueryString["ActivityRunETime"].Trim());
            }
            else
            {
                SqlQueryText = "update dbo.Act_ActInfoMain";
                SqlQueryText += string.Format(" set ActivityName='{0}'", Request.QueryString["ActivityName"].Trim());
                SqlQueryText += string.Format(",ActivityStartTime='{0}'", Request.QueryString["ActivityStartTime"].Trim());
                SqlQueryText += string.Format(",ActivityEndTime='{0}'", Request.QueryString["ActivityEndTime"].Trim());
                SqlQueryText += string.Format(",Start='{0}'", Request.QueryString["Start"].Trim());
                SqlQueryText += string.Format(",MaxNum='{0}'", Request.QueryString["MaxNum"].Trim());
                SqlQueryText += string.Format(",MinNum='{0}'", Request.QueryString["MinNum"].Trim());
                SqlQueryText += string.Format(",MaxAge='{0}'", Request.QueryString["MaxAge"].Trim());
                SqlQueryText += string.Format(",MinAge='{0}'", Request.QueryString["MinAge"].Trim());
                SqlQueryText += string.Format(",Place='{0}'", Request.QueryString["Place"].Trim());
                SqlQueryText += string.Format(",ActivityRunSTime='{0}'", Request.QueryString["ActivityRunSTime"].Trim());
                SqlQueryText += string.Format(",ActivityRunETime='{0}'", Request.QueryString["ActivityRunETime"].Trim());
                SqlQueryText += string.Format(",Numbers={0}-Numbers", Request.QueryString["MaxNum"].Trim());
                SqlQueryText += string.Format(" where ActInfoMain_ID='{0}'",Request.QueryString["ActInfoMain_ID"]);
            }
            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                HttpContext.Current.Cache.Insert("SlidePic_" + Request.QueryString["AdFlag"], "");
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void DisableActInfoMain(string DbTableName)
        {
            string strSqlCommand = string.Format("Update Act_ActInfoMain Set Start ='禁用' where ActInfoMain_ID in({0})", Request.QueryString["ActInfoMain_ID"]);
            List<string> Sql = new List<string>();
            Sql.Add(strSqlCommand);
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        protected void DisableActOrder(string DbTableName)
        {
            string strSqlCommand = string.Format("Update Act_Order Set Status ='0' where ActOrderID in({0})", Request.QueryString["ActOrderID"]);
            string strSqlCommand2 = string.Format("update dbo.Act_ActInfoMain set JoinNum=JoinNum-1,Numbers=Numbers+1 where ActInfoMain_ID ='{0}'", Request.QueryString["ActInfoMain_ID"]);
            List<string> Sql = new List<string>();
            Sql.Add(strSqlCommand);
            Sql.Add(strSqlCommand2);
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
               
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }
    }
}