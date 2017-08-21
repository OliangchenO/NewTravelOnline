using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
//using System.Threading;

namespace TravelOnline.EncryptCode
{
    public class SecurityCode
    {
        //AES加密函数
        public static string Aes_Encrypt(string toEncrypt, string keys)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(keys);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        //AES解密函数
        public static string Aes_Decrypt(string toDecrypt, string keys)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(keys);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Md5_Encrypt(string md5str, Int16 types)
        {
            if (types == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").Substring(8, 16).ToUpper();
            }
            else if (types == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").ToUpper();
            }
            return "";
        }
    }
}