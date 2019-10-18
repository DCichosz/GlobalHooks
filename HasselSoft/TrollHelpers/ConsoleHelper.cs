using GlobalHook;

namespace HasselSoft.TrollHelpers
{
    public static class ConsoleHelper
    {
        public static void ChangeVisibility(ConsoleVisibility consoleVisibility) =>
            NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), (int) consoleVisibility);
    }
}