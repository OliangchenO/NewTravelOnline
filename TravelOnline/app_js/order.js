//if ($.cookie("loginstep") != "login") {
//    if ($.cookie("orderuid") == null) top.location = "/app/main";
//}
var detailid = "";
var pagehash = window.location.hash.replace("#", "").toLowerCase();
var actions = window.location.hash.replace("#", "").toLowerCase();
if (pagehash.indexOf("orderdetail") > -1) {
    var sid = pagehash.split("?");
    actions = sid[0];
    detailid = sid[1];
}
var state = { action: actions, title: "", url: "#" + pagehash };
var scroll_top = 0;

$(document).ready(function () {
    App.init();
    App.blockUI({ boxed: true });
    $("#loginname").val($.cookie("loginusername"));
    if (state.action == "first" || actions == "first") {
        var url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
        $.getJSON(url, function (obj) {
            if (obj.success) {
                LoadPage("first");
            } else {
                $.cookie("loginstep", "first", { expires: 30, path: '/WeChat' });
                state = { action: "login", url: "#login" };
                history.pushState(state, "用户登录", "#login");
                LoadPage("login");
            }
        }, 'json');
    } else {
        if (state) {
            LoadPage(state.action);
        } else {
            LoadPage(actions);
        }
    }
});

function LoadPage(flag) {
    $(".sub_view").hide();
    $(".icon_home").hide();
    switch (flag) {
        default:
            $("#titlename").html("订单详情");
            document.title = "订单详情";
            break;
        case "login":
            $("#titlename").html("用户登录");
            document.title = "用户登录";
            break;
        case "reg":
            $("#titlename").html("用户注册");
            document.title = "用户注册";
            break;
        case "member":
            $("#titlename").html("会员中心");
            document.title = "会员中心";
            $(".icon_home").show();
            break;
        case "orderlist":
            $("#titlename").html("我的订单");
            document.title = "我的订单";
            $(".icon_home").show();
            break;
        case "fxorderlist":
            $("#titlename").html("我的分销订单");
            document.title = "我的分销订单";
            $(".icon_home").show();
            break;
        case "coupon":
            $("#titlename").html("我的优惠券");
            document.title = "我的优惠券";
            $(".icon_home").show();
            break;
        case "integral":
            $("#titlename").html("我的积分");
            document.title = "我的积分";
            $(".icon_home").show();
    }
    if ($("#" + flag + "_view").length > 0) {
        $("#" + flag + "_view").show();
        if (flag == "orderlist" || flag == "fxorderlist") scroll(scroll_top, 0);
        App.unblockUI();
    }
    else {
        var url = "";
        App.blockUI({ boxed: true });
        switch (flag) {
            default:
                return;
                break;
            case "first":
                url = "../../WeChat/AjaxService.aspx?action=OrderFirstStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                break;
            case "second":
                url = "../../WeChat/AjaxService.aspx?action=OrderSecondStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                break;
            case "third":
                url = "../../WeChat/AjaxService.aspx?action=OrderThirdStep&uid=" + $.cookie("orderuid") + "&r=" + Math.random();
                break;
            case "orderlist":
                url = "../../WeChat/AjaxService.aspx?action=OrderList" + "&pages=1&r=" + Math.random();
                break;
            case "fxorderlist":
                url = "../../WeChat/AjaxService.aspx?action=FxOrderList" + "&pages=1&r=" + Math.random();
                break;
            case "coupon":
                url = "../../WeChat/AjaxService.aspx?action=CouponList" + "&pages=1&r=" + Math.random();
                break;
            case "orderdetail":
                url = "../../WeChat/AjaxService.aspx?action=OrderDetail" + "&uid=" + detailid + "&r=" + Math.random();
                break;
            case "integral":
                url = "../../WeChat/AjaxService.aspx?action=IntegralDetail&r=" + Math.random();
                break;
        }
        //App.unblockUI();
        $.get(url, function (data) {
            //var obj = "";
            switch (flag) {
                default:
                    $("#main_view").append(data);
                    break;
                case "orderdetail":
                    obj = eval("(" + data + ")");
                    $("#main_view").append(ShowOrderDetail_New(obj));
                    //$("#main_view").append(data);
                    break;
                case "integral":
                    var obj = eval("(" + data + ")");
                    $("#main_view").append(ShowIntegralDetail(obj));
                    break;
                case "first":
                    $("#main_view").append(data);
                    //var obj = eval("(" + data + ")");
                    //$("#main_view").append(ShowOrderFirstStep_New(obj));
                    Spin();
                    CountPrice();
                    break;
                case "second":
                    $("#main_view").append(data);
                    //$("#main_view").append(ShowOrderSecondStep_New(obj));
                    DoiCheck();
                    break;
                case "orderlist":
                    //var obj = eval(data);
                    var obj = eval("(" + data + ")");
                    //$("#main_view").append(obj.content);
                    $("#main_view").append(ShowOrderList_New(obj));
                    $("#serch-next").html("点击继续加载...");
                    $("#serch-next").show();
                    //if (obj.pages > obj.pagecount) $("#serch-next").hide();
                    //$("#s_pages").val(obj.pages);
                    if (obj.pages + 1 > obj.PageCount) $("#serch-next").hide();
                    $("#s_pages").val(obj.pages + 1);
                    break;
                case "fxorderlist":
                    //var Obj = eval(data);
                    var obj = eval("(" + data + ")");
                    //$("#main_view").append(Obj.content);
                    $("#main_view").append(ShowFxOrderList_New(obj));
                    $("#serch-next").html("点击继续加载...");
                    $("#serch-next").show();
                    //if (Obj.pages > Obj.pagecount) $("#serch-next").hide();
                    //$("#s_pages").val(Obj.pages);
                    if (obj.pages + 1 > obj.PageCount) $("#serch-next").hide();
                    $("#s_pages").val(obj.pages + 1);
                    break;
                case "coupon":
                    //var Obj = eval(data);
                    //$("#main_view").append(Obj.content);
                    var obj = eval("(" + data + ")");
                    $("#main_view").append(ShowCouponList_New(obj));
                    break;
            }
            App.unblockUI();
        });
    }
}
function ShowIntegralDetail(data) {
    var html = "", nums = "";
    html += "<div class='sub_view' id='integral_view'>";
    html += "<div class='recommend_wrap'>";
    if (data.sumIntegral != "") {
        html += "<div class='price_box' style='font-size: 16px;'><span class='allprice price'>当前可用积分为：" + data.sumIntegral + "</span></div>";
    }
    if (data.rows != null) {
        $(data.rows).each(function (index, obj) {
            if (Number(obj.Childs) > 0) {
                nums = obj.Adults + "成人 " + obj.Childs + "儿童";
            }
            else {
                nums = obj.Adults + "成人";
            }
            html += "<div class='recommend_detail'>";
            html += "<div class='recommend_txt'>";
            html += "<h3>订单号：" + obj.AutoId + "</h3>";
            html += "<div>线路：" + obj.LineName + "</div>";
            html += "<div>日期：" + obj.BeginDate + "</div>";
            html += "<div>人数：" + nums + "</div>";
            html += "<div>费用：<span class='o-price'>¥" + obj.Price + "</span></div>";
            html += "<div>积分抵扣：" + obj.integral + "</div>";
            html += "</div></div>";
        });
    }
    html += "</div></div>";
    return html;
}
//function ShowOrderFirstStep_New(data) {
//    var html = "";
//    if (data.PlanStaPrice != "" && data.PlanExtPrice != "") {
//        if (data.rows != null) {
//            html += "<div class='sub_view' id='first_view'>";
//            html += "<div class='product_wrap'>";
//            html += "<h3>" + data.rows[0].LineName + "</h3>";
//            if (data.rows[0].EqualToday) {
//                html += "<p class='xdesc'><h2><i class='fa fa-calendar'></i> 出发日期: " + data.rows[0].data + "</h2></p>";
//            }
//            html += "<div class='price_box' style='font-size: 16px;'><i class='fa fa-cny'></i> <span class='allprice price'></span></div>";
//            html += "<input id='s_ordernums' type='hidden' value='" + data.rows[0].OrderNums + "'/>";
//            html += "<input id='s_adults' type='hidden' value='" + data.rows[0].Adults + "'/>";
//            html += "<input id='s_childs' type='hidden' value='" + data.rows[0].Childs + "'/>";
//            html += "</div>";
//            html += "<div class='recommend_wrap'>";
//            if (data.PlanStaPrice != null) {
//                html += "<div class='recommend_detail'>";
//                html += "<div class='recommend_txt price_01'>";
//                html += "<h3>基本费用</h3>";
//                for (var i = 0; i < data.PlanStaPrice.length; i++) {
//                    if (data.PlanStaPrice[i].PriceType == "儿童价" && data.rows[0].Childs == 0) {
//                    } else {
//                        html += "<div class='row sellprice' id='S" + data.PlanStaPrice[i].PriceId + "'>";
//                        html += "<div class='col-xs-7'>";
//                        html += "<div><span class='pricename'>" + data.PlanStaPrice[i].PriceType + "</span><span class='sprice'><i class='fa fa-cny'></i> " + data.PlanStaPrice[i].Price + "</span></div>";
//                        html += "<div class='pricememo'>" + data.PlanStaPrice[i].PriceName + "</div>";
//                        html += "</div>";
//                        html += "<div class='col-xs-5'>";
//                        html += "<div style='width:100px'>";
//                        html += "<input tps=SellPrice tagid='" + data.PlanStaPrice[i].PriceId + "' price='" + data.PlanStaPrice[i].Price + "' type='text' class='form-control touch' readonly ";
//                        switch (data.PlanStaPrice[i].PriceType) {
//                            case "成人价":
//                                html += data.AdultOption;
//                                data.AdultOption = data.DefaultOption;
//                                break;
//                            case "儿童价":
//                                html += data.ChildOption;
//                                data.ChildOption = data.DefaultChildOption;
//                                break;
//                            default:
//                                html += data.DefaultOption;
//                                break;
//                        }
//                        html += " /></div>";
//                        html += "</div></div>";
//                    }
//                }
//                html += "</div></div>";
//            }
//            if (data.PlanExtPrice != null) {
//                var ExtType = "", NeedPrice = 0, NoNeedPrice = 0;
//                for (var i = 0; i < data.PlanExtPrice.length; i++) {
//                    if (data.PlanExtPrice[i].OnlineNeeds == "0") {
//                        NoNeedPrice++;
//                    } else {
//                        NeedPrice++;
//                    }
//                }
//                if (NeedPrice > 0) {
//                    html += "<div class='recommend_detail'>";
//                    html += "<div class='recommend_txt price_02'>";
//                    html += "<h3>必选费用</h3>";
//                    for (var j = 0; j < data.PlanExtPrice.length; j++) {
//                        switch (data.PlanExtPrice[j].InfoId) {
//                            case "1":
//                                ExtType = "单房差";
//                                break;
//                            case "2":
//                                ExtType = "自费项目";
//                                break;
//                            case "3":
//                                ExtType = "小费";
//                                break;
//                            case "4":
//                                ExtType = "其他费用";
//                                break;
//                            case "5":
//                                ExtType = "保险费用";
//                                break;
//                            default:
//                                ExtType = "";
//                                break;
//                        }
//                        if (data.PlanExtPrice[j].OnlineNeeds != "0") {
//                            html += "<div class='row sellprice' id='P" + data.PlanExtPrice[j].PriceId + "'>";
//                            html += "<div class='col-xs-7'>";
//                            html += "<div><span class='pricename'>" + ExtType + "</span><span class='sprice'><i class='fa fa-cny'></i> " + data.PlanExtPrice[j].Price + "</span></div>";
//                            html += "<div class='pricememo'>" + data.PlanExtPrice[j].PriceType + " " + data.PlanExtPrice[j].PriceName + "</div>";
//                            html += "</div>";
//                            html += "<div class='col-xs-5'>";
//                            html += "<div style='width:100px'>";
//                            html += "<input tps=ExtPrice tagid='" + data.PlanExtPrice[j].PriceId + "' price='" + data.PlanExtPrice[j].Price + "' type='text' class='form-control touch' readonly ";
//                            switch (data.PlanExtPrice[j].OnlineNeeds) {
//                                case "1":
//                                    html += "value=" + Adults + " max=" + Adults + " min=" + Adults + "";
//                                    break;
//                                case "2":
//                                    html += "value=" + Childs + " max=" + Childs + " min=" + Childs + "";
//                                    break;
//                                case "3":
//                                    html += "value=" + OrderNums + " max=" + OrderNums + " min=" + OrderNums + "";
//                                    break;
//                                default:
//                                    html += "value=" + OrderNums + " max=" + OrderNums + " min=" + OrderNums + "";
//                                    break;
//                            }
//                            html += " /></div>";
//                            html += "</div></div>";
//                        }
//                    }
//                    html += "</div></div>";
//                }
//                if (NoNeedPrice > 0) {
//                    html += "<div class='recommend_detail'>";
//                    html += "<div class='recommend_txt price_03'>";
//                    html += "<h3>可选费用</h3>";
//                    for (var i = 0; i < data.PlanExtPrice.length; i++) {
//                        switch (data.PlanExtPrice[i].InfoId) {
//                            case "1":
//                                ExtType = "单房差";
//                                break;
//                            case "2":
//                                ExtType = "自费项目";
//                                break;
//                            case "3":
//                                ExtType = "小费";
//                                break;
//                            case "4":
//                                ExtType = "其他费用";
//                                break;
//                            case "5":
//                                ExtType = "保险费用";
//                                break;
//                            default:
//                                ExtType = "";
//                                break;
//                        }
//                        if (data.PlanExtPrice[i].OnlineNeeds == "0") {
//                            html += "<div class='row sellprice' id='P" + data.PlanExtPrice[i].PriceId + "'>";
//                            html += "<div class='col-xs-7'>";
//                            html += "<div><span class='pricename'>" + ExtType + "</span><span class='sprice'><i class='fa fa-cny'></i> " + data.PlanExtPrice[i].Price + "</span></div>";
//                            html += "<div class='pricememo'>" + data.PlanExtPrice[i].PriceType + " " + data.PlanExtPrice[i].PriceName + "</div>";
//                            html += "</div>";
//                            html += "<div class='col-xs-5'>";
//                            html += "<div style='width:100px'>";
//                            html += "<input tps=ExtPrice tagid='" + data.PlanExtPrice[i].PriceId + "' price='" + data.PlanExtPrice[i].Price + "' type='text' class='form-control touch' readonly value=0 max=" + data.rows[0].OrderNums + " min=0 /></div>";
//                            html += "</div></div>";
//                        }
//                    }
//                    html += "</div></div>";
//                }
//            }
//            if (data.Integral != null) {
//                html += "<div class='recommend_detail'>";
//                html += "<div class='recommend_txt price_04'>";
//                html += "<h3>积分抵扣</h3>";
//                html += "<div class='row sellprice'>";
//                html += "<div class='col-xs-7'>";
//                html += "<div><span class='pricename'>可用积分</span><span class='sprice'>" + data.Integral + "积分</span><input id='sumIntegral' type='hidden' value='" + data.Integral + "' /><input id='Integral_ratio' type='hidden' value='" + data.Integral_ratio + "' /></div>";
//                html += "</div>";
//                html += "<div class='col-xs-5'>";
//                html += "<div style='width:100px'>";
//                html += "<input id='integral' onblur='Validate()' style='border-style:none;' type='text' placeholder='输入积分(" + data.Integral_ratio + "积分=1元)' />";
//                html += "</div></div></div></div>";
//            }
//            html += "</div>";
//            html += "<div class='pre-footer order-footer'  style='position: fixed; bottom: -1px; left: 0px;width:101%'>";
//            html += "<div class='container'>";
//            html += "<div class='row'>";
//            html += "<div class='col-xs-8' style='text-align:left;'><i class='fa fa-cny'></i> <span class='allprice'>0</span></div>";
//            html += "<div class='col-xs-4' style='text-align:center'><a class='yd cur' href='javascript:;' id='ordernow'><i class='fa fa-chevron-circle-right'></i> 下一步</a></div>";
//            html += "</div></div></div></div>";
//        } else {
//            html += "<div class='sub_view info' id='first_view'>";
//            html += "订单不存在，不能继续预订！";
//            html += "</div>";
//        }
//    } else {
//        html += "<div class=\"sub_view info\" id=\"first_view\">";
//        html += "订单不存在，不能继续预订！";
//        html += "</div>";
//    }
//    return html;
//}
//function ShowOrderSecondStep_New(data) {
//    var html = "";
//    if (data.rows != null) {
//        if (data.rows[0].PriceFlag) {
//            html = "<div class='sub_view' id='second_view'>订单费用错误，不能继续预订</div>";
//            return html;
//        }
//        var preferAmount = 0;
//        if (data.preferAmount != null) {
//            preferAmount = data.preferAmount;
//        }
//        html += "<div class='sub_view' id='second_view'>";
//        html += "<div class='recommend_wrap'>";
//        html += "<div class='recommend_detail'>";
//        html += "<div class='recommend_txt'>";
//        html += "<h3>联系人信息</h3>";
//        html += "<form id='form1' onsubmit='return false;' method='post'>";
//        html += "<input maxlength='30' id='ordername' type='text' name='ordername' class='form-control ordertext' placeholder='联系人姓名' value='" + data.Online_UserName + "'>";
//        html += "<input maxlength='11' id='orderphone' type='text' name='orderphone' class='form-control ordertext' placeholder='手机号码' value='" + data.Online_WeChatPhone + "'>";
//        html += "<input maxlength='50' id='orderemail' type='text' name='orderemail' class='form-control ordertext' placeholder='电子邮件' value='" + data.Online_WeChatEmail + "'>";
//        html += "<input maxlength='100' id='ordermemo' type='text' name='ordermemo' class='form-control ordertext' placeholder='订单特别说明'>";
//        html += "<input id='Preference' type='hidden' value=''>";
//        html += "<input id='preferAmount' type='hidden' value='" + preferAmount + "'>";
//        html += "</form>";
//        html += "</div></div>";
//        html += "<div class='recommend_detail'>";
//        html += "<div class='recommend_txt'>";
//        html += "<h3 class='pay'>";
//        html += "<input value='1' type='radio' id='radio-1' name='iCheck' checked><label for='radio-1' class='hovers'>在线支付</label>&nbsp;&nbsp;&nbsp;&nbsp;";
//        html += "</h3>";
//        html += "<div id='Pre1'>";
//        html += "<div class='pre'>在线支付每人立减 0 元</div>";
//        if (preferAmount > 0) {
//            html += "<div class='pre'>早定早优惠每人立减 " + preferAmount + " 元</div>";
//        }
//        html += "<div>请于订单确认后24小时之内，通过网上支付方式付清全部费用！</div>";
//        html += "</div>";
//        html += "<select id='Pre2' class='form-control Branch' style='display:none'>";
//        html += data.PayOption + "</select></div></div></div>";
//        html += "<div class='pre-footer order-footer'  style='position: fixed; bottom: -1px; left: 0px;width:101%'>";
//        html += "<div class='container'>";
//        html += "<div class='row'>";
//        html += "<div class='col-xs-8' style='text-align:left;'>";
//        if (data.Online_UserId == "") {
//            html += "<a class='yd cur' href='javascript:;' id='loginnow'><i class='fa fa-user'></i> 注册或登录</a>";
//        }
//        html += "</div>";
//        html += "<div class='col-xs-4' style='text-align:center'><a class='yd cur' href='javascript:;' id='submitorder'><i class='fa fa-chevron-circle-right'></i> 下一步</a></div>";
//        html += "</div></div></div></div>";
//    } else {
//        html += "<div class='sub_view info' id='second_view'>订单不存在，不能继续预订！</div>";
//    }
//    return html;
//}
//function ShowOrderThirdStep_New(data) {
//    var html = "";
//    if (data.rows != null) {
//        var AdjustTime = "24";
//        if (data.rows[0].ProductType == "InLand") {
//            AdjustTime = "12";
//        }
//        html += "<div class='sub_view' id='third_view'>";
//        html += "<div class='recommend_wrap'>";
//        html += "<div class='recommend_detail'>";
//        html += "<div class='recommend_txt'>";
//        if (data.rows[0].OrderFlag == "1") {
//            html += "<h3>预订成功（已占位）</h3>";
//        }
//        if (data.rows[0].OrderFlag == "0") {
//            html += "<h3>预订成功（待确认）</h3>";
//        }
//        html += "<div style='font-size: 14px;line-height:30px'>";
//        html += "您的订单号：<span class='orderid'>" + data.rows[0].autoid + "</span><br/>";
//        html += "我们的工作人员将会尽快通过电话或者EMAIL和您联系。如果您所填写的信息有误，此订单将被取消。<br/>";
//        if (data.rows[0].OrderFlag == "1") {
//            html += "请您在<span class='orderid'>预订后" + AdjustTime + "小时</span>以内付款（紧急情况或特殊情况除外）。<br/>";
//        }
//        if (data.rows[0].OrderFlag == "0") {
//            html += "请您在<span class='orderid'>订单确认以后" + AdjustTime + "小时</span>以内付款（紧急情况或特殊情况除外）。<br/>";
//        }
//        html += "如果因特殊原因无法成行，我们也会及时通知您。";
//        html += "</div></div></div>";
//        html += "<div class='recommend_detail'>";
//        html += "<div class='recommend_txt'>";
//        html += "<h3>付款方式</h3>";
//        html += "<div style='font-size: 14px;line-height:30px'>";
//        if (data.rows[0].PayType == "1" && data.rows[0].OrderFlag == "1" && data.rows[0].PayFlag == "0") {
//            html += "<A class='btn yellow' href='/wechat/pay.aspx?OrderId=" + data.orderid + "' target='_blank'>在线支付</A>";
//        }
//        if (data.rows[0].PayType == "2") {
//            html += "<div class='infos'>" + data.BranchName + "</DIV>";
//            html += "<div class='infos'>" + data.BranchMap + "</DIV>";
//        }
//        html += "</div></div></div>";
//        html += "<div class='recommend_detail'>";
//        html += "<div class='recommend_txt'>";
//        html += "<h3>联系方式</h3>";
//        html += "<ul>";
//        html += "<li>电话：<a class='margin-right-20' href='tel:02164747595'>021-64747595、64747596</a></li>";
//        html += "<li>上海中国青年旅行社有限公司</li>";
//        html += "<li>联系地址：上海市徐汇区衡山路2号（200031）</li>";
//        html += "<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>";
//        html += "</ul>";
//        html += "</div></div></div>";
//        html += "<div class='pre-footer order-footer'  style='position: fixed; bottom: -1px; left: 0px;width:101%'>";
//        html += "<div class='container'>";
//        html += "<div class='row'>";
//        html += "<div class='col-xs-7' style='text-align:left;'></div>";
//        html += "<div class='col-xs-5' style='text-align:center'><a class='yd cur' href='app/main'><i class='fa fa-home'></i> 返回首页</a></div>";
//        html += "</div></div></div></div>";
//    } else {
//        html += "<div class='sub_view info' id='third_view'>订单不存在，不能继续预订！</div>";
//    }
//    return html;
//}
function ShowOrderList_New(data) {
    var html = "", html2 = "";
    html2 += CreateLineListString_New(data, "OrderList");
    if (data.pages == 1) {
        html += "<div class='sub_view' id='orderlist_view'>";
        html += "<div class='recommend_wrap'  id='orderlist'>";
        html += html2;
        html += "</div><div class='text-center' style='margin-bottom: 50px;margin-top: -10px;'>";
        html += "<a style='display:none;width:150px;' id='serch-next' href='javascript:;' class='btn btn-default'></a>";
        html += "</div></div>";
    } else {
        html += html2;
    }
    return html;
}
function ShowFxOrderList_New(data) {
    var html = "", html2 = "";
    html2 += CreateLineListString_New(data, "fxOrderList");
    if (data.pages == 1) {
        html += "<div class='sub_view' id='orderlist_view'>";
        html += "<div class='recommend_wrap'  id='orderlist'>";
        html += html2;
        html += "</div><div class='text-center' style='margin-bottom: 50px;margin-top: -10px;'>";
        html += "<a style='display:none;width:150px;' id='serch-next' href='javascript:;' class='btn btn-default'></a>";
        html += "</div></div>";
    } else {
        html += html2;
    }
    return html;
}
function CreateLineListString_New(data, flag) {
    var html = "";
    if (data.rowcount != 0) {
        if (data.rows != null) {
            $(data.rows).each(function (index, obj) {
                html += "<div class='recommend_detail'>";
                html += "<div class='recommend_txt'>";
                html += "<h3>订单号：" + obj.AutoId + obj.nums + "</h3>";
                html += "<div>线路：" + obj.LineName + "</div>";
                html += "<div>日期：" + obj.date + "</div>";
                html += "<div>费用：<span class='o-price'>¥" + obj.Price + "</span></div>";
                html += "<div>状态：" + obj.PhotoPath + "</div>";
                if (data.Fx_UserId.length > 0 && data.Fx_Login.length > 0) {
                    html += "<div>下单人：" + obj.OrderName + "</div>";
                    html += "<div>联系电话：<a href='tel:{0}'>" + obj.OrderMobile + "</a></div>";
                }
                html += "<a id='btnOrderDetail' class='btn yellow' href='javascript:;' tag='" + obj.OrderId + "'>查看订单</a>";
                if (Number(obj.PayFlag) == 0 && obj.OrderFlag == "1" && flag == "OrderList") {
                    html += "&nbsp;&nbsp;<a id='btnCancelOrder' class='btn yellow' href=\"javascript:CancelOrder('" + obj.OrderId + "');\">订单取消</a>";
                }
                html += "</div></div>";
            });
        }
    } else {
        html += "非常抱歉，没有找到您的订单";
    }
    return html;
}
function ShowCouponList_New(data) {
    var html = "", html2 = "";
    if (data.rowcount != 0) {
        if (data.rows != null) {
            $(data.rows).each(function (index, obj) {
                html2 += "<div class='recommend_detail'>";
                html2 += "<div class='recommend_txt'>";
                html2 += "<h3>" + obj.PhotoPath + "：" + obj.uno + "</h3>";
                html2 += "<div>有效日期：" + obj.bDate + " 至 " + obj.eDate + "</div>";
                html2 += "<div>抵用金额：<span class='o-price'>¥" + obj.par + "</span></div>";
                html2 += "<div>使用说明：" + obj.memo + "</div>";
                html2 += "</div></div>";
            });
        }
    } else {
        html2 += "非常抱歉，没有找到您的优惠券";
    }
    if (data.pages == 1) {
        html += "<div class='sub_view' id='coupon_view'>";
        html += "<div class='recommend_wrap'  id='orderlist'>";
        html += html2;
        html += "</div><div class='text-center' style='margin-bottom: 50px;margin-top: -10px;'>";
        html += "<a style='display:none;width:150px;' id='serch-next' href='javascript:;' class='btn btn-default'></a>";
        html += "</div></div>";
    } else {
        html += html2;
    }
    return html;
}
function ShowOrderDetail_New(data) {
    var html = "";
    if (data.rows != null) {
        html += "<div class='sub_view' id='orderdetail_view'>";
        html += "<div class='recommend_wrap'>";
        html += "<div class='recommend_detail'>";
        html += "<div class='recommend_txt'>";
        html += "<h3>订单号：" + data.rows[0].AutoId + "</h3>";
        html += "<div>线路：" + data.rows[0].LineName + "</div>";
        html += "<div>日期：" + data.rows[0].date + "</div>";
        html += "<div>人数：" + data.rows[0].nums + "</div>";
        html += "<div>费用：<span class='o-price'>¥" + data.rows[0].Price + "</span></div>";
        html += "<div>已付：" + data.rows[0].pay + "</div>";
        html += "<div>状态：" + data.rows[0].Pics + "</div>";
        html += "</div></div>";
        html += "<div class='recommend_detail'>";
        html += "<div class='recommend_txt'>";
        html += "<h3>联系人信息</h3>";
        html += "<h3>姓名：" + data.rows[0].OrderName + "</h3>";
        if (data.rows[0].OrderTel.length > 0) {
            html += "<h3>电话：" + data.rows[0].OrderTel + "</h3>";
        }
        if (data.rows[0].OrderMobile.length > 0) {
            html += "<h3>手机：" + data.rows[0].OrderMobile + "</h3>";
        }
        if (data.rows[0].OrderFax.length > 0) {
            html += "<h3>传真：" + data.rows[0].OrderFax + "</h3>";
        }
        if (data.rows[0].OrderEmail > 0) {
            html += "<h3>邮箱：" + data.rows[0].OrderEmail + "</h3>";
        }
        html += "<span style = 'color: #e84d1c'>客服会联系您确认游客信息</span>";
        html += "</div></div>";
        html += "<div class='recommend_detail'>";
        html += "<div class='recommend_txt'>";
        html += "<h3>费用清单</h3>";
        if (data.row1 != null) {
            $(data.row1).each(function (index, obj) {
                html += "<div class='col-price'>";
                html += "<div><span class='pricename'>" + obj.PriceName + " (" + obj.SellPrice + " × " + obj.OrderNums + ")</span><span class='sprice'><i class='fa fa-cny'></i> " + obj.SumPrice + "</span></div>";
                html += "<div class='pricememo'>" + obj.PriceMemo + "</div>";
                html += "</div>";
            });
            if (data.couponAmount != null && Number(data.couponAmount) > 0) {
                html += "<div class='col-price'>";
                html += "<div><span class='pricename'>积分抵扣 (" + Number(data.couponAmount) * Number(data.Integral_ratio) + " / " + data.Integral_ratio + ")</span><span class='sprice'><i class='fa fa-cny'></i> -" + data.couponAmount + "</span></div>";
                html += "<div class='pricememo'>" + data.Integral_ratio + "积分=1元</div>";
                html += "</div>";
            }
            html += "</div></div>";
            html += "<div class='recommend_detail'>";
            html += "<div class='recommend_txt'>";
            html += "<h3>付款方式</h3>";
            html += "<div style='font-size: 14px;line-height:30px'>";
            if (data.row1[0].PayType == "1" && data.row1[0].OrderFlag == "1" && data.row1[0].PayFlag == "0") {
                html += "<A class='btn yellow' href='/wechat/pay.aspx?OrderId=" + data.orderid + "' target='_blank'>在线支付</A>";
            }
            if (data.row1[0].PayType == "2") {
                html += "<div class='infos'>" + data.BranchName + "</DIV>";
                html += "<div class='infos'>" + data.BranchMap + "</DIV>";
            }
            html += "</div></div></div>";
            html += "<div class='recommend_detail'>";
            html += "<div class='recommend_txt'>";
            html += "<h3>联系方式</h3>";
            html += "<ul>";
            html += "<li>电话：<a class='margin-right-20' href='tel:02164747596'>021-64747596</a></li>";
            html += "<li>传真：021-64742928(出境)&nbsp;&nbsp;021-64670982(国内)</li>";
            html += "<li>上海中国青年旅行社有限公司</li>";
            html += "<li>联系地址：上海市徐汇区衡山路2号（200031）</li>";
            html += "<li>如对以上预订有任何疑问，请速给我们来电！谢谢您的预订！</li>";
            html += "</ul>";
            html += "</div></div></div></div>";
        }
    } else {
        html += "<div class='sub_view info' id='third_view'>订单不存在，不能继续预订！</div>";
    }
    return html;
}

