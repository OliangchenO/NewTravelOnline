<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelTemp.aspx.cs" Inherits="TravelOnline.Model.ModelTemp" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>页面标题 - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/linelist.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <style type="text/css">
        
    </style>
    <script type="text/javascript">
        if (screen.width >= 1280) {
            //自定义宽屏css
            document.write("<style type='text/css'></style>");
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


<%--自定义内容 begin--%>
<%--导航面包屑 begin--%>
<div class="container" >
    <div class="row">
        <div class="span12">
            <ul class="breadcrumb">
                <li><a href="/">首页</a> <span class="divider">/</span></li>
                <li><a href="/tour/outbound.aspx">出国旅游</a> <span class="divider">/</span></li>
                <li><a href="/outbound/491-0-0.html">东南亚</a> <span class="divider">/</span></li>
                <li class="active">泰国</li>
            </ul>
        </div>
    </div>
</div>
<%--导航面包屑 end--%>

<%--两列式样 begin--%>
<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            <%--内容都放到span12的这个div里--%>
		    <div class="left_part">
                <div class="left_Recommend">
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
            </div>

            <div class="right_part">
                <div class="news newscontent">
                    <h3>文章标题</h3>
                    <div class="summary">简介信息</div>
                    <div class="content">文章内容正文</div>
                </div>
            </div>

        </div>
	</div>
</div>
<%--两列式样 end--%>

<%--一列式样 begin--%>
<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            <%--内容放到span12的这个div里--%>
            <div class="news newscontent">
                <h3>文章标题</h3>
                <div class="summary">简介信息</div>
                <div class="content">文章内容正文</div>
            </div>
        </div>
	</div>
</div>
<%--一列式样 end--%>
<%--自定义内容 end--%>

<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">

</script>
</body>
</html>