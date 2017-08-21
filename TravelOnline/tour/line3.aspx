<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="line.aspx.cs" Inherits="TravelOnline.tour.line" %>
<%@ Register src="../NewMaster/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="../NewMaster/footer_20140822.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="../NewMaster/index_destination.ascx" tagname="index_destination" tagprefix="uc3" %>
<%@ Register src="../NewMaster/menu.ascx" tagname="menu" tagprefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=TitleString %> <% =TravelOnline.Class.Common.PublicPageKeyWords.PublicLineListTitle%></title>
    <meta name="description" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.OutBoundKeywords %> />
    <link href="/css/bootstrap.css" rel="stylesheet" media="screen" />
    <link href="/css/index.css" rel="stylesheet" />
    <link href="/css/line.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/js/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/js/jquery.idTabs.js"></script>
    <script type="text/javascript" src="/js/datePicker.js"></script>
    <script type="text/javascript" src="/js/jquery.nav.js"></script>
    <script type="text/javascript" src="/js/order.js"></script>
    <script type="text/javascript" src="/js/jquery.cookie.js"></script>
    <script type="text/javascript" src="/js/setcookie.js"></script>
    <script type="text/javascript" src="/js/myfocus-2.0.4.min.js"></script>
    <script type="text/javascript" src="/js/CruisesOrder.js"></script>
    <script type="text/javascript" src="/Scripts/jNotify.jquery.js"></script>
    <script type="text/javascript" src="/js/boot.page.js"></script>
    <style type="text/css">
    .CruisesDate1 {background-color:#5BB75B;color:#ffffff;TEXT-ALIGN: center;CURSOR: pointer;FONT-WEIGHT: normal; font-family: Arial Unicode MS;FONT-SIZE: 12px;WIDTH: 120px;HEIGHT: 60px;FLOAT: left;OVERFLOW: hidden; PADDING: 5px 5px 5px 5px;MARGIN-RIGHT: 6px}
    .CruisesDate1 .CruisesSpan{font-size: 16px; font-weight: bold}
    .CruisesDate1 .route_2 {COLOR: #ffffff;}
    
    .onsale_place{height:auto;width:100%;margin-left:0px;position:relative;overflow:hidden;border:0px solid #ffffff;border-width:0px 0px 0px 0px;}
    .onsale_place .product{ height:110px;padding:5px 5px;border:0px solid #ffffff;border-width:0px 0px 0px 0px;display:block;position:relative;}
    .onsale_place .product:hover{height:100px;padding:3px 3px;position:relative;text-decoration:none;border:2px solid #FFBE32;background-color:#fff;}
    .onsale_place .product .name_wrap{display:block;width:190px;height:32px;overflow:hidden;line-height:16px;}
    .onsale_place .product_zoomfix{float:left;overflow:hidden;width:150px;height:120px;border:0px solid #ffffff;margin:0 15px}
    .onsale_place .product img{width:140px;height:80px;float:left;}
    .onsale_place .product .price_box{background-color:#3B352C;height:20px;width:140px;display:block;overflow:hidden;margin-bottom:5px;}
    .route a{padding:0 2px;color: #F89406;text-decoration: none;}
    .route a:hover {*color: #468847;text-decoration:underline;}
    #linelist .view_list {margin: 10px 0px 5px 0px;overflow: hidden;height: 200px;}
    #linelist .view_list ul {margin-left: 0px;overflow: hidden;}
    #linelist .view_list li {margin: 0 12px 0px 0px;padding:4px 4px 4px 4px;width: 170px;height: 190px;overflow: hidden;float: left;border:1px solid #DDDDDD;}
    #linelist .view_list li:hover{position:relative;text-decoration:none;border:2px solid #FFBE32;padding:3px 3px 3px 3px;height:190px;z-index:2;background-color:#fff;}
    #linelist .view_list dl{padding-left:5px;padding-top: 0px;padding-bottom: 0px;}
    #linelist .view_list dt{width: 170px;padding:5px 0;font-size: 14px;font-family: Microsoft YaHei,Hiragino Sans GB;font-weight: bold;overflow: hidden;text-overflow: ellipsis;white-space: nowrap;}
    #linelist .view_list dd{color: #999999;width: 165px;padding-left: 0px;margin-top: 30px;}
    #linelist .view_list li img{width:170px;height:120px;}
    </style>
    <script type="text/javascript">
       <%=PlanDateJason%>
	</script>
    <script type="text/javascript">
       //var defaultStartDate = '2013-11-16'; var defaultEndDate = '2013-12-12'; var json = [{ 'planid': '207165', 'date': '2013-10-16', 'price': '4310', 'content': '报名截止', 'route': '324234' }, { 'planid': '207166', 'date': '2013-10-23', 'price': '4310', 'content': '已满', 'route': '423' }, { 'planid': '222919', 'date': '2013-11-06', 'price': '4480', 'content': '尚有余位', 'route': '0' }, { 'planid': '222920', 'date': '2013-11-13', 'price': '4480', 'content': '尚有余位', 'route': '0' }, { 'planid': '222922', 'date': '2013-11-27', 'price': '4480', 'content': '尚有余位', 'route': '0' }, { 'planid': '222922', 'date': '2013-12-11', 'price': '4480', 'content': '尚有余位', 'route': '0' }, { 'planid': '222922', 'date': '2013-11-29', 'price': '4480', 'content': '尚有余位', 'route': '0' }, { 'planid': '222922', 'date': '2014-02-12', 'price': '4480', 'content': '尚有余位', 'route': '0'}];
        if (screen.width >= 1280) {
            document.write("<style type='text/css'>.onsale_place .product_zoomfix{margin:0 4px}#linelist .view_list li {margin: 0 22px 0px 0px;}</style>");
        }
    </script>
</head>
<body>
<uc1:Header ID="Header1" runat="server" />
<%--<% =TravelOnline.Class.NewClass.TopMenu.GetTopMenuString("OutBound")%>--%>
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

<div class="topadback">
    <div class="container" >
	    <div class="row">
            <div class="span12">
                <ul class="breadcrumb"><%=BreadCrumb%></ul>
            </div>
		    <div class="span12">

                <div class="linetop" style="height:450px;background-color:#fff;border:1px solid #CCCCCC;">

                    <div style="height:430px;padding:10px;float:left;width:330px;border-right:0px solid #CCCCCC;">
                        <div id="xgalleryShow" class="xgallery_show">
                            <img class="xgallery_img" id="xgalleryImg" src="<%=FirstImg %>" onerror="this.src='/Images/none.gif'" alt="<%=LineName %>" />
                        </div>
                        <div class="xgallery_thumb" id="pic_small_wrapper">
                            <div class="xgallery_thumb_wrapper" >
                                <ul id="list_smallpic" class="xgallery_thumb_list clearfix"><%=PicString %>
                                    <%--<li class="current"><img src-org="" src="" alt="" onerror="this.src='/Images/none.gif'"></li>--%>
                                </ul>
                            </div>
                            <a class="xgallery_prev" href="javascript:"><i></i></a><a class="xgallery_next" href="javascript:"><i></i></a>
                        </div>
                        <div id="leftstring" style="padding: 20px 0 0 40px;margin-bottom: 0px;">
                            <div id="ckepop" style="">
                                <!-- Baidu Button BEGIN -->
                                <div id="bdshare" class="bdshare_t bds_tools get-codes-bdshare">
                                <span class="bds_more">分享到：</span>
                                <a class="bds_tsina"></a>
                                <a class="bds_qzone"></a>
                                <a class="bds_tqq"></a>
                                <a class="bds_renren"></a>
                                <a class="bds_t163"></a>
                                <a class="bds_kaixin001"></a>
                                <a class="shareCount"></a>
                                </div>
                                <script type="text/javascript" id="bdshare_js" data="type=tools&amp;uid=6867647" ></script>
                                <script type="text/javascript" id="bdshell_js"></script>
                                <script type="text/javascript">
                                    document.getElementById("bdshell_js").src = "http://bdimg.share.baidu.com/static/js/shell_v2.js?cdnversion=" + Math.ceil(new Date() / 3600000)
                                </script>
                                <!-- Baidu Button END -->
                            </div>
                            <%--<div id="ckepop" style="">
                                <span class="jiathis_txt">分享：</span>
                                <a class="jiathis_button_tsina"></a>
                                <a class="jiathis_button_tqq"></a>
                                <a class="jiathis_button_renren"></a>
                                <a class="jiathis_button_kaixin001"></a>
                                <a class="jiathis_button_douban"></a>
                                <a class="jiathis_button_qzone"></a>
                                <a href="http://www.jiathis.com/share" class="jiathis jiathis_txt jiathis_separator jtico jtico_jiathis" target="_blank"></a>
                                <iframe width="63" height="24" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0" scrolling="no" frameborder="No" border="0" src="http://widget.weibo.com/relationship/followbutton.php?width=63&height=24&uid=1823201272&style=1&btn=light&dpc=1"></iframe>
                            </div>
                            <script type="text/javascript" >
                                var jiathis_config = {
                                    hideMore: false
                                }
                            </script>
                            <script type="text/javascript" src="http://v2.jiathis.com/code/jia.js" charset="utf-8"></script>--%>
                        </div><br/>
                        <div style="padding: 20px 0 0 45px;margin-bottom: 10px;">
                            <a href="javascript:" onclick="AddToFavorite()" class="btn btn-success" style="margin-right: 20px;">收藏线路</a>
                            <a href="/routeprint/<%=id %>.html" target="_blank" class="btn btn-success <%=PrintHide %>">打印行程</a>
                        </div>
                    </div>
                    <div class="grid_m">
                        <div class="xbase">
                            <div class="xbase_row1"><h1 class="xname"><%=LineName%></h1><p class="xdesc"><%=LineFeature%></p></div>
                            <div class="xbase_row2">
                                <dl class="xbase_item xprice xprice_origin">
                                    <dt class="xbase_col1">线路编号</dt>
                                    <dd class="xbase_col2"><span class="mod_price xprice_val"><%=id %></span> <%=mp_pic%> <%=tags%></dd>
                                </dl>
                                <dl class="xbase_item xprice">
                                    <dt class="xbase_col1">销售价格</dt>
                                    <dd class="xbase_col2"><span class="mod_price xprice_val"><i>¥</i><%=Price %><i>起</i></span><%--<span class="xprice_tag">团购价</span>--%></dd>
                                </dl>
                                <dl class="xbase_item xgift">
                                    <dt class="xbase_col1">促销信息</dt>
                                    <dd class="xbase_col2">
                                        <ul class="xgift_list">
                                            <li class="sea_pop_parent">
                                                <div class="x_mod_gift"><%=Preference%>&nbsp;</div>
                                            </li>
                                        </ul>
                                    </dd>
                                </dl>
                                <div class="alert alert-block" style="margin-top:40px">
                                    <div class="<%=StopSellHide %>" style="margin:0px 0 10px 0px;padding-bottom:10px;font-size:14px;height:30px;">
                                        <div style="float:left;line-height:26px;">成人：</div>
                                        <div class="select_box">
                                            <span id="crnum" class="select_txt" tag="" date="">2</span><a class="selet_open"><i class="icon-chevron-down icon-white"></i></a>
                                            <div class="option"><a>0</a><a>1</a><a>2</a><a>3</a><a>4</a><a>5</a><a>6</a><a>7</a><a>8</a><a>9</a></div>
                                        </div>
                                        <div style="float:left;line-height:26px;">儿童：</div>
                                        <div class="select_box">
                                            <span id="etnum" class="select_txt" tag="" date="">0</span><a class="selet_open"><i class="icon-chevron-down icon-white"></i></a>
                                            <div class="option"><a>0</a><a>1</a><a>2</a><a>3</a><a>4</a><a>5</a><a>6</a><a>7</a><a>8</a><a>9</a></div>
                                        </div>
                                        <div class="<%=PlanDateDropHide %>" style="float:left;line-height:26px;">出发日期：</div>
                                        <div class="select_box <%=PlanDateDropHide %>" style="width:190px;">
                                            <span id="select_plandate" class="select_txt" tag="" date=""></span><a class="selet_open"><i class="icon-chevron-down icon-white"></i></a>
                                            <div class="option" id="plandate_droplist"></div>
                                        </div>
                                    </div>
                                    <div style="margin:15px 0 15px 12px;font-size:14px;"><%=LargeButtomString %></div>
                                </div>
                                <div class="alert alert-success">
                                    <h5>产品专家热线：021-64747516</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="container" style="padding-top:20px">
	<div class="row">
		<div class="span12">
		    <div class="left_part">
                <div class="left_Recommend">
                    <div class="Recommend_title">最近浏览</div>
                    <div id="Div2" class="Recommend_box">
                        <ul class="view" id="CookieView"></ul>
                    </div>
                </div>
            </div>

            <div class="right_part">
                <div class="right_intro <%=RouteFeatureHide %>">
                    <div class="Recommend_title">线路特色推荐</div>
                    <div class="right_introbox"><%=RouteFeature %></div>
                </div>
                <div class="right_intro <%=LineViewsHide %>">
                    <div class="Recommend_title">行程景点导览</div>
                    <div class="right_introbox"><div class="onsale_place"><%=LineViews%></div></div>
                </div>
                <div class="right_intro <%=VisaFeatureHide %>">
                    <div class="Recommend_title">签证办理说明</div>
                    <div class="right_introbox"><%=VisaFeature%></div>
                </div>
                <div class="right_intro hxxc <%=CruisesRouteHide %>">
                    <div class="Recommend_title">航线行程简介</div>
                    <div class="right_introbox">
                        <div id="trips" class="alert alert-block trips">
                            <div class="tripsHead">以下时间仅供参考，请以实际船票为准</div>
                            <div class="tripsBody">
                                <div class="tripsItem"><span class="terms"><strong>天数</strong></span><span class="port"><strong>停靠港口</strong></span> <span class="arrive"><strong>抵达</strong></span> <span class="startUp"><strong>启航</strong></span></div>
                                <div id="tripList" class="tripList">
                                    <div class="tripStorage">
                                    <%--<div class="tripsItem"><span class="terms">第1天</span> <span class="port">上海母港出发</span><span class="arrive">&nbsp;</span><span class="startUp">17:00&nbsp;</span></div>--%>
                                    <%=CruisesRoute %>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="myFocus"><!--焦点图盒子-->
                          <div class="loading"></div><!--载入画面(可删除)-->
                          <div class="pic"><!--图片列表-->
  	                        <%=CruisesPicUrl%>
                          </div>
                        </div>
                    </div>
                </div>
                
            </div>

            <div class="right_part">
                <div class="x_mod_tab">
                    <div class="x_mod_tab_hd" id="info_tit">
                        <div id="topnav" class="nav"> 
                            <ul id="nav"> 
                                <%=NavString %>
                            </ul>
                            <%=SmallButtomString %>
                        </div> 
                    </div>
                    <div class="x_mod_tab_bd <%=LineListInfo %>">
                        <div id="room_order" class="alert alert-block <%=CruisesRouteHide %>">
                            <h4>可供预定的舱型</h4>
                            <div id="cruisesroom" class="cruisesroom"><%=CruisesRoomListString %></div>
                        </div>
                        <div id="OrderHere" class="alert alert-block <%=CruisesRouteHide %>">
                            <h4>选择的房间和人数<a href="javascript:" id="selectmore" class="hide" onclick="ScrollTo('room_order')" style="font-weight: normal;font-size: 13px;margin-left:20px"><i class="icon-chevron-right"></i>继续选择其他房型点这里</a></h4>
                            <table id="Table1" style="width: 100%;margin:10px 0 0px 0px;font-size:14px;height:30px;" border="0">
                                <tr>
                                    <td>
                                        <span id="ShowPriceText">当前没有选择任何舱房，请选择后再预定</span><span id="ShowPriceCount" class="pcount"></span>
                                    </td>
                                    <td width="140px">
                                        <a id="OrderBtn" href="javascript:" onclick="CruisesOrder()" class="btn btn-large btn-warning">开始预订</a>
                                        <span id="islogin" style="display: none;FLOAT: left; FONT-SIZE: 14px; " class="iloading1">正在提交，请稍候...</span>
                                    </td>
                                </tr>
                            </table>

                            <div id="roomdiv" class="roomdiv">
                                <div class="roomHead">双人间最少入住2人，不满2人需要补房差；三人间或四人间同舱的第3、第4位可享受价格优惠；</div>
                                <form id="form_data" onsubmit="return false;" method="post">
                                    <div id="DIV3" style="DISPLAY:none">
                                        <input id="TB_LineId" name="TB_LineId" type="hidden" value="<%=id %>"/>
                                        <input id="TB_BeginDate" name="TB_BeginDate" type="hidden" value="<%=begindate %>"/>
                                        <input id="AllPeople" name="AllPeople" type="hidden" value="0"/>
                                        <input id="AllAdult" name="AllAdult" type="hidden" value="0"/>
                                        <input id="AllChilds" name="AllChilds" type="hidden" value="0"/>
                                        <input id="AllRoom" name="AllRoom" type="hidden" value="0"/>
                                        <input id="AllPrice" name="AllPrice" type="hidden" value="0"/>
                                    </div>
                                    <table id="RoomSelectList" class="hide" style="width: 100%;">
                                        <tr class="tit">
                                            <td width="30%">房间类型</td>
                                            <td width="5%">成人</td>
                                            <td width="5%">儿童</td>
                                            <td width="5%">房间数</td>
                                            <td width="10%">第1、2人价格</td>
                                            <td width="10%">第3成人价</td>
                                            <td width="10%">第3儿童价</td>
                                            <td width="10%">价格小计</td>
                                            <td width="3%">&nbsp;</td>
                                        </tr>
                                    </table>
                                </form>
                            </div>
                        </div>

                        <a id="plan_calendar"></a>
                        <div id="plan_calendar1" class="plandate_info <%=CalendarHide %>">
                            <div id="plandate" class="plan_date"></div>
                            <div class="alert alert-success">【行程打印】为<font color="#FF6600"><b>桔红色</b></font>时，表示该行程与参考行程有所不同，请点击查看</div>
                        </div>
                        <div id="plan_calendar2" class="plandate_info <%=PlanDateHide %>">
                            <div id="Div4" style="height:70px;margin-bottom: 10px;"><%=PlanDateString %></div>
                            <div class="alert alert-success">【行程打印】为<font color="#FF6600"><b>桔红色</b></font>时，表示该行程与参考行程有所不同，请点击查看</div>
                        </div>
                        <div class="right_intro">
                            <div class="Recommend_title">行程安排</div>
                            <div class="right_routebox">
                                <div class="detail_info" id="route_info">
                                    <div id="linelist"><div><%=RouteInfos %></div></div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="detail_info <%=RouteServiceInfosHide %>" id="service_info">
                            <div class="alert alert-success"><ul><%=RouteServiceInfos %></ul></div>
                        </div>

                        <div class="right_intro">
                            <div class="Recommend_title">费用描述</div>
                            <div class="right_introbox">
                                <div class="detail_info" id="price_info">
                                    <div class="info_title">报价包含</div>	
                                    <div class="info_content"><%=PriceIn %></div>
                                </div>
                                <div class="detail_info">
                                    <div class="info_title">报价不含</div>	
                                    <div class="info_content"><%=PriceOut %></div>
                                </div>
                                <div class="detail_info">
                                    <div class="info_title">自费项目</div>	
                                    <div class="info_content"><%=OwnExpense %></div>
                                </div>
                            </div>
                        </div>
                        <div class="right_intro">
                            <div class="Recommend_title">温馨提醒</div>
                            <div class="right_introbox">
                                <div class="detail_info">
                                    <div class="info_title" id="memo_info">注意事项</div>	
                                    <div class="info_content"><%=Attentions %></div>
                                </div>
                                <div class="detail_info">
                                    <div class="info_title">购物商店</div>	
                                    <div class="info_content"><%=Shopping %></div>
                                </div>
                            </div>
                        </div>
                        
                    </div>

                    <div class="x_mod_tab_bd <%=VisaListInfo %>">
                        <div class="right_intro">
                            <div class="Recommend_title">签证办理所需材料</div>
                            <div class="right_introbox">
                                <div class="detail_info <%=Visa1Hide %>" id="visa_info">
                                    <div class="info_title" id="Div5">身份证明</div>	
                                    <div class="info_content Visa"><%=Visa1 %></div>
                                </div>
                                <div class="detail_info <%=Visa2Hide %>">
                                    <div class="info_title">资产证明</div>	
                                    <div class="info_content Visa"><%=Visa2%></div>
                                </div>
                                <div class="detail_info <%=Visa3Hide %>">
                                    <div class="info_title">工作证明</div>	
                                    <div class="info_content Visa"><%=Visa3%></div>
                                </div>
                                <div class="detail_info <%=Visa4Hide %>">
                                    <div class="info_title">学生及儿童</div>	
                                    <div class="info_content Visa"><%=Visa4%></div>
                                </div>
                                <div class="detail_info <%=Visa5Hide %>">
                                    <div class="info_title">老人</div>	
                                    <div class="info_content Visa"><%=Visa5%></div>
                                </div>
                                <div class="detail_info <%=Visa6Hide %>">
                                    <div class="info_title">其他</div>	
                                    <div class="info_content Visa"><%=Visa6%></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="right_intro <%=RelativeLineHide %>" style="margin:-10px 0 15px">
                        <div class="Recommend_title">相关线路推荐</div>
                        <div class="onsale_product" style="height:193px;">
                            <%=RelativeLine %>
                        </div>
                    </div>
                    <div id="order_service" class="alert alert-success alert-block">
                        <h1>在线预订流程</h1>
                        <img src="/img/order_step.png"/>
                    </div>

                </div>

            </div>
            
            
        </div>
    </div>
</div>

<div id="inputs" style="DISPLAY:none">
    <input id="TB_LineFlag" type="hidden" value="<%=LineFlag %>"/>
    <input id="TB_CanSell" type="hidden" value="<%=CanSell %>"/>
    <input id="TB_Price" type="hidden" value="<%=Price %>"/>
    <input id="TB_pic" type="hidden" value="<%=FirstPic %>"/>
</div>
<uc2:Footer ID="Footer1" runat="server" />
<script type="text/javascript">
//    $(function () {
//        //自定义绑定事件
//        $("#topnav li").click(function () {
//            $(this).addClass("current").siblings().removeClass("current");
//            $("html,body").animate({ scrollTop: $("#" + $(this).attr("tag")).offset().top }, 1000);
//        });
    //    });

    function AddToFavorite() {
        var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                LineFavorite();
            }
            else {
                LoginNow("Favorite");
            }
        })
    }

    function LineFavorite() {
        if ($(".thickbox").length != 0) {
            jdThickBoxclose()
        }
        var url = "/tour/ajaxservice.aspx?action=Favorite&id=" + $("#TB_LineId").val() + "&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                jSuccess("<strong>" + date.content + "</strong>", { ShowOverlay: false, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
            else {
                jError('<strong>收藏失败，请稍后再试!</strong>', { autoHide: false, clickOverlay: true, HorizontalPosition: 'center', VerticalPosition: 'center' });
            }
        })
    }

    function ShowSight(id) {
        if ($("#Sight" + id).is(":visible") == false) {
            $(".viewtr").hide();
            $("#Sight" + id).toggle();
        }
        else {
            $("#Sight" + id).hide()
        }
    }

    var showrouteflag = "0";
    function showroute() {
        showrouteflag = "1";
    }

    function Order() {
        alert("还没有开始预订，请等候通知");
        return false;

        if ($("#TB_LineFlag").val() == "2") {
            ScrollTo("room_order");
            return false;
        }
        if (showrouteflag == "0") {
            var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
            $.getJSON(url, function (date) {
                if (date.success == 0) {
                    OrderNow();
                }
                else {
                    LoginNow("order");
                }
            })
        }
        showrouteflag = "0";
    }

    function OrderNow() {
        if ($(".thickbox").length != 0) {
            jdThickBoxclose()
        }
        if ($("#TB_LineFlag").val() == "2") {
            $("#islogin").show();
            $("#OrderBtn").hide();
            var url = "/CruisesOrder/AjaxService.aspx?action=OrderSubmit&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/CruisesOrder/FirstStep/" + obj.success + ".html";
                }
                if (obj.error) {
                    $("#islogin").hide();
                    $("#OrderBtn").show();
                    alert(obj.error);
                }
            });
        }
        else {
            var planid = "0";
            var begindate = "";
            if ($("#TB_LineFlag").val() == "1") {
                planid = $("#select_plandate").attr("tag");
                if (planid == "" || planid == null) {
                    alert("请选择您的出发日期！");
                    return false;
                }
                begindate = $("#select_plandate").attr("date");
            }
            
            var url = "/Purchase/OrderNow.aspx?lineid=" + $("#TB_LineId").val() + "&planid=" + planid + "&begindate=" + begindate + "&nums=" + $("#crnum").html() + "&etnums=" + $("#etnum").html() + "&r=" + Math.random();
            //alert(url);
            $.jdThickBox({
                type: "iframe",
                title: "在线预订",
                source: url,
                width: 580,
                height: 420,
                _title: "thicktitler",
                _close: "thickcloser",
                _con: "thickconr"
            })
        }
        
    }


    function CruisesOrder() {
        if ($("#AllRoom").val() == "0") {
            alert("请选择您需要预定的房间数量！");
            return false;
        }
        var url = "/Login/AjaxService.aspx?action=IsLogin&r=" + Math.random();
        $.getJSON(url, function (date) {
            if (date.success == 0) {
                OrderNow();
            }
            else {
                LoginNow("order");
            }
        })
    }
    
    //根据浏览器宽度定义日历数量
    var ShowMonthNum = 1;
    if (screen.width >= 1280) {ShowMonthNum = 2; }
    //if ($("#TB_LineFlag").val() == "2") myFocus.set({id: 'myFocus',pattern: 'mF_games_tb'});

    $(document).ready(function () {
        if ($("#TB_LineFlag").val() == "2") {
            myFocus.set({ id: 'myFocus', pattern: 'mF_games_tb' });
            if ($("#cruisesroom").html() != "") $("#cruisesroom ul").idTabs();
//            var url = "/tour/ajaxservice.aspx?action=CruisesRoomList&Id=<%=id %>&r=" + Math.random();
//            $("#cruisesroom").html("<div class=iloading>正在加载中，请稍候...</div>");
//            $.getJSON(url, function (date) {
//                $("#cruisesroom").html(date.content);
//                $("#cruisesroom ul").idTabs();
//            })
        }
        
        
        var url = "/tour/ajaxservice.aspx?action=LineCount&Id=<%=id %>&r=" + Math.random();
        $.get(url, function (data) { });

        BindHistory();
        HistoryRecord();

        $("#WeekHotRank li").mouseover(function () {
            $("#WeekHotRank .p-img").hide();
            $("#WeekHotRank .p-price").hide();
            $("#WeekHotRank .p-day").hide();
            $(this).find('.p-img').show();
            $(this).find('.p-price').show();
            $(this).find('.p-day').show();
            $(this).addClass("fore").siblings().removeClass("fore");
        });

        var MaxWidth = $("#list_smallpic li").length * 62;
        $("#list_smallpic").css({ "width": MaxWidth, "left": 0 });
        $(".xgallery_prev").hide();
        if ($("#list_smallpic li").length < 5) {
            $(".xgallery_next").hide();
        }

        $(".xgallery_prev").click(function () {
            var left = parseInt($("#list_smallpic").css("left"));
            $("#list_smallpic").animate({ left: (left + 62) + "px" });
            $(".xgallery_next").show();
            if (left == -62) $(".xgallery_prev").hide();
        });

        $(".xgallery_next").click(function () {
            var left = parseInt($("#list_smallpic").css("left"));
            $("#list_smallpic").animate({ left: (left - 62) + "px" });
            $(".xgallery_prev").show();
            if (MaxWidth + left - 62 == 248) $(".xgallery_next").hide();
        });

        $("#list_smallpic li").mouseover(function () {
            $(this).addClass("current").siblings().removeClass("current");
            $("#xgalleryImg").attr("src", $(this).find('img').attr("src-org"));
        });

        $(".select_box").click(function (event) {
            event.stopPropagation();
            $(this).find(".option").toggle();
            $(this).parent().siblings().find(".option").hide();
        });

        $(document).click(function (event) {
            var eo = $(event.target);
            if ($(".select_box").is(":visible") && eo.attr("class") != "option" && !eo.parent(".option").length)
                $('.option').hide();
        });

        //日历数据提取 TB_LineFlag
        if ($("#TB_CanSell").val() == "yes" && $("#TB_LineFlag").val() == "1") {
            $("#plandate").showRenderCalendar({ events: eval(json), showNum: ShowMonthNum });
            initTips();
            PlanDropDownListBind();
        }

        /*赋值给文本框*/
        $(".option a").click(function () {
            $(this).parent().siblings(".select_txt").html($(this).html());
            $(this).parent().siblings(".select_txt").attr("tag", $(this).attr("tag"));
            $(this).parent().siblings(".select_txt").attr("date", $(this).attr("date"));
        })

        $("#topnav").smartFloat();
        $('#nav').onePageNav({ scrollOffset: 50, scrollThreshold: 0.2 });

    })
</script>
</body>
</html>