$('#serch-next').live("click", function () {
    $("#serch-next").html("<img src='/images/ajax-loader.gif'> 努力加载中...");
    url = "../../WeChat/AjaxService.aspx?action=OrderList" + "&pages=" + $("#s_pages").val() + "&r=" + Math.random();
    App.unblockUI();
    $.get(url, function (data) {
        //var obj = eval(data);
        var obj = eval("(" + data + ")");
        $("#serch-next").html("点击继续加载...");
        $("#serch-next").show();
        //if (obj.pages > obj.pagecount) $("#serch-next").hide();
        //$("#s_pages").val(obj.pages);
        //$("#orderlist").append(obj.content);
        if (obj.pages + 1 > obj.pageCount) $("#serch-next").hide();
        $("#s_pages").val(obj.pages + 1);
        $("#orderlist").append(ShowOrderList_New(obj));
        App.unblockUI();
    });
});

function DoiCheck() { 
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-grey',
        radioClass: 'iradio_square-grey',
        increaseArea: '20%'
    });

    $('input').on('ifChecked', function (event) {
        RadioSet($(this).val());
    });
}

function RadioSet(vals) {
    if (vals == "1") {
        $("#Pre2").hide();
        $("#Pre1").show();
    }
    else {
        $("#Pre1").hide();
        $("#Pre2").show();
    }
};

