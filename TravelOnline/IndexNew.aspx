<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexNew.aspx.cs" Inherits="TravelOnline.IndexNew" %>
<%@ Register src="NewPage/IndexHeader.ascx" tagname="Header" tagprefix="uc1" %>
<%@ Register src="NewPage/IndexFooter.ascx" tagname="Footer" tagprefix="uc2" %>
<%@ Register src="NewPage/LeftSlide.ascx" tagname="LeftSlide" tagprefix="uc3" %>
<%@ Register src="NewPage/IndexHot.ascx" tagname="IndexHot" tagprefix="uc4" %>
<!DOCTYPE HTML>
<html>
<head>
	<title><% =TravelOnline.Class.Common.PublicPageKeyWords.PublicTitle %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1">
    <meta name="description" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicDescription %> />
    <meta name="keywords" content=<%=TravelOnline.Class.Common.PublicPageKeyWords.PublicKeywords %> />
    <meta property="qc:admins" content="111753216663314363757" /> 
    <link rel="shortcut icon" href="">
    <link href="newcss/common.css" rel="stylesheet" type="text/css" />
    <link href="newcss/temphefot.css" rel="stylesheet" type="text/css" />
    <link href="newcss/index.css" rel="stylesheet" type="text/css" />
    <link href="newcss/page.css" rel="stylesheet" type="text/css" />
    <!--2017年3月更新css-->
    <link href="newcss/newindex.css" rel="stylesheet" type="text/css" />
    <script src="newjs/jquery-1.7.2.min.js"></script>
	<script type="text/javascript" src="newjs/My97DatePicker/WdatePicker.js"></script>
	<script type="text/javascript" src="newjs/jquery.cookie.js"></script>
    <style>.wrap .index-spring .index-spring-l .pro_list ul dl dt img,.wrap .index-top .top-sale .product_list li dl dt a img,.wrap .index-trip .product-wrap .product-list li dl dt a img {width: 100%;height:150px}
    </style>
	<!--[IF IE 6]>
		<script type="text/javascript" src="newjs/iepng.js"></script>
		<script type="text/javascript">
		DD_belatedPNG.fix("*");
		document.execCommand("BackgroundImageCache", false, true);
		</script>
	<![ENDIF]-->
    <script type="text/javascript">
        $(function () {
            ShowIndex_Ad_Slide();
            ShowIndex_Ad_Banner();
            ShowIndex_Line_Sell();
            ShowIndex_Ad_Season();
            ShowIndex_Line_Season();
            ShowIndex_Line_TabByIndex_FreeTour();
            ShowIndex_Line_ListByIndex_FreeTour();
            ShowIndex_Line_TabByIndex_Outbound();
            ShowIndex_Line_ListByIndex_Outbound();
            ShowIndex_Line_TabByIndex_Inland();
            ShowIndex_Line_ListByIndex_Inland();
            ShowIndex_OtherLine_List();
            ShowIndex_OtherTab();
            ShowIndex_Visa();
            //ShowIndex_Famous();
        });

        function ShowIndex_Ad_Slide() {
            var data = eval(<%=GetIndex_Ad_Slide("N_Index_Slide")%>);
            if (data != null) {
                $("#Index_Ad_Slide").html(formatterIndex_Ad_Slide(data));
            }
        }
        function formatterIndex_Ad_Slide(data) {
            var html1 = "<div id=\"indexTopbanner\" class=\"banner\"><ul class=\"clearfix\">";
            var html2 = "<div id=\"bannerGoods\" class=\"bannertxt absolute-box\"><ul class=\"clearfix\">";
            $(data.rows).each(function (index, obj) {
                html1 += "<li " + obj.styles + "><a href=\"" + obj.AdPageUrl + "\" target=\"_blank\"><img src=\"" + obj.AdPicUrl + "\" alt=\"" + obj.AdName + "\" title=\"" + obj.AdName + "\" /></a></li>";
                html2 += "<li " + obj.styles + "><p>" + obj.AdName + "</p></li>";
            });
            html1 += "</ul></div>";
            html2 += "</ul></div>";
            var html = html1 + html2;
            return html;
        }

        function ShowIndex_Line_Sell() {
            var data = eval(<%=GetIndex_Line_Sell("Index_Sell",1)%>);
            if (data != null) {
                $("#Index_Line_Sell").html(formatterIndex_Line_Sell(data));
            }
        }
        function formatterIndex_Line_Sell(data) {
            var html = ""
            var i=0;
            html += "<ul id=\"vic\" class=\"product-list worth-list current fl clearfix\" style=\"position:absolute;\">";
            
            $(data.rows).each(function (index, obj) {
                if(obj.total>5||obj.rowIndex%5==0){
                    html += "<div class=\"doc clearfix\">";
                }
                html += "<li><dl><dt class=\"relative-box\"><a href=\"/line/" + obj.Lineid + ".html\" target=\"_blank\"><img src=\"" + obj.Pics + "\" alt=\"" + obj.LineName + "\" title=\"" + obj.LineName + "\"></a>";
                html += "<p class=\"absolute-box fl clearfix\"><b class=\"cost rl\"><span>¥</span>" + obj.Price + "<i>起</i></b></p></dt>";
                html += "<dd><a href=\"/line/" + obj.Lineid + ".html\" target=\"_blank\">" + obj.LineName + "</a></dd></dl></li>";
                i++;
                if(i%5==0){
                    html += "</div>";
                }
            });
            html += "</ul>";
            return html;
        }

        function ShowIndex_Ad_Banner() {
            var data = eval(<%=GetIndex_Ad_Banner("N_Index_Banner")%>);
            if (data != null) {
                $("#Index_Ad_Banner").html(formatterIndex_Ad_Banner(data));
            }
        }
        function formatterIndex_Ad_Banner(data) {
            var html = "";
            $(data.rows).each(function (index, obj) {
                html += "<div class='under-ad'><a href=\"" + obj.AdPageUrl + "\" target=\"_blank\"><img src=\"" + obj.AdPicUrl + "\" alt=\"" + obj.AdName + "\" title=\"" + obj.AdName + "\"></a></div>";
            });
            return html;
        }

        function ShowIndex_Ad_Season() {
            var data = eval(<%=GetIndex_Ad_Season("N_Index_Season")%>);
            if (data != null) {
                $("#Index_Ad_Season").html(formatterIndex_Ad_Season(data));
            }
        }
        function formatterIndex_Ad_Season(data) {
            var html = "<a href=\"" + data.rows[0].AdPageUrl + "\" target=\"_blank\"><img src=\"" + data.rows[0].AdPicUrl + "\" alt=\"" + data.rows[0].AdName + "\" title=\"" + data.rows[0].AdName + "\"></a>";
            return html;
        }

        function ShowIndex_Line_Season() {
            var data = eval(<%=GetIndex_Line_Season("Index_Season")%>);
            if (data != null) {
                $("#pro_mrTop").html(formatterIndex_Line_Season(data));
            }
        }
        function formatterIndex_Line_Season(data) {
            var html = "<li class=\"clearfix\">";
            $(data.rows).each(function (index, obj) {
                html += "<dl " + obj.styles + ">";
                html += "<dt><a href=\"/line/" + obj.Lineid + ".html\" title=\"" + obj.LineName + "\" target=\"_blank\"><img src=\"" + obj.Pics + "\" alt=\"" + obj.LineName + "\" /></a></dt>";
                html += "<dd class=\"tit\"><a href=\"/line/" + obj.Lineid + ".html\" title=\"" + obj.LineName + "\" target=\"_blank\">" + obj.LineName + "</a></dd>";
                //html += "<p>" + obj.LineFeature + "</p>";
                html += "<p></p>";
                html += "<div class=\"msg clearfix\">";
                html += "<div class=\"theme " + obj._planstyle + "\">" + obj.tname + "</div>";
                html += "<div class=\"price\"><span>¥</span>" + obj.Price + " <b>起</b></div>";
                html += "</div></dl>";
                if (obj.indexJ == 3 && obj.rowIndex != data.total - 1) {
                    html += "<li class=\"clearfix\">";
                }
            });
            html += "</li>";
            return html;
        }

        function ShowIndex_Line_TabByIndex_FreeTour() {
            var html='<h2 class="relative-box fl">玩转自由行</h2>';
            html += '<ul id="chujing_area" class="fl clearfix">';
            var data = eval(<%=GetIndex_Line_Tab("Index_FreeTour")%>);
            if (data != null) {
                html += formatterIndex_Line_Tab(data);
            }
            html += '<a href="/freetour.html" class="pro-all" target="_blank">更多自由行产品>></a></ul>';
            $("#Index_FreeTourToIndex_Line_Tab").html(html);
        }
        function ShowIndex_Line_TabByIndex_Outbound() {
            var html = '<h2 class="relative-box fl">出境旅游</h2>';
            html+='<ul id="chujing_area" class="fl clearfix">';
            var data = eval(<%=GetIndex_Line_Tab("Index_Outbound")%>);
            if (data != null) {
                html += formatterIndex_Line_Tab(data);
            }
            html += '<a href="/outbound.html" class="pro-all" target="_blank">更多出境游产品>></a></ul>';
            $("#Index_OutboundToIndex_Line_Tab").html(html);
        }
        function ShowIndex_Line_TabByIndex_Inland() {
            var html='<h2 class="relative-box fl">国内旅游</h2>';
            html+='<ul id="guonei_area" class="fl relative-box clearfix">';
            var data=eval(<%=GetIndex_Line_Tab("Index_Inland")%>);
            if(data!=null){
                html += formatterIndex_Line_Tab(data);
            }
            html += '<a href="/inland.html" class="pro-all" target="_blank">更多国内游产品>></a></ul>';
            $("#Index_InlandToIndex_Line_Tab").html(html);
        }
        function formatterIndex_Line_Tab(data) {
            var html = "";
            $(data.rows).each(function (index, obj) {
                html += "<li " + obj.styles + ">" + obj.Cname + "</li>";
            });
            return html;
        }

        function ShowIndex_Line_ListByIndex_FreeTour() {
            var html = '';
            var data = eval(<%=GetIndex_Line_List("Index_FreeTour")%>);
            if (data != null) {
                html += formatterIndex_Line_List(data);
            }
            $("#Index_FreeTourToIndex_Line_List").html(html);
        }
        function ShowIndex_Line_ListByIndex_Outbound() {
            var html = '';
            var data = eval(<%=GetIndex_Line_List("Index_Outbound")%>);
            if (data != null) {
                html += formatterIndex_Line_List(data);
            }
            $("#Index_OutboundToIndex_Line_List").html(html);
        }
        function ShowIndex_Line_ListByIndex_Inland() {
            var html = '';
            var data = eval(<%=GetIndex_Line_List("Index_Inland")%>);
            if (data != null) {
                html += formatterIndex_Line_List(data);
            }
            $("#Index_InlandToIndex_Line_List").html(html);
        }

        function formatterIndex_Line_List(data) {
            var html = "";
            $(data.rows).each(function (index, obj) {
                html += "<ul id=\"cjProduct_img\" class=\"product-list " + obj.styles + " fl clearfix\">";
                $(data.childRows).each(function (idx, obj2) {
                    if (obj2._parentId == obj.Id)
                    {
                        if (obj2.rowIndex < 4) {
                            html += "<li " + obj2.style1 + "><dl><dt class=\"relative-box\">";
                            html += "<a href=\"/line/" + obj2.Lineid + ".html\" target=\"_blank\"><img src=\"" + obj2.Pics + "\" alt=\"" + obj2.LineName + "\" title=\"" + obj2.LineName + "\"></a>";
                            html += "<p class=\"absolute-box fl clearfix\">";
                            html += "<span class=\"fl\" title=\"" + obj2.tname + "\">" + obj2.tname + "</span>";
                            html += "<b class=\"cost rl\"><span>¥</span>" + obj2.Price + "<i>起</i></b></p>";
                            html += "</dt><dd><a href=\"/line/" + obj2.Lineid + ".html\" target=\"_blank\">" + obj2.LineName + "</a></dd>";
                            html += "</dl></li>";
                        } else {
                            if (obj2.rowIndex == 4) {
                                html += "<div class=\"line fl\"></div><div class=\"details fl clearfix relative-box\"><div class=\"details-l fl\">";
                            }
                            if (obj2.rowIndex == 7) {
                                html += "</div><div class=\"details-m fl\"></div><div class=\"details-l fl\">";
                            }
                            html += "<a class=\"column clearfix\" href=\"/line/" + obj2.Lineid + ".html\" title=\"\" target=\"_blank\"><span class=\"fl\">" + obj2.LineName + "</span>";
                            html += "<p class=\"cost rl\"><b>¥</b>" + obj2.Price + "<i class=\"relative-box\">起</i></p></a>";
                        }
                    }
                });
                if (obj._childRowCount <= 4) {
                    html += "<div class=\"line fl\"></div><div class=\"details fl clearfix relative-box\"><div class=\"details-l fl\">";
                }
                if (obj._childRowCount == 0) {
                    html += "<div><div>";
                }
                if (obj.flag == true) {
                    html += "</div><a class=\"more hide absolute-box\" href=\"" + obj.Url + "\" target=\"_blank\">更多" + obj.LineName + "线路>></a></div>";
                } else {
                    html += "</div></div>";
                }
                html += "</ul>";
            });
            return html;
        }

        function ShowIndex_OtherLine_List() {
            var html = '<div class="freedom-ad fl">';
            html += '<a class="txt"><img src="image/ship_ad.jpg" alt="" title="" /></a>';
            html += '</div>';
            html += '<ul id="cjProduct_img" class="product-list group-list current fl clearfix relative-box">';
            html += '<div id="city_area02" class="group-city clearfix">';
            html += '</div>';
            var data = eval(<%=GetIndex_OtherLine_List("Index_Cruise")%>);
            if (data != null) {
                html += formatterIndex_OtherLine_List(data);
            }
            html += '</ul><a class="relative-box" href="/search/1063-0-0-0-0-0-0-0.html" target="_blank" style="left:1136px; top:-16px; color:#2382d9;">更多邮轮>></a>';
            $("#Index_Other").html(html);
        }
        function formatterIndex_OtherLine_List(data) {
            var html = "";
            $(data.rows).each(function (index, obj) {
                html += "<ul class=\"clearfix " + obj.styles + "\">";
                $(data.childRows).each(function (idx, obj2) {
                    if (obj2._parentId == obj.Id) {
                        html += "<li " + obj2.style1 + "><dl><dt class=\"relative-box\">";
                        html += "<a href=\"/line/" + obj2.Lineid + ".html\" target=\"_blank\"><img src=\"" + obj2.Pics + "\" alt=\"" + obj2.LineName + "\" title=\"" + obj2.LineName + "\"></a>";
                        html += "<p class=\"absolute-box fl clearfix\">";
                        html += "<span class=\"fl\" title=\"\"></span>";
                        html += "<b class=\"cost rl\"><span>¥</span>" + obj2.Price + "<i>起</i></b></p>";
                        html += "</dt><dd><a href=\"/line/" + obj2.Lineid + ".html\" target=\"_blank\">" + obj2.LineName + "</a></dd>";
                        html += "</dl></li>";
                    }
                });
                if (obj.flag) {
                    html += "<a class=\"more hide absolute-box\" href=\"" + obj.Url + "\" target=\"_blank\">更多" + obj.Cname + "线路>></a>";
                }
                html += "</ul>";
            });
            return html;
        }

        function ShowIndex_OtherTab() {
            var data = eval(<%=GetIndex_OtherTab("Index_Cruise")%>);
            if (data != null) {
                $("#city_area02").html(formatterIndex_OtherTab(data));
            }
        }
        function formatterIndex_OtherTab(data) {
            var html = "";
            $(data.rows).each(function (index, obj) {
                html += "<dl " + obj.styles + ">" + obj.Cname + "</dl>";
            });
            html += "</li>";
            return html;
        }

        function ShowIndex_Visa() {
            var html = "<div id=\"qz_answer\" class=\"freedom-ad qz-column fl\">";
            html += "<h3 class=\"relative-box\">签证办理须知</h3>";
            html += "<p><a title=\"很多发达国家（如美国和欧洲国家）办理签证申请都需要提前预约，如电话预约、在线预约和邮件预约。 预约时间确定才可以在预约当天去领馆交材料。需要预约时间的签证通常是需要面试的，但是目前部分申根国保留预约流程，但是已经逐渐取消面试流程，如法国和德国。一般发展中国家如东南亚办理签证，通常都不需要预约，可直接交材料给使馆。部分发达国家也不需要预约，如加拿大和澳大利亚，新西兰等。\" target=\"_blank\">签证办理需要哪些材料？</a><b></b></p>";
            html += "<p><a title=\"申办外国签证，一般都必须附有申请人的照片，有的和签证一起使用，有的贴在签证申请表格上。申请人申办签证时，必须另外备有照片。这些照片必须和护照上的照片完全一致。\" target=\"_blank\">签证照片有特殊要求吗？</a><b></b></p>";
            html += "<p><a title=\"一旦领馆受理了您的签证申请，不论获签与否，签证费均无法退还哦。\" target=\"_blank\">被拒签后签证费会退还吗？  </a><b></b></p></div>";
            html += "<ul id=\"cjProduct_img\" class=\"product-list group-list qz-group-list current fl clearfix relative-box\">";
            html += "<div class=\"empty\"></div>";
            var data = eval(<%=GetIndex_Visa("Index_Visa")%>);
            if (data != null) {
                html += formatterIndex_Visa(data);
            }
            html += "<a class=\"more qz-more absolute-box\" href=\"/visa.html\" target=\"_blank\">更多签证>></a></ul>";
            $("#Index_Visa").html(html);
        }

        function ShowIndex_Famous() {
            var tbody = "";
            var obj = <%=new HtmlString(GetIndex_Famous(5))%>;
            if(obj.length==0){return;}
            $.each(obj, function (n, value) {
                var html = "";
                html += "<li><dl>";
                html += "<dt class=\"relative-box\"><a href=\"/line/" + value.MisLineId + ".html\" target=\"_blank\"><img src=\""+value.Pics+"\" alt=\"\" title=\"" + value.LineName + "\"></a><p class=\"absolute-box fl clearfix\"><span class=\"fl\">"+value.PlanType+"</span><b class=\"cost rl\"><span>¥</span>" + value.Price + "<i>起</i></b></p></dt>";
                html += "<dd><a href=\"/line/" + value.MisLineId + ".html\" title=\"" + value.LineFeature + "\" target=\"_blank\">" + value.LineName + "</a></dd>";
                html += "</dl></li>";
                tbody += html;
            });
            $("#Index_Famous").append(tbody);
        }

        function formatterIndex_Visa(data) {
            var html = "<div class=\"column\">";
            $(data.rows).each(function (index, obj) {
                if (obj.rowIndex == 3 || obj.rowIndex==6) {
                    html += "</div><div class=\"column\">";
                }
                html += "<a class='" + obj.styles + " clearfix' href='/line/" + obj.Lineid + ".html' target='_blank'>";
                html += "<dt><img style='width: 90px;height:70px' src='" + obj.Pics + "' alt='' title='" + obj.LineName + "'></dt>";
                html += "<dd class='txt'>" + obj.LineName + "</dd>";
                html += "<dd class='cost'><span>¥</span>" + obj.Price + "</dd></a>";
            });
            html += "</div>";
            return html;
        }
    </script>
    <script type="text/javascript" src="newjs/common.js"></script>
