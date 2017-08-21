<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TravelOnline.Login.Register" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>注册新用户</title>
<meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
<meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />  
<link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
<link href="/Styles/registapp.css" rel="stylesheet" type="text/css" />
<link href="/Styles/regist.entry.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script>
<%if (false) { %>
<script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script>
<%} %>
<script type="text/javascript" src="/Scripts/login.base.js"></script>
</head>
<body>
<div class="wrap-app">
  <FORM id=formpersonal onsubmit="return false;" method=post>
    <DIV id="regist">
      <DIV class="app-regist-tit">
        <H2>注册新用户</H2>
      </DIV>
      <DIV class="mc-app">
      <div class="applogo"><img src="../Images/apprigisterlogo.png" width="254" height="105" /></div>
        <DIV class="form">
          <div class="item-app">
            <div class="input-app">
              <input type="text" id="mail" style="width:380px;height:40px" name="mail" class="text" tabindex="1" maxlength="50" placeholder="用户名(邮箱)"/>
              <label id="mail_succeed" class="app-blank"></label>
              <span class="clr"></span>
              <div id="mail_error"></div>
            </div>
          </div>
          <div id="o-password">
            <div class="item-app"> 
              <div class="input-app">
                <input type="password" id="pwd" style="width:380px;height:40px" name="pwd" class="text" tabindex="2" placeholder="设置密码" />
                <label id="pwd_succeed" class="app-blank"></label>
                <span class="clr"></span>
               <!-- <label class="hide" id="pwdstrength"><span >安全程度：</span><b></b></label>-->
                <label id="pwd_error"></label>
              </div>
            </div>
            <div class="item-app"> 
              <div class="input-app">
                <input type="password" id="pwd2" style="width:380px;height:40px" name="pwd2" class="text" tabindex="3" placeholder="确认密码">
                <label id="pwd2_succeed" class="app-blank"></label>
                <span class="clr"></span>
                <label id="pwd2_error"></label>
              </div>
            </div>
          </div>
          <%--<DIV class=item><SPAN class=label>推荐人用户名：</SPAN> 
<DIV class=fl><INPUT id=referrer class=text tabIndex=5 value=可不填 name=referrer> 
<LABEL id=referrer_succeed class="blank invisible"></LABEL><SPAN 
class=clr></SPAN><LABEL id=referrer_error></LABEL></DIV></DIV>--%>
          <div class="item-app"> 
            <div class="input-app">
              <input type="text" id="authcode" name="authcode" style="width:175px;height:40px" class="text" tabindex="4" autocomplete="off" MaxLength="6" placeholder="请输入验证码"/>
              <label id="authcode_succeed" class="app-blank"></label>
              <label class="img"> <img id="JD_Verification1" style="WIDTH: 173px; HEIGHT: 50px; CURSOR: pointer;cursor:pointer;" onclick="this.src='VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="VerifyCode.aspx?&uid=<%=ucode %>" alt=""> </label>
              <span class="clr"></span>
              <label id="authcode_error"></label>
            </div>
          </div>
          <DIV style="height:40px">
           <div class="submit-buttom">
            <INPUT id="registsubmit" class="btn-img btn-regist" tabIndex="8" value="同意以下协议，提交" type="button">
           </div>
          </DIV>
        </DIV>
        <div class="input-app">
        <DIV id="protocol-con-app" >
          <H5>一、本站服务条款的确认和接纳</H5>
          <P>本站的各项电子服务的所有权和运作权归本站。本站提供的服务将完全按照其发布的服务条款和操作规则严格执行。用户同意所有服务条款并完成注册程序，才能成为本站的正式用户。用户确认：本协议条款是处理双方权利义务的约定，除非违反国家强制性法律，否则始终有效。在下订单的同时，您也同时承认了您拥有购买这些产品的权利能力和行为能力，并且将您对您在订单中提供的所有信息的真实性负责。</P>
          <H5>二、服务简介</H5>
          <P>本站运用自己的操作系统通过国际互联网络为用户提供网络服务。同时，用户必须：</P>
          <UL>
            <LI>(1)自行配备上网的所需设备，包括个人电脑、调制解调器或其它必备上网装置。
            <LI>(2)自行负担个人上网所支付的与此服务有关的电话费用、网络费用。 </LI>
          </UL>
          <P>基于本站所提供的网络服务的重要性，用户应同意</P>
          <UL>
            <LI>(1)提供详尽、准确的个人资料。
            <LI>(2)不断更新注册资料，符合及时、详尽、准确的要求。 </LI>
          </UL>
          <P>本站保证不公开用户的真实姓名、地址、电子邮箱和联系电话等用户信息， 除以下情况外：</P>
          <UL>
            <LI>(1)用户授权本站透露这些信息。
            <LI>(2)相应的法律及程序要求本站提供用户的个人资料。 </LI>
          </UL>
        </DIV>
       </div>
        <!--[if !ie]>form end<![endif]--> 
      </DIV>
      <!--[if !ie]>mc end<![endif]--></DIV>
  </FORM>
