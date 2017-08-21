<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TravelOnline.Destination.index" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>旅游目的地 - <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle%></title>
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/css/ddlevelsmenu-base.css" />
    <link rel="stylesheet" type="text/css" href="/css/ddlevelsmenu-sidebar.css" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <script type="text/javascript" src="/js/ddlevelsmenu.js"></script>
    <style type="text/css">

    </style>
    <script type="text/javascript">
        if (screen.width >= 1280) {
            //自定义宽屏css
            document.write("<style type='text/css'>.content {width: 1170px;}</style>");
        }
        //顶部菜单栏示例
        ddlevelsmenu.setup("ddtopmenubar", "topbar")
        //侧面菜单栏示例
        ddlevelsmenu.setup("ddsidemenubar", "sidebar")

        function go(num) {
            for (var id = 1; id <= 2; id++) {
                var tit = "t0" + id;
                var titsub = "sub_t0" + id;
                if (id == num) {
                    document.getElementById(tit).className = "titb";
                    document.getElementById(titsub).className = "sub_content1";
                    //document.getElementById(titsub).style.display="block";
                }
                else {
                    document.getElementById(tit).className = "tita";
                    document.getElementById(titsub).className = "sub_content2";
                    //document.getElementById(titsub).style.display="none";
                }
            }
        }
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<div id="menu">
    <div class="container" >
        <div class="row">
            <div class="span12" style="background:#01AA07;">
                <uc4:menu ID="menu1" runat="server" />
                <uc3:index_destination ID="index_destination1" runat="server" />
            </div>
        </div>
    </div>
</div>

<%--导航面包屑 begin--%>
<div class="container" >
    <div class="row">
        <div class="span12">
            <ul class="breadcrumb">
                <li><a href="/">首页</a> <span class="divider">/</span></li>
                <li class="active">旅游目的地</li>
            </ul>
        </div>
    </div>
</div>
<%--导航面包屑 end--%>

<%--一列式样 begin--%>
<div class="container" style="margin-bottom: 10px;">
	<div class="row">
		<div class="span12">
            
