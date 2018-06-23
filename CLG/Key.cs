using System;
using System.Threading;
namespace CLG
{
    class Key
    {
        private string[] gameString;
        private char keyId;

        public string[] GameString { get => gameString; set => gameString = value; }
        public char KeyId { get => keyId; set => keyId = value; }

        public Key()
        {

            int num = new Random(DateTime.Now.Millisecond).Next(0, Config.Columns);
            gameString = new string[Config.Columns];
            //
            for (int i = 0; i < Config.Columns; i++)
            {
                if (i == num)
                {
                    KeyId = Config.GameKeys[i];
                    gameString[i] ="[█" + KeyId + "█]";
                }
                else
                {
                    gameString[i] = "[   ]";
                }
            }
            Config.Debug(keyId);
            Config.Debug(gameString);
        }
    }
}
