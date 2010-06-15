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
using ContentPooling;
using Todesesser;
using Todesesser.ObjectPooling;

namespace TodesesserGame
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        ContentPool contentPool;
        ObjectPool objectPool;
        GraphicsDeviceManager graphics;
        GameCore gameCore;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentPool = new ContentPool(Content);
            objectPool = new ObjectPool(contentPool);
            gameCore = new GameCore(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, contentPool, objectPool, graphics, this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            gameCore.Initialize();
        }

        protected override void LoadContent()
        {
            gameCore.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            gameCore.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gameCore.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
