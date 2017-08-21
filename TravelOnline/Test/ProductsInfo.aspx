<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductsInfo.aspx.cs" Inherits="TravelOnline.Travel.ProductsInfo" %>
<%@ Register src="~/Master/Header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="~/Master/SortList.ascx" tagname="SortList" tagprefix="uc2" %>
<%@ Register src="~/Master/Footer.ascx" tagname="Footer" tagprefix="uc3" %>
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
    <script type="text/javascript" src="/Scripts/date.js"></script>
    <script type="text/javascript"src="/Scripts/jquery.datePicker.min-2.1.2.js"></script>
</head>
<body id="<%=BodyId %>">
    <uc1:Header ID="Header1" runat="server" />
    <uc2:SortList ID="SortList1" runat="server" />
    <script type="text/javascript" src="/Scripts/hotwords.js"></script>
    <div class="w main">
    <div class=left>
        <div id=right-report class="m right-report r-report">
            <div class=mt>
                <H2>旅游线路搜索</H2>
                <div class=extra></div>
            </div>
            <div class=mc>
                <UL>
                    <LI>·<A href="news.aspx?id=3838" target=_blank>旅游线路搜索</A></LI>
                </UL>
            </div>
        </div><!--left No.1-->

        <div class="m rank" clstag="thirdtype|keycount|thirdtype|mrank">
	        <div class="mt">
		        <h2>同类线路热卖排行</h2>
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
		        <h2>最近浏览的线路</h2>
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
        <a href="/index.html">首页</a>&nbsp;&gt;&nbsp;<a href = "/OutBound.html">出境旅游</a>&nbsp;&gt;&nbsp;<a href="#">日韩</a>&nbsp;&gt;&nbsp;日本大阪京都长野箱根东京七日欢乐游
    </div>
    <!--crumb end-->

    <DIV id=tops class="m select"  clstag="thirdtype|keycount|thirdtype|select">
<DIV class=mt>

</DIV>
 </DIV>


<DIV id=preview clstag="shangpin|keycount|product|preview">
<DIV id=spec-n1 class=piczoom ><IMG 
alt="HTC S710e原装座充 适用HTC S710e/S710d/C510e/S510e/G11/G12手机" 
onerror="this.src='http://www.360buy.com/images/none/none_347.gif'" 
src="/images/a/9eb3b88203282.jpg" width=320 
height=240 
jqimg="/images/a/9eb3b88203282.jpg"></DIV>
<DIV id=spec-n5 clstag="shangpin|keycount|product|spec-n5">
<DIV id=spec-left class=control></DIV>
<DIV id=spec-right class=control></DIV>
<DIV id=spec-list>
<UL class=list-h>
  <LI><IMG 
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/s_01.jpg" width=62 
  height=50></LI>
  <LI><IMG  
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/s_02.bmp" width=62 
  height=50></LI>
    <LI><IMG name=2444/9e8211c9-2cc8-4b0d-8cf1-ff941f251f19.jpg 
  alt="HTC S710e原装座充 适用HTC S710e/S710d/C510e/S510e/G11/G12手机" 
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/s_03.jpg" width=62 
  height=50></LI>
    <LI><IMG name=2444/9e8211c9-2cc8-4b0d-8cf1-ff941f251f19.jpg 
  alt="HTC S710e原装座充 适用HTC S710e/S710d/C510e/S510e/G11/G12手机" 
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/9e8211c9-2cc8-4b0d-8cf1-ff941f251f19.jpg" width=62 
  height=50></LI>
    <LI><IMG  
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/s_02.bmp" width=62 
  height=50></LI>
    <LI><IMG  
  onerror="this.src='http://www.360buy.com/images/none/none_50.gif'" 
  src="/images/a/s_02.bmp" width=62 
  height=50></LI>
  </UL></DIV></DIV>
</DIV>