function Spin() {
    $(".touch").each(function () {
        $(this).TouchSpin({
            initval: 1,
            min: $(this).attr("min"),
            max: $(this).attr("max"),
            buttondown_class: "btn quantity-down",
            buttonup_class: "btn quantity-up"
        });
        $(this).bind('change', function () {
            CountPrice();
        });
    });
}

function Validate() {
    if ($("#integral").val() != "") {
        if (Number($("#integral").val()) >= 0) {
            if (Number($("#sumIntegral").val()) < Number($("#integral").val())) {
                showmessage("积分不足");
                $("#integral").val("");
                return false;
            } else {
                if (Number($("#integral").val()) % Number($("#Integral_ratio").val()) != 0) {
                    showmessage("输入积分为" + $("#Integral_ratio").val() + "的倍数");
                    $("#integral").val("");
                    return false;
                }
                return true;
            }
        } else {
            showmessage("输入的积分格式不正确");
            return false;
        }
    }
}

function CountPrice() {
    var PriceSum = 0;
    $(".touch").each(function () {
        PriceSum += Number($(this).attr("price")) * Number($(this).val());
    });
    //$(".allprice").html(PriceSum - (Number($("#integral").val()) / 20));
    $(".allprice").html(PriceSum);
}

$('#ordernow').live("click", function () {
    //判断并提交订单
    var Nums = 0;
    var OrderNums = Number($("#s_ordernums").val());
    $(".price_01 .touch").each(function () {
        Nums += Number($(this).val());
    });
    if (Nums != OrderNums) {
        showmessage("基本费用人数不符");
        return false;
    }

    var Parms = "";
    $(".sellprice").each(function () {
        var pid = "#" + $(this).attr("id");
        if ($(pid + " .touch").val() != "0") {
            Parms += $(pid + " .touch").attr("tps") + "@@";
            Parms += $(pid + " .touch").attr("tagid") + "@@";
            Parms += $(pid + " .pricename").html() + "@@";
            Parms += $(pid + " .pricememo").html() + "@@";
            Parms += $(pid + " .touch").attr("price") + "@@";
            Parms += $(pid + " .touch").val() + "@@";
            Parms += Number($(pid + " .touch").attr("price")) * Number($(pid + " .touch").val());
            Parms += "||";
        }
    });
    Parms = Parms.substr(0, Parms.length - 2);
    $("#PriceStrings").val(Parms);
    url = "../../WeChat/AjaxService.aspx?action=OrderPrice&uid=" + $.cookie("orderuid") + "&price=" + $(".allprice").html() + "&r=" + Math.random();
    $.post(url, $("#form_data").serialize(), function (obj) {
        //var obj = eval(data);
        if (obj.success) {
            state = { action: "second", url: "#second" };
            history.pushState(state, "预订", "#second");
            LoadPage("second");
        }
        else {
            showmessage("订单提交失败");
        }
    }, 'json');
});

