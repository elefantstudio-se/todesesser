using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Todesesser.Core
{
    public class GameFunctions
    {
        public static double GetAngle(Vector2 pos1, Vector2 pos2)
        {
            double feta = Math.Atan((pos2.Y - pos1.Y) / (pos2.X - pos1.X));
            if (pos2.Y < pos1.Y)
            {
                feta += MathHelper.ToRadians(180);
            }
            return feta + MathHelper.ToRadians(90);
        }
    }
}
