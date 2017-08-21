<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TravelOnline.index" %>
<%@ Register src="NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta name="description" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.idTabs.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/superslide.2.1.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <style type="text/css">
        .fullSlide {float:left;width:760px;z-index:1;height:300px;OVERFLOW: hidden;}
        .fullSlide .prev,.fullSlide .next {left:5%;}
        .fullSlide .next {left:auto;right:5%;}
	    .fullSlide .hd {width:100%;position:absolute;z-index:5;bottom:0;left:0;height:30px;line-height:30px;}
        .fullSlide .hd ul {text-align:center;}
        
        .destination_detail dt a {color: #038307;}
        #fullSlide1 .bd li {height:386px;}
        #fullSlide1 .bd li a {height:386px;}
        #fullSlide2 .bd li {height:386px;}
        #fullSlide2 .bd li a {height:386px;}
        #DestinationMenu {position:relative;display:block;margin-top:0px;z-index:9;height:360px;}
        
        .bottomlink a {
            color: #999999;
            display: inline-block;
            line-height: 24px;
            margin-right: 8px;
	        text-decoration:none;
	        min-width:48px;
        }
        .bottomlink a:hover {
            color: #01aa07;
	        text-decoration:underline;
        }
        .hot_line UL {
	        PADDING-BOTTOM: 5px; PADDING-LEFT: 10px; PADDING-RIGHT: 5px; PADDING-TOP: 0px
        }
        .hot_line LI {
	        LINE-HEIGHT: 23px; HEIGHT: 23px; OVERFLOW: hidden;color:#999999
        }
        .hot_line LI a{
	        color:#999999
        }
        .hot_line .op {
	        LINE-HEIGHT: 21px; HEIGHT: 21px; OVERFLOW: hidden
        }
        .hot_tit {padding:2px 5px;FONT-SIZE: 14px;FONT-WEIGHT: bold;color:#333333}
        .hot_area {background-color:#F7F7F7;float:left;width:218px;HEIGHT: 193px;margin-left:0px;border:1px solid #DDDDDD;border-width:1px 1px 1px 1px;position:relative;}
        .hot_area img {width:218px;height:193px;}
        .hot_des {HEIGHT: 40px;padding:2px 5px 2px 10px; OVERFLOW: hidden}
        .hot_des a{min-width:48px;display:inline-block;font-size:12px;line-height:20px;height:20px;margin-right:4px;color:#999999}
        .partner {}
        .partner li {width:117px;float:left;}
        .partner li img{width:110px;height:45px;border:1px solid #DDDDDD;margin-bottom:10px;}
    </style>
    <script type="text/javascript">
        DestinationMenuFlag = "index";
        if (screen.width >= 1280) { document.write("<style type='text/css'>.fullSlide {width:990px;}</style>"); }
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<div id="menu">
    <div class="container" >
        <div class="row">
            <div class="span12" style="background:#01AA07;">
                <div id="menusort" style=""><div >全部旅游目的地</div></div>
                <ul class="menulink" style="margin-bottom: 0px;">
                    <li><a href="/outbound.html">出境旅游</a></li>
                    <li><a href="/inland.html">国内旅游</a></li>
                    <li><a href="/freetour.html">自由行</a></li>
                    <li><a href="/cruises.html">邮轮旅游</a></li>
                    <li><a href="/visa.html">签证</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
<%--1170*360--%>
<div class="topadback">
    <div class="container">
	    <div class="row">
		    <div class="span12">
                <div class="left_ad" style="width:180px;">
                    <uc3:index_destination ID="index_destination1" runat="server" />
                    <div style="BACKGROUND: #01AA07;float:left;width:180px;HEIGHT: 183px;margin-left:0px;position:relative;border-top: 1px solid #02BA30;">
            
                    </div>
                </div>
                <div class="fullSlide" id="fullSlideBig">
                    <div class="bd"><ul><%=SlidePicHtml%></ul></div>
                    <div class="hd"><ul></ul></div>
                    <span class="prev"></span> <span class="next"></span>
                </div>
                <div class="announcement_top">
                    <div class="announcement basefix">
                        <h2>公告 <i class="icon-volume-down"></i></h2><a class="more" target="_blank" href="/info/index_news.html">更多</a>
                        <div class="pull-left"><div id="block2"><ul id="rolltxt"><%=Announcement%></ul></div></div>
                        <b></b>
                    </div>
                    <div id="citie" class="pull-left"><ul><%=Citie%></ul></div>
                </div>
		    </div>
	    </div>
    </div>
</div>

<%--特价促销--%>
<div class="container" style="margin-top: 15px;">
	<div class="row">
        <div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 优惠促销 / 特价旅游线路</h3>
            </div>
            <div class="onsale_product"  style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_Preferences", "IndexRecom", "2", "SmallPic", "", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>

<div class="container" style="margin-top: 20px;">
	<div class="row">
		<div class="span12"><ul class="ulike_r2"><%=Index_Up_3%></ul></div>
	</div>
</div>

<%--出境--%>
<div class="container" style="margin-top: 15px;">
	<div class="row">
		<div class="span12">
		    <div class="left_ad">
                <div class="sy_mod_f_tit">
                    <h3><i class="icon-tags" style="margin-top: 6px;"></i> 出境旅游线路推荐</h3>
                </div>
                <div class="fullSlide"  id="fullSlide1" style="width:220px;HEIGHT:386px;">
                    <div class="bd"><ul><%=OutBound_Small%></ul></div>
                    <div class="hd"><ul></ul></div>
                    <span class="prev"></span> <span class="next"></span>
                </div>
                <div class="hot_area">
                    <div class="hot_tit">本月热点线路</div>
                    <div class="hot_line">
                        <ul><%=LeftHot_OutBound%></ul>
                    </div>
                    <div class="hot_tit">热点目的地</div>
                    <div class="hot_des"><%=LeftArea_OutBound%></div>
                </div>
            </div>
            <div class="right_ad">
                <div class="content">
                    <div class="index_area">
                        <a href="/outbound/227-0-0.html" target="_blank" >港澳</a>
                        <a href="/outbound/231-0-0.html" target="_blank" >日韩</a>
                        <a href="/outbound/489-0-0.html" target="_blank">澳新</a>
                        <a href="/outbound/491-0-0.html" target="_blank">东南亚</a>
                        <a href="/outbound/492-0-0.html" target="_blank">欧洲</a>
                        <a href="/outbound/493-0-0.html" target="_blank">中东非洲</a>
                        <a href="/outbound/637-0-0.html" target="_blank" >美加</a>
                        <a href="/outbound/580-0-0.html" target="_blank" >南美</a>
                        <a href="/outbound/851-0-0.html" target="_blank" >台湾</a>
                    </div>
                    <div class="onsale_product onsale_index" style="height:579px;">
                        <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_OutBound", "IndexRecom", "3", "BigPic", "outbound", "", "", "", 9)%>
                    </div>
                 </div>
            </div>
        </div>
	</div>
</div>



<%--国内--%>
<div class="container" style="margin-top: 20px;">
	<div class="row">
		<div class="span12">
		    <div class="left_ad">
                <div class="sy_mod_f_tit">
                    <h3><i class="icon-tags" style="margin-top: 6px;"></i> 国内旅游线路推荐</h3>
                </div>
                <div class="fullSlide"  id="fullSlide2" style="width:220px;HEIGHT: 386px;">
                    <div class="bd"><ul><%=InLand_Small%></ul></div>
                    <div class="hd"><ul></ul></div>
                    <span class="prev"></span> <span class="next"></span>
                </div>
                <div  class="hot_area">
                    <div class="hot_tit">本月热点线路</div>
                    <div class="hot_line">
                        <ul><%=LeftHot_InLand%></ul>
                    </div>
                    <div class="hot_tit">热点目的地</div>
                    <div class="hot_des"><%=LeftArea_InLand%></div>
                </div>
            </div>
            <div class="right_ad">
                <div class="content">
                    <div class="index_area">
                        <a href="/inland/281-0-0.html" target="_blank" >北方</a>
                        <a href="/inland/466-0-0.html" target="_blank" >西南</a>
                        <a href="/inland/469-0-0.html" target="_blank" >西北</a>
                        <a href="/inland/223-0-0.html" target="_blank" >华东</a>
                        <a href="/inland/224-0-0.html" target="_blank" >华南</a>
                        <a href="/inland/280-0-0.html" target="_blank" >华中</a>
                    </div>
                    <div class="onsale_product onsale_index" style="height:579px;">
                        <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_InLand", "IndexRecom", "3", "BigPic", "inland", "", "", "", 9)%>
                    </div>
                 </div>
                 
            </div>
        </div>
	</div>
</div>


<%--邮轮签证--%>
<div class="container" style="margin-top: 25px;">
	<div class="row">
		<div class="span12">
		    <div class="left_ad">
                <div class="sy_mod_f_tit">
                    <h3><i class="icon-tags" style="margin-top: 6px;"></i> 自由行、邮轮及签证</h3>
                </div>
                <div class="hot_area">
                    <%=Hot_FreeTour%><%--<img src="/Upload/AdImage/2013100618153507411849.jpg"></img>--%>
                </div>
            </div>
            <div class="right_ad">
                <div id="div_tabs" class="usual">
                    <ul> 
                        <li><a class="selected" href="#div001">自由行</a></li>
                        <li><a href="#div002">邮轮</a></li>
                        <li><a href="#div003">单办签证</a></li>
                    </ul>
                    <div class="content">
                        <div id="div001">
                            <div class="onsale_product onsale_index" style="height:193px;">
                                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_FreeTour", "IndexRecom", "3", "SmallPic", "freetour", "", "", "", 4)%>
                            </div>
                        </div>
                        <div id="div002">                   
                            <div class="onsale_product onsale_index" style="height:193px;">
                                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_Cruises", "IndexRecom", "3", "SmallPic", "cruises", "", "", "", 4)%>
                            </div>
                        </div>
                        <div id="div003">
                            <div class="onsale_product onsale_index" style="height:193px;">
                                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_Visa", "IndexRecom", "3", "SmallPic", "visa", "", "", "", 4)%>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div id="topnav" class="nav" style=""> 
                    <ul id="nav"> 
                        <li class="current"><a href="#plan_calendar" hidefocus="true">出发日期</a></li><li class=""><a href="#route_info" hidefocus="true">行程安排</a></li><li><a href="#price_info" hidefocus="true">费用描述</a></li><li><a href="#memo_info" hidefocus="true">温馨提醒</a></li><li><a href="#order_service" hidefocus="true">预订须知</a></li>
                    </ul>
                </div>--%>
            </div>
        </div>
	</div>
</div>


<div class="container" style="margin-top: 20px;">
	<div class="row">
		<div class="span12"><ul class="ulike_r2"><%=Index_Down_3%></ul></div>
	</div>
</div>


<%--青旅名牌--%>
<div class="container" style="margin-top: 15px;">
	<div class="row">
        <div class="span12">
            <div class="sy_mod_f_tit">
                <h3><img src="/img/mp.jpg" style="margin-top: -3px;width:30px;height:25px" alt="青旅名牌产品" /> 青旅名牌产品</h3>
            </div>
            <div class="onsale_product"  style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_Famous", "IndexRecom", "1", "SmallPic", "", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>

<%--特价促销--%>
<div class="container" style="margin-top: 15px;">
	<div class="row">
        <div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-tags" style="margin-top: 6px;"></i> 本周热卖</h3>
            </div>
            <div class="onsale_product"  style="height:193px;">
                <% =TravelOnline.tour.LineRecommend.LineRecommendCache("Index_Hot", "IndexRecom", "4", "SmallPic", "", "", "", "", 5)%>
            </div>
        </div>
	</div>
</div>

<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-search" style="margin-top: 6px;"></i> 热门搜索</h3>
            </div>
		    <div class="bottomlink">
            <a href="/outbound/492-34-0-0-0-0-0-0-0-1.html" target="_blank">瑞士旅游</a>
            <a href="/outbound/492-101-0-0-0-0-0-0-0-1.html" target="_blank">芬兰旅游</a>
            <a href="/outbound/231-22-0-0-0-0-0-0-0-1.html" target="_blank">日本旅游</a>
            <a href="/outbound/231-23-0-0-0-0-0-0-0-1.html" target="_blank">韩国旅游</a>
            <a href="/outbound/491-16-0-0-0-0-0-0-0-1.html" target="_blank">泰国旅游</a>
            <a href="/outbound/491-16-151-0-0-0-0-0-0-1.html" target="_blank">清迈旅游</a>
            <a href="/outbound/491-19-0-0-0-0-0-0-0-1.html" target="_blank">越南旅游</a>
            <a href="/outbound/637-37-0-0-0-0-0-0-0-1.html" target="_blank">美国旅游</a>
            <a href="/outbound/493-87-251-0-0-0-0-0-0-1.html" target="_blank">迪拜旅游</a>
            <a href="/outbound/493-64-0-0-0-0-0-0-0-1.html" target="_blank">南非旅游</a>
            <a href="/outbound/227-110-0-0-0-0-0-0-0-1.html" target="_blank">香港旅游</a>
            <a href="/outbound/851-112-0-0-0-0-0-0-0-1.html" target="_blank">台湾旅游</a>
            <a href="/inland/281-13-0-0-0-0-0-0-0-1.html" target="_blank">北京旅游</a>
            <a href="/inland/281-136-641-0-0-0-0-0-0-1.html" target="_blank">青岛旅游</a>
            <a href="/inland/466-81-0-0-0-0-0-0-0-1.html" target="_blank">云南旅游</a>
            <a href="/inland/466-124-568-0-0-0-0-0-0-1.html" target="_blank">桂林旅游</a>
            <a href="/inland/466-124-569-0-0-0-0-0-0-1.html" target="_blank">北海旅游</a>
            <a href="/inland/224-116-0-0-0-0-0-0-0-1.html" target="_blank">海南旅游</a>
            <a href="/inland/223-14-0-0-0-0-0-0-0-1.html" target="_blank">上海旅游</a>
            <a href="/inland/223-137-649-0-0-0-0-0-0-1.html" target="_blank">南京旅游</a>
            <a href="/inland/281-129-0-0-0-0-0-0-0-1.html" target="_blank">河南旅游</a>
            <a href="/cruises/1063-0-0-0-0-0-0-0-0-1.html" target="_blank">日韩油轮</a>
            <a href="/outbound/492-27-0-0-0-0-0-0-0-1.html" target="_blank">意大利旅游</a>
            <a href="/outbound/492-29-0-0-0-0-0-0-0-1.html" target="_blank">西班牙旅游</a>
            <a href="/outbound/492-93-0-0-0-0-0-0-0-1.html" target="_blank">葡萄牙旅游</a>
            <a href="/outbound/492-95-0-0-0-0-0-0-0-1.html" target="_blank">奥地利旅游</a>
            <a href="/outbound/491-26-219-0-0-0-0-0-0-1.html" target="_blank">巴厘岛旅游</a>
            <a href="/outbound/491-25-212-0-0-0-0-0-0-1.html" target="_blank">长滩岛旅游</a>
            <a href="/outbound/491-16-148-0-0-0-0-0-0-1.html" target="_blank">普吉岛旅游</a>
            <a href="/outbound/491-21-0-0-0-0-0-0-0-1.html" target="_blank">尼泊尔旅游</a>
            <a href="/outbound/491-20-0-0-0-0-0-0-0-1.html" target="_blank">柬埔寨旅游</a>
            <a href="/outbound/637-37-414-0-0-0-0-0-0-1.html" target="_blank">夏威夷旅游</a>
            <a href="/outbound/637-38-0-0-0-0-0-0-0-1.html" target="_blank">加拿大旅游</a>
            <a href="/outbound/489-56-0-0-0-0-0-0-0-1.html" target="_blank">新西兰旅游</a>
            <a href="/outbound/493-33-0-0-0-0-0-0-0-1.html" target="_blank">土耳其旅游</a>
            <a href="/inland/469-374-831-0-0-0-0-0-0-1.html" target="_blank">九寨沟旅游</a>
            <a href="/inland/280-127-582-0-0-0-0-0-0-1.html" target="_blank">张家界旅游</a>
            <a href="/cruises/1065-0-0-0-0-0-0-0-0-1.html" target="_blank">东南亚油轮</a>
            <a href="/outbound/491-24-0-0-0-0-0-0-0-1.html" target="_blank">马尔代夫旅游</a>
            <a href="/outbound/489-55-498-0-0-0-0-0-0-1.html" target="_blank">澳大利亚旅游</a>
            <a href="/freetour/1059-0-0-0-0-0-0-0-0-1.html" target="_blank">港澳台自由行</a>
            
            </div>
        </div>
	</div>
</div>

<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-flag" style="margin-top: 6px;"></i> 友情链接</h3>
            </div>
		    <div class="bottomlink"><%=FirendLink %></div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-star" style="margin-top: 6px;"></i> 合作伙伴</h3>
            </div>
		    <div class="partner"><ul><%=PartnerLink%></ul></div>
        </div>
	</div>
</div>
<div class="container">
	<div class="row">
		<div class="span12">
        
        </div>
	</div>
</div>
<uc2:Footer ID="Footer1" runat="server" />

<script type="text/javascript">
    $("#div_tabs ul").idTabs();
	
    function extractNodes(pNode) {
        if (pNode.nodeType == 3) return null;
        var node, nodes = new Array();
        for (var i = 0; node = pNode.childNodes[i]; i++) {
            if (node.nodeType == 1) nodes.push(node);
        }
        return nodes;
    }
    var obj = document.getElementById("rolltxt");
    settime = 0;
    var t = setInterval(rolltxt, 50);
    function rolltxt() {
        if (obj.scrollTop % (obj.clientHeight) == 0) {
            settime += 1;
            if (settime == 50) {
                obj.scrollTop += 1;
                settime = 0;
            }
        } else {
            obj.scrollTop += 1;
            if (obj.scrollTop == (obj.scrollHeight - obj.clientHeight)) {
                obj.scrollTop = 0;
            }
        }
    }
    obj.onmouseover = function () { clearInterval(t) }
    obj.onmouseout = function () { t = setInterval(rolltxt, 50) }


    $('#citie li').hover(function () {
        $(this).find('.citie_img').hide();
        $(this).find('.citie_img2').show();
    }, function () {
        $('.citie_img').show();
        $('.citie_img2').hide();
    })


    jQuery("#fullSlideBig").hover(function () {
        jQuery(this).find(".prev,.next").stop(true, true).fadeTo("show", 0.5)
    },
    function () {
        jQuery(this).find(".prev,.next").fadeOut()
    });

    jQuery("#fullSlideBig").slide({
        titCell: ".hd ul",
        mainCell: ".bd ul",
        effect: "fold",
        autoPlay: true,
        autoPage: true,
        trigger: "click",
        startFun: function (i) {
            var curLi = jQuery("#fullSlideBig .bd li").eq(i);
            if (!!curLi.attr("_src")) {
                curLi.css("background-image", curLi.attr("_src")).removeAttr("_src")
            }
        }
    });

    jQuery("#fullSlide1").slide({
        titCell: ".hd ul",
        mainCell: ".bd ul",
        effect: "fold",
        autoPlay: true,
        autoPage: true,
        trigger: "click",
        startFun: function (i) {
            var curLi = jQuery("#fullSlide1 .bd li").eq(i);
            if (!!curLi.attr("_src")) {
                curLi.css("background-image", curLi.attr("_src")).removeAttr("_src")
            }
        }
    });

    jQuery("#fullSlide2").slide({
        titCell: ".hd ul",
        mainCell: ".bd ul",
        effect: "fold",
        autoPlay: true,
        autoPage: true,
        trigger: "click",
        startFun: function (i) {
            var curLi = jQuery("#fullSlide2 .bd li").eq(i);
            if (!!curLi.attr("_src")) {
                curLi.css("background-image", curLi.attr("_src")).removeAttr("_src")
            }
        }
    });
</script>
</body>
</html>
