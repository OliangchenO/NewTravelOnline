using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace TravelOnline.Management
{
    public partial class ProductTypeInfo : System.Web.UI.Page
    {
        public string id, ProductName, MisClassId, ProductSort, ProductType, setddl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }

            id = Request.QueryString["Id"];
            if (!IsPostBack)
            {
                if (id != null)
                {
                    LoadInfo();
                }
            }

        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_ProductType where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                ProductName = DS.Tables[0].Rows[0]["ProductName"].ToString();
                MisClassId = DS.Tables[0].Rows[0]["MisClassId"].ToString();
                ProductSort = DS.Tables[0].Rows[0]["ProductSort"].ToString();
                ProductType = DS.Tables[0].Rows[0]["ProductType"].ToString();
                setddl = "disabled=\"disabled\"";
            }
            else
            {
                Response.Write("查询错误，没找到任何数据！");
                Response.End();
            }
        }
    }
}