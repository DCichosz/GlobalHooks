//using System;
//using System.IO;
//using System.Net;
//using System.Runtime.InteropServices;

//namespace HasselSoft
//{
//    public sealed class Wallpaper
//    {
//        public enum Style
//        {
//            Tiled,
//            Centered,
//            Stretched
//        }

//        private const int SPI_SETDESKWALLPAPER = 20;
//        private const int SPIF_UPDATEINIFILE = 0x01;
//        private const int SPIF_SENDWININICHANGE = 0x02;

//        [DllImport("user32.dll", CharSet = CharSet.Auto)]
//        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

//        public static void Set(Uri uri, Style style)
//        {
//            var stream = new WebClient().OpenRead(uri.ToString());

//            var img = Image.FromStream(stream);

//            var tempPath = Path.Combine(Path.GetTempPath(), "hasselsoft.bmp");

//            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

//            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

//            switch (style)
//            {
//                case Style.Stretched:
//                    key.SetValue(@"WallpaperStyle", 2.ToString());
//                    key.SetValue(@"TileWallpaper", 0.ToString());
//                    break;
//                case Style.Centered:
//                    key.SetValue(@"WallpaperStyle", 1.ToString());
//                    key.SetValue(@"TileWallpaper", 0.ToString());
//                    break;
//                case Style.Tiled:
//                    key.SetValue(@"WallpaperStyle", 1.ToString());
//                    key.SetValue(@"TileWallpaper", 1.ToString());
//                    break;
//            }

//            SystemParametersInfo(SPI_SETDESKWALLPAPER,
//                0,
//                tempPath,
//                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
//        }
//    }
//}