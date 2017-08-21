<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManagerHeader.ascx.cs" Inherits="TravelOnline.Master.ManagerHeader" %>
<div id=shortcut>
    <div class=w>
        <div class=collect><B></B><A onclick=addToFavorite() href="javascript:void(0)">收藏青旅商城后台管理</A></div>
        <ul>
            <li id=loginfo class=fore1>您好！欢迎来到青旅商城后台管理！请登录后再操作！</li>
        </ul>
        <span class=clr></span>
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
            <%--<div id="more_search_toolbars" class="tips">
	            <div class="tips-text">
                    <div class="advance_box" id="SearchProBox" style=""><h2>高级搜索<a style="float:right;" href="javascript:" onclick="hide_more_search()">关闭</a></h2>
                        <ul>
                            <li><strong>关键字</strong><input type="text" id="more_search_key" name="key" autocomplete="off" maxlength="20" style="width: 300px" value="请输入线路编号、名称或目的地"/></li>
                            <li><strong>出发日期</strong><input class="runcode" type="text" id="more_search_date" name="more_search_date" autocomplete="off" maxlength="10" style="width: 100px" readonly="readonly" />  ～　<input class="runcode" type="text" id="more_search_dateend" name="more_search_dateend" autocomplete="off" maxlength="10" style="width: 100px" readonly="readonly" /></li>
                            <li><strong>价格范围</strong><input type="text" id="more_search_price1" name="price" autocomplete="off" maxlength="8" style="width: 100px" />  ～　<input type="text" id="more_search_price2" name="price" autocomplete="off" maxlength="8" style="width: 100px" /></li>
                            <li><strong>线路类型</strong>
                                <div class="select_box">
                                    <label><input type="checkbox" name="type" value="1"/> 出境</label>
                                    <label><input type="checkbox" name="type" value="2"/> 国内</label>
                                    <label><input type="checkbox" name="type" value="3"/> 自由行</label>
                                    <label><input type="checkbox" name="type" value="4"/> 邮轮</label>
                                    <label><input type="checkbox" name="type" value="5"/> 签证</label>
                                </div>
                            </li>
                            <li><strong>天数</strong>
                                <div class="select_box">
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
                            <li><div style="margin-top: 20px;height: 40px;"><a id="more_search_now" href="javascript:" style="margin-left:70px;color:#fff" onclick="more_search()" class="btn btn-large btn-warning">搜素</a></div></li>
                        </ul>
                     </div>
                     
                </div>
	            <div class="tips-angle diamond"></div>
            </div>--%>
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

<script type="text/javascript">
    function addToFavorite() {
        var a = "http://www.scyts.com/manage/Login.aspx";
        var b = "上海青旅在线商城后台管理";
        if (document.all) {
            window.external.AddFavorite(a, b)
        } else if (window.sidebar) {
            window.sidebar.addPanel(b, a, "")
        } else {
            alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。")
        }
    }
</script>