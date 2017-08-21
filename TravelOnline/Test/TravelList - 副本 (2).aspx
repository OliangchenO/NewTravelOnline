<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravelList.aspx.cs" Inherits="TravelOnline.Travel.TravelList" %>
<%@ Register src="/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="description" content="上海青旅商城，为您提供在线旅游服务" />
    <meta name="Keywords" content="网上旅游,青旅商城" />
    <link href="/Styles/MySite.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script> <%if (false) { %> <script type="text/javascript" src="scripts/jquery-1.6-vsdoc.js"></script> <%} %>
    <script type="text/javascript" src="/Scripts/base.js"></script>
    <script type="text/javascript" src="/Scripts/base.Pages.js"></script>
    <script type="text/javascript">
        (function () {
            var _sn = ["Styles/slide_OutBound"];
            var _su = "/";
            var _sw = screen.width;
            var _se, _st;
            for (i in _sn) {
                _se = document.createElement("link");
                _se.type = "text/css";
                _se.rel = "stylesheet";
                if (_sw >= 1280) {
                    _st = _su + _sn[i] + ".w.css";
                } else {
                    _st = _su + _sn[i] + ".css";
                }
                _se.href = _st;
                document.getElementsByTagName("head")[0].appendChild(_se);
            }
        })()
    </script>
