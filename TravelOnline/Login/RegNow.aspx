<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegNow.aspx.cs" Inherits="TravelOnline.Login.RegNow" %>
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
<div id=entry class=w>
<div style="PADDING-TOP: 20px" class=mc>

<FORM id=formpersonal onsubmit="return false;" method=post>
<DIV class=form>
<div class="item"> 
	<span class="label">您的姓名：</span> 
	<div class="fl"> 
		<input type="text" id="username" name="username" class="text" tabindex="1" maxlength="20" /> 
		<label id="username_succeed" class="blank"></label> 
		<span class="clr"></span> 
		<div id="username_error"></div> 
    					
	</div> 
</div>
<div class="item"> 
	<span class="label">电子邮件：</span> 
	<div class="fl"> 
		<input type="text" id="mail" name="mail" class="text" tabindex="1" maxlength="50" /> 
		<label id="mail_succeed" class="blank"></label> 
		<span class="clr"></span> 
		<div id="mail_error"></div> 
    					
	</div> 
</div> 
<div class="item"> 
	<span class="label">手机号码：</span> 
	<div class="fl"> 
		<input type="text" id="mobile" name="mobile" class="text" tabindex="1" maxlength="20" /> 
		<label id="mobile_succeed" class="blank"></label> 
		<span class="clr"></span> 
		<div id="mobile_error"></div> 
    					
	</div> 
</div>
<div id="o-password">
	<div class="item">
		<span class="label">设置密码：</span>
		<div class="fl">
			<input type="password" id="pwd" name="pwd" class="text" tabindex="2" />
			<label id="pwd_succeed" class="blank"></label>
			<span class="clr"></span>
			<label class="hide" id="pwdstrength"><span class="fl">安全程度：</span><b></b></label>
			<label id="pwd_error"></label>
    		<span class="clr"></span>
            <input type="checkbox" class="checkbox" id="viewpwd"><label class="ftx23" for="viewpwd">显示密码字符</label>
		</div>
	</div>
    <span class="clr"></span>
	<div class="item">
		<span class="label">确认密码：</span>
		<div class="fl">
			<input type="password" id="pwd2" name="pwd2" class="text" tabindex="3">
			<label id="pwd2_succeed" class="blank"></label>
			<span class="clr"></span>
			<label id="pwd2_error"></label>
    						
		</div>
	</div>
</div>
<div class="item"> 
	<span class="label">验证码：</span> 
	<div class="fl"> 
		<input type="text" id="authcode" name="authcode" class="text text-1" tabindex="4" autocomplete="off" MaxLength="6" /> 
		<label id="authcode_succeed" class="blank"></label> 
		<label class="img"> 
            <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="VerifyCode.aspx?&uid=<%=ucode %>" alt=""> 
		</label> 
		<label class="ftx23">&nbsp;<a  href="javascript:void(0)" onclick="verc()" class="flk13">换一换</a></label> 
		<span class="clr"></span> 
		<label id="authcode_error"></label> 
    </div> 
</div> 


    <div class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=registsubmit class="btn-img btn-entry" tabIndex=8 value="提交注册" type=button> 
    <LABEL id=islogin style="padding-left: 30px; display: none;"><DIV class=iloading>正在验证登陆，请稍候...</DIV></LABEL>
    <span class="label1">&nbsp;</span>
    <label style="padding-left: 30px;"><a href="loginnow.aspx?flag=<%=loginflag %>" class="flk13">用户登录</a></label>
    </div>
</FORM>

</div></div>

</div>

<script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.regist.now.js"></script>
</body>
</html>


