<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="line.aspx.cs" Inherits="TravelOnline.NewPage.line" %>
<%@ Register src="/NewPage/header.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="/NewPage/Footer.ascx" tagname="Footer" tagprefix="uc2" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><%=LineName %>-上海青旅</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="Keywords" content=<% =TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <meta name="save" content="history">
    <link rel="shortcut icon" href="" />
    <link href="/newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/page.css" rel="stylesheet" type="text/css" />
    <link href="/newcss/line.css" rel="stylesheet" type="text/css" />
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


      <link href="/newjs/galleria/galleria.classic.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="/newjs/jquery.blockui.min.js"></script>
       <script type="text/javascript" src="/newjs/galleria/galleria.js"></script>

	<!--[IF IE 6]>
		<script type="text/javascript" src="js/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
    <style type="text/css">
        .info-r .recommend-box {height: 195px;}
        .detail-tab-box { background:#ffffff; z-index:9999;}
        #js_route_days { background:#ffffff; z-index:1;}
        .detail-tab .green a{background: #8ab923;color: #fff;}
        .under-tab-box .data-detail .pink a{border: 1px solid #ff6666; background: #ff6666; color: #fff;}
        .routecontent {display:table;}
        .left-col {width: 120px; float: left;}
        .right-col {width: 810px; float: left;}
        .hide {display: none;}
        .info-r .line-info .sale-info {height: 40px; overflow: hidden;}
        .buy-content .t5,.select-num .choose {position: relative; padding-left: 7px; width: 37px; height: 28px; line-height: 30px; border: 1px solid #ccc; font-size: 16px; cursor: pointer;}
        .buy-content .t5 p,.choose .show-num {display: none; position: absolute; left: -1px; top: 28px; z-index: 50; padding: 3px 0; width: 44px; border: 1px solid #ccc; background: #fff;}
        .buy-content .t5 a,.choose .show-num a {display: block; padding-left: 7px; height: 20px; line-height: 20px;} 
        .buy-content .t5 a:hover,.choose .show-num a:hover {background: #7daf0f; color: #fff;}
        .buy-content .t5 b {background: url('../image/detail/pro_ui.png') no-repeat;position: absolute; z-index: 20; background-position: 0 -257px; top: 11px; right: 7px; width: 8px; height: 8px;}
        .buy-content .t2,.buy-content .t2 p {width: 280px;}
        .selectdate {background: #f8f4c0;}
        .showCalendarPanel table {height: 220px; }
        .side-content .list a .linename{line-height:30px;height:30px;overflow:hidden;}
        #nav_order a.print 
        {
            margin-left: 0px;
            padding-left: 20px;
            background: url('/image/dayin.png') no-repeat left 50%;
            color: #5e74a6;
        }
        
        .select-content dd {border-bottom: 1px dotted #F2F1F1;}
        .select-content dd .con1 span {font-weight:bold}--%>
        #loading {text-align:center}
        .priceloading{ font-size:14px;font-weight:bold;padding-left:30px;}
        #order-info{display:table;}
        .select2me {width: 60px;}
        
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
       

    </style>
</head>
<body id="<%=BodyId %>">
	<!--页头Begin-->
    <uc1:Header ID="Header1" runat="server" />
    <!--页头End-->
	<!--正文内容Begin-->
	<div class="wrap clearfix">
        <div class="link-nav">
				<a href="/index.html">首页</a>
				<span>></span>
                <%=BreadCrumb %>
		</div>
		<div class="main-content fl">
			<!--产品名称-->
            <div id="Div1" style="DISPLAY:none">
                <input id="TB_Pic" type="hidden" value="<%=Pics %>"/>
                <input id="TB_Tag" type="hidden" value="<%=Tags %>"/>
            </div>
			<div class="main-top">
				<div class="top-tit"><%=LineName %><i style="padding-left:20px"><%=LineFeature %></i>
					<b class="<%=tag %>"></b>
					<span>|</span>
					<i>线路编号：<%=LineId%></i>
				</div>
			</div>
			<!--产品信息-->
			<div class="main-info clearfix">
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
					<!--日历 Bengin-->
					<div id="plan_calendar1" class="plandate_info">
    					<div id="plandate" class="plan_date"></div>
					</div>
					<!--日历 End-->
				</div>
				<div class="info-r fl">
					<div class="price-info">
                        <%if (Convert.ToString(ConfigurationManager.AppSettings["shyhlj200"]).IndexOf("," + LineId + ",") > -1)
                          {%>
                        <%} else{%>
                        <span>青旅价：</span>
                        <div class="cost fl clearfix">
                            <p style="color:#fff;text-decoration:line-through;">
                                <span>¥</span>
                                <span><%=Price%></span>
                            </p>
                        </div>
                        <div id="jia" class="sp fl">起价说明
                            <div class="show jia_box">
                                本起价是可选出发日期中，按双人出行共住一间房核算的最低单人价格（邮轮部分产品可能四人出行共用一间房间）。产品价格会根据您所选择的出发日期、出行人数、入住酒店房型、航班或交通以及所选附加服务的不同而有所差别。
                                <b></b>
                            </div>
                        </div>
						
                        
                        <%if (Convert.ToString(ConfigurationManager.AppSettings["ZhongChouHuoDong"]).IndexOf("," + LineId + ",") > -1){%>
                        <%if (seckillNum != null && seckillNum != ""){ %>
                            <div style="float:right; font: 14px/30px 'Microsoft Yahei'; color: red; width: 110px;">仅余<%=100-System.Int32.Parse(seckillNum)-4%>限额</div>
                            <div style="float:right; font: 14px/30px 'Microsoft Yahei'; color: red; width: 104px;">已有<%=System.Int32.Parse(seckillNum)+4 %>抢购</div>
                        <%}else{ %>
                            <div style="float:right; font: 14px/30px 'Microsoft Yahei'; color: red; width: 110px;">仅余96限额</div>
                            <div style="float:right; font: 14px/30px 'Microsoft Yahei'; color: red; width: 104px;">已有4人抢购</div>
                        <%} %>
                        <%} %>
					</div>
                    <!--
                    <div class="price-info" style="padding-top:0;">
                        <span>促销价：</span>
						<div class="cost fl clearfix">
							<p>
								<span>¥</span>
								<%if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1 && System.Int32.Parse(Price) > 1500)
                                {%>
                                <%if (System.Int32.Parse(Price) > 3000) {%>
                                    <%=System.Int32.Parse(Price)-1000 %>
                                <%}else{ %>
                                    <%=System.Int32.Parse(Price)-500 %>
                                <%} %>
                                <%}else if (Convert.ToString(ConfigurationManager.AppSettings["shyhlj200"]).IndexOf("," + LineId + ",") > -1 && System.Int32.Parse(Price) > 1500)
                                { %>
                                <%=System.Int32.Parse(Price)%>
                                <%}else if (System.Int32.Parse(Price) < 1500){ %>
                                <%=System.Int32.Parse(Price)%>
                                <%}else{
                                    if (System.Int32.Parse(Price) > 3000){%>
                                    <%=System.Int32.Parse(Price) - 500%>
                                    <%}else{ %>
                                    <%=System.Int32.Parse(Price) - 250%>
                                <%}} %>
								<i>起</i>
							</p>
						</div>
                        <span style="font-size:14px; color:#773115;"><%if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1)
                                                                       {%>
                                （建行分期满3000减1000）
                                <%}else if (Convert.ToString(ConfigurationManager.AppSettings["shyhlj200"]).IndexOf("," + LineId + ",") > -1){ %>
                                <%}else{ %>
                                （建行分期满3000减500）
                                <%} %>
                        </span>
						<div id="you" class="sp fl">优惠详情
							<div class="show you_box">
                                <%if (Convert.ToString(ConfigurationManager.AppSettings["specialpfq"]).IndexOf("," + LineId + ",") > -1)
                                  {%>
                                单笔订单金额满3000元并通过建行分期付款方式在线支付成功的，可享3期、6期0手续费并立减1000元/单优惠，具体每期还款金额请以银行网站公布为准！
                                <%}else if (Convert.ToString(ConfigurationManager.AppSettings["shyhlj200"]).IndexOf("," + LineId + ",") > -1){ %>
                                【上海银行美好生活卡专享】持上海银行美好生活卡客人报名此线路，报名立减200元/人（参与本次活动的“美好生活卡”持卡人必须出行，每张“美好生活卡”限购1单，每单最多优惠1人）不得与其它优惠同享！（需用美好生活卡注册会员后可享受优惠：点击免费注册-选择“美好生活卡”客户注册）
                                <%}else{ %>
                                单笔订单金额满3000元并通过建行分期付款方式在线支付成功的，可享3期、6期0手续费并立减500元/单优惠，具体每期还款金额请以银行网站公布为准！
                                <%} %>
								<b></b>
							</div>
						</div>
                        </br>
                        <div id="fen" class="sp fl" style="color:#f00;" hidden>分期：￥<b style="font-size:18px;"><%=Math.Ceiling(System.Double.Parse(Price)/6) %></b> /期 起
                            <div class="show fen_box">
                                <strong style="color:#659006;font-weight:bold;">支持以下分期方式</strong>
                                <p style="color:#93B34C;">............................................................................................</p>
                                <p style="padding-bottom:15px;">首付出发</p>
                                <ul class="payment_box">
                                    <li>期数</li><li>分期价</li><li>含利息</li><li>月利率</li></br>
                                    <li class="actua">6期</li><li class="actua">￥
                                        <%=Math.Ceiling(System.Double.Parse(Price)/6) %></li><li class="actua">0</li><li class="actua">0</li>
                                    <li class="actua">3期</li><li class="actua">￥
                                        <%=Math.Ceiling(System.Double.Parse(Price)/3) %></li><li class="actua">0</li><li class="actua">0</li>
                                </ul></br>
                                <p style="color:#527903;">（以上分期是根据青旅价计算得出，仅作参考）</p>
                                <b></b>
                            </div>
                        </div>
                        <%} %>
                        
                        
                        <%if (Convert.ToString(ConfigurationManager.AppSettings["ZhongChouHuoDong"]).IndexOf("," + LineId + ",") > -1)
                        {%>
                        <%if (seckillNum != null && seckillNum != "")
                        { %>
                            <div style="float:right; font: 16px/30px 'Microsoft Yahei'; color: red; width: 104px;">已<%=System.Int32.Parse(seckillNum) %>抢购</div>
                            <div style="float:right; font: 16px/30px 'Microsoft Yahei'; color: red; width: 104px;">仅剩余<%=100 - System.Int32.Parse(seckillNum)%>限额</div>  
                        <%}
                        else
                        { %>
                            <div style="float:right; font: 16px/30px 'Microsoft Yahei'; color: red; width: 104px;">已0人抢购</div>
                            <div style="float:right; font: 16px/30px 'Microsoft Yahei'; color: red; width: 104px;">仅剩余100限额</div>  
                        <%}
                        }%>
                    </div>
                    -->
					<div class="line-info">
                        <img id="QRCode" style="position:absolute; right:15px; WIDTH: 80px; HEIGHT: 80px; CURSOR: pointer" src="/newpage/pay/MakeQRCode.aspx?data=http%3a%2f%2fwww.scyts.com%2fapp%2fline%3f<%=LineId%>" alt="" >
                        <div class="wx-text" style="position:absolute; right:15px; top:110px; width:80px; line-height: 16px; text-align:center;">扫描此二维码</br>手机查看线路</div>
						<dl>
							<span class="list-name">线路编号：</span>
							<em><%=LineId%></em>
						</dl>
						<dl>
							<span class="list-name">出游天数：</span>
							<em><%=LineDays %>天</em>
						</dl>
						<dl>
							<span class="list-name">发团日期：</span>
							<em>详见发团日历表</em>
						</dl>
						<dl class="sale-info clearfix">
							<span class="cf60 fl ">优惠信息：</span>
							<em class="cf60 fl"><%=Preference %></em>
						</dl>
                        <%--<dl>
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
                        </dl>--%>
                        
						<dl class="btn-box">
							<a class="btn" href="#order-info"></a>
                            <div class="collect">
								<a href="javascript:;" onclick="AddToFavoriteLine()">收藏
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
							<b></b>
						</h4>
						<ul>
							<li><%=RouteFeature%></li>
						</ul>
					</div>
					<div class="hotline">4006-777-666</div>
				</div>
			</div>
            <div class="clearfix"></div>
			<!--产品选择-->
			<div id="order-info" class="buy-content">
				<div class="resource-box clearfix">
					<dl class="fl clearfix">
						<dd class="t1">出行日期</dd>
						<dd class="t2">
							<span class="dynamic"></span>
							<b></b>
							<p id="select_plandate"></p>
						</dd>
						<dd class="t3">成人</dd>
                        <dd>
                            <select id="adults" name="adults" class="select2me">
                                <option value="1">1</option>
                                <option value="2" selected="selected">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                            </select>
                        </dd>
						<dd class="t3">儿童</dd>
                        <dd>
                            <select id="childs" name="childs" class="select2me">
                                <option value="0">0</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                            </select>
                        </dd>
					</dl>
					<div class="settle fl clearfix">
						<h4>总价</h4>
						<div class="all">¥<span id="AllPrice">0</span></div>
					</div>
					<div class="buy-now fl">
						<a id="OrderNow" href="javascript:;" onclick="Order()"></a>
					</div>
				</div>
				<div class="resource-wrap">
					<div class="all-resource">
						<div class="selectable clearfix">
                            <div id="loading">
                                <img src="../image/ajax-loading.gif" />
                                <span class="priceloading">正在查询价格，请稍候...</span>
                                <div class='wire'></div>
                            </div>
                            <form action="#" id="submit_form">
                                <div id="inputs" style="DISPLAY:none">
                                    <input name="TB_LineId" id="TB_LineId" type="hidden" value="<%=LineId %>"/>
                                    <input name="TB_LineName" id="TB_LineName" type="hidden" value="<%=LineName %>"/>
                                    <input name="TB_Price" id="TB_Price" type="hidden" value="<%=Price %>"/>
                                    <input name="planid" id="planid" type="hidden" />
                                    <input name="begindate" id="begindate" type="hidden" />
                                    <input id="TB_ProductFlag" type="hidden" value="line"/>
                                    <input id="TB_Sale" type="hidden" value="<%=Sale %>"/>
                                    <input id="isSeckill" type="hidden" value="<%=isSeckill %>"/>
                                    <input id="canSale" type="hidden" value="<%=canSale %>"/>
                                    <input id="TB_Seckill" type="hidden" value="<%=ConfigurationManager.AppSettings["seckill"].ToString() %>"/>
                                </div>
                                <div id="price_list">
                            
                                </div>
                            </form>
                            <h4>费用包含</h4>
							<div class="select-content fl clearfix">
								<div class="explain fl">
									<ul>
										<%=PriceIn %>
									</ul>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
            <!--Tab-->
            <div id="state_content" class="detail-tab-box">
				<div id="js_tab" class="detail-tab clearfix">
                    <ul id="nav">
                        <li class="green"><a href="#xcap">行程安排</a></li>
                        <li><a href="#fysm">费用说明</a></li>
                        <%=VisaFileHide%>
                        <li><a href="#ydxz">预定须知</a></li>
                    </ul>
					<div class="buy-now-fixed" id="nav_order">
                        <a class="print" style="cursor: pointer;">打印行程单</a>
                        <a id="orderinfo" href="#order-info"></a>
					</div>
				</div>
			</div>
            <!--选择行程打印日期-->
            <div class="choose-data-box">
                <dl class="fl clearfix">
                    <dd class="t1">出行日期</dd>
                    <dd class="t2 clearfix">
                        <span id="get-toggle" class="get-data"></span>
                        <b></b>
                        <p id="inData"></p>
                    </dd>
                    <a class="sure" target="_blank">确定</a>
                    <div class="off-data">X</div>
                </dl>
            </div>
            <!--END-->
            <div class="clearfix"></div>
            <div class="under-tab-box" style="padding-top:25px">
                <div class="routecontent">
                    <div class="left-col">
                        <div id="xcap" class="present">行程安排</div>
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
					<div class="expense">费用说明</div>
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
					<div class="qz-box">签证信息</div>
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
					<div class="yd-box">预定须知</div>
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
		<div class="side-content rl">
			<h4><span></span>其他人正在看</h4>
			<div class="list">
				<ul>
                    <%=TravelOnline.NewPage.Class.CacheClass.GuessYouLike(lineclass.ToString(),5)%>
				</ul>	
			</div>
		</div>
	</div>


    <!-- wmc 经典效果 start-->
     <style>
        .extend_spot_title ,.extend_spot_content{
        width:300px;
        margin:0 auto;
        text-align:left;
        font-size:12px;
        color:#666;
        font-family:"Microsoft YaHei";
        margin-left:35px;
        padding-right:30px;
      
      
      
    
        }

        .extend_spot_title {
        line-height:35px;
        font-size:18px;
        font-weight:bold;
        margin-top:5px;
        border-bottom:1px solid #EEE;
        margin-bottom:5px;
        padding-bottom:5px;
          padding-top:30px;
        }
        .extend_spot_content {
          line-height:18px;
        margin-bottom:5px;
        height:340px;
        overflow-y:scroll;
        line-height:20px;
        word-break: break-all;
        word-wrap: break-word;

        text-align:justify;
        }

        .blockPage{
        overflow:hidden;
       border-radius:10px;
        box-shadow: 0px 15px 15px #000;
      
        }

         

        .extend_spot_left {
        float:left;
        width:65%;
        overflow:hidden;
         background:#000;
        }

        .extend_spot_right {
                overflow:hidden;
              float:left;
            width:35%;
        }

         .extend_spot_a {
         cursor:pointer;
         }
    </style>
      <div id="extend_spotview" style="display:none;width:1000px;overflow:hidden;">

        <div class="extend_spot_left">
            <div id="galleria" class="extend_spot_img">
              

            </div>
        </div>
        <div class="extend_spot_right">
            <div class="extend_spot_title"></div>
            <div class="extend_spot_content"></div>
        </div>
        </div>

       <!-- wmc 经典效果 end-->


	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
    <script type="text/javascript">
        <%=PlanDateJason%>
        var ShowMonthNum = 1;
        $(document).ready(function () {
            HistoryRecord();
            $('select').select2({ minimumResultsForSearch: -1 });

            $("#adults,#childs").on("change", function (e) {
                $("#loading").show();
                $("#price_list").hide();
                setTimeout(function () { Loading() }, 500)
            })
            $("#price_list .select2me").live("change", function (e) {
                CountPrice();
            })
            $("#loading").hide();
            $("#plandate").showRenderCalendar({ events: eval(json), showNum: ShowMonthNum });
            initTips();
            PlanDropDownListBind();

            $("#state_content").smartFloat();
            $("#js_route_days").pin({ containerSelector: ".routecontent", padding: { top: 100, bottom: 50 } });
            $('#nav').onePageNav({ scrollOffset: 50, scrollThreshold: 0.1, currentClass: 'green' });
            $('#nav_days').onePageNavNew({ scrollOffset: 50, scrollThreshold: 0.1, currentClass: 'pink' });

            var isSale = $("#TB_Sale").val();
            if (isSale == 1) {
                $("[href='#order-info']").removeAttr("href").addClass('act-off');
                $("#OrderNow").removeAttr("onclick").hide();
            }
            var isSeckill = $('#isSeckill').val();
            var canSale = $('#canSale').val();

            if (isSeckill == 1) {
                if (canSale == 0) {
                    $("[href='#order-info']").addClass('act-ready').removeAttr("href").removeClass('act-off');
                    $("#OrderNow").removeAttr("onclick").hide();
                }
            }

        })

        function LoadPrice(planid, begindate) {
            $("#planid").val(planid);
            $("#begindate").val(begindate);
            $("#loading").show();
            $("#price_list").html("");
            $("#price_list").hide();
            setTimeout(function () { Loading() }, 500)
        }

        function Loading() {
            var url = "../NewPage/AjaxService.aspx?action=LoadPrice&lineid=" + $("#TB_LineId").val() + "&planid=" + $("#planid").val() +
                      "&begindate=" + $("#begindate").val() + "&adults=" + $("#adults").val() + "&childs=" + $("#childs").val() + "&r=" + Math.random();
            $.get(url, function (data) {
                $("#loading").hide();
                $("#price_list").show();
                $("#price_list").html(data);
                $("#state_content").smartFloat();
                $("#js_route_days").pin({ containerSelector: ".routecontent", padding: { top: 100, bottom: 50 } });
                $('select').select2({ minimumResultsForSearch: -1 });
                CountPrice();
            });
        }

        function CountPrice() {
            var PriceCount = 0;
            $("#price_list dl").each(function () {
                var id = $(this).attr("tag");
                var nums = Number($(this).find("#p9_" + id).val());
                var price = Number($(this).find("#p5_" + id).val());
                if (nums > 0) {
                    $(this).find(".sure").addClass("on");
                    $(this).find("#p10_" + id).html("￥" + (nums * price));
                    PriceCount += nums * price;
                }
                else {
                    $(this).find(".sure").removeClass("on");
                    $(this).find("#p10_" + id).html("- -")
                }
            });
            $("#AllPrice").html(PriceCount);
        }


        function Order() {
            if ($("#price_list").html() == "") {
                alert("请选择费用后再预订！");
                return;
            }
            var adults = Number($("#adults").val());
            var childs = Number($("#childs").val());
            var nums = adults + childs;
            var selnums = 0;
            for (i = 0; i < $("input[name='p1']").length; i++) {
                if ($("input[name='p1']:eq(" + i + ")").val() == "SellPrice") selnums += Number($("select[name='p9']:eq(" + i + ")").val());
            }
            if (nums != selnums) {
                alert("基本团费人数与预订人数不符，请检查！");
                return;
            }
            $("#OrderNow").removeAttr("onclick");
            var url = "/newpage/AjaxService.aspx?action=SaveOrderInfo&adults=" + $("#adults").val() + "&childs=" + $("#childs").val() + "&AllPrice=" + $("#AllPrice").html() + "&r=" + Math.random();
            $.post(url, $("#submit_form").serialize(), function (data) {
                var obj = eval(data);
                if (obj.success) {
                    top.location = "/secondstep.html?orderid=" + obj.success;
                }
                else {
                    alert(obj.error);
                    $("#OrderNow").attr("onclick", "Order()");
                }
            });
        }

        function AddToFavoriteLine() {
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
                    alert(date.content);
                }
                else {
                    alert("收藏失败，请稍后再试!");
                }
            })
        }
	</script>



      <script>

        var test_data = { viewname: "测试1", intro: "fdfasfsdafsafsafsfsafsdafsafsdafdsadfsdafsdafsdafsdafsadfsdafsafsdafsadfsad", pics:  "http://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Bowling_Balls_Beach_2_edit.jpg/800px-Bowling_Balls_Beach_2_edit.jpg,http://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Interior_convento_3.jpg/800px-Interior_convento_3.jpg" };



        //替换所有
        function extend_init_spot(element) {
      
            $("." + element).each(function (index, obj) {
                var spot_arr = [];
                var pattern = /\【(.+?)\】/g;

                var spot_str_arr = $(obj).html().match(pattern);
                if (spot_str_arr != null) {
                    for (var i = 0; i < spot_str_arr.length; i++) {
                 
                        var param = (spot_str_arr[i].replace(/【/g, "").replace(/】/g, ""));
                     
                       
                        $(obj).html($(obj).html().replace(spot_str_arr[i], "<a class='extend_spot_a' onclick='extend_spot(this)' spotname='" + param + "'>" + spot_str_arr[i] + "</a>"));
                     //  $(obj).html().replace(pattern2, "<a onclick='extend_show_spot(this)' spotname='" + param + "'>" + spot_str_arr[i] + "</a>");
                    }

                }

            });
          

        }

        function extend_spot(obj) {
            var spot_name = $(obj).attr("spotname");
        
            extend_load_spot(spot_name);
            //初始化图片
       
        }

        function extend_load_spot(name) {
           
            if (name == ""||name==null) {
                return;
            }
            var url = "/newpage/AjaxService.aspx?action=showScenic&scenicName=" + name;
            //提交
            $.ajax({
                url: url,
                cache: true,
                type: "get",
               // data: JSON.stringify({ model: { LoginName: LoginName, PassWord: PassWord } }),
                contentType: 'application/json; charset=utf-8',  //must
                dataType: "text",
                success: function (data) {
                 
                    if (data != "" && data != null) {
                        var json_data = $.parseJSON(data);
                        //初始化图片
                        extend_set_spot(json_data);
                      
                    }
                   
                }
            });

        }



        function extend_show_spot() {
            var width = $("#extend_spotview").width();
            var height = $("#extend_spotview").height();
            $.blockUI({
                message: $("#extend_spotview"),
                css: {
                    border: '1px solid black',
                    width: width + 'px',
                    height: height + 'px',
                    left: ($(window).width() - width) / 2 + 'px',
                    top: ($(window).height() - height) / 2 + 'px',
                  
                    cursor: "default"
                },
                baseZ:10000
            });
            $('.blockOverlay').click(extend_close_spot);
        }


        function extend_set_spot(data) {
            $(".extend_spot_img").html("");

            //标题
            $(".extend_spot_title").html(data.viewname);
            //名称
            $(".extend_spot_content").html(data.intro);
            if (data.pics != null && data.pics != "") {
            var img_arr = data.pics.split(",");

            var extend_spot_data = [];
            //图片
            $(img_arr).each(function (index, obj) {
                if (obj != null && obj != "") {
                 //   $(".extend_spot_img").append(extend_set_spotitem(index, obj));
                    extend_spot_data.push(extend_set_spotitem(index,obj));
                }
            });
            extend_event_spot(extend_spot_data);
            }
            extend_show_spot();
        }

        function extend_set_spotitem(index, obj) {
            //var result = "";
            //result += "<a href='" + obj + "'> <img src='" + obj + "'  data-big='" + obj + "'   >   </a>";
            //return result;

            return { image: obj, big: obj };
        }

      
        function extend_event_spot(data) {
         
       
            Galleria.loadTheme('/newjs/galleria/galleria.classic.js');
            $("#galleria").height(500);
            // $("#galleria").galleria();
        
          
            Galleria.run('#galleria', { dataSource: data });

             
        }

        function extend_close_spot() {
          //  $("#extend_spotview").html("");
            $.unblockUI();
        }

   

        function changePage(url) {


            $.mobile.changePage("test2", {
                transition: "slide"

            });

        }
        extend_init_spot("day-content");
    </script>
    
</body>
</html>