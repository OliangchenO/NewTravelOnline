<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="TravelOnline.Users.UserHome" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<%@ Register src="~/Master/UserCenterMenu.ascx" tagname="UserCenterMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员中心</title>
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
        .user_icon{height:16px;}
        .row {
width: 800px; height:30px;
}
    .user_info_tit {
        float: left;
        width: 68px;
        color: #666;
        }
    .user_info_con {
        float: left;
        }
em {
color: #45991A;
}
.info_tips {
color: #999;
}

    </style>
</head>
<body id="none">

    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <div class="w main">

        <div class=left>
            <uc4:UserCenterMenu ID="UserCenterMenu1" runat="server" />
        </div>

 <div class="right-extra">

<div class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/users/UserHome.aspx">会员中心</A></div>

<form id="formlogin" runat="server">
    <div style="font-size:14px;font-weight:bold;padding:10px 10px">您好，尊敬的会员，欢迎来到上海青旅！</div>
    <div style="padding:10px 10px">
        <div class="row">
	        <span class="user_info_tit">您的积分：</span>
	        <div class="user_info_con">
		        <em><%=Allintegral%></em> 积分 
		        <span class="info_tips <%=hyhide %>">(加入积分会员后参加旅游即可获得积分， <a href="#" target="_blank">积分规则</a>)</span>
	        </div>
        </div>
        <div class="row">
	        <span class="user_info_tit">您的等级：</span>
	        <div class="user_info_con">
		        <span class="user_icon_wrap"><img class="user_icon" src="/images/icon/chinaz86.png"> <em><%=hyname %></em>
		        </span>
		        <span class="info_tips <%=hyhide %>">
                您还不是积分俱乐部会员， <a href="/login/joinmember.aspx">请点这里加入积分会员俱乐部</a>
		        <%--(<span id="need_consume_more_id" style="">再消费<em>0</em>元即可升级</span> )--%>
		        </span>  
	        </div>
        </div>
    </div>


    <div style="margin-top:10px;height:30px;color:#398510;font-size:14px;padding:5px 15px;">
        <STRONG>最近的订单</STRONG>
    </div>
    <div id=explain class="m select datagrid">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="5" ForeColor="#333333" Width="822px" GridLines="Horizontal" SortExpression="AutoId" 
            SortDirection="DESC" PageSize="10">
	        <RowStyle BackColor="#ffffff" HorizontalAlign="Center" Wrap="True" 
                Height="50px" />
	        <Columns>
                <asp:BoundField DataField="AutoId" HeaderText="订单号" SortExpression="AutoId">
		            <HeaderStyle Width="6%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                    </ItemTemplate>
                    <HeaderStyle Width="6%" />
                </asp:TemplateField>
                <asp:BoundField DataField="OrderTime" HeaderText="预订日期" SortExpression="OrderTime" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="LineName" HeaderText="旅游线路" SortExpression="LineName">
		            <HeaderStyle Width="30%" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="BeginDate" HeaderText="出发日期" SortExpression="BeginDate" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="8%" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderNums" HeaderText="人数" SortExpression="OrderNums">
		            <HeaderStyle Width="5%" />
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="金额" SortExpression="Price">
		            <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Pay" HeaderText="已支付" SortExpression="Price">
		            <HeaderStyle Width="8%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
	        </Columns>
	        <HeaderStyle BackColor="#4BA41B" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>

    <div style="margin-top:20px;height:30px;color:#398510;font-size:14px;padding:5px 15px;">
        <STRONG>最近收藏的线路</STRONG>
    </div>
    <div id=explain class="m select datagrid">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" 
            CellPadding="5" Width="822px" GridLines="Horizontal" >
	        <RowStyle BackColor="#ffffff" HorizontalAlign="Center" Wrap="True" 
                Height="50px" />
	        <Columns>
                <asp:BoundField DataField="linename" HeaderText="旅游线路" SortExpression="linename">
		            <HeaderStyle Width="70%" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="price" HeaderText="价格" SortExpression="price">
		            <HeaderStyle Width="10%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="inputdate" HeaderText="收藏日期" SortExpression="inputdate" DataFormatString="{0:yy-MM-dd}">
		            <HeaderStyle Width="10%" />
                </asp:BoundField>
	        </Columns>
	        <HeaderStyle BackColor="#4BA41B" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</form>

</div>

<div class=clr></div></div>
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