</head>
<body id="pop">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Scripts/hotwords.js"></script>
    <div class="w main">
    <div class=left>
        
        <div id=right-report class="m right-report r-report">
            <div class=mt>
                <H2>出境旅游线路搜索</H2>
                <div class=extra></div>
            </div>
            <div class=mc>
                <UL>
                    <LI>·<A href="news.aspx?id=3838" target=_blank>出境旅游线路搜索</A></LI>
                </UL>
            </div>
        </div><!--left No.1-->

        <div class="m rank" clstag="thirdtype|keycount|thirdtype|mrank">
	        <div class="mt">
		        <h2>一周热卖排行榜</h2>
	        </div>
	        <div class="mc">
		        <ul class="tabcon">
                    <li><span>1</span><div class="p-name"><a href='product/358178.html'>日本东京箱根伊豆京都大阪六日游</a></div></li>
                    <li><span>2</span><div class="p-name"><a href='product/358183.html'>CLUB MED民丹岛+新加坡4晚6日</a></div></li>
                    <li><span>3</span><div class="p-name"><a href='product/354413.html'>韩国全罗南道首尔5日经典游</a></div></li>
                    <li><span>4</span><div class="p-name"><a href='product/352631.html'>菲常假期★长滩岛4日包机直飞</a></div></li>
                    <li><span>5</span><div class="p-name"><a href='product/213684.html'>日本东京箱根伊豆京都大阪六日游</a></div></li>
                    <li><span>6</span><div class="p-name"><a href='product/379883.html'>菲常假期★长滩岛4日包机直飞</a></div></li>
                    <li><span>7</span><div class="p-name"><a href='product/297206.html'>韩国全罗南道首尔5日经典游</a></div></li>
                    <li><span>8</span><div class="p-name"><a href='product/275545.html'>春节大阪北海道东京迎春六日</a></div></li>
                    <li><span>9</span><div class="p-name"><a href='product/297206.html'>CLUB MED民丹岛+新加坡4晚6日</a></div></li>
                </ul>
	        </div>
        </div><!--rank end-->

        <div class="m rank" clstag="thirdtype|keycount|thirdtype|mrank">
	        <div class="mt">
		        <h2>最新推出线路</h2>
	        </div>
	        <div class="mc">
		        <ul class="tabcon">
                    <li><span>1</span><div class="p-name"><a href='product/358178.html'>日本东京箱根伊豆京都大阪六日游</a></div></li>
                    <li><span>2</span><div class="p-name"><a href='product/358183.html'>CLUB MED民丹岛+新加坡4晚6日</a></div></li>
                    <li><span>3</span><div class="p-name"><a href='product/354413.html'>韩国全罗南道首尔5日经典游</a></div></li>
                    <li><span>4</span><div class="p-name"><a href='product/352631.html'>菲常假期★长滩岛4日包机直飞</a></div></li>
                    <li><span>5</span><div class="p-name"><a href='product/213684.html'>日本东京箱根伊豆京都大阪六日游</a></div></li>
                    <li><span>6</span><div class="p-name"><a href='product/379883.html'>菲常假期★长滩岛4日包机直飞</a></div></li>
                    <li><span>7</span><div class="p-name"><a href='product/297206.html'>韩国全罗南道首尔5日经典游</a></div></li>
                    <li><span>8</span><div class="p-name"><a href='product/275545.html'>春节大阪北海道东京迎春六日</a></div></li>
                    <li><span>9</span><div class="p-name"><a href='product/297206.html'>CLUB MED民丹岛+新加坡4晚6日</a></div></li>
                </ul>
	        </div>
        </div><!--rank end-->
                
        <%--<div class="m" id="comment" clstag="homepage|keycount|homepage|comment">
			<div class="mt">
				<h2>最热评价</h2>
			</div>
			<div class="mc"><ul><li class='fore'><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/351107_4cb14760-ecf2-4eb2-8718-fd2bf0fc1d22_1.html' target='_blank'>到手了，晒~</a></h5><div class='content'><a href='127.0.0.1/product/351107.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/4871/d3144894-49f8-4ab7-a2eb-9f82a1309457.jpg'></a><a href='http://club.360buy.com/bbsDetail/351107_4cb14760-ecf2-4eb2-8718-fd2bf0fc1d22_1.html' target='_blank'>一直在等戴妃降价，却入手了这部。。。座充很有特色，像滑盖手机；背板比想象中光滑，可以当镜子；2G卡已经在机身里面了。需要参考的就请看图片吧。（限制只能上十张图？）</a></div><span class='ammount'>共28条回复</span><span class='user'><a href='http://club.360buy.com/userreview/11803265-1-1.html' target='_blank'>Hewsun</a></span></li><li><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/125851_9c7b8a28-7560-42d1-83f3-0a0be015842f_1.html' target='_blank'>外观苗条，非常好看！节约空间！风力够大！</a></h5><div class='content'><a href='127.0.0.1/product/125851.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/724\59820026-6085-4029-88fc-4ba5d4501cdf.jpg'></a><a href='http://club.360buy.com/bbsDetail/125851_9c7b8a28-7560-42d1-83f3-0a0be015842f_1.html' target='_blank'>很不错 1、安装简单方便，女孩，老人都能应付。 2、送风均匀、柔和，感觉比老式风扇舒服多了 3、比老式风扇小巧，2个手指就能拎走。 4、占地方少，不碍事。</a></div><span class='ammount'>共8条回复</span><span class='user'><a href='http://club.360buy.com/userreview/518454-1-1.html' target='_blank'>weixin晒单专用</a></span></li><li><h5>[晒单]<a href='http://club.360buy.com/bbsDetail/365001_ad957c37-daf8-4607-aa59-cf35dd2b7dbd_1.html' target='_blank'>晒晒昨天刚到手的加大蒙古包蚊帐</a></h5><div class='content'><a href='127.0.0.1/product/365001.html' target='_blank'><img width='50' height='50' src='http://img11.360buyimg.com/n5/1471/f5f5fdfb-81b3-4fb5-bfa4-9152e40f006d.jpg'></a><a href='http://club.360buy.com/bbsDetail/365001_ad957c37-daf8-4607-aa59-cf35dd2b7dbd_1.html' target='_blank'>前天晚上11点下的订单，昨天下午3点就到了。快递神速&nbsp;，快递员服务也不错。蚊帐呢，刚打开包装看到那么多根管子，不知道该从哪里下手&nbsp;。后来看安装说...</a></div><span class='ammount'>共4条回复</span><span class='user'><a href='http://club.360buy.com/userreview/3364926-1-1.html' target='_blank'>greatdave</a></span></li></ul></div>
		</div><!--comment end-->--%>
        <div class="m da211x90" id="Index_Left1"><script type="text/javascript" src="/Scripts/AD/Index_Left1.js"></script></div>
        <div class="m da211x90" id="Index_Left2"><script type="text/javascript" src="/Scripts/AD/Index_Left2.js"></script></div>
    </div><!--left end-->  
      
    <div class="right-extra">
 <div class="crumb">
         <a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<a href = "/OutBound.html">出境旅游</a>&nbsp;&gt;&nbsp;<a href="#">日韩</a>
        </div>
