<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay.aspx.cs" Inherits="TravelOnline.WeChat.pay" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>在线支付</title>
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
<style>

.recommend_txt li, .fee_txt li {
color: #666;
font-size: 14px;
line-height: 20px;
margin-bottom: 6px;
position:inherit;
padding-left: 0px;
list-style-type:none;
}
</style>
<script type="text/javascript" src="../Scripts/jquery-1.6.min.js">
    alert("加载中...");
//    var url = "http://www.scyts.com/wechat/WeiXinGetOpenIdaspx.aspx?flag=true";
    $.post("http://www.scyts.com/wechat/WeiXinGetOpenIdaspx.aspx",
    {
        name: "Donald Duck",
        city: "Duckburg"
    },
    function () {
        alert("\n状态：" + obj.success);
    });
//    $.post(url, function (obj) {
//            alert("go!!!");
//            if (obj.success) {
//                alert(obj.success);
//            }
//            else {
//                alert("获取微信信息失败，请稍后再试");
//            }
//        }
//                              );
</script>

<!--百度统计3.0 账号:shbvip-scyts，更新日期2016/1/27-->
        <script>
var _hmt = _hmt || [];
(function() {
  var hm = document.createElement("script");
  hm.src = "//hm.baidu.com/hm.js?3d7ea07f7149cb10bd17920587e5985b";
  var s = document.getElementsByTagName("script")[0];
  s.parentNode.insertBefore(hm, s);
})();
</script>
	<!--百度统计3.0 END-->
	
	<!--百度统计3.0 账号:上海中国青年旅行社，更新日期2016/1/27-->
		<script>
		var _hmt = _hmt || [];
		(function() {
		  var hm = document.createElement("script");
		  hm.src = "//hm.baidu.com/hm.js?a8456f78e5a9a51324fa762afc0390db";
		  var s = document.getElementsByTagName("script")[0]; 
		  s.parentNode.insertBefore(hm, s);
		})();
		</script>
	<!--百度统计3.0 END-->
	
	<!--百度统计3.0 账号:上海青旅seo，更新日期2016/1/27-->
				<script>
		var _hmt = _hmt || [];
		(function() {
		  var hm = document.createElement("script");
		  hm.src = "//hm.baidu.com/hm.js?d3779a2d6ba03f2452d29c67eeaa3a08";
		  var s = document.getElementsByTagName("script")[0]; 
		  s.parentNode.insertBefore(hm, s);
		})();
		</script>

	<!--百度统计3.0 END-->
	
</head>
<body id="mainbody">
<form id="form1" runat="server"><%--onsubmit="javascript:return check_null(this);"--%>
<!-- BEGIN HEADER -->
<div id="header" class="pre-header" style="position: fixed; top: 0px; left: 0px;width:101%">
    <div class="container">
        <div class="row" style="height:24px">
            <a class="icon_back" href="javascript:;" onclick="javascript:history.go(-2)"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename">在线支付</div>
        </div>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="P_OrderId" name="P_OrderId" type="hidden" value="<%=OrderId %>"/>
        <input id="P_LineName" name="P_LineName" type="hidden" value="<%=LineName %>"/>
        <input id="P_Price" name="P_Price" type="hidden" value="<%=Price %>"/>
        <input id="P_Date" name="P_Date" type="hidden" value="<%=BeginDate %>"/>
        <input id="P_Nums" name="P_Nums" type="hidden" value="<%=Nums %>"/>
        <input id="P_Pay" name="P_Pay" type="hidden" value="<%=Pay %>"/>
        <input id="P_Yfk" name="P_Yfk" type="hidden" value="<%=yfk %>"/>
        <input id="P_Yue" name="P_Yue" type="hidden" value="<%=YeE %>"/>
        <input id="CMB_PayNo" name="CMB_PayNo" type="hidden" value=""/>
        <input id="P_AutoId" name="P_AutoId" type="hidden" value="<%=AutoId %>"/>
    </div>
