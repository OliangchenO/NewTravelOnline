<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineDetail.aspx.cs" Inherits="TravelOnline.Travel.LineDetail" %>
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
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/date.js"></script>
    <script type="text/javascript"src="/Scripts/jquery.datePicker.min-2.1.2.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <style>
    #tip {position:absolute;display:none;}
    #tip s {position:absolute;top:20px;left:-21px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #4BA41B transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
    #tip s i{position:absolute;top:-10px;left:-8px;display:block;width:0px;height:0px;font-size:0px;line-height:0px;border-color:transparent #fff transparent transparent;border-style:dashed solid dashed dashed;border-width:10px;}
    #tip .t_box {position:relative;background-color:#CCC;filter:alpha(opacity=50);-moz-opacity:0.5;bottom:-3px;right:-3px;}
    #tip .t_box div{width:220px;position:relative;background-color:#FFF;border:1px solid #4BA41B;padding:5px;top:-3px;left:-3px;}
    #logul {LINE-HEIGHT: 25px;}
    #logul li {PADDING-LEFT: 5px;PADDING-RIGHT: 5px;BORDER-BOTTOM: #f3e7c7 1px solid;}
    #logul .logtit{LINE-HEIGHT: 20px;background-color:#F7F7F7;}
    #summary .msprice {text-decoration:line-through;FONT-FAMILY: Arial Unicode MS; COLOR: #999; FONT-SIZE: 20px; FONT-WEIGHT: blod; MARGIN-RIGHT: 18px}
    .zjrx {MARGIN-LEFT: 10px;FONT-SIZE: 14px;}
    .GreenSpan{FONT-SIZE: 14px; FONT-WEIGHT: blod; color:#398510}
    </style>
</head>
<body id="<%=BodyId %>">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <div class="w main">
    <div class=left>
        <div id=right-report class="m right-report r-report1">
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
  <%--<LI class=clearfix><SPAN class=fl>商品评分：</SPAN> <DIV id=star418674 class=fl><DIV class="star sa5"></DIV></DIV></LI>
  <LI class=clearfix><SPAN class=fl>门店价格：</SPAN> <STRONG class=msprice> ￥<%=MsPrice %> </STRONG></LI>
  <LI><DIV class=fl>网上预订：<STRONG class=price>￥<%=LinePrice %></STRONG></DIV><SPAN id=promotion1></SPAN><A id="priceexplain" href="javascript:void(0)">(起价说明)</A><DIV class=clr></DIV></LI>--%>
  <LI><DIV class=fl>销售价格：<STRONG class=price>￥<%=LinePrice %></STRONG> <%=Preference %></DIV></LI>
  <LI class=clearfix>&nbsp;</LI>
  <LI id=cx class=hide></LI>
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


<%--  <SPAN>推荐分享：</SPAN> 
  <DIV>
  <A id=site-sina title=推荐到新浪微博 onclick="tj('http://v.t.sina.com.cn/share/share.php?appkey=2000229224','sina')" href="javascript:void(0)"></A>
  <A id=site-qzone title=推荐到腾讯微博 onclick="tj('http://v.t.qq.com/share/share.php?source=1000002&amp;site=http://www.scyts.com','qzone')" href="javascript:void(0)"></A>
  <A id=site-renren title=推荐到人人 onclick="tj('http://share.renren.com/share/buttonshare/post/1004?','renren')" href="javascript:void(0)"></A>
  <A id=site-kaixing title=推荐到开心网 onclick="tj('http://www.kaixin001.com/repaste/share.php?','kaixing')" href="javascript:void(0)"></A>
  <A id=site-douban title=推荐到豆瓣 onclick="tj('http://www.douban.com/recommend/?','douban')" href="javascript:void(0)"></A>
  <A id=site-msn title=推荐到MSN onclick="tj('http://profile.live.com/badge/?','MSN')" href="javascript:void(0)"></A>
    <script type="text/javascript">
  	    var sinaurl = "";
  	    function tj(url, stype) {
  	        var title = "<%=LineName %>";
  	        var content = "我在@上海青旅商城 发现了一条非常不错的旅游线路：<%=LineName %>，价格：￥<%=LinePrice %>。感觉不错，分享一下";
  		    var img = "<%=Pics %>";
  		    var productUrl = location.href;
  		    if (stype == "qzone") {
  			    url = url + "&title=" + content + "&pic=" + img + "&url=" + productUrl;
  		    }
  		    if (stype == "sina") {
  			    url = url + "&title=" + content + "&pic=" + img + "&url=" + productUrl;
  			}
  		    if (stype == "renren") {
  			    url = url + "title=" + title + "&content=" + content + "&pic=" + img + "&url=" + productUrl;
  		    }
  		    if (stype == "kaixing") {
  			    url = url + "rtitle=" + title + "&rcontent=" + content + "&rurl=" + productUrl;
  		    }
  		    if (stype == "douban") {
  			    url = url + "title=" + title + "&comment=" + content + "&url=" + productUrl;
  		    }
  		    if (stype == "MSN") {
  			    url = url + "url=" + productUrl + "&title=" + title + "&description=" + content + "&screenshot=" + img;
  		    }
  			window.open(encodeURI(url), "", "height=500, width=600");
  	    }
    </script>
  </DIV>--%></LI>
</UL><!--infos end-->

<div id=choose class=m>
<DL class=amount>
  <DT>
 成人 <select id="crnum" name="crnum" style="width: 35px">
<option value="1">1</option>
<option value="2" selected="selected">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
</select> &nbsp;
 儿童 <select id="etnum" name="etnum" style="width: 35px">
<option value="0">0</option>
<option value="1">1</option>
<option value="2">2</option>
<option value="3">3</option>
<option value="4">4</option>
<option value="5">5</option>
<option value="6">6</option>
<option value="7">7</option>
<option value="8">8</option>
<option value="9">9</option>
</select>
  </DT>
  <DD><%--<div style="width: 60px"><A class=reduce onclick="setAmount.reduce('#pamount')" href="javascript:void(0)">-</A>
  <INPUT id=pamount onkeyup="setAmount.modify('#pamount')" value=1 type=text>
  <A class=add onclick="setAmount.add('#pamount')" href="javascript:void(0)">+</A></div>--%>
<div> 出发日期: <select name="PlanDateSelect1" id="PlanDateSelect1"></select></div></DD>
</DL>
<div class=btns>
<A id=InitCartUrl class=btn-0yuan onclick="BuyUrl(<%=id %>)" href="javascript:void(0)">在线购买</A>
<div id="fqPanel" class="fl"></div>
<INPUT id="btn_fav" class=btn-coll onclick="mark(<%=id %>)" value="收 藏" type=button>
<INPUT id="btn_print" class=btn-coll onclick="RoutePrint(<%=id %>)" value="打 印" type=button>
<%--<A class="btn-link" href="/RoutePrint/<%=id %>.html" target="_blank">打 印</A>--%>
<SPAN class=zjrx>热线电话：<br>
&nbsp;&nbsp;4006-777-666</SPAN>
<span class=clr></span></div></div>
<DIV id="inputs" style="DISPLAY:none">
<input id="TB_LineId" type="hidden" value="<%=id %>"/>
</DIV>
</div><!--right-extra end-->

<div class="right-extra">
<div id=manager class="m select"><div class=mt><H1></H1><STRONG>特色推荐</STRONG></div><div class="tj"><%=RouteFeature %></div></div>
<div id=PlanDate class="m detail">
<UL class=tab><LI class=curr>出发日期<SPAN></SPAN></LI></UL>
<div class="mc tabcon borders">
    <div id="DateList" class="mc tabcon <%=hideit1 %>"><div id="showdate" class="plan_date"><%=NoPlanList %></div></div>
    <div id="NewList" class="mc tabcon <%=hideit2 %>"><div id="Div2" class="plan_date">    
    <div class="tj">
        <div id="CruisesDateList"> <%=PlanDateList %>
<%--            <div id="Date_37333"class="CruisesDate1" tag="37333" date="2012-05-01"><span>2012-05-01</span>￥222.00<br>尚有余位</div>
            <div id="Date_37338"class="CruisesDate1" tag="37338" date="2012-05-02">2012-05-02<br>￥222.00<br>尚有余位</div>--%>
        </div>    
    </div>    
    </div></div>
    <div id="Div1" style="padding: 10px; font-size: 14px; color: #800000">【行程打印】为红颜色时，该行程与参考行程有所不同，请点击查看</div>
</div><!--tabcon end 实际出发行程可能与参考行程有所不同，请点击报名时注意查看提示信息-->
</div><!--detail end-->
<span class=clr></span>
</div><!--right-extra end-->

<div class="right-extra">
<div id="detail" class="m detail">
<UL class=tab>
<LI class=curr data="d-all">行程安排<span></span></LI>
<LI data="d-fuwu">服务标准<span></span></LI>
<LI data="d-feiyong">费用描述<span></span></LI>
<LI data="d-zhuyi">温馨提醒<span></span></LI>
<LI data="d-visa" class="<%=visafilehide %>">签证文档<span></span></LI>
<LI data="d-xuzhi">预订须知<span></span></LI>
</UL><!--知识库标签-->
<div id="d-all" class="mc fore tabcon">
<div id="detaillist" class="m"><div class="mc"><%=RouteInfos %></div></div><!--行程内容 end-->
<div id="d-fuwu-ct"><%=RouteServiceInfos%></div><!--服务标准 end-->
<div id="d-feiyong-ct"><%=RoutePriceInfos%></div><!--报价包含不包含和自费项目 end-->
<div id="d-zhuyi-ct"><%=RouteAttentionsInfos%></div><!--温馨提醒及购物商店 end-->
<div id="d-visa-ct" class="<%=visafilehide %>"><%=VisaFileInfo %></div><!--tabcon end-->
<div id="d-xuzhi-ct"><%=RegularOrderProcess%><%=RegularContractInfos %><%=RegularPayInfos %></div><!--预订须知等 end-->
</div><!--tabcon end-->
<div id="d-fuwu" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-feiyong" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-zhuyi" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-visa" class="mc tabcon hide"></div><!--tabcon end-->
<div id="d-xuzhi" class="mc tabcon hide"></div><!--tabcon end-->
</div><!--detail end-->
</div>
<div class="right-extra"></div><!--right-extra end-->
</div><!--w main end-->
<span class=clr></span>
<div id="tip"><div class="t_box"><div><s><i></i></s><span id=olog>本起价是可选出发日期中，按双人出行共住一间房核算的最低单人价格。产品价格会根据您所选择的出发日期、出行人数、入住酒店房型、航班或交通以及所选附加服务的不同而有所差别。</span></div></div></div>
<SCRIPT type="text/javascript">
    $(function () {
        $('#Text2').calendar({ minDate: '%y-%M-%d', btnBar: false });
//        $.jdThickBox({
//            type: "text",
//            source: "line.aspx",
//            width: 300,
//            height: 100,
//            title: "提示",
//            _countdown: 6
//        })
    });

    function SerchIt() {
        if ($("#Text1").val() == "" && $("#Text2").val() == "") {
            alert("请输入目的地、线路名称（或编号）、出发日期");
            $("#SerchNow").attr("href", "javascript:void(0);");
            return false;
        }
        $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
    }

    var showrouteflag = "0";
    function showroute() {
        showrouteflag = "1";
     }

     function GoToOrder() {
         if (showrouteflag == "0") {
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
        showrouteflag = "0";
    }

    function OrderNow() {
        if ($(".thickbox").length != 0) {
            jdThickBoxclose()
        }
        var planid = $("#PlanDateSelect1").val();
        if (planid == "" || planid ==null) {
            alert("请选择您的出发日期！");
            return false;
        }
        var begindate = $("#PlanDateSelect1 option:selected").text().substr(0, 10);
        var url = "/Purchase/OrderNow.aspx?lineid=" + $("#TB_LineId").val() + "&planid=" + planid + "&begindate=" + begindate + "&nums=" + $("#crnum").val() + "&etnums=" + $("#etnum").val() + "&r=" + Math.random();
        $.jdThickBox({
            type: "iframe",
            title: "在线预订",
            source: url,
            width: 580,
            height: 420,
            _title: "thicktitler",
            _close: "thickcloser",
            _con: "thickconr"
        })
    }

    $(function () {
        $('#priceexplain').mouseover(function () {
            $('#tip').show();
        }).mouseout(function () {
            $('#tip').hide();
        }).mousemove(function (e) {
            $('#tip').css({ "top": (e.pageY - 30) + "px", "left": (e.pageX + 30) + "px" })
        })
    });

    function RoutePrint(Cid) {
        window.open("/RoutePrint/" + Cid + ".html");
    }

    $(".CruisesDate1").click(function () {
        $(this).addClass("CruisesSelect").siblings().removeClass("CruisesSelect");
        $("#PlanDateSelect1").val($(this).attr("tag"));
        GoToOrder();
    });
</SCRIPT>
<script type="text/javascript" src="/Scripts/base.lib.js"></script>
<%=PlanScripts %>
<%--<script type="text/javascript" src="/Js/PlanDate/<%=id %>.js"></script>--%>
<script type="text/javascript" src="/Scripts/base.product.js"></script>
<script type="text/javascript" src="/Scripts/PlanDateList.js"></script>

<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


