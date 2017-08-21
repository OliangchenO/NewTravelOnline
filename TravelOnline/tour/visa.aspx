<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visa.aspx.cs" Inherits="TravelOnline.tour.visa" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>签证 - <% =TravelOnline.Class.Common.PublicPageKeyWords.VisaTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.VisaKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/superslide.2.1.js"></script>
    <script type="text/javascript" src="/js/jquery.idTabs.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <style type="text/css">
        .fullSlide {z-index:1;width:940px;}
        .fullSlide .hd {z-index:5;}
        .fullSlide .prev,.fullSlide .next {left:5%;}
        .fullSlide .next {left:auto;right:5%;}
    </style>
    <script type="text/javascript">
        if (screen.width < 1280) {
            document.write("<style type='text/css'>.product_list{width:936px;}.usual{width:940px;}.usual .content{width:940px;}</style>");
        }
        else {
            document.write("<style type='text/css'>.fullSlide {width:1170px;}</style>");
        }
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<div id="menu">
    <div class="container" >
        <div class="row">
            <div class="span12" style="background:#01AA07;">
                <uc4:menu ID="menu1" runat="server" />
                <uc3:index_destination ID="index_destination1" runat="server" />
            </div>
        </div>
    </div>
</div>

<div class="topadback" style="padding-bottom:0px;">
    <div class="container">
	    <div class="row">
		    <div class="span12">
                <div class="fullSlide">
                    <div class="bd"><ul><%=SlidePicHtml%></ul></div>
                    <div class="hd"><ul></ul></div>
                    <span class="prev"></span> <span class="next"></span>
                </div>
            </div>
	    </div>
    </div>
</div>

<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 签证行热卖排行</h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Visa_Hot", "NewRecom", "4", "SmallPic", "visa", "", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 20px;"><ul class="ulike_r2"><%=Visa_3%></ul></div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 亚洲国家签证<a class="more" target="_blank" href="/visa/259-0-0.html">更多亚洲国家签证</a></h3>
            </div>
            <div class="onsale_box">
                <div class="onsale_product" style="height:193px;">
                    <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Visa_Recommend_259", "NewRecom", "4", "SmallPic", "visa", "259", "", "", 5)%>
                </div>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 欧洲国家签证<a class="more" target="_blank" href="/visa/260-0-0.html">更多欧洲国家签证</a></h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Visa_Recommend_260", "NewRecom", "4", "SmallPic", "visa", "260", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 美洲国家签证<a class="more" target="_blank" href="/visa/264-0-0.html">更多美洲国家签证</a></h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Visa_Recommend_264", "NewRecom", "4", "SmallPic", "visa", "264", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 中东非洲国家签证<a class="more" target="_blank" href="/visa/266-0-0.html">更多中东非洲国家签证</a></h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Visa_Recommend_266", "NewRecom", "4", "SmallPic", "visa", "266", "", "", 5)%>
            </div>
        </div>

        
	</div>
</div>
<div class="container" style="margin-top: 20px;margin-bottom: 20px;">
	<div class="row">
		<div class="span12"><%=Visa_T%></div>
	</div>
</div>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    jQuery(".fullSlide").hover(function () {
        jQuery(this).find(".prev,.next").stop(true, true).fadeTo("show", 0.5)
    },
    function () {
        jQuery(this).find(".prev,.next").fadeOut()
    });
    jQuery(".fullSlide").slide({
        titCell: ".hd ul",
        mainCell: ".bd ul",
        effect: "fold",
        autoPlay: true,
        autoPage: true,
        trigger: "click",
        startFun: function (i) {
            var curLi = jQuery(".fullSlide .bd li").eq(i);
            if (!!curLi.attr("_src")) {
                curLi.css("background-image", curLi.attr("_src")).removeAttr("_src")
            }
        }
    });
</script>
</body>
</html>