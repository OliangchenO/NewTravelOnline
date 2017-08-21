<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order.aspx.cs" Inherits="TravelOnline.WeChat.order" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title>订单详情</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0" name="viewport">
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
<link href="/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css" rel="stylesheet" type="text/css">
<link href="/assets/plugins/iCheck/skins/square/grey.css" rel="stylesheet" type="text/css">
<!-- Theme styles END -->
<link href="/app_css/custom.css" rel="stylesheet">
<link rel="shortcut icon" href="~/favicon.ico">
</head>
<body id="mainbody">
<!-- BEGIN HEADER -->
<div id="header" class="pre-header" style="position: fixed; top: 0px; left: 0px;width:101%">
    <div class="container">
        <div class="row">
            <a class="icon_back" href="javascript:;" onclick="javascript:history.go(-1)"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename">订单详情</div>
            <%--<a class="icon_home" href="/app/main"><i class="fa fa-home"></i></a>--%>
        </div>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="page_url" type="hidden" value="" />
    </div>
    <form id="form_data" onsubmit="return false;" method="post">
        <input id="PriceStrings" name="PriceStrings" type="hidden" value=""/>
    </form>
</div>
<!-- END HEADER -->
        
<div id="main_view" style="margin-top: 45px;margin-bottom: 50px;"></div>
    <%--<div class="sub_view" id="first_view">
        <div class="product_wrap" style="margin-top: 45px;">
            <h3>“欢度春节~新马4晚5日·全程四-五星”香港转机</h3>
            <p class="xdesc"><h2><i class="fa fa-calendar"></i> 出发日期: 2014-07-27</h2></p>
            <div class="price_box" style="font-size: 16px;"><i class="fa fa-cny"></i> <span class="allprice price">199.00</span></div>
        </div>
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>基本费用</h3>
                    <div class="row">
                        <div class="col-xs-7">
                            <div class="pricename">成人价</div>
                            <div class="pricememo">成人价发的规范的广泛的郭德纲的股份的法国队</div>
                        </div>
                        <div class="col-xs-5">
                            <div style="width:100px"><input tagid="" price="" type="text" class="form-control touch" value=2 max=3 min=1></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-7">
                            <div class="pricename">儿童价</div>
                            <div class="pricememo">成人价发的规范的广泛的郭德纲的股份的法国队</div>
                        </div>
                        <div class="col-xs-5">
                            <div style="width:100px"><input id="Text1" type="text" name="child_num" class="form-control touch" style="font-size:20px;width:40px;font-weight:bold"  max=2 min=1></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>必选费用</h3>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>可选费用</h3>
                </div>
            </div>
        </div>
    </div>
    <div class="sub_view" style="" id="second_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>联系人信息</h3>
                    <form id="form1" onsubmit="return false;" method="post">
                    <input id="ordername" type="text" name="ordername" class="form-control ordertext" placeholder="联系人姓名">
                    <input id="orderphone" type="text" name="orderphone" class="form-control ordertext" placeholder="手机号码">
                    <input id="orderemail" type="text" name="orderemail" class="form-control ordertext" placeholder="电子邮件">
                    <input id="ordermemo" type="text" name="ordermemo" class="form-control ordertext" placeholder="订单特别说明">
                    <input id="Preference" type="hidden" value="">
                    </form>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt" id="Branch">
                    <h3>
                        <input class="pay" value="1" type="radio" id="radio-1" name="iCheck" checked><label for="radio-1" class="hovers">在线支付</label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <input class="pay" value="2" type="radio" id="radio-2" name="iCheck"><label for="radio-2" class="hovers">门店支付</label>
                    </h3>
                    <div id="Pre1">
                        <div class="pre">在线支付每人立减 500 元</div>
                        <div>请于订单确认后24小时之内，通过网上支付方式付清全部费用！</div>
                    </div>
                    <select id="Pre2" class="form-control Branch" style="display:none">
                        <%=BranchOption %>
                    </select>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3><input type="checkbox" id="checkbox-1"><label for="checkbox-1" class="hovers">需要发票</label></h3>
                    <div>不需要发票</div>
                    <div><input id="Text5" type="text" name="child_num" class="form-control ordertext" placeholder="发票抬头"></div>
                </div>
            </div>
        </div>
    </div>
    --%>

<%--<div class="clearfix"></div>
<div class="pre-footer order-footer"  style="position: fixed; bottom: -1px; left: 0px;width:101%">
    <div class="container">
        <div class="row">
            <div class="col-xs-8" style="text-align:left;"><i class="fa fa-user"></i> 注册或登录</div>
            <div class="col-xs-4" style="text-align:center"><a class="yd cur" href="javascript:;" id="ordernow"><i class="fa fa-chevron-circle-right"></i> 下一步</a></div>
        </div>
    </div>
</div>--%>
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
<script src="/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js"></script>
<script src="/assets/plugins/iCheck/icheck.js"></script>
<!-- END CORE PLUGINS -->
<script src="/assets/scripts/app.js"></script>
<script src="/app_js/order.js"></script>
<script type="text/javascript">
//    $("touch").TouchSpin({
//        initval: 1,
//        min: $("#child_num").attr("min"),
//        max: 3,
//        buttondown_class: "btn quantity-down",
//        buttonup_class: "btn quantity-up"
//    });

//    $(".touch").each(function () {
//        $(this).TouchSpin({
//            initval: 1,
//            min: $(this).attr("min"),
//            max: $(this).attr("max"),
//            buttondown_class: "btn quantity-down",
//            buttonup_class: "btn quantity-up"
//        });
//        $(this).bind('change', function () {
//            //alert($(this).val());
//        });
//    });

    $(document).ready(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-grey',
            radioClass: 'iradio_square-grey',
            increaseArea: '20%' // optional
        });

        $('input').on('ifChecked', function (event) {
            RadioSet($(this).val());
        });

    });

    function RadioSet(vals) {
        if (vals == "1") {
            $("#Pre2").hide();
            $("#Pre1").show();
        }
        else {
            $("#Pre1").hide();
            $("#Pre2").show();
        }
    };
</script>
</body>
</html>
