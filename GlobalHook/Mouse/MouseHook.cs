using System;

namespace GlobalHook.Mouse
{
    public static class MouseHook
    {
        private static readonly NativeMethods.HookProc HookProc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;

        public static event EventHandler<MouseEventsArgs> OnLeftButtonDown;
        public static event EventHandler<MouseEventsArgs> OnMouseMove;

        public static IntPtr SetHook()
        {
            _hookId =  NativeMethods.SetWindowsHookEx((int) HookType.WH_MOUSE_LL, HookProc,
                NativeMethods.LoadLibrary("SmallBasic Extension.dll"), 0);
            return _hookId;
        }

        public static void RemoveHook() => _hookId = IntPtr.Zero;


        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch ((MouseMessage)wParam)
                {
                    case MouseMessage.WM_LBUTTONDOWN:
                        OnLeftButtonDown?.Invoke(null, new MouseEventsArgs
                        {
                            Button = MouseButtons.Left,
                            State = MouseState.MouseDown
                        });
                        break;
                    case MouseMessage.WM_MOUSEMOVE:
                        OnMouseMove?.Invoke(null, new MouseEventsArgs
                        {
                            Button = MouseButtons.None,
                            State = MouseState.MouseMove
                        });
                        break;
                }
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
