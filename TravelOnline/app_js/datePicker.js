(function (D) { D.fn.extend({ renderCalendar: function (P) { var X = function (Y) { return document.createElement(Y) }; P = D.extend({}, D.fn.datePicker.defaults, P); if (P.showHeader != D.dpConst.SHOW_HEADER_NONE) { var M = D(X("tr")); for (var S = Date.firstDayOfWeek; S < Date.firstDayOfWeek + 7; S++) { var H = S % 7; var R = Date.dayNames[H]; M.append(jQuery(X("th")).attr({ scope: "col", abbr: R, title: R, "class": (H == 0 || H == 6 ? "weekend" : "weekday") }).html(P.showHeader == D.dpConst.SHOW_HEADER_SHORT ? R.substr(0, 1) : R)) } } var E = D(X("table")).attr({ cellspacing: 2, className: "jCalendar" }).append((P.showHeader != D.dpConst.SHOW_HEADER_NONE ? D(X("thead")).append(M) : X("thead"))); var F = D(X("tbody")); var U = (new Date()).zeroTime(); var W = P.month == undefined ? U.getMonth() : P.month; var N = P.year || U.getFullYear(); var K = new Date(N, W, 1); var J = Date.firstDayOfWeek - K.getDay() + 1; if (J > 1) { J -= 7 } var O = Math.ceil(((-1 * J + 1) + K.getDaysInMonth()) / 7); K.addDays(J - 1); var V = function () { if (P.hoverClass) { D(this).addClass(P.hoverClass) } }; var G = function () { if (P.hoverClass) { D(this).removeClass(P.hoverClass) } }; var L = 0; while (L++ < O) { var Q = jQuery(X("tr")); for (var S = 0; S < 7; S++) { var I = K.getMonth() == W; var T = D(X("td")).text(K.getDate() + "").attr("className", (I ? "current-month " : "other-month ") + (K.isWeekend() ? "mweekend " : "mweekday ") + (I && K.getTime() == U.getTime() ? "today " : "")).hover(V, G); if (P.renderCallback) { P.renderCallback(T, K, W, N) } Q.append(T); K.addDays(1) } F.append(Q) } E.append(F); return this.each(function () { D(this).empty().append(E) }) }, datePicker: function (E) { if (!D.event._dpCache) { D.event._dpCache = [] } E = D.extend({}, D.fn.datePicker.defaults, E); return this.each(function () { var G = D(this); var I = true; if (!this._dpId) { this._dpId = D.event.guid++; D.event._dpCache[this._dpId] = new A(this); I = false } if (E.inline) { E.createButton = false; E.displayClose = false; E.closeOnSelect = false; G.empty() } var F = D.event._dpCache[this._dpId]; F.init(E); if (!I && E.createButton) { F.button = D('<a href="#" class="dp-choose-date" title="' + D.dpText.TEXT_CHOOSE_DATE + '">' + D.dpText.TEXT_CHOOSE_DATE + "</a>").bind("click", function () { G.dpDisplay(this); this.blur(); return false }); G.after(F.button) } if (!I && G.is(":text")) { G.bind("dateSelected", function (K, J, L) { this.value = J.asString() }).bind("change", function () { if (this.value != "") { var J = Date.fromString(this.value); if (J) { F.setSelected(J, true, true) } } }); if (E.clickInput) { G.bind("click", function () { G.dpDisplay() }) } var H = Date.fromString(this.value); if (this.value != "" && H) { F.setSelected(H, true, true) } } G.addClass("dp-applied") }) }, dpSetDisabled: function (E) { return B.call(this, "setDisabled", E) }, dpSetStartDate: function (E) { return B.call(this, "setStartDate", E) }, dpSetEndDate: function (E) { return B.call(this, "setEndDate", E) }, dpGetSelected: function () { var E = C(this[0]); if (E) { return E.getSelected() } return null }, dpSetSelected: function (G, F, E) { if (F == undefined) { F = true } if (E == undefined) { E = true } return B.call(this, "setSelected", Date.fromString(G), F, E, true) }, dpSetDisplayedMonth: function (E, F) { return B.call(this, "setDisplayedMonth", Number(E), Number(F), true) }, dpDisplay: function (E) { return B.call(this, "display", E) }, dpSetRenderCallback: function (E) { return B.call(this, "setRenderCallback", E) }, dpSetPosition: function (E, F) { return B.call(this, "setPosition", E, F) }, dpSetOffset: function (E, F) { return B.call(this, "setOffset", E, F) }, dpClose: function () { return B.call(this, "_closeCalendar", false, this[0]) }, _dpDestroy: function () { } }); var B = function (G, F, E, I, H) { return this.each(function () { var J = C(this); if (J) { J[G](F, E, I, H) } }) }; function A(E) { this.ele = E; this.displayedMonth = null; this.displayedYear = null; this.startDate = null; this.endDate = null; this.showYearNavigation = null; this.closeOnSelect = null; this.displayClose = null; this.selectMultiple = null; this.verticalPosition = null; this.horizontalPosition = null; this.verticalOffset = null; this.horizontalOffset = null; this.button = null; this.renderCallback = []; this.selectedDates = {}; this.inline = null; this.context = "#dp-popup" } D.extend(A.prototype, { init: function (E) { this.setStartDate(E.startDate); this.setEndDate(E.endDate); this.setDisplayedMonth(Number(E.month), Number(E.year)); this.setRenderCallback(E.renderCallback); this.showYearNavigation = E.showYearNavigation; this.closeOnSelect = E.closeOnSelect; this.displayClose = E.displayClose; this.selectMultiple = E.selectMultiple; this.verticalPosition = E.verticalPosition; this.horizontalPosition = E.horizontalPosition; this.hoverClass = E.hoverClass; this.setOffset(E.verticalOffset, E.horizontalOffset); this.inline = E.inline; if (this.inline) { this.context = this.ele; this.display() } }, setStartDate: function (E) { if (E) { this.startDate = Date.fromString(E) } if (!this.startDate) { this.startDate = (new Date()).zeroTime() } this.setDisplayedMonth(this.displayedMonth, this.displayedYear) }, setEndDate: function (E) { if (E) { this.endDate = Date.fromString(E) } if (!this.endDate) { this.endDate = (new Date("12/31/2999")) } if (this.endDate.getTime() < this.startDate.getTime()) { this.endDate = this.startDate } this.setDisplayedMonth(this.displayedMonth, this.displayedYear) }, setPosition: function (E, F) { this.verticalPosition = E; this.horizontalPosition = F }, setOffset: function (E, F) { this.verticalOffset = parseInt(E) || 0; this.horizontalOffset = parseInt(F) || 0 }, setDisabled: function (E) { $e = D(this.ele); $e[E ? "addClass" : "removeClass"]("dp-disabled"); if (this.button) { $but = D(this.button); $but[E ? "addClass" : "removeClass"]("dp-disabled"); $but.attr("title", E ? "" : D.dpText.TEXT_CHOOSE_DATE) } if ($e.is(":text")) { $e.attr("disabled", E ? "disabled" : "") } }, setDisplayedMonth: function (E, L, I) { if (this.startDate == undefined || this.endDate == undefined) { return } var H = new Date(this.startDate.getTime()); H.setDate(1); var K = new Date(this.endDate.getTime()); K.setDate(1); var G; if ((!E && !L) || (isNaN(E) && isNaN(L))) { G = new Date().zeroTime(); G.setDate(1) } else { if (isNaN(E)) { G = new Date(L, this.displayedMonth, 1) } else { if (isNaN(L)) { G = new Date(this.displayedYear, E, 1) } else { G = new Date(L, E, 1) } } } if (G.getTime() < H.getTime()) { G = H } else { if (G.getTime() > K.getTime()) { G = K } } var F = this.displayedMonth; var J = this.displayedYear; this.displayedMonth = G.getMonth(); this.displayedYear = G.getFullYear(); if (I && (this.displayedMonth != F || this.displayedYear != J)) { this._rerenderCalendar(); D(this.ele).trigger("dpMonthChanged", [this.displayedMonth, this.displayedYear]) } }, setSelected: function (K, E, F, H) { if (E == this.isSelected(K)) { return } if (this.selectMultiple == false) { this.selectedDates = {}; D("td.selected", this.context).removeClass("selected") } if (F && this.displayedMonth != K.getMonth()) { this.setDisplayedMonth(K.getMonth(), K.getFullYear(), true) } this.selectedDates[K.toString()] = E; var I = "td."; I += K.getMonth() == this.displayedMonth ? "current-month" : "other-month"; I += ':contains("' + K.getDate() + '")'; var J; D(I, this.ele).each(function () { if (D(this).text() == K.getDate()) { J = D(this); J[E ? "addClass" : "removeClass"]("selected") } }); if (H) { var G = this.isSelected(K); $e = D(this.ele); $e.trigger("dateSelected", [K, J, G]); $e.trigger("change") } }, isSelected: function (E) { return this.selectedDates[E.toString()] }, getSelected: function () { var E = []; for (s in this.selectedDates) { if (this.selectedDates[s] == true) { E.push(Date.parse(s)) } } return E }, display: function (E) { if (D(this.ele).is(".dp-disabled")) { return } E = E || this.ele; var L = this; var H = D(E); var K = H.offset(); var M; var N; var G; var I; if (L.inline) { M = D(this.ele); N = { id: "calendar-" + this.ele._dpId, className: "dp-popup dp-popup-inline" }; I = {} } else { M = D("body"); N = { id: "dp-popup", className: "dp-popup" }; I = { top: K.top + L.verticalOffset, left: K.left + L.horizontalOffset }; var J = function (Q) { var O = Q.target; var P = D("#dp-popup")[0]; while (true) { if (O == P) { return true } else { if (O == document) { L._closeCalendar(); return false } else { O = D(O).parent()[0] } } } }; this._checkMouse = J; this._closeCalendar(true) } M.append(D("<div></div>").attr(N).css(I).append(D("<h2></h2>"), D('<div class="dp-nav-prev"></div>').append(D('<a class="dp-nav-prev-year" href="#" title="' + D.dpText.TEXT_PREV_YEAR + '">&lt;&lt;</a>').bind("click", function () { return L._displayNewMonth.call(L, this, 0, -1) }), D('<a class="dp-nav-prev-month" href="#" title="' + D.dpText.TEXT_PREV_MONTH + '">&lt;</a>').bind("click", function () { return L._displayNewMonth.call(L, this, -1, 0) })), D('<div class="dp-nav-next"></div>').append(D('<a class="dp-nav-next-year" href="#" title="' + D.dpText.TEXT_NEXT_YEAR + '">&gt;&gt;</a>').bind("click", function () { return L._displayNewMonth.call(L, this, 0, 1) }), D('<a class="dp-nav-next-month" href="#" title="' + D.dpText.TEXT_NEXT_MONTH + '">&gt;</a>').bind("click", function () { return L._displayNewMonth.call(L, this, 1, 0) })), D("<div></div>").attr("className", "dp-calendar")).bgIframe()); var F = this.inline ? D(".dp-popup", this.context) : D("#dp-popup"); if (this.showYearNavigation == false) { D(".dp-nav-prev-year, .dp-nav-next-year", L.context).css("display", "none") } if (this.displayClose) { F.append(D('<a href="#" id="dp-close">' + D.dpText.TEXT_CLOSE + "</a>").bind("click", function () { L._closeCalendar(); return false })) } L._renderCalendar(); D(this.ele).trigger("dpDisplayed", F); if (!L.inline) { if (this.verticalPosition == D.dpConst.POS_BOTTOM) { F.css("top", K.top + H.height() - F.height() + L.verticalOffset) } if (this.horizontalPosition == D.dpConst.POS_RIGHT) { F.css("left", K.left + H.width() - F.width() + L.horizontalOffset) } D(document).bind("mousedown", this._checkMouse) } }, setRenderCallback: function (E) { if (E == null) { return } if (E && typeof (E) == "function") { E = [E] } this.renderCallback = this.renderCallback.concat(E) }, cellRender: function (J, E, H, G) { var K = this.dpController; var I = new Date(E.getTime()); J.bind("click", function () { var L = D(this); if (!L.is(".disabled")) { K.setSelected(I, !L.is(".selected") || !K.selectMultiple, false, true); if (K.closeOnSelect) { K._closeCalendar() } } }); if (K.isSelected(I)) { J.addClass("selected") } for (var F = 0; F < K.renderCallback.length; F++) { K.renderCallback[F].apply(this, arguments) } }, _displayNewMonth: function (F, E, G) { if (!D(F).is(".disabled")) { this.setDisplayedMonth(this.displayedMonth + E, this.displayedYear + G, true) } F.blur(); return false }, _rerenderCalendar: function () { this._clearCalendar(); this._renderCalendar() }, _renderCalendar: function () { D("h2", this.context).html(Date.monthNames[this.displayedMonth] + " " + this.displayedYear); D(".dp-calendar", this.context).renderCalendar({ month: this.displayedMonth, year: this.displayedYear, renderCallback: this.cellRender, dpController: this, hoverClass: this.hoverClass }); if (this.displayedYear == this.startDate.getFullYear() && this.displayedMonth == this.startDate.getMonth()) { D(".dp-nav-prev-year", this.context).addClass("disabled"); D(".dp-nav-prev-month", this.context).addClass("disabled"); D(".dp-calendar td.other-month", this.context).each(function () { var H = D(this); if (Number(H.text()) > 20) { H.addClass("disabled") } }); var G = this.startDate.getDate(); D(".dp-calendar td.current-month", this.context).each(function () { var H = D(this); if (Number(H.text()) < G) { H.addClass("disabled") } }) } else { D(".dp-nav-prev-year", this.context).removeClass("disabled"); D(".dp-nav-prev-month", this.context).removeClass("disabled"); var G = this.startDate.getDate(); if (G > 20) { var F = new Date(this.startDate.getTime()); F.addMonths(1); if (this.displayedYear == F.getFullYear() && this.displayedMonth == F.getMonth()) { D("dp-calendar td.other-month", this.context).each(function () { var H = D(this); if (Number(H.text()) < G) { H.addClass("disabled") } }) } } } if (this.displayedYear == this.endDate.getFullYear() && this.displayedMonth == this.endDate.getMonth()) { D(".dp-nav-next-year", this.context).addClass("disabled"); D(".dp-nav-next-month", this.context).addClass("disabled"); D(".dp-calendar td.other-month", this.context).each(function () { var H = D(this); if (Number(H.text()) < 14) { H.addClass("disabled") } }); var G = this.endDate.getDate(); D(".dp-calendar td.current-month", this.context).each(function () { var H = D(this); if (Number(H.text()) > G) { H.addClass("disabled") } }) } else { D(".dp-nav-next-year", this.context).removeClass("disabled"); D(".dp-nav-next-month", this.context).removeClass("disabled"); var G = this.endDate.getDate(); if (G < 13) { var E = new Date(this.endDate.getTime()); E.addMonths(-1); if (this.displayedYear == E.getFullYear() && this.displayedMonth == E.getMonth()) { D(".dp-calendar td.other-month", this.context).each(function () { var H = D(this); if (Number(H.text()) > G) { H.addClass("disabled") } }) } } } }, _closeCalendar: function (E, F) { if (!F || F == this.ele) { D(document).unbind("mousedown", this._checkMouse); this._clearCalendar(); D("#dp-popup a").unbind(); D("#dp-popup").empty().remove(); if (!E) { D(this.ele).trigger("dpClosed", [this.getSelected()]) } } }, _clearCalendar: function () { D(".dp-calendar td", this.context).unbind(); D(".dp-calendar", this.context).empty() } }); D.dpConst = { SHOW_HEADER_NONE: 0, SHOW_HEADER_SHORT: 1, SHOW_HEADER_LONG: 2, POS_TOP: 0, POS_BOTTOM: 1, POS_LEFT: 0, POS_RIGHT: 1 }; D.dpText = { TEXT_PREV_YEAR: "Previous year", TEXT_PREV_MONTH: "Previous month", TEXT_NEXT_YEAR: "Next year", TEXT_NEXT_MONTH: "Next month", TEXT_CLOSE: "Close", TEXT_CHOOSE_DATE: "Choose date" }; D.dpVersion = "$Id: jquery.datePicker.js 15 2008-12-17 04:40:18Z kelvin.luck $"; D.fn.datePicker.defaults = { month: undefined, year: undefined, showHeader: D.dpConst.SHOW_HEADER_SHORT, startDate: undefined, endDate: undefined, inline: false, renderCallback: null, createButton: true, showYearNavigation: true, closeOnSelect: true, displayClose: false, selectMultiple: false, clickInput: false, verticalPosition: D.dpConst.POS_TOP, horizontalPosition: D.dpConst.POS_LEFT, verticalOffset: 0, horizontalOffset: 0, hoverClass: "dp-hover" }; function C(E) { if (E._dpId) { return D.event._dpCache[E._dpId] } return false } if (D.fn.bgIframe == undefined) { D.fn.bgIframe = function () { return this } } D(window).bind("unload", function () { var F = D.event._dpCache || []; for (var E in F) { D(F[E].ele)._dpDestroy() } }) })(jQuery);

