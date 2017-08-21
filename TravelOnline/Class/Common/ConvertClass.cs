using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class MyConvert
{
    public static string DtSerializeJson(DataTable dt)
    {
        if (dt.Rows.Count == 0) return "[]";
        JavaScriptDateTimeConverter mTimeConverter = new JavaScriptDateTimeConverter();
        IsoDateTimeConverter IsoTimeConverter = new IsoDateTimeConverter();
        IsoTimeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        string sj = JsonConvert.SerializeObject(dt, new DataTableConverter(), IsoTimeConverter);
        return sj;
    }

    /// <summary>
    /// 生成日期随机码
    /// </summary>
    /// <returns></returns>
    public static string GetTimeRandomCode()
    {
        #region
        return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        #endregion
    }

    public static decimal ConToDec(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        decimal TryConvert = default(decimal);
        try
        {
            TryConvert = decimal.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return 0;
        }
        return TryConvert;
    }

    public static double ConToDouble(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        double TryConvert = default(double);
        try
        {
            TryConvert = double.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return 0;
        }
        return TryConvert;
    }

    public static int ConToInt(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        int TryConvert = default(int);
        try
        {
            TryConvert = int.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return 0;
        }
        return TryConvert;
    }

    public static string DateToString(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        DateTime TryConvert = default(DateTime);
        try
        {
            TryConvert = DateTime.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return null;
        }
        return Inputs;
    }

    public static string ConToDate(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        DateTime TryConvert = default(DateTime);
        try
        {
            TryConvert = DateTime.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return "null";
        }
        return "'" + Inputs + "'";
    }

    public static DateTime ConToDateTime(string Inputs)
    {
        //转换给定字符串为数字，返回 Decimal
        DateTime TryConvert = default(DateTime);
        try
        {
            TryConvert = DateTime.Parse(Inputs);
            //转换给定的字符串
        }
        catch
        {
            return DateTime.Today;
        }
        return TryConvert;
    }

    //生成随机字符码

    #region 
    public static string CreateVerifyCode(int codeLen)
    {

        string codeSerial = "1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";

        string[] arr = codeSerial.Split(',');

        string code = "";

        int randValue = -1;

        Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

        for (int i = 0; i < codeLen; i++)
        {
            randValue = rand.Next(0, arr.Length - 1);

            code += arr[randValue];
        }

        return code;
    }
    #endregion

    //生成随机验证码

    #region
    public static string CreateNumberVerifyCode(int codeLen)
    {

        string codeSerial = "1,2,3,4,5,6,7,8,9,0";

        string[] arr = codeSerial.Split(',');

        string code = "";

        int randValue = -1;

        Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

        for (int i = 0; i < codeLen; i++)
        {
            randValue = rand.Next(0, arr.Length - 1);

            code += arr[randValue];
        }

        return code;
    }
    #endregion

    public static string TeShuChar(string Htmlstring)
    {
        Htmlstring = Htmlstring.Replace(",", "");
        Htmlstring = Htmlstring.Replace("'", "");
        Htmlstring = Htmlstring.Replace("@", "");
        Htmlstring = Htmlstring.Replace("<", "");
        Htmlstring = Htmlstring.Replace(">", "");
        return Htmlstring;
    }

    ///   <summary>   
    ///   去除HTML标记   
    ///   </summary>   
    ///   <param   name="NoHTML">包括HTML的源码   </param>   
    ///   <returns>已经去除后的文字</returns>   
    public static string NoHTML(string Htmlstring)
    {
        //删除脚本   
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML   
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

        return Htmlstring;
    }

    ///   移除HTML标签   
    
    public static string ParseTags(string HTMLStr)
    {
        return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
    }

    public static void SaveErrorToLogFile(string inErrorlog, string filename)
    {
        //string path = System.IO.Directory.GetCurrentDirectory(); //Application.StartupPath.StartupPath + @"\Errorlog.txt";
        string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + filename + ".txt";

        try
        {
            StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("UTF-8"));
            writer.WriteLine("");
            writer.WriteLine(DateTime.Now.ToString() + ":");
            writer.WriteLine(inErrorlog);
            writer.Close();
        }
        catch (Exception exception)
        {
            string message = exception.Message;
        }
    }

    /// <summary>  
    /// 获取当前时间戳  
    /// </summary>  
    /// <param name="bflag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
    /// <returns></returns>  
    public static string GetTimeStamp(bool bflag = true)
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        string ret = string.Empty;
        if (bflag)
            ret = Convert.ToInt64(ts.TotalSeconds).ToString();
        else
            ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

        return ret;
    }  

    //根据gps计算距离
    public double GetDistance(double Lat1, double Long1, double Lat2, double Long2)
    {
        double Lat1r = ConvertDegreeToRadians(Lat1);
        double Lat2r = ConvertDegreeToRadians(Lat2);
        double Long1r = ConvertDegreeToRadians(Long1);
        double Long2r = ConvertDegreeToRadians(Long2);

        double R = 6371; // Earth's radius (km)
        double d = Math.Acos(Math.Sin(Lat1r) *
            Math.Sin(Lat2r) + Math.Cos(Lat1r) *
            Math.Cos(Lat2r) *
            Math.Cos(Long2r - Long1r)) * R;
        return d;
    }

    private double ConvertDegreeToRadians(double degrees)
    {
        return (Math.PI / 180) * degrees;
    }

}