<!--crumb end-->

          
       <DIV id=i-right class=m>
<DIV id=hotsale clstag="thirdtype|keycount|thirdtype|hotsaleM">
<DIV class=mt>
<H2>日韩旅游专家推荐</H2></DIV>
<DIV class="mc list-h">
<dl><dt><a target="_blank" href='#'><img src="/images/i2.jpg" width="120" /></a></dt><dd><div class="p-name"><a target="_blank" href='#'>罗技MK250  无线套线套线套装149元送20元京券！<font color="#ff6600" >现在购买即可获取20元京券，相当于129元！</font></a></div><div class="p-price" >￥2267.00</div><div><a class=btns target="_blank" href="#">立即预订</a></div></dd></dl>
<dl><dt><a target="_blank" href='#'><img src="/images/i1.jpg" width="120" /></a></dt><dd><div class="p-name"><a target="_blank" href='#'>罗技MK250  无线套装149元送20元京券！<font color="#ff6600" >现在购买即可获取20元京券，相当于129元！</font></a></div><div class="p-price" >￥2267.00</div><div><a class=btns target="_blank" href="#">立即预订</a></div></dd></dl>
<dl><dt><a target="_blank" href='#'><img src="/images/i2.jpg" width="120" /></a></dt><dd><div class="p-name"><a target="_blank" href='#'>罗技MK250  无线套装149元送20元京券！<font color="#ff6600" >现在购买即可获取20元京券，相当于129元！</font></a></div><div class="p-price" >￥2267.00</div><div><a class=btns target="_blank" href="#">立即预订</a></div></dd></dl>
</DIV></DIV>
</DIV><!--i-right end-->



<DIV id=select class=m clstag="thirdtype|keycount|thirdtype|select">
<DIV class=mt>
<H1></H1><STRONG>旅游产品筛选</STRONG>
<DIV class=extra><A 
href="#">重置筛选条件</A></DIV></DIV>

<DL class=fore>
  <DT>目的地：</DT>
  <DD>
  <DIV><A id=0 class=curr 
  href="#">全部</A></DIV>
  <DIV><A id=61330 
  href="#>东京</A></DIV>
  <DIV><A id=20940 
  href="#>大阪</A></DIV>
  <DIV><A id=20941 
  href="#">釜山</A></DIV>
  <DIV><A id=56857 
  href="#">首尔</A></DIV>
  <DIV><A id=20942 
  href="#">箱根</A></DIV>
  <DIV><A id=28632 
  href="#">北海道</A></DIV>
  <DIV><A id=60632 
  href="#">济州岛</A></DIV>
  <DIV><A id=56460 
  href="#">京都</A></DIV></DD></DL>

<DL>
  <DT>价格：</DT>
  <DD>
  <DIV><A id=A2 class=curr 
  href="#">全部</A></DIV>
  <DIV><A id=20963 
  href="#">1-499</A></DIV>
  <DIV><A id=20965 
  href="#">500-999</A></DIV>
  <DIV><A id=20966 
  href="#">1000-1999</A></DIV>
  <DIV><A id=20967 
  href="#">2000-3999</A></DIV>
  <DIV><A id=20968 
  href="#">4000-5999</A></DIV>
    <DIV><A id=209681 
  href="#">6000-7999</A></DIV>
    <DIV><A id=209682 
  href="#">8000-9999</A></DIV>
  <DIV><A id=20969 
  href="#">10000元以上</A></DIV></DD></DL>
  <DL>
  <DT>天数：</DT>
  <DD>
  <DIV><A id=A1 class=curr 
  href="#">全部</A></DIV>
  <DIV><A id=A11 
  href="#">一天</A></DIV>
  <DIV><A id=20959 
  href="#">两天</A></DIV>
  <DIV><A id=20960 
  href="#">三天</A></DIV>
  <DIV><A id=20961 
  href="#">四天</A></DIV>
  <DIV><A id=20962 
  href="#">五天</A></DIV>
  <DIV><A id=A5 
  href="#">六天</A></DIV>
  <DIV><A id=A6 
  href="#">七天</A></DIV>
  <DIV><A id=A7 
  href="#">八天</A></DIV>
  <DIV><A id=A8 
  href="#">九天</A></DIV>
  <DIV><A id=A9 
  href="#">十天</A></DIV>
  <DIV><A id=A10 
  href="#">十天以上</A></DIV></DD></DL>