/*
 * Date prototype extensions. Doesn't depend on any
 * other code. Doens't overwrite existing methods.
 *
 * Adds dayNames, abbrDayNames, monthNames and abbrMonthNames static properties and isLeapYear,
 * isWeekend, isWeekDay, getDaysInMonth, getDayName, getMonthName, getDayOfYear, getWeekOfYear,
 * setDayOfYear, addYears, addMonths, addDays, addHours, addMinutes, addSeconds methods
 *
 * Copyright (c) 2006 Jörn Zaefferer and Brandon Aaron (brandon.aaron@gmail.com || http://brandonaaron.net)
 *
 * Additional methods and properties added by Kelvin Luck: firstDayOfWeek, dateFormat, zeroTime, asString, fromString -
 * I've added my name to these methods so you know who to blame if they are broken!
 * 
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 */

/**
 * An Array of day names starting with Sunday.
 * 
 * @example dayNames[0]
 * @result 'Sunday'
 *
 * @name dayNames
 * @type Array
 * @cat Plugins/Methods/Date
 */
Date.dayNames = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

/**
 * An Array of abbreviated day names starting with Sun.
 * 
 * @example abbrDayNames[0]
 * @result 'Sun'
 *
 * @name abbrDayNames
 * @type Array
 * @cat Plugins/Methods/Date
 */
