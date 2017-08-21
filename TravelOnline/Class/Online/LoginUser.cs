using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Management;

namespace TravelOnline.LoginUsers
{
    public class LoginUser
    {
        public class RegistUser
        {
            public string Id { get; set; }
            public string UserEmail { get; set; }
            public string LoginPassWord { get; set; }
            public string UserName { get; set; }
            public string Tel { get; set; }
            public string Mobile { get; set; }
            public int Sex { get; set; }
            public string BirtyDay { get; set; }
            public string Address { get; set; }
            public string ZipCode { get; set; }
            public int Marriage { get; set; }
            public int Income { get; set; }
            public int Vip { get; set; }
            public string Hobby { get; set; }
            public int Career { get; set; }
            public string Deptid { get; set; }
            public string Companyid { get; set; }
            public string DeptName { get; set; }
            public string CompanyName { get; set; }
            public string RebateFlag { get; set; }
            public string YJDeptName { get; set; }
            public string CardID { get; set; }
            public string UserType { get; set; }
            public string ThirdPartyType { get; set; }
            public string ThirdPartyID { get; set; }
            //微信号
            public string Wxid { get; set; }
            //微店名称
            public string Storename { get; set; }
            public string headimgurl { get; set; }
        }//用户注册

        public static bool LoginUser_Sql(RegistUser RegUserInfo, string DoFlag)
        {
            bool flag = false;
            string SqlQueryText="";
            StringBuilder QueryString = new StringBuilder();

            switch (DoFlag)
            {
                case "Regist":
                    SqlQueryText = string.Format("insert into OL_LoginUser (Id,UserEmail,LoginPassWord,RegTime,LoginCount,LastLoginTime,UserName,Mobile,CardID,UserType,ThirdPartyType,ThirdPartyID,Company) values ('{0}','{1}','{2}','{3}','1','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                        RegUserInfo.Id,
                        RegUserInfo.UserEmail,
                        RegUserInfo.LoginPassWord,
                        DateTime.Now.ToString(),
                        RegUserInfo.UserName,
                        RegUserInfo.Mobile,
                        RegUserInfo.CardID,
                        RegUserInfo.UserType,
                        RegUserInfo.ThirdPartyType,
                        RegUserInfo.ThirdPartyID,
                        RegUserInfo.CompanyName
                        );
                    break;
                case "FxRegist":
                    SqlQueryText = string.Format("insert into OL_FXLoginUser (Id,UserEmail,LoginPassWord,RegTime,LoginCount,LastLoginTime,UserName,Mobile,Wxid,Storename,Tel,Address,headimgurl,ThirdPartyType,ThirdPartyID) values ('{0}','{1}','{2}','{3}','1','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                        RegUserInfo.Id,
                        RegUserInfo.UserEmail,
                        RegUserInfo.LoginPassWord,
                        DateTime.Now.ToString(),
                        RegUserInfo.UserName,
                        RegUserInfo.Mobile,
                        RegUserInfo.Wxid,
                        RegUserInfo.Storename,
                        RegUserInfo.Tel,
                        RegUserInfo.Address,
                        RegUserInfo.headimgurl,
                        RegUserInfo.ThirdPartyType,
                        RegUserInfo.ThirdPartyID
                        );
                    break;
                case "WeChatRegist":
                    SqlQueryText = string.Format("insert into OL_LoginUser (Id,UserEmail,LoginPassWord,RegTime,LoginCount,LastLoginTime,UserName,Mobile) values ('{0}','{1}','{2}','{3}','1','{3}','{4}','{5}')",
                        RegUserInfo.Id,
                        RegUserInfo.UserEmail,
                        RegUserInfo.LoginPassWord,
                        DateTime.Now.ToString(),
                        RegUserInfo.UserName,
                        RegUserInfo.Mobile
                        );
                    break;
                case "EditInfo":
                    QueryString.Append("update OL_LoginUser set ");
                    QueryString.Append("UserName='{1}',");
                    QueryString.Append("Tel='{2}',");
                    QueryString.Append("Mobile='{3}',");
                    QueryString.Append("Sex={4},");
                    QueryString.Append("BirtyDay='{5}',");
                    QueryString.Append("Address='{6}',");
                    QueryString.Append("ZipCode='{7}',");
                    QueryString.Append("Marriage='{8}',");
                    QueryString.Append("Income='{9}',");
                    QueryString.Append("Hobby='{10}',");
                    QueryString.Append("Career='{11}'");
                    QueryString.Append(" where id='{0}'");
                    SqlQueryText = string.Format(
                        QueryString.ToString(),
                        RegUserInfo.Id,
                        RegUserInfo.UserName,
                        RegUserInfo.Tel,
                        RegUserInfo.Mobile,
                        RegUserInfo.Sex,
                        RegUserInfo.BirtyDay,
                        RegUserInfo.Address,
                        RegUserInfo.ZipCode,
                        RegUserInfo.Marriage,
                        RegUserInfo.Income,
                        RegUserInfo.Hobby,
                        RegUserInfo.Career
                        );
                    break;
                case "PassWord":
                    SqlQueryText = string.Format("update OL_LoginUser set LoginPassWord='{1}' where id='{0}'",
                        RegUserInfo.Id,
                        RegUserInfo.LoginPassWord
                        );
                    break;
                case "NotMember":
                    SqlQueryText = string.Format("insert into OL_LoginUser (Id,RegTime,LoginCount,LastLoginTime,UserName,ThirdPartyType,ThirdPartyID) values ('{0}','{1}',1,'{1}','{2}','{3}','{4}')",
                        RegUserInfo.Id,
                        DateTime.Now.ToString(),
                        RegUserInfo.UserName,
                        RegUserInfo.ThirdPartyType,
                        RegUserInfo.ThirdPartyID
                        );
                    break;
                case "4":
                    
                    break;
                default:
                    flag = false;
                    break;
            }
            //if (DoFlag == "Insert")
            //{
            //    SqlQueryText = string.Format("insert into OL_LoginUser" +
            //        " (Id,UserEmail,LoginPassWord,UserName,Mobile,Sex) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
            //        RegUserInfo.Id,
            //        RegUserInfo.UserEmail,
            //        RegUserInfo.LoginPassWord,
            //        RegUserInfo.UserName,
            //        RegUserInfo.Mobile,
            //        RegUserInfo.Sex
            //        );
            //}
            //else {
            //    SqlQueryText = string.Format("update OL_LoginUser set UserEmail='{1}',LoginPassWord='{2}' where id='{0}')", 
            //        RegUserInfo.Id,
            //        RegUserInfo.UserEmail,
            //        RegUserInfo.LoginPassWord
            //        );
            //}

            if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
            {
                flag = true;
            }
            return flag;
        }//LoginUser表数据更新

