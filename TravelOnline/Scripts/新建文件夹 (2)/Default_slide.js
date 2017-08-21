$("#slide").jdSlide({
    width: (screen.width >= 1280) ? 766 : 546,
    height: 270,
    type: "string",
    pics: [{
        src:
        (screen.width >= 1280) ? "/images/ad/glbanner.jpg" : "/images/ad/glbanner.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "桂林山水",
        breviary: "#",
        type: "img"
    }, {
        src:
        (screen.width >= 1280) ? "/images/ad/上海陕西.jpg" : "/images/ad/上海陕西.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "西安世博会",
        breviary: "#",
        type: "img"
    }, {
        src:
        (screen.width >= 1280) ? "/images/ad/春节欧洲.jpg" : "/images/ad/春节欧洲.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "春节欧洲",
        breviary: "#",
        type: "img"
    }, { src:
        (screen.width >= 1280) ? "/images/ad/海南广告.jpg" : "/images/ad/海南广告.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "8月海南",
        breviary: "#",
        type: "img"
    }, {
        src:
        (screen.width >= 1280) ? "/images/ad/日本banner.jpg" : "/images/ad/日本banner.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "风情日本",
        breviary: "#",
        type: "img"
    }, {
        src:
        (screen.width >= 1280) ? "/images/ad/新埃及.jpg" : "/images/ad/新埃及.jpg",
        href: "/travel/ProductsInfo.aspx",
        alt: "梦幻埃及",
        breviary: "#",
        type: "img"
    }]
})