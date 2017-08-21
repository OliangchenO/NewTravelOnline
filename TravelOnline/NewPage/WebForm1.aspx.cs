using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelOnline.NewPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> Sql = new List<string>();
            string SqlQueryText = string.Format("select MisLineId,Destinationid from OL_Line", "");
            DataSet DS = new DataSet();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    if (DS.Tables[0].Rows[i]["Destinationid"].ToString().Length > 3)
                    {
                        string[] DestId = DS.Tables[0].Rows[i]["Destinationid"].ToString().Split(',');
                        for (int ii = 1; ii < DestId.Length - 1; ii++)
                        {
                            Sql.Add(SqlQueryText = string.Format("insert into LineDest (lineid,destid) values ('{0}','{1}')",
                                DS.Tables[0].Rows[i]["MisLineId"].ToString(),
                                DestId[ii]
                            ));
                        }
                    }
                }
                string[] SqlQueryList = Sql.ToArray();
                if (MyDataBaseComm.Transaction(SqlQueryList) == true)
                {
                    Response.Write("调整成功");
                }
                else
                {
                    Response.Write("调整错误");
                }
            }
        }
    }
}