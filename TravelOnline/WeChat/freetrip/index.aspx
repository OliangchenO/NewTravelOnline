<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TravelOnline.WeChat.freetrip.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no"/>
  
    <title></title>
      <link href="/WeChat/freetrip/Content/css/common.css" rel="stylesheet" />
        <style>

       * {
            margin: 0;
            padding: 0;
            text-decoration:none;
        }
        a {
        color:#333;
        }

        ul {
        
       list-style:none;
        }
        body {
        
        background:#e5e5e5;
        font-size:12px;
        color:#333;
     overflow-y:auto;
        }


     

        .hidden {
        display:none;
        }
   

        .noborder {
        border:0;
        }

        .index_top {
        
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
    

                /*banner*/

 /*middle*/

    .condtion_overscroll{
        position:fixed;
      
        
        }
        .index_middle {
      width:100%;
  
        }
        .index_condition_bar {
            height:35px;
           background:#fff;
           width:100%;
           margin-top:10px;
           line-height:35px;
        
        }
            .index_modeArrive{
                float:right;
                margin-right:15px;
                font-size:12px;
            }

                .index_modeArrive span {
                margin-right:10px;
                }

        .index_condition_content {
           
        display:none;
        width:100%;
        
        }

        .index_condition_list {
            overflow:hidden;
            background:white;
            border:1px solid #dddddd;
            padding-top:10px;
            padding-bottom:10px;
        
        
        }
        .index_condition_list li{
            float:left;
          margin-left:25px;
         
        
         line-height:30px;
         text-align:center;
       opacity:0.9;
        }

        /*content*/
        .index_content {
        width:100%;
      
        }


        .index_linelist {
        overflow:hidden;
        width:100%;
        list-style:none;
        background:#f5f5f5;
        
        }
        .index_lineitem {
          width:100%;
        float:left;
        background:white;
        padding-bottom:10px;
        line-height:20px;
       margin-bottom:10px;
        border-top:1px solid #eee;
       border-bottom:1px solid #eee;
        }

        .index_lineitem_content {
        width:94%;
        margin:0 auto;
          overflow : hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
        }
        .index_lineitem_detail {
        font-size:14px;
        }

            .index_lineitem_detail p {
               overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
            
            }
      
          .index_lineitem_detail img{
            margin-right:5px;
            float:left;
            width:15px;
            margin-top:2px;
          
            }

         

        .index_lineitem_title {
            line-height:30px;
            font-size:14px;
            position:relative;
        }
         .index_lineitem_title  p{
           overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    }
        .index_money {
        
        float:right;
        color:#ff6600;
        /*position:absolute;
        top:0px;*/
        right:15px;
        }

            .index_money span {
          
            font-size:18px;
            }

        .index_lineitem_item {
         font-size:14px;
        }

        .index_lineitem_down {
        
        float:right;
        margin-right:5px;
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


            .condition_cur {
                color:#ec407a;
                font-weight:bold;
            }


            .index_shopbtn {
            display:block;display:inline-block;width:100px;height:30px;background:white;color:#ec407a;line-height:30px;
                border-radius:5px;
             border:1px solid #ec407a;
                
            }

            .index_shopbtn_cur {
            
          background:#ec407a;color:white;
            }
         

            .index_shopbtn img {
                position:relative;
                top:6px;
                left:8px;
              margin-right:10px;
              width:20px;
            }


            .index_shopcontent {
                background:#ec407a;
                opacity:0.9;
                width: 100%;
                display:none;
               
                top: 0;
                position: absolute;
                padding-top:20px;
                padding-bottom:150px;
            }

                .index_shopcontent ul {
                    list-style:none;
                    overflow:hidden;
                }

                .index_shopcontent li {
                    float:left;
                    font-size:14px;
                    margin-left:5px;
                    margin-top:10px;
                    margin-top:10px;
                }

                .index_shopcontent a {
                     padding-left:5px;
                     padding-right:5px;
                     color:white;
                }
    </style>
</head>
<body>
       <div class="index">
            <div class="index_top">
                <div class="common_top" >
                    <span class="common_top_title"><span class="tttspans" style="font-size:9px;"></span>青青自由行<span class="tttspan"></span></span>
             <%--     <a class="common_top_back"><img src="Content/img/arrow_back.png" height="20" /></a>--%>
                    <a class="common_top_menu" onclick="$('.common_top_menucontent').toggle();"><img src="Content/img/btn_menu.png" height="25"  /></a>
                    <div class="common_top_menucontent">
                        <div style="position:absolute;top:-23px;right:15px;"> <img src="content/img/arrow.png" /></div>
                        <ul >
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

            </div>

            <div class="index_middle" id="index_middle">

                <div class="index_condition_bar"  onclick="$('.index_condition_content').toggle();">
                    <a class="index_modeArrive" ><span>目的地</span><img  src="Content/img/arrow_down.png" style="width:15px;"/></a>
             
                </div>
                <div class="index_condition_content">

                    <ul class="index_condition_list">
                         <li tag="" class="condition_cur" onclick="load_line(this)">全部</li>
                        <li tag="日本" onclick="load_line(this)">日本</li>
                    <li tag="横滨" onclick="load_line(this)">横滨</li>
                           <li tag="加拿大" onclick="load_line(this)">加拿大</li>
                           <li tag="杭州" onclick="load_line(this)">杭州</li>
                    </ul>
                </div>

            </div>
        <div class="index_content" >
         
            <ul class="index_linelist">
               <%-- <li class="index_lineitem">
                   
                        <div>
                            <img src="~/Content/img/text_index_banner.png" style="width:100%;height:140px;" />
                        </div>
                        <div class="index_lineitem_content">
                            <div class="index_lineitem_title">
                                <span>[普吉岛]自由行6天四晚</span><span class="index_money"> <span>$2999</span>起</span>
                            </div>
                          
                              
                                <div class="index_lineitem_detail">
                                    <p onclick="detail_open(0)" ><img src="~/Content/img/diamond.png" style="width:15px;" />青岛、威海、日照、蓬莱、烟台、连云港等地全线游览...<span class="index_lineitem_down" ><img src="~/Content/img/arrow_down.png" /></span></p>
                                    <p class="hidden"><img src="~/Content/img/diamond.png" style="width:15px;" />青岛、威海、日照、蓬莱、烟台、连云港等地全线游览，价格优惠！</p>
                                    <p class="hidden"><img src="~/Content/img/diamond.png" style="width:15px;" />展开一幅独特的海上画卷，走进红瓦绿树，感受帆船之都—青岛；感受气候宜人的海滨城市—威海；八仙渡海在此地，飘渺仙境入梦来的蓬莱仙境；行程丰富多彩，不容错过！</p>
                                    <p class="hidden"><img src="~/Content/img/diamond.png" style="width:15px;" />沿海地段以灯塔、自然礁石群为主题的灯塔公园，感受“十里涛声卷落沙”的美妙意境</p>

                                    <p onclick="detail_close(0)" class="hidden"><span class="index_lineitem_down" ><img src="~/Content/img/arrow_up.png" /></span></p>
                                </div>
                        </div>
                  
                </li>--%>


           
                
            </ul>
        </div>
    </div>
    <div class="index_shopcontent" >
         <div style="position:absolute;top:-10px;left:5%;z-index:111;"><img width="14" src="content/img/arrow-2.png" /></div>
       <ul >
           <li><a>ABC_mart 八重洲地下街</a></li>
           <li><a>银座三月店</a></li>
           <li><a>东极首创店 </a></li>
       </ul>

    </div>

    <script type="text/javascript">
        window.onload=function(){
            var lineList = eval(<%=lineList%>);
            var bannerList = eval(<%=bannerList %>);
          //  alert(lineList.lineList[0].LineName);
        }
        
    </script>
</body>
</html>
<script src="/WeChat/freetrip/Scripts/jquery-1.11.3.min.js"></script>
<script src="/WeChat/freetrip/Scripts/iscroll-master/build/iscroll.js"></script>
<script src="/WeChat/freetrip/Scripts/common.js"></script>
<script>



  // document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);

    var data_banner =eval(<%=bannerList %>);
    var data_line= eval(<%=lineList%>).lineList;
    var banner_scroll;



    var width = 0;
    var conditionover = $(".index_banner").height() + 10;

    function init() {

        //获取总宽度
        width = $("body").width();
  
     
       
         init_banner();
         init_line();
     
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
        $(data).each(function (index,obj) {

            result += set_banner_item(obj);
          
            indexResult += " <li  class='banner_page_default' ></li>";
        });

        $(".banner_list").html(result);
        $(".banner_page_list").html(indexResult);

        $(".banner_list img").width(width);
    }

    function set_banner_item(obj){
        return " <li ><a  href='" + obj.AdPageUrl + "'><img  src='" + obj.AdPicUrl + "'  /></a></li>"; s
    }



    

    //line

    function init_line() {
      
        set_line(data_line);

    }


    function set_line(data) {
        var result = "";
       
        $(data).each(function (index, obj) {

            result += set_line_item(index,obj);
        });
        $(".index_linelist").html(result);
       

    }

    function set_line_item(index, obj) {
      
        var result = "<li class='index_lineitem'><a href='linedetail.aspx?id=" + obj.MisLineId + "'> <img src='" + obj.Pics + "' style='width:100%;height:140px;' /></a>";
        result += "<div class='index_lineitem_content'><div class='index_lineitem_title'><p style='width:76%;'>" + obj.LineName + "</p></div>";

        result+="<div class='index_lineitem_detail'>";
     //   result+="<p onclick='detail_open(0)' ><img src='Content/img/diamond.png' style='width:15px;'/>青岛、威海、日照、蓬莱、烟台、连云港等地全线游览...<span class='index_lineitem_down' ><img src='Content/img/arrow_down.png' /></span></p>";
      //  result += "<p ><img src='Content/img/diamond.png' style='width:15px;' />" + obj.LineFeature + "</p>";
        //    result+="<p onclick='detail_close(0)' class='hidden'><span class='index_lineitem_down' ><img src='Content/img/arrow_up.png' /></span></p>";
        result += "<span class='index_shopbtn' onclick='toggle_shop("+index+")' ><img src='content/img/eyes-2.png'/>查看商圈</span>";
        result += "<span class='index_money'> <span>￥" + obj.Price + "</span>起</span>";
        result+="</div></div></li>";
        return result;
    }

  
    function toggle_shop(index) {
     
        var shopdata = data_line[index].tradingArea;
        $(".index_shopcontent").hide();
        $(".index_shopbtn").children("img").attr("src","content/img/eyes-2.png");
       
        if ($(".index_shopbtn").eq(index).hasClass("index_shopbtn_cur")) {
            $(".index_shopbtn").eq(index).removeClass("index_shopbtn_cur");


        } else {
            $(".index_shopbtn").removeClass("index_shopbtn_cur");
        //加载数据
        $(".index_shopcontent ul").html("");
        if (shopdata!=null){
            for (i = 0; i < shopdata.length; i++) {
              
            $(".index_shopcontent ul").append(set_shopitem(shopdata[i]));

        }
     }
        //呈现
       
       // var height =  $("body").height();
        var top = $(".index_shopbtn").eq(index).offset().top+40;
            //  $(".index_shopcontent").height((height-top));
        $(".index_shopbtn").eq(index).children("img").attr("src", "content/img/eyes.png");

        $(".index_shopbtn").eq(index).addClass("index_shopbtn_cur");
        $(".index_shopcontent").css("top",top+"px").show();
        }
    }

    function set_shopitem(data) {
     
        return "<li><a href='shopdetail.aspx?id="+data.id+"'>" + data.name + "</a></li>";

    }

        //顶部菜单


        //产品介绍
        function detail_open(index) {
            $(".index_lineitem_detail").eq(index).children().eq(0).hide().siblings().slideDown(500);
        }

        function detail_close(index) {
            $(".index_lineitem_detail").eq(index).children().slideUp(500).eq(0).show();
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
        //window change

        //condtion——scroll
     //  window.ontouchmove = e_condtion_scroll;
      // window.ontouchstart = e_condtion_scroll;
       // window.ontouchend = e_condtion_scroll;

  //window.onscroll = e_condtion_scroll;
     

        function e_condtion_scroll(e) {
            var t = document.documentElement.scrollTop || document.body.scrollTop;

           
           if (t > conditionover) {
               $(".index_middle").addClass("condtion_overscroll");


           } else  {
               $(".index_middle").removeClass("condtion_overscroll");


           }
        }


        //function menuFixed(id) {
        //    var obj = document.getElementById(id);
        //    var _getHeight = obj.offsetTop;
        //    window.onscroll = function () {
        //        changePos(id, _getHeight);
        //    }
        //}
        //function changePos(id, height) {
        //    var obj = document.getElementById(id);
        //    var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
        //    if (scrollTop < height) {
        //        obj.style.position = 'relative';
        //        obj.style.top = "0px";
        //    } else {
        //        obj.style.top = "30px";
        //        obj.style.position = 'fixed';
        //    }
        //}

        //condtion——scroll
    


        function load_line(obj) {

            var key = $(obj).attr("tag");
 
            //$.get("/WeChat/AjaxService.aspx?action=FreetripSearchLineList&dest=" + key, function (result) {
            //   // $(".index_condition_content").hide();
            //    data_line = JSON.parse(result).lineList;
            //    set_line(data_line)
            //});

            var url = ("/WeChat/AjaxService.aspx?action=FreetripSearchLineList&dest=" + key);
            $.ajax({
                url: url,
                type: "GET",
                cache:false,
                dataType: 'text',
                beforeSend:function(){
                    $(".index_linelist").html("<div style='width:100%;text-align:center;'><img style='width:90%;' src='content/img/loading.gif'</div>");
                },
                success: function (data) {
                    data_line = JSON.parse(data).lineList;
                    $(obj).addClass("condition_cur").siblings().removeClass("condition_cur");
                    set_line(data_line);
                  
                },

                error: function (er) {

                    alert(er);
                }


            });
        }


        init();



  
   
</script>