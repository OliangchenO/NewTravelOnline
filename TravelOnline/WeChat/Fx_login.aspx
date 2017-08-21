<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fx_login.aspx.cs" Inherits="TravelOnline.WeChat.Fx_login" %>

<!DOCTYPE html>
<html lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0" name="viewport">
<title>分销会员登陆</title>
<link rel="stylesheet" href="/app_css/fenxiao/distribution_register.css" type="text/css" media="all" />
<script type="text/javascript" src="/newjs/loginreg.js"></script>
<link href="/newcss/loginreg.css" rel="stylesheet" type="text/css" />
</head>

<body>
<div class="main">
    <div class="wraper" style="padding-left:10px;">
        <h2 class="green">上青旅分销用户登陆</h2>
        <form class="loginform" id="loginform">
            <table width="100%" style="table-layout:fixed;">
                <tr>
                    <td style="width:70px;">手机号<b>*</b></td>
                    <td style="width:205px;"><input type="text"  maxlength="11" name="mobile" id="mobile" class="inputxt"  placeholder="请填写手机号" datatype="n11-11"  errormsg="请输入有效11位手机号" nullmsg="请填写手机号" /></td>
                </tr>
                <tr>
                    <td>密码：<b>*</b></td>
                    <td><input type="password" value="" name="userpassword" id="userpassword" class="inputxt" placeholder="" datatype="*6-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">验证码</td>
                    <td style="width:205px;">
                        <input type="text" id="authcode" name="authcode" tabindex="4" autocomplete="off"  maxlength="4" name="name" class="inputxt"  placeholder="请填写验证码" />
                    </td>
                    <span id="authcode_succeed" class="blank"></span>
                </tr>
                <div class="login-error"></div>
                <tr>
                    <td style="width:70px;"></td>
                    <span id="authcode_succeed" class="blank"></span>
                    <td class="img"> 
                        <img id="JD_Verification1" style="width:80px; height:30px; cursor:pointer; margin-left:8px;" onclick="this.src='/users/VerifyCode.aspx?&uid=<%=123456 %>&yys='+new Date().getTime()" src="/users/VerifyCode.aspx?&uid=<%=123456 %>" alt="" style="cursor:pointer;width:100px;height:26px;" /> 
                        <span class="ftx23">&nbsp;看不清？<a  href="javascript:void(0)" onclick="verc()" class="flk13">换一张</a></span>
                    </td>
                    <span class="clr"></span> 
                    <span id="authcode_error"></span> 
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2" style="padding:10px 0 18px 0;">
                        <a class="login_button" href="javascript:;">登录</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href="Fx_register.aspx">立即注册</a>
                    </td>
                </tr>
            </table>
            <dl class="other-login">
                <dt>使用合作网站账号登录</dt>
                <dd>
                    <a href="/WeChat/WeiXinLogin.aspx?code=021ee70dc496d4dba2207b3f8043885z&state=Fx_regedit" class="sina" target="_blank">微信</a>
                </dd>
           </dl>
        </form>
    </div>
</div>
<script type="text/javascript" src="/newjs/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/newjs/Validform_v5.3.2_min.js"></script>

<script type="text/javascript">
    $(function(){
        $(".registerform").Validform();
    })

    function verc() {
        $("#JD_Verification1").click();
    }

    $(".login_button").click(function () {
        $(this).html("正在登录，请稍候...");
        $(this).attr("disabled", true);
        var url = "AjaxService.aspx?action=FxLogin&r=" + Math.random();
        $.post(url, $("#loginform").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                top.location = "main_fx.aspx";
            }
            else {
                $(".login_button").html("登陆");
                $(".login_button").attr("disabled", false);
                $(".login-error").html(obj.error);
            }
        });
    })
</script>
</body>
</html>
