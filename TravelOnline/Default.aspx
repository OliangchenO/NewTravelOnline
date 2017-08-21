<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TravelOnline.Default" %>
<%@ Register src="Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <link href="Styles/MySite.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/EasyUI/themes/icon.css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript" src="/Scripts/lhgcalendar/lhgcalendar.min.js"></script>
    <script type="text/javascript" src="/Scripts/EasyUI/easyloader.js"></script>
    <script type="text/javascript">
        (function () { var _sn = ["Styles/slide"]; var _su = "/"; var _sw = screen.width; var _se, _st; for (i in _sn) { _se = document.createElement("link"); _se.type = "text/css"; _se.rel = "stylesheet"; if (_sw >= 1280) { _st = _su + _sn[i] + ".w.css" } else { _st = _su + _sn[i] + ".css" } _se.href = _st; document.getElementsByTagName("head")[0].appendChild(_se) } })()
    </script>
    <style>
        #links .links A {
	    MARGIN: 0px 5px
        }
        #links .links SPAN {
	        MARGIN: 0px 5px
        }
        #links .links SPAN A {
	        MARGIN: 0px
        }
        #links .copyright {
	        MARGIN: 10px 0px; FONT-FAMILY: arial
        }
        #links .ilinks A {
	        MARGIN: 0px 5px
        }
        .forget .mt {
	        BACKGROUND: url(/images/tit_regist.jpg) #d1d1d1 repeat-x 0px -34px; HEIGHT: 33px
        }
        .forget .mt H2 {
	        LINE-HEIGHT: 33px; PADDING-LEFT: 15px; BACKGROUND: url(/images/tit_regist.jpg) no-repeat 0px 0px; FLOAT: left; HEIGHT: 33px
        }
        .forget .mt SPAN {
	        TEXT-ALIGN: right; LINE-HEIGHT: 33px; FLOAT: right; HEIGHT: 33px
        }
        .forget .mt B {
	        WIDTH: 10px; BACKGROUND: url(/images/tit_regist.jpg) no-repeat 0px -68px; FLOAT: right; HEIGHT: 33px
        }
        .forget .mc {
	        MARGIN-BOTTOM: 15px;BORDER-BOTTOM: #E6E6E6 1px solid; BORDER-LEFT: #E6E6E6 1px solid; PADDING-BOTTOM: 10px; PADDING-LEFT: 10px; PADDING-RIGHT: 10px; BORDER-TOP: #E6E6E6 0px solid; BORDER-RIGHT: #E6E6E6 1px solid; PADDING-TOP: 10px
        }
    </style>
</head>
<body id="index">
    <form id="form1" runat="server">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Js/Hot/hotwords.js"></script>
    <div class="w main">
    <div class=left>        
        <div id=right-report class="m right-report r-report">
            <div class=mt><H2>快速搜索</H2><div class=extra></div></div>
            <div class=mc>
                <UL>
                    <LI>目的地、线路名称或编号</LI>
                    <LI><input id="Text1" type="text" style="width: 190px" /></LI>
                    <LI>出发日期</LI>
                    <LI style="PADDING-BOTTOM: 2px;"><input class="runcode" id="Text2" type="text" style="width: 100px" />&nbsp;&nbsp;&nbsp;&nbsp;<a id="SerchNow" href="127.0.0.1" class="easyui-linkbutton" plain="true" iconCls="icon-search" onclick="SerchIt()">搜 索</a></LI>
                </UL>
            </div>
        </div><!--left No.1-->
        
        <div id=brand class=m><div class=mt><H2>特色主题旅游</H2><div class=extra></div></div>
            <div class=mc><UL><%=IndexTopicTravel%></UL></div>
        </div><!--left No.2-->        
        <div class="m da211x90" id="IL1"><script type="text/javascript" src="/Js/AD/IL1.js"></script></div>
        <div class="m da211x90" id="IL2"><script type="text/javascript" src="/Js/AD/IL2.js"></script></div>        
        <div class="m rank"><div class="mt"><h2>一周热卖排行榜</h2></div><div class="mc"><ul class="tabcon"><%=LineOnHotSale %></ul></div></div><!--rank end-->
        <div class="m da211x90" id="IL3"><script type="text/javascript" src="/Js/AD/IL3.js"></script></div>
        <div class="m da211x90" id="IL4"><script type="text/javascript" src="/Js/AD/IL4.js"></script></div>
        <div class="m da211x90" id="IL5"><script type="text/javascript" src="/Js/AD/IL5.js"></script></div>
        <div class="m da211x90" id="IL6"><script type="text/javascript" src="/Js/AD/IL6.js"></script></div>
    </div><!--left end-->
    <div class="right-extra">
        <div class=middle>
            <div id=slide></div>
            <script type="text/javascript" src="/Js/AD/Index.js"></script><!--slide end-->
            
            <div id=madding class=m>
            <div class=mt>
            <H2>每周特惠推荐</H2>
            <div class=extra></div></div>
            <div id=madding-1>
            <div style="MARGIN: 73px auto" class=iloading>正在加载中，请稍候...</div></div>
            <script type="text/javascript" src="/Js/Preferences/Index.js"></script>
            <script type="text/javascript" src="/Scripts/Preferences.js"></script>
            </div> <!--madding end-->
           
        </div> <!--middle end-->
       
        <div class=right>
            <div id=report class=m><div class=mt><H2>青旅快讯</H2><div class=extra><A href="/News.html">更多&gt;&gt;</A></div></div>
                <div class=mc><UL><%=IndexAffiche%></UL></div>
            </div><!--report end-->
            
            <div id=Div4 class="m right-report">
                <div class=mt>
                    <H2>旅游工具箱</H2>
                    <div class=extra></div>
                </div>
                <div class=mc>
                    <DIV id=pay_service>
                        <DIV class=switch-scroll-panel>
                            <UL class="switch-nav switch-content cls">
                                <LI><A class=icon-donate href="http://www.weather.com.cn/weather/101020100.shtml" target=_blank>天气预报</A> </LI>
                                <LI><A class=icon-coin href="http://www.boc.cn/sourcedb/whpj" target=_blank>汇率</A> </LI>
                                <LI><A class=icon-phone href="http://weibo.com/scyts" target=_blank>青旅微博</A> </LI>
                            </UL>
                        </DIV>
                    </DIV>
                </div>
            </div><!--report end-->

            <div id=Div2 class="m right-report">
                <div class=mt>
                    <H2>订阅免费特价信息</H2>
                    <div class=extra></div>
                </div>
                <div class=mc>
                <IMG src="images/PSD_cd0096_00628.jpg" width=200 height=83>
                </div>
            </div><!--report end-->
            
        </div><!--right end-->
    </div><!--right-extra end-->

    <div class="right-extra">
        <div class=middle>

           <div id=madding class=m><div class=mt><H2>精彩自由行</H2><div class=extra1><A href="/FreeTour.html">更多&gt;&gt;</A></div></div>
           <DIV id=madding-2 class="mc list-h">
           <%=IndexFreeTour %>
