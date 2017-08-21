/*getCommentSummary*/
var pshowclub = {
    getCommentSummary: function () {
        var getCommentUrl = "http://club.360buy.com/ProductPageService.aspx?method=GetCommentSummaryBySkuId&referenceId={0}&callback=?".replace("{0}", $("div[id^='star']").attr("id").replace("star", ""));
        $.getJSON(getCommentUrl, function (result) {
            if (!result) return;
            var domobj = $("#star" + result.SkuId);
            if (!domobj.html()) {
                domobj = $("div[id^='star']");
            }
            domobj.html("<div class=\"star sa" + result.AverageScore + "\"></div><a href=\"http://www.360buy.com/product/" + result.SkuId + ".html#comment\" class=\"num-comment\">(����" + result.CommentCount + "������)</a>");
        });
    }
};
pshowclub.getCommentSummary(); /*fitting*/
$(function () {
    $("#fitting-list").jdMarquee({
        deriction: "left",
        width: (screen.width >= 1280) ? 930 : 710,
        height: 180,
        step: 2,
        speed: 4,
        delay: 10,
        control: true,
        _front: "#fitting-right",
        _back: "#fitting-left"
    })
}); /*suit*/
asyncScript(function () {
    var pshowskuid = $("#name").attr("pshowskuid");
    $.getJSONP("http://price.360buy.com/PromotionData.aspx?pid=" + pshowskuid + "&type=pack", getSuitService);
});

function checkIndex(obj) {
    for (var i = 0; i < obj.length; i++) {
        if (document.getElementById(obj[i]).getAttribute("load")) {
            return i;
        }
    }
}

function setSuitWidth(url, object, n) {
    var element = object.find(".suits").eq(0);
    var x = element.find("li").length;
    var z = (screen.width >= 1280) ? 158 : 140;
    var w = (screen.width >= 1280) ? 632 : 420;
    element.css({
        "overflow-x": (x * z > w) ? "scroll" : "hidden"
    });
    element.find("ul").eq(0).css({
        "width": x * z
    });
    object.find("img").each(function () {
        var src2 = $(this).attr("src2");
        $(this).attr("src", src2);
        $(this).removeAttr("src2")
    });
}

function getSuitService(json) {
    if (!json) {
        return;
    }
    var data = json.data;
    if (data == null || data == "") {
        return;
    }
    var html = "<ul class='tab-sub'>";
    var price = 0,
        d1, src;
    var html1 = "<div class='p-img'><a href='http://www.360buy.com/product/{0}.html' target='_blank'><img {src}='{domain}n4/{1}' width='100' height='100' /></a></div><div class='p-name'><a href='http://www.360buy.com/product/{0}.html' target='_blank'>{2}</a></div>";
    for (var i = 0; i < data.length; i++) {
        html += "<li class='" + ((i == 0) ? "fore curr" : "") + "'>�Ż���װ" + (i + 1) + "</li>";
    }
    html += "</ul>";
    $.each(data, function (i) {
        src = (i == 0) ? "src" : "src2";
        html2 = html1.replace("{src}", src);
        price = data[0].Discount;
        d1 = getRandomDomain(data[i].MainSkuId);
        html += "<div class='" + ((i == 0) ? "tabcon-sub" : "tabcon-sub hide") + "'>";
        html += "<div class='master'>";
        html += html2.replace(/\{0\}/g, data[i].MainSkuId).replace(/\{1\}/g, data[i].MainSkuPicUrl).replace(/\{2\}/g, data[i].MainSkuName).replace("{domain}", d1);
        html += "<div class='icon-add'></div>";
        html += "</div>";
        html += "<div class='suits' id='suits-" + i + "'><ul class='list-h'>";
        var s2 = data[i].ProductList,
            d2;
        for (var j = 0; j < s2.length; j++) {
            d2 = getRandomDomain(s2[j].SkuId);
            html += "<li>";
            html += html2.replace(/\{0\}/g, s2[j].SkuId).replace(/\{1\}/g, s2[j].SkuPicUrl).replace(/\{2\}/g, s2[j].SkuName).replace("{domain}", d2);
            html += "</li>";
        };
        html += "</ul></div><div class='i-suit'><b></b></div>";
        html += "<div class='infos'>";
        html += "<div class='p-name'><a href='http://www.360buy.com/suite/" + data[i].PackId + "-" + data[i].MainSkuId + ".html' target='_blank'>" + data[i].PackName + "</a></div>";
        html += "<div class='p-price'>��װ�ۣ�<strong>" + data[i].PackPrice + "</strong></div>";
        html += "<div class='p-market'>ԭ�ۣ�<del>" + data[i].PackListPrice + "</del></div>";
        html += "<div class='p-saving'>������ʡ��" + data[i].Discount + "Ԫ</div>";
        html += "<div class='btns'><a href='http://jd2008.360buy.com/purchase/initcart.aspx?pId=" + data[i].PackId + "&pCount=1&pType=2&mainSkuId=" + data[i].MainSkuId + "' class='btn-buy'>������װ</a></div>";
        html += "</div>";
        html += "</div>";
    });
    var index = checkIndex(["tab-fitting", "tab-group", "placeholder-2a"]);
    var flag = $("#placeholder-1").attr("flag") && ($("#placeholder-1").attr("flag") == 0);
    if (!flag) {
        //���Ʒ��
        html = "<div class='mt'><h2>�Ż���װ</h2></div><div class='mc'>" + html + "</div>";
        $("#placeholder-1").addClass("recommend").html(html).show().jdTab({
            event: "click",
            tab: ".tab-sub",
            content: ".tabcon-sub"
        }, setSuitWidth);
        setSuitWidth("", $("#placeholder-1").find(".tabcon-sub").eq(0));
    } else {
        //ȫƷ��
        if (typeof index == "number") {
            //�����ȼ���
            $("#placeholder-2a").append("�Ż���װ").show();
            $("#placeholder-2b").html(html).addClass("suit");
            $("#recommend").jdTab({
                event: "click",
                index: index
            });
        } else {
            //�����ȼ���
            $("#placeholder-2a").append("�Ż���װ").addClass("curr").attr("load", "true").show();
            $("#placeholder-2b").html(html).addClass("suit").show();
            $("#recommend").show().jdTab({
                event: "click",
                index: 2
            });
        }
        setSuitWidth("", $("#placeholder-2b").find(".tabcon-sub").eq(0));
        $("#placeholder-2b").jdTab({
            event: "click",
            tab: ".tab-sub",
            content: ".tabcon-sub"
        }, setSuitWidth)
    }
} /*gift*/
var GiftCardWid = null;

