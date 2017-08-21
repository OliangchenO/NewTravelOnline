<%@ OutputCache Duration="86400" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="summary.aspx.cs" Inherits="TravelOnline.Destination.summary" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=pagetitle %> 旅游目的地 - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/linelist.css" rel="stylesheet" />
    <link href="/css/mF_pithy_tb.css" rel="stylesheet" />
    <link href="/css/summary.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/jquery.nav.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/myfocus-2.0.4.min.js"></script>
    <script type="text/javascript" src="/js/boot.page.zp.js"></script>
    
    <%=MapScriptFile%>
    <style type="text/css">
        .right_part{float: right;}
    </style>
    <script type="text/javascript">
        //pin
        $.fn.smartFloat = function () {
            var position = function (element) {
                var top = element.position().top; //当前元素对象element距离浏览器上边缘的距离
                var pos = element.css("position"); //当前元素距离页面document顶部的距离 
                $(window).scroll(function () { //侦听滚动时 
                    var scrolls = $(this).scrollTop();
                    if (scrolls > top) { //如果滚动到页面超出了当前元素element的相对页面顶部的高度 
                        if (window.XMLHttpRequest) { //如果不是ie6 
                            element.css({ //设置css 
                                position: "fixed", //固定定位,即不再跟随滚动 
                                top: 0 //距离页面顶部为0 
                            }).addClass("shadow"); //加上阴影样式.shadow 
                        } else { //如果是ie6 
                            element.css({
                                top: scrolls  //与页面顶部距离 
                            });
                        }
                    } else {
                        $("#nav_order").hide();
                        element.css({ //如果当前元素element未滚动到浏览器上边缘，则使用默认样式 
                            position: pos,
                            top: top
                        }).removeClass("shadow"); //移除阴影样式.shadow 
                    }
                });
            };
            return $(this).each(function () {
                position($(this));
            });
        };
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

<%--导航面包屑 begin--%>
<div class="container" >
    <div class="row">
        <div class="span12">
            <ul class="breadcrumb">
                <li><a href="/">首页</a> <span class="divider">/</span></li>
                <li><a href="/place.html">目的地</a> <span class="divider">/</span></li>
                <%=breadcrumb%>
            </ul>
        </div>
    </div>
</div>
<%--导航面包屑 end--%>

<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            <%--内容放到span12的这个div里--%>
            <div class="desname">
                <h3><a href="/place/<%=id %>.html"><%=cname %></a><span><%=ename %></span></h3>
                <%--<iframe style="" allowtransparency="true" frameborder="0" width="140" height="36" scrolling="no" src="http://tianqi.2345.com/plugin/widget/index.htm?s=3&z=3&t=1&v=0&d=3&k=&f=1&q=1&e=1&a=0&c=54511&w=180&h=36"></iframe>--%>
            </div>
        </div>
	</div>
</div>
<div style="DISPLAY:none">
    <input id="TB_Flag" name="TB_Flag" type="hidden" value="<%=flag %>"/>
    <input id="TB_ShowPic" name="TB_ShowPic" type="hidden" value="<%=ShowPic %>"/>
