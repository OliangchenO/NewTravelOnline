//广告图片的高度改这里，大图和小图的高度
var maxheigth = 299;
var minheigth = 50;

//宽屏大图bigpic 和 小图smlpic
var KP_bigpic = "/Images/topad/909/1170_1.jpg";
var KP_smlpic = "/Images/topad/909/1170_2.jpg";

//窄屏大图bigpic 和 小图smlpic
var ZP_bigpic = "/Images/topad/909/937_1.jpg";
var ZP_smlpic = "/Images/topad/909/937_2.jpg";

//图片链接地址
var AD_url = "http://www.scyts.com/ad/mariner909/";


if (screen.width >= 1280)
{ document.write("<div id=\"MyMoveAd\" style=\"height:50px;text-align:center;overflow:hidden;\"><A href=\"" + AD_url + "\" target=_blank><IMG id=\"MyMoveAdPic\" src=\"" + KP_smlpic + "\" border=0></A></div>"); }
else { document.write("<div id=\"MyMoveAd\" style=\"height:50px;text-align:center;overflow:hidden;\"><A href=\"" + AD_url + "\" target=_blank><IMG id=\"MyMoveAdPic\" src=\"" + ZP_smlpic + "\" border=0></A></div>"); }


var intervalId = null;
var viewIndex = readCookie("RushMoveAd") || 0;

function slideAd(id, nStayTime, sState, nMaxHth, nMinHth) {
    this.stayTime = nStayTime * 1000 || 4000;
    this.maxHeigth = nMaxHth || maxheigth;
    this.minHeigth = nMinHth || minheigth;
    this.state = sState || "down";
    var obj = document.getElementById(id);
    if (intervalId != null) window.clearInterval(intervalId);
    function openBox() {
        var h = obj.offsetHeight;
        obj.style.height = ((this.state == "down") ? (h + 4) : (h - 4)) + "px";
        if (obj.offsetHeight > this.maxHeigth) {
            window.clearInterval(intervalId);
            intervalId = window.setInterval(closeBox, this.stayTime);
        }
        if (obj.offsetHeight < this.minHeigth) {
            window.clearInterval(intervalId);
            $("#MyMoveAd").height(minheigth);
            if (screen.width >= 1280) {
                $("#MyMoveAdPic").attr("src", KP_smlpic);
            }
            else {
                $("#MyMoveAdPic").attr("src", ZP_smlpic);
            }

           createCookie("RushMoveAd", "1", 1);
        }
    }
    function closeBox() {
        slideAd(id, this.stayTime, "up", nMaxHth, nMinHth);
    }
    intervalId = window.setInterval(openBox, 10);
}	

if (viewIndex == "0") {
    if (screen.width >= 1280) {
        $("#MyMoveAdPic").attr("src", KP_bigpic);
    }
    else {
        $("#MyMoveAdPic").attr("src", ZP_bigpic);
    }
    $("#MyMoveAd").height(maxheigth);
    slideAd('MyMoveAd', 4, "down");
}