Date.abbrDayNames = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];

/**
 * An Array of month names starting with Janurary.
 * 
 * @example monthNames[0]
 * @result 'January'
 *
 * @name monthNames
 * @type Array
 * @cat Plugins/Methods/Date
 */
Date.monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

/**
 * An Array of abbreviated month names starting with Jan.
 * 
 * @example abbrMonthNames[0]
 * @result 'Jan'
 *
 * @name monthNames
 * @type Array
 * @cat Plugins/Methods/Date
 */
Date.abbrMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

/**
 * The first day of the week for this locale.
 *
 * @name firstDayOfWeek
 * @type Number
 * @cat Plugins/Methods/Date
 * @author Kelvin Luck
 */
Date.firstDayOfWeek = 1;

/**
 * The format that string dates should be represented as (e.g. 'dd/mm/yyyy' for UK, 'mm/dd/yyyy' for US, 'yyyy-mm-dd' for Unicode etc).
 *
 * @name format
 * @type String
 * @cat Plugins/Methods/Date
 * @author Kelvin Luck
 */
Date.format = 'dd/mm/yyyy';
//Date.format = 'mm/dd/yyyy';
//Date.format = 'yyyy-mm-dd';
//Date.format = 'dd mmm yy';

/**
 * The first two numbers in the century to be used when decoding a two digit year. Since a two digit year is ambiguous (and date.setYear
 * only works with numbers < 99 and so doesn't allow you to set years after 2000) we need to use this to disambiguate the two digit year codes.
 *
 * @name format
 * @type String
 * @cat Plugins/Methods/Date
 * @author Kelvin Luck
 */
