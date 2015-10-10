using DeltaEpsilon.Engine;
using DeltaEpsilon.Engine.Input;
using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Vector2 = DeltaEpsilon.Engine.Utils.Vector2;
using Vector3 = DeltaEpsilon.Engine.Utils.Vector3;

namespace Test.Source
{
    class TestGame : App
    {
        public TestGame()
        {
            Initialize();
            InitializeInput();
            InitializeGraphics();
            InitializeAudio();

            Mouse.FPS = true;

            fullscreenRtMs = new RenderTargetMultisample(Screen.Width, Screen.Height);

            CurrentScene = new SceneMain();

            Run();
        }

        RenderTargetMultisample fullscreenRtMs;
        RenderTarget fullscreenRt;

        public override void Render()
        {
            fullscreenRtMs.SetSize(Screen.Width, Screen.Height);

            // Rendering the scene to RT
            fullscreenRtMs.Bind();
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GraphicsHelper.SetupPerspective(90f, (double)Screen.Width / Screen.Height, 0.01f, 500f);
            GL.LoadIdentity();
            GL.Enable(EnableCap.Multisample);
            scene?.Render();
            fullscreenRtMs.UnBind();
            // Rendering the scene to RT

            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //   GraphicsHelper.SetupPerspective(90f, (double)Screen.Width / Screen.Height, 0.01f, 500f);
            GL.MatrixMode(MatrixMode.Projection);
            GraphicsHelper.SetupFullscreenOrtho();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Disable(EnableCap.DepthTest);

            // Sky renering
            //GraphicsHelper.Disable2DTexture();
            //GL.Begin(PrimitiveType.Quads);

            //GL.Color3(0.1f, 0.7f, 1f);

            //GL.Vertex3(-1f * Screen.Aspect, 1f, -1f);
            //GL.Vertex3(-1f, -1f, -1f);
            //GL.Vertex3(1f, -1f, -1f);
            //GL.Vertex3(1f, 1f, -1f);

            //GL.End();
            // Sky rendering

            // Drawing fullscreen RT
            GL.Enable(EnableCap.Multisample);
            GL.Enable(EnableCap.Texture2D);
            fullscreenRtMs.BindTexture();
            GL.Scale(1, -1, 1);
            GL.Translate(0, 0, -1f);
            GraphicsHelper.DrawQuadWithUV(0, -Screen.Height, Screen.Width, Screen.Height);

            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, fullscreenRtMs.id);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
            GL.BlitFramebuffer(0, 0, Screen.Width, Screen.Height, 0, 0, Screen.Width, Screen.Height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);

            Graphics.RenderWindow.Display();
        }

    }
}
