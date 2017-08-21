using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace TravelOnline.Activity
{
    public partial class Print : System.Web.UI.Page
    {
        public string ActOrderID;
        public string GuestID;
        public string GuestName;
        public string ActInfoMain_ID;
        public string ActName;
        public string Place;
        public string ActivityRunSTime;
        public string ActivityRunETime;
        public string AutoID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActOrderID = Request.QueryString["ActOrderID"];
                if (ActOrderID == null)
                {

                }
                else
                {
                    GetPrintInfo(ActOrderID);
                }
            }
        }

        private void GetPrintInfo(string ActOrderID)
        {
            string strSqlCommand = string.Format("select * from dbo.Act_Order where ActOrderID='{0}'", ActOrderID);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(strSqlCommand);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GuestID = DS.Tables[0].Rows[0]["GuestID"].ToString();
                this.lblGuestName.Text = DS.Tables[0].Rows[0]["GuestName"].ToString();
                //ActInfoMain_ID = DS.Tables[0].Rows[0]["ActInfoMain_ID"].ToString();
                this.lblActName.Text = DS.Tables[0].Rows[0]["ActName"].ToString();
                ActInfoMain_ID = DS.Tables[0].Rows[0]["ActInfoMain_ID"].ToString();
                AutoID = DS.Tables[0].Rows[0]["OL_OrderID"].ToString();
            }

            string strSqlCommand2 = string.Format("select * from dbo.Act_ActInfoMain where ActInfoMain_ID='{0}'", ActInfoMain_ID);
            DataSet DS2 = new DataSet();
            DS2.Clear();
            DS2 = MyDataBaseComm.getDataSet(strSqlCommand2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                this.lblActivityRunSTime.Text = Convert.ToDateTime(DS2.Tables[0].Rows[0]["ActivityRunSTime"]).ToString("MM月dd日HH:mm");
                this.lblActivityRunETime.Text = Convert.ToDateTime(DS2.Tables[0].Rows[0]["ActivityRunETime"]).ToString("MM月dd日HH:mm");
                this.lblPlace.Text = DS2.Tables[0].Rows[0]["Place"].ToString();

            }
            string strSqlCommand3 = string.Format("select * from dbo.View_Act_GuestInfo where AutoID='{0}' and GuestID='{1}'", AutoID, GuestID);
            DataSet DS3 = new DataSet();
            DS3.Clear();
            DS3 = MyDataBaseComm.getDataSet(strSqlCommand3);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                this.lblRoomNoID.Text = DS3.Tables[0].Rows[0]["roomnoid"].ToString();
                

            }
        }
    }
}