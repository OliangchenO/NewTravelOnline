/**
* @author ddpei@Ctrip.com
* @class c.ui.imageSlider
* @description ImageSldier
*/
define(['cBase', 'cUIBase', 'libs'], function (cBase, UIBase) {
    "use strict";
    return new cBase.Class({
        __propertys__: function () {
            //手势方向
            this.ENUM = {
                DIR: {
                    LEFT: "LEFT",
                    RIGHT: "RIGHT"
                }
            };

            //轮播图片数组 array [object] object :{src,alt,onClick}
            this.images = [];
            //是否自动播放
            this.autoPlay = true;
            //当前图片索引
            this.index = 0;
            //自动播放延迟(单位：毫秒)
            this.delay = 3 * 1000;
            //轮播方向
            this.dir = this.ENUM.DIR.LEFT;
            //图片加载 失败/错误 后显示的图片
            this.errorImage = "";
            //图片加载中显示的图片
            this.lodingImage = "";
            //轮播开始的事件
            this.onChange;
            //图片点击事件
            this.onImageClick;
            //容器 string
            this.container;
            //轮播结束
            this.onChanged;
            //该控件事件的上下文
            this.scope;
            //是否显示导航
            this.showNav = true;
            //是否使用自动高度
            this.autoHeight = false;
            //默认图，当没有图片数据的时候默认显示
            this.defaultImageUrl;
            //自定义高度
            this.defaultHeight;
            //自定义高度
            this.imageSize;
            //是否循环播放
            this.loop = false;

            //加载中图片
            this.errorImageUrl = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAAYCAMAAAAGTsm6AAAAMFBMVEX6+vr+/v7+/v7+/v7+/v7+/v7+/v7y8vLv7+/+/v7+/v7+/v729vYAAADt7e3+/v6PQhf9AAAADnRSTlOtVTOIZpki1+wRd0TCAMJ8iPYAAAKfSURBVHjaxZWLjqMwDEXJO7FN7v//7WITOgQ6pVpptbdSecTxiW3iLOt/0g3cirj3po1ZL5lcuw8Sud8QgegJbALy+laMZb/UdY2n9Xj9F/wKFnwLXu/KRiSLG7ISwk+s6Fm9n9PgZUjNKvJ34LLeRThECl4L4ssDyMKak7NLTRvKc41z28Bia15dEVWxwkYiShAicgb26QiwAY6ZC1gV4wCTXdXUjey09gFMOCSvVSv46u2kiEnyMm3DNGGIP4GLCLpIgQyOmD266MsqKnuQPnJbkWhTBalSeIEFycAdZi3in2osOjHM4CqmO5i0gvcaG9ipr4gyDT+BaQKbUom3VLc6xqXewaymBfQdOOu3QFew14HSbGPVLYRCediPBHZ5D2b0bOt6BBvvBsZLoqtvGIEGCWRCJVOQOIEFVpMqfwleaEFajLImNId0dKWb6AXWqrQg8NvN8ggmJRHcBNZiJu1OwyIclY8UWZUAxyoinsBqHtcMegSnHcwzOJfKXODUrwfVa8lq7z+uZ7Dboo3gR3CB0q/gcs5kB+I8KSKlnmdwrTu4AXnpj706j213ATvtlQU8knIJ2HdkRjmfoLkplL0ikJAewYTwDnw0icxRbeRyhCPoaGpTEiiGdpwY+RFcDSSbJVu76vacmYlS31PN1nfpRfHVdnjuqPHcWfS919ttoD2BCXWcyRPYYVcgzr533ki1DwLBuJZw9ERsD0mPCUH3+nr7xc9ghu4jEvT18nGFyHvfcx3eTjGbyaEDMuLJgvG+JRTz3pmA1ApQQ/4d3KpOaxraRhhgd65xC0C0gOyiHs/7iBcrZ65HElKFuQkAPp1OLTUFiNoysU7lfKr/usa6L4TS0Vfm8uVoo8q17Y/KdseyXFP9T9VOn9s88gcLTIlzurC4gAAAAABJRU5ErkJggg==';

            this.loadImageUrl = 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAAYCAMAAAAGTsm6AAAAMFBMVEX6+vr+/v7+/v7+/v7+/v7+/v7+/v7+/v7y8vLv7+/+/v729vb+/v4AAADt7e3+/v6c4gmbAAAADnRSTlOtM4hmVZkiEdfsRMJ3AHDRYrYAAAGwSURBVHjaxdbZktsgEAVQLSxNL9z//9vQgvFYUjKpkqnkPhgJbI4bJMuL/af8DGeS8IcRZm+Shnwb21U/hQ1Ivx9gLL0pDbqMKbYJsN2TDlGPuiF3RzGhYrJ7FF9Rh42wT4VTbrD40WqBxEM89lAjpL2GA15j7h/gdIIzp0fwd2FijB62ER6TO/zdd40+g0kEVYQgw5EDbn3eWcRznEhFh6t4yhgrz+BXQYztDBfpucNyWmr9GNYTfCTSflrqyXDyO0Wv8OoDlH3cCpmRptmwe3cYr0jrsgzwv4EXXRAX2VTVInJAnL7UCvaXcNnjXGI7zuMdG/gLLuoRSG+fw7HDfIYTFWZCUGVboaXMvY89BNevML1PXIH9BRN7ImJvH8MJ5NwVDqqBCTwWpdjIftnjXfaHsGK7wyMCS9wmVtdmP50K+CCS8fFzVY/zxKwaa19qBlqn5qmwooxn8gkO6NmU01org3KpM2GG30cqqHa5uLad27F/h4q1OcAyEc7Fp8teWhMGHN73OG/9io7evGX78K9PjtkBcZLVa105ve2/2V5CLzHaewTyAJ6QlVf7a34BIRiNTVIrVfEAAAAASUVORK5CYII=';
            //加载中图片节点
            this._loadingNode;
            //错误节点
            this._errorNode;
            //是否正在加载图片
            this._isloadingImage = false;
            //编号前缀
            this._pfix = "slider";
            //当前是是否正在轮播图片
            this._changing = false;
            //容器节点
            this._containerNode;
            //根节点
            this._rootNode;
            //图片层节点
            this._imageNode;
            //导航层节点
            this._navNode;
            //图片加载节点
            this._imageLoaderNode;
            //手指起始位置位置
            this._handerStartPos = {
                x: 0,
                y: 0
            };
            //手指滑动最小偏移量
            this._moveValue = 50;
            //图片数量
            this._imageCount = 0;
            //是否已经创建
            this._played = false;
            //当前轮播尺寸
            this._size = { height: 0, width: 0 };
            //上一次的轮播尺寸
            this._lastSize = { height: 0, width: 0 };
            //当前轮播的对象
            this._displayImage;
            //待切换的图像
            this._nextImage;
            //加载中图片测试
            this._loadingImage = new Image();
            //切换图片完成需要执行的事件
            this._changeCompletedEvents = [];
            //settimeout
            this._autoPlayTimeout;
            //加载信息
            this._loadMsg = "加载中...";
            //是否首次加载
            this.firstLoad = true;
            //保留加载正确的图片尺寸
            this._defaultSize = { width: 0, height: 0 };
            //加载中图片
            this._loadingImage = new Image();
            //错误图片
            this._errorImage = new Image();
        },
        initialize: function (args) {
            for (var key in args) {
                switch (key) {
                    case "images":
                    case "autoPlay":
                    case "delay":
                    case "dir":
                    case "index":
                    case "onChange":
                    case "autoPlay":
                    case "onImageClick":
                    case "scope":
                    case "onChanged":
                    case "errorImageUrl":
                    case "loadImageUrl":
                        // case "autoHeight":
                    case "loop":
                    case "showNav":
                    case "defaultImageUrl":
                    case "defaultHeight":
                    case "imageSize":
                        this[key] = args[key];
                        break;
                    case "container":
                        this._containerNode = (typeof args[key] === "string" ? $(args[key]) : args[key]);
                        this[key] = args[key];
                        break;
                }
            }

            //验证参数
            this._validArgs();
            //纠正参数
            this._correctArgs();
            //图片数量
            this._imageCount = this.images.length;
            //加载中
            this._loadingImage.src = this.loadImageUrl;
            //错误图
            this._errorImage.src = this.errorImageUrl;

            if (this.imageSize && this.imageSize.width && this.imageSize.height) {
                this.autoHeight = false;
            }
            else {
                this.autoHeight = true;
            }

        }
        //开始播放，入口
        , play: function () {
            if (!this._played) {
                this._played = true;
                this._injectHTML();
                this._bindEvents();
            }

            this.rePlay();
        }
        //暂停自动播放
        , stop: function () {
            this._clearAutoPlay();
        }
        //重新自动播放
        , rePlay: function () {
            if (this.autoPlay) {
                this._autoPlay();
            }
        }
        //自动播放
        , _autoPlay: function () {
            this._autoPlayTimeout = setTimeout($.proxy(function () {
                if (!this._isloadingImage) {
                    this._play();
                }
            }, this), this.delay);
        }
        //切换至下一张
        , next: function () {
            if (!this._changing) {
                this._play();
            }
        }
        //切换至上一张
        , pre: function () {
            if (this._changing) {
                return;
            }

            if (this.dir === this.ENUM.DIR.RIGHT) {
                if (this.index >= this._imageCount - 1) {
                    if (!this.loop) {
                        return;
                    }
                    this.index = 0;
                }
                else {
                    this.index++;
                }
            }
            else {
                if (this.index <= 0) {
                    if (!this.loop) {
                        return;
                    }
                    this.index = this._imageCount - 1;
                }
                else {
                    this.index--;
                }
            }
            this.goto(this.index);
        }
        // 直接按照默认方向跳转至指定索引图片
        , goto: function (index) {
            this.index = index;
            this._changeImage();
        }
        //根据默认的dir立刻切换图片
        , _play: function () {
            if (this.dir === this.ENUM.DIR.RIGHT) {
                this._imageToRight();
            }
            else {
                this._imageToLeft();
            }
        }
        //清除自动播放
        , _clearAutoPlay: function () {
            clearTimeout(this._autoPlayTimeout);
        }
        //验证参数
        , _validArgs: function () {
            if (!this.container || !this._containerNode) {
                throw "[c.widget.imageSlider]:no container!";
            }
        }
        //纠正某些参数设置不合理
        , _correctArgs: function () {
            //轮播延迟过小
            if (this.delay <= 500) {
                this.delay = 2 * 1000;
            }
        }
        //创建HTML
        , _createHTML: function () {
            return ["<div class=\"cui-sliderContainer\" style=\"width:100%;position:relative;\">",
                    "<div class=\"cui-imageContainer\" style=\"width:100%;\">",
                    "</div>",
                    "<div class=\"cui-navContainer\" style=\"color:#1491c5;position:absolute;\"></div>",
                    "<div class=\"cui-imageLoader\">",
                    "</div>"].join("");
        }
        //创建导航
        , _createNav: function () {
            var navhtml = [];
            for (var i = 0; i < this._imageCount; i++) {
                var current = (i == this.index ? "cui-slide-nav-item-current" : "");
                navhtml.push("<span class=\"cui-slide-nav-item " + current + "\"></span>")
                //navhtml.push("<span data-index=\"" + i + "\" style=\"padding-left:10px\"> " + current + "</span>");
            }
            this._navNode.empty().html(navhtml.join(" "));
        }
        //注入HTML 以及 初始化对象
        , _injectHTML: function () {
            this._rootNode = $(this._createHTML());
            this._containerNode.html(this._rootNode);
            this._imageNode = this._rootNode.find(".cui-imageContainer");
            this._navNode = this._rootNode.find(".cui-navContainer");
            if (!this.showNav || this._imageCount <= 1) {
                this._navNode.css("display", "none");
            }
            this._imageNode.empty();
            this._createLoading();
            if (this._imageCount > 0) {
                this._createImageItem(this.index, $.proxy(function (imageInfo, image) {
                    this._createNav();
                    //根据第一张图片的高度来设置整个轮播布局的高度
                    this._setSize(image.height, image.width);
                    this._displayImage = imageInfo;
                    this._createImageContainer();
                }, this));
            }
            else {
                this._createDefault();
                this._loadingNode.css("display", "none");
            }
        }
        //单击
        , _onImageClick: function () {
            var imageInfo = this.images[this.index];
            if (imageInfo && imageInfo.onClick) {
                imageInfo.onClick.call(this.scope || this, imageInfo);
                return;
            }
            if (this.onImageClick) {
                this.onImageClick.call(this.scope || this);
            }
        }
        //获取image
        , _createImageItem: function (index, callback) {
            this._isloadingImage = true;
            !index && (index = 0);
            var imageInfo = this._getImageInfo(index);
            var image = new Image();
            imageInfo.src && (image.src = imageInfo.src);
            imageInfo.alt && (image.alt = imageInfo.alt);
            var self = this;
            image.onload = function () {
                imageInfo.orgImage = image;
                if (!self.autoHeight) {
                    self._defaultSize.width = image.width;
                    self._defaultSize.height = image.height;
                }
                self._isloadingImage = false;
                callback.call(self, imageInfo, image);
            }

            image.onerror = function () {
                imageInfo.loadError = true;
                self._errorImage = new Image();
                self._errorImage.src = self.errorImageUrl;
                self._isloadingImage = false;
                self._errorImage.onload = function () {
                    imageInfo.orgImage = self._errorImage;
                    callback.call(self, imageInfo, self._errorImage);
                }
            }
        }
        //获取image 配置信息
        , _getImageInfo: function (index) {
            !index && (index = 0);
            for (var i = 0, len = this.images.length; i < len; i++) {
                if (index === i) {
                    return this.images[i];
                }
            }
            throw new Error("[c.ui.imageSlider]:image index is " + index + ",but images.length is " + len);
        }
        //绑定事件
        , _bindEvents: function () {
            this._containerNode.bind("touchmove", $.proxy(this._touchmove, this));
            this._containerNode.bind("touchstart", $.proxy(this._touchstart, this));
            this._containerNode.bind("touchend", $.proxy(this._touchend, this));
            $(window).on("resize", $.proxy(this._resize, this));
            this._navNode.bind("click", $.proxy(this._switchImage, this));
            this._imageNode.bind("click", $.proxy(this._onImageClick, this));
        }
        //手动切换图片
        , _switchImage: function (e) {
            var element = e.targetElement || e.srcElement;
            var index = $(element).data("index");

            if (index !== 0 && !index) {
                return;
            }

            if (this.index === (+index)) {
                return;
            }
            this.index = index;
            this._changeImage();
        }
        //图片左滑
        , _imageToRight: function () {
            //当处于第一张时，调到最后一样
            if (this.index <= 0) {
                if (!this.loop) {
                    return;
                }
                this.index = this._imageCount - 1
            }
            else {
                this.index--;
            }
            this._changeImage(this.ENUM.DIR.LEFT);
        }
        //图片右滑
        , _imageToLeft: function () {
            if (this.index >= this._imageCount - 1) {
                if (!this.loop) {
                    return;
                }
                this.index = 0;
            }
            else {
                this.index++;
            }

            this._changeImage(this.ENUM.DIR.RIGHT);
        }
        , _changeImage: function (dir) {
            if (this._imageCount <= 1) {
                return;
            }
            //清除自动播放    
            this._clearAutoPlay();
            //表示正在轮播图片
            this._changing = true;
            //如果没有指定方向，则按照默认方向滑动
            !dir && (dir = this.dir);
            var imageInfo = this.images[this.index];
            if (imageInfo.node) {
                this._nextImage = imageInfo;
                this._showSliderImage(dir);
            }
            else {
                this._nextImage = { node: this._loadingNode, orgImage: this._loadingImage };
                this._showLoading();
                this._createImageItem(this.index, $.proxy(function (imageInfo, image) {
                    this._createImageContainer();
                    this._nextImage = imageInfo;
                    this._showSliderImage(dir);
                }, this));
            }


        }
        , _showSliderImage: function (dir, moveValue) {
            //获取初始位置，以及目标位置
            var initNextImageLeft = 0,
                initDisplayImageLeft = 0,
                offsetNextImageLeft = 0,
                offsetDisplayImageLeft = 0;
            if (dir === this.ENUM.DIR.LEFT) {
                initNextImageLeft = -1 * this._size.width;
                initDisplayImageLeft = 0;

                offsetNextImageLeft = 0;
                offsetDisplayImageLeft = this._size.width;
            }
            else {
                initNextImageLeft = this._size.width;
                initDisplayImageLeft = 0;

                offsetNextImageLeft = 0;
                offsetDisplayImageLeft = -1 * this._size.width;
            }


            //设置切换图片初始位置
            this._displayImage.node.css("left", initDisplayImageLeft);
            this._nextImage.node.css("left", initNextImageLeft).css("display", "");

            this._nextImage.node.animate({ "left": offsetNextImageLeft }, 500, 'ease-out', $.proxy(function () {
                this._changing = false;
                this._changeCompeted();
                this.rePlay();
            }, this));

            //使用zepto动画
            this._displayImage.node.animate({ "left": offsetDisplayImageLeft }, 500, 'ease-out', $.proxy(function () {
                this._displayImage.node.css("display", "none").css("left", 0);
                this._displayImage = this._nextImage;
            }, this));

        }
        , _touchmove: function (e) {
            e.preventDefault();
            if (this._changing) {
                return;
            }
            var pos = UIBase.getMousePosOfElement(e.targetTouches[0], e.currentTarget);
            //计算手势
            var diffX = pos.x - this._handerStartPos.x;
            if (diffX > 0 && diffX > this._moveValue) {
                this._imageToRight();
            }
            else if (diffX < 0 && Math.abs(diffX) > this._moveValue) {
                this._imageToLeft();
            }
        }
        , _touchstart: function (e) {
            e.preventDefault();
            var pos = UIBase.getMousePosOfElement(e.targetTouches[0], e.currentTarget);
            this._handerStartPos = {
                x: pos.x,
                y: pos.y
            };
        }
        , _touchend: function (e) {
            e.preventDefault();
        }
        //根据屏幕宽度，等比例缩放显示高度和宽度
        , _setSize: function (height, width) {
            this._size.width = Math.ceil($(window).width());
            this._size.height = Math.ceil(height * (this._size.width / width));

            if (this._size.height < 100) {
                this._size.height = 100;
                this._size.width = width * (this._size.height / height);
            }

            this._rootNode.css("width", this._size.width).css("height", this._size.height);
            this._imageNode.find("div").find("img").css("width", this._size.width).css("height", this._size.height);

            //定位导航
            if (this.showNav) { //如果不显示导航，则不定位导航
                this._setNavPos();
            }
        }
        //定位导航位置
        , _setNavPos: function () {
            var left = (this._size.width - 2 * (this._imageCount * 10)) / 2; //居中计算LEFT值
            var top = this._size.height - 30; //距离底部边框30px
            this._navNode.css("left", left).css("top", top);
        }
        , _resize: function () {
            //自适应高度，根据图片不同，生成的高度不同
            this._lastSize.width = this._size.width;
            this._lastSize.height = this._size.height;
            //定义了图片宽高，则使用宽高的值，进行图片压缩
            if (this.imageSize && this.imageSize.height && this.imageSize.width) {
                this._setSize(this.imageSize.height, this.imageSize.width);
            }
            //反之，按照图片的宽度与显示设备的宽度来等比缩放
            else {
                if (this._displayImage && !this._displayImage.loadError) {
                    this._setSize(this._displayImage.orgImage.height, this._displayImage.orgImage.width);
                }
            }
        }
        //轮播结束时触发
        , _changeCompeted: function () {
            //调用需要在回调结束时触发的事件
            for (var key in this._changeCompletedEvents) {
                this._changeCompletedEvents[key]();
            }
            this._changeCompletedEvents.length = 0;
            //切换导航到相应的索引
            this._changeNav();
            //如果没有设置图片宽高，则根绝当前图片来重新渲染图片大小
            if (this.autoHeight) {
                this._resize();
            }
            //回调
            this.onChanged && this.onChanged.call(this.scope || this, this.images[this.index], this.index);
        }
        //切换导航至对应的图片索引
        , _changeNav: function () {
            if (this.showNav) {
                this._navNode.find("span").removeClass("cui-slide-nav-item-current");
                $(this._navNode.find("span")[this.index]).addClass("cui-slide-nav-item-current");
            }
        }
        //创建图片容器
        , _createImageContainer: function () {
            var imageInfo = this.images[this.index];
            this._loadingNode.css("display", "none");
            if (!imageInfo.node) {
                var top = UIBase.getElementPos(this._rootNode[0]).top - 48;
                if (imageInfo.loadError) {
                    imageInfo.node = $(this._createImageHtml(this.errorImageUrl, imageInfo.alt));
                }
                else {
                    imageInfo.node = $(this._createImageHtml(imageInfo.src, imageInfo.alt));
                }

                this._imageNode.append(imageInfo.node);
                imageInfo.node.css("position", "absolute").css("left", 0); //.css("top", top);
                imageInfo.node.bind("click", function (e) {
                    e.preventDefault();
                });
            }
            if (this.autoHeight) {
                this._resize();
            }
        }
        , _createLoading: function () {
            if (this.firstLoad) {
                this._loadingNode = $(this._createImageHtml(this.loadImageUrl));
                var loading = ["<div class=\"cui-breaking-load\">",
                             "<div class=\"cui-i cui-m-logo\">",
                             "</div> <div class=\"cui-i cui-w-loading\">",
                             "</div></div>"];

                this._loadingNode.html(loading.join(" "));
                if (!this.autoHeight) {
                    this._resize();
                    this._setLoadingPos();
                }
                this._imageNode.append(this._loadingNode);
                this.firstLoad = false;
            }
        }

        , _setLoadingPos: function () {
            this._loadingNode.css("position", "absolute").css("height", this._size.height).css("width", this._size.width);
            if (this._size.height) {
                var top = (this._size.height - 70) / 2;
                this._loadingNode.find(".cui-breaking-load").css("top", top);
            }
        }
        , _showLoading: function () {
            this._loadingNode.css("display", "");
            this._setLoadingPos();
        }
        , _createDefault: function () {
            if (this.defaultImageUrl) {
                var defaultImage = new Image();
                defaultImage.src = this.defaultImageUrl;
                var self = this;
                defaultImage.onload = function () {
                    var defaultImageNode = $(self._createImageHtml(self.defaultImageUrl));
                    self._imageNode.append(defaultImageNode);
                    self._displayImage = defaultImage;
                    if (!self.autoHeight) {
                        self._setSize(self.imageSize.height, self.imageSize.width);
                    }
                    else {
                        self._setSize(defaultImage.height, defaultImage.width);
                    }
                }
            }
        }
        , _createImageHtml: function (src, alt) {
            return "<div class=\"image-node slider-imageContainerNode\"><img style=\"width:"
                + this._size.width + "px;height:" + this._size.height + "px\" src=\"" +
                src + "\" alt=\"" + (alt ? alt : "") + "\"></div>"
        }
        //指定某个控件在容器中居中


    });
});