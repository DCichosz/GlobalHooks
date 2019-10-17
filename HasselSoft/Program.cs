using System;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;
using GlobalHook;

namespace HasselSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Keyboard.SetHook() ? "Hooked keyboard" : "Couldn't hook keyboard");
            Console.WriteLine(Mouse.SetHook() ? "Hooked mouse" : "Couldn't hook mouse");

            while (!NativeMethods.GetMessage(out NativeMethods.MSG msg, IntPtr.Zero, 0, 0))
            {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
            
        }
    }
}