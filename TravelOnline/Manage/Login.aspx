<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TravelOnline.Manage.Login" %>
<%@ Register src="~/Master/ManagerHeader.ascx" tagname="ManagerHeader" tagprefix="uc1" %>
<%@ Register src="~/Master/ManagerFooter.ascx" tagname="ManagerFooter" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理登陆</title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages1.js"></script>
    <style>
        #entry .label {WIDTH: 350px;}
        #entry .form {WIDTH: 900px;}
    </style>
</head>
<body>
    <uc1:ManagerHeader ID="ManagerHeader1" runat="server" />
    <div class="w main">

    
   <DIV id=entry class=w>
<DIV class=mt>
<H2>后台管理登录</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
<form id=formlogin onsubmit="return false;" method=post>
<DIV class=form>
<div class="item">
	<span class="label">用户名：</span>
	<div class="fl">
		<input type="text" id="managename" name="managename" class="text" tabindex="1" value="<%=ManagerLoginName %>"/>
		<label id="managename_succeed" class="blank"></label>
		<span class="clr"></span>
		<div id="managename_error"></div>
                        
	</div>
</div>

<div class="item"> 
				<span class="label">密码：</span> 
				<div class="fl"> 
					<input type="password" id="loginpwd" name="loginpwd" class="text" tabindex="2" sta="0" maxlength="18"> 
					<label id="loginpwd_succeed" class="blank invisible"></label> 
					<label><a href="retrievepassword.aspx" class="flk13">忘记密码?</a></label> 
					<span class="clr"></span> 
					<label id="loginpwd_error" class="null"></label> 
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

<%--<DIV id=o-authcode class=item><SPAN class=label>验证码：</SPAN>
<DIV class=fl><INPUT type="text" id=authcode class="text text-1" tabIndex=6 name=authcode> 
<LABEL id=authcode_succeed 
class="blank"></LABEL><LABEL class=img><IMG style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" 
id=JD_Verification1 
onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" 
alt="" src="/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" > 
</LABEL> <LABEL class=ftx23>&nbsp;<A class=flk13 onclick=verc() 
href="###">换一换</A></LABEL><SPAN class=clr></SPAN><LABEL 
id=authcode_error></LABEL></DIV></DIV>--%>
<DIV id=autoentry class=item><SPAN class=label>&nbsp;</SPAN> 
<DIV class=fl><LABEL class=mar><INPUT id=chkRememberUsername class=checkbox 
CHECKED type=checkbox name=chkRememberUsername> 记住用户名</LABEL> <LABEL><INPUT 
id=chkRememberMe class="checkbox invisible" type=checkbox name=chkRememberMe></LABEL> 
</DIV></DIV>
<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=loginsubmit class="btn-img btn-entry" tabIndex=8 value=登录 type=button> 
    <LABEL id=islogin style="padding-left: 30px; display: none;"><DIV class=iloading>正在验证登陆，请稍候...</DIV></LABEL>
</DIV>
</DIV>
    </form>
<%--    <DIV id=guide>
<H5>还不是青旅商城用户？</H5>
<DIV class=content>现在免费注册成为青旅商城用户，便能立刻享受便宜又放心的环球之旅。</DIV><A 
class="btn-link btn-personal" href="javascript:regist();">注册新用户</A> 
</DIV>--%>
<SPAN class=clr></SPAN></DIV></DIV>
</DIV>
    <uc3:ManagerFooter ID="ManagerFooter1" runat="server" />
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Manage.Validate.entry.js"></script>
</body>
</html>

