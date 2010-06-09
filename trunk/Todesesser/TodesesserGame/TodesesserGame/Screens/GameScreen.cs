using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.ObjectPooling;
using Todesesser.Map.Maps;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;
using Todesesser.WeaponEngine;
using Todesesser.WeaponEngine.Weapons;
using Todesesser.Map;
using System.Diagnostics;

namespace Todesesser.Screens
{
    public class GameScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private KeyboardState keyboardState;
        private bool prevReleased = false;
        private WeaponEngine.WeaponEngine weaponEngine;

        //Objects:
        ObjectPlayer player;
        ObjectCursor cursor;

        //Map:
        MapTest testmap;

        //FPS:
        private float deltaFPSTime = 0;
        private double currentFPS = -1;

        public GameScreen(GraphicsDevice graphicsDevice, ObjectPool Objects, ContentPool Content)
        {
            this.Objects = Objects;
            this.Content = Content;
            this.ScreenName = "Game";
            this.GraphicsDevice = graphicsDevice;
            this.Batch = new SpriteBatch(GraphicsDevice);
            testmap = new MapTest(Batch, Content, Objects);
            weaponEngine = new WeaponEngine.WeaponEngine(Content, Objects);
        }

        public override void Initialize()
        {   
            base.Initialize();
        }

        public override void LoadContent()
        {
            //Player:
            Content.AddTexture2D("Player\\debug", "Player");
            player = (ObjectPlayer)Objects.AddObject(ObjectPool.ObjectTypes.Player, "Player", "Player");
            player.Position = new Vector2((GraphicsDevice.Viewport.Width / 2) - (player.Texture.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (player.Texture.Height / 2));

            //Cursor:
            Content.AddTexture2D("Cursors\\Scope", "Cursor");
            cursor = (ObjectCursor)Objects.AddObject(ObjectPool.ObjectTypes.Cursor, "Cursor", "Cursor");
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Content.AddTexture2D("Misc\\Clear\\1x1white", "1x1white");

            //Map:
            testmap.LoadContent();
            testmap.Initialize();

            //Weapons:
            weaponEngine.LoadContent();
            weaponEngine.AddAvailableWeapon(new Glock());
            weaponEngine.AddAvailableWeapon(new USP());

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //Weapons:
            weaponEngine.Update(gameTime, int.Parse(player.Position.X.ToString()), int.Parse(player.Position.Y.ToString()), testmap, GameFunctions.GetAngle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), player.Position));

            player.Update(gameTime);
            cursor.Position = new Vector2(Mouse.GetState().X - (cursor.Texture.Width / 2), Mouse.GetState().Y - (cursor.Texture.Height / 2));

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {
                testmap.Offset.Y += player.Speed;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                testmap.Offset.Y -= player.Speed;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                testmap.Offset.X -= player.Speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                testmap.Offset.X += player.Speed;
            }
            if (prevReleased == false)
            {
                if (keyboardState.IsKeyUp(Keys.Escape))
                {
                    prevReleased = true;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    GameData.GameState = GameData.GameStates.Paused;
                    prevReleased = false;
                }
            }



            testmap.Update(gameTime, testmap);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Batch.Begin();

            //Setup RenderTarget for Player
            RenderTarget2D ptarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None);
            GraphicsDevice.SetRenderTarget(ptarget);
            
            //Draw Player:
            player.Draw(gameTime, Batch, GameFunctions.GetAngle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), player.Position));

            //Weapons:
            weaponEngine.Draw(gameTime, Batch, GameFunctions.GetAngle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), player.Position));

            Batch.End();

            //Set back to main Render Target.
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.White);

            Batch.Begin();

            testmap.Draw(gameTime);

            Rectangle dest = new Rectangle(int.Parse(player.Position.X.ToString()), int.Parse(player.Position.Y.ToString()), ptarget.Width, ptarget.Height);
            Batch.Draw(ptarget, dest, new Rectangle(0, 0, ptarget.Width, ptarget.Height), Color.White, float.Parse(GameFunctions.GetAngle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), player.Position).ToString()), player.Position, SpriteEffects.None, 1);

            cursor.Draw(gameTime, Batch);

            //FPS:
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            float fps2 = 1 / elapsed;
            deltaFPSTime += elapsed;

            if (deltaFPSTime > 1)
            {
                currentFPS = Math.Round(float.Parse(fps2.ToString()));
                deltaFPSTime -= 1;
            }

            if (weaponEngine.CurrentWeapon != null)
            {
                Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Current Weapon = " + weaponEngine.CurrentWeapon.Name, new Vector2(0, 0), Color.Black);
            }
            else
            {
                Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Current Weapon = null", new Vector2(0, 0), Color.Black);
            }
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Loaded Content = " + Content.Count, new Vector2(0, 20), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Loaded Objects = " + (Objects.Count + testmap.Objects.Count), new Vector2(0, 40), Color.Black);
            if (currentFPS >= 60)
            {
                Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, currentFPS.ToString(), new Vector2(GraphicsDevice.Viewport.Width - Content.GetSpriteFont("MainFont").SpriteFont.MeasureString(currentFPS.ToString()).X - 5, 0), Color.Green);
            }
            else if (currentFPS <= 30 && currentFPS >= 0)
            {
                Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, currentFPS.ToString(), new Vector2(GraphicsDevice.Viewport.Width - Content.GetSpriteFont("MainFont").SpriteFont.MeasureString(currentFPS.ToString()).X - 5, 0), Color.Red);
            }
            else
            {
                Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, currentFPS.ToString(), new Vector2(GraphicsDevice.Viewport.Width - Content.GetSpriteFont("MainFont").SpriteFont.MeasureString(currentFPS.ToString()).X - 5, 0), Color.Orange);
            }
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Mouse X: " + Mouse.GetState().X, new Vector2(0, 60), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Mouse Y: " + Mouse.GetState().Y, new Vector2(0, 80), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Rotation: " + player.Rotation, new Vector2(0, 100), Color.Black);
            int rot = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(player.Rotation.ToString()))) - 90;
            double xs = Math.Cos((rot * Math.PI) / 180);
            double xy = Math.Sin((rot * Math.PI) / 180);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "XS: " + (xs).ToString(), new Vector2(0, 120), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "XY: " + (xy).ToString(), new Vector2(0, 140), Color.Black);

            //Calculate Ray Vectors (length 100)
            double xe = Convert.ToInt32(player.Position.X);
            double ye = Convert.ToInt32(player.Position.Y);
            
            while (Vector2.Distance(player.Position, new Vector2(float.Parse(xe.ToString()), float.Parse(ye.ToString()))) <= 300)
            {
                xe += xs;
                ye += xy;
            }

            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "Player: " + player.Position.ToString(), new Vector2(0, 160), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "End = " + new Vector2(float.Parse(xe.ToString()), float.Parse(ye.ToString())).ToString(), new Vector2(0, 180), Color.Black);
            Batch.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, "DISTANCE = " + Vector2.Distance(player.Position, new Vector2(float.Parse(xe.ToString()), float.Parse(ye.ToString()))).ToString(), new Vector2(0, 200), Color.Black);

            GameFunctions.DrawLine(Batch, Content.GetTexture2D("1x1white").Texture, player.Position, new Vector2(float.Parse(xe.ToString()), float.Parse(ye.ToString())), Color.Red);

            Batch.End();

            ptarget.Dispose();

            base.Draw(gameTime);
        }
    }
}