<UL id=summary>
<DIV id=name>
<H1>日本大阪京都长野箱根东京七日欢乐游<FONT 
style="COLOR: #ff0000" id=advertiseWord>特价</FONT></H1></DIV><!--pname end-->
  <LI><SPAN>线路编号：3212</SPAN></LI>
  <LI>
  <DIV class=fl>预&nbsp;订&nbsp;价：<STRONG class=price>￥12880.00</STRONG></DIV><SPAN 
  id=promotion1></SPAN><!--金牌以上会员价--><A 
  href="#">(起价说明)</A> 
  <DIV class=clr></DIV></LI>
  <SCRIPT type=text/javascript>      $("#otherprice").hoverForIE6();</SCRIPT>

  
  <LI class=clearfix clstag="shangpin|keycount|product|pingfen"><SPAN 
  class=fl>商品评分：</SPAN> 
  <DIV id=star418674 class=fl>
  <DIV class="star sa5"></DIV></DIV></LI>
  <LI id=cx class=hide></LI><!--促销-->
  <LI id=tz class=hide></LI>

  <LI class=partake><SPAN>推荐分享：</SPAN> 
  <DIV><A id=site-sina title=推荐到新浪微博 
  onclick="tj('http://v.t.sina.com.cn/share/share.php?appkey=2445336821','sina')" 
  href="javascript:void(0)"></A> <A id=site-qzone title=推荐到腾讯微博 
  onclick="tj('http://v.t.qq.com/share/share.php?source=1000002&amp;site=http://www.360buy.com','qzone')" 
  href="javascript:void(0)"></A> <A id=site-renren title=推荐到人人 
  onclick="tj('http://share.renren.com/share/buttonshare/post/1004?','renren')" 
  href="javascript:void(0)"></A> <A id=site-kaixing title=推荐到开心网 
  onclick="tj('http://www.kaixin001.com/repaste/share.php?','kaixing')" 
  href="javascript:void(0)"></A> <A id=site-douban title=推荐到豆瓣 
  onclick="tj('http://www.douban.com/recommend/?','douban')" 
  href="javascript:void(0)"></A> <A id=site-msn title=推荐到MSN 
  onclick="tj('http://profile.live.com/badge/?','MSN')" 
  href="javascript:void(0)"></A> <A id=site-qq title=通过QQ发送链接给好友 
  href="javascript:void(0)"></A> <A id=site-email title=邮件 
  href="#"></A> </DIV></LI>
</UL><!--infos end-->

<SCRIPT type=text/javascript>
    var setAmount = {
        min: 1,
        max: 99,
        reg: function (x) {
            return new RegExp("^[1-9]\\d*$").test(x);
        },
        amount: function (obj, mode) {
            var x = $(obj).val();
            if (this.reg(x)) {
                if (mode) {
                    x++;
                } else {
                    x--;
                }
            } else {
                alert("请输入正确的数量！");
                $(obj).val(1);
                $(obj).focus();
            }
            return x;
        },
        reduce: function (obj) {
            var x = this.amount(obj, false);
            if (x >= this.min) {
                $(obj).val(x);
            } else {
                alert("商品数量最少为" + this.min);
                $(obj).val(1);
                $(obj).focus();
            }
        },
        add: function (obj) {
            var x = this.amount(obj, true);
            if (x <= this.max) {
                $(obj).val(x);
            } else {
                alert("商品数量最多为" + this.max);
                $(obj).val(99);
                $(obj).focus();
            }
        },
        modify: function (obj) {
            var x = $(obj).val();
            if (x < this.min || x > this.max || !this.reg(x)) {
                alert("请输入正确的数量！");
                $(obj).val(1);
                $(obj).focus();
            }
        }
    }

    function BuyUrl(wid) {
        var pcounts = $("#pamount").val();
        var patrn = /^[0-9]{1,2}$/;
        if (!patrn.exec(pcounts)) {
            pcounts = 1;
        }
        else {
            if (pcounts <= 0)
                pcounts = 1;
            if (pcounts >= 100)
                pcounts = 99;
        }
        $("#InitCartUrl").attr("href", "http://127.0.0.1/purchase/InitCart.aspx?pid=" + wid + "&pcount=" + pcounts + "&ptype=1");
    }
				</SCRIPT>
