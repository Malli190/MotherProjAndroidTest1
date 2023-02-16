using Game.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Game
{
    public static class MathHelper
    {
        public static float AngleVector(Location a, Location b)
        {
            Location c = b - a;
            
            return (float)(Math.Atan2(c.y, c.x) + Math.PI / 2);
        }
        public static float ChisloFromProcent(float procent, float max)
        {
            return max * (procent / 100);
        }
        public static float ProcentFromChislo(float procent, float max)
        {
            return (procent / max) * 100;
        }
    }
}
