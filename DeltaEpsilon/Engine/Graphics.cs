using DeltaEpsilon.Engine.Input;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

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
            new OpenTK.GameWindow();

            ContextSettings contextSettings = new ContextSettings()
            {
                MajorVersion = 32,
                MinorVersion = 32,
                DepthBits = 32,
                AntialiasingLevel = 4
            };
            window = new RenderWindow(new VideoMode((uint)App.AppInstance.Configuration.GetOrDefault("width", 1280), (uint)App.AppInstance.Configuration.GetOrDefault("height", 720)), "DeltaEpsilon", Styles.Default, contextSettings);
            window.SetActive(true);
            window.SetFramerateLimit(120);
            window.Closed += delegate { window.Close(); App.AppInstance.isRunning = false; };

            InputController.Instance.InitWindow(window);

            window.Resized += (sender, e) =>
            {
                App.AppInstance.Configuration["width"] = e.Width;
                App.AppInstance.Configuration["height"] = e.Height;
                window.SetView(new SFML.Graphics.View(new FloatRect(0, 0, e.Width, e.Height)));
            };
        }

        Dictionary<string, Utils.Texture> textures = new Dictionary<string, Utils.Texture>(); 

        public static Utils.Texture GetTexture(string filename)
        {
            if (!Instance.textures.ContainsKey(filename))
            {
                int id = GL.GetInteger(GetPName.Texture2D);
                Instance.textures.Add(filename, new Utils.Texture(FS.CreateStream(filename)));
                GL.BindTexture(TextureTarget.Texture2D, id);
            }
            return Instance.textures[filename];
        }

        public static RenderWindow RenderWindow => Instance.window;
        public static Graphics Instance { get; private set; }
    }
}