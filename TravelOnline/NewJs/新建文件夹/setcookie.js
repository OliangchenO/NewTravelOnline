//$.cookie("pageurl", location.href, { expires: 30, path: '/' });

//记录最近浏览过的商品
var cookieName = "NewHistoryCookies";   //cookie名称
var nid;                          //最新访问的商品ID
var N = 5;                        //设置cookie保存的浏览记录的条数

function HistoryRecord() {
    var historyp;
    nid = $("#TB_LineId").val();
    if (nid == null || nid == "") {
        return;
    }
    nid += "|" + $("#TB_LineName").val();
    nid += "|" + $("#TB_Price").val();
    nid += "|" + $("#TB_pic").val();
    nid += "|" + $("#TB_Tag").val(); ; //跟团游，自由行等

    if ($.cookie(cookieName) == null) //cookie 不存在
    {
        $.cookie(cookieName, nid, { expires: 30, path: '/' });
        return;
    }
    else //cookies已经存在
    {
        historyp = $.cookie(cookieName);
    };

    var pArray = historyp.split(',');
    historyp = nid;
    //判断是该商品编号是否存在于最近访问的记录里面
    var count = 0;
    for (var i = 0; i < pArray.length; i++) {
        if (pArray[i] != nid) {
            historyp = historyp + "," + pArray[i];
            count++;
            if (count == N - 1) {
                break;
            }
        }
    }
    $.cookie(cookieName, historyp, { expires: 30, path: '/' });
}

//获取最近浏览过的商品
function BindHistory() {
    var historyp = "";
    if ($.cookie(cookieName) != null) {
        historyp = $.cookie(cookieName);
    }

    if (historyp == null && historyp == "") {
        return;
    }
    else {
        var prdIDs = [];
        var CookieGoods = "";
        var strtemp = "";
        var str = "<div class=\"record\">" +
	                "<div class=\"record-img\">" +
		                "<a href=\"/line.html?id={0}\" title=\"{1}\" target=\"_blank\"><img style='width:231px;height:110px' src=\"{3}\" alt=\"{1}\" onerror=\"this.src='/Images/none.gif'\"/></a>" +
	                "</div>" +
	                "<div class=\"record-tit\"><a href=\"/line.html?id={0}\" title=\"{1}\" target=\"_blank\">{1}</a></div>" +
	                "<div class=\"record-con\">" +
		                "{4}" +
		                "<p><span>¥</span>{2}<i>起</i></p>" +
	                "</div>" +
                "</div>" +
                "<div class=\"division\"></div>"
        var pArray = historyp.split(',');
        for (var i = 0; i < pArray.length; i++) {
            if (pArray[i] != "") {
                prdIDs = pArray[i].split('|');
                strtemp = str;
                CookieGoods += String.format(strtemp, prdIDs[0], prdIDs[1], prdIDs[2], prdIDs[3], prdIDs[4]);
             }
        }

        $("#CookieView").html(CookieGoods);
    }
}

String.format = function() {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for ( var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
};