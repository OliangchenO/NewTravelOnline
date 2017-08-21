<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="linedetail.aspx.cs" Inherits="TravelOnline.WeChat.freetrip.linedetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no"/>
    <title></title>
    <link href="/WeChat/freetrip/Content/css/common.css" rel="stylesheet" />
     <style>
        .linedetail {
        
         overflow:hidden;
         padding-bottom:50px;
        }
         .linedetail_top {
         overflow:hidden;
         }

       .index_banner {
        margin-top:40px;
        position:relative;
        }
  


    /*banner*/

        #wrapper_banner {
            width:100%;
            height:200px;
            overflow:hidden;
        }

        #scroller_banner{
     
	    height:200px;
	    float:left;
	    padding:0;
        
        }



        #scroller_banner li {
	        display:inline-block;
	        display:block;
    
             float:left;
	        height:200px;

        }

            #scroller_banner img {
            height:200px;
            }
        .banner_page {
            
            position:absolute;
         
            top:0px;
             z-index:100;
             overflow:hidden;
             text-align:center;
             width:100%;
           
             overflow:hidden;
               top:180px;
            }

            .banner_page_list {
            
                clear:both;
                float:none;
                margin:0 auto;
                list-style:none;
                overflow:hidden;
              
            }

                .banner_page_default{
                   
                    background:#bbb;
                   display:block;
                   display:inline-block;
                   width:5px;
                   height:5px;
                   margin-left:7px;
                   border-radius:15px;
                opacity:0.5;
                }

            .banner_page_cur {
                background:white;
              opacity:1;
            }



                /*banner*/


                /**top */


        .linedetail_top {
        
        margin-bottom:10px;
        }
            .linedetail_top_content {
                padding-left:4%;
             padding-right:4%;
             background:white;
             border-bottom:1px solid #eee;
             overflow:hidden;
              padding-bottom:10px;
                   padding-top:10px;
            }

                .linedetail_top_content div {
              margin-top:2px;
                line-height:25px;
                }
                .linedetail_top_content p {
                    width:92%;
                    
                    overflow : hidden;
                  /*text-overflow: ellipsis;
                  display: -webkit-box;
                  -webkit-line-clamp: 1;
                  -webkit-box-orient: vertical;*/
                  font-size:12px;
                  font-size:14px;
                  line-height:20px;
                  font-weight:bold;
                }

        .linedetail_top_keyword {
        float:left;
        line-height:25px;
        color:#aaa;
        font-size:12px;

        }

        .linedetail_top_money {
            float:right;
           line-height:25px;
         color:#ff6600;

        font-size:15px;
        
        }


        /**top*/

        .line_reason {
            list-style:none;
            font-size:13px;
            width:92%;
            margin-left:4%;
        overflow:hidden;
     
        }
            .line_reason li {
                 line-height:20px;
            
            }


            /***/
        .line_date {
            list-style:none;
          width:92%;
            margin-left:4%;
                 overflow:hidden;
                 width:100%;
        }

        .line_date li{
         line-height:20px;
         float:left;
         margin-right:10px;
       font-size:13px;
        
        }

        /**tab */

        .linedetail_content {
        margin-bottom:10px;
        background:white;
       
        overflow:hidden;
        }

        .linedetail_tab {
            list-style:none;
            overflow:hidden;
            background:white;
               border-bottom:1px solid #ddd;
            
        }
            .linedetail_tab_item {
                list-style:none;
                display:block;
                display:inline-block;
                width:20%;
                text-align:center;
                float:left;
                margin-top:5px;
                
                line-height:20px;
                font-size:14px;
             
            }

        .linedetail_tab_item_hover {
         border-bottom:2px solid #ec407a;
          
        }


        .linedetail_tab_content {
        background:white;
       overflow:hidden;
       padding-bottom:10px;
        }

        
   
        /**tab detail*/
        .tab_detail{
        width:92%;
       margin-left:4%;
        font-size:13px;
        }

        .tab_detail_flight {
        width:100%;
        overflow:hidden;
        padding-top:10px;
        padding-bottom:10px;
        border-bottom:1px dashed #ddd;
        
        }

        .tab_detail_flight_left {
            width:15%;
         
            overflow:hidden;
            text-align:center;
            float:left;
            line-height:50px;
        
        }

          .tab_detail_flight_right {
            width:80%;
            overflow:hidden;
            float:left;
              border-left:1px solid #ccc;
              padding-left:4%;
        }

          .tab_detail_flightlist{
              list-style:none;
              width:100%;
              overflow:hidden;
          }

        .tab_detail_flightlist_item{
            float:left;
            width:45%;
            line-height:25px;
          
        }

        .tab_detail_flightlist_itemimg {
             float:left;
            width:10%;
            overflow:hidden;
        }

            .tab_detail_flightlist_itemimg img {
                width:15px;
                margin-top:10px;
            }

        .tab_detail_flight_content {
            width:100%;
            text-align:left;
       
        }
        .hotel_info {
        margin-top:5px;
        width:100%;
        overflow:hidden;
        }

            .hotel_info p ,.hotel_info span{
            
            float:left;
            line-height:20px;
            }

            .hotel_info p {
            font-weight:bold;
            width:80%;
           
            }

        .hotel_maininfo {
        font-weight:bold;
        font-size:14px;
      
        }
            

        /*行程*/
           .tab_trip{
        width:92%;
       margin-left:4%;
        font-size:13px;
        }
.trip_pic img,.cp_link ul img,.shopping_mall img {width: 100%;}
.trip_content h2 span {padding-right: 20px;}
.trip_content p {font-size: 13px; }
.trip_content .trip_pic {width: 100%; height: 100px; overflow: hidden;}
.trip_content .service {margin: 10px 0;}
.trip_content .service img {float: left; margin-right: 5px;}
.trip_content .service p {float: left; display: block;display:inline-block; width: 86%;}
.trip_content .service li {margin: 5px 0; color: #1e88e5;}
.trip_content .service span,.trip_introduce p {font-size: 13px;}
.cp_link ul {width: 100%; height: 160px; overflow: hidden;margin-bottom:5px;overflow:hidden;}
.trip_content .cp_link a {display: inline-block; width: 48%;}
.trip_content .cp_link span {display: inline-block; width: 2%;}
.trip_content .cp_link p {width: 100%; text-align: center;font-size:12px;line-height:13px;}
/*须知*/
        .tab_notice {
            width:92%;
       margin-left:4%;
        font-size:13px;
        }
.notice_content p {font-size: 13px; }

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
.target_tit .sale span {font-size: 14px; }
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
     display:none;
        }
            .coupon_item_upbtn span {
                float: right;
                margin-right: 5px;
                   
            }

        .coupon_item_downbtn {
          
        float:right;margin-right:5px;margin-top:-16px;
     
        
        }

        .tog_open {
        display:none;
        }

        .linedetail_tab_content_item {
        display:none;
        
        }

         /*cal */
         .curdate {
            background:#ff6600;
         }


         /*点评*/
              .tab_comment {
            width:92%;
       margin-left:4%;
        font-size:13px;
        }

      .common_comment {
                        overflow:hidden;
                        }

                        .common_comment_content_list {

                         list-style:none;
                        margin-top:20px;
                        border-bottom:1px solid #ccc;
                        padding-bottom:10px;
                        }

                        .common_comment_content_item {
                        width:100%;
                        display:block;
                        display:inline-block;
                        width:100%;
                    
                        }


                        .comment_person ,.comment_star{
                            color:#ff6600;
                          float:left;
                       font-size:14px;
                        
                        }

                        .comment_star {
                          
                            margin-left:10px;
                        }
                        .comment_star img{
                            position: relative;
                            top:6px;
                        }

                        .comment_date {
                        float:right;
                        color:#ccc;
                       line-height:30px;
                        }

                        .common_comment_img_list {
                           overflow:hidden;
                            margin-top:10px;
                        
                        }

                            .common_comment_img_list li {
                            float:left;
                            
                           
                              display:block;
                              display:inline-block;
                              width:33%;
                            
                                
                            }


                            .common_comment_img_list img {
                           margin:0;
                             padding:0;
                             float:left;
                             width:90%;
                            margin-left:5%;
                             
                                 border-radius:5px;
                                 height:70px;
                            }

                                     .div_flappage {
                            height:30px;
                            line-height:30px;
                            overflow:hidden;
                            text-align:center;
                            width:60%;
                            margin:0 auto;
                            font-size:14px;
                            background:#ec407a;
                            color:white;
                            border-radius:5px;
                            margin-top:10px;
                            margin-bottom:5px;
                         }
    </style>