</div>
<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            <%--内容都放到span12的这个div里--%>
		    <div class="left_part">
                <div class="left_Recommend">
                    <div class="Recommend_box">
                        <ul class="infos">
                            <li class="noline"><a href="/summary/<%=id %>.html"><i class="icon-globe"></i> 旅行指南</a></li>
                            <li><a href="/sight/<%=id %>.html"><i class="icon-picture"></i> 景点介绍</a></li>
                            <li><a href="/traffic/<%=id %>.html"><i class="icon-plane"></i> 交通概况</a></li>
                            <li><a href="/journals/<%=id %>.html"><i class="icon-th-large"></i> 游记攻略</a></li>
                            <li><a href="/placemap/<%=id %>.html"><i class="icon-map-marker"></i> 目的地地图</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_Recommend">
                    <div class="Recommend_title"><%=cname %>线路推荐</div>
                    <div id="Div1" class="Recommend_box">
                        <ul class="alsobuy"><% =TravelOnline.tour.LineRecommend.DestinationLineSellCache("DestinationLineSell", id, 3)%></ul>
                    </div>
                </div>
            </div>
            <div id="PlacePicUrl" class="right_part <%=PicHide %>" style="margin-bottom: 15px;">
                <div id="myFocus"><!--焦点图盒子-->
                    <div class="loading"></div>
                    <div class="pic"><%=PlacePicUrl%></div>
                </div>
            </div>
            <div class="right_part <%=PlaceHide %>" style="float:right">
                <div class="sy_mod_f_tit">
                    <h3><i class="icon-globe" style="margin-top: 6px;"></i> <%=PlaceTitle %><a class="more" target="_blank" href="/sight/<%=id %>.html">更多<%=PlaceUrl%></a></h3>
                </div>
                <div class="onsale_place"><%=PlaceList %>
                    <%--<div class="product_zoomfix">
                        <a class="product" target="_blank" href="/line/12234.html" hidefocus="true">
                            <img src="/images/none.gif" data-original="/Images/Views/201001/M_1001121530158.jpg" alt="【35】天目湖、南山竹海二日游jsq">
                            <span class="price_box"><strong class="place">浙江</strong></span>
                        </a>
                    </div>--%>
                </div>
                <div class="sy_mod_f_tit" style="margin-top: 15px;">
                    <h3><i class="icon-th-large" style="margin-top: 6px;"></i> <%=cname %>游记<a class="more" target="_blank" href="/journals/<%=id %>.html">更多<%=cname %>游记</a></h3>
                </div>
                <div class="onsale_journal"><%=PlaceJournalList%></div>
                <div class="sy_mod_f_tit" style="margin-top: 15px;">
                    <h3><i class="icon-flag" style="margin-top: 6px;"></i> <%=cname %>旅游线路<a class="more" target="_blank" href="/search.html?destination=<%=id %>">更多<%=cname %>线路</a></h3>
                </div>
                <div class="onsale_product">
                    <%=DestinationLineList%>
                </div>
            </div>
            <div class="right_part <%=SummaryHide %>">
                <div class="desname">
                    <h4><%=contentname %></h4>
                </div>
                <div class="x_mod_tab_hd" id="info_tit">
                    <div id="topnav" class="nav"> 
                        <ul id="nav"> 
                            <%=NavString %>
                        </ul>
                    </div> 
                </div>
                <div class="news newscontent" style="margin-top:30px">
                    <div class="content"><%=NewsContent %></div>
                </div>
            </div>
            <div class="right_part <%=SightHide %>">
                <div class="alert alert-block <%=SightRecommendHide %>">
                    <h1><%=SightTitle%></h1>
                    <div class="sight_list"><%=SightRecommend %>
                        <%--<ul>
                            <li>
                                <a href="/sight/yunnan100007/4200.html" target="_blank"><img src="http://dimg02.c-ctrip.com/images/tg/749/927/958/32a19f5d19e04fcc8bdedecab06a4ab1_C_220_140.jpg" width="220" height="140"></a>
                                <dl>
                                    <dt>
                                        <i class="sight"></i>
                                        <a href="/sight/yunnan100007/4200.html" target="_blank">东方女儿国泸沽湖</a>
                                    </dt>
                                    <dd>
                                        准备过年时去泸沽湖，有同行的没?本人男，在东莞，预计1月…
                                    </dd>
                                </dl>
                            </li>
                        </ul>--%>
                    </div>
                </div>
                <div class="desname">
                    <h4><%=contentname %></h4>
                </div>
                <%=SightList %>
                <div class="view_list <%=ViewListHide %>"><%=ViewList %></div>
                <div id="bottompages" class="m clearfix" style="padding:5px 20px 20px 20px;"><%=BottomPages%></div>
                <%--<div class="csingle_sight">
                    <div class="cityimg">
                        <a href="/place/lijiang32.html" target="_blank"><img src="/images/none.gif" alt=""></a><i></i><span>丽江</span>
                    </div>
                    <dl>
                        <dt><a href="/place/lijiang32.html">丽江</a></dt>
                            <dd class="ellipsis">
                                推荐景点：<a href="/sight/lijiang32/3049.html" target="_blank">玉龙雪山</a>、<a href="/sight/lijiang32/3050.html" target="_blank">甘海子</a>、<a href="/sight/lijiang32/3053.html" target="_blank">玉峰寺</a>、<a href="/sight/lijiang32/3054.html" target="_blank">长江第一湾</a>、<a href="/sight/lijiang32/3055.html" target="_blank">虎跳峡</a>
                            </dd>
                        <dd>
                            <a href="/sightlist/lijiang32.html" target="_blank">丽江所有景点<i class="icon-chevron-right"></i></a>
                        </dd>
                    </dl>
                </div>--%>
            </div>
            <div class="right_part <%=MapHide %>"  style="margin-bottom:20px">
                <div class="desname">
                    <h4><%=contentname %></h4>
                </div>
                <div class="news newscontent">
                    <div class="content"><%=MapContent%></div>
                </div>
            </div>
            <div class="right_part <%=BaiduMapHide %>">
                <div class="desname">
                    <h4><%=BaiduMapName%></h4>
                </div>
                <div id="allmap"></div>
            </div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 50px;">
	<div class="row">
		<div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-globe" style="margin-top: 6px;"></i> 热门目的地</h3>
            </div>
		    <div class="bottomlink"><%=HotDestination %>
            </div>
        </div>
	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
            <div class="sy_mod_f_tit">
                <h3><i class="icon-globe" style="margin-top: 6px;"></i> 热门景点</h3>
            </div>
		    <div class="bottomlink"><%=HotPlaceView %>
            </div>
        </div>
	</div>
</div>
<%--<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            <div class="news newscontent">
                <iframe width="420" scrolling="no" height="60" frameborder="0" allowtransparency="true" src="http://i.tianqi.com/index.php?c=code&id=12&icon=1&num=5"></iframe>
                    <iframe src="http://m.weather.com.cn/m/pn12/weather.htm " width="245" height="110"  
                     marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0"  
                     scrolling="no"></iframe>
            </div>
        </div>
	</div>
</div>--%>
 
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#TB_ShowPic").val() == "yes") {
            myFocus.set({ id: 'myFocus', pattern: 'mF_pithy_tb', thumbShowNum: 5 });
            $("#PlacePicUrl").show();
        }

        $("#topnav").smartFloat();
        $('#nav').onePageNav({ scrollOffset: 15, scrollThreshold: 0.1 });
    })
</script>
<%=MapScript %>
<%--<script type="text/javascript">

    // 百度地图API功能
    var map = new BMap.Map("allmap");                        // 创建Map实例
    map.centerAndZoom(new BMap.Point(121.444625, 31.19912), 15);     // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
    map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
    map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
    map.enableScrollWheelZoom();                            //启用滚轮放大缩小
    map.addControl(new BMap.MapTypeControl());          //添加地图类型控件

</script>--%>
</body>
</html>