$('#submitorder').live("click", function () {
    url = "../../WeChat/AjaxService.aspx?action=CheckOnline&r=" + Math.random();
	$.get(url, function (obj) {
    if (obj.success) {
    if ($("#ordername").val() == "") {
        showmessage("姓名不能为空");
        return false;
    }
    var info = CheckRegPhone($("#orderphone").val());
    if (info != "") {
        showmessage(info);
        return false;
    }
    var PayType = "";
    if ($(".pay :radio:checked").val() == "1") {
        PayType = "1@" + $("#Preference").val();
    }
    else {
        PayType = "2@" + $("#Pre2").val();
    }
    url = "../../WeChat/AjaxService.aspx?action=OrderSubmit&uid=" + $.cookie("orderuid") + "&paytype=" + PayType + "&integral=" + $("#integral").val() + "&r=" + Math.random();
    $.post(url, $("#form1").serialize(), function (obj) {
        if (obj.success) {
            $.cookie("lineid", null);
            $.cookie("planid", null);
            $.cookie("plandate", null);
            if (obj.success == "ordersave") {
                state = { action: "ordersave", url: "#ordersave" };
                history.pushState(state, "预订", "#ordersave");
                LoadPage("ordersave");
            }
            if (obj.success == "OK") {
                state = { action: "third", url: "#third" };
                history.pushState(state, "预订", "#third");
                LoadPage("third");
            }
        }
        else {
            showmessage(obj.error);
        }
    }, 'json');}
    else {
        $.cookie("loginstep", "second", { expires: 30, path: '/WeChat' });
        state = { action: "login", url: "#login" };
        history.pushState(state, "用户登录", "#login");
        LoadPage("login");
    }
}, 'json');
});

