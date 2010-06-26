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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Lighting;
using Lighting.LightingTypes;
using System.Diagnostics;
using Lighting.ObjectTypes;
using GPU2D;
using GPU2D.Primitives;

namespace TodLighting
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Lighting:
        AmbientLight[] ambientLights;
        BaseHull[] objectHulls;
        Texture2D alphaClearTexture;
        GPU2DEngine gpu2dEngine;

        Texture2D clearWhite;
        Effect basicEffect;

        int t = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gpu2dEngine = new GPU2DEngine(Content.Load<Effect>("Basic"), graphics);
            clearWhite = Content.Load<Texture2D>("1x1white");
            basicEffect = Content.Load<Effect>("Basic");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Lighting:
            ambientLights = new AmbientLight[1];
            //ambientLights[0] = new AmbientLight(graphics, Content.Load<Texture2D>("light"), basicEffect, Color.Red, 100, new Vector2(100, 600), clearWhite, gpu2dEngine);
            //ambientLights[1] = new AmbientLight(graphics, Content.Load<Texture2D>("light"), basicEffect, Color.Red, 100, new Vector2(300, 600), clearWhite, gpu2dEngine);
            ambientLights[0] = new AmbientLight(graphics, Content.Load<Texture2D>("light"), basicEffect, Color.Red, 100, new Vector2(500, 600), clearWhite, gpu2dEngine);
            objectHulls = new BaseHull[3];
            objectHulls[0] = new RectangleHull(new Vector2(0, 300), 50, 50);
            objectHulls[1] = new RectangleHull(new Vector2(200, 350), 50, 50);
            objectHulls[2] = new RectangleHull(new Vector2(0, 450), 50, 50);

            alphaClearTexture = Content.Load<Texture2D>("AlphaOne");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) == false)
            {
                if (objectHulls[0].Position.X <= 800)
                {
                    int x = 200 + Convert.ToInt32(60 * Math.Sin(t / 5));
                    int y = 200 + Convert.ToInt32(15 * Math.Cos(t / 5));
                    objectHulls[0].Position = new Vector2(x, y);
                    t += 1;
                }
                else
                {
                    objectHulls[0].Position = new Vector2(0, 0);
                }
                if (objectHulls[2].Position.X <= 800)
                {
                    objectHulls[2].Position = new Vector2(objectHulls[2].Position.X + 1, objectHulls[2].Position.Y);
                }
                else
                {
                    objectHulls[2].Position = new Vector2(0, objectHulls[2].Position.Y);
                }
            }
            foreach (AmbientLight light in ambientLights)
            {
                light.Update(gameTime, objectHulls);
            }
            foreach (BaseHull hull in objectHulls)
            {
                hull.Update(gameTime, ambientLights);
            }

            gpu2dEngine.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            gpu2dEngine.Draw(graphics);

            spriteBatch.Begin();

            foreach (BaseHull hull in objectHulls)
            {
                hull.Draw(spriteBatch, Content);
            }

            foreach (AmbientLight light in ambientLights)
            {
                light.Draw(graphics, spriteBatch, objectHulls);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
