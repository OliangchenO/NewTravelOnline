<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="TravelOnline.Test.WebForm6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>无标题文档</title>
<script>

    UpLoadFileCheck = function () {
        this.AllowExt = ".jpg,.gif"; //允许上传的文件类型 0为无限制 每个扩展名后边要加一个"," 小写字母表示 
        this.AllowImgFileSize = 0; //允许上传文件的大小 0为无限制 单位：KB 
        this.AllowImgWidth = 0; //允许上传的图片的宽度 0为无限制　单位：px(像素) 
        this.AllowImgHeight = 0; //允许上传的图片的高度 0为无限制　单位：px(像素) 
        this.ImgObj = new Image();
        this.ImgFileSize = 0;
        this.ImgWidth = 0;
        this.ImgHeight = 0;
        this.FileExt = "";
        this.ErrMsg = "";
        this.IsImg = false; //全局变量

    }

    UpLoadFileCheck.prototype.CheckExt = function (obj) {
        this.ErrMsg = "";
        this.ImgObj.src = obj.value;
        //this.HasChecked=false; 	
        if (obj.value == "") {
            this.ErrMsg = "\n请选择一个文件";
        }
        else {
            this.FileExt = obj.value.substr(obj.value.lastIndexOf(".")).toLowerCase();
            alert(this.FileExt);
            if (this.AllowExt != 0 && this.AllowExt.indexOf(this.FileExt) == -1)//判断文件类型是否允许上传 
            {
                this.ErrMsg = "\n该文件类型不允许上传。请上传 " + this.AllowExt + " 类型的文件，当前文件类型为" + this.FileExt;
            }
        }
        if (this.ErrMsg != "") {
            this.ShowMsg(this.ErrMsg, false);
            return false;
        }
        else { 
            //return this.CheckProperty(obj);
        }

        if (this.ImgObj.readyState != "complete")//
        {
            sleep(1000); //一秒使用图能完全加载				
        }

        if (this.IsImg == true) {
            this.ImgWidth = this.ImgObj.width; //取得图片的宽度 
            this.ImgHeight = this.ImgObj.height; //取得图片的高度
            if (this.AllowImgWidth != 0 && this.AllowImgWidth < this.ImgWidth)
                this.ErrMsg = this.ErrMsg + "\n图片宽度超过限制。请上传宽度小于" + this.AllowImgWidth + "px的文件，当前图片宽度为" + this.ImgWidth + "px";

            if (this.AllowImgHeight != 0 && this.AllowImgHeight < this.ImgHeight)
                this.ErrMsg = this.ErrMsg + "\n图片高度超过限制。请上传高度小于" + this.AllowImgHeight + "px的文件，当前图片高度为" + this.ImgHeight + "px";
        }

        this.ImgFileSize = Math.round(this.ImgObj.fileSize / 1024 * 100) / 100; //取得图片文件的大小 
        alert(this.ImgFileSize);
        if (this.AllowImgFileSize != 0 && this.AllowImgFileSize < this.ImgFileSize)
            this.ErrMsg = this.ErrMsg + "\n文件大小超过限制。请上传小于" + this.AllowImgFileSize + "KB的文件，当前文件大小为" + this.ImgFileSize + "KB";

        if (this.ErrMsg != "") {
            this.ShowMsg(this.ErrMsg, false);
            return false;
        }
        else
            return true;
        
    }

    UpLoadFileCheck.prototype.CheckProperty = function (obj) {
        if (this.ImgObj.readyState != "complete")//
        {
            sleep(1000); //一秒使用图能完全加载				
        }

        if (this.IsImg == true) {
            this.ImgWidth = this.ImgObj.width; //取得图片的宽度 
            this.ImgHeight = this.ImgObj.height; //取得图片的高度
            if (this.AllowImgWidth != 0 && this.AllowImgWidth < this.ImgWidth)
                this.ErrMsg = this.ErrMsg + "\n图片宽度超过限制。请上传宽度小于" + this.AllowImgWidth + "px的文件，当前图片宽度为" + this.ImgWidth + "px";

            if (this.AllowImgHeight != 0 && this.AllowImgHeight < this.ImgHeight)
                this.ErrMsg = this.ErrMsg + "\n图片高度超过限制。请上传高度小于" + this.AllowImgHeight + "px的文件，当前图片高度为" + this.ImgHeight + "px";
        }

        this.ImgFileSize = Math.round(this.ImgObj.fileSize / 1024 * 100) / 100; //取得图片文件的大小 
        alert(this.ImgFileSize);	
        if (this.AllowImgFileSize != 0 && this.AllowImgFileSize < this.ImgFileSize)
            this.ErrMsg = this.ErrMsg + "\n文件大小超过限制。请上传小于" + this.AllowImgFileSize + "KB的文件，当前文件大小为" + this.ImgFileSize + "KB";

        if (this.ErrMsg != "") {
            this.ShowMsg(this.ErrMsg, false);
            return false;
        }
        else
            return true;
    }

    UpLoadFileCheck.prototype.ShowMsg = function (msg, tf)//显示提示信息 tf=false 显示错误信息 msg-信息内容 
    {
        /*msg=msg.replace("\n","<li>"); 
        msg=msg.replace(/\n/gi,"<li>"); 
        */
        alert(msg);
    }
    function sleep(num) {
        var tempDate = new Date();
        var tempStr = "";
        var theXmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        while ((new Date() - tempDate) < num) {
            tempStr += "\n" + (new Date() - tempDate);
            try {
                theXmlHttp.open("get", "about:blank?JK=" + Math.random(), false);
                theXmlHttp.send();
            }
            catch (e) { ; }
        }
        //containerDiv.innerText=tempStr;   
        return;
    }

    function c(obj) {
        var d = new UpLoadFileCheck();
        d.IsImg = true;
        d.AllowImgFileSize = 100;
        d.CheckExt(obj)
    }
</script>

</head>

<body>
<input name="" type="file"   onchange="c(this)"/>


</body>
</html>
