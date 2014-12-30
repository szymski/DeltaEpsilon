using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace DeltaEpsilon.Engine.Utils
{
    public class GraphicsHelper
    {
        public static void Enable2DTexture() => Gl.glEnable(Gl.GL_TEXTURE_2D);
        public static void Disable2DTexture() => Gl.glDisable(Gl.GL_TEXTURE_2D);

        public static void DrawQuad(float x, float y, float w, float h)
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glVertex2f(x, y);
            Gl.glVertex2f(x + w, y);
            Gl.glVertex2f(x + w, y + h);
            Gl.glVertex2f(x, y + h);
            Gl.glEnd();
        }

        public static void DrawQuadWithUV(float x, float y, float w, float h)
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3f(x, y, 0);
            Gl.glTexCoord2f(1, 0);
            Gl.glVertex3f(x + w, y, 0);
            Gl.glTexCoord2f(1, 1);
            Gl.glVertex3f(x + w, y + h, 0);
            Gl.glTexCoord2f(0, 1);
            Gl.glVertex3f(x, y + h, 0);
            Gl.glEnd();
        }

        public static void DrawTexturedQuad(float x, float y, float w, float h)
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            DrawQuadWithUV(x, y, w, h);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
        }

        public static void DrawQuadWithClippedUV(float x, float y, float w, float h, float textureWidth, float textureHeight, float xOffset, float yOffset, float xSize, float ySize)
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glTexCoord2f(xOffset / textureWidth, yOffset / textureHeight);
            Gl.glVertex3f(x, y, 0);
            Gl.glTexCoord2f((xSize + xOffset)/textureWidth, yOffset / textureHeight);
            Gl.glVertex3f(x + w, y, 0);
            Gl.glTexCoord2f((xSize + xOffset) / textureWidth, (ySize + yOffset) / textureHeight);
            Gl.glVertex3f(x + w, y + h, 0);
            Gl.glTexCoord2f(xOffset / textureWidth, (ySize + yOffset) / textureHeight);
            Gl.glVertex3f(x, y + h, 0);
            Gl.glEnd();
        }

        public static void DrawOutlineQuad(float x, float y, float w, float h)
        {
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2f(x-1, y-1);
            Gl.glVertex2f(x-1 + w+2, y-1);
            Gl.glVertex2f(x-1 + w+2, y-1 + h+2);
            Gl.glVertex2f(x-1, y-1 + h+2);
            Gl.glVertex2f(x - 1, y - 1);
            Gl.glEnd();
        }

        public static void SetupOrtho(int zNear = -1, int zFar = 1)
        {
            Gl.glLoadIdentity();
            Gl.glViewport(0, 0, Screen.Width, Screen.Height);
            Gl.glOrtho(0, Screen.Width, Screen.Height, 0, zNear, zFar);
        }

    }
}