</head>
<body>
        <div class="linedetail linedetail_container"> 
        <div class="linedetail_top">
            <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                <span class="common_top_title"><span class="tttspans"></span>产品详情<span class="tttspan"></span></span>
                <a class="common_top_back" href="index.aspx" target="_self"><img src="Content/img/arrow_back.png" height="20" /></a>
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
              <div class="index_banner">
                
                    <div id="wrapper_banner">
                        <div id="scroller_banner">
                            <ul class="banner_list">
<%--                                <li ><a  href="http://www.baidu.com"><img src="Content/img/text_index_banner.png"  style="height:200px;"/></a></li>
                                <li><a  href="www.baidu.com"><img src="Content/img/text_index_banner.png" style="height:200px;" /></a></li>
                                <li><a  href="www.baidu.com"><img src="Content/img/text_index_banner.png" style="height:200px;" /></a></li>
                                <li><a  href="www.baidu.com"><img src="Content/img/text_index_banner.png" style="height:200px;" /></a></li>--%>
                            </ul>
                        </div>
                   </div>
                    <div class="banner_page">
                       
                      <ul class="banner_page_list">
                         <%-- <li class="banner_page_default banner_page_cur"></li>
                        <li  class="banner_page_default" ></li>--%>
                      </ul>
                           
                    </div>
                </div>
            <div class="linedetail_top_content">
              
                <p >[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚吉岛]自由行6天4晚[普吉岛]自由行6天4晚[普吉岛]自由行6天4晚由行6天4晚</p>
                <div >
                    <span class="linedetail_top_keyword">自由行<span style="margin-left:5px;margin-right:5px;">|</span>上海出发</span>
                    <span class="linedetail_top_money">$2199起</span>
                </div>
            </div>
        </div>
    

        <div class="common_nav">
            <div class="common_nav_bar">
                推荐理由
            </div>
            <div class="common_nav_content">
                <ul class="line_reason">
                    <li>1我是谁</li>
                    <li>2我是谁</li>
                    <li>3我是谁</li>
                </ul>

            </div>
        </div>


        <div class="common_nav" style="position:relative;">
            <div class="common_nav_bar">
                出行日期
            </div>
            <div class="common_nav_content">
                <a onclick="go_view(1)">
                <ul class="line_date">
                   
                </ul>
               <img style="position:absolute;top:40px;right:15px;" src="Content/img/arrow_right.png" />
                    </a>
            </div>
        </div>

        <div class="linedetail_content">
                <ul class="linedetail_tab">
                    <li class="linedetail_tab_item " tag="0" onclick="tab_choose(this)">详情</li>
                    <li class="linedetail_tab_item" tag="1" onclick="tab_choose(this)">行程</li>
                    <li class="linedetail_tab_item"  tag="2" onclick="tab_choose(this)">须知</li>
                    <li class="linedetail_tab_item" tag="3" onclick="tab_choose(this)">商圈</li>
                    <li class="linedetail_tab_item" tag="4" onclick="tab_choose(this)">点评</li>
                </ul>
                <div class="linedetail_tab_content">
                        <div class="tab_detail linedetail_tab_content_item">
                                <ul >
                                    <li class="common_tab_item">
                                        <div class="common_tab_titlebar">目的地橄榄>></div>
                                        <div class="linedetail_detail">
                                            这里是清新脱俗的泰北玫瑰，香气迷人，五彩缤纷。这里是那首《小城故事》中所唱的泰国古城，若是你能来，收获特别多。
                                            这里就是清迈，这座小城安静，淡雅，空气干净清新，呼吸就像在亲吻恋人的脸颊。这里没有消磨时间的温柔海滩，没有疯狂血拼的豪华商场，有的只是近乎于凝固的时间和让人心情无比舒爽的安静。你可以毫无目的的在古城中放空漫步，也可以穿越丛林跋山涉水，更可以在周末夜市疯狂血拼，喧闹和安详都自然地结合在这个城市。
                                        </div>
                                    </li>
                                    <li  class="common_tab_item">
                                        <div class="common_tab_titlebar">航班信息>></div>
                                        <div class="tab_detail_flight_list">
                                        <div class="tab_detail_flight">
                                            <div class="tab_detail_flight_left">去程</div>
                                            <div class="tab_detail_flight_right">
                                                <ul class="tab_detail_flightlist">
                                                    <li class="tab_detail_flightlist_item">
                                                        <p>
                                                           广州白云机场     广州白云机场    广州白云机场    广州白云机场    广州白云机场    广州白云机场    广州白云机场    广州白云机场
                                                        </p>
                                                      
                                                    </li>
                                                    <li class="tab_detail_flightlist_itemimg"><img src="Content/img/icon_arr-r.png"/></li>
                                                    <li class="tab_detail_flightlist_item">
                                                        <p>
                                                            广州广州广州广州广州广州广州广州广州广州广州广州广州广州广州广州广州
                                                        </p>
                                                      

                                                    </li>
                                                </ul>
                                                <div class="tab_detail_flight_content">中国南方航空公司/CZ3081/波音747</div>
                                            </div>
                                        </div>
                                        <div class="tab_detail_flight">
                                            <div class="tab_detail_flight_left">去程</div>
                                            <div class="tab_detail_flight_right">
                                                <ul class="tab_detail_flightlist">
                                                    <li class="tab_detail_flightlist_item">
                                                        <p>
                                                            广州白云机场 <span>08:15</span>
                                                        </p>
                                                       
                                                    </li>
                                                    <li class="tab_detail_flightlist_itemimg"><img src="Content/img/icon_arr-r.png" /></li>
                                                    <li class="tab_detail_flightlist_item">
                                                        <p>
                                                            广州<span>08:30</span>
                                                        </p>
                                                    

                                                    </li>
                                                </ul>
                                                <div class="tab_detail_flight_content">中国南方航空公司/CZ3081/波音747</div>
                                            </div>
                                        </div>
                                            </div>
                                    </li>
                                    <li class="common_tab_item">

                                        <div class="common_tab_titlebar">酒店信息>></div>

                                        <div class="tab_detail_hotel">
                                            <ul class="tab_detail_hotel_list">
                                             <%--   <li class="tab_detail_hotel_item">
                                                    <div class="hotel_maininfo"><span class="common_tab_titlebar2">1.普吉岛懂吧前夕盾甲村MILLFDSAFDAS REPORT PATONG </span>(高级房room jfdlksajflksdajflsdajf))</div>
                                                    <div class="hotel_info"><span>酒店地址:</span><p>199,rat-uthit 200pefdsafsdae road,kathu,phutl,831500,thailed,321312321</p></div>
                                                    <div class="hotel_info"><span>酒店电话:</span><p>+412321312312312321</p></div>
                                                    <div class="hotel_info"><span>酒店电话:</span><p>距离苏万达国际机场大约36分钟路程(56公里)</p></div>
                                                </li>--%>
                                            </ul>
                                        </div>
