using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Todesesser.Core
{
    public class GameFunctions
    {
        public static double GetAngle(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2((pos2.Y - pos1.Y) , (pos2.X - pos1.X)) - MathHelper.PiOver2;
        }

        public static Texture2D CreateBlankTexture(GraphicsDevice device)
        {
            Texture2D blank = new Texture2D(device, 1, 1, true, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            return blank;
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

        public static void DrawLine(SpriteBatch batch, Texture2D spr, Vector2 a, Vector2 b, Color col, Vector2 offset)
        {
            Vector2 Origin = new Vector2(0.5f, 0.0f);
            Vector2 diff = b - a;
            float angle;
            Vector2 Scale = new Vector2(1.0f, diff.Length() / spr.Height);

            angle = (float)(Math.Atan2(diff.Y, diff.X)) - MathHelper.PiOver2;

            batch.Draw(spr, new Vector2(a.X - offset.X, a.Y - offset.Y), null, col, angle, Origin, Scale, SpriteEffects.None, 1.0f);
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        public static List<Point> BresenhamLine(int x0, int y0, int x1, int y1)
        {
            // Optimization: it would be preferable to calculate in 
            // advance the size of "result" and to use a fixed-size array 
            // instead of a list. 

            List<Point> result = new List<Point>();

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = 0;
            int ystep;
            int y = y0;
            if (y0 < y1) ystep = 1; else ystep = -1;
            for (int x = x0; x <= x1; x++)
            {
                if (steep) result.Add(new Point(y, x));
                else result.Add(new Point(x, y));
                error += deltay;
                if (2 * error >= deltax)
                {
                    y += ystep;
                    error -= deltax;
                }
            }

            return result;
        }
    }
}