<DL>
  <DT>主题：</DT>
  <DD>
  <DIV><A id=A3 class=curr 
  href="#">全部</A></DIV>
  <DIV><A id=20974 
  href="#">浪漫婚庆</A></DIV>
  <DIV><A id=20971 
  href="#">清新田园</A></DIV>
  <DIV><A id=20975 
  href="#">走天下</A></DIV>
  <DIV><A id=20970 
  href="#">看天下</A></DIV>
  <DIV><A id=20972 
  href="#">玫瑰婚典</A></DIV>
  <DIV><A id=20973 
  href="#">山水之约</A></DIV></DD></DL>
 </DIV><!--select end -->
 </DIV><!--right-extra end-->


     <div class="right-extra">
<DIV id=filter clstag="thirdtype|keycount|thirdtype|filter">
<DIV class="fore item">排序&nbsp;</DIV>
<UL class="item tab">
  <LI class=curr><A 
  href="/products/11652-81130-11863-0-0-0-0-0-0-0-1-1-1.html">销量</A><SPAN></SPAN></LI>
  <LI class='price curr down' ><B></B><A 
  href="/products/11652-81130-11863-0-0-0-0-0-0-0-1-3-1.html">价格</A><SPAN></SPAN></LI>
  <LI class='price curr up'><B></B><A 
  href="/products/11652-81130-11863-0-0-0-0-0-0-0-1-4-1.html">旅游天数</A><SPAN></SPAN></LI>
  <LI><A 
  href="/products/11652-81130-11863-0-0-0-0-0-0-0-1-5-1.html">更新时间</A><SPAN></SPAN></LI></UL>
<DIV class="pagin pagin-m fr"><SPAN class=text>共100条线路</SPAN><SPAN class=text>1/0</SPAN><SPAN 
class=prev-disabled>上一页<B></B></SPAN><SPAN 
class=next-disabled>下一页<B></B></SPAN></DIV><SPAN class=clr></SPAN>

<DIV class=extra>
<SPAN 
class=clr></SPAN></DIV>
</DIV>



<DIV id=plist class=m clstag="thirdtype|keycount|thirdtype|plist">

<%--<UL class=list-h><BR><FONT style="FONT-SIZE: 14px">&nbsp;抱歉，没有找到符合条件的商品！<A 
  class=link_1 
  href="#i-right">查看全部商品</A></FONT><BR><BR></UL>--%>

<DIV class="mc">
<%--<dl>现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取现在购买即可获取</dl>
--%>
<dl>
<dt><a target="_blank" href='#'>
<IMG src="/images/i1.jpg" /></a></dt>

<dd>
<div class="p-name"><a target="_blank" href='#'>★菲常假期★长滩岛4日包机直飞</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥5655.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=coll355725 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,355725,'★菲常假期★长滩岛4日包机直飞')" value=对比 type=button></DIV>
<%--<div class="pd"><SPAN>出发：</SPAN>08/09,08/20...</div>--%>
<%--<div class="pbtn"><a class=btns target="_blank" href="#">立即预订</a></div>--%>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>“欢度春节~新马4晚5日·全程四-五星”新航直飞新航直飞</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥8765.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button2 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,355726,'“欢度春节~新马4晚5日·全程四-五星”新航直飞新航直飞')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>韩国首尔济州五日欢乐游(春节)(热线)！</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥2999.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button6 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557251,'韩国首尔济州五日欢乐游(春节)(热线)')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>日本东京箱根伊豆京都大阪六日游(两晚温泉)！</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥5688.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button7 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557252,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>日本大阪京都长野箱根东京七日欢乐游</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥4298.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button8 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557253,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>春节日本精品6日自然生态温泉滑雪深度游</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥6250.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button9 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557254,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>春节大阪北海道东京迎春六日</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥8900.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button10 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557255,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>CLUB MED民丹岛+新加坡4晚6日</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥12880.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button11 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557256,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>“轻松游泰国”-曼谷芭堤雅5晚6日游纯玩团</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥3500.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button1 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557257,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>“欢度春节~新马4晚5日·全程四-五星”新航直飞新航直飞</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥62267.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button3 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557258,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>CLUB MED民丹岛+新加坡4晚6日</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥5688.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button4 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,3557259,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>

