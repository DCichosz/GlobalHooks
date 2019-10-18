using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using GlobalHook;

namespace HasselSoft.TrollHelpers
{
	public static class Wallpaper
	{
		private const int SPI_SETDESKWALLPAPER = 20;
		private const int SPIF_UPDATEINIFILE = 0x01;
		private const int SPIF_SENDWININICHANGE = 0x02;

		public static string GetImageFromWeb(string uri, string pictureName, ImageFormat format)
		{
			var stream = new WebClient().OpenRead(uri);

			var img = Image.FromStream(stream, false, false);

			var filePath = Path.Combine(Path.GetTempPath(), $"{pictureName}.{format.ToString().ToLower()}");

			img.Save(filePath, format);

			return filePath;
		}

		public static void Set(string path) => NativeMethods.SystemParametersInfo(SPI_SETDESKWALLPAPER,
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
				Desktop.MinimalizeAllWindows();
				Thread.Sleep(1000);
				g.CopyFromScreen(0, 0, 0, 0, new Size(actualPixelsX, actualPixelsY));
				Desktop.RestoreAllWindows();
				bmp.Save(tempPath, format);
			}

			return tempPath;
		}

		public static void SetWallPaperAndHideIconsWithTaskBar(string picturePath, string pictureName,
			ImageFormat format)
		{
			Desktop.ShowOrHideIcons();
			Desktop.TaskBar.Hide();
			Set(picturePath);
		}
	}
}