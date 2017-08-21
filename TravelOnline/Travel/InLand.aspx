<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InLand.aspx.cs" Inherits="TravelOnline.InLand" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>国内旅游 - <% =TravelOnline.Class.Common.PublicPageKeyWords.InLandTitle%></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.InLandKeywords %> />
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
<body id="tuan">
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
                <LI>·<A href="#" target=_blank>出境须知</A></LI>                   
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
            <script type="text/javascript" src="/Js/AD/InLand.js"></script><!--slide end-->
            
            <div id=madding class=m>
            <div class=mt>
            <H2>特惠推荐</H2>
            <div class=extra></div></div>
            <div id=madding-1>
            <div style="MARGIN: 73px auto" class=iloading>正在加载中，请稍候...</div></div>
            <script type="text/javascript" src="/Js/Preferences/InLand.js"></script>
            <script type="text/javascript" src="/Scripts/Preferences.js"></script>
            </div> <!--madding end-->
           
        </div> <!--middle end-->
       
        <div class=right>
            <div id=report class=m>
                <div class=mt>
                    <H2>国内旅游资讯</H2>
                    <div class=extra><A href="/News.html">更多&gt;&gt;</A></div>
                </div>
                <div class=mc>
                    <UL>
                        <%=InLandAffiche%>
                    </UL>
                </div>
            </div><!--report end-->

            <div class="m da211x90" id="IR1"><script type="text/javascript" src="/Js/AD/IR1.js"></script></div>
            <div class="m da211x90" id="IR2"><script type="text/javascript" src="/Js/AD/IR2.js"></script></div>
            
        </div><!--right end-->
    </div><!--right-extra end-->

    <div class="right-extra">
        <%=InLandProductHtml%>          
    </div><!--right-extra end-->
    </div><!--w main end-->
    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/base.lib.js"></script>
    <script type="text/javascript" src="/Js/ProductDetail/InLand.js"></script>
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
