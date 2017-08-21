using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

namespace TravelOnline.Management
{
    public partial class LineDestination : System.Web.UI.Page
    {
        public string Cid, flag, DestinationInfos, Line_Desid, viewdays, Integral;
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

            Cid = Request.QueryString["Cid"];
            flag = Request.QueryString["flag"];//Destination

            if (!IsPostBack)
            {
                switch (flag)
                {
                    case "Destination":
                        DestinationInfos = MyDataBaseComm.getScalar("select DestinationList from OL_ProductType where Id='" + Cid + "'");
                        break;
                    case "view":
                        //DestinationInfos = MyDataBaseComm.getScalar("select viewlist from OL_Line where MisLineId='" + Cid + "'");
                        //Line_Desid = MyDataBaseComm.getScalar("select Destinationid from OL_Line where MisLineId='" + Cid + "'");
                        //Integral = MyDataBaseComm.getScalar("select Integral from OL_Line where MisLineId='" + Cid + "'");
                        LoadInfo();
                        break;
                    default:
                        //DestinationInfos = MyDataBaseComm.getScalar("select DestinationList from OL_Line where MisLineId='" + Cid + "'");
                        //Integral = MyDataBaseComm.getScalar("select Integral from OL_Line where MisLineId='" + Cid + "'");
                        LoadInfo();
                        break;
                }
            }

        }

        protected void LoadInfo()
        {
            string SqlQueryText = "select viewlist,Destinationid,DestinationList,Integral from OL_Line where MisLineId='" + Cid + "'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                
                if (flag == "view")
                {
                    DestinationInfos = DS.Tables[0].Rows[0]["viewlist"].ToString();
                    Line_Desid = DS.Tables[0].Rows[0]["Destinationid"].ToString();
                    //viewdays = DS.Tables[0].Rows[0]["viewdays"].ToString();
                }
                else
                {
                    DestinationInfos = DS.Tables[0].Rows[0]["DestinationList"].ToString();
                } 
                Integral = DS.Tables[0].Rows[0]["Integral"].ToString();
            }
        }


    }
}