using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using TravelOnline.Class.Manage;

using Sunrise.Spell;
using TravelOnline.Class.Common;

namespace TravelOnline.Management
{
    public partial class DestinationInfo : System.Web.UI.Page
    {
        public string id, DestinationName, Dtype, Ename, MisClassId, SortNum, ClassPath, setddl, OldName, PinYin, SortPinYin, map_x, map_y, map_size;
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
            //DestinationName = AllPinYin.IndexCode("西藏");
            //Ename=Spell.MakeSpellCode("西藏", SpellOptions.EnableUnicodeLetter);

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
            string SqlQueryText = string.Format("select * from OL_Destination where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                DestinationName = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                OldName = DS.Tables[0].Rows[0]["DestinationName"].ToString();
                Ename = DS.Tables[0].Rows[0]["Ename"].ToString();
                ClassPath = DS.Tables[0].Rows[0]["ClassPath"].ToString().Replace("0,", "");
                MisClassId = DS.Tables[0].Rows[0]["MisClassId"].ToString();
                SortNum = DS.Tables[0].Rows[0]["SortNum"].ToString();
                PinYin = DS.Tables[0].Rows[0]["PinYin"].ToString();
                SortPinYin = DS.Tables[0].Rows[0]["SortPinYin"].ToString();
                Dtype = DS.Tables[0].Rows[0]["Dtype"].ToString();
                map_x = DS.Tables[0].Rows[0]["map_x"].ToString();
                map_y = DS.Tables[0].Rows[0]["map_y"].ToString();
                map_size = DS.Tables[0].Rows[0]["map_size"].ToString();
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