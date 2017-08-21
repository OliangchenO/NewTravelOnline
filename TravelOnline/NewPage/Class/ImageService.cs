using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;


namespace TravelOnline.NewPage.Class
{
    public static class ImageService
    {
        /**/
        /// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        public static void MakeThumbnail(System.Drawing.Image orgImage, string thumbnailPath, int orgwidth, int orgheight, int towidth, int toheight, string mode, int img_x, int img_y)
        {
            float ratio = 1;
            int thumbwidth = 0;
            int thumbheight = 0;

            int x = 0;
            int y = 0;

            switch (mode)
            {
                case "W"://指定宽，高按比例                     
                    ratio = (float)orgheight / orgwidth;
                    thumbwidth = orgwidth <= towidth ? orgwidth : towidth;
                    thumbheight = orgwidth <= towidth ? orgheight : Convert.ToInt32(towidth * ratio);
                    break;
                case "H"://指定高，宽按比例 
                    ratio = (float)orgwidth / orgheight;
                    thumbwidth = orgheight <= towidth ? orgwidth : Convert.ToInt32(towidth * ratio);
                    thumbheight = orgheight <= towidth ? orgheight : towidth;
                    break;
                default:
                    break;
            }

            System.Drawing.Image img = orgImage;

            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(thumbwidth, thumbheight);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //源图片宽和高
            int imgWidth = img.Width;
            int imgHeight = img.Height;

            //绘制微缩图
            g.DrawImage(img, new System.Drawing.Rectangle(0, 0, thumbwidth, thumbheight), new System.Drawing.Rectangle(x, y, orgwidth, orgheight), GraphicsUnit.Pixel);

            try
            {
                bmp.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                //img.Dispose();
                //g.Dispose();
                //bmp.Dispose();
                if (img != null) img.Dispose();
                if (g != null) g.Dispose();
                if (bmp != null) bmp.Dispose();
                if (orgImage != null) orgImage.Dispose();
            }
        }

        public static void CutImage(string src, string tosrc, int Width, int Height, string Anchor)
        {
            System.Drawing.Image imgPhoto;
            imgPhoto = Image.FromFile(src, true);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int destX = 0;
            int destY = 0;

            float sourcePercent = 0;
            float destPercent = 0;

            sourcePercent = ((float)sourceWidth / (float)sourceHeight);
            destPercent = ((float)Width / (float)Height);

            if (destPercent > sourcePercent)
            {
                destY = (int)((sourceHeight - (sourceWidth / destPercent)) / 2);
                sourceHeight = (int)(sourceWidth / destPercent);
            }
            else
            {
                destX = (int)((sourceWidth - (sourceHeight * destPercent)) / 2);
                sourceWidth = (int)(sourceHeight * destPercent);

            }

            //bmp： 最终要建立的 微缩图 位图对象。
            Bitmap bmp = new Bitmap(Width, Height);

            //g: 绘制 bmp Graphics 对象
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            //为Graphics g 对象 初始化必要参数，很容易理解。
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //绘制微缩图
            g.DrawImage(imgPhoto, new System.Drawing.Rectangle(0, 0, Width, Height), new System.Drawing.Rectangle(destX, destY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            try
            {
                bmp.Save(tosrc, ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                if (imgPhoto != null) imgPhoto.Dispose();
                if (g != null) g.Dispose();
                if (bmp != null) bmp.Dispose();
            }
        }

    }
}