Date.fullYearStart = '20';

(function() {

	/**
	 * Adds a given method under the given name 
	 * to the Date prototype if it doesn't
	 * currently exist.
	 *
	 * @private
	 */
	function add(name, method) {
		if( !Date.prototype[name] ) {
			Date.prototype[name] = method;
		}
	};
	
	/**
	 * Checks if the year is a leap year.
	 *
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.isLeapYear();
	 * @result true
	 *
	 * @name isLeapYear
	 * @type Boolean
	 * @cat Plugins/Methods/Date
	 */
	add("isLeapYear", function() {
		var y = this.getFullYear();
		return (y%4==0 && y%100!=0) || y%400==0;
	});
	
	/**
	 * Checks if the day is a weekend day (Sat or Sun).
	 *
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.isWeekend();
	 * @result false
	 *
	 * @name isWeekend
	 * @type Boolean
	 * @cat Plugins/Methods/Date
	 */
	add("isWeekend", function() {
		return this.getDay()==0 || this.getDay()==6;
	});
	
	/**
	 * Check if the day is a day of the week (Mon-Fri)
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.isWeekDay();
	 * @result false
	 * 
	 * @name isWeekDay
	 * @type Boolean
	 * @cat Plugins/Methods/Date
	 */
	add("isWeekDay", function() {
		return !this.isWeekend();
	});
	
	/**
	 * Gets the number of days in the month.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getDaysInMonth();
	 * @result 31
	 * 
	 * @name getDaysInMonth
	 * @type Number
	 * @cat Plugins/Methods/Date
	 */
	add("getDaysInMonth", function() {
		return [31,(this.isLeapYear() ? 29:28),31,30,31,30,31,31,30,31,30,31][this.getMonth()];
	});
	
	/**
	 * Gets the name of the day.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getDayName();
	 * @result 'Saturday'
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getDayName(true);
	 * @result 'Sat'
	 * 
	 * @param abbreviated Boolean When set to true the name will be abbreviated.
	 * @name getDayName
	 * @type String
	 * @cat Plugins/Methods/Date
	 */
	add("getDayName", function(abbreviated) {
		return abbreviated ? Date.abbrDayNames[this.getDay()] : Date.dayNames[this.getDay()];
	});

	/**
	 * Gets the name of the month.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getMonthName();
	 * @result 'Janurary'
	 *
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getMonthName(true);
	 * @result 'Jan'
	 * 
	 * @param abbreviated Boolean When set to true the name will be abbreviated.
	 * @name getDayName
	 * @type String
	 * @cat Plugins/Methods/Date
	 */
	add("getMonthName", function(abbreviated) {
		return abbreviated ? Date.abbrMonthNames[this.getMonth()] : Date.monthNames[this.getMonth()];
	});

	/**
	 * Get the number of the day of the year.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getDayOfYear();
	 * @result 11
	 * 
	 * @name getDayOfYear
	 * @type Number
	 * @cat Plugins/Methods/Date
	 */
	add("getDayOfYear", function() {
		var tmpdtm = new Date("1/1/" + this.getFullYear());
		return Math.floor((this.getTime() - tmpdtm.getTime()) / 86400000);
	});
	
	/**
	 * Get the number of the week of the year.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.getWeekOfYear();
	 * @result 2
	 * 
	 * @name getWeekOfYear
	 * @type Number
	 * @cat Plugins/Methods/Date
	 */
	add("getWeekOfYear", function() {
		return Math.ceil(this.getDayOfYear() / 7);
	});

	/**
	 * Set the day of the year.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.setDayOfYear(1);
	 * dtm.toString();
	 * @result 'Tue Jan 01 2008 00:00:00'
	 * 
	 * @name setDayOfYear
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("setDayOfYear", function(day) {
		this.setMonth(0);
		this.setDate(day);
		return this;
	});
	
	/**
	 * Add a number of years to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addYears(1);
	 * dtm.toString();
	 * @result 'Mon Jan 12 2009 00:00:00'
	 * 
	 * @name addYears
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addYears", function(num) {
		this.setFullYear(this.getFullYear() + num);
		return this;
	});
	
	/**
	 * Add a number of months to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addMonths(1);
	 * dtm.toString();
	 * @result 'Tue Feb 12 2008 00:00:00'
	 * 
	 * @name addMonths
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addMonths", function(num) {
		var tmpdtm = this.getDate();
		
		this.setMonth(this.getMonth() + num);
		
		if (tmpdtm > this.getDate())
			this.addDays(-this.getDate());
		
		return this;
	});
	
	/**
	 * Add a number of days to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addDays(1);
	 * dtm.toString();
	 * @result 'Sun Jan 13 2008 00:00:00'
	 * 
	 * @name addDays
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addDays", function(num) {
		//this.setDate(this.getDate() + num);
		this.setTime(this.getTime() + (num*86400000) );
		return this;
	});
	
	/**
	 * Add a number of hours to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addHours(24);
	 * dtm.toString();
	 * @result 'Sun Jan 13 2008 00:00:00'
	 * 
	 * @name addHours
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addHours", function(num) {
		this.setHours(this.getHours() + num);
		return this;
	});

	/**
	 * Add a number of minutes to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addMinutes(60);
	 * dtm.toString();
	 * @result 'Sat Jan 12 2008 01:00:00'
	 * 
	 * @name addMinutes
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addMinutes", function(num) {
		this.setMinutes(this.getMinutes() + num);
		return this;
	});
	
	/**
	 * Add a number of seconds to the date object.
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.addSeconds(60);
	 * dtm.toString();
	 * @result 'Sat Jan 12 2008 00:01:00'
	 * 
	 * @name addSeconds
	 * @type Date
	 * @cat Plugins/Methods/Date
	 */
	add("addSeconds", function(num) {
		this.setSeconds(this.getSeconds() + num);
		return this;
	});
	
	/**
	 * Sets the time component of this Date to zero for cleaner, easier comparison of dates where time is not relevant.
	 * 
	 * @example var dtm = new Date();
	 * dtm.zeroTime();
	 * dtm.toString();
	 * @result 'Sat Jan 12 2008 00:01:00'
	 * 
	 * @name zeroTime
	 * @type Date
	 * @cat Plugins/Methods/Date
	 * @author Kelvin Luck
	 */
	add("zeroTime", function() {
		this.setMilliseconds(0);
		this.setSeconds(0);
		this.setMinutes(0);
		this.setHours(0);
		return this;
	});
	
	/**
	 * Returns a string representation of the date object according to Date.format.
	 * (Date.toString may be used in other places so I purposefully didn't overwrite it)
	 * 
	 * @example var dtm = new Date("01/12/2008");
	 * dtm.asString();
	 * @result '12/01/2008' // (where Date.format == 'dd/mm/yyyy'
	 * 
	 * @name asString
	 * @type Date
	 * @cat Plugins/Methods/Date
	 * @author Kelvin Luck
	 */
	add("asString", function(format) {
		var r = format || Date.format;
		if (r.split('mm').length>1) { // ugly workaround to make sure we don't replace the m's in e.g. noveMber
			r = r.split('mmmm').join(this.getMonthName(false))
				.split('mmm').join(this.getMonthName(true))
				.split('mm').join(_zeroPad(this.getMonth()+1))
		} else {
			r = r.split('m').join(this.getMonth()+1);
		}
		r = r.split('yyyy').join(this.getFullYear())
			.split('yy').join((this.getFullYear() + '').substring(2))
			.split('dd').join(_zeroPad(this.getDate()))
			.split('d').join(this.getDate());
		return r;
	});
	
	/**
	 * Returns a new date object created from the passed String according to Date.format or false if the attempt to do this results in an invalid date object
	 * (We can't simple use Date.parse as it's not aware of locale and I chose not to overwrite it incase it's functionality is being relied on elsewhere)
	 *
	 * @example var dtm = Date.fromString("12/01/2008");
	 * dtm.toString();
	 * @result 'Sat Jan 12 2008 00:00:00' // (where Date.format == 'dd/mm/yyyy'
	 * 
	 * @name fromString
	 * @type Date
	 * @cat Plugins/Methods/Date
	 * @author Kelvin Luck
	 */
	Date.fromString = function(s)
	{
		var f = Date.format;
		
		var d = new Date('01/01/1970');
		
		if (s == '') return d;

		s = s.toLowerCase();
		var matcher = '';
		var order = [];
		var r = /(dd?d?|mm?m?|yy?yy?)+([^(m|d|y)])?/g;
		var results;
		while ((results = r.exec(f)) != null)
		{
			switch (results[1]) {
				case 'd':
				case 'dd':
				case 'm':
				case 'mm':
				case 'yy':
				case 'yyyy':
					matcher += '(\\d+\\d?\\d?\\d?)+';
					order.push(results[1].substr(0, 1));
					break;
				case 'mmm':
					matcher += '([a-z]{3})';
					order.push('M');
					break;
			}
			if (results[2]) {
				matcher += results[2];
			}
			
		}
		var dm = new RegExp(matcher);
		var result = s.match(dm);
		for (var i=0; i<order.length; i++) {
			var res = result[i+1];
			switch(order[i]) {
				case 'd':
					d.setDate(res);
					break;
				case 'm':
					d.setMonth(Number(res)-1);
					break;
				case 'M':
					for (var j=0; j<Date.abbrMonthNames.length; j++) {
						if (Date.abbrMonthNames[j].toLowerCase() == res) break;
					}
					d.setMonth(j);
					break;
				case 'y':
					d.setYear(res);
					break;
			}
		}

		return d;
	};
	
	// utility method
	var _zeroPad = function(num) {
		var s = '0'+num;
		return s.substring(s.length-2)
		//return ('0'+num).substring(-2); // doesn't work on IE :(
	};
	
})();


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
                            var price = 0;
                            var content = "";
                            var currentDay = $td.text();
                            if (planDay.content == "已满") {
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + planDay.price + "</span><br/><span class='route_5'>已满</span>"; //title='" + planDay.planid + "'
                            }
                            else {
                                if (typeof (Group_Date) != 'undefined' && thisDate.asString("yyyy-mm-dd") == Group_Date) {
                                    price = parseFloat(planDay.price) - parseFloat(Group_Discount);
                                    content = "参团立减";
                                } else {
                                    price = planDay.price;
                                    content = planDay.content;
                                }
                                var hrefString = "<a href='javascript:void(0);'>" + currentDay + "</a><br/><span class='route_1'>￥" + price + "</span><br/><span class='route_2'>" + content + "</span>"; //title='" + planDay.planid + "'
                                
                            }
