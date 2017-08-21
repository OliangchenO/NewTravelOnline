<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cruises.aspx.cs" Inherits="TravelOnline.tour.cruises" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>邮轮旅游 - <% =TravelOnline.Class.Common.PublicPageKeyWords.CruisesTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.CruisesKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/superslide.2.1.js"></script>
    <script type="text/javascript" src="/js/jquery.idTabs.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    
    <style type="text/css">
        .hot_destination{background-color:#F6FFF3;width:176px;border:2px solid #01AA07;border-width:2px 2px 2px 2px;position:relative;z-index:20;}
        .destination_box{height:245px;padding-top:0px;text-align:center;}
        .destination_box a {line-height:50px;height:50px;}
        .destination_box a img{padding-top:10px;padding-bottom:10px;}
        
        .fullSlide {z-index:1;}
        .fullSlide .hd {z-index:5;}
        .boatpic{float:left;overflow:hidden;}
        .boatpic img{width:470px;height:161px;}
         

    </style>
    <script type="text/javascript">
        if (screen.width < 1280) {
            document.write("<style type='text/css'>.right_part{margin-left:0px;width:941px;}.boatpic {width:447px;}.product_list{width:934px;}.usual{width:940px;}.usual .content{width:940px;}</style>");
        }
        else {
            document.write("<style type='text/css'>.boatpic img{height:200px;width:585px;}.right_part{margin-left:0px;width:1170px;}.product_list{width:1166px;}</style>");
        }
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<%--<% =TravelOnline.Class.NewClass.TopMenu.GetTopMenuString("OutBound")%>--%>
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

<div class="fullSlide">
    <div class="bd"><ul><%=SlidePicHtml%></ul></div>
    <div class="hd"><ul></ul></div>
    <span class="prev"></span> <span class="next"></span>
</div>

<div class="topadback" style="padding-bottom:0px;">
    <div class="container" >
	    <div class="row">
		    <div class="span12">

		        <div class="left_part" style="margin-top: -290px;">
                    <div class="hot_destination">
                        <div style="padding-left:10px;color:#ffffff; LINE-HEIGHT: 30px;font-size: 14px; font-weight: bold;background-color:#01AA07;height:30px;">邮轮公司</div>
                        <div class="destination_box" style=" ">
                        <a href="" target="_blank" ><img src="/img/royal_logo.gif" alt="" /></a>
                        <a href="" target="_blank" ><img src="/img/costa_logo.gif" alt="" /></a>
                        <a href="" target="_blank" ><img src="/img/princess_logo.gif" alt="" /></a>
                        <a href="" target="_blank" ><img src="/img/star_logo.gif" alt="" /></a>
                        </div>
                    </div>
		        </div>

                <%--<div class="right_part">
                    <div class="onsale_product product_list" style="height:200px;background-color:#fff;border:2px solid #01AA07;border-width:2px 2px 2px 2px;position:relative;overflow:hidden;" >
                        <div class="product_zoomfix" style="color:#FFF;background:#01AA07;height:200px;width:40px;border:1px solid #01AA07; border-width: 0px 0px 2px 0px;"> 
                            <div class="product_zoomfix" style="padding-left:9px;padding-top:10px;line-height:20px;font-weight:bold;FONT-SIZE: 16px;color:#FFF;height:200px;text-align:center;width:20px;"> 
                            <i class="icon-tags icon-white"></i>船队特别推荐
                            </div>
                        </div>
                        <div class="boatpic"> 
                            <a href="javascript:" title=""><img src="/img/boat.jpg" alt="" /></a>
                        </div>
                        <div class="boatpic"> 
                            <a target="_blank" href="javascript:" title=""><img src="/img/boat.jpg" alt="" /></a>
                        </div>
                    </div>
	            </div>--%>
            </div>
        </div>
    </div>
</div>

<div class="container" style="margin-top: 10px;">
	<div class="row">
        <div class="span12">
	        <div class="sy_mod_f_tit">
                <h3><i class="icon-flag" style="margin-top: 6px;"></i> 船队特别推荐</h3>
            </div>
            <%=ShipRecomm %>
            <%--<div class="boatpic"><a href="javascript:" title=""><img src="/img/boat.jpg" alt="" /></a></div>
            <div class="boatpic"> 
                <a target="_blank" href="javascript:" title=""><img src="/img/boat.jpg" alt="" /></a>
            </div>--%>
        </div>
		<div class="span12"  style="margin-top: 15px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 邮轮线路热卖排行</h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Hot", "NewRecom", "4", "SmallPic", "Cruises", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>

<div class="container" style="margin-top: 20px;">
	<div class="row">
		<div class="span12"><ul class="ulike_r2"><%=Cruises_3%></ul></div>
	</div>
</div>

<div class="container" style="margin-top: 5px;">
	<div class="row">
<%--		<div class="span12">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 特惠航线推荐</h3>
            </div>
            <div class="onsale_product" style="">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Preferences", "NewRecom", "2", "BigPic", "Cruises", "", "", "", 7)%>
            </div>
        </div>--%>
        <div class="span12">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 特惠航线推荐</h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Preferences", "NewRecom", "2", "SmallPic", "Cruises", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 日韩航线专卖<a class="more" target="_blank" href="/cruises/1063-0-0-0-0-0-0-0-0-1.html">更多日韩航线</a></h3>
            </div>
            <div class="onsale_product" style="">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Recommend_1063", "NewRecom", "3", "BigPic", "Cruises", "1063", "", "", 7)%>
            </div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 欧美航线专卖<a class="more" target="_blank" href="/cruises/1066-0-0-0-0-0-0-0-0-1.html">更多欧美航线</a></h3>
            </div>
            <div class="onsale_product" style="">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Recommend_1066", "NewRecom", "3", "BigPic", "Cruises", "1066", "", "", 7)%>
            </div>
        </div>

	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
        <div class="span12" style="margin-top: 6px;">
	        <div class="sy_mod_f_tit">
                <h3><i class="icon-tags"></i> 东南亚航线专卖<a class="more" target="_blank" href="/cruises/1065-0-0-0-0-0-0-0-0-1.html">更多东南亚航线</a></h3>
            </div>
            <div class="onsale_product" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Cruises_Recommend_1065", "NewRecom", "3", "SmallPic", "Cruises", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 20px;">
	<div class="row">
		<div class="span12"><%=Cruises_T%></div>
	</div>
</div>

<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
		

        </div>
	</div>
</div>

<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $("#DIV10 ul").idTabs();

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