var divid = "objautodiv";
var spanindex = 0;

//单击其它地方关掉
var active = false;
var flag = false;
if (window.addEventListener) {//判断浏览器，flag=true为firfox，false为IE
    flag = true;
}

if (flag) {
    document.addEventListener("click", hide, false);
} else {
    document.attachEvent("onclick", hide);
}

function hide(e) {
    var evg = e.srcElement ? e.srcElement : e.target;
    if (active == false && $("#" + divid).show && evg.getAttribute("type") != "text") {
        $("#" + divid).hide();
    }
}
//点击结束

document.write("<div id=\"objautodiv\" class=\"DropListDiv\"></div>"); 

function show(objtxt, autoid,url,doit) {
    var code = event.keyCode;
    $.get(url, function (result) {
        var text = "";
        $(result).find("MisERP").each(function () {
            var name = $(this).find("listname").text();
            var name_id = $(this).find("listid").text();
            var ext1 = $(this).find("e1").text();
            var ext2 = $(this).find("e2").text();
            var ext3 = $(this).find("e3").text();
            text += "<span id=\"SelectAutoList\" sname=\"" + name + "\" tag=\"" + name_id + "\" tagid=\"" + autoid + "\" onclick='SelectList(\"" + objtxt.id + "\",this,\"" + doit + "\")' onmouseover='MoveIt(\"" + objtxt.id + "\",this)' onmouseout='MoveOut(\"" + objtxt.id + "\",this)'>" + name + ext1 + ext2 + ext3 + "</span>";
        })
        $("#objautodiv").html(text);
        SetDIV(objtxt);
    });
}

function SetDIV(objtxt)
{
    var arr=$("#objautodiv span").length;
    if(arr>0)
    {
        spanindex=0;
        $("#"+divid).show();
        $("#"+divid+" span").each(function(index){
            $(this).attr("pn", index);
        })
        $("#"+divid).css("left",$(objtxt).offset().left);
        $("#"+divid).css("top",$(objtxt).offset().top+$(objtxt).height()+5);
        $("#" + divid).width($(objtxt).width() + 25);
    }
    else
    {
        $("#" + divid).hide();
    }
}

function MoveIt(txtid,objspan)
{
    $("span[pn]").removeAttr("class");
    spanindex=$(objspan).attr("pn");
    js="$('span[pn="+spanindex+"]').css('backgroundColor','#3399FF')";
    $("span[pn=" + spanindex + "]").attr({ "class": "select" });
    active = true;
}

function MoveOut(txtid, objspan) {
    active = false;
}

function SelectList(txtid,objspan,callback)
{
    spanindex=$(objspan).attr("pn");
	var tagid=$("span[pn="+spanindex+"]").attr("tagid");
	if (tagid!="")
	{
		$("#"+tagid).val($("span[pn="+spanindex+"]").attr("tag"));
	}
	$("#"+txtid).val($("span[pn="+spanindex+"]").attr("sname"));
	if (callback!="") afterselet(callback);
	$("#" + divid).hide();
}
