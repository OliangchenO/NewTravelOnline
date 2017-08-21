(function () {
    var ads = [{
        width: 546,
        width2: 766,
        height: 90,
        url: "/IMAGES/AD/766_120_3.gif",
        url2: "/IMAGES/AD/766_120_3.gif",
        alt: "家电",
        link: "#"
    }, {
        width: 546,
        width2: 766,
        height: 90,
        url: "/IMAGES/AD/766_120_4.gif",
        url2: "/IMAGES/AD/766_120_4.gif",
        alt: "",
        link: "#"
    }];
    var rate = [1];
    asyncScript(function () {
        setRandomAds(ads, rate, "Index_Center2", true);
    })
})();