</li>
                                </ul>


                        </div>

                    <!--行程-->
                    <div class='tab_trip linedetail_tab_content_item'>
                        <div class='trip_content'>
                            <div class='data_info'>
                                <div class="trip_main_content">
                                  <%--  <div class="trip_main_item">
                                            <h2 class='common_tab_titlebar common_margin_t10'><span>D1</span>逛清迈夜市满载而归</h2>
                                            <div class='trip_pic'><img src='Content/img/1.png' /></div>
                                            <div class='service'>
                                                <ul>
                                                    <li class='clearfix'><img src='Content/img/t1.png' /><p>清迈谭易思廷酒店(Eastin Tan Hotel Chiang Mai)清迈谭易思廷酒店(Eastin Tan Hotel Chiang Mai)清迈谭易思廷酒店(Eastin Tan Hotel Chiang Mai)清迈谭易思廷酒店(Eastin Tan Hotel Chiang Mai)</p></li>
                                                    <li class='clearfix'><img src='Content/img/t2.png' /><p>机场至酒店接机服务</p></li>
                                                    <li class='clearfix'><img src='Content/img/t3.png' /><p>午餐:自理；晚餐:自理；</p></li>
                                                </ul>
                                                <span>抵达清迈机场，我们为您安排了贴心接机服务。入住后，您可以免费使用酒店提供的游泳池、健身房，也可以预约酒店内的按摩和Spa服务，放松自己。</span>
                                            </div>
                                        </div>--%>
                                    </div>
                                <h3 class='common_tab_titlebar common_margin_t10'>青青旅行为您推荐>></h3>
                                <div class='trip_introduce'>
                                    <h2 class='common_tab_titlebar2'>【周末夜市Night Bazaar】</h2>
                                    <p>清迈夜市已经成为当地名片，各种夜市能满足热爱深夜逛街的旅行者。太阳下山后，夜市就活了起来，一街的美食和原创商品加上街头的嘉年华气氛，绝对能让你在清迈厌倦睡觉。清迈大学北门外也有一个每天举行的夜市，货品价格更合理。深受当地学生的欢迎。清迈夜市已经成为当地名片，各种夜市能满足热爱深夜逛街的旅行者。太阳下山后，夜市就活了起来，一街的美食和原创商品加上街头的嘉年华气氛，绝对能让你在清迈厌倦睡觉。清迈大学北门外也有一个每天举行的夜市，货品价格更合理。深受当地学生的欢迎。</p>
                                </div>
                                <div class='trip_introduce'>
                                    <h2 class='common_tab_titlebar2 common_margin_t10'>【清迈古城Chiang Mai Old City】</h2>
                                    <p>没走几步路就会让金碧辉煌刺痛眼镜。有名气的庙宇总是挤满了游客和虔诚的当地人。无论室外有多喧闹和炎热，一进入清迈寺庙的殿堂内，溽热便消失了，佛祖安详慈悲的面容让你焦躁的心平静了下来。没走几步路就会让金碧辉煌刺痛眼镜。有名气的庙宇总是挤满了游客和虔诚的当地人。无论室外有多喧闹和炎热，一进入清迈寺庙的殿堂内，溽热便消失了，佛祖安详慈悲的面容让你焦躁的心平静了下来。</p>
                                    <div class='cp_link common_margin_t10'>
                                        <a href='javascript:void(0);'>
                                            <ul>
                                                <li><img src='Content/img/wifi.jpg' /></li>
                                            </ul>
                                            <p>泰国3G上网卡（非赠送）泰国3G上网卡（非赠送）</p>
                                            <p class='colorF60'>￥59/张</p>
                                        </a>
                                        <span></span>
                                        <a href='javascript:void(0);'>
                                            <ul>
                                                <li><img src='Content/img/wifi.jpg' /></li>
                                            </ul>
                                            <p>泰国3G上网卡（非赠送）泰国3G上网卡（非赠送）</p>
                                            <p class='colorF60'>￥59/张</p>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--须知-->
                    <div class='tab_notice linedetail_tab_content_item'>
                        <div class='notice_content'>
           <%--                 <div class='contain common_margin_t10'>
                                <h2 class='common_tab_titlebar'>费用包含>></h2>
                                <p>1、往返直飞普吉岛机票及税金（吉祥航空）；</p>
                                <p>2、CLUBMED度假村四晚住宿(含一日三餐)；</p>
                                <p>3、CLUBMED会员费、机场酒店间往返接送；</p>
                                <p>4、CLUBMED度假村内旅游意外险；</p>
                            </div>
                            <div class='uncontain common_margin_t10'>
                                <h2 class='common_tab_titlebar'>费用不含>></h2>
                                <p>1、泰国签证费；</p>
                                <p>2、航空公司机票及税金涨价因素；</p>
                                <p>3、因不可抗力原因所产生的额外费用；</p>
                                <p>4、旅游费用包含内容以外的其他费用；</p>
                            </div>
                            <div class='careful common_margin_t10'>
                                <h2 class='common_tab_titlebar'>注意事项>></h2>
                                <p>1、客人因个人原因于出发前取消预定行程，客人将承担代办证件、机票、酒店手续等实际发生的费用甚至全额，尽情谅解。</p>
                                <p>2、由于航空公司航班变更或其他不可抗力因素，造成出团日期或最终行程无法和原订行程一致，我社不承担违约责任。</p>
                                <p>3、如遇航空公司机票税金、燃油附加费上涨，我社将对价格做出相应的调整。</p>
                                <p>4、未满18岁报名者若无家长陪同前往，如要单独参加自由行需家长写委托声明。</p>
                            </div>--%>
                        </div>
                    </div>



                    <!--商圈-->
                    <div class='tab_shopping linedetail_tab_content_item'>
                        <div class='shopping_content'>
                            <div class='screen common_margin_t10'>
                                <ul  class="shopping_condition" >
                                    <li class='red'>全部</li>
                                    <li>京都</li>
                                    <li>东京</li>
                                    <li>大阪</li>
                                    <li>北海道</li>
                                </ul>
                            </div>

                            <div class="shopping_list">
                                <div class="shoppin_item">
                                    <div class='shopping_mall'>
                                        <img src='Content/img/3.png' />
                                    </div>
                                    <div class='target_tit'>
                                        <p class='grey'>日本 东京</p>
                                        <p>有乐町ITOCiA</p>
                                        <div class='sale'>优惠<span>5</span>%</div>
                                    </div>
                                    <div class='tog_close common_margin_t5 tog_open_tag'>
                                        <span class='fx'>返现</span>
                                        <span class='zk'>折扣</span>
                                        <span class='cx'>促销</span>
                                        <span class='zl'>赠礼</span>
                                        <p class='store' >
                                            <img src='Content/img/shop.png' >
                                            店家详情
                                            <div class="coupon_item_downbtn" onclick="open_shoppinginfo(this)" tag="0"><img src="Content/img/arrow_down.png" /></div>
                                        </p>

                                    </div>
                                    <div class='tog_open'>
                                        <dl  >
                                            <dd class='clearfix'><span class='fx'>返现</span><p>凭xx银行卡购物返现5%凭xx银行卡购物返现5%凭xx银行卡购物返现5%凭xx银行卡购物返现5%</p></dd>
                                            <dd class='clearfix'><span class='zk'>折扣</span><p>点击领取优惠券可享受5%折扣</p></dd>
                                            <dd class='clearfix'><span class='cx'>促销</span><p>商店促销活动</p></dd>
                                            <dd class='clearfix'><span class='zl'>赠礼</span><p>购满xxx元赠送精美礼品一份</p></dd>
                                        </dl>
                                        <div class='coupon'>

                                            <h2 class='common_tab_titlebar'>优惠券>></h2>
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
                                        <div class='xq'>
                                            <h2 class='common_tab_titlebar'>店家详情>></h2>
                                            <p><img src='Content/img/diamond.png'>有乐町ITOCiA是一座开业于2007年的复合商业设施，兼备购物、餐饮、办公等功能，吸引着20岁左右的年轻人。</p>
                                            <p><img src='Content/img/diamond.png'>有乐町ITOCiA位于有乐町站旁边，在有乐町站JR中央出口可直接到达，在有乐町D7b出口步行约1分钟、银座一点股站2出口步行约2分钟也可到达。</p>
                                            <p><img src='Content/img/diamond.png'>大厦共20层，其中1-8层为购物餐饮区，之上是办公区。商场经营的货物以男女时装、杂货为主，餐饮则包含了中日韩特色料理。</p>
                                        </div>
                                    </div>
                                    <div class="coupon_item_upbtn" onclick="close_shoppinginfo(this)" tag="0"><span><img src="Content/img/arrow_up.png" /></span></div>
                                </div>


                                <div class="shoppin_item">
                                    <a>
                                        <div class='shopping_mall'>
                                            <img src='Content/img/3.png' />
                                        </div>
                                        <div class='target_tit'>
                                            <p class='grey'>日本 东京</p>
                                            <p>有乐町ITOCiA</p>
                                            <div class='sale' >优惠<span>5</span>%</div>
                                        </div>
                                        <div class='tog_close common_margin_t5 tog_open_tag'>
                                            <span class='fx'>返现</span>
                                            <span class='zk'>折扣</span>
                                            <span class='cx'>促销</span>
                                            <span class='zl'>赠礼</span>
                                        </div>
                                     </a>
                                </div>

                            </div>

                        </div>
                  

                      
                    </div>
                        <div class='tab_comment linedetail_tab_content_item'>
                                  <ul class="comment_comment_list comment_has_list">
                     

                          
                        </ul>
                               <div class="div_flappage div_flappage_has" onclick="load_comment(false)">加载更多 </div>
                        </div>


                </div>

        </div>

      <%--  <div class="common_nav">
            <div class="common_nav_bar">
               签证信息
            </div>
            <div class="common_nav_content">
               


            </div>
        </div>--%>


  
              <div class="common_foot">
                <div class="common_foot_btn" style="background:#91cb14;">
                 <a href="tel:4006-777-666"><img src="Content/img/phone.png"/> <span >电话预定 </span></a> 
                </div>
                <div class="common_foot_btn" onclick="go_view(1);">
                    <img src="Content/img/order.png" />  <span >开始预定 </span>
                </div>
            </div>
    </div>

    <link href="Content/css/custom.css" rel="stylesheet" />

   <!--下单 start---------------------------------------------------------------------------------------------------------------------->
    <div id="LineDateJson" style="display:none;" ></div>
