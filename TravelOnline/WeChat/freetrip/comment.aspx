<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="comment.aspx.cs" Inherits="TravelOnline.WeChat.freetrip.comment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
       <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no"/>
      <link href="/WeChat/freetrip/Content/css/common.css" rel="stylesheet" />
     <style>
        body {
            background:white;
        }


        /*评论提交*/
        .comment_content {
        margin-top:50px;
      width:92%;
        margin-left:4%;
        overflow:hidden;
        }

            .comment_content ul {
            overflow:hidden;
            }

        .comment_content_score {
            font-size:14px;
        overflow:hidden;
        
        }
        .comment_content_star {
      overflow:hidden;

        }

        .comment_content_star img {
        width:28px;
        margin-left:2px;
        position:relative;
        top:5px;
      
    
        }
        .comment_content_title {
        font-size:15px;
        font-weight:bold;
        }

            .comment_content_title img {
            position:relative;
            top:8px;
            margin-right:5px;
            }

        .comment_content_bar {
            height:5px;
            background:#ec407a;
            margin-top:10px;
            border-radius:5px;
        
        }

        .comment_content li{
            margin-bottom:15px;
            overflow:hidden;
        }

        textarea {
        width:100%;height:150px;
       
      box-shadow:0;
        border:1px solid #bbb;
        border-radius:5px;
        -webkit-appearance: none;
        line-height:20px;
        font-size:13px;
         padding:5px;
        }

        .comment_upload_btn {
        float:left;
        width:30%;
        background:#ddd;
        overflow:hidden;
        margin-left:2%;
        height:70px;
        border-radius:5px;
        }

         .comment_upload_container {
         
         float:left;
        width:30%;
     
        overflow:hidden;
        margin-left:2%;
        height:70px;
        border-radius:5px;
       position:relative;
         }

         .comment_upload_container_img{
             width:100%;
                  height:70px;
         
         }

            .comment_upload_btn img {
                  width:100%;
                  height:70px;
                
            }

        .comment_submit_btn {
        width:60%;
        height:40px;
        background:#ec407a;
        color:white;
        font-size:15px;
        font-weight:bold;
        line-height:40px;
        margin:0 auto;
        text-align:center;
        border-radius:5px;
        margin-top:25px;
        }


        /**待评论*/

        .comment_list_content {
      
        overflow:hidden;
          /*width:92%;
        margin-left:4%;*/
        margin-top:50px;
        
        }

        .comment_tab_bar {
          overflow:hidden;
          width:60%;
        border-radius:5px;
        border:1px solid #ec407a;
        margin:0 auto;
        line-height:30px;
        text-align:center;
        }

        .comment_tab_btn{
            width:50%;
            background:white;
            color: #ec407a;
            float:left;
        }

        .comment_tab_btn_cur {
         background: #ec407a;
            color:white;
        
        }

        .comment_comment_list {
        
        overflow:hidden;
        margin-top:15px;
        }

      
        .comment_order_num {
            width:100%;
            height:30px;
            line-height:30px;
            padding-left:4%;
            overflow:hidden;
            font-size:13px;
            background:#ec407a;
            color:white;
        
        }

        .comment_line_banner img{
            width:100%;
            height:200px;    
        
        }

        .comment_line_info {
            line-height:25px;
            font-size:14px;
            width:92%;
            margin-left:4%;
            position:relative;
        }

        .comment_border {
        width:100%;
        height:5px;
        background:#ccc;
        margin-top:15px;
        }

        .comment_line_money span{
        color:#ff6600;
        }

        .comment_gocomment {
            position:absolute;
            width:100px;
            height:30px;
            line-height:20px;
            border:1px solid #ec407a;
            font-size:14px;
            color:#ec407a;
            border-radius:5px;
            bottom:0px;
            right:5px;
            overflow:hidden
        
        }
            .comment_gocomment img {
            width:20px;
            height:20px;
            position:relative;
            top:4px;
            margin-left:10px;
            margin-right:5px;
            }

        .comment_comment_info {
        margin-top:10px;
        overflow:hidden;
        border-top:1px solid #ccc;
        }


            /**公用*/
               .common_comment {
                        overflow:hidden;
                        }

                        .common_comment_content_list {

                         list-style:none;
                        margin-top:20px;
                    
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
    <link href="Scripts/jQuery-File-Upload-9.12.3/css/jquery.fileupload.css" rel="stylesheet" />
  
    <link href="Scripts/jQuery-File-Upload-9.12.3/css/jquery.fileupload-ui.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <div class="comment">
             <div class="comment_edit"  style="display:none;">
                 <div class="comment_top">
                     <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                         <span class="common_top_title"><span class="tttspans"></span>我的点评<span class="tttspan"></span></span>
                         <a class="common_top_back"  onclick="go_page(this)" href="/WeChat/freetrip/comment.aspx"><img src="Content/img/arrow_back.png" height="20" /></a>
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
                 <div class="comment_content">
                    <ul >
                        <li>
                            <div class="comment_content_title"><img src="Content/img/comment/comment_title.png" width="30" />您的评论</div>
                            <div class="comment_content_bar"></div>
                        </li>
                        <li>
                              <input  style='display:none;' class="star_scrore"  type="text" value="5"/>
                            <div class="comment_content_score">
                                <span>评分:</span>
                                <span class="comment_content_star">
                                    <img onclick="choose_star(0,this)" tag="极差" src="Content/img/comment/star_full.png" />
                                    <img  onclick="choose_star(1,this)" tag="有待提高"  src="Content/img/comment/star_full.png" />
                                    <img  onclick="choose_star(2,this)" tag="一般" src="Content/img/comment/star_full.png" />
                                    <img  onclick="choose_star(3,this)" tag="不错" src="Content/img/comment/star_full.png" />
                                    <img  onclick="choose_star(4,this)"  tag="优秀"  src="Content/img/comment/star_full.png" />
                                </span>
                                <span style="color:#ec407a;" class="comment_content_star_result"> 完美</span>
                              
                            </div>
                        </li>
                        <li>
                            <textarea class="comment_content_context" style="width:99%;height:150px;padding:0;" placeholder="请写下您对本次旅行的回忆"></textarea>
                          <div style="color:#ccc;text-align:right;">仅限100字</div>

                        </li>
                        <li >
                            <div style="font-size:14px;">上传照片:</div>
                            <div class="upload_img_list" style="margin-top:5px;overflow:hidden;">

                               

                                <div class="comment_upload_btn" >
                                <span id="weixin_upload" class="btn btn-primary fileinput-button">
                                    <span> <img  src="Content/img/comment/comment_photo.png" /></span>
                                     <input type="file" class="weixin_image" >
                                     </span>

                                </div>
                             
                            </div>
                            <div style="color:#ccc;text-align:right;">最多3张图</div>

                        </li>
                        <li >
                            <div class="comment_submit_btn" onclick="save_comment()">保存</div>

                        </li>
                    </ul>

                 </div>

             </div>


             <div class="comment_list">
                 <div class="comment_top">
                     <div class="common_top" style="position:fixed;left:0px;top:0px;z-index:10;">
                         <span class="common_top_title"><span class="tttspans"></span>我的评论<span class="tttspan"></span></span>
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

                 </div>

                 <div class="comment_list_content">
                     <div class="comment_tab_bar">
                         <div class="comment_tab_btn comment_tab_btn_cur" onclick="choose_comment_tab(this)"  index="0">待点评</div>
                         <div class="comment_tab_btn "  onclick="choose_comment_tab(this)"  index="1">已点评</div>
                     </div>
                     <div class="comment_tab_item">
                          <ul class="comment_comment_list comment_wait_list">
                             <%-- <li>
                                  <div class="comment_order_num">订单号:18333</div>
                                <div class="comment_line_banner"><a><img src="Content/img/text_index_banner.png" /></a></div>
                                  <div class="comment_line_info">
                                      <div class="comment_line_name">[FIT]普吉岛分角度来看沙发了可是大家来看</div>
                                      <div class="comment_line_startdate">出发日期：<span>2015-3-22</span></div>
                                      <div class="comment_line_money">总金额：<span>￥5800</span></div>
                                      <div class="comment_gocomment" onclick="go()"><img src="Content/img/comment/comment_go.png" />去点评</div>
                                  </div>
                                  <div class="comment_border"></div>
                              </li>--%>

                            
                           </ul>

                         <div class="div_flappage div_flappage_wait" onclick="load_wait(false)">加载更多 </div>
                     </div>

                     <div class="comment_tab_item" style="display:none;">
                         <ul class="comment_comment_list comment_has_list">
                           <li>
                                 <div class="comment_order_num">订单号:18333</div>
                                 <div class="comment_line_banner"><a><img src="Content/img/text_index_banner.png" /></a></div>
                                 <div class="comment_line_info">
                                     <div class="comment_line_name">[FIT]普吉岛分角度来看沙发了可是大家来看</div>
                                     <div class="comment_line_startdate">出发日期：<span>2015-3-22</span></div>
                                     <div class="comment_line_money">总金额：<span>￥5800</span></div>
                                  <div class="comment_comment_info">
                                      <ul class="common_comment_content_list">
                                          <li class="common_comment_content_item">
                                              <div class="comment_person ">
                                                  <img src="Content/img/comment/comment_person.png" />
                                                  <span>dsadasdasda</span>

                                              </div>
                                              <div class="comment_star">
                                                  <img width="15" src="Content/img/comment/star_full.png" />
                                                  <img width="15" src="Content/img/comment/star_full.png" />
                                                  <img width="15" src="Content/img/comment/star_full.png" />
                                                  <img width="15" src="Content/img/comment/star_full.png" />
                                                  <img width="15" src="Content/img/comment/star_empty.png" />
                                              </div>
                                              <div class="comment_date">

                                                  2016-02-05
                                              </div>
                                          </li>
                                          <li class="common_comment_item">
                                              <p>
                                                  邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放
                                                  邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放
                                                  邮轮点击撒赖看见分厘卡圣诞节分厘卡撒旦解放
                                              </p>

                                          </li>
                                          <li class="common_comment_item">
                                              <ul class="common_comment_img_list">
                                                  <li class="common_comment_img_item">
                                                      <img src="Content/img/tesss.jpg" />
                                                  </li>
                                                  <li class="common_comment_img_item">
                                                      <img src="Content/img/tesss.jpg" />
                                                  </li>
                                                  <li class="common_comment_img_item">
                                                      <img src="Content/img/tesss.jpg" />
                                                  </li>
                                              </ul>

                                          </li>
                                      </ul>


                                  </div>
                                 </div>
                                 <div class="comment_border"></div>
                             </li>

                  
                         </ul>
                             <div class="div_flappage div_flappage_has" onclick="load_has(false)">加载更多 </div>
                     </div>
                 </div>
             </div>
     </div>
</body>
</html>
<script src="Scripts/jquery-1.11.3.min.js"></script>
<script src="Scripts/jQuery-File-Upload-9.12.3/js/vendor/jquery.ui.widget.js"></script>

<script src="Scripts/jQuery-File-Upload-9.12.3/js/jquery.iframe-transport.js"></script>
<script src="Scripts/jQuery-File-Upload-9.12.3/js/jquery.fileupload.js"></script>

<script src="/assets/plugins/jquery.blockui.min.js"></script>
<script src="Scripts/common.js"></script>
<script>
    


    

    function init() {
        load_wait(true);
        load_has(true);
    }

    
    //wait
    var wait_data=[];
    var wait_pageindex = 1;
    var wait_pagesize = 3;
    var wait_currpage = 0;
    var wait_totalpage = 0;

    function load_wait(isclear) {
        if (isclear) {
            wait_data = [];
             wait_pageindex = 1;
             wait_pagesize = 3;
             wait_currpage = 0;
            wait_totalpage = 0;
        }
        var url = "/WeChat/AjaxService.aspx?action=queryCommentList";
        url += "&pagesize=" + wait_pagesize;
        url += "&currpage=" + wait_pageindex;
        url += "&commentStatus=UNCOMMENT";
        $.ajax({
            url: url,
            cache: false,
            type: "get",
          //  data: JSON.stringify({ pageIndex: pageIndex, pagesize: pageSize }),
         //   contentType: 'application/json; charset=utf-8',  //must
            dataType: "JSON",
            success: function (data) {
                if (data.commentList != null) {
                    if (wait_pageindex < data.pageCount) {
                        wait_pageindex = (wait_pageindex + 1);
                    } else {
                        $(".div_flappage_wait").hide();
                    }
                    set_wait(data.commentList, isclear);

                 
                }
            },
            beforeSend: function () {
                // 加载loading

            }
        });

    }

    function set_wait(data,isclear) {
        if (isclear) {
            $(".comment_wait_list").html("");
        }
        $(data).each(function (index,obj) {
            $(".comment_wait_list").append(set_wait_item(index,obj));
        });
  
    }

    function set_wait_item(index,data) {
        var result = "";
        result += "<li tag=" + data.id + "><div class='comment_order_num'>订单号:" + data.orderId + "</div>";
        result += "<div class='comment_line_banner'><a><img src='" + data.linePic + "' /></a></div>";
        result+= "<div class='comment_line_info'>";
        result += "<div class='comment_line_name'>" + data.lineName + "</div>";
        result += "<div class='comment_line_startdate'>出发日期：<span>" + formatDate(data.beginDate) + "</span></div>";
        result += "<div class='comment_line_money'>总金额：<span>￥" + data.price + "</span></div>";
        result += "<div class='comment_gocomment' tag='" + data.id + "'  onclick='go_comment(this)'><img src='Content/img/comment/comment_go.png' />去点评</div>";
        result += "</div><div class='comment_border'></div></li>";
        return result;
    }


    //wait
    var has_data = [];
    var has_pageindex = 1;
    var has_pagesize = 3;
    var has_currpage = 0;
    var has_totalpage = 0;

    function load_has(isclear) {
        if (isclear) {
             has_data = [];
            has_pageindex = 1;
             has_pagesize = 3;
             has_currpage = 0;
             has_totalpage = 0;
        }
        var url = "/WeChat/AjaxService.aspx?action=queryCommentList";
        url += "&pagesize=" + has_pagesize;
        url += "&currpage=" + has_pageindex;
        url += "&commentStatus=COMMENTED";
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
                        $(".div_flappage_has").show();
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
        result += "<li><div class='comment_order_num'>订单号:" + data.orderId + "</div>";
        result += "<div class='comment_line_banner'><a><img src='" + data.linePic + "' /></a></div>";
        result += "<div class='comment_line_info'>";
        result += "<div class='comment_line_name'>" + data.lineName + "</div>";
        result += "<div class='comment_line_startdate'>出发日期：<span>" + formatDate(data.beginDate) + "</span></div>";
        result += "<div class='comment_line_money'>总金额：<span>￥" + data.price + "</span></div>";
        result += set_has_item_comment(data);
        result += "</div><div class='comment_border'></div></li>";
        return result;
    }

    function set_has_item_comment(data) {
        var result = "";
        result+="<div class='comment_comment_info'><ul class='common_comment_content_list'> <li class='common_comment_content_item'>";
        result += "<div class='comment_person'><img src='Content/img/comment/comment_person.png' /><span>" + data.userName + "</span></div>";
        result += "<div class='comment_star'>" + set_has_rank(data.rank)+ "</div>";
        result += "<div class='comment_date'>" + formatDate(data.commentTime) + "</div></li>";
        result += "<li class='common_comment_item'><p>" + data.context + "</p> </li>";
        result += "<li class='common_comment_item'><ul class='common_comment_img_list'>" + set_has_img(data.pic) + "</ul>";
        result += "</li></ul></div>";
        return result;
    }

    function set_has_rank(rank) {
        var result = "";
      
        for (var i = 1; i <=5; i++) {
            var name="star_empty.png";
            if (rank > i) {
                name = "star_full.png";
            }
            result += "<img width='15' src='Content/img/comment/"+name+"' />";
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

    // multiple
    $(function () {
        $(".weixin_image").fileupload({
            url: '/WeChat/AjaxService.aspx?action=uploadImgFile',
            sequentialUploads: true
        }).bind('fileuploadprogress', function (e, data) {
            //var progress = parseInt(data.loaded / data.total * 100, 10);
            //$("#weixin_progress").css('width', progress + '%');
            //$("#weixin_progress").html(progress + '%');
        }).bind('fileuploaddone', function (e, data) {
            //$("#weixin_show").attr("src", "__PUBLIC__/" + data.result);
            //$("#weixin_upload").css({ display: "none" });
            //$("#weixin_cancle").css({ display: "" });

            add_img(data.result);
           
        });

    });

    function add_img(data) {
   
   
        var result = " <div class='comment_upload_container' style='backgroud:#fff;'><img onclick='delete_img(this)' style='width:20px;height:20px;position:absolute;top:2px;right:2px;' src='content/img/cha.png' /><span> <img class='comment_upload_container_img' src='" + data + "' /></span></div>";
        $("#weixin_upload").parent().before(result);
        if ($(".comment_upload_container").size() > 2) {
            $("#weixin_upload").parent().hide();
        }
    }


    function choose_comment_tab(obj) {
        $(obj).addClass("comment_tab_btn_cur").siblings().removeClass("comment_tab_btn_cur");
        var index = $(obj).attr("index");
        
        $(".comment_tab_item").hide().eq(index).show();
    }

    var now_comment_id;

    function go_comment(obj) {
        init_go_comment();
        $(".comment_list").hide().siblings().show();
        var id = $(obj).attr("tag");
        now_comment_id=id;
      
    }
    
    function choose_star(index, obj) {
    
        var content = $(obj).attr("tag");
        $(".comment_content_star img:gt(" + index + ")").attr("src", "Content/img/comment/star_empty.png");
        $(".comment_content_star img:lt(" + (index + 1) + ")").attr("src", "Content/img/comment/star_full.png");
        $(".comment_content_star_result").html(content);
      $(".star_scrore").val(index+1);
    }


    function delete_img(obj){
        $(obj).parent().remove();
        if ($(".comment_upload_container").size() <3) {
            $("#weixin_upload").parent().show();
        }
    }

    function save_comment() {
      
        var id = now_comment_id;
        var rank = $(".star_scrore").val();
        var pic = "";
        $(".comment_upload_container_img").each(function (index,obj) {
            if ($(obj).attr("src") != "") {
                if (index != 0) {
                    pic += ","
                }
                pic += $(obj).attr("src");
             
            }

        });

        var context = $(".comment_content_context").val();
        var url = "/WeChat/AjaxService.aspx?action=publishComment";
    
      
        //提交
        $.ajax({
            url: url,
            cache: false,
            type: "post",
            data: { id: id, rank: rank, pic: pic, context: context },
            //   contentType: 'application/json; charset=utf-8',  //must
            dataType: "JSON",
            success: function (data) {
                $.unblockUI();
             
                if (data.isError == 0) {
                    show_loading(data.msg);
                    window.setTimeout(function () {
                        $.unblockUI();
                        $(".comment_list").show().siblings().hide();
                        $(".comment_wait_list li[tag='" + id + "']").remove();
                        init_go_comment();
                        load_has(true);
                    }, 2000);
                 
                  

                } else {
                    show_loading(data.msg);
                    window.setTimeout(function () {
                        $.unblockUI();
                      
                    }, 2000);
                }


            },
            beforeSend: function () {
                // 加载loading
                show_loading("处理中");
            }
        });


    }
    function show_loading(msg) {
        $.blockUI({
            message: msg,
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


    function init_go_comment() {
        $(".comment_content_star img").eq(4).trigger("click");
        $(".comment_upload_container").remove();
        now_comment_id =null;
        $(".comment_content_context").val("")
    }

    init();
</script>