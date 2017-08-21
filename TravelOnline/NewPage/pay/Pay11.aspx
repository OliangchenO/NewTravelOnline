<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="TravelOnline.NewPage.Pay" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title>上海青旅官网:旅游网_自由行_旅游签证_国内游_出国游_旅游线路推荐上海青旅官网</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="上海青旅官网(www.scyts.com)-上海市首批5A级旅行社,全国旅游标准化试点旅行社,全国百强旅行社,提供包含国内旅游.出境旅游.自由行.邮轮旅游及旅游签证等服务" />
    <meta name="keywords" content="上海市首批5A级旅行社,上海旅行社,旅游,度假,出境旅游,国内旅游,上海青旅,上海中国青年旅行社" />
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
</head>
<body>
	<!--页头Begin-->
	<div class="order-header">
		<div class="narrow">
            <div class="header-top-order">
                <a class="logo-s" title="上海中国青年旅行社" href="http://www.scyts.com/"></a>
                <div class="sidebar rl">
                    <ul class="clearfix">
                        <li>您好，<a class="colorF60" href="javascript:;"><%=username %></a></li>
                        <li><a href="/login/logout.aspx">退出</a></li>
                        <li class="">|</li>
                        <li><a href="javascript:;">帮助中心</a></li>
                    </ul>
                </div>
            </div>
        </div>
	</div>
	<!--正文内容Begin-->
	<div class="narrow clearfix">
    	<div class="order-con">
        	<i></i>
        	<h2><%=linename %></h2>
            <dl>
            	<dd>订单号：<span><%=autoid %></span><a href="/OrderView/<%=OrderId %>.html" target="_blank">订单详情</a></dd>
            </dl>
            <dl>
            	<dd>预定人数：<%=ordernums %>人</dd>
                <dd>出发日期：<%=orderdate %></dd>
            </dl>
            <div class="con-info <%=hide1 %>">
            	<p>
                您的订单已既时占位成功，请在24小时内通过网银或支付宝付款，付款后您的占位信息将保留；<br/>
                订单确认后如未及时支付，订单将自动转为非占位状态（紧急情况或特殊情况除外）。
                </p>
            </div>
            <div class="con-info np <%=hide2 %>">
            	<p>
                此产品需要确认后才能付款，我们将在30分钟内联系您，核对确认订单信息。<br/>
                非工作时间下单，我们将在您下单次日后12小时内联系您，祝您旅行愉快！（我们的工作时间是周一至周五09:00-17:30）
                </p>
            </div>
            <div class="sum">您需要支付的金额为：<span><b>¥</b><%=price %><em>元</em></span></div>
        </div>
        <div class="payment-box <%=hide3 %>">
        	<ul>
                <%=payhide11 %>
                <%=payhide21 %>
            </ul>
            <div class="payment-con clearfix">
                <div class="tabs bankpay">
                <form class="payform" action="#">
                    <input id="OrderId" name="OrderId" type="hidden" value="<%=OrderId %>"/>
                    <%--<label class="lab">
                        <input name="bank" type="radio" value="AliPay" checked=checked/>
                        <div class="bank zfb fl">
                            <b></b>
                        </div>
                    </label>
                    <div class="clear"></div>
                    <label class="lab">
                        <input name="bank" type="radio" value="ICBCB2C"/>
                        <div class="bank gshy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CMB"/>
                        <div class="bank zshy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CCB"/>
                        <div class="bank jshy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="BOCB2C"/>
                        <div class="bank zghy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="ABC"/>
                        <div class="bank nyhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="COMM"/>
                        <div class="bank jthy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="PSBC-DEBIT" checked=checked/>
                        <div class="bank yzhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CEBBANK"/>
                        <div class="bank gdhy fl">
                            <b></b>
                        </div>
                    </label>--%>
                    <label class="lab">
                        <input name="bank" type="radio" value="SPDB" checked=checked/>
                        <div class="bank pfhy fl">
                            <b></b>
                        </div>
                    </label>
                    <%--<label class="lab">
                        <input name="bank" type="radio" value="GDB"/>
                        <div class="bank gfhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CITIC"/>
                        <div class="bank zxhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CIB"/>
                        <div class="bank xyhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="SDB"/>
                        <div class="bank szhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="CMBC"/>
                        <div class="bank mshy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="BJBANK"/>
                        <div class="bank bjhy fl">
                            <b></b>
                        </div>
                    </label>
                    <label class="lab">
                        <input name="bank" type="radio" value="HZCBB2C"/>
                        <div class="bank hzhy fl">
                            <b></b>
                        </div>
                    </label>--%>
                    <div class="wenxin"><span>温馨提示：</span>请妥善保管您的账户信息，尽量避免在公共场所进行支付。</div>
                    <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="onlinepay"></a></div>
                </form>
                </div>
                <%--<div class="tabs pfcard <%=payhide22 %>">
                    <form id="submit_form" class="registerform" onsubmit="return false;" action="javascript:;">
                        <input id="pf_orderid" name="pf_orderid" type="hidden" value="<%=OrderId %>"/>
                        <input id="OrderAmt" name="OrderAmt" type="hidden" value="<%=price %>"/>
                        <!--未开通快捷支付-->
                        <div class="unpaid">
                            <dl class="clearfix">
                                <dt>支付方式</dt>
                                <dd>
                                    <div class="spdi"><img src="../image/spdcard.png" width="165" height="45"></div>
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
                </div>--%>
                <div class="popover-mask"></div>
                
                <script type="text/javascript">
					$(".registerform").Validform({
						showAllError:true,
						ignoreHidden:true,
						tiptype: 3,
						callback: function (data) {
						    PuFa();
						}
					});
				</script>
            </div>
        </div>
        <div class="order-nav <%=hide2 %>">
        	<h3>网站导航 >></h3>
            <ul>
            	<li><a href="/index.html">首页</a></li>
                <li><a href="/outbound.html">出境游</a></li>
                <li><a href="/inland.html">国内游</a></li>
                <li><a href="/freetour.html">自由行</a></li>
                <li><a href="/cruise.html">邮轮旅游</a></li>
                <li><a href="/visa.html">签证</a></li>
            </ul>
        </div>
        <div class="line-b"></div>
	</div>
	<!--正文内容End-->
	
    <!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        $('#onlinepay').click(function () {
            top.location = "/newpage/pay/alipay.aspx?orderid=" + $('#OrderId').val() + "m=alipay&p=" + $('input[name="bank"]:checked').val();
            //window.open("/newpage/pay/alipay.aspx?orderid=" + $('#OrderId').val() + "m=alipay&p=" + $('input[name="bank"]:checked').val());
        })

        function PuFa() {
            $("#ordersubmit").val("付款中，请稍候...");
            $("#ordersubmit").attr('disabled', "true");
            var url = "/newpage/AjaxService.aspx?action=PuFaPay&r=" + Math.random();
            $.post(url, $("#submit_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/fifthstep.html?orderid=" + obj.success;
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