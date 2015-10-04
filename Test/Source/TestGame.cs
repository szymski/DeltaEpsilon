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
            RenderScene();
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

        public Vector2 rot = new Vector2();
        public Vector3 pos = new Vector3();

        public void RenderScene()
        {
            GraphicsHelper.Disable2DTexture();

            GL.Rotate(rot.y, -1, 0, 0);
            GL.Rotate(rot.x, 0, -1, 0);

            GL.Translate(-pos.x, -pos.y, -pos.z);

            GL.PushMatrix();
            GL.Translate(0f, 0f, -4);
            GL.Rotate(Time.Millis * 0.2 % 360, 1.0f, 1.0f, 1.0f);


            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1f, 0f, 0f);

            GL.Vertex3(-0.25f, -0.25f, -0.25f);
            GL.Vertex3(-0.25f, 0.25f, -0.25f);
            GL.Vertex3(0.25f, 0.25f, -0.25f);
            GL.Vertex3(0.25f, -0.25f, -0.25f);

            GL.Color3(0f, 1f, 0f);

            GL.Vertex3(-0.25f, -0.25f, -0.25f);
            GL.Vertex3(0.25f, -0.25f, -0.25f);
            GL.Vertex3(0.25f, -0.25f, 0.25f);
            GL.Vertex3(-0.25f, -0.25f, 0.25f);

            GL.Color3(0f, 0f, 1f);

            GL.Vertex3(-0.25f, -0.25f, -0.25f);
            GL.Vertex3(-0.25f, -0.25f, 0.25f);
            GL.Vertex3(-0.25f, 0.25f, 0.25f);
            GL.Vertex3(-0.25f, 0.25f, -0.25f);

            GL.Color3(0f, 1f, 1f);

            GL.Vertex3(-0.25f, -0.25f, 0.25f);
            GL.Vertex3(0.25f, -0.25f, 0.25f);
            GL.Vertex3(0.25f, 0.25f, 0.25f);
            GL.Vertex3(-0.25f, 0.25f, 0.25f);

            GL.Color3(1f, 0f, 1f);

            GL.Vertex3(-0.25f, 0.25f, -0.25f);
            GL.Vertex3(-0.25f, 0.25f, 0.25f);
            GL.Vertex3(0.25f, 0.25f, 0.25f);
            GL.Vertex3(0.25f, 0.25f, -0.25f);

            GL.Color3(1f, 1f, 0f);

            GL.Vertex3(0.25f, -0.25f, -0.25f);
            GL.Vertex3(0.25f, 0.25f, -0.25f);
            GL.Vertex3(0.25f, 0.25f, 0.25f);
            GL.Vertex3(0.25f, -0.25f, 0.25f);

            GL.End();
            GL.PopMatrix();

            GL.Translate(0f, -2f, 0);


            GL.Scale(20f, 20f, 20f);
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1f, 1f, 1f);

            GL.Vertex3(-0.25f, -0.25f, -0.25f);
            GL.Vertex3(-0.25f, -0.25f, 0.25f);
            GL.Vertex3(0.25f, -0.25f, 0.25f);
            GL.Vertex3(0.25f, -0.25f, -0.25f);

            GL.End();


            //Graphics.GetTexture("lol.png").Bind();

            //GL.Color3(1, 1, (Time.Millis / 1000f) % 1);
            //GL.Enable(EnableCap.Texture2D);
            //GraphicsHelper.DrawQuadWithUV(0, 0, 1, 1);
        }

        public override void Update()
        {
            rot += Mouse.Acceleration * Time.DeltaTime * 4;

            rot.y = MathUtils.Clamp(rot.y, -90, 90);

            if (Keyboard.GetKey(KeyCode.W))
                pos += new Angle(rot.y, rot.x, 0).Forward * Time.DeltaTime * -5;

            if (Keyboard.GetKey(KeyCode.S))
                pos += new Angle(rot.y, rot.x, 0).Forward * Time.DeltaTime * 5;

            if (Keyboard.GetKey(KeyCode.A))
                pos += new Angle(0, rot.x + 90, 0).Forward * Time.DeltaTime * -5;

            if (Keyboard.GetKey(KeyCode.D))
                pos += new Angle(0, rot.x - 90, 0).Forward * Time.DeltaTime * -5;

            if (Keyboard.GetKey(KeyCode.Space))
                pos += new Vector3(0, 1, 0) * Time.DeltaTime * 5;

            if (Keyboard.GetKeyDown(KeyCode.Escape))
                isRunning = false;

            if (Mouse.GetButtonDown(0))
            {
                Audio.PlaySound("test1.wav");
            }
            if (Mouse.GetButtonDown(1))
            {
                Audio.PlaySound("test2.wav");
            }
        }
    }
}
