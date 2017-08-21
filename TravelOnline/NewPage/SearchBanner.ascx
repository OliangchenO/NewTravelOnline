<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBanner.ascx.cs" Inherits="TravelOnline.NewPage.SearchBanner" %>
<div class="seach-wrap clearfix">
	<div id="popularSeach" class="hotseach-box">
		<div class="tit">热门城市(可直接输入城市)<span class="close">关闭</span></div>
		<div class="seach-con">
			<div class="nav-section">
				<h3>
					<i class="blue"></i>
					<span>出境游</span>
				</h3>
				<div class="city">
					<a>香港</a><a>台湾</a><a>韩国</a><a>日本</a><a>英国</a><a>美国</a><a>德国</a><a>新加坡</a><a>泰国</a><a>曼谷</a><a>法国</a><a>土耳其</a>
					<a>柬埔寨</a><a>台湾</a><a>清迈</a><a>尼泊尔</a><a>澳大利亚</a><a>意大利</a><a>葡萄牙</a><a>瑞士</a><a>澳门</a><a>马尔代夫</a><a>普吉岛</a>
					<a>巴厘岛</a><a>斯里兰卡</a><a>希腊</a><a>加拿大</a>
				</div>
			</div>
			<div class="nav-section">
				<h3>
					<i class="red"></i>
					<span>国内游</span>
				</h3>
				<div class="city">
					<a>北京</a><a>云南</a><a>广西</a><a>海南</a><a>三亚</a><a>四川</a><a>山东</a><a>哈尔滨</a><a>广西</a><a>桂林</a><a>山西</a><a>陕西</a>
					<a>黑龙江</a><a>厦门</a><a>上海</a><a>浙江</a><a>苏州</a><a>安徽</a><a>江西</a><a>重庆</a><a>宜昌</a>
				</div>
			</div>
			<div class="nav-section">
				<h3>
					<i class="purple"></i>
					<span>主题</span>
				</h3>
				<div class="city">
					<a>温泉</a><a>游山</a><a>纯玩</a><a>古镇</a><a>亲水</a><a>蜜月</a><a>赏花</a><a>海岛</a><a>主题乐园</a>
				</div>
			</div>
		</div>
	</div>
	<input type="text" value="" id="searchPageInput" />
	<button type="submit" class="btn" id="submitPage" ></button>
	<i id="search_keyword"></i>
</div>