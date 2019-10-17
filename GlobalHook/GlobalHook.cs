﻿using System;
using System.Diagnostics;

namespace GlobalHook
{
	public abstract class GlobalHook
	{
		protected static IntPtr HookId;

		public static void RemoveHook() => ClearAndRemoveHook();
		public static IntPtr SetHook(HookType hookType, NativeMethods.HookProc hookProc)
		{
			using (var curProcess = Process.GetCurrentProcess())
			using (var curModule = curProcess.MainModule)

			{
				HookId=NativeMethods.SetWindowsHookEx((int)hookType, hookProc,
					NativeMethods.GetModuleHandle(curModule?.ModuleName), 0);
			}

			return HookId;
			//NativeMethods.SetWindowsHookEx((int) hookType, hookProc,
			//	NativeMethods.LoadLibrary("SmallBasic Extension.dll"), 0);
		}

		public static IntPtr GetHook() => HookId;

		private static void ClearAndRemoveHook()
		{
			NativeMethods.UnhookWindowsHookEx(HookId);
			HookId = IntPtr.Zero;
		}
	}
}