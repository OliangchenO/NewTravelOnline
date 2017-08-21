
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
		var that = $(this);
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
$(function(){
	$('.sp').mouseenter(function(){                 //起价说明
		$('.show').show();
	}).mouseleave(function(){
		$('.show').hide();
	})


	$('#shareItem').mouseenter(function(){                 //分享
		$('#fxBox').show();
	}).mouseleave(function(){
		$('#fxBox').hide();
	})


	$('#order-info').find('.t2').click(function(){       
		var that = $(this);
		that.find('p').show();
		that.delegate('a', 'click',function(e){             //选择出发日期
			var _pNum = $('#order-info .t2').find('span');
			_pNum.text($(this).text());
			$(this).parent().hide();
			e.stopPropagation();
		})
	})



	$('#order-info').find('.t4').each(function(){       
		var that = $(this);
		that.click(function(){
			$(this).find('p').show();
		})
	})


	$('#order-info .t4').delegate('a', 'click',function(e){             //选择人数
		var that = $(this);
		var _pNum = $('#order-info .t4').find('span');
		_pNum.text($(this).text());
		$('#order-info .t4').find('p').hide();
		e.stopPropagation();
	})


	$('#order-info #js_select_num').each(function(){       
		var that = $(this);
		that.click(function(){
			$(this).find('.show-num').show();
		})
	})


	$('#order-info #js_show_num').delegate('a', 'click',function(e){             //选择份数
			var that = $(this);
			var _otherNum = $('#order-info #js_select_num').find('span');
			_otherNum.text($(this).text());
			$('#order-info #js_show_num').hide();
			e.stopPropagation();
		})


	$('#js_tab').delegate('a', 'click',function(){                  //详情页tab
		var that = $(this);
		that.addClass('green').siblings().removeClass('green');
	}) 
	

	$('#js_route_days').delegate('a', 'click',function(){            //行程天数
		var that = $(this);
		that.addClass('pink').siblings().removeClass('pink');
	}) 
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




















