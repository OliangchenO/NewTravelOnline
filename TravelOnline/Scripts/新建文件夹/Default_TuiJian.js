var rushdata=[
    [{
        n: "春节大阪北海道东京迎春六日",
        p: "1499.00",
        i: "i1.jpg",
        l: "154874"
    }, {
        n: "韩国全罗南道首尔5日经典游(新线)！",
        p: "136.00",
        i: "i2.jpg",
        l: "231516"
    }, {
        n: "日本大阪京都长野箱根东京七日欢乐游",
        p: "99.00",
        i: "i1.jpg",
        l: "205199"
    }],
    [{
        n: "日本东京箱根伊豆京都大阪六日游(两晚温泉)！",
        p: "168.00",
        i: "i2.jpg",
        l: "1000253514"
    }, {
        n: "CLUB MED民丹岛+新加坡4晚6日",
        p: "179.00",
        i: "i1.jpg",
        l: "136458"
    }, {
        n: "韩国全罗南道首尔5日经典游",
        p: "1599.00",
        i: "i2.jpg",
        l: "225401"
    }],
    [{
        n: "CLUB MED民丹岛+新加坡4晚6日",
        p: "2199.00",
        i: "i1.jpg",
        l: "255094"
    }, {
        n: "“欢度春节~新马4晚5日·全程四-五星”新航直飞",
        p: "3499.00",
        i: "i2.jpg",
        l: "328184"
    }, {
        n: "菲常假期★长滩岛4日包机直飞",
        p: "2999.00",
        i: "i1.jpg",
        l: "388962"
    }]
];

function rushingGoods() {
            this.data = null;
            this.viewIndex = readCookie("rush") || 0;
            this.html = [];
            this.tabItems = []; //height=\"100\" 
            this.fragment = ["<dl><dt class=\"p-img\"><a href=\"http://{url}.html\" target=\"_blank\"><img {src}=\"/images/{img}\" width=\"160\" alt=\"{name}\"/></a>{price}</dt><dd class=\"p-name\"><a href=\"http://{url}.html\" target=\"_blank\">{name}</a></dd></dl>", "<div  class=\"tabcon list-h ", "<ul class=\"tab\">", "<li class=\"", "</div>", "</li>", "</ul>", "hide", "curr", "\">"];
            this.formatStr = function (i, flag) {
                var _this = this,
            arr = this.data[i],
            html = [];
                $.each(arr, function (i) {
                    var price = (arr[i].p <= 0) ? "" : "<div class=\"p-price\">￥" + arr[i].p + "</div>";
                    var domain, src = flag ? "src2" : "src",
                url = "/travel/ProductsInfo.aspx?id=";
                    url = url + arr[i].l;
                    switch (arr[i].l.match(/(\d)$/)[1] % 5) {
                        case 0:
                            domain = 10;
                            break;
                        case 1:
                            domain = 11;
                            break;
                        case 2:
                            domain = 12;
                            break;
                        case 3:
                            domain = 13;
                            break;
                        case 4:
                            domain = 14;
                            break;
                        default:
                            domain = 10;
                            break;
                    }
                    html.push(_this.fragment[0].replace(/\{url\}/g, url).replace(/\{img\}/g, arr[i].i).replace(/\{price\}/g, price).replace(/\{name\}/g, unescape(arr[i].n)).replace(/\{src\}/, src).replace(/\{domain\}/, domain));
                });
                return html.join("");
            };
            this.pushStr = function () {
                var _this = this;
                $.each(_this.data, function (i) {
                    _this.html.push(_this.fragment[1].replace(/\{index\}/, parseInt(i) + 1));
                    _this.html.push((i == _this.viewIndex) ? "" : _this.fragment[7]);
                    _this.html.push(_this.fragment[9]);
                    _this.html.push((i == _this.viewIndex) ? _this.formatStr(i) : _this.formatStr(i, 1));
                    _this.html.push(_this.fragment[4]);
                    _this.tabItems.push(_this.fragment[3]);
                    _this.tabItems.push((i == _this.viewIndex) ? _this.fragment[8] : "");
                    _this.tabItems.push(_this.fragment[9]);
                    _this.tabItems.push(parseInt(i) + 1);
                    _this.tabItems.push(_this.fragment[5])
                })
            };
            this.init = function () {
                this.tabItems.push(this.fragment[2]);
                this.pushStr();
                this.tabItems.push(this.fragment[6]);
                this.tabItems = this.tabItems.join("");
                this.html = this.html.join("");
                $("#madding-1").html(this.html);
                $("#madding h2").after(this.tabItems);
                $("#madding").jdTab({
                    index: this.viewIndex
                }, function (url, object, n) {
                    object.find("img").each(function () {
                        var src2 = $(this).attr("src2");
                        $(this).attr("src", src2);
                        $(this).removeAttr("src2");
                    });
                });

                //$("#TextBox1").val(this.html);
                //$("#Text1AA").val(this.tabItems);
                //Text1AA
                this.viewIndex = (this.viewIndex > this.data.length - 2) ? 0 : parseInt(this.viewIndex) + 1;
                createCookie("rush", this.viewIndex, 1);
            };
        };
        var rush = new rushingGoods();
        rush.data = rushdata;
        rush.init();
        
        