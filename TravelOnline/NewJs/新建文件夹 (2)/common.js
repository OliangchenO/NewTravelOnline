
/*我的青旅*/
$(function(){
	$('#top_dropmenu').hover(function(){
		var that = $(this);
		that.find('b').css('background-position','-15px 0');
		that.find('ul').stop().slideDown(200);
	},function(){
		$('#top_dropmenu').find('ul').stop().slideUp(200);
		$('#top_dropmenu').find('b').css('background-position','0 0');
	});
})


/*微信*/
$(function(){
	$('#top_showwx').hover(function(){
		var that =$(this);
		that.find('.top-wx').stop().show();
	},function(){
		$('.top-wx').stop().hide();
	});
})

/*城市切换*/
$(function(){
	$('#changeNav').find('li').each(function(){
		var that = $(this);
		that.click(function(){
			$(this).addClass('current').siblings().removeClass('current');
		});
	});

//	$('#changeArea').find('li').each(function(){  //旅游项目对应内容
//		var that = $(this);
//		that.click(function(){
//			that.addClass('current').siblings().removeClass('current').end().parents('#changeArea').siblings().eq($(this).index()).addClass('action').siblings().removeClass('action');
//		});
//	});
})


/*搜索框*/
$(function(){
	$('#seachItem').click(function(){                            //显示热门搜索
		var that = $(this);
		that.parents('.wrap-pd').children('#whither_seach').show();
		that.css('border-color','#2382d9').val('').next().addClass('color2382d9');
	})


	$('#seach_listSelect').mouseenter(function(){                 //详情页面搜索
		$('#seach_selectBox').show();
	}).mouseleave(function(){
		$('#seach_selectBox').hide();
	})


	$('#seach_selectBox a').click(function(){
		$('#seach_listSelect span').text($(this).text());
		$('#seach_selectBox').hide();
	})


	$('#whither_seach b').click(function(){
		$('#whither_seach').hide();
		$('#seachItem').css('border-color','#c6c6c6');
	})
	

	$('#whither_seach a').click(function(){                      //点击目的地添加到文本框
		var that = $(this);
		$('#seachItem').val($(this).text());
		that.parents('#whither_seach').hide();
		$('#seachItem').css('border-color','#c6c6c6');
	})


	$('#searchPageInput').click(function(){        //搜索页面显示热门搜索
		$('#popularSeach').show();
	})


	$('#popularSeach .close').click(function(){        
		$('#popularSeach').hide();
	})


	$('#popularSeach a').click(function(){                      //搜索页面点击目的地添加到文本框
		var that = $(this);
		$('#searchPageInput').val($(this).text());
		$('#popularSeach').hide();
	})


	$('#outbound-area').delegate('li', 'click',function(){          //出境目的地弹出层tab栏切换
		var that = $(this);
		$(this).addClass('current').siblings().removeClass('current').end().parents('.outbound-box').find('.column-area').eq($(this).index()).addClass('action').siblings().removeClass('action');
	});


	$('#seach').hover(function(){
		var that = $(this);
		that.toggleClass('color2382d9');
	});
})


/*高级搜索*/
$(function(){   
	$('#js_show_hotseach').click(function(){        					 //页面顶部显示高级搜索
		$('#unlimited').show();
	})


	$('#highSeach').click(function(){         						 //显示高级搜索
		$('#unlimited').show();
	})

	$('#hotseachClose').click(function(){
		$('#unlimited').hide();
		showbg.remove();
	})

	$('#seachTxt, #js_show_hotseach').focus(function(){
		var that = $(this);
		that.addClass('blue');
		if($(this).val() == '请输入目的地、主题或关键字'){
			$(this).val('');
		}
	}).blur(function(){
		$('#seachTxt, #js_show_hotseach').removeClass('blue');
		if($(this).val() == ''){
			$(this).val('请输入目的地、主题或关键字');
		}
	})


	$('#unlimited').delegate('span','click',function(){ 			//点击不限清空元素内容
		var that = $(this);
		that.parent().siblings().val('');
	})

	$('#unlimiteDay').click(function(){   //点击不限清空复选框
		var selectBox = $('.select-box').find('input[type="checkbox"]');
		selectBox.each(function(){
			$(this).removeAttr('checked','false');
		});
	});

	$('#hotseachBtn').hover(function(){         
		var that = $(this);
		that.toggleClass('action');
	});
})

