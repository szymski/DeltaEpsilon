using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaEpsilon.Engine;
using DeltaEpsilon.Engine.Utils;
using OpenTK.Graphics.OpenGL;

namespace Test.Source
{
    class SceneMain : Scene
    {
        public Vector2 rot = new Vector2();
        public Vector3 pos = new Vector3();

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
                App.AppInstance.isRunning = false;

            if (Mouse.GetButtonDown(0))
            {
                Audio.PlaySound("test1.wav");
            }
            if (Mouse.GetButtonDown(1))
            {
                Audio.PlaySound("test2.wav");
            }
        }

        public override void Render()
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
    }
}
