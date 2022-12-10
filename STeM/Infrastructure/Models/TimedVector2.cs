using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace STeM.Infrastructure.Models
{
    public class TimedVector2
    {
        public TimedVector2(DateTime time, Vector2 vector2)
        {
            Time = time;
            Vector2 = vector2;
        }

        public DateTime Time { get; private set; }
        public Vector2 Vector2 { get; private set; }
    }
}
