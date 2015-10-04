using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    [Serializable]
    public class Vector3
    {
        bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector3)obj);
        }

        public float x = 0;
        public float y = 0;
        public float z = 0;

        public Vector3()
        {

        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Normalized
        {
            get
            {
                float inverse = 1 / Magnitude;

                return new Vector3(
                    x * inverse,
                    y * inverse,
                    z * inverse);
            }
        }

        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(SumComponentSqrs());
            }
        }

        public Vector3 Rounded
        {
            get
            {
                return new Vector3((float)Math.Round(x), (float)Math.Round(y), (float)Math.Round(z));
            }
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator *(Vector3 v1, float value)
        {
            return new Vector3(v1.x * value, v1.y * value, v1.z * value);
        }

        public static Vector3 operator /(Vector3 v1, float value)
        {
            return new Vector3(v1.x / value, v1.y / value, v1.z / value);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !(v1.x == v2.x && v1.y == v2.y && v1.z == v2.z);
        }

        public static float SumComponents(Vector3 v1)
        {
            return v1.x + v1.y + v1.z;
        }

        public float SumComponents()
        {
            return SumComponents(this);
        }

        public static float SumComponentSqrs(Vector3 v1)
        {
            Vector3 v2 = SqrComponents(v1);
            return v2.SumComponents();
        }

        public float SumComponentSqrs()
        {
            return SumComponentSqrs(this);
        }

        public static Vector3 SqrComponents(Vector3 v1)
        {
            return new Vector3(
                v1.x * v1.x,
                v1.y * v1.y,
                v1.z * v1.z);
        }

        public Vector3 SqrComponents()
        {
            return SqrComponents(this);
        }

        public override string ToString()
        {
            return x + ", " + y;
        }
    }
}
