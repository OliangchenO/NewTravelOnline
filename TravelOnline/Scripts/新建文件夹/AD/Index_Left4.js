(function () {
    var ads = [{
        width: 211,
        width2: 0,
        height: 90,
        url: "/Images/AD/211_90.jpg",
        url2: "",
        alt: "",
        link: "#"
    }, {
        width: 211,
        width2: 0,
        height: 90,
        url: "/Images/AD/211_90_1.jpg",
        url2: "",
        alt: "",
        link: "#"
    }];
    var rate = [1];
    asyncScript(function () {
        setRandomAds(ads, rate, "Index_Left4", false);
    })
})();