<DIV id=choose class=m clstag="shangpin|keycount|product|choose">
<DL class=amount>
  <DT>　预订人数：</DT>
  <DD><A class=reduce onclick="setAmount.reduce('#pamount')" 
  href="javascript:void(0)">-</A><INPUT id=pamount 
  onkeyup="setAmount.modify('#pamount')" value=1 type=text><A class=add 
  onclick="setAmount.add('#pamount')" href="javascript:void(0)">+</A></DD></DL>
<DIV class=btns>
<A id=InitCartUrl class=btn-0yuan 
onclick=BuyUrl(418674);
href="127.0.0.1" 
clstag="shangpin|keycount|product|InitCartUrl">在线购买</A>
<DIV id=fqPanel class="fl " onclick=mark(418674,3);></DIV><INPUT id=coll418674 class=btn-coll onclick=mark(418674,4); value="收 藏" type=button clstag="shangpin|keycount|product|btn-coll">
<INPUT id=Button2 class=btn-coll onclick=mark(418674,4); value="打 印" type=button clstag="shangpin|keycount|product|btn-coll">
<SPAN class=clr></SPAN></DIV></DIV>
    </div><!--right-extra end-->


    <div class="right-extra">
    
 <DIV id=manager class="m select">
<DIV class=mt>
<H1></H1><STRONG>特色推荐</STRONG>
<DIV class=extra><A 
href="#"></A></DIV></DIV>
<DIV class=tj>
日本经典特色行程！<br>
行程安排丰富，一次性畅游东京/富士山/京都/大阪，游遍本州各大城市﹗ 特别安排入住特色温泉酒店，享受泡温泉乐﹗<br>
特別贈送双温泉+2晚5星酒店!!<br>
体验关西文化魅力，感受精致行程﹗<br>
本社承诺采用专业导游，不设强逼自费及购物，轻松自在渡过难忘假期﹗<br>
</DIV>
</DIV>
<script type="text/javascript" language="javascript">
//    var json = [{ "planid": "71026", "date": "2011-06-21", "price": 4500, "content": "4500\r\n4100" }, { "planid": "71026", "date": "2011-07-21", "price": 4500, "content": "4500\r\n4100" }, { "planid": "71027", "date": "2011-08-06", "price": 4500, "content": "4500\r\n4100" }, { "planid": "71027", "date": "2011-09-06", "price": 4500, "content": "4500\r\n4100" }, { "planid": "71027", "date": "2011-11-06", "price": 4500, "content": "4500\r\n4100"}];
//    var entityId = 3286;
//    var entityTitle = '本州常规双温泉6日游（阪东）';
//    var defaultStartDate = '2011-04-21';
//    var defaultEndDate = '2011-5-21';
//    //var startDate = '2011-07-21'
    </script>

<DIV id=PlanDate class="m detail" clstag="shangpin|keycount|product|detail">
<UL class=tab>
  <LI class=curr>出发日期<SPAN></SPAN></LI>
  </UL>
<DIV class="mc tabcon borders">

