<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupon.aspx.cs" Inherits="TravelOnline.WeChat.coupon" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>上海中国青年旅行社</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0" name="viewport">
<meta content="" name="description">
<meta content="" name="author">
<meta name="MobileOptimized" content="320">
<!-- Global styles START -->          
<link href="/assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
<link href="/assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css">
<!-- Global styles END -->
<!-- Theme styles START -->
<link href="/assets/css/style-metronic.css" rel="stylesheet" type="text/css">
<link href="/assets/css/style.css" rel="stylesheet" type="text/css">
<link href="/assets/css/style-responsive.css" rel="stylesheet" type="text/css"> 
<link href="/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css" rel="stylesheet" type="text/css">
<link href="/assets/plugins/iCheck/skins/square/grey.css" rel="stylesheet" type="text/css">
<!-- Theme styles END -->
<link href="/app_css/custom.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
</head>
<body id="mainbody">
<!-- BEGIN HEADER -->
<div id="header" class="pre-header" style="position: fixed; top: 0px; left: 0px;width:101%">
    <div class="container">
        <div class="row">
            <a class="icon_back" href="javascript:;" onclick="javascript:history.go(-1)"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename">上海中国青年旅行社</div>
            <a class="icon_home" href="/app/main"><i class="fa fa-home"></i></a>
        </div>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="CouponId" type="hidden" value="<%=CouponId %>" />
    </div>
</div>
<!-- END HEADER -->
<div id="main_view" style="margin-top: 45px;margin-bottom: 50px;">
    <div class="sub_view" id="login_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>登录</h3>
                    <form id="loginform" onsubmit="return false;" method="post">
                    <input required id="loginname" type="text" name="loginname" class="form-control ordertext" placeholder="手机号码或邮件地址" maxlength="50">
                    <input required id="loginpwd" type="password" name="loginpwd" class="form-control ordertext" placeholder="登录密码（6-20位字符）" maxlength="20">
                    <input type="text" id="authcode" name="authcode" class="form-control1 ordertext" placeholder="验证码"maxlength="6" style="width:100px;"> 
					<img id="Verification" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="../../login/VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
					<label class="ftx23">&nbsp;<a href="javascript:;" onclick="verc()">看不清？换一张</a></label>
                    </form>
                </div>
                <div class="recommend_txt" style="margin:20px 20px;text-align:center">
                    
                </div>
                <div class="row" style="margin:20px 20px;text-align:center">
                    <div class="col-xs-5"><a class="btn yellow" href="javascript:;" id="login">登录</a></div>
                    <div class="col-xs-7"><a class="btn yellow" href="javascript:;" id="regnew">注册新用户</a></div>
                </div>
            </div>
        </div>
    </div>
    <div class="sub_view" style="display:none;" id="reg_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>注册新用户</h3>
                    <form id="regform" onsubmit="return false;" method="post">
                    <input id="regname" type="text" name="regname" class="form-control ordertext" placeholder="您的姓名" maxlength="10">
                    <input id="regphone" type="text" name="regphone" class="form-control ordertext" placeholder="手机号码" maxlength="11">
                    <input id="regemail" type="text" name="regemail" class="form-control ordertext" placeholder="电子邮件" maxlength="50">
                    <input id="regpwd" type="password" name="regpwd" class="form-control ordertext" placeholder="登录密码（6-20位字符）" maxlength="20">
                    </form>
                </div>
                <div class="recommend_txt" style="margin:20px 20px;text-align:center">
                    
                </div>
                <div class="row" style="margin:20px 20px;text-align:center">
                    <div class="col-xs-5"><a class="btn yellow" href="javascript:;" id="loginnow">已注册？点这里</a></div>
                    <div class="col-xs-7"><a class="btn yellow" href="javascript:;" id="regnow">提交注册</a></div>
                </div>
            </div>
        </div>
    </div>
    
