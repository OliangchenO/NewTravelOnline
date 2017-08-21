<%@ OutputCache Duration="86400" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SightDetail.aspx.cs" Inherits="TravelOnline.Destination.SightDetail" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=pagetitle %></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/linelist.css" rel="stylesheet" />
    <link href="/css/Sight.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/jquery.nav.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/myfocus-2.0.4.min.js"></script>
    <script type="text/javascript" src="/js/boot.page.zp.js"></script>
    <%=MapScriptFile%>
    <style type="text/css">
        
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
            <div class="desname">
                <h3><%=cname %><span><%=ename %></span></h3>
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
            <div class="left">
                <div id="PlacePicUrl hide">
                    <div id="myFocus"><!--焦点图盒子-->
                        <div class="loading"></div>
                        <div class="pic"><%=PlacePicUrl%></div>
                    </div>
                </div>
            </div>
            <div class="right">
                <div class="alert alert-block">
                    <ul><%=ViewInfo%></ul>
                </div>
            </div>
            
        </div>
	</div>
</div>
<div class="container" style="margin-top: 10px;">
	<div class="row">
		<div class="span12">
            <div class="bigleft">
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
                <div class="sy_mod_f_tit" style="margin-top:20px">
                    <h3><i class="icon-flag" style="margin-top: 6px;"></i> <%=PlaceTitle %><a class="more" target="_blank" href="/sight/<%=desid %>.html">更多<%=PlaceUrl%></a></h3>
                </div>
                <div class="onsale_place"><%=PlaceList %></div>
            </div>
            <div class="bigright">
                <div class="left_Recommend">
                    <div class="Recommend_title"><%=desname%>线路推荐</div>
                    <div id="Div1" class="Recommend_box">
                        <ul class="alsobuy"><% =TravelOnline.tour.LineRecommend.DestinationLineSellCache("DestinationLineSell", desid, 3)%></ul>
                    </div>
                </div>
                <div class="left_Recommend">
                    <div class="Recommend_title">相关攻略</div>
                    <div id="Div6" class="Recommend_box">
                        <ul class="Journal"><% =TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("LeftLineJournal", desid, desid, 9)%></ul>
                    </div>
                </div>
            </div>
            
        </div>
	</div>
</div>

<div class="container" style="margin-top: 30px;">
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
                <h3><i class="icon-th-large" style="margin-top: 6px;"></i> 热门景点</h3>
            </div>
		    <div class="bottomlink"><%=HotPlaceView %>
            </div>
        </div>
	</div>
</div>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#TB_ShowPic").val() == "yes") {
            myFocus.set({ id: 'myFocus', pattern: 'mF_kdui' });
            $("#PlacePicUrl").show();
        }

        $("#topnav").smartFloat();
        $('#nav').onePageNav({ scrollOffset: 15, scrollThreshold: 0.1 });
    })
</script>
<%=MapScript %>
</body>
</html>
