<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="linelist.aspx.cs" Inherits="TravelOnline.tour.linelist" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleString %><% =TravelOnline.Class.Common.PublicPageKeyWords.PublicLineListTitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/linelist.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/js/setcookie.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
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

                <div class="left_Recommend">
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
                </div>
                <div class="left_Recommend">
                    <div class="Recommend_title">最近浏览</div>
                    <div id="Div2" class="Recommend_box">
                        <ul class="view" id="CookieView"></ul>
                    </div>
                </div>
            </div>
            <div class="right_part">
                <div class="right_Recommend">
                    <div class="right_Recommend_title">旅游专家特别推荐</div>
                    <div id="Div3" class="Recommend_box">
                        <div class="onsale_product" style="height:193px;">
                            <% =TravelOnline.tour.LineRecommend.LineRecommendCache("LineListRecommend_" + linetype + "_" + lineclass.ToString(), "NewRecom", "3", "SmallPic", linetype, lineclass.ToString(), "", "", 4)%>
                        </div>
                    </div>
                </div>
                <div class="right_Recommend">
                    <div class="right_Recommend_title">旅游产品筛选<a href="<%=ResetSort %>" style="float:right;font-size:12px;margin-right:10px;font-weight:normal;">重置筛选条件</a></div>
                    <div id="select" class="Recommend_select">
                        <dl>
                            <dt>目的地：</dt>
                            <dd><%=Province_Sort%></dd>
                        </dl>
                        <dl class="<%=hide %>">
                            <dt>城市：</dt>
                            <dd><%=City_Sort%></dd>
                        </dl>
                        <dl>
                            <dt>价格：</dt>
                            <dd><%=Price_Sort%></dd>
                        </dl>
                        <dl>
                            <dt>天数：</dt>
                            <dd><%=Day_Sort%></dd>
                        </dl>
                        <dl>
                            <dt>旅游主题：</dt>
                            <dd><%=Topic_Sort%></dd>
                        </dl>
                    </div>
                </div>

                <div id="filter" class="right_Recommend" style="margin-bottom: 0px;">
                    <div class="fore1">
                        <dl class="order"><dt>排序：</dt><%=Sort_String%></dl>
                        <%=TopPages%>
                    </div>
                </div>

                <div id="linelist">
                    <div>
                        <%--<dl>
                            <dt><a href="/OutBound/227/12132.html" target="_blank"><img onerror="this.src='/Images/none.gif'" src="/Images/Views/200905/M_0905271614621.jpg"></a></dt>
                            <dd>
                                <div style="width:860px;height: 80px;">
                                    <div style="FLOAT: left;width:710px;height: 80px;">
                                        <div class="p-name" style=""><a href="/OutBound/231/6765.html" target="_blank">畅游日本~东京箱根京都大阪六日精选游畅游日本</a><span class="label label-success">热卖</span><span class="label label-important">纯玩</span><span class="badge">1</span></div>
                                        <div>日本常规行程，适合第一次去日本的客人</div>
                                        <div><span class="label label-warning">6天</span>出发班期：10/16, 10/23, 10/30...</div>
                                    </div>
                                    <div style="padding-left:5px;FLOAT: right;width:130px;height: 80px;border-left:1px solid #DDDDDD;">
                                        <div class="p-price">￥5480<em>起</em></div>
                                        <div class="pd">编号：12322</div>
                                        <div class="pd"><a href="#" class="btn btn-small btn-success">收藏</a> <a href="#" class="btn btn-small btn-warning">预定</a></div>
                                    </div>
                                </div>
                            </dd>
                        </dl>--%>
                        <%=ListResult%>
                    </div>
                </div>
                <%--<div><%=texts%></div>--%>
                <div id="bottompages" class="m clearfix"><%=BottomPages%></div>
            </div>
        </div>
    </div>
</div>
<%--<div class="mod_backtop"><a id=gotoTop href="javascript:" title="回顶部" ytag="72300">回顶部</a></div>--%>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
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

    function favorite(id) {
        var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                url = "/tour/ajaxservice.aspx?action=Favorite&id=" + id + "&r=" + Math.random();
                $.getJSON(url, function (date) {
                    if (date.success == 0) {
                        jSuccess("<strong>" + date.content + "</strong>", { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    }
                    else {
                        jError('<strong>收藏失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
                    }
                })
            }
            else {
                jError('<strong>您还没有登录，请登录后再操作!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
        })
    }
</script>
</body>
</html>