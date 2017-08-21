<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FourthStep.aspx.cs" Inherits="TravelOnline.NewPage.order.FourthStep" %>
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
    	<div class="line-b"></div>
        <div class="orderbox">
            <ul>
                <li class="cur"><i class="ci gc"><b></b></i><p class="gp">选择线路</p></li>
                <li class="cur"><i class="ci gc"><b></b></i><p class="gp">填写信息</p></li>
                <li class="cur"><i class="ci gc"><b></b></i><p class="gp">预定成功</p></li>
                <li class="cur"><i class="ci gc"><b></b></i><p class="gp">在线支付</p></li>
                <li><i class="ci">5</i><p>支付成功</p></li>
            </ul>
        </div>
		<div class="pay-finish">
        	<i class="sbi"></i>
        	<h2 class="fail">抱歉，支付失败！</h2>
            <dl>
            	<dd>
                	<p class="code">信息代码：<em><%=ErrorCode %></em><br/>
                	<p class="reason">由于<%=ErrorInfo %>原因，造成您暂时无法支付，请稍后尝试。</p>
                </dd>
            </dl>
            <dl>
            	<dd>您可以<a class="<%=hide %>" href="/pay.html?orderid=<%=OrderId %>">重新尝试</a>或<a href="/index.html">返回首页</a></dd>
            </dl>
        </div>
	</div>
	<!--正文内容End-->
    <!--页尾Begin-->
	<uc2:Footer ID="Footer1" runat="server" />
    <script type="text/javascript">
        $('#onlinepay').click(function () {
            top.location = "/newpage/pay/alipay.aspx?orderid=" + $('#OrderId').val() + "m=alipay&p=" + $('input[name="bank"]:checked').val();
            //window.open("/newpage/pay/alipay.aspx?orderid=" + $('#OrderId').val() + "m=alipay&p=" + $('input[name="bank"]:checked').val());
        })

	</script>
</body>
</html>