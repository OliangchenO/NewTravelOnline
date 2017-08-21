using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using TravelOnline.GetCombineKeys;
using System.Data;
using System.Text;
using TravelOnline.EncryptCode;

namespace TravelOnline.Management
{
    public partial class Member : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text != Convert.ToString(ConfigurationManager.AppSettings["UpLoadPassWord"]))
            {
                Response.Write("<script>alert('密码错误！');</script>");
                return;
            }

            StringBuilder Strings = new StringBuilder();
            string SqlQueryText = "select top 1000 *,(case MemberSex when '女' then '0' else '1'  end) as Sex, (CASE PassportType WHEN '身份证' THEN '1' when '护照' then '2' when '军官证' then '5'  ELSE '1' END) AS IdType from Member order by MemberId";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    SqlQueryText = string.Format("insert into OL_LoginUser (Id,UserEmail,LoginPassWord,UserName,Tel,Mobile,Sex,BirtyDay,Address,ZipCode,Hobby,LoginCount,LastLoginTime,Fax,IdType,IdNumber,Company,RegTime,LoginIp,OldId) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",
                            Convert.ToString(CombineKeys.NewComb()),
                            DS.Tables[0].Rows[i]["MemberEmail"].ToString(),
                            SecurityCode.Md5_Encrypt(DS.Tables[0].Rows[i]["MemberPassword"].ToString(), 32),
                            DS.Tables[0].Rows[i]["MemberName"].ToString(),
                            DS.Tables[0].Rows[i]["MemberTel"].ToString(),
                            DS.Tables[0].Rows[i]["MemberNation"].ToString(),
                            DS.Tables[0].Rows[i]["Sex"].ToString(),
                            DS.Tables[0].Rows[i]["MemberBirthday"].ToString(),
                            DS.Tables[0].Rows[i]["MemberAddress"].ToString(),
                            DS.Tables[0].Rows[i]["MemberZipcode"].ToString(),
                            DS.Tables[0].Rows[i]["MemberInterest"].ToString(),
                            DS.Tables[0].Rows[i]["MemberLoginCount"].ToString(),
                            DS.Tables[0].Rows[i]["MemberLastTime"].ToString(),
                            DS.Tables[0].Rows[i]["MemberFax"].ToString(),
                            DS.Tables[0].Rows[i]["IdType"].ToString(),
                            DS.Tables[0].Rows[i]["MemberPassport"].ToString(),
                            DS.Tables[0].Rows[i]["MemberCorp"].ToString(),
                            DS.Tables[0].Rows[i]["MemberRegTime"].ToString(),
                            DS.Tables[0].Rows[i]["MemberIp"].ToString(),
                            DS.Tables[0].Rows[i]["MemberId"].ToString()
                    );

                    if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
                    {
                        SqlQueryText = string.Format("DELETE FROM Member where MemberId='{0}'", DS.Tables[0].Rows[i]["MemberId"].ToString());
                        MyDataBaseComm.ExcuteSql(SqlQueryText);
                    }
                }
                Response.Write("<script>alert('导入完成！');</script>");
            }
        }
    }
}