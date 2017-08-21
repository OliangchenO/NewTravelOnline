<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_fx.aspx.cs" Inherits="TravelOnline.WeChat.main_fx" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="zh" class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">
    <title>上海中国青年旅行社</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0" name="viewport">
    <meta content="" name="description">
    <meta content="" name="author">
    <meta name="MobileOptimized" content="320">
    <!-- Global styles START -->
    <link href="/assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="/assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css">
    <!-- Global styles END -->
    <!-- Page level plugin styles START -->
    <link href="/assets/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet">
    <link href="/assets/plugins/bxslider/jquery.bxslider.css" rel="stylesheet">
    <link rel="stylesheet" href="/assets/plugins/layerslider/css/layerslider.css" type="text/css">
    <!-- Page level plugin styles END -->
    <!-- Theme styles START -->
    <link href="/assets/css/style-metronic.css" rel="stylesheet" type="text/css">
    <link href="/assets/css/style.css?v=20141203" rel="stylesheet" type="text/css">
    <link href="/assets/css/style-responsive.css" rel="stylesheet" type="text/css">
    <link href="/assets/css/custom.css" rel="stylesheet" type="text/css">
    <!-- Theme styles END -->
    <!--Time START-->
    <link rel="stylesheet" href="http://fonts.useso.com/css?family=Open+Sans:300,300italic,400,400italic,600,600italic,700,700italic">
    <link rel="stylesheet" type="text/css" href="/app_css/time/default.css">
    <script src="/assets/plugins/jquery-1.10.2.min.js"></script>
    <script src="/app_js/time/jquery.knob.js"></script>
    <script src="/app_js/time/jquery.throttle.js"></script>
    <script src="/app_js/time/jquery.classycountdown.js"></script>
    <style>
        .ClassyCountdownDemo {
            width: 100%;
            display: block;
        }
    </style>
    <!--Time END-->
    <link href="/app_css/main.css" rel="stylesheet">
    <link rel="shortcut icon" href="~/favicon.ico">
    <!--IndexList START-->
    <link rel="stylesheet" href="/app_css/index_list/middle.css" media="screen and (min-width: 320px)">
    <script src="/app_js/index_list/lazyload.js"></script>
    <script type="text/javascript">
        function ShowWeChatFlashAd() {
            var html = "";
            var data = eval(<%=TravelOnline.WeChat.WeChatClass.GetWeChatFlashAd_New()%>);
            if (data != null) {
                html += "<div class=\"swipe\" id=\"mySwipe\">";
                html += "<div class=\"swipe-wrap\">";
                var Fx_UserId = eval(<%=Fx_UserId%>);
                $(data.rows).each(function (idx, obj) {
                    if (null != Fx_UserId) {
                        html += "<div><a href=\"" + obj.AdPageUrl + "?userId=" + Fx_UserId + "\"><img class=\"img-responsive img-h250\" src=\"" + obj.AdPicUrl + "\"/></a></div>";
                    } else {
                        html += "<div><a href=\"" + obj.AdPageUrl + "\"><img class=\"img-responsive img-h250\" src=\"" + obj.AdPicUrl + "\"/></a></div>";
                    }
                });
                html += "</div></div><ul id=\"position\"><li class=\"cur\"></li> \r\n";
                for (var i = 0; i < data.total - 1; i++) {
                    html += "<li class=\"\"></li> \r\n";
                }
                html += "</ul>";
            }
            html += " \r\n";
            $("#WeChatFlashAd").html(html);
        }

        <%--function ShowWeChatFlashSale() {
            var html = "";
            var data = eval(<%=TravelOnline.WeChat.WeChatClass.GetWeChatFlashSale_New("")%>);
            if (data != null) {
                var Fx_UserId = eval(<%=Fx_UserId%>);
                if (null != Fx_UserId) {
                    html = "<div class='row'><div class='col-xs-12'><h3 class='row-tit row-h3'>限时抢购<a class='more' href='" + data.row1[0].Url + "?userId=" + Fx_UserId + "'>更多></a></h3></div></div>";
                } else {
                    html = "<div class='row'><div class='col-xs-12'><h3 class='row-tit row-h3'>限时抢购<a class='more' href='" + data.row1[0].Url + "'>更多></a></h3></div></div>";
                }
                $(data.rows).each(function (index, obj) {
                    html += "<div class='row'><a href='" + obj.url + "'><div class='col-xs-7 overflow'><img src='" + obj.PhotoPath + "' alt='" + obj.Cname + "', title='" + obj.Cname + "'/></div>";
                    html += "<div class='col-xs-5 product'><p>" + obj.Cname + "</p><strong>￥<span>" + obj.Price + "</span></strong></div>";
                    html += "<div class='col-xs-5 product' style='border:0;'><form name='form1'><input type='text' name='left' size='30' style='font-size:12px;'></form></div></a></div>";
                });
                $("#WeChatFlashSale").html(html);
            }
        }

        function ShowWeChatRecom() {
            var html = "<div class=\"row\"><div class=\"col-xs-12\"><h3 class=\"row-tit row-h3\">特别推荐</h3></div></div>";
            var data = eval(<%=TravelOnline.WeChat.WeChatClass.GetWeChatRecom_New("sticker")%>);
            if (data != null) {
                $(data.rows).each(function (index, obj) {
                    html += "<div class='row b-color'><a href='" + obj.url + "'><div class='col-xs-3 pic'>";
                    html += "<div class='tile' style='height:60px;overflow:hidden;'><img src='" + obj.PhotoPath + "' style='width:100%; height:60px;'/>";
                    html += "<div class='span4 text-center pos50' style='top:0;font-size:12px;'><p class='colorFFF'>" + obj.Types + "</p></div></div></div>";
                    html += "<div class='col-xs-6 name'><p>" + obj.Cname + "</p></div>";
                    html += "<div class='col-xs-2 cos'><p class='text-right price'><span>￥</span>" + obj.Price + "</p></div></a></div>";
                });
            }
            $("#container").html(html);
        }

        function ShowWeChatIndexRecom() {
            var html = "";
            var data = eval(<%=TravelOnline.WeChat.WeChatClass.GetWeChatIndexRecom_New("sticker")%>);
            if (data != null) {
                $(data.rows).each(function (index, obj) {
                    html += "<li>";
                    html += "<div class=\"product-item product-list\">";
                    html += "<div class=\"pi-img-wrapper\">";
                    html += "<a href=\"" + obj.url + "\"><img src=\"" + obj.PhotoPath + "\" class=\"img-responsive\" alt=\"" + obj.Cname + "\"></a>";
                    html += "</div>";
                    html += "<h3><a href=\"" + obj.url + "\">" + obj.Cname + "</a></h3>";
                    html += "<div class=\"pi-price\">&#165;" + obj.Price + "起</div>";
                    html += "<a href=\"" + obj.url + "\" class=\"btn btn-default add2cart\">去看看</a>";
                    html += "</div></li>";
                });
                $("#WeChatIndexRecom").html(html);
            }
        }--%>

        $(function () {
            ShowWeChatFlashAd();
            //ShowWeChatFlashSale();
            //ShowWeChatRecom();
            //ShowWeChatIndexRecom();

            $("#container img").lazyload({
                threshold: 400,
                placeholder: "/Images/lazyload.gif",
                effect: "fadeIn"
            });
        });
    </script>
    <!--IndexList END-->

    <!--百度统计3.0 账号:shbvip-scyts，更新日期2016/1/27-->
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?3d7ea07f7149cb10bd17920587e5985b";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
    </script>
    <!--百度统计3.0 END-->

    <!--百度统计3.0 账号:上海中国青年旅行社，更新日期2016/1/27-->
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?a8456f78e5a9a51324fa762afc0390db";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
    </script>
    <!--百度统计3.0 END-->

    <!--百度统计3.0 账号:上海青旅seo，更新日期2016/1/27-->
    <script>
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?d3779a2d6ba03f2452d29c67eeaa3a08";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
    </script>

    <!--百度统计3.0 END-->

