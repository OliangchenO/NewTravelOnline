﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="TravelOnline.Travel.Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
     <div id="showdatebb" class="plan_date">
     <div class="calendarPanel">
     <div class="monthTitle">
     <table class="monthTable">
     <tbody>
     <tr><td class="prevMonth"><a href="javascript:void(0);" title="上一个月"><img src="./本州常规双温泉6日游_files/mbi_003.gif"></a></td><td class="monthTitle">2011年07月</td></tr>
     </tbody>
     </table>
     </div><div id="showCalendarPanel0" class="showCalendarPanel"><table cellspacing="2" class="jCalendar" border="0"><thead><tr><th scope="col" abbr="一" title="一" class="weekday">一</th><th scope="col" abbr="二" title="二" class="weekday">二</th><th scope="col" abbr="三" title="三" class="weekday">三</th><th scope="col" abbr="四" title="四" class="weekday">四</th><th scope="col" abbr="五" title="五" class="weekday">五</th><th scope="col" abbr="六" title="六" class="weekend">六</th><th scope="col" abbr="日" title="日" class="weekend">日</th></tr></thead><tbody><tr><td class="other-month weekday ">27</td><td class="other-month weekday ">28</td><td class="other-month weekday ">29</td><td class="other-month weekday ">30</td><td class="current-month weekday ">1</td><td class="current-month weekend ">2</td><td class="current-month weekend ">3</td></tr><tr><td class="current-month weekday ">4</td><td class="current-month weekday ">5</td><td class="current-month weekday ">6</td><td class="current-month weekday ">7</td><td class="current-month weekday ">8</td><td class="current-month weekend ">9</td><td class="current-month weekend ">10</td></tr><tr><td class="current-month weekday ">11</td><td class="current-month weekday ">12</td><td class="current-month weekday ">13</td><td class="current-month weekday ">14</td><td class="current-month weekday ">15</td><td class="current-month weekend ">16</td><td class="current-month weekend ">17</td></tr><tr><td class="current-month weekday ">18</td><td class="current-month weekday ">19</td><td class="current-month weekday ">20</td><td class="current-month weekday  hasEvent"><a href="javascript:void(0);" title="71026">21</a><br><span class="planPrice">4500元</span></td><td class="current-month weekday ">22</td><td class="current-month weekend ">23</td><td class="current-month weekend ">24</td></tr><tr><td class="current-month weekday ">25</td><td class="current-month weekday ">26</td><td class="current-month weekday ">27</td><td class="current-month weekday ">28</td><td class="current-month weekday ">29</td><td class="current-month weekend ">30</td><td class="current-month weekend ">31</td></tr></tbody></table></div></div><div class="calendarPanel"><div class="monthTitle"><table class="monthTable"><tbody><tr><td class="monthTitle">2011年08月</td><td class="nextMonth"><a href="javascript:void(0);" title="下一个月"><img src="./本州常规双温泉6日游_files/mbi_005.gif"></a></td></tr></tbody></table></div><div id="showCalendarPanel1" class="showCalendarPanel"><table cellspacing="2" class="jCalendar"><thead><tr><th scope="col" abbr="一" title="一" class="weekday">一</th><th scope="col" abbr="二" title="二" class="weekday">二</th><th scope="col" abbr="三" title="三" class="weekday">三</th><th scope="col" abbr="四" title="四" class="weekday">四</th><th scope="col" abbr="五" title="五" class="weekday">五</th><th scope="col" abbr="六" title="六" class="weekend">六</th><th scope="col" abbr="日" title="日" class="weekend">日</th></tr></thead><tbody><tr><td class="current-month weekday ">1</td><td class="current-month weekday ">2</td><td class="current-month weekday ">3</td><td class="current-month weekday ">4</td><td class="current-month weekday ">5</td><td class="current-month weekend  hasEvent"><a href="javascript:void(0);" title="71027">6</a><br><span class="planPrice">4500元</span></td><td class="current-month weekend ">7</td></tr><tr><td class="current-month weekday ">8</td><td class="current-month weekday ">9</td><td class="current-month weekday ">10</td><td class="current-month weekday ">11</td><td class="current-month weekday ">12</td><td class="current-month weekend ">13</td><td class="current-month weekend ">14</td></tr><tr><td class="current-month weekday ">15</td><td class="current-month weekday ">16</td><td class="current-month weekday ">17</td><td class="current-month weekday ">18</td><td class="current-month weekday ">19</td><td class="current-month weekend ">20</td><td class="current-month weekend ">21</td></tr><tr><td class="current-month weekday ">22</td><td class="current-month weekday ">23</td><td class="current-month weekday ">24</td><td class="current-month weekday ">25</td><td class="current-month weekday ">26</td><td class="current-month weekend ">27</td><td class="current-month weekend ">28</td></tr><tr><td class="current-month weekday ">29</td><td class="current-month weekday ">30</td><td class="current-month weekday ">31</td><td class="other-month weekday ">1</td><td class="other-month weekday ">2</td><td class="other-month weekend ">3</td><td class="other-month weekend ">4</td></tr></tbody></table></div></div></div>

    </div>
    </form>
</body>
</html>