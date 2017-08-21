using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.Data;
using TravelOnline.Class.Purchase;
using TravelOnline.Class.Travel;

namespace TravelOnline.CruisesOrder
{
    public partial class SelectCar : System.Web.UI.Page
    {
        public string Cid, LineId, Visitid, VisitListId, CarListInfo, SelectNums;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserRight"]).IndexOf("@7@2") == -1)
            {
                Response.Write("没有操作权限！");
                Response.End();
            }
            LineId = Request.QueryString["LineId"];
            Visitid = Request.QueryString["Visitid"];
            VisitListId = Request.QueryString["Id"];
            SelectNums = VisitListId.Split(',').Length.ToString();
            CarListInfo = GetCarList();
        }

        protected string GetCarList()
        {
            int SelectNums = VisitListId.Split(',').Length;//VisitListId.Split(",".ToCharArray()).Length;
            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "SELECT * FROM View_CR_BusNo where Lineid='" + LineId + "' and Visitid='" + Visitid + "'";
            
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                Strings.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%;\">");
                Strings.Append("<tr class=tit1>");
                Strings.Append("<td width=\"15%\">车号</td>");
                Strings.Append("<td width=\"40%\">观光线路</td>");
                Strings.Append("<td width=\"15%\">座位数</td>");
                Strings.Append("<td width=\"10%\">已分配</td>");
                Strings.Append("<td width=\"10%\">剩余</td>");
                Strings.Append("<td width=\"10%\">选择</td>");
                Strings.Append("</tr>");
                int Nums = 0;
                string RadioBox = "";
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Nums = MyConvert.ConToInt(DS.Tables[0].Rows[i]["Berth"].ToString()) - MyConvert.ConToInt(DS.Tables[0].Rows[i]["nums"].ToString());
                    RadioBox = "&nbsp;";
                    if (Nums >= SelectNums)
                    {
                        RadioBox = string.Format("<input id=\"Radio{0}\" type=\"radio\" name=\"CarNos\" value=\"{0}\" />", DS.Tables[0].Rows[i]["id"].ToString());
                    }
                    Strings.Append(string.Format("<tr><td><b>{0}</b></td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>",
                        DS.Tables[0].Rows[i]["BusNo"].ToString(),
                        DS.Tables[0].Rows[i]["visitname"].ToString(),
                        DS.Tables[0].Rows[i]["Berth"].ToString(),
                        DS.Tables[0].Rows[i]["nums"].ToString(),
                        Nums,
                        RadioBox
                    ));
                }

                Strings.Append("</table>");
            }
            return Strings.ToString();
        }
    }
}