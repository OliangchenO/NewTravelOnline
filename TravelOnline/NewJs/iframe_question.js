
(function(id,url,body_height){

var wenjuanGetData = function(e) {
    var data = e.data.toString().split(',');
    var iframeHeight = (data[0]*1)+45;
    var top = data[1]*1;
    if(body_height){
      wenjuanObj.style.height = body_height + 'px';
      wenjuanObj.style.display = 'block';
      wenjuanObj.style.overflowY = 'auto';
    }

    NewIframe.style.height = iframeHeight + 'px';

    if(top==-2){

        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
        return false;


    }if(top!==-1){

        if(body_height){
            wenjuanObj.scrollTop = top;
            var scrolltop = NewIframe.offsetTop;
            //document.body.scrollTop = scrolltop;
            //document.documentElement.scrollTop = scrolltop;
        }else{
            var scrolltop = NewIframe.offsetTop+top;
            //document.body.scrollTop = scrolltop;
            //document.documentElement.scrollTop = scrolltop;
        }
    }else{
        if(body_height){
            wenjuanObj.scrollTop = 0;
        }
        var scrolltop = NewIframe.offsetTop;
        //document.body.scrollTop = scrolltop;
        //document.documentElement.scrollTop = scrolltop;
    }
};

if (typeof window.addEventListener != 'undefined') {
window.addEventListener('message', wenjuanGetData, false);
} else if (typeof window.attachEvent != 'undefined') {
window.attachEvent('onmessage',wenjuanGetData);
}

var scriptObj = {};
var DomScript = document.getElementsByTagName("script");
var scriptURL = 'http://www.wenjuan.com/iframe/56de6a27a320fc30429c28b3/';
for (var i = 0;i<DomScript.length;i++){
  if (DomScript[i].src.indexOf(scriptURL)!=-1){
    scriptObj = DomScript[i];
    break;
  }
}

var wenjuanObj = document.createElement('wenjuan');
var parentObj = scriptObj.parentNode;
wenjuanObj.id = 'wj_survey';
scriptObj.parentNode.appendChild(wenjuanObj);
parentObj.insertBefore(wenjuanObj,scriptObj);

if(!id){id='WenjuanScript'}
var NewIframe = document.createElement("iframe");
    NewIframe.id = 'WJ_survey';
    NewIframe.name = 'WJ_survey';
    NewIframe.width = "100%";
    NewIframe.frameborder = "0";
    NewIframe.src = url;
    NewIframe.style.minHeight = "400px";
    NewIframe.style.border = '0px';
    NewIframe.style.overflow= 'hidden';
    NewIframe.allowTransparency= 'true';
    //NewIframe.style.background = 'url("http://www.wenjuan.com/static/img/survey/loader.gif") no-repeat center center';
    NewIframe.setAttribute("frameborder", "0", 0);
wenjuanObj.appendChild(NewIframe);
//NewIframe.onload=function(){NewIframe.style.background = 'none';}
})('WenjuanScript','http://www.wenjuan.com/s/qmmqY3Y/?iframe=1&', false); 