(function () {
    var ads = [{
        width: 211,
        width2: 0,
        height: 180,
        url: "/Images/AD/211_90.jpg",
        url2: "",
        alt: "俏牌",
        link: "#"

    }];

    var rate = [1];
    asyncScript(function () {
        setRandomAds(ads, rate, "OutBound_Right1", false);
    })
})();