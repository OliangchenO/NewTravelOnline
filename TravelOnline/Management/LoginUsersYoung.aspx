<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUsersYoung.aspx.cs" Inherits="TravelOnline.Management.LoginUsersYoung" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>注册用户</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/default/easyui.css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <%--<script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>--%>
    <script type="text/javascript">

	</script>
    <style>
    .uemail {WIDTH: 170px;OVERFLOW: hidden;}
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

    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <DIV class="w main">
        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

<DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>注册用户列表</SPAN></DIV>

 <DIV class="m select">
    <form id="formlogin" runat="server">
    <DIV class=mt>
        <H1></H1><STRONG>注册用户列表</STRONG>
    </DIV>
    <div class="serchbar">
        用户名：<asp:TextBox ID="TB_Name" class="text" runat="server" Width="70"></asp:TextBox>&nbsp;
        <asp:DropDownList ID="TB_UserType" runat="server">
            <asp:ListItem Value="">全部</asp:ListItem>
            <asp:ListItem Value="upload">已上传</asp:ListItem>
            <asp:ListItem Value="unUpload">未上传</asp:ListItem>
        </asp:DropDownList> &nbsp;
        邮件：<asp:TextBox ID="TB_Email" class="text" runat="server" Width="100"></asp:TextBox>&nbsp;
        注册日期：<asp:TextBox ID="TB_Bdate" class="iconDate" runat="server" Width="85"></asp:TextBox>-
        <asp:TextBox ID="TB_Edate" class="iconDate" runat="server" Width="85"></asp:TextBox>
        &nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="javascript:$('#GridView_Serch_Button').click();">查询</a>
    </div>
    <div class="toolbar" style="DISPLAY:none">
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-reload" onclick="javascript:$('#GridView_Refresh_Button').click();">刷新</a>
		&nbsp;&nbsp;
        <a href="javascript:void(0);" class="easyui-linkbutton" plain="true" iconCls="icon-print" onclick="allchks()">打印</a>
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
            OnDataBound="GridView_DataBound" GridLines="Vertical" SortExpression="RegTime" 
            SortDirection="DESC" PageSize="20">
	        <RowStyle BackColor="#F4FFDE" HorizontalAlign="Center" Wrap="True" 
                Height="40px" />
	        <Columns>
                <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName">
		            <HeaderStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="UserEmail" HeaderText="电子邮件" SortExpression="UserEmail">
		            <HeaderStyle Width="20%" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Company" HeaderText="单位名称" SortExpression="Company">
		            <HeaderStyle Width="25%" />
                </asp:BoundField>
                <asp:BoundField DataField="Mobile" HeaderText="手机" SortExpression="Mobile">
		            <HeaderStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="RegTime" HeaderText="注册日期" SortExpression="RegTime" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="LastLoginTime" HeaderText="最后登录" SortExpression="LastLoginTime" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="11%" />
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
<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#TB_Bdate').calendar({ maxDate: '#TB_Edate', btnBar: false });
            $('#TB_Edate').calendar({ minDate: '#TB_Bdate', btnBar: false });
        });

        function ShowInfo(cid) {
            var url = "LoginUserInfo.aspx?UserId=" + cid;
            //alert("aa");
            var dg = new $.dialog({ id: 'No1', page: url, title: '注册用户详细信息', width: 450, height: 550, fixed: true, btnBar: false, skin: 'aero', iconTitle: false, maxBtn: false, cover: true });
            dg.ShowDialog();        
        }
    </script> 
</body>
</html>