</div>
<!-- END HEADER -->
<div id="main_view" style="margin-top: 45px;margin-bottom: 50px;">
    <div>
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>订单详情</h3>
                    <div>
                    <ul class=payul>
                    <li class="<%=hide %>"><div class=tname>旅游线路：</div><div class=tinfo><%=LineName%></div></li>
                    <li class="<%=hide %>"><div class=tname>出发日期：</div><div class=tinfo><%=BeginDate%></div></li>
                    <li class="<%=hide %>"><div class=tname>订单号：</div><div class=tinfo><%=AutoId %></div></li>
                    <li class="<%=hide %>"><div class=tname>预订人数：</div><div class=tinfo><%=Nums%>人 <%=NumsInfo%></div></li>
                    <li><div class=tname>订单金额：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="spanAmount"><%=Price%></span> </div></li>
                    <%=FkInfo%>
                    <li class="<%=hide6 %>">
                        此产品为预购产品，您现在需支付的99元将作为定金，2015年11月11日当天付清余款，取消定单，定金不予退还
                    </li>
                    <li><div class=tname>已付款：</div><div class=tinfo><span class="base_price02">&yen;</span><span class="base_price02" id="span2"><%=Pay%></span></div></li>
                    <li><div class=tname>本次付款：</div><div class=tinfo><span class="base_price02" style="FONT-SIZE: 20px;">&yen;</span><input id="P_PayNow" name="P_PayNow" readonly type="text" class="easyui-numberbox" precision="0" max="999999" size="7" maxlength="9" style="WIDTH: 100px;text-align:center;FONT-WEIGHT: bold; FONT-SIZE: 20px; color:#e56700;BORDER-BOTTOM: #196297 1px solid;BORDER-LEFT: #ffffff 1px solid;BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid;" value="<%=YeE %>"/></div></li>
                    <li class="<%=hide7 %>">
                        此产品为拼团产品，您现在需支付<%=YeE %>元将作为定金，拼团成功客服会通知您付清余款。
                    </li>
                    </ul>
                    </div>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>支付方式</h3>
                    <div>
                    <ul class=payul>
                    <li class="<%=hideAliPay %>"><div class=tname></div><div class=banks><input id="Radio1" type="radio" name="paytype" value="AliPay" <%=AliPayCheck %> /><label for="Radio1" class=banklogo>&nbsp;<IMG for="Radio1" src="/Images/AliPay.jpg"></label>&nbsp;</div></li>
                    <li class="<%=hideSHRCBPay %>"><div class=tname></div><div class=banks><input type="radio" name="paytype" value="SHRCB" id="Radio26" checked="checked" /><label for="Radio25" class=banklogo>&nbsp;<IMG src="/Images/bank_ns.png"></label>&nbsp;</div></li>
                    <li  class="<%=hideWeiXinPay %>"><div class=tname></div><div class=banks><input id="Radio8" type="radio" name="paytype" value="WeiXinPay" <%=WeiXinPayCheck %> /><label for="Radio1" class=banklogo>&nbsp;<IMG for="Radio1" src="/Images/WeiXinPaylogo.jpg"></label>&nbsp;</div></li>
                    <li class="<%=hidePuFaPay %>"><div class=tname></div><div class=banks><input id="Radio13" type="radio" name="paytype" value="PuFaPay" <%=PuFaPayCheck %> /><label for="Radio1" class=banklogo>&nbsp;<IMG for="Radio1" src="/image/spdcard.png"></label>&nbsp;</div></li>
                    <li id="JHPay" name="fq" class="<%=hideCcbPay %>"><div class=tname></div><div class=banks><input id="Radio15" type="radio" name="paytype" value="CCBINSPay" <%=CcbPayCheck %> /><label for="Radio1" class=banklogo>&nbsp;<IMG for="Radio1" src="/images/JH_PaymentPay.png"></label><span style="margin-left:10px;color:#2382d9;"></span>&nbsp;</div></br>
                    <dl style="display:none;">
                        <dd>选择分期数：
                            <select class="staging" id="INS" name="INS" style="margin-left: 22px;">
                                <option selected="selected" value="3">使用三个月建行分期付款</option>
                                <option value="6">使用六个月建行分期付款</option>
                            </select>
                        </dd>
                    </dl></li>
                    <li class="<%=hidePingAn %>"><div class=tname></div><div class=banks><input type="radio" name="paytype" value="SPABANK" id="Radio7" <%=PingAnCheck %> /><label for="Radio7" class=banklogo>>&nbsp;<IMG src="/Images/Alipay/bank_40.png"></label><input type="radio" name="paytype" value="SDB" id="Radio2" ><label class=banklogo><IMG src="/Images/Alipay/bank_26.png"></label>&nbsp;</div></li>
                    <li><div class=tname></div><div class=banks>&nbsp;</div></li>
                    <li class="<%=hideAliAll %>">
                        <div class=tname></div>
                        <div class=banks>
                            <input type="radio" name="paytype" value="IcbcPay" id="Radio3"/><label for="Radio3" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_03.png"></label><br><br>
                            <input type="radio" name="paytype" value="CMB" id="Radio9"/><label for="Radio9" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_07.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="CCB" id="Radio21"/><label for="Radio21" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_05.png"></label><br><br>
                            <input type="radio" name="paytype" value="BOCB2C" id="Radio22"><label for="Radio22" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_25.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="ABC" id="Radio4"/><label for="Radio4" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_15.png"></label><br><br>
                            <input type="radio" name="paytype" value="COMM" id="Radio10"/><label for="Radio10" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_09.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="PSBC-DEBIT" id="Radio20"/><label for="Radio20" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_34.png"></label><br><br>
                            <input type="radio" name="paytype" value="CEBBANK" id="Radio23"/><label for="Radio23" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_18.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="SPDB" id="Radio5"/><label for="Radio5" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_24.png"></label><br><br>
                            <input type="radio" name="paytype" value="GDB" id="Radio11"><label for="Radio11" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_16.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="CITIC" id="Radio19"/><label for="Radio19" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_23.png"></label><br><br>
                            <input type="radio" name="paytype" value="CIB" id="Radio24"/><label for="Radio24" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_17.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="SDB" id="Radio6"><label for="Radio6" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_26.png"></label><br><br>
                            <input type="radio" name="paytype" value="CMBC" id="Radio12"/><label for="Radio12" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_31.png"></label>
                            <br><br>
                            <input type="radio" name="paytype" value="BJBANK" id="Radio18"/><label for="Radio18" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_32.png"></label><br><br>
                            <input type="radio" name="paytype" value="HZCBB2C" id="Radio25"/><label for="Radio25" class=banklogo>&nbsp;<IMG src="/Images/Alipay/bank_42.png"></label>
                        </div>
                    </li>
                    </ul>
                    </div>
                </div>
                <script>
                    $(function(){                              //显示隐藏分期
                        var choice = $('#JHPay').find('dl');
                        $('.payul').delegate('li', 'click',function(){ 
                            var id = $(this).attr("id");
                            if(id == 'JHPay'){
                                choice.show();
                            }else{
                                choice.hide();
                            }
                        })
                    })
                </script>
            </div>
        </div>
        <div class="pre-footer order-footer"  style="position: fixed; bottom: -1px; left: 0px;width:101%">
            <div class="container">
                <div class="row">
                    <div class="col-xs-12" style="text-align:center"><asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" class="yd cur"><i class="fa fa-chevron-circle-right"></i> 现在支付</asp:LinkButton></div>
                </div>
            </div>
        </div>
    </div>
