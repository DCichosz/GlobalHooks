using System;
using System.Runtime.InteropServices;
using GlobalHook;
using Microsoft.Win32;

namespace HasselSoft.TrollHelpers
{
    public static class Desktop
    {
        public static class TaskBar
        {
            private const int SW_HIDE = 0;
            private const int SW_SHOW = 1;

            public static void Show() =>
                NativeMethods.ShowWindow(new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), SW_SHOW);

            public static void Hide() =>
                NativeMethods.ShowWindow(new IntPtr(NativeMethods.FindWindow("Shell_TrayWnd", "")), SW_HIDE);
        }
        public static void HideIcons()
        {

        }

        public static void ShowIcons() => NativeMethods.ShowWindow(new IntPtr(NativeMethods.FindWindow("Progman", "")), 5);
    }
}