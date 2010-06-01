using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.ObjectPooling;
using ContentPooling;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling.ObjectTypes;
using Microsoft.Xna.Framework.Input;

namespace Todesesser.Screens
{
    public class MenuScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private ObjectButton buttonNewGame;
        private ObjectCursor cursor;

        public MenuScreen(GraphicsDevice graphicsDevice, ObjectPool Objects, ContentPool Content)
        {
            this.Objects = Objects;
            this.Content = Content;
            this.ScreenName = "Menu";
            this.GraphicsDevice = graphicsDevice;
            this.Batch = new SpriteBatch(GraphicsDevice);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            //Load Buttons
            Content.AddTexture2D("Buttons\\Menu\\NewGame", "Buttons-Menu-NewGame");
            buttonNewGame = (ObjectButton)Objects.AddObject(ObjectPool.ObjectTypes.Button, "Buttons-Menu-NewGame", "Buttons-Menu-NewGame");
            buttonNewGame.X = 50;
            buttonNewGame.Y = 20;
            //Load Cursor
            Content.AddTexture2D("Cursors\\Test", "MenuCursor");
            cursor = (ObjectCursor)Objects.AddObject(ObjectPool.ObjectTypes.Cursor, "MenuCursor", "MenuCursor");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            buttonNewGame.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Batch.Begin();

            buttonNewGame.Draw(gameTime, Batch);
            cursor.Draw(gameTime, Batch);

            Batch.End();

            base.Draw(gameTime);
        }
    }
}