$('#loginnow').live("click", function () {
    $.cookie("loginstep", "second", { expires: 30, path: '/WeChat' });
    state = { action: "login", url: "#login" };
    history.pushState(state, "登录", "#login");
    LoadPage("login");
});

$('#regnew').live("click", function () {
    state = { action: "reg", url: "#reg" };
    history.pushState(state, "注册", "#reg");
    LoadPage("reg");
});

//$('#orderlist .btn').live("click", function () {
//    App.blockUI({ boxed: true });
//    scroll_top = document.body.scrollTop;
//    if ($(this).attr("tag") != detailid) $("#orderdetail_view").remove();
//    state = { action: "orderdetail", url: "#orderdetail?" + $(this).attr("tag") };
//    history.pushState(state, "订单详情", "#orderdetail?" + $(this).attr("tag"));
//    detailid = $(this).attr("tag");
//    LoadPage("orderdetail");
//});

$("#btnOrderDetail").live("click", function () {
    App.blockUI({ boxed: true });
    scroll_top = document.body.scrollTop;
    if ($(this).attr("tag") != detailid) $("#orderdetail_view").remove();
    state = { action: "orderdetail", url: "#orderdetail?" + $(this).attr("tag") };
    history.pushState(state, "订单详情", "#orderdetail?" + $(this).attr("tag"));
    detailid = $(this).attr("tag");
    LoadPage("orderdetail");
});

