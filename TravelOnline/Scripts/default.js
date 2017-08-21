mlazyload({
    defObj: "#OutBound",
    defHeight: -50,
    fn: function () {
        $("#OutBound").jdTab({
            type: "dynamic",
            auto: false,
            event: "click",
            source: "data"
        }, function (url, object, n) {
            if (!url) {
                return
            }
            var referenceType = url//"Club";
            refreshComments("IndexProductList", referenceType, 0, 0)
        })
    }
});

mlazyload({
    defObj: "#InLand",
    defHeight: -50,
    fn: function () {
        $("#InLand").jdTab({
            type: "dynamic",
            auto: false,
            event: "click",
            source: "data"
        }, function (url, object, n) {
            if (!url) {
                return
            }
            var referenceType = url//"Club";
            refreshComments("IndexProductList", referenceType, 0, 0)
        })
    }
});