</head>
<body style="background: #E8EBF2;">
    <div class="container nmp" style="position: relative">
        <div class="row fxshop">
            <div id="infoShop" class="head_photo">
                <div class="photo_wrap">
                    <%if (Convert.ToString(Session["Fx_Headimgurl"]).Length > 0)
                        {%>
                    <img src="<%= Convert.ToString(Session["Fx_Headimgurl"])%>" style="width: 100%;" />
                    <%}
                        else
                        { %>
                    <img src="/images/ppp.jpg" style="width: 100%;" />
                    <%} %>
                </div>
                <p>
                    <%if (null != Session["Fx_Storename"] && Session["Fx_Storename"] != "")
                        { %>
                    <%= Session["Fx_Storename"]%>
                    <%}
                        else
                        {%>
                分销店铺
                <%}%>
                    <span></span>
                </p>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row">
            <div class="infoshop_box">
                <div class="background_img">
                    <img src="/images/infoShop_bg.png" style="width: 100%;" />
                    <b class="close_shop">x</b>
                    <ul>
                        <li>
                            <%if (null != Session["Fx_Wxid"] && Session["Fx_Wxid"] != "")
                                { %>
                            <em>微信号：</em><span><%=Session["Fx_Wxid"] %></span></br>
                        <%} %>
                            <%if (null != Session["Fx_Mobile"] && Session["Fx_Mobile"] != "")
                                { %>
                            <em>手机号：</em><a style="color: #fff;" href="tel:<%= Session["Fx_Mobile"]%>"><%= Session["Fx_Mobile"]%></a></br>
                        <%} %>
                            <%if (null != Session["Fx_Tel"] && Session["Fx_Tel"] != "")
                                { %>
                            <em>座&nbsp;&nbsp;机：</em><a href="tel:<%= Session["Fx_Tel"]%>"><%=Session["Fx_Tel"] %></a></br>
                        <%} %>
                            <%if (null != Session["Fx_Address"] && Session["Fx_Address"] != "")
                                { %>
                            <em>地&nbsp;&nbsp;址：</em><span><%=Session["Fx_Address"] %></span>
                            <%} %>
                        </li>
                        <%if (Session["Fx_Login"] != null)
                            { %>
                        <li>
                            <a href="Fx_editinfo.aspx">编辑</a>
                        </li>
                        <%}
                            else
                            {%>
                        <a href="Fx_Loginout.aspx">退出当前账户</a>
                        <%} %>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(function () {
            $("#infoShop").click(function () {        //显示弹窗
                var zzc = $('<div class="index_mask"></div>');
                $('body').append(zzc);
                var _screenWidth = $(document).width();
                var _screenHeight = $(document).height();
                $(zzc).css({
                    'width': _screenWidth + 'px',
                    'height': _screenHeight + 'px',
                }).show();
                $(".infoshop_box").show();
            })

            $(".close_shop").click(function () {
                $(".infoshop_box").hide();
                $(".index_mask").remove();
            })
        })
    </script>
    <div class="addWrap" id="WeChatFlashAd">
        <%--<div class="swipe" id="mySwipe">
    <div class="swipe-wrap">
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/1.jpg"/></a></div>
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/2.jpg"/></a></div>
    </div>
  </div>
  <ul id="position"><li class="cur"></li><li class=""></li></ul>--%>
        <%--<% =TravelOnline.WeChat.WeChatClass.GetWeChatFlashAd()%>--%>
    </div>

    <div class="container">
        <input type="hidden" id="UserId" name="UserId" value="<%=Session["Fx_UserId"] %>" />
        <input type="hidden" id="Storename" name="Storename" value="<%=Session["Fx_Storename"] %>" />
        <input type="hidden" id="Mobile" name="Mobile" value="<%=Session["Fx_Mobile"] %>" />
        <input type="hidden" id="UserEmail" name="UserEmail" value="<%=Session["Fx_UserEmail"] %>" />
        <input type="hidden" id="UserName" name="UserName" value="<%=Session["Fx_UserName"] %>" />
        <input type="hidden" id="Wxid" name="storename" value="<%=Session["Fx_Wxid"] %>" />
        <input type="hidden" id="Tel" name="Tel" value="<%=Session["Fx_Tel"] %>" />
        <input type="hidden" id="Address" name="Address" value="<%=Session["Fx_Address"] %>" />
        <input type="hidden" id="Headimgurl" name="Headimgurl" value="<%=Session["Fx_Headimgurl"] %>" />
        <div class="row">
            <div class="search-box" style="padding-left: 10px">
                <form onsubmit="javascript:return check_null();" action="/WeChat/linelist.aspx?linetype=search&userId=<%=Session["Fx_UserId"] %>" method="post">
                    <div class="input-group">
                        <input type="text" id="search" name="search" placeholder="搜索关键字或目的地名称" class="form-control">
                        <span class="input-group-btn">
                            <button class="btn btn-primary" type="submit"><i class="fa fa-search"></i></button>
                        </span>
                    </div>
                </form>
            </div>
        </div>
        <div class="tiles row">
            <div class="col-xs-8">
                <a href="/WeChat/linelist.aspx?linetype=outbound&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/cj.jpg" style="width: 100%; height: 100%;" />
                        <div class="span8 text-center pos50">
                            <p class="colorFFF">出境旅游</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <a href="/WeChat/linelist.aspx?linetype=inland&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/gn.jpg" style="width: 100%; height: 100%;" />
                        <div class="span4 text-center pos50">
                            <p class="colorFFF">国内旅游</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <a href="/WeChat/linelist.aspx?linetype=cruises&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/yl.jpg" style="width: 100%; height: 100%;" />
                        <div class="span4 text-center pos50">
                            <p class="colorFFF">邮轮旅游</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <a href="/WeChat/linelist.aspx?linetype=freetour&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/zyx.jpg" style="width: 100%; height: 100%;" />
                        <div class="span4 text-center pos50">
                            <p class="colorFFF">自由行</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <a href="/WeChat/linelist.aspx?linetype=visa&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/qz.jpg" style="width: 100%; height: 100%;" />
                        <div class="span4 text-center pos50">
                            <p class="colorFFF">签证</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-8">
                <a href="/WeChat/linelist.aspx?linetype=recommend&userId=<%=Session["Fx_UserId"] %>">
                    <div class="tile">
                        <img src="/assets/img/djtj.jpg" style="width: 100%; height: 100%;" />
                        <div class="span8 text-center pos50">
                            <p class="colorFFF">当季推荐</p>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-xs-4">
                <a href="javascript:;" id="myorder">
                    <div class="tile bg-PETERRIVER col-xs-4">
                        <div class="tile-body">
                            <i class="fa fa-user"></i>
                        </div>
                        <div class="tile-object text-center">
                            <div style="font: 18px 'Microsoft Yahei'">
                                会员中心
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </div>

    <script>
        startclock()
        var timerID = null;
        var timerRunning = false;
        function showtime() {
            Today = new Date();
            var NowHour = Today.getHours();
            var NowMinute = Today.getMinutes();
            var NowMonth = Today.getMonth();
            var NowDate = Today.getDate();
            var NowYear = Today.getYear();
            var NowSecond = Today.getSeconds();
            if (NowYear < 2000)
                NowYear = 1900 + NowYear;
            Today = null;
            /*
            Hourleft = 23 - NowHour   
            Minuteleft = 59 - NowMinute   
            Secondleft = 59 - NowSecond */

            Hourleft = 23 - NowHour
            Minuteleft = 59 - NowMinute
            Secondleft = 59 - NowSecond


            Yearleft = 2016 - NowYear
            Monthleft = 1 - NowMonth - 1
            Dateleft = 18 - NowDate - 1


            if (Secondleft < 0) {
                Secondleft = 60 + Secondleft;
                Minuteleft = Minuteleft - 1;
            }
            if (Minuteleft < 0) {
                Minuteleft = 60 + Minuteleft;
                Hourleft = Hourleft - 1;
            }
            if (Hourleft < 0) {
                Hourleft = 24 + Hourleft;
                Dateleft = Dateleft - 1;
            }
            if (Dateleft < 0) {
                Dateleft = 31 + Dateleft;
                Monthleft = Monthleft - 1;
            }
            if (Monthleft < 0) {
                Monthleft = 12 + Monthleft;
                Yearleft = Yearleft - 1;
            }
            Temp = Dateleft + '天: ' + Hourleft + ': ' + Minuteleft + ': ' + Secondleft + ''
            document.form1.left.value = Temp;
            timerID = setTimeout("showtime()", 1000);
            timerRunning = true;
        }
        var timerID = null;
        var timerRunning = false;
        function stopclock() {
            if (timerRunning)
                clearTimeout(timerID);
            timerRunning = false;
        }
        function startclock() {
            stopclock();
            showtime();
        }
        //
    </script>
    <div class="container border-wrap" style="background: #fff;" id="WeChatFlashSale">
        <%= TravelOnline.WeChat.WeChatClass.GetWeChatFlashSale("")%>
    </div>

    <div id="container" class="container">
        <div class="row">
            <div class="col-xs-12">
                <h3 class="row-tit row-h3">特别推荐</h3>
            </div>
        </div>
        <% =TravelOnline.WeChat.WeChatClass.GetWeChatRecom("sticker")%>
    </div>

    <div class="main padding-top-10" id="SpcRecomm" hidden>
        <div class="container">
            <!-- BEGIN TWO PRODUCTS & PROMO -->
            <div class="row">
                <!-- BEGIN CONTENT -->
                <div class="col-xs-12">
                    <h3 class="margin-left-5 row-h3">特别推荐</h3>
                    <div class="bxslider-wrapper">
                        <ul class="bxslider" data-slides-phone="1" data-slides-tablet="2" data-slides-desktop="4" data-slide-margin="15" id="WeChatIndexRecom">
                            <% =TravelOnline.WeChat.WeChatClass.GetWeChatIndexRecom("sticker")%>
                        </ul>
                    </div>
                    <!-- END CONTENT <img src="/Images/Views/200910/M_0910141627241.JPG" class="img-responsive" alt="新航马尔代夫6天4晚（自由行）">-->
                </div>
            </div>
            <!-- END TWO PRODUCTS & PROMO -->
        </div>
    </div>

    <!--底部导航-->
    <div class="container float-nav">
        <div class="row">
            <div class="col-xs-3 nav-num">
                <a href="/WeChat/main_fx.aspx">
                    <div class="tit-box">
                        <div class="img">
                            <img src="../Images/nav/01.png" />
                        </div>
                        <p class="on">首页</p>
                    </div>
                </a>
            </div>

            <div class="col-xs-3 nav-num">
                <%if (null != Session["Fx_Mobile"] && Session["Fx_Mobile"] != "")
                    { %>
                <a href="tel:<%= Session["Fx_Mobile"]%>" />
                <%}
                    else
                    { %>
                <a href="tel:4006777666" />
                <%} %>
                <div class="tit-box">
                    <div class="img">
                        <img src="../Images/nav/03.png" />
                    </div>
                    <p>热线电话</p>
                </div>
                </a>
            </div>
            <%if (Convert.ToString(Session["Fx_Login"]).Length > 0)
                {%>
            <div class="col-xs-3 nav-num">
                <a href="javascript:;" id="myFxOrder">
                    <div class="tit-box">
                        <div class="img">
                            <img src="../Images/nav/04.png" />
                        </div>
                        <p>我的</p>
                    </div>
                </a>
            </div>
            <%} %>
        </div>
    </div>
    <script>
        $(function () {
            var windowH = $(window).height(); //导航置顶
            var fixedH = (windowH - 60) + 'px';
            $('.float-nav').css('top', fixedH);

            $('.tit-box').click(function () {
                var bro = $('.nav-num');
                var that = $(this);
                that.find('p').addClass('on').parents(bro).siblings(bro).find('p').removeClass('on');
            });
        })
    </script>

    <!-- BEGIN CORE PLUGINS (REQUIRED FOR ALL PAGES) -->
    <!--[if lt IE 9]>
