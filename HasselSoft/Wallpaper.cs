using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

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

			img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

			SystemParametersInfo(SPI_SETDESKWALLPAPER,
				0,
				tempPath,
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
		}
	}
}