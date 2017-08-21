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
    public partial class AfficheInfo : System.Web.UI.Page
    {
        public string id, AfficheName, AfficheType, AfficheContent, AfficheFlag, InfoTitle, options;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Convert.ToString(Session["Manager_UserId"]).Length == 0) Response.Redirect("/manage/Login.aspx", true);
            if (Convert.ToString(Session["Manager_UserId"]).Length == 0)
            {
                Response.Write("尚未登录");
                Response.End();
            }

            id = Request.QueryString["Id"];
            AfficheFlag = Request.QueryString["Flag"];
            if (id != null)
            {
                LoadAfficheInfo();
            }
            CreatInfos(AfficheFlag);
        }

        protected void LoadAfficheInfo()
        {
            string SqlQueryText = string.Format("select * from OL_Affiche where id='{0}'", id);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                id = DS.Tables[0].Rows[0]["Id"].ToString();
                AfficheName = DS.Tables[0].Rows[0]["AfficheName"].ToString();
                AfficheType = DS.Tables[0].Rows[0]["AfficheType"].ToString();
                AfficheContent = DS.Tables[0].Rows[0]["AfficheContent"].ToString();
                AfficheFlag = DS.Tables[0].Rows[0]["AfficheFlag"].ToString();
                AfficheType = DS.Tables[0].Rows[0]["AfficheType"].ToString();
            }
        }

        protected void CreatInfos(string flags)
        {
            StringBuilder CString = new StringBuilder();
            switch (flags)
            {
                case "1":
                    InfoTitle = "公告信息";
                    CString.Append("<option value=\"Index\">首页</option>");
                    CString.Append("<option value=\"OutBound\">出境旅游</option>");
                    CString.Append("<option value=\"InLand\">国内旅游</option>");
                    CString.Append("<option value=\"FreeTour\">自由行</option>");
                    CString.Append("<option value=\"Cruises\">邮轮</option>");
                    CString.Append("<option value=\"Visa\">签证</option>");
                    CString.Append("<option value=\"N_News\">最新资讯</option>");
                    options = CString.ToString();
                    break;
                case "2":
                    InfoTitle = "服务信息";
                    CString.Append("<option value=\"Service7\">关于我们</option>");
                    CString.Append("<option value=\"Service1\">联系我们</option>");
                    CString.Append("<option value=\"Service2\">人才招聘</option>");
                    CString.Append("<option value=\"Service3\">同行分销</option>");
                    CString.Append("<option value=\"Service4\">广告服务</option>");
                    CString.Append("<option value=\"Service5\">服务终端</option>");
                    CString.Append("<option value=\"Service6\">销售联盟</option>");
                    options = CString.ToString();
                    break;
                case "3":
                    InfoTitle = "帮助信息";
                    CString.Append("<option value=\"Help1\">帮助分类1</option>");
                    CString.Append("<option value=\"Help2\">帮助分类2</option>");
                    CString.Append("<option value=\"Help3\">帮助分类3</option>");
                    CString.Append("<option value=\"Help4\">帮助分类4</option>");
                    CString.Append("<option value=\"Help5\">帮助分类5</option>");
                    CString.Append("<option value=\"Help6\">帮助分类6</option>");
                    options = CString.ToString();
                    break;
                case "4":
                    InfoTitle = "关于我们";
                    break;
                case "5":
                    InfoTitle = "出境须知";
                    CString.Append("<option value=\"OutBound1\">出境须知1</option>");
                    CString.Append("<option value=\"OutBound2\">出境须知2</option>");
                    CString.Append("<option value=\"OutBound3\">出境须知3</option>");
                    CString.Append("<option value=\"OutBound4\">出境须知4</option>");
                    CString.Append("<option value=\"OutBound5\">出境须知5</option>");
                    CString.Append("<option value=\"OutBound6\">出境须知6</option>");
                    options = CString.ToString();
                    break;
                default:
                    break;
            }
        }
    }
}