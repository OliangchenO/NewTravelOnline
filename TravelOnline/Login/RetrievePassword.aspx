<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetrievePassword.aspx.cs" Inherits="TravelOnline.Login.RetrievePassword" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>找回密码</title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/login.base.js"></script>
</head>
<body>
    <uc1:Header ID="Header1" runat="server" />
    <div class="w main">
        
    
   <FORM id=formpersonal onsubmit="return false;" method=post>
   <DIV id=regist class=w>
<DIV class=mt>
<H2>找回密码</H2><B></B> 
 </DIV>
<DIV class=mc>

<DIV class=form>

<div class="item"> 
	<span class="label">用户名(邮箱)：</span> 
	<div class="fl"> 
		<input type="text" id="mail" name="mail" class="text" tabindex="1" maxlength="50" /> 
		<label id="mail_succeed" class="blank"></label> 
		<span class="clr"></span> 
		<div id="mail_error"></div> 
    					
	</div> 
</div> 
<%--<DIV class=item><SPAN class=label>推荐人用户名：</SPAN> 
<DIV class=fl><INPUT id=referrer class=text tabIndex=5 value=可不填 name=referrer> 
<LABEL id=referrer_succeed class="blank invisible"></LABEL><SPAN 
class=clr></SPAN><LABEL id=referrer_error></LABEL></DIV></DIV>--%>
<div class="item"> 
	<span class="label">验证码：</span> 
	<div class="fl"> 
		<input type="text" id="authcode" name="authcode" class="text text-1" tabindex="4" autocomplete="off" MaxLength="6" /> 
		<label id="authcode_succeed" class="blank"></label> 
		<label class="img"> 
            <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
		</label> 
		<label class="ftx23">&nbsp;看不清？<a  href="javascript:void(0)" onclick="verc()" class="flk13">换一张</a></label> 
		<span class="clr"></span> 
		<label id="authcode_error"></label> 
    </div> 
</div> 

<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=registsubmit class="btn-img btn-regist" tabIndex=8 value=发送密码找回邮件 type=button> 
</DIV></DIV>
<!--[if !ie]>form end<![endif]-->
</DIV><!--[if !ie]>mc end<![endif]--></DIV>
</FORM>

</div>
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.findpwd.js"></script>
    <uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


