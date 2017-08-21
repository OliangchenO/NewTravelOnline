//list_id
clstag_list = "";

//call uri
var calluri = "http://fairyservice.360buy.com/WebService.asmx/MarkEx?callback=?";

var loguri = "http://csc.360buy.com/log.ashx?type1=$type1$&type2=$type2$&data=$data$&callback=?";

//callback function
callback1 = function(data) {
	;
}

//log
log = function(type1, type2, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) {

	var data = '';
	for (i = 2; i < arguments.length; i++) {
		data = data + arguments[i] + '|||';
	}

	var url = loguri.replace(/\$type1\$/, escape(type1));
	url = url.replace(/\$type2\$/, escape(type2));
	url = url.replace(/\$data\$/, escape(data));

	$.getJSON(
            url,
            callback1
        );
}

//mark
mark = function(sku, type) {

	$.getJSON(
            calluri,
            { sku: sku, type: type },
            callback1
        );

	log(1, type, sku);
}

//页面点击截获
document.onclick = function funClick(e) {

	e = e || event;
	var tag = e.srcElement || e.target;
	var clstag = $(tag).attr('clstag');

	while (!clstag) {

		tag = tag.parentNode;
		
		if (!tag) {
			break;
		}

		if (tag.nodeName == 'BODY') {
			break;
		}
		clstag = $(tag).attr('clstag');
	}

	if (clstag) {
		var temp = clstag.split('|');
		var page = temp[0];
		var key = temp[1];
		var type1 = temp[2];
		var type2 = temp[3];

		switch (key) {
			case "keycount": 	//所有keycount的记录
				log(type1, type2);
				break;
		}
	}

}
/////////////////////////////////////////////////////////////////////
//各个推荐的下单统计记录
function loginfo(rtype) {
    var CartWare = getCookie("reWids" + rtype)
    var strs = CartWare.split(',');
    var AllRecommendCookies = "";
    var AllPrice = 0;
    
    for (var i = 0; i < strs.length; i++) {
        strs[i] = String(strs[i]);   
         
        if (strs[i].indexOf("#") > 0) {
            var price = strs[i].split('#');
            if (SucInfo_OrderDetail.indexOf(price[0]) >= 0) {
            
               if (price.length > 1) {
                    log('4', 'R' + rtype, SucInfo_OrderId, SucInfo_OrderType, SucInfo_OrderDetail);
                    AllPrice = Number(AllPrice) + Number(price[1]);                    
                    AllRecommendCookies += price[0] + ",";
                }
                
            }         
            
        }
        else {
            if (SucInfo_OrderDetail.indexOf(strs[i]) >= 0) {
                log('4', 'R' + rtype, SucInfo_OrderId, SucInfo_OrderType, SucInfo_OrderDetail);
                AllRecommendCookies += strs[i] + ",";  
            }
        }
       
    }
    if (AllRecommendCookies.length > 0) {
        log('4', '1', SucInfo_OrderId, SucInfo_OrderType, SucInfo_OrderDetail, AllRecommendCookies, AllPrice);
    }
    
}


//页面载入
function funLoad() {
    var locationpathname = this.location.pathname.toLowerCase();
	//下单结果页的记录
	if (locationpathname.indexOf('succeed_', 0) > 0)
	{
	    var RecommendStr = "1:2:3:4:5:1a:1b:BR1:BR2:BR3:BR4:BR5:DDR:GR1:GR2:GR3:GR4:VR1:VR2:VR3:VR4:VR5:NR";
	    var RecommendStrSplit = RecommendStr.split(":");
	    for (var i = 0; i < RecommendStrSplit.length; i++) {
	        if (getCookie("reWids" + RecommendStrSplit[i]) != null && getCookie("reWids" + RecommendStrSplit[i]) != "") {
	            loginfo(RecommendStrSplit[i]);
	        }
	    }		
		
	}

	if (locationpathname.indexOf('shoppingcart', 0) > 0) {
		log('R2&Page', 'Show');
	}
	if (locationpathname.indexOf('user_home', 0) > 0) {
		log('R3&Page', 'Show');
	}
	if (locationpathname.indexOf('initcart.aspx', 0) > 0) {
		log('R4R5&Page', 'Show');
	}
	if (locationpathname.indexOf('orderlist.aspx', 0) > 0) {
		log('DDR&Page', 'Show');
	}

}

//Cookies操作
//---------------------------------------------------------------
function setCookie(name, value, date)//两个参数，一个是cookie的名子，一个是值
{
	var Days = date;
	var exp = new Date();    //new Date("December 31, 9998");
	exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
	document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/;domain=.360buy.com";
}
function getCookie(name)//读取cookies函数        
{
	var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
	if (arr != null) return unescape(arr[2]); return null;

}
//---------------------------------------------------------------

funLoad();

//// JScript 文件
document.write('<script src=http://wl.360buy.com/wl.aspx?url=' + escape(document.location) + '&title=' + escape(document.title));
document.write('></script>');


