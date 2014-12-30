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

        static Random random = new Random();

        public static float Random(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }
    }
}
