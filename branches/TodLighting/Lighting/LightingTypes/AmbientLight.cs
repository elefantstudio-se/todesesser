//Todesesser, XNA 4.0 C# Game.
//Copyright (C) 2010  Dean Gardiner and Taylor Lodge.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Email: gardiner91@gmail.com.
//
//Full Licence can be found at http://www.gnu.org/licenses/gpl-3.0.txt.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TodLighting;
using System.Diagnostics;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using GPU2D;
using GPU2D.Primitives;

namespace Lighting.LightingTypes
{
    public class AmbientLight
    {
        //Variables
        private Vector2 position;
        private float range;
        private Color colour;
        private Texture2D lightTexture;

        private Vector2 hullAngles;
        private Texture2D lineTexture;

        private double rotation = 0;

        GPU2DEngine gpu2dEngine;

        //Methods
        public AmbientLight(GraphicsDeviceManager graphics, Texture2D texture, Effect basicEffect, Color colour, float range, Vector2 position, Texture2D lineTexture, GPU2DEngine gpu2dEngine)
        {
            this.position = position;
            this.range = range;
            this.colour = colour;
            this.lightTexture = texture;
            this.lineTexture = lineTexture;
            this.gpu2dEngine = gpu2dEngine;
        }

        public void Update(GameTime gameTime, BaseHull[] hulls)
        {
            if (rotation < 360)
            {
                rotation += MathHelper.ToRadians(30);
            }
            else
            {
                rotation = MathHelper.ToRadians(0);
            }

            foreach (BaseHull hull in hulls)
            {
                //Get Angle from light to hull.
                double angle = GameFunctions.GetAngle(Position, hull.Position);
                hullAngles = hull.Position;
            }
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, BaseHull[] hulls)
        {
            Vector2 center = new Vector2(lightTexture.Width / 2, lightTexture.Height / 2);
            float scale = range / ((float)lightTexture.Width / 2.0f);
            spriteBatch.Draw(lightTexture, position, null, colour, 0, center, scale, SpriteEffects.None, 1);

            if (hulls.Length >= 1)
            {
                foreach (BaseHull hull in hulls)
                {
                    GameFunctions.DrawLine(spriteBatch, lineTexture, Position, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), Color.Red);
                    GameFunctions.DrawLine(spriteBatch, lineTexture, Position, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), Color.Green);

                    double angle1 = GameFunctions.GetAngle(Position, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height));
                    double angle2 = GameFunctions.GetAngle(Position, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y));

                    int rot1 = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(angle1.ToString())));
                    rot1 = 0 - rot1;
                    int rot2 = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(angle2.ToString())));
                    rot2 = 0 - rot2;

                    Vector2 projection1 = GameFunctions.GetVectorProjection(new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), rot1, 800);
                    Vector2 projection2 = GameFunctions.GetVectorProjection(new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), rot2, 800);

                    GameFunctions.DrawLine(spriteBatch, lineTexture, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), projection1, Color.Red);
                    GameFunctions.DrawLine(spriteBatch, lineTexture, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), projection2, Color.Green);

                    gpu2dEngine.DrawPrimitive(new G2Rectangle(projection1, projection2, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), Color.Black, 0.5f));
                }
            }
        }

        //Properties
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public float Range
        {
            get { return this.range; }
            set { this.range = value; }
        }
        public Color Colour
        {
            get { return this.colour; }
            set { this.colour = value; }
        }
        public Texture2D LightTexture
        {
            get { return this.lightTexture; }
            set { this.lightTexture = value; }
        }
    }
}
