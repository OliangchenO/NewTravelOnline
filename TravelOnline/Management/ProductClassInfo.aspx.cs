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
    public partial class ProductClassInfo : System.Web.UI.Page
    {
        public string id, ProductName, ProductType, ProductUrl, MisClassId, ProductSort, ClassPath, setddl;
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

            id = Request.QueryString["Id"];
            if (!IsPostBack)
            {
                //Button1.Attributes.Add("onclick", "javascript:return check_null();");
                if (id != null)
                {
                    LoadInfo();
                }
            }

            //if (Request.QueryString["flag"] == "ok") ClientScript.RegisterStartupScript(this.GetType(), "", "<script>jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });</script>");
        }

        protected void LoadInfo()
        {
            string SqlQueryText = string.Format("select * from OL_ProductClass where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                ProductName = DS.Tables[0].Rows[0]["ProductName"].ToString();
                ProductUrl = DS.Tables[0].Rows[0]["ProductUrl"].ToString();
                ClassPath = DS.Tables[0].Rows[0]["ClassPath"].ToString().Replace("0,", "");;
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

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string SqlQueryText;
        //    if (id == null)
        //    {
        //        if (TB_SelectClass.Text == "")
        //        {
        //            SqlQueryText = string.Format("insert into OL_ProductClass (ProductName,ProductUrl,ProductSort,MisClassId) values ('{0}','{1}','{2}','{3}')",
        //                TB_ClassName.Text.Trim(),
        //                TB_Url.Text.Trim(),
        //                TB_ProductSort.Text,
        //                TB_MisClassId.Text
        //            );
        //        }
        //        else
        //        {
        //            SqlQueryText = string.Format("insert into OL_ProductClass (ParentId,ProductName,ProductUrl,ProductSort,MisClassId,ClassPath) values ('{0}','{1}','{2}','{3}','{4}','{5},{6}')",
        //                TB_SelectClass.Text,
        //                TB_ClassName.Text.Trim(),
        //                TB_Url.Text.Trim(),
        //                TB_ProductSort.Text,
        //                TB_MisClassId.Text,
        //                GetProductClass.getParentPathByClassID(TB_SelectClass.Text),
        //                TB_SelectClass.Text
        //            );
        //        }
        //    }
        //    else
        //    {
        //        SqlQueryText = string.Format("update OL_ProductClass set ProductName='{1}',ProductUrl='{2}',ProductSort='{3}',MisClassId='{4}' where id={0}",
        //            id,
        //            TB_Url.Text.Trim(),
        //            TB_Url.Text.Trim(),
        //            TB_ProductSort.Text,
        //            TB_MisClassId.Text
        //        );
        //    }

        //    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
        //    {
        //        GetProductClass.BindClassName();
        //        if (id == null)
        //        {
        //            //Response.Redirect("ProductClassInfo.aspx?flag=ok", true); //myrefresh()
        //            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>myrefresh();</script>");
                
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>jSuccess('<strong>信息保存成功!</strong>', { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });</script>");
        //        }
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>jError('<strong>信息保存失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });</script>");
        //    }
        //}
    }
}