<DIV id=DateList class="mc tabcon">
<div id="showdate" class="plan_date"></div>
<%--<div id="showdatebb" class="plan_date"><div class="calendarPanel"><div class="monthTitle"><table class="monthTable"><tbody><tr><td class="prevMonth"><a href="javascript:void(0);" title="上一个月"><img src="./本州常规双温泉6日游_files/mbi_003.gif"></a></td><td class="monthTitle">2011年07月</td></tr></tbody></table></div><div id="showCalendarPanel0" class="showCalendarPanel"><table cellspacing="2" class="jCalendar"><thead><tr><th scope="col" abbr="一" title="一" class="weekday">一</th><th scope="col" abbr="二" title="二" class="weekday">二</th><th scope="col" abbr="三" title="三" class="weekday">三</th><th scope="col" abbr="四" title="四" class="weekday">四</th><th scope="col" abbr="五" title="五" class="weekday">五</th><th scope="col" abbr="六" title="六" class="weekend">六</th><th scope="col" abbr="日" title="日" class="weekend">日</th></tr></thead><tbody><tr><td class="other-month weekday ">27</td><td class="other-month weekday ">28</td><td class="other-month weekday ">29</td><td class="other-month weekday ">30</td><td class="current-month weekday ">1</td><td class="current-month weekend ">2</td><td class="current-month weekend ">3</td></tr><tr><td class="current-month weekday ">4</td><td class="current-month weekday ">5</td><td class="current-month weekday ">6</td><td class="current-month weekday ">7</td><td class="current-month weekday ">8</td><td class="current-month weekend ">9</td><td class="current-month weekend ">10</td></tr><tr><td class="current-month weekday ">11</td><td class="current-month weekday ">12</td><td class="current-month weekday ">13</td><td class="current-month weekday ">14</td><td class="current-month weekday ">15</td><td class="current-month weekend ">16</td><td class="current-month weekend ">17</td></tr><tr><td class="current-month weekday ">18</td><td class="current-month weekday ">19</td><td class="current-month weekday ">20</td><td class="current-month weekday  hasEvent"><a href="javascript:void(0);" title="71026">21</a><br><span class="planPrice">4500元</span></td><td class="current-month weekday ">22</td><td class="current-month weekend ">23</td><td class="current-month weekend ">24</td></tr><tr><td class="current-month weekday ">25</td><td class="current-month weekday ">26</td><td class="current-month weekday ">27</td><td class="current-month weekday ">28</td><td class="current-month weekday ">29</td><td class="current-month weekend ">30</td><td class="current-month weekend ">31</td></tr></tbody></table></div></div><div class="calendarPanel"><div class="monthTitle"><table class="monthTable"><tbody><tr><td class="monthTitle">2011年08月</td><td class="nextMonth"><a href="javascript:void(0);" title="下一个月"><img src="./本州常规双温泉6日游_files/mbi_005.gif"></a></td></tr></tbody></table></div><div id="showCalendarPanel1" class="showCalendarPanel"><table cellspacing="2" class="jCalendar"><thead><tr><th scope="col" abbr="一" title="一" class="weekday">一</th><th scope="col" abbr="二" title="二" class="weekday">二</th><th scope="col" abbr="三" title="三" class="weekday">三</th><th scope="col" abbr="四" title="四" class="weekday">四</th><th scope="col" abbr="五" title="五" class="weekday">五</th><th scope="col" abbr="六" title="六" class="weekend">六</th><th scope="col" abbr="日" title="日" class="weekend">日</th></tr></thead><tbody><tr><td class="current-month weekday ">1</td><td class="current-month weekday ">2</td><td class="current-month weekday ">3</td><td class="current-month weekday ">4</td><td class="current-month weekday ">5</td><td class="current-month weekend  hasEvent"><a href="javascript:void(0);" title="71027">6</a><br><span class="planPrice">4500元</span></td><td class="current-month weekend ">7</td></tr><tr><td class="current-month weekday ">8</td><td class="current-month weekday ">9</td><td class="current-month weekday ">10</td><td class="current-month weekday ">11</td><td class="current-month weekday ">12</td><td class="current-month weekend ">13</td><td class="current-month weekend ">14</td></tr><tr><td class="current-month weekday ">15</td><td class="current-month weekday ">16</td><td class="current-month weekday ">17</td><td class="current-month weekday ">18</td><td class="current-month weekday ">19</td><td class="current-month weekend ">20</td><td class="current-month weekend ">21</td></tr><tr><td class="current-month weekday ">22</td><td class="current-month weekday ">23</td><td class="current-month weekday ">24</td><td class="current-month weekday ">25</td><td class="current-month weekday ">26</td><td class="current-month weekend ">27</td><td class="current-month weekend ">28</td></tr><tr><td class="current-month weekday ">29</td><td class="current-month weekday ">30</td><td class="current-month weekday ">31</td><td class="other-month weekday ">1</td><td class="other-month weekday ">2</td><td class="other-month weekend ">3</td><td class="other-month weekend ">4</td></tr></tbody></table></div></div></div>
--%>
</DIV>

</DIV><!--tabcon end-->
</DIV><!--detail end-->
<SPAN class=clr></SPAN>
    
                        
    </div><!--right-extra end-->

    <div class="right-extra">
      