/*首页侧边栏*/
$(function(){
	var num = 1;
	var timer;
	$('#js_sidenav li').mouseenter(function(){        
		var that = $(this);
		that.addClass('cur').siblings().removeClass('cur').end().parents().find('.side_jmp').eq($(this).index()).show().siblings('.side_jmp').hide();
	})

	$('#js_sidenav').mouseleave(function(){        
		var that = $(this);
		that.find('li').removeClass('cur');
		$('#js_sidenav .side_jmp').hide();
	})

	$('#js_move_ad_num li').mouseenter(function(){
		var myIndex = $(this).index();
		$('#js_move_ad li').eq(myIndex).addClass('cur').siblings().removeClass('cur');
		$(this).addClass('act').siblings().removeClass('act');
		num = $(this).index() + 1;
	})

	function run(){
		$('#js_move_ad_num li').eq(num % 2).addClass('act').siblings().removeClass('act');
		$('#js_move_ad li').eq(num % 2).addClass('cur').siblings().removeClass('cur');
		num++;
	}
	timer = setInterval(run,4000);
	$('#js_move_ad_num li, #js_move_ad li').mouseenter(function(){
		clearInterval(timer);
	}).mouseleave(function(){
		timer = setInterval(run,4000);
	})

})


/*banner*/
$(function(){
	var num = 1;
	var timer;
	$('#bannerGoods li').mouseenter(function(){
		var myIndex = $(this).index();
		$('#indexTopbanner li').eq(myIndex).addClass('current').siblings().removeClass('current');
		$(this).addClass('current').siblings().removeClass('current');
		num = $(this).index() + 1;
	})

	function run(){
		$('#indexTopbanner li').eq(num % 5).addClass('current').siblings().removeClass('current');
		$('#bannerGoods li').eq(num % 5).addClass('current').siblings().removeClass('current');
		num++;
	}
	timer = setInterval(run,5000);
	$('#bannerGoods li, #indexTopbanner li').mouseenter(function(){
		clearInterval(timer);
	}).mouseleave(function(){
		timer = setInterval(run,5000);
	})
})


/*特惠精选*/
$(function(){
	$('#topsaleImg').delegate('li', 'hover',function(){
		var that = $(this);
		that.find('.salemask').toggleClass('action');
	});

})


/*当季推荐*/
$(function () {
    var _proNum = 1;
    var _nums = $("#pro_mrTop li").length;
    var setTime;
    var posX = '52px';
    $('#change_Bg').toggle                                       //换一批
		(function () {
		    var that = $(this);
		    that.css({ 'background-position': '0 -325px', 'background-position-x': posX });
		    up();
		}, function () {
		    $(this).css({ 'background-position': '0 -348px', 'background-position-x': posX });
		    up();
		}, function () {
		    $(this).css({ 'background-position': '0 -303px', 'background-position-x': posX });
		    up();
		}
	)

    function up() {
        $('#pro_mrTop').stop().animate({ top: _proNum % _nums * -290 }, 500);
        _proNum++;
        if (_proNum > _nums) _proNum = 1;
    }

    setTime = setInterval(up, 5000);

    $('#pro_mrTop,#change_Bg').mouseenter(function () {
        clearInterval(setTime);
    }).mouseleave(function () {
        setTime = setInterval(up, 5000);
    })
})