</head>
<body id="index">
    <uc1:Header ID="Header1" runat="server" />
	<!--正文内容Begin-->
	<div class="wrap">
		<div class="index-top clearfix">
            <uc3:LeftSlide ID="LeftSlide1" runat="server" />
            <div class="top-ad m">
                <div id="Index_Ad_Slide">
                    <!--
                    <div id="indexTopbanner" class="banner">
					<ul class="clearfix">
						<li>
							<a href="javascript:;" target="_blank"><img src="image/AD_01.png" alt="济州之旅" title="济州之旅"></a>
						</li>
						<li>
							<a href="javascript:;" target="_blank"><img src="image/AD_02.png" alt="济州之旅" title="济州之旅"></a>
						</li>
						<li>
							<a href="javascript:;" target="_blank"><img src="image/AD_01.png" alt="济州之旅" title="济州之旅"></a>
						</li>
						<li>
							<a href="javascript:;" target="_blank"><img src="image/AD_03.png" alt="济州之旅" title="济州之旅"></a>
						</li>
						<li>
							<a href="javascript:;" target="_blank"><img src="image/AD_01.png" alt="济州之旅" title="济州之旅"></a>
						</li>
					</ul>
				</div>
				    <div id="bannerGoods" class="bannertxt absolute-box">
					<ul class="clearfix">
						<li class="current">皇家邮轮济州</li>
						<li>皇家邮轮济州</li>
						<li>皇家邮轮济州</li>
						<li>皇家邮轮济州</li>
						<li class="no-border">皇家邮轮济州</li>
					</ul>
				</div>
                    -->
                </div>
				<!--nnn 新Banner下主题广告-->
				<div class="top-sale" id="Index_Ad_Banner">
                    <!--
					<div class="under-ad">
						<a href="javascript:;" target="_blank">
							<img src="image/13.png" title="" alt="">
						</a>
					</div>
					<div class="under-ad">
						<a href="javascript:;" target="_blank">
							<img src="image/11.png" title="" alt="">
						</a>
					</div>
					<div class="under-ad">
						<a href="javascript:;" target="_blank">
							<img src="image/13.png" title="" alt="">
						</a>
					</div>
					<div class="under-ad">
						<a href="javascript:;" target="_blank">
							<img src="image/11.png" title="" alt="">
						</a>
					</div>
					<div class="under-ad">
						<a href="javascript:;" target="_blank">
							<img src="image/13.png" title="" alt="">
						</a>
					</div>
                    -->
				</div>
			</div>
        </div>
        <!--nnn 超值特卖 旅游分期 特价酒店 特价签证-->
        <div class="index-trip worth">
            <div class="tab-wrap clearfix">
                <ul id="chujing_area" class="tit fl clearfix">
                    <li class="current">特价精选</li>
                    <span class="js_vic">换一批</span>
                    <a href="search.html?key=%E7%89%B9%E6%83%A0" style="right:10px;" class="pro-all" target="_blank" style="right:-8px;">更多特价精选>></a>
                </ul>
            </div>
            <div class="product-wrap bottom-free clearfix" id="Index_Line_Sell" style="position:relative;">
                <!--list超值热卖-->
                <ul id="vic" class="product-list worth-list current fl clearfix" style="position:absolute;">
                    <div class="doc clearfix">
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_01.png" alt="" title="香港纯玩双园4日游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自助游</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">香港纯玩双园4日游(迪士尼乐园、海洋公园)</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_02.png" alt="" title="斯里兰卡全景7日"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">跟团游</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">斯里兰卡全景7日-名奈利亚动物园+海上列车</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_03.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_04.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_05.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">半自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                    </div>
                    <div class="doc clearfix">
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_01.png" alt="" title="香港纯玩双园4日游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自助游</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">香港纯玩双园4日游(迪士尼乐园、海洋公园)</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_02.png" alt="" title="斯里兰卡全景7日"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">跟团游</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">斯里兰卡全景7日-名奈利亚动物园+海上列车</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_03.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_04.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                        <li>
                            <dl>
                                <dt class="relative-box">
                                    <a href="javascript:;" target="_blank"><img src="image/trip_05.png" alt="" title="【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游"></a>
                                    <p class="absolute-box fl clearfix">
                                        <span class="fl">半自由行</span>
                                        <b class="cost rl"><span>¥</span>2338<i>起</i></b>
                                    </p>
                                </dt>
                                <dd>
                                    <a href="javascript:;" target="_blank">【春节版绚丽春晖】新西兰南北岛冰川岛屿湾魔戒11日深度游</a>
                                </dd>
                            </dl>
                        </li>
                    </div>
                </ul>
            </div>
        </div>
        <!--当季推荐-->
        <!--
		<div class="index-spring clearfix">
			<div class="index-spring-l fl">
				<div class="top-wrap">
					<h2></h2>
				</div>
				<div class="index-spring-ad" id="Index_Ad_Season">
					<%--<%=TravelOnline.NewPage.Class.CacheClass.Index_Ad_Season()%>--%>
				</div>
				<div class="change_pro">
					<div id="change_Bg" class="exchange">换一批</div>
				</div>
				<div class="pro_list">
					<ul id="pro_mrTop">
						<%--<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Season()%>--%>
					</ul>
				</div>
			</div>
            <uc4:IndexHot ID="IndexHot1" runat="server" />
		</div>
         -->
        <!--当季推荐 End-->
       
        <!--玩转自由行-->
		<div class="index-trip">
			<div class="tab-wrap top-ziyou clearfix" id="Index_FreeTourToIndex_Line_Tab">
				<%--<h2 class="relative-box fl">玩转自由行</h2>
				<ul id="chujing_area" class="fl clearfix">
					<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_FreeTour")%>
                    <a href="/freetour.html" class="pro-all" target="_blank">更多自由行产品>></a>
				</ul>--%>
			</div>
			<div class="product-wrap bottom-free clearfix">
				<div class="theme-trip fl">
					<h3 style="color:#49c184;">自驾旅游</h3>
					<div class="plc auto">
						<a href="search.html?key=%e6%9a%91%e6%9c%9f%e4%ba%b2%e5%ad%90" class="other" target="_blank">暑期亲子</a></br>
						<a href="search.html?key=%e8%87%aa%e7%94%b1%e5%ae%b6" class="other" target="_blank">自由行套餐</a>
					</div>
					<div class="plc high1">
						<a href="search.html?key=%e7%8e%b2%e7%8f%91%e6%b5%b7%e5%b2%9b"><img src="image/trip_free01.jpg" /></a>
					</div>
					<div class="plc high1">
						<a href="search.html?key=%e5%90%8d%e5%8f%a4%e5%b1%8b"><img src="image/trip_free02.jpg" /></a>
					</div>
				</div>
                <div id="Index_FreeTourToIndex_Line_List"></div>
			</div>
		</div>
		<!--玩转自由行 End -->
		
		<!--出境旅游-->
		<div class="index-trip">
			<div class="tab-wrap clearfix" id="Index_OutboundToIndex_Line_Tab">
				<%--<h2 class="relative-box fl">出境旅游</h2>
				<ul id="chujing_area" class="fl clearfix">
                    <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_Outbound")%>
                    <a href="/outbound.html" class="pro-all" target="_blank">更多出境游产品>></a>
				</ul>--%>
			</div>
			<div class="product-wrap clearfix">
                <!--nnn 出境左侧-->
				<div class="theme-trip fl">
					<h3>热门目的地</h3>
					<div class="plc">
						<a href="search.html?key=%e5%b7%b4%e5%8e%98%e5%b2%9b" target="_blank">巴厘岛</a>
						<a href="search.html?key=%e9%a9%ac%e5%b0%94%e4%bb%a3%e5%a4%ab" target="_blank">马尔代夫</a>
						<a href="search.html?key=%e6%b3%b0%e5%9b%bd" target="_blank">泰国</a>
						<a href="search.html?key=%e9%9f%a9%e5%9b%bd" target="_blank">韩国</a>
						<a href="search.html?key=%e6%97%a5%e6%9c%ac" target="_blank">日本</a>
						<a href="search.html?key=%e7%91%9e%e5%a3%ab" target="_blank">瑞士</a>
						<a href="search.html?key=%e7%be%8e%e5%9b%bd" target="_blank">美国</a>
						<a href="search.html?key=%e6%9f%ac%e5%9f%94%e5%af%a8" target="_blank">柬埔寨</a>
						<a href="search.html?key=%e6%96%b0%e5%8a%a0%e5%9d%a1" target="_blank">新加坡</a>
						<a href="search.html?key=%e6%84%8f%e5%a4%a7%e5%88%a9" target="_blank">意大利</a>
						<a href="search.html?key=%e8%8d%b7%e5%85%b0" target="_blank">荷兰</a>
					</div>
					<div class="plc high2">
						<a href="search.html?key=%e9%bb%84%e7%9f%b3%e5%85%ac%e5%9b%ad"><img src="image/trip_cj.png" /></a>
					</div>
				</div>
                <div id="Index_OutboundToIndex_Line_List"></div>
                <%--<div class="theme-trip fl">
					<a href="http://www.scyts.com/line/17398.html" target="_blank" title="浪漫骄澳*澳大利亚悉尼黄金海岸大堡礁墨尔本东海岸全览10天8晚"><img src="image/side_outbound.jpg" alt="浪漫骄澳*澳大利亚悉尼黄金海岸大堡礁墨尔本东海岸全览10天8晚"></a>
				</div>
				<%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_Outbound")%>--%>
			</div>
		</div>
        <!--出境旅游 End -->

		<!--国内旅游-->
		<div class="index-trip">
			<div class="tab-wrap top-guonei clearfix" id="Index_InlandToIndex_Line_Tab">
				<%--<h2 class="relative-box fl">国内旅游</h2>
				<ul id="guonei_area" class="fl relative-box clearfix">
                    <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_Tab("Index_Inland")%>
                    <a href="/inland.html" class="pro-all" target="_blank">更多国内游产品>></a>
				</ul>--%>
			</div>
			<div class="product-wrap bottom-guonei clearfix">
                <div class="theme-trip fl">
				<!--nnn 国内左侧-->
					<h3 style="color:#ea535a;">热门城市</h3>
					<div class="plc">
						<a href="search.html?key=%e4%b8%bd%e6%b1%9f" target="_blank">丽江</a>
						<a href="search.html?key=%e6%a1%82%e6%9e%97" target="_blank">桂林</a>
						<a href="search.html?key=%e8%a5%bf%e8%97%8f" target="_blank">西藏</a>
						<a href="search.html?key=%e6%b5%b7%e5%8d%97" target="_blank">海南</a>
						<a href="search.html?key=%e5%bc%a0%e5%ae%b6%e7%95%8c" target="_blank">张家界</a>
						<a href="search.html?key=%e5%ae%89%e5%90%89" target="_blank">安吉</a>
						<a href="search.html?key=%e9%95%bf%e7%99%bd%e5%b1%b1" target="_blank">长白山</a>
						<a href="search.html?key=%e4%b9%9d%e5%af%a8%e6%b2%9f" target="_blank">九寨沟</a>
						<a href="search.html?key=%e5%a9%ba%e6%ba%90" target="_blank">婺源</a>
						<a href="search.html?key=%e5%8c%97%e4%ba%ac" target="_blank">北京</a>
						<a href="search.html?key=%e4%b8%8a%e6%b5%b7" target="_blank">上海</a>
					</div>
					<div class="plc high2">
						<a href="javascript:void(0);"><img src="image/trip_gn.png" /></a>
					</div>
				</div>
                <div id="Index_InlandToIndex_Line_List"></div>
                <%--<div class="theme-trip fl">
					<a href="http://www.scyts.com/line/16915.html" target="_blank" title="特惠全景*云南丽江、香格里拉、大理双飞5日（丽江往返，住宿升级）"><img src="image/side_inland.jpg" alt="特惠全景*云南丽江、香格里拉、大理双飞5日（丽江往返，住宿升级）"></a>
				</div>
                <%=TravelOnline.NewPage.Class.CacheClass.Index_Line_List("Index_Inland")%>--%>
			</div>
		</div>
        <!--国内旅游 End-->

		<!--自由行、邮轮、签证-->
		<div class="index-trip">
			<div class="top-group relative-box clearfix">
				<p class="ship-icon current fl"></p>
				<p class="qz-icon fl"></p>
				<ul id="group_area" class="absolute-box fl clearfix">
					<h2 class="current">邮轮</h2>
					<h2>签证</h2>
				</ul>
				<span class="pos01">|</span>
				<span class="pos02">|</span>
			</div>
			<div class="product-wrap bottom-group clearfix">
				<!--邮轮-->
				<dl class="clearfix show-group" id="Index_Other">
					<%--<div class="freedom-ad fl">
						<a class="txt"><img src="image/ship_ad.jpg" alt="" title="" /></a>
					</div>
					<!--list邮轮-->
					<ul id="cjProduct_img" class="product-list group-list current fl clearfix relative-box">
						<div id="city_area02" class="group-city clearfix">
							<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherTab("Index_Cruise")%>
						</div>
						<%=TravelOnline.NewPage.Class.CacheClass.Index_OtherLine_List("Index_Cruise")%>
					</ul>
					<a class="relative-box" href="/search/1063-0-0-0-0-0-0-0.html" target="_blank" style="left:1136px; top:-16px; color:#2382d9;">更多邮轮>></a>--%>
				</dl>
				<!--签证-->
				<dl class="clearfix hide-group" id="Index_Visa">
					<%--<div id="qz_answer" class="freedom-ad qz-column fl">
						<h3 class="relative-box">签证办理须知</h3>
						<p><a title="很多发达国家（如美国和欧洲国家）办理签证申请都需要提前预约，如电话预约、在线预约和邮件预约。 预约时间确定才可以在预约当天去领馆交材料。需要预约时间的签证通常是需要面试的，但是目前部分申根国保留预约流程，但是已经逐渐取消面试流程，如法国和德国。
