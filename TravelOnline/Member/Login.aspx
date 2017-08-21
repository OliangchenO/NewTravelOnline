<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TravelOnline.Member.Login" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>会员登录 - 上海青旅官网</title>
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
	<h1 class="login-logo">
		<a href="/index.html">上海青旅</a>
	</h1>
	<!--正文内容Begin-->
	<div class="lg-content clearfix">
		<div class="cartoon fl">
			<img src="/image/member/logoin_cartoon.jpg">
		</div>
		<div class="loginbox fl">
			<h2 class="lg-title">
				会员登录
			</h2>
			<div class="lg-select" id="js_login_select">
				<span class="index-select mrr cur">普通登录</span>
				<span class="index-select">手机动态密码登录</span>
			</div>
            <ul class="lg-form email">
            <form action="#" id="email_form">
				<li>
					登录名
					<input type="text" class="lg-input" name="UserName" id="UserName" placeholder="手机号/邮箱" value="<%=LoginUser %>"/>
				</li>
				<li>
					密&nbsp;&nbsp;&nbsp;&nbsp;码
					<input type="password" class="lg-input" maxlength="18" name="PassWord" id="PassWord" placeholder=""/>
				</li>
				<li class="input-error-wrap">
					<div class="login-error"></div>
				</li>
				<li class="wjma">
					<label>
						<input type="checkbox" name="chkAutoLogin" checked="checked">
						记住用户名
					</label>
					<a href="findpassword.aspx">忘记密码？</a>
				</li>
				<li class="lg-btn">
					<a class="login" href="javascript:;">登&nbsp;&nbsp;&nbsp;&nbsp;录</a>
				</li>
				<span class="mumber">
					还不是上海青旅会员？
					<a href="/member/register.html">立即注册</a>
				</span>
			</form>
            </ul>
            <ul class="lg-form mobile" style="display:none;">
			<form action="#" id="mobile_form">
				<li>
					手机号
					<input type="text" class="lg-input" name="mobilePhone" id="mobilePhone"  value="<%=LoginMobile %>"/>
				</li>
				<li class="action-psw">
					密&nbsp;&nbsp;&nbsp;&nbsp;码
					<input type="password" class="lg-input" maxlength="4" name="MPassWord" id="MPassWord" placeholder="请输入动态密码" style="width:124px;"/>
					<INPUT id="Button1" name="Button1" value="发送动态密码" type=button style="background-color: white;cursor:pointer;margin-left:10px" />
				</li>
				<li class="input-error-wrap">
					<div class="login-error"></div>
				</li>
                <li class="wjma">
					<label>
						<input type="checkbox" name="chkAMobileLogin" checked="checked">
						记住手机号
					</label>
				</li>
				<li class="lg-btn">
					<a class="login" href="javascript:;">登&nbsp;&nbsp;&nbsp;&nbsp;录</a>
				</li>
                <span class="mumber">
					还不是上海青旅会员？
					<a href="/member/register.html">立即注册</a>
				</span>
			</form>
			</ul>
            <%--<dl class="other-login">
				<dt>使用合作网站账号登录</dt>
				<dd>
					<a href="javascript:;" class="qq" target="_blank">QQ</a>
					<a href="javascript:;" class="sina" target="_blank">新浪微博</a>
					<a href="javascript:;" class="baidu" target="_blank">百度</a>
					<a href="javascript:;" class="pay" target="_blank">支付宝</a>
				</dd>
			</dl>--%>
		</div>
	</div>
	<!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.email .login').click(function () {
                if ($("#UserName").val() == "") {
                    $('.email .login-error').html("<i></i>请填写登录名");
                    return false;
                }
                if ($("#PassWord").val() == "") {
                    $('.email .login-error').html("<i></i>请填写密码");
                    return false;
                }
                $('.email .login-error').html("正在登录，请稍候...");
                var url = "AjaxService.aspx?action=EmailLogin&r=" + Math.random();
                $.post(url, $("#email_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        top.location = obj.success;
                    }
                    else {
                        $('.email .login-error').html(obj.error);
                    }
                });
            })

            $('.mobile .login').click(function () {
                if ($("#mobilePhone").val() == "") {
                    $('.mobile .login-error').html("<i></i>请填写手机号");
                    return false;
                }
                if ($("#mobilePhone").val().length < 4) {
                    $('.mobile .login-error').html("<i></i>手机号长度必须是11位");
                    return false;
                }
                if ($("#MPassWord").val() == "") {
                    $('.mobile .login-error').html("<i></i>请输入动态密码");
                    return false;
                }
                if ($("#MPassWord").val().length < 4) {
                    $('.mobile .login-error').html("<i></i>请正确填写动态密码");
                    return false;
                }
                $('.mobile .login-error').html("正在登录，请稍候...");
                var url = "AjaxService.aspx?action=MobileLogin&r=" + Math.random();
                $.post(url, $("#mobile_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        $('.mobile .login-error').html("");
                        top.location = obj.success;
                    }
                    else {
                        $('.mobile .login-error').html(obj.error);
                    }
                });
            })
        })

        var wait = 60;
        function time(o) {
            if (wait == 0) {
                o.removeAttribute("disabled");
                o.value = "发送动态密码";
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
            if ($("#mobilePhone").val().length < 4) {
                $('.mobile .login-error').html("<i></i>手机号长度必须是11位");
                return false;
            }
            time(this);
            var url = "AjaxService.aspx?action=SendSMS&r=" + Math.random();
            $.post(url, $("#mobile_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    $('.mobile .login-error').html(obj.success);
                }
                else {
                    $('.mobile .login-error').html(obj.error);
                }
            });

        });
	</script>
</body>
</html>