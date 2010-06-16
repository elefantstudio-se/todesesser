using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Core;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectAchivementShelf : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private enum SlideTypes { Up, Down, Done };

        //Events:
        public delegate void OnMessageFinishedDelegate();
        public event OnMessageFinishedDelegate OnMessageFinished;

        //MessageShow Data:
        SlideTypes slideUp = SlideTypes.Done;
        string message;
        Texture2D icon;
        int timeout = 5; // in seconds
        DateTime upTime;
        int speed = 1;

        public ObjectAchivementShelf(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            this.Position = new Vector2(GameData.ScreenSize.Width - Texture.Width, GameData.ScreenSize.Height);
        }

        public void Show(string message, string contentKey, int timeout, int speed)
        {
            this.message = message;
            if (contentKey != null)
            {
                this.icon = Content.GetTexture2D(contentKey).Texture;
            }
            else
            {
                this.icon = null;
            }
            this.slideUp = SlideTypes.Up;
            this.timeout = timeout;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            #region Slide Shelf
            if (slideUp == SlideTypes.Up)
            {
                if (this.Position.Y != GameData.ScreenSize.Height - Texture.Height)
                {
                    this.Position = new Vector2(this.Position.X, this.Position.Y - speed);
                }
                else
                {
                    slideUp = SlideTypes.Done;
                    upTime = DateTime.Now;
                }
            }
            else if (slideUp == SlideTypes.Down)
            {
                if (this.Position.Y != GameData.ScreenSize.Height)
                {
                    this.Position = new Vector2(this.Position.X, this.Position.Y + speed);
                }
                else
                {
                    slideUp = SlideTypes.Done;
                    OnMessageFinished();
                }
            }
            #endregion

            TimeSpan diff = DateTime.Now - upTime;
            if (diff.Seconds >= timeout && slideUp == SlideTypes.Done)
            {
                this.slideUp = SlideTypes.Down;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (message != null)
            {
                sb.Draw(Texture, new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height), Color.White);
                if (icon != null)
                {
                    sb.Draw(icon, new Rectangle(Convert.ToInt32(this.Position.X) + 21, Convert.ToInt32(this.Position.Y) + 18, icon.Width, icon.Height), Color.White);
                }
                sb.DrawString(Content.GetSpriteFont("MainFont").SpriteFont, message, new Vector2(this.Position.X + 100, this.Position.Y + 20), Color.Black);
            }
            base.Draw(gameTime, sb);
        }

        public float X
        {
            get { return this.Position.X; }
            set { this.Position = new Vector2(value, this.Position.Y); }
        }

        public float Y
        {
            get { return this.Position.Y; }
            set { this.Position = new Vector2(this.Position.X, value); }
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }
    }
}
