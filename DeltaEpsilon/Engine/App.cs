using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    public abstract class App
    {
        public Configuration Configuration { get; private set; }

        public void Initialize()
        {
            AppInstance = this;
            Log.ClearLog();
            Log.Print("Initializing DeltaEpsilon");
            Configuration = Configuration.LoadFromFile("config.cfg", true);
        }

        public void InitializeGraphics()
        {
            Log.Print("Initializing Graphics");
            new Graphics();
        }

        public Stopwatch timer = new Stopwatch();
        public Stopwatch timer2 = new Stopwatch();
        public Stopwatch timer3 = new Stopwatch();
        public float deltaTime = 0f;
        public float timeScale = 1f;

        public int FPS = 0;
        public float eFPS = 0;
        public float updateDelta = 0;
        public int ticks = 0;
        public float time = 0;
        public long millis = 0;

        public bool isRunning = true;

        public void Run()
        {
            timer2.Start();
            timer3.Start();

            while (isRunning) Loop();

            Close();
        }

        public void Loop()
        {
            timer.Start();

            Graphics.RenderWindow.DispatchEvents();

            Update();

            updateDelta = ((float)timer.Elapsed.TotalMilliseconds / 1000f);

            Render();

            time += (float)timer.Elapsed.TotalMilliseconds;
            millis = timer3.ElapsedMilliseconds;
            if (timer2.ElapsedMilliseconds >= 1000)
            {
                FPS = ticks;
                time -= 1000;
                ticks = 0;
                timer2.Restart();

            }

            deltaTime = ((float)timer.Elapsed.TotalMilliseconds / 1000f);
            timer.Reset();
            ticks++;
        }

        public virtual void Close()
        {
            Log.Print("Closing DeltaEpsilon");
            Configuration.SaveToFile("config.cfg");
        }

        public abstract void Update();

        public abstract void Render();

        public static App AppInstance { get; private set; }
    }
}
