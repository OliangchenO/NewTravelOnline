<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="TravelOnline.Test.WebForm6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>�ޱ����ĵ�</title>
<script>

    UpLoadFileCheck = function () {
        this.AllowExt = ".jpg,.gif"; //�����ϴ����ļ����� 0Ϊ������ ÿ����չ�����Ҫ��һ��"," Сд��ĸ��ʾ 
        this.AllowImgFileSize = 0; //�����ϴ��ļ��Ĵ�С 0Ϊ������ ��λ��KB 
        this.AllowImgWidth = 0; //�����ϴ���ͼƬ�Ŀ��� 0Ϊ�����ơ���λ��px(����) 
        this.AllowImgHeight = 0; //�����ϴ���ͼƬ�ĸ߶� 0Ϊ�����ơ���λ��px(����) 
        this.ImgObj = new Image();
        this.ImgFileSize = 0;
        this.ImgWidth = 0;
        this.ImgHeight = 0;
        this.FileExt = "";
        this.ErrMsg = "";
        this.IsImg = false; //ȫ�ֱ���

    }

    UpLoadFileCheck.prototype.CheckExt = function (obj) {
        this.ErrMsg = "";
        this.ImgObj.src = obj.value;
        //this.HasChecked=false; 	
        if (obj.value == "") {
            this.ErrMsg = "\n��ѡ��һ���ļ�";
        }
        else {
            this.FileExt = obj.value.substr(obj.value.lastIndexOf(".")).toLowerCase();
            alert(this.FileExt);
            if (this.AllowExt != 0 && this.AllowExt.indexOf(this.FileExt) == -1)//�ж��ļ������Ƿ������ϴ� 
            {
                this.ErrMsg = "\n���ļ����Ͳ������ϴ������ϴ� " + this.AllowExt + " ���͵��ļ�����ǰ�ļ�����Ϊ" + this.FileExt;
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
            sleep(1000); //һ��ʹ��ͼ����ȫ����				
        }

        if (this.IsImg == true) {
            this.ImgWidth = this.ImgObj.width; //ȡ��ͼƬ�Ŀ��� 
            this.ImgHeight = this.ImgObj.height; //ȡ��ͼƬ�ĸ߶�
            if (this.AllowImgWidth != 0 && this.AllowImgWidth < this.ImgWidth)
                this.ErrMsg = this.ErrMsg + "\nͼƬ���ȳ������ơ����ϴ�����С��" + this.AllowImgWidth + "px���ļ�����ǰͼƬ����Ϊ" + this.ImgWidth + "px";

            if (this.AllowImgHeight != 0 && this.AllowImgHeight < this.ImgHeight)
                this.ErrMsg = this.ErrMsg + "\nͼƬ�߶ȳ������ơ����ϴ��߶�С��" + this.AllowImgHeight + "px���ļ�����ǰͼƬ�߶�Ϊ" + this.ImgHeight + "px";
        }

        this.ImgFileSize = Math.round(this.ImgObj.fileSize / 1024 * 100) / 100; //ȡ��ͼƬ�ļ��Ĵ�С 
        alert(this.ImgFileSize);
        if (this.AllowImgFileSize != 0 && this.AllowImgFileSize < this.ImgFileSize)
            this.ErrMsg = this.ErrMsg + "\n�ļ���С�������ơ����ϴ�С��" + this.AllowImgFileSize + "KB���ļ�����ǰ�ļ���СΪ" + this.ImgFileSize + "KB";

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
            sleep(1000); //һ��ʹ��ͼ����ȫ����				
        }

        if (this.IsImg == true) {
            this.ImgWidth = this.ImgObj.width; //ȡ��ͼƬ�Ŀ��� 
            this.ImgHeight = this.ImgObj.height; //ȡ��ͼƬ�ĸ߶�
            if (this.AllowImgWidth != 0 && this.AllowImgWidth < this.ImgWidth)
                this.ErrMsg = this.ErrMsg + "\nͼƬ���ȳ������ơ����ϴ�����С��" + this.AllowImgWidth + "px���ļ�����ǰͼƬ����Ϊ" + this.ImgWidth + "px";

            if (this.AllowImgHeight != 0 && this.AllowImgHeight < this.ImgHeight)
                this.ErrMsg = this.ErrMsg + "\nͼƬ�߶ȳ������ơ����ϴ��߶�С��" + this.AllowImgHeight + "px���ļ�����ǰͼƬ�߶�Ϊ" + this.ImgHeight + "px";
        }

        this.ImgFileSize = Math.round(this.ImgObj.fileSize / 1024 * 100) / 100; //ȡ��ͼƬ�ļ��Ĵ�С 
        alert(this.ImgFileSize);	
        if (this.AllowImgFileSize != 0 && this.AllowImgFileSize < this.ImgFileSize)
            this.ErrMsg = this.ErrMsg + "\n�ļ���С�������ơ����ϴ�С��" + this.AllowImgFileSize + "KB���ļ�����ǰ�ļ���СΪ" + this.ImgFileSize + "KB";

        if (this.ErrMsg != "") {
            this.ShowMsg(this.ErrMsg, false);
            return false;
        }
        else
            return true;
    }

    UpLoadFileCheck.prototype.ShowMsg = function (msg, tf)//��ʾ��ʾ��Ϣ tf=false ��ʾ������Ϣ msg-��Ϣ���� 
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