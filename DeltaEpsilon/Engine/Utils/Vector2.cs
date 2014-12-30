using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    [Serializable]
    public class Vector2
    {
        public float x = 0;
        public float y = 0;

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float length
        {
            get
            {
                return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            }
        }

        public float angle
        {
            get
            {
                return (float)(Math.Atan2(x, y) * (180 / Math.PI));
            }
        }

        public Vector2 floored
        {
            get
            {
                return new Vector2((float)Math.Floor(x), (float)Math.Floor(y));
            }
        }

        public Vector2 rounded
        {
            get
            {
                return new Vector2((float)Math.Round(x), (float)Math.Round(y));
            }
        }

        public Vector2 normalized
        {
            get
            {
                float len = length;
                return new Vector2(x/len, y/len);
            }
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator *(Vector2 v1, float value)
        {
            return new Vector2(v1.x * value, v1.y * value);
        }

        public static Vector2 operator /(Vector2 v1, float value)
        {
            return new Vector2(v1.x / value, v1.y / value);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return (v1.x == v2.x && v1.y == v2.y);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1.x == v2.x && v1.y == v2.y);
        }

        public override string ToString()
        {
            return x + ", " + y;
        }

        public static bool IsInRange(Vector2 position, Vector2 size, Vector2 vector)
        {
            if (vector.x > position.x && vector.y > position.y && vector.x < position.x + size.x && vector.y < position.y + size.y) return true;
            return false;
        }

        public static float Distance(Vector2 vec1, Vector2 vec2)
        {
            return (float)Math.Sqrt(Math.Pow((vec1 - vec2).x, 2) + Math.Pow((vec1 - vec2).y, 2));
        }
    }
}
