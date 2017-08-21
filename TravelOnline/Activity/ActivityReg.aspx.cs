using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TravelOnline.Class.Common;

namespace TravelOnline
{
    public partial class ActivityReg : BasePage
    {
        public string strActInfoMain_ID;
        public string strActivityName;
        public object[,] ActInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //验证按钮：验证是否为空及验证后台数据是否正确
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (this.txbOrderNo.Text.Length > 0 && this.txbPhone.Text.Length > 0 && this.txbOrderNo.Text != "订单号" && this.txbPhone.Text != "联系电话")//验证是否有输入
            {
                if (CheckOrder(this.txbOrderNo.Text, this.txbPhone.Text))//订单号，手机号验证是否成功
                {
                    this.ddlGuestName.Visible = true;//显示儿童姓名下拉框
                    this.td.Visible = true;
                    this.btnCheck.Visible = false;//验证按钮隐藏
                    this.btnNext.Visible = true;//下一步按钮显示
                    this.txbOrderNo.Enabled = false;//订单号输入框禁用
                    this.txbPhone.Enabled = false;//手机号输入框禁用
                    this.lblCheckAlter.Text = "";//警告框内容清楚。
                    this.lblCheckAlter.Visible = false;//警告框隐藏。
                }
                else
                {
                    this.lblCheckAlter.Text = "订单号与手机号错误，请确认！";
                }
            }
            else
            {
                this.lblCheckAlter.Text = "订单号与手机号不能为空，请确认！";
            }
        }

