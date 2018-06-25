using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MadMilkman.Ini;

namespace CLG
{
    class Config
    {
        //I just mash my keys without looking anywhere while holding my keyboard like a gitarhero guitar :^)
        private static string iniPath = string.Format("{0}\\{1}",Directory.GetCurrentDirectory(), "CLG.ini");
        //
        public static int Rows;
        public static int Columns;
        public static bool VerticalFlip;
        public static String Name;
        public static char[] Keys;
        //
        public static char[] GameKeys;
        //
        public static bool DebugToConsole = false;

        public static void Conf()
        {
            if (!LoadIni())
            {
                if (CreateIni())
                {
                    if (!LoadIni())
                    {
                        Console.WriteLine("Critical Error!\nCould not load config\nPress any key to exit...");
                        Console.ReadKey(false);
                    }
                }
            }
        }

        private static IniOptions iniOptions()
        {
            IniOptions options = new IniOptions();
            options.CommentStarter = IniCommentStarter.Hash;
            options.Compression = false;
            options.Encoding = Encoding.UTF8;
            options.EncryptionPassword = null;
            return options;
        }

        private static bool LoadIni()
        {
            try
            {
                IniFile file = new IniFile(iniOptions());
                file.Load(iniPath);

                // Map 'yes' value as 'true' boolean.
                file.ValueMappings.Add("yes", true);
                file.ValueMappings.Add("true", true);
                file.ValueMappings.Add("1", true);
                // Map 'no' value as 'false' boolean.
                file.ValueMappings.Add("no", false);
                file.ValueMappings.Add("false", false);
                file.ValueMappings.Add("0", false);

                //IniSection Debug = file.Sections["Debug"];
                //
                //Debug.Keys["DebugToConsole"].TryParseValue(out DebugToConsole);
                //
                IniSection Settings = file.Sections["Settings"];
                //
                Settings.Keys["Rows"].TryParseValue(out Rows);
                Settings.Keys["Columns"].TryParseValue(out Columns);
                Settings.Keys["VerticalFlip"].TryParseValue(out VerticalFlip);
                Settings.Keys["Name"].TryParseValue(out Name);
                //
                IniSection keys = file.Sections["Keys"];
                //
                Keys = new Char[9];
                //
                Keys[0] = ToChar(keys.Keys["K1"].Value);
                Keys[1] = ToChar(keys.Keys["K2"].Value);
                Keys[2] = ToChar(keys.Keys["K3"].Value);
                Keys[3] = ToChar(keys.Keys["K4"].Value);
                Keys[4] = ToChar(keys.Keys["K5"].Value);
                Keys[5] = ToChar(keys.Keys["K6"].Value);
                Keys[6] = ToChar(keys.Keys["K7"].Value);
                Keys[7] = ToChar(keys.Keys["K8"].Value);
                Keys[8] = ToChar(keys.Keys["K9"].Value);
                //

            }
            catch(FileNotFoundException e)
            {
                Debug(e.Message);
                return false;
            }

            GameKeys = new Char[Columns];
            GameKeysLoad();

            Debug(
                "Debug: " + Environment.NewLine +
                "   Rows: " + Rows + Environment.NewLine +
                "   Columns: " + Columns + Environment.NewLine +
                "   VerticalFlip: " + VerticalFlip + Environment.NewLine +
                "   Name: " + Name + Environment.NewLine +
                "   Keys: " + string.Join(" ", Keys) + Environment.NewLine + Environment.NewLine
                );
            return true;
        }

        private static bool CreateIni()
        {
            int Indentation = 4;
            // Create new file with a default formatting.
            IniFile file = new IniFile(iniOptions());

            // Add new section.
            //IniSection Debug = file.Sections.Add("Debug");
            // Add trailing comment.
            //Debug.TrailingComment.Text = "Debug Info";

            // Add new key and its value.
            //IniKey DebugToConsole = Debug.Keys.Add("DebugToConsole", "true");
            //Add leading comment.
            //DebugToConsole.TrailingComment.Text = "If true, will print debuggin info in the console";
            //Set Key Indentation
            //DebugToConsole.LeftIndentation = Indentation;
            //
            //
            IniSection Settings = file.Sections.Add("Settings");
            Settings.TrailingComment.Text = "You can change these values to customize gameplay experience \\:D/";
            //
            IniKey Rows = Settings.Keys.Add("Rows", "10");
            Rows.LeftIndentation = Indentation;
            Rows.TrailingComment.Text = "How much rows will the game load.";
            Rows.TrailingComment.LeftIndentation = Indentation;
            //
            IniKey Columns = Settings.Keys.Add("Columns", "4");
            Columns.LeftIndentation = Indentation;
            Columns.TrailingComment.Text = "How much keys do you wanna use.(1-9)";
            Columns.TrailingComment.LeftIndentation = Indentation;
            //
            IniKey verticalFlip = Settings.Keys.Add("VerticalFlip", "true");
            verticalFlip.LeftIndentation = Indentation;
            verticalFlip.TrailingComment.Text = "If set to true, game will scroll down.";
            verticalFlip.TrailingComment.LeftIndentation = Indentation;
            //
            IniKey Name = Settings.Keys.Add("Name", "Guest");
            Name.LeftIndentation = Indentation;
            Name.TrailingComment.Text = "Wich name your scores will have.";
            Name.TrailingComment.LeftIndentation = Indentation;
            //
            //
            IniSection Keys = file.Sections.Add("Keys");
            Keys.TrailingComment.Text = "Do I really need to comment this?";
            //
            IniKey K1 = Keys.Keys.Add("K1", "a");
            K1.LeftIndentation = Indentation;
            //
            IniKey K2 = Keys.Keys.Add("K2", "s");
            K2.LeftIndentation = Indentation;
            //
            IniKey K3 = Keys.Keys.Add("K3", "d");
            K3.LeftIndentation = Indentation;
            //
            IniKey K4 = Keys.Keys.Add("K4", "f");
            K4.LeftIndentation = Indentation;
            //
            IniKey K5 = Keys.Keys.Add("K5", " ");
            K5.LeftIndentation = Indentation;
            //
            IniKey K6 = Keys.Keys.Add("K6", "j");
            K6.LeftIndentation = Indentation;
            //
            IniKey K7 = Keys.Keys.Add("K7", "k");
            K7.LeftIndentation = Indentation;
            //
            IniKey K8 = Keys.Keys.Add("K8", "l");
            K8.LeftIndentation = Indentation;
            //
            IniKey K9 = Keys.Keys.Add("K9", "´");
            K9.LeftIndentation = Indentation;

            // Save file.
            try
            {
                file.Save(iniPath);
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public static void GameKeysLoad()
        {
            List<int> keys = new List<int>();
            if (Columns > 9) { Columns = 9; }
            if (Columns < 1) { Columns = 1; }

            int k = 9 - Columns;
            if (k % 2 == 0)
            {
                for (int i = 0; i < Columns; i++)
                {
                    GameKeys[i] = Keys[((k / 2) + i)];
                }
            }
            else
            {
                int half = Columns / 2;
                for (int i = 0; i < Columns; i++)
                {
                    if(i < half)
                    {
                        GameKeys[i] = Keys[((k / 2) + i)];
                    }
                    else
                    {
                        GameKeys[i] = Keys[((k / 2) + i + 1)];
                    }
                    
                }
            }
        }

        public static void Debug(object debugInfo)
        {
            if (DebugToConsole)
            {
                Console.WriteLine(debugInfo);
            }
        }

        public static char ToChar(string value)
        {
            if (value == "")
            {
                return ' ';
            }
            return Convert.ToChar(value);
        }
    }
}