<dl>
<dt><a target="_blank" href='#'><IMG src="/images/i1.jpg" /></a></dt>
<dd>
<div class="p-name"><a target="_blank" href='#'>日本大阪京都长野箱根东京七日欢乐游</a></div>
<div class="ps">新航直飞、全程无购物</div>
<div class="ps"><SPAN>行程：</SPAN>8天&nbsp;&nbsp;&nbsp;&nbsp;<SPAN>出发班期：</SPAN>8/9、8/20...</div>
</dd>
<dt>
<div class="p-price" >￥899.00</div>
<div class="pd"><font color="#ff6600" >编号：</font>34535</div>
<DIV class=btns_s><INPUT id=Button5 class=btn-coll onclick=feed_publish_collect(2,355725); value=收藏 type=button><INPUT class=btn-comp onclick="addToCompare(this,35572510,'COZZY蔻姿40支全棉环保斜纹儿童单人三件套Car festival')" value=对比 type=button></DIV>
</dt>
</dl>


<%--<dl><dt><a target="_blank" href='#'><IMG onerror="this.src='http://www.360buy.com/images/none/none_150.gif' src2="/images/i1.jpg" /></a></dt><dd><div class="p-name"><a target="_blank" href='#'>罗技MK250  无线套装149元送20元京券！<font color="#ff6600" >现在购买即可获取20元京券，相当于129元！</font></a></div><div class="p-price" >￥2267.00</div><div><a class=btns target="_blank" href="#">立即预订</a></div></dd></dl>
<dl><dt><a target="_blank" href='#'><IMG alt=小绵羊双人豪华提花四件套幸福之约 
  onerror="this.src='http://www.360buy.com/images/none/none_150.gif'" width=160 
  height=160 
  src2="http://img10.360buyimg.com/n2/1216/16701da4-f70d-49b0-a3ab-ed894c722a02.jpg"></a></dt><dd><div class="p-name"><a target="_blank" href='#'>罗技MK250  无线套装149元送20元京券！<font color="#ff6600" >现在购买即可获取20元京券，相当于129元！</font></a></div><div class="p-price" >￥2267.00</div><div><a class=btns target="_blank" href="#">立即预订</a></div></dd></dl>
--%>
</DIV>



  
  </DIV><!--plist end-->
<DIV class="m clearfix">
<DIV class="pagin fr"></DIV><!--pagin end--></DIV>

</DIV><!--right-extra end-->
<SCRIPT type=text/javascript>
    function lazyload(option) { var settings = { defObj: null, defHeight: 0 }; settings = $.extend(settings, option || {}); var defHeight = settings.defHeight, defObj = (typeof settings.defObj == "object") ? settings.defObj.find("img") : $(settings.defObj).find("img"); var pageTop = function () { var d = document, y = (navigator.userAgent.toLowerCase().match(/iPad/i) == "ipad") ? window.pageYOffset : Math.max(d.documentElement.scrollTop, d.body.scrollTop); return d.documentElement.clientHeight + y - settings.defHeight }; var imgLoad = function () { defObj.each(function () { if ($(this).offset().top <= pageTop()) { var src2 = $(this).attr("src2"); if (src2) { $(this).attr("src", src2).removeAttr("src2") } } }) }; imgLoad(); $(window).bind("scroll", function () { imgLoad() }) } lazyload({ defObj: "#plist" })
	    </SCRIPT>
</div><!--right-extra end-->

    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <SCRIPT type=text/javascript>
        initScrollY = 0;
        proIDs = new Array();
        compare();
    </SCRIPT>
</body>
</html>
