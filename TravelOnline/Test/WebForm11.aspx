<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm11.aspx.cs" Inherits="TravelOnline.Test.WebForm11" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
<style type="text/css">
body, html,#allmap {width: 500px;height: 300px;overflow: hidden;margin:0;}
</style>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=1hBsSkjOVNVr7WwRs0tqwMTl"></script>
<title>地图官网展示效果</title>
</head>
<body>


<div id="allmap"></div>
</body>
</html>
<script type="text/javascript">

    // 百度地图API功能
//    var map = new BMap.Map("allmap");                        // 创建Map实例
//    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);     // 初始化地图,设置中心点坐标和地图级别
//    map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
//    map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
//    map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
//    map.enableScrollWheelZoom();                            //启用滚轮放大缩小
//    map.centerAndZoom(point, 15); 
//    map.addControl(new BMap.MapTypeControl());          //添加地图类型控件
    //    map.setCurrentCity("北京");          // 设置地图显示的城市 此项是必须设置的

    // 百度地图API功能
    var map = new BMap.Map("allmap");            // 创建Map实例
    var point = new BMap.Point(121.444625, 31.19912);    // 创建点坐标
    map.centerAndZoom(point, 18);                     // 初始化地图,设置中心点坐标和地图级别。
    map.enableScrollWheelZoom();                            //启用滚轮放大缩小
</script>

