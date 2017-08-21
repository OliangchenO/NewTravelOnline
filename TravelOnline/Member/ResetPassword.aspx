<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="TravelOnline.Member.ResetPassword" %>
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
			<h2 class="fd-title">找回密码</h2>
			<div class="reg-form mobile">
            <form action="#" id="mobile_form">
				<dl class="reg-form-line clearfix">
					<dt>新密码</dt>
					<dd>
						<input type="text" class="reg-input" maxlength="18" name="pwd" id="pwd" placeholder="6-18位字母、数字或符号">
					</dd>
				</dl>
				<dl class="reg-form-line clearfix">
					<dt>确认密码</dt>
					<dd>
						<input type="text" class="reg-input" maxlength="18" name="pwd2" id="pwd2" placeholder="">
					</dd>
				</dl>
				<div class="reg-error"></div>
				<dl class="reg-form-line clearfix">
					<dt>&nbsp;</dt>
					<dd>
						<a href="javascript:;"  class="reg-btn next-step">提交</a>
					</dd>
				</dl>
			</form>
            </div>
		</div>
	</div>
	<!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.next-step').click(function () {
                if ($("#pwd").val() == "") {
                    $('.reg-error').html("<i></i>请填写密码");
                    return false;
                }
                if ($("#pwd2").val() == "") {
                    $('.reg-error').html("<i></i>请再次输入新密码");
                    return false;
                }
                if ($("#pwd").val().length < 6) {
                    $('.reg-error').html("<i></i>密码需为6-18位字母、数字或符号");
                    return false;
                }
                if ($("#pwd").val() != $("#pwd2").val()) {
                    $('.reg-error').html("<i></i>您两次输入的密码不一致");
                    return false;
                }
                
                $('.reg-error').html("");
                var url = "AjaxService.aspx?action=ResetPassword&r=" + Math.random();
                $.post(url, $("#mobile_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        top.location = "/member/passwordsuccess.html";
                    }
                    else {
                        $('.reg-error').html(obj.error);
                    }
                });
            })
        })
	</script>
</body>
</html>