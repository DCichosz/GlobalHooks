﻿using System;
using System.Runtime.InteropServices;

namespace GlobalHook
{
	public static class NativeMethods
	{
		/// <summary>
		///     The CallWndProc hook procedure is an application-defined or library-defined
		///     callback function used with the SetWindowsHookEx function. The HOOKPROC type
		///     defines a pointer to this callback function. CallWndProc is a placeholder for
		///     the application-defined or library-defined function name.
		/// </summary>
		/// <param name="nCode">
		///     Specifies whether the hook procedure must process the message.
		/// </param>
		/// <param name="wParam">
		///     Specifies whether the message was sent by the current thread.
		/// </param>
		/// <param name="lParam">
		///     Pointer to a CWPSTRUCT structure that contains details about the message.
		/// </param>
		/// <returns>
		///     If nCode is less than zero, the hook procedure must return the value returned
		///     by CallNextHookEx. If nCode is greater than or equal to zero, it is highly
		///     recommended that you call CallNextHookEx and return the value it returns;
		///     otherwise, other applications that have installed WH_CALLWNDPROC hooks will
		///     not receive hook notifications and may behave incorrectly as a result. If the
		///     hook procedure does not call CallNextHookEx, the return value should be zero.
		/// </returns>
		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		public enum GetWindowCmd : uint
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}

		public static uint SPIF_SENDCHANGE = 0x02;
		public static uint SPI_SETANIMATION = 0x0049;

		[DllImport("user32.dll")]
		public static extern bool GetMessage(out Msg lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

		[DllImport("user32.dll")]
		public static extern bool TranslateMessage([In] ref Msg lpMsg);

		[DllImport("user32.dll")]
		public static extern IntPtr DispatchMessage([In] ref Msg lpmsg);

		/// <summary>
		///     The SetWindowsHookEx function installs an application-defined hook
		///     procedure into a hook chain. You would install a hook procedure to monitor
		///     the system for certain types of events. These events are associated either
		///     with a specific thread or with all threads in the same desktop as the
		///     calling thread.
		/// </summary>
		/// <param name="hookType">
		///     Specifies the type of hook procedure to be installed
		/// </param>
		/// <param name="callback">Pointer to the hook procedure.</param>
		/// <param name="hMod">
		///     Handle to the DLL containing the hook procedure pointed to by the lpfn
		///     parameter. The hMod parameter must be set to NULL if the dwThreadId
		///     parameter specifies a thread created by the current process and if the
		///     hook procedure is within the code associated with the current process.
		/// </param>
		/// <param name="dwThreadId">
		///     Specifies the identifier of the thread with which the hook procedure is
		///     to be associated.
		/// </param>
		/// <returns>
		///     If the function succeeds, the return value is the handle to the hook
		///     procedure. If the function fails, the return value is 0.
		/// </returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetWindowsHookEx(int hookType,
			HookProc callback, IntPtr hMod, uint dwThreadId);

		/// <summary>
		///     The UnhookWindowsHookEx function removes a hook procedure installed in
		///     a hook chain by the SetWindowsHookEx function.
		/// </summary>
		/// <param name="hhk">Handle to the hook to be removed.</param>
		/// <returns>
		///     If the function succeeds, the return value is true.
		///     If the function fails, the return value is false.
		/// </returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int FindWindow(string className, string windowText);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
			string lpszWindow);

		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string libraryName);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
			CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);


		[DllImport("User32.dll")]
		public static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("User32.dll")]
		public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

		public static void StartListening()
		{
			while (!GetMessage(out var msg, IntPtr.Zero, 0, 0))
			{
				TranslateMessage(ref msg);
				DispatchMessage(ref msg);
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowInfo(IntPtr hwnd, ref WindowInfo pwi);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SystemParametersInfo(uint uiAction,
			uint uiParam,
			ref Animationinfo pvParam,
			uint fWinIni);


		[Serializable]
		public struct Msg
		{
			public IntPtr hwnd;

			public IntPtr lParam;

			public int message;

			public int pt_x;

			public int pt_y;

			public int time;

			public IntPtr wParam;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct WindowInfo
		{
			public uint cbSize;
			public Rect rcWindow;
			public Rect rcClient;
			public uint dwStyle;
			public uint dwExStyle;
			public uint dwWindowStatus;
			public uint cxWindowBorders;
			public uint cyWindowBorders;
			public ushort atomWindowType;
			public ushort wCreatorVersion;

			public WindowInfo(bool? filler)
				: this() // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
			{
				cbSize = (uint) Marshal.SizeOf(typeof(WindowInfo));
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rect
		{
			private readonly int _Left;
			private readonly int _Top;
			private readonly int _Right;
			private readonly int _Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Animationinfo
		{
			public uint cbSize;
			public int iMinAnimate;

			public Animationinfo(bool? filler)
				: this() // Allows automatic initialization of "cbSize" with "new Animationinfo(null/true/false)".
			{
				cbSize = (uint) Marshal.SizeOf(typeof(WindowInfo));
			}
		}
	}
}