using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace TravelOnline.Class.Manage
{
    public class ManageUsers
    {
       public class UserClass
        {
            public string Id { get; set; }
            public string LoginName { get; set; }
            public string UserName { get; set; }
            public string LoginPassWord { get; set; }
            public int UserRight { get; set; }
            public int UserDept { get; set; }
            public string RightInfos { get; set; }
        }//用户信息

       public static bool LoginUser_Sql(UserClass RegUserInfo, string DoFlag)
       {
           bool flag = false;
           string SqlQueryText = "";
           StringBuilder QueryString = new StringBuilder();

           switch (DoFlag)
           {
               case "Regist":
                   SqlQueryText = string.Format("insert into OL_ManageUser (Id,LoginName,UserName,LoginPassWord,UserRight,UserDept) values ('{0}','{1}','{2}','{3}','{4}','{5}')",
                       RegUserInfo.Id,
                       RegUserInfo.LoginName,
                       RegUserInfo.UserName,
                       RegUserInfo.LoginPassWord,
                       RegUserInfo.UserRight,
                       RegUserInfo.UserDept
                       );
                   break;
               case "EditInfo":
                   QueryString.Append("update OL_ManageUser set ");
                   QueryString.Append("LoginName='{1}',");
                   QueryString.Append("UserName='{2}',");
                   QueryString.Append("LoginPassWord='{3}',");
                   QueryString.Append("UserRight='{4}',");
                   QueryString.Append("UserDept='{5}'");
                   QueryString.Append(" where id='{0}'");
                   SqlQueryText = string.Format(
                       QueryString.ToString(),
                       RegUserInfo.Id,
                       RegUserInfo.LoginName,
                       RegUserInfo.UserName,
                       RegUserInfo.LoginPassWord,
                       RegUserInfo.UserRight,
                       RegUserInfo.UserDept
                       );
                   break;
               case "PassWord":
                   SqlQueryText = string.Format("update OL_ManageUser set LoginPassWord='{1}' where id='{0}'",
                       RegUserInfo.Id,
                       RegUserInfo.LoginPassWord
                       );
                   break;
               case "4":

                   break;
               default:
                   flag = false;
                   break;
           }
           
           if (MyDataBaseComm.ExcuteSql(SqlQueryText) == true)
           {
               flag = true;
           }
           return flag;
       }//ManageUser表数据更新

       public static UserClass ManageUseDetail(string QueryText)
       {
           string SqlQueryText;
           if (QueryText.Length == 0) QueryText = "1=1";
           SqlQueryText = string.Format("select top 1 *,(select RightCode from OL_UserRight where id=OL_ManageUser.UserRight) as RightInfos from OL_ManageUser where {0}", QueryText);
           UserClass UserDetail = new UserClass();
           DataSet DS = new DataSet();
           DS.Clear();
           DS = MyDataBaseComm.getDataSet(SqlQueryText);
           if (DS.Tables[0].Rows.Count > 0)
           {
               UserDetail.Id = DS.Tables[0].Rows[0]["Id"].ToString();
               UserDetail.LoginPassWord = DS.Tables[0].Rows[0]["LoginPassWord"].ToString();
               UserDetail.LoginName = DS.Tables[0].Rows[0]["LoginName"].ToString();
               UserDetail.UserName = DS.Tables[0].Rows[0]["UserName"].ToString();
               UserDetail.UserRight = MyConvert.ConToInt(DS.Tables[0].Rows[0]["UserRight"].ToString());
               UserDetail.UserDept = MyConvert.ConToInt(DS.Tables[0].Rows[0]["UserDept"].ToString());
               UserDetail.RightInfos = DS.Tables[0].Rows[0]["RightInfos"].ToString();
               return UserDetail;
           }
           else
           {
               return null;
           }
       }//ManageUser数据查询

        
    }
}