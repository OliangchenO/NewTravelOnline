using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelOnline.GetCombineKeys;
using TravelOnline.LoginUsers;
using System.Text;
using TravelOnline.Class.Common;
using System.Data;
using System.Data.SqlClient;

namespace TravelOnline.Test
{


    public partial class WebForm1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                this.GridView1.Attributes.Add("SortExpression", "id");
                this.GridView1.Attributes.Add("SortDirection", "ASC");

                // 绑定数据源到GridView
                this.GridView_DataBind();
            }
        }

        protected override void GridView_DataBind()
        {
            //dpage = new BasePage();
            //获取数据，可以是获取全部，也可以是只获取当前页数据

            //...

            //如果你想自己设置总记录数，可以使用IsUseCustomRecordCount

            //这种情况适合分页获取少量数据的情况

            //IsUseCustomRecordCount = true;

            //GridView_RecordCount = 100;

            //绑定数据到GridView
            DataTable dtBind = this.getDB();
            //GridView_RecordCount = dtBind.Rows.Count;

            this.GridView1.DataSource = dtBind;
            GridView1.DataBind();

        }

        private DataTable getDB()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("age");

            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "caili", "27" });
            dt.Rows.Add(new object[] { "000003", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000006", "hekui", "26" });
            dt.Rows.Add(new object[] { "000007", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000008", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000009", "liyang", "25" });
            dt.Rows.Add(new object[] { "00000115", "caili", "27" });
            dt.Rows.Add(new object[] { "0000011", "hekui", "26" });
            dt.Rows.Add(new object[] { "0000022", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "0000033", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "00000444", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            dt.Rows.Add(new object[] { "000001", "hekui", "26" });
            dt.Rows.Add(new object[] { "000002", "zhangyu", "26" });
            dt.Rows.Add(new object[] { "000003", "zhukundian", "27" });
            dt.Rows.Add(new object[] { "000004", "liyang", "25" });
            dt.Rows.Add(new object[] { "000005", "caili", "27" });
            return dt;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //// Guid abc = CombineKeys.NewComb();
            //Guid ucode;
            //ucode = System.Guid.NewGuid();
            ////string aaass = ucode.ToString;
            //this.Label1.Text = System.IO.Directory.GetCurrentDirectory();//" " +  Convert.ToString(CombineKeys.NewComb()); //+ CombineKeys.GetDateFromComb(ucode);
            //TextBox1.Text = AppDomain.CurrentDomain.BaseDirectory + @"Errorlog.txt";  //Convert.ToString(System.Guid.NewGuid());
            ////string path = Application.StartupPath + @"\Errorlog.txt";
            
            //string SelectQueryText = string.Format("select top 1 id from OL_LoginUser where UserEmail='{0}'", TextBox1.Text);
            //if (MyDataBaseComm.getScalar(SelectQueryText) != null)
            //{
            //    Response.Write("{\"success\":1}");
            //    //Response.End();
            //}
            //else
            //{
            //    Response.Write("{\"success\":0}");
            //    //Response.End();
            //}
            GetUserInfo();
        }

        protected void GetUserInfo()
        {
            LoginUser.RegistUser RUser = new LoginUser.RegistUser();
            StringBuilder QueryString = new StringBuilder();
            QueryString.Append("id='");
            QueryString.Append(this.TextBox1.Text.Trim());
            QueryString.Append("'");
            RUser = LoginUser.LoginUseDetail(QueryString.ToString());
            if (RUser != null)
            {
                this.Label1.Text = RUser.Id + " / ";
                this.Label1.Text += RUser.UserEmail + " / ";
                this.Label1.Text += RUser.LoginPassWord + " / ";
                this.Label1.Text += RUser.UserName + " / ";
                this.Label1.Text += RUser.Tel + " / ";
                this.Label1.Text += RUser.Mobile + " / ";
                this.Label1.Text += RUser.Sex + " / ";
                this.Label1.Text += RUser.BirtyDay + " / ";
                this.Label1.Text += RUser.Address + " / ";
                this.Label1.Text += RUser.ZipCode + " / ";
                this.Label1.Text += RUser.Income + " / ";
            }
            else
            {
                this.Label1.Text = "没有数据";
            }


        }

    }
}