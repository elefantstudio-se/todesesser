using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todesesser.ObjectPooling;
using Todesesser.ObjectPooling.ObjectTypes;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Todesesser.Screens
{
    public class PauseScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private ObjectButton buttonExit;
        private ObjectCursor cursor;
        private bool prevReleased = false;

        public PauseScreen(GraphicsDevice graphicsDevice, ObjectPool Objects, ContentPool Content)
        {
            this.Objects = Objects;
            this.Content = Content;
            this.ScreenName = "Pause";
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
            Content.AddTexture2D("Buttons\\Menu\\Exit", "Buttons-Pause-Exit");
            Content.AddTexture2D("Buttons\\Menu\\ExitSelected", "Buttons-Pause-Exit_Selected");
            buttonExit = (ObjectButton)Objects.AddObject(ObjectPool.ObjectTypes.Button, "Buttons-Pause-Exit", "Buttons-Pause-Exit");
            buttonExit.X = 50;
            buttonExit.Y = 90;
            buttonExit.OnClick += new ObjectButton.OnClickDelegate(button_OnClick);
            //Load Cursor
            Content.AddTexture2D("Cursors\\Basic", "PauseCursor");
            cursor = (ObjectCursor)Objects.AddObject(ObjectPool.ObjectTypes.Cursor, "PauseCursor", "PauseCursor");

            base.LoadContent();
        }

        void button_OnClick(ObjectButton sender)
        {
            switch (sender.Key)
            {
                case "Buttons-Pause-Exit":
                    GameData.GameState = GameData.GameStates.Exiting;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            cursor.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            GameData.MouseRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, cursor.Texture.Width, cursor.Texture.Height);
            buttonExit.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
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
                    GameData.GameState = GameData.GameStates.Playing;
                    prevReleased = false;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Batch.Begin();

            buttonExit.Draw(gameTime, Batch);
            cursor.Draw(gameTime, Batch);

            Batch.End();

            base.Draw(gameTime);
        }
    }
}
