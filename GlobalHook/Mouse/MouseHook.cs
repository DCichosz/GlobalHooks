using System;

namespace GlobalHook.Mouse
{
    public static class MouseHook
    {
        private static readonly NativeMethods.HookProc HookProc = HookCallback;
        private static readonly IntPtr HookId = IntPtr.Zero;
        
        public static bool SetHook() =>
            NativeMethods.SetWindowsHookEx((int) HookType.WH_MOUSE_LL, HookProc,
                NativeMethods.LoadLibrary("SmallBasic Extension.dll"), 0) != default;

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var wmMouse = (MouseMessage) wParam;
                switch (wmMouse)
                {
                    case MouseMessage.WM_LBUTTONDOWN:
                        Console.WriteLine("hehehe");
                        break;
                    case MouseMessage.WM_MOUSEMOVE:
                        Console.WriteLine(VirtualMouse.GetCursorPosition());
                        break;
                }
            }

            return NativeMethods.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}
