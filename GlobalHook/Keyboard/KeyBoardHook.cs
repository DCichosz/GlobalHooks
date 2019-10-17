/*
 Example USAGE !!


  static void Main(string[] args)
        {
            Keyboard.SetHook();

            while ((!NativeMethods.GetMessage(out NativeMethods.MSG msg, IntPtr.Zero, 0, 0)))
            {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }
 */


using System;
using System.Runtime.CompilerServices;
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