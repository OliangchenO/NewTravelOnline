$.cookie("pageurl", location.href, { expires: 30, path: '/' });

//��¼������������Ʒ
var cookieName = "HistoryCookies";   //cookie����
var nid;                          //���·��ʵ���ƷID
var N = 9;                        //����cookie����������¼������

function HistoryRecord() {
    var historyp;
    nid = $("#TB_LineId").val();
    if (nid == null || nid == "") {
        return;
    }
    nid += "|" + $(".xname").html();
    nid += "|" + $("#TB_Price").val();
    nid += "|" + $("#TB_pic").val();
    
    if ($.cookie(cookieName) == null) //cookie ������
    {
        $.cookie(cookieName, nid, { expires: 30, path: '/' });
        return;
    }
    else //cookies�Ѿ�����
    {
        historyp = $.cookie(cookieName);
    };

    var pArray = historyp.split(',');
    historyp = nid;
    //�ж��Ǹ���Ʒ����Ƿ������������ʵļ�¼����
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

//��ȡ������������Ʒ
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
        var pArray = historyp.split(',');
        for (var i = 0; i < pArray.length; i++) {
            if (pArray[i] != "") {
                //prdIDs.push(pArray[i]);
                prdIDs = pArray[i].split('|');
                CookieGoods += "<li class='fore'><div class='p-img'><a target='_blank' href='/line/" + prdIDs[0] + ".html'><img alt='" + prdIDs[1] + "' src='" + prdIDs[3] + "' onerror=\"this.src='/Images/none.gif'\"></a></div><div class='p-name'><a target='_blank' href='/line/" + prdIDs[0] + ".html'>" + prdIDs[1] + "</a></div><div class='p-price'><strong>&yen;" + prdIDs[2] + "</strong></div></li>"
             }
        }

        $("#CookieView").html(CookieGoods);
    }
}
    