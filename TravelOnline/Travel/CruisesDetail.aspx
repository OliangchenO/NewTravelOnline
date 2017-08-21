<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruisesDetail.aspx.cs" Inherits="TravelOnline.Travel.CruisesDetail" %>
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
  <LI><DIV class=fl>销售价格：<STRONG class=price>￥<%=LinePrice %></STRONG></DIV></LI>
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
</LI>
</UL><!--infos end-->

<div id=choose class=m>

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
<%--<input id="PlanId" type="hidden" value=""/>
<input id="BeginDate" type="hidden" value=""/>--%>
</DIV>
</div><!--right-extra end-->

<div class="right-extra">

<div class="m select"><div class=mt><H1></H1><STRONG>选择出发日期</STRONG></div><div class="tj">
    <div id=CruisesDateList>
<%--        <div class="CruisesDate" tag="01">2011-12-11</div><div class="CruisesDate" tag="02">2011-12-21</div>
        <div class="CruisesDate" tag="03">2011-12-21</div><div class="CruisesDate" tag="04">2011-12-21</div>
        <div class="CruisesDate" tag="05">2011-12-21</div><div class="CruisesDate" tag="06">2011-12-21</div>--%>
        <%=PlanString %>
    </div>    
</div></div>
<div id=CruisesRooms class="m select"><div class=mt><H1></H1><STRONG>选择舱位</STRONG></div><div class="tj">
    <div id=CruisesRoomList>
        <%--<div id="01">
            <div class="mc tabcon borders01">
                <ul id=SellPrice class=Cruises>
                    <li class=cur><div class=ttype>类型</div><div class=tname>名称</div><div class=tsname>配置</div><div class=tprice>价格</div><div class=tnum>预订</div><div class=tpic></div></li>
                    <li class=priceli tps=SellPrice tag=125750 id=P125750><div class=ftype>内仓</div><div class=fname>舱位Q二人间</div><div class=fsname>人数：2人<br>面积：20平米<br>楼层：甲板2，3，4，5层</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value="0">0</option></select>间 &nbsp;<select class=psel><option value="0">0</option></select>成人 &nbsp;<select class=psel><option value="0">0</option></select>儿童</div><div id=pic class=fnpic></div></li>
                    <li class=priceli tps=SellPrice tag=125750 id=Li2><div class=ftype>内仓</div><div class=fname>舱位Q二人间</div><div class=fsname>人数：2人<br>面积：20平米<br>楼层：甲板2，3，4，5层</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value="0">0</option><option value="1">1</option><option value="2" selected="selected">2</option></select></div><div id=Div2 class=fnpic></div></li>
                    <li class=priceli tps=SellPrice tag=125750 id=Li3><div class=ftype>内仓</div><div class=fname>舱位Q二人间</div><div class=fsname>人数：2人<br>面积：20平米<br>楼层：甲板2，3，4，5层</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value="0">0</option><option value="1">1</option><option value="2" selected="selected">2</option></select></div><div id=Div3 class=fnpic></div></li>
                </ul>
            </div>
        </div>
        <div id="02" class="hide">
        22
        </div>
        <div id="03" class="hide">03</div>
        <div id="04" class="hide">
            <div class="mc tabcon borders01"><ul id=Ul1 class=price><li class=cur><div class=ttype>费用类型</div><div class=tname>名称</div><div class=tprice>价格</div><div class=tnum>人数</div><div class=tsum>单项合计</div><div class=tpic></div></li><li class=priceli tps=SellPrice tag=125750 id=Li1><div class=ftype>成人价</div><div class=fname>周三</div><div class=fprice>&yen;<span class=sellprice>321</span></div><div class=fnum><select class=psel><option value="0">0</option><option value="1">1</option><option value="2" selected="selected">2</option></select></div><div class=fsum>&yen;<span class=sumprice>0</span></div><div id=Div1 class=fnpic></div></li></ul></div>
        
        </div>
        <div id="05" class="hide">05</div>
        <div id="06" class="hide">06</div>--%>
        <%=RoomString %>
    </div>