/*国内旅游*/
$(function(){
	$('#chujing_area, #guonei_area, #item_area').delegate('li', 'click',function(){        //首页tab切换
		var that = $(this);
		that.addClass('current').siblings().removeClass('current').end().parents('.tab-wrap').siblings().find('ul').eq($(this).index()).addClass('current').siblings().removeClass('current');
	});


	$('#chujing_area, #guonei_area, #item_area').delegate('li', 'click',function(){        //线路精选tab切换
		var that = $(this);
		that.addClass('current').siblings().removeClass('current').end().parents('.tab-wrap').siblings().find('#changeProduct_list').eq($(this).index()).addClass('current').siblings().removeClass('current');
	});


	$('.line-selected #changeProduct_list').each(function(){
		var that = $(this);
		that.delegate('dl', 'mouseenter', function(){  							 //滑出推荐理由
			var that = $(this);
			that.find('.recommend').stop().animate({'top':'0'},300).show();
		}).delegate('dl', 'mouseleave',function(){
			that.find('.recommend').stop().animate({'top':'140'},300);
		});
	})
	

	$('#cjProduct_img, #changeProduct_list').each(function(){           //产品底部出现蓝边
		var that = $(this);
		that.delegate('li', 'hover',function(){
			var that = $(this);
			that.toggleClass('current').siblings().removeClass('current');

		});
	});


	$('#group_area').delegate('h2', 'click',function(){        //自由行、邮轮、签证
		var that = $(this);
		that.addClass('current').siblings().removeClass('current').end().parents('.top-group').find('p').eq($(this).index()).addClass('current').siblings().removeClass('current');
		that.parents('.top-group').siblings('.bottom-group').children('dl').eq($(this).index()).addClass('show-group').removeClass('hide-group').siblings('dl').removeClass('show-group').addClass('hide-group');
	});


	$('#freedom_txt, #theme_ad').mouseenter(function(){                            //自由行广告文字移动
		var that = $(this);
		that.children().find('span').animate({'text-indent': '10px'},400);
	}).mouseleave(function(){
		$(this).children().find('span').animate({'text-indent': '0'},400);
	})

	
	$('#city_area').delegate('dl', 'click',function(){        
		var that = $(this);
		that.addClass('action').siblings().removeClass('action').end().parents('.group-list').find('ul').eq($(this).index()).addClass('action').siblings().removeClass('action');
	});

	
	$('#city_area01, #city_area02, #city_area03').delegate('dl', 'click',function(){        
		var that = $(this);
		that.addClass('action').siblings().removeClass('action').end().parents('.group-list').find('ul').eq($(this).index()).addClass('action').siblings().removeClass('action');
	});

	
	$('#qz_answer, #free_answer').delegate('p', 'hover',function(){ 
		var that = $(this);
		that.find('b').toggleClass('color');
	});
})


$(function(){
	var num = 1;
	$('#change_line').delegate('li', 'click',function(){                           //邮轮航线切换
		var that = $(this);
		that.find('span').addClass('current').parent().siblings('li').children('span').removeClass('current');
		
	});

	$('#question li').mouseover(function(){                                        //邮轮常见问题切换
			var myIndex = $(this).index();
			$('#question_list').stop().animate({marginLeft:myIndex*-1200},500);
			$(this).addClass('current').siblings().removeClass();
			num = $(this).index() + 1;
		})
})

/*
$(function(){
	 $('#state_content a[href*=#]').click(function() {                             //签证产品列表锚点
	 	var that = $(this);
		that.addClass('colorF38e22').siblings().removeClass('colorF38e22');
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var $target = $(this.hash);
            $target = $target.length && $target || $('[name=' + this.hash.slice(1) + ']');
            if ($target.length) {
                var targetOffset = $target.offset().top;
                $('html,body').animate({
                    scrollTop: targetOffset
                },
                500);
                return false;
            }
        }

    });


	$(window).scroll(function(){
		if($(window).scrollTop() > 200){
			$('#state_content').addClass('fixed')
		}else{
			$('#state_content').removeClass('fixed')
		}
	});
})
*/

$(function(){
	$('.species-wrap').delegate('li', 'click',function(){             //筛选条件
		var that = $(this);
		$(this).addClass('current').siblings().removeClass('current').end().parent().siblings('.prop-list').find('.prop-item').eq($(this).index()).addClass('action').siblings().removeClass('action');
	})


	$('.prop-item').delegate('a', 'click',function(){            
		var that = $(this);
		that.addClass('color').siblings().removeClass('color');
	})


	$('.screening-wrap a').click(function(){                         //升降序
		var that = $(this);
		that.addClass('color').siblings().removeClass('color');
		that.find('i').toggleClass('pos71');
	})


	$('#price_chose01,#price_chose02').each(function(){
		var that = $(this);
		that.click(function(){
			$(this).val('');
		});
	})

})