        public static RegistUser LoginUseDetail(string QueryText)
        {
            string SqlQueryText;
            if (QueryText.Length == 0) QueryText = "1=1";
            SqlQueryText = string.Format("select top 1 *,(select deptname from DeptInfo where id=OL_LoginUser.deptid) as thdeptname,(select companyname from Company where id=OL_LoginUser.companyid) as companyname,(select RebateFlag from Company where id=OL_LoginUser.companyid) as RebateFlag from OL_LoginUser where {0}", QueryText);
            RegistUser UserDetail = new RegistUser();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                UserDetail.Id = DS.Tables[0].Rows[0]["Id"].ToString();
                UserDetail.UserEmail = DS.Tables[0].Rows[0]["UserEmail"].ToString();
                UserDetail.LoginPassWord = DS.Tables[0].Rows[0]["LoginPassWord"].ToString();
                UserDetail.UserName = DS.Tables[0].Rows[0]["UserName"].ToString();
                UserDetail.Tel = DS.Tables[0].Rows[0]["Tel"].ToString();
                UserDetail.Mobile = DS.Tables[0].Rows[0]["Mobile"].ToString();
                UserDetail.Sex = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Sex"].ToString());

                UserDetail.BirtyDay = MyConvert.DateToString(DS.Tables[0].Rows[0]["BirtyDay"].ToString());
                UserDetail.Address = DS.Tables[0].Rows[0]["Address"].ToString();
                UserDetail.ZipCode = DS.Tables[0].Rows[0]["ZipCode"].ToString();
                UserDetail.Marriage = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Marriage"].ToString());
                UserDetail.Income = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Income"].ToString());
                UserDetail.Vip = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Vip"].ToString());
                UserDetail.Hobby = DS.Tables[0].Rows[0]["Hobby"].ToString();
                UserDetail.Career = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Career"].ToString());

                UserDetail.Deptid = DS.Tables[0].Rows[0]["deptid"].ToString();
                UserDetail.Companyid = DS.Tables[0].Rows[0]["companyid"].ToString();

                UserDetail.DeptName = DS.Tables[0].Rows[0]["thdeptname"].ToString();
                UserDetail.CompanyName = DS.Tables[0].Rows[0]["companyname"].ToString();
                UserDetail.RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
                UserDetail.YJDeptName = DS.Tables[0].Rows[0]["deptname"].ToString();
                UserDetail.CardID = DS.Tables[0].Rows[0]["cardId"].ToString();
                UserDetail.UserType = DS.Tables[0].Rows[0]["userType"].ToString();
                return UserDetail;
            }
            else
            {
                return null;
            }
        }//LoginUser数据查询


