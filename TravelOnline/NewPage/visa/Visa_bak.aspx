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
					<a href="javascript:;" target="_blank">更多>></a>
				</h2>
				<div class="product-wrap clearfix">
					<ul>
						<li class="hot"><a href="javascript:;" target="_blank">什么是落地签证？</a></li>
						<li><a href="javascript:;" target="_blank">签证需要哪些材料？</a></li>
						<li><a href="javascript:;" target="_blank">签证的有效期及停留期？</a></li>
						<li><a href="javascript:;" target="_blank">如果是外籍人士，是否可以代办签证？</a></li>
						<li><a href="javascript:;" target="_blank">签证办理是否要预约？</a></li>
						<li><a href="javascript:;" target="_blank">签证办理时间要多久？</a></li>
					</ul>
					<ul>
						<li class="hot"><a href="javascript:;" target="_blank">拒签后还可以继续申请签证吗？</a></li>
						<li class="hot"><a href="javascript:;" target="_blank">办理签证是不是都要去领馆面试的？</a></li>
						<li><a href="javascript:;" target="_blank">签证是否能保证通过呢？</a></li>
						<li><a href="javascript:;" target="_blank">一般拒签都有哪些原因？</a></li>
						<li><a href="javascript:;" target="_blank">什么是签证的有效次数？</a></li>
						<li><a href="javascript:;" target="_blank">怎样才能提高出签率？</a></li>
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
