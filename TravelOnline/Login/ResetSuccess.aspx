<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetSuccess.aspx.cs" Inherits="TravelOnline.FindPassword.ResetSuccess" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>密码重设成功</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages1.js"></script>
    
</head>
<body>
    <uc1:Header ID="Header1" runat="server" />
    <div class="w main">
    
   <DIV id=forget class=w>
<DIV class=mt>
<H2>密码重设成功</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
    <DIV class=success>
        <DIV class=fa><IMG src="/Images/ok.gif" width=80px></DIV>
        <DIV class=fl>
            <SPAN style="LINE-HEIGHT: 25px; color: #009900; font-size: 24px; font-weight: bold;">密码重设成功！</SPAN><BR><BR>
            <SPAN class=infos>您现在可以用新的密码登陆！</SPAN>
            
        </DIV>
        <A class="btn-link btn-personal" href="javascript:gotolink();">返回首页</A> 
    </DIV>
<SPAN class=clr></SPAN></DIV></DIV>
</DIV>
    <uc3:Footer ID="Footer1" runat="server" />
<%--    <script type="text/javascript" src="/Scripts/login.Validate.js"></script>
    <script type="text/javascript" src="/Scripts/login.Validate.entry.js"></script>--%>
</body>
</html>