<script src="/assets/plugins/respond.min.js"></script>  
<![endif]-->
    <script type="text/javascript" src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="/assets/plugins/jquery-migrate-1.2.1.min.js"></script>
    <script src="/assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <script src="/assets/plugins/back-to-top.js"></script>
    <script src="/assets/plugins/jQuery-slimScroll/jquery.slimscroll.min.js"></script>
    <script src="/assets/plugins/jquery.cookie.min.js"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL JAVASCRIPTS (REQUIRED ONLY FOR CURRENT PAGE) -->
    <script src="/assets/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
    <script src="/assets/plugins/bxslider/jquery.bxslider.min.js"></script>
    <!-- BEGIN PAGE LEVEL JAVASCRIPTS (REQUIRED ONLY FOR CURRENT PAGE) -->
    <script src="/assets/scripts/app.js"></script>
    <script src="/app_js/swipe.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            App.init();

            if ($.cookie("view_flag") == null) {
                $.cookie("view_flag", "2", { expires: 90, path: '/WeChat' });
            }

            var bullets = document.getElementById('position').getElementsByTagName('li');
            var banner = Swipe(document.getElementById('mySwipe'), {
                auto: 2000,
                continuous: true,
                disableScroll: false,
                callback: function (pos) {
                    var i = bullets.length;
                    while (i--) {
                        bullets[i].className = ' ';
                    }
                    bullets[pos].className = 'cur';
                }
            });

            if (navigator.userAgent.indexOf("UCBrowser") > 0) {
                $("#SpcRecomm").hide();
            }
            else {
                App.initBxSlider();
            }
        });

        <%=config%>
        wx.ready(function () {
            var Storename = $("#Storename").val();
            var Headimgurl = $("#Headimgurl").val();
            var title = "";
            if (Storename != null) {
                title = Storename;
            }
            var link = window.location.href + "?userId=" + $("#UserId").val();
            //在此输入各种API
            //分享到朋友圈
            wx.onMenuShareTimeline({
                title: "微店分享", // 分享标题
                desc: "上海青旅-我的微店分享", // 分享描述
                link: link, // 分享链接
                imgUrl: Headimgurl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
            //分享给朋友
            wx.onMenuShareAppMessage({
                title: "微店分享", // 分享标题
                desc: "上海青旅-我的微店分享", // 分享描述
                link: link, // 分享链接

                imgUrl: Headimgurl, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
        })

        $('#myorder').live("click", function () {
            url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
            $.cookie("loginstep", "member", { expires: 30, path: '/WeChat' });
            $.get(url, function (obj) {
                if (obj.success) {
                    //top.location = "/app/order#orderlist";
                    top.location = "/WeChat/order.aspx#member";
                }
                else {
                    top.location = "/WeChat/order.aspx#login";
                }
            }, 'json');
        });

        $('#myFxOrder').live("click", function () {
            url = "../../WeChat/AjaxService.aspx?action=CheckFxOnline&r=" + Math.random();
            $.cookie("loginstep", "member", { expires: 30, path: '/WeChat' });
            $.get(url, function (obj) {
                if (obj.success) {
                    //top.location = "/app/order#orderlist";
                    top.location = "/WeChat/order.aspx#fxorderlist";
                }
                else {
                    alert("未登录分销账户！");
                }
            }, 'json');
        });

        function check_null() {
            if ($("#search").val() == "" || $("#search").val() == "搜索关键字或目的地名称") {
                alert("请输入搜索关键字");
                return false;
            }
            $.cookie("search", $("#search").val(), { expires: 90, path: '/WeChat' });
        }


    </script>
    <!-- END PAGE LEVEL JAVASCRIPTS -->
</body>
</html>
