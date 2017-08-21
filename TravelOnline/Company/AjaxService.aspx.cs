using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TravelOnline.Class.Manage;
using TravelOnline.GetCombineKeys;
using TravelOnline.EncryptCode;
using TravelOnline.Class.Travel;
using System.Data;
using TravelOnline.Class.Common;
using System.Text;

namespace TravelOnline.Company
{
    public partial class AjaxService : System.Web.UI.Page
    {
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
                Response.Write("{\"info\":\"尚未登录\"}");
                Response.End();
            }
            switch (Request.QueryString["action"])
            {
                case "CompanyInfo":
                    CompanyInfo();
                    break;
                case "DeptInfo":
                    DeptInfo();
                    break;
                case "UserInfo":
                    UserInfo();
                    break;
                case "DeleteCompany":
                    DeleteSelectInfos("Company");
                    break;
                case "DeleteUser":
                    DeleteSelectInfos("OL_LoginUser");
                    break;
                default:
                    Response.Write("{\"success\":1}");
                    Response.End();
                    break;
            }
        }
        protected void DeleteSelectInfos(string DbTableName)
        {
            if (MyDataBaseComm.DeleteExcuteSql(DbTableName, "", string.Format("'{0}'", Request.QueryString["Id"])) == true)
            {
                Response.Write("{\"success\":0}");
            }
            else
            {
                Response.Write("{\"success\":1}");
            }
            Response.End();
        }

        //部门信息
        protected void UserInfo()
        {
            Guid ucode = CombineKeys.NewComb();
            string SqlQueryText;
            if (Request.Form["Cid"].Length <30)
            {
                SqlQueryText = string.Format("select id from OL_LoginUser where UserEmail='{0}'", Request.Form["useremail"].Trim());
                DataSet DS1 = new DataSet();
                DS1.Clear();
                DS1 = MyDataBaseComm.getDataSet(SqlQueryText);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    Response.Write("({\"error\":\"邮件地址已经存在，不能保存！\"})");
                    Response.End();

                }

                string mobile = Request.Form["mobile"].Trim();
                string pm = mobile;
                if (mobile.Length < 8) pm = "0000000" + mobile;
                string password = SecurityCode.Md5_Encrypt(pm.Substring(pm.Length - 6), 32);

                SqlQueryText = string.Format("insert into OL_LoginUser (Id,UserEmail,UserName,Mobile,Company,companyid,deptid,LoginPassWord,LoginCount,RegTime,LastLoginTime,misid) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','0','{8}','{8}','{9}')",
                    ucode,
                    Request.Form["useremail"].Trim(),
                    Request.Form["username"].Trim(),
                    Request.Form["mobile"].Trim(),
                    Request.Form["company"].Trim(),
                    Request.Form["companyid"].Trim(),
                    Request.Form["deptid"].Trim(),
                    password,
                    DateTime.Now.ToString(),
                    Request.Form["misid"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update OL_LoginUser set UserName='{1}',Mobile='{2}',misid='{3}',deptid='{4}' where id='{0}'",
                    Request.Form["Cid"],
                    Request.Form["username"].Trim(),
                    Request.Form["mobile"].Trim(),
                    Request.Form["misid"].Trim(),
                    Request.Form["deptid"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"用户信息保存失败\"})");
            }
        }

        //部门信息
        protected void DeptInfo()
        {
            Guid ucode = CombineKeys.NewComb();
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into DeptInfo (uid,companyid,deptname,misid) values ('{0}','{1}','{2}','{3}')",
                    ucode,
                    Request.Form["companyid"].Trim(),
                    Request.Form["deptname"].Trim(),
                    MyConvert.ConToInt(Request.Form["misid"].Trim())
                );
            }
            else
            {
                SqlQueryText = string.Format("update DeptInfo set deptname='{1}',misid='{2}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["deptname"].Trim(),
                    MyConvert.ConToInt(Request.Form["misid"].Trim())
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"部门信息保存失败\"})");
            }
        }

        protected void CompanyInfo()
        {
            Guid ucode = CombineKeys.NewComb();
            string SqlQueryText;
            if (MyConvert.ConToInt(Request.Form["Cid"]) == 0)
            {
                SqlQueryText = string.Format("insert into Company (uid,companyname,sortpy,address,zipcode,area,city,tel,fax,misid,RebateFlag) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                    ucode,
                    Request.Form["companyname"].Trim(),
                    FirstPinYin.IndexCode(Request.Form["companyname"].Trim()),
                    Request.Form["address"].Trim(),
                    Request.Form["zipcode"].Trim(),
                    Request.Form["s_province"].Trim(),
                    Request.Form["s_city"].Trim(),
                    Request.Form["tel"].Trim(),
                    Request.Form["fax"].Trim(),
                    MyConvert.ConToInt(Request.Form["misid"].Trim()),
                    Request.Form["RebateFlag"].Trim()
                );
            }
            else
            {
                SqlQueryText = string.Format("update Company set companyname='{1}',sortpy='{2}',address='{3}',zipcode='{4}',area='{5}',city='{6}',tel='{7}',fax='{8}',misid='{9}',RebateFlag='{10}' where id={0}",
                    Request.Form["Cid"],
                    Request.Form["companyname"].Trim(),
                    FirstPinYin.IndexCode(Request.Form["companyname"].Trim()),
                    Request.Form["address"].Trim(),
                    Request.Form["zipcode"].Trim(),
                    Request.Form["s_province"].Trim(),
                    Request.Form["s_city"].Trim(),
                    Request.Form["tel"].Trim(),
                    Request.Form["fax"].Trim(),
                    MyConvert.ConToInt(Request.Form["misid"].Trim()),
                    Request.Form["RebateFlag"].Trim()
                );
            }

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                Response.Write("({\"success\":\"OK\"})");
            }
            else
            {
                Response.Write("({\"error\":\"公司信息保存失败\"})");
            }
        }


    }
}