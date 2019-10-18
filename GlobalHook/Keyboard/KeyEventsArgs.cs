 using System;

namespace GlobalHook.Keyboard
{
    public class KeyEventsArgs
    {
        public Keys Key => Enum.Parse<Keys>(KeyCode.ToString());
        public int KeyCode { get; set; }
        public KeyState KeyState { get; set; }
    }

    public enum KeyState
    {
        KeyDown,
        KeyUp
    }
}
