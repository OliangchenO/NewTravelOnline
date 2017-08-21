using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace TravelOnline.Class.Manage
{
    public class UserRight
    {
        public class UserRightClass
        {
            public string Id { get; set; }
            public string RightName { get; set; }
            public string RightCode { get; set; }
            public string RightFlag { get; set; }
        }//UserRight信息

        public static bool UserRight_Sql(UserRightClass RightInfo, string DoFlag)
        {
            bool flag = false;
            string SqlQueryText = "";
            StringBuilder QueryString = new StringBuilder();

            switch (DoFlag)
            {
                case "AddNew":
                    SqlQueryText = string.Format("insert into OL_UserRight (RightName,RightCode,RightFlag) values ('{0}','{1}','{2}')",
                        RightInfo.RightName,
                        RightInfo.RightCode,
                        RightInfo.RightFlag
                        );
                    break;
                case "EditInfo":
                    QueryString.Append("update OL_UserRight set ");
                    QueryString.Append("RightName='{1}',");
                    QueryString.Append("RightCode='{2}'");
                    QueryString.Append(" where id='{0}'");
                    SqlQueryText = string.Format(
                        QueryString.ToString(),
                        RightInfo.Id,
                        RightInfo.RightName,
                        RightInfo.RightCode
                        );
                    break;
                case "Delete":
                    SqlQueryText = string.Format("delete from OL_UserRight where id='{0}'",
                        RightInfo.Id
                        );
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
        }//OL_UserRight表数据更新

        public static UserRightClass UserRightDetail(string QueryText)
        {
            string SqlQueryText;
            if (QueryText.Length == 0) QueryText = "1=1";
            SqlQueryText = string.Format("select top 1 * from OL_UserRight where {0}", QueryText);
            UserRightClass RightDetail = new UserRightClass();
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(SqlQueryText);
            if (DS.Tables[0].Rows.Count > 0)
            {
                RightDetail.Id = DS.Tables[0].Rows[0]["Id"].ToString();
                RightDetail.RightName = DS.Tables[0].Rows[0]["RightName"].ToString();
                RightDetail.RightCode = DS.Tables[0].Rows[0]["RightCode"].ToString();
                RightDetail.RightFlag = DS.Tables[0].Rows[0]["RightFlag"].ToString();
                return RightDetail;
            }
            else
            {
                return null;
            }
        }//OL_UserRight数据查询

        public static string CreatRightCheckBox(string RightCode,string RightFlag)
        {
            string CheckBoxString="";
            switch (RightFlag)
            {
                case "Menu":
                    CheckBoxString = MenuCheckBox(RightCode);
                    break;
                case "Operation":
                    CheckBoxString = MenuOpCheckBox(RightCode);
                    break;
                case "ChangePassWord":

                    break;
                case "EditInfo":
                   
                    break;
                default:
                    
                    break;
            }
            //StringBuilder CheckBoxString = new StringBuilder();
            return CheckBoxString;
        }

        public static string MenuCheckBox(string RightCode)
        {
            StringBuilder CheckBoxString = new StringBuilder();
            CheckBoxString.Append("<DIV class=mt><H1></H1><STRONG>功能菜单明细选项</STRONG></DIV>");

            CheckBoxString.Append("<DL class=fore><DT>登录用户管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@1@1,", RightCode));
            CheckBoxString.Append("用户权限设置</DIV>");
            CheckBoxString.Append(IsChecked("@1@2,", RightCode));
            CheckBoxString.Append("部门设置</DIV>");
            CheckBoxString.Append(IsChecked("@1@3,", RightCode));
            CheckBoxString.Append("登录用户设置</DIV>");
            CheckBoxString.Append(IsChecked("@1@4,", RightCode));
            CheckBoxString.Append("同行旅行社</DIV>");
            CheckBoxString.Append(IsChecked("@1@5,", RightCode));
            CheckBoxString.Append("同行用户</DIV>");
            //CheckBoxString.Append(IsChecked("@1@4,", RightCode));
            //CheckBoxString.Append("修改登录密码</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>基础信息设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("@2@1,", RightCode));
            CheckBoxString.Append("旅游产品分类导航</DIV>");
            CheckBoxString.Append(IsChecked("@2@2,", RightCode));
            CheckBoxString.Append("旅游产品大类设置</DIV>");
            CheckBoxString.Append(IsChecked("@2@3,", RightCode));
            CheckBoxString.Append("基础资料信息</DIV>");
            CheckBoxString.Append(IsChecked("@2@4,", RightCode));
            CheckBoxString.Append("目的地</DIV>");
            CheckBoxString.Append(IsChecked("@2@5,", RightCode));
            CheckBoxString.Append("SEO内链关键字</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>网站广告管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@3@1,", RightCode));
            CheckBoxString.Append("Flash轮换广告</DIV>");
            //CheckBoxString.Append(IsChecked("@3@2,", RightCode));
            //CheckBoxString.Append("Banner随机广告</DIV>");
            CheckBoxString.Append(IsChecked("@3@3,", RightCode));
            CheckBoxString.Append("首页主题旅游</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>公共信息管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@4@1,", RightCode));
            CheckBoxString.Append("公告信息</DIV>");
            CheckBoxString.Append(IsChecked("@4@2,", RightCode));
            CheckBoxString.Append("服务信息</DIV>");
            //CheckBoxString.Append(IsChecked("@4@3,", RightCode));
            //CheckBoxString.Append("关于我们</DIV>");
            CheckBoxString.Append(IsChecked("@4@4,", RightCode));
            CheckBoxString.Append("友情链接</DIV>");
            CheckBoxString.Append(IsChecked("@4@5,", RightCode));
            CheckBoxString.Append("帮助信息</DIV>");
            CheckBoxString.Append(IsChecked("@4@6,", RightCode));
            CheckBoxString.Append("出境须知</DIV>");
            CheckBoxString.Append(IsChecked("@4@7,", RightCode));
            CheckBoxString.Append("游记攻略</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>旅游产品管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@5@1,", RightCode));
            CheckBoxString.Append("出境旅游</DIV>");
            CheckBoxString.Append(IsChecked("@5@5,", RightCode));
            CheckBoxString.Append("国内旅游</DIV>");
            CheckBoxString.Append(IsChecked("@5@2,", RightCode));
            CheckBoxString.Append("自由行</DIV>");
            CheckBoxString.Append(IsChecked("@5@3,", RightCode));
            CheckBoxString.Append("邮轮旅游</DIV>");
            CheckBoxString.Append(IsChecked("@5@4,", RightCode));
            CheckBoxString.Append("单项服务</DIV>");
            CheckBoxString.Append(IsChecked("@5@5,", RightCode));
            CheckBoxString.Append("优惠券</DIV>");
            CheckBoxString.Append(IsChecked("@5@6,", RightCode));
            CheckBoxString.Append("线路推荐</DIV>");
            CheckBoxString.Append(IsChecked("@5@7,", RightCode));
            CheckBoxString.Append("线路专题</DIV>");
            CheckBoxString.Append(IsChecked("@5@11,", RightCode));
            CheckBoxString.Append("线路专题-首页</DIV>");
            CheckBoxString.Append(IsChecked("@5@8,", RightCode));
            CheckBoxString.Append("线路优惠</DIV>");
            CheckBoxString.Append(IsChecked("@5@9,", RightCode));
            CheckBoxString.Append("线路优惠-国内</DIV>");
            CheckBoxString.Append(IsChecked("@5@10,", RightCode));
            CheckBoxString.Append("线路优惠-出境</DIV>");
            CheckBoxString.Append(IsChecked("@5@11,", RightCode));
            CheckBoxString.Append("商圈管理</DIV>");
            CheckBoxString.Append(IsChecked("@5@12,", RightCode));
            CheckBoxString.Append("点评管理</DIV>");
            CheckBoxString.Append(IsChecked("@5@13,", RightCode));
            CheckBoxString.Append("线路拼团</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>邮轮包船：</DT><DD>");
            CheckBoxString.Append(IsChecked("@7@1,", RightCode));
            CheckBoxString.Append("邮轮信息</DIV>");
            CheckBoxString.Append(IsChecked("@7@2,", RightCode));
            CheckBoxString.Append("包船航线</DIV>");
            CheckBoxString.Append(IsChecked("@7@3,", RightCode));
            CheckBoxString.Append("包船订单</DIV>");
            CheckBoxString.Append(IsChecked("@7@4,", RightCode));
            CheckBoxString.Append("变更申请</DIV>");
            CheckBoxString.Append(IsChecked("@7@5,", RightCode));
            CheckBoxString.Append("同步记录</DIV>");
            CheckBoxString.Append(IsChecked("@7@6,", RightCode));
            CheckBoxString.Append("船上活动</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>会员管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@6@1,", RightCode));
            CheckBoxString.Append("会员信息管理</DIV>");
            CheckBoxString.Append(IsChecked("@6@2,", RightCode));
            CheckBoxString.Append("订单管理</DIV>");
            CheckBoxString.Append(IsChecked("@6@5,", RightCode));
            CheckBoxString.Append("临时订单</DIV>");
            CheckBoxString.Append(IsChecked("@6@6,", RightCode));
            CheckBoxString.Append("禧荣订单</DIV>");
            CheckBoxString.Append(IsChecked("@6@7,", RightCode));
            CheckBoxString.Append("积分信息管理</DIV>");
            CheckBoxString.Append(IsChecked("@6@3,", RightCode));
            CheckBoxString.Append("会员评价</DIV>");
            CheckBoxString.Append(IsChecked("@6@4,", RightCode));
            CheckBoxString.Append("有问必答</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>报表中心：</DT><DD>");
            CheckBoxString.Append(IsChecked("@8@1,", RightCode));
            CheckBoxString.Append("会员报表</DIV>");
            CheckBoxString.Append(IsChecked("@8@2,", RightCode));
            CheckBoxString.Append("销售报表</DIV>");
            CheckBoxString.Append(IsChecked("@8@3,", RightCode));
            CheckBoxString.Append("产品报表</DIV>");
            CheckBoxString.Append(IsChecked("@8@4,", RightCode));
            CheckBoxString.Append("财务报表</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>分销管理：</DT><DD>");
            CheckBoxString.Append(IsChecked("@10@1,", RightCode));
            CheckBoxString.Append("分销订单</DIV>");
            CheckBoxString.Append("</DD></DL>");

            return CheckBoxString.ToString();
        }

        public static string MenuOpCheckBox(string RightCode)
        {
            StringBuilder CheckBoxString = new StringBuilder();
            CheckBoxString.Append("<DIV class=mt><H1></H1><STRONG>业务菜单明细选项</STRONG></DIV>");

            CheckBoxString.Append("<DL class=fore><DT>出境旅游权限设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("$1$0,", RightCode));
            CheckBoxString.Append("全部</DIV>");
            CheckBoxString.Append(IsChecked("$1$1,", RightCode));
            CheckBoxString.Append("东南亚</DIV>");
            CheckBoxString.Append(IsChecked("$1$2,", RightCode));
            CheckBoxString.Append("美加</DIV>");
            CheckBoxString.Append(IsChecked("$1$3,", RightCode));
            CheckBoxString.Append("欧洲</DIV>");
            CheckBoxString.Append(IsChecked("$1$4,", RightCode));
            CheckBoxString.Append("南美</DIV>");
            CheckBoxString.Append(IsChecked("$1$5,", RightCode));
            CheckBoxString.Append("港澳</DIV>");
            CheckBoxString.Append(IsChecked("$1$6,", RightCode));
            CheckBoxString.Append("台湾</DIV>");
            CheckBoxString.Append(IsChecked("$1$7,", RightCode));
            CheckBoxString.Append("日韩</DIV>");
            CheckBoxString.Append(IsChecked("$1$8,", RightCode));
            CheckBoxString.Append("澳新</DIV>");
            CheckBoxString.Append(IsChecked("$1$9,", RightCode));
            CheckBoxString.Append("中东非洲</DIV>");
            CheckBoxString.Append(IsChecked("$1$10,", RightCode));
            CheckBoxString.Append("德铁欧洲</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>国内旅游权限设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("$2$0,", RightCode));
            CheckBoxString.Append("全部</DIV>");
            CheckBoxString.Append(IsChecked("$2$1,", RightCode));
            CheckBoxString.Append("华南</DIV>");
            CheckBoxString.Append(IsChecked("$2$2,", RightCode));
            CheckBoxString.Append("华东</DIV>");
            CheckBoxString.Append(IsChecked("$2$3,", RightCode));
            CheckBoxString.Append("华中</DIV>");
            CheckBoxString.Append(IsChecked("$2$4,", RightCode));
            CheckBoxString.Append("北方</DIV>");
            CheckBoxString.Append(IsChecked("$2$5,", RightCode));
            CheckBoxString.Append("西南</DIV>");
            CheckBoxString.Append(IsChecked("$2$6,", RightCode));
            CheckBoxString.Append("西北</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>自由行权限设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("$3$0,", RightCode));
            CheckBoxString.Append("全部</DIV>");
            CheckBoxString.Append(IsChecked("$3$1,", RightCode));
            CheckBoxString.Append("国内</DIV>");
            CheckBoxString.Append(IsChecked("$3$2,", RightCode));
            CheckBoxString.Append("港澳台</DIV>");
            CheckBoxString.Append(IsChecked("$3$3,", RightCode));
            CheckBoxString.Append("日韩</DIV>");
            CheckBoxString.Append(IsChecked("$3$4,", RightCode));
            CheckBoxString.Append("东南亚</DIV>");
            CheckBoxString.Append(IsChecked("$3$5,", RightCode));
            CheckBoxString.Append("美加</DIV>");
            CheckBoxString.Append(IsChecked("$3$6,", RightCode));
            CheckBoxString.Append("欧洲</DIV>");
            CheckBoxString.Append(IsChecked("$3$7,", RightCode));
            CheckBoxString.Append("中东非洲</DIV>");
            CheckBoxString.Append(IsChecked("$3$8,", RightCode));
            CheckBoxString.Append("澳新</DIV>");
            CheckBoxString.Append(IsChecked("$3$9,", RightCode));
            CheckBoxString.Append("FIT</DIV>");
            CheckBoxString.Append("</DD></DL>");


            CheckBoxString.Append("<DL><DT>邮轮权限设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("$4$0,", RightCode));
            CheckBoxString.Append("全部</DIV>");
            CheckBoxString.Append(IsChecked("$4$1,", RightCode));
            CheckBoxString.Append("日韩航线</DIV>");
            CheckBoxString.Append(IsChecked("$4$2,", RightCode));
            CheckBoxString.Append("东南亚线</DIV>");
            CheckBoxString.Append(IsChecked("$4$3,", RightCode));
            CheckBoxString.Append("台湾航线</DIV>");
            CheckBoxString.Append(IsChecked("$4$4,", RightCode));
            CheckBoxString.Append("欧美航线</DIV>");
            CheckBoxString.Append("</DD></DL>");

            CheckBoxString.Append("<DL><DT>单项服务权限设置：</DT><DD>");
            CheckBoxString.Append(IsChecked("$5$0,", RightCode));
            CheckBoxString.Append("全部</DIV>");
            CheckBoxString.Append(IsChecked("$5$1,", RightCode));
            CheckBoxString.Append("亚洲</DIV>");
            CheckBoxString.Append(IsChecked("$5$2,", RightCode));
            CheckBoxString.Append("入台证</DIV>");
            CheckBoxString.Append(IsChecked("$5$3,", RightCode));
            CheckBoxString.Append("欧洲</DIV>");
            CheckBoxString.Append(IsChecked("$5$4,", RightCode));
            CheckBoxString.Append("美洲</DIV>");
            CheckBoxString.Append(IsChecked("$5$5,", RightCode));
            CheckBoxString.Append("中东非洲</DIV>");
            CheckBoxString.Append(IsChecked("$5$6,", RightCode));
            CheckBoxString.Append("大洋洲</DIV>");
            CheckBoxString.Append("</DD></DL>");

            return CheckBoxString.ToString();
        }

        public static string IsChecked(string Codes,string RightCode)
        {
            if (RightCode.IndexOf(Codes) > 0)
            {
                return string.Format("<DIV><input type=checkbox name=CheckBox value=\"{0}\" checked=checked />", Codes); 
            }
            else
            {
                return string.Format("<DIV><input type=checkbox name=CheckBox value=\"{0}\" />", Codes); 
            }
        }

        public static string IsOpChecked(string Codes, string RightCode)
        {
            if (RightCode.IndexOf(Codes) > 0)
            {
                return string.Format("<DIV><input type=checkbox name=OpCheckBox value=\"{0}\" checked=checked />", Codes);
            }
            else
            {
                return string.Format("<DIV><input type=checkbox name=OpCheckBox value=\"{0}\" />", Codes);
            }
        }
    }
}