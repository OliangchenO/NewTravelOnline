<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftSlide.ascx.cs" Inherits="TravelOnline.NewPage.LeftSlide" %>
<%--    <div id="leftSidenav" class="sidenav relative-box fl">
		<div id="unlimited" class="hotseach">
			<ul>
				<li class="input-txt">
					<strong class="fb">关键字</strong>
					<input id="seachTxt" type="text" value="请输入目的地、主题或关键字" />
				</li>
				<li class="input-data">
					<strong class="fb">出发日期
						<span>不限</span>
					</strong>
					<input class="Wdate" type="text" onClick="WdatePicker({minDate:'%y-%M-{%d}', isShowOthers:false})"/>
					<b>~</b>
					<input class="Wdate" type="text" onClick="WdatePicker({minDate:'%y-%M-{%d}', isShowOthers:false})"/>
				</li>
				<li class="input-cost">
					<strong class="fb">价格范围
						<span>不限</span>
					</strong>
					<input type="text"/>
					<b>~</b>
					<input type="text"/>
				</li>
				<li class="input-day">
					<strong class="fb">天数
						<b id="unlimiteDay">不限</b>
					</strong>
					<div class="select-box">
						<label><input type="checkbox"/>1日</label>
						<label><input type="checkbox"/>2日</label>
						<label><input type="checkbox"/>3日</label>
						<label><input type="checkbox"/>3日以上</label>
					</div>
				</li>
				<li class="mb30">
					<a id="hotseachBtn" class="fb seach-btn" href="new_search.html?s=">搜<b></b>索</a>
					<i id="hotseachClose"></i>
				</li>
			</ul>
		</div>
		<div class="sidenav-top">
			<div class="sidenav-top-seach clearfix">
				<input id="seachItem" class="fl" type="text" value="请输入目的地、主题或关键字" />
				<span id="seach">搜索</span>
			</div>
			<div class="sidenav-top-txt clearfix">
				<span class="fl">高级搜索更快、更准确</span>
				<p id="highSeach" class="rl"><a href="new_search.html?s=">高级搜索</a></p>
			</div>
		</div>
		<div class="sidenav-bottom">
			<div class="city-tab">
				<div id="changeArea" class="area">
					<ul class="clearfix">
						<li class="current">出境游</li>
						<li>国内游</li>
						<li>自由行</li>
						<li class="no-border">邮轮</li>
					</ul>
				</div>
				<!--出境-->
				<div class="submenu action">
					<div class="column-2 clearfix">
						<h2 class="fb fl">港澳</h2>
						<p class="clearfix fl">
							<a class="colorf00" href="new_search.html?s=" target="_blank">港澳</a>
							<a href="new_search.html?s=" target="_blank">香港</a>
							<a href="new_search.html?s=" target="_blank">澳门</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">台湾</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">台湾</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">日韩</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">日本</a>
							<a href="new_search.html?s=" target="_blank">东京</a>
							<a href="new_search.html?s=" target="_blank">韩国</a>
							<a href="new_search.html?s=" target="_blank">首尔</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">澳新</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">澳大利亚</a>
							<a href="new_search.html?s=" target="_blank">新西兰</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">东南亚</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">泰国</a>
							<a href="new_search.html?s=" target="_blank">新加坡</a>
							<a href="new_search.html?s=" target="_blank">越南</a>
							<a href="new_search.html?s=" target="_blank">斯里兰卡</a>
							<a href="new_search.html?s=" target="_blank">尼泊尔</a>
							<a href="new_search.html?s=" target="_blank">印度</a>
							<a href="new_search.html?s=" target="_blank">塞班岛</a>
							<a href="new_search.html?s=" target="_blank">普吉岛</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">欧洲</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">英国</a>
							<a href="new_search.html?s=" target="_blank">法国</a>
							<a href="new_search.html?s=" target="_blank">德国</a>
							<a href="new_search.html?s=" target="_blank">瑞典</a>
							<a href="new_search.html?s=" target="_blank">希腊</a>
							<a href="new_search.html?s=" target="_blank">意大利</a>
							<a href="new_search.html?s=" target="_blank">葡萄牙</a>
							<a href="new_search.html?s=" target="_blank">奥地利</a>
							<a href="new_search.html?s=" target="_blank">丹麦</a>
							<a href="new_search.html?s=" target="_blank">芬兰</a>
							<a href="new_search.html?s=" target="_blank">捷克</a>
							<a href="new_search.html?s=" target="_blank">瑞士</a>
							<a href="new_search.html?s=" target="_blank">西班牙</a>
							<a href="new_search.html?s=" target="_blank">挪威</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">中东非</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">埃及</a>
							<a href="new_search.html?s=" target="_blank">肯尼亚</a>
							<a href="new_search.html?s=" target="_blank">土耳其</a>
							<a href="new_search.html?s=" target="_blank">南非</a>
							<a href="new_search.html?s=" target="_blank">阿联酋</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">美加</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">美国</a>
							<a href="new_search.html?s=" target="_blank">加拿大</a>
						</p>
					</div>
					<div class="column-2 clearfix">
						<h2 class="fb fl">南美</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">墨西哥</a>
							<a href="new_search.html?s=" target="_blank">阿根廷</a>
							<a href="new_search.html?s=" target="_blank">巴西</a>
							<a href="new_search.html?s=" target="_blank">智力</a>
							<a href="new_search.html?s=" target="_blank">秘鲁</a>
						</p>
					</div>
				</div>
				<!--国内-->
				<div class="submenu">
					<div class="column-1 clearfix">
						<h2 class="fb fl">热门</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">上海</a>
							<a href="new_search.html?s=" target="_blank">北京</a>
							<a href="new_search.html?s=" target="_blank">广州</a>
							<a href="new_search.html?s=" target="_blank">厦门</a>
							<a href="new_search.html?s=" target="_blank">海南</a>
							<a href="new_search.html?s=" target="_blank">海南</a>
							<a href="new_search.html?s=" target="_blank">海南</a>
							<a href="new_search.html?s=" target="_blank">海南</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">北方</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">北京</a>
							<a href="new_search.html?s=" target="_blank">河南</a>
							<a href="new_search.html?s=" target="_blank">山东</a>
							<a href="new_search.html?s=" target="_blank">青岛</a>
							<a href="new_search.html?s=" target="_blank">辽宁</a>
							<a href="new_search.html?s=" target="_blank">哈尔滨</a>
							<a href="new_search.html?s=" target="_blank">黑龙江</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">西南</h2>
						<p class="clearfix fl">
							<a class="colorf00" href="new_search.html?s=" target="_blank">云南</a>
							<a href="new_search.html?s=" target="_blank">昆明</a>
							<a href="new_search.html?s=" target="_blank">广西</a>
							<a href="new_search.html?s=" target="_blank">桂林</a>
							<a href="new_search.html?s=" target="_blank">南宁</a>
							<a href="new_search.html?s=" target="_blank">西藏</a>
							<a href="new_search.html?s=" target="_blank">拉萨</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">西北</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">山西</a>
							<a href="new_search.html?s=" target="_blank">四川</a>
							<a href="new_search.html?s=" target="_blank">内蒙古</a>
							<a href="new_search.html?s=" target="_blank">陕西</a>
							<a href="new_search.html?s=" target="_blank">新疆</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">华东</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">上海</a>
							<a href="new_search.html?s=" target="_blank">浙江</a>
							<a href="new_search.html?s=" target="_blank">江苏</a>
							<a href="new_search.html?s=">安徽</a>
							<a href="new_search.html?s=">江西</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">华南</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">广州</a>
							<a href="new_search.html?s=" target="_blank">海南</a>
							<a href="new_search.html?s=" target="_blank">福建</a>
							<a href="new_search.html?s=" target="_blank">厦门</a>
						</p>
					</div>
					<div class="column-1 clearfix">
						<h2 class="fb fl">华中</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">湖南</a>
							<a href="new_search.html?s=" target="_blank">湖北</a>
							<a href="new_search.html?s=" target="_blank">重庆</a>
							<a href="new_search.html?s=" target="_blank">贵州</a>
							<a href="new_search.html?s=" target="_blank">青海</a>
							<a href="new_search.html?s=" target="_blank">长江三峡</a>
							<a href="new_search.html?s=" target="_blank">张家界</a>
							<a href="new_search.html?s=" target="_blank">嘉峪关</a>
							<a href="new_search.html?s=" target="_blank">宜昌</a>
						</p>
					</div>
				</div>
				<!--自由行-->
				<div class="submenu">
					<div class="column-3 clearfix">
						<h2 class="fb fl">热门</h2>
						<p class="clearfix fl">
							<a class="colorf00" href="new_search.html?s=" target="_blank">普吉岛</a>
							<a href="new_search.html?s=" target="_blank">巴厘岛</a>
							<a href="new_search.html?s=" target="_blank">沙巴</a>
							<a href="new_search.html?s=" target="_blank">岘港</a>
							<a href="new_search.html?s=" target="_blank">柬埔寨</a>
							<a href="new_search.html?s=" target="_blank">曼谷</a>
							<a href="new_search.html?s=" target="_blank">清迈</a>
							<a href="new_search.html?s=" target="_blank">香港</a>
						</p>
					</div>
					<div class="column-3 clearfix">
						<h2 class="fb fl">东南亚</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">普吉岛</a>
							<a href="new_search.html?s=" target="_blank">巴厘岛</a>
							<a href="new_search.html?s=" target="_blank">沙巴</a>
							<a href="new_search.html?s=" target="_blank">岘港</a>
							<a href="new_search.html?s=" target="_blank">菲律宾</a>
							<a href="new_search.html?s=" target="_blank">薄荷岛</a>
							<a href="new_search.html?s=" target="_blank">宿雾</a>
							<a href="new_search.html?s=" target="_blank">马尼拉</a>
							<a href="new_search.html?s=" target="_blank">越南</a>
							<a href="new_search.html?s=" target="_blank">柬埔寨</a>
							<a href="new_search.html?s=" target="_blank">河内</a>
							<a href="new_search.html?s=" target="_blank">暹粒</a>
							<a href="new_search.html?s=" target="_blank">泰国</a>
							<a href="new_search.html?s=" target="_blank">曼谷</a>
							<a href="new_search.html?s=" target="_blank">清迈</a>
							<a href="new_search.html?s=" target="_blank">苏梅岛</a>
							<a href="new_search.html?s=" target="_blank">马来西亚</a>
							<a href="new_search.html?s=" target="_blank">吉隆坡</a>
							<a href="new_search.html?s=" target="_blank">槟城</a>
							<a href="new_search.html?s=" target="_blank">印度尼西亚</a>
							<a href="new_search.html?s=" target="_blank">雅加达</a>
						</p>
					</div>
					<div class="column-3 clearfix">
						<h2 class="fb fl">港澳台</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">香港</a>
							<a href="new_search.html?s=" target="_blank">澳门</a>
							<a href="new_search.html?s=" target="_blank">台湾</a>
						</p>
					</div>
					<div class="column-3 clearfix">
						<h2 class="fb fl">国内</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">广西</a>
							<a href="new_search.html?s=" target="_blank">桂林</a>
						</p>
					</div>
				</div>
				<!--邮轮-->
				<div class="submenu">
					<div class="column-3 clearfix">
						<h2 class="fb fl">欧美线</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">法国</a>
							<a href="new_search.html?s=" target="_blank">意大利</a>
							<a href="new_search.html?s=" target="_blank">西班牙</a>
							<a href="new_search.html?s=" target="_blank">美国</a>
						</p>
					</div>
					<div class="column-3 clearfix">
						<h2 class="fb fl">日韩线</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">韩国</a>
							<a href="new_search.html?s=" target="_blank">日本</a>
						</p>
					</div>
					<div class="column-3 clearfix">
						<h2 class="fb fl">东南亚</h2>
						<p class="clearfix fl">
							<a href="new_search.html?s=" target="_blank">新加坡</a>
						</p>
					</div>
					<div class="column-3 no-mrb clearfix">
						<a class="freedom" href="new_search.html?s=" title="特价内舱双人房"><img src="image/freedom_bg.jpg" alt="特价内舱双人房"></a>
					</div>
				</div>
			</div>
		</div>
	</div>--%>
    <div id="js_sidenav" class="basefix">
				<ul class="destination_list">
					<li>
						<div class="destination_col">
							<i class="icon_side"></i>
							<h3>周边游</h3>
							<p>
								<a href="new_search.html?s=223-0-14-0-0-0-1-0" target="_blank">上海</a>
								<a href="new_search.html?s=223-0-138-0-0-0-1-0" target="_blank">浙江</a>
								<a href="new_search.html?s=223-0-137-0-0-0-1-0" target="_blank">江苏</a>
								<a href="new_search.html?s=223-0-139-0-0-0-1-0" target="_blank">安徽</a>
								<a href="new_search.html?s=223-0-140-0-0-0-1-0" target="_blank">江西</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col destination_inland">
							<i class="icon_side icon_inland"></i>
							<h3>国内旅游</h3>
							<p>
								<a href="new_search.html?s=223-0-14-0-0-0-1-0" target="_blank">上海</a>
								<a href="new_search.html?s=281-0-13-0-0-0-1-0" target="_blank">北京</a>
								<a href="new_search.html?s=224-0-125-553-0-0-1-0" target="_blank">广州</a>
								<a href="new_search.html?s=466-0-81-0-0-0-1-0" target="_blank">云南</a>
								<a href="new_search.html?s=224-0-141-673-0-0-1-0" target="_blank">厦门</a>
								<a href="new_search.html?s=280-0-127-0-0-0-1-0" target="_blank">湖南</a>
								<a href="new_search.html?s=469-0-142-0-0-0-1-0" target="_blank">陕西</a>
								<a href="new_search.html?s=466-0-124-568-0-0-1-0" target="_blank">桂林</a>
								<a href="new_search.html?s=466-0-124-0-0-0-1-0" target="_blank">广西</a>
								<a href="new_search.html?s=281-0-133-625-0-0-1-0" target="_blank">哈尔滨</a>
								<a href="new_search.html?s=280-0-127-582-0-0-1-0" target="_blank">张家界</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_hk"></i>
							<h3>港澳台&nbsp; 日本&nbsp; 韩国</h3>
							<p>
								<a href="new_search.html?key=迪士尼" target="_blank" class="vital">迪士尼</a>
								<a href="new_search.html?s=851-0-112-712-0-0-1-0" target="_blank">台北</a>
								<a href="new_search.html?s=231-0-22-185-0-0-1-0" target="_blank">大阪</a>
								<a href="new_search.html?s=231-0-23-199-0-0-1-0" target="_blank">首尔</a>
								<a href="new_search.html?s=231-0-23-200-0-0-1-0" target="_blank">济州岛</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_asia"></i>
							<h3>东南亚&nbsp; 泰国</h3>
							<p>	
								<a href="new_search.html?s=491-0-26-219-0-0-1-0" target="_blank">巴厘岛</a>
								<a href="new_search.html?s=491-0-20-0-0-0-1-0" target="_blank">柬埔寨</a>
								<a href="new_search.html?s=491-0-18-0-0-0-1-0" target="_blank">新加坡</a>
								<a href="new_search.html?s=491-0-24-0-0-0-1-0" target="_blank">马尔代夫</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_europe"></i>
							<h3>欧洲</h3>
							<p>
								<a href="new_search.html?s=492-0-30-0-0-0-1-0" target="_blank">英国</a>
								<a href="new_search.html?s=492-0-15-0-0-0-1-0" target="_blank">法国</a>
								<a href="new_search.html?s=492-0-32-0-0-0-1-0" target="_blank">希腊</a>
								<a href="new_search.html?s=492-0-27-0-0-0-1-0" target="_blank">意大利</a>
								<a href="new_search.html?s=492-0-93-0-0-0-1-0" target="_blank">葡萄牙</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_usa"></i>
							<h3>美洲</h3>
							<p>
								<a href="new_search.html?s=637-0-37-0-0-0-1-0" target="_blank">美国</a>
								<a href="new_search.html?s=637-0-38-0-0-0-1-0" target="_blank">加拿大</a>
								<a href="new_search.html?s=580-0-42-0-0-0-1-0" target="_blank">墨西哥</a>
								<a href="new_search.html?s=580-0-47-0-0-0-1-0" target="_blank">巴西</a>
								<a href="new_search.html?s=580-0-50-0-0-0-1-0" target="_blank">智利</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_australia"></i>
							<h3>澳新 &nbsp;中东非</h3>
							<p>
								<a href="new_search.html?s=489-0-55-0-0-0-1-0" target="_blank">澳大利亚</a>
								<a href="new_search.html?s=493-0-62-0-0-0-1-0" target="_blank">埃及</a>
								<a href="new_search.html?s=493-0-62-0-0-0-1-0" target="_blank">肯尼亚</a>
								<a href="new_search.html?s=493-0-33-0-0-0-1-0" target="_blank">土耳其</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
					<li>
						<div class="destination_col">
							<i class="icon_side icon_cruise"></i>
							<h3>邮轮</h3>
							<p>
								<a href="new_search.html?s=1063-0-0-0-0-0-1-0" target="_blank">日韩航线</a>
								<a href="new_search.html?s=1066-0-0-0-0-0-1-0" target="_blank">欧美航线</a>
								<a href="new_search.html?s=1065-0-0-0-0-0-1-0" target="_blank">东南亚航线</a>
							</p>
							<i class="icon_side icon_arrow"></i>
						</div>
					</li>
				</ul>
				<!--周边-->
				<div class="side_jmp near" style="top:0;">
					<div class="left_detail">
						<dl class="detail_list">
							<dt>出游方式</dt>
							<dd>
								<a href="new_search.html?s=0-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=0-2-0-0-0-0-1-0" target="_blank">自由行</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>游玩天数</dt>
							<dd>
								<a href="new_search.html?s=0-0-0-0-1-0-1-0" target="_blank">一日游</a>
								<a href="new_search.html?s=0-0-0-0-2-0-1-0" target="_blank">二日游</a>
								<a href="new_search.html?s=0-0-0-0-3-0-1-0" target="_blank">三日游</a>
								<a href="new_search.html?s=0-0-0-0-0-0-1-0" target="_blank">三日以上</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=223-0-138-656-0-0-1-0" target="_blank">杭州</a>
								<a href="new_search.html?s=223-0-137-650-0-0-1-0" target="_blank">苏州</a>
								<a href="new_search.html?s=223-0-137-652-0-0-1-0" target="_blank">无锡</a>
								<a href="new_search.html?s=223-0-138-657-0-0-1-0" target="_blank">宁波</a>
								<a href="new_search.html?key=西塘" target="_blank" class="org">西塘</a>
								<a href="new_search.html?key=乌镇" target="_blank">乌镇</a>
								<a href="new_search.html?s=223-0-138-820-0-0-1-0" target="_blank">舟山</a>
								<a href="new_search.html?s=223-0-138-826-0-0-1-0" target="_blank">温州</a>
								<a href="new_search.html?s=223-0-138-662-0-0-1-0" target="_blank">安吉</a>
								<a href="new_search.html?s=223-0-138-660-0-0-1-0" target="_blank">绍兴</a>
								<a href="new_search.html?key=普陀山" target="_blank">普陀山</a>
								<a href="new_search.html?key=镇江" target="_blank">镇江</a>
								<a href="new_search.html?s=223-0-138-658-0-0-1-0" target="_blank">台州</a>
								<a href="new_search.html?key=横店" target="_blank">横店</a>
								<a href="new_search.html?key=象山" target="_blank">象山</a>
								<a href="new_search.html?key=崇明" target="_blank">崇明</a>
								<a href="new_search.html?key=阳澄湖" target="_blank">阳澄湖</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=西湖" target="_blank">西湖</a>
								<a href="new_search.html?key=横店" target="_blank">横店影视城</a>
								<a href="new_search.html?key=浙西大峡谷" target="_blank">浙西大峡谷</a>
								<a href="new_search.html?key=千岛湖" target="_blank">千岛湖</a>
								<a href="new_search.html?key=古漪园" target="_blank" class="org">古漪园</a>
								<a href="new_search.html?key=枫泾" target="_blank">枫泾古镇</a>
								<a href="new_search.html?key=天目山" target="_blank">天目山</a>
								<a href="new_search.html?key=灵隐寺" target="_blank">灵隐寺</a>
							</dd>
						</dl>
					</div>
					<div class="right_detail">
						<a href="new_search.html?s=" target="_blank"><img src="image/side_jmp_pic1-1.png" title="" alt=""></a>
					</div>
				</div>
				<!--国内-->
				<div class="side_jmp internal" style="top:0;">
					<div class="left_detail">
						<dl class="detail_list">
							<dt>北方</dt>
							<dd>
								<a href="new_search.html?s=281-0-13-0-0-0-1-0" target="_blank">北京</a>
								<a href="new_search.html?s=281-0-136-0-0-0-1-0" target="_blank">山东</a>
								<a href="new_search.html?s=281-0-129-0-0-0-1-0" target="_blank">河南</a>
								<a href="new_search.html?s=281-0-135-0-0-0-1-0" target="_blank">辽宁</a>
								<a href="new_search.html?s=281-0-136-641-0-0-1-0" target="_blank">青岛</a>
								<a href="new_search.html?s=281-0-134-0-0-0-1-0" target="_blank">吉林</a>
								<a href="new_search.html?s=281-0-133-625-0-0-1-0" target="_blank">哈尔滨</a>
								<a href="new_search.html?s=281-0-133-0-0-0-1-0" target="_blank">黑龙江</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>华东</dt>
							<dd>
								<a href="new_search.html?s=223-0-14-0-0-0-1-0" target="_blank">上海</a>
								<a href="new_search.html?s=223-0-137-650-0-0-1-0" target="_blank">苏州</a>
								<a href="new_search.html?s=223-0-137-652-0-0-1-0" target="_blank">无锡</a>
								<a href="new_search.html?s=223-0-137-0-0-0-1-0" target="_blank">江苏</a>
								<a href="new_search.html?s=223-0-137-654-0-0-1-0" target="_blank" class="org">常州</a>
								<a href="new_search.html?s=223-0-138-656-0-0-1-0" target="_blank">杭州</a>
								<a href="new_search.html?s=223-0-138-657-0-0-1-0" target="_blank">宁波</a>
								<a href="new_search.html?s=223-0-138-820-0-0-1-0" target="_blank">舟山</a>
								<a href="new_search.html?s=223-0-138-0-0-0-1-0" target="_blank">浙江</a>
								<a href="new_search.html?s=223-0-139-0-0-0-1-0" target="_blank">安徽</a>
								<a href="new_search.html?s=223-0-140-0-0-0-1-0" target="_blank">江西</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>华中</dt>
							<dd>
								<a href="new_search.html?s=280-0-127-0-0-0-1-0" target="_blank">湖南</a>
								<a href="new_search.html?s=280-0-127-926-0-0-1-0" target="_blank">长沙</a>
								<a href="new_search.html?s=280-0-128-0-0-0-1-0" target="_blank">湖北</a>
								<a href="new_search.html?s=280-0-128-590-0-0-1-0" target="_blank">宜昌</a>
								<a href="new_search.html?s=280-0-128-589-0-0-1-0" target="_blank">武汉</a>
								<a href="new_search.html?s=280-0-113-0-0-0-1-0" target="_blank">重庆</a>
								<a href="new_search.html?s=280-0-373-0-0-0-1-0" target="_blank">贵州</a>
								<a href="new_search.html?s=280-0-126-0-0-0-1-0" target="_blank">青海</a>
								<a href="new_search.html?s=280-0-126-576-0-0-1-0" target="_blank">西宁</a>
								<a href="new_search.html?s=280-0-144-0-0-0-1-0" target="_blank">甘肃</a>
								<a href="new_search.html?s=280-0-144-698-0-0-1-0" target="_blank">嘉峪关</a>
								<a href="new_search.html?s=280-0-144-696-0-0-1-0" target="_blank">兰州</a>
							</dd>
						</dl>
					</div>
					<div class="left_detail right">
						<dl class="detail_list">
							<dt>西南</dt>
							<dd>
								<a href="new_search.html?s=466-0-81-0-0-0-1-0" target="_blank">云南</a>
                                <a href="new_search.html?s=466-0-81-739-0-0-1-0" target="_blank">丽江</a>
                                <a href="new_search.html?s=466-0-81-740-0-0-1-0" target="_blank">昆明</a>
                                <a href="new_search.html?s=466-0-81-741-0-0-1-0" target="_blank">大理</a>
								<a href="new_search.html?s=466-0-124-0-0-0-1-0" target="_blank">广西</a>
                                <a href="new_search.html?s=466-0-124-568-0-0-1-0" target="_blank">桂林</a>
                                <a href="new_search.html?s=466-0-124-569-0-0-1-0" target="_blank">北海</a>
                                <a href="new_search.html?s=466-0-124-570-0-0-1-0" target="_blank">南宁</a>
								<a href="new_search.html?s=466-0-122-0-0-0-1-0" target="_blank">西藏</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>华南</dt>
							<dd>
								<a href="new_search.html?s=224-0-116-0-0-0-1-0" target="_blank" class="org">海南</a>
								<a href="new_search.html?s=224-0-116-725-0-0-1-0" target="_blank">三亚</a>
								<a href="new_search.html?s=224-0-116-726-0-0-1-0" target="_blank">海口</a>
								<a href="new_search.html?s=224-0-125-0-0-0-1-0" target="_blank">广东</a>
								<a href="new_search.html?s=224-0-125-562-0-0-1-0" target="_blank">汕头</a>
								<a href="new_search.html?s=224-0-125-566-0-0-1-0" target="_blank">潮州</a>
								<a href="new_search.html?s=224-0-141-0-0-0-1-0" target="_blank">福建</a>
								<a href="new_search.html?s=224-0-141-673-0-0-1-0" target="_blank" class="org">厦门</a>
								<a href="new_search.html?s=224-0-141-674-0-0-1-0" target="_blank">福州</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>西北</dt>
							<dd>
								<a href="new_search.html?s=469-0-142-0-0-0-1-0" target="_blank">陕西</a>
                                <a href="new_search.html?s=469-0-142-685-0-0-1-0" target="_blank">西安</a>
                                <a href="new_search.html?s=469-0-142-687-0-0-1-0" target="_blank">咸阳</a>
								<a href="new_search.html?s=469-0-132-0-0-0-1-0" target="_blank">新疆</a>
                                <a href="new_search.html?s=469-0-132-617-0-0-1-0" target="_blank">乌鲁木齐</a>
                                <a href="new_search.html?s=469-0-143-0-0-0-1-0" target="_blank">山西</a>
								<a href="new_search.html?s=469-0-143-693-0-0-1-0" target="_blank">太原</a>
								<a href="new_search.html?s=469-0-374-0-0-0-1-0" target="_blank">四川</a>
                                <a href="new_search.html?s=469-0-374-831-0-0-1-0" target="_blank" class="org">九寨沟</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--港澳台 日韩-->
				<div class="side_jmp outbound" style="top:0;">
					<div class="left_detail">
						<h3>港澳台</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=227-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=227-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=227-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=227-0-110-0-0-0-1-0" target="_blank">香港</a>
								<a href="new_search.html?s=227-0-111-0-0-0-1-0" target="_blank">澳门</a>
								<a href="new_search.html?s=851-0-112-0-0-0-1-0" target="_blank">台湾</a>
								<a href="new_search.html?s=851-0-112-712-0-0-1-0" target="_blank">台北</a>
								<a href="new_search.html?s=851-0-112-714-0-0-1-0" target="_blank">高雄</a>
								<a href="new_search.html?s=851-0-112-715-0-0-1-0" target="_blank">桃园</a>
								<a href="new_search.html?s=851-0-112-713-0-0-1-0" target="_blank">垦丁</a>
								<a href="new_search.html?s=851-0-112-718-0-0-1-0" target="_blank">宜兰</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=香港迪士尼" target="_blank">香港迪士尼</a>
								<a href="new_search.html?key=海洋公园" target="_blank">海洋公园</a>
								<a href="new_search.html?key=大三巴牌坊" target="_blank">大三巴牌坊</a>
							</dd>
						</dl>
					</div>
					<div class="left_detail">
						<h3>日韩</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=231-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=231-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=231-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=231-0-22-184-0-0-1-0" target="_blank">东京</a>
								<a href="new_search.html?s=231-0-22-185-0-0-1-0" target="_blank">大阪</a>
								<a href="new_search.html?s=231-0-23-201-0-0-1-0" target="_blank">釜山</a>
								<a href="new_search.html?s=231-0-23-199-0-0-1-0" target="_blank">首尔</a>
								<a href="new_search.html?s=231-0-23-202-0-0-1-0" target="_blank">仁川</a>
								<a href="new_search.html?s=231-0-23-203-0-0-1-0" target="_blank">江原道</a>
								<a href="new_search.html?s=231-0-23-200-0-0-1-0" target="_blank">济州岛</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=富士山" target="_blank">富士山</a>
								<a href="new_search.html?key=浅草寺" target="_blank">浅草寺</a>
								<a href="new_search.html?key=明洞" target="_blank">明洞</a>
								<a href="new_search.html?key=乐天世界" target="_blank">乐天世界</a>
								<a href="new_search.html?key=首尔塔" target="_blank">首尔塔</a>
								<a href="new_search.html?key=景福宫" target="_blank">景福宫</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--东南亚 泰国-->
				<div class="side_jmp outbound" style="top:0;">
					<div class="left_detail">
						<h3>东南亚</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=491-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=491-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=491-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=491-0-26-219-0-0-1-0" target="_blank">巴厘岛</a>
								<a href="new_search.html?s=491-0-25-212-0-0-1-0" target="_blank">长滩岛</a>
								<a href="new_search.html?s=491-0-17-0-0-0-1-0" target="_blank">马来西亚</a>
								<a href="new_search.html?s=491-0-882-0-0-0-1-0" target="_blank">缅甸</a>
								<a href="new_search.html?s=491-0-19-0-0-0-1-0" target="_blank">越南</a>
								<a href="new_search.html?s=491-0-908-909-0-0-1-0" target="_blank">塞班岛</a>
								<a href="new_search.html?s=491-0-881-0-0-0-1-0" target="_blank">老挝</a>
								<a href="new_search.html?s=491-0-779-0-0-0-1-0" target="_blank">文莱</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=海神庙" target="_blank">海神庙</a>
								<a href="new_search.html?key=克拉码头" target="_blank">克拉码头</a>
								<a href="new_search.html?key=金巴兰码头" target="_blank">金巴兰码头</a>
								<a href="new_search.html?key=圣淘沙岛" target="_blank">圣淘沙岛</a>
							</dd>
						</dl>
					</div>
					<div class="left_detail">
						<h3>泰国</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=491-1-16-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=491-2-16-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=491-3-16-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=491-0-16-149-0-0-1-0" target="_blank">曼谷</a>
								<a href="new_search.html?s=491-0-16-151-0-0-1-0" target="_blank">清迈</a>
								<a href="new_search.html?s=491-0-16-150-0-0-1-0" target="_blank">芭提雅</a>
								<a href="new_search.html?s=491-0-16-157-0-0-1-0" target="_blank">甲米</a>
								<a href="new_search.html?s=491-0-16-148-0-0-1-0" target="_blank">普吉岛</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=四面佛幻奇乐园" target="_blank">四面佛幻奇乐园</a>
								<a href="new_search.html?key=双龙寺" target="_blank">双龙寺</a>
								<a href="new_search.html?key=巴东海滩" target="_blank">巴东海滩</a>
								<a href="new_search.html?key=皇帝岛" target="_blank">皇帝岛</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--欧洲-->
				<div class="side_jmp outbound europe" style="top:58px;">
					<div class="left_detail">
						<h3>欧洲</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=492-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=492-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=492-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=492-0-30-0-0-0-1-0" target="_blank">英国</a>
								<a href="new_search.html?s=492-0-15-0-0-0-1-0" target="_blank">法国</a>
								<a href="new_search.html?s=492-0-29-0-0-0-1-0" target="_blank">西班牙</a>
								<a href="new_search.html?s=492-0-27-0-0-0-1-0" target="_blank">意大利</a>
								<a href="new_search.html?s=492-0-32-0-0-0-1-0" target="_blank">希腊</a>
								<a href="new_search.html?s=492-0-15-71-0-0-1-0" target="_blank">巴黎</a>
								<a href="new_search.html?s=492-0-28-0-0-0-1-0" target="_blank">德国</a>
								<a href="new_search.html?s=492-0-34-0-0-0-1-0" target="_blank">瑞士</a>
								<a href="new_search.html?s=492-0-30-298-0-0-1-0" target="_blank">伦敦</a>
								<a href="new_search.html?s=492-0-32-317-0-0-1-0" target="_blank">雅典</a>
								<a href="new_search.html?s=492-0-27-268-0-0-1-0" target="_blank">罗马</a>
								<a href="new_search.html?s=492-0-36-0-0-0-1-0" target="_blank">俄罗斯</a>
								<a href="new_search.html?key=爱琴海" target="_blank">爱琴海岛</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=卢浮宫" target="_blank">卢浮宫</a>
								<a href="new_search.html?key=埃菲尔铁塔" target="_blank">埃菲尔铁塔</a>
								<a href="new_search.html?key=卢塞恩" target="_blank">卢塞恩</a>
								<a href="new_search.html?key=圣彼得大教堂" target="_blank">圣彼得大教堂</a>
								<a href="new_search.html?key=天鹅堡" target="_blank">天鹅堡</a>
								<a href="new_search.html?key=塞纳河游船" target="_blank">塞纳河游船</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--美洲-->
				<div class="side_jmp outbound europe" style="top:150px;">
					<div class="left_detail">
						<h3>美洲</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=637-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=637-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=637-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=637-0-37-0-0-0-1-0" target="_blank">美国</a>
								<a href="new_search.html?s=637-0-38-0-0-0-1-0" target="_blank">加拿大</a>
								<a href="new_search.html?s=580-0-48-0-0-0-1-0" target="_blank">阿根廷</a>
								<a href="new_search.html?s=580-0-47-0-0-0-1-0" target="_blank">巴西</a>
								<a href="new_search.html?s=580-0-42-0-0-0-1-0" target="_blank">墨西哥</a>
								<a href="new_search.html?s=637-0-37-414-0-0-1-0" target="_blank">夏威夷</a>
								<a href="new_search.html?s=637-0-37-415-0-0-1-0" target="_blank">关岛</a>
								<a href="new_search.html?s=637-0-37-411-0-0-1-0" target="_blank">洛杉矶</a>
								<a href="new_search.html?s=637-0-37-407-0-0-1-0" target="_blank">纽约</a>
								<a href="new_search.html?s=637-0-37-412-0-0-1-0" target="_blank">华盛顿</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=美国迪士尼" target="_blank">美国迪士尼</a>
								<a href="new_search.html?key=环球影城" target="_blank">环球影城</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--澳新 中东非-->
				<div class="side_jmp outbound" style="top:209px;">
					<div class="left_detail">
						<h3>澳新</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=489-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=489-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=489-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=489-0-55-0-0-0-1-0" target="_blank">澳大利亚</a>
								<a href="new_search.html?s=489-0-56-0-0-0-1-0" target="_blank">新西兰</a>
								<a href="new_search.html?s=489-0-57-0-0-0-1-0" target="_blank">斐济</a>
								<a href="new_search.html?s=489-0-55-486-0-0-1-0" target="_blank">悉尼</a>
								<a href="new_search.html?s=489-0-55-485-0-0-1-0" target="_blank">墨尔本</a>
								<a href="new_search.html?s=489-0-55-911-0-0-1-0" target="_blank">黄金海岸</a>
								<a href="new_search.html?s=489-0-56-503-0-0-1-0" target="_blank">奥克兰</a>
								<a href="new_search.html?s=489-0-56-504-0-0-1-0" target="_blank">皇后镇</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=奥克兰动物园" target="_blank">奥克兰动物园</a>
								<a href="new_search.html?key=大堡礁" target="_blank">大堡礁</a>
								<a href="new_search.html?key=悉尼歌剧院" target="_blank">悉尼歌剧院</a>
							</dd>
						</dl>
					</div>
					<div class="left_detail">
						<h3>中东非</h3>
						<dl class="detailother_list">
							<dt>游玩方式</dt>
							<dd>
								<a href="new_search.html?s=493-1-0-0-0-0-1-0" target="_blank">跟团游</a>
								<a href="new_search.html?s=493-2-0-0-0-0-1-0" target="_blank">自由行</a>
								<a href="new_search.html?s=493-3-0-0-0-0-1-0" target="_blank">当地游</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门目的地</dt>
							<dd>
								<a href="new_search.html?s=493-0-66-0-0-0-1-0" target="_blank">毛里求斯</a>
								<a href="new_search.html?s=493-0-33-0-0-0-1-0" target="_blank">土耳其</a>
								<a href="new_search.html?s=493-0-87-251-0-0-1-0" target="_blank">迪拜</a>
								<a href="new_search.html?s=493-0-90-0-0-0-1-0" target="_blank">约旦</a>
								<a href="new_search.html?s=493-0-64-0-0-0-1-0" target="_blank">南非</a>
								<a href="new_search.html?s=493-0-62-0-0-0-1-0" target="_blank">埃及</a>
							</dd>
						</dl>
						<dl class="detailother_list">
							<dt>热门景点</dt>
							<dd>
								<a href="new_search.html?key=马赛马拉国家公园" target="_blank">马赛马拉国家公园</a>
								<a href="new_search.html?key=好望角" target="_blank">好望角</a>
								<a href="new_search.html?key=鹿岛" target="_blank">鹿岛</a>
								<a href="new_search.html?key=死海" target="_blank">死海</a>
							</dd>
						</dl>
					</div>
				</div>
				<!--邮轮-->
				<div class="side_jmp near" style="top:256px;">
					<div class="left_detail">
						<dl class="detail_list">
							<dt>邮轮航线</dt>
							<dd>
								<a href="new_search.html?s=1063-0-0-0-0-0-1-0" target="_blank">日韩航线</a>
								<a href="new_search.html?s=1066-0-0-0-0-0-1-0" target="_blank">欧美航线</a>
								<a href="new_search.html?s=1065-0-0-0-0-0-1-0" target="_blank">东南亚航线</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>邮轮公司</dt>
							<dd>
								<a href="new_search.html?key=歌诗达" target="_blank">歌诗达邮轮</a>
								<a href="new_search.html?key=量子号" target="_blank">量子号邮轮</a>
								<a href="new_search.html?key=皇家加勒比" target="_blank">皇家加勒比</a>
								<a href="new_search.html?key=千禧号" target="_blank">千禧号邮轮</a>
								<a href="new_search.html?key=维多利亚邮轮" target="_blank">维多利亚邮轮</a>
								<a href="new_search.html?key=海洋水手号" target="_blank">海洋水手号邮轮</a>
							</dd>
						</dl>
						<dl class="detail_list">
							<dt>热门线路</dt>
							<dd>
<!--								<a href="new_search.html?s=" target="_blank">杭州</a>
								<a href="new_search.html?s=" target="_blank">苏州</a>
								<a href="new_search.html?s=" target="_blank">无锡</a>
								<a href="new_search.html?s=" target="_blank">宁波</a>
								<a href="new_search.html?s=" target="_blank" class="org">西塘</a>
								<a href="new_search.html?s=" target="_blank">乌镇</a>
								<a href="new_search.html?s=" target="_blank">舟山</a>
								<a href="new_search.html?s=" target="_blank">温州</a>
								<a href="new_search.html?s=" target="_blank">安吉</a>
								<a href="new_search.html?s=" target="_blank">绍兴</a>
								<a href="new_search.html?s=" target="_blank">普陀山</a>
								<a href="new_search.html?s=" target="_blank">镇江</a>
								<a href="new_search.html?s=" target="_blank">台州</a>
								<a href="new_search.html?s=" target="_blank">横店</a>
								<a href="new_search.html?s=" target="_blank">象山</a>
								<a href="new_search.html?s=" target="_blank">崇明</a>
								<a href="new_search.html?s=" target="_blank">阳澄湖</a>
-->							</dd>
						</dl>
					</div>
				</div>
			</div>
			