</div>
</form>
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
    function do_onload() {
        var d = new Date();
        //给出定单日期
        fm.date.value = d.getYear() * 10000 + (1 + d.getMonth()) * 100 + d.getDate();
        //给出不重复的定单号
        var s = d.getYear() * 366 + d.getMonth() * 31 + d.getDate();
        s = (s * 24 + d.getHours()) * 60 + d.getMinutes();
        s = s * 60 + d.getSeconds();
        fm.billno.value = s.toString().substring(1);
    }

    function check_null(theForm) {
        var S_Yfk = 0;
        var S_Nums = 0;
        var S_Yue = 0;
        var S_Pay = 0;
        var S_Now = 0;

        S_Yfk = Number($("#P_Yfk").val());
        S_Yue = Number($("#P_Yue").val());
        S_Now = Number($("#P_PayNow").val());
        S_Pay = Number($("#P_Pay").val());

        if (S_Now == 0) {
            alert("请输入本次付款金额！");
            return false;
        }

        if (S_Now > S_Yue) {
            alert("本次付款金额大于应付余额！");
            return false;
        }

        if (S_Pay == 0) {
            if (S_Now < S_Yfk) {
                alert("本次付款金额少于最低预付款！");
                return false;
            }
        }
    }

    $(document).ready(function () 
    {
        var d = new Date();
        //给出不重复的定单号
        var s = d.getYear() * 366 + d.getMonth() * 31 + d.getDate();
        s = (s * 24 + d.getHours()) * 60 + d.getMinutes();
        s = s * 60 + d.getSeconds();
        $("#CMB_PayNo").val(s.toString().substring(1));

        $('input').iCheck
        ({
            checkboxClass: 'icheckbox_square-grey',
            radioClass: 'iradio_square-grey',
            increaseArea: '20%'
        });
    })
</script>
</body>
</html>

