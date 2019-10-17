using System;
using System.Drawing.Imaging;
using System.Text.Json;
using System.Threading;
using GlobalHook;
using GlobalHook.Keyboard;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;
using HasselSoft.TrollHelpers;

namespace HasselSoft
{
	class Program
	{
		static void Main(string[] args)
		{
			//Shortcut.PermamentAutoStart();

			Mouse.OnLeftButtonDown += (sender, e) =>
			{
				Console.WriteLine("down");
			};
			Mouse.OnLeftButtonUp += (sender, e) =>
			{
				return;
			};
			Mouse.OnRightButtonUp += (sender, e) => { Console.WriteLine("Dupciaright"); };
			Mouse.OnMouseMove += (sender, e) => { Console.WriteLine(JsonSerializer.Serialize(e)); };

			//Keyboard.OnKeyPress += (sender, e) =>
			//{
			//	Console.WriteLine(e.Key);
			//	if (e.Key == Keys.Space)
			//		Wallpaper.Set(new Uri("https://i.imgur.com/LRD7LMG.jpg"), Wallpaper.Style.Centered, "boskidawid", ImageFormat.Bmp);
			//};

			Keyboard.OnEscapePress += (sender, e) =>
			{
				Keyboard.RemoveHook();
				Mouse.RemoveHook();
			};

			Console.WriteLine(Keyboard.SetHook() != default ? "Hooked keyboard" : "Couldn't hook keyboard");

			Console.WriteLine(Mouse.SetHook() != default ? "Hooked mouse" : "Couldn't hook mouse");
			NativeMethods.StartListening();
		}
	}
}