using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Input
{
    public sealed class Mouse
    {
        public static bool GetButton(int button) => InputController.Instance.GetMouseButton(button);
        public static bool GetButtonUp(int button) => InputController.Instance.GetMouseButtonUp(button);
        public static bool GetButtonDown(int button) => InputController.Instance.GetMouseButtonDown(button);

        public static Vector2 MousePosition => InputController.Instance.MousePosition;
        public static int MouseWheelDelta => InputController.Instance.mouseWhellDelta;
    }
}
