using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using System.Text.RegularExpressions;

namespace TravelOnline.Users
{
    public partial class JournalsService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0 || Convert.ToString(Session["Manager_UserId"]).Length > 0)
            {

            }
            else
            {
                Response.Write("({\"error\":\"尚未登录\"})");
                Response.End();
            }

            switch (Request.QueryString["action"])
            {
                case "Summary":
                    SaveSummary();
                    break;
                case "Save":
                    SaveInfo();
                    break;
                case "Audit":
                    AuditInfo();
                    break;
                case "Recom":
                    RecomInfo();
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }

        }

        public void SaveSummary()
        {
            string ucode = "";
            string SqlQueryText = "";
            string title = StripHT(Request.Form["editorValue"].Trim());
            if (title.Length > 50) title = title.Substring(0, 48);

            if (Request.Form["uid"] == "")
            {
                Guid ucodes = CombineKeys.NewComb();
                ucode = ucodes.ToString();
                string parentid = "";
                SqlQueryText = string.Format("insert into OL_Summary (uid,typeid,typename,desid,parentid,title,contents,userid,inputdate,flag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                    ucode,
                    Request.Form["typeid"].Trim(),
                    Request.Form["typename"].Trim(),
                    Request.Form["desid"].Trim(),
                    parentid,
                    title,
                    Request.Form["editorValue"].Trim().Replace("'", "&#39;"),
                    Convert.ToString(Session["Manager_UserId"]),
                    DateTime.Now.ToString(),
                    Request.Form["flag"].Trim()
                );
            }
            else
            {
                ucode = Request.Form["uid"];
                SqlQueryText = string.Format("update OL_Summary set typeid='{1}',typename='{2}',contents='{3}',inputdate='{4}',title='{5}' where uid='{0}'",
                    ucode,
                    Request.Form["typeid"].Trim(),
                    Request.Form["typename"].Trim(),
                    Request.Form["editorValue"].Trim().Replace("'", "&#39;"),
                    DateTime.Now.ToString(),
                    title
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"" + ucode + "\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
        }

        public void AuditInfo()
        {
            string SqlQueryText = "";
            //SqlQueryText = string.Format("update OL_Journal set flag='1',seo='{1}',Destinationid='{2}',Destination='{3}',DestinationList='{4}',FirstDestination='{5}'  where uid='{0}'",
            //        Request.Form["uid"],
            //        Request.Form["seo"].Trim(),
            //        Request.Form["Destinationid"].Trim(),
            //        Request.Form["DestinationName"].Trim(),
            //        Request.Form["Des_List"].Trim(),
            //        Request.Form["FirstDestination"].Trim()
            //    );

            SqlQueryText = string.Format("update OL_Journal set flag='1',title='{1}',contents='{2}',tags='{3}',coverpic='{4}',coverid='{5}',albumid='{6}',seo='{7}',Destinationid='{8}',Destination='{9}',DestinationList='{10}',FirstDestination='{11}',parentid='{12}',seocontent='{13}',coverpicurl='{14}',EditDate='{15}' where uid='{0}'",
                Request.Form["uid"],
                Request.Form["title"].Trim(),
                Request.Form["editorValue"].Trim().Replace("'", "&#39;"),
                Request.Form["tag"].Trim(),
                Request.Form["coverimgurl"].Trim(),
                Request.Form["coverimgid"].Trim(),
                Request.Form["albumid"].Trim(),
                Request.Form["seo"].Trim(),
                Request.Form["Destinationid"].Trim(),
                Request.Form["DestinationName"].Trim(),
                Request.Form["Des_List"].Trim(),
                Request.Form["FirstDestination"].Trim(),
                TravelOnline.Destination.Class.PlaceClass.GetJournalDesParentId(Request.Form["Destinationid"].Trim()),
                TravelOnline.Destination.Class.PlaceClass.GetSeoLinkKeyWord(Request.Form["editorValue"].Trim().Replace("'", "&#39;"), "8"),
                Request.Form["coverpicurl"].Trim(),
                DateTime.Now.ToString()
            );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"ok\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();

        }

        public void SaveInfo()
        {
            //Guid ucode = CombineKeys.NewComb();
            string ucode = "";
            string SqlQueryText = "";
            if (Request.Form["uid"] == "")
            {
                ucode = Request.Form["Journalid"];
                SqlQueryText = string.Format("insert into OL_Journal (uid,title,contents,userid,inputdate,tags,coverpic,coverid,albumid,seo,Destinationid,Destination,DestinationList,FirstDestination,parentid,EditDate) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')",
                    ucode,
                    Request.Form["title"].Trim(),
                    Request.Form["editorValue"].Trim(),
                    Convert.ToString(Session["Online_UserId"]),
                    DateTime.Now.ToString(),
                    Request.Form["tag"].Trim(),
                    Request.Form["coverimgurl"].Trim(),
                    Request.Form["coverimgid"].Trim(),
                    Request.Form["albumid"].Trim(),
                    Request.Form["seo"].Trim(),
                    Request.Form["Destinationid"].Trim(),
                    Request.Form["DestinationName"].Trim(),
                    Request.Form["Des_List"].Trim(),
                    Request.Form["FirstDestination"].Trim(),
                    TravelOnline.Destination.Class.PlaceClass.GetJournalDesParentId(Request.Form["Destinationid"].Trim()),
                    DateTime.Now.ToString()
                );
            }
            else
            {
                ucode = Request.Form["uid"];
                SqlQueryText = string.Format("update OL_Journal set flag='0',title='{1}',contents='{2}',tags='{3}',coverpic='{4}',coverid='{5}',albumid='{6}',seo='{7}',Destinationid='{8}',Destination='{9}',DestinationList='{10}',FirstDestination='{11}',parentid='{12}',EditDate='{13}', where uid='{0}'",
                    ucode,
                    Request.Form["title"].Trim(),
                    Request.Form["editorValue"].Trim(),
                    Request.Form["tag"].Trim(),
                    Request.Form["coverimgurl"].Trim(),
                    Request.Form["coverimgid"].Trim(),
                    Request.Form["albumid"].Trim(),
                    Request.Form["seo"].Trim(),
                    Request.Form["Destinationid"].Trim(),
                    Request.Form["DestinationName"].Trim(),
                    Request.Form["Des_List"].Trim(),
                    Request.Form["FirstDestination"].Trim(),
                    TravelOnline.Destination.Class.PlaceClass.GetJournalDesParentId(Request.Form["Destinationid"].Trim()),
                    DateTime.Now.ToString()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"" + ucode + "\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
        }

        public void RecomInfo()
        {
            //Guid ucode = CombineKeys.NewComb();
            string ucode = "";
            string SqlQueryText = "";
            ucode = Request.Form["Id"];
            SqlQueryText = string.Format("update OL_Journal set Recom='{1}',EditDate='{2}' where uid='{0}'",
                    ucode,
                    Request.Form["DoFlag"].Trim(),
                    DateTime.Now.ToString()
                );

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"" + ucode + "\"})");
            }
            else
            {
                Response.Write("({\"error\":\"error\"})");
            }
            Response.End();
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

        private string StripHT(string strHtml)  //从html中提取纯文本
        {
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string strOutput = regex.Replace(strHtml, "");//替换掉"<"和">"之间的内容
            strOutput = strOutput.Replace("<", "");
            strOutput = strOutput.Replace(">", "");
            strOutput = strOutput.Replace(" ", "");
            strOutput = strOutput.Replace("&nbsp;", "");
            strOutput = strOutput.Replace("&lt;", "");
            strOutput = strOutput.Replace("&gt;", "");
            strOutput = strOutput.Replace("&#39;", "");
            strOutput = strOutput.Replace("&quot;", "");
            return strOutput;
        }
    }
}