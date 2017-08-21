<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="TravelOnline.Member.RegisterSuccess" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>注册成功 - 上海青旅官网</title>
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
			<a href="/member/login.html">登录</a>
			<span>|</span>
			<a href="/help.html">帮助中心</a>
		</div>
	</h1>
	<!--正文内容Begin-->
	<div class="find-content">
		<div class="find-box">
			<h2 class="fd-title">注册成功</h2>
			<div class="pass-wrap">
				<div class="success">
					<i></i>
					<span>恭喜您！注册成功。</span><br/>
					<p>您的登录用户名为：<%=UserName %></p>
					<p>为了方便您更好的享受旅游服务，建议您完善个人资料。<a href="/users/userhome.aspx">进入会员中心</a></p>
					<p><em id="time" href="/index.html">10</em>秒后自动返回首页</p>
				</div>
			</div>
		</div>
	</div>
	<script type="text/javascript">
	    function delayURL(url) {
	        var delay = document.getElementById("time").innerHTML;
	        if (delay > 0) {
	            delay--;
	            document.getElementById("time").innerHTML = delay
	        } else {
	            window.top.location.href = url
	        }
	        setTimeout("delayURL('" + url + "')", 1000)
	    }

	    delayURL(<%=Url %>);
	</script>
	<!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
</body>
</html>
