<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisaDetail.aspx.cs" Inherits="TravelOnline.NewPage.VisaDetail" %>
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
        .info-r .recommend-box {height: 219px;}
        .detail-tab-box { background:#ffffff; z-index:9999;}
        #js_route_days { background:#ffffff; z-index:1;}
        .detail-tab .green a{background: #8ab923;color: #fff;}
        .under-tab-box .data-detail .pink a{border: 1px solid #ff6666; background: #ff6666; color: #fff;}
        .routecontent {display:table;}
        .left-col {width: 120px; float: left;}
        .right-col {width: 810px; float: left;}
        .hide {display: none;}
        .info-r .line-info .sale-info {height: 40px; overflow: hidden;}
        .info-r .line-info .btn-box {position: relative; height: 60px;}
        .info-r .line-info .btn-box .collect {display: inline; box-shadow: 0 0 2px #bbb; position: relative; float: left; margin: 10px 8px 20px 0; width: 70px; height: 22px; border: 1px solid #ccc;}
        .info-r .line-info .btn-box .line {position: absolute; left: -26px; top: 60px; width: 417px; height: 1px; background: #dcdcdc;}
        .buy-content .t5,.select-num .choose {position: relative; padding-left: 7px; width: 37px; height: 28px; line-height: 30px; border: 1px solid #ccc; font-size: 16px; cursor: pointer;}
        .buy-content .t5 p,.choose .show-num {display: none; position: absolute; left: -1px; top: 28px; z-index: 50; padding: 3px 0; width: 44px; border: 1px solid #ccc; background: #fff;}
        .buy-content .t5 a,.choose .show-num a {display: block; padding-left: 7px; height: 20px; line-height: 20px;} 
        .buy-content .t5 a:hover,.choose .show-num a:hover {background: #7daf0f; color: #fff;}
        .buy-content .t5 b {background: url('../image/detail/pro_ui.png') no-repeat;position: absolute; z-index: 20; background-position: 0 -257px; top: 11px; right: 7px; width: 8px; height: 8px;}
        .buy-content .t2,.buy-content .t2 p {width: 280px;}
        .selectdate {background: #f8f4c0;}
        .showCalendarPanel table {height: 220px; }
        .side-content .list a .linename{line-height:30px;height:30px;overflow:hidden;}
        <%--.select-content dd .con1 {width: 600px;}
        .select-content dd {border-bottom: 1px dotted #F2F1F1;}
        .select-content dd .con1 span {font-weight:bold}--%>
        #loading {text-align:center}
        .priceloading{ font-size:14px;font-weight:bold;padding-left:30px;}
        #order-info{display:table;}
        .select2me {width: 60px;}
        #nav_order a.print {
            padding-left: 20px;
            background: url('/image/dayin.png') no-repeat left 50%;
            color: #5e74a6;
        }
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
        <div id="inputs" style="DISPLAY:none">
            <form action="#" id="submit_form">
                <input name="TB_LineId" id="TB_LineId" type="hidden" value="<%=LineId %>"/>
                <input name="TB_LineName" id="TB_LineName" type="hidden" value="<%=LineName %>"/>
                <input name="TB_Price" id="TB_Price" type="hidden" value="<%=Price %>"/>
                <input name="TB_Pic" id="TB_Pic" type="hidden" value="<%=FirstImg %>"/>
                <input name="TB_ProductFlag" id="TB_ProductFlag" type="hidden" value="visa"/>
                <input name="planid" id="planid" type="hidden" value="0"/>
                <input name="begindate" id="begindate" type="hidden" />
            </form>
            <input name="TB_Tag" id="TB_Tag" type="hidden" value="<div class='trip-class qz'>签证</div>"/>
        </div>
		<div class="link-nav">
				<a href="/index.html">首页</a>
				<span>></span>
                <%=BreadCrumb %>
		</div>
		<div class="main-content fl">
			<!--产品名称-->
			<!--产品信息-->
			<div class="main-info ">
			    <div class="info-b">
                    <div class="visa-info-l fl">
                		<div id="flag"><img src="<%=FirstImg %>" width="160" height="106"></div>
                        <div class="executive">
                        	<h4>受理范围：</h4>
                            <p>适用于长期工作地为（ 安徽 、 江苏 、 浙江 、上海 ）的申请人</p>
                        </div>
                    </div><!--左边-->              
                    <div class="visa-info-r fl">
                   	  <div id="visa-tit"><%=LineName%><i>线路编号：<%=LineId %></i></div>
                        <div id="visa-price"><p><span>¥</span><%=Price %></p></div>
                        <div class="line-info">
                        	<dl>
                            	<dd>办理说明：<span><%=LineFeature%></span></dd>
                            </dl>
                            <dl>
                            	<dd>停留时间：<span><%=Visa_1 %></span></dd>
                                <dd>有效期：<span><%=Visa_2 %></span></dd>
                            </dl>
                            <dl>
                            	<dd>办理时长：<span><%=Visa_3 %></span></dd>
                            </dl>
                        </div>
                      <div class="visa-select clearfix" id="order-info">
                        	<dl>
                            	<dd class="t1">出行人数</dd>
                            	<dd>
                                	<select id="adults" name="adults" class="select2me">
                                        <option value="0">0</option>
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
                                <dd class="t2">出行日期</dd>
                                <dd class="t3"><input id="Wdate" class="Wdate" type="text" onClick="WdatePicker({minDate:new Date()})"></dd>
                            </dl>
                        </div>
                        <dl class="btn-box">
						    <a class="btn" onclick="Order()"></a>
							<div id="addcollect" class="collect">
								<a  href="#order-info">收藏
									<b></b>
								</a>
							</div>
							<!--<div id="shareItem" class="collect relative-box">
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
							</div>-->
						</dl>
                      <div class="hottel"><p>咨询热线<span>4006-777-666</span></p></div>
                  </div><!--右边-->  
                    <div class="flow fl"></div>
                  <div class="clear"></div>
                </div>
			</div>
            <!--Tab-->
			<div id="state_content" class="detail-tab-box">
				<div id="js_tab" class="detail-tab clearfix">
					<ul id="nav">
                        <li class="green"><a href="#sxcl">所需材料</a></li>
                        <li><a href="#ydxz">预定须知</a></li>
                    </ul>
					<div class="buy-now-fixed">
						<a id="orderinfo" href="#"></a>
					</div>
				</div>
			</div>
			<div class="under-tab-box">
                <div id="sxcl" class="material">所需材料</div>
				<div class="detail_content">
					<div class="material-tab">
                    	<ul>
                        	<li class="cur">身份证明</li>
                            <li class="<%=Visa2Hide %>">资产证明</li>
                            <li class="<%=Visa3Hide %>">工作证明</li>
                            <li class="<%=Visa4Hide %>">学生及儿童</li>
                            <li class="<%=Visa5Hide %>">老人</li>
                            <li class="<%=Visa6Hide %>">其他</li>
                        </ul>
                        <div class="material-content">
                        	<div id="zaizhi">
                            	<dl class="clearfix">
                                	<%=Visa1%>
                                </dl>
                            </div>
                            <div id="ziyou" class="hide">
                            	<dl class="clearfix">
                                	<%=Visa2%>
                                </dl>
                            </div>
                            <div id="tuixiu" class="hide">
                            	<dl class="clearfix">
                                	<%=Visa3%>
                                </dl>
                            </div>
                            <div id="wuye" class="hide">
                            	<dl class="clearfix">
                                	<%=Visa4%>
                                </dl>
                            </div>
                            <div id="shiba" class="hide">
                            	<dl class="clearfix">
                                	<%=Visa5%>
                                </dl>
                            </div>
                            <div id="shibax"class="hide">
                            	<dl class="clearfix">
                                	<%=Visa6%>
                                </dl>
                            </div>
                        </div>
                    </div>
                    <div class="send-mail">您可以将签证材料复印件发送至<a href="mailto:visa@scyts.com">visa@scyts.com</a></div>
				</div>
				<div class="line"></div>
				<div class="detail_content top25">
					<div id="ydxz" class="yd-box">预定须知</div>
					<div class="ydxz-list">
						<div class="pattern">签证须知</div>
						<div class="pattern-length">
                            <ul>
                            	<li>请务必确保您的护照在归国后仍有半年以上有效期，并且至少留有两页空白签证页；</li>
                                <li>以上签证价格、所需材料，若领馆临时变更，以领馆变更为准；</li>
                                <li>办理时长指领馆正常受理工作日（领馆节假日随各国情况有所差异）往返快递时间另计；</li>
                                <li>准备好材料后可以亲自前往分公司或快递材料至分公司（具体地址客服会告知），请在快递材料时注明您的订单号和办理的国家签证类型；</li>
                                <li>您的签证申请是否成功，完全由该国的签证官根据您递交的申请材料独立判断，本公司不得以任何方式的干预或交涉；本公司重申，在任何情况下，本公司都不承担由签证申请结果而导致被追溯任何赔偿的责任和义务；</li>
                                <li>有关签证资料上公布的签证有效期和停留天数，仅做参考而非任何法定承诺，一切均以签证官签发的签证内容，为唯一依据。</li>
                                <li>使馆保留要求申请人补资料或要求申请人前往使馆面试的权利；</li>
                                <li>请您待拿到签证及护照后再出机票或与酒店确认付款，避免不必要的损失；</li>
                            </ul>
						</div>
                        <div class="pattern <%=hide %>">特别提醒</div>
						<div class="pattern-length <%=hide %>">
                            <ul>
                            	<li><%=RouteFeature %></li>
                            </ul>
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
	<!--正文内容End-->
	<!--页尾Begin-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--页尾End-->
    <script type="text/javascript">
        $(document).ready(function () {
            HistoryRecord();
            $('select').select2({ minimumResultsForSearch: -1 });
            $("#orderinfo").hide();
            $("#state_content").smartFloat();
            $('#nav').onePageNav({ scrollOffset: 50, scrollThreshold: 0.1, currentClass: 'green' });
        })

        function Order() {
            if ($("#Wdate").val() == "") {
                alert("请选择出行日期后再预订！");
                return;
            }
            var begindate = $("#Wdate").val();
            $("#begindate").val(begindate);
            var adults = Number($("#adults").val());
            var price = Number($("#TB_Price").val());
            var allprice = adults * price;
            $("#OrderNow").removeAttr("onclick");
            var url = "/newpage/AjaxService.aspx?action=SaveOrderInfo&adults=" + $("#adults").val() + "&childs=0&AllPrice=" + allprice + "&r=" + Math.random();
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
	</script>
</body>
</html>