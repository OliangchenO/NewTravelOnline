﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinPay.aspx.cs" Inherits="TravelOnline.NewPage.pay.WeixinPay" %>
<%@ Register src="/NewPage/MemberFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>微信支付 - 上海青旅官网</title>
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
    <form id="form1" runat="server">
    <div>
        <div style="margin-left: 10px;color:#00CD00;font-size:30px;font-weight: bolder;">微信扫码支付</div><br/>
	    <asp:Image ID="ImgWeiXinPay" runat="server" style="width:200px;height:200px;"/>
    </div>
    </form>
</body>
</html>
