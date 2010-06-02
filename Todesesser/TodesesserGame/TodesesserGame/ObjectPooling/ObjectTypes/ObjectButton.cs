using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectButton : ObjectBase
    {
        private string contentKey;
        private string contentKeySel;
        private ContentPool Content;
        public delegate void OnClickDelegate(ObjectButton sender);
        public event OnClickDelegate OnClick;
        private bool selected;

        public ObjectButton(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(0, 0);
            this.contentKey = ContentKey;
            this.contentKeySel = ContentKey + "_Selected";
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.X + (GameData.MouseRectangle.Width / 2) >= Position.X && mouse.Y + (GameData.MouseRectangle.Height / 2) >= Position.Y &&
                mouse.X + (GameData.MouseRectangle.Width / 2) <= (Position.X + Texture.Width) && mouse.Y + (GameData.MouseRectangle.Height / 2) <= (Position.Y + Texture.Height))
            {
                selected = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick(this);
                }
            }
            else
            {
                selected = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (selected == false)
            {
                sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), Color.White);
            }
            else
            {
                sb.Draw(TextureSelected, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), TextureSelected.Width, TextureSelected.Height), Color.White);
            }
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {
            if (selected == false)
            {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()) - int.Parse(offset.X.ToString()), int.Parse(this.Position.Y.ToString()) - int.Parse(offset.Y.ToString()), Texture.Width, Texture.Height), null, Color.White);
                        }
            else
            {
                sb.Draw(TextureSelected, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), TextureSelected.Width, TextureSelected.Height), Color.White);
            }
            base.Draw(gameTime, sb, offset);
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

        public Texture2D TextureSelected
        {
            get { return this.Content.GetTexture2D(contentKeySel).Texture; }
        }
    }
}