</div>
<script type="text/javascript" src="/Scripts/Validate/Validate.js"></script> 
<script>

    //领券的券号uid
    var uuid = "75cb372a-1071-48e1-bb5b-a3650124b158";
            
    $.extend(validateFunction, {
        FORM_validate: function () {
            $("#pwd").jdValidate(validatePrompt.pwd, validateFunction.pwd, true);
            $("#pwd2").jdValidate(validatePrompt.pwd2, validateFunction.pwd2, true);
            $("#mail").jdValidate(validatePrompt.mail, validateFunction.mail, true);
            $("#authcode").jdValidate(validatePrompt.authcode, validateFunction.authcode, true);
            return validateFunction.FORM_submit(["#pwd", "#pwd2", "#mail", "#authcode"])
        }
    });

    //调用
    setTimeout(function () { $("#mail").get(0).focus(); }, 0);
    $("#pwd").bind("keyup", function () {
        validateFunction.pwdstrength();
    }).jdValidate(validatePrompt.pwd, validateFunction.pwd)
    $("#pwd2").jdValidate(validatePrompt.pwd2, validateFunction.pwd2);
    $("#mail").jdValidate(validatePrompt.mail, validateFunction.mail);
    $("#authcode").jdValidate(validatePrompt.authcode, validateFunction.authcode);
    $("#viewpwd").bind("click", function () {
        if ($(this).is(":checked") == true) {
            validateFunction.showPassword("text");
            //alert($(this).attr("checked"));
            $("#o-password").addClass("pwdbg");
        } else {
            validateFunction.showPassword("password");
            $("#o-password").removeClass("pwdbg");
            //alert($(this).attr("checked"));
        }
    });

    $("#registsubmit").click(function () {
        if (uuid == "") {
            alert("系统已暂停注册用户！");
            return false;
        }
        var flag = validateFunction.FORM_validate();
        if (flag) {
            $(this).attr({ "disabled": "disabled" }).attr({ "value": "提交中,请稍等" });
            $.ajax({
                type: "POST",
                url: "RegistService.aspx?action=CouponReceive&Uid=" + uuid,
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                data: $("#formpersonal").serialize(),
                error: function () {
                    $("#registsubmit").attr({ "value": "注册失败，请重试" });
                    $("#registsubmit").removeAttr("disabled");
                },
                success: function (result) {
                    if (result) {
                        //alert(result);
                        var obj = eval(result);
                        if (obj.info) {
                            $("#registsubmit").removeAttr("disabled");
                            $("#registsubmit").attr({ "value": "同意以下协议，提交" });
                            alert(obj.info);
                            if (obj.info == "验证码错误") {
                                $("#authcode").val("")
                                setTimeout(function () { $("#authcode").get(0).focus(); }, 0);
                                verc();
                            }
                            if (obj.info == "邮件地址已被注册") {
                                setTimeout(function () { $("#mail").get(0).focus(); }, 0);
                                verc();
                            }
                            if (obj.info == "注册失败") {
                                $("#registsubmit").attr({ "value": "注册失败，请重试" });
                                $("#registsubmit").removeAttr("disabled");
                            }
                        }
                        if (obj.success) {
                            var ToUrl = "";
                            //注册成功后，页面是否跳转
                            var url = "/Purchase/AjaxService.aspx?action=CouponReceive&Uid=" + uuid + "&r=" + Math.random();
                            $.getJSON(url, function (date) {
                                if (date.success) {
                                    if (ToUrl == "") {
                                        alert("优惠券领用成功！请到您的优惠券查看");
                                    }
                                    else {
                                        window.location = ToUrl;
                                    }
                                }
                                else {
                                    alert("优惠券领用失败，请稍后重试");
                                }
                            })
                            
                        }
                    }
                }
            });
        }
    });

    function verc() {
        $("#JD_Verification1").click();
    }

    $("#authcode").bind('keyup', function (event) {
        if (event.keyCode == 13) {
            $("#registsubmit").click();
        }
    });

</script>
</body>
</html>
