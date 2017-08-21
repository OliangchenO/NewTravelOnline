<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shopdetail.aspx.cs" Inherits="TravelOnline.WeChat.freetrip.shopdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no"/>
    <title></title>
     <link href="/WeChat/freetrip/Content/css/common.css" rel="stylesheet" />
    <style>
            .shopdetail_container {
                margin-top:40px;
        
         overflow:hidden;
         padding-bottom:50px;
        }
         .shopdetail_top {
         overflow:hidden;
         }

        .shopdetail_content {
        
        background:white;
        overflow:hidden;
        }

        /*商圈*/
        .tab_shopping {

          width:92%;
       margin-left:4%;
        font-size:13px;
        }
        .shopping_content {
       
        overflow:hidden;
       
        }
        .shoppin_item {
            border:1px solid #eee;
             margin-bottom:10px;
           padding:5px 5px 5px 5px;
           padding-bottom:10px;
           padding-top:10px;
           border-radius:5px;
        
          
        }


.shopping_content .screen {width: 100%;}
.shopping_content .red {color: #ee3c78; font-weight: bold;}
.shopping_content li {display: inline-block; padding: 0 10px;line-height:25px; font-size: 13px;}
.shopping_content .shopping_mall {width: 100%; }
.target_tit {position: relative;}
.shopping_content .target_tit p {font-size: 13px; }
.target_tit .grey {color: #999;}
.target_tit .sale {font-size: 13px; color: #f60; position: absolute; right: 5px; top: 11px;}
.target_tit .sale span {font-size: 16px;}
        .tog_close {
        overflow:hidden;
        }
.tog_close span,.tog_open span {font-size: 13px; color: #fff; display: inline-block; background: #00b0ff;  padding: 0 15px; margin: 0 10px 10px 0; border-radius: 4px;}
.tog_close .zk,.tog_open .zk {background: #26a69a;}
.tog_close .cx,.tog_open .cx {background: #d32f2f;}
.tog_close .zl,.tog_open .zl {background: #f56139;}
.tog_close .store {font-size: 14px;}
.tog_open .xq img {position: relative; top: 5px; width:15px}
        .store img {
            position: relative;
        width:20px;
        top:2px;
        }
.tog_open .coupon {margin: 10px 0; position: relative;}
.tog_open span {float: left; width: 32px; text-align: center; margin: 5px 10px 5px 0;}
.tog_open dl p {float: left; width: 76%; display: block;}
.tog_open dd,.tog_open dt {font-size: 13px; }
.shopping_content .tog_open dd {}
.tog_open .coupon dd {float: right; padding: 0 10px; width: 30%; text-align: center; color: #fff; border-radius: 6px; background: #ee3c78;}
.c9 {color: #999;}
.tog_open .xq p {font-size: 13px; }
.tog_open .xq img {margin-right: 10px;}
        .tog_open p {
            padding-top:5px;
        
        }
.btn_ticket {
        width:96%;
        height:30px;
        line-height:30px;
        color:white;
        border-radius:5px;
        margin:0 auto;
      background: #ff6600; 
      text-align:center;
      font-size:14px;
        margin-top:5px;
        margin-bottom:5px;
           margin-left:2%;
}
        .coupon_div {
        
        border:1px solid #ddd;
        background:#f8f8f8;
        border-radius:5px;padding-top:10px;
        margin-bottom:10px;
        }

        .coupon_dl {
        width:96%;
        margin-left:2%;
        
        }
        .coupon_item {
            padding-bottom:10px;
        
        }
          .coupon_item_upbtn {
        
      width:100%;
     margin-bottom:10px;
   
        }
            .coupon_item_upbtn span {
                float: right;
                margin-right: 5px;
                   
            }

        .coupon_item_downbtn {
          
        float:right;margin-right:5px;margin-top:-16px;
     
        
        }

        .tog_open {
      
        }

        .linedetail_tab_content_item {
        
        
        }

        .shop_articlelist {
        
        margin-top:10px;
        overflow:hidden;
        }

        .show_articleitem {
        margin-top:10px;
        }

      
    </style>
</head>
<body>
   
    <div class="shopdetail_container">
        <div class="shopdetail_top">
                 <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                <span class="common_top_title"><span class="tttspans"></span>商圈详情<span class="tttspan"></span></span>
                <a class="common_top_back" onclick="history.back()" target="_self"><img src="Content/img/arrow_back.png" height="20" /></a>
                <a class="common_top_menu" onclick="$('.common_top_menucontent').toggle();"><img src="Content/img/btn_menu.png" height="25" /></a>
                <div class="common_top_menucontent">
                       <div style="position:absolute;top:-23px;right:15px;"> <img src="content/img/arrow.png" /></div>
                    <ul>
                              <li class="common_top_menucontent_item" onclick="go_page(this)" href="/WeChat/freetriporder.aspx#orderlist">我的订单</li>
                            <li  class="common_top_menucontent_item"  onclick="go_page(this)" href="/WeChat/freetrip/comment.aspx">我的点评</li>
                            <li class="common_top_menucontent_item noborder"  onclick="go_page(this)" href="/WeChat/freetriporder.aspx#coupon">我的优惠券</li>
                    </ul>

                </div>
            </div>

        </div>
        
        <div class="shopdetail_content">
                  <div class='tab_shopping linedetail_tab_content_item'>
                        <div class='shopping_content'>
                       

                            <div class="shopping_list">
                                <div class="shoppin_item">
                                    <div class='shopping_mall'>
                                        <img src='Content/img/3.png' width="100%" />
                                    </div>
                                    <div class='target_tit'>
                                        <p class='grey'>日本 东京</p>
                                     <%--   <p>有乐町ITOCiA</p>--%>
                                        <div class='sale'>优惠<span>5</span>%</div>
                                    </div>
                                    <div class='tog_close common_margin_t5 tog_open_tag'>
                                      
                                     <%--   <p class='store' >
                                            <img src='Content/img/shop.png' >
                                            店家详情
                                          
                                        </p>--%>

                                    </div>
                                    <div class='tog_open'>
                                        <dl  class="activity_list">
                                            <dd class='clearfix'><span class='fx'>返现</span><p>凭xx银行卡购物返现5%凭xx银行卡购物返现5%凭xx银行卡购物返现5%凭xx银行卡购物返现5%</p></dd>
                                            <dd class='clearfix'><span class='zk'>折扣</span><p>点击领取优惠券可享受5%折扣</p></dd>
                                            <dd class='clearfix'><span class='cx'>促销</span><p>商店促销活动</p></dd>
                                            <dd class='clearfix'><span class='zl'>赠礼</span><p>购满xxx元赠送精美礼品一份</p></dd>
                                        </dl>
                                        <div class='coupon'>

                                            <h2 class='common_tab_titlebar'>优惠券>></h2>
                                            <div class="coupon_list">
                                                <div class="coupon_div">
                                                    <dl class='clearfix coupon_dl'>
                                                        <dt>满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享</dt>
                                                        <dt class='c9'>使用期限：2016.04.01-2016.04.30</dt>
                                                 
                                                    </dl>
                                                    <div class="btn_ticket">点击领取</div>
                                                </div>
                                                <div class="coupon_div">
                                                    <dl class='clearfix coupon_dl'>
                                                        <dt>满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享</dt>
                                                        <dt class='c9'>使用期限：2016.04.01-2016.04.30</dt>
                                                  
                                                    </dl>
                                                    <div class="btn_ticket">点击领取</div>
                                                </div>
                                                <div class="coupon_div">
                                                    <dl class='clearfix coupon_dl'>
                                                        <dt>满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享受5%折扣满10000元可享</dt>
                                                        <dt class='c9'>使用期限：2016.04.01-2016.04.30</dt>
                                                 
                                                    </dl>
                                                    <div class="btn_ticket">点击领取</div>
                                                </div>
                                                </div>
                                        </div>
                                        <div class='xq'>
                                            <h2 class='common_tab_titlebar'>店家详情>></h2>
                                            <div class="shopdetail_info">
                                                <p><img  src='Content/img/diamond.png'>有乐町ITOCiA是一座开业于2007年的复合商业设施，兼备购物、餐饮、办公等功能，吸引着20岁左右的年轻人。</p>
                                                <p><img src='Content/img/diamond.png'>有乐町ITOCiA位于有乐町站旁边，在有乐町站JR中央出口可直接到达，在有乐町D7b出口步行约1分钟、银座一点股站2出口步行约2分钟也可到达。</p>
                                                <p><img src='Content/img/diamond.png'>大厦共20层，其中1-8层为购物餐饮区，之上是办公区。商场经营的货物以男女时装、杂货为主，餐饮则包含了中日韩特色料理。</p>
                                            </div>
                                            </div>
                                    </div>

                                    <div class="shop_articlelist">
                                       <%-- <div class="show_articleitem">
                                            <div class="common_tab_titlebar2">321312</div>
                                            <div><img style="width:100%;" /></div>
                                             <div><p></p></div>
                                        </div>--%>

                                    </div>
                                  
                                </div>



                            </div>

                        </div>
                  

                      
                    </div>

        </div>
    </div>
  
</body>
</html>
<script src="/WeChat/freetrip/Scripts/jquery-1.11.3.min.js"></script>
<script src="Scripts/common.js"></script>
<script src="/assets/plugins/jquery.blockui.min.js"></script>
<script>
     var shopdata=eval(<%=shopDetail %>);
    function init() {
        set_shopdetail();
    }

    function set_shopdetail() {
        var tradingArea = shopdata.tradingArea[0];
        //加载信息
        $(".shopping_mall").children("img").attr("src", tradingArea.pic);
        $(".sale").html(tradingArea.flag);
        $(".grey").html(tradingArea.name);
        //加载活动
        $(".activity_list").html("");
        if (shopdata.activity!=null){
        $(shopdata.activity).each(function (index,obj) {
            $(".activity_list").append(set_activity(obj));
        });
        }

        //加载优惠券
        $(".coupon_list").html("");
        if (shopdata.coupon != null) {
            $(shopdata.coupon).each(function (index, obj) {
                $(".coupon_list").append(set_coupon(obj));
            });
        }
       
        //加载商店详情
        $(".shopdetail_info").html("");
        var detail_arr = tradingArea.detail.split(";");
        $(detail_arr).each(function(index,obj){
            $(".shopdetail_info").append(" <p><img style='width:22px;' src='Content/img/diamond.png'>"+obj+"</p>");
        });
      

        //加载文章
        $(".shop_articlelist").html("");
        $(shopdata.store).each(function (index, obj) {
         
            $(".shop_articlelist").append(set_shoparticle(obj));

        });

    }

    function set_shoparticle(obj) {
        return "<div class='show_articleitem'><div class='common_tab_titlebar2'>" + obj.name + "</div><div><img src='" + obj.pic + "' style='width:100%;' /></div><div><p>" + obj.context + "</p></div></div>";
    }


    function set_activity(obj) {
        return " <dd class='clearfix'><span class='fx' style='background:"+obj.color+";'>" + obj.name + "</span><p>" + obj.context + "</p></dd>"
    }

    function set_coupon(obj) {
        var result="";
        result+="<div class='coupon_div'><dl class='clearfix coupon_dl'>";
        result += "<dt>" + obj.context + "</dt>";
        result += "<dt class='c9'>使用期限：" + formatDate(obj.starDate, "yyyy.MM.dd") + "-" + formatDate(obj.endDate, "yyyy.MM.dd") + "</dt></dl>";
        result += "<div class='btn_ticket' tag=" + obj.barCodeImg + " onclick='get_couponcode(this)'>点击领取</div> </div>";
        return result;
    }
  
    function get_couponcode(obj) {
        var barcode = $(obj).attr("tag");
        $.blockUI({
            message: "<img onclick= '$.unblockUI(); ' src='" + barcode + "'  width='100%;'/>",
            css: {
                left: ($(window).width() - 360) / 2 + 'px',
                width:"350px",
                border: 'none',
              padding: '5px',
                'line-height': '50px',
                backgroundColor: '#fff',
                'border-radius': '5px',
                '-webkit-border-radius': '5px',
                '-moz-border-radius': '5px',
               
                opacity: .9,
                color: '#fff'
            }

        });
    }



  

    init();
</script>