using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class AfficheAjaxService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("{\"info\":\"尚未登录\"}");
                Response.End();
            }

            string SqlQueryText = "";
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into OL_Affiche (AfficheName,AfficheType,AfficheContent,EditUser,EditTime,AfficheFlag) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                    Request.Form["AfficheName"].Trim(),
                    Request.Form["DropDownList1"].Trim(),
                    Request.Form["content"].Trim(),
                    Convert.ToString(Session["Manager_UserId"]),
                    DateTime.Now.ToString(),
                    Request.Form["AfficheFlag"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update OL_Affiche set AfficheName='{1}',AfficheType='{2}',AfficheContent='{3}',EditUser='{4}',EditTime='{5}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["AfficheName"].Trim(),
                    Request.Form["DropDownList1"].Trim(),
                    Request.Form["content"].Trim(),
                    Convert.ToString(Session["Manager_UserId"]),
                    DateTime.Now.ToString()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                //HttpContext.Current.Cache.Insert(string.Format("AfficheHtml{0}", Request.Form["DropDownList1"].Trim()), "");
                HttpContext.Current.Cache.Insert("Announcement", "");
                Response.Write("({\"success\":0})");
            }
            else
            {
                Response.Write("({\"success\":1})");
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
    }
}