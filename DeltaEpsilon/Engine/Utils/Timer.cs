using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    public class Timer
    {
        public class TimerEntry
        {
            public float delay = 0;
            public long nextMillis;
            public bool active = true;
            public int repetitions;
            public Action callback;
        }

        static List<TimerEntry> simpleTimers = new List<TimerEntry>();
        static Dictionary<string, TimerEntry> standardTimers = new Dictionary<string, TimerEntry>();

        public static Dictionary<string, TimerEntry> Timers => standardTimers;

        public static void Simple(float delay, Action callback)
        {
            simpleTimers.Add(new TimerEntry()
            {
                callback = callback,
                delay = delay,
                nextMillis = Time.Millis + (long)(delay * 1000)
            });
        }

        public static void Create(string id, float delay, int repetitions, Action callback)
        {
            if (standardTimers.ContainsKey(id))
                standardTimers.Remove(id);

            standardTimers.Add(id, new TimerEntry()
            {
                callback = callback,
                repetitions = repetitions,
                delay = delay,
                nextMillis = Time.Millis + (long)(delay * 1000)
            });
        }

        public static void Start(string id)
        {
            if (standardTimers[id] != null)
                standardTimers[id].active = true;
        }

        public static void Stop(string id)
        {
            if (standardTimers[id] != null)
                standardTimers[id].active = false;
        }

        public static void Remove(string id)
        {
            standardTimers.Remove(id);
        }

        public static void UpdateTimers()
        {
            // Simple timer updating
            foreach (var timer in simpleTimers)
            {
                if (timer.nextMillis <= Time.Millis)
                {
                    timer.callback();
                    simpleTimers.Remove(timer);
                    break;
                }
            }

            // Standard timer updating
            foreach (var timer in standardTimers)
            {
                if (timer.Value.active && timer.Value.nextMillis <= Time.Millis)
                {
                    timer.Value.callback();
                    timer.Value.nextMillis += (long)(timer.Value.delay*1000);
                    if(timer.Value.repetitions == 0) continue;
                    timer.Value.repetitions--;
                    if (timer.Value.repetitions <= 0)
                    {
                        standardTimers.Remove(timer.Key);
                        break;
                    }
                }
            }
        }
    }
}