//                            if (planDay.route != "-1") {
//                                if (planDay.route == "0") {
//                                    hrefString += "<br><div class=pa><a class=pa onclick='showroute()' target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'><span class='route_3'>行程打印</span></a></div>";
//                                }
//                                else {
//                                    hrefString += "<br><div class=pa><a onclick='showroute()' target=_blank href='/Purchase/ShowRoute.aspx?id=" + planDay.planid + "'><span class='route_4'>行程打印</span></a></div>";
//                                }
//                            }
                            $td.attr({ "tag": planDay.planid });
                            $td.attr({ "date": thisDate.asString("yyyy-mm-dd") });
                            $td.attr({ "price": price });
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
                var lastTitleDate = new Date(year, month, lastday).addMonths(i+1);
                //var currentTitleDate = new Date(year, month, day);

                $(this).append("<div class='calendarPanel'></div>");
                var currentPanel = $(this).find(".calendarPanel:eq(" + i + ")");
                $(currentPanel).append("<div class='monthTitle'></div>");
                $(currentPanel).find(".monthTitle").append("<table class='monthTable'><tr></tr></table>");
                var currentMonthTitle = $(currentPanel).find(".monthTitle > .monthTable tr");
//                if (currentTitleDate > defaultDate) {
//                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
//                        $(currentMonthTitle).append("<td id='prevMonth' class='prevMonth'><a href='javascript:void(0);' title='上一个月'><img src=\"/images/mbi_003.gif\">上月</a></td>");
//                    }
//                }
//                else {
//                    if (s.showPrevMonthButton && i == 0 && !s.showPrevMonthPanel) {
//                        $(currentMonthTitle).append("<td class='prevMonth'><img src=\"/images/mbi_004.gif\">上月</td>");
//                    }
//                }

                $(currentMonthTitle).append("<td class='monthTitle'>" + currentTitleDate.asString("yyyy年mm月") + "</td>");

