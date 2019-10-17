using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;
using GlobalHook;
using GlobalHook.Keyboard;
using HasselSoft.TrollHelpers;

namespace HasselSoft
{
	class Program
	{
		static void Main(string[] args)
		{
			Mouse.OnLeftButtonDown += (sender, e) =>
			{
				Console.WriteLine(JsonSerializer.Serialize(e));
			};

			Keyboard.OnKeyPress += (sender, e) =>
			{
				Console.WriteLine(e.Key);
				if (e.Key == Keys.Space)
					Wallpaper.Set(new Uri("https://i.imgur.com/LRD7LMG.jpg"), Wallpaper.Style.Centered, "boskidawid", ImageFormat.Bmp);

				Shortcut.PermamentAutoStart();
			};

			Console.WriteLine(Keyboard.SetHook() != default ? "Hooked keyboard" : "Couldn't hook keyboard");
			Console.WriteLine(Mouse.SetHook() != default ? "Hooked mouse" : "Couldn't hook mouse");

			NativeMethods.StartListening();
		}
	}
}