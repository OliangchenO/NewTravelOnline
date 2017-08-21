<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FreeTour.aspx.cs" Inherits="TravelOnline.Travel.FreeTour" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自由行旅游 - <% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourTitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.FreeTourKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript">
        (function () { var _sn = ["Styles/slide_OutBound"]; var _su = "/"; var _sw = screen.width; var _se, _st; for (i in _sn) { _se = document.createElement("link"); _se.type = "text/css"; _se.rel = "stylesheet"; if (_sw >= 1280) { _st = _su + _sn[i] + ".w.css" } else { _st = _su + _sn[i] + ".css" } _se.href = _st; document.getElementsByTagName("head")[0].appendChild(_se) } })()
    </script>
</head>
<body id="auction">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <div class="w main">
    <div class=left>
        
        <div id=right-report class="m right-report r-report">
            <div class=mt><H2>旅游线路搜索</H2><div class=extra></div></div>
            <div class=mc>
                <UL>
                    <LI>目的地、线路名称或编号</LI>
                    <LI><input id="Text1" type="text" style="width: 190px" /></LI>
                    <LI>出发日期</LI>
                    <LI style="PADDING-BOTTOM: 2px;"><input class="runcode" id="Text2" type="text" style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;<a id="SerchNow" href="127.0.0.1" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="SerchIt()">搜 索</a></LI>
                </UL>
            </div>
        </div><!--left No.1-->

        <div class="m" id="comment">
			<div class="mt">
				<h2>出境须知</h2>
			</div>
			<div class="mc">
            <UL>
                    <LI>·<A href="news.aspx?id=3838" target=_blank>出境须知</A></LI>
                    
                   
                </UL>
            </div>
		</div><!--comment end-->
        
        <div class="m rank">
	        <div class="mt">
		        <h2>一周热卖排行榜</h2>
	        </div>
	        <div class="mc">
		        <ul class="tabcon">
                <%=LineOnHotSale %>
                </ul>
	        </div>
        </div><!--rank end-->
                
        <%--<div class="m" id="comment" clstag="homepage|keycount|homepage|comment">
			<div class="mt">
				<h2>最热评价</h2>
			</div>
			<div class="mc"><ul><li class='fore'><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/351107_4cb14760-ecf2-4eb2-8718-fd2bf0fc1d22_1.html' target='_blank'>到手了，晒~</a></h5><div class='content'><a href='127.0.0.1/product/351107.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/4871/d3144894-49f8-4ab7-a2eb-9f82a1309457.jpg'></a><a href='http://club.360buy.com/bbsDetail/351107_4cb14760-ecf2-4eb2-8718-fd2bf0fc1d22_1.html' target='_blank'>一直在等戴妃降价，却入手了这部。。。座充很有特色，像滑盖手机；背板比想象中光滑，可以当镜子；2G卡已经在机身里面了。需要参考的就请看图片吧。（限制只能上十张图？）</a></div><span class='ammount'>共28条回复</span><span class='user'><a href='http://club.360buy.com/userreview/11803265-1-1.html' target='_blank'>Hewsun</a></span></li><li><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/125851_9c7b8a28-7560-42d1-83f3-0a0be015842f_1.html' target='_blank'>外观苗条，非常好看！节约空间！风力够大！</a></h5><div class='content'><a href='127.0.0.1/product/125851.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/724\59820026-6085-4029-88fc-4ba5d4501cdf.jpg'></a><a href='http://club.360buy.com/bbsDetail/125851_9c7b8a28-7560-42d1-83f3-0a0be015842f_1.html' target='_blank'>很不错 1、安装简单方便，女孩，老人都能应付。 2、送风均匀、柔和，感觉比老式风扇舒服多了 3、比老式风扇小巧，2个手指就能拎走。 4、占地方少，不碍事。</a></div><span class='ammount'>共8条回复</span><span class='user'><a href='http://club.360buy.com/userreview/518454-1-1.html' target='_blank'>weixin晒单专用</a></span></li><li><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/365001_ad957c37-daf8-4607-aa59-cf35dd2b7dbd_1.html' target='_blank'>晒晒昨天刚到手的加大蒙古包蚊帐</a></h5><div class='content'><a href='127.0.0.1/product/365001.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/1471/f5f5fdfb-81b3-4fb5-bfa4-9152e40f006d.jpg'></a><a href='http://club.360buy.com/bbsDetail/365001_ad957c37-daf8-4607-aa59-cf35dd2b7dbd_1.html' target='_blank'>前天晚上11点下的订单，昨天下午3点就到了。快递神速&nbsp;，快递员服务也不错。蚊帐呢，刚打开包装看到那么多根管子，不知道该从哪里下手&nbsp;。后来看安装说...</a></div><span class='ammount'>共4条回复</span><span class='user'><a href='http://club.360buy.com/userreview/3364926-1-1.html' target='_blank'>greatdave</a></span></li></ul></div>
		</div><!--comment end-->--%>

        <div class="m da211x90" id="IL1"><script type="text/javascript" src="/Js/AD/IL1.js"></script></div>
        <div class="m da211x90" id="IL2"><script type="text/javascript" src="/Js/AD/IL2.js"></script></div>
        <div class="m da211x90" id="IL3"><script type="text/javascript" src="/Js/AD/IL3.js"></script></div>
        <div class="m da211x90" id="IL4"><script type="text/javascript" src="/Js/AD/IL4.js"></script></div>
        <div class="m da211x90" id="IL5"><script type="text/javascript" src="/Js/AD/IL5.js"></script></div>
        <div class="m da211x90" id="IL6"><script type="text/javascript" src="/Js/AD/IL6.js"></script></div>
        
    </div><!--left end-->    

    <div class="right-extra">
        <div class=middle>
            <div id=slide></div>
            <script type="text/javascript" src="/Js/AD/FreeTour.js"></script><!--slide end-->
            
            <div id=madding class=m>
            <div class=mt>
            <H2>自由行特惠推荐</H2>
            <div class=extra></div></div>
            <div id=madding-1>
            <div style="MARGIN: 73px auto" class=iloading>正在加载中，请稍候...</div></div>
            <script type="text/javascript" src="/Js/Preferences/FreeTour.js"></script>
            <script type="text/javascript" src="/Scripts/Preferences.js"></script>
            </div> <!--madding end-->
           
        </div> <!--middle end-->
       
        <div class=right>
            <div id=report class=m>
                <div class=mt>
                    <H2>自由行旅游资讯</H2>
                    <div class=extra><A href="/News.html">更多&gt;&gt;</A></div>
                </div>
                <div class=mc>
                    <UL>
                        <%=OutBoundAffiche%>
                    </UL>
                </div>
            </div><!--report end-->
            <div class="m da211x90" id="IR1"><script type="text/javascript" src="/Js/AD/IR1.js"></script></div>
            <div class="m da211x90" id="IR2"><script type="text/javascript" src="/Js/AD/IR2.js"></script></div>
        </div><!--right end-->
    </div><!--right-extra end-->


    <div class="right-extra">
        <%=OutBoundProductHtml %>
    </div><!--right-extra end-->
    </div><!--w main end-->
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/base.lib.js"></script>
    <script type="text/javascript" src="/Js/ProductDetail/FreeTour.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Text2').calendar({ minDate: '%y-%M-%d', btnBar: false });
        });

        function SerchIt() {
            if ($("#Text1").val() == "" && $("#Text2").val() == "") {
                alert("请输入目的地、线路名称（或编号）、出发日期");
                $("#SerchNow").removeAttr("target");
                $("#SerchNow").attr("href", "javascript:void(0);");
                return false;
            }
            $("#SerchNow").attr("target", "_blank");
            $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
        }
	</script>
</body>
</html>

