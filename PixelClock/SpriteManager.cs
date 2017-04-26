using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace PixelClock
{
    /// <summary>
    /// Manages the sprite cropped bitmaps
    /// </summary>
    class SpriteManager
    {
        public BitmapSource Sheet;
        public CroppedBitmap[,] Numbers;
        public CroppedBitmap[] Periods;
        public CroppedBitmap[] Days;

        public SpriteManager(Bitmap bmp)
        {
            Sheet = Imaging.CreateBitmapSourceFromHBitmap(
               bmp.GetHbitmap(),
               IntPtr.Zero,
               Int32Rect.Empty,
               BitmapSizeOptions.FromEmptyOptions());

            Numbers = new CroppedBitmap[10, 2];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 2; j++)
                    Numbers[i, j] = new CroppedBitmap(Sheet, new Int32Rect(6 * i, 6 * j, 6, 6));

            Periods = new CroppedBitmap[2];
            for (int i = 0; i < 2; i++)
                Periods[i] = new CroppedBitmap(Sheet, new Int32Rect(12 * i, 18, 12, 6));

            Days = new CroppedBitmap[7];
            for (int i = 0; i < 7; i++)
                Days[i] = new CroppedBitmap(Sheet, new Int32Rect(12 * i, 12, 12, 6));
        }
    }
}
