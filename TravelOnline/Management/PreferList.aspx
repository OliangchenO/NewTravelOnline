﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferList.aspx.cs" Inherits="TravelOnline.Management.PreferList" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>优惠券明细</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/jNotify.jquery.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <style type="text/css">
        #explain table {table-layout:fixed;}
        #explain td {white-space:nowrap;overflow:hidden;BORDER-right: #EFEFEF 1px solid;}
        .fixColleft {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; }
        .fix_hidden {Z-INDEX: 120; left:expression(this.offsetParent.scrollLeft-1); POSITION: relative; overflow:hidden; white-space:nowrap;}
        .headline { font-size:16px; background:url(/images/bg_function.png) no-repeat;padding:0 0 10px 45px; margin:20px 5px 0;}
    </style>
</head>
<body id="none">
<uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
<uc2:SortList ID="SortListNew1" runat="server" />
<DIV class="w main">
<form id="form1" runat="server">
        <div class="main">
            <div id="order_title">
            <h2 class="headline"><SPAN class=headstep>优惠券明细</SPAN></h2>
            </div>
            <div class="serch"> 领用人: 
                <asp:TextBox class=text 
                    ID="tb_cname" runat="server" Width="100px" MaxLength="50"></asp:TextBox> 
                &nbsp; 状态: <asp:DropDownList ID="DropDownList1" 
                    runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem Value="0">未使用</asp:ListItem>
                    <asp:ListItem Value="1">已使用</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
            </div>
            <DIV id="inputs" class=hide>
                <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
                <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
            </DIV>
            <div class="long">
                <asp:ScriptManager ID="sm1" runat="server" />
                <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <DIV id=explain class="datagrid2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
                    Width="100%" AllowPaging="True" PageSize="20" onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
                    OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="id" SortDirection="DESC">
	                <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" />
	                <Columns>
                        <asp:TemplateField HeaderText="&lt;input type='checkbox' onclick='chkall(this)' name='chk_all' id='chk_all'&gt;">
                            <ItemTemplate>
                                <input id="CheckBox<%#Container.DataItemIndex+1 %>" type="checkbox" name="CheckBox" value="<%# DataBinder.Eval(Container.DataItem, "Id") %>" />
                            </ItemTemplate>
                            <HeaderStyle Width="3%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="4%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单号">
                            <ItemTemplate></ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="usedate" HeaderText="使用日期" SortExpression="usedate" DataFormatString="{0:yy-MM-dd}">
		                    <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserName" HeaderText="领用人" SortExpression="UserName">
		                    <HeaderStyle Width="10%" />
                        </asp:BoundField>
	                </Columns>
                    <PagerTemplate>
                        <div class="pagination"><span class="pagecounts">
                            第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页</span>&nbsp;<asp:label id="lblRecorCount" runat="server"></asp:label>条记录&nbsp;&nbsp;
                            <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="首页"><img src="/images/icons/pagination_first.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>" ToolTip="上一页"><img src="/images/icons/pagination_prev.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="下一页"><img src="/images/icons/pagination_next.gif" alt="" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"  Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>" ToolTip="尾页"><img src="/images/icons/pagination_last.gif" alt="" /></asp:LinkButton>&nbsp;&nbsp;
                                到<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'  Width="30px" CssClass="simpletextbox"></asp:TextBox>页
                            <asp:Button ID="GoToPageButton" runat="server" Width="40px"  OnClick="GridView_PageTurn" Text="跳转" CssClass="simplebutton" /></div>
                    </PagerTemplate>
	                <PagerStyle BackColor="#F7F7F7" ForeColor="#333333" HorizontalAlign="right" Font-Size="12px" />
	                <HeaderStyle BackColor="#4BA41B" Font-Bold="True" ForeColor="White" />
	                <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </DIV>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView_Refresh_Button" />
                        <asp:AsyncPostBackTrigger ControlID="GridView_Serch_Button" />
                    </Triggers>
                </asp:UpdatePanel>                
            </div>          
        </div>
        <SCRIPT language="javascript" src="/Scripts/NowrapSet.js"></SCRIPT>
    </form>
<DIV class=clr></DIV>
</DIV>
<uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
<script type="text/javascript">
    

</script>
</body>
</html>