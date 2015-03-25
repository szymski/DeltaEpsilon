using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    public class Texture : SFML.Graphics.Texture
    {
        public Texture(Stream s) : base(s)
        {

        }

        public void Bind()
        {
            SFML.Graphics.Texture.Bind(this);
        }
    }
}
