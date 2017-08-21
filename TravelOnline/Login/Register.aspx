<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TravelOnline.Login.Register" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>注册新用户</title>
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
<H2>注册新用户</H2><B></B> 
 </DIV>
<DIV class=mc>

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
			<input type="checkbox" class="checkbox" id="viewpwd">
			<label class="ftx23" for="viewpwd">显示密码字符</label>
			<span class="clr"></span>
			<label class="hide" id="pwdstrength"><span class="fl">安全程度：</span><b></b></label>
			<label id="pwd_error"></label>
    						
		</div>
	</div>
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


<DIV class=item><SPAN class=label>&nbsp;</SPAN> <INPUT id=registsubmit class="btn-img btn-regist" tabIndex=8 value=同意以下协议，提交 type=button> 
</DIV></DIV>
<DIV id=protocol-con>
<H5>一、本站服务条款的确认和接纳</H5>
<P>本站的各项电子服务的所有权和运作权归本站。本站提供的服务将完全按照其发布的服务条款和操作规则严格执行。用户同意所有服务条款并完成注册程序，才能成为本站的正式用户。用户确认：本协议条款是处理双方权利义务的约定，除非违反国家强制性法律，否则始终有效。在下订单的同时，您也同时承认了您拥有购买这些产品的权利能力和行为能力，并且将您对您在订单中提供的所有信息的真实性负责。</P>
<H5>二、服务简介</H5>
<P>本站运用自己的操作系统通过国际互联网络为用户提供网络服务。同时，用户必须：</P>
<UL>
  <LI>(1)自行配备上网的所需设备，包括个人电脑、调制解调器或其它必备上网装置。 
  <LI>(2)自行负担个人上网所支付的与此服务有关的电话费用、网络费用。 </LI></UL>
<P>基于本站所提供的网络服务的重要性，用户应同意</P>
<UL>
  <LI>(1)提供详尽、准确的个人资料。 
  <LI>(2)不断更新注册资料，符合及时、详尽、准确的要求。 </LI></UL>
<P>本站保证不公开用户的真实姓名、地址、电子邮箱和联系电话等用户信息， 除以下情况外：</P>
<UL>
  <LI>(1)用户授权本站透露这些信息。 
  <LI>(2)相应的法律及程序要求本站提供用户的个人资料。 </LI></UL>
</DIV>
<!--[if !ie]>form end<![endif]-->
</DIV><!--[if !ie]>mc end<![endif]--></DIV>
</FORM>

</div>
    <script type="text/javascript" src="/Scripts/Validate/Validate.js"></script>
    <script type="text/javascript" src="/Scripts/Validate/Validate.regist.js"></script>
    <uc3:Footer ID="Footer1" runat="server" />
</body>
</html>

