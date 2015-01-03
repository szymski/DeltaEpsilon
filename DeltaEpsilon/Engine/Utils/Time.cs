using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    public class Time
    {
        public static float DeltaTime => App.AppInstance.deltaTime * TimeScale;
        public static float RealDeltaTime => App.AppInstance.deltaTime;
        public static float TimeScale { get; set; } = 1;
        public static long Millis => App.AppInstance.millis;
    }
}
