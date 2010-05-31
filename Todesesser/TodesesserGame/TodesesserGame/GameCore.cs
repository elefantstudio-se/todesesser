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
            player = (ObjectPlayer)Objects.AddObject(ObjectPool.ObjectTypes.Player, "Player", "Player");
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
            Graphics.GraphicsDevice.Clear(Color.Black);
            batch.Begin();

            player.Draw(gameTime, batch);

            batch.End();
        }
    }
}
