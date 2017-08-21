<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fx_editinfo.aspx.cs" Inherits="TravelOnline.WeChat.Fx_editinfo" %>

<!DOCTYPE html>
<html lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0" name="viewport">
<title>分销会员信息修改</title>
<link rel="stylesheet" href="/app_css/fenxiao/distribution_register.css" type="text/css" media="all" />
<link href="/newcss/loginreg.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/newjs/loginreg.js"></script>
</head>

<body>
<div class="main">
    <div class="wraper">
        <h2 class="green">上青旅分销用户信息修改</h2>
        <form class="registerform" id="registerform">
            <input type="hidden" id="random" name="random" />
            <table style="table-layout:fixed;">
                <tr>
                    <td style="width:70px;">手机号<b>*</b></td>
                    <td style="width:205px;"><input type="text" maxlength="11" name="mobile" id="mobile" class="inputxt" value="<%=user.Mobile %>" placeholder="请填写手机号" /></td>
                </tr>
                <div class="reg-error mobilePhone_e"></div>
                <tr>
                    <td style="width:70px;">验证码<b>*</b></td>
                    <td style="width:205px;">
                        <input type="text" id="authcode" name="authcode" tabindex="4" autocomplete="off"  maxlength="4" class="inputxt"  placeholder="请填写验证码" />
                    </td>
                    <span id="authcode_succeed" class="blank"></span> 
                    <td class="img"> 
                        <img id="JD_Verification1" style="width:80px; height:30px; cursor:pointer; margin-left:8px;" onclick="this.src='../users/VerifyCode.aspx?&uid=<%=123456 %>&yys='+new Date().getTime()" src="/users/VerifyCode.aspx?&uid=<%=123456 %>" alt="" style="cursor:pointer;width:100px;height:26px;" /> 
                        <span class="ftx23">&nbsp;看不清？<a  href="javascript:void(0)" onclick="verc()" class="flk13">换一张</a></span>
                    </td>
                    <span class="clr"></span> 
                    <span id="authcode_error"></span> 
                </tr>
                <%if (Convert.ToString(Session["Fx_mobileLogin"]).Length > 0){ %>
                <tr>
                    <td>密码：<b>*</b></td>
                    <td><input type="password" value="" id="userpassword" name="userpassword" class="inputxt" placeholder="请输入密码" datatype="*6-16" nullmsg="请设置密码！" errormsg="密码范围在6~16位之间！" /></td>
                </tr>
                <%} %>
                <tr>
                    <td style="width:70px;">用户名</td>
                    <td style="width:205px;"><input type="text" value="<%=user.UserName %>" id="username" name="username" placeholder="请输入用户名" class="inputxt" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">邮箱地址</td>
                    <td style="width:205px;"><input type="text" name="email" id="email" class="inputxt" placeholder="请输入邮箱地址" value="<%=user.UserEmail %>"  datatype="e"  errormsg="邮箱格式不正确" nullmsg="请填写邮箱地址" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">微信号</td>
                    <td style="width:205px;"><input type="text" value="<%=user.Wxid %>" id="wxid" name="wxid" placeholder="请输入微信号" class="inputxt" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">微店名称</td>
                    <td style="width:205px;"><input type="text" value="<%=user.Storename %>" id="storename" name="storename" placeholder="请输入微店名称" class="inputxt" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">座机号</td>
                    <td style="width:205px;"><input type="text" value="<%=user.Tel %>" maxlength="12" id="tel" name="tel" placeholder="请输入座机号" class="inputxt" /></td>
                </tr>
                <tr>
                    <td style="width:70px;">地址</td>
                    <td style="width:205px;"><input type="text" value="<%=user.Address %>" id="address" name="address" placeholder="请输入地址" class="inputxt" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2" style="padding:10px 0 18px 0;">
                        <input type="button" class="reg-btn" value="提 交" /> <input type="reset" value="重 置" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</div>

<script type="text/javascript" src="/newjs/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/newjs/Validform_v5.3.2_min.js"></script>

<script type="text/javascript">
    $(function () {
        $(".registerform").Validform();
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

    $(".reg-btn").click(function () {
        $(this).val("注册中，请稍候...");
        $(this).attr("disabled", true);
        var url = "AjaxService.aspx?action=EditInfo&r=" + Math.random();
        $.post(url, $("#registerform").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                top.location = "main_fx.aspx";
            }
            else {
                $(".reg-btn").val("提 交");
                $(".reg-btn").attr("disabled", false);
                if (obj.email) {
                    $('.email_e').html(obj.email);
                    return false;
                }
                if (obj.mobilePhone) {
                    $('.mobilePhone_e').html(obj.mobilePhone);
                    return false;
                }
                if (obj.error) {
                    $('.mobilePhone_e').html(obj.error);
                    return false;
                }
            }
        });
    });

    $("#Button1").click(function () {
        if ($("#mobile").val().length < 11) {
            $('.mobilePhone_e').html("<i></i>手机号长度必须是11位");
            return false;
        }

        time(this);

        var random = Math.random();
        $("#random").val(random);
        var url = "AjaxService.aspx?action=FXSendRegSMS&code=" + $("#authcode").val().toLocaleLowerCase() + "&r=" + random;
        $.post(url, $("#registerform").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                $('.Phoneyzm_e').html(obj.success);
            }
            else {
                $('.Phoneyzm_e').html(obj.error);
            }
        });

    });

    function verc() {
        $("#JD_Verification1").click();
    }


</script>
</body>
</html>