function getGift(wid) {
    $.jdThickBox({
        type: "json",
        width: 600,
        height: 530,
        title: "ѡ��ؿ���ʽ",
        source: "http://www.360buy.com/giftAjax.aspx?getGift=get&wid=" + wid + "&jsoncallback=?"
    }, function (url, ele) {
        $.getJSON(url, function (json) {
            if (json.info != "") {
                ele.html(json.info)
            }
        })
    })
}
function setGift() {
    var array = document.getElementById("changeCard").getElementsByTagName("input");
    var wid;
    for (var i = 0; i < array.length; i++) {
        if (array[i].type == 'radio' && array[i].checked) {
            wid = array[i].value;
            break
        }
    }
    var to = document.getElementById("To").value;
    var form = document.getElementById("From").value;
    var msg = document.getElementById("Message").value;
    to = to.replace(/<[\s\S]*?>/, "");
    form = form.replace(/<[\s\S]*?>/, "");
    msg = msg.replace(/<[\s\S]*?>/, "");
    GiftCardWid = wid;
    if (wid == "" || wid == null) {
        alert("��ѡ��ؿ���ʽ��");
        return false
    }
    if (to == "" || to == null) {
        alert("����д�ؿ��ռ��ˣ�");
        return false
    }
    if (msg == "" || msg == null) {
        alert("����д�ؿ����ݣ�");
        return false
    }
    if (msg.length > 80) {
        alert("�ؿ����ݳ��Ȳ��ܳ���80��Ŀǰ�Ѿ�����" + msg.length);
        return false
    }
    if (form == "" || form == null) {
        alert("����д�ؿ������ˣ�");
        return false
    }
    $.getJSON("http://www.360buy.com/giftAjax.aspx?getGift=set&wid=" + wid + "&To=" + to + "&From=" + form + "&msg=" + msg + "&jsoncallback=?", function (json) {
        if (json.info != "") {
            if (json.info == "true") {
                if (CardUrl != null) {
                    window.location.href = CardUrl
                } else if (CardFun != null) {
                    eval(CardFun)
                }
            } else {
                alert("������Ϣʧ�ܣ�")
            }
        }
    })
}
var CardUrl = null;
var CardFun = null;

function Card() {
    this.Wid = 0;
    this.Url = '';
    this.Fun = '';
    this.Show = function () {
        CardUrl = this.Url;
        CardFun = this.Fun;
        getGift(this.Wid)
    }
} /*forminicart_fq 20100601*/
var isIe = (window.ActiveXObject) ? true : false;
var display4 = false;

function showTip100(proobj) {
    var TipDivW = $(proobj).width();
    var TipDivH = $(proobj).height();
    var TipDiv = $("<div id='c04tip' style='z-index:20000;position:absolute;width:" + eval(TipDivW + 5) + "px;height:" + eval(TipDivH + 5) + "px;'><div style='position:absolute;margin:5px 0 0 5px;width:" + TipDivW + "px;height:" + TipDivH + "px;background:#BCBEC0;z-index:20001;'></div></div>")
    if (display4 == false) {
        if (fq_returnData) {
            fq_formatData(fq_returnData);
        }
        TipDiv.append($(proobj));
        $(document.body).prepend(TipDiv);
        $(proobj).show();
        display4 = true;
    } else {
        $("#c04tip").show();
    }
    $("#c04tip").css({
        top: parseInt(document.documentElement.scrollTop + (document.documentElement.clientHeight - $("#c04tip").height()) / 2) + "px",
        left: (document.documentElement.clientWidth - $("#c04tip").width()) / 2 + "px"
    })
    $("#Tip_apply,#Tip_continue,.Tip_Close").click(function () {
        $("#c04tip").fadeOut();
    });
}
var fq_serverSite = "http://jd2008.360buy.com/purchase/";
var fq_serverUrl = "ajaxServer/ForMiniCart_fq.aspx";
var fq_btnPanel = "fqPanel";
var fq_skuId = "";
var fq_TipHtml = "";
var isFqOpen = true;
var isYbOpen = true;
var fq_returnData = null;
if (isFqOpen) {
    if (!isIe) {
        try {
            fq_init();
        } catch (e) {
            document.addEventListener('DOMContentLoaded', fq_init, null);
        }
    } else {
        fq_init();
    }
}

function fq_init() {
    if (document.getElementById('pShowSkuId')) {
        fq_skuId = $("#pShowSkuId").text();
        if (fq_skuId) fq_showFq(fq_skuId);
    }
}
if (isYbOpen) {
    if (!isIe) {
        try {
            yb_init();
        } catch (e) {
            document.addEventListener('DOMContentLoaded', yb_init, null);
        }
    } else {
        yb_init();
    }
}

function yb_init() {
    if (document.getElementById('pShowSkuId')) {
        fq_skuId = $("#pShowSkuId").text();
        if (fq_skuId) yb_showYb(fq_skuId);
    }
}

function fq_showFq(skuId) {
    var js = document.createElement('script');
    js.type = 'text/javascript';
    js.src = fq_serverSite + fq_serverUrl + '?roid=' + Math.random() + '&action=getFqInfo&id=' + skuId;
    document.getElementsByTagName('head')[0].appendChild(js);
}

function yb_showYb(skuId) {
    var js = document.createElement('script');
    js.type = 'text/javascript';
    js.src = fq_serverSite + fq_serverUrl + '?roid=' + Math.random() + '&action=getYBInfo&id=' + skuId;
    document.getElementsByTagName('head')[0].appendChild(js);
}

function fq_showFq_html(obj) {
    if (obj != null) {
        if (obj.json.length == 0) {
            return;
        } else {
            if (obj.json.length == 0) return;
            if (obj.json[0].error != null) {
                return;
            }
            document.getElementById(fq_btnPanel).innerHTML = "<input type=\"button\" value=\"���ڸ���\" class=\"btn-divide\" onclick=\"showTip100('#Fqfk_Tip');\"/>";
            fq_returnData = obj;
        }
    }
}

