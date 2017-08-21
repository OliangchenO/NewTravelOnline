using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Cruises
{
    public partial class CruisesRoute : System.Web.UI.Page
    {
        public string lineid, shipid, Routes, dinner, Dtime1, Dtime2, Dnum1, Dnum2;
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
                Response.Write("尚未登录");
                Response.End();
            }
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }

            lineid = Request.QueryString["lineid"];

            if (lineid != null)
            {
                LoadInfo();
            }
            else
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
        }

        protected void LoadInfo()
        {
            dinner = "";// MyDataBaseComm.getScalar("select dinner from OL_Line where MisLineId='" + lineid + "'");

            string SqlQueryText = string.Format("select dinner,shipid from OL_Line where MisLineId='{0}'", lineid);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                dinner = DS.Tables[0].Rows[0]["dinner"].ToString();
                shipid = DS.Tables[0].Rows[0]["shipid"].ToString();
            }
            Dtime1 = "";
            Dtime2 = "";
            Dnum1 = "";
            Dnum2 = "";
            string[] Dtime = dinner.Split('|');
            if (Dtime.Length > 1)
            {
                
                Dtime1 = Dtime[0].Split("@".ToCharArray())[0];
                Dnum1 = Dtime[0].Split("@".ToCharArray())[1];
                Dtime2 = Dtime[1].Split("@".ToCharArray())[0];
                Dnum2 = Dtime[1].Split("@".ToCharArray())[1];
                //.Split("/".ToCharArray())[0]
            }
            StringBuilder RouteString = new StringBuilder();

            SqlQueryText = string.Format("select * from CR_Route where lineid='{0}'", lineid);
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //Cid = DS.Tables[0].Rows[0]["id"].ToString();
                for (int i = 0; i <= DS.Tables[0].Rows.Count-1; i++)
                {
                    RouteString.Append("<tr>");
                    RouteString.Append(string.Format("<td class=fi>第{0}天</td>", i+1));
                    RouteString.Append(string.Format("<td><input class=ipt id=\"harbour\" name=\"harbour\" type=\"text\" style=\"width: 180px;\" maxlength=\"100\" value=\"{0}\" /></td>", DS.Tables[0].Rows[i]["harbour"].ToString()));
                    RouteString.Append(string.Format("<td><input class=ipt id=\"arrive\" name=\"arrive\" type=\"text\" style=\"width: 80px;\" maxlength=\"50\" value=\"{0}\" /></td>", DS.Tables[0].Rows[i]["arrive"].ToString()));
                    RouteString.Append(string.Format("<td><input class=ipt id=\"sail\" name=\"sail\" type=\"text\" style=\"width: 80px;\" maxlength=\"50\" value=\"{0}\" /></td>", DS.Tables[0].Rows[i]["sail"].ToString()));
                    RouteString.Append(string.Format("<td><input class=ipt id=\"visit\" name=\"visit\" type=\"text\" style=\"width: 180px;\" maxlength=\"100\" value=\"{0}\" /></td>", DS.Tables[0].Rows[i]["visit"].ToString()));
                    RouteString.Append("</tr>");
                }
                
            }
            else
            {
                int days = MyConvert.ConToInt(MyDataBaseComm.getScalar("select LineDays from OL_Line where MisLineId='" + lineid + "'"));
                
                for (int i = 1; i <= days; i++)
                {
                    RouteString.Append("<tr>");
                    RouteString.Append(string.Format("<td class=fi>第{0}天</td>", i));
                    RouteString.Append("<td><input class=ipt id=\"harbour\" name=\"harbour\" type=\"text\" style=\"width: 180px;\" maxlength=\"100\" /></td>");
                    RouteString.Append("<td><input class=ipt id=\"arrive\" name=\"arrive\" type=\"text\" style=\"width: 80px;\" maxlength=\"50\" /></td>");
                    RouteString.Append("<td><input class=ipt id=\"sail\" name=\"sail\" type=\"text\" style=\"width: 80px;\" maxlength=\"50\" /></td>");
                    RouteString.Append("<td><input class=ipt id=\"visit\" name=\"visit\" type=\"text\" style=\"width: 180px;\" maxlength=\"100\" /></td>");
                    RouteString.Append("</tr>");
                }
                
            }

            Routes = RouteString.ToString();
        }
    }
}