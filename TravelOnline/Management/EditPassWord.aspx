<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPassWord.aspx.cs" Inherits="TravelOnline.Management.EditPassWord" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/SortListNew.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<%@ Register src="~/Master/ManageMenu.ascx" tagname="ManageMenu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改密码</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/user.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jNotify.jquery.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
</head>
<body id="none">

    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <uc2:SortList ID="SortListNew1" runat="server" />
    <script type="text/javascript" src="/Scripts/hotwords.js"></script>
    <DIV class="w main">

        <DIV class=left>
            <uc4:ManageMenu ID="ManageMenu1" runat="server" />
        </DIV>

 <div class="right-extra">

<DIV class=crumb><A href="../index.html">首页</A>&nbsp;&gt;&nbsp;<A 
href="/Management/ManageHome.aspx">管理中心首页</A>&nbsp;&gt;&nbsp;<SPAN>修改密码</SPAN></DIV>


 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>修改您的登录密码</STRONG>
</DIV>
</DIV>
    <FORM id=formlogin onsubmit="return false;" method=post>
<DIV id=explain class="m m1">
<DIV class=o-mt></DIV>
<DIV class=mc>
<DIV id=regist class=w>
<DIV class=form>
<DIV class=item><SPAN class=label>旧登录密码：</SPAN> 
<DIV class=fl><INPUT id=loginpwd class=text tabIndex=1 type=password 
name=loginpwd> <LABEL id=loginpwd_succeed 
class="blank invisible"></LABEL><SPAN 
class=clr></SPAN><LABEL id=loginpwd_error></LABEL></DIV></DIV>
<div id="o-password">
	<div class="item">
		<span class="label">设置新密码：</span>
		<div class="fl">
			<input type="password" id="pwd" name="pwd" class="text" tabindex="2" />
			<label id="pwd_succeed" class="blank"></label>
			<input type="checkbox" class="checkbox" id="viewpwd">
			<label class="ftx23" for="viewpwd">显示密码字符</label>
			<span class="clr"></span>
			<label class="hide" id="pwdstrength"><span class="fl">安全程度：</span><b></b></label>
			<label id="pwd_error"></label>
    						
		</div>
	</div>
	<div class="item">
		<span class="label">再次录入新密码：</span>
		<div class="fl">
			<input type="password" id="pwd2" name="pwd2" class="text" tabindex="3">
			<label id="pwd2_succeed" class="blank"></label>
			<span class="clr"></span>
			<label id="pwd2_error"></label>
    						
		</div>
	</div>
</div>

<div class="item" id="o-authcode"> 
	<span class="label">验证码：</span> 
	<div class="fl"> 
		<input type="text" id="authcode" name="authcode" class="text text-1" tabindex="3" sta="0" maxlength="6"> 
		<label id="authcode_succeed" class="blank"></label> 
		<label class="img"> 
        <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="/login/VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
		</label> 
		<label class="ftx23">&nbsp;看不清？<a href="###" onclick="verc()" class="flk13">换一张</a></label> 
		<span class="clr"></span> 
		<label id="authcode_error" class="null"></label> 
	</div> 
</div> 
<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=loginsubmit class="btn-img btn-entry" tabIndex=8 value=修改密码 type=button>  
</DIV></DIV></DIV>
</DIV></DIV>
    </FORM>

</DIV>

<DIV class=clr></DIV></DIV>
    <SPAN class=clr></SPAN>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.ManagePwd.js"></script>
    <script type="text/javascript" src="/Scripts/base.menu.js"></script>

</body>
</html>