</div></div>
<div id=manager class="m select"><div class=mt><H1></H1><STRONG>特色推荐</STRONG></div><div class="tj"><%=RouteFeature %></div></div>
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
    $(function () {
        $('#Text2').calendar({ minDate: '%y-%M-%d', btnBar: false });
    });

    $(".CruisesDate").click(function () {
        $(this).addClass("CruisesSelect").siblings().removeClass("CruisesSelect");
        var roomid = "#" + $(this).attr("tag");
        $("#PlanId").val($(this).attr("tag"));
        $("#BeginDate").val($(this).html());
        $(roomid).removeClass("hide").siblings().addClass("hide");
        //alert($("#BeginDate").val()) 
    });

    $(function () {
        $('.ddlnums').change(function () {
            var parent = $(this).parents("li");
            var parentid = "#" + $(this).parents("li").attr("id");
            var AllBeds = Number($(parentid).attr("beds")) * Number($(parentid + " select:eq(0)").val());
            var adult = Number($(parentid + " select:eq(1)").val());
            var childs = Number($(parentid + " select:eq(2)").val());
            if ((adult + childs) == 0) {
                $(parentid + " div:last").attr({ "class": "fnpic" });
                alert($(parentid + " div:eq(1)").html() + "，入住人数不能为空！");
                $(this).val("0");
                return false;
            }
            if ((adult + childs) > AllBeds) {
                $(parentid + " div:last").attr({ "class": "fnpic" });
                alert("您选择了 " + $(parentid + " select:eq(0)").val() + "间 " + $(parentid + " div:eq(1)").html() + "，可入住总人数为：" + AllBeds + "人！");
                $(this).val("0");
                return false;
            }
            else {
                $(parentid + " div:last").attr({ "class": "fpic" });
                //alert($(this).val());
            }
        });
    });

    function SerchIt() {
        if ($("#Text1").val() == "" && $("#Text2").val() == "") {
            alert("请输入目的地、线路名称（或编号）、出发日期");
            $("#SerchNow").attr("href", "javascript:void(0);");
            return false;
        }
        $("#SerchNow").attr("href", "/Search.aspx?keyword=" + escape($("#Text1").val()) + "&pdate=" + $("#Text2").val());
    }

    function GoToOrder() {
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
        var planid = $("#PlanId").val();
        if (planid == "" || planid == null) {
            alert("请选择您的出发日期！");
            return false;
        }

        var Parms = "";
        var parentid = "#" + planid;
        var Nums = 0;
        $(parentid + " .priceli").each(function () {
            var pid = "#" + $(this).attr("id");
            Nums += Number($(pid + " select:eq(0)").val());

            var AllBeds = Number($(this).attr("beds")) * Number($(pid + " select:eq(0)").val());
            var adult = Number($(pid + " select:eq(1)").val());
            var childs = Number($(pid + " select:eq(2)").val());
            if ((adult + childs) != AllBeds) {
                alert("您选择了 " + $(pid + " select:eq(0)").val() + "间 " + $(pid + " div:eq(1)").html() + "，成人和儿童数之和与可入住人数 " + AllBeds + "人 不符！");
                Parms = "";
                return;
            }
            else {
                if (Number($(pid + " select:eq(0)").val()) != 0) {
                    //Parms += $(pid + " .psel").val();
                    Parms += $(this).attr("id") + "@@";
                    Parms += $(this).attr("roomid") + "@@";
                    Parms += $(pid + " .fname").html() + "@@";
                    Parms += $(this).attr("beds") + "@@";
                    Parms += Number($(pid + " select:eq(0)").val()) + "@@";
                    Parms += Number($(pid + " select:eq(1)").val()) + "@@";
                    Parms += Number($(pid + " select:eq(2)").val()) + "@@";
                    Parms += $(this).attr("price") + "@@";
                    Parms += "||";
                }
            }
        });
        
        if (Nums == 0) {
            alert("请选择您要预定的舱位！")
            return;
        }
        if (Nums > 9) {
            alert("您预定的舱位不能超过9间！")
            return;
        }
        if (Parms == "") {
            return;
        }
        //alert(Parms);
        //return;
        Parms = Parms.substr(0, Parms.length - 2);
        var begindate = $("#BeginDate").val();
        var url = "/Purchase/CuisesNow.aspx?lineid=" + $("#TB_LineId").val() + "&planid=" + planid + "&begindate=" + begindate + "&Parms=" + escape(Parms) + "&r=" + Math.random();
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

    $(document).ready(function () {
        onload_handler();
    });

    function onload_handler() {
        var pid = $("#PlanId").val();
        $("#Date_" + pid).addClass("CruisesSelect");
        $("#" + pid).removeClass("hide");

    }

    function ShowMoreCruises(PlanId) {
        var url = "/Travel/AjaxLineList.aspx?action=LoadCruisesList&PlanId=" + PlanId;
        //window.open(url);
        //$("#loading").center();
        //$("#" + PlanId).html("<div class=iloading>正在加载中，请稍候...</div>");
        $.getJSON(url, function (date) {
            $("#" + PlanId).html(date.content);
        })
    }
  
</SCRIPT>
<script type="text/javascript" src="/Scripts/base.lib.js"></script>
<%=PlanScripts %>
<%--<script type="text/javascript" src="/Js/PlanDate/<%=id %>.js"></script>--%>
<script type="text/javascript" src="/Scripts/base.product.js"></script>
<script type="text/javascript" src="/Scripts/PlanDateList11.js"></script>

<uc3:Footer ID="Footer1" runat="server" />
</body>
</html>


