<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TravelOnline.AD._2017Young.index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="X-UA-Compatible" content="IE=8">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="假期，签证,出境旅游,上海青旅,旅游景点,旅游线路,新年特辑">
<meta name="description" content="2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛">
<title>2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛</title>
<link href="css/common.css" rel="stylesheet" type="text/css" />
<link href="css/headerTop.css" rel="stylesheet" type="text/css" />
<link href="css/index.css" rel="stylesheet" type="text/css" />
<link href="css/demo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
<script type="text/javascript" src="js/koala.min.1.5.js"></script>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        setCookie("redirectUrl", "/AD/2017Young/index.aspx");
   });

   //设置Cookie
   function setCookie(name, value) {
       var exp = new Date();
       exp.setTime(exp.getTime() + 1 * 3600000); //24小时
       document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
   }

   function getCookie(name) {
       var strcookie = document.cookie;
       var resultList = new Array();
       var arrcookie = strcookie.split("; ");
       for (var i = 0; i < arrcookie.length; i++) {
           var arr = arrcookie[i].split("=");
           if (arr[0] == name) resultList.unshift(arr[1]);
       }
       return resultList;
   }

   //删除Cookie
   function DelCookie(name) {
       setCookie(name, expired);
       alert(document.cookie);
   }
</script>
</head>

