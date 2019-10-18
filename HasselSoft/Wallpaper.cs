using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using GlobalHook;

namespace HasselSoft
{
	public sealed partial class Wallpaper
	{

		private const int SPI_SETDESKWALLPAPER = 20;
		private const int SPIF_UPDATEINIFILE = 0x01;
		private const int SPIF_SENDWININICHANGE = 0x02;

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		public static void Set(Uri uri, Style style, string pictureName, ImageFormat format)
		{

			var stream = new WebClient().OpenRead(uri.ToString());

			var img = Image.FromStream(stream, false, false);

			var tempPath = Path.Combine(Path.GetTempPath(), $"{pictureName}.{format.ToString().ToLower()}");

			img.Save(tempPath, format);

			SystemParametersInfo(SPI_SETDESKWALLPAPER,
				0,
				tempPath,
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}

        public static void SetFromScreenshot(string path) => SystemParametersInfo(SPI_SETDESKWALLPAPER,
            0,
            path,
            SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        public static string TakeScreenShot(string screenShotName, ImageFormat format)
        {
            var primary = NativeMethods.GetDC(IntPtr.Zero);
            var actualPixelsX = NativeMethods.GetDeviceCaps(primary, 118);
            var actualPixelsY = NativeMethods.GetDeviceCaps(primary, 117);
            var tempPath = Path.Combine(Path.GetTempPath(), $"{screenShotName}.{format.ToString().ToLower()}");

            var bmp = new Bitmap(actualPixelsX, actualPixelsY);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(actualPixelsX, actualPixelsY));
                bmp.Save(tempPath, format);  // saves the image
            }
            return tempPath;
        }


    }
}