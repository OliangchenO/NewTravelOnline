(function () {
    var ads = [{
        width: 546,
        width2: 766,
        height: 90,
        url: "/IMAGES/AD/766_120_1.gif",
        url2: "/IMAGES/AD/766_120_1.gif",
        alt: "",
        link: "#"
    }, {
        width: 546,
        width2: 766,
        height: 90,
        url: "/IMAGES/AD/766_120_2.gif",
        url2: "/IMAGES/AD/766_120_2.gif",
        alt: "",
        link: "#"
    }];
    var rate = [1];
    asyncScript(function () {
        setRandomAds(ads, rate, "Index_Center1", true);
    })
})();