<%--<UL class="madding-2">
  <LI><A href="127.0.0.1/product/232428.html" target=_blank><IMG src="images/i2.jpg" width=80><DIV>CLUB MED民丹岛+新加坡4晚6日</DIV><DIV><FONT color=#409115>度假首选体验一流度假村的魅力</FONT></DIV></A><div class=pl-price>￥1239.00</div></LI>
  <LI><A href="127.0.0.1/product/341996.html" target=_blank><IMG src="images/i1.jpg" width=80>
  <DIV>菲常假期★长滩岛4日包机直飞</DIV><DIV><FONT color=#409115>全程四-五星、新航直飞</FONT></DIV></A><div class=pl-price>￥1239.00</div>
  </LI>
</UL>
  <SPAN class=clr></SPAN>
<UL class="mc list-h madding-3">
  <LI>·<A href="#" target=_blank><FONT color=#ff6600>日本大阪京都长野箱根东京七日欢乐游</FONT></A></LI>
</UL>--%>
  </DIV></div> <!--madding end-->

        </div> <!--middle end-->

       
        <div class=right>
            <div class="m right-report">
                <div class=mt>
                    <H2>自由行目的地推荐</H2>
                    <div class=extra></div>
                </div>
                <div class=mc style="height: 240px">
                <DIV class=freearea><div class=free1><a href="/FreeTour/1059-42.html" target=_blank><IMG src="images/dsn.gif" width=60 height="50"></a></div><div class=free2><a href="/FreeTour/1059-42.html" target=_blank><FONT color=#409115>香港</FONT></a><br>香港是自由之都，购物天堂和美食天堂。</DIV></DIV>
                <DIV class=freearea><div class=free1><a href="/FreeTour/1062-0.html" target=_blank><IMG src="images/hainan.jpg" width=60 height="50"></a></div><div class=free2><a href="/FreeTour/1062-0.html" target=_blank><FONT color=#409115>海南</FONT></a><br>南海上一颗璀璨明珠仅次于台湾的全国第二大岛。</DIV></DIV>
                <DIV class=freearea><div class=free1><a href="/FreeTour/1061-544.html" target=_blank><IMG src="images/xjp.jpg" width=60 height="50"></a></div><div class=free2><a href="/FreeTour/1061-544.html" target=_blank><FONT color=#409115>新加坡</FONT></a><br>汇聚现代与传统风格，有“花园城市”之美称。</DIV></DIV>
                <DIV class=freearea><div class=free1><a href="/FreeTour/1061-543.html" target=_blank><IMG src="images/mlxy.jpg" width=60 height="50"></a></div><div class=free2><a href="/FreeTour/1061-543.html" target=_blank><FONT color=#409115>马来西亚</FONT></a><br>天堂不只出现在神话中，也未必离我们很遥远。</DIV></DIV>
                
                </div>
            </div><!--report end-->
        </div><!--right end-->
    </div><!--right-extra end-->

    <div class="right-extra">
        <div class=middle>
        <div id=madding class=m>
        <div class=mt>
        <H2>出境旅游</H2>
        <div class=extra1><A href="/OutBound.html">更多出境&gt;&gt;</A></div></div>
        </div><!--newproduct end-->

        <div class="m da0x120" id="IC1"><script type="text/javascript" src="/Js/AD/IC1.js"></script></div>
        
        <div id=Show_tabs>
            <DIV id="OutBound" class=m><%=IndexOutBoundHtml%></DIV>
        </DIV>

        <div id=madding class=m>
        <div class=mt>
        <H2>国内旅游</H2>
        <div class=extra1><A href="/InLand.html">更多国内&gt;&gt;</A></div></div>
        </div><!--newproduct end-->
        
        <div class="m da0x120" id="IC2"><script type="text/javascript" src="/Js/AD/IC2.js"></script></div>
        <div id=Show_tabs><DIV id="InLand" class=m><%=IndexInLandHtml%></DIV></DIV>   
        </div><!--middle end-->

        <div class=right>
            <div class="m da211x90" id="IR1"><script type="text/javascript" src="/Js/AD/IR1.js"></script></div>
            <div class="m da211x90" id="IR2"><script type="text/javascript" src="/Js/AD/IR2.js"></script></div>
            <div class="m da211x90" id="IR3"><script type="text/javascript" src="/Js/AD/IR3.js"></script></div>
            <div class="m da211x90" id="IR4"><script type="text/javascript" src="/Js/AD/IR4.js"></script></div>
            <div class="m da211x90" id="IR5"><script type="text/javascript" src="/Js/AD/IR5.js"></script></div>
            <div class="m da211x90" id="IR6"><script type="text/javascript" src="/Js/AD/IR6.js"></script></div>
        </div><!--right end-->
  
    </div><!--right-extra end-->
    
    <div class="right-extra">
        <%--<DIV class=middle>
            <div class="m da0x120" id="IC3"><script type="text/javascript" src="/Js/AD/IC3.js"></script></div>
        </DIV><!--middle end-->--%>

        <DIV class=right>

        </DIV><!--right end-->
    </div><!--right-extra end-->