function fq_formatData(obj) {
    var t = document.getElementById('Fqfk_Tip');
    t.style.width = "500px";
    var str = "";
    str += "<div class='Tip_Title'><em><img src='" + fq_serverSite + "skin/images/tip_close.jpg' class='Tip_Close'/></em>���ڸ���</div>";
    str += "<div class='Tip_Content'><div style='text-align:left;'>����Ʒ֧�������������ÿ����ڸ��</div>";
    str += "<div>";
    str += "<table width='100%' cellpadding='0' cellspacing='1' bgcolor='#C0C0C0'>";
    str += "<tr style='background:#EBF4FB'><th>����</th><th>3��</th><th>6��</th><th>12��</th><th>24��</th></tr>";
    for (var i = 0; i < obj.json.length; i++) {
        str += "<tr style='background:#fff'><td><img src='" + fq_serverSite + "skin/images/ins_bank/fqfk_bank_" + obj.json[i].Id + ".jpg' border='0' /></td>";
        str += "<td>" + ((obj.json[i].p3 != "-") ? "<strong>" + obj.json[i].p3 + "Ԫ</strong>��3��" : "-") + "</td>";
        str += "<td>" + ((obj.json[i].p6 != "-") ? "<strong>" + obj.json[i].p6 + "Ԫ</strong>��6��" : "-") + "</td>";
        str += "<td>" + ((obj.json[i].p12 != "-") ? "<strong>" + obj.json[i].p12 + "Ԫ</strong>��12��" : "-") + "</td>";
        str += "<td>" + ((obj.json[i].p24 != "-") ? "<strong>" + obj.json[i].p24 + "Ԫ</strong>��24��" : "-") + "</td></tr>";
    }
    str += '</table>';
    str += '</div>';
    str += "<div style='text-align:left;padding:7px 0 0 3px'>�����������ÿ����߷��ڣ������˹���ˣ����Ը����١������ĵķ���֧����ʽ��</div>";
    str += "</div>";
    str += "<div style='padding:9px;padding-bottom:13px'><img src='" + fq_serverSite + "skin/images/fqflow.gif' /></div>";
    str += "<div class='Tip_Submit'><a  style='border:none' href='" + fq_serverSite + "ShoppingCart_fqInit.aspx?skuId=" + fq_skuId + "&skuCount=1' id='Tip_apply'><img src='" + fq_serverSite + "skin/images/fqstart.gif' /></a><a class='link_1' style='margin-right:210px;border:none;color:#000' href='http://www.360buy.com/help/dividedpay.aspx' target='_blank'>�鿴���ڸ������</a></div>";
    t.innerHTML = str;
}
var buyBtnLink;

function fq_showYb_html(obj) {
    var sDiv = $("#placeholder-1");
    var strHtml = "";
    if (sDiv.length != 0 || sDiv != null) {
        if (fq_skuId < 1000000000) {
            strHtml += "<div class='i-mc'><b>�����ؿ�����</b> - ���񸽺ؿ������⾡���</div>";
            strHtml += "<ul>";
            strHtml += "<li><div class='content'>�������ؿ�����������д�ؿ����ݣ�</div><a href='#none' class='btn-buy' onclick=\"Card_buy();\">����</a></li>";
            strHtml += "</ul>";
        }
        if (obj != null && obj.json.length > 0 && obj.json[0].error == null) {
            var strYb = "<ul>";
            for (var i = 0; i < obj.json.length; i++) {
                if (obj.json[i].Type != "0") continue;
                strYb += "<li><div class='content'>��<a target='_blank' href='http://www.360buy.com/help/" + obj.json[i].HName + "'>" + obj.json[i].Name + "</a>";
                strYb += "<font color='red' style='margin-right:12px'>" + obj.json[i].Message + "</font><strong style='color:red'>��" + obj.json[i].Price + "</strong>&nbsp;&nbsp;";
                strYb += "<a target='_blank' href='http://www.360buy.com/help/" + obj.json[i].HName + "'>[��ϸ˵��]</a></div>";
                strYb += "<a href='" + fq_serverSite + "InitCart.aspx?pid=" + fq_skuId + "&pcount=1&ptype=1&ybId=" + obj.json[i].Id + "' class='btn-buy'>����</a></li>";
            }
            strYb += "</ul>";
            if (strYb != "<ul></ul>") {
                strHtml += "<div class='i-mc'><b>�����ӱ�����</b> - ����ʱ����������޷�Χ���㡢�������Ᵽ�ϣ�ʹ�ø����ģ�</div>";
                strHtml += strYb;
            }
            var strQx = "<ul>";
            for (var i = 0; i < obj.json.length; i++) {
                if (obj.json[i].Type != "1") continue;
                strQx += "<li><div class='content'>��<a href='http://www.360buy.com/product/" + obj.json[i].Id + ".html' target='_blank'>" + obj.json[i].Name + "</a><font color='red' style='margin-right:12px'>" + obj.json[i].Message + "</font><strong style='color:red'>��" + obj.json[i].Price + "</strong></div>";
                strQx += "<a href='" + fq_serverSite + "InitCart.aspx?pid=" + obj.json[i].Id + "&pcount=1&ptype=1' class='btn-buy'>����</a>";
                strQx += "&nbsp;&nbsp;<a href='http://www.360buy.com/help/special/index.aspx' target='_blank'>[�������]</a>";
                strQx += "</li>";
            }
            strQx += "</ul>";
            if (strQx != "<ul></ul>") {
                strHtml += "<div class='i-mc'><b>��ϴ����</b> - ��ϴ�ҵ粻�ٳ���������</div>";
                strHtml += strQx;
            }
            strDiy = "<ul>";
            for (var i = 0; i < obj.json.length; i++) {
                if (obj.json[i].Type != "2") continue;
                strDiy += "<li><div class='content'>��<a href='http://www.360buy.com/product/" + obj.json[i].Id + ".html' target='_blank'>" + obj.json[i].Name + "</a><font color='red' style='margin-right:12px'>" + obj.json[i].Message + "</font><strong style='color:red'>��" + obj.json[i].Price + "</strong></div>";
                strDiy += "<a href='" + fq_serverSite + "InitCart.aspx?pid=" + obj.json[i].Id + "&pcount=1&ptype=1' class='btn-buy'>����</a>";
                strDiy += "&nbsp;&nbsp;<a href='http://www.360buy.com/help/special/index.aspx' target='_blank'>[�������]</a>";
                strDiy += "</li>";
            }
            strDiy += "</ul>";
            if (strDiy != "<ul></ul>") {
                strHtml += "<div class='i-mc'><b>����DIY����</b> - �������Ű���DIY��װ�������ɣ�</div>";
                strHtml += strDiy;
            }
        }
        if (strHtml != "") {
            var index = checkIndex(["tab-fitting", "tab-group", "placeholder-2a"]);
            var flag = $("#placeholder-1").attr("flag") && ($("#placeholder-1").attr("flag") == 0);
            if (flag) {
                strHtml = "<div class='mt'><h2>�Ƽ�����</h2></div><div class='mc jdservice'>" + strHtml + "</div>";
                sDiv.html(strHtml).show();
            } else {
                if (typeof index == "number") {
                    //�����ȼ���
                    $("#placeholder-2a").append("�Ƽ�����").show();
                    $("#placeholder-2b").addClass("jdservice").html(strHtml);
                    $("#recommend").jdTab({
                        event: "click",
                        index: index
                    });
                } else {
                    //�����ȼ���
                    $("#placeholder-2a").append("�Ƽ�����").addClass("curr").attr("load", "true").show();
                    $("#placeholder-2b").addClass("jdservice").html(strHtml).show();
                    $("#recommend").show().jdTab({
                        event: "click",
                        index: 2
                    });
                }
            }
        }
    }
}

