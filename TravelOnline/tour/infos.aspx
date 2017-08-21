<%@ OutputCache Duration="60" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infos.aspx.cs" Inherits="TravelOnline.tour.infos" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleString %> <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicLineListTitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =Keywords %> />
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
                <div class="left_Recommend <%=NewsHide %>">
                    <div id="infos" class="Recommend_title">资讯类别</div>
                    <div class="Recommend_box">
                        <ul class="infos">
                            <li><a href="/info/index_news.html">青旅快讯</a></li>
                            <li><a href="/info/outbound_news.html">出国旅游快讯</a></li>
                            <li><a href="/info/inland_news.html">国内旅游快讯</a></li>
                            <li><a href="/info/freetour_news.html">自由行快讯</a></li>
                            <li><a href="/info/cruises_news.html">邮轮旅游快讯</a></li>
                            <li><a href="/info/visa_news.html">签证相关快讯</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_Recommend <%=ServiceHide %>">
                    <div class="Recommend_title">服务信息</div>
                    <div class="Recommend_box">
                        <ul class="infos" id="service">
                            <li><a href="/service/aboutus.html">关于我们</a></li>
                            <li><a href="/service/contactus.html">联系我们</a></li>
                            <li><a href="/service/joinus.html">人才招聘</a></li>
                            <li><a href="/service/partner.html">同行合作</a></li>
                            <li><a href="/service/advertising.html">广告服务</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_Recommend <%=ServiceHide %>">
                    <div class="Recommend_title">办公电话</div>
                    <div class="Recommend_box">
                        <ul class="tels" id="Ul1">
                            <li>服务时间：8:00－18：00</li>
                            <li>&nbsp;&nbsp;&nbsp;工作日：周一至周日</li>
                            <li>呼叫中心：<span>4006 777 666</span></li>
                            <li>办公电话：<span>021-64330000</span></li>
                            <li>办公传真：<span>021-64330507</span></li>
                            <li class="email">客服邮箱：<br/><a href="mailto:service@scyts.com">service@scyts.com</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_Recommend <%=ServiceHide %>">
                    <div class="Recommend_title">顾客投诉</div>
                    <div class="Recommend_box">
                        <ul class="tels" id="Ul2">
                            <li class="email">投诉信箱：<br/><a href="mailto:tousu@scyts.com">tousu@scyts.com</a></li>
                        </ul>
                    </div>
                </div>
                <div class="left_Recommend <%=JournaHide %>">
                    <div class="Recommend_title">相关旅游线路</div>
                    <div class="Recommend_box">
                        <ul class="alsobuy"><% =JournalLineList %></ul>
                    </div>
                </div>
                <div class="left_Recommend <%=JournaListHide %>">
                    <div class="Recommend_title">特惠线路推荐</div>
                    <div class="Recommend_box">
                        <ul class="alsobuy"><% =JournalLineRecommend%></ul>
                    </div>
                </div>
                <div id="JournaListHide" class="left_Recommend <%=CookieViewHide %>">
                    <div class="Recommend_title">最近浏览</div>
                    <div class="Recommend_box">
                        <ul class="view" id="CookieView"></ul>
                    </div>
                </div>
            </div>
            <div class="right_part <%=NewsListHide %>">
                <div class="news">
                    <h2><%=TitleString %></h2>
                    <div id="NewsList" class="newlist"><ul><%=ListResult%></ul></div>
                </div>
                <div id="bottompages" class="m clearfix" style="padding:5px 20px 20px 20px;"><%=BottomPages%></div>
                <div class="right_Recommend">
                    <div class="right_Recommend_title">特别推荐</div>
                    <div id="Div3" class="Recommend_box">
                        <div class="onsale_product" style="height:193px;">
                            <%=RecommendList%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right_part <%=NewsContentHide %>">
                <div class="news newscontent">
                    <h3><%=NewsTitle %></h3>
                    <div class="summary"><%=NewsTime%></div>
                    <div class="content"><%=NewsContent%></div>
                </div>
                <div class="right_Recommend <%=JournaHide %>"  style="margin-top:20px;">
                    <div class="right_Recommend_title">游记推荐</div>
                    <div id="Div1" class="Recommend_box">
                        <div class="onsale_product" style="height:193px;">
                            <ul class="Journal"><%=JournaRecomm %></ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div id="inputs" style="DISPLAY:none">
    <input id="tb_pagetype" type="hidden" value="<%=pagetype %>"/>
</div>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        var flag = $("#tb_pagetype").val();
        if (flag == "journallist" || flag == "showjournal") BindHistory();
    })
</script>
</body>
</html>
