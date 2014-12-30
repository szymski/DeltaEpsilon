using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DeltaEpsilon.Engine
{
    public class Graphics
    {

        public Graphics()
        {
            Instance = this;
            CreateWindow();
        }

        RenderWindow window;

        public void CreateWindow()
        {
            ContextSettings contextSettings = new ContextSettings()
            {
                MajorVersion = 255,
                MinorVersion = 255,
                DepthBits = 32,
                AntialiasingLevel = 4
            };
            window = new RenderWindow(new VideoMode((uint)App.AppInstance.Configuration.GetOrDefault("width", 1280), (uint)App.AppInstance.Configuration.GetOrDefault("height", 720)), "DeltaEpsilon", Styles.Default, contextSettings);
            window.SetActive(true);
            window.SetFramerateLimit(120);
            window.SetKeyRepeatEnabled(false);
            window.Closed += delegate { window.Close(); App.AppInstance.isRunning = false; };

            //_window.KeyPressed += Input.instance.KeyPressed;
            //_window.KeyReleased += Input.instance.KeyReleased;
            //_window.GainedFocus += Input.instance.GainedFocus;
            //_window.LostFocus += Input.instance.LostFocus;
            //_window.MouseButtonPressed += Input.instance.MouseButtonPressed;
            //_window.MouseButtonReleased += Input.instance.MouseButtonReleased;
            //_window.MouseWheelMoved += Input.instance.MouseWheelMoved;

            window.Resized += (sender, e) =>
            {
                App.AppInstance.Configuration["width"] = e.Width;
                App.AppInstance.Configuration["height"] = e.Height;
                window.SetView(new SFML.Graphics.View(new FloatRect(0, 0, e.Width, e.Height)));
            };
        }

        public static RenderWindow RenderWindow => Instance.window;
        public static Graphics Instance { get; private set; }
    }
}