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
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script>  //电商订单数据监控
        var _hmt = _hmt || [];
        var orderInfo = {
                "orderId": "<%=autoid %>",
                "orderTotal": <%=YeE%>,
                "item": []
            };
            orderInfo.item.push({
                "skuName": "<%=linename %>",
                "category": "旅游产品",
                "Price": <%=YeE%>/<%=ordernums %>,
                "Quantity": <%=ordernums %>
            });
            _hmt.push(['_trackOrder', orderInfo]);
    </script>
</head>
<body>
	<!--页头Begin-->
    <!-- scyts.com Baidu tongji analytics --><script type="text/javascript">
    var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
    document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F670e68bb0a5926537ba7c720575bc7eb' type='text/javascript'%3E%3C/script%3E"));
    </script>
	<div class="order-header">
		<div class="narrow">
            <div class="header-top-order">
                <a class="logo-s" title="上海中国青年旅行社" href="http://www.scyts.com/"></a>
                <div class="sidebar rl">
                    <ul class="clearfix">
                        <%=LoginInfo%>
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
                您的订单已既时占位成功，请在30分钟内付款，付款后您的占位信息将保留；<br/>
                订单确认后如未及时支付，订单将在30分钟后自动取消。
                </p>
            </div>
            <div class="con-info <%=hide6 %>">
            	<p>
                此产品为预购产品，您现在需支付的99元将作为定金，2016年11月11日当天付清余款，取消定单，定金不予退还
                </p>
            </div>
            <div class="con-info np <%=hide2 %>">
            	<p>
                此产品需要确认后才能付款，我们将在30分钟内联系您，核对确认订单信息。<br/>
                非工作时间下单，我们将在您下单次日后12小时内联系您，祝您旅行愉快！（我们的工作时间是周一至周五09:00-17:30）
                </p>
            </div>
            <div class="con-info np <%=hide1_1 %>">
            	<p>
                您的订单已既时占位成功，请在30分钟内付款，付款后您的占位信息将保留；<br/>
                订单确认后如未及时支付，订单将在30分钟后自动取消。<br />
                请把所需材料原件扫描，通过<a href="http://www.173travel.com/scyts">http://www.173travel.com/scyts</a>上传
                </p>
            </div>
            <div class="sum">您需要支付的金额为：<span><b>¥</b><%=YeE%><em>元</em></span>&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="<%=payhide12 %>">
                <div class="sum">第三方支付，最低预付款金额为：<span><b>¥</b><%=yfk%><em>元</em></span></div>
                <div class="sum write">请填写您要支付的金额：
                    <span><b class="empty">¥</b></span><input name="money" id="money" type="text" value="<%=YeE%>" class="cost easyui-numberbox" precision="0" max="99999999" maxlength="8">
                </div>
            </div>
        </div>
        <div class="order-con" style="display:none;">
            <h2><%=linename %></h2>
            <div class="sum">
                <span>抱歉，该产品已售罄，您的订单占位失败！</span>
            </div>
        </div>
        <div class="payment-box <%=hide3 %>">
        	<ul>
                <%=payhide11 %>
                <%=payhide21 %>
                <%=payhide31 %>
                <%=payhide41 %>
                <%if(!(Convert.ToString(ConfigurationManager.AppSettings["SHRCBLine"]).IndexOf(lineId) > -1)){ %>
                    <li id='qfqpay'>去分期<b class="ico_hui"></b></li>
                <%} %>
                <%=payhide51 %>
                
            </ul>
            <div class="payment-con clearfix">
                <!--支付宝支付-->
                <div class="tabs bankpay <%=payhide12 %>">
                    <form class="payform" action="#">
                        <input id="OrderId" name="OrderId" type="hidden" value="<%=OrderId %>"/>
                        <%if(Convert.ToString(ConfigurationManager.AppSettings["SHRCBLine"]).IndexOf(lineId) > -1) {%>
                        <label class="lab">
                            <input name="bank" type="radio" value="SHRCB" checked=checked/>
                            <div class="bank nsyh fl">
                                <b></b>
                            </div>
                        </label>
                        <%} else{ %>
                        <label class="lab">
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
                            <input name="bank" type="radio" value="PSBC-DEBIT"/>
                            <div class="bank yzhy fl">
                                <b></b>
                            </div>
                        </label>
                        <label class="lab">
                            <input name="bank" type="radio" value="CEBBANK"/>
                            <div class="bank gdhy fl">
                                <b></b>
                            </div>
                        </label>
                        <label class="lab">
                            <input name="bank" type="radio" value="SPDB"/>
                            <div class="bank pfhy fl">
                                <b></b>
                            </div>
                        </label>
                        <label class="lab">
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
                        </label>
                        <label class="lab">
                            <input name="bank" type="radio" value="SHRCB"/>
                            <div class="bank nsyh fl">
                                <b></b>
                            </div>
                        </label>
                        <%} %>
                        <div class="wenxin"><span>温馨提示：</span>请妥善保管您的账户信息，尽量避免在公共场所进行支付。</div>
                        <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="onlinepay"></a></div>
                    </form>
                </div>
                <!--浦发信用卡支付-->
                <div class="tabs pfcard <%=payhide22 %>">
                    <form id="submit_form" class="registerform" onsubmit="return false;" action="javascript:;">
                        <input id="pf_orderid" name="pf_orderid" type="hidden" value="<%=OrderId %>"/>
                        <input id="autoID" name="autoID" type="hidden" value="<%=autoid %>"/>
                        <input id="OrderAmt" name="OrderAmt" type="hidden" value="<%=pufaprice %>"/>
                        <input id="P_Pay" name="P_Pay" type="hidden" value="<%=Pays %>"/>
                        <input id="P_Yfk" name="P_Yfk" type="hidden" value="<%=yfk %>"/>
                        <div class="change_xyk">
                            <label id="jtcard" class="lab">
                                <input name="bank" type="radio" value="BOCM" checked="checked"/>
                                <div class="bank jthy fl">
                                    <b></b>
                                </div>
                            </label>
                            <label id="pfcard" class="lab" >
                                <input name="bank" type="radio" value="SPDB"/>
                                <div class="bank pfhy fl">
                                    <b></b>
                                </div>
                            </label>
                            <div id="jt_xyk" class="narrow  clearfix">
                                <!--未开通快捷支付-->
                                <div class="unpaid">
                                    <dl class="clearfix">
                                        <dt>卡号</dt>
                                        <dd class="d1">
                                            <input class="input-w" id="cardNo" name="cardNo" type="text" value="" datatype="n" nullmsg="请填写信用卡卡号" errormsg="卡号不正确，请重新填写" />
                                        </dd>
                                    </dl>
                                    <dl class="clearfix">
                                        <dt>卡片有效期</dt>
                                        <dd class="d1">
                                            <input id="expireDate" name="expireDate" value="" class="input-w" type="text" maxlength="4" datatype="n4-4" placeholder="格式：yyMM 示例：3011" />
                                        </dd>
                                        <div class="card-yzm"><img src="../../image/cardb.png" width="229" height="97"></div>
                                    </dl>
                                    <dl class="clearfix">
                                        <dt>姓名</dt>
                                        <dd class="d1">
                                            <input id="customerName" name="customerName" value="" class="input-w" type="text" />
                                        </dd>
                                        <div class="card-yzm"><img src="../../image/cardb.png" width="229" height="97"></div>
                                    </dl>
                                    <dl class="clearfix">
                                        <dt>手机动态验证码</dt>
                                        <dd class="d1">
                                            <input id="jtcode" name="jtcode"class="input-w" type="text" value="" maxlength="6" datatype="*6-6" nullmsg="请填写动态密码" errormsg="请正确填写动态密码" />
                                            <span class="yzm"><input id="Button2" name="Button2" value="免费获取动态密码" type=button style="background-color: white;cursor:pointer;margin-left:10px;margin-top:10px" /></span>
                                        </dd>
                                    </dl>
                                    <dl class="clearfix">
                                        <dt></dt>
                                        <dd class="d1">
                                            <input id="jtordersubmit" class="agpay" type="submit" value="付款" />
                                        </dd>
                                    </dl>
                                </div>
                            </div>
                            <div id="pf_xyk" class="narrow  clearfix" style="display:none;">
            	                <!--未开通快捷支付-->
                                <div class="unpaid">
                                    <dl class="clearfix">
                                        <dt>支付方式</dt>
                                        <dd>
                                            <div class="spdi"><img src="../../image/spdcard.png" width="165" height="45"></div>
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
                                        <div class="card-yzm"><img src="../../image/cardb.png" width="229" height="97"></div>
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
                            </div>
                            
                        </div>
                    </form>
                </div>
                <!--网银支付-->
                <div class="tabs bankpay <%=payhide32 %>">
                    <form class="payform" action="#">
                        
                        <div class="clear"></div>
                        <!--中国银联-->
                        <label class="lab">
                            <input name="banklist" type="radio" value="CHINAPAY"  checked="checked"/>
                            <div class="bank chinapay fl">
                                <b></b>
                            </div>
                        </label>
                        <!--建设银行-->
                        <label id="hide_fq" class="lab">
                            <input name="banklist" type="radio" value="CCB"/>
                            <div class="bank jshy fl">
                                <b class="ico_fq" style="background-position: 0 -48px"></b>
                            </div>
                        </label></br>
                        
                        <label id="hide_fq" class="lab">
                            <input name="banklist" type="radio" value="SHBK"/>
                            <div class="bank shhy fl">
                                <b></b>
                            </div>
                        </label>
                        <label class="lab">
                            <input name="banklist" type="radio" value="SHRCB"/>
                            <div class="bank nsyh fl">
                                <b></b>
                            </div>
                        </label>
                        
                        <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="BankPay"></a></div>
                    </form>
                </div>
               
                <!--微信支付-->
                <div class="tabs bankpay <%=payhide42 %>">
                    <form class="payform wx_pay01" action="#">
                        <div class="clear"></div>
                        <label class="lab">
                            <input name="bank" type="radio" value="WXPay" checked=checked/>
                            <div class="bank wx fl">
                                <b class="wx-icon"></b>
                            </div>
                            <strong class="seeing"><span>温馨提示：</span>点击“下一步”后，请打开手机微信的“扫一扫”，扫描二维码</strong>
                            <img src="../../image/mobile_pic.png" style="float:right; width: 200px;">
                        </label></br>
                        <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="go_WXPay"></a></div>
                    </form>
                </div>
                <!--去分期-->
                <div class="tabs bankpay hide">
                    <form class="payform wx_pay01" action="#">
                        <div class="clear"></div>
                        <label id="show_fq" class="lab">
                            <input name="qfqbanklist" type="radio" value="CCBINS" checked="checked"/>
                            <div class="bank jshy fl">
                                <b class="ico_fq"></b>
                            </div>
                            <span style="color:#42b5e5;position:relative;top:15px;left:15px;font-size:16px;">
                        	</label></br>
                             <dl>
                                <dd>选择分期数：
                                    <select class="staging" id="INS" style="margin: 0 0 60px 22px; display:none;">
                                        <option selected="selected" value="3">使用三个月建行分期付款</option>
                                        <option value="6">使用六个月建行分期付款</option>
                                    </select>
                                </dd>
                            </dl>
                        
                        <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="qgqPay"></a></div>
                    </form>
                </div>
                <!--上海银行借记卡-->
                <div class="tabs bankpay <%=payhide52 %>">
                    <form class="payform wx_pay01" action="#">
                        <div class="clear"></div>
                        <label class="lab">
                            <input name="bank" type="radio" value="SHPay" checked=checked/>
                            <div class="bank wx fl">
                                <b class="sbank-icon"></b>
                            </div>
                        </label></br>
                        <div class="gopaybox"><input class="" type="submit" value="同意以下条款，去付款" /><a href="javascript:;" id="BOSHDebitPay"></a></div>
                    </form>
                </div>
                
            </div>
            <script>
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
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            if (S_Pay == 0) {
                if (S_Now < S_Yfk) {
                    alert("本次付款金额少于最低预付款！");
                    return false;
                }
            }
            top.location = "/newpage/pay/alipay.aspx?orderid=" + $('#OrderId').val() + "m=alipay&p=" + $('input[name="bank"]:checked').val() + "&amt=" + $('#money').val();
        })

        $('#BankPay').click(function () {
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            //if (S_Pay == 0) {
            //    if (S_Now < S_Yfk) {
            //        alert("本次付款金额少于最低预付款！");
            //        return false;
            //    }
            //}
            top.location = "/newpage/pay/CCBPay.aspx?orderid=" + $('#OrderId').val() + "&amt=" + $('#money').val() + "&bankname=" + $('input[name="banklist"]:checked').val();

            
        })

        $('#qgqPay').click(function () {
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            var ins = Number($("#INS").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            top.location = "/newpage/pay/CCBPay.aspx?orderid=" + $('#OrderId').val() + "&amt=" + $('#money').val() + "&bankname=" + $('input[name="qfqbanklist"]:checked').val()+"&INS="+ins;

            
        })
        
        $('#BOSHDebitPay').click(function () {
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            top.location = "/newpage/pay/BOSHDebitPay.aspx?orderid=" + $('#OrderId').val() +"&amt=" + $('#money').val();
        })


        function PuFa() {
            var S_Now = Number($("#money").val());
            $("#OrderAmt").val(S_Now);
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

        $('#jtordersubmit').click(function () {
            $("#jtordersubmit").val("付款中，请稍候...");
            $("#jtordersubmit").attr('disabled', "true");
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            if ($("#cardNo").val() == "") {
                alert("请填写您的信用卡卡号");
                return false;
            }
            if ($("#expireDate").val() == "") {
                alert("请填写您的卡片有效期");
                return false;
            }
            if ($("#customerName").val() == "") {
                alert("请填写您的姓名");
                return false;
            }
            if ($("#jtcode").val() == "") {
                alert("请填写动态验证码");
                return false;
            }
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            var url = "/newpage/AjaxService.aspx?action=BOCMPay&r=" + Math.random() + "&amt=" + $('#money').val();
            $.post(url, $("#submit_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/fifthstep.html?orderid=" + obj.success;
                }
                else {
					alert(obj.error);
                    $("#jtordersubmit").val("付款");
                    $('#jtordersubmit').removeAttr("disabled");
                }
            });
        })

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

        //微信扫码支付
        $('#go_WXPay').click(function () {
            var S_Pay = Number($("#P_Pay").val());
            var S_Yfk = Number($("#P_Yfk").val());
            var S_Now = Number($("#money").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            if (S_Pay == 0) {
                if (S_Now < S_Yfk) {
                    alert("本次付款金额少于最低预付款！");
                    return false;
                }
            }
            top.location = "/newpage/pay/WeixinPay.aspx?orderid=" + $('#OrderId').val() + "&amt=" + $('#money').val();
        })

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

        $("#Button2").click(function () {
            if ($("#cardNo").val() == "") {
                alert("请填写您的信用卡卡号");
                return false;
            }
            if ($("#expireDate").val() == "") {
                alert("请填写您的卡片有效期");
                return false;
            }
            if ($("#customerName").val() == "") {
                alert("请填写您的姓名");
                return false;
            }
            var S_Now = Number($("#money").val());
            if (S_Now <= 0) {
                alert("请填写您要支付的金额");
                return false;
            }
            $("#jtordersubmit").val("动态密码发送中，请稍候...");
            $("#jtordersubmit").attr('disabled', "true");
            time(this);
            var url = "/newpage/AjaxService.aspx?action=SendBOCMSMS&r=" + Math.random()+ "&amt=" + S_Now;
            $.post(url, $("#submit_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    alert("动态密码发送成功，请查收短信");
                }
                else {
                    alert("动态密码发送失败，请稍后再试");
                }
                $("#jtordersubmit").val("付款");
                $('#jtordersubmit').removeAttr("disabled");
            });

        });
	</script>
    <script type="text/javascript">
    var _hmt = _hmt || [];
    (function() {
      var hm = document.createElement("script");
      hm.src = "//hm.baidu.com/hm.js?670e68bb0a5926537ba7c720575bc7eb";
      var s = document.getElementsByTagName("script")[0]; 
      s.parentNode.insertBefore(hm, s);
    })();
</script>
</body>
</html>