        //下一步按钮：验证选择的儿童是否已经参加过活动
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (IsJoin(this.txbOrderNo.Text, this.txbPhone.Text, this.ddlGuestName.SelectedItem.ToString()))//判断儿童是否参加过活动
            {
                this.divCheck.Visible = false;
                this.divSave.Visible = true;
                this.ddlGuestName.Enabled = false;
                this.btnSave.Visible = true;
                this.btnNext.Visible = false;
                this.lblGuestName.Text = this.ddlGuestName.SelectedItem.ToString();
                ShowActInfo();
                Session["GuestName"] = this.ddlGuestName.SelectedItem.ToString();
                Session["ActivityName"] = this.rblActivityInfo.SelectedValue;
            }
            else
            {
                Session["ActivityName"] = strActivityName;
                Session["GuestName"] = this.ddlGuestName.SelectedItem.ToString();
                this.lblCheckAlter.Visible = true;
                this.lblCheckAlter.Text = string.Format("已报名:“{0}”！", Session["ActName"]);
                this.btnCancel.Visible = true;
                this.btnPrint1.Visible = true;
            }
        }

        //保存按钮
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ActInfo = (string[,])Session["List"];
            if (this.rblActivityInfo.SelectedValue == "")
            {
                this.lblSaveAlter.Text = "请选择活动！";
                return;
            }
                
            if (IsJoin(Session["AutoId"].ToString(), Session["OrderMobile"].ToString(), Session["GuestName"].ToString()))
            {
                if (IsAge(this.txbOrderNo.Text, this.txbPhone.Text, this.ddlGuestName.SelectedItem.ToString(), ActInfo[this.rblActivityInfo.SelectedIndex, 1].ToString()))//年龄满足11岁以下
                {

                    if (Save(Session["GuestID"].ToString(), Session["GuestName"].ToString(), Session["ActInfoMain_ID"].ToString(), Session["ActivityName"].ToString(), Session["AutoId"].ToString(), Session["OrderMobile"].ToString(), Session["PlanNo"].ToString()))
                    {
                        this.lblSaveAlter.Text = "报名成功！";
                        //Response.Write("<script language='javascript'>if(confirm('保存成功！'))</script>"); 
                        this.btnSave.Visible = false;
                        this.btnPrint2.Visible = true;
                    }
                    else
                    {
                        this.lblSaveAlter.Text = "系统保存异常请重试！";
                    }
                }
                else
                {
                    this.lblSaveAlter.Text = "儿童年龄超过活动限制！";
                    Session["ActivityName"] = null;
                }
            }
            else
            {
                this.lblSaveAlter.Visible = true;
                this.lblSaveAlter.Text = string.Format("已报名过{0}！", Session["ActName"].ToString());
                this.btnCancel.Visible = true;
                this.btnPrint1.Visible = true;
            }
        }

        //取消按钮：取消儿童已报名的活动。
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string strSqlCommand_Update = string.Format("update dbo.Act_ActInfoMain set JoinNum=JoinNum-1,Numbers=Numbers+1 where ActInfoMain_ID='{0}'", Session["ActInfoMain_ID"]);
            string strSqlCommand_Update2 = string.Format("update dbo.Act_Order set Status='0' where ActOrderID='{0}'", Session["ActOrderID"]);
            List<string> Sql = new List<string>();
            Sql.Add(strSqlCommand_Update);
            Sql.Add(strSqlCommand_Update2);
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                this.lblCheckAlter.Text = "取消报名成功！按“下一步继续报名！”";
                this.btnCancel.Visible = false;
                this.btnPrint1.Visible = false;
            }
            else
            {
                this.lblCheckAlter.Text = "取消报名发生异常,请重试！";
            }
        }

        //订单号手机号检查。
        private bool CheckOrder(string OrderID, string Mobile)
        {
            string strSqlCommand = "SELECT AutoId,GuestName,OrderMobile,PlanNo FROM [dbo].[View_Act_GuestInfo]";
            strSqlCommand += string.Format(" where AutoId='{0}' and OrderMobile ='{1}' group by AutoId,GuestName,OrderMobile,PlanNo", OrderID, Mobile);
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(strSqlCommand);
            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlGuestName.DataSource = DS.Tables[0].DefaultView;
                ddlGuestName.DataBind();
                Session["AutoId"] = DS.Tables[0].Rows[0]["AutoId"].ToString();
                Session["OrderMobile"] = DS.Tables[0].Rows[0]["OrderMobile"].ToString();
                Session["PlanNo"] = DS.Tables[0].Rows[0]["PlanNo"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        //显示可报名的活动
        private void ShowActInfo()
        {
            string strSqlCmd = "select * from dbo.Act_ActInfoMain where joinNum<MaxNum and Start ='正常'";
            DataSet DS = new DataSet();
            DS.Clear();
            DS = MyDataBaseComm.getDataSet(strSqlCmd);
            rblActivityInfo.DataSource = DS.Tables[0].DefaultView;
            rblActivityInfo.DataBind();
            int DSrows = DS.Tables[0].Rows.Count;
            ActInfo = new string[DSrows,2];
            for (int i = 0; i < DSrows; i++)
            {
                rblActivityInfo.Items[i].Text += "，剩余报名人数:" + DS.Tables[0].Rows[i]["Numbers"] + "名";
                if (Convert.ToInt16(DS.Tables[0].Rows[i]["Numbers"]) <= 0)
                {
                    rblActivityInfo.Items[i].Enabled = false;
                }
                ActInfo[i, 0] = DS.Tables[0].Rows[i]["ActInfoMain_ID"].ToString();
                ActInfo[i, 1] = DS.Tables[0].Rows[i]["ActivityName"].ToString();
                Session["List"] = ActInfo;
            }
        }

        //判断是否已经参加过活动
        private bool IsJoin(string OrderID,string OrderMobile,string GuestName)
        {
            string strSqlCommand = "select * from dbo.Act_Order";
            strSqlCommand += string.Format(" where OL_OrderID='{0}' and OrderMobile='{1}' and GuestName='{2}' and Status='1'",
            OrderID, OrderMobile, GuestName);
            DataSet DS_Activity_CheckAge = new DataSet();
            DS_Activity_CheckAge.Clear();
            DS_Activity_CheckAge = MyDataBaseComm.getDataSet(strSqlCommand);
            if (DS_Activity_CheckAge.Tables[0].Rows.Count <= 0)//未参加过活动
            {
                return true;
            }
            else
            {
                Session["ActName"] = DS_Activity_CheckAge.Tables[0].Rows[0]["ActName"].ToString();
                Session["ActInfoMain_ID"] = DS_Activity_CheckAge.Tables[0].Rows[0]["ActInfoMain_ID"].ToString();
                Session["ActOrderID"] = DS_Activity_CheckAge.Tables[0].Rows[0]["ActOrderID"].ToString();
                return false;
            }
        }

        protected void ddlGuestName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblCheckAlter.Visible = false;
            this.lblCheckAlter.Text = "";
            this.btnCancel.Visible = false;
            this.btnPrint1.Visible = false;
        }

        private bool IsAge(string OrderID, string OrderMobile, string GuestName, string ActInfoMain_ID)
        {
            ActInfo = (string[,])Session["List"];
            Session["ActInfoMain_ID"] = ActInfo[this.rblActivityInfo.SelectedIndex, 0].ToString();
            string strSqlCommand = "select * from dbo.View_Act_GuestInfo";
            strSqlCommand += string.Format(" where AutoID='{0}' and OrderMobile='{1}' and GuestName='{2}'", Session["AutoId"], Session["OrderMobile"], this.ddlGuestName.SelectedItem);
            strSqlCommand += string.Format(" and Age<=(select MaxAge from dbo.Act_ActInfoMain where Start ='正常' and ActInfoMain_ID ='{0}')", Session["ActInfoMain_ID"].ToString());
            strSqlCommand += string.Format(" and Age>=(select MinAge from dbo.Act_ActInfoMain where Start ='正常' and ActInfoMain_ID ='{0}')", Session["ActInfoMain_ID"].ToString());
            DataSet DS_Activity_CheckAge = new DataSet();
            DS_Activity_CheckAge.Clear();
            DS_Activity_CheckAge = MyDataBaseComm.getDataSet(strSqlCommand);
            if (DS_Activity_CheckAge.Tables[0].Rows.Count > 0)//年龄满足
            {
                Session["GuestID"] = DS_Activity_CheckAge.Tables[0].Rows[0]["GuestID"].ToString();
                Session["ActivityName"] = ActInfo[this.rblActivityInfo.SelectedIndex, 1].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        //活动订单保存
        private bool Save(string GuestID, string GuestName, string ActInfoMain_ID, string ActivityName, string OL_OrderID, string OrderMobile, string PlanNo)
        {
            ActInfo = (string[,])Session["List"];
            string strSqlCommand_insert = "INSERT INTO dbo.[Act_Order]([GuestID],[GuestName],[ActInfoMain_ID],[ActName],[JoinTime],[Status],[OL_OrderID],[OrderMobile],[PlanNo])VALUES ";
            strSqlCommand_insert += string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
            GuestID, GuestName, ActInfoMain_ID, ActivityName, System.DateTime.Now, "1", OL_OrderID, OrderMobile, PlanNo);
            string strSqlCommand_Update = string.Format("update dbo.Act_ActInfoMain set JoinNum=JoinNum+1,Numbers=Numbers-1 where ActInfoMain_ID='{0}'", ActInfo[this.rblActivityInfo.SelectedIndex,0].ToString());
            List<string> Sql = new List<string>();
            Sql.Add(strSqlCommand_insert);
            Sql.Add(strSqlCommand_Update);
            string[] SqlQuery = Sql.ToArray();
            if (MyDataBaseComm.Transaction(SqlQuery) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //打印订单按钮
        protected void btnPrint1_Click1(object sender, EventArgs e)
        {
            if (Session["ActOrderID"] != null)
            {
                Response.Write(string.Format("<script language='javascript'>window.open('Print.aspx?ActOrderID={0}');</script>", Session["ActOrderID"].ToString()));
            }
            else
            {
                IsJoin(Session["AutoId"].ToString(), Session["OrderMobile"].ToString(), Session["GuestName"].ToString());
                Response.Write(string.Format("<script language='javascript'>window.open('Print.aspx?ActOrderID={0}');</script>", Session["ActOrderID"].ToString()));
            }
        }
    }
}