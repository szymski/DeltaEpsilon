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
        static Dictionary<string, TimerEntry> everyTimers = new Dictionary<string, TimerEntry>();

        public static Dictionary<string, TimerEntry> Timers => standardTimers;

        /// <summary>
        /// Creates a simple timer with specified delay.
        /// </summary>
        /// <param name="delay">Delay in seconds</param>
        /// <param name="callback">Function to call</param>
        public static void Simple(float delay, Action callback)
        {
            simpleTimers.Add(new TimerEntry()
            {
                callback = callback,
                delay = delay,
                nextMillis = Time.Millis + (long)(delay * 1000)
            });
        }

        /// <summary>
        /// Calls function after specified time. Use only in loops.
        /// </summary>
        /// <param name="id">Timer Id</param>
        /// <param name="delay">Delay in seconds</param>
        /// <param name="callback">Function to call</param>
        public static void Every(string id, float delay, Action callback)
        {
            if (!everyTimers.ContainsKey(id))
            {
                everyTimers.Add(id, new TimerEntry()
                {
                    callback = callback,
                    nextMillis = Time.Millis + (long)(delay * 1000)
                });
            }
            else if(everyTimers[id].nextMillis <= Time.Millis)
            {
                everyTimers[id].callback();
                everyTimers.Remove(id);
            }
        }

        /// <summary>
        /// Creates a timer with specified id and executes after delay with x repetitions.
        /// </summary>
        /// <param name="id">Unique timer Id</param>
        /// <param name="delay">Delay in seconds</param>
        /// <param name="repetitions">Repetition count</param>
        /// <param name="callback">Function to call</param>
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

        /// <summary>
        /// Starts the timer if stopped.
        /// </summary>
        /// <param name="id">Timer Id</param>
        public static void Start(string id)
        {
            if (standardTimers[id] != null)
                standardTimers[id].active = true;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        /// <param name="id">Timer Id</param>
        public static void Stop(string id)
        {
            if (standardTimers[id] != null)
                standardTimers[id].active = false;
        }

        /// <summary>
        /// Removes a timer.
        /// </summary>
        /// <param name="id">Timer Id</param>
        public static void Remove(string id)
        {
            standardTimers.Remove(id);
        }

        /// <summary>
        /// Updates all timers. Use only in main loop.
        /// </summary>
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
