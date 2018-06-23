using System;

namespace CLG
{
    class Score
    {
        public static int Standard(int combo, int elapseds)
        {
            return Convert.ToInt32((10 * (combo * combo ) * Config.Columns) / ((++elapseds) * (40f / Config.Rows)));
        }
    }
}
