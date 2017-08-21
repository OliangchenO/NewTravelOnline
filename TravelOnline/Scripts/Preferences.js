function rushingGoods() {
            this.data = null;
            //this.viewIndex = readCookie("rush") || 0;
            this.viewIndex = 0;
            this.html = [];
            this.tabItems = []; //height=\"100\" 
            this.fragment = ["<dl><dt class=\"p-img\"><a href=\"/{url}.html\" target=\"_blank\"><img {src}=\"{img}\" width=\"160\" alt=\"{name}\" onerror=\"this.src='/Images/none.gif'\" /></a>{price}</dt><dd class=\"p-name\"><a href=\"/{url}.html\" target=\"_blank\">{name}</a></dd></dl>", "<div  class=\"tabcon list-h ", "<ul class=\"tab\">", "<li class=\"", "</div>", "</li>", "</ul>", "hide", "curr", "\">"];
            this.formatStr = function (i, flag) {
                var _this = this,
            arr = this.data[i],
            html = [];
                $.each(arr, function (i) {
                    var price = (arr[i].p <= 0) ? "" : "<div class=\"p-price\">￥" + arr[i].p + "</div>";
                    var domain, src = flag ? "src2" : "src",
                	url = "";
                    url = arr[i].l;
                    domain = 10;
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
        
        