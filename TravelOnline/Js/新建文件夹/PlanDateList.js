(function ($) {
    $.fn.extend({
        showRenderCalendar: function (s) {
            var currentCalenderPanel = $(this);
            s = $.extend({}, $.fn.params, s);
            Date.dayNames = ['日', '一', '二', '三', '四', '五', '六'];
            var eventsDict = {};
            var arr = defaultStartDate.split("-");
            var endarr = defaultEndDate.split("-");
            var defaultDate = new Date(arr[0], arr[1] - 1, arr[2]);
            var PlanEndDate = new Date(endarr[0], endarr[1] - 1, endarr[2]);
            var today = new Date();
            if (defaultDate < today) {
                defaultDate = new Date();
            }

            var month = s.month >= 0 ? s.month : defaultDate.getMonth();
            var year = s.year ? s.year : defaultDate.getFullYear();
            var day = s.day ? s.day : defaultDate.getDate();
            var lastday = 1;
            var currentDate = new Date(year, month, day);
            if (s.events != null) {

                for (var i = 0; i < s.events.length; i++) {
                    eventsDict[s.events[i].date] = { "planid": s.events[i].planid, "date": s.events[i].date, "price": s.events[i].price, "content": s.events[i].content, "route": s.events[i].route };
                }
            }

            var myranderCallback = function ($td, thisDate, month, year) {
                if (thisDate.getMonth() != month) {
                    $td.html("&nbsp;");
                }
                var planDay = eventsDict[thisDate.asString("yyyy-mm-dd")];
                if (planDay) {
                    if (thisDate > today) {
                        if (thisDate.getMonth() == month) {
                            $td.addClass(s.hoverClass);
                            var currentDay = $td.text();
                            if (planDay.content == "已满") {
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + planDay.price + "</span><br/><span class='route_5'>已满</span>"; //title='" + planDay.planid + "'
                            }
                            else {
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + planDay.price + "</span><br/><span class='route_2'>" + planDay.content + "</span>"; //title='" + planDay.planid + "'
                            }
                            if (planDay.route != "-1") {

                                if (planDay.route == "0") {
                                    hrefString += "<br><span class='route_3'><a target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'>行程打印</a></span>";
                                }
                                else {
                                    hrefString += "<br><span class='route_4'><a target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'>行程打印</a></span>";
                                }
                            }
                            $td.attr({ "tag": planDay.planid });
                            $td.html(hrefString);
                        }
                        else {
                            $td.html("&nbsp;");
                        }
                    }
                }
                if (thisDate <= today) {
                    $td.addClass(s.outClass);
                }
            }

            $(currentCalenderPanel).empty();

            for (var i = 0; i < s.showNum; i++) {
                var currentTitleDate = new Date(year, month, day).addMonths(i);
                var lastTitleDate = new Date(year, month, lastday).addMonths(1);
                //var currentTitleDate = new Date(year, month, day);

                $(this).append("<div class='calendarPanel'></div>");
                var currentPanel = $(this).find(".calendarPanel:eq(" + i + ")");
                $(currentPanel).append("<div class='monthTitle'></div>");
                $(currentPanel).find(".monthTitle").append("<table class='monthTable'><tr></tr></table>");
                var currentMonthTitle = $(currentPanel).find(".monthTitle > .monthTable tr");
                if (currentTitleDate > defaultDate) {
                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
                        $(currentMonthTitle).append("<td id='prevMonth' class='prevMonth'><a href='javascript:void(0);' title='上一个月'><img src=\"/images/mbi_003.gif\">上月</a></td>");
                    }
                }
                else {
                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
                        $(currentMonthTitle).append("<td class='prevMonth'><img src=\"/images/mbi_004.gif\">上月</td>");
                    }
                }

                $(currentMonthTitle).append("<td class='monthTitle'>" + currentTitleDate.asString("yyyy年mm月") + "</td>");

                if (lastTitleDate < PlanEndDate) {
                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
                        $(currentMonthTitle).append("<td id='nextMonth' class='nextMonth'><a href='javascript:void(0);' title='下一个月'>下月<img src=\"/images/mbi_005.gif\"></a></td>");
                    }
                }
                else {
                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
                        $(currentMonthTitle).append("<td class='nextMonth'>下月<img src=\"/images/mbi_006.gif\"></td>");
                    }
                }
                //alert(currentTitleDate.asString("yyyy-mm-dd") + " / " + lastTitleDate.asString("yyyy-mm-dd") + " / " + PlanEndDate.asString("yyyy-mm-dd"))
                
                $(currentPanel).append("<div id='showCalendarPanel" + i + "' class='showCalendarPanel' ></div>");
                $("#showCalendarPanel" + i).renderCalendar({ month: currentTitleDate.getMonth(), year: currentTitleDate.getFullYear(), renderCallback: myranderCallback });
            }

            var prevButton = s.showPrevMonthPanel ? $(s.showPrevMonthPanel) : $(this).find("#prevMonth");
            var nextButton = s.showNextMonthPanel ? $(s.showNextMonthPanel) : $(this).find("#nextMonth");
            $(prevButton).bind('click', function () {
                var prevMonthDate = currentDate.addMonths(-ShowMonthNum);
                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: prevMonthDate.getFullYear(), month: prevMonthDate.getMonth() });
                initTips();
            });

            $(nextButton).bind('click', function () {
                var nextMonthDate = currentDate.addMonths(ShowMonthNum);
                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: nextMonthDate.getFullYear(), month: nextMonthDate.getMonth() });
                initTips();
            });
        }
    });

    $.fn.params = {
        showNum: 1,
        showMonthTitle: true,
        showNextMonthButton: true,
        showPrevMonthButton: true,
        showNextMonthPanel: undefined,
        showPrevMonthPanel: undefined,
        month: undefined,
        year: undefined,
        day: undefined,
        hoverClass: 'hasEvent',
        outClass: 'outdays',
        events: null
    };
})(jQuery);

//var hrefList;
//var plan = {};



var initTips = function () {
    $("#plandate .hasEvent").bind("click", function () {
        $("#PlanDateSelect1").val($(this).attr("tag"));
        GoToOrder();
    });
};

var CloseToolTip = function() {
    //$("#plantooltip").hide();
};

function PlanDropDownListBind()
{
	var newtemp = eval(json);
	var newtemp_="";//"<option value=\"\">请选择出发日期</option>";
	var its = 0;
    var today = new Date();
    
    
	for (var n = 0; n<newtemp.length; n++) {
		var arr = newtemp[n].date.split("-");
		var defaultDate = new Date(arr[0], arr[1] - 1, arr[2]);
		if (defaultDate > today) {
		    if (newtemp[n].content != "已满") {
		        newtemp_ += "<a tag=" + newtemp[n].planid + ">" + newtemp[n].date + "  " + newtemp[n].price + "元起</a>"
		        if (its == 0) {
		            $("#select_plandate").html(newtemp[n].date + "  " + newtemp[n].price + "元起");
		            $("#select_plandate").attr("tag", newtemp[n].planid);
		            its = 1;
		        }
            }
		}
    
	}
    $("#plandate_droplist").html(newtemp_);
}