<input type="text" style="display:none;"  id="s_plandate"/>
<input type="text" style="display:none;"   id="s_planid"/>
 <input type="text" style="display:none;"   id="s_lineid"/>
   
     <!--日历-->




<div style="display:none;background:white;" id="PlanDate_view" class="linedetail_container">

      <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                <span class="common_top_title"><span class="tttspans"></span>选择日期<span class="tttspan"></span></span>
                <a class="common_top_back" onclick='go_view(0);' target="_self"><img src="Content/img/arrow_back.png" height="20" /></a>
                <a class="common_top_menu" onclick="$('.common_top_menucontent').toggle();"><img src="Content/img/btn_menu.png" height="25" /></a>
                <div class="common_top_menucontent">
                    <ul>
                              <li class="common_top_menucontent_item" onclick="go_page(this)" href="/WeChat/freetriporder.aspx#orderlist">我的订单</li>
                            <li  class="common_top_menucontent_item"  onclick="go_page(this)" href="/WeChat/freetrip/comment.aspx">我的点评</li>
                            <li class="common_top_menucontent_item noborder"  onclick="go_page(this)" href="/WeChat/freetriporder.aspx#coupon">我的优惠券</li>
                    </ul>

                </div>
</div>
    <div id="plandate" style="margin-top:40px;padding-bottom:50px;" class="plan_date"></div>
       <div class="common_foot">
                <div class="common_foot_btn" style="background:#91cb14;">
                 <a href="tel:4006-777-666"><img src="Content/img/phone.png"/> <span >电话预定 </span></a> 
                </div>
                <div class="common_foot_btn">
                  <a onclick="go_view(2)"> <img src="Content/img/next.png" />  <span >下一步 </span></a> 
                </div>
            </div>