<body>
   <script type="text/javascript" src="js/backtotop.js"></script>
   <p id="back-to-top"><a href="#top"><span></span>回到顶部</a></p>
   <!--Content-->
   <!--Top-->
    <div class="wrap">
    <div id="content">
        <div class="content-box">
        <img src="images/悦华大酒店.jpg" style="width:1200px; height:400px;" alt="上海悦华大酒店"/>
        <div class="index-nav">
			<div class="nav-column">
				<ul id="changeNav" class="nav-main clearfix">
					<li class="current"><a href="#">竞赛规程</a></li>
                    <li><a href="/AD/2017YoungPlan">会务安排</a></li>
                    <%if (Session["Online_UserId"] == null){ %>
					<li><a href="/Member/loginYoung.aspx">报名登录</a></li>
                    <%}else{%>
                        <li><a href="/Login/Logout.aspx">退出登录</a></li>
                    <%} %>
				</ul>
			</div>
		</div>
        <div style="width:900px; margin:0 auto;">
        <p>
            上海奉贤，取“敬奉贤人”之意，相传孔子弟子言偃曾来境讲学而得名。地处上海市南部，南临杭州
        </p>
        <div>
            <p style="display:block; width:40%; float:left;">湾，西北枕黄浦江。气候温润，四季分明，拥有上海最好的空气质量，并拥有滩浒岛、万佛阁、青村桃园等自然人文景观。近年来，作为上海产业经济的重要承载区，奉贤区政府提出并实践了大力发展美丽健康产业的构想，把优越的自然禀赋与坚实的美丽健康产业基础有机整合，打造成“东方美谷”。</p>
            <img src="images/东方美谷.jpg" alt="东方美谷" style="width:55%; float:left; padding:20px;"/>
        </div>
        <p style="font-weight:bold;">
            冠名酒店：上海悦华大酒店
        </p>
        <div style="width:900px; margin:0 auto;height:360px;">
        <img src="images/酒店图片.jpg" alt="悦华大酒店" style="width:55%; float:left; margin:20px 20px 20px 0;"/>
        <p style="display:inline; width:35%; float:left; margin:20px 20px;">上海悦华大酒店简介：坐落于上海市奉贤区南桥镇江海路229号，地理位置优越，是一家具有传奇色彩和丰富文化底蕴的五星级酒店，旨在为每一位客人创造难忘的独特体验。</p>
        </div>
        </div>
        
        <p align="center"><strong><span style="width:1200px;display:inline-block;">2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛</span></strong></p>
            <p align="center"><span>竞赛规程</span></p>

			<p>一、组织机构</p>
			<p>主办单位：中国青少年宫协会</p>
			<p>承办单位：中国青少年宫协会体育体验专业委员会</p>
			<p class="margin-left120">上海市青少年活动中心</p>
			<p>支持单位：上海邱艳舞蹈工作室</p>
			<p class="margin-left120">上海中国青年旅行社有限公司</p>
			<p>二、比赛日期：2017年 8 月 19、20 日（18日报到，21日返程）</p>
			<p>三、比赛地点：上海市奉贤中学（上海市奉贤区南奉公路7058号）</p>
			<p>四、参赛单位：</p>
				<p>全国各青少年宫、青少年活动中心、青少年体育舞蹈培训机构。</p>
			<p>五、竞赛规则：</p>
			<p>1、选手应认真选择合适的赛项，可以参加一项以上赛事，在同一赛事中不许更换舞伴。报名选项应以年龄较大的舞伴为准确定所选组别。</p>
			<p>2、队列舞要求6人同跳所选套路，队列由6人分两排（每排3人）组成，音乐由大会统一安排，服装颜色不限但须统一，符合年龄要求。</p>
			<p>3、各组不足三队即向上并组，组委会有权根据报名情况对赛事作出临时调整。</p>
			<p>六、奖项设置：</p>
			<p>1、青年组、全能组，前三名颁发奖杯、奖牌、证书，4-8名颁发奖牌、证书。</p>
			<p>2、少年、少儿、幼儿各组冠军颁发奖杯。前3名分别颁发奖牌、证书，4-8名颁发证书。</p>
			<p>3、女子精英组冠军颁发奖杯，前3名颁发奖牌、证书；4-8名颁发证书。</p>
			<p>4、女子单人、双人各组按一、二、三等奖录取，分别颁发奖牌、证书。</p>
			<p>5、女子6人组合各组按一、二、三等奖录取，分别颁发奖牌、证书。</p>
			<p>6、集体项目1-3名颁发奖杯，4-8名颁发奖牌、证书。</p>
			<p>7、评选“体育道德风尚奖”。</p>
			<p>8、评选“优秀运动员”，“优秀教练员”，“优秀裁判员”，分别颁发证书。  </p>
			<p>9、获奖运动员可申请获得由中国青少年宫协会颁发的相应“全国青少年儿童业余运动员技术等级”证书和证章。</p>
			<p>七、参赛费用：</p>
			<p>1、所有双人组别（包括女子双人、单项组）报名费150元/人(300元/对)。</p>
			<p>2、单人组报名费为200元/人。</p>
			<p>3、6人组报名费为100元/人。</p>
			<p>4、团体舞12人（含12人）以下1500元/组，12人以上2000元/组。</p>
			<p>5、所有选手在原有报名级别的基础上，可继续兼报其它组别，所有兼报组别的报名费都按正常收取。</p>
			<p>6、各参赛队往返交通费、食宿费自理。食宿费用由食宿提供单位收取并开具发票。参赛报名费由上海市青少年活动中心收取并开具发票。</p>
			<p>7、请各参赛单位于7月15日前将报名费汇至：</p>
			<p>户名：上海市青少年活动中心</p>
			<p>帐户：31630803002787384</p>
			<p>开户行名称：上海银行闸北支行</p>
			<p>8、所有参赛运动员比赛期间由承办方代为投保人身意外险。</p>
			<p>八、报名方法：</p>
			<p>1、各组别单位统一组织报名参赛。如当地没有组队参赛，参赛选手可直接向大赛组委会报名，由组委会直接汇总到各自所在省市代表队；</p>
			<p>2、每队可报领队一人、教练一人、管理人员一人，运动员不限；</p>
			<p>3、比赛报名日期：2017年5月10日起至2017年7月15日止。报名截止后，报名费用一律不退，组别不得更改。</p>
			<p>4、报名表（附件1）请务必按要求逐项填写清楚，报名网址：www.shqzx.com （“2017全国青少年宫体蹈比赛”报名窗口）。报到时，请携带参赛队员户口簿复印件的年龄证明，否则无效。如虚报年龄，一经查出，取消其参赛资格或比赛成绩。</p>
			<p>九、联系方式</p>
			<p>1、中国青少年宫协会（北京市前门东大街10号）</p>
			<p>联系人：褚晓宇</p>                     
			<p>联系电话：010-67031358</p>                  
			<p>2、上海市青少年活动中心</p>
			<p>比赛报名联系人：刘 浏、覃  燕</p>
			<p>联系电话：021-63171432</p>
			<p>上海邱艳舞蹈工作室</p>
			<p>比赛报名联系人：王  蓉</p>
			<p>联系电话：021-66242163</p>
			<p>会务安排联系人：杜虹岑021-64747775、孙静021-64747761</p>

			<p>十、比赛纪律：</p>
			<p>1、领队和教练员负责本队的行政管理，做好全员思想工作，必须服从命令听指挥。参赛选手严格遵守赛场纪律、服从裁判、尊重观众，确保比赛顺利进行。</p>
			<p>2、参赛选手必须提前15分钟到场完成赛前检录工作，迟到或缺席作弃权处理。</p>
			<p>十一、日程安排（暂定）</p>
			<table width="1200px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="25%" class="title">时间</td>
					<td width="25%" class="title">上午</td>
					<td width="50%" class="title">下午</td>
				</tr>
				<tr>
					<td>8月18日（周五）</td>
					<td>各代表队报到</td>
					<td><p>1.各代表队报到</p>
						<p>2.19：00~20：00召开领队教练裁判联席及安全工作会议<p>
					</td>
				</tr>
				<tr>
					<td>8月19日（周六）</td>
					<td>初、复赛</td>
					<td>初、复赛
					</td>
				</tr>
				<tr>
					<td>8月20日（周日）</td>
					<td>复、决赛</td>
					<td><p>1.开幕式，邀请英国黑池职业拉丁新星冠军现场表演</p>
						<p>2.决赛<p>
					</td>
				</tr>
				<tr>
					<td>8月21日（周一）</td>
					<td colspan="2"><p>1、继续参加体育舞蹈夏令营活动及大师训练班（由英国黑池职业拉丁新星冠军等亲自授课，半日，限额100名）</p>
						<p>2、返程</p>
					</td>
				</tr>
			</table>
			<p id="pos" class="redline">十二、本规程的解释权属主办单位，未尽事宜另行通知。</p>
            <p class="redline">报名方式：下载附件中的"2017全国青少年宫体育舞蹈比赛报名表"，并填写完成（打*号的为必填项），在下方完成上传。</p>
            <p class="redline">注：组别、项目选择可参考附件中的"2017全国青少年宫体育舞蹈比赛组别、项目"</p>
            <p class="redline">会务安排报名方式：请点击首页的会务安排，进入页面后鼠标点击所需的预定项目，即可跳入预定界面完成预定。</p>
            
			<p>附件：</p>
			<p>1、<a class="downLoadFile" href="2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛报名表.xlsx">《2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛报名表》点击下载</a></p>
			<p>2、<a class="downLoadFile" href="2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛组别、项目.doc">《2017“东方美谷·悦华杯”全国青少年宫体育舞蹈比赛组别、项目》点击下载</a></p>
			 
            <form id="form1" runat="server">  
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="upload"/>  
                <asp:Button ID="Button1" runat="server" Text="上传" CssClass="uploadButton" OnClick="Button1_Click" />  
                <asp:Label ID="Label1" runat="server" Text="" CssClass="redColor"></asp:Label>
            </form>
            <%if(userType!=null&&userType=="upload"){ %>
                <a href="<%= uploadPath%>" style="border: 5px solid #dedede;">已上传报名表，点击查看</a>
            <%} %>
            <p class="fr">中国青少年宫协会</p>
			<p class="fr">上海市青少年活动中心</p>
			<p class="fr">2017年4月</p>
        </div>
		</div>
        </div>
   <!--Footer-->
   <!-- 在线咨询 begin -->
   <div class="main-im">
      <div id="open_im" class="open-im">&nbsp;</div>  
      <div class="im_main" id="im_main">
         <div id="close_im" class="close-im"><a href="javascript:void(0);" title="点击关闭">&nbsp;</a></div>
         <a href="//shang.qq.com/wpa/qunwpa?idkey=8431f33631ec4d6fc0154fb03ec18061af66acd462999a5f8f20b0ffe5d3fe0c" target="_blank" class="im-qq qq-a" title="在线QQ客服">
            <div class="qq-container"></div>
            <div class="qq-hover-c"><img class="img-qq" src="http://demo.lanrenzhijia.com/2015/service0119/images/qq.png"></div>
            <span> QQ在线咨询</span>
         </a>
         <div class="im-footer" style="position:relative">
            <div class="weixing-container">
               <div class="weixing-show">
                  <div class="weixing-txt">QQ扫一扫<br>加入群聊</div>
                  <img class="weixing-ma" src="images/qq群.jpg">
                  <div class="weixing-sanjiao"></div>
                  <div class="weixing-sanjiao-big"></div>
               </div>
            </div>
            <div class="go-top"><a href="javascript:;" title="返回顶部"></a> </div>
            <div style="clear:both"></div>
         </div>
      </div>
   </div>
   <script type="text/javascript">
      $(function(){
         $('#close_im').bind('click',function(){
            $('#main-im').css("height","0");
            $('#im_main').hide();
            $('#open_im').show();
         });
         $('#open_im').bind('click',function(e){
            $('#main-im').css("height","272");
            $('#im_main').show();
            $(this).hide();
         });
         $('.go-top').bind('click',function(){
            $(window).scrollTop(0);
         });
         $(".weixing-container").bind('mouseenter',function(){
            $('.weixing-show').show();
         })
         $(".weixing-container").bind('mouseleave',function(){        
            $('.weixing-show').hide();
         });
      });
   </script>
   <!-- 在线咨询 end-->
   <div class="foot">
      <div class="attention">
         <p>
            <strong>关注更多活动</strong>
         </p>
      </div>
      <div class="scanning clearfix">
         <div class="wrap-box clearfix">
            <a class="wx"></a>
            <div class="ts">
               <h6>上海青旅官方微信</h6>
               <p>
                  <span>扫描二维码</span><br/>
                  <span>了解最新旅游咨询</span>
               </p>
            </div>
         </div>
         <div class="wrap-box clearfix">
            <a class="wb" href="http://weibo.com/scyts"></a>
            <div class="ts">
               <h6>上海青旅官方微信</h6>
               <a href="http://weibo.com/scyts" class="me">关注我们</a>
            </div>
         </div>
      </div>
      <div class="bottomNav">
         <div class="link">
            <a href="http://www.scyts.com/service/aboutus.html" target="_blank" rel="nofollow">关于我们</a>
            <a href="http://www.scyts.com/service/contactus.html" target="_blank" rel="nofollow">联系我们</a>
            <a href="http://www.scyts.com/service/joinus.html" target="_blank" rel="nofollow">人才招聘</a>
            <a href="http://www.scyts.com/OutBound.html" target="_blank" rel="nofollow">出境旅游</a>
            <a href="http://www.scyts.com/InLand.html" target="_blank" rel="nofollow">国内旅游</a>
            <a href="http://www.scyts.com/FreeTour.html" target="_blank" rel="nofollow">自由行</a>
            <a href="http://www.scyts.com/Visa.html" target="_blank" rel="nofollow">单办签证</a>
         </div>
         <p>沪ICP备12040718号  Copyright©1997-2017  上海中国青年旅行社 版权所有</p>
      </div>
   </div>
   <script type="text/javascript">
      var _hmt = _hmt || [];
      (function() {
        var hm = document.createElement("script");
        hm.src = "//hm.baidu.com/hm.js?670e68bb0a5926537ba7c720575bc7eb";
        var s = document.getElementsByTagName("script")[0]; 
        s.parentNode.insertBefore(hm, s);
      })();
   </script>
   <!-- Baidu Button BEGIN --> 
   <script type="text/javascript" id="bdshare_js" data="type=slide&amp;img=1&amp;pos=left&amp;uid=1147359" ></script> 
   <script type="text/javascript" id="bdshell_js"></script> 
   <script type="text/javascript">
   var bds_config={"bdTop":150};
   document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + Math.ceil(new Date()/3600000);
   </script> 
   <!-- Baidu Button END --> 

   <script type="text/javascript">
   var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
   document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3Feca822017290a9945e6643915f0d8353' type='text/javascript'%3E%3C/script%3E"));
   </script>
</body>
</html>