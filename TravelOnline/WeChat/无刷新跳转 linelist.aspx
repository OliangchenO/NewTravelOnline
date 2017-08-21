<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="linelist.aspx.cs" Inherits="TravelOnline.WeChat.linelist" %>
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
<link href="/app_css/custom.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
</head>
<body id="mainbody">
<div id="linelist_view">
<!-- BEGIN HEADER -->
<div class="pre-header" style="position: fixed; top: 0px; left: 0px;width:101%">
    <div class="container">
        <div class="row">
            <a class="icon_back" href="javascript:;" onclick="javascript:history.go(-1)"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename"><%=typename %></div>
            <a class="icon_home" href="/app/main"><i class="fa fa-home"></i></a>
        </div>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="s_typename" type="hidden" value="<%=typename %>"/>
        <input id="s_linetype" type="hidden" value="<%=linetype %>"/>
        <input id="s_filter" type="hidden" value="1"/>
        <input id="s_dest" type="hidden" value="0"/>
        <input id="s_pages" type="hidden" value="1"/>
        <input id="s_navbar" type="hidden" value="1"/>
    </div>
</div>
<!-- END HEADER -->
<div role="navigation" class="navbar header no-margin" style="margin-top: 45px;">
    <div class="container">
        <div class="mynavbar-header">
            <div class="cart-block" >
                <i class="glyphicon glyphicon-map-marker"></i>
                <div class="cart-info">
                    <a href="javascript:void(0);" class="cart-info-count" id="linkname"><%=typename %></a>
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
            <button id="viewflag2" class="navbar-toggle" type="button" value="2">
                <i class="fa fa-list-ul"></i>
            </button>
            <button id="viewflag1" class="navbar-toggle" type="button" value="1">
                <i class="fa fa-picture-o"></i>
            </button>
        </div>
        
    </div>
</div>
<div class="bxslider-wrapper" style="margin-top: -10px;">
    <ul class="bxslider">
    <%--<li>
        <div class="product-item">
        <div class="pi-img-wrapper">
            <img src="/assets/temp/products/k1.jpg" class="img-responsive" alt="Berry Lace Dress">
        </div>
        <h3><a href="item.html">Berry Lace Dress Berry Lace Dress Berry Lace Dress</a></h3>
        <div class="pi-price">$29.00</div>
        <a href="#" class="btn btn-default add2cart">去看看</a>
        </div>
    </li>--%>
    </ul>
</div>
<div class="text-center" style="margin-bottom: 50px;margin-top: -10px;">
    <a id="serch-next" href="javascript:;" class="btn btn-default"></a>
</div>
<!-- BEGIN FOOTER -->
<div class="clearfix"></div>
<div class="clearfix"></div>
<div class="pre-footer"  style="position: fixed; bottom: -1px; left: 0px;width:101%">
    <div class="container">
    <div class="row">
        <div class="col-xs-3"><a class="px cur" tag="1">推荐<i class="fa fa-thumbs-o-up"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="2">天数<i id="sort-days" class="fa fa-arrow-up"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="4">价格<i class="fa fa-arrow-down"></i></a></div>
        <div class="col-xs-3"><a class="px" tag="5">价格<i class="fa fa-arrow-up"></i></a></div>
    </div>
    </div>
