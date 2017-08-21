<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginYoung.aspx.cs" Inherits="TravelOnline.Member.LoginYoung" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>会员登录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <link rel="shortcut icon" href="">
    <link href="/AD/2017Young/css/common.css" rel="stylesheet" type="text/css" />
    <link href="/AD/2017Young/css/headerTop.css" rel="stylesheet" type="text/css" />
    <link href="/AD/2017Young/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/AD/2017Young/css/demo.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/loginreg.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/loginreg.js"></script>
    <script type="text/javascript" src="/newjs/jquery.cookie.js"></script>
</head>
<body>
	<!--页头Begin-->
	<!--Top-->
   <div class="headtop">
      <div class="topposition">
         <a href="http://www.scyts.com/" class="logo fl" target="_blank"><img src="/AD/2017Young/images/logo5A.jpg" alt="上海青旅5A旅行社" /></a>
         <span class="telphone fl">
            <img src="/AD/2017Young/images/tel.gif" alt="4006-777-666" />
         </span>
         <ul class="topNav fl">
            <li><a href="http://www.scyts.com/index.html">首页</a></li>
            <li><a href="http://www.scyts.com/outbound.html">出境游</a></li>
            <li><a href="http://www.scyts.com/inland.html">国内游</a></li>
            <li><a href="http://www.scyts.com/freetour.html">自由行</a></li>
            <li><a href="http://www.scyts.com/cruise.html">邮轮旅游</a></li>
            <li><a href="http://www.scyts.com/visa.html">单办签证</a></li>
         </ul>
      </div>
   </div>
	<!--正文内容Begin-->
	<div class="content clearfix">
        <div class="content-box-1000">
        <img src="/AD/2017Young/images/悦华大酒店.jpg" style="width:1000px; height:400px;" alt="上海悦华大酒店"/>
        <div class="index-nav">
			<div class="nav-column">
				<ul id="changeNav" class="nav-main clearfix">
					<li><a href="/AD/2017Young/index.aspx">竞赛规程</a></li>
					<li class="current"><a href="#">报名登录</a></li>
				</ul>
			</div>
		</div>
        <div style="margin-top:40px;">
		<div class="cartoon fl">
			<img src="/image/member/logoin_cartoon.jpg">
		</div>
		<div class="loginbox fl">
			<h2 class="lg-title">
				会员登录
			</h2>
            <ul class="lg-form email" style="margin-top:20px;">
                <form action="#" id="email_form">
    				<li>
    					登录名
    					<input type="text" class="lg-input" name="UserName" id="UserName" placeholder="手机号/邮箱" value="<%=LoginUser %>"/>
    				</li>
    				<li>
    					密&nbsp;&nbsp;码
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
    					还不是会员？
    					<a href="/member/registerYoung.aspx">立即注册</a>
    				</span>
    			</form>
            </ul>
		</div>
        </div>
       </div>
	</div>
	<!--页尾Begin-->
    <!-- 在线咨询 end-->
   <div class="foot">
      <div class="attention">
         <p>
            <strong>关注更多活动</strong>
         </p>
      </div>
      <div class="scanning clearfix">
         <div class="wrap-box clearfix">
            <a class="wx"></a>
            <div class="ts">
               <h6>上海青旅官方微信</h6>
               <p>
                  <span>扫描二维码</span><br/>
                  <span>了解最新旅游咨询</span>
               </p>
            </div>
         </div>
         <div class="wrap-box clearfix">
            <a class="wb" href="http://weibo.com/scyts"></a>
            <div class="ts">
               <h6>上海青旅官方微信</h6>
               <a href="http://weibo.com/scyts" class="me">关注我们</a>
            </div>
         </div>
      </div>
      <div class="bottomNav">
         <div class="link">
            <a href="http://www.scyts.com/service/aboutus.html" target="_blank" rel="nofollow">关于我们</a>
            <a href="http://www.scyts.com/service/contactus.html" target="_blank" rel="nofollow">联系我们</a>
            <a href="http://www.scyts.com/service/joinus.html" target="_blank" rel="nofollow">人才招聘</a>
            <a href="http://www.scyts.com/OutBound.html" target="_blank" rel="nofollow">出境旅游</a>
            <a href="http://www.scyts.com/InLand.html" target="_blank" rel="nofollow">国内旅游</a>
            <a href="http://www.scyts.com/FreeTour.html" target="_blank" rel="nofollow">自由行</a>
            <a href="http://www.scyts.com/Visa.html" target="_blank" rel="nofollow">单办签证</a>
         </div>
         <p>沪ICP备12040718号  Copyright©1997-2017  上海中国青年旅行社 版权所有</p>
      </div>
   </div>
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
                verc();
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

        function verc() {
            $("#JD_Verification1").click();
        }

        function getCookie(name) {
            var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

            if (arr = document.cookie.match(reg))

                return unescape(arr[2]);
            else
                return null;
        }

        $("#Button1").click(function () {
            if ($("#mobilePhone").val().length < 4) {
                $('.mobile .login-error').html("<i></i>手机号长度必须是11位");
                return false;
            }
            var code = getCookie("CheckCode");
            if ($("#authcode").val().toLocaleLowerCase() != code.toLocaleLowerCase()) {
                $('.login-error').html("<i></i>验证码错误");
                return false;
            }
            time(this);
            var random = Math.random();
            $("#random").val(random);
            var url = "AjaxService.aspx?action=SendSMS&code=" + $("#authcode").val().toLocaleLowerCase() + "&r=" + random;
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