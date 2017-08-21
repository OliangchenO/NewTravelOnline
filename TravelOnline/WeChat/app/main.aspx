<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WeChat.app.main" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>上海中国青年旅行社</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0" name="viewport">
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
<link href="/assets/css/style.css" rel="stylesheet" type="text/css">
<link href="/assets/css/style-responsive.css" rel="stylesheet" type="text/css">  
<link href="/assets/css/custom.css" rel="stylesheet" type="text/css">
<!-- Theme styles END -->
<link href="/css/main.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
</head>
<body>
<div class="addWrap">
  <div class="swipe" id="mySwipe">
    <div class="swipe-wrap">
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/1.jpg"/></a></div>
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/2.jpg"/></a></div>
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/3.jpg"/></a></div>
      <div><a href="javascript:;"><img class="img-responsive img-h250" src="/images/4.jpg"/></a></div>
    </div>
  </div>
  <ul id="position">
    <li class="cur"></li>
    <li class=""></li>
    <li class=""></li>
    <li class=""></li>
  </ul>
</div>

<div class="container">
    <div class="tiles row">
        <div class="col-xs-8">
            <div class="tile bg-GREENSEA">
		        <div class="corner">
		        </div>
		        <div class="tile-body">
			        <i class="fa fa-user"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        出境旅游
			        </div>
		        </div>
	        </div>
        </div>
        <div class="col-xs-4">
            <div class="tile bg-TURQUOISE">
		        <div class="tile-body">
			        <i class="fa fa-coffee"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        国内旅游
			        </div>
		        </div>
	        </div>
        </div>
        <div class="col-xs-4">
            <div class="tile bg-PUMPKIN">
		        <div class="tile-body">
			        <i class="fa fa-globe"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        邮轮
			        </div>
		        </div>
	        </div>
        </div>
        <div class="col-xs-4">
            <div class="tile bg-CARROT">
		        <div class="corner">
		        </div>
		        <div class="tile-body">
			        <i class="fa fa-asterisk"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        自由行
			        </div>
		        </div>
	        </div>
        </div>
        <div class="col-xs-4">
            <div class="tile bg-ORANGE">
		        <div class="tile-body">
			        <i class="fa fa-user"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        签证
			        </div>
		        </div>
	        </div>
        </div><div class="col-xs-8">
            <div class="tile bg-BELIZEHOLE col-xs-8">
		        <div class="tile-body">
			        <i class="fa fa-coffee"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        当季推荐
			        </div>
		        </div>
	        </div>
        </div>
        <div class="col-xs-4">
            <div class="tile bg-PETERRIVER col-xs-4">
		        <div class="tile-body">
			        <i class="fa fa-user"></i>
		        </div>
		        <div class="tile-object">
			        <div class="number">
					        我的订单
			        </div>
		        </div>
	        </div>
        </div>
        
        
	</div>	
</div>

<div class="main padding-top-10">
      <div class="container">
        <!-- BEGIN TWO PRODUCTS & PROMO -->
        <div class="row">
        <!-- BEGIN CONTENT -->
        <div class="col-xs-12">
            <h3 class="margin-left-5 row-h3">特别推荐</h3>
            <div class="bxslider-wrapper">
              <ul class="bxslider" data-slides-phone="1" data-slides-tablet="2" data-slides-desktop="4" data-slide-margin="15">
                <li>
                  <div class="product-item">
                    <div class="pi-img-wrapper">
                      <img src="/assets/temp/products/k1.jpg" class="img-responsive" alt="Berry Lace Dress">
                    </div>
                    <h3><a href="item.html">Berry Lace Dress</a></h3>
                    <div class="pi-price">$29.00</div>
                    <a href="#" class="btn btn-default add2cart">去看看</a>
                    <div class="sticker sticker-new"></div>
                  </div>
                </li>
                <li>
                  <div class="product-item">
                    <div class="pi-img-wrapper">
                      <img src="/assets/temp/products/k2.jpg" class="img-responsive" alt="Berry Lace Dress">
                    </div>
                    <h3><a href="item.html">Berry Lace Dress2</a></h3>
                    <div class="pi-price">$29.00</div>
                    <a href="#" class="btn btn-default add2cart">去看看</a>
                  </div>
                </li>
              </ul>
            </div>
          </div>
          <!-- END CONTENT -->
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
        <a class="margin-left-20" href="javascript:;">官方网站</a>
        </div>
        <!-- END COPYRIGHT -->
        <!-- BEGIN PAYMENTS -->
        <div class="col-xs-12 padding-top-5 margin-bottom-10">
        2014 © 上海中国青年旅行社
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
<script src="/assets/plugins/jquery-1.10.2.min.js"></script>
<script src="/assets/plugins/jquery-migrate-1.2.1.min.js"></script>
<script src="/assets/plugins/bootstrap/js/bootstrap.min.js"></script>      
<script src="/assets/plugins/back-to-top.js"></script>
<script src="/assets/plugins/jQuery-slimScroll/jquery.slimscroll.min.js"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL JAVASCRIPTS (REQUIRED ONLY FOR CURRENT PAGE) -->
<script src="/assets/plugins/fancybox/source/jquery.fancybox.pack.js"></script>
<script src="/assets/plugins/bxslider/jquery.bxslider.min.js"></script>
<!-- BEGIN PAGE LEVEL JAVASCRIPTS (REQUIRED ONLY FOR CURRENT PAGE) -->
<script src="/assets/scripts/app.js"></script>
<script src="/js/swipe.js"></script> 
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();

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
        }
        else {
            App.initBxSlider();
        }
    });
</script>
<!-- END PAGE LEVEL JAVASCRIPTS -->
</body>
</html>
