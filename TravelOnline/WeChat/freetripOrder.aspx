<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="freetripOrder.aspx.cs" Inherits="TravelOnline.WeChat.freetripOrder" %>
<!DOCTYPE html>
<!--[if IE 8]> <html lang="zh" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="zh" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!--><html lang="zh" class="no-js"> <!--<![endif]-->
<head>
<meta charset="utf-8">
<title></title>
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
<link href="/newcss/loginreg.css" rel="stylesheet" type="text/css" />
<!-- Theme styles END -->
    <link href="/app_css/freetrip_custom.css" rel="stylesheet" />
<link rel="shortcut icon" href="~/favicon.ico">



<!--百度统计3.0 账号:shbvip-scyts，更新日期2016/1/27-->
        <script>
var _hmt = _hmt || [];
(function() {
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
		(function() {
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
		(function() {
		  var hm = document.createElement("script");
		  hm.src = "//hm.baidu.com/hm.js?d3779a2d6ba03f2452d29c67eeaa3a08";
		  var s = document.getElementsByTagName("script")[0]; 
		  s.parentNode.insertBefore(hm, s);
		})();
		</script>

	<!--百度统计3.0 END-->
	
</head>
<body id="mainbody">
<!-- BEGIN HEADER -->
<div id="header" class="pre-header" style="position: fixed; top: 0px; left: 0px;width:101%">
    <div class="container">
        <div class="row">
            <a class="icon_back" href="javascript:;" onclick="javascript:history.go(-1)"><i class="fa fa-reply"></i></a>
            <div class="tit" id="titlename"></div>
            <%if (null != Session["UserFrom"] && Session["UserFrom"].ToString().Equals("APP")) {%>
            <a class="icon_home" href="/app/Navmain"><i class="fa fa-home"></i></a>
            <%}else if (null != Session["Fx_Login"]){%>
            <a class="icon_home" href="/WeChat/main_fx.aspx"><i class="fa fa-home"></i></a>
            <%}else if (null == Session["Fx_Login"] && null != Session["Fx_UserId"]){%>
            <a class="icon_home" href="/WeChat/main_fx.aspx?userId=<%= Session["Fx_UserId"]%>"><i class="fa fa-home"></i></a>
            <% }else{ %>
            <a class="icon_home" href="/app/main"><i class="fa fa-home"></i></a>
            <%} %>
            <a class="icon_home" href="http://mp.weixin.qq.com/s?__biz=MjM5MTYxNzUyMA==&mid=201422557&idx=1&sn=944a0be1e76b58f2abf543b0763ca2e6&scene=18&scene=1&srcid=0127FqZ7ZUCvw9C4De7ImbwB#rd"><img src="/Images/iconfont-font47.png"/></a>
        </div>
    </div>
    <div id="inputs" style="DISPLAY:none">
        <input id="page_url" type="hidden" value="" />
        <input id="s_pages" type="hidden" value="1"/>
    </div>
    <form id="form_data" onsubmit="return false;" method="post">
        <input id="PriceStrings" name="PriceStrings" type="hidden" value=""/>
    </form>
</div>
<!-- END HEADER -->
<div id="main_view" style="margin-top: 45px;margin-bottom: 50px;">
    <%--<div class="sub_view" style="display:none;" id="orderdetail_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>订单号：100695</h3>
                    <div>线路：“欢度春节~新马4晚5日·全程四-五星”香港转机</div>
                    <div>日期：2014-07-20</div>
                    <div>人数：2成人 1儿童</div>
                    <div>费用：<span class="o-price">¥1000.00</span></div>
                    <div>状态：占位</div>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>费用明细</h3>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>付款方式</h3>
                    <div style="font-size: 14px;line-height:25px">
                        <div class="infos">普陀营业部 武宁路225号西宫内（河边大道4号）</div>
                        <div class="infos">
                            <div class="oinfo">联系人：秦钢，电话：32130227、62441490，邮编：200063，营业时间：08:30-19:30</div>
                            <div class="oname">门店地图：</div>
                            <div class="mapoinfo"><img src="/Images/BranchMap/61.jpg"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="sub_view" style="display:none;" id="ordersave_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>预订结果</h3>
                    <div style="font-size: 14px;line-height:30px">由于系统通讯原因，您的订单未能成功提交，系统将您的订单自动暂存，您随时可以对此订单进行修改，待系统通讯恢复正常或我们的工作人员和您联系后，您可以再次提交此订单，对此给您造成的不便，敬请谅解。</div>
                </div>
            </div>
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>联系方式</h3>
                    <ul>
                        <li>电话：<a class="margin-right-20" href="tel:02134014501">021-34014501</a></li>
                        <li>传真：021-64742928(出境)&nbsp;&nbsp;021-64670982(国内)</li>
                        <li>上海中国青年旅行社有限公司</li>
                        <li>联系地址：上海市徐汇区衡山路2号（200031）</li>  
                        <li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>    
                    </ul>
                </div>
            </div>
        </div>
        <div class="pre-footer order-footer"  style="position: fixed; bottom: -1px; left: 0px;width:101%">
            <div class="container">
                <div class="row">
                    <div class="col-xs-7" style="text-align:left;"></div>
                    <div class="col-xs-5" style="text-align:center"><a class="yd cur" href="app/main"><i class="fa fa-chevron-circle-right"></i> 返回首页</a></div>
                </div>
            </div>
        </div>
    </div>
    <div class="sub_view" style="display:none;" id="login_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>登录</h3>
                    <form id="loginform" onsubmit="return false;" method="post">
                    <input required id="loginname" type="text" name="loginname" class="form-control ordertext" placeholder="手机号码或邮件地址" maxlength="50">
                    <input required id="loginpwd" type="password" name="loginpwd" class="form-control ordertext" placeholder="登录密码（6-20位字符）" maxlength="20">
                    <input type="text" id="authcode" name="authcode" class="form-control1 ordertext" placeholder="验证码"maxlength="6" style="width:100px;"> 
					<img id="Verification" style="WIDTH: 80px; HEIGHT: 30px; CURSOR: pointer" onclick="this.src='/login/VerifyCode.aspx?&uid=<%=ucode %>&yys='+new Date().getTime()" src="../../login/VerifyCode.aspx?&uid=<%=ucode %>" alt="" style="cursor:pointer;width:100px;height:26px;"> 
					<label class="ftx23">&nbsp;<a href="javascript:;" onclick="verc()">看不清？换一张</a></label>
                    </form>
                </div>
                <div class="recommend_txt" style="margin:20px 20px;text-align:center">
                    
                </div>
                <div class="row" style="margin:20px 20px;text-align:center">
                    <div class="col-xs-5"><a class="btn yellow" href="javascript:;" id="login">登录</a></div>
                    <div class="col-xs-7"><a class="btn yellow" href="javascript:;" id="regnew">注册新用户</a></div>
                </div>
                <%if (null != Session["UserFrom"] && Session["UserFrom"].ToString().Equals("APP")) {%>
                <%}else{ %>
                <dl class="other-login">
                <dt>使用合作网站账号登录</dt>
                <dd>
                    <a href="/WeChat/WeiXinLogin.aspx?state=Wx_regedit" class="sina" id="weixinLogin" target="_blank">微信</a>
                </dd>
                </dl>
                <%} %>
            </div>
        </div>
    </div>
    <div class="sub_view" style="display:none;" id="reg_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>注册新用户</h3>
                    <form id="regform" onsubmit="return false;" method="post">
                    <input id="regname" type="text" name="regname" class="form-control ordertext" placeholder="您的姓名" maxlength="10">
                    <input id="regphone" type="text" name="regphone" class="form-control ordertext" placeholder="手机号码" maxlength="11">
                    <input id="regemail" type="text" name="regemail" class="form-control ordertext" placeholder="电子邮件" maxlength="50">
                    <input id="regpwd" type="password" name="regpwd" class="form-control ordertext" placeholder="登录密码（6-20位字符）" maxlength="20">
                    </form>
                </div>
                <div class="recommend_txt" style="margin:20px 20px;text-align:center">
                    <a class="btn yellow" href="javascript:;" id="regnow">提交注册</a>
                </div>
            </div>
        </div>
    </div>
    <div class="sub_view" style="display:none;" id="member_view">
        <div class="recommend_wrap">
            <div class="recommend_detail">
                <div class="recommend_txt">
                    <h3>会员中心</h3>
                    <div class="tiles row">
                        <div class="col-xs-12">
                            <a href="javascript:;" id="myorder">
                                <div class="tile1 bg-CARROT">
		                            <div class="corner">
		                            </div>
		                            <div class="tile1-body">
			                            <i class="fa fa-shopping-cart"></i>
		                            </div>
		                            <div class="tile1-object">
			                            <div class="number">
					                            我的订单
			                            </div>
		                            </div>
	                            </div>
                            </a>
                        </div>
                        <div class="col-xs-12">
                            <a href="javascript:;" id="mycoupon">
                                <div class="tile1 bg-BELIZEHOLE">
		                            <div class="tile1-body">
			                            <i class="fa fa-tags"></i>
		                            </div>
		                            <div class="tile1-object">
			                            <div class="number">
					                            我的优惠券
			                            </div>
		                            </div>
	                            </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
                    
    </div>
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
<script src="/assets/plugins/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js"></script>
<script src="/assets/plugins/iCheck/icheck.js"></script>
<!-- END CORE PLUGINS -->
<script src="/assets/scripts/app.js"></script>
<script src="/app_js/order.js"></script>
<script type="text/javascript">
    
    $('#myorder').live("click", function () {
        url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
        $.cookie("loginstep", "myorder", { expires: 30, path: '/WeChat' });
        $.get(url, function (obj) {
            if (obj.success) {
                state = { action: "orderlist", url: "#orderlist" };
                history.pushState(state, "我的订单", "#orderlist");
                LoadPage("orderlist");
            }
            else {
                state = { action: "login", url: "#login" };
                history.pushState(state, "用户登录", "#login");
                LoadPage("login");
            }
        }, 'json');
    });

    $('#mycoupon').live("click", function () {
        url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
        $.cookie("loginstep", "mycoupon", { expires: 30, path: '/WeChat' });
        $.get(url, function (obj) {
            if (obj.success) {
                state = { action: "coupon", url: "#coupon" };
                history.pushState(state, "我的优惠券", "#coupon");
                LoadPage("coupon");
            }
            else {
                state = { action: "login", url: "#login" };
                history.pushState(state, "用户登录", "#login");
                LoadPage("login");
            }
        }, 'json');
    });
</script>
</body>
</html>
