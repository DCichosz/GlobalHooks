using System;
using System.Collections.Generic;

namespace GlobalHook.Keyboard
{
    public class KeyEventsArgs
    {
        public Keys Key => Enum.Parse<Keys>(KeyCode.ToString());
        public int KeyCode { get; set; }
        public KeyState KeyState { get; set; }

        public List<Keys> ModifierKeys
        {
            get
            {
                var modifiers = new List<Keys>();
                if (NativeMethods.GetKeyState((int)Keys.ShiftKey) < 0) modifiers.Add(Keys.Shift);
                if (NativeMethods.GetKeyState((int)Keys.ControlKey) < 0) modifiers.Add(Keys.Control);
                if (NativeMethods.GetKeyState((int)Keys.Menu) < 0) modifiers.Add(Keys.Alt);
                return modifiers;
            }
        }
    }

    public enum KeyState
    {
        KeyDown,
        KeyUp
    }
}