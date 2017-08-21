/**
 * jQuery.ScrollTo - Easy element scrolling using jQuery.
 * Copyright (c) 2007-2009 Ariel Flesler - aflesler(at)gmail(dot)com | http://flesler.blogspot.com
 * Dual licensed under MIT and GPL.
 * Date: 5/25/2009
 * @author Ariel Flesler
 * @version 1.4.2
 *
 * http://flesler.blogspot.com/2007/10/jqueryscrollto.html
 */
; (function (d) { var k = d.scrollTo = function (a, i, e) { d(window).scrollTo(a, i, e) }; k.defaults = { axis: 'xy', duration: parseFloat(d.fn.jquery) >= 1.3 ? 0 : 1 }; k.window = function (a) { return d(window)._scrollable() }; d.fn._scrollable = function () { return this.map(function () { var a = this, i = !a.nodeName || d.inArray(a.nodeName.toLowerCase(), ['iframe', '#document', 'html', 'body']) != -1; if (!i) return a; var e = (a.contentWindow || a).document || a.ownerDocument || a; return d.browser.safari || e.compatMode == 'BackCompat' ? e.body : e.documentElement }) }; d.fn.scrollTo = function (n, j, b) { if (typeof j == 'object') { b = j; j = 0 } if (typeof b == 'function') b = { onAfter: b }; if (n == 'max') n = 9e9; b = d.extend({}, k.defaults, b); j = j || b.speed || b.duration; b.queue = b.queue && b.axis.length > 1; if (b.queue) j /= 2; b.offset = p(b.offset); b.over = p(b.over); return this._scrollable().each(function () { var q = this, r = d(q), f = n, s, g = {}, u = r.is('html,body'); switch (typeof f) { case 'number': case 'string': if (/^([+-]=)?\d+(\.\d+)?(px|%)?$/.test(f)) { f = p(f); break } f = d(f, this); case 'object': if (f.is || f.style) s = (f = d(f)).offset() } d.each(b.axis.split(''), function (a, i) { var e = i == 'x' ? 'Left' : 'Top', h = e.toLowerCase(), c = 'scroll' + e, l = q[c], m = k.max(q, i); if (s) { g[c] = s[h] + (u ? 0 : l - r.offset()[h]); if (b.margin) { g[c] -= parseInt(f.css('margin' + e)) || 0; g[c] -= parseInt(f.css('border' + e + 'Width')) || 0 } g[c] += b.offset[h] || 0; if (b.over[h]) g[c] += f[i == 'x' ? 'width' : 'height']() * b.over[h] } else { var o = f[h]; g[c] = o.slice && o.slice(-1) == '%' ? parseFloat(o) / 100 * m : o } if (/^\d+$/.test(g[c])) g[c] = g[c] <= 0 ? 0 : Math.min(g[c], m); if (!a && b.queue) { if (l != g[c]) t(b.onAfterFirst); delete g[c] } }); t(b.onAfter); function t(a) { r.animate(g, j, b.easing, a && function () { a.call(this, n, b) }) } }).end() }; k.max = function (a, i) { var e = i == 'x' ? 'Width' : 'Height', h = 'scroll' + e; if (!d(a).is('html,body')) return a[h] - d(a)[e.toLowerCase()](); var c = 'client' + e, l = a.ownerDocument.documentElement, m = a.ownerDocument.body; return Math.max(l[h], m[h]) - Math.min(l[c], m[c]) }; function p(a) { return typeof a == 'object' ? a : { top: a, left: a} } })(jQuery);

//pin
$.fn.smartFloat = function () {
    var position = function (element) {
        var top = element.position().top; //��ǰԪ�ض���element����������ϱ�Ե�ľ��� 
        var pos = element.css("position"); //��ǰԪ�ؾ���ҳ��document�����ľ��� 
        $(window).scroll(function () { //��������ʱ 
            var scrolls = $(this).scrollTop();
            if (scrolls > top) { //���������ҳ�泬���˵�ǰԪ��element�����ҳ�涥���ĸ߶� 
                if (window.XMLHttpRequest) { //�������ie6 
                    element.css({ //����css 
                        position: "fixed", //�̶���λ,�����ٸ������ 
                        top: 0 //����ҳ�涥��Ϊ0 
                    }).addClass("shadow"); //������Ӱ��ʽ.shadow 
                } else { //�����ie6 
                    element.css({
                        top: scrolls  //��ҳ�涥������ 
                    });
                }
                $("#nav_order").show();
            } else {
                $("#nav_order").hide();
                element.css({ //�����ǰԪ��elementδ������������ϱ�Ե����ʹ��Ĭ����ʽ 
                    position: pos,
                    top: top
                }).removeClass("shadow"); //�Ƴ���Ӱ��ʽ.shadow 
            }
        });
    };
    return $(this).each(function () {
        position($(this));
    });
};
/*
 * jQuery One Page Nav Plugin
 * http://github.com/davist11/jQuery-One-Page-Nav
 *
 * Copyright (c) 2010 Trevor Davis (http://trevordavis.net)
 * Dual licensed under the MIT and GPL licenses.
 * Uses the same license as jQuery, see:
 * http://jquery.org/license
 *
 * @version 0.9
 */
(function(e){e.fn.onePageNav=function(j){var h=e.extend({},e.fn.onePageNav.defaults,j),d={};d.sections={};d.bindNav=function(b,c,a){var f=b.parent(),g=b.attr("href"),i=e(window);if(!f.hasClass(a.currentClass)){a.begin&&a.begin();d.adjustNav(c,f,a.currentClass);i.unbind(".onePageNav");e.scrollTo(g,a.scrollSpeed,{easing:a.easing,offset:{top:-a.scrollOffset},onAfter:function(){if(a.changeHash)window.location.hash=g;i.bind("scroll.onePageNav",function(){d.scrollChange(c,a)});a.end&&a.end()}})}};d.adjustNav=
function(b,c,a){b.find("."+a).removeClass(a);c.addClass(a)};d.getPositions=function(b,c){b=b.find("a");if(c.filter!=="")b=b.filter(c.filter);b.each(function(){var a=e(this).attr("href"),f=e(a).offset();f=f.top;d.sections[a.substr(1)]=Math.round(f)-c.scrollOffset})};d.getSection=function(b,c){var a="";c=Math.round(e(window).height()*c.scrollThreshold);for(var f in d.sections)if(d.sections[f]-c<b)a=f;return a};d.scrollChange=function(b,c){d.getPositions(b,c);var a=e(window).scrollTop();a=d.getSection(a,
c);a!==""&&d.adjustNav(b,b.find("a[href=#"+a+"]").parent(),c.currentClass)};d.init=function(b,c){var a=false,f=b.find("a");if(c.filter!=="")f=f.filter(c.filter);f.bind("click",function(g){d.bindNav(e(this),b,c);g.preventDefault()});d.getPositions(b,c);e(window).bind("scroll.onePageNav",function(){a=true});setInterval(function(){if(a){a=false;d.scrollChange(b,c)}},250)};return this.each(function(){var b=e(this),c=e.meta?e.extend({},h,b.data()):h;d.init(b,c)})};e.fn.onePageNav.defaults={currentClass:"current",
changeHash:false,easing:"swing",filter:"",scrollSpeed:750,scrollOffset:0,scrollThreshold:0.5,begin:false,end:false}})(jQuery);