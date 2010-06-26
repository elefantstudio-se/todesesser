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
using Todesesser.Core;

namespace Todesesser.Screens
{
    public class MenuScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private ObjectButton buttonNewGame;
        private ObjectButton buttonExit;
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
            #region Load Buttons
            Content.AddTexture2D("Buttons\\Menu\\NewGame", "Buttons-Menu-NewGame");
            Content.AddTexture2D("Buttons\\Menu\\NewGameSelected", "Buttons-Menu-NewGame_Selected");
            buttonNewGame = (ObjectButton)Objects.AddObject(ObjectPool.ObjectTypes.Button, "Buttons-Menu-NewGame", "Buttons-Menu-NewGame");
            buttonNewGame.X = 50;
            buttonNewGame.Y = 220;
            buttonNewGame.OnClick += new ObjectButton.OnClickDelegate(button_OnClick);

            Content.AddTexture2D("Buttons\\Menu\\Exit", "Buttons-Menu-Exit");
            Content.AddTexture2D("Buttons\\Menu\\ExitSelected", "Buttons-Menu-Exit_Selected");
            buttonExit = (ObjectButton)Objects.AddObject(ObjectPool.ObjectTypes.Button, "Buttons-Menu-Exit", "Buttons-Menu-Exit");
            buttonExit.X = 50;
            buttonExit.Y = 290;
            buttonExit.OnClick += new ObjectButton.OnClickDelegate(button_OnClick);
            #endregion
            //Load Cursor
            Content.AddTexture2D("Cursors\\Basic", "MenuCursor");
            cursor = (ObjectCursor)Objects.AddObject(ObjectPool.ObjectTypes.Cursor, "MenuCursor", "MenuCursor");
            #region Load Menu Backgrounds
            Content.AddTexture2D("Menu\\todesesser_menu_1080p", "menu1080p");
            Content.AddTexture2D("Menu\\todesesser_menu_900p", "menu900p");
            Content.AddTexture2D("Menu\\todesesser_menu_720p", "menu720p");
            Content.AddTexture2D("Menu\\todesesser_menu_800x600", "menu800x600");
            #endregion

            base.LoadContent();
        }

        void button_OnClick(ObjectButton sender)
        {
            switch (sender.Key)
            {
                case "Buttons-Menu-NewGame":
                    GameData.GameState = GameData.GameStates.Playing;
                    break;
                case "Buttons-Menu-Exit":
                    GameData.GameState = GameData.GameStates.Exiting;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            GameData.MouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, cursor.Texture.Width, cursor.Texture.Height);
            buttonNewGame.Update(gameTime);
            buttonExit.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Batch.Begin();

            switch (Batch.GraphicsDevice.Viewport.Height)
            {
                case 1080:
                    if (Batch.GraphicsDevice.Viewport.Width == 1920)
                    {
                        Batch.Draw(Content.GetTexture2D("menu1080p").Texture, new Rectangle(0, 0, 1920, 1080), Color.White);
                    }
                    break;
                case 900:
                    if (Batch.GraphicsDevice.Viewport.Width == 1440)
                    {
                        Batch.Draw(Content.GetTexture2D("menu900p").Texture, new Rectangle(0, 0, 1440, 900), Color.White);
                    }
                    break;
                case 720:
                    if (Batch.GraphicsDevice.Viewport.Width == 1280)
                    {
                        Batch.Draw(Content.GetTexture2D("menu720p").Texture, new Rectangle(0, 0, 1280, 720), Color.White);
                    }
                    break;
                case 600:
                    if (Batch.GraphicsDevice.Viewport.Width == 800)
                    {
                        Batch.Draw(Content.GetTexture2D("menu800x600").Texture, new Rectangle(0, 0, 800, 600), Color.White);
                    }
                    break;
            }

            buttonNewGame.Draw(gameTime, Batch);
            buttonExit.Draw(gameTime, Batch);
            cursor.Draw(gameTime, Batch);

            Batch.End();

            base.Draw(gameTime);
        }
    }
}