</div>
       <!--日历-->

    <!--订单-->
    <div style="display:none;" class="linedetail_container order_person">
              <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                <span class="common_top_title"><span class="tttspans"></span>选择人数<span class="tttspan"></span></span>
                <a class="common_top_back" onclick='go_view(1);' target="_self"><img src="Content/img/arrow_back.png" height="20" /></a>
                <a class="common_top_menu" onclick="$('.common_top_menucontent').toggle();"><img src="Content/img/btn_menu.png" height="25" /></a>
                <div class="common_top_menucontent">
                    <ul>
                           <li class="common_top_menucontent_item" onclick="go_page(this)" href="/WeChat/freetriporder.aspx#orderlist">我的订单</li>
                            <li  class="common_top_menucontent_item"  onclick="go_page(this)" href="/WeChat/freetrip/comment.aspx">我的点评</li>
                            <li class="common_top_menucontent_item noborder"  onclick="go_page(this)" href="/WeChat/freetriporder.aspx#coupon">我的优惠券</li>
                    </ul>

                </div>
        </div>
                        <div class="order_person_content">
                             <div class="common_nav" style="position:relative;">
                        <div class="common_nav_bar">
                            预定人数
                        </div>
                        <div class="common_nav_content">
                                <div class="order_person_choose">
                                   <ul  class="order_person_choose_list">
                                       <li>
                                             <div class="choose_list_left"><img src="content/img/adult.png" />成人</div>
                                            <div  class="choose_list_right">
                                                <div class="choose_list_right_content">
                                                    <a class="choose_list_right_content_l"  onclick="reduce_pserson('adult_num',1);">-</a>
                                                      <input id="adult_num" type="text" value="1" name="adult_num"  maxlength="2"/>
                                                      <a class="choose_list_right_content_r" onclick="add_pserson('adult_num',99);">+</a>
                                                </div>
                                            </div>
                                       </li>
                                        <li>
                                             <div  class="choose_list_left"> <img src="content/img/child.png" />儿童</div> 
                                            <div  class="choose_list_right">
                                                  <div class="choose_list_right_content">
                                                    <a class="choose_list_right_content_l" onclick="reduce_pserson('child_num',0);">-</a>
                                                      <input id="child_num" type="text" value="0" name="child_num"  maxlength="2"/>
                                                      <a class="choose_list_right_content_r" onclick="add_pserson('child_num',99);">+</a>
                                                     </div>

                                            </div>
                                       </li>
                                   </ul>
                              

                                </div>

                        </div>
                    </div>
                    

                      <div class="common_nav" style="position:relative;">
                            <div class="common_nav_bar">
                                旅游出行安全告知书
                            </div>
                            <div class="common_nav_content">
                                   <div class="order_person_notice">
                                        <ul>
                                            <li>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;尊敬的嘉宾：您好！感谢您参加由上海中国青年旅行社组织的旅游活动,在旅游活动前,请您仔细阅读这份我们特别为您和同行人员提供的《旅游出行安全告知书》。在旅游行程中随团领队和导游员的友情提示及各地旅游设施的安全告知、各处接待人员的口头告知，在此不再做额外说明。为了您和同行人员旅游途中的安全，我们特别请您和您的同行人员在出行前阅读下列事项（如您认为合同所附的一份不够，可以向我们销售人员索取），这是我们应尽告知的责任，也是保障您的权益。希望您细心留意。衷心祝愿您：旅游愉快！

                                            </li>
                                             <li>
                                                 <div class="common_tab_titlebar">
                                                     一、乘机告知：
                                                 </div>
                                                      <ul>
                                                    <li>1.请注意航班号、起飞时间。于飞机起飞前二小时抵达机场的指定集合地点，以免拥挤及影响办理登机等相关手续。</li>
                                                    <li>2.国内旅游的，请带好有效的身份证件，出境的请带好有效的护照和机票。且不能随包托运。</li>
                                                    <li>3.大件行李和刀具、尖锐物品、液体类（包括化妆品）请办理托运。易碎物品请注意包装。手机和数码相机、摄像机等的电池只能随身携带，不能托运。</li>
                                                    <li>4.进入海关、安检后，请把握好时间，按登机牌上指定时间或机场广播通知前往登机口准时登机。</li>
                                                    <li>5.搭乘飞机时，请按机长和乘务员要求随时扣紧安全带，以免气流突变等因素影响安全。</li>
                                                    <li>6.飞行途中，请严格遵守机上的一切规章制度，听从机长和乘务员的指令。</li>
                                                    <li>7.飞机降落后，在飞机没有停稳之前，请勿提前解开安全扣和站立起来以及拿取行李。</li>
                                                    </ul>
                                               </li>
                                               <li>
                                                 <div class="common_tab_titlebar">
                                                   二、乘车告知：
                                                 </div>
                                                       <ul>
                                                    <li>1.在旅游车启动前，请将自己的小件行李放在行李架上，最好是不要离开自己的视线，放好放稳，避免落下，造成物品损坏或不必要的砸伤。大件行李请放在行李舱中或空闲的座位上。</li>
                                                    <li>2.乘坐旅游车时，车上座位有安全带的请系上安全带，防止遇上颠簸发生身体撞伤。</li>
                                                    <li>3.乘坐旅游车时，老人和未成年儿童要有成年人陪护，年龄比较小的最好是由成年人抱在怀中，在行车途中不要在车内走动、追逐嬉戏，以免在紧急制动和颠簸时发生危险。</li>
                                                    <li>4.车辆在颠簸路段行驶过程中请不要喝水或吃东西（主要是坚果类）。以免发生呛水或卡咽等情况发生危险。为保持车内卫生，旅游车上一般不允许吃水果及瓜子等带皮食品。</li>
                                                    <li>5.车辆在行驶中请勿任意更换坐位，勿将身体伸向车窗外。</li>
                                                    <li>6.有晕车、高血压、心脏病病症的旅游者，请提前服用有效的药物，在旅途中若是有不舒服的症状，请及时告诉司机或导游。</li>
                                                    <li>7.下车参观、游览时，请将现金、护照等其他贵重物品随身携带。不要将现金、机票、有价证券、贵重物品（数码相机/摄像机、钱包、手机等）放在行李里，更不要放在车上，以免丢失。</li>
                                                    <li>8.上下车时，请注意其他方向来车等路面不安全因素以以免发生危险。</li>
                                                    <li>9.请遵守当地的交通法规与行走习惯，过马路请走人行横道线，如景区不设人行道及横道线的，请注意往来车辆。</li>
                                                    </ul>
                                               </li>
                                                 <li>
                                                 <div class="common_tab_titlebar">
                                                 三、住宿告知：
                                                 </div>
                                                     <ul>
                                                    <li>1.入住宾馆时妥善保管好自己财物，贵重物品交前台保管或存于保险箱内，如随身携带，切勿离手，小心被窃。</li>
                                                    <li>2.当您进入客房后的时间段内，应作为您的自行安排活动时间段，您将享有充分的个人私密空间，在此时间段内请您注意自己的安全。</li>
                                                    <li>3.请您仔细阅读住宿须知，爱护宾馆内的各种物品，损坏需照价赔偿。请勿在房间内乱涂乱写乱倒垃圾。并请保管好房间钥匙，不要丢失，在房内请随时将房门扣上安全锁。</li>
                                                    <li>4.在客房洗澡时，注意区分冷热水龙头，避免烫伤，以及注意地面和浴盆内湿滑，避免滑到摔伤。</li>
                                                    <li>5.请勿在床上吸烟和饮酒，以及在灯具等照明设施上晾衣物。</li>
                                                    <li>6.在酒店中行走时应注意台阶高低和各类障碍物，以及注意走道中是否有水渍等，注意防滑，以免摔伤。</li>
                                                    <li>7.请仔细查看酒店的紧急出口及逃生标志，听到警报器响，请由紧急出口迅速离开。如遇火灾，请用湿被子护身，湿毛巾捂住口鼻逃生。利用床单、被子、毛巾等房内物品和设施，做好防烟防火措施、尽快疏散、积极自救。</li>
                                                    <li>8.若要使用健身房健身的，请量力而行，以免发生事故。</li>
                                                    <li>9.游泳池未开放时间，请勿擅自入池，并切记无救生员在场请勿入池。</li>
                                                    <li>10.退房前，仔细检查所携带的行李物品，特别注意证件和贵重财物（如：手机、相机、首饰、手表等物品）是否有遗忘。按规定时间前往酒店柜台办理退房手续（如有额外消费或需赔偿的请自行结清款项）</li>
                                                    </ul>
                                               </li>
                                                <li>
                                                 <div class="common_tab_titlebar">
                                               四、餐饮告知：
                                                 </div>
                                                    <ul>
                                                    <li>1.用餐前要先洗手，不食用不卫生、不合格的食品和饮料。</li>
                                                    <li>2.一般团队餐的餐标较低，都是以吃饱为原则，以卫生为根本。因每个人的宗教信仰、口味要求不同，若有特殊要求应提前向领队、导游或餐厅负责人说明。</li>
                                                    <li>3.旅游团队餐饮以食品卫生安全为首，旅游团队的用餐一般都是在旅游局指定的定点餐厅用餐，如果客人自己安排用餐一定检查卫生是否安全达标。切勿在外随意用餐，尤其是生冷海鲜等水产品、冰饮品。</li>
                                                    <li>4.食用海鲜应根据自身情况适量食用，切忌暴食暴饮。应注意：暴吃海鲜会中毒，忌海鲜加啤酒（加速尿酸形成）、海鲜加水果、海鲜配茶水（海鲜的钙与水果、茶中的鞣酸结合，易引起腹痛、恶心、呕吐）。</li>
                                                    <li>5.用餐时请遵守餐厅的规章制度，切勿损坏餐厅的物品，如有赔偿由游客自行承担。</li>
                                                    </ul>
                                               </li>
                                              <li>
                                                 <div class="common_tab_titlebar">
                                             五、旅游安全告知：
                                                 </div>
                                                    <ul>
                                                    <li>1.旅游中请注意人身、财产和卫生安全，时刻注意防治传染病、流行病等，不到流行病高发区进行旅游活动，不与高危人群接触。</li>
                                                    <li>2.请注意自觉遵守目的地国家（地区）的法律法规和宗教和风俗习惯,若违反当地国（地区）的法律，法规或破坏当地风俗会给您带来不必要的经济纠纷或法律责任。</li>
                                                    <li>3.遵守所有观光区、餐厅、饭店、游乐设施等各种场所的安全注意事项。</li>
                                                    <li>4.患有心脏病、高血压、哮喘、恐高症、残疾、精神病等疾病的游客，以及孕妇、酒后、不能自理、自控的游客，应在旅游过程中特别注意安全，做到只参加自己身体能承受的项目和游览点。</li>
                                                    <li>5.景点游览，特别是登山旅游，出发前应检查好携带的各种装备，根据自身的身体情况，做好旅游安排，谨遵“走路看脚下、看景不走路、摄影照相要站稳、所用器械要带牢”的安全教诲。行走雪地或陡峭道路时，请小心谨慎。</li>
                                                    <li>6.玩海，看大海时，请在景区允许并且导游指定的安全区域内活动。如果您想参与海边或海上娱乐活动，请您让领队或导游陪同前往，在尽情玩耍嬉水和垂钓时，请您多注意身边的海浪及脚下的青苔以防不测，同时请照顾好身边未成年人。有的海滩潮水湍急，海况、海边的地理环境都比较复杂，请您让水性比较好的人陪同前往，同时也请您穿好救生衣或带好救生圈。如果您想在海边拍照留影，特别要注意身后海浪的侵袭。但是遇到有风浪的天气，请您不要参与海边或海上活动。海边戏水请勿超越安全警戒线。</li>
                                                    <li>7.乘船，出海乘坐休闲渔船观光、垂钓，在船上，请您遵守乘船规定，服从工作人员管理，照顾好身边小孩。如果您会晕船，您可以在开船前半小时服用晕船药。船靠岸时不要拥挤，在工作人员的帮助下上下船，游客上船后一定要穿好救生衣并坐稳。搭乘快艇请扶紧把手或坐稳，勿随意移动。</li>
                                                    <li>8.在旅游中带孩子的家长要关照孩子，不要在严禁嬉戏的公共场所内随意嬉戏，叫喊，乱跑。儿童及老人须有照应能力的人陪同。</li>
                                                    <li>9.搭乘缆车时，请依序上下，听从工作人员指挥，切勿超载时强行搭乘。</li>
                                                    <li>10.环保，如果您吸烟或在野外进行野餐、烧烤等活动，离开时请您务必把火种熄灭，以免发生火灾。如果您在游玩时找不到垃圾筒，返回时请您把它带回，并放在沿途设立的垃圾筒内。为保护生态环境请不要进入围栏践踏花草、采摘等。</li>
                                                    <li>11.请保管好个人的钱包、证件以及贵重物品。数额较大的现金最好分开存放，口袋里预留小额现金，做到钱财不露眼，人多时背包、拎包放前面，以防被盗。</li>
                                                    <li>12.若有突发情况需求助时，请及时联系领队、导游或拨打当地的应急报警电话。</li>
                                                    </ul>
                                               </li>
                                              <li>
                                                 <div class="common_tab_titlebar">
                                                  六、自由活动
                                                 </div>
                                                     <ul>
                                                    <li>1.您参加自费活动和购物活动的时间段内，各位游客都按自己的喜好分别活动，都享有自己的充分自由支配空间，故都应作为自由活动期间。故在此时间内您应当注意自己的人身、财产安全。</li>
                                                    <li>2.在自由活动期间，请大家注意安全，并根据自己的身体条件应当选择自己能够控制风险的活动项目，并在自己能够控制风险的范围内活动。旅行社不建议您参加“潜水、游泳、高速摩托艇、降落伞、高空弹跳、攀崖、漂流、骑马、骑大象、骑骆驼、直升机、大峡谷小飞机、吉普车越野驾驶等高危娱乐活动。</li>
                                                    <li>3.如您一定要参加高危娱乐项目的，当您参加浮潜时，请务必穿着救生衣，接受浮潜教练、救生员、领队、导游的讲解，并于岸边练习使用呼吸面具方得下水，并不可超越安全区域活动。如参加自费骑马、骑骆驼等活动时，请切记务须在服务人员之伴随或伴骑之下方得进行，万勿自行脱队或策马狂奔，以维自身安全。如您参加骑马、漂流、探险、自驾车等其他高危、高刺激娱乐项目需在活动前了解具体的活动常识，请您挑选适合自己的各种项目，活动前检查好各种器械，注意安全，服从指导人员的指挥，永远记住“安全第一” 旅游人身意外伤害险不含这些项目的赔偿，需单独增加旅游娱乐保险费。对参加此类高危娱乐活动的，发生危险时，旅行社不承担任何责任。</li>
                                                    <li>4.参加娱乐活动要服从领队、导游统一安排，要选择当地旅游部门核准的娱乐场地及娱乐项目。团体活动时应紧跟团队，不参加集体组织娱乐活动的人员，请先行确认您对环境熟悉，确认您能自行确保自身安全的情况下进行自由活动，以免发生意外，并请告知领队、导游。</li>
                                                    <li>5.请勿参加黄、毒、赌此类的娱乐活动。</li>
                                                    <li>6.在自由活动期间，您最好至少保持两、三人一起活动，最好与熟悉的人结伴。切忌单独外出。最好与熟悉的人结伴，既可增添旅游的乐趣，又能互相照顾。</li>
                                                    <li>7.夜间应避免单独外出。夜间或自由活动时间内需自行外出的，请告知领队、导游，并应特别注意安全。</li>
                                                    </ul>
                                               </li>
                                              <li>
                                                 <div class="common_tab_titlebar">
                                                  七、购物
                                                 </div>
                                                    <ul>
                                                    <li>1.购物时需在看到自己需要的物品和相应的发票及质保书等票据后再行付款。</li>
                                                    <li>2.旅游中的购物，应以“量财而出、喜欢才买。不买就不摸，不讨价还价，不品头论足”为原则，以防恶商欺负。理智消费、切忌摆阔；购物中应时刻注意随身携带的物品，谨防偷窃、购物后检查随身携带的物品，勿遗忘丢失。</li>
                                                    <li>3.购买贵重物品和首饰、食用类保健品、药材时，应多听少动，做到要能辨别真伪、注意计量器具是否正确、并了解市场价格后理智消费。在旅行社的定点商店购买物品时，请索取发票以及珠宝首饰的质保书（成份说明书），还应注意商家恶意调包的恶劣手法。</li>
                                                    <li>4.切记在公共场合财不露白，购物时也勿当众取出整叠钞票、也勿当众数钞票。</li>
                                                    </ul>
                                               </li>
                                              <li>
                                                 <div class="common_tab_titlebar">
                                                  八、天气变化等安全告知：
                                                 </div>
                                                    <ul>
                                                    <li>1.如去草原、湿地、山地或高原等地区旅游气候昼夜温差大，请多带衣物，骑马、登山请穿旅游鞋或软底鞋，穿长裤戴手套等。</li>
                                                    <li>2.遇有紧急情况（地震、火情、飓风等），不要慌张，镇定地判断情况。注意当地的天气预警报告，提高自我保护意识，主动地实行自救，服从公共执政部门的指挥，即使在最危险的时刻，也要保持镇静，等候救援人员的救助。</li>
                                                    <li>3.如遇海拔高氟点低请大家根据自己的身体最好饮用自备矿泉水以防肠胃不适。因昼夜温差较大洗浴时要根据自己体能酌情而已以防感冒。旅游出发前应事先了解当地的气候、气温，携带相应的用品（如雨伞、遮阳帽、墨镜等）和衣物及自需药品。</li>
                                                    <li>4.注意高原雷雨天气的雷电的科学躲避，不要到树下、民房下避雨。不要走动，应原地蹲下，关闭带电的器具及金属杆雨伞。</li>
                                                    <li>5.出发前请注意查询旅游目的地的气候情况，注意带好相应的衣物和防护用品。如无法查询到的，可在出发前向领队等相关人员咨询。</li>
                                                    </ul>
                                               </li>
                                            <li>
                                                <div class="common_tab_titlebar2">
                                                 希望全体旅游嘉宾积极配合领队和导游的工作，互相关爱，互相帮助，团结协作，共同完成这次愉快的旅行。如果您结束旅游时，脸上依然还带着阳光般的笑容，那就是您对我们最高的奖赏。
                                            </div>
                                            </li>
                                        </ul>

                                   </div>
                            </div>
                        </div>
                </div>
            <div class="common_foot">
                <div class="common_foot_btn" style="background:#91cb14;">
                 <a href="tel:4006-777-666"><img src="Content/img/phone.png"/> <span >电话预定 </span></a> 
                </div>
                <div class="common_foot_btn">
                  <a onclick="go_order()"> <img src="Content/img/next.png" />  <span >下一步 </span></a> 
                </div>
            </div>
    </div>


  <!--订单-->





     <!--下单 end---------------------------------------------------------------------------------------------------------------------->




