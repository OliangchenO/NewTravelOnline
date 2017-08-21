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

namespace TravelOnline.Travel
{
    public partial class ShowDeck : System.Web.UI.Page
    {
        public string DeckString, ship, deck;
        protected void Page_Load(object sender, EventArgs e)
        {
            ship = Request.QueryString["ship"];
            deck = Request.QueryString["deck"];

            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "SELECT * from CR_Pic where shipid='" + ship + "' and deck='" + deck + "' and pictype='deck'";
            DataSet DS1 = new DataSet();
            DS1.Clear();
            DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS1.Tables[0].Rows.Count; i++)
                {
                    DeckString += string.Format("<IMG onerror=\"this.src='/Images/none.gif'\" src=\"{0}\" title=\"{1}\"><br><br>", DS1.Tables[0].Rows[i]["picurl"].ToString(), DS1.Tables[0].Rows[i]["roomtype"].ToString());
                }
            }
            else
            {
                DeckString = "暂无甲板示意图";
            }
        }
    }
}