using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Input
{
    public sealed class Keyboard
    {
        public static bool GetKey(KeyCode key) => InputController.Instance.GetKey(key);
        public static bool GetKeyUp(KeyCode key) => InputController.Instance.GetKeyUp(key);
        public static bool GetKeyDown(KeyCode key) => InputController.Instance.GetKeyDown(key);



        public static List<KeyCode> PressedKeys => InputController.Instance.pressedKeys;
    }
}
