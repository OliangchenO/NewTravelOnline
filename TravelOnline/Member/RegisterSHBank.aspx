<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TravelOnline.Member.Register" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>上海银行美好生活会员注册 - 上海青旅官网</title>
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
		<a href="../IndexNew.aspx" class="logo">上海青旅</a>
		<div class="bk-nav">
			<a href="/member/login.html">登录</a>
			<span>|</span>
			<a href="javascript:void(0)">帮助中心</a>
		</div>
	</h1>
	<!--正文内容Begin-->
	<div class="reg-content clearfix">
		<div class="regbox fl">
			<h2 class="reg-title">上海银行美好生活会员注册<!--<a class="shback" href="javascript:void(0)">上海银行美好生活注册</a>--></h2>
            <form action="#" id="submit_form">
			<div class="reg-form">
				<dl class="reg-form-line clearfix">
					<dt>卡号</dt>
					<dd>
						<input type="text" class="reg-input" maxlength="18" name="CardID" id="CardID" placeholder="可作为登录用户名">
					</dd>
					<b class="pass mobilePhone"></b>
				</dl>
				<div class="reg-error mobilePhone_e"></div>
				<dl class="reg-form-line clearfix">
					<dt>密码</dt>
					<dd>
						<input type="password" class="reg-input" maxlength="18" name="password" id="password" placeholder="6-18位字母、数字或符号">
					</dd>
					<b class="pass password"></b>
				</dl>
				<div class="reg-error password_e"></div>
				<dl class="reg-form-line clearfix">
					<dt>确认密码</dt>
					<dd>
						<input type="password" class="reg-input" maxlength="18" name="confirm_password" id="confirm_password" placeholder="再次输入密码">
					</dd>
					<b class="pass confirm_password"></b>
				</dl>
				<div class="reg-error confirm_password_e"></div>
				<dl class="reg-form-line pt8 clearfix">
					<dt class="accept">&nbsp;</dt>
					<dd>
						<label>
							<input id="xieyi" type="checkbox" checked="checked">我已同意并接受
						</label>
						<span id="js_show_pop">《上海银行美好生活服务条款》</span>
					</dd>
				</dl>
				<dl class="reg-form-line clearfix">
					<dt>&nbsp;</dt>
					<dd>
						<%--<a href="javascript:;" class="reg-btn">同意协议并注册</a>--%>
                        <INPUT type=button class="reg-btn" value="同意协议并注册" />
					</dd>
				</dl>
			</div>
		    </form>
        </div>
		<div class="reg-catoon fl">
			<img src="/image/member/logoin_cartoon.jpg">
		</div>
	</div>
	<div class="term-pop" id="pop">
		<div class="pop-title">
			<span class="close">x</span>
			<h3>上海银行美好生活服务条款</h3>
		</div>
		<div class="pop-wrap">
			<p class="pop-list">1、本站服务条款的确认和接纳</p>
			<p class="pop-con">本站的各项电子服务的所有权和运作权归本站。本站提供的服务将完全按照其发布
			的服务条款和操作规则严格执行。用户同意所有服务条款并完成注册程序，才能成
			为本站的正式用户。用户确认：本协议条款是处理双方权利义务的约定，除非违反
			国家强制性法律，否则始终有效。在下订单的同时，您也同时承认了您拥有购买这
			些产品的权利能力和行为能力，并且将您对您在订单中提供的所有信息的真实性负
			责。
			</p>
			<p class="pop-list">2、服务简介</p>
			<p class="pop-con">本站运用自己的操作系统通过国际互联网络为用户提供网络服务。同时，用户必须：
			(1)自行配备上网的所需设备，包括个人电脑、调制解调器或其它必备上网装置。 
			(2)自行负担个人上网所支付的与此服务有关的电话费用、网络费用。 
			基于本站所提供的网络服务的重要性，用户应同意
			(1)提供详尽、准确的个人资料。 
			(2)不断更新注册资料，符合及时、详尽、准确的要求。 
			本站保证不公开用户的真实姓名、地址、电子邮箱和联系电话等用户信息， 除以下
			情况外：
			(1)用户授权本站透露这些信息。 
			(2)相应的法律及程序要求本站提供用户的个人资料。
			</p>
			<p class="pop-list">3、服务的变更和终止</p>
			<p class="pop-con">您完全理解并同意，本服务涉及到互联网及移动通讯等服务，可能会受到各个环节不稳定因素的影响。因此任何因不可抗力、计算机病毒或黑客攻击、系统不稳定、用户所在位置、用户关机、GSM网络、互联网络、通信线路等其他上海中国青年旅行社无法预测或控制的原因，造成的服务中断、取消或终止的风险。您须自行承担以上风险，上海中国青年旅行社对服务之及时性、安全性、准确性不做任何保证。
			上海中国青年旅行社需要定期或不定期地对提供网络服务的平台或相关的设备进行检修或者维护，如因此类情况而造成网络服务（包括收费网络服务）在合理时间内的中断，上海中国青年旅行社无需为此承担任何责任。上海中国青年旅行社保留不经事先通知为维修保养、升级或其它目的暂停全部或部分的网络服务的权利。
			您完全理解并同意，除本服务协议另有规定外，鉴于网络服务的特殊性，上海中国青年旅行社有权随时变更、中断或终止部分或全部的网络服务，且无需通知您，也无需对您或任何第三方承担任何责任。
			</p>
			<p class="pop-list">4、本网站的权利</p>
			<p class="pop-con">本网站在下列情况下可以不通过向会员通知，直接删除其上载的内容：
			1）有损于本网站，会员或第三者名誉的内容；
			2）利用上海中国青年旅行社服务系统上载、张贴或传送任何非法、有害、胁迫、滥用、骚扰、侵害、中伤、粗俗、猥亵、诽谤、侵害他人隐私、辱骂性的、恐吓性的、庸俗淫秽的及有害或种族歧视的或道德上令人不快的包括其他任何非法的内容；
			3）侵害本网站或第三者的版权，著作权等内容；
			4）存在与本网站提供的服务无关的内容；
			5）无故盗用他人的ID(固有用户名)，姓名上载、张贴或传送任何内容及恶意更改，伪造他人上载内容。
			对于上海中国青年旅行社开发的软件，任何用户不得对该等软件进行如下违法行为：
			1) 开展方向工程、反向编译或反汇编，或以其他方式发现其原始编码，以及实施任何涉嫌侵害著作权等其他知识产权的行为；
			2) 以出租、租赁、销售、转授权、分配或其他任何方式向第三方转让该等软件或利用该等软件为任何第三方提供相似服务；
			3) 任何复制该等软件的行为；
			4) 以移除、规避、破坏、损害或其他任何方式干扰该等软件的安全功能；
			5) 以不正当手段取消该等软件上权利声明或权利通知的；
			6) 其他违反法律规定的行为。
			上海中国青年旅行社网络服务LOGO等为上海青旅的注册商标，受法律的保护。任何用户不得侵犯上海中国青年旅行社注册商标权。
			</p>
			<p class="pop-list">5、免责声明</p>
			<p class="pop-con">任何人因使用本网站而可能遭致的意外及其造成的损失（包括因下载本网站可能链接的第三方网站内容而感染电脑病毒），上海中国青年旅行社对此概不负责，亦不承担任何法律责任。
			本网站禁止制作、复制、发布、传播等具有反动、色情、暴力、淫秽等内容的信息，一经发现，立即删除。若您因此触犯法律，上海中国青年旅行社对此不承担任何法律责任。
			本网站用户自行上传，上海中国青年旅行社默认用户享有此类素材图片或文字的版权。我们仅提供一个展示、交流的平台，不对其内容的准确性、真实性、正当性、合法性负责，也不承担任何法律责任。
			任何单位或个人认为通过本网站网页内容可能涉嫌侵犯其著作权，应该及时向我们提出书面权利通知，并提供身份证明、权属证明及详细侵权情况证明。我们收到上述法律文件后，将会依法尽快处理。
			上海中国青年旅行社内所有内容并不反映任何上海中国青年旅行社之意见及观点。
			上海中国青年旅行社不保证为向用户便利而设置的到外部网页的链接的准确性和完整性，同时，对于该等外部链接指向的不由上海中国青年旅行社实际控制的任何网页上的内容，上海中国青年旅行社不承担任何责任。
			</p>
			<p class="pop-list">6、用户权利</p>
			<p class="pop-con">您完成注册申请手续后，意味着您已获得上海中国青年旅行社账户（用户名）的使用权。您应提供及时、详尽及准确的个人资料，并不断更新注册资料，符合及时、详尽准确的要求。您应妥善保管您的账户（用户名）和密码，通过您的账户（用户名）和密码操作或实施的行为，将视为您本人的行为，由您本人承担相应的责任和后果。如果您发现他人不当使用您的账户或有任何其他可能危及您的账户安全的情形时，您应当立即以书面、有效方式通知上海中国青年旅行社，要求上海中国青年旅行社暂停相关服务。在此，您理解上海中国青年旅行社对您的请求采取行动需要合理时间，上海中国青年旅行社对在采取行动前已经产生的后果（包括但不限于您的任何损失）不承担任何责任。
			</p>
			<p class="pop-list">7、隐私保护</p>
			<p class="pop-con">用户隐私是上海中国青年旅行社的一项基本政策，上海中国青年旅行社保证不对外公开或向第三方提供用户注册资料及用户在使用网络服务时存储在上海中国青年旅行社的非公开内容，但下列情况除外：
			1）事先获得用户的明确授权；
			2）根据有关的法律法规要求；
			3）按照相关政府主管部门的要求；
			4）为维护社会公众的利益；
			5）为维护上海中国青年旅行社的合法权益。
			6）根据本声明的其他规定及上海中国青年旅行社认为其他必要情形。
			上海中国青年旅行社可能会与第三方合作向用户提供相关的网络服务，在此情况下，如该第三方同意承担与上海中国青年旅行社同等的保护用户隐私的责任，则上海中国青年旅行社可将用户的注册资料等提供给该第三方。
			在不透露单个用户隐私资料的前提下，上海中国青年旅行社有权对整个用户数据库进行分析并对用户数据库进行商业上的利用。
			上海中国青年旅行社可能会自动接收并暂时记录用户的浏览器和服务器日志上的信息，其中包括IP地址、上海中国青年旅行社cookie中的信息，以及用户需求的网页记录。上海中国青年旅行社网络会将这些资料用于改进为你提供的服务。
			</p>
		</div>
		<div class="submit">
			<a href="javascript:;" class="pop-btn">已阅读并同意此条款</a>
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
                    CardID: {
                        minlength: 18,
                        required: true,
                        digits: true
                    },
                    password: {
                        minlength: 6,
                        required: false
                    },
                    confirm_password: {
                        required: true,
                        minlength: 6,
                        equalTo: "#password"
                    }
                },
                messages: {
                    CardID: {
                        required: "<i></i>请输手机号码",
                        minlength: "<i></i>请填写有效的18位卡号",
                        digits: "<i></i>请正确填写手机号"
                    },
                    password: {
                        required: "<i></i>请设置登录密码",
                        minlength: "<i></i>密码需为6-18位字母、数字或符号"
                    },
                    confirm_password: {
                        required: "<i></i>请再次输入密码",
                        minlength: "<i></i>密码需为6-18位字母、数字或符号",
                        equalTo: "<i></i>您两次输入的密码不一致"
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
                $(this).val("注册中，请稍候...");
                $(this).attr("disabled", true);
                var url = "AjaxService.aspx?action=SHBankRegister&r=" + Math.random();
                $.post(url, $("#submit_form").serialize(), function (data) {
                    var obj = eval(data);
                    if (obj.success) {
                        top.location = "/member/regsuccess.html";
                    }
                    else {
                        $(".reg-btn").val("同意协议并注册");
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
            if ($("#mobilePhone").val().length < 11) {
                $('.mobilePhone_e').html("<i></i>手机号长度必须是11位");
                return false;
            }
            time(this);
            var url = "AjaxService.aspx?action=SendRegSMS&r=" + Math.random();
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