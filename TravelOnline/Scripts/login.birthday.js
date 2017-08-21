var GetBirthday =$("#UserBirthday").val();
var originalBirthday = GetBirthday.split("-");
var originalBirthdayYear = originalBirthday[0];  // 原年份
var originalBirthdayMonth = parseInt(originalBirthday[1], 10); // 原月份
var originalBirthdayDay = parseInt(originalBirthday[2], 10); // 原日期
    	    
var nowdate=new Date(); //获取当前时间的年份
var nowYear= nowdate.getFullYear();//当前年份
var nowMonth= nowdate.getMonth()+1;//当前年份

//清空年份、月份的下拉框 进行重新添加选项
$("#birthdayYear").empty();
$("#birthdayMonth").empty();

//首先为年份字段 添加选项
for(var startYear=nowYear;startYear>=1920;startYear--){ 
	var year;
	$("<option value='"+startYear+"'>"+startYear+"</option>").appendTo("#birthdayYear");
}
for(var startMonth=1;startMonth<=12;startMonth++){ 
	$("<option value='"+startMonth+"'>"+startMonth+"</option>").appendTo("#birthdayMonth");
}

if(originalBirthdayYear==null || originalBirthdayYear=="" || originalBirthdayYear=="1"){
	setTimeout(function() {$("#birthdayYear").val(nowYear);}, 1); 
	setTimeout(function() {$("#birthdayMonth").val(nowYear);}, 1); 
	//$("#birthdayYear").val(nowYear);//设置当前年份为所选
	//$("#birthdayMonth").val(nowMonth);//设置当前年份为所选
	changeSelectBrithdayDay();
}else{
	setTimeout(function() {$("#birthdayYear").val(originalBirthdayYear);}, 1); 
	setTimeout(function() {$("#birthdayMonth").val(originalBirthdayMonth);}, 1); 
	//$("#birthdayYear").val(originalBirthdayYear);//设置当前年份为所选
	//$("#birthdayMonth").val(originalBirthdayMonth);//设置当前年份为所选
	changeSelectBrithdayDay();
	setTimeout(function() {$("#birthdayMonth").val(originalBirthdayMonth);}, 1); 
	//$("#birthdayMonth").val(originalBirthdayDay);//设置当前年份为所选
}

//选择生日年份后触发
$("#birthdayYear").change( function (){
	changeSelectBrithdayDay();
});

//选择生日月份后触发
$("#birthdayMonth").change( function (){
	changeSelectBrithdayDay();
});
//选择生日日期后触发
$("#birthdayDay").change( function (){
});
//根据所选择的年份、月份计算月最大天数,并重新填充生日下拉框中的日期项
function changeSelectBrithdayDay(){
	var maxNum;
	var month=$("#birthdayMonth").val();
	var year=$("#birthdayYear").val();
	if(year==0){ //如果年份没有选择，则按照闰年计算日期(借用2004年为闰年)
		year=2004;
	}
	if( month==0){
		maxNum=31;
	}else if( month==2 ){
		if( year%400==0 || ( year%4==0 && year%100!=0)){ //判断闰年
			maxNum=29;
		}else{
			maxNum=28;
		}
	}else if( month==4 || month==6 || month==9 || month==11){
		maxNum=30;
	}else{
		maxNum=31;
	}
	
	//清空日期的下拉框 进行重新添加选项
	$("#birthdayDay").empty();
	for(var startDay=1;startDay<=maxNum;startDay++){
		$("<option value='"+startDay+"'>"+startDay+"</option>").appendTo("#birthdayDay");
	}
	if(maxNum>=originalBirthdayDay){
		setTimeout(function() {$("#birthdayDay").val(originalBirthdayDay);}, 1); 
		//$("#birthdayDay").val(originalBirthdayDay);//设置当前年份为所选
	}else{
		setTimeout(function() {$("#birthdayDay").val(1);}, 1); 
		//$("#birthdayDay").val(1);//设置当前年份为所选
		originalBirthdayDay=1;
	}
	
}


//去掉空格 并用去掉空格后的字符串替代显示
function delspace(name){
	var inputValue=$("#"+name).val();
	while(inputValue.indexOf(" ")!=-1){
		inputValue=inputValue.replace(" ","");
	}
	$("#"+name).val(inputValue);
}

//去掉左右尖括号 并用去掉空格后的字符串替代显示
function replaceBrackets(name){	
	var inputValue=$(name).val();
	while(inputValue.indexOf("<")!=-1){
		inputValue=inputValue.replace("<","[");
	}
	while(inputValue.indexOf(">")!=-1){
		inputValue=inputValue.replace(">","]");
	}
	while(inputValue.indexOf("&")!=-1){
		inputValue=inputValue.replace("&"," ");
	}
	$(name).val(inputValue);
}

//去掉某个字符  （消除对后面验证正则表达式的判定影响）
function replaceChar(name,char){
	var inputValue=$("#"+name).val();
	while(inputValue.indexOf(char)!=-1){ //去掉-影响
		inputValue=inputValue.replace(char,"");
	}
	return inputValue;
}