//                if (lastTitleDate < PlanEndDate) {
//                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
//                        $(currentMonthTitle).append("<td id='nextMonth' class='nextMonth'><a href='javascript:void(0);' title='下一个月'>下月<img src=\"/images/mbi_005.gif\"></a></td>");
//                    }
//                }
//                else {
//                    if (s.showNextMonthButton && i == s.showNum - 1 && !s.showNextMonthPanel) {
//                        $(currentMonthTitle).append("<td class='nextMonth'>下月<img src=\"/images/mbi_006.gif\"></td>");
//                    }
//                }
                //alert(currentTitleDate.asString("yyyy-mm-dd") + " / " + lastTitleDate.asString("yyyy-mm-dd") + " / " + PlanEndDate.asString("yyyy-mm-dd"))

                $(currentPanel).append("<div id='showCalendarPanel" + i + "' class='showCalendarPanel' ></div>");
                $("#showCalendarPanel" + i).renderCalendar({ month: currentTitleDate.getMonth(), year: currentTitleDate.getFullYear(), renderCallback: myranderCallback });
            }

//            var prevButton = s.showPrevMonthPanel ? $(s.showPrevMonthPanel) : $(this).find("#prevMonth");
//            var nextButton = s.showNextMonthPanel ? $(s.showNextMonthPanel) : $(this).find("#nextMonth");
//            $(prevButton).bind('click', function () {
//                var prevMonthDate = currentDate.addMonths(-ShowMonthNum);
//                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: prevMonthDate.getFullYear(), month: prevMonthDate.getMonth() });
//                initTips();
//            });

//            $(nextButton).bind('click', function () {
//                var nextMonthDate = currentDate.addMonths(ShowMonthNum);
//                $(currentCalenderPanel).showRenderCalendar({ events: s.events, showNum: ShowMonthNum, year: nextMonthDate.getFullYear(), month: nextMonthDate.getMonth() });
//                initTips();
//            });
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


var initTips = function () {
    $("#plandate .hasEvent").bind("click", function () {
        //        $("#select_plandate").html($(this).attr("date") + "  " + $(this).attr("price") + "元起");
        //        $("#select_plandate").attr("tag", $(this).attr("tag"));
        //        $("#select_plandate").attr("date", $(this).attr("date"));
        //        Order();
        alert($(this).attr("date"));
    });
};