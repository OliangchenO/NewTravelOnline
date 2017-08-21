<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="TravelOnline.NewMaster.header" %>
<div id=shortcut>
    <div class="container">
        <div class="row">
            <div class=span12>
                <div class="collect"><div style="FLOAT: left;PADDING-right: 20px;"><B></B><a onclick="addToFavorite()" href="javascript:void(0)">收藏青旅在线商城</a></div><div style="FLOAT: left;"></div></div>
                <div class="collect" style="FLOAT: right;PADDING-right: 20px;" id="loginarea">
                欢迎您来到青旅！ &nbsp; <a href="javascript:login();"><i class="icon-user"></i> 登录</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href="javascript:regist();"><i class="icon-cog"></i> 注册</a>
                </div>
                <script src="/login/welcome.aspx?uid=<%=ucode %>" type="text/javascript"></script>
                <%=Infos%>
                <%--<ul style="FLOAT: right;">
                    <li>
                        欢迎您，aassqq &nbsp;&nbsp;|&nbsp;&nbsp;
                    </li>
                    <li id="topbar_dropmenu">
                        <div id="topbar_dropmenu_a" class="mod_dropmenu_hd"> <a id="topbar_usercenter" href="javascript:void(0)">会员中心<i class="icon-chevron-down"></i></a></div>
                        <div class="mod_dropmenu_pop">dfsdfdsf</div>
                    </li>
                    <li>
                        &nbsp;&nbsp;|&nbsp;&nbsp; <a href="/login/logout.aspx"><i class=" icon-off"></i> 退出</a>
                    </li>
                </ul>--%>
                <%--<a href="javascript:login();"><i class="icon-user"></i> 登录</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href="javascript:regist();"><i class="icon-cog"></i> 注册</a>--%>
            </div>
        </div>
    </div>
</div>
<div class="container" style="HEIGHT: 90px;">
	<div class="row">
        <div class="span12" style="MARGIN-TOP: 15px">
            <div class="fl"><A href="/"><img src="/Images/logo.gif" width="300" height="57" alt="上海青旅官网" /></A></div>
            <div class="collect fl" style="MARGIN: 7px 0px 0px 40px">
                <div class="collect pull-left">
                    <form class="navbar-form pull-left" onsubmit="javascript:return checkform(this);" name="search" action="/search.html">
                        <input type="text" id="search_key" name="key" tabindex="8" autocomplete="off" value="请输入线路编号、名称或目的地" />
                        <input id="mod_search_btn" tabindex="9" type="submit" title="搜索" /><input id="more_search_btn" tabindex="9" type="button" value="高级搜索" />
                    </form><br/>
                    <div style="MARGIN: 20px 0px 0px 5px;width:250px;">
                        热门搜索：<a href="/outbound/227-110-0.html" target="_blank">香港</a>&nbsp;
                        <a href="/outbound/227-111-0.html" target="_blank">澳门</a>&nbsp;
                        <a href="/outbound/851-112-712.html" target="_blank">台北</a>&nbsp;
                        <a href="/outbound/231-22-184.html" target="_blank">东京</a>&nbsp;
                        <a href="/outbound/489-56-0.html" target="_blank">新西兰</a>
                        
                    </div>
                </div>
                
            </div>
            <div class="fr"><img src="/img/tel.gif" height="57" alt="上海青旅官网" /></div>
            <div id="more_search_toolbars" class="tips">
	            <div class="tips-text">
                    <div class="advance_box" id="SearchProBox" style=""><h2>高级搜索<a style="float:right;" href="javascript:" onclick="hide_more_search()">关闭</a></h2>
                        <ul>
                            <li><strong>关键字</strong><input type="text" id="more_search_key" name="key" autocomplete="off" maxlength="20" style="width: 300px" value="请输入线路编号、名称或目的地"/></li>
                            <li><strong>出发日期</strong><input class="runcode" type="text" id="more_search_date" name="more_search_date" autocomplete="off" maxlength="10" style="width: 100px" readonly="readonly" />  ～　<input class="runcode" type="text" id="more_search_dateend" name="more_search_dateend" autocomplete="off" maxlength="10" style="width: 100px" readonly="readonly" /></li>
                            <li><strong>价格范围</strong><input type="text" id="more_search_price1" name="price" autocomplete="off" maxlength="8" style="width: 100px" />  ～　<input type="text" id="more_search_price2" name="price" autocomplete="off" maxlength="8" style="width: 100px" /></li>
                            <li><strong>线路类型</strong>
                                <div class="advance_select_box">
                                    <label><input type="checkbox" name="type" value="1"/> 出境</label>
                                    <label><input type="checkbox" name="type" value="2"/> 国内</label>
                                    <label><input type="checkbox" name="type" value="3"/> 自由行</label>
                                    <label><input type="checkbox" name="type" value="4"/> 邮轮</label>
                                    <label><input type="checkbox" name="type" value="5"/> 签证</label>
                                </div>
                            </li>
                            <li><strong>天数</strong>
                                <div class="advance_select_box">
                                    <label><input type="checkbox" name="day" value="1"/> 1天</label>
                                    <label><input type="checkbox" name="day" value="2"/> 2天</label>
                                    <label><input type="checkbox" name="day" value="3"/> 3天</label>
                                    <label><input type="checkbox" name="day" value="4"/> 4天</label>
                                    <label><input type="checkbox" name="day" value="5"/> 5天</label>
                                    <label><input type="checkbox" name="day" value="6"/> 6天</label>
                                    <label><input type="checkbox" name="day" value="7"/> 7天</label>
                                    <label><input type="checkbox" name="day" value="8"/> 8天</label>
                                    <label><input type="checkbox" name="day" value="9"/> 9天</label>
                                    <label><input type="checkbox" name="day" value="10"/> 10天以上</label>
                                </div>
                            </li>
                            <li><div style="margin-top: 20px;height: 40px;"><a id="more_search_now" href="javascript:" style="margin-left:70px;color:#fff" onclick="more_search()" class="btn btn-large btn-warning">搜索</a></div></li>
                        </ul>
                     </div>
                     
                </div>
	            <div class="tips-angle diamond"></div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
<script type="text/javascript" src="/js/header.js"></script>
