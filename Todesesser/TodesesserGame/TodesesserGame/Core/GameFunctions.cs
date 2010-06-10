using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            feta += MathHelper.ToRadians(90);
            float feta_float = float.Parse(feta.ToString());
            float feta_deg = MathHelper.ToDegrees(feta_float);
            if (feta_deg >= 90 && feta_deg <= 180)
            {
                feta += MathHelper.ToRadians(180);
            }
            if (feta_deg >= 270 && feta_deg <= 360)
            {
                feta += MathHelper.ToRadians(180);
            }
            return feta;
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
    }
}