</body>
</html>
<script src="/WeChat/freetrip/Scripts/jquery-1.11.3.min.js"></script>
<script src="/WeChat/freetrip/Scripts/iscroll-master/build/iscroll.js"></script>
<script src="/assets/plugins/jquery.cookie.min.js"></script>
<script src="/app_js/datePicker.js"></script>
<script src="/assets/plugins/jquery.blockui.min.js"></script>
<script src="/WeChat/freetrip/Scripts/common.js"></script>
<script>
    var data_main=eval(<%=lineInfoDetail%>);
    var data_banner = data_main.Pics;
    var banner_scroll;

    $("#s_lineid").val(data_main.MisLineId);
  
    var width = 0;


    function init() {

        //获取总宽度
        width = $("body").width();
     

        init_banner();
        set_travel();
        init_calender();
        load_comment(true);
        $(".linedetail_tab_item").eq(0).trigger("click");
    }
    //TAB
    function tab_choose(obj) {
        var index = $(obj).attr("tag");
        $(obj).addClass("linedetail_tab_item_hover").siblings().removeClass("linedetail_tab_item_hover");
        $(".linedetail_tab_content_item").eq(index).show().siblings().hide();
     
    }

    //toggle
    function open_shoppinginfo(obj) {
        var index = $(obj).attr("tag");
        $(obj).hide();
        $(".tog_open_tag").eq(index).hide();
        $(".store").eq(index).hide();
        $(".tog_open").eq(index).slideDown();
      $(".coupon_item_upbtn").eq(index).show();
    }

    function close_shoppinginfo(obj) {
        var index = $(obj).hide().attr("tag");
       $(".tog_open").eq(index).hide();
        $(".coupon_item_downbtn").show();
        $(".store").eq(index).show();
        $(".coupon_item_upbtn").eq(index).hide();
        $(".tog_open_tag").eq(index).show();
    }


    //banner


    function init_banner() {
        $("#scroller_banner").width(width * data_banner.length);
        //生成HTML
        set_banner(data_banner);
        banner_scroll = new IScroll('#wrapper_banner', {
            scrollX: true,
            scrollY: false,
            momentum: false,
            snap: true,
            snapSpeed: 300,
            keyBindings: false,
            click: true
            //,
            //indicators: {

            //    el: document.getElementById('indicator_banner'),
            //    resize: true
            // }

        });

        $(".banner_page_list").children().eq(0).addClass("banner_page_cur");
        banner_scroll.on('scrollEnd', function () { $(".banner_page_list").children().eq(banner_scroll.currentPage.pageX).addClass("banner_page_cur").siblings().removeClass("banner_page_cur"); });
    }



    function set_banner(data) {

        var result = "";
        var indexResult = "";
        $(data).each(function (index, obj) {

            result += set_banner_item(obj);

            indexResult += " <li  class='banner_page_default' ></li>";
        });

        $(".banner_list").html(result);
        $(".banner_page_list").html(indexResult);

        $(".banner_list img").width(width);
    }

    function set_banner_item(obj) {
     
        return " <li ><a  ><img  src='" + obj + "'  /></a></li>"; 
    }

    //window change
    $(window).on('orientationchange', function () {

        setTimeout(function () {

            width = $("body").width();
            $("#scroller_banner img").width(width);
            $("#scroller_banner").width(width * data_banner.length);
            banner_scroll.refresh();

        }, 100);



    });


    //产品信息
    function set_travel() {
        //顶部信息
        $(".linedetail_top_content p").html(data_main.LineName);
        $(".linedetail_top_keyword").html(data_main.LineType + "<span style='margin-left:5px;margin-right:5px;'>|</span>上海出发");
        $(".linedetail_top_money").html("￥" + data_main.Price);
        //推荐理由
        $(".line_reason").html( data_main.Feature );
        //出行日期
        var date_arr = data_main.Pdates.split(",");
     for (i = 0; i < date_arr.length; i++)
     {
         $(".line_date").append("<li>"+date_arr[i]+"</li>");
     }
        //签证信息

        //tab
     set_tab_detail()
     set_tab_trip();
     set_tab_notice();
     set_tab_shopping_condition();
     set_tab_shopping(data_main.tradingArea);
    }




    function set_tab_detail() {
       
        //目的地概览
        $(".linedetail_detail").html(data_main.DestinationInfo);
        //航班信息
       
        $(".tab_detail_flight_list").html("");
        $(data_main.TrafficInfo).each(function (index,obj) {
            $(".tab_detail_flight_list").append(set_flight_item(index, obj));
        });

        ////酒店信息
    
        for (i = 0; i < data_main.hotelInfo.length; i++) {
         
            $(".tab_detail_hotel_list").append(set_tab_hotel_item(i, data_main.hotelInfo[i]));
        }
        //$(".tab_detail_hotel_list").html(data_main.hotelInfo);
    }



    function set_flight_item(index,obj) {
        var result="<div class='tab_detail_flight'><div class='tab_detail_flight_left'>"+obj.name+"</div>";
        result+="<div class='tab_detail_flight_right'><ul class='tab_detail_flightlist'>";
        result += "<li class='tab_detail_flightlist_item'><p>" + obj.from + "</p></li>";
        result+="<li class='tab_detail_flightlist_itemimg'><img src='Content/img/icon_arr-r.png'/></li>";
        result += "<li class='tab_detail_flightlist_item'> <p>" + obj.to + "</p>";
        result += "</li></ul><div class='tab_detail_flight_content'>" + obj.context + "</div></div></div>";
        return result;

    }



    function set_tab_hotel_item(index, obj) {
 
        var hotelInfo_arr = obj.split(";;");
        var result = "<li class='tab_detail_hotel_item'><div class='hotel_maininfo'><span class='common_tab_titlebar2'>" +(index+1)+"."+ hotelInfo_arr[0]+ " </span></div>";
        result += "<div class='hotel_info'><span>" + hotelInfo_arr[1] + "</span></div>";
        result += "<div class='hotel_info'><span>" + hotelInfo_arr[2] + "</span></div>";
          
            return result;
    }


    function set_tab_trip() {
        //行程
        $(data_main.routeInfoList).each(function (index,obj) {
            $(".trip_main_content").append(set_tab_trip_item(index,obj));
        });

        //攻略

        //广告
    }




    function set_tab_trip_item(index, obj) {
     
        var imgstr = "";
   
        if (obj.pic != null && obj.pic != "") {
            var imgstr = "<div class='trip_pic'><img style='height:100px;width:100%;' src='" + obj.pic + "' /></div>";
        } 
      
        var result="";
        result += "<div class='trip_main_item'><h2 class='common_tab_titlebar common_margin_t10'><span>D" + obj.daterank + "</span>"+obj.rname+"</h2>";
        result +=  imgstr ;
        result+= "<div class='service'><ul>";
        result += "<li class='clearfix'><img src='Content/img/t1.png' /><p>" + obj.room + "</p></li>";
        result += "<li class='clearfix'><img src='Content/img/t2.png' /><p>" + obj.bus + "</p></li><li class='clearfix'><img src='Content/img/t3.png' /><p>" + obj.dinner + "</p></li></ul>";
        result += "<span>" + obj.route + "</span></div>     </div>";
                                                
        return result;
    }

    function set_tab_notice() {
        //费用包含
        $(".notice_content").append(set_tab_notice_item(data_main.PriceIn, "费用包含"));
        //费用不包含
        $(".notice_content").append(set_tab_notice_item(data_main.PriceOut, "费用不包含"));
        //注意事项
        $(".notice_content").append(set_tab_notice_item(data_main.Attentions, "注意事项"));
    }

    function set_tab_notice_item(obj,title) {
        return "<div class='contain common_margin_t10'>   <h2 class='common_tab_titlebar'>" + title + ">></h2>"+obj+"</div>";
                           
    }



    function set_tab_shopping_condition() {
      
        var d_arr = data_main.Destinationid.split(",");
        var d_arr2 = data_main.Destination.split(",");
     
        $(".shopping_condition").html("");
        var result = "";
        result += "<li  class='red'  onclick='choose_shoppingbydes(this)' tag=''>全部</li>";
        for (var i = 0; i < d_arr.length; i++)
        {
            result += "<li onclick='choose_shoppingbydes(this)' tag=" + d_arr[i] + ">" + d_arr2[i] + "</li>";
           
      
        
          
        }
        $(".shopping_condition").html(result);
    }

  
    function choose_shoppingbydes(obj) {
      
        $(obj).addClass("red").siblings().removeClass("red");
        var id = $(obj).attr("tag");
        //筛选数据
        var data = [];
        if (id != "") {
            $(data_main.tradingArea).each(function (index, obj) {
                if (obj.destid == id) {
                    data.push(obj);
                }
            });
        } else {
            data = data_main.tradingArea;
        }
        //
        set_tab_shopping(data);
    }

    function set_tab_shopping(data) {
        $(".shopping_list").html("");
        if (data!=null){
        $(data).each(function (index, obj) {
            $(".shopping_list").append( set_tab_shopping_item(index, obj))
            
        });
        }
    }

    function set_tab_shopping_item(index, data) {

        var result = "";
        result += "<div class='shoppin_item'><a  href='shopdetail.aspx?id=" + data.id + "' target='_self'><div class='shopping_mall'>";
        result += "<img src='" + data.pic + "' /></div><div class='target_tit'><p class='grey'>" + data.name + "</p><p></p><div class='sale' ><span>"+data.flag+"</span></div>";
        result += "</div><div class='tog_close common_margin_t5 tog_open_tag'>";

        if (data_main.activity != null) {
            //分类添加
            $(data_main.activity).each(function (index2, obj2) {
                if (obj2.tradingAreaId == data.id) {
                    result += "<span class='fx' style='background:" + obj2.color + ";'>"+obj2.name+"</span>";
                }
            });
        }
      
      //  result += "<span class='fx'>返现</span><span class='cx'>促销</span><span class='zl'>赠礼</span>";
        result += "</div></a></div>";
     
        return result;

      
    }


 

   ////////////////////////// 下单开始    ///////////////////////////////////////////

    function go_view(index) {
        scrollTo(0, 0);
        if (index == 1) {
            $("#plandate .hasEvent").removeClass("curdate");
            $("#s_plandate").val("");
            $("#s_planid").val();
        }
       else if (index == 2) {
            if ($("#s_plandate").val() == "") {
                show_message("请选择出发日期");
                return false;
            }
       } else if (index == 3) {
           $("#child_num").val(0);
           $("#adult_num").val(1);
       }
        $(".linedetail_container").hide().eq(index).show();


    

    }
  //日历 start
    var calendarNums = 5;

 
    function init_calender() {
        var lineid = $("#s_lineid").val();
        var url = "/WeChat/AjaxService.aspx?action=LoadLineDateJson&lineid=" + lineid;
        calendarNums = 0;
   
        $.get(url, function (data) {
         
            $("#LineDateJson").html(data);
    
          
          
           
            if (calendarNums > 0) {
                $("#plandate").showRenderCalendar({ events: eval(json), showNum: calendarNums });

      
                $("#plandate .hasEvent").bind("click", function () {
                  
                    $("#plandate .hasEvent").removeClass("curdate");
                    $(this).addClass("curdate");
                    $("#s_plandate").val($(this).attr("date"));
                    $("#s_planid").val($(this).attr("tag"));
                });
            }
        
        });
    }

    //日历 end


    //选人  start
    function add_pserson(target,max) {
        var now_num =parseInt( $("#" + target).val());
        if (now_num < max) {
            var new_num = (now_num + 1);
            $("#" + target).val(new_num);
        }
    }
    function reduce_pserson(target,min) {
        var now_num = parseInt( $("#" + target).val());
        if (now_num >min) {
            var new_num = (now_num -1);
            $("#" + target).val(new_num);
        }


    }

    function go_order() {
        if ($("#s_plandate").val() == "") {
            go_view(1);
            return false;
        }

        show_loading();

        var url = "../../WeChat/AjaxService.aspx?action=TempOrder&lineid=" + $('#s_lineid').val() + "&planid=" + $('#s_planid').val() + "&begindate=" + $('#s_plandate').val() + "&adults=" + Number($('#adult_num').val()) + "&childs=" + Number($('#child_num').val()) + "&r=" + Math.random();
        $.getJSON(url, function (date) {
         
            if (date.success == 0) {
                $.cookie("orderuid", date.orderuid, { expires: 30, path: '/WeChat' });
                $.cookie("lineid", $('#s_lineid').val(), { expires: 7, path: '/WeChat' });
                $.cookie("planid", $('#s_planid').val(), { expires: 7, path: '/WeChat' });
                $.cookie("plandate", $('#s_plandate').val(), { expires: 7, path: '/WeChat' });
                //top.location = "/WeChat/order.aspx#first";
                window.location.href = "/WeChat/freetripOrder.aspx#first";
            }
            else {
                $.unblockUI();
                show_message(date.error);
                return false;
            }
        })


    }



    //选人 end
  
    function show_loading() {
        $.blockUI({
            message: "Processing...",
            css: {
                border: 'none',
                padding: '15px',
                'line-height': '50px',
                backgroundColor: '#000',
                'border-radius': '5px',
                '-webkit-border-radius': '5px',
                '-moz-border-radius': '5px',
                opacity: .7,
                color: '#fff'
            }

        });

    }


    function show_message(msg) {
       
        $.blockUI({
            message: msg,
           
            css: {
                border: 'none',
                padding: '15px',
                'line-height':'50px',
                backgroundColor: '#000',
                'border-radius':'5px',
                '-webkit-border-radius': '5px',
                '-moz-border-radius': '5px',
                opacity: .7,
                color: '#fff'
            }
          
        });
        window.setTimeout(function () {
            $.unblockUI();
        }, 2000);
    }



    //点评
 
    //wait
    var has_data = [];
    var has_pageindex = 1;
    var has_pagesize = 3;
    var has_currpage = 0;
    var has_totalpage = 0;



    function load_comment(isclear) {

        var url = "/WeChat/AjaxService.aspx?action=queryCommentList";
        url += "&pagesize=" + has_pagesize;
        url += "&currpage=" + has_pageindex;
        url += "&commentStatus=COMMENTED";
        url += "&auditStatus=AUDITED";
        url += "&lineId=" + data_main.MisLineId;
        $.ajax({
            url: url,
            cache: false,
            type: "get",
            //  data: JSON.stringify({ pageIndex: pageIndex, pagesize: pageSize }),
            //   contentType: 'application/json; charset=utf-8',  //must
            dataType: "JSON",
            success: function (data) {
                if (data.commentList != null) {
                    if (has_pageindex < data.pageCount) {
                        has_pageindex = (has_pageindex + 1);
                    } else {
                        $(".div_flappage_has").hide();
                    }
                    set_has(data.commentList, isclear);


                }
            },
            beforeSend: function () {
                // 加载loading

            }
        });

    }

    function set_has(data, isclear) {
        if (isclear) {
            $(".comment_has_list").html("");
        }
        $(data).each(function (index, obj) {
            $(".comment_has_list").append(set_has_item(index, obj));
        });

    }

    function set_has_item(index, data) {
        var result = "";
        //result += "<li><div class='comment_order_num'>订单号:" + data.orderId + "</div>";
        //result += "<div class='comment_line_banner'><a><img src='" + data.linePic + "' /></a></div>";
        //result += "<div class='comment_line_info'>";
        //result += "<div class='comment_line_name'>" + data.lineName + "</div>";
        //result += "<div class='comment_line_startdate'>出发日期：<span>" + formatDate(data.beginDate) + "</span></div>";
        //result += "<div class='comment_line_money'>总金额：<span>￥" + data.price + "</span></div>";
        result += set_has_item_comment(data);
        result += "</div><div class='comment_border'></div></li>";
        return result;
    }

    function set_has_item_comment(data) {
        var result = "";
        result += "<div class='comment_comment_info'><ul class='common_comment_content_list'> <li class='common_comment_content_item'>";
        result += "<div class='comment_person'><img src='Content/img/comment/comment_person.png' /><span>" + data.userName + "</span></div>";
        result += "<div class='comment_star'>" + set_has_rank(data.rank) + "</div>";
        result += "<div class='comment_date'>" + formatDate(data.commentTime) + "</div></li>";
        result += "<li class='common_comment_item'><p>" + data.context + "</p> </li>";
        result += "<li class='common_comment_item'><ul class='common_comment_img_list'>" + set_has_img(data.pic) + "</ul>";
        result += "</li></ul></div>";
        return result;
    }

    function set_has_rank(rank) {
        var result = "";

        for (var i = 1; i <= 5; i++) {
            var name = "star_empty.png";
            if (rank > i) {
                name = "star_full.png";
            }
            result += "<img width='15' src='Content/img/comment/" + name + "' />";
        }
        return result;

    }


    function set_has_img(pic) {
        var result = "";
        if (pic == "") {
            return result;
        }
        var picarr = pic.split(",");

        for (var i = 0; i < picarr.length; i++) {


            result += "<li class='common_comment_img_item'><img src='" + picarr[i] + "' /></li>";
        }

        return result;
    }


    init();

</script>
