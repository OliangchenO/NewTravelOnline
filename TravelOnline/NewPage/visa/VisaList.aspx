<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisaList.aspx.cs" Inherits="TravelOnline.NewPage.visa.VisaList" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
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
    <script type="text/javascript" src="/newjs/jquery.nav.js"></script>
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
	<div class="wrap">
		<div class="link-nav">
			<ul class="clearfix">
				<li><a href="/index.html" target="_self">首页</a></li>
				<span>></span>
				<li><a href="/visa.html" target="_self">签证办理</a></li>
				<span>></span>
				签证全部产品
			</ul>
		</div>
		<div class="state-wrap">
			<div id="state_content" class="menu clearfix">
                <ul id="nav">
                    <%=TravelOnline.NewPage.Class.CacheClass.Third_Visa_Tab("Visa_All")%>
                    <li class="last"></li>
                </ul>
            </div>
			<%=TravelOnline.NewPage.Class.CacheClass.Third_Visa_List("Visa_All")%>
		</div>
	</div>
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#state_content").smartFloat();
            $('#nav').onePageNav({ scrollOffset: 50, scrollThreshold: 0.1 });
        })
    </script>
</body>
</html>
