<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PuFaPay.aspx.cs" Inherits="TravelOnline.WeChat.PuFaPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>浦发信用卡支付</title>
    <link rel="shortcut icon" href="">
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/order.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/line.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <script src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/common.js"></script>
	<script type="text/javascript" src="/newjs/jquery.pagination.js"></script>
	<script type="text/javascript" src="/newjs/img.js"></script>
	<script type="text/javascript" src="/newjs/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/newjs/datePicker.js"></script>
    <script type="text/javascript" src="/newjs/datebind.js"></script>
    <script type="text/javascript" src="/newjs/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
</head>
<body>
    <div class="narrow clearfix">

        <div class="payment-box">
        	
            <div class="payment-con clearfix">
                <div class="tabs pfcard">
                    <form id="submit_form" class="registerform" onsubmit="return false;" action="javascript:;">
                        <input id="pf_orderid" name="pf_orderid" type="hidden" value="<%=OrderID%>"/>
                        
                        <input id="P_Pay" name="P_Pay" type="" value="<%=Pays %>"/>
                        <input id="P_Yfk" name="P_Yfk" type="" value="<%=yfk %>"/>
                        <div class="narrow clearfix">
    	                <!--未开通快捷支付-->
                        <div class="unpaid">
                            <dl class="clearfix">
                                <dt>支付方式</dt>
                                <dd>
                                    <div class="spdi"><img src="../image/spdcard.png" width="165" height="45"></div>
                                </dd>
                            </dl>
                            <dl class="clearfix">
                                <dt>支付金额</dt>
                                <dd class="d1">
                                    <span><b class="empty">¥</b></span><input name="OrderAmt" id="OrderAmt" readonly="value" type="text" value="<%=total_fee%>" class="cost easyui-numberbox" precision="0" max="99999999" maxlength="8">
                                </dd>
                            </dl>
                            
                            <dl class="clearfix">
                                <dt>卡号</dt>
                                <dd class="d1">
                                    <input class="input-w" id="xykh" name="xykh" type="text" value="" datatype="n" nullmsg="请填写信用卡卡号" errormsg="卡号不正确，请重新填写" />
                                </dd>
                            </dl>
                            <dl class="clearfix">
                                <dt>信用卡验证码</dt>
                                <dd class="d1">
                                    <input id="cvv2" name="cvv2" value="" class="input-w" type="password" maxlength="3" value="" datatype="n3-3" nullmsg="请填写信用卡验证码" errormsg="信用卡验证码须为3位数字" />
                                </dd>
                                <div class="card-yzm"><img src="../image/cardb.png" width="229" height="97"></div>
                            </dl>
                            <dl class="clearfix">
                                <dt>手机动态验证码</dt>
                                <dd class="d1">
                                    <input id="pfcode" name="pfcode"class="input-w" type="text" value="" maxlength="6" datatype="*6-6" nullmsg="请填写动态密码" errormsg="请正确填写动态密码" />
                                    <span class="yzm"><input id="Button1" name="Button1" value="免费获取动态密码" type=button style="background-color: white;cursor:pointer;margin-left:10px;margin-top:10px" /></span>
                                </dd>
                            </dl>
                            <dl class="clearfix">
                                <dt></dt>
                                <dd class="d1">
                                    <input id="ordersubmit" class="agpay" type="submit" value="付款" />
                                </dd>
                            </dl>
                        </div>
                    </form>
                </div>
                <div class="popover-mask"></div>
                
                <script type="text/javascript">
                    $(".registerform").Validform({
                        showAllError: true,
                        ignoreHidden: true,
                        tiptype: 3,
                        callback: function (data) {
                            PuFa();
                        }
                    });
				</script>
            </div>
        </div>
        
        <div class="line-b"></div>
	</div>

                    <script type="text/javascript">
                        function PuFa() {
//                            var S_Now = Number($("#money").val());
//                            $("#OrderAmt").val(S_Now);
                            $("#ordersubmit").val("付款中，请稍候...");
                            $("#ordersubmit").attr('disabled', "true");
                            var url = "../newpage/AjaxService.aspx?action=PuFaPay&r=" + Math.random();
                            $.post(url, $("#submit_form").serialize(), function (data) {
                                var obj = eval(data);
                                if (obj.success) {
                                    top.location = "/fifthstep.html?orderid=" + obj.success;
                                    alert("支付成功！");
                                    //top.location ="/WeChat/AjaxService.aspx?action=OrderDetail&uid=" +<%=OrderID%> +".html" ;
                                }
                                else {
                                    alert(obj.error);
                                    $("#ordersubmit").val("付款");
                                    $('#ordersubmit').removeAttr("disabled");
                                }
                            });
                        }

                        var wait = 60;
                        function time(o) {
                            if (wait == 0) {
                                o.removeAttribute("disabled");
                                o.value = "免费获取动态密码";
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

                            if ($("#xykh").val() == "") {
                                alert("请填写您的信用卡卡号");
                                return false;
                            }
                            $("#ordersubmit").val("动态密码发送中，请稍候...");
                            $("#ordersubmit").attr('disabled', "true");
                            time(this);
                            var url = "/newpage/AjaxService.aspx?action=SendPuFaSMS&r=" + Math.random();
                            $.post(url, $("#submit_form").serialize(), function (data) {
                                var obj = eval(data);
                                if (obj.success) {
                                    alert("动态密码发送成功，请查收短信");
                                }
                                else {
                                    alert("动态密码发送失败，请稍后再试");
                                }
                                $("#ordersubmit").val("付款");
                                $('#ordersubmit').removeAttr("disabled");
                            });

                        });
                    </script>
</body>
</html>
