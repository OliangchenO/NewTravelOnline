<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visa.aspx.cs" Inherits="TravelOnline.NewPage.visa.Visa" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="/NewPage/VisaBanner.ascx" tagname="VisaBanner" tagprefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.VisaTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.VisaKeywords %>" />
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
<body id="visa">
	<!--页头Begin-->
	<uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
        <div class="aside">
            <uc3:VisaBanner ID="VisaBanner1" runat="server" />
        </div>

		<!--搜索与banner-->
		<div class="index-top clearfix">
            <%=TravelOnline.NewPage.Class.CacheClass.Second_Ad_Slide("N_S_Visa_Slide", "")%>
		</div>
		<!--热门签证-->
		<div class="visa-content clearfix">
			<div class="visa-Success fl">
				<img src="../image/visa/success.png" alt="">
			</div>
			<div class="visa-hot fl">
				<h2 class="relative-box">热门签证
					<a href="/visalist.html">更多签证>></a>
				</h2>
				<div class="product-wrap clearfix">
					<%=TravelOnline.NewPage.Class.CacheClass.Index_Visa()%>
				</div>
			</div>
		</div>
		<!--签证常见问题-->
		<div class="visa-question clearfix">
			<div class="visa-setp fl">
				<img src="../image/visa/visa_step.png" alt="">
			</div>
			<div class="common-question fl">
				<h2 class="relative-box">签证办理常见问题
				</h2>
				<div class="product-wrap clearfix">
					<ul>
						<li class="hot">
							<a href="javascript:;">什么是落地签证？</a>
							<span title="所谓落地签证,是指申请人不能直接从所在国家取得前往国家的签证,而是持护照和该国有关机关发给的入境许可证明等抵达该国口岸后,再签发签证。落地签证通常是单边的。
	落地签证的国家目前有泰国、越南、柬埔寨、马尔代夫、印尼、尼泊尔，韩国济州岛可以免签。但最好还是准备第三国签证和相应的国际机票作为资料。
	特殊情况的落地签证：在很多特殊的情况下，如发生战争，航班分流迫降，直系亲属的突然死亡等等，有些国家是准许中国公民在不可预见的情况下申请落地签证的。如果没有很特殊的情况，还是不要以身试法的好。普通的旅游或商务目的是不能拿到豁免的。
	申请办理落地签证需注意事项：申请人一定要注意,办理这种落地签证的国家是很少的。申请人在出发前一定要搞清楚后,方能启程,千万不能盲目行动,以免抵达后得不到落地签证。
	补充：“免签国家”是指不用申请目的地国家的签证，即可进入旅游的国家，现在暂时只有毛里求斯和韩国，最长可停留15天。“过境免签”是指从该国经转前往其他目的地，不必申请该国签证可短暂停留，在韩国和新加坡，中国公民可享有此待遇。">所谓落地签证,是指申请人不能直接从所在国家取得前往国家的签证,而是持护照和该国有关机关发给的入境许可证明等抵达该国口岸后,再签发签证。落地签证通常是单边的。
	</span>
						</li>
						<li>
							<a href="javascript:;">签证需要哪些材料？</a>
							<span title="">签证申请材料规定涉及面广，又经常变化。详细的签证材料说明可以到同程旅游的签证详细页面阅读。</span>
						</li>
						<li>
							<a href="javascript:;" target="_blank">出行前，先签证还是先买机票？</a>
							<span title="此问题，成为让不少人苦恼的问题。但是“谁先谁后”的问题需要分两种情况说明。
   一种情况是，你所要申请签证的领事馆要求你出示机票原件，如泰国，你就必须先购买机票。另一种情况：像欧洲拒签率比较大，所以他们只需要出示机票预订单就可以了。如法国、意大利、德国等不能办理落地签证的欧洲国家，在出行前，旅客还需在本国先购机票，再凭着机座订单去办理签证。
由于我国边检部门需要旅客在出境时出示他国签证，所以市民出游可办理落地签证的东南亚的国家，需先办理第三国家的签证才能出境。旅客为了节省时间，通常会先办第三国的签证进行“第一次”旅游，再转机到这些国家，再办理落地签证。去落地签证国家的时候，出示回程机票即可办理签证。旅行社签证组人员建议先办理好签证，同时订机票但不出票，待签证出来后，再购买机票。">此问题，成为让不少人苦恼的问题。但是“谁先谁后”的问题需要分两种情况说明。
   一种情况是，你所要申请签证的领事馆要求你出示机票原件，如泰国，你就必须先购买机票。另一种情况：像欧洲拒签率比较大，所以他们只需要出</span>
						</li>
					</ul>
					<ul>
						<li class="hot">
							<a href="javascript:;">拒签后还可以继续申请签证吗？</a>
							<span>驻海外使领馆对任何签证的申请，都有绝对的决定权。如果签证申请被使领馆以某种理由拒绝，申请人可以提交重新申请的权利。但是如果之前有过拒签记录，短期内不建议再次申请同一国签证。</span>
						</li>
						<li class="hot">
							<a href="javascript:;">签证是否能保证通过呢？</a>
							<span>所有的签证都不能保证通过，都是根据客人自己提供的材料由使馆去决定是否可以获得签证的。</span>
						</li>
						<li>
							<a href="javascript:;">如果是外籍人士，是否可以代办签证？</a>
							<span>外籍人士的签证我们是无法代办的哦，具体办理事宜，请您咨询相关领馆。外籍人士的中国籍配偶，需要办理签证的，我司可以代办。</span>
						</li>
					</ul>
				</div>
			</div>
		</div>
	</div>
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
</body>
</html>
