using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    public sealed class Screen
    {
        public static int Width => (int)Graphics.RenderWindow.Size.X;
        public static int Height => (int)Graphics.RenderWindow.Size.Y;
        public static Vector2 Size => new Vector2(Width, Height);
        public static Vector2 DesktopSize => new Vector2(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
    }
}