/*详情页*/
$(function () {
    $('.sp').mouseenter(function () {                 //起价说明
        $('.show').show();
    }).mouseleave(function () {
        $('.show').hide();
    })

    $('#shareItem').mouseenter(function () {                //分享
        $('#fxBox').show();
    }).mouseleave(function () {
        $('#fxBox').hide();
    })
    $(document).bind("click", function () {
        $('#order-info p').hide();
    });

    $('#order-info').find('.t2').click(function () {
        window.event.cancelBubble = true;
        $('#order-info p').hide();
        var that = $(this);
        that.find('p').show();
        that.delegate('a', 'click', function (e) {             //选择出发日期
            var _pNum = $('#order-info .t2').find('span');
            _pNum.text($(this).text());
            _pNum.attr("tag", $(this).attr("tag"));
            _pNum.attr("date", $(this).attr("date"));
            LoadPrice($(this).attr("tag"), $(this).attr("date"));
            $(this).parent().hide();
            e.stopPropagation();
        })
    })

    $('#order-info').find('.t4').click(function () {
        window.event.cancelBubble = true;
        $('#order-info p').hide();
        var that = $(this);
        that.find('p').show();
        that.delegate('a', 'click', function (e) { 
            var _pNum = $('#order-info .t4').find('span');
            _pNum.text($(this).text());
            $(this).parent().hide();
            e.stopPropagation();
        })
    })

    $('#order-info').find('.t5').click(function () {
        window.event.cancelBubble = true;
        $('#order-info p').hide();
        var that = $(this);
        that.find('p').show();
        that.delegate('a', 'click', function (e) { 
            var _pNum = $('#order-info .t5').find('span');
            _pNum.text($(this).text());
            $(this).parent().hide();
            e.stopPropagation();
        })
    })

    $('#js_tab').find('a').click(function () {
        var that = $(this);
        that.addClass('green').siblings().removeClass('green');
    })
	$('#js_tab_c').find('a').click(function () {
        var that = $(this);
        that.addClass('blue').siblings().removeClass('blue');
    })

//    $('#js_route_days').delegate('a', 'click', function () {
//        var that = $(this);
//        that.addClass('pink').siblings().removeClass('pink');
//    })
})

/*返回顶部*/
$(function(){
	$("#goTop").hide();

	$(window).scroll(function(){
		if($(window).scrollTop() > 100){
			$('#goTop').fadeIn(1000);
		}else{
			$('#goTop').fadeOut(1000);
		}
	});

	$('#goTop').click(function(){
		$('body,html').animate({scrollTop:0},300);
		return false;
	});

})


/* 签证详情页标签卡 */
$(function(){
	var $tab_li = $('.material-tab ul li');
	$tab_li.click(function(){
		$(this).addClass('cur').siblings().removeClass('cur');
		var index = $tab_li.index(this);
		$('.material-content > div').eq(index).show().siblings().hide();
	});	
});

$(function () {
    /* 邮轮详情页标签卡 */
    var $tab_li = $('.cruises-select ul li');
    $tab_li.click(function () {
        $(this).addClass('cur').siblings().removeClass('cur');
        var index = $tab_li.index(this);
        $('.select-content > div').eq(index).show().siblings().hide();
    });

    /*邮轮详情页取消边框*/
    $("#nc .choose-box:last,#hj .choose-box:last,#lt .choose-box:last,#tf .choose-box:last").css("border", "none");

    /*邮轮详情页舱房详情*/
    $('.cangfang').mouseover(function () {
        var this_obj = $(this).find(".cf-detail");
        this_obj.show();
        if ($(this).attr("tag") != "0" && this_obj.html() == "") {
            this_obj.html("载入中请稍候...");
            var url = "/NewPage/AjaxService.aspx?action=LoadRoomPic&id=" + $(this).attr("tag") + "&r=" + Math.random();
            //window.open(url);
            $.get(url, function (data) {
                this_obj.html(data);
            });

        }
    }).mouseleave(function () {
        $(this).find(".cf-detail").hide();
    });

    /*岸上观光选择详情*/
    $('.sight-select .s2').mouseover(function () {
        $(this).find(".s-detail").show();
    }).mouseleave(function () {
        $(this).find(".s-detail").hide();
    });

    /*邮轮详情页舱房点击展开*/
    $('.choose-box .sdl .t7').click(function () {
        var this_obj = $(this).next('.choose');
        this_obj.slideToggle(300);
        if ($(this).attr("tag") != "0" && this_obj.html() == "") {

            this_obj.html("载入中请稍候...");
            var url = "/NewPage/AjaxService.aspx?action=LoadRoomInfo&id=" + $(this).attr("tag") + "&r=" + Math.random();
            $.get(url, function (data) {
                this_obj.html(data);
                $('select').select2({ minimumResultsForSearch: -1 });
            });
        }
    });

    /*邮轮详情页舱房点击确定*/
    //    $('.hdl .d5').toggle(function () {
    //        $(this).find('b').css("background-position", "0 -333px");
    //    }, function () {
    //        $(this).find('b').css("background-position", "-17px -316px");
    //    });

    /*邮轮详情页舱房修改遮罩*/
    $('.alter').click(function () {
        $('.popover-mask').show();
        $(this).next('.alter-popover').show();
    })
    $('.confirm, .cancel').click(function () {
        $('.popover-mask').hide();
        $('.alter-popover').hide();
    });

    /*邮轮详情页选择岸上观光遮罩*/
//    $('.affirm-buy a').click(function () {
//        $('.popover-mask').show();
//        $('.shore-popover').show();
//    })
    $('.shore-popover .close').click(function () {
        $('.popover-mask').hide();
        $('.shore-popover').hide();
    });

    /*邮轮详情页岸上观光*/
    $(".shore .click-bnt").toggle(function () {
        $(this).next('.sight-box').css("height", "auto");
        $(this).html("隐藏详情<i></i>");
        $(this).find('i').css("background-position", "-14px -393px");
    }, function () {
        $(this).next('.sight-box').css("height", "16px");
        $(this).html("展开详情<i></i>");
        $(this).find('i').css("background-position", "0px -393px");
    }
		);
});

