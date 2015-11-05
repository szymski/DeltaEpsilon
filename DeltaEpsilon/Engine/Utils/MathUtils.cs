using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    public static class MathUtils
    {
        public static float Sin(float degrees)
        {
            return (float)Math.Sin(Math.PI * degrees / 180f);
        }

        public static float Cos(float degrees)
        {
            return (float)Math.Cos(Math.PI * degrees / 180f);
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        public static float Lerp(float t, float from, float to)
        {
            return (1 - t) * from + t * to;
        }

        static Random random = new Random();

        public static float Random(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        #region Vector functions

        public static Vector2 Lerp(float t, Vector2 from, Vector2 to)
        {
            return (1 - t) * from + t * to;
        }

        public static Vector2 MoveTowards(float t, Vector2 from, Vector2 to)
        {
            Vector2 diff = to - from;
            return from + diff.Normalized * t;
        }

        #endregion
    }
}
