namespace CLG
{
    class Helper
    {
        public static void ClearCurrentConsole()
        {
            int currentLineCursor = ++System.Console.CursorTop;
            int targetLine = currentLineCursor - 100;
            int linesToKeep = 3;
            if (Config.DebugToConsole)
            {
                linesToKeep += 3;
            }
            while (currentLineCursor > targetLine && currentLineCursor > linesToKeep)
            {
                System.Console.SetCursorPosition(0, currentLineCursor);
                System.Console.Write(new string(' ', System.Console.WindowWidth));
                System.Console.SetCursorPosition(0, currentLineCursor);
                currentLineCursor--;
            }
        }
    }
}
