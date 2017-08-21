using System;
using System.Web;
using System.Text;
using System.IO;
/// <summary>
/// Summary description for ToolsClass
/// </summary>
public class ToolsClass : System.Web.UI.Page
{
    public ToolsClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region "获取用户IP地址"
    /// <summary>
    /// 获取用户IP地址
    /// </summary>
    /// <returns></returns>
    public static string GetIPAddress()
    {

        string user_IP = string.Empty;
        user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;
        return user_IP;
    }
    #endregion
    public static string unescape(string s)
    {

        string str = s.Remove(0, 2);//删除最前面两个＂%u＂
        string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串＂%u＂分隔
        byte[] byteArr = new byte[strArr.Length * 2];
        for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
        {
            byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节
            byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
        }
        str = System.Text.Encoding.Unicode.GetString(byteArr);　//把字节转为unicode编码
        return str;

    }
    /*在C#后台实现JavaScript的函数escape()的字符串转换
    些方法支持汉字*/
    public static string escape(string s)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

        for (int i = 0; i < byteArr.Length; i += 2)
        {
            sb.Append("%u");
            sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式

            sb.Append(byteArr[i].ToString("X2"));
        }
        return sb.ToString();
    }
    //xml头
    public static void xmlheader()
    {
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Charset = "coding";
        HttpContext.Current.Response.ContentType = "text/xml";
        HttpContext.Current.Response.Expires = 0;
        HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now;
        HttpContext.Current.Response.CacheControl = "no-cache";
        HttpContext.Current.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
    }
    //json头
    public static void jsonheader()
    {
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Charset = "gb2312";
        //HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.ContentType = "text/html";
        HttpContext.Current.Response.Expires = 0;
        HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now;
        HttpContext.Current.Response.CacheControl = "no-cache";
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - -
     * Stream 和 byte[] 之间的转换
     * - - - - - - - - - - - - - - - - - - - - - - - */
    /// <summary>
    /// 将 Stream 转成 byte[]
    /// </summary>
    public static byte[] StreamToBytes(Stream stream)
    {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);

        // 设置当前流的位置为流的开始
        stream.Seek(0, SeekOrigin.Begin);
        return bytes;
    }

    /// <summary>
    /// 将 byte[] 转成 Stream
    /// </summary>
    public static Stream BytesToStream(byte[] bytes)
    {
        Stream stream = new MemoryStream(bytes);
        return stream;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - -
    * Stream 和 文件之间的转换
    * - - - - - - - - - - - - - - - - - - - - - - - */
    /// <summary>
    /// 将 Stream 写入文件
    /// </summary>
    public static void StreamToFile(Stream stream, string fileName)
    {
        // 把 Stream 转换成 byte[]
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        // 设置当前流的位置为流的开始
        stream.Seek(0, SeekOrigin.Begin);

        // 把 byte[] 写入文件
        FileStream fs = new FileStream(fileName, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        bw.Write(bytes);
        bw.Close();
        fs.Close();
    }

    /// <summary>
    /// 从文件读取 Stream
    /// </summary>
    public static Stream FileToStream(string fileName)
    {
        // 打开文件
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        // 读取文件的 byte[]
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes, 0, bytes.Length);
        fileStream.Close();
        // 把 byte[] 转换成 Stream
        Stream stream = new MemoryStream(bytes);
        return stream;
    }
}
