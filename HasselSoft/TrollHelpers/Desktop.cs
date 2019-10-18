using System;
using System.Runtime.InteropServices;
using GlobalHook;

namespace HasselSoft.TrollHelpers
{
	public static class Desktop
	{
		private const int MIN_ALL = 419;
		private const int MIN_ALL_UNDO = 416;
		private const int WM_COMMAND = 0x111;

		private static readonly IntPtr ToggleDesktopCommand = new IntPtr(0x7402);

		public static void ShowOrHideIcons() => NativeMethods.SendMessage(
			NativeMethods.GetWindow(new IntPtr(NativeMethods.FindWindow("Progman", "Program Manager")),
				NativeMethods.GetWindowCmd.GW_CHILD), WM_COMMAND, ToggleDesktopCommand, IntPtr.Zero);

		public static bool IconsVisible()
		{
			var hWnd = NativeMethods.GetWindow(NativeMethods.GetWindow(new IntPtr(NativeMethods.FindWindow("Progman", "Program Manager")), NativeMethods.GetWindowCmd.GW_CHILD), NativeMethods.GetWindowCmd.GW_CHILD);
			NativeMethods.WindowInfo info = new NativeMethods.WindowInfo();
			info.cbSize = (uint)Marshal.SizeOf(info);
			NativeMethods.GetWindowInfo(hWnd, ref info);
			return (info.dwStyle & 0x10000000) == 0x10000000;
		}

		public static IntPtr MinimalizeAllWindows() => NativeMethods.SendMessage(
			new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), WM_COMMAND, (IntPtr) MIN_ALL, IntPtr.Zero);

		public static IntPtr RestoreAllWindows() => NativeMethods.SendMessage(
			new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), WM_COMMAND, (IntPtr) MIN_ALL_UNDO, IntPtr.Zero);

		public static void DisableWindowAnimation()
		{
			NativeMethods.Animationinfo ai = new NativeMethods.Animationinfo();
			ai.cbSize = (uint)Marshal.SizeOf(ai);
			ai.iMinAnimate = 0;   // turn all animation off
			NativeMethods.SystemParametersInfo(NativeMethods.SPI_SETANIMATION, 0, ref ai, NativeMethods.SPIF_SENDCHANGE);
		}

		public static class TaskBar
		{
			private const int SW_HIDE = 0;
			private const int SW_SHOW = 1;

			public static void Show() =>
				NativeMethods.ShowWindow(new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), SW_SHOW);

			public static void Hide() =>
				NativeMethods.ShowWindow(new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), SW_HIDE);
		}
	}
}