function yb_sel(obj) {
    var chks = document.getElementsByName('ybChk');
    for (var i = 0; i < chks.length; i++) {
        if (obj != chks[i]) {
            chks[i].checked = false;
        }
    }
    var cartLink = document.getElementById('gouwuche').parentNode;
    if (obj.checked) {
        cartLink.href = buyBtnLink + "&ybId=" + obj.value;
    } else {
        cartLink.href = buyBtnLink;
    }
}

function Card_buy() {
    var card = new Card();
    card.Fun = "Card_buy_finish('" + fq_skuId + "');";
    card.Url = null;
    card.Show();
}

function Card_buy_finish(mainSkuId) {
    this.location = 'http://jd2008.360buy.com/purchase/InitCart.aspx?pid=' + mainSkuId + '&pcount=1&ptype=1&addId=' + GiftCardWid;
} /*20110303*/


$.extend(jdModelCallCenter, {
    usefulComment: function (object) {
        $.login({
            modal: true,
            complete: function (result) {
                if (result.IsAuthenticated) {
                    var commentId = object.parent().attr("id");
                    var isUseful = object.attr("name") == "agree";
                    if (object.attr("enabled") != "true") {
                        $.getJSON("http://club.360buy.com/clubservice.aspx?callback=?", {
                            method: "VoteForComment",
                            commentId: commentId,
                            isUseful: isUseful
                        }, function (result) {
                            object.attr("enabled", "true");
                            if (result.Result) {
                                var count = parseInt(object.attr("title")) + 1;
                                object.attr("title", count);
                                if (isUseful) {
                                    object.html("����({0})".format(count));
                                } else {
                                    object.html("û��({0})".format(count));
                                }
                            }
                        });
                    }
                }
            }
        });
        mark(location.href.match(/(\d+).html/)[1], 5);
    }
});
$(".btn-agree,.btn-oppose").livequery("click", function () {
    var current = $(this);
    $.extend(jdModelCallCenter.settings, {
        object: current,
        fn: function () {
            jdModelCallCenter.usefulComment(this.object);
        }
    });
    jdModelCallCenter.settings.fn();
}); /*20100809*/

function GetSnsAvatar() {
    var strPin = "";
    var pins = $("#comment").children("div:visible").find("div.u-name a");
    if (pins.length > 0) {
        for (var i = 0; i < pins.length; i++) {
            strPin += pins[i].innerHTML + ",";
        }
        strPin = strPin.substring(0, strPin.lastIndexOf(','));
        $.getJSONP("http://i.360buy.com/user/upload/img/show/" + encodeURI(escape(strPin)) + ".html?callback=GetAvatarCallback", GetAvatarCallback);
    }
}

function GetAvatarCallback(data) {
    if (data) {
        for (var j = 0; j < data.info.length; j++) {
            if (data.info[j].imgurl.indexOf('no-img') <= 0) $("#comment div:visible a:contains('" + data.info[j].pin + "')").parent().prev().find('img').attr('src', data.info[j].imgurl + "_sma_.jpg");
        }
    }
} /*20100908 ��Ʒ���۷�ҳ*/
$.fn.pagination.options0 = {
    items_per_page: 5,
    num_display_entries: 5,
    current_page: 0,
    num_edge_entries: 2,
    link_to: "#comment",
    prev_text: "��һҳ",
    next_text: "��һҳ",
    ellipse_text: "...",
    prev_show_always: false,
    next_show_always: false,
    callback: pageproductSelected0
};

function pageproductSelected0(page_id, jq) {
    $.fn.pagination.options0.current_page = page_id;
    refreshComments("Product", "0");
}
$.fn.pagination.options1 = {
    items_per_page: 5,
    num_display_entries: 5,
    current_page: 0,
    num_edge_entries: 2,
    link_to: "#comment",
    prev_text: "��һҳ",
    next_text: "��һҳ",
    ellipse_text: "...",
    prev_show_always: false,
    next_show_always: false,
    callback: pageproductSelected1
};

function pageproductSelected1(page_id, jq) {
    $.fn.pagination.options1.current_page = page_id;
    refreshComments("Product", "1");
}
$.fn.pagination.options3 = {
    items_per_page: 5,
    num_display_entries: 5,
    current_page: 0,
    num_edge_entries: 2,
    link_to: "#comment",
    prev_text: "��һҳ",
    next_text: "��һҳ",
    ellipse_text: "...",
    prev_show_always: false,
    next_show_always: false,
    callback: pageproductSelected3
};

function pageproductSelected3(page_id, jq) {
    $.fn.pagination.options3.current_page = page_id;
    refreshComments("Product", "3");
}
$.fn.pagination.options5 = {
    items_per_page: 5,
    num_display_entries: 5,
    current_page: 0,
    num_edge_entries: 2,
    link_to: "#comment",
    prev_text: "��һҳ",
    next_text: "��һҳ",
    ellipse_text: "...",
    prev_show_always: false,
    next_show_always: false,
    callback: pageproductSelected5
};

function pageproductSelected5(page_id, jq) {
    $.fn.pagination.options5.current_page = page_id;
    refreshComments("Product", "5");
}