        public static RegistUser LoginFxUser(string QueryText)
        {
            string SqlQueryText;
            if (QueryText.Length == 0) QueryText = "1=1";
            SqlQueryText = string.Format("select top 1 *,(select deptname from DeptInfo where id=OL_FXLoginUser.deptid) as thdeptname,(select companyname from Company where id=OL_FXLoginUser.companyid) as companyname,(select RebateFlag from Company where id=OL_FXLoginUser.companyid) as RebateFlag from OL_FXLoginUser where {0}", QueryText);
            RegistUser UserDetail = new RegistUser();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                UserDetail.Id = DS.Tables[0].Rows[0]["Id"].ToString();
                UserDetail.UserEmail = DS.Tables[0].Rows[0]["UserEmail"].ToString();
                UserDetail.LoginPassWord = DS.Tables[0].Rows[0]["LoginPassWord"].ToString();
                UserDetail.UserName = DS.Tables[0].Rows[0]["UserName"].ToString();
                UserDetail.Tel = DS.Tables[0].Rows[0]["Tel"].ToString();
                UserDetail.Mobile = DS.Tables[0].Rows[0]["Mobile"].ToString();
                UserDetail.Sex = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Sex"].ToString());

                UserDetail.BirtyDay = MyConvert.DateToString(DS.Tables[0].Rows[0]["BirtyDay"].ToString());
                UserDetail.Address = DS.Tables[0].Rows[0]["Address"].ToString();
                UserDetail.ZipCode = DS.Tables[0].Rows[0]["ZipCode"].ToString();
                UserDetail.Marriage = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Marriage"].ToString());
                UserDetail.Income = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Income"].ToString());
                UserDetail.Vip = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Vip"].ToString());
                UserDetail.Hobby = DS.Tables[0].Rows[0]["Hobby"].ToString();
                UserDetail.Career = MyConvert.ConToInt(DS.Tables[0].Rows[0]["Career"].ToString());

                UserDetail.Deptid = DS.Tables[0].Rows[0]["deptid"].ToString();
                UserDetail.Companyid = DS.Tables[0].Rows[0]["companyid"].ToString();

                UserDetail.DeptName = DS.Tables[0].Rows[0]["thdeptname"].ToString();
                UserDetail.CompanyName = DS.Tables[0].Rows[0]["companyname"].ToString();
                UserDetail.RebateFlag = DS.Tables[0].Rows[0]["RebateFlag"].ToString();
                UserDetail.YJDeptName = DS.Tables[0].Rows[0]["deptname"].ToString();
                UserDetail.CardID = DS.Tables[0].Rows[0]["cardId"].ToString();
                UserDetail.UserType = DS.Tables[0].Rows[0]["userType"].ToString();
                UserDetail.ThirdPartyID = DS.Tables[0].Rows[0]["ThirdPartyID"].ToString();
                UserDetail.ThirdPartyType = DS.Tables[0].Rows[0]["ThirdPartyType"].ToString();
                UserDetail.Wxid = DS.Tables[0].Rows[0]["Wxid"].ToString();
                UserDetail.Storename = DS.Tables[0].Rows[0]["Storename"].ToString();
                UserDetail.headimgurl = DS.Tables[0].Rows[0]["headimgurl"].ToString();
                return UserDetail;
            }
            else
            {
                return null;
            }
        }
    }
    
}