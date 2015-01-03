﻿using DeltaEpsilon.Engine;
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
            InitializeAudio();
            Run();
        }

        public override void Render()
        {
            Graphics.RenderWindow.Clear();
            GraphicsHelper.SetupOrtho();

            glColor3f(1, 0, 0);
            GraphicsHelper.DrawQuad(0, 0, 256, 256);

            Graphics.RenderWindow.Display();
        }

        public override void Update()
        {
            if(Mouse.GetButtonDown(0))
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