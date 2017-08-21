<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeTour.aspx.cs" Inherits="TravelOnline.NewPage.freetour.FreeTour" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="/NewPage/FreeTourBanner.ascx" tagname="FreeTourBanner" tagprefix="uc3" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourKeywords %>" />
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
<body id="freetour">
	<!--页头Begin-->
	<uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
        <div class="aside">
            <uc3:FreeTourBanner ID="FreeTourBanner1" runat="server" />
        </div>

        <div class="content fl">
            <!--banner-->
            <div class="index-top">
                <div class="banner-box relative-box">
                    <%=TravelOnline.NewPage.Class.CacheClass.Second_Ad_Slide("N_S_FreeTour_Slide", "")%>
                </div>
            </div>
            <!--特价-->
		    <div class="sale-content">
			    <%=TravelOnline.NewPage.Class.CacheClass.Second_Line_Sell("FreeTour_Sell")%>
		    </div>

            <!--出境短线-->
			<div class="line-selected">
				<div class="tab-wrap clearfix">
					<h2 class="fl">出境短线</h2>
					<ul id="item_area" class="rl clearfix">
						<%=TravelOnline.NewPage.Class.CacheClass.Second_Line_Tab("FreeTour_01")%>
					</ul>
				</div>
				<div class="product-wrap">
                    <%=TravelOnline.NewPage.Class.CacheClass.Second_Line_List("FreeTour_01", "4")%>
                </div>
			</div>

            <!--出境长线-->
			<div class="line-selected">
				<div class="tab-wrap clearfix">
					<h2 class="fl">出境长线</h2>
					<ul id="item_area" class="rl clearfix">
						<%=TravelOnline.NewPage.Class.CacheClass.Second_Line_Tab("FreeTour_02")%>
					</ul>
				</div>
				<div class="product-wrap">
                    <%=TravelOnline.NewPage.Class.CacheClass.Second_Line_List("FreeTour_02", "4")%>
                </div>
			</div>

            <!--国内热门-->
			<div class="line-selected">
				<div class="tab-wrap clearfix">
					<h2 class="fl">国内热门</h2>
					<ul id="item_area" class="rl clearfix">
						<%=TravelOnline.NewPage.Class.CacheClass.Second_Line_Tab("FreeTour_03")%>
					</ul>
				</div>
				<div class="product-wrap">
                    <%=TravelOnline.NewPage.Class.CacheClass.Second_Line_List("FreeTour_03", "4")%>
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