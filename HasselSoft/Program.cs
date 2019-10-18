using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using GlobalHook;
using HasselSoft.TrollHelpers;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;

namespace HasselSoft
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//Shortcut.PermamentAutoStart();
			//Keyboard.OnKeyPress += (sender, e) =>
			//{
			//    Console.WriteLine(e.Key);
			//    if (e.Key == Keys.Space)
			//        Wallpaper.Set(new Uri("https://i.imgur.com/LRD7LMG.jpg"), Wallpaper.Style.Centered, "boskidawid", ImageFormat.Bmp);
			//};

#if DEBUG
			// MYSZKA LAGUJE!!
			//ConsoleHelper.ChangeVisibility(ConsoleVisibility.Show);
			//Mouse.OnLeftButtonDown += (sender, e) =>
			//    Desktop.ShowIcons();
			//Mouse.OnLeftButtonUp += (sender, e) => { };
			//Mouse.OnRightButtonUp += (sender, e) => Desktop.HideIcons();
			//Mouse.OnMouseMove += (sender, e) => Console.WriteLine(JsonSerializer.Serialize(e));

			Keyboard.OnCPress += (sender, e) => Keyboard.RemoveHook();
			Keyboard.OnVPress += (sender, e) => Desktop.ShowOrHideIcons();
			Keyboard.OnBPress += (sender, e) =>
			{
				// troll desktop
				var fileName = "hehe";
				var fileFormat = ImageFormat.Png;
				var path = Wallpaper.TakeScreenShot(fileName, fileFormat);
				Wallpaper.SetWallPaperAndHideIconsWithTaskBar(path, fileName, fileFormat);
			};
			Keyboard.OnSpacePress += (sender, e) =>
				Wallpaper.Set(Wallpaper.GetImageFromWeb("https://i.imgur.com/LRD7LMG.jpg", "boskiDawid",
					ImageFormat.Png));
#endif
			Keyboard.OnEscapePress += (sender, e) =>
			{
				// fun clear
				Desktop.TaskBar.Show();
				if (!Desktop.IconsVisible())
					Desktop.ShowOrHideIcons();
				Desktop.RestoreAllWindows();
				Environment.Exit(0);
			};

			Mouse.SetHook();
			Keyboard.SetHook();
			NativeMethods.StartListening();
		}
	}
}