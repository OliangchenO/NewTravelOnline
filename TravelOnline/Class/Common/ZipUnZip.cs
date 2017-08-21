using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace TravelOnline.Class.Common
{

    public class GZipUnZip
    {

        public byte[] GZipCompress(string StrNeedCompress)
        {
            //将字符串转化为字节数组
            byte[] buffer = Encoding.Default.GetBytes(StrNeedCompress);

            // 压缩字节数组
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    // 将字节数组压缩后  写入内存流
                    gs.Write(buffer, 0, buffer.Length);
                }
                //内存流中数据转化为字节数组
                return ms.ToArray();
            }
        }

        //解压数据
        public Byte[] DeCompress(Byte[] data)
        {
            using (MemoryStream source = new MemoryStream())
            {
                using (GZipStream gs = new GZipStream(new MemoryStream(data), CompressionMode.Decompress, true))
                {
                    //从压缩流中读出所有数据
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = gs.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        source.Write(bytes, 0, n);
                    }
                }
                return source.ToArray();
            }
        }
    }

}