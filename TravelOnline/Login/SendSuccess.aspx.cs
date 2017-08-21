using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.Login
{
    public partial class SendSuccess : System.Web.UI.Page
    {
        public string UserEmail;
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            if (uid != null)
            {
                string SqlQueryText = string.Format("select * from OL_FindPWD where findid='{0}'", uid);

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    UserEmail = DS.Tables[0].Rows[0]["UserEmail"].ToString();
                }
            }
            
        }
    }
}