</div>
<!-- END FOOTER -->
</div>
<!-- BEGIN CORE PLUGINS (REQUIRED FOR ALL PAGES) -->
<!--[if lt IE 9]>
<script src="/assets/plugins/respond.min.js"></script>
<![endif]-->  
<script src="/assets/plugins/jquery-1.10.2.min.js"></script>
<script src="/assets/plugins/jquery-migrate-1.2.1.min.js"></script>
<script src="/assets/plugins/bootstrap/js/bootstrap.min.js"></script>      
<script src="/assets/plugins/back-to-top.js"></script>
<script src="/assets/plugins/jQuery-slimScroll/jquery.slimscroll.min.js"></script>
<script src="/assets/plugins/jquery.blockui.min.js"></script>
<script src="/assets/plugins/jquery.cookie.min.js"></script>
<!-- END CORE PLUGINS -->
<script src="/assets/scripts/app.js"></script>
<script type="text/javascript">
    var state = { action: "list", title: "", url: "" };
    //history.pushState(state);
    
    jQuery(document).ready(function () {
        if ($.cookie("view_flag") == null) {
            $.cookie("view_flag", "1", { expires: 90, path: '/app' });
        }
        else {
            $("#s_navbar").val($.cookie("view_flag"));
            if ($.cookie("view_flag") == "2") {
                $("#viewflag2").addClass("cur");
            }
            else {
                $("#viewflag1").addClass("cur");
            }
        };
        App.init();
        $("#serch-next").hide();
        App.blockUI({ boxed: true });
        LoadList();
        history.pushState(state);

        $('.navbar-toggle').click(function () {
            $("#s_navbar").val($(this).val());
            $.cookie("view_flag", $(this).val(), { expires: 30, path: '/app' });
            $('.navbar-toggle').removeClass("cur");
            $(this).addClass("cur");
            if ($('.product-item').hasClass('product-list')) {
                $('.product-item').removeClass("product-list");
            } else {
                $('.product-item').addClass("product-list");
            }
        });

        $('.pre-footer .px').click(function () {
            var filter = $(this).attr("tag")
            if ($(this).attr("tag") == "2") {
                if ($("#s_filter").val() == "2") filter = "3";
            }
            if (filter == $("#s_filter").val()) return;
            $('.pre-footer .px').removeClass("cur");
            $(this).addClass("cur");
            $("#s_filter").val(filter);
            if ($("#s_filter").val() == "3") {
                $('#sort-days').removeClass("fa-arrow-up");
                $('#sort-days').addClass("fa-arrow-down");
            }
            if ($("#s_filter").val() == "2") {
                $('#sort-days').removeClass("fa-arrow-down");
                $('#sort-days').addClass("fa-arrow-up");
            }
            $("#s_pages").val("1");
            App.blockUI({ boxed: true });
            LoadList();
            scroll(0, 0);
        });

        $('#serch-next').click(function () {
            if ($("#serch-next").html() == "没有更多线路了") {
                return false;
            } else {
                $("#serch-next").html("<img src='/images/ajax-loader.gif'> 努力加载中...");
                LoadList()
            }
        });


    });

    function LoadList() {
        var url = "../../WeChat/AjaxService.aspx?action=LoadLineList&navbar=" + $("#s_navbar").val() + "&linetype=" + $("#s_linetype").val() + "&filter=" + $("#s_filter").val() + "&dest=" + $("#s_dest").val() + "&pages=" + $("#s_pages").val();
        //window.open(url);
        $.getJSON(url, function (date) {
            if (date.pagecount == 0) {
                $(".bxslider").append(date.content);
                $("#serch-next").hide();
            }
            else {
                if ($("#s_pages").val() == "1") $(".bxslider").html("");
                $("#serch-next").html("点击继续加载...");
                $("#serch-next").show();
                if (date.pages == date.pagecount) $("#serch-next").hide();
                $("#s_pages").val(date.pages);
                $(".bxslider").append(date.content);
                //$(".bxslider").append(date.content).trigger('create');
            }
            App.unblockUI();
        })
    }

    //window.onhashchange = function () { alert(window.location.hash) }
    window.onpopstate = function (e) {
        document.title = $("#s_typename").val();
        switch (e.state.action) {
            default:
                document.title = $("#s_typename").val();
                //$(".bxslider").show();display: none
                //$(".bxslider").css("display", "block");
                $("#linelist_view").removeAttr("style")
                break;
            case "line":
                document.title = e.state.title;
                //$(".bxslider").hide();
                $("#linelist_view").css("display", "none");
                break;
        }
    }

    $('.bxslider a').live("click", function () {
        //alert($(this).html());
        document.title = $(this).html();
        state = { action: "line", title: $(this).html(), url: $(this).attr("tag") };
        history.pushState(state, $(this).html(), $(this).attr("tag"));
        //$(".bxslider").hide();
        $("#linelist_view").css("display", "none");
    });

//    if (history && history.pushState) {
//        var loaded = false;
//        $(window).bind("popstate", function () {
//            if (!loaded) {
//                loaded = true;
//            } else {
//                $.getScript(location.href);
//            }
//        });
//    }
</script>
</body>
</html>