<div class="right-extra">
<DIV class=middle><DIV id=newpros class=m><DIV class=mt><H2>邮轮旅游</H2><DIV class=extra><A href="/Cruises.html">更多&gt;&gt;</A></DIV></DIV>
<DIV id=newpros1 class="mc list-h">
<UL><%=IndexCruises %></UL></DIV></DIV></DIV><!--middle end-->

<DIV class=middle><DIV id=newpros class=m><DIV class=mt><H2>签证办理</H2><DIV class=extra><A href="/Visa.html">更多&gt;&gt;</A></DIV></DIV>
<DIV id=newpros2 class="mc list-h">
<UL>
<%=IndexVisa %>
<%--  <LI>
  <DIV class=p-img><A href="127.0.0.1/product/401785.html" target=_blank><IMG src="/images/677.png" width=100 height=100></A></DIV>
  <DIV class=p-name><A href="127.0.0.1/product/401785.html" target=_blank>美国商务签证<FONT color=#ff6600>上海领区、需面试</FONT></A></DIV>
  <DIV class=p-price>青旅价：<STRONG>￥1500.00</STRONG></DIV></LI>

  <LI>
  <DIV class=p-img><A href="127.0.0.1/product/401781.html" 
  target=_blank><IMG  
  src="/images/677.png" width=100 
  height=100></A></DIV>
  <DIV class=p-name><A  
  href="127.0.0.1/product/401781.html" target=_blank>美国探亲访友签证 
  <FONT color=#ff6600>需面试</FONT></A></DIV>
  <DIV class=p-price>青旅价：<STRONG>￥1000.00</STRONG></DIV></LI>
  --%>
  </UL></DIV></DIV></DIV><!--middle end-->

        <DIV class=right>
            
        </DIV><!--right end-->
    
    </div><!--right-extra end-->
    <%--<div class="right-extra"></div><!--right-extra end-->
    
        <SPAN class=clr></SPAN>
    </div>--%>
    </div><!--w main end-->
    <SPAN class=clr></SPAN>

    <DIV class=w>
        <DIV class=forget>
            <DIV class=mt><H2>友情链接</H2><B></B></DIV>
            <DIV id=links class=mc>
                <DIV id=links class="links"><%=FriendLink%></DIV>
            </DIV>
        </DIV>
    </DIV>
    <uc3:Footer ID="Footer1" runat="server" />
    </form>
    <script type="text/javascript" src="/Scripts/base.lib.js"></script>
    <script type="text/javascript" src="/Scripts/default.js"></script>
    <script type="text/javascript">
        //$("#newpros1").eq(0).picMarquee({ width: (screen.width >= 1200) ? 766 : 546, height: 164, delay: 20, auto: true });
        $("#newpros2").eq(0).picMarquee({ width: (screen.width >= 1200) ? 766 : 546, height: 164, delay: 10, auto: true });
        $(function(){
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