<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsApiPayPage.aspx.cs" Inherits="TravelOnline.WeChat.JsApiPayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/> 
    <title>微信支付样例-JSAPI支付</title>
</head>

           <script type="text/javascript">
           //获取共享地址
           window.onload = function ()
           {
               if (typeof WeixinJSBridge == "undefined")
               {
                   if (document.addEventListener)
                   {
                       document.addEventListener('WeixinJSBridgeReady', editAddress, false);
                   }
                   else if (document.attachEvent)
                   {
                       document.attachEvent('WeixinJSBridgeReady', editAddress);
                       document.attachEvent('onWeixinJSBridgeReady', editAddress);
                   }
               }
               else
               {
                   editAddress();
               }
           };




               //调用微信JS api 支付
               function onBridgeReady()
               {
                   WeixinJSBridge.invoke('getBrandWCPayRequest',<%=wxJsApiParam%>,//josn串
                   function(res)
                   {     
                        if(res.err_msg == "get_brand_wcpay_request:ok" )
                        {
                            alert("支付成功！");
                        }     // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                        else
                        {
                            alert("支付失败！"+res.err_msg);
                        }
                    }); 
                 }

                 if (typeof WeixinJSBridge == "undefined")
                 {
                     if( document.addEventListener )
                     {
                        document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
                       }
                     else if (document.attachEvent)
                     {
                               document.attachEvent('WeixinJSBridgeReady', onBridgeReady); 
                               document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
                      }
                  }
                  else
                  {
                       onBridgeReady();
                   }
               
     </script>

<body>
    <form id="Form1" runat="server">
        <br/>
	    <%--<div align="center">
		    <br/><br/><br/>
            <asp:Button ID="submit" runat="server" Text="确认并支付" OnClientClick="" style="width:210px; height:50px; border-radius: 15px;background-color:#00CD00; border:0px #FE6714 solid; cursor: pointer;  color:white;  font-size:16px;" />
	    </div>--%>
    </form>
</body>
</html>