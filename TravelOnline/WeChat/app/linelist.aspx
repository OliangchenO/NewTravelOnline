<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="linelist.aspx.cs" Inherits="WeChat.app.linelist" %>
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
<!-- Theme styles START -->
<link href="/assets/css/style-metronic.css" rel="stylesheet" type="text/css">
<link href="/assets/css/style.css" rel="stylesheet" type="text/css">
<link href="/assets/css/style-responsive.css" rel="stylesheet" type="text/css">  
<!-- Theme styles END -->
<link href="/css/custom.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
</head>
<body>
<div class="pre-header" style="position: fixed; top: 0px; left: 0px;">
    <div class="container">
        <div class="row">
            <a class="icon_back"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename">出境旅游</div>
            <a class="icon_home"><i class="fa fa-home"></i></a>
        </div>
    </div>        
</div>

<!-- BEGIN HEADER -->
<div role="navigation" class="navbar header no-margin" style="margin-top: 45px;">
    <div class="container">
        <div class="mynavbar-header">
            <div class="cart-block" >
                <i class="glyphicon glyphicon-map-marker"></i>
                <div class="cart-info">
                    <a href="javascript:void(0);" class="cart-info-count">日本玛丽希亚芭芭拉</a>
                </div>
                <div class="cart-content-wrapper">
                    <div class="cart-content">
                    <ul class="scroller" style="height: 250px;">
                        <li></li>
                    </ul>
                    <div class="text-right">
                        <a href="shopping-cart.html" class="btn btn-default">View Cart</a>
                    </div>
                    </div>
                </div>
            </div>
            <button class="navbar-toggle" type="button" value="2">
                <i class="fa fa-list-ul"></i>
            </button>
            <button class="navbar-toggle cur" type="button" value="1">
                <i class="fa fa-picture-o"></i>
            </button>
        </div>
        
    </div>
</div>
<!-- END HEADER -->



<%--<div class="clearfix"></div>--%>
<div class="bxslider-wrapper"   style="margin-bottom: 70px;margin-top: -10px;">
    <ul class="bxslider">
    <li>
        <div class="product-item">
        <div class="pi-img-wrapper">
            <img src="/assets/temp/products/k1.jpg" class="img-responsive" alt="Berry Lace Dress">
        </div>
        <h3><a href="item.html">Berry Lace Dress Berry Lace Dress Berry Lace Dress</a></h3>
        <div class="pi-price">$29.00</div>
        <a href="#" class="btn btn-default add2cart">去看看</a>
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
<%--<div class="main1 padding-top-20">
      <div class="container1">
        <!-- BEGIN TWO PRODUCTS & PROMO -->
        <div class="row1">
        <!-- BEGIN CONTENT -->
        <div class="col-xs-121">
            
          </div>
          <!-- END CONTENT -->
        </div>        
        <!-- END TWO PRODUCTS & PROMO -->
    </div>
</div>--%>
<!-- BEGIN FOOTER -->
<div class="clearfix"></div>
<div class="clearfix"></div>
<div class="pre-footer"  style="position: fixed; bottom: 0px; left: 0px;">
    <div class="container">
    <div class="row">
        <!-- BEGIN COPYRIGHT -->
        <div class="col-xs-3"><a class="px cur" tag="1">推荐<i class="fa fa-thumbs-o-up"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="2">天数<i id="sort-days" class="fa fa-arrow-up"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="4">价格<i class="fa fa-arrow-down"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="5">价格<i class="fa fa-arrow-up"></i></a></div>
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
<script src="/assets/scripts/app.js"></script>
<script src="/js/swipe.js"></script> 
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        //App.smartFloat('.pre-header');


        $('.navbar-toggle').click(function () {
            //alert($(this).val());
            $('.navbar-toggle').removeClass("cur");
            $(this).addClass("cur");
            if ($('.product-item').hasClass('product-list')) {
                $('.product-item').removeClass("product-list");
            } else {
                $('.product-item').addClass("product-list");
            } 
        });

        $('.pre-footer .px').click(function () {
            //alert($(this).attr("tag"));
            $('.pre-footer .px').removeClass("cur");
            $(this).addClass("cur");
            if ($(this).attr("tag") == "2") {
                if ($('#sort-days').hasClass('fa-arrow-up')) {
                    $('#sort-days').removeClass("fa-arrow-up");
                    $('#sort-days').addClass("fa-arrow-down");
                } else {
                    $('#sort-days').removeClass("fa-arrow-down");
                    $('#sort-days').addClass("fa-arrow-up");
                } 
            }
            
        });
    });
</script>
<!-- END PAGE LEVEL JAVASCRIPTS -->
</body>
</html>