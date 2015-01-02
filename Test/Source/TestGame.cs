using DeltaEpsilon.Engine;
using DeltaEpsilon.Engine.Input;
using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl.Gl;

namespace Test.Source
{
    class TestGame : App
    {
        public TestGame()
        {
            Initialize();
            InitializeInput();
            InitializeGraphics();
            Run();
        }

        float asd = 0;

        public override void Render()
        {
            Graphics.RenderWindow.Clear();
            GraphicsHelper.SetupOrtho();

            glColor3f(1, 0, 0);
            GraphicsHelper.DrawQuad(asd, 0, 256, 256);

            Graphics.RenderWindow.Display();
        }

        public override void Update()
        {
            asd += (Keyboard.GetKey(KeyCode.D) ? 1 : -1) * Time.DeltaTime * 100;
        }
    }
}
