using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TodLighting
{
    public class GameFunctions
    {
        public static double GetAngle(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2((pos2.Y - pos1.Y), (pos2.X - pos1.X)) - MathHelper.PiOver2;
        }

        public static void DrawLine(SpriteBatch batch, Texture2D spr, Vector2 a, Vector2 b, Color col)
        {
            Vector2 Origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = b - a;
            float angle;
            Vector2 Scale = new Vector2(1.0f, diff.Length() / spr.Height);

            angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            batch.Draw(spr, a, null, col, angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }

        public static Vector2 GetVectorProjection(Vector2 start, int rotation, int distance)
        {
            double x = Math.Sin(MathHelper.ToRadians(rotation));
            double y = Math.Cos(MathHelper.ToRadians(rotation));
            x = x * distance;
            y = y * distance;
            x = start.X + x;
            y = start.Y + y;

            return new Vector2(float.Parse(x.ToString()), float.Parse(y.ToString()));
        }
    }
}
