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
    public class KeyboardHook : GlobalHook
    {

		public static event EventHandler<KeyEventsArgs> OnKeyPress;
		public static event EventHandler<KeyEventsArgs> OnEscapePress;
        public static event EventHandler<KeyEventsArgs> OnCPress;
        public static event EventHandler<KeyEventsArgs> OnVPress; 

		public static IntPtr SetHook() =>
			SetHook(HookType.WH_KEYBOARD_LL, HookProc);

		private static readonly NativeMethods.HookProc HookProc = HookCallback;
        public static KeyEventsArgs KeyEventsArgs { get; set; }
		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                switch ((KeyboardMessage)wParam)
                {
                    case KeyboardMessage.WM_KEYDOWN:
                        KeyEventsArgs = new KeyEventsArgs
                        {
                            KeyCode = Marshal.ReadInt32(lParam),
                            KeyState = KeyState.KeyDown
                        };
                        switch (KeyEventsArgs.Key)
                        {
                            case Keys.C:
                                OnCPress?.Invoke(null, KeyEventsArgs);
                                break;
                            case Keys.Escape:
                                OnEscapePress?.Invoke(null, KeyEventsArgs);
                                break;
                            case Keys.V:
                                OnVPress?.Invoke(null, KeyEventsArgs);
                                break;
                        }
                        
                        
                        OnKeyPress?.Invoke(null, KeyEventsArgs);
                        break;
                    case KeyboardMessage.WM_KEYUP:
                        break;

                }
            }

            return NativeMethods.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}