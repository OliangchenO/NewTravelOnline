<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordSuccess.aspx.cs" Inherits="TravelOnline.Member.PasswordSuccess" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>找回密码 - 上海青旅官网</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <link rel="shortcut icon" href="">
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/loginreg.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/loginreg.js"></script>
    <script type="text/javascript" src="/newjs/jquery.cookie.js"></script>
</head>
<body>
	<!--页头Begin-->
	<h1 class="reg-top">
		<a href="/index.html" class="logo">上海青旅</a>
		<div class="bk-nav">
			<a href="/member/register.html">注册</a>
			<span>|</span>
			<a href="/member/login.html">登录</a>
			<span>|</span>
			<a href="/help.html">帮助中心</a>
		</div>
	</h1>
	<!--正文内容Begin-->
	<div class="find-content">
		<div class="find-box">
			<h2 class="fd-title">重置密码成功</h2>
			<div class="pass-wrap">
				<div class="success">
					<i></i>
					<span>恭喜您！密码重置成功。</span>
					<a href="/member/login.html">立即登录>></a>
				</div>
				<div class="other-line">
					<span>您可能感兴趣的：</span>
					<a href="javascript:;">更多特价产品</a>
					<a href="javascript:;">出境旅游</a>
					<a href="javascript:;">海洋量子号</a>
				</div>
			</div>
		</div>
	</div>
	<!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
</body>
</html>
