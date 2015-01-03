using DeltaEpsilon.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Input
{
    internal sealed class InputController
    {
        public InputController()
        {
            Instance = this;
        }

        public void InitWindow(SFML.Window.Window window)
        {
            window.SetKeyRepeatEnabled(false);
            window.KeyPressed += KeyPressed;
            window.KeyReleased += KeyReleased;
            window.GainedFocus += GainedFocus;
            window.LostFocus += LostFocus;
            window.MouseButtonPressed += MouseButtonPressed;
            window.MouseButtonReleased += MouseButtonReleased;
            window.MouseWheelMoved += MouseWheelMoved;
        }

        enum KeyState { None, Down, Up }

        Dictionary<KeyCode, KeyState> keys = new Dictionary<KeyCode, KeyState>();
        internal List<KeyCode> pressedKeys = new List<KeyCode>();
        Dictionary<int, KeyState> mouseButtons = new Dictionary<int, KeyState>();

        internal int mouseWhellDelta = 0;
        internal bool focused = true;

        public void GainedFocus(object sender, EventArgs e)
        {
            focused = true;
        }

        public void LostFocus(object sender, EventArgs e)
        {
            focused = false;
        }

        public void KeyPressed(object sender, EventArgs e)
        {
            SetKey((KeyCode)((SFML.Window.KeyEventArgs)e).Code, KeyState.Down);
            pressedKeys.Add((KeyCode)((SFML.Window.KeyEventArgs)e).Code);
        }

        public void KeyReleased(object sender, EventArgs e)
        {
            SetKey((KeyCode)((SFML.Window.KeyEventArgs)e).Code, KeyState.Up);
        }

        public void MouseWheelMoved(object sender, SFML.Window.MouseWheelEventArgs e)
        {
            mouseWhellDelta = e.Delta;
        }

        public void MouseButtonPressed(object sender, EventArgs e)
        {
            SetMouseButton((int)((SFML.Window.MouseButtonEventArgs)e).Button, KeyState.Down);
        }

        public void MouseButtonReleased(object sender, EventArgs e)
        {
            SetMouseButton((int)((SFML.Window.MouseButtonEventArgs)e).Button, KeyState.Up);
        }

        void SetKey(KeyCode key, KeyState state)
        {
            if (keys.ContainsKey(key)) keys[key] = state;
            else keys.Add(key, state);
        }

        void SetMouseButton(int key, KeyState state)
        {
            if (mouseButtons.ContainsKey(key)) mouseButtons[key] = state;
            else mouseButtons.Add(key, state);
        }

        KeyState _GetKey(KeyCode key)
        {
            if (keys.ContainsKey(key)) return keys[key];
            else return KeyState.None;
        }

        KeyState _GetMouseButton(int button)
        {
            if (mouseButtons.ContainsKey(button)) return mouseButtons[button];
            else return KeyState.None;
        }

        public void Update()
        {
            keys.Clear();
            pressedKeys.Clear();
            mouseButtons.Clear();
            mouseWhellDelta = 0;
        }

        public bool GetKeyDown(KeyCode key)
        {
            return (_GetKey(key) == KeyState.Down);
        }

        public bool GetKey(KeyCode key)
        {
            return SFML.Window.Keyboard.IsKeyPressed((SFML.Window.Keyboard.Key)key);
        }

        public bool GetKeyUp(KeyCode key)
        {
            return (_GetKey(key) == KeyState.Up);
        }

        public bool GetMouseButtonDown(int button)
        {
            return (_GetMouseButton(button) == KeyState.Down);
        }

        public bool GetMouseButton(int button)
        {
            return SFML.Window.Mouse.IsButtonPressed((SFML.Window.Mouse.Button)Enum.ToObject(typeof(SFML.Window.Mouse.Button), button));
        }

        public bool GetMouseButtonUp(int button)
        {
            return (_GetMouseButton(button) == KeyState.Up);
        }

        public Vector2 MousePosition
        {
            get
            {
                SFML.Window.Vector2i vec = SFML.Window.Mouse.GetPosition(Graphics.RenderWindow);
                return new Vector2(vec.X, vec.Y);
            }
        }

        public static InputController Instance { get; private set; }
    }
}