/*预订填写展开*/
$(function(){
	$(".titbox .click-bnt").toggle(function(){
        $(this).next('.shrink').css("height","auto");
        $(this).html("隐藏详情<i></i>");
		$(this).find('i').css("background-position","-83px -353px");
        },function(){
        $(this).next('.shrink').css("height","21px");
        $(this).html("展开详情<i></i>");     
		$(this).find('i').css("background-position","-68px -353px");     
        }
		);     
});

/*表单*/
 $(function(){
		$(".input-w, #ms").focus(function(){
			  if($(this).val() ==this.defaultValue){  
                  $(this).val("");     
				  $(this).removeClass("text_default");      
			  };
			  
		}).blur(function(){
			 if ($(this).val() == '') {
                $(this).val(this.defaultValue);
				$(this).addClass("text_default");
             };
			 
		});
		
		$("#ms").keyup(function(){
		   var len = $(this).val().length;
		   if(len > 99){
			$(this).val($(this).val().substring(0,100));
		   }
		   var num = 100 - len;
		   $("#ms").text(num);
		  });
		
		/*现在/稍后 填写*/
	   $(".sr-box .rad1").click(function(){
		  $(this).parent().siblings('.sr-info').show();
		  $(this).siblings(".sr-box .rad2").find('span').hide();
		});	
	   $(".sr-box .rad2").click(function(){
		  $(this).parent().siblings('.sr-info').hide();
		  $(this).find('span').show();
		});	
		
		/*是否需要发票,点击否清空内容*/
	   $(".fq-f").click(function(){
		  $(this).parent().siblings('.fp-box').hide();
		  $(this).parent().siblings('.fp-box').find(":input").val("").remove("selected");
		});	
	   $(".fq-s").click(function(){
		  $(this).parent().siblings('.fp-box').show();
		});	
		
		/*发票配送方式*/
		$(".fp-box .kd").click(function(){
		  $(this).parent().siblings('.exp1').show();
		  $(this).parent().siblings('.exp2').hide();
		});	
	   $(".fp-box .zd").click(function(){
		  $(this).parent().siblings('.exp2').show();
		  $(this).parent().siblings('.exp1').hide();
		});	
		
		/*点击身份证*/
			$(".cer").change(function(){
			$(this).next(".zj").val("证件号码");
			if( $(this).val() == "1"){
			$(this).parent().next(".visa-sr").hide();
			}
			else
			{
			$(this).parent().next(".visa-sr").show();
			}
            });
			
		/*保存信息*/
		$(".save-box .s2").toggle(function(){ 
		  $(this).find('i').css("background-position","-48px -390px"); 
		},function(){
		  $(this).find('i').css("background-position","-35px -390px"); 
		});
		
		/*游客信息清空*/
		//$(".save-box .s3").click(function() {
//            $(this).parent().parent().next(".ykxx").find(":input").val("")/*.removeAttr("checked")*/.remove("selected");//核心
//			$(this).parent().parent().next(".ykxx").find(".visa-sr").hide();
//        });
		
		//证件稍后输入清空内容
		$(".sr-box .rad2").click(function() {
            $(this).parent().next(".sr-info").find(":input").val("");
			$(this).parent().next(".sr-info").find(".visa-sr").hide();
        });
		
		
})

