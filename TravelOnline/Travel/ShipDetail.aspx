<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipDetail.aspx.cs" Inherits="TravelOnline.Travel.ShipDetail" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=LineName %> - <%=titleinfo%> - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/shoppingcart.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/Cruises.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Styles/Hotel.css"/>
    <link type="text/css" rel="stylesheet" href="/Styles/galleryview.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.galleryview-1.1.js"></script>
    <script type="text/javascript" src="/Scripts/lhgdialog/lhgdialog.min.js?s=default,chrome,aero"></script>
    <style>
        #tip {position:absolute;display:none;zIndex:1000}
        #tip s {position:absolute;top:20px;left:-21px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #4BA41B transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
        #tip s i{position:absolute;top:-10px;left:-8px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #fff transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
        #tip .t_box {position:relative;background-color:#CCC;filter:alpha(opacity=50);-moz-opacity:0.5;bottom:-3px;right:-3px;}
        #tip .t_box div{width:300px;position:relative;background-color:#FFF;border:1px solid #4BA41B;padding:5px;top:-3px;left:-3px;}
        a.tip  {
	    text-decoration:none;
	    outline:none;
	    color:#159ce9;
        }
        a.tip :visited {
	        text-decoration:none;
	        outline:none;
	        color:#159ce9;
        }
        a.tip:hover {
	        COLOR: #159ce9; TEXT-DECORATION: underline
        }
        #logul {LINE-HEIGHT: 25px;}
        #logul li {PADDING-LEFT: 5px;PADDING-RIGHT: 5px;BORDER-BOTTOM: #f3e7c7 1px solid;}
        #logul .logtit{LINE-HEIGHT: 20px;background-color:#F7F7F7;}
        .GreenSpan{FONT-SIZE: 14px; FONT-WEIGHT: blod; color:#398510}
    </style>
</head>
<body id="<%=BodyId %>">
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
                    <LI style="PADDING-BOTTOM: 2px;"><input class="runcode" id="Text2" type="text" style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;<a id="SerchNow" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="SerchIt()">搜 索</a></LI>
                </UL>
            </div>
        </div><!--left No.1-->
        <div class="m da211x90" id="IL1"><script type="text/javascript" src="/Js/AD/IL1.js"></script></div>
        <div class="m da211x90" id="IL2"><script type="text/javascript" src="/Js/AD/IL2.js"></script></div>
        <div class="m rank"><div class="mt"><h2>同类线路热卖排行</h2></div><div class="mc"><ul class="tabcon"><%=LineOnHotSale %></ul></div></div><!--rank end-->
        <div id=recent class="m rank"><div class="mt"><h2>最近浏览的线路</h2></div><div class="mc"><ul class="tabcon"><LI><DIV class=iloading>正在加载中，请稍候...</DIV></LI></ul></div></div><!--rank end-->
        <div class="m da211x90" id="IL3"><script type="text/javascript" src="/Js/AD/IL3.js"></script></div>
        <div class="m da211x90" id="IL4"><script type="text/javascript" src="/Js/AD/IL4.js"></script></div>
        <div class="m da211x90" id="IL5"><script type="text/javascript" src="/Js/AD/IL5.js"></script></div>
        <div class="m rank"><div class="mt"><h2>游记攻略</h2></div><div class="mc"><ul class="tabcon"><%=Journal%></ul></div></div><!--rank end-->
    </div><!--left end-->  
      
    <div class="right-extra">
        <div class="crumb"><a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<%=Map1 %>&nbsp;&gt;&nbsp;<%=Map2 %>&nbsp;&gt;&nbsp;<%=LineName %>
    </div>
    <!--crumb end-->

<DIV id=tops class="m select"><DIV class=mt></DIV></DIV>
 <DIV id=preview>
<DIV id=spec-n1 class=piczoom ><%=FirstImg %></DIV>
<DIV id=spec-n5><DIV id=spec-left class=control></DIV><DIV id=spec-right class=control></DIV><DIV id=spec-list><UL class=list-h><%=ImgList %></UL></DIV></DIV>
</DIV>
<UL id=summary>
<DIV id=name>
<H1><%=LineName %><FONT 
style="COLOR: #ff0000" id=advertiseWord></FONT></H1></DIV><!--pname end-->
  <LI><SPAN>线路编号：<%=id %></SPAN></LI>  
  <LI><DIV class=fl>销售价格：<STRONG class=price>￥<%=LinePrice %></STRONG> <%=Preference %></DIV></LI>
  <LI class=clearfix>&nbsp;</LI>
  <LI>出发日期：<STRONG style="font-size: 13px; font-weight: bold"> <%=begindate %></STRONG></LI>
  <%--<LI class=clearfix>&nbsp;</LI>--%>
  <LI id=tz class=hide></LI>
  <LI>
<!-- JiaThis Button BEGIN -->
<div id="ckepop"><span class="jiathis_txt">&nbsp;&nbsp;&nbsp;分享到：</span>
<a class="jiathis_button_tsina"></a>
<a class="jiathis_button_tqq"></a>
<a class="jiathis_button_renren"></a>
<a class="jiathis_button_kaixin001"></a>
<a class="jiathis_button_douban"></a>
<a class="jiathis_button_qzone"></a>
<a class="jiathis_button_tsohu"></a>
<a href="http://www.jiathis.com/share" class="jiathis jiathis_txt jiathis_separator jtico jtico_jiathis" target="_blank"></a>
&nbsp;&nbsp;&nbsp;<iframe width="63" height="24" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0" scrolling="no" frameborder="No" border="0" src="http://widget.weibo.com/relationship/followbutton.php?width=63&height=24&uid=1823201272&style=1&btn=light&dpc=1"></iframe>
</div>
<script type="text/javascript" >
    var jiathis_config = {
        hideMore: false
    }
</script>
<script type="text/javascript" src="http://v2.jiathis.com/code/jia.js" charset="utf-8"></script>
<!-- JiaThis Button END -->
</LI>
</UL><!--infos end-->

<div id=choose class=m>

<div class=btns>
<A id=InitCartUrl class=btn-0yuan  onclick="ScrollTo()">在线购买</A>
<div id="fqPanel" class="fl"></div>
<INPUT id="btn_fav" class=btn-coll onclick="mark(<%=id %>)" value="收 藏" type=button>
<INPUT id="btn_print" class=btn-coll onclick="RoutePrint(<%=id %>)" value="打 印" type=button>
<%--<A class="btn-link" href="/RoutePrint/<%=id %>.html" target="_blank">打 印</A>--%>
<SPAN class=zjrx>产品专家热线：<br>
&nbsp;&nbsp;4006-777-666</SPAN>
<span class=clr></span></div></div>
</div><!--right-extra end-->

<div class="right-extra">
<div id=manager class="m select"><div class=mt><H1></H1><STRONG>特色推荐</STRONG></div><div class="tj"><%=RouteFeature %></div></div>

<div id=PlanDate class="m detail">
<UL class=tab><LI class=curr>行程简介<SPAN></SPAN></LI></UL>
<div class="mc tabcon borders">
    <div class=tripsdiv>
        <DIV id=trips class=trips>
            <DIV class=tripsHead>以下时间仅供参考，请以实际船票为准</DIV>
            <DIV class=tripsBody>
                <DIV class=tripsItem><SPAN class="terms"><STRONG>天数</STRONG></SPAN><SPAN class="port"><STRONG>停靠港口</STRONG></SPAN> <SPAN class="arrive"><STRONG>抵达</STRONG></SPAN> <SPAN class="startUp"><STRONG>启航</STRONG></SPAN></DIV>
                <DIV id=tripList class=tripList>
                    <DIV class=tripStorage>
                    <%--<DIV class=tripsItem><SPAN class=terms>第5天</SPAN> <SPAN class=port>香港</SPAN><SPAN class=arrive>11:00 </SPAN><SPAN class=startUp>--</SPAN></DIV>--%>
                    <%=TripsString %>
                    </DIV>
                </DIV>
            </DIV>
        </DIV>
    </div>
    <div id="RoomPicSlide" class="CR_PicList"><%=CruisesPicString%></div>
</div>
</div>
<A id="RoomHere"></A>
<div id="hoteldetail" class="hoteldetail">
<%--    <UL class=hoteltab>
        <LI class=curr>内仓房<span></span></LI>
        <LI>海景房<span></span></LI>
        <LI>阳台房<span></span></LI>
        <LI>套房<span></span></LI>
    </UL><!--知识库标签-->
    <div id=tabdiv>
        <div class="tabcon borders"><table style="width: 100%;" border="0" cellpadding="0" cellspacing="0"><tr class=tit><td width="25%">房间类型</td><td width="15%">床型</td><td width="10%">早餐 Breakfast</td><td width="10%">宽带 Broadband</td><td class=al width="10%">挂牌价 List Price</td><td class=al width="10%">预定价 Order Price</td><td width="8%">&nbsp;</td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(37)">豪华房/Deluxe Room</a></td><td class=ac>大床 King</td><td class=ac>单早 One</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1745</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(37)">Booking</a></td></tr><tr class=hide id=h37><td colspan="7"><div id=show37></div></td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(38)">豪华房 / Deluxe Room</a></td><td class=ac>双床 Twin</td><td class=ac>双早 Two</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1895</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(38)">预定</a></td></tr><tr class=hide id=h38><td colspan="7"><div id=show38></div></td></tr></table></div>
        <div class="tabcon borders hide"><table style="width: 100%;" border="0" cellpadding="0" cellspacing="0"><tr class=tit><td width="25%">房间类型 </td><td width="15%">Bed Type</td><td width="10%">早餐 Breakfast</td><td width="10%">宽带 Broadband</td><td class=al width="10%">挂牌价 List Price</td><td class=al width="10%">预定价 Order Price</td><td width="8%">&nbsp;</td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(322)">豪华房/Deluxe Room</a></td><td class=ac>大床 King</td><td class=ac>单早 One</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1745</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(371)">Booking</a></td></tr><tr class=hide id=Tr1><td colspan="7"><div id=Div4></div></td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(38)">豪华房 / Deluxe Room</a></td><td class=ac>双床 Twin</td><td class=ac>双早 Two</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1895</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(38)">Booking</a></td></tr><tr class=hide id=Tr2><td colspan="7"><div id=Div5></div></td></tr></table></div>
        <div class="tabcon borders hide"><table style="width: 100%;" border="0" cellpadding="0" cellspacing="0"><tr class=tit><td width="25%">房间类型</td><td width="15%">床型</td><td width="10%">早餐 Breakfast</td><td width="10%">宽带 Broadband</td><td class=al width="10%">挂牌价 List Price</td><td class=al width="10%">预定价 Order Price</td><td width="8%">&nbsp;</td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(37)">豪华房/Deluxe Room</a></td><td class=ac>大床 King</td><td class=ac>单早 One</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1745</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(37)">Booking</a></td></tr><tr class=hide id=Tr3><td colspan="7"><div id=Div1></div></td></tr><tr class=tir><td class=al><a class=roomname href="javascript:;" onclick="RoomClick(38)">豪华房 / Deluxe Room</a></td><td class=ac>双床 Twin</td><td class=ac>双早 Two</td><td class=ac>收费 Charge</td><td class=al><dfn> &yen;4080</dfn></td><td class=al><dfn> &yen;1895</dfn></td><td align="center"><a class=btns href="javascript:void(0);" onclick="Order(38)">预定</a></td></tr><tr class=hide id=Tr4><td colspan="7"><div id=Div2></div></td></tr></table></div>
    </div>--%>
    <%=CruisesRoomString%>
</div>

<div class="clear"><br /></div>

<div id=OrderHere class="m detail">
    <UL class=tab><LI class=currlong>选择的房间和人数<SPAN></SPAN></LI></UL>
    <div class="mc tabcon borders">
        <div style="padding-bottom: 10px;padding-left: 10px;LINE-HEIGHT: 40px; HEIGHT: 40px;COLOR: #ED8611; FONT-SIZE: 14px; FONT-WEIGHT: blod;">
           <table id="Table1" style="width: 100%;" border="0">
                <tr>
                    <td>
                        <span id="ShowPriceText">当前没有选择任何舱房，请选择后再预定</span><span id="ShowPriceCount" class=pcount></span>
                    </td>
                    <td width="160px">
                        <a id=OrderBtn class="btn_book_1" href="javascript:void(0)" onclick="Order()" >开始预订</a><span id="islogin" style="display: none;FLOAT: left; FONT-SIZE: 14px; " class="iloading1">正在提交，请稍候...</span>
                    </td>
                </tr>
            </table>
        </div>
        
        <div id=roomdiv class=roomdiv>
            <DIV class=roomHead>双人间最少入住2人，不满2人需要补房差；三人间或四人间同舱的第3、第4位可享受价格优惠；</DIV>
            <form id="form_data" onsubmit="return false;" method="post">
                <DIV id="inputs" style="DISPLAY:none">
                    <input id="TB_LineId" name="TB_LineId" type="hidden" value="<%=id %>"/>
                    <input id="TB_BeginDate" name="TB_BeginDate" type="hidden" value="<%=begindate %>"/>
                    <input id="AllPeople" name="AllPeople" type="hidden" value="0"/>
                    <input id="AllAdult" name="AllAdult" type="hidden" value="0"/>
                    <input id="AllChilds" name="AllChilds" type="hidden" value="0"/>
                    <input id="AllRoom" name="AllRoom" type="hidden" value="0"/>
                    <input id="AllPrice" name="AllPrice" type="hidden" value="0"/>
                </DIV>
                <table id="RoomSelectList" class="hide" style="width: 100%;">
                    <tr class=tit>
                        <td width="30%">房间类型</td>
                        <td width="5%">成人</td>
                        <td width="5%">儿童</td>
                        <td width="5%">房间数</td>
                        <td width="10%">第1、2人价格</td>
                        <td width="10%">第3成人价</td>
                        <td width="10%">第3儿童价</td>
                        <td width="10%">价格小计</td>
                        <td width="3%">&nbsp;</td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</div>

<span class=clr></span>
</div><!--right-extra end-->

<div class="right-extra">
<div id="detail" class="m detail">
<UL class=tab>
<LI class=curr data="d-all">行程安排<span></span></LI>
<LI data="d-fuwu">服务标准<span></span></LI>
<LI data="d-feiyong">费用描述<span></span></LI>
<LI data="d-zhuyi">温馨提醒<span></span></LI>
<LI data="d-xuzhi">预订须知<span></span></LI>
</UL><!--知识库标签-->
<div id="d-all" class="mc fore tabcon">
<div id="detaillist" class="m"><div class="mc"><%=RouteInfos %></div></div><!--行程内容 end-->
<div id="d-fuwu-ct"><%=RouteServiceInfos%></div><!--服务标准 end-->
<div id="d-feiyong-ct"><%=RoutePriceInfos%></div><!--报价包含不包含和自费项目 end-->
<div id="d-zhuyi-ct"><%=RouteAttentionsInfos%></div><!--温馨提醒及购物商店 end-->
<div id="d-xuzhi-ct"><%=RegularOrderProcess%><%=RegularContractInfos %><%=RegularPayInfos %></div><!--预订须知等 end-->
</div><!--tabcon end-->
<div id="d-fuwu" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-feiyong" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-zhuyi" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-xuzhi" class="mc tabcon hide"></div><!--tabcon end-->
</div><!--detail end-->
</div>
<div class="right-extra"></div><!--right-extra end-->
</div><!--w main end-->
<span class=clr></span>
<div id="tip"><div class="t_box"><div><s><i></i></s><span id=olog>本起价是可选出发日期中，按双人出行共住一间房核算的最低单人价格。产品价格会根据您所选择的出发日期、出行人数、入住酒店房型、航班或交通以及所选附加服务的不同而有所差别。</span></div></div></div>
<SCRIPT type="text/javascript">
    function SerchIt() {
        if ($("#Text1").val() == "" && $("#Text2").val() == "") {
            alert("请输入目的地、线路名称（或编号）、出发日期");
            $("#SerchNow").attr("href", "javascript:void(0);");
            return false;
        }
        $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
    }

    $(function () {
        $('#Text2').calendar({ minDate: '%y-%M-%d', btnBar: false });
    });

    function Order() {
        if($("#AllRoom").val()=="0") {
            alert("请选择您需要预定的房间数量！");
            return false;
        }
        var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                OrderNow();
            }
            else {
                LoginNow();
            }
        })
    }

    function OrderNow() {
        if ($(".thickbox").length != 0) {
            jdThickBoxclose()
        }
        $("#islogin").show();
        $("#OrderBtn").hide();
        
        var url = "/CruisesOrder/AjaxService.aspx?action=OrderSubmit&r=" + Math.random();
        $.post(url, $("#form_data").serialize(), function (data) {
            var obj = eval(data);
            if (obj.success) {
                top.location = "/CruisesOrder/FirstStep/" + obj.success + ".html";
            }
            if (obj.error) {
                $("#islogin").hide();
                $("#OrderBtn").show();
                alert(obj.error);
            }
        });
    }

    function RoutePrint(Cid) {
        window.open("/RoutePrint/" + Cid + ".html");
    }


    $(".RoomSelectButton  strong").click(function () {
        var href = "OrderHere";
        var pos = $(href).offset().top;
        $("html,body").animate({ scrollTop: pos }, 1000);
        return false;
    });
  
</SCRIPT>
<script type="text/javascript" src="/Scripts/CruisesOrder.js"></script>
<script type="text/javascript" src="/Scripts/Cruises.Pages.js"></script>
<script type="text/javascript" src="/Scripts/base.lib.js"></script>

<%--<%=PlanScripts %><script type="text/javascript" src="/Js/PlanDate/<%=id %>.js"></script>--%>
<script type="text/javascript" src="/Scripts/base.product.js"></script>

<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


