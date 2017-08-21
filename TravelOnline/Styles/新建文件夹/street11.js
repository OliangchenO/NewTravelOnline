(function(){
	$("#hot-rank").jdTab({
		currClass:"on",
		auto:true
	})
})();
(function(){
	$(".reveal-s-h li").hover(function(){
		$(this).children(".a-img").show().end().siblings().children(".a-img").hide();
	});
})();