一般发展中国家如东南亚办理签证，通常都不需要预约，可直接交材料给使馆。
部分发达国家也不需要预约，如加拿大和澳大利亚，新西兰等。" target="_blank">签证办理需要哪些材料？</a><b></b></p>
						<p><a title="申办外国签证，一般都必须附有申请人的照片，有的和签证一起使用，有的贴在签证申请表格上。申请人申办签证时，必须另外备有照片。这些照片必须和护照上的照片完全一致。" target="_blank">签证照片有特殊要求吗？</a><b></b></p>
						<p><a title="一旦领馆受理了您的签证申请，不论获签与否，签证费均无法退还哦。 " target="_blank">被拒签后签证费会退还吗？  </a><b></b></p>
					</div>--%>
					<!--list签证-->
					<%--<ul id="cjProduct_img" class="product-list group-list qz-group-list current fl clearfix relative-box">
						<div class="empty"></div>
						<%=TravelOnline.NewPage.Class.CacheClass.Index_Visa()%>
						<a class="more qz-more absolute-box" href="/visa.html" target="_blank">更多签证>></a>
					</ul>--%>
				</dl>
			</div>
		</div>
	    <!--自由行、邮轮、签证 End -->
	    <!--青旅名牌-->
		<%--<div class="index-trip">
			<div class="tab-wrap famous clearfix">
				<h3></h3>
			</div>
			<div class="product-wrap fan clearfix">
				<ul class="product-list current fl clearfix">
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/15859.html" target="_blank"><img src="image/fan01.jpg" alt="" title="韩国首尔4天(1天自由活动送乐天世界通票入住5星级酒店)"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>3600<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/15859.html" title="韩国首尔4天(1天自由活动送乐天世界通票入住5星级酒店)" target="_blank">韩国首尔4天(1天自由活动送乐天世界通票入住5星级酒店)</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/20340.html" target="_blank"><img src="image/fan03.jpg" alt="" title="日本大阪、奈良、京都、滋贺、白川乡、长野、东京6日"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>7999<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/20340.html" title="日本大阪、奈良、京都、滋贺、白川乡、长野、东京6日" target="_blank">日本大阪、奈良、京都、滋贺、白川乡、长野、东京6日</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/19523.html" target="_blank"><img src="image/fan04.jpg" alt="" title="全景品质纯玩*美国东西海岸大瀑布夏威夷14天12晚"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>25800<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/19523.html" title="全景品质纯玩*美国东西海岸大瀑布夏威夷14天12晚" target="_blank">全景品质纯玩*美国东西海岸大瀑布夏威夷14天12晚</a>
							</dd>
						</dl>
					</li>
					<li>
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/20414.html"><img src="image/fan02.jpg" alt="" title="浪漫骄澳*澳大利亚悉尼大堡礁墨尔本东海岸全览11天8晚（新航A380，两晚五星，企鹅岛龙虾餐，赠送考拉合照）"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>26999<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/20414.html" title="浪漫骄澳*澳大利亚悉尼大堡礁墨尔本东海岸全览11天8晚（新航A380，两晚五星，企鹅岛龙虾餐，赠送考拉合照）" target="_blank">浪漫骄澳*澳大利亚悉尼大堡礁墨尔本东海岸全览11天8晚（新航A380，两晚五星，企鹅岛龙虾餐，赠送考拉合照）</a>
							</dd>
						</dl>
					</li>
					<li class="no-mrr">
						<dl>
							<dt class="relative-box">
								<a href="http://www.scyts.com/line/20364.html" target="_blank"><img src="image/fan05.jpg" alt="" title="一见青“新”新西兰南北岛雪山湖泊古城温泉10天8晚（纽航直飞+大汉堡+日式三文鱼餐+萤火虫洞+温泉泡浴+东线中线全揽）"></a>
								<p class="absolute-box fl clearfix">
									<span class="fl">跟团游</span>
									<b class="cost rl"><span>¥</span>23800<i>起</i></b>
								</p>
							</dt>
							<dd>
								<a href="http://www.scyts.com/line/20364.html" title="一见青“新”新西兰南北岛雪山湖泊古城温泉10天8晚（纽航直飞+大汉堡+日式三文鱼餐+萤火虫洞+温泉泡浴+东线中线全揽）" target="_blank">一见青“新”新西兰南北岛雪山湖泊古城温泉10天8晚（纽航直飞+大汉堡+日式三文鱼餐+萤火虫洞+温泉泡浴+东线中线全揽）</a>
							</dd>
						</dl>
					</li>
				</ul>
			</div>--%>
		<!--<div class="index-trip">
			<div class="tab-wrap famous clearfix">
				<h3></h3>
			</div>
            
			<div class="product-wrap fan clearfix">
				<ul class="product-list current fl clearfix" id ="Index_Famous">
				</ul>
			</div>
        </div>-->
        <!--青旅名牌 End -->
        
    </div>
	<!--正文内容End-->
    <uc2:Footer ID="Footer1" runat="server" />
    <!--底部微信通栏-->
    <!--<div class="bottomnav-wx"></div>-->
    <div class="bottomnav-wx-wrap">
    	<div class="wx-content">
    		<a href="http://www.scyts.com/showinfo/311.html" target="_blank">公司声明</a>
    		<i id="bottomnav-close" title="关闭">×</i>
    	</div>
    </div>
</body>
</html>
