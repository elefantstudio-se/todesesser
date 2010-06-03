﻿using System;
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

namespace Todesesser.Screens
{
    public class GameScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private KeyboardState keyboardState;
        private bool prevReleased = false;

        //Objects:
        ObjectPlayer player;
        ObjectCursor cursor;

        //Map:
        MapTest testmap;

        public GameScreen(GraphicsDevice graphicsDevice, ObjectPool Objects, ContentPool Content)
        {
            this.Objects = Objects;
            this.Content = Content;
            this.ScreenName = "Game";
            this.GraphicsDevice = graphicsDevice;
            this.Batch = new SpriteBatch(GraphicsDevice);
            testmap = new MapTest(Batch, Content, Objects);
        }

        public override void Initialize()
        {   
            base.Initialize();
        }

        public override void LoadContent()
        {
            //Player:
            Content.AddTexture2D("Player\\player", "Player");
            player = (ObjectPlayer)Objects.AddObject(ObjectPool.ObjectTypes.Player, "Player", "Player");
            player.Position = new Vector2((GraphicsDevice.Viewport.Width / 2) - (player.Texture.Width / 2), (GraphicsDevice.Viewport.Height / 2) - (player.Texture.Height / 2));

            //Cursor:
            Content.AddTexture2D("Cursors\\Scope", "Cursor");
            cursor = (ObjectCursor)Objects.AddObject(ObjectPool.ObjectTypes.Cursor, "Cursor", "Cursor");
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            //Map:
            testmap.LoadContent();
            testmap.Initialize();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

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



            testmap.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Batch.Begin();

            testmap.Draw(gameTime);

            //Get Angle from Player to Mouse
            double dx = (player.Position.X + (player.Texture.Width / 2)) - Mouse.GetState().X;
            double dy = (player.Position.Y + (player.Texture.Height / 2)) - Mouse.GetState().Y;
            double rot = Math.Atan2(dy, dx);

            player.Draw(gameTime, Batch, rot);

            cursor.Draw(gameTime, Batch);

            Batch.End();

            base.Draw(gameTime);
        }
    }
}
