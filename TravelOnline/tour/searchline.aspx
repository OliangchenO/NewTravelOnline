<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchline.aspx.cs" Inherits="TravelOnline.tour.searchline" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleString %> <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicLineListTitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.OutBoundKeywords %> />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/linelist.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/js/setcookie.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
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

<div class="container" >
    <div class="row">
        <div class="span12">
            <ul class="breadcrumb"><%=BreadCrumb%></ul>
        </div>
    </div>
</div>

<div class="container" >
	<div class="row">
		<div class="span12">
		    <div class="left_part">

                <%--<div class="left_Recommend">
                    <div class="Recommend_title">一周热卖排行</div>
                    <div id="WeekHotRank" class="Recommend_box">
                        <ul class="rank"><% =TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("LeftLineHotSell", linetype, lineclass.ToString(), 9)%></ul>
                    </div>
                </div>
                <div class="left_Recommend">
                    <div class="Recommend_title">特惠线路推荐</div>
                    <div id="Div1" class="Recommend_box">
                        <ul class="alsobuy"><% =TravelOnline.tour.LineRecommend.LeftLineRecommendSellCache("LeftLinePreferences", linetype, lineclass.ToString(), 3)%></ul>
                    </div>
                </div>--%>
                <div class="left_Recommend">
                    <div class="Recommend_title">最近浏览</div>
                    <div id="Div2" class="Recommend_box">
                        <ul class="view" id="CookieView"></ul>
                    </div>
                </div>
            </div>
            <div class="right_part">
                <%--<div class="right_Recommend">
                    <div class="right_Recommend_title">旅游专家特别推荐</div>
                    <div id="Div3" class="Recommend_box">
                        <div class="onsale_product" style="height:193px;">
                            <% =TravelOnline.tour.LineRecommend.LineRecommendCache("LineListRecommend_" + linetype + "_" + lineclass.ToString(), "NewRecom", "3", "SmallPic", linetype, lineclass.ToString(), 4)%>
                        </div>
                    </div>
                </div>--%>

                <div id="filter" class="right_Recommend" style="margin-bottom: 0px;">
                    <div class="fore1">
                        <dl class="order"><dt>排序：</dt><%=SortString%></dl>
                        <%=TopPages%>
                    </div>
                </div>
                <div id="linelist">
                    <div><%=ListResult%></div>
                </div>
                <div id="bottompages" class="m clearfix"><%=BottomPages%></div>
            </div>
        </div>
    </div>
</div>
<div id="inputs" style="DISPLAY:none">
    <input id="tb_key" type="hidden" value="<%=key %>"/>
    <input id="tb_date" type="hidden" value="<%=date %>"/>
    <input id="tb_dateend" type="hidden" value="<%= dateend %>"/>
    <input id="tb_p1" type="hidden" value="<%=p1 %>"/>
    <input id="tb_p2" type="hidden" value="<%=p2 %>"/>
</div>
<%--<div class="mod_backtop"><a id=gotoTop href="javascript:" title="回顶部" ytag="72300">回顶部</a></div>--%>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#tb_key").val()!="") $("#search_key").val($("#tb_key").val());
        if ($("#tb_key").val() != "") $("#more_search_key").val($("#tb_key").val());
        $("#more_search_date").val($("#tb_date").val());
        $("#more_search_dateend").val($("#tb_dateend").val());
        $("#more_search_price1").val($("#tb_p1").val());
        $("#more_search_price2").val($("#tb_p2").val());
        $("#WeekHotRank li").mouseover(function () {
            $("#WeekHotRank .p-img").hide();
            $("#WeekHotRank .p-price").hide();
            $("#WeekHotRank .p-day").hide();
            $(this).find('.p-img').show();
            $(this).find('.p-price').show();
            $(this).find('.p-day').show();
            $(this).addClass("fore").siblings().removeClass("fore");
        });
        BindHistory();
    })
    
</script>
</body>
</html>