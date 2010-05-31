using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;

namespace Todesesser
{
    public class GameCore
    {
        private int Width;
        private int Height;
        private ContentPool Content;
        private ObjectPool Objects;
        private GraphicsDeviceManager Graphics;
        private SpriteBatch batch;


        private ObjectPlayer player;

        public GameCore(int width, int height, ContentPool content, ObjectPool objects, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Width = width;
            Height = height;
            Content = content;
            Objects = objects;
        }

        public void Initialize()
        {
            batch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        public void LoadContent()
        {
            Content.AddTexture2D("Player\\player", "Player");
            Content.AddSpriteFont("Fonts\\Main", "MainFont");
            player = (ObjectPlayer)Objects.AddObject(ObjectPool.ObjectTypes.Player, "Player", "Player");
            //GameData.GameState = GameData.GameStates.Menu;
            GameData.GameState = GameData.GameStates.Playing;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.Position.Y += 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.Position.Y -= 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                player.Position.X += 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                player.Position.X -= 1;
            }
        }

        public void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.White);
            batch.Begin();

            Content.GetSpriteFont("MainFont").Draw(batch, 5, 0, Color.Black, "GameState = " + GameData.GameState);
            
            player.Draw(gameTime, batch);

            batch.End();
        }
    }
}
