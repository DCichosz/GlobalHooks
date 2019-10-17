using System;
using System.Drawing;

namespace GlobalHook.Mouse
{
    public class MouseEventsArgs
    {
        public MouseButtons Button { get; set; }
        public Point Position => VirtualMouse.GetCursorPosition();
        public MouseState State { get; set; }
		public bool DoubleClick { get; set; }

        public int X => Position.X;
        public int Y => Position.Y;
		public DateTime Time => DateTime.Now;
    }

    public enum MouseButtons
    {
        None,
        Left,
        Right,
        Middle
    }

    public enum MouseState
    {
        MouseUp,
        MouseDown,
        MouseMove
    }
}