function getProductComments(result) {
    var pid = location.href.match(/(\d+).html/)[1];
    if (!result.CommentSummary) result.CommentSummary = 0;
    //$("div[id^=star]").html(result.GetCommentSummary.process(result));	
    $("#cnum0").text("(" + result.CommentSummary.CommentCount + ")");
    $("#cnum1").text("(" + result.CommentSummary.GoodCount + ")");
    $("#cnum2").text("(" + result.CommentSummary.GeneralCount + ")");
    $("#cnum3").text("(" + result.CommentSummary.PoorCount + ")");
    switch (result.Score) {
    case 0:
        $("#comment-0").html(result.GetProductComments.process(result));
        $("#commentsPage" + result.Score).pagination(result.CommentSummary.CommentCount, $.fn.pagination.options0);
        break;
    case 5:
        $("#comment-1").html(result.GetProductComments.process(result));
        $("#commentsPage" + result.Score).pagination(result.CommentSummary.GoodCount, $.fn.pagination.options5);
        break;
    case 3:
        $("#comment-2").html(result.GetProductComments.process(result));
        $("#commentsPage" + result.Score).pagination(result.CommentSummary.GeneralCount, $.fn.pagination.options3);
        break;
    case 1:
        $("#comment-3").html(result.GetProductComments.process(result));
        $("#commentsPage" + result.Score).pagination(result.CommentSummary.PoorCount, $.fn.pagination.options1);
        break;
    default:
        $("#comment-0").html(result.GetProductComments.process(result));
        $("#commentsPage" + result.Score).pagination(result.CommentSummary.CommentCount, $.fn.pagination.options0);
    }
    GetSnsAvatar();
} /*20110324*/

function getCommentSummaryWithComments(result) {
    if (result.ReferenceType == 0) {
        $("#discuss-0").html(result.Result);
    } else if (result.ReferenceType == 1) {
        $("#discuss-2").html(result.Result);
    } else if (result.ReferenceType == 2) {
        $("#discuss-3").html(result.Result);
    } else if (result.ReferenceType == 3) {
        $("#discuss-4").html(result.Result);
    } else if (result.ReferenceType == 4) {
        $("#discuss-1").html(result.Result);
    }
} /*consultation 0603*/
var consultationServiceUrl = "http://club.360buy.com/consultationservice.aspx?callback=?";
var referServiceUrl = "http://search.360buy.com/sayword?callback=?";
$("#consult-0").before("<div class='search'><div class='i-search1'><strong>��ѯǰ���������������ֿ�ݣ�</strong><input type='text' class='text' id='txbReferSearch' value='��������ѯ�ؼ���' onblur=\"if (this.value==''){this.value=this.defaultValue;}\" onfocus=\"if (this.value==this.defaultValue){this.value=''}\" /><input type='button' class='btn-search' id='btnReferSearch' value='����' /></div><div class='i-search2'><strong>��ܰ��ʾ:</strong>�򳧼Ҹ��Ĳ�Ʒ��װ�����ػ��߸������������û���κ���ǰ֪ͨ����ÿλ��ѯ�߹������������ʱ��Ȳ�ͬ��Ϊ�����»ظ�����������3������Ч���������ѽ����ο������ɴ˸����������������½⣬лл��</div></div><div class='result clearfix' id='consult-result' style='display:none'></div><div id='consult-resultlist' style='display:none;'></div>");
$.fn.pagination.options = {
    items_per_page: 8,
    num_display_entries: 5,
    current_page: 0,
    num_edge_entries: 0,
    link_to: "#consult",
    prev_text: "��һҳ",
    next_text: "��һҳ",
    ellipse_text: "...",
    prev_show_always: false,
    next_show_always: false,
    callback: pageSelected
};

function pageSelected(page_id, jq) {
    $.fn.pagination.options.current_page = page_id;
    refreshReferList();
}

function refreshReferList() {
    var productId = parseInt($.query.get("id"));
    if (isNaN(productId) || productId == 0) {
        var result = location.href.match(/(\d+)(.html)/);
        if (result != null) {
            productId = parseInt(result[1]);
        }
    }
    if (productId > 0) {
        $.getJSON(referServiceUrl, {
            wid: productId,
            keyword: encodeURI($("#txbReferSearch").val()),
            page: $.fn.pagination.options.current_page + 1,
            ps: $.fn.pagination.options.items_per_page
        }, function (result) {
            $("#consult .mc").hide();
            var list = result[0].list;
            var count = result[0].total >= 40 ? 40 : result[0].total;
            $("#consult-result").css("display", "block").html("<div class='fl'>��������<strong>" + count + "</strong>�������ѯ<span id='noneRefer' style='display:none;'>�����Ը���̵Ĺؼ��ʻ�����ؼ���</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href='#none' id='backConsultations'>����</a></div><div class='fr'><em>���������»ظ�����������3������Ч���������ѽ����ο���</em></div>");
            if (list.length == 0) {
                $("#noneRefer").show();
                $("#consult-resultlist").empty().hide();
            } else {
                $("#consult-resultlist").empty().show();
                for (var element in list) {
                    var d1 = list[element].sindate.substring(0, list[element].sindate.lastIndexOf(":"));
                    var d2 = list[element].sindate2.substring(0, list[element].sindate2.lastIndexOf(":"));
                    var s2 = list[element].sword2 ? list[element].sword2 : "";
                    $("#consult-resultlist").append("<div class='item'><div class='user'><spanc lass='u-name'>���ѣ�" + list[element].nickname + "</span><span class='date-ask'>" + d1 + "</span></div><dl class='ask'><dt><b></b>��Ѷ���ݣ�</dt><dd>" + list[element].sword + "</dd></dl><dl class='answer'><dt><b></b>�����ظ���</dt><dd>" + s2 + "</dd></dl><div class='useful' id='" + list[element].sid + "'>�������ǵĻظ���<a class='btn-pleased' href='#none' name='2'>����</a>(<span>" + list[element].zantong + "</span>)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a class='btn-unpleased' href='#none' name='2'>������</a>(<span>" + list[element].fd + "</span>)</div></div>");
                }
                $("#consult-resultlist .item:odd").addClass("odd");
                $("#consult-resultlist").append("<div class='clearfix'><div id='ReferPagenation' class='pagin fr' style='margin-top:10px;'></div></div>");
                $("#ReferPagenation").pagination(count, $.fn.pagination.options);
            }
        });
    }
}
$("#btnReferSearch").livequery("click", function () {
    $.fn.pagination.options.current_page = 0;
    refreshReferList();
});
$("#txbReferSearch").livequery("keydown", function (event) {
    if (event.keyCode == 13) {
        $("#btnReferSearch").click();
    }
});
$("#txbReferSearch").livequery("click", function () {
    this.value = "";
});
$("#backConsultations").livequery("click", function () {
    $("#consult .result").hide();
    $("#consult-resultlist").hide();
    $("#txbReferSearch").val("��������ѯ�ؼ���");
    var index = $("#consult .tab").eq(0).find("li").index($("#consult .tab").eq(0).find("li[class=curr]"));
    $("#consult .tabcon").eq(index).show();
});
$("#consultation").livequery("click", function () {
    $.login({
        returnUrl: $(this).attr("name"),
        complete: function (result) {
            if (result.IsAuthenticated) {
                location.href = this.returnUrl;
            }
        }
    });
});
$("#login").livequery("click", function () {
    $.login();
}); /*20100527*/

