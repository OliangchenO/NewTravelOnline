<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexNew_test.aspx.cs" Inherits="TravelOnline.IndexNew_test" %>
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
    <meta name="description" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="keywords" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <meta property="qc:admins" content="111753216663314363757" /> 
    <link rel="shortcut icon" href="">
    <link href="newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="newcss/page.css" rel="stylesheet" type="text/css" />
    <script src="newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="newjs/common.js"></script>
	<script type="text/javascript" src="newjs/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="newjs/jquery.cookie.js"></script>
    <style>.wrap .index-spring .index-spring-l .pro_list ul dl dt img,.wrap .index-top .top-sale .product_list li dl dt a img,.wrap .index-trip .product-wrap .product-list li dl dt a img {width: 100%;height:150px}
    </style>
    <!--[IF IE 6]>
		<script type="text/javascript" src="newjs/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
</head>
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
					<a href="http://www.scyts.com/line/13518.html" target="_blank" title="爱上巴厘岛6天4晚（雅加达转机，阿勇河漂流+蓝梦岛+黑沙滩）"><img src="image/side_tour.jpg" alt="爱上巴厘岛6天4晚（雅加达转机，阿勇河漂流+蓝梦岛+黑沙滩）"></a>
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
					<a href="http://www.scyts.com/line/17398.html" target="_blank" title="浪漫骄澳*澳大利亚悉尼黄金海岸大堡礁墨尔本东海岸全览10天8晚"><img src="image/side_outbound.jpg" alt="浪漫骄澳*澳大利亚悉尼黄金海岸大堡礁墨尔本东海岸全览10天8晚"></a>
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
					<a href="http://www.scyts.com/line/16915.html" target="_blank" title="特惠全景*云南丽江、香格里拉、大理双飞5日（丽江往返，住宿升级）"><img src="image/side_inland.jpg" alt="特惠全景*云南丽江、香格里拉、大理双飞5日（丽江往返，住宿升级）"></a>
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
						<a class="txt"><img src="image/ship_ad.jpg" alt="" title="" /></a>
					</div>
					<!--list邮轮-->
					<ul id="cjProduct_img" class="product-list group-list current fl clearfix relative-box">
						<div id="city_area02" class="group-city clearfix">
							<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherTab("Index_Cruise")%>
						</div>
						<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherLine_List("Index_Cruise")%>
					</ul>
					<a class="relative-box" href="/search/1063-0-0-0-0-0-0-0.html" target="_blank" style="left:1136px; top:-16px; color:#2382d9;">更多邮轮>></a>
				</dl>
				<!--签证-->
				<dl class="clearfix hide-group">
					<div id="qz_answer" class="freedom-ad qz-column fl">
						<h3 class="relative-box">签证办理须知</h3>
						<p><a title="很多发达国家（如美国和欧洲国家）办理签证申请都需要提前预约，如电话预约、在线预约和邮件预约。 预约时间确定才可以在预约当天去领馆交材料。需要预约时间的签证通常是需要面试的，但是目前部分申根国保留预约流程，但是已经逐渐取消面试流程，如法国和德国。
一般发展中国家如东南亚办理签证，通常都不需要预约，可直接交材料给使馆。
部分发达国家也不需要预约，如加拿大和澳大利亚，新西兰等。" target="_blank">签证办理需要哪些材料？</a><b></b></p>
						<p><a title="申办外国签证，一般都必须附有申请人的照片，有的和签证一起使用，有的贴在签证申请表格上。申请人申办签证时，必须另外备有照片。这些照片必须和护照上的照片完全一致。" target="_blank">签证照片有特殊要求吗？</a><b></b></p>
						<p><a title="一旦领馆受理了您的签证申请，不论获签与否，签证费均无法退还哦。 " target="_blank">被拒签后签证费会退还吗？  </a><b></b></p>
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
	    <!--青旅名牌-->
		<div class="index-trip">
			<div class="tab-wrap famous clearfix">
				<h3></h3>
			</div>
			<div class="product-wrap fan clearfix">
				<ul class="product-list current fl clearfix">
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/17464.html" target="_blank"><img src="image/fan02.jpg" alt="" title="尊“荣”系列*食色台湾环岛8日（2晚升级5星酒店）"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>6280<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/17464.html" title="尊“荣”系列*食色台湾环岛8日" target="_blank">尊“荣”系列*食色台湾环岛8日（2晚升级5星酒店）</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/18604.html" target="_blank"><img src="image/fan03.jpg" alt="" title="“青”松霓虹*日本金沢、白川乡、大阪、道后温泉6日"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>7499<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/18604.html" title="“青”松霓虹*日本金沢、白川乡、大阪、道后温泉6日" target="_blank">“青”松霓虹*日本金沢、白川乡、大阪、道后温泉6日</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/17540.html" target="_blank"><img src="image/fan04.jpg" alt="" title="南欧纵览*葡萄牙、西班牙、安道尔11天9晚"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>13499<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/17540.html" title="南欧纵览*葡萄牙、西班牙、安道尔11天9晚" target="_blank">南欧纵览*葡萄牙、西班牙、安道尔11天9晚</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/18205.html" target="_blank"><img src="image/fan01.jpg" alt="" title="澳洲大堡礁墨尔本东海岸全览10日经典游"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>15700<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/18205.html" title="澳洲大堡礁墨尔本东海岸全览10日经典游" target="_blank">澳洲大堡礁墨尔本东海岸全览10日经典游</a>
							</dd>
						</dl>
					</li>
					<li class="no-mrr">
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/16501.html" target="_blank"><img src="image/fan05.jpg" alt="" title="一见青“新”新西兰南北岛观鲸温泉冰川魔戒11天8晚（纽航）"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>22800<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/16501.html" title="一见青“新”新西兰南北岛观鲸温泉冰川魔戒11天8晚（纽航）" target="_blank">一见青“新”新西兰南北岛观鲸温泉冰川魔戒11天8晚（纽航）</a>
							</dd>
						</dl>
					</li>
				</ul>
			</div>
		</div>
        <!--青旅名牌 End -->
        
    </div>
	<!--正文内容End-->
    <uc2:Footer ID="Footer1" runat="server" />
</body>
</html>
