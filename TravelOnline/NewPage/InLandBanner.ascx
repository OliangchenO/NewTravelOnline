<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InLandBanner.ascx.cs" Inherits="TravelOnline.NewPage.InLandBanner" %>
    	<div class="hotplace-wrap">
			<h2>国内热门目的地</h2>
			<div class="place-con">
				<p>海南<span>|</span>福建</p>
				<a href="search.html?s=224-0-116-725-0-0-0-0" target="_blank">三亚</a>
				<a href="search.html?s=224-0-116-730-0-0-0-0" target="_blank">博鳌</a>
				<a href="search.html?s=224-0-116-729-0-0-0-0" target="_blank">琼海</a></br>
				<a href="search.html?s=224-0-141-0-0-0-0-0" target="_blank">福建</a>
				<a href="search.html?s=224-0-141-673-0-0-0-0" target="_blank">厦门</a>
			</div>
			<div class="place-con">
				<p>广西<span>|</span>云南</p>
				<a href="search.html?s=466-0-124-568-0-0-0-0" target="_blank">桂林</a>
				<a href="search.html?s=466-0-124-569-0-0-0-0" target="_blank">北海</a>
				<a href="search.html?s=466-0-124-570-0-0-0-0" target="_blank">南宁</a></br>
				<a href="search.html?s=466-0-81-740-0-0-0-0" target="_blank">昆明</a>
				<a href="search.html?s=466-0-81-739-0-0-0-0" target="_blank">丽江</a>
				<a href="search.html?s=466-0-81-741-0-0-0-0" target="_blank">大理</a>
			</div>
			<div class="place-con">
				<p>湖南<span>|</span>湖北</p>
				<a href="search.html?s=280-0-127-582-0-0-0-0" target="_blank">张家界</a>
				<a href="search.html?s=280-0-127-582-822-0-0-0" target="_blank">凤凰古城</a></br>
				<a href="search.html?s=280-0-128-590-0-0-0-0" target="_blank">宜昌</a>
				<a href="search.html?s=280-0-128-861-0-0-0-0" target="_blank">神农架</a>
				<a href="search.html?s=280-0-128-966-0-0-0-0" target="_blank">武当山</a>
			</div>
			<div class="place-con">
				<p>华东</p>
				<a href="search.html?s=223-0-14-0-0-0-0-0" target="_blank">上海</a>
				<a href="search.html?s=223-0-137-0-0-0-0-0" target="_blank">江苏</a>
				<a href="search.html?s=223-0-138-0-0-0-0-0" target="_blank">浙江</a>
				<a href="search.html?s=223-0-138-656-0-0-0-0" target="_blank">杭州</a>
				<a href="search.html?s=223-0-138-657-0-0-0-0" target="_blank">宁波</a>
				<a href="search.html?s=223-0-139-665-0-0-0-0" target="_blank">黄山</a>
			</div>
			<div class="place-con">
				<p>西南</p>
				<a href="search.html?s=466-0-122-0-0-0-0-0" target="_blank">西藏</a>
				<a href="search.html?s=466-0-122-732-0-0-0-0" target="_blank">拉萨</a>
				<a href="search.html?s=466-0-122-734-0-0-0-0" target="_blank">林芝</a>
				<a href="search.html?s=466-0-122-735-0-0-0-0" target="_blank">日喀则</a>
			</div>
			<div class="place-con">
				<p>北方</p>
				<a href="search.html?s=281-0-13-0-0-0-0-0" target="_blank">北京</a>
				<a href="search.html?s=281-0-129-0-0-0-0-0" target="_blank">河南</a>
				<a href="search.html?s=281-0-136-0-0-0-0-0" target="_blank">山东</a>
				<a href="search.html?s=281-0-136-641-0-0-0-0" target="_blank">青岛</a>
				<a href="search.html?s=281-0-133-0-0-0-0-0" target="_blank">黑龙江</a>
				<a href="search.html?s=281-0-134-0-0-0-0-0" target="_blank">吉林</a>
				<a href="search.html?s=281-0-133-625-0-0-0-0" target="_blank">哈尔滨</a>
			</div>
			<div class="place-con">
				<p>西北</p>
				<a href="search.html?s=469-0-143-0-0-0-0-0" target="_blank">山西</a>
				<a href="search.html?s=469-0-142-0-0-0-0-0" target="_blank">陕西</a>
				<a href="search.html?s=469-0-142-685-0-0-0-0" target="_blank">西安</a>
				<a href="search.html?s=469-0-131-0-0-0-0-0" target="_blank">内蒙古</a>
				<a href="search.html?s=469-0-132-620-0-0-0-0" target="_blank">吐鲁番</a>
				<a href="search.html?s=469-0-132-617-0-0-0-0" target="_blank">乌鲁木齐</a>
			</div>
		</div>
		<div class="ranking">
			<h3>热销排行</h3>
			<%=TravelOnline.NewPage.Class.CacheClass.Second_Hot("Inland_Hot")%>
		</div>
		<div class="month-hot">
			<h3>年度热门推荐</h3>
			<div class="month-con">
				<div class="place">
					<a href="search.html?s=466-0-81-739-0-0-0-0" target="_blank">丽江</a>
					<a href="search.html?s=280-0-128-861-0-0-0-0" target="_blank">神农架</a>
					<a href="search.html?s=224-0-116-725-0-0-0-0" target="_blank">三亚</a>
					<a href="search.html?s=280-0-373-704-0-0-0-0" target="_blank">贵阳</a>
					<a href="search.html?s=466-0-124-568-0-0-0-0" target="_blank">桂林</a>
					<a href="search.html?s=466-0-81-742-0-0-0-0" target="_blank">香格里拉</a>
					<a href="search.html?s=224-0-141-673-0-0-0-0" target="_blank">厦门</a>
					<a href="search.html?s=466-0-122-732-0-0-0-0" target="_blank">拉萨</a>
					<a href="search.html?s=281-0-136-641-0-0-0-0" target="_blank">青岛</a>
					<a href="search.html?s=281-0-134-631-0-0-0-0" target="_blank">长白山</a>
					<a href="search.html?s=281-0-135-637-0-0-0-0" target="_blank">大连</a>
					<a href="search.html?s=469-0-132-0-0-0-0-0" target="_blank">新疆</a>
					<a href="search.html?s=280-0-128-860-0-0-0-0" target="_blank">长江三峡</a>
					<a href="search.html?s=466-0-81-742-0-0-0-0" target="_blank">香格里拉</a>
					<a href="search.html?s=466-0-81-741-0-0-0-0" target="_blank">大理</a>
					<a href="search.html?s=469-0-374-831-0-0-0-0" target="_blank">九寨沟</a>
					<a href="search.html?s=281-0-136-641-0-0-0-0" target="_blank">青岛</a>
				</div>
			</div>
		</div>