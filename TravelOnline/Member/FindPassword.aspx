<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindPassword.aspx.cs" Inherits="TravelOnline.Member.FindPassword" %>
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
					<dt>手机号</dt>
					<dd>
						<input type="text" class="reg-input" name="mobilePhone" id="mobilePhone" placeholder="">
					</dd>
				</dl>
				<dl class="reg-form-line clearfix">
					<dt>短信验证码</dt>
					<dd>
						<input type="text" class="reg-input" maxlength="4" name="MPassWord" id="MPassWord" placeholder="请查收手机短信，并填写验证码">
					</dd>
					<INPUT id="Button1" name="Button1" value="免费获取验证码" type=button style="background-color: white;cursor:pointer;margin-left:10px;margin-top:10px" />
				</dl>
				<div class="reg-error"></div>
					<%--<i></i>
					请填写手机号——手机号码不存在，请重新输入——请填写验证码——验证码错误，请重新填写——短信验证码已失效，请重新获取--%>
				
				<dl class="reg-form-line clearfix">
					<dt>&nbsp;</dt>
					<dd>
						<a href="javascript:;" class="reg-btn next-step">下一步</a>
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
                if ($("#mobilePhone").val() == "") {
                    $('.reg-error').html("<i></i>请填写手机号");
                    return false;
                }
                if ($("#mobilePhone").val().length < 4) {
                    $('.reg-error').html("<i></i>手机号长度必须是11位");
                    return false;
                }
                if ($("#MPassWord").val() == "") {
                    $('.reg-error').html("<i></i>请填写验证码");
                    return false;
                }
                if ($("#MPassWord").val().length < 4) {
                    $('.reg-error').html("<i></i>请正确填写验证码");
                    return false;
                }
                //$(this).setAttribute("disabled", true);
                $('.reg-error').html("");
                var url = "AjaxService.aspx?action=MobileLogin&r=" + Math.random();
                $.post(url, $("#mobile_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        top.location = "/member/resetpassword.html";
                    }
                    else {
                        $('.reg-error').html(obj.error);
                    }
                });
            })
        })

        var wait = 60;
        function time(o) {
            if (wait == 0) {
                o.removeAttribute("disabled");
                o.value = "免费获取验证码";
                wait = 60;
            } else {
                o.setAttribute("disabled", true);
                o.value = "重新发送(" + wait + ")";
                wait--;
                setTimeout(function () {
                    time(o)
                },
		1000)
            }
        }

        $("#Button1").click(function () {
            if ($("#mobilePhone").val().length < 11) {
                $('.reg-error').html("<i></i>手机号长度必须是11位");
                return false;
            }
            time(this);
            var url = "AjaxService.aspx?action=SendSMS&r=" + Math.random();
            $.post(url, $("#mobile_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    $('.reg-error').html(obj.success);
                }
                else {
                    $('.reg-error').html(obj.error);
                }
            });

        });
	</script>
</body>
</html>