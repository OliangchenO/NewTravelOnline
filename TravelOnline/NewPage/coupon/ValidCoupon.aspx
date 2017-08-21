<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidCoupon.aspx.cs" Inherits="TravelOnline.NewPage.coupon.ValidCoupon" %>

<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心 - 我的优惠券</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <style>
    #tip {position:absolute;display:none;}
    #tip s {position:absolute;top:20px;left:-21px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #4BA41B transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
    #tip s i{position:absolute;top:-10px;left:-8px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #fff transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
    #tip .t_box {position:relative;background-color:#CCC;filter:alpha(opacity=50);-moz-opacity:0.5;bottom:-3px;right:-3px;}
    #tip .t_box div{width:400px;position:relative;background-color:#FFF;border:1px solid #4BA41B;padding:5px;top:-3px;left:-3px;}
    a.tip  {
	text-decoration:none;
	outline:none;
	color:#159ce9;
    }
    a.tip :visited {
	    text-decoration:none;
	    outline:none;
	    color:#159ce9;
    }
    a.tip:hover {
	    COLOR: #159ce9; TEXT-DECORATION: underline
    }
    #logul {LINE-HEIGHT: 25px;}
    #logul li {PADDING-LEFT: 5px;PADDING-RIGHT: 5px;BORDER-BOTTOM: #f3e7c7 1px solid;}
    #logul .logtit{LINE-HEIGHT: 20px;background-color:#F7F7F7;}
    </style>
</head>
<body id="none">
 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>我的优惠券查询</STRONG>
    </DIV>
    <div class="toolbar">&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" Width="120">
            <asp:ListItem Value="1">未使用</asp:ListItem>
            <asp:ListItem Value="2">已使用</asp:ListItem>
            <asp:ListItem Value="3">已过期</asp:ListItem>
        </asp:DropDownList>
        &nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
        &nbsp;&nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		<DIV id="inputs" style="DISPLAY:none">
            <input id="TB_DeptId" type="hidden" value="0"/>
            <asp:Button ID="GridView_Refresh_Button" runat="server" onclick="GridView_Refresh" Text="Button" />
            <asp:Button ID="GridView_Serch_Button" runat="server" onclick="GridView_Serch" Text="Button" />
        </DIV>
	</div>
    <DIV id=explain class="datagrid">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="5" ForeColor="#333333" AllowSorting="True"  OnSorting="GridView_Sorting"
            Width="822px" AllowPaging="True" 
            onpageindexchanging="GridView_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" 
            OnDataBound="GridView_DataBound" GridLines="Horizontal" SortExpression="begindate" 
            SortDirection="ASC" PageSize="10">
	        <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" 
                Height="40px" />
	        <Columns>
                <asp:BoundField DataField="uno" HeaderText="券密码" SortExpression="uno" >
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:BoundField DataField="par" HeaderText="面值" SortExpression="par">
		            <HeaderStyle Width="6%" />
                </asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="需消费金额" SortExpression="amount">
		            <HeaderStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="memo" HeaderText="优惠券描述" SortExpression="memo">
		            <HeaderStyle Width="30%" />
                    <ItemStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="有效期">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="20%" />
                    <ItemStyle HorizontalAlign="left" />
                </asp:TemplateField>
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
    <div id="tip"><div class="t_box"><div><s><i></i></s><span id=olog></span></div></div></div>
    </form>
</DIV>  
</body>
</html>
