var DestinationMenuFlag = "";
$(function () {
    $("a").attr("hidefocus", "true")
})
$(function () {
    $("img").lazyload({ threshold: 400, effect: "fadeIn", placeholder: "img/grey.gif" });
});
function gotoTop(min_height) {
    min_height ? min_height = min_height : min_height = 600;
    //为窗口的scroll事件绑定处理函数
    $(window).scroll(function () {
        //获取窗口的滚动条的垂直位置
        var s = $(window).scrollTop();
        //当窗口的滚动条的垂直位置大于页面的最小高度时，让返回顶部元素渐现，否则渐隐
        if (s > min_height) {
            $("#BackToPageTop").fadeIn(100);
        } else {
            $("#BackToPageTop").fadeOut(200);
        };
    });
};
gotoTop(300);