/* 支付方式标签卡 */
$(function(){
	var $tab_li = $('.payment-box ul li');
	$tab_li.click(function(){
		$(this).addClass('cur').siblings().removeClass('cur');
		var index = $tab_li.index(this);
		$('.payment-con .tabs').eq(index).show().siblings().hide();
	});	
	
	/*支付页服务协议*/
	$('.pfxy').click(function(){
		$('.popover-mask').show();
		$('.popover').show();
	})
	$('.popover .agc').click(function(){
		$('.popover-mask').hide();
		$('.popover').hide();
		$(".che").attr("checked","checked");
	});
	
	/*卡背面*/
	$('#cvv2').focus(function(){
		$(this).parent().next(".card-yzm").show();
	})
	$('#cvv2').blur(function(){
		$(this).parent().next(".card-yzm").hide();
	});
	
	
});

function test(Names){
	var Name
	for (var i=1;i<11;i++){	//  更改数字4可以改变选择的内容数量，在下拉总数值的基础上+1.比如：下拉菜单有5个值，则4变成6
		var tempname="mune_c"+i                                                                            
		var NewsHot="c"+i	//  “c”是ID名称，比如：ID命名为“case1”，这里的“c”即为“case”
		if (Names==tempname){
			Nnews=document.getElementById(NewsHot)
			Nnews.style.display='';
		}else{
			Nnews=document.getElementById(NewsHot)
			Nnews.style.display='none';   
		}
	}
}

//分享	
    $(document).ready(function (e) {
        var share_url = encodeURIComponent(location.href);
        var share_title = encodeURIComponent(document.title);
        var share_pic = "../images/sharepic.jpg";  //默认的分享图片
        var share_from = encodeURIComponent("上海青旅网站"); //分享自（仅用于QQ空间和朋友网，新浪的只需更改appkey 和 ralateUid就行）
        //qq空间
        $('.kj').click(function (e) {
            window.open("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_onekey?url=" + share_url + "&title=" + share_title + "&pics=" + share_pic + "&site=" + share_from + "", "newwindow");
        });
        //新浪微博
        $('.wb').click(function (e) {
            window.open("http://v.t.sina.com.cn/share/share.php?title=" + share_url + "&title=" + share_title + "&pics=" + share_pic + "&site=" + share_from + "", "newwindow");
        });
        //人人
        $('.rr').click(function (e) {
            window.open('http://widget.renren.com/dialog/share?resourceUrl=' + share_url + '&title=' + share_title + '&images=' + share_pic + '', 'newwindow');
        });
        //腾通微博
        $('.tx').click(function (e) {
            window.open('http://share.v.t.qq.com/index.php?c=share&a=index&title=' + share_title + '&site=' + share_from + '&pic=' + share_pic + '&url=' + share_url + '', 'newwindow');
        });
		//豆瓣
		$('.db').click(function (e) {
            window.open('http://www.douban.com/recommend/?url=' + share_url + '&pics=' + share_pic + '&title=' + share_title + '&site=' + share_from + '', 'newwindow');
        });
		//qq
		$('.qq').click(function (e) {
            window.open('http://connect.qq.com/widget/shareqq/index.html?url=' + share_url + '&pics=' + share_pic + '&title=' + share_title + '&site=' + share_from + '', 'newwindow');
        });
    });

    function AddFavorite() {
        sURL = "www.scyts.com";
        var a = "http://www.scyts.com/";
        var b = "上海青旅";
        try {
            if (document.all) {
                window.external.AddFavorite(a, b)
            } else if (window.sidebar) {
                window.sidebar.addPanel(b, a, "")
            } else {
                alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。")
            }
        }
        catch (e) {
            alert("对不起，您的浏览器不支持此操作!\n请您使用菜单栏或Ctrl+D收藏本站。")
        }
    }






