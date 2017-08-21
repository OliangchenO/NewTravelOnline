<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cruise.aspx.cs" Inherits="TravelOnline.NewPage.cruise.cruise" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.CruisesTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.CruisesKeywords %>" />
    <link rel="shortcut icon" href="">
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <script src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/common.js"></script>
	<script type="text/javascript" src="/newjs/My97DatePicker/WdatePicker.js"></script>
	<!--[IF IE 6]>
		<script type="text/javascript" src="js/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
<body id="cruise">
	<!--页头Begin-->
	<uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap">
		<!--搜索与banner-->
		<div class="index-top">
            <%=TravelOnline.NewPage.Class.CacheClass.Second_Ad_Slide("N_S_Cruise_Slide", "cruise")%>
		</div>
		<!--邮轮航线-->
		<div class="shipping-line">
			<h1>邮轮航线</h1>
			<div id="change_line" class="tab-wrap">
				<ul id="item_area" class="clearfix">
					<%=TravelOnline.NewPage.Class.CacheClass.Second_Cruise_Tab("Cruise_Best")%>
				</ul>
			</div>
			<div class="product-wrap clearfix">
				<%=TravelOnline.NewPage.Class.CacheClass.Second_Cruise_List("Cruise_Best")%>
			</div>
		</div>
		<!--邮轮品牌-->
		<div class="shipping-brand">
			<h2>邮轮品牌</h2>
			<span></span>
			<ul class="clearfix">
				<li><a><img src="../image/youlun/ship-brand01.jpg" alt=""></a></li>
				<li><a><img src="../image/youlun/ship-brand02.jpg" alt=""></a></li>
				<li><a><img src="../image/youlun/ship-brand03.jpg" alt=""></a></li>
				<li><a><img src="../image/youlun/ship-brand04.jpg" alt=""></a></li>
				<li><a><img src="../image/youlun/ship-brand05.jpg" alt=""></a></li>
			</ul>
		</div>
		<!--常见问题-->
		<div class="question relative-box">
			<h2>常见问题</h2>
			<span></span>
			<a class="more" href="javascript:;" target="_blank">查看更多问题>></a>
			<div class="q-list">
				<ul id="question_list" class="clearfix">
					<li class="QA1 clearfix">
						<div class="QA-box">
							<div class="q-word">邮轮旅游适合哪些人？</div>
							<div class="a-word" title="">如果你喜欢轻松旅行、吃喝享乐，或者拖家带口，邮轮很适合你；如果你喜欢的是探索、挑战极限，那就不一定会对邮轮感兴趣了。不过现在邮轮乘客的年龄正在越来越年轻化。另外，单身乘客不但价格通常加倍，而且也会比较无...</div>
						</div>
						<div class="QA-box">
							<div class="q-word">邮轮上需要付小费吗？</div>
							<div class="a-word" title="">邮轮的小费情况是根据邮轮公司不同，支付方式也各不相同。例如：美国皇家加勒比公司的邮轮产品基本多为预付方式，在支付船票时就需要支付小费。比如欧洲国家邮轮公司则不需要提前支付小费，也就是说船票里是不涵盖小费...</div>
						</div>
						<div class="QA-box">
							<div class="q-word">邮轮上有中文服务吗？</div>
							<div class="a-word" title="">以中国港口为母港的邮轮会配备部分中国船员。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">如何挑选仓位？</div>
							<div class="a-word" title="">首先要决定的是内舱，海景舱，还是带阳台舱。如果除了睡觉很少呆在房间，那么内舱绝对是最好选择，想看景就到酒吧游泳场去就行了，而且在茫茫大海上没什么景，如果是去阿拉斯加，经济允许的话可以定个阳台。</div>
						</div>
					</li>
					<li class="QA2 clearfix">
						<div class="QA-box">
							<div class="q-word">出团通知在哪里？</div>
							<div class="a-word" title="">出行前1-3个工作日，客服专员将以邮件、传真方式为您发送书面的出团通知，并与您电话或者短信确认，提醒您相关旅游注意事项。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">坐邮轮会不会晕船？是不是楼层越高越晃呢？？</div>
							<div class="a-word" title="">一般情况下在船上是不会晕船的，除非是台风季节风浪很大的时候有点晃。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">船上怎么消费？用人民币可以吗？</div>
							<div class="a-word" title="">一船上建议带好美金或者有美金账户的双币信用卡，VISA，MASTER卡。不可以用人民币。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">岸上观光可以自己游玩吗？</div>
							<div class="a-word" title="">我们是团队旅游，所有的都需要团进团出。</div>
						</div>
					</li>
					<li class="QA3 clearfix">
						<div class="QA-box">
							<div class="q-word">船上的免税店东西便宜还是岸上的便宜？</div>
							<div class="a-word" title="">这个您可以先看下船上的免税店的价格，等上岸后再比较下岸上的免税店。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">船上有信号吗？手机可以对外打电话吗？</div>
							<div class="a-word" title="">船在公海的时候是没有信号的，建议开通国际漫游，等靠岸后可以使用手机。</div>
						</div>
						<div class="QA-box">
							<div class="q-word">房间里面可以抽烟吗？</div>
							<div class="a-word" title="">邮轮上所有的房间都是禁止吸烟的，只有甲板和统一的区域才可以吸烟。 </div>
						</div>
						<div class="QA-box">
							<div class="q-word">船上都有哪些活动？都是免费的吗？</div>
							<div class="a-word" title="">船上的活动各式各样，泳池、健身房、大剧院、篮球场等，船上如果点一些含酒精类饮料、做SPA之类的话是需要收费的，其余基本上都是免费的。</div>
						</div>
					</li>
				</ul>
				<ol id="question" class="clearfix">
					<li class="current"></li>
					<li></li>
					<li></li>
				</ol>
			</div>
		</div>
	</div>
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
</body>
</html>
