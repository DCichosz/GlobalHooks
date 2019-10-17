using System;
using System.Text.Json;
using Keyboard = GlobalHook.Keyboard.KeyboardHook;
using Mouse = GlobalHook.Mouse.MouseHook;
using GlobalHook;

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
            };

            Console.WriteLine(Keyboard.SetHook()  != default ? "Hooked keyboard" : "Couldn't hook keyboard");
            Console.WriteLine(Mouse.SetHook()  != default ? "Hooked mouse" : "Couldn't hook mouse");

            NativeMethods.StartListening();
            
        }
    }
}