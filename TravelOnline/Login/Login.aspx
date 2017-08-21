<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TravelOnline.Login.Login" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出境旅游</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript" src="/js/jquery.cookie.js"></script>
    
</head>
<body>
    <uc1:Header ID="Header1" runat="server" />
    <div class="w main">
   
   <DIV id=entry class=w>
<DIV class=mt>
<H2>用户登录</H2><B></B></DIV>
<DIV style="PADDING-TOP: 30px" class=mc>
<form id=formlogin onsubmit="return false;" method=post>
<DIV class=form>
<div class="item"> 
				<span class="label">邮箱或手机：</span> 
				<div class="fl"> 
					<input type="text" id="loginname" name="loginname" class="text" tabindex="1" value="<%=LoginUserEmail %>" sta="0" maxlength="50"> 
					<label id="loginname_succeed" class="blank invisible"></label> 
					<span class="clr"></span> 
					<label id="loginname_error" class="focus"></label> 
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
                    <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
					</label> 
					<label class="ftx23">&nbsp;看不清？<a href="###" onclick="verc()" class="flk13">换一张</a></label> 
					<span class="clr"></span> 
					<label id="authcode_error" class="null"></label> 
				</div> 
			</div> 


<DIV id=autoentry class=item><SPAN class=label>&nbsp;</SPAN> 
<DIV class=fl><LABEL class=mar><INPUT id=chkRememberUsername class=checkbox 
CHECKED type=checkbox name=chkRememberUsername> 记住用户名</LABEL> <LABEL><INPUT 
id=chkRememberMe class="checkbox invisible" type=checkbox name=chkRememberMe></LABEL> 
</DIV></DIV>
<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=loginsubmit class="btn-img btn-entry" tabIndex=8 value=登录 type=button> 
    <LABEL id=islogin style="padding-left: 30px; display: none;"><DIV class=iloading>正在验证登陆，请稍候...</DIV></LABEL>
</DIV>
</DIV>
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.entry.js"></script>
    </form>
<DIV id=guide>
<H5>还不是青旅商城用户？</H5>
<DIV class=content>现在免费注册成为青旅商城用户，便能立刻享受便宜又放心的环球之旅。</DIV><A 
class="btn-link btn-personal" href="javascript:regist();">注册新用户</A> 
</DIV><SPAN 
class=clr></SPAN></DIV></DIV>
</DIV>
    <uc3:Footer ID="Footer1" runat="server" />


</body>
</html>
