namespace Updater.Custom
{
    using System;

    public static class Display
    {
        public static void DrawAt(int left, int top, string message)
        {
            int prevLeft = Console.CursorLeft;
            int prevTop = Console.CursorTop;

            Console.SetCursorPosition(left, top);
            Console.Write(message);
            Console.SetCursorPosition(prevLeft, prevTop);
        }
    }
}