<div class="bgwap">
<div class="content">
  <div class="side">
    <h3>旅行目的地</h3>
    <div id="ddsidemenubar" class="markermenu">
      <ul>
        <li><a href="/summary/2.html" rel="ddsubmenuside1">国内</a></li>
        <li><a href="/summary/3.html" rel="ddsubmenuside2">亚洲</a></li>
        <li><a href="/summary/4.html" rel="ddsubmenuside3">欧洲</a></li>
        <li><a href="/summary/5.html" rel="ddsubmenuside4">北美洲</a></li>
        <li><a href="/summary/5.html" rel="ddsubmenuside7">南美洲</a></li>
        <li><a href="/summary/7.html" rel="ddsubmenuside5">大洋洲</a></li>
        <li><a href="javascript:" rel="ddsubmenuside6">其他洲</a></li>
      </ul>
    </div>
    <ul id="ddsubmenuside1" class="ddsubmenustyle">
      <li> <strong>华东</strong> <a href="/summary/649.html">南京</a><a href="/summary/652.html">无锡</a><a href="/summary/656.html">杭州</a><a href="/summary/139.html">安徽</a><a href="/summary/673.html">厦门</a><a href="/summary/668.html">景德镇</a><a href="/summary/670.html">井冈山</a></li>
      <li> <strong>西南</strong> <a href="/summary/739.html">丽江</a><a href="/summary/568.html">桂林</a><a href="/summary/570.html">南宁</a><a href="/summary/732.html">拉萨</a><a href="/summary/735.html">日喀则</a><a href="/summary/742.html">香格里拉</a></li>
      <li> <strong>西北</strong> <a href="/summary/693.html">太原</a><a href="/summary/695.html">大同</a><a href="/summary/697.html">敦煌</a><a href="/summary/708.html">成都</a><a href="/summary/832.html">乐山</a><a href="/summary/608.html">呼伦贝尔</a><a href="/summary/613.html">乌兰浩特</a><a href="/summary/685.html">西安</a><a href="/summary/617.html">乌鲁木齐</a> </li>
      <li> <strong>华北</strong> <a href="/summary/13.html">北京</a><a href="/summary/693.html">太原</a><a href="/summary/594.html">洛阳</a><a href="/summary/595.html">开封</a><a href="/summary/641.html">青岛</a><a href="/summary/637.html">大连</a><a href="/summary/638.html">沈阳</a><a href="/summary/631.html">长白山</a><a href="/summary/625.html">哈尔滨</a> </li>
      <li> <strong>华南</strong> <a href="/summary/553.html">广州</a><a href="/summary/555.html">深圳</a><a href="/summary/725.html">三亚</a><a href="/summary/726.html">海口</a><a href="/summary/836.html">西沙群岛</a></li>
      <li> <strong>华中</strong> <a href="/summary/583.html">长沙</a><a href="/summary/582.html">张家界</a><a href="/summary/589.html">武汉</a><a href="/summary/861.html">神农架</a><a href="/summary/830.html">丰都鬼城</a><a href="/summary/705.html">遵义</a><a href="/summary/706.html">安顺</a></li>
      <li> <strong>港澳台</strong> <a href="/summary/110.html">香港</a><a href="/summary/111.html">澳门</a><a href="/summary/712.html">台北</a><a href="/summary/713.html">垦丁</a><a href="/summary/721.html">台南</a> </li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside2" class="ddsubmenustyle">
      <li> <strong>东南亚</strong> <a href="/summary/16.html" rel="nofollow">泰国</a><a href="/summary/17.html" rel="nofollow">马来西亚</a><a href="/summary/18.html" rel="nofollow">新加坡</a><a href="/summary/19.html" rel="nofollow">越南</a><a href="/summary/20.html" rel="nofollow">柬埔寨</a><a href="/summary/25.html" rel="nofollow">菲律宾</a> </li>
      <li> <strong>南亚</strong> <a href="/summary/24.html" rel="nofollow">马尔代夫</a><a href="/summary/21.html" rel="nofollow">尼泊尔</a><a href="/summary/83.html" rel="nofollow">印度</a><a href="/summary/82.html" rel="nofollow">斯里兰卡</a><a href="/summary/84.html" rel="nofollow">不丹</a> </li>
      <li> <strong>日韩</strong> <a href="/summary/22.html" rel="nofollow">日本</a><a href="/summary/23.html" rel="nofollow">韩国</a></li>
      <li> <strong>中东</strong> <a href="/summary/87.html" rel="nofollow">阿联酋</a><a href="/summary/88.html" rel="nofollow">以色列</a><a href="/summary/89.html" rel="nofollow">伊朗</a><a href="/summary/90.html" rel="nofollow">约旦</a><a href="/summary/91.html" rel="nofollow">沙特阿拉伯</a> </li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside3" class="ddsubmenustyle">
      <li> <strong>西欧</strong> <a href="/summary/15.html" rel="nofollow">法国</a><a href="/summary/30.html" rel="nofollow">英国</a><a href="/summary/31.html" rel="nofollow">荷兰</a><a href="/summary/802.html" rel="nofollow">比利时</a><a href="/summary/92.html" rel="nofollow">卢森堡</a> </li>
      <li> <strong>南欧</strong> <a href="/summary/27.html" rel="nofollow">意大利</a><a href="/summary/29.html" rel="nofollow">西班牙</a><a href="/summary/32.html" rel="nofollow">希腊</a><a href="/summary/93.html" rel="nofollow">葡萄牙</a><a href="/summary/94.html" rel="nofollow">梵蒂冈</a> </li>
      <li> <strong>中欧</strong> <a href="/summary/28.html" rel="nofollow">德国</a><a href="/summary/95.html" rel="nofollow">奥地利</a><a href="/summary/96.html" rel="nofollow">捷克</a><a href="/summary/34.html" rel="nofollow">瑞士</a><a href="/summary/97.html" rel="nofollow">匈牙利</a><a href="/summary/98.html" rel="nofollow">波兰</a> </li>
      <li> <strong>北欧</strong> <a href="/summary/99.html" rel="nofollow">丹麦</a><a href="/summary/35.html" rel="nofollow">挪威</a><a href="/summary/100.html" rel="nofollow">瑞典</a><a href="/summary/101.html" rel="nofollow">芬兰</a><a href="/summary/102.html" rel="nofollow">冰岛</a> </li>
      <li> <strong>东欧</strong> <a href="/summary/36.html" rel="nofollow">俄罗斯</a><a href="/summary/33.html" rel="nofollow">土耳其</a><a href="/summary/103.html" rel="nofollow">乌克兰</a><a href="/summary/104.html" rel="nofollow">立陶宛</a><a href="/summary/105.html" rel="nofollow">白俄罗斯</a><a href="/summary/106.html" rel="nofollow">拉脱维亚</a> </li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside4" class="ddsubmenustyle">
      <li> <strong>美国</strong> <a href="/summary/407.html" rel="nofollow">纽约</a><a href="/summary/411.html" rel="nofollow">洛杉矶</a><a href="/summary/408.html" rel="nofollow">旧金山</a><a href="/summary/414.html" rel="nofollow">夏威夷</a><a href="/summary/413.html" rel="nofollow">拉斯维加斯</a><a href="/summary/412.html" rel="nofollow">华盛顿</a> </li>
      <li> <strong>加拿大</strong> <a href="/summary/432.html" rel="nofollow">温哥华</a><a href="/summary/433.html" rel="nofollow">多伦多</a><a href="/summary/434.html" rel="nofollow">蒙特利尔</a><a href="/summary/921.html" rel="nofollow">班夫</a><a href="/summary/435.html" rel="nofollow">渥太华</a> </li>
      <li> <strong>其他</strong> <a href="/summary/41.html" rel="nofollow">古巴</a><a href="/summary/42.html" rel="nofollow">墨西哥</a><a href="/summary/43.html" rel="nofollow">哥斯达黎加</a><a href="/summary/44.html" rel="nofollow">巴哈马</a><a href="/summary/45.html" rel="nofollow">牙买加</a><a href="/summary/46.html" rel="nofollow">波多黎各</a> </li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside7" class="ddsubmenustyle">
      <li> <strong>巴西</strong> <a href="/summary/460.html" rel="nofollow">里约热内卢</a><a href="/summary/462.html" rel="nofollow">圣保罗</a><a href="/summary/463.html" rel="nofollow">伊瓜苏</a><a href="/summary/465.html" rel="nofollow">巴伊亚</a></li>
      <li> <strong>智利</strong> <a href="/summary/472.html" rel="nofollow">复活节岛</a><a href="/summary/473.html" rel="nofollow">圣地亚哥</a><a href="/summary/474.html" rel="nofollow">康塞普西翁</a><a href="/summary/476.html" rel="nofollow">阿里卡</a></li>
      <li> <strong>其他</strong> <a href="/summary/48.html" rel="nofollow">阿根廷</a><a href="/summary/49.html" rel="nofollow">秘鲁</a><a href="/summary/51.html" rel="nofollow">玻利维亚</a><a href="/summary/52.html" rel="nofollow">哥伦比亚</a><a href="/summary/53.html" rel="nofollow">委内瑞拉</a><a href="/summary/54.html" rel="nofollow">厄瓜多尔</a><a href="/summary/767.html" rel="nofollow">乌拉圭</a> </li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside5" class="ddsubmenustyle">
      <li> <strong>澳大利亚</strong> <a href="/summary/486.html" rel="nofollow">悉尼</a><a href="/summary/485.html" rel="nofollow">墨尔本</a><a href="/summary/911.html" rel="nofollow">黄金海岸</a><a href="/summary/488.html" rel="nofollow">凯恩斯</a><a href="/summary/498.html" rel="nofollow">塔斯马尼亚</a><a href="/summary/809.html" rel="nofollow">布里斯本</a> </li>
      <li> <strong>新西兰</strong> <a href="/summary/56.html" rel="nofollow">新西兰</a><a href="/summary/503.html" rel="nofollow">奥克兰</a><a href="/summary/504.html" rel="nofollow">皇后镇</a><a href="/summary/506.html" rel="nofollow">基督城</a><a href="/summary/505.html" rel="nofollow">惠灵顿</a><a href="/summary/510.html" rel="nofollow">西海岸</a> </li>
      <li> <strong>其他</strong> <a href="/summary/57.html" rel="nofollow">斐济</a><a href="/summary/58.html" rel="nofollow">帕劳</a><a href="/summary/59.html" rel="nofollow">大溪地</a></li>
    </ul>
    <!--ddsubmenustyle end-->
    
    <ul id="ddsubmenuside6" class="ddsubmenustyle">
      <li> <strong>非洲</strong> <a href="/summary/62.html" rel="nofollow">埃及</a><a href="/summary/63.html" rel="nofollow">肯尼亚</a><a href="/summary/64.html" rel="nofollow">南非</a><a href="/summary/65.html" rel="nofollow">摩洛哥</a><a href="/summary/66.html" rel="nofollow">毛里求斯</a><a href="/summary/67.html" rel="nofollow">塞舌尔</a><a href="/summary/68.html" rel="nofollow">坦桑尼亚</a><a href="/summary/69.html" rel="nofollow">马达加斯加</a><a href="/summary/813.html" rel="nofollow">突尼斯</a> </li>
      <li>
        <strong>南极洲</strong> <a href="/summary/70.html" rel="nofollow">南极</a> </li>
    </ul>
    <!--ddsubmenustyle end--> 
    
    
  </div>
  <div class="map">
    <div class="tit">
      <ul>
        <li id="t01" class="titb" onClick="go(1)" style="border-radius:5px 0 0 5px">出境</li>
        <li id="t02" class="tita" onClick="go(2)" style="border-radius:0 5px 5px 0">国内</li>
      </ul>
      <div class="clear"></div>
    </div>
    <div id="sub_t01" class="sub_content1"> 
      <div class="map-world">
        
        <div class="place SouthAmerica"> <span>南美洲</span>
          <div class="detail_map"> <h2><a href="/summary/6.html" target="_blank">南美洲</a></h2><a href="/summary/47.html" target="_blank">巴西</a><a href="/summary/49.html" target="_blank">秘鲁</a><a href="/summary/50.html" target="_blank">智利</a><a href="/summary/48.html" target="_blank">阿根廷</a><a href="/summary/767.html" target="_blank">乌拉圭</a><a href="/summary/51.html" target="_blank">玻利维亚</a><a href="/summary/52.html" target="_blank">哥伦比亚</a><a href="/summary/53.html" target="_blank">委内瑞拉</a><a href="/summary/54.html" target="_blank">厄瓜多尔</a></div>
        </div>
        <div class="place America"> <span>北美洲</span>
          <div class="detail_map"> <h2><a href="/summary/5.html" target="_blank">北美洲</a></h2><a href="/summary/37.html" target="_blank">美国</a><a href="/summary/41.html" target="_blank">古巴</a><a href="/summary/765.html" target="_blank">海地</a><a href="/summary/38.html" target="_blank">加拿大</a><a href="/summary/42.html" target="_blank">墨西哥</a><a href="/summary/44.html" target="_blank">巴哈马</a><a href="/summary/45.html" target="_blank">牙买加</a><a href="/summary/46.html" target="_blank">波多黎各</a><a href="/summary/43.html" target="_blank">哥斯达黎加</a></div>
        </div>
        <div class="place Oceania"> <span>大洋洲</span>
          <div class="detail_map"> <h2><a href="/summary/7.html" target="_blank">大洋洲</a></h2><a href="/summary/57.html" target="_blank">斐济</a><a href="/summary/58.html" target="_blank">帕劳</a><a href="/summary/56.html" target="_blank">新西兰</a><a href="/summary/59.html" target="_blank">大溪地</a><a href="/summary/55.html" target="_blank">澳大利亚</a></div>
        </div>
        <div class="place Africa"> <span>非洲</span>
          <div class="detail_map"> <h2><a href="/summary/8.html" target="_blank">非洲</a></h2><a href="/summary/62.html" target="_blank">埃及</a><a href="/summary/64.html" target="_blank">南非</a><a href="/summary/63.html" target="_blank">肯尼亚</a><a href="/summary/65.html" target="_blank">摩洛哥</a><a href="/summary/67.html" target="_blank">塞舌尔</a><a href="/summary/813.html" target="_blank">突尼斯</a><a href="/summary/66.html" target="_blank">毛里求斯</a><a href="/summary/68.html" target="_blank">坦桑尼亚</a><a href="/summary/69.html" target="_blank">马达加斯加</a></div>
        </div>
        <div class="place Asia"> <span>亚洲</span>
          <div class="detail_map"> <h2><a href="/summary/3.html" target="_blank">亚洲</a></h2><a href="/summary/16.html" target="_blank">泰国</a><a href="/summary/19.html" target="_blank">越南</a><a href="/summary/22.html" target="_blank">日本</a><a href="/summary/23.html" target="_blank">韩国</a><a href="/summary/83.html" target="_blank">印度</a><a href="/summary/84.html" target="_blank">不丹</a><a href="/summary/85.html" target="_blank">蒙古</a><a href="/summary/86.html" target="_blank">朝鲜</a><a href="/summary/89.html" target="_blank">伊朗</a><a href="/summary/90.html" target="_blank">约旦</a><a href="/summary/779.html" target="_blank">文莱</a><a href="/summary/881.html" target="_blank">老挝</a><a href="/summary/882.html" target="_blank">缅甸</a><a href="/summary/908.html" target="_blank">塞班</a><a href="/summary/25.html" target="_blank">菲律宾</a><a href="/summary/18.html" target="_blank">新加坡</a><a href="/summary/20.html" target="_blank">柬埔寨</a><a href="/summary/21.html" target="_blank">尼泊尔</a><a href="/summary/87.html" target="_blank">阿联酋</a><a href="/summary/88.html" target="_blank">以色列</a><a href="/summary/24.html" target="_blank">马尔代夫</a><a href="/summary/17.html" target="_blank">马来西亚</a><a href="/summary/82.html" target="_blank">斯里兰卡</a><a href="/summary/26.html" target="_blank">印度尼西亚</a><a href="/summary/91.html" target="_blank">沙特阿拉伯</a></div>
        </div>
        <div class="place Europe"> <span>欧洲</span>
          <div class="detail_map"> <h2><a href="/summary/4.html" target="_blank">欧洲</a></h2><a href="/summary/15.html" target="_blank">法国</a><a href="/summary/28.html" target="_blank">德国</a><a href="/summary/30.html" target="_blank">英国</a><a href="/summary/31.html" target="_blank">荷兰</a><a href="/summary/32.html" target="_blank">希腊</a><a href="/summary/34.html" target="_blank">瑞士</a><a href="/summary/35.html" target="_blank">挪威</a><a href="/summary/96.html" target="_blank">捷克</a><a href="/summary/98.html" target="_blank">波兰</a><a href="/summary/99.html" target="_blank">丹麦</a><a href="/summary/100.html" target="_blank">瑞典</a><a href="/summary/101.html" target="_blank">芬兰</a><a href="/summary/102.html" target="_blank">冰岛</a><a href="/summary/27.html" target="_blank">意大利</a><a href="/summary/29.html" target="_blank">西班牙</a><a href="/summary/33.html" target="_blank">土耳其</a><a href="/summary/36.html" target="_blank">俄罗斯</a><a href="/summary/92.html" target="_blank">卢森堡</a><a href="/summary/93.html" target="_blank">葡萄牙</a><a href="/summary/95.html" target="_blank">奥地利</a><a href="/summary/97.html" target="_blank">匈牙利</a><a href="/summary/94.html" target="_blank">梵蒂冈</a><a href="/summary/103.html" target="_blank">乌克兰</a><a href="/summary/104.html" target="_blank">立陶宛</a><a href="/summary/762.html" target="_blank">摩纳哥</a><a href="/summary/797.html" target="_blank">安道尔</a><a href="/summary/802.html" target="_blank">比利时</a><a href="/summary/105.html" target="_blank">白俄罗斯</a><a href="/summary/106.html" target="_blank">拉脱维亚</a><a href="/summary/796.html" target="_blank">斯洛伐克</a><a href="/summary/916.html" target="_blank">列支敦士登</a></div>
        </div>
      </div> 
    </div>
    <div id="sub_t02" class="sub_content2">
      <div class="map-china">
        <div class="place  hainan"> <span>海南</span>
          <div class="detail_map"> <h2><a href="/summary/116.html" target="_blank">海南</a></h2><a href="/summary/725.html" target="_blank">三亚</a><a href="/summary/726.html" target="_blank">海口</a><a href="/summary/728.html" target="_blank">万宁</a><a href="/summary/729.html" target="_blank">琼海</a><a href="/summary/730.html" target="_blank">博鳌</a><a href="/summary/731.html" target="_blank">文昌</a><a href="/summary/836.html" target="_blank">西沙群岛</a><a href="/summary/727.html" target="_blank">五指山市</a></div>
        </div>
        <div class="place  aomen"> <span>澳门</span>
          <div class="detail_map"><h2><a href="/summary/111.html" target="_blank">澳门</a></h2></div>
        </div>
        <div class="place  xianggang"> <span>香港</span>
          <div class="detail_map"><h2><a href="/summary/110.html" target="_blank">香港</a></h2></div>
        </div>
        <div class="place  taiwan"> <span>台湾</span>
          <div class="detail_map"> <h2><a href="/summary/112.html" target="_blank">台湾</a></h2><a href="/summary/712.html" target="_blank">台北</a><a href="/summary/713.html" target="_blank">垦丁</a><a href="/summary/714.html" target="_blank">高雄</a><a href="/summary/715.html" target="_blank">桃园</a><a href="/summary/716.html" target="_blank">基隆</a><a href="/summary/721.html" target="_blank">台南</a><a href="/summary/718.html" target="_blank">宜兰</a><a href="/summary/719.html" target="_blank">澎湖</a><a href="/summary/720.html" target="_blank">嘉义</a><a href="/summary/724.html" target="_blank">南投</a><a href="/summary/723.html" target="_blank">彰化</a><a href="/summary/758.html" target="_blank">台中</a><a href="/summary/804.html" target="_blank">花莲</a><a href="/summary/805.html" target="_blank">台东</a></div>
          </div>
        <div class="place  guangdong"> <span>广东</span>
          <div class="detail_map"> <h2><a href="/summary/125.html" target="_blank">广东</a></h2><a href="/summary/553.html" target="_blank">广州</a><a href="/summary/554.html" target="_blank">珠海</a><a href="/summary/555.html" target="_blank">深圳</a><a href="/summary/557.html" target="_blank">肇庆</a><a href="/summary/558.html" target="_blank">东莞</a><a href="/summary/559.html" target="_blank">江门</a><a href="/summary/560.html" target="_blank">惠州</a><a href="/summary/561.html" target="_blank">佛山</a><a href="/summary/562.html" target="_blank">汕头</a><a href="/summary/563.html" target="_blank">湛江</a><a href="/summary/564.html" target="_blank">韶关</a><a href="/summary/565.html" target="_blank">中山</a><a href="/summary/566.html" target="_blank">潮州</a><a href="/summary/567.html" target="_blank">清远</a></div>
        </div>
        <div class="place  guangxi"> <span>广西</span>
          <div class="detail_map"> <h2><a href="/summary/124.html" target="_blank">广西</a></h2><a href="/summary/568.html" target="_blank">桂林</a><a href="/summary/569.html" target="_blank">北海</a><a href="/summary/570.html" target="_blank">南宁</a><a href="/summary/571.html" target="_blank">阳朔</a><a href="/summary/572.html" target="_blank">柳州</a><a href="/summary/573.html" target="_blank">钦州</a><a href="/summary/574.html" target="_blank">百色</a><a href="/summary/575.html" target="_blank">梧州</a></div>
        </div>
        <div class="place  yunnan"> <span>云南</span>
          <div class="detail_map"> <h2><a href="/summary/81.html" target="_blank">云南</a></h2><a href="/summary/739.html" target="_blank">丽江</a><a href="/summary/740.html" target="_blank">昆明</a><a href="/summary/741.html" target="_blank">大理</a><a href="/summary/744.html" target="_blank">瑞丽</a><a href="/summary/745.html" target="_blank">腾冲</a><a href="/summary/746.html" target="_blank">楚雄</a><a href="/summary/747.html" target="_blank">普洱</a><a href="/summary/875.html" target="_blank">泸沽湖</a><a href="/summary/742.html" target="_blank">香格里拉</a><a href="/summary/743.html" target="_blank">西双版纳</a></div>
        </div>
        <div class="place  fujian"> <span>福建</span>
          <div class="detail_map"> <h2><a href="/summary/141.html" target="_blank">福建</a></h2><a href="/summary/673.html" target="_blank">厦门</a><a href="/summary/674.html" target="_blank">福州</a><a href="/summary/675.html" target="_blank">莆田</a><a href="/summary/676.html" target="_blank">泉州</a><a href="/summary/677.html" target="_blank">龙岩</a><a href="/summary/678.html" target="_blank">晋江</a><a href="/summary/679.html" target="_blank">惠安</a><a href="/summary/680.html" target="_blank">宁德</a><a href="/summary/681.html" target="_blank">南平</a><a href="/summary/682.html" target="_blank">永定</a><a href="/summary/684.html" target="_blank">漳州</a></div>
        </div>
        <div class="place  guizhou"> <span>贵州</span>
          <div class="detail_map"> <h2><a href="/summary/373.html" target="_blank">贵州</a></h2><a href="/summary/704.html" target="_blank">贵阳</a><a href="/summary/705.html" target="_blank">遵义</a><a href="/summary/706.html" target="_blank">安顺</a><a href="/summary/707.html" target="_blank">铜仁</a></div>
        </div>
        <div class="place  hunan"> <span>湖南</span>
          <div class="detail_map"> <h2><a href="/summary/127.html" target="_blank">湖南</a></h2><a href="/summary/583.html" target="_blank">长沙</a><a href="/summary/584.html" target="_blank">岳阳</a><a href="/summary/585.html" target="_blank">常德</a><a href="/summary/586.html" target="_blank">永州</a><a href="/summary/587.html" target="_blank">湘西</a><a href="/summary/588.html" target="_blank">怀化</a><a href="/summary/582.html" target="_blank">张家界</a></div>
        </div>
        <div class="place  jiangxi"> <span>江西</span>
          <div class="detail_map"> <h2><a href="/summary/140.html" target="_blank">江西</a></h2><a href="/summary/667.html" target="_blank">南昌</a><a href="/summary/669.html" target="_blank">九江</a><a href="/summary/671.html" target="_blank">赣州</a><a href="/summary/672.html" target="_blank">上饶</a><a href="/summary/668.html" target="_blank">景德镇</a><a href="/summary/670.html" target="_blank">井冈山</a></div>
        </div>
        <div class="place  zhejiang"> <span>浙江</span>
          <div class="detail_map"> <h2><a href="/summary/138.html" target="_blank">浙江</a></h2><a href="/summary/656.html" target="_blank">杭州</a><a href="/summary/657.html" target="_blank">宁波</a><a href="/summary/658.html" target="_blank">台州</a><a href="/summary/659.html" target="_blank">嘉兴</a><a href="/summary/660.html" target="_blank">绍兴</a><a href="/summary/661.html" target="_blank">临安</a><a href="/summary/662.html" target="_blank">安吉</a><a href="/summary/820.html" target="_blank">舟山</a><a href="/summary/826.html" target="_blank">温州</a><a href="/summary/839.html" target="_blank">嘉善</a><a href="/summary/845.html" target="_blank">桐乡</a><a href="/summary/883.html" target="_blank">金华</a><a href="/summary/884.html" target="_blank">衢州</a><a href="/summary/885.html" target="_blank">丽水</a><a href="/summary/851.html" target="_blank">德清县</a></div>
        </div>
        <div class="place  chongqing"> <span>重庆</span>
          <div class="detail_map"> <h2><a href="/summary/113.html" target="_blank">重庆</a></h2><a href="/summary/859.html" target="_blank">武隆</a><a href="/summary/830.html" target="_blank">丰都鬼城</a></div>
        </div>
        <div class="place  sichuan"> <span>四川</span>
          <div class="detail_map"> <h2><a href="/summary/374.html" target="_blank">四川</a></h2><a href="/summary/708.html" target="_blank">成都</a><a href="/summary/711.html" target="_blank">黄龙</a><a href="/summary/832.html" target="_blank">乐山</a><a href="/summary/710.html" target="_blank">攀枝花</a><a href="/summary/831.html" target="_blank">九寨沟</a><a href="/summary/833.html" target="_blank">峨眉山</a><a href="/summary/709.html" target="_blank">稻城亚丁</a></div>
        </div>
        <div class="place  hubei"> <span>湖北</span>
          <div class="detail_map"> <h2><a href="/summary/128.html" target="_blank">湖北</a></h2><a href="/summary/589.html" target="_blank">武汉</a> <a href="/summary/590.html" target="_blank">宜昌</a><a href="/summary/591.html" target="_blank">襄阳</a><a href="/summary/592.html" target="_blank">赤壁</a><a href="/summary/593.html" target="_blank">荆州</a><a href="/summary/861.html" target="_blank">神农架</a><a href="/summary/863.html" target="_blank">十堰市</a></div>
        </div>
        <div class="place  anhui"> <span>安徽</span>
          <div class="detail_map"> <h2><a href="/summary/139.html" target="_blank">安徽</a></h2><a href="/summary/663.html" target="_blank">合肥</a><a href="/summary/664.html" target="_blank">芜湖</a><a href="/summary/665.html" target="_blank">黄山</a><a href="/summary/843.html" target="_blank">池州</a><a href="/summary/919.html" target="_blank">徽州</a><a href="/summary/666.html" target="_blank">马鞍山</a><a href="/summary/838.html" target="_blank">齐云山</a></div>
        </div>
        <div class="place  xizhang"> <span>西藏</span>
          <div class="detail_map"> <h2><a href="/summary/122.html" target="_blank">西藏</a></h2><a href="/summary/732.html" target="_blank">拉萨</a><a href="/summary/734.html" target="_blank">林芝</a><a href="/summary/736.html" target="_blank">芒康</a><a href="/summary/737.html" target="_blank">阿里</a><a href="/summary/778.html" target="_blank">江孜</a><a href="/summary/733.html" target="_blank">纳木错</a><a href="/summary/735.html" target="_blank">日喀则</a></div>
        </div>
        <div class="place  shanghai"> <span>上海</span>
          <div class="detail_map"> <h2><a href="/summary/14.html" target="_blank">上海</a></h2></div>
           </div>
           <div class="place  jiangsu"> <span>江苏</span>
          <div class="detail_map"> <h2><a href="/summary/137.html" target="_blank">江苏</a></h2><a href="/summary/649.html" target="_blank">南京</a><a href="/summary/650.html" target="_blank">苏州</a><a href="/summary/651.html" target="_blank">扬州</a><a href="/summary/652.html" target="_blank">无锡</a><a href="/summary/654.html" target="_blank">常州</a><a href="/summary/655.html" target="_blank">镇江</a><a href="/summary/849.html" target="_blank">泰州</a><a href="/summary/854.html" target="_blank">溧阳</a><a href="/summary/653.html" target="_blank">连云港</a></div>
        </div>
          <div class="place  henan"> <span>河南</span>
          <div class="detail_map"> <h2><a href="/summary/129.html" target="_blank">河南</a></h2><a href="/summary/594.html" target="_blank">洛阳</a><a href="/summary/595.html" target="_blank">开封</a><a href="/summary/596.html" target="_blank">郑州</a><a href="/summary/597.html" target="_blank">南阳</a><a href="/summary/598.html" target="_blank">周口</a><a href="/summary/599.html" target="_blank">安阳</a><a href="/summary/600.html" target="_blank">商丘</a><a href="/summary/865.html" target="_blank">焦作</a><a href="/summary/870.html" target="_blank">登封</a><a href="/summary/601.html" target="_blank">平顶山</a></div>
        </div> 
         <div class="place  shanxi"> <span>陕西</span>
          <div class="detail_map"> <h2><a href="/summary/142.html" target="_blank">陕西</a></h2><a href="/summary/685.html" target="_blank">西安</a><a href="/summary/686.html" target="_blank">延安</a><a href="/summary/687.html" target="_blank">咸阳</a><a href="/summary/688.html" target="_blank">宝鸡</a><a href="/summary/689.html" target="_blank">汉中</a><a href="/summary/690.html" target="_blank">榆林</a><a href="/summary/691.html" target="_blank">安康</a><a href="/summary/692.html" target="_blank">渭南</a></div>
        </div> 
        <div class="place  qinghai"> <span>青海</span>
          <div class="detail_map"> <h2><a href="/summary/126.html" target="_blank">青海</a></h2><a href="/summary/576.html" target="_blank">西宁</a><a href="/summary/579.html" target="_blank">玉树</a><a href="/summary/577.html" target="_blank">格尔木</a><a href="/summary/580.html" target="_blank">德令哈</a><a href="/summary/581.html" target="_blank">海西州</a><a href="/summary/578.html" target="_blank">可可西里</a></div>
        </div>
        <div class="place  shan_xi"> <span>山西</span>
          <div class="detail_map"> <h2><a href="/summary/143.html" target="_blank">山西</a></h2><a href="/summary/693.html" target="_blank">太原</a><a href="/summary/694.html" target="_blank">平遥</a><a href="/summary/695.html" target="_blank">大同</a><a href="/summary/738.html" target="_blank">晋中</a><a href="/summary/756.html" target="_blank">忻州</a></div>
        </div>
        <div class="place  shandong"> <span>山东</span>
          <div class="detail_map"> <h2><a href="/summary/136.html" target="_blank">山东</a></h2><a href="/summary/641.html" target="_blank">青岛</a><a href="/summary/642.html" target="_blank">济南</a><a href="/summary/643.html" target="_blank">日照</a><a href="/summary/644.html" target="_blank">烟台</a><a href="/summary/645.html" target="_blank">威海</a><a href="/summary/646.html" target="_blank">泰安</a><a href="/summary/647.html" target="_blank">淄博</a><a href="/summary/648.html" target="_blank">蓬莱</a><a href="/summary/867.html" target="_blank">曲阜</a><a href="/summary/868.html" target="_blank">泰安</a><a href="/summary/886.html" target="_blank">枣庄</a></div>
        </div>
        <div class="place  ningxia"> <span>宁夏</span>
          <div class="detail_map"> <h2><a href="/summary/372.html" target="_blank">宁夏</a></h2><a href="/summary/701.html" target="_blank">银川</a><a href="/summary/702.html" target="_blank">中卫</a><a href="/summary/703.html" target="_blank">固原</a></div>
        </div>
        <div class="place  hebei"> <span>河北</span>
          <div class="detail_map"> <h2><a href="/summary/130.html" target="_blank">河北</a></h2><a href="/summary/604.html" target="_blank">邯郸</a><a href="/summary/605.html" target="_blank">保定</a><a href="/summary/606.html" target="_blank">承德</a><a href="/summary/607.html" target="_blank">衡水</a><a href="/summary/602.html" target="_blank">秦皇岛</a><a href="/summary/603.html" target="_blank">石家庄</a></div>
        </div>
        <div class="place  tianjing"> <span>天津</span>
          <div class="detail_map"> <h2><a href="/summary/905.html" target="_blank">天津</a></h2></div>
        </div>
        <div class="place  beijing"> <span>北京</span>
          <div class="detail_map"> <h2><a href="/summary/13.html" target="_blank">北京</a></h2></div>
        </div>
        <div class="place  gansu"> <span>甘肃</span>
          <div class="detail_map"> <h2><a href="/summary/144.html" target="_blank">甘肃</a></h2><a href="/summary/696.html" target="_blank">兰州</a><a href="/summary/697.html" target="_blank">敦煌</a><a href="/summary/699.html" target="_blank">张掖</a><a href="/summary/700.html" target="_blank">酒泉</a><a href="/summary/698.html" target="_blank">嘉峪关</a></div>
        </div>
        <div class="place  nmg"> <span>内蒙古</span>
          <div class="detail_map"> <h2><a href="/summary/131.html" target="_blank">内蒙古</a></h2><a href="/summary/614.html" target="_blank">包头</a><a href="/summary/615.html" target="_blank">赤峰</a><a href="/summary/609.html" target="_blank">阿尔山</a><a href="/summary/616.html" target="_blank">海拉尔</a> <a href="/summary/608.html" target="_blank">呼伦贝尔</a><a href="/summary/610.html" target="_blank">呼和浩特</a><a href="/summary/611.html" target="_blank">鄂尔多斯</a><a href="/summary/612.html" target="_blank">乌兰察布</a><a href="/summary/613.html" target="_blank">乌兰浩特</a></div>
        </div>
        <div class="place xinjiang "> <span>新疆</span>
          <div class="detail_map"> <h2><a href="/summary/132.html" target="_blank">新疆</a></h2><a href="/summary/619.html" target="_blank">伊犁</a><a href="/summary/621.html" target="_blank">哈密</a><a href="/summary/622.html" target="_blank">喀什</a><a href="/summary/877.html" target="_blank">伊宁</a><a href="/summary/618.html" target="_blank">喀纳斯</a><a href="/summary/620.html" target="_blank">吐鲁番</a><a href="/summary/623.html" target="_blank">库尔勒</a><a href="/summary/624.html" target="_blank">阿克苏</a><a href="/summary/876.html" target="_blank">布尔津</a><a href="/summary/880.html" target="_blank">满洲里</a><a href="/summary/617.html" target="_blank">乌鲁木齐</a><a href="/summary/879.html" target="_blank">克拉玛依</a></div>
        </div>
        <div class="place  liaoning"> <span>辽宁</span>
          <div class="detail_map"> <h2><a href="/summary/135.html" target="_blank">辽宁</a></h2><a href="/summary/637.html" target="_blank">大连</a><a href="/summary/638.html" target="_blank">沈阳</a><a href="/summary/640.html" target="_blank">鞍山</a><a href="/summary/874.html" target="_blank">丹东</a><a href="/summary/639.html" target="_blank">葫芦岛</a></div>
        </div>
        <div class="place  jilin"> <span>吉林</span>
          <div class="detail_map"> <h2><a href="/summary/134.html" target="_blank">吉林</a></h2><a href="/summary/632.html" target="_blank">长春</a><a href="/summary/633.html" target="_blank">延吉</a><a href="/summary/634.html" target="_blank">延边</a><a href="/summary/635.html" target="_blank">四平</a><a href="/summary/636.html" target="_blank">通化</a><a href="/summary/631.html" target="_blank">长白山</a> </div>
        </div>
        <div class="place  hlj"> <span>黑龙江</span>
          <div class="detail_map"> <h2><a href="/summary/133.html" target="_blank">黑龙江</a></h2><a href="/summary/628.html" target="_blank">大庆</a><a href="/summary/629.html" target="_blank">漠河</a><a href="/summary/625.html" target="_blank">哈尔滨</a><a href="/summary/627.html" target="_blank">佳木斯</a><a href="/summary/626.html" target="_blank">齐齐哈尔</a><a href="/summary/630.html" target="_blank">大兴安岭</a></div>
        </div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
</div>



        </div>
	</div>
</div>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
    (function ($) {

        $(".place").hover(function () {
            var curr = $(this).attr("class");
            var place = curr.replace("place ", "");
            $(this).find(".detail_map").slideDown(200).show();


        }, function () {
            $(this).find(".detail_map").hide();


        }
	)


    })(jQuery);
</script>
</body>
</html>