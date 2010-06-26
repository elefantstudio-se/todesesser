using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todesesser.ObjectPooling;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Todesesser.Core;

namespace Todesesser.Screens
{
    public class SplashScreen : BaseScreen
    {
        private ObjectPool Objects;
        private ContentPool Content;
        private DateTime CurrentTime;
        private DateTime EndTime;

        public SplashScreen(GraphicsDevice graphicsDevice, ObjectPool Objects, ContentPool Content)
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
            Content.AddSpriteFont("Fonts\\Splash", "SplashFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentTime = DateTime.Now;
            if (EndTime.Year == 0001)
            {
                EndTime = DateTime.Now;
                EndTime = EndTime.AddSeconds(1);
            }
            else
            {
                if (CurrentTime.CompareTo(EndTime) == 1)
                {
                    GameData.GameState = GameData.GameStates.Menu;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Batch.Begin();

            SpriteFont font = Content.GetSpriteFont("SplashFont").SpriteFont;
            string splashText = "Todesesser";

            Batch.DrawString(font, splashText, new Vector2(GraphicsDevice.Viewport.Width / 2 - (font.MeasureString(splashText).X / 2), GraphicsDevice.Viewport.Height / 2 - (font.MeasureString(splashText).Y / 2)), Color.White);

            Batch.End();

            base.Draw(gameTime);
        }
    }
}