function refreshConsultations(pType) {
    var productId = parseInt($.query.get("id"));
    if (isNaN(productId) || productId == 0) {
        var productId = parseInt($.query.get("wid"));
        if (isNaN(productId) || productId == 0) {
            var result = location.href.match(/(\d+)(.html)/);
            if (result != null) {
                productId = parseInt(result[1]);
            }
        }
    }
    if (productId > 0) {
        $.getJSONP("http://club.360buy.com/consultationservice.aspx?callback=getConsultations&method=GetConsultations&productId=" + productId + "&pType=" + pType, getConsultations);
    }
} /*20100601*/
$(".btn-pleased,.btn-unpleased").livequery("click", function () {
    var current = $(this);
    $.login({
        complete: function (result) {
            if (result.IsAuthenticated != null && result.IsAuthenticated) {
                var productId = parseInt($.query.get("id"));
                if (isNaN(productId) || productId == 0) {
                    var result = location.href.match(/(\d+)(.html)/);
                    if (result != null) {
                        productId = parseInt(result[1]);
                    }
                }
                var consultationId = $(current).parent().attr("id");
                var score = parseInt($(current).attr("name"));
                if (productId > 0) {
                    $.getJSON(consultationServiceUrl, {
                        method: "VoteForConsultation",
                        productId: productId,
                        consultationId: consultationId,
                        score: score
                    }, function (result) {
                        if (result.Result) {
                            $(current).text("��ͶƱ");
                            $(current).next("span").text(parseInt($(current).next("span").text()) + 1);
                        } else {
                            $(current).text("��ͶƱ");
                        }
                    });
                }
            }
        }
    });
    mark(location.href.match(/(\d+).html/)[1], 5);
}); /*btn-comment*/
$(".btn-comment").livequery("click", function () {
    var url = $(this).attr("href");
    $.extend(jdModelCallCenter.settings, {
        clstag1: 0,
        clstag2: 0,
        fn: function () {
            jdModelCallCenter.autoLocation(url);
        }
    });
    jdModelCallCenter.settings.fn();
    return false;
}); /*20110117*/

