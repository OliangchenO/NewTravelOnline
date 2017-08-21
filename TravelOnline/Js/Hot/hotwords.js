    (function () {
        var A = "<strong>热门搜索：</strong><a href='/Search.aspx?keyword=" + escape("日本") + "' target='_blank'>日本</a><a href='/Search.aspx?keyword=" + escape("韩国") + "' target='_blank'>韩国</a><a href='/Search.aspx?keyword=" + escape("马尔代夫") + "' target='_blank'>马尔代夫</a><a href='/Search.aspx?keyword=" + escape("香港") + "' target='_blank'>香港</a><a href='/Search.aspx?keyword=" + escape("台湾") + "' target='_blank'>台湾</a><a href='/Search.aspx?keyword=" + escape("埃及") + "' target='_blank'>埃及</a><a href='/Search?keyword=" + escape("新加坡") + "' target='_blank'>新加坡</a><a href='/Search.aspx?keyword=" + escape("马来西亚") + "' target='_blank'>马来西亚</a><a href='/Search.aspx?keyword=" + escape("澳门") + "' target='_blank'>澳门</a>";
        var B = ["普吉岛", "美国", "海南", "济州岛","四川"];
        var R = [3, 3, 3, 3];
        var referrer = document.referrer;
        if (referrer.match(/http:\/\/www.google.com/) || referrer.match(/http:\/\/cn.bing.com/)) {
            var word = referrer.match(/\Wq=(.*?)&/)[1];
            if (!new RegExp("[\\u4e00-\\u9fa5]").test(word)) {
                B = decodeURI(word);
            }
        } else {
            B = getRandomObj(B, R);
        }
        $("#hotwords").html(A);
        $("#key").val(B).bind("focus", function () {
            if (this.value == B) {
                this.value = "";
                this.style.color = "#333"
            }
        }).bind("blur", function () {
            if (this.value == "") {
                this.value = B;
                this.style.color = "#999"
            }
        });
    })();