</div>
<!-- BEGIN CORE PLUGINS (REQUIRED FOR ALL PAGES) -->
<!--[if lt IE 9]>
<script src="/assets/plugins/respond.min.js"></script>
<![endif]-->  
<script src="/assets/plugins/jquery-1.10.2.min.js"></script>
<script src="/assets/plugins/jquery-migrate-1.2.1.min.js"></script>
<script src="/assets/plugins/bootstrap/js/bootstrap.min.js"></script>      
<script src="/assets/plugins/back-to-top.js"></script>
<script src="/assets/plugins/jQuery-slimScroll/jquery.slimscroll.min.js"></script>
<script src="/assets/plugins/jquery.blockui.min.js"></script>
<script src="/assets/plugins/jquery.cookie.min.js"></script>
<script src="/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js"></script>
<script src="/assets/plugins/iCheck/icheck.js"></script>
<!-- END CORE PLUGINS -->
<script src="/assets/scripts/app.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        $("#loginname").val($.cookie("loginusername"));
    });
    $('#regnew').live("click", function () {
        $("#reg_view").show();
        $("#login_view").hide();
    });
    $('#loginnow').live("click", function () {
        $("#reg_view").hide();
        $("#login_view").show();
    });

    $('#login').live("click", function () {
        var info = CheckLoginName($("#loginname").val());
        if (info != "") {
            showmessage(info);
            return false;
        }
        if ($("#loginpwd").val() == "") {
            showmessage("密码不能为空");
            return false;
        }
        if ($("#authcode").val() == "") {
            showmessage("验证码不能为空");
            return false;
        }
        $.cookie("loginusername", $("#loginname").val(), { expires: 30, path: '/app' });
        url = "../../WeChat/AjaxService.aspx?action=Login&r=" + Math.random();
        $.post(url, $("#loginform").serialize(), function (obj) {
            if (obj.success) {
                $("#Verification").click();
                if ($("#CouponId").val().length < 36) {
                    top.location = "/app/main";
                }
                else {
                    var url = "/Purchase/AjaxService.aspx?action=CouponReceive&Uid=" + $("#CouponId").val() + "&r=" + Math.random();
                    $.getJSON(url, function (date) {
                        if (date.success) {
                            top.location = "/app/order#coupon";
                        }
                        else {
                            //showmessage("优惠券领用失败");
                            showmessage(date.error);
                        }
                    })
                }
            }
            else {
                showmessage(obj.error);
            }
        }, 'json');
    });

    function CheckLoginName(str) {
        var info = "";
        if (str.length == "") info = "手机或邮件不能为空";
        if (str.indexOf("@") == -1) {
            if (str.length != 11) info = "手机或邮件填写不正确";
            var valid_char = '0123456789'
            for (i = 0; i <= str.length; i++) {
                var the_char = str.charAt(i)
                if (valid_char.indexOf(the_char) == -1) {
                    info = "手机号码填写不正确";
                }
            }
        }
        return info;
    }

    $('#regnow').live("click", function () {
        if ($("#regname").val() == "") {
            showmessage("姓名不能为空");
            return false;
        }
        var info = CheckRegPhone($("#regphone").val());
        if (info != "") {
            showmessage(info);
            return false;
        }
        info = CheckRegEmail($("#regemail").val());
        if (info != "") {
            showmessage(info);
            return false;
        }
        if ($("#regpwd").val() == "") {
            showmessage("登录密码不能为空");
            return false;
        }
        if ($("#regpwd").val().length < 6) {
            showmessage("密码至少6位");
            return false;
        }
        url = "../../WeChat/AjaxService.aspx?action=Reg&r=" + Math.random();
        $.post(url, $("#regform").serialize(), function (obj) {
            if (obj.success) {
                $.cookie("loginusername", $("#regphone").val(), { expires: 30, path: '/app' });
                if ($("#CouponId").val().length < 36) {
                    top.location = "/app/main";
                }
                else {
                    var url = "/Purchase/AjaxService.aspx?action=CouponReceive&Uid=" + $("#CouponId").val() + "&r=" + Math.random();
                    $.getJSON(url, function (date) {
                        if (date.success) {
                            top.location = "/app/order#coupon";
                        }
                        else {
                            showmessage(date.error);
                        }
                    })
                }
                
            }
            else {
                showmessage(obj.error);
            }
        }, 'json');
    });

    function CheckRegPhone(str) {
        var info = "";
        if (str.length != 11) info = "手机号码填写不正确";
        var valid_char = '0123456789'
        for (i = 0; i <= str.length; i++) {
            var the_char = str.charAt(i)
            if (valid_char.indexOf(the_char) == -1) {
                info = "手机号码填写不正确";
            }
        }
        return info;
    }

    function CheckRegEmail(str) {
        var info = "";
        if (str.indexOf("@") == -1) info = "邮件填写不正确";
        return info;
    }

    function verc() {
        $("#Verification").click();
    }

    function showmessage(msg) {
        App.blockUI({ message: msg,
            boxed: true,
            textOnly: true
        });
        window.setTimeout(function () {
            App.unblockUI();
        }, 200000);
    }

    $(document).bind("click", function () {
        App.unblockUI();
    });

    $(document).live('touchstart', function () {
        App.unblockUI();
    });
</script>
</body>
</html>