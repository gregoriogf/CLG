using System;

namespace CLG
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Conf();
            Intro();
            ConsoleKey input;
            Helper.ClearCurrentConsole();
            do
            {
                input = Console.ReadKey(true).Key;
                
                switch (input)
                {
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.A:
                        Helper.ClearCurrentConsole();
                        About();
                        break;
                    case ConsoleKey.D1:
                        Helper.ClearCurrentConsole();
                        Game g = new Game();
                        g.Start();
                        break;
                    default:
                        break;
                }
            } while (input != ConsoleKey.Escape);
        }

        public static void Intro()
        {
            Console.WriteLine(
            "Press esc To Stop" + Environment.NewLine +
            "keys are:  " +
            string.Join(" ", Config.GameKeys) + Environment.NewLine +
            "Press key 1 to start" + Environment.NewLine);
        }

        public static void About()
        {
            Helper.ClearCurrentConsole();
            Console.WriteLine(
                Environment.NewLine + Environment.NewLine +
                "This game was coded by insomnyawolf" + Environment.NewLine +
                "And ths is the about function example");
        }
    }
}
