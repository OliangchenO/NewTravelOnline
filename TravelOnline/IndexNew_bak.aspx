<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexNew.aspx.cs" Inherits="TravelOnline.IndexNew" %>
<%@ Register src="NewPage/IndexHeader.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="NewPage/IndexFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="NewPage/LeftSlide.ascx" tagname="LeftSlide" tagprefix="uc3" %>
<%@ Register src="NewPage/IndexHot.ascx" tagname="IndexHot" tagprefix="uc4" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="keywords" content="<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link rel="shortcut icon" href="">
    <link href="newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="newcss/page.css" rel="stylesheet" type="text/css" />
    <script src="newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="newjs/common.js"></script>
	<script type="text/javascript" src="newjs/My97DatePicker/WdatePicker.js"></script>
    <style>.wrap .index-spring .index-spring-l .pro_list ul dl dt img,.wrap .index-top .top-sale .product_list li dl dt a img,.wrap .index-trip .product-wrap .product-list li dl dt a img {width: 100%;height:150px}
    </style>
    <!--[IF IE 6]>
		<script type="text/javascript" src="newjs/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
<body id="index">
    <uc1:Header ID="Header1" runat="server" />
	<!--正文内容Begin-->
	<div class="wrap">
		<div class="index-top clearfix">
            <uc3:LeftSlide ID="LeftSlide1" runat="server" />
            <div class="top-ad m">
				<%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Slide()%>
				<div class="top-sale">
					<div class="top-sale-l">
                        <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Sell()%>
					</div>
				</div>
			</div>
            <div class="side-ad">
                <%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Banner()%>
			</div>
        </div>

        <!--当季推荐-->
		<div class="index-spring clearfix">
			<div class="index-spring-l fl">
				<div class="top-wrap">
					<h2></h2>
				</div>
				<div class="index-spring-ad">
					<%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Season()%>
				</div>
				<div class="change_pro">
					<div id="change_Bg" class="exchange">换一批</div>
				</div>
				<div class="pro_list">
					<ul id="pro_mrTop">
						<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Season()%>
					</ul>
				</div>
			</div>
            <uc4:IndexHot ID="IndexHot1" runat="server" />
		</div>
        <!--当季推荐 End-->

        <!--玩转自由行-->
		<div class="index-trip">
			<div class="tab-wrap top-ziyou clearfix">
				<h2 class="relative-box fl">玩转自由行</h2>
				<ul id="chujing_area" class="fl clearfix">
					<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_FreeTour")%>
                    <a href="/freetour.html" class="pro-all" target="_blank">更多自由行产品>></a>
				</ul>
			</div>
			<div class="product-wrap bottom-free clearfix">
				<div class="theme-trip fl">
				
					<a href="javascript:;" target="_blank" title="关岛"><img src="image/ad-tour.png" alt="关岛"></a>
				</div>
				<!--list推荐-->
                <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_FreeTour")%>
			</div>
		</div>
		<!--玩转自由行 End -->
		
		<!--出境旅游-->
		<div class="index-trip">
			<div class="tab-wrap clearfix">
				<h2 class="relative-box fl">出境旅游</h2>
				<ul id="chujing_area" class="fl clearfix">
                    <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_Outbound")%>
                    <a href="/outbound.html" class="pro-all" target="_blank">更多出境游产品>></a>
				</ul>
			</div>
			<div class="product-wrap clearfix">
                <div class="theme-trip fl">
					<a href="javascript:;" target="_blank" title="关岛"><img src="image/ad-guonei.png" alt="关岛"></a>
				</div>
				<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_Outbound")%>
			</div>
		</div>
        <!--出境旅游 End -->

		<!--国内旅游-->
		<div class="index-trip">
			<div class="tab-wrap top-guonei clearfix">
				<h2 class="relative-box fl">国内旅游</h2>
				<ul id="guonei_area" class="fl relative-box clearfix">
                    <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_Inland")%>
                    <a href="/inland.html" class="pro-all" target="_blank">更多国内游产品>></a>
				</ul>
			</div>
			<div class="product-wrap bottom-guonei clearfix">
                <div class="theme-trip fl">
					<a href="javascript:;" target="_blank" title="云南"><img src="image/ad-chuing.png" alt="云南"></a>
				</div>
                <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_Inland")%>
			</div>
		</div>
        <!--国内旅游 End-->

		<!--自由行、邮轮、签证-->
		<div class="index-trip">
			<div class="top-group relative-box clearfix">
				<p class="ship-icon current fl"></p>
				<p class="qz-icon fl"></p>
				<ul id="group_area" class="absolute-box fl clearfix">
					<h2 class="current">邮轮</h2>
					<h2>签证</h2>
				</ul>
				<span class="pos01">|</span>
				<span class="pos02">|</span>
			</div>
			<div class="product-wrap bottom-group clearfix">
				<!--邮轮-->
				<dl class="clearfix show-group">
					<div class="freedom-ad fl">
						<a class="txt" href="javascript:;"><img src="image/ship_ad.jpg" alt="" title="" /></a>
					</div>
					<!--list邮轮-->
					<ul id="cjProduct_img" class="product-list group-list current fl clearfix relative-box">
						<div id="city_area02" class="group-city clearfix">
							<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherTab("Index_Cruise")%>
						</div>
						<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherLine_List("Index_Cruise")%>
					</ul>
				</dl>
				<!--签证-->
				<dl class="clearfix hide-group">
					<div id="qz_answer" class="freedom-ad qz-column fl">
						<h3 class="relative-box">签证办理须知</h3>
						<p><a href="javascript:;" title="签证办理需要哪些材料？" target="_blank">签证办理需要哪些材料？</a><b></b></p>
						<p><a href="javascript:;" title="签证办理说明？" target="_blank">签证办理说明？</a><b></b></p>
						<p><a href="javascript:;" title="签证办理签约方式？" target="_blank">签证办理签约方式？</a><b></b></p>
					</div>
					<!--list签证-->
					<ul id="cjProduct_img" class="product-list group-list qz-group-list current fl clearfix relative-box">
						<div class="empty"></div>
						<%=TravelOnline.NewPage.Class.CacheClass.Index_Visa()%>
						<a class="more qz-more absolute-box" href="/visa.html" target="_blank">更多签证>></a>
					</ul>
				</dl>
			</div>
		</div>
	    <!--自由行、邮轮、签证 End -->

        
    </div>
	<!--正文内容End-->
    <uc2:Footer ID="Footer1" runat="server" />
</body>
</html>