<DIV id=detail class="m detail" clstag="shangpin|keycount|product|detail">
<UL class=tab>
  <LI class=curr data="d-all">行程安排<SPAN></SPAN></LI>
  <LI data="d-fuwu">服务标准<SPAN></SPAN></LI>
  <LI data="d-feiyong">费用描述<SPAN></SPAN></LI>
  <LI data="d-zhuyi">温馨提醒<SPAN></SPAN></LI>
  <LI data="d-xuzhi">预订须知<SPAN></SPAN></LI>
  <!--知识库标签--></UL>
<DIV id="d-all" class="mc fore tabcon">

<DIV id=detaillist class=m>

<DIV class="mc">

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 1 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 2 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 3 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 4 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 5 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 6 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>

<dl>
<dt><IMG src="/images/i1.jpg" /></dt>
<dd>
<div class="p-name"><SPAN>第 7 天</SPAN>上海-东京</div>
<div class="stander">
<UL>
  <LI>交通：<SPAN>国际飞行时间约11小时30分左右</SPAN></LI>
   <LI>用餐：<SPAN>早中（中华料理或日式定食）晚（中华料理或日式定食）</SPAN></LI>
    <LI>住宿：<SPAN>SUNROUTE HTL 或同级</SPAN></LI>
  </UL>
</div>
<div class=ps>早餐后前往日本自然文化遗产之地白川乡；参观世界文化遗产【“合掌造”式的民宅】（停留约30分钟）；随后游览富有传统气息的日本街道－【三之町古街】（停留约50分钟），继而前往高山,参观有名的「高山朝市」、而后德川幕府时期建造的旧式官府【高山阵屋跡】前摄影留念，追忆当年番主生活的情景。当晚入住温泉酒店；享受在雪中入浴的特殊感受，品尝正宗日式怀石料理。</div>
</dd>
</dl>
</DIV>
</DIV><!--detaillist end-->

<DIV id="d-fuwu-ct" >
 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>服务标准</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>
旅游交通：舒适旅游双飞、空调旅游大巴<br>
导游服务：优秀导游服务及领队<br>
保险服务：旅游责任险<br>
</DIV>
</DIV>
</DIV><!--服务标准 end-->

<DIV id="d-feiyong-ct" >
 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>价格包含</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>★往返国际及内陆机票★全程燃油税★机场税★旅游签证★全程中文领队及当地中文导游服务<br>
★行程上所列景点第一门票★境外绿牌车旅游大巴★行程中所列团队用餐(午:日元1000X3次及晚:日元1,000X2次及行程所列特色风味餐)<br>
★当地3-4星级酒店（相当于国内3星）★双人间住宿（如旺季期间所安排酒店标间爆满，会自动提升全单间）
<br>
</DIV>
</DIV>

 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>价格不含</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>
★全程小费人民币300/人 （大小同价）★行程中所列自费项目★出入境行李海关课税<br>
★旅客旅游意外险(请建议客人自行购买)、★超重行李的托运费及保管费★酒店内收费电视、电话、饮品、烟酒等个人消费税，境外个人消费费用自理。<br>
★护照工本费200元★由于目前国际燃油税不断上涨, 若航空公司临时调整燃油税, 本社有权按实际情况补收税金差价, 敬请配合!
<br>
保险服务：旅游责任险<br>
</DIV>
</DIV>

 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>自费项目</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>
【东京迪斯尼乐园–日元9,000/成人、12岁以下小童 日元7,000】<br>
【台场海滨公园(远眺彩虹桥及富士电视台)+台场小火车一站+午餐–日元5,000/人】<br>
【东京湾游船+六本木新城（眺望东京铁塔）+午餐–日元5,000/人】 <br> 
【银座购物乐+表参道HILLS自由购物+烧烤自助餐–日元8,500/人】<br>
【横滨观光 : 中华街+山下公园+21世纪未来港+三井奥特莱斯购物城 横滨港湾+海萤大桥展望塔-日元7,000/人】 <br> 
【富士山五合目+ 花之都公园 (6月17日至7月10日期间改往河口湖八木崎熏衣草祭)–日元5,500/人】<br>
【芦之湖游船 +大涌谷+ 箱根缆车(大涌谷至桃源台) – 日元5,000/人】  <br>
【清水寺+新京极商店街–日元4,000/人】<br>
【金阁寺+祗园艺伎街+茶道体验–日元4,500/人】  
</DIV>
</DIV>
</DIV><!--服务标准 end-->

