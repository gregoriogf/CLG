using System;
using System.Collections.Generic;
using System.Threading;

namespace CLG
{
    class Game
    {
        private List<Key> game;
        private int combo;
        public Game()
        {
            combo = 0;
            //
            game = new List<Key>();
            for (int x = 0; x < Config.Rows; x++)
            {
                game.Add(new Key());
                //Avoids getting the same at first load
                Thread.Sleep(10);
            }

        }
        
        public void Start()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //
            bool hit;
            char keyinfo;
            //
            do
            {
                OutputWriter.Output(game);
                ConsoleKeyInfo c = Console.ReadKey(true);
                keyinfo = c.KeyChar;

                if(keyinfo == '\0') { keyinfo = KeyConvert(c.Key); }

                hit = (keyinfo == game[0].KeyId);
                if (hit)
                {
                    combo++;
                    game.RemoveAt(0);
                    game.Add(new Key());
                }
            } while (hit);
            watch.Stop();
            int elapseds = Convert.ToInt32(watch.ElapsedMilliseconds / 1000);
            Console.WriteLine(string.Format("{0} got {1} points in {2} seconds.", Config.Name, Score.Standard(combo, elapseds), elapseds));
        }

        private char KeyConvert(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Oem7:
                    return '´';
                default:
                    throw new Exception(string.Format("Key {0} not implemented", key));
            }
        }
    }
    class OutputWriter
    {
        public static void Output(List<Key> game)
        {
            string outputline = "";
            //
            if (Config.VerticalFlip)
            {
                for(int i = Config.Rows-1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        outputline += System.Environment.NewLine + "========" + string.Join("==", game[i].GameString) + "========";
                    }
                    else
                    {
                        outputline += System.Environment.NewLine + "        " + string.Join("  ", game[i].GameString) + "        ";
                    }
                }
            }
            else
            {
                for (int i = 0; i < Config.Rows; i++)
                {
                    if (i == 0)
                    {
                        outputline += System.Environment.NewLine + "========" + string.Join("==", game[i].GameString) + "========";
                    }
                    else
                    {
                        outputline += System.Environment.NewLine + "        " + string.Join("  ", game[i].GameString) + "        ";
                    }
                }
            }
            //
            Helper.ClearCurrentConsole();
            Console.WriteLine(outputline);
        }
    }
}
