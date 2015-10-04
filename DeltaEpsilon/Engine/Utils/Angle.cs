using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaEpsilon.Engine.Utils
{
    public class Angle
    {
        public float pitch = 0f, yaw = 0f, roll = 0f;

        public Angle()
        {

        }

        public Angle(float p, float y, float r)
        {
            pitch = p;
            yaw = y;
            roll = r;
        }

        public Vector3 Forward => new Vector3(MathUtils.Sin(yaw) * MathUtils.Cos(pitch), -MathUtils.Sin(pitch), MathUtils.Cos(yaw) * MathUtils.Cos(pitch));
    }
}
