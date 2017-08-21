<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginNow.aspx.cs" Inherits="TravelOnline.Login.LoginNow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="/scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
    <script type="text/javascript">
        if (top.location == location) {
            window.location = "/index.html";
        }
    </script>
    <style type="text/css"> 
    #entry .mc {
	BORDER-BOTTOM: #d1d1d1 0px solid; BORDER-LEFT: #d1d1d1 0px solid; PADDING-BOTTOM: 20px; PADDING-LEFT: 10px; PADDING-RIGHT: 20px; BORDER-TOP: #d1d1d1 0px solid; BORDER-RIGHT: #d1d1d1 0px solid; PADDING-TOP: 40px
    }
    #entry .form {
	    WIDTH: 500px; FLOAT: left; OVERFLOW: hidden;MARGIN-TOP: 15px
    }
    #entry .label {
	TEXT-ALIGN: right; WIDTH: 100px; FONT-SIZE: 14px
}
    </style> 
</head>
<body>
    <div class="w main">
  
<DIV id=entry class=w>
<DIV style="PADDING-TOP: 20px" class=mc>
<form id=formlogin onsubmit="return false;" method=post>
<DIV class=form>
<div class="item"> 
	<span class="label">邮箱或手机：</span> 
	<div class="fl"> 
		<input type="text" id="loginname" name="loginname" class="text" tabindex="1" value="<%=LoginUserEmail %>" sta="0" maxlength="50"> 
		<label id="loginname_succeed" class="blank invisible"></label> 
		<span class="clr"></span> 
		<label id="loginname_error" class="focus"></label>
        <input id="loginflag" name="loginflag" type="hidden" value="<%=loginflag %>"/>
	</div> 
</div> 
<div class="item"> 
	<span class="label">密码：</span> 
	<div class="fl"> 
		<input type="password" id="loginpwd" name="loginpwd" class="text" tabindex="2" sta="0" maxlength="18"> 
		<label id="loginpwd_succeed" class="blank invisible"></label> 
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
		<label class="ftx23"><a href="javascript:void(0)" onclick="verc()" class="flk13">换一换</a></label> 
		<span class="clr"></span> 
		<label id="authcode_error" class="null"></label> 
	</div> 
</div> 
<DIV id=autoentry class=item><SPAN class=label>&nbsp;</SPAN> 
<DIV class=fl><LABEL class=mar><INPUT id=chkRememberUsername class=checkbox 
CHECKED type=checkbox name=chkRememberUsername> 记住用户名</LABEL> <LABEL><INPUT 
id=chkRememberMe class="checkbox invisible" type=checkbox name=chkRememberMe></LABEL> 
</DIV>
</DIV>
<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=loginsubmit class="btn-img btn-entry" tabIndex=8 value=登录 type=button> 
    <span class="label1">&nbsp;</span>
    <INPUT id=Button1 class="btn-img btn-entry <%=hide %>" onclick="orderit()"  value="免登陆下单" type="button" style="margin-left:20px">
    <%--<label style="padding-left: 30px;"><a href="javascript:void(0)" class="flk13" onclick="window.parent.regist();">新用户注册</a></label>--%>
    
</DIV>
<DIV class=item>
    <SPAN class=label>&nbsp;</SPAN> <LABEL id=islogin style="padding-left: 30px; display: none;"><DIV class=iloading>正在验证登陆，请稍候...</DIV></LABEL>
    <label style="padding-left: 30px;"><a href="regnow.aspx?flag=<%=loginflag %>" class="flk13">新用户注册</a></label>
</DIV>
</DIV>
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.entry.now.js"></script>
    </form>
    
<SPAN 
class=clr></SPAN></DIV></DIV>
</DIV>
<script type="text/javascript">
    function orderit() {
        this.location = "/login/NotMemberOrderSubmit.aspx";
    }
</script>
</body>
</html>