function CancelOrder(orderid) {
    var url = "../../WeChat/AjaxService.aspx?action=CancelOrder&OrderId=" + orderid;
    $.getJSON(url, function (data) {
        if (data.success == "OK") {
            $("#orderlist_view").remove();
            state = { action: "orderlist", url: "#orderlist" };
            history.pushState(state, "我的订单", "#orderlist");
            LoadPage("orderlist");
        }
        else {
            showmessage(data.success);
        }
    });
}

$('#login').live("click", function () {
    var info = CheckLoginName($("#loginname").val());
    if (info != "") {
        showmessage(info);
        return false;
    }
    if ($("#loginpwd").val() == "") {
        showmessage("密码不能为空");
        return false;
    }
    if ($("#authcode").val() == "") {
        showmessage("验证码不能为空");
        return false;
    }
    $.cookie("loginusername", $("#loginname").val(), { expires: 30, path: '/WeChat' });
    url = "../../WeChat/AjaxService.aspx?action=Login&r=" + Math.random();
    $.post(url, $("#loginform").serialize(), function (obj) {
        if (obj.success) {
            switch ($.cookie("loginstep")) {
                default:
                    top.location = "/WeChat/main.aspx";
                    break;
                case "first":
                    $("#second_view").remove();
                    state = { action: "second", url: "#first" };
                    history.pushState(state, "预订", "#first");
                    LoadPage("first");
                    break;
                case "second":
                    $("#second_view").remove();
                    state = { action: "second", url: "#second" };
                    history.pushState(state, "预订", "#second");
                    LoadPage("second");
                    break;
                case "member":
                    state = { action: "member", url: "#member" };
                    history.pushState(state, "会员中心", "#member");
                    LoadPage("member");
                    break;
                case "myorder":
                    state = { action: "orderlist", url: "#orderlist" };
                    history.pushState(state, "我的订单", "#orderlist");
                    LoadPage("orderlist");
                    break;
                case "mycoupon":
                    state = { action: "coupon", url: "#coupon" };
                    history.pushState(state, "我的优惠券", "#coupon");
                    LoadPage("coupon");
                    break;
                case "myintegral":
                    state = { action: "integral", url: "#integral" };
                    history.pushState(state, "我的积分", "#integral");
                    LoadPage("integral");
            }
        }
        else {
            showmessage(obj.error);
        }
    }, 'json');
});

