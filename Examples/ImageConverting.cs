using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MaximovInk.ConsoleGameEngine
{
    public class ImageConverting : Engine
    {


        private Bitmap bitmap;

        public ImageConverting(short width = 460, short height = 150, string Title = "Raycasting", short fontw = 6, short fonth = 6, bool showFps = true)
            : base(width, height, Title, fontw, fonth, showFps)
        {
        }
 
        protected override void OnStart()
        {
            var b = new Bitmap("image.jpg");
            bitmap = (Bitmap)FixedSize(b, Height, Width, false);
            bitmap.Save("image1.jpg");

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Character ch = Character.Full;
                    COLOR color = COLOR.FG_WHITE;
                    ColorUtilites.GetConsoleColor(bitmap.GetPixel(x, y), out ch, out color);

                    DrawPixel((short)x, (short)y, (short)ch, (short)color);
                }
            }
            Apply();

        }

        public static Image FixedSize(Image imgPhoto, int Height, int Width, bool needToFill)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (Width / (float)sourceWidth);
            nPercentH = (Height / (float)sourceHeight);
            if (!needToFill)
            {
                if (nPercentH < nPercentW)
                {
                    nPercent = nPercentH;
                }
                else
                {
                    nPercent = nPercentW;
                }
            }
            else
            {
                if (nPercentH > nPercentW)
                {
                    nPercent = nPercentH;
                    destX = (int)Math.Round((Width -
                    (sourceWidth * nPercent)) / 2);
                }
                else
                {
                    nPercent = nPercentW;
                    destY = (int)Math.Round((Height -
                    (sourceHeight * nPercent)) / 2);
                }
            }

            if (nPercent > 1)
                nPercent = 1;

            int destWidth = (int)Math.Round(sourceWidth * nPercent);
            int destHeight = (int)Math.Round(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(
            destWidth <= Width ? destWidth : Width,
            destHeight < Height ? destHeight : Height,
            PixelFormat.Format32bppRgb);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.Default;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;

            grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
