using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace Creatives.Models
{
    public class ImageUpload
    {
        static public Image ResizeFile(HttpPostedFileBase file, int targeWidth, int targetHeight)
        {
            Image originalImage = Image.FromStream(file.InputStream, true, true);
            var newImage = new MemoryStream();
            Rectangle origRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height);
            int newWidth = targeWidth;
            int newHeight = targetHeight;
            var bitmap = new Bitmap(newWidth, newHeight);

            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight), origRect, GraphicsUnit.Pixel);
                    bitmap.Save(newImage, originalImage.RawFormat);
                }
                return (Image)bitmap;
            }
            catch
            {  
                if (bitmap != null)
                    bitmap.Dispose();
                throw new Exception("Error resizing file.");
            }
        }
    }
}