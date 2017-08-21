<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="TravelOnline.Master.Header" %>
<div id=shortcutnew>
    <div class="container">
        <div class="row">
            <div class=span12>
                <div class="collect"><div style="FLOAT: left;PADDING-right: 20px;"><B></B><a onclick="addToFavorite()" href="javascript:void(0)">收藏青旅在线商城</a></div><div style="FLOAT: left;"></div></div>
                <div class="collect" style="FLOAT: right;PADDING-right: 20px;" id="loginarea">
                欢迎您来到青旅！ &nbsp; <a href="javascript:login();"><i class="icon-user"></i> 登录</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href="javascript:regist();"><i class="icon-cog"></i> 注册</a>
                </div>
                <script src="/login/welcome.aspx?uid=<%=ucode %>" type="text/javascript"></script>
                <%=Infos%>
            </div>
        </div>
    </div>
</div>
<div class="container" style="HEIGHT: 90px;">
	<div class="row">
        <div class="span12" style="MARGIN-TOP: 15px">
            <div class="fl"><A href="/"><img src="/Images/logo.gif" width="300" height="57" alt="上海青旅官网" /></A></div>
            <%--<div class="collect fl" style="MARGIN: 0px 0px 0px 50px">
                <div class="collect pull-left" style="">
                    <form class="navbar-form pull-left" onsubmit="javascript:return checkform(this);" name="search" action="/search.html">
                        <input class="serch_input" type="text" id="search_key" name="key" tabindex="8" autocomplete="off" value="请输入线路编号、名称或目的地" />
                        <input id="mod_search_btn" tabindex="9" type="submit" title="搜索" /><input id="more_search_btn" tabindex="9" type="button" value="高级搜索" />
                    </form>
                </div>
            </div>--%>
            <div class="fr"><img src="/img/tel.gif" height="57" alt="上海青旅官网" /></div>
        </div>
    </div>
</div>
<script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
<script type="text/javascript" src="/js/header.js"></script>
<div id="menu" style="margin-bottom: 10px;">
    <div class="container" >
        <div class="row">
            <div class="span12" style="background:#01AA07;">
                <ul class="menulink" style="margin-bottom: 0px;">
                    <li><a href="/">首页</a></li>
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
