using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace TravelOnline.AD._2017Young
{
    public partial class index : System.Web.UI.Page
    {
        public string userId, userType, uploadPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                userId = Convert.ToString(Session["Online_UserId"]);
                if (Convert.ToString(Session["Online_UserType"]).Length > 0)
                {
                    userType = Convert.ToString(Session["Online_UserType"]);
                    uploadPath = "File/" + Convert.ToString(Session["Online_UserId"]) + ".xlsx";
                }
            }
            
        }

        /// <summary>
        /// 文件上传按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["Online_UserId"]).Length > 0)
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = FileUpload1.FileName;
                    string FileType = FileName.Substring(FileName.LastIndexOf(".") + 1);
                    if (FileType != "xlsx")
                    {
                        Label1.Text = "文件类型不正确！请上传xlsx文件";
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("File/") + Convert.ToString(Session["Online_UserId"]) + "." + FileType);
                        string SqlQueryText = string.Format("UPDATE OL_LoginUser set UserType='upload' where id='{0}'", Convert.ToString(Session["Online_UserId"]));
                        MyDataBaseComm.ExcuteSql(SqlQueryText);
                        Session["Online_UserType"] = "upload";
                        Session["UploadPath"] = "File/" + Convert.ToString(Session["Online_UserId"]) + "." + FileType;
                        Label1.Text = "上传成功！";
                    }
                }
            } else
            {
                Response.Redirect("/Member/loginYoung.aspx", true);
            }
        }
    }
}