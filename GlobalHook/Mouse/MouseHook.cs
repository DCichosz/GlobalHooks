using System;

namespace GlobalHook.Mouse
{
    public class MouseHook : GlobalHook
    {
        private static readonly NativeMethods.HookProc HookProc = HookCallback;

        public static event EventHandler<MouseEventsArgs> OnLeftButtonDown;
        public static event EventHandler<MouseEventsArgs> OnLeftButtonUp;
        public static event EventHandler<MouseEventsArgs> OnLeftDoubleClick;
		public static event EventHandler<MouseEventsArgs> OnMouseMove;
        public static event EventHandler<MouseEventsArgs> OnRightButtonUp; 

        public static IntPtr SetHook() =>
	        SetHook(HookType.WH_MOUSE_LL, HookProc);

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
					case MouseMessage.WM_LBUTTONUP:
						OnLeftButtonUp?.Invoke(null, new MouseEventsArgs
						{
							Button = MouseButtons.Left,
							State = MouseState.MouseUp
						});
						break;
					case MouseMessage.WM_LBUTTONDBLCLK:
						OnLeftDoubleClick?.Invoke(null, new MouseEventsArgs
						{
							Button = MouseButtons.Left,
							State = MouseState.MouseUp,
							DoubleClick = true
						});
						break;
					case MouseMessage.WM_RBUTTONUP:
						OnRightButtonUp?.Invoke(null, new MouseEventsArgs
						{
							Button = MouseButtons.Right,
							State = MouseState.MouseUp
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

            return NativeMethods.CallNextHookEx(HookId, nCode, wParam, lParam);
		}

	}
}
