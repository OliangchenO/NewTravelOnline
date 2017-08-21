
/* Login */
$(function(){
	$('#js_login_select').delegate('span', 'click',function(){        //登录方式切换
		var that = $(this);
		that.addClass('cur').siblings().removeClass('cur').end().parents('#js_login_select').siblings('ul').eq($(this).index()).show().siblings('ul').hide();
	});
})


/* register */
$(function(){
    $('#js_show_pop').click(function(){
        $('#pop').show();

        var zzc = $('<div class="pop-mask"></div>');  //遮罩层
        $('body').append(zzc);
        
        $('#pop .pop-btn, #pop .close').click(function(){
            $('#pop').hide();
            $('.pop-mask').remove();
        })
    })
})

function sendStats(url){
    var n = "log_"+ (new Date()).getTime();					//倒计时
    var c = window[n] = new Image();
    c.onload = (c.onerror=function(){window[n] = null;});
    c.src = 'http://www.scyts.com' + url;
    c = null;  
}

var time = document.getElementById('time');
var btn = document.getElementById('Btn');
function count(){
    if( +time.innerHTML > 0 ){
        time.innerHTML = time.innerHTML - 1;
    }else{
        sendStats('gotoscyts');
        location.href = btn.href;
    }
}
setInterval(count , 1000);

btn.onclick = function(){
    sendStats('gotoscyts');
};