function getConsultations(result) {
    if (result.GetConsultations != null) {
        switch (result.SearchParameter.PType) {
        case 0:
            $("#consult-0").html(result.GetConsultations.process(result));
            break;
        case 2:
            $("#consult-1").html(result.GetConsultations.process(result));
            break;
        case 3:
            $("#consult-2").html(result.GetConsultations.process(result));
            break;
        case 4:
            $("#consult-3").html(result.GetConsultations.process(result));
            break;
        case 5:
            $("#consult-4").html(result.GetConsultations.process(result));
            break;
        default:
            $("#consult-0").html(result.GetConsultations.process(result));
            break;
        }
    }
} /*jdVideo*/
jdVideo.prototype = {
    obj_360: $("#view360"),
    obj_video: $("#video"),
    obj_v2: $("#btn-v2"),
    obj_sub_360: "btn-view360",
    obj_sub_video: "btn-video",
    obj_sub_v2: "btn-v2",
    _$: function (str, type) {
        switch (type) {
        case "id":
            return $("#" + str);
        case "class":
            return $("." + str);
        }
    },
    open360: function () {
        var object = this.obj_360,
            url = this.url_360;
        if (object.html() == "") {
            var str = [];
            str.push("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab' width='350' height='350'><param name='movie' value='");
            str.push(url);
            str.push("'><param name='quality' value='high'><param name='wmode' value='transparent'><embed src='");
            str.push(url);
            str.push("' quality='high' width='350' height='350' type='application/x-shockwave-flash' PLUGINSPAGE='http://www.macromedia.com/go/getflashplayer' wmode='transparent'></embed></object>");
            str = str.join("");
            object.html(str).show();
        } else {
            object.show();
        }
        this.obj_v2.hide();
    },
    openVideo: function () {
        var object = this.obj_video,
            url = this.url_video;
        if (object.html() == "") {
            var str = [];
            var player = "http://video10.360buyimg.com/pl.swf";
            str.push("<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' width='350' height='350'><param name='movie' value='");
            str.push(player);
            str.push("' /><param name='allowfullscreen' value='true' /><param name='allowscriptaccess' value='always' /><param name='wmode' value='transparent' /><param name='flashvars' value='file=");
            str.push(url);
            str.push("&autostart=true' /><embed type='application/x-shockwave-flash' src='");
            str.push(player);
            str.push("' width='350' height='350' allowscriptaccess='always' allowfullscreen='true' flashvars='file=");
            str.push(url);
            str.push("&autostart=true' wmode='transparent' /></object>");
            str = str.join("");
            object.html(str).show();
        } else {
            object.show();
        }
        this.obj_v2.hide();
    },
    close: function () {
        if ($.browser.msie) {
            this.obj_video.html("");
        }
        this.obj_360.hide();
        this.obj_video.hide();
        if (this.url_360 != null || this.url_video != null) this.obj_v2.show();
    },
    init: function () {
        var _this = this;
        if (this.url_360 != null && this.url_video != null) {
            this.obj_v2.attr("class", this.obj_sub_v2).show();
            this._$(this.obj_sub_360, "id").click(function () {
                _this.open360();
            });
            this._$(this.obj_sub_video, "id").click(function () {
                _this.openVideo();
            });
        } else if (this.url_360 != null && this.url_video == null) {
            this.obj_v2.attr("class", this.obj_sub_360).show();
            this._$(this.obj_sub_360, "class").click(function () {
                _this.open360();
            });
        } else if (this.url_360 == null && this.url_video != null) {
            this.obj_v2.attr("class", this.obj_sub_video).show();
            this._$(this.obj_sub_video, "class").click(function () {
                _this.openVideo();
            });
        } else {
            return;
        }
    }
};
var flash = new jdVideo();
flash.init();
$(function () {
    $(".jqzoom").jqueryzoom({
        xzoom: 400,
        yzoom: 400,
        offset: 10,
        position: "right",
        preload: 1,
        lens: 1
    });
    $("#spec-list").jdMarquee({
        deriction: "left",
        width: 300,
        height: 56,
        step: 2,
        speed: 4,
        delay: 10,
        control: true,
        _front: "#spec-right",
        _back: "#spec-left"
    });
    $("#spec-list img").bind("mouseover", function () {
        flash.close();
        var src = $(this).attr("src");
        $("#spec-n1 img").eq(0).attr({
            src: src.replace("\/n5\/", "\/n1\/"),
            jqimg: src.replace("\/n5\/", "\/n0\/")
        });
        $(this).css({
            "border": "2px solid #ff6600",
            "padding": "1px"
        });
    }).bind("mouseout", function () {
        $(this).css({
            "border": "1px solid #ccc",
            "padding": "2px"
        });
    });
}); /*share*/
$(function () {
    var html = [],
        modelHeight;
    var url = window.location.href + "?sid=";
    url = (readCookie("pin")) ? (url + readCookie("pin")) : url;
    html.push("<div class=\"model-prompt model-partake\"><div class=\"form\"><label>��Ʒ���ӣ�</label><input type=\"text\" class=\"text\" value='");
    html.push(url);
    html.push("'/></div>");
    if ($.browser.msie) {
        html.push("<div class=\"ac\"><input type=\"button\" class=\"btn-copy\" value=\"���Ʋ������ҵĺ���\" onclick=\"window.clipboardData.setData('Text','" + url + "');$('.model-partake').html('��Ʒ���ӵ�ַ�Ѿ����ƣ�������ճ����QQ��MSN���ʼ��з��͸�������!')\"></div>");
        modelHeight = 90;
    } else {
        html.push("<div class=\"i-con\"><b></b>�����������֧���Զ����ƹ��ܡ������԰�סCtrl+C������Ʒ���ӵ�ַ����������</div>");
        modelHeight = 110;
    }
    html.push("</div>");
    html = html.join("");
    $("#site-qq").jdThickBox({
        type: "text",
        width: 400,
        height: modelHeight,
        source: html,
        _fastClose: true
    });
    $("#site-qqmsn").jdThickBox({
        type: "text",
        width: 400,
        height: modelHeight,
        source: html,
        _fastClose: true
    });
}); /*jqueryzoom*/
(function ($) {
    $.fn.jqueryzoom = function (options) {
        var settings = {
            xzoom: 200,
            yzoom: 200,
            offset: 10,
            position: "right",
            lens: 1,
            preload: 1
        };
        if (options) {
            $.extend(settings, options);
        };
        var noalt = '';
        $(this).hover(function () {
            var imageLeft = $(this).offset().left;
            var imageTop = $(this).offset().top;
            var imageWidth = $(this).children('img').get(0).offsetWidth;
            var imageHeight = $(this).children('img').get(0).offsetHeight;
            noalt = $(this).children("img").attr("alt");
            var bigimage = $(this).children("img").attr("jqimg");
            $(this).children("img").attr("alt", '');
            if ($("div.zoomdiv").get().length == 0) {
                $(this).after("<div class='zoomdiv'><img class='bigimg' src='" + bigimage + "'/></div>");
                $(this).append("<div class='jqZoomPup'>&nbsp;</div>");
            };
            if (settings.position == "right") {
                if (imageLeft + imageWidth + settings.offset + settings.xzoom > screen.width) {
                    leftpos = imageLeft - settings.offset - settings.xzoom;
                } else {
                    leftpos = imageLeft + imageWidth + settings.offset;
                }
            } else {
                leftpos = imageLeft - settings.xzoom - settings.offset;
                if (leftpos < 0) {
                    leftpos = imageLeft + imageWidth + settings.offset;
                }
            };
            $("div.zoomdiv").css({
                top: imageTop,
                left: leftpos
            });
            $("div.zoomdiv").width(settings.xzoom);
            $("div.zoomdiv").height(settings.yzoom);
            $("div.zoomdiv").show();
            if (!settings.lens) {
                $(this).css('cursor', 'crosshair');
            };
            $(document.body).mousemove(function (e) {
                mouse = new MouseEvent(e);
                var bigwidth = $(".bigimg").get(0).offsetWidth;
                var bigheight = $(".bigimg").get(0).offsetHeight;
                var scaley = 'x';
                var scalex = 'y';
                if (isNaN(scalex) | isNaN(scaley)) {
                    var scalex = (bigwidth / imageWidth);
                    var scaley = (bigheight / imageHeight);
                    $("div.jqZoomPup").width((settings.xzoom) / (scalex * 1));
                    $("div.jqZoomPup").height((settings.yzoom) / (scaley * 1));
                    if (settings.lens) {
                        $("div.jqZoomPup").css('visibility', 'visible');
                    }
                };
                xpos = mouse.x - $("div.jqZoomPup").width() / 2 - imageLeft;
                ypos = mouse.y - $("div.jqZoomPup").height() / 2 - imageTop;
                if (settings.lens) {
                    xpos = (mouse.x - $("div.jqZoomPup").width() / 2 < imageLeft) ? 0 : (mouse.x + $("div.jqZoomPup").width() / 2 > imageWidth + imageLeft) ? (imageWidth - $("div.jqZoomPup").width() - 2) : xpos;
                    ypos = (mouse.y - $("div.jqZoomPup").height() / 2 < imageTop) ? 0 : (mouse.y + $("div.jqZoomPup").height() / 2 > imageHeight + imageTop) ? (imageHeight - $("div.jqZoomPup").height() - 2) : ypos;
                };
                if (settings.lens) {
                    $("div.jqZoomPup").css({
                        top: ypos,
                        left: xpos
                    });
                };
                scrolly = ypos;
                $("div.zoomdiv").get(0).scrollTop = scrolly * scaley;
                scrollx = xpos;
                $("div.zoomdiv").get(0).scrollLeft = (scrollx) * scalex;
            });
        }, function () {
            $(this).children("img").attr("alt", noalt);
            $(document.body).unbind("mousemove");
            if (settings.lens) {
                $("div.jqZoomPup").remove();
            };
            $("div.zoomdiv").remove();
        });
        count = 0;
        if (settings.preload) {
            $('body').append("<div style='display:none;' class='jqPreload" + count + "'>360buy</div>");
            $(this).each(function () {
                var imagetopreload = $(this).children("img").attr("jqimg");
                var content = jQuery('div.jqPreload' + count + '').html();
                jQuery('div.jqPreload' + count + '').html(content + '<img src=\"' + imagetopreload + '\">');
            });
        }
    }
})(jQuery);

