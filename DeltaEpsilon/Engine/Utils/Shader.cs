using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace DeltaEpsilon.Engine.Utils
{
    public enum ShaderType
    {
        Vertex = 0x1,
        Fragment = 0x2
    }

    public class Shader
    {
        public int ID { get; private set; }
        public Dictionary<string, int> Variables { get; private set; } = new Dictionary<string, int>();

        public Shader(string source, ShaderType type)
        {
            if (type == ShaderType.Fragment)
                Compile("", source);
            else
                Compile(source, "");
        }

        public Shader(string vertexSource, string fragmentSource)
        {
            Compile(vertexSource, fragmentSource);
        }

        public void Bind()
        {
            GL.UseProgram(ID);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }

        private bool Compile(string vertexSource, string fragmentSource)
        {
            int status_code = -1;
            string info = "";

            ID = GL.CreateProgram();

            if (vertexSource != "")
            {
                int vertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
                GL.ShaderSource(vertexShader, vertexSource);
                GL.CompileShader(vertexShader);
                GL.GetShaderInfoLog(vertexShader, out info);
                GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out status_code);

                if (status_code != 1)
                {
                    Console.WriteLine("Failed to Compile Vertex Shader Source." +
                        Environment.NewLine + info + Environment.NewLine + "Status Code: " + status_code.ToString());

                    GL.DeleteShader(vertexShader);
                    GL.DeleteProgram(ID);
                    ID = 0;

                    return false;
                }

                GL.AttachShader(ID, vertexShader);
                GL.DeleteShader(vertexShader);
            }

            if (fragmentSource != "")
            {
                int fragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
                GL.ShaderSource(fragmentShader, fragmentSource);
                GL.CompileShader(fragmentShader);
                GL.GetShaderInfoLog(fragmentShader, out info);
                GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status_code);

                if (status_code != 1)
                {
                    Console.WriteLine("Failed to Compile Fragment Shader Source." +
                        Environment.NewLine + info + Environment.NewLine + "Status Code: " + status_code.ToString());

                    GL.DeleteShader(fragmentShader);
                    GL.DeleteProgram(ID);
                    ID = 0;

                    return false;
                }

                GL.AttachShader(ID, fragmentShader);
                GL.DeleteShader(fragmentShader);
            }

            GL.LinkProgram(ID);
            GL.GetProgramInfoLog(ID, out info);
            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out status_code);

            if (status_code != 1)
            {
                Console.WriteLine("Failed to Link Shader Program." +
                    Environment.NewLine + info + Environment.NewLine + "Status Code: " + status_code.ToString());

                GL.DeleteProgram(ID);
                ID = 0;

                return false;
            }

            return true;
        }

        private int GetVariableLocation(string name)
        {
            if (Variables.ContainsKey(name))
                return Variables[name];

            int location = GL.GetUniformLocation(ID, name);

            if (location != -1)
                Variables.Add(name, location);
            else
                Console.WriteLine("Failed to retrieve Variable Location." +
                    Environment.NewLine + "Variable Name not found.", "Error");

            return location;
        }

        public void SetVariable(string name, float x)
        {
            if (ID > 0)
            {
                GL.UseProgram(ID);

                int location = GetVariableLocation(name);
                if (location != -1)
                   GL.Uniform1(location, x);

                GL.UseProgram(0);
            }
        }

        public void SetVariable(string name, float x, float y)
        {
            if (ID > 0)
            {
                GL.UseProgram(ID);

                int location = GetVariableLocation(name);
                if (location != -1)
                    GL.Uniform2(location, x, y);

                GL.UseProgram(0);
            }
        }

        public void SetVariable(string name, float x, float y, float z)
        {
            if (ID > 0)
            {
                GL.UseProgram(ID);

                int location = GetVariableLocation(name);
                if (location != -1)
                    GL.Uniform3(location, x, y, z);

                GL.UseProgram(0);
            }
        }

        public void SetVariable(string name, float x, float y, float z, float w)
        {
            if (ID > 0)
            {
                GL.UseProgram(ID);

                int location = GetVariableLocation(name);
                if (location != -1)
                    GL.Uniform4(location, x, y, z, w);

                GL.UseProgram(0);
            }
        }

        public void SetVariable(string name, Vector2 vector)
        {
            SetVariable(name, vector.x, vector.y);
        }

    }
}
