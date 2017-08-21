<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndexHeader.ascx.cs" Inherits="TravelOnline.NewPage.IndexHeader" %>
<!--页头Begin-->
	<div class="header">
		<div class="header-top">
			<div class="column  clearfix">
				<div class="siderbar-r rl">
					<ul class="clearfix">
						<li class="register fl">
							<%=logininfo %>
						</li>
						<li rel="nofollow"  id="top_dropmenu" class="my-menu relative-box fl mr">我的青旅
							<ul>
								<li><a href="/users/userorder.aspx" target="_blank">我的订单</a></li>
								<li><a href="/users/journals.aspx" target="_blank">我的游记</a></li>
								<li><a href="/users/userinfo.aspx" target="_blank">个人信息</a></li>
								<li><a href="/users/changepassword.aspx" target="_blank">修改密码</a></li>
                                <li><a href="/users/NotMember.aspx" target="_blank">非会员订单</a></li>
                                <li><a href="/users/mycoupon.aspx" target="_blank">我的优惠券</a></li>
								<li><a href="/users/userIntegral.aspx" target="_blank">我的积分</a></li>
							</ul>
							<b></b>
						</li>
						<li rel="nofollow"  class="wx fl">
							<a id="top_showwx" class="weixin relative-box" href="javascript:;">微信
								<div class="top-wx absolute-box">
								<img src="image/wx.png" alt="青旅微信二维码"/>
								<p>扫描二维码</p>
								<p>了解最新旅游资讯</p>
								<b class="arr" style="background-position: -32px 0"></b>
							</div>
							</a>
						</li>
						<li rel="nofollow"  class="fl"><a class="weibo relative-box" href="http://weibo.com/scyts?sudaref=www.scyts.com" title="关注青旅" style="background-position: 0 -36px">微博</a></li>
                        <li rel="nofollow"  class="collect fl"><a href="../OfficeFiles/游客满意度调查表17年最终版.doc">满意度调查表</a></li>
					</ul>
				</div>
			</div>
		</div>
		<div class="header-lv clearfix">
			<a href="http://www.scyts.com" title="上海中国青年旅行社" class="logo fl">上海中国青年旅行社</a>
			<div class="header-ad sch fl clearfix">
				<div class="sch_l fl">
					<div class="sch_select fl" id="seach_listSelect">
						<span class="SelectLineType">所有产品</span>
						<i></i>
						<div class="sch_list" id="seach_selectBox">
							<a href="javascript:;" tag="">所有产品</a>
							<a href="javascript:;" tag="1">跟团游</a>
							<a href="javascript:;" tag="2">自由行</a>
							<a href="javascript:;" tag="3">邮轮</a>
							<a href="javascript:;" tag="4">签证</a>
						</div>
					</div>
                    <form class="fl" onsubmit="javascript:return checkform(this);" name="search" action="/search.html">
                        <input class="fl" type="text" id="js_show_hotseach" name="key" tabindex="8" autocomplete="off" value="请输入目的地、主题或关键字" />
                    </form>
				</div>
				<div class="sch_r fl">
					<input type="button" class="seach-btn_01" value="搜 &nbsp;索">
				</div>
				<div id="unlimited" class="hotseach on">
					<ul>
						<li class="input-txt">
							<input id="seachTxt" type="text" value="请输入目的地、主题或关键字" />
						</li>
						<li class="input-data">
							<strong class="fb">出发日期
								<span>不限</span>
							</strong>
							<input id="d1" class="Wdate" readonly=readonly type="text" onClick="WdatePicker({minDate:'%y-%M-{%d}', isShowOthers:false})"/>
							<b>~</b>
							<input id="d2" class="Wdate" readonly=readonly type="text" onClick="WdatePicker({minDate:'%y-%M-{%d}', isShowOthers:false})"/>
						</li>
						<li class="input-cost">
							<strong class="fb">价格范围
								<span>不限</span>
							</strong>
							<input id="p1" type="text"/>
							<b>~</b>
							<input id="p2" type="text"/>
						</li>
						<li class="mb30">
							<a id="hotseachBtn" class="seach-btn seach-btn_01" href="javascript:;">搜<b></b>索</a>
							<i id="hotseachClose"></i>
						</li>
					</ul>
				</div>
				<div class="h_hotword fl">
					<span>热门搜索：</span>
					<a href="search/493-0-66-0-0-0-0-0.html" target="_blank">毛里求斯</a>
					<a href="search/491-0-16-0-0-0-0-0.html" target="_blank">泰国</a>
					<a href="search/492-0-32-0-0-0-0-0.html" target="_blank">希腊</a>
					<a href="search/231-0-23-0-0-0-0-0.html" target="_blank">韩国</a>
				</div>
			</div>
			<div class="header-phone rl">
				<p>服务时间：09:00-17:30</p>
				<p class="colorE66202">4006-777-666<p style="font: 12px 'Microsoft Yahei'; color:#e66202;">专家热线：64743032、64743035</p></p>
			</div>
		</div>
		<div class="index-nav m">
			<div class="nav-column clearfix">
				<span>全部旅游产品分类<i></i></span>
				<ul id="changeNav" class="nav-main fl clearfix">
					<li class="index"><a href="<%=url0 %>">首页</a></li>
					<li class="outbound"><a href="<%=url1 %>">出境旅游</a></li>
					<li class="inland"><a href="<%=url2 %>">国内旅游</a></li>
					<li class="freetour"><a href="<%=url3 %>">玩转自由行</a></li>
					<li class="cruise"><a href="<%=url4 %>">邮轮旅游</a></li>
					<li class="visa"><a href="<%=url5 %>">签证</a></li>
					<li class="island"><a href="http://www.scyts.com/search/1279-0-0-0-0-0-0-0.html">海岛</a></li>
                    <li class="island"><a href="http://www.scyts.com/search.html?key=club">clubmed</a></li>
					<li class="schedule" style="position:relative;">
						<i></i>
						<a href="https://www.wenjuan.com/s/RFNN3uW/" target="_blank">定制游</a>
					</li>
				</ul>
			</div>
		</div>
	</div>
	<!--页头End-->
    <script type="text/javascript">
        $('.seach-btn_01').click(function () {
            var searchkey1 = $("#js_show_hotseach").val();
            var searchkey2 = $("#seachTxt").val();
            var key = "";
            var url = "";
            var plantype = GetType($('.SelectLineType').html());
            if (searchkey1 == "请输入目的地、主题或关键字" && searchkey2 == "请输入目的地、主题或关键字") {
                alert("请输入目的地、主题或关键字");
                return false;
            }
            if (searchkey1 != "请输入目的地、主题或关键字") key = searchkey1;
            if (searchkey2 != "请输入目的地、主题或关键字") key = searchkey2;

            url += "key=" + encodeURIComponent(key);
            if (plantype != "") url += "&s=" + plantype;
            if ($("#d1").val() != "") url += "&d1=" + $("#d1").val();
            if ($("#d2").val() != "") url += "&d2=" + $("#d2").val();
            if ($("#p1").val() != "") url += "&p1=" + $("#p1").val();
            if ($("#p2").val() != "") url += "&p2=" + $("#p2").val();
            window.location = "search.html?" + url;
        })

        function GetType(flag) {
            if (flag == "所有产品") return "";
            if (flag == "跟团游") return "0-1-0-0-0-0-0-0";
            if (flag == "自由行") return "0-2-0-0-0-0-0-0";
            if (flag == "邮轮") return "0-3-0-0-0-0-0-0";
            if (flag == "签证") return "0-4-0-0-0-0-0-0";
        }
    </script>