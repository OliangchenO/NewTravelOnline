<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruiseDetail.aspx.cs" Inherits="TravelOnline.NewPage.CruiseDetail" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><%=LineName %>-上海青旅</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %>" />
    <meta name="Keywords" content="<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %>" />
    <link rel="shortcut icon" href="" />
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/line.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/oldline.css" rel="stylesheet" type="text/css" />
    <link href="/newjs/select2/select2.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="/newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="/newjs/common.js"></script>
    <script type="text/javascript" src="/newjs/jquery.pagination.js"></script>
    <script type="text/javascript" src="/newjs/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="/newjs/img.js"></script>
	<script type="text/javascript" src="/newjs/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="/newjs/datePicker.js"></script>
    <script type="text/javascript" src="/newjs/datebind.js"></script>
    <script type="text/javascript" src="/newjs/jquery.nav.js"></script>
    <script type="text/javascript" src="/newjs/select2/select2.min.js"></script>
    <script type="text/javascript" src="/newjs/select2/select2_locale_zh-CN.js"></script>
    <script type="text/javascript" src="/newjs/jquery.cookie.js"></script>
    <script type="text/javascript" src="/newjs/setcookie.js"></script>
	<!--[IF IE 6]>
		<script type="text/javascript" src="js/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
    <style type="text/css">
        .detail-tab-box { background:#ffffff; z-index:9;}
        #js_route_days { background:#ffffff; z-index:1;}
        .detail-tab .green a{background: #8ab923;color: #fff;}
        .under-tab-box .data-detail .pink a{border: 1px solid #ff6666; background: #ff6666; color: #fff;}
        .routecontent {display:table;}
        .left-col {width: 120px; float: left;}
        .right-col {width: 810px; float: left;}
        .hide {display: none;}
        .info-r .line-info .sale-info {height: 57px; overflow: hidden;}
        .buy-content .t5,.select-num .choose {position: relative; padding-left: 7px; width: 37px; height: 28px; line-height: 30px; border: 1px solid #ccc; font-size: 16px; cursor: pointer;}
        .buy-content .t5 p,.choose .show-num {display: none; position: absolute; left: -1px; top: 28px; z-index: 50; padding: 3px 0; width: 44px; border: 1px solid #ccc; background: #fff;}
        .buy-content .t5 a,.choose .show-num a {display: block; padding-left: 7px; height: 20px; line-height: 20px;} 
        .buy-content .t5 a:hover,.choose .show-num a:hover {background: #7daf0f; color: #fff;}
        .buy-content .t5 b {background: url('../image/detail/pro_ui.png') no-repeat;position: absolute; z-index: 20; background-position: 0 -257px; top: 11px; right: 7px; width: 8px; height: 8px;}
        .buy-content .t2,.buy-content .t2 p {width: 280px;}
        .selectdate {background: #f8f4c0;}
        .showCalendarPanel table {height: 220px; }
        .side-content .list a .linename{line-height:30px;height:30px;overflow:hidden;}
        .select-content dd {border-bottom: 0px dotted #F2F1F1;}
        .select-content dd .con1 span {font-weight:bold}--%>
        #loading {text-align:center}
        .priceloading{ font-size:14px;font-weight:bold;padding-left:30px;}
        #order-info{display:table;}
        .select2me {width: 60px;}
        .cruises-fr{background-position: -106px -277px;color: #666666;}
        .under-tab-box .data-detail .pink a {border: 1px solid #feb503; background: #feb503; color: #fff;}
        #nav_order a.cprint {
            padding-left: 20px;width: 100px;
            background: url('/image/dayin.png') no-repeat left 50%;
            color: #5e74a6;
        }
        .cruises-select .select-content .room .choose-box .choose .hdl .d5 .on{ background-position:0px -333px;}

        .cruises-select .select-content .room .choose-box .choose {height: 45px;}
        .detail-tab a.print {margin-left: 10px;}
        .thickframe{z-index:10000000;border-bottom:0;position:fixed;filter:alpha(opacity=0);border-left:0;width:100%;background:#000;height:100%;border-top:0;top:0;border-right:0;left:0;opacity:0;}
        .thickdiv{z-index:10000001;border-bottom:0;position:fixed;filter:alpha(opacity=15);border-left:0;width:100%;background:#000;height:100%;border-top:0;top:0;border-right:0;left:0;opacity:0.15;}
        .thickbox{z-index:10000002;position:absolute;background:url(/img/bg_shadow.gif) no-repeat -4px 0;overflow:hidden;-moz-border-radius:0 20px 6px;-webkit-border-radius:0 20px 6px 20px;padding:0 4px 4px 0;}
        .thicktitle{border-bottom:#c4c4c4 0 solid;border-left:#c4c4c4 1px solid;line-height:27px;font-family:arial, 宋体;background:#f3f3f3;height:27px;color:#333;font-size:14px;border-top:#c4c4c4 1px solid;font-weight:700;border-right:#c4c4c4 1px solid;-moz-border-radius:4px 4px 0 0;-webkit-border-radius:4px 4px 0 0;padding:0 10px;}
        .thickcon{border-bottom:#c4c4c4 1px solid;border-left:#c4c4c4 1px solid;background:#fff;overflow:hidden;border-top:#c4c4c4 1px solid;border-right:#c4c4c4 1px solid;padding:10px;}
        .thickloading{background:url(/images/loading.gif) #fff no-repeat center center;}
        #thicktitler{border-bottom:medium none;border-left:medium none;background:#80D04D;color:#fff;border-top:medium none;border-right:medium none;padding:0 11px;}
        #thickconr{border-bottom:#80D04D 1px solid;border-left:#80D04D 1px solid;border-top:#80D04D 1px solid;border-right:#80D04D 1px solid;}
        .thickclose:link,.thickclose:visited{z-index:100000;position:absolute;line-height:100px;width:15px;display:block;background:url(/img/bg_thickbox.gif) no-repeat 0 -18px;height:15px;font-size:0;overflow:hidden;top:7px;right:12px;}
        #thickcloser:link,#thickcloser:visited{width:16px;background-position:0 0;height:17px;top:6px;right:9px;}
       .tdn,.tds {color:#ff6600; font-family:Arial, Helvetica, sans-serif; font-size:14px; font-weight:bold;}
       .selected-info{ padding:0 18px; width:680px;}
       .affirm-box{ width:200px; padding-right:26px; padding-top:18px; position:relative;}
       .cruises-select .select-content .room .choose-box .choose .hdl .select{ background:#FFF;margin-left:9px; padding-right:22px; border: 0px solid #CCCCCC;cursor: pointer; height: 19px;line-height: 21px;position: relative;width: 22px; margin-top:8px;}
       .detail-tab .blue {
        background: #178ddf;
        color: #fff;
        }
        .detail-tab .blue a {
        background: #178ddf;
        color: #fff;
        }
        .con-next {
        padding: 0 10px;
        font: bold 14px/40px 'Microsoft Yahei';
        color: #fff;
        background: #609841;
        height: 40px;
        margin-bottom: 20px;
        }
    </style>
</head>
<body id="<%=BodyId %>">
	<!--页头Begin-->
    <uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
        <div id="inputs" style="DISPLAY:none">
            <input id="TB_LineName" type="hidden" value="<%=LineName %>"/>
            <input id="TB_Price" type="hidden" value="<%=Price %>"/>
            <input id="TB_Pic" type="hidden" value="<%=Pics %>"/>
            <input id="TB_Tag" type="hidden" value="<%=Tags %>"/>
        </div>
		<div class="link-nav">
				<a href="/index.html">首页</a>
				<span>></span>
                <%=BreadCrumb %>
		</div>
		<div class="main-content fl">
			<!--产品名称-->
			<div class="main-top">
				<div class="top-tit" style="padding-left:0px"><%=LineName %>
					<span>|</span>
					<i>线路编号：<%=LineId%></i>
				</div>
			</div>
			<!--产品信息-->
			<div class="main-info clearfix" style=" border-bottom:4px solid #e1e1e1; border-right:4px solid #e1e1e1">
				<div class="info-l fl">
					<!--banner Begin-->
					<div id="tFocus">
						<div class="prev" id="prev"></div>
						<div class="next" id="next"></div>
						<ul id="tFocus-pic">
                            <%=RouteBigImg %>
						</ul>
						<div id="tFocusBtn">
							<a href="javascript:void(0);" id="tFocus-leftbtn">上一张</a>
							<div id="tFocus-btn">
								<ul>
                                    <%=RouteSmallImg%>
								</ul>
							</div>
							<a href="javascript:void(0);" id="tFocus-rightbtn">下一张</a>
						</div>
					</div>
					<script type="text/javascript">addLoadEvent(Focus());</script>
					<!--banner End-->
					<div class="left-b">
                    	<div class="hotline"></div>
                    </div>
				</div>
				<div class="info-r fl" style="border:none;">
					<div class="price-info" style="background:#5cb6e0">
						<span>青旅价：</span>
						<div class="cost fl clearfix">
							<p>
								<span>¥</span>
								<%=Price %>
								<i>起</i>
							</p>
						</div>
						<div class="sp fl">起价说明
							<div class="show">
								本起价是在最近24小时内测算的，按双人出行共用一间房核算的最低单人价格。例如，您选择了相应日期2位成人出行，上海青旅为您推荐的房型及最少入住天数，每人即可享受此最低起价价格。产品价格会根据您所选择的出发日期、出行人数、入住房型以及所选附加服务的不同而有所差别。
								<b></b>
							</div>
						</div>
					</div>
					<div class="line-info">
						<dl>
							<span class="list-name">起航日期：</span>
							<em><%=begindate%></em>
                            <%--<span class="list-name" style="margin-left:53px;">返航日期：</span>
							<em>2015-6-30</em>--%>
						</dl>
						<dl class="sale-info clearfix">
							<span class="cf60 fl ">优惠信息：</span>
							<em class="cf60 fl"><%=Preference %></em>
						</dl>
                        
						<dl class="btn-box">
							<a class="btn" href="#cfyd"></a>
                            <div id="addcollect" class="collect">
								<a href="javascript:;">收藏
									<b></b>
								</a>
							</div>
                            <div id="shareItem" class="collect relative-box">
								<a href="javascript:;">分享
									<b class="share"></b>
								</a>
								<div id="fxBox" class="share-box">
									<a class="qq" href="javascript:;"></a>
									<a class="wb" href="javascript:;"></a>
									<a class="tx" href="javascript:;"></a>
									<a class="rr" href="javascript:;"></a>
									<a class="db" href="javascript:;"></a>
									<a class="kj" href="javascript:;"></a>
								</div>
							</div>
							<div class="line"></div>
						</dl>
					</div>
					<div class="recommend-box">
						<h4>推荐理由
							<b class="c-bh"></b>
						</h4>
						<ul>
							<li><%=RouteFeature%></li>
						</ul>
					</div>
				</div>
			</div>
            <div class="clearfix"></div>
			<!--Tab-->
            <div id="state_content" class="detail-tab-box">
				<div id="js_tab_c" class="detail-tab clearfix">
					<%--<a class="blue" href="#cfyd">舱房预定</a>
                    <a href="#hxjs">航线介绍</a>
                    <a href="#xcap">行程介绍</a>
					<a href="#fysm">费用说明</a>
					<a href="#qzxx">签证信息</a>
					<a href="#ydxz">预定须知</a>--%>
                    <ul id="nav">
                        <li class="blue"><a href="#cfyd">舱房预定</a></li>
                        <li><a href="#hxjs">航线介绍</a></li>
                        <li><a href="#xcap">行程介绍</a></li>
                        <li><a href="#fysm">费用说明</a></li>
                        <li><a href="#qzxx">签证信息</a></li>
                        <li><a href="#ydxz">预定须知</a></li>
                    </ul>
                    <div class="buy-now-fixed" id="nav_order">
                        <a href="/route.html?id=<%=LineId %>" target=_blank class="cprint">打印行程单</a>
						<a id="orderinfo" href="#cfyd"></a>
					</div>
				</div>
			</div>
                
            <div class="clearfix"></div>
            <div class="under-tab-box" style="margin-bottom: 0px;border-bottom: 0px solid #ccc;">
                <!--舱房预定-->
            	<div class="cruises-select" id="cfyd">
                <!--舱房预定头部-->
                	<ul><%=RoomNo1 %></ul>
                    <div class="selsct-tt">
                    	<table width="100%" border="0" cellspacing="0" cellpadding="0">
  							  <tr>
   								<td width="121">舱房类型</td>
                                <td width="96">&nbsp;</td>
                                <td width="103">房型面积</td>
                                <td width="103">可住人数</td>
                                <td width="130">第1、2人价格</td>
                                <td width="130">第3、4成人价格</td>
                                <td width="130">第3、4儿童价格</td>
                                <td width="121">&nbsp;</td>
                              </tr>
                         </table>
                    </div>
                  <!--舱房选择-->
               	  <div class="select-content"><%=RoomNo2 %></div>
                </div>
                
                <!--已选舱房-->
                <div class="selected-tit"><i></i>已选舱房类型</div>
                <div class="line"></div>
                <!--修改确认-->
                <div class="selected-box">
                	<div class="selected-info fl" style="min-height:120px">
                        <form id="form_data" onsubmit="return false;" method="post" action="javascript:;">
                            <div id="DIV3" style="DISPLAY:none">
                                <input id="TB_LineId" name="TB_LineId" type="hidden" value="<%=LineId %>"/>
                                <input id="TB_BeginDate" name="TB_BeginDate" type="hidden" value="<%=begindate %>"/>
                                <input id="AllPeople" name="AllPeople" type="hidden" value="0"/>
                                <input id="AllAdult" name="AllAdult" type="hidden" value="0"/>
                                <input id="AllChilds" name="AllChilds" type="hidden" value="0"/>
                                <input id="AllRoom" name="AllRoom" type="hidden" value="0"/>
                                <input id="AllPrice" name="AllPrice" type="hidden" value="0"/>
                                <input id="TB_ProductFlag" type="hidden" value="cruise"/>
                            </div>
                            <table id="RoomSelectList" style="width: 100%;">
                                <tbody>
                                <tr class="selected-t-tit">
                                    <td width="18%" class="ftd">舱房类型</td>
                                    <td width="5%">成人</td>
                                    <td width="5%">儿童</td>
                                    <td width="5%">房间数</td>
                                    <td width="12%">第1、2人价格</td>
                                    <td width="12%">第3成人价</td>
                                    <td width="12%">第3儿童价</td>
                                    <td width="12%">价格小计</td>
                                    <td width="5%">&nbsp;</td>
                                </tr>
                                </tbody>
                            </table>
                        </form>
                        <div class="popover-mask"></div>
                    </div>
                    <div class="affirm-box rl">
                    	<div id="total" class="total">合计总计：<span><em>￥</em>0</span></div>
                        <div class="affirm-buy"><a href="javascript:;" onclick="CruisesOrder()"></a><a id="Button1" href="javascript:;"><img src="/image/detail/waiting.png" /></a></div>
                    </div>
                    <div class="clear"></div>
                </div>
                
                <div class="line"></div>
                


                <div class="route-tit" id="hxjs"><i></i>航线介绍</div>
                <div class="route-ta">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr class="trt">
                    <td width="17%">天数</td>
                    <td width="35%">停靠港口</td>
                    <td width="24%">抵达时间</td>
                    <td width="24%">起航时间</td>
                    </tr>
                    <%=CruisesRoute %>
                </table>
                </div>
                <div class="line"></div>
            </div>
            <div class="under-tab-box">
                <div class="routecontent">
                    <div class="left-col">
                        <div id="xcap" class="present" style="background-position: -106px -277px;">行程安排</div>
				        <!--行程安排-->
				        <div id="js_route_days" class="data-detail">
                            <ul id="nav_days">
                                <%=NavDays%>
                            </ul>
				        </div>
                    </div>
                    <div class="right-col">
                        <%=Routes %>
				    </div>
                </div>
                <div class="clearfix"></div>
                <div class="line"></div>
				<div id="fysm" class="detail_content top25">
					<div class="expense cruises-fr">费用说明</div>
					<div class="expense-list">
						<div class="baohan">费用包含</div>
						<div class="baohan-length">
							<%=PriceIn1%>
						</div>
					</div>
					<div class="expense-list top30">
						<div class="baohan">费用不含</div>
						<div class="baohan-length">
							<%=PriceOut%>
						</div>
					</div>
				</div>
				<div class="line"></div>
				<div id="qzxx" class="detail_content top25 <%=hide %>">
					<div class="qz-box cruises-fr">签证信息</div>
					<div class="qz-news-box">
						<div class="qz-news">签证信息</div>
						<div class="download">
							<%--<a href="javascript:;" title="">http://www.scyts.com/line/15551.zip</a>--%>
                            <%=VisaFileInfo %>
						</div>
					</div>
				</div>
				<div class="line"></div>
				<div id="ydxz" class="detail_content top25">
					<div class="yd-box cruises-fr">预定须知</div>
					<%--<div class="ydxz-list">
						<div class="pattern">签约方式</div>
						<div class="pattern-length">
							<p>传真签约：双方在合同上签字盖章后，通过传真进行签约。</p>
							<p>快递签约：我们把盖章合同快递到您，您签字后快递回我们公司，完成签约。 </p>
							<p>合同范本：<%=ContractInfos %> </p>
						</div>
					</div>
					<div class="ydxz-list top30">
						<div class="pattern">付款方式</div>
						<div class="pattern-length">
							<p>在线支付：支付宝、快钱等多种在线支付方式，供您选择。</p>
							<p>门店付款：您可以到距离你最近的门店，完成付款。 </p>
						</div>
					</div>--%>
                    <div class="ydxz-list top30">
						<div class="shopping">自费项目</div>
						<div class="shopping-length">
							<%=OwnExpense%>
						</div>
					</div>
					<div class="ydxz-list top30">
						<div class="shopping">购物商店</div>
						<div class="shopping-length">
							<%=Shopping%>
						</div>
					</div>
					<div class="ydxz-list top30">
						<div class="pattern">注意事项</div>
						<div class="pattern-length">
							<%=Attentions%>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!--右侧-->
		<div class="side-content-c rl">
			<h4><span></span>其他人正在看</h4>
			<div class="list">
				<ul>
                    <%=TravelOnline.NewPage.Class.CacheClass.GuessYouLike(lineclass.ToString(),5)%>
				</ul>	
			</div>
		</div>
	</div>
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Button1").hide();
            HistoryRecord();
            $('select').select2({minimumResultsForSearch: -1});
            initTips();
            
            $("#state_content").smartFloat();
            $("#js_route_days").pin({containerSelector: ".routecontent",padding: {top: 100, bottom: 50}});
            $('#nav').onePageNav({ scrollOffset: 50, scrollThreshold: 0.1, currentClass: 'blue'});
            $('#nav_days').onePageNavNew({ scrollOffset: 50, scrollThreshold: 0.1, currentClass: 'pink'});
        })

        

        //选择舱房数量计算价格
        $(function () {
            $('.select2me').live("change", function () {
                var parent = $(this).parents("dl");
                var parentid = "#" + parent.attr("id");

                berth = parseInt(parent.attr("mnum"));
                adult = parseInt($(parentid + " select:eq(0)").val());
                childs = parseInt($(parentid + " select:eq(1)").val());
                if ((adult + childs) == 0 || adult == 0) {
                    $(parentid + " .pcount").html("&yen;0");
                    $(parentid + " .hide").html("0");
                    $(parentid + " select:eq(2)").html("<option value=\"0\">0</option>");
                    return;
                }
                range = [], str = "", nums = "0";
                if (adult != 0) {
                    range.push(Math.ceil((adult + childs) / berth));
                    range.push(Math.ceil(adult + childs));
                    if (range[0] != 0) {
                        nums = range[0];
                        for (var i = range[0]; i <= range[1]; i++) {
                            str += "<option value=\"" + i + "\">" + i + "</option>";
                        };
                        if (berth > 2) {
                            str = "<option value=\"" + range[0] + "\">" + range[0] + "</option>";
                        }
                    }
                }
                else {
                    str = "<option value=\"0\">0</option>";
                }
                $(parentid + " select:eq(2)").html(str);
                $(parentid + " select:eq(2)").select2("val", nums);
                calTotal(parentid);

            })

            $('.select2co').live("change", function () {
                var parent = $(this).parents("dl");
                var parentid = "#" + parent.attr("id");
                calTotal(parentid);

            })

            function calTotal(pid) {

                var berth = Number($(pid).attr("mnum"));
                var price1 = Number($(pid).attr("p1"));
                var price2 = Number($(pid).attr("p2"));
                var price3 = Number($(pid).attr("p3"));
                var adult = Number($(pid + " select:eq(0)").val());
                var childs = Number($(pid + " select:eq(1)").val());
                var hourseNum = Number($(pid + " select:eq(2)").val());
                var allPeple = adult + childs;
                var total = 0;
                switch (berth) {
                    case 1:
                        total = allPeple * price1;
                        break
                    case 2:
                        total = hourseNum * price1 * 2;
                        break
                    default:
                        total = hourseNum * price1 * 2;
                        if (childs >= hourseNum * (berth - 2)) {
                            total += hourseNum * (berth - 2) * price3;
                        }
                        else {
                            total += childs * price3;
                            total += (hourseNum * (berth - 2) - childs) * price2;
                        }
                        break
                }
                
                $(pid + " .countprice").val(total);
                if (total == 0) {
                    $(pid + " .d4 span:eq(0)").html("<em>￥</em>0");
                } else {
                    $(pid + " .d4 span:eq(0)").html("<em>￥</em>" + total);
                }
            }

        });

        $(function () {
            $('.hdl .d5').live("click", function () {
                allotid = $(this).parents("dl").attr("tag");
                if ($(this).find('b').hasClass("on")) {
                    return
                }
                pid = "#" + $(this).parents("dl").attr("id");
                roomname = $(pid).attr("tps");
                berth = parseInt($(pid).attr("mnum"));
                haverooms = parseInt($(pid).attr("haveroom"));
                allprice = parseInt($(pid + " .countprice").val());
                price1 = parseInt($(pid).attr("p1"));
                price2 = "&yen;" + $(pid).attr("p2");
                price3 = "&yen;" + $(pid).attr("p3");
                adult = parseInt($(pid + " select:eq(0)").val());
                childs = parseInt($(pid + " select:eq(1)").val());
                hourseNum = parseInt($(pid + " select:eq(2)").val());
                if (price2 == "&yen;0") price2 = "--";
                if (price3 == "&yen;0") price3 = "--";

                if (hourseNum < 1) {
                    alert("房间数量必须选择");
                    return;
                }
                if (hourseNum > haverooms) {
                    alert("您选择的" + roomname + "只剩余" + haverooms + "间，请重新选择");
                    return;
                }
                if (adult < hourseNum) {
                    alert(hourseNum + "间" + roomname + "都必须入住一位成人");
                    return;
                }
                allPeple = adult + childs;
                if (berth > 2) {
                    if (allPeple != hourseNum * berth) {
                        alert("您选择的是" + hourseNum + "间" + berth + "人间，入住人数必须是" + hourseNum * berth + "人");
                        return;
                    }
                }

                str = "<tr>";
                str += "<td class=ftd>" + roomname + "</td><td>" + adult + "</td><td>" + childs + "</td><td>" + hourseNum + "间</td><td class=tdn>&yen;" + price1 + "</td><td class=tdn>" + price2 + "</td><td class=tdn>" + price3 + "</td><td class=tds>&yen;" + allprice + ""
                str += "<input class='RS_ID' name='RS_ID' type='hidden' value='" + allotid + "'/><input class='RS_CR' name='RS_CR' type='hidden' value='" + adult + "'/><input class='RS_ET' name='RS_ET' type='hidden' value='" + childs + "'/>";
                str += "<input class='RS_ROOM' name='RS_ROOM' type='hidden' value='" + hourseNum + "'/><input class='RS_PRICE' name='RS_PRICE' type='hidden' value='" + allprice + "'/><input class='RS_NUM' name='RS_NUM' type='hidden' value='" + allPeple + "'/>";
                str += "</td>";
                str += "<td><a href='javascript:;' class='delete' tag=\"" + allotid + "\">删除</a></td>";
                str += "</tr>";
                //alert(str);selectmore
                $("#RoomSelectList").show();
                $("#RoomSelectList").append(str);
                $(this).find('b').addClass("on");
                CountPrice();
            })

            $('.delete').live("click", function () {
                var id = $(this).attr("tag");
                DeleteRoom(id, this);
            })

            function DeleteRoom(id, obj) {
                if (confirm('确认要删除选中的房型吗？')) {
                }
                else
                { return false; }
                trs = $(obj).parents("td").parents("tr");
                trs.remove();
                $("#RB_" + id).find('b').removeClass("on");
                CountPrice();
            }

            function CountPrice() {
                Adult = 0;
                Childs = 0;
                Rooms = 0;
                SumPrice = 0;
                SumPeople = 0;

                $(".RS_CR").each(function () { Adult += Number($(this).val()); });
                $(".RS_ET").each(function () { Childs += Number($(this).val()); });
                $(".RS_ROOM").each(function () { Rooms += Number($(this).val()); });
                $(".RS_PRICE").each(function () { SumPrice += Number($(this).val()); });
                SumPeople = Adult + Childs;
                $("#AllPeople").val(SumPeople);
                $("#AllAdult").val(Adult);
                $("#AllChilds").val(Childs);
                $("#AllRoom").val(Rooms);
                $("#AllPrice").val(SumPrice);
                
                $("#total").html("合计总计：<span><em>&yen;</em>" + SumPrice + "</span>");
            }

        });


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

        function LoginNow(flag) {
            $.jdThickBox({
                type: "iframe",
                title: "您还没有登录",
                source: "/login/loginnow.aspx?flag=" + flag,
                width: 440,
                height: 480,
                _title: "thicktitler",
                _close: "thickcloser",
                _con: "thickconr"
            })
        }

        function OrderNow() {
            if ($(".thickbox").length != 0) {
                jdThickBoxclose()
            }
            $(".affirm-buy a").hide();
            $("#Button1").show();
            var url = "/CruisesOrder/AjaxService.aspx?action=OrderSubmit&r=" + Math.random();
            $.post(url, $("#form_data").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/CruisesOrder/FirstStep/" + obj.success + ".html";
                }
                if (obj.error) {
                    $(".affirm-buy a").show();
                    $("#Button1").hide();
                    alert(obj.error);
                }
            });

        }

	</script>
</body>
</html>