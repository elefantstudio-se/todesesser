using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser
{
    public class GameCore
    {
        private int Width;
        private int Height;
        private ContentPool Content;
        private GraphicsDeviceManager Graphics;
        private SpriteBatch batch;

        public GameCore(int width, int height, ContentPool content, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Width = width;
            Height = height;
            Content = content;
        }

        public void Initialize()
        {
            batch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            batch.Begin();
            Content.GetTexture2D("Player").Draw(batch, 0, 0, Color.White);
            batch.End();
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void LoadContent()
        {
            Content.AddTexture2D("Player\\player", "Player");
        }
    }
}
