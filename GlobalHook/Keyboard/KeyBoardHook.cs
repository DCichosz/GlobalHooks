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
using System.Runtime.InteropServices;

namespace GlobalHook.Keyboard
{
    public static class KeyboardHook
    {

        private static readonly NativeMethods.HookProc HookProc = HookCallback;
        private static readonly IntPtr HookId = IntPtr.Zero;


        private static int _keycode;

        public static bool SetHook() =>
            NativeMethods.SetWindowsHookEx((int) HookType.WH_KEYBOARD_LL, HookProc,
                NativeMethods.LoadLibrary("SmallBasic Extension.dll"), 0) != default;

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr) KeyboardMessage.WM_KEYDOWN)
                {
                    _keycode = Marshal.ReadInt32(lParam);
                    Console.WriteLine(Enum.GetName(typeof(Keys), _keycode));
                }
                else if (wParam == (IntPtr) KeyboardMessage.WM_KEYUP)
                {
                    _keycode = -1;
                }
            }

            return NativeMethods.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}