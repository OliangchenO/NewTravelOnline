<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotMember.aspx.cs" Inherits="TravelOnline.Users.notmemberorder" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>非会员订单 - 上海青旅官网</title>
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
    <script type="text/javascript" src="/newjs/jquery.validate.min.js"></script>
</head>
<body>
	<!--页头Begin-->
	<h1 class="reg-top">
		<a href="/index.html" class="logo">上海青旅</a>
		<div class="bk-nav">
            <% if (Convert.ToString(Session["Online_UserId"]).Length == 0){%>
            
            <a href="/member/login.html">请登陆</a><span>|</span><a href="/member/register.html">免费注册</a>
            
            <%}else{ %>
            您好， <%= Convert.ToString(Session["Online_UserName"])%> <span>|</span><a href="/login/logout.aspx">退出</a>
            <%} %>
			<span>|</span>
			<a href="javascript:void(0)">帮助中心</a>
		</div>
	</h1>
	<!--正文内容Begin-->
	<div class="reg-content clearfix">
		<div class="regbox fl">
			<h2 class="reg-title">非会员订单</h2>
            <form action="#" id="submit_form">
			<div class="reg-form">
				<dl class="reg-form-line clearfix">
					<dt>手机号</dt>
					<dd>
						<input type="text" class="reg-input" maxlength="11" name="mobilePhone" id="mobilePhone" placeholder="请填写手机号码">
					</dd>
					<b class="pass mobilePhone"></b>
				</dl>
				<div class="reg-error mobilePhone_e"></div>
                <dl class="reg-form-line clearfix">
                    <dt>验证码</dt>
                    <dd>
                        <input type="text" id="authcode" name="authcode" class="reg-input" tabindex="4" autocomplete="off" MaxLength="6" placeholder="请填写验证码"/>
                    </dd>
                    <label id="authcode_succeed" class="blank"></label> 
		            <label class="img"> 
                        <img id="JD_Verification1" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
		            </label> 
		            <label class="ftx23">&nbsp;看不清？<a  href="javascript:void(0)" onclick="verc()" class="flk13">换一张</a></label> 
		            <span class="clr"></span> 
		            <label id="authcode_error"></label> 
                </dl>
                <div class="reg-error authcode_e"></div>
                <dl class="reg-form-line clearfix">
                    <dt>短信验证码</dt>
                    <dd>
                        <input type="text" class="reg-input" maxlength="6" name="Phoneyzm" id="Phoneyzm" placeholder="请查收手机短信，并填写短信验证码">
                    </dd>
                    <%--<span><a href="javascript:;">免费获取验证码</a></span>--%>
                    <INPUT id="Button1" name="Button1" value="免费获取验证码" type=button style="background-color: white;cursor:pointer;margin-left:10px;margin-top:10px" />
                </dl>
                <div class="reg-error Phoneyzm_e"></div>
				<dl class="reg-form-line clearfix">
					<dt>&nbsp;</dt>
					<dd>
                        <INPUT type=button class="reg-btn" value="提交" />
					</dd>
				</dl>
			</div>
		    </form>
        </div>
		<div class="reg-catoon fl">
			<img src="/image/member/logoin_cartoon.jpg">
		</div>
	</div>
	<!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".reg-form-line b").hide();
            var form = $('#submit_form');
            form.validate({
                doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
                focusInvalid: false,
                rules: {
                    mobilePhone: {
                        minlength: 11,
                        required: true,
                        digits: true
                    },
                    Phoneyzm: {
                        minlength: 4,
                        required: true,
                        digits: true
                    },
                },
                messages: {
                    mobilePhone: {
                        required: "<i></i>请输手机号码",
                        minlength: "<i></i>请填写有效的11位手机号码",
                        digits: "<i></i>请正确填写手机号"
                    },
                    Phoneyzm: {
                        required: "<i></i>请填写您收到的短信验证码",
                        minlength: "<i></i>短信验证码为4位数字",
                        digits: "<i></i>短信验证码为4位数字"
                    }
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    $("." + element.attr("id") + "_e").html(error.html());
                    $("." + element.attr("id")).hide();
                },
                success: function (label, element) {
                    $("." + element.attr("id")).show();
                }

            });

            $(".reg-btn").click(function () {
                if (form.valid() == false) {
                    return false;
                }
                $(this).val("提交中，请稍候...");
                $(this).attr("disabled", true);
                var url = "AjaxService.aspx?action=validPhoneyzm&flag=ordersearch&r=" + Math.random();
                $.post(url, $("#submit_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        top.location = "NotMemberOrder.aspx?mobilePhone=" + $("#mobilePhone").val();
                    }
                    else {
                        $(".reg-btn").val("提交");
                        $(".reg-btn").attr("disabled", false);
                        if (obj.error) {
                            $('.Phoneyzm_e').html(obj.error);
                            return false;
                        }
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
            if ($("#mobilePhone").val().length < 11) {
                $('.mobilePhone_e').html("<i></i>手机号长度必须是11位");
                return false;
            }
            if ($("#authcode").val().length < 4) {
                $('.authcode_e').html("<i></i>验证码长度是4位");
                return false;
            }
            var code = getCookie("CheckCode");
            if ($("#authcode").val().toLocaleLowerCase() != code.toLocaleLowerCase()) {
                $('.authcode_e').html("<i></i>验证码错误");
                return false;
            }
            time(this);
            var url = "AjaxService.aspx?action=SendNotMemSMS&flag=ordersearch&r=" + Math.random();
            $.post(url, $("#submit_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    $('.Phoneyzm_e').html(obj.success);
                }
                else {
                    $('.Phoneyzm_e').html(obj.error);
                }
            });

        });
	</script>
</body>
</html>