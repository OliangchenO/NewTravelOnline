<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="freetour.aspx.cs" Inherits="TravelOnline.tour.freetour" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自由行 - <% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/superslide.2.1.js"></script>
    <script type="text/javascript" src="/js/jquery.idTabs.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <style type="text/css">
        .right_part{margin-left:0px;}
        .destination_box{height:245px;padding-top:0px;}
        .hot_destination{background-color:#F6FFF3;width:176px;border:2px solid #01AA07;border-width:2px 2px 2px 2px;position:relative;z-index:20;}
        .destination_small {overflow:hidden;}
        .destination_small dd{padding-left:66px;line-height:0px;position:relative;_position:static;font-size:0;overflow:hidden;_zoom:1;}
        .destination_small dd{width:176px;padding-left:8px;position:relative;font-size:0;overflow:hidden;_zoom:1;}
        .destination_small dd a,.destination_detail .local_type a {min-width:48px;max-width:150px;display:inline-block;font-size:12px;line-height:25px;height:25px;margin-right:6px;}
        .destination_small dt{padding-left:8px;font-weight:bold;width:176px;z-index:2;margin-top:8px;padding-top:0px;text-align:left;}
        .destination_small dt a{color: #038307;}
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
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 自由行热卖排行</h3>
            </div>
            <div class="onsale_product product_list" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("FreeTour_Hot", "NewRecom", "4", "SmallPic", "FreeTour", "", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 20px;"><ul class="ulike_r2"><%=FreeTour_3%></ul></div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 国内自由行专卖<a class="more" target="_blank" href="/freetour/1062-0-0.html">更多国内自由行</a></h3>
            </div>
            <div class="onsale_product product_list" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("FreeTour_Recommend_1062", "NewRecom", "4", "SmallPic", "FreeTour", "1062", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 港澳台自由行专卖<a class="more" target="_blank" href="/freetour/1059-0-0.html">更多港澳台自由行</a></h3>
            </div>
            <div class="onsale_product product_list" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("FreeTour_Recommend_1059", "NewRecom", "4", "SmallPic", "FreeTour", "1059", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 东南亚自由行专卖<a class="more" target="_blank" href="/freetour/1061-0-0.html">更多东南亚自由行</a></h3>
            </div>
            <div class="onsale_product product_list" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("FreeTour_Recommend_1061", "NewRecom", "4", "SmallPic", "FreeTour", "1061", "", "", 5)%>
            </div>
        </div>

        <div class="span12" style="margin-top: 10px;">
		    <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 美加自由行专卖<a class="more" target="_blank" href="/freetour/1195-0-0.html">更多美加自由行</a></h3>
            </div>
            <div class="onsale_product product_list" style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("FreeTour_Recommend_1195", "NewRecom", "4", "SmallPic", "FreeTour", "1195", "", "", 5)%>
            </div>
        </div>

	</div>
</div>
<div class="container" style="margin-top: 20px;margin-bottom: 20px;">
	<div class="row">
		<div class="span12"><%=FreeTour_T%></div>
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
