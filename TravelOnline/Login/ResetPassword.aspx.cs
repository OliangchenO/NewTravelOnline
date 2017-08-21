using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

namespace TravelOnline.FindPassword
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        public Guid ucode;
        public string uid, useremail;
        protected void Page_Load(object sender, EventArgs e)
        {
            ucode = System.Guid.NewGuid();
            uid = Request.QueryString["uid"];
            if (uid != null)
            {
                string SqlQueryText = string.Format("select * from OL_FindPWD where findid='{0}'", uid);

                DataSet DS = new DataSet();
                DS.Clear();
                DS = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    useremail = DS.Tables[0].Rows[0]["UserEmail"].ToString();
                    DateTime rp_times = DateTime.Now;
                    rp_times = rp_times.AddHours(-2);
                    if (Convert.ToDateTime(DS.Tables[0].Rows[0]["SaveTime"]) < rp_times)
                    {
                        Response.Write("密码重设链接已失效，请重新发送密码找回邮件");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("传递的参数错误，请重试");
                    Response.End();
                }
            }
            else
            {
                Response.Write("传递的参数错误，请重试");
                Response.End();
            }
        }
    }
}