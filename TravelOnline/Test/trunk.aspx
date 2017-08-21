<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="trunk.aspx.cs" Inherits="TravelOnline.Test.trunk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="/js/jquery.countdown.js"></script>
<script type="text/javascript">
    $(function () {
        $('#counter').countdown({
            image: '/img/digits.png',
            startTime: '01:12:12:00'
        });

        $('#counter_2').countdown({
            image: '/img/digits.png',
            startTime: '00:10',
            timerEnd: function () { alert('end!'); },
            format: 'mm:ss'
        });
    });
    </script>
    <style type="text/css">
      br { clear: both; }
      .cntSeparator {
        font-size: 54px;
        margin: 10px 7px;
        color: #000;
      }
      .desc { margin: 7px 3px; }
      .desc div {
        float: left;
        font-family: Arial;
        width: 70px;
        margin-right: 65px;
        font-size: 13px;
        font-weight: bold;
        color: #000;
      }
    </style>
  </head>
<body>
  <div id="counter"></div>
  <div class="desc">
    <div>Días</div>
    <div>Horas</div>
    <div>Minutos</div>
    <div>Segundos</div>
  </div>
  <br />
  <br />
  <br />
  <div id="counter_2"></div>
  <div class="desc">
    <div>Minutos</div>
    <div>Segundos</div>
  </div>
</body>
</html>
