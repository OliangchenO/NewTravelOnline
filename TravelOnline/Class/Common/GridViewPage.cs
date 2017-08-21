using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TravelOnline.Class.Common
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        #region 分页相关属性和方法
        /// <summary>
        /// 总的记录数，目前仅用于自定义记录数
        /// </summary>
        protected int GridView_RecordCount = 0;

        /// <summary>
        /// 使用自定义的记录数
        /// 需要设置GridView_RecordCount
        /// 如果一次性绑定全部数据，不需要使用自定义，GridView可以通过DataSource自己获取
        /// </summary>
        protected bool IsUseCustomRecordCount = false;

        /// <summary>
        /// 实现GridView数据绑定的虚方法
        /// 在具体的页面类中重写这个方法，在PageTurn方法中就会调用重写的方法，以实现分页后的数据重新绑定
        /// </summary>
        protected virtual void GridView_DataBind()
        {
        }

        protected void GridView_Refresh(object sender, EventArgs e)
        {
            GridView_DataBind();
        }

        /// <summary>
        /// 分页页码跳转
        /// </summary>
        /// <param name="sender">跳转按钮</param>
        /// <param name="e"></param>
        protected void GridView_PageTurn(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btnGoPage;
            System.Web.UI.WebControls.GridView GridView1;
            System.Web.UI.WebControls.TextBox txtGoPage;
            try
            {
                btnGoPage = (System.Web.UI.WebControls.Button)sender;
                GridView1 = (System.Web.UI.WebControls.GridView)btnGoPage.NamingContainer.Parent.Parent;
                txtGoPage = (System.Web.UI.WebControls.TextBox)GridView1.BottomPagerRow.FindControl("txtGoPage");
            }
            catch
            {
                //MessageBox.Show(this.Page, "页码输入框和跳转按钮都必须在GridView的分页模板中！");
                return;
            }

            int pageIndex = 1;
            bool goSuccess = false;

            if (!string.IsNullOrEmpty(txtGoPage.Text.Trim()))
            {
                if (int.TryParse(txtGoPage.Text.Trim(), out pageIndex))
                {
                    pageIndex--;

                    if (pageIndex >= 0 && pageIndex < GridView1.PageCount)
                    {
                        goSuccess = true;

                        GridView1.PageIndex = pageIndex;
                        GridView_DataBind();
                    }
                }
            }

            if (!goSuccess)
            {
                //MessageBox.Show(this.Page, "无效的页码！");
                return;
            }
        }

        /// <summary>
        /// 页码改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            System.Web.UI.WebControls.GridView GridView1 = sender as System.Web.UI.WebControls.GridView;

            GridView1.PageIndex = e.NewPageIndex;
            GridView_DataBind();
        }

        /// <summary>
        /// 索引改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>GridView_Sorting
        protected void GridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            System.Web.UI.WebControls.GridView GridView1 = sender as System.Web.UI.WebControls.GridView;

            // 从事件参数获取排序数据列
            string sortExpression = e.SortExpression.ToString();

            // 假定为排序方向为“顺序”
            string sortDirection = "ASC";

            // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
            if (sortExpression == GridView1.Attributes["SortExpression"])
            {
                //获得下一次的排序状态
                sortDirection = (GridView1.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
            }

            // 重新设定GridView排序数据列及排序方向
            GridView1.Attributes["SortExpression"] = sortExpression;
            GridView1.Attributes["SortDirection"] = sortDirection;

            GridView_DataBind();

            //GridView1.PageIndex = e.NewPageIndex;
            //GridView_DataBind();
        }

        /// <summary>
        /// GridView数据绑定完毕之后触发，显示记录数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView_DataBound(object sender, EventArgs e)
        {
            //获取当前GridView
            System.Web.UI.WebControls.GridView GridView1 = sender as System.Web.UI.WebControls.GridView;

            if (GridView1.BottomPagerRow == null)
            {
                return;
            }

            //总是显示分页行
            GridView1.BottomPagerRow.Visible = true;

            System.Web.UI.WebControls.Label lblRecorCount = (System.Web.UI.WebControls.Label)GridView1.BottomPagerRow.FindControl("lblRecorCount");
            if (IsUseCustomRecordCount)
            {
                lblRecorCount.Text = GridView_RecordCount.ToString();
            }
            else
            {
                if (GridView1.DataSource == null)
                {
                    return;
                }

                //根据数据类型，动态获取绑定的数据源的记录数
                if (GridView1.DataSource.GetType() == typeof(DataView))
                {
                    lblRecorCount.Text = ((DataView)GridView1.DataSource).Count.ToString();
                }
                else if (GridView1.DataSource.GetType() == typeof(DataTable))
                {
                    lblRecorCount.Text = ((DataTable)GridView1.DataSource).Rows.Count.ToString();
                }
                else if (GridView1.DataSource.GetType() == typeof(DataSet))
                {
                    lblRecorCount.Text = ((DataSet)GridView1.DataSource).Tables[0].Rows.Count.ToString();
                }
                else if (GridView1.DataSource is Array)
                {
                    lblRecorCount.Text = ((Array)GridView1.DataSource).Length.ToString();
                }
                else if (GridView1.DataSource.GetType() is System.Collections.IList)
                {
                    lblRecorCount.Text = ((System.Collections.IList)GridView1.DataSource).Count.ToString();
                }
                else if (GridView1.DataSource.GetType() is System.Collections.ICollection)
                {
                    lblRecorCount.Text = ((System.Collections.ICollection)GridView1.DataSource).Count.ToString();
                }
                else
                {
                    //TspOALog.Write("绑定到GridView的数据类型未知：" + GridView1.DataSource.GetType());
                }
            }
        }
        #endregion
    }
}