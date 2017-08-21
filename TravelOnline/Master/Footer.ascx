<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="TravelOnline.Master.Footer" %>
<div id="BackToPageTop"><a href="#" title="回顶部">回顶部</a></div>

<div class="container" style="text-align:center;height:50px;line-height:30px;FONT-SIZE: 12px;border-top: 1px solid #F0F0F0;">
	<div class="row">
		<div class="span12">
            <div class="flinks">
                <a href="/service/aboutus.html" target="_blank">关于我们</a>|
                <a href="/service/contactus.html" target="_blank">联系我们</a>|
                <a href="/service/joinus.html" target="_blank">人才招聘</a>|
                <a href="/service/partner.html" target="_blank">同行分销</a>|
                <a href="/service/advertising.html" target="_blank">广告服务</a>
            </div>
		    <div>沪ICP备案编号：05016600  Copyright©2006-2016  上海中国青年旅行社 版权所有</div>
            <div class="ilinks">
            <script type="text/JavaScript">                function change(eleId) { var str = document.getElementById(eleId).href; var str1 = str.substring(0, (str.length - 6)); str1 += RndNum(6); document.getElementById(eleId).href = str1; } function RndNum(k) { var rnd = ""; for (var i = 0; i < k; i++) { rnd += Math.floor(Math.random() * 10); } return rnd; }</script>
             <a id="urlknet" tabindex="-1" href="https://ss.cnnic.cn/verifyseal.dll?sn=2011042500100008043&amp;ct=df&amp;pa=340789" target="_blank"><img oncontextmenu="return false;" onclick="change('urlknet')" border="true" name="seal" alt="可信网站" src="/Images/112_40_EAWZul.jpg" width="112" height="40"></a>
             </div>
        </div>
	</div>
</div>

<script type="text/javascript">
    function gotoTop(min_height) {
        min_height ? min_height = min_height : min_height = 200;
        //为窗口的scroll事件绑定处理函数
        $(window).scroll(function () {
            //获取窗口的滚动条的垂直位置
            var s = $(window).scrollTop();
            //当窗口的滚动条的垂直位置大于页面的最小高度时，让返回顶部元素渐现，否则渐隐
            if (s > min_height) {
                $("#BackToPageTop").fadeIn(100);
            } else {
                $("#BackToPageTop").fadeOut(200);
            };
        });
    };
    gotoTop(200);
</script>
