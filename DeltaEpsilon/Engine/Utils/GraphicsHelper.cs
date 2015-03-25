using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace DeltaEpsilon.Engine.Utils
{
    public class GraphicsHelper
    {
        public static void Enable2DTexture() => GL.Enable(EnableCap.Texture2D);
        public static void Enable2DTextureB() { GL.Enable(EnableCap.Texture2D); GL.Enable(EnableCap.Blend); }
        public static void Disable2DTexture() => GL.Disable(EnableCap.Texture2D);
        public static void Disable2DTextureB() { GL.Disable(EnableCap.Texture2D); GL.Disable(EnableCap.Blend); }

        public static void DrawQuad(float x, float y, float w, float h)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(x, y);
            GL.Vertex2(x + w, y);
            GL.Vertex2(x + w, y + h);
            GL.Vertex2(x, y + h);
            GL.End();
        }

        public static void DrawQuadWithUV(float x, float y, float w, float h)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0,0);
            GL.Vertex2(x, y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(x + w, y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(x + w, y + h);
            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y + h);
            GL.End();
        }

        public static void DrawTexturedQuad(float x, float y, float w, float h)
        {
            GL.Enable(EnableCap.Texture2D);
            DrawQuadWithUV(x, y, w, h);
            GL.Disable(EnableCap.Texture2D);
        }

        public static void DrawQuadWithClippedUV(float x, float y, float w, float h, float textureWidth, float textureHeight, float xOffset, float yOffset, float xSize, float ySize)
        {
            GL.End();
            GL.TexCoord2(xOffset / textureWidth, yOffset / textureHeight);
            GL.Vertex2(x, y);
            GL.TexCoord2((xSize + xOffset) / textureWidth, yOffset / textureHeight);
            GL.Vertex2(x + w, y);
            GL.TexCoord2((xSize + xOffset) / textureWidth, (ySize + yOffset) / textureHeight);
            GL.Vertex2(x + w, y + h);
            GL.TexCoord2(xOffset / textureWidth, (ySize + yOffset) / textureHeight);
            GL.Vertex2(x, y + h);
            GL.End();
        }

        public static void DrawOutlineQuad(float x, float y, float w, float h)
        {
            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(x -1, y-1);
            GL.Vertex2(x -1 + w+2, y-1);
            GL.Vertex2(x -1 + w+2, y-1 + h+2);
            GL.Vertex2(x -1, y-1 + h+2);
            GL.Vertex2(x - 1, y - 1);
            GL.End();
        }

        public static void SetupFullscreenOrtho(int zNear = -1, int zFar = 1)
        {
            GL.LoadIdentity();
            GL.Viewport(0, 0, Screen.Width, Screen.Height);
            GL.Ortho(0, Screen.Width, Screen.Height, 0, zNear, zFar);
        }

    }
}