function MouseEvent(e) {
    this.x = e.pageX;
    this.y = e.pageY;
} /*adservice*/
var AdServiceUrl = "http://www.360buy.com/ajaxService.aspx";

function GetAdWordCallback(json) {
    if (json) {
        $("#advertiseWord").html(json.html)
    }
};

function GetSingleAdWord(skuId) {
    asyncScript(AdServiceUrl + "?callback=GetAdWordCallback&stype=single&skuid=" + skuId, GetAdWordCallback)
}; /*comment*/
mlazyload({
    defObj: "#comment",
    defHeight: -50,
    fn: function () {
        $("#comment").jdTab({
            event: "click",
            type: "dynamic",
            source: "data"
        }, function (url, object, n) {
            if (!url) {
                return
            }
            refreshComments("Product", url)
        });
    }
});
mlazyload({
    defObj: "#consult",
    defHeight: -50,
    fn: function () {
        $("#consult").jdTab({
            type: "dynamic",
            event: "click",
            source: "data"
        }, function (url, object, n) {
            switch (url) {
            case "0":
                refreshConsultations(1);
                break;
            case "2":
                refreshConsultations(2);
                break;
            case "3":
                refreshConsultations(3);
                break;
            case "4":
                refreshConsultations(4);
                break;
            case "5":
                refreshConsultations(5);
                break;
            case "7":
                getDaservice("A-product-02", object);
                break;
            case "8":
                getDaservice("A-product-03", object);
                break;
            case "9":
                getDaservice("A-product-04", object);
                break
            }
        })
    }
});
mlazyload({
    defObj: "#discuss",
    defHeight: -50,
    fn: function () {
        $("#discuss").jdTab({
            type: "dynamic",
            event: "click",
            source: "data"
        }, function (url, object, n) {
            if (!url) {
                return
            }
            var referenceType = "Club";
            switch (url) {
            case "4":
                referenceType = "Order";
                break;
            case "1":
                referenceType = "User";
                break;
            case "2":
                referenceType = "Question";
                break;
            case "3":
                referenceType = "Friend";
                break;
            default:
                break;
            }
            refreshComments(referenceType, 0)
        })
    }
});


function refreshComments(referenceType, parameter) {
    var referenceId = $.query.get("id");
    if (referenceId == "") {
        referenceId = $.query.get("wid");
    }
    if (referenceId == "") {
        var result = location.href.match(/(\d+)(.html)/);
        if (result != null) {
            referenceId = result[1];
        }
    }
    if (referenceId != "") {
        var url = "http://club.360buy.com/clubservice/productcomment-{0}-{1}-{2}.html";
        if (referenceType == "Product") {
            switch (parameter) {
            case "0":
                url = url.format(referenceId, parameter, $.fn.pagination.options0.current_page);
                break;
            case "1":
                url = url.format(referenceId, parameter, $.fn.pagination.options1.current_page);
                break;
            case "3":
                url = url.format(referenceId, parameter, $.fn.pagination.options3.current_page);
                break;
            case "5":
                url = url.format(referenceId, parameter, $.fn.pagination.options5.current_page);
                break;
            default:
                url = url.format(referenceId, parameter, $.fn.pagination.options0.current_page);
                break
            }
            $.getJSONP(url, getProductComments);
        } else if (referenceType == "Club" || referenceType == "User" || referenceType == "Question" || referenceType == "Order" || referenceType == "Friend") {
            url = "http://club.360buy.com/clubservice/comment-{0}-{1}.html".format(referenceType, referenceId);
            $.getJSONP(url, getCommentSummaryWithComments);
        }
    }
} /*usefulComment#20101029*/

function getDaservice(id, object) {
    $.jmsajax({
        url: "http://www.360buy.com/newsserver.asmx",
        method: "PayExplain",
        data: {
            id: id
        },
        success: function (data) {
            if (data != null) {
                object.html(data)
            }
        }
    })
}; /*easybuy*/

function initEasyBuy() {
    var productId = null;
    if ($("#InitCartUrl").css("display") != "none") {
        productId = parseInt($("#name").attr("pshowskuid"));
        var eb = readCookie("eb");
        if (eb == 1 || eb == null || eb == undefined) {
            $.ajax({
                url: "http://buy.360buy.com/purchase/flows/easybuy/FlowService.ashx",
                type: "get",
                data: {
                    action: "CanBuy",
                    skuId: productId
                },
                dataType: "jsonp",
                success: function (r) {
                    $("#easybuy").remove();
                    if (r.Flag) {
                        $("<a href=\"#none\" class=\"btn-easy\" id=\"easybuy\">���ɹ�</a>").insertBefore("#InitCartUrl");
                        $(".btn-easy").click(function () {
                            $(".btn-easy").hide();
                            $.extend(jdModelCallCenter.settings, {
                                clstag1: 0,
                                clstag2: 0,
                                id: productId,
                                fn: function () {
                                    DoOrder(this.id)
                                }
                            });
                            jdModelCallCenter.settings.fn()
                        })
                    }
                }
            })
        }
    }
};
var DoOrder = function (pid) {
        $.login({
            modal: true,
            complete: function (result) {
                if (result != null && result.IsAuthenticated != null && result.IsAuthenticated) {
                    var num = $.trim($("#pamount").val());
                    $.ajax({
                        url: "http://buy.360buy.com/purchase/flows/easybuy/FlowService.ashx",
                        type: "get",
                        data: {
                            action: "SubmitOrderByDefaultTemplate",
                            skuId: pid,
                            num: $("#pamount").val()
                        },
                        dataType: "jsonp",
                        success: function (r) {
                            if (r.Flag) {
                                window.location = "http://jd2008.360buy.com/purchase/Succeed_EasyBuy.aspx"
                            } else {
                                $(".btn-easy").show();
                                if (r.Message != null) {
                                    alert(r.Message);
                                } else {
                                    alert("��ʱ�޷��ύ,�����Ժ�����!");
                                }
                            }
                        }
                    })
                } else {
                    $(".btn-easy").show()
                }
            }
        })
    };
initEasyBuy(); /*rollWidgets*/
pannel.gotop.init({
    element: "#float-bar"
}, function () {
    $(document.body).append("<div id='float-bar'><div class='return'><a href='#'>�ض���</a></div></div>");
});
/*
	@Last-Modified:2011/05/10
*/