function iTimePoint(iTimeSlideId, dateId, timeLineId, titleTop, titleId, defaultShow){
    /* 传入参数说明:
     * iTimeSlideId: 外围ID名. 本样例DOM中#itimeslide;
     * dateId: 日期ID名. 本样例DOM中#date;
     * timeLineId: 时间点分布ID名. 本样例DOM中#timeline;
     * titleTop: 标题容器上方小三角ID名. 本样例DOM中#titletop;
     * titleId: 标题容器ID名. 本样例DOM中#title;
     * defaultShow: 设定初始显示的时间点, 默认为0, 可不传值
     */
     
    //参数判断,测试用,成功运行后可删除
    if (arguments.length < 5 || arguments.length>6) {
        //alert('参数传入错误,请传入5或6个值! :)');
        //return false;
    }
	
	//通用方法
    var iBase = {
        //document.getElementById
        Id: function(name){
            return document.getElementById(name);
        },
        //时间点动画显示
        PointSlide: function(elem, val){
            //可通过修改i+=5中的5控制滑动速度
            for (var i = 0; i <= 100; i += 10) {
                (function(){
                    //这个pos定义很重要,若直接使用闭包获取到的不是上面的i
                    var pos = i;
                    //平滑移动
                    setTimeout(function(){
                        elem.style.left = pos * val / 100 + 'px';
                    }, (pos + 1) * 10);
                })();
            }
        },
        //为元素添加样式
        AddClass: function(elem, val){
            //若元素无class,直接赋值
            if (!elem.className) {
                elem.className = val;
            }else {
                //否则通过添加空格新增一个class
                var oVal = elem.className;
                oVal += ' ';
                oVal += val;
                elem.className = val;
            }
        },
        //获取元素索引
        Index: function(cur, obj){
            for (var i = 0; i < obj.length; i++) {
                if (obj[i] == cur) {
                    return i;
                }
            }
        }
    }
	//整个函数变量定义区
    var dataLen = JSONData.length;
    var iTimeSilde = iBase.Id(iTimeSlideId);
    var date = iBase.Id(dateId);
    var timeLine = iBase.Id(timeLineId);
    var titletop = iBase.Id(titleTop);
    var title = iBase.Id(titleId);
    var iTimeSildeW = iTimeSilde.offsetWidth;//幻灯区实际宽度
    var timePoint = document.createElement('ul');//用来存储时间点的ul
    var timePointLeft = null;//时间点相对于父元素左边距离
    var timePointLeftCur = null;//每两个时间点间距
    var pointIndex = 0;//时间点在队列中的索引值
	var defaultShow = defaultShow || 0;//默认显示的时间
	var clearFun=null;//当用户无意识的划过时中止执行
	var that=null;
    //根据数据条数生成对应的时间点html
    for (var i = 0; i < dataLen; i++) {
        timePoint.innerHTML += '<li></li>';
    }
    //将时间点插入到时间线DIV中
    timeLine.appendChild(timePoint)
    var timePoints = timeLine.getElementsByTagName('li');
    //时间点平滑显示
    for (var i = 0; i < timePoints.length; i++) {
		//每两个时间点间间距
        timePointLeftCur = parseInt(iTimeSildeW / (dataLen + 1));
		//计算对应时间点左边距
        timePointLeft = (i + 1) * timePointLeftCur;
		//时间点动画形式初始化
        iBase.PointSlide(timePoints[i], timePointLeft);
		//初始显示时间点
        setTimeout(function(){
            timePoints[defaultShow].onmouseover();
        }, 1200);
		//获取时间点默认class值,为鼠标事件做准备
        timePoints[i].oldClassName = timePoints[i].className;
        timePoints[i].onmouseover = function(){
			that = this;//确保clearFun中的this是当前的this
			//提升用户体验,当用户无意识地划过时不执行函数
			clearFun=setTimeout(function(){
				//计算出当前时间点索引值,为鼠标划出做准备
		        pointIndex = iBase.Index(that, timePoints);
				//去除上一个时间点高亮样式
				for (var m = 0; m < timePoints.length; m++) {
					if (m != pointIndex) {
						timePoints[m].className = timePoints[m].oldClassName
					}
				}
				//为当前时间点加载高亮样式
	            iBase.AddClass(that, 'hover');
				//切换日期及标题值
	            date.innerHTML = '<span>' + (JSONData[pointIndex]['date'] || '') + '</span><em></em>';
	            title.innerHTML = (JSONData[pointIndex]['title'] || '')
	            //title.innerHTML = '<a href="' + (JSONData[pointIndex]['href'] || '') + '">' + (JSONData[pointIndex]['title'] || '') + '</a>';
	            //改变日期及标题的位置,此处减去的数字,可根据实际样式调整
	            date.style.left = ((pointIndex + 1) * timePointLeftCur - 50) + 'px';
	            titletop.style.left = ((pointIndex + 1) * timePointLeftCur + 3) + 'px';
	            //当标题框左边距与标题框宽度之和大于外围宽度时,以右边为绝对点
	            if ((title.offsetWidth + (pointIndex + 1) * timePointLeftCur) < iTimeSildeW) {
	                title.style.left = ((pointIndex + 1) * timePointLeftCur - (timePointLeftCur/2)) + 'px';
	            }else {
	                title.style.left = (iTimeSildeW - title.offsetWidth) + 'px';
	            }
	            title.style.left="2px";
	            title.style.width="790px";
	            //显示日期/时间点/标题
	            date.style.display = 'none';
	            titletop.style.display = 'block';
	            title.style.display = 'block';
			},200);//200为认定无意识划过的时间,可自行调节
        }
        timePoints[i].onmouseout = function(){
			//若停留时间低于200ms,认定为无意识划过,中止函数
			clearTimeout(clearFun);
        }
    }
}