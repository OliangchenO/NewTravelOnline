﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_m.aspx.cs" Inherits="TravelOnline.WeChat.main_m" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
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
<link rel="stylesheet" href="http://fonts.useso.com/css?family=Open+Sans:300,300italic,400,400italic,600,600italic,700,700italic" >
<link rel="stylesheet" type="text/css" href="/app_css/time/default.css">
<script src="/assets/plugins/jquery-1.10.2.min.js"></script>
<script src="/app_js/time/jquery.knob.js"></script>
<script src="/app_js/time/jquery.throttle.js"></script>
<script src="/app_js/time/jquery.classycountdown.js"></script>
<style>
    .ClassyCountdownDemo { width:100%; display:block }
</style>
<!--Time END-->
<link href="/app_css/main.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
<!--IndexList START-->
<link rel="stylesheet" href="/app_css/index_list/middle.css" media="screen and (min-width: 320px)">
<script src="/app_js/index_list/lazyload.js"></script>
<script type="text/javascript">
    $(function () {
        $("#container img").lazyload({
            threshold: 400,
            placeholder: "/Images/lazyload.gif",
            effect: "fadeIn"
        });
    });
</script>
<!--IndexList END-->
</head>
<body style="background:#ddd;">
<div class="addWrap">
  <%--<div class="swipe" id="mySwipe">
    <div class="swipe-wrap">
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/1.jpg"/></a></div>
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/2.jpg"/></a></div>
    </div>
  </div>
  <ul id="position"><li class="cur"></li><li class=""></li></ul>--%>
  <% =TravelOnline.WeChat.WeChatClass.GetWeChatFlashAd()%>
</div>

<div class="container">
    <div class="row">
        <div class="search-box" style="padding-left:10px">
            <form onsubmit="javascript:return check_null();" action="/app/search" method="post">
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
            <a href="/app/outbound">
                <div class="tile">
		            <img src="/assets/img/cj.jpg" style="width:100%; height:100%;"/>
                    <div class="span8 text-center pos50">
                        <p class="colorFFF">出境旅游</p>
                    </div>
	            </div>
            </a>
        </div>
        <div class="col-xs-4">
            <a href="/app/inland">
                <div class="tile">
                    <img src="/assets/img/gn.jpg" style="width:100%; height:100%;"/>
                    <div class="span4 text-center pos50">
                        <p class="colorFFF">国内旅游</p>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-xs-4">
            <a href="/app/cruises">
                <div class="tile">
		            <img src="/assets/img/yl.jpg" style="width:100%; height:100%;"/>
                    <div class="span4 text-center pos50">
                        <p class="colorFFF">邮轮旅游</p>
                    </div>
	            </div>
            </a>
        </div>
        <div class="col-xs-4">
            <a href="/app/freetour">
                <div class="tile">
		            <img src="/assets/img/zyx.jpg" style="width:100%; height:100%;"/>
                    <div class="span4 text-center pos50">
                        <p class="colorFFF">自由行</p>
                    </div>
	            </div>
            </a>
        </div>
        <div class="col-xs-4">
            <a href="/app/visa">
                <div class="tile">
		            <img src="/assets/img/qz.jpg" style="width:100%; height:100%;"/>
                    <div class="span4 text-center pos50">
                        <p class="colorFFF">签证</p>
                    </div>
	            </div>
            </a>
        </div>
        <div class="col-xs-8">
        <a href="/app/recommend">
            <div class="tile">
		        <img src="/assets/img/djtj.jpg" style="width:100%; height:100%;"/>
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


<div class="container border-wrap" style="background:#fff;">
    <%= TravelOnline.WeChat.WeChatClass.GetWeChatFlashSale("")%>
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
              <ul class="bxslider" data-slides-phone="1" data-slides-tablet="2" data-slides-desktop="4" data-slide-margin="15">
                <% =TravelOnline.WeChat.WeChatClass.GetWeChatIndexRecom("sticker")%>
              </ul>
            </div>
          <!-- END CONTENT <img src="/Images/Views/200910/M_0910141627241.JPG" class="img-responsive" alt="新航马尔代夫6天4晚（自由行）">-->
        </div>
    </div>        
    <!-- END TWO PRODUCTS & PROMO -->
  </div>
</div>

<!-- BEGIN FOOTER -->
<div class="footer padding-top-5">
    <div class="container">
    <div class="row">
        <!-- BEGIN COPYRIGHT -->
        <div class="col-xs-12">
        <a class="margin-right-20" href="tel:4006777666"><i class="fa fa-phone-square"></i> 4006-777-666</a>
        <a class="margin-left-20" href="http://www.scyts.com">官方网站</a>
        </div>
        <!-- END COPYRIGHT -->
        <!-- BEGIN PAYMENTS -->
        <div class="col-xs-12 padding-top-5 margin-bottom-10">
        2016 © 上海中国青年旅行社
        </div>
        <!-- END PAYMENTS -->
    </div>
    </div>
</div>
<!-- END FOOTER -->
<!-- BEGIN CORE PLUGINS (REQUIRED FOR ALL PAGES) -->
<!--[if lt IE 9]>
<script src="/assets/plugins/respond.min.js"></script>  
<![endif]-->  

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
            $.cookie("view_flag", "2", { expires: 90, path: '/app' });
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

    $('#myorder').live("click", function () {
        url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
        $.cookie("loginstep", "member", { expires: 30, path: '/app' });
        $.get(url, function (obj) {
            if (obj.success) {
                //top.location = "/app/order#orderlist";
                top.location = "/app/order#member";
            }
            else {
                top.location = "/app/order#login";
            }
        }, 'json');
    });

    function check_null() {
        if ($("#search").val() == "" || $("#search").val() == "搜索关键字或目的地名称") {
            alert("请输入搜索关键字");
            return false;
        }
        $.cookie("search", $("#search").val(), { expires: 90, path: '/app' });
    }
</script>
<!-- END PAGE LEVEL JAVASCRIPTS -->
</body>
</html>