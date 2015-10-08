using DeltaEpsilon.Engine.Input;
using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine
{
    public sealed class Mouse
    {
        /// <summary>
        /// Returns true if specified button is hold
        /// </summary>
        /// <param name="button">Button id</param>
        /// <returns></returns>
        public static bool GetButton(int button) => InputController.Instance.GetMouseButton(button);

        /// <summary>
        /// Returns true when button is released
        /// </summary>
        /// <param name="button">Button id</param>
        /// <returns></returns>
        public static bool GetButtonUp(int button) => InputController.Instance.GetMouseButtonUp(button);

        /// <summary>
        /// Returns true once when button is pressed
        /// </summary>
        /// <param name="button">Button id</param>
        /// <returns></returns>
        public static bool GetButtonDown(int button) => InputController.Instance.GetMouseButtonDown(button);

        public static void DisableButton(int button) => InputController.Instance.disabledButtons.Add(button);


        public static bool FPS
        {
            get { return InputController.Instance.fps; }
            set { InputController.Instance.fps = value; }
        }

        public static bool CursorVisible
        {
            get { return InputController.Instance.mouseCursorVisible; }
            set { InputController.Instance.mouseCursorVisible = value; }
        }

        public static Vector2 Acceleration => InputController.Instance.acceleration;

        public static Vector2 Position => InputController.Instance.MousePosition;
        public static Vector2 WorldPosition => App.AppInstance.GetWorldMousePosition();

        public static int MouseWheelDelta => InputController.Instance.mouseWhellDelta;
    }
}
