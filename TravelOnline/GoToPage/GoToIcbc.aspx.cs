using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace TravelOnline.GoToPage
{
    public partial class GoToIcbc : System.Web.UI.Page
    {
        string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLineInfo();
        }

        protected void LoadLineInfo()
        {
            string SqlQueryText = string.Format("select top 1 * from OL_Line where MisLineId='{0}'", Request.QueryString["lineid"]);

            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (MyConvert.ConToInt(DS.Tables[0].Rows[0]["Shipid"].ToString()) > 0)
                {
                    Url = string.Format("~/Ship/{0}/{1}.html", DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["MisLineId"].ToString());
                }
                else
                {
                    switch (DS.Tables[0].Rows[0]["LineType"].ToString())
                    {
                        case "OutBound":
                            Url = string.Format("~/OutBound/{0}/{1}.html", DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["MisLineId"].ToString());
                            break;
                        case "InLand":
                            Url = string.Format("~/InLand/{0}/{1}.html", DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["MisLineId"].ToString());
                            break;
                        case "Cruises":
                            Url = string.Format("~/Cruises/{0}/{1}.html", DS.Tables[0].Rows[0]["LineClass"].ToString(), DS.Tables[0].Rows[0]["MisLineId"].ToString());
                            break;
                        default:
                            Url = "";
                            break;
                    }
                }

                if (Url == "")
                {
                    Response.Write("参数不正确，不能跳转页面！");
                }
                else
                {
                    HttpCookie cookie = default(HttpCookie);
                    cookie = new HttpCookie("OnlyIcbcPay", "YES");
                    cookie.Expires = DateTime.Now.AddDays(90);
                    Response.Cookies.Add(cookie);
                    Response.Redirect(Url, true);
                }
            }
        }
    }
}