/*
 Example USAGE !!


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
 */


using System;
using System.Runtime.InteropServices;

namespace GlobalHook.Keyboard
{
    public static class KeyboardHook
    {

        public static event EventHandler<KeyEventsArgs> OnKeyPress;

        private static readonly NativeMethods.HookProc HookProc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;


        public static IntPtr SetHook()
        {
            _hookId =  NativeMethods.SetWindowsHookEx((int) HookType.WH_KEYBOARD_LL, HookProc,
                NativeMethods.LoadLibrary("SmallBasic Extension.dll"), 0);
            return _hookId;
        }

        public static void RemoveHook() => _hookId = IntPtr.Zero;

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch ((KeyboardMessage)wParam)
                {
                    case KeyboardMessage.WM_KEYDOWN:
                        OnKeyPress?.Invoke(null, new KeyEventsArgs
                        {
                            KeyCode = Marshal.ReadInt32(lParam),
                            KeyState = KeyState.KeyDown
                        });
                        break;
                }
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}