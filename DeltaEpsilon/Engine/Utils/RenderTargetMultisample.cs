using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace DeltaEpsilon.Engine.Utils
{
    public class RenderTargetMultisample
    {
        public int width, height;
        public int id;
        public int texId;
        public int rbId;


        public RenderTargetMultisample(int w, int h)
        {
            width = w;
            height = h;

            // Generate new texture for FBO
            texId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2DMultisample, texId);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
           // GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, (byte[])null);
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgba, width, height, true);
            GL.BindTexture(TextureTarget.Texture2DMultisample, 0);

            prevID = GL.GetInteger(GetPName.FramebufferBinding);

            // Generate FBO
            id = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, texId, 0);

            // Generate render buffer
            rbId = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbId);
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 4, RenderbufferStorage.Depth24Stencil8, width, height);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, rbId);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevID);
        }

        int prevID = 0;

        public void Bind()
        {
            prevID = GL.GetInteger(GetPName.FramebufferBinding);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
        }

        public void UnBind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevID);
        }

        public void BindTexture()
        {
            GL.BindTexture(TextureTarget.Texture2DMultisample, texId);
        }

        public void SetSize(int w, int h)
        {
            if (w != width && h != height)
            {
                width = w;
                height = h;

                GL.DeleteRenderbuffer(rbId);
                GL.DeleteFramebuffer(id);
                GL.DeleteTexture(texId);

                // Generate new texture for FBO
                texId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2DMultisample, texId);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                // GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, (byte[])null);
                GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgba, width, height, true);
                GL.BindTexture(TextureTarget.Texture2DMultisample, 0);

                prevID = GL.GetInteger(GetPName.FramebufferBinding);

                // Generate FBO
                id = GL.GenFramebuffer();
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, texId, 0);

                // Generate render buffer
                rbId = GL.GenRenderbuffer();
                GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rbId);
                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, 4, RenderbufferStorage.Depth24Stencil8, width, height);
                GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
                GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, rbId);

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, prevID);
            }
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK.Graphics.OpenGL;

//namespace DeltaEpsilon.Engine.Utils
//{
//    public class RenderTarget
//    {
//        public int width, height;
//        public int id;
//        public int texId;


//        public RenderTarget(int w, int h)
//        {
//            GL.Enable(EnableCap.Multisample);


//            width = w;
//            height = h;

//            texId = GL.GenTexture();
//            GL.BindTexture(TextureTarget.Texture2D, texId);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
//            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
//            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, (byte[])null);
//            //GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgba8, width, height, false);

//            id = GL.GenFramebuffer();
//            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
//            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texId, 0);
//        }

//        public void Bind()
//        {
//            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);
//        }

//        public void BindTexture()
//        {
//            GL.BindTexture(TextureTarget.Texture2D, texId);
//        }

//        public void SetSize(int w, int h)
//        {
//            width = w;
//            height = h;
//            GL.BindTexture(TextureTarget.Texture2D, texId);
//            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, w, h, 0, PixelFormat.Bgra, PixelType.UnsignedByte, (byte[])null);
//        }
//    }
//}

