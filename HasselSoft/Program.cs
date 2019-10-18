using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
            // MYSZKA LAGUJE!!
            //ConsoleHelper.ChangeVisibility(ConsoleVisibility.Show);
            //Mouse.OnLeftButtonDown += (sender, e) =>
            //    Desktop.ShowIcons();
            //Mouse.OnLeftButtonUp += (sender, e) => { };
            //Mouse.OnRightButtonUp += (sender, e) => Desktop.HideIcons();
            //Mouse.OnMouseMove += (sender, e) => Console.WriteLine(JsonSerializer.Serialize(e));

            //Keyboard.OnCPress += (sender, e) => Desktop.ShowIcons();
            //Keyboard.OnVPress += (sender, e) => Desktop.HideIcons();
            Desktop.HideIcons();
#endif

            Keyboard.OnEscapePress += (sender, e) =>
                Environment.Exit(0);
            
            Mouse.SetHook();
            Keyboard.SetHook();
            NativeMethods.StartListening();
        }
    }
}