<DIV id="d-zhuyi-ct" >
 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>注意事项</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>
1.以上仅限于团队正常运作情况下，如遇到罢工，天气，交通严重堵塞等特殊情况，可能参观时间 略有减少，如遇团队人数较多，可能购物时间略有增加，敬请谅解。<br>
2.膳食由我司预先安排的餐厅，如团友不共同进食，将不能退费。<br>
3.如客人因自身原因未能入境，地接费用已经产生，费用不能退还。<br>
4.行程安排以当地实际环境次序有变动.<br>
5.如果因游客自身行为、身体健康等原因，或者因为天气情况飞机停航、大巴滞留等不可抗力原因造成游客财产损失的，我旅行社不承担任何赔偿责任！在境外请听从导游和领队的安排，否则因此产生的一切后果,由自己负责，谢谢合作！<br>
6. 团队回国后护照由领队统一收取便以我社送往日本领事馆备案消签工作。<br>
特别声明: 日本团体观光赴日签证规定, 客人必须全程跟团活动, 不得擅自离团, 否则当地接待社将报警处理!<br>

</DIV>
</DIV>

 <DIV class="m select">
<DIV class=mt>
<H1></H1><STRONG>购物商店</STRONG>
<DIV class=extra><A 
href="#">返回顶部</A></DIV></DIV>
<DIV class=tj>
大亚或光伸免税店<br>
永山或TOKIS或LAOX电器店
<br>
秋叶原电器街—TOKIS电器店<br>
</DIV>
</DIV>
</DIV><!--服务标准 end-->


<DIV id="d-xuzhi-ct" >
<DIV class="m select"  clstag="thirdtype|keycount|thirdtype|select">
        <DIV class=mt>
        <H1></H1><STRONG>在线预订流程</STRONG>
        <DIV class=extra><A 
href="#">返回顶部</A></DIV>
        </DIV>
        <DIV class=content>
<IMG src="/images/order_step.jpg">
</DIV>
        
        </DIV>

         <DIV class="m select"  clstag="thirdtype|keycount|thirdtype|select">
        <DIV class=mt>
        <H1></H1><STRONG>签约方式</STRONG>
        <DIV class=extra><A 
href="#">返回顶部</A></DIV>
        </DIV>
        <DL class=fore>
  <DT>传真签约：</DT>
  <DD><DIV>双方在合同上签字盖章后，通过传真进行签约。</DIV>
  </DD></DL>
  <DL>
  <DT>快递签约：</DT>
  <DD><DIV>我们把盖章合同快递到您，您签字后快递回我们公司，完成签约。</DIV>
  </DD></DL>
  <DL>
  <DT>上门签约：</DT>
  <DD><DIV>预订时留下您的详细联系方式，我们会派出业务人员上门拜访，完成签约。</DIV>
  </DD></DL>
  <DL>
  <DT>门店签约：</DT>
  <DD><DIV><A  
href="#">上海中国青年旅行社门市网店</A></DIV>
  </DD></DL>
    <DL>
  <DT>合同范本：</DT>
  <DD><DIV><A  
href="#">上海市出境旅游合同范本</A></DIV>
<DIV><A  
href="#">上海中国青年旅行社出境押金协议</A><DIV>
  </DD></DL>
        </DIV>


        <DIV class="m select"  clstag="thirdtype|keycount|thirdtype|select">
        <DIV class=mt>
        <H1></H1><STRONG>付款方式</STRONG>
        <DIV class=extra><A 
