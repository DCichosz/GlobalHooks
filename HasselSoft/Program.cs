using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.Json;
using System.Threading;
using GlobalHook;
using GlobalHook.Keyboard;
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

#if DEBUG
            Mouse.OnLeftButtonDown += (sender, e) => Console.WriteLine(e.Button);
            Mouse.OnLeftButtonUp += (sender, e) => { };
            Mouse.OnRightButtonUp += (sender, e) => Console.WriteLine(e.Button);
            Mouse.OnMouseMove += (sender, e) => Console.WriteLine(JsonSerializer.Serialize(e));

            Keyboard.OnKeyPress += (sender, e) => e.ModifierKeys.ForEach((s) => Console.Write(s + ", "));
#endif

            Keyboard.OnEscapePress += (sender, e) =>
                Thread.CurrentThread.Join();

            Mouse.SetHook();
            Keyboard.SetHook();
            NativeMethods.StartListening();
        }
    }
}