<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Journals.aspx.cs" Inherits="TravelOnline.NewPage.journals.Journals" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourTitle%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourKeywords %> />
    <link rel="shortcut icon" href="">
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/pageJournal.css" rel="stylesheet" type="text/css" />
    <script src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/My97DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    $(function () {
        setCookie("redirectUrl", "/NewPage/journals/Journals.aspx");
        onAsideLeft();
        onBanner();
        onTab("HotJournals");
        onTab("InlandJournals");
        onTab("OutboundJournals");
    })

    //设置Cookie
    function setCookie(name, value) {
        var exp = new Date();
        exp.setTime(exp.getTime() + 24 * 3600000); //24小时
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
    }

    function onAsideLeft() {
        var obj = <%=new HtmlString(Second_Ad_Slide("N_S_Journal_SL", 1))%>;
        if(obj.length==0){return;}
        var tbody = "";
        $.each(obj, function (n, value) {
            tbody += "<a href=\""+value.AdPageUrl+"\"><img src=\""+value.AdPicUrl+"\" alt=\""+value.AdName+"\" title=\""+value.AdName+"\"></a>";
        });
        $("#aside-left").html(tbody);
    }

    function onBanner() {
        var obj = <%=new HtmlString(Second_Ad_Slide("N_S_Journal_Slide", 5))%>;
        if(obj.length==0){return;}
        var tbody = "";
        tbody+=  "<ul class=\"banner-list-box clearfix\" id=\"indexTopbanner\">";
        $.each(obj, function (n, value) {
            var trs = "";
            if (n == 0) trs += "<li class=\"current\"><a href=\""+value.AdPageUrl+"\" target=\"_blank\"><img src=\""+value.AdPicUrl+"\" alt=\""+value.AdName+"\" title=\""+value.AdName+"\"></a></li>";
            else trs += "<li><a href=\""+value.AdPageUrl+"\" target=\"_blank\"><img src=\""+value.AdPicUrl+"\" alt=\""+value.AdName+"\" title=\""+value.AdName+"\"></a></li>";
            tbody += trs;
        });
        tbody+="</ul>";
        tbody+=  "<ol id=\"bannerGoods\" class=\"absolute-box clearfix\">";
        $.each(obj, function (n, value) {
            var trs = "";
            if (n == 0) trs += "<li class=\"current\"></li>";
            else trs += "<li></li>";
            tbody += trs;
        });
        tbody+="</ol>";
        $("#banner").append(tbody);
    }

    function onTab(recomtype){
        var obj,item,title;
        switch (recomtype) {
            case "HotJournals": obj = <%=new HtmlString(Recom_Journals_List("HotJournals",5))%>;item = $("#item_area_hotjournals");title="热门游记"; break;
            case "InlandJournals": obj = <%=new HtmlString(Recom_Journals_List("InlandJournals",5))%>;item = $("#item_area_inlandjournal");title="国内游记"; break;
            case "OutboundJournals": obj = <%=new HtmlString(Recom_Journals_List("OutboundJournals",5))%>;item = $("#item_area_outboundjournals");title="出境游记"; break;
            default: return;
        }
        if(obj.length==0){return;}
        var tbody = "";
        tbody += "<div class=\"tab-wrap clearfix\"><h2 class=\"fl\">"+ title +"</h2></div>"
        tbody+="<div class=\"product-wrap\"><ul id=\"changeProduct_list\" class=\"product-list current clearfix\">";
        $.each(obj, function (n, value) {
            var trs="";
            trs += "<li class=\"\"><dl><dt><p class=\"clearfix\"></p>";
            trs += "<a href='/showjournal/"+value.id+".html' target=\"_blank\"><img src=\""+value.coverpicurl+"\" title=\""+ value.title +"\"></a>";
            trs += "</dt><dd><a href=\"/showjournal/"+value.id+".html' title=\"\" target=\"_blank\">"+value.title+"</a></dd></dl></li>";
            tbody += trs;
        });
        tbody+="</div></div>";
        item.append(tbody);
    }
</script>
	<script type="text/javascript" src="/newjs/common.js"></script>
</head>
<body id="freetour">
	<!--页头Begin-->
	<uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
        <div class="content fl">
            <div class="aside" id="aside-left">
            </div> 
            <!--banner-->
            <div class="index-top">
                <div class="banner-box relative-box">
                    <div class="banner-box relative-box rl" id="banner"></div>
                </div>
            </div>

            <!--热门游记-->
			<div class="line-selected" id="item_area_hotjournals"></div>

            <!--国内游记-->
			<div class="line-selected" id="item_area_inlandjournal"></div>

            <!--出境游记-->
			<div class="line-selected" id="item_area_outboundjournals"></div>

        </div>
	</div>
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
</body>
</html>