href="#">返回顶部</A></DIV>
        </DIV>
        <DL class=fore>
  <DT>在线支付：</DT>
  <DD><DIV>支付宝、快钱等多种在线支付方式，供您选择。</DIV>
  </DD></DL>
  <DL>
  <DT>门店付款：</DT>
  <DD><DIV>您可以到距离你最近的门店，完成付款。</DIV>
  </DD></DL>
  <DL>
  <DT>上门收款：</DT>
  <DD><DIV>预订时留下您的详细联系方式，我们会派出业务人员上门拜访，完成收款。</DIV>
  </DD></DL>
      
        </DIV>

</DIV><!--服务标准 end-->

    </DIV><!--tabcon end-->
    <DIV id="d-fuwu" class="mc tabcon hide"></DIV><!--tabcon end-->
    <DIV id="d-feiyong" class="mc tabcon hide"></DIV><!--tabcon end-->
    <DIV id="d-zhuyi" class="mc tabcon hide"></DIV><!--tabcon end-->
    <DIV id="d-xuzhi" class="mc tabcon hide"></DIV><!--tabcon end-->
    </DIV><!--detail end-->

    </div><!--right-extra end-->

    <div class="right-extra">
        
 </div><!--right-extra end-->
               
    </div><!--w main end-->

    <SPAN class=clr></SPAN>
    <uc3:Footer ID="Footer1" runat="server" />
    <script type="text/javascript" src="/Scripts/base.lib.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //第一步取得需要写入cookie的内容 
            var pro_url = location.href; // document.URL; //页面地址 
            var pro_name = $("title").html(); // $(".Product_Name h1").text(); //产品名称 
            var canAdd = true; //默认可以插入 
            //        alert(pro_name);
            //        alert(pro_url);

            //第二步，取得cookie并取得已有历史记录产品数目,对于cookie的操作，这里用了jquery的一个插件，你不喜欢，可以用自己的 
            var hisProduct = readCookie("hisProduct");
            alert(hisProduct);
            var len = 0;
            if (hisProduct) {
                hisProduct = eval('(' + hisProduct + ')'); //静普通字符串转换成json 
                len = hisProduct.length;
            }

            //第三步，判断当前页面的产皮是否已经在记录中，用产品名称去比较 
            $(hisProduct).each(function (i) {
                if (this.proname == pro_name) {
                    canAdd = false; //已经存在 
                    return;
                }
            })

            //第四步，可以添加的话。 
            if (canAdd) {
                var temp = "[";
                var startNum = 0;
                if (len > 9) { startNum = 1; } //如果已经记录产品>2，前面说了，最多3个产品，所以这里其实是3，那么不需要第一个产品的记录 
                for (var m = startNum; m < len; m++) {
                    temp = temp + "{\"url\":\"" + hisProduct[m].url + "\",\"proname\":\"" + hisProduct[m].proname + "\"},";
                }
                temp = temp + "{\"url\":\"" + pro_url + "\",\"proname\":\"" + pro_name + "\"}]";
                createCookie("hisProduct", temp, 90);
            }

            //第五步，OK，输出吧 
            var newtemp = eval('(' + readCookie("hisProduct") + ')');
            var newtemp_ = "";
            i = 1;
            for (var n = newtemp.length - 1; n > -1; n--) {//这里你可以输出为自己要的格式
                //<li><span>2</span><div class="p-name"><a href='product/358183.html'>CLUB MED民丹岛+新加坡4晚6日</a></div></li>
                newtemp_ = newtemp_ + "<li><span>" + i + "</span><div class=p-name><a target='_blank' href='" + newtemp[n].url + "'>" + newtemp[n].proname + "<\/a></div></li>";
                i++;
                //alert(newtemp_);
            }
            $("#recent ul").html(newtemp_); //我输出到prohistory这个div里面 
        })

</script>
    <script type="text/javascript">
        //图片宽度
        //var PicRoomWidth = 430;
    </script>
    <script type="text/javascript" src="/Js/PlanDate/3121.js"></script>
    <script type="text/javascript" src="/Scripts/base.product.js"></script>
    <script type="text/javascript" src="/Scripts/bus-detail.js"></script>
    
    <script src="/Scripts/jquery.thickbox11.js" type="text/javascript"></script>
</body>
</html>