function CheckLoginName(str) {
    var info = "";
    if (str.length == "") info = "手机或邮件不能为空";
    if (str.indexOf("@") == -1) {
        if (str.length != 11) info = "手机或邮件填写不正确";
        var valid_char = '0123456789'
        for (i = 0; i <= str.length; i++) {
            var the_char = str.charAt(i)
            if (valid_char.indexOf(the_char) == -1) {
                info = "手机号码填写不正确";
            }
        }
    }
    return info;
}

$('#regnow').live("click", function () {
    var info = CheckRegPhone($("#regphone").val());
    if (info != "") {
        showmessage(info);
        return false;
    }
    if ($("#regpwd").val() == "") {
        showmessage("登录密码不能为空");
        return false;
    }
    if ($("#regpwd").val().length < 6) {
        showmessage("密码至少6位");
        return false;
    }
    url = "../../WeChat/AjaxService.aspx?action=Reg&r=" + Math.random();
    $.post(url, $("#regform").serialize(), function (obj) {
        if (obj.success) {
            $.cookie("loginusername", $("#regphone").val(), { expires: 30, path: '/WeChat' });
            if ($.cookie("loginstep") == "second") {
                $("#main_view").remove("#second_view");
                state = { action: "second", url: "#second" };
                history.pushState(state, "预订", "#second");
                LoadPage("second");
            }
            else {
                top.location = "/WeChat/main.aspx";
            }
        }
        else {
            showmessage(obj.error);
        }
    }, 'json');
});

function CheckRegPhone(str) {
    var info = "";
    if (str.length != 11) info = "手机号码填写不正确";
    var valid_char = '0123456789'
    for (i = 0; i <= str.length; i++) {
        var the_char = str.charAt(i)
        if (valid_char.indexOf(the_char) == -1) {
            info = "手机号码填写不正确";
        }
    }
    return info;
}

function CheckRegEmail(str) {
    var info = "";
    if (str.indexOf("@") == -1) info = "邮件填写不正确";
    return info;
}

function verc() {
    $("#Verification").click();
}

function showmessage(msg) {
    App.blockUI({ message: msg,
        boxed: true,
        textOnly: true
    });
    window.setTimeout(function () {
        App.unblockUI();
    }, 200000);
}

$(document).bind("click", function () {
    App.unblockUI();
});

$(document).live('touchstart', function () {
    App.unblockUI();
});