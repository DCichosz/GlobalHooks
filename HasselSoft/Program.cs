using System;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using GlobalHook;
using GlobalHook.Keyboard;
using GlobalHook.Mouse;
using HasselSoft.TrollHelpers;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;

namespace HasselSoft
{
	class Program
	{
		static void Main(string[] args)
		{

            //Shortcut.PermamentAutoStart();
            //Keyboard.OnKeyPress += (sender, e) =>
            //{
            //    Console.WriteLine(e.Key);
            //    if (e.Key == Keys.Space)
            //        Wallpaper.Set(new Uri("https://i.imgur.com/LRD7LMG.jpg"), Wallpaper.Style.Centered, "boskidawid", ImageFormat.Bmp);
            //};

            Mouse.OnLeftButtonDown += (sender, e) =>
            {
                Console.WriteLine(e.Button);
            };
            Mouse.OnLeftButtonUp += (sender, e) =>
            {
                return;
            };
            Mouse.OnRightButtonUp += (sender, e) => { Console.WriteLine("Dupciaright"); };
            Mouse.OnMouseMove += (sender, e) => { Console.WriteLine(JsonSerializer.Serialize(e)); };
            Console.WriteLine(Mouse.SetHook() != default ? "Hooked mouse" : "Couldn't hook mouse");


            Keyboard.OnEscapePress += (sender, e) =>
            {
                Console.WriteLine(e.Key);
            };

            Console.WriteLine(Keyboard.SetHook() != default ? "Hooked keyboard" : "Couldn't hook keyboard");

            NativeMethods.StartListening();
        }
	}
}