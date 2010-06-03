using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectPlayer : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private int speed = 2;

        public ObjectPlayer(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(0, 0);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                Speed = 3;
            }
            else
            {
                Speed = 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), Color.White);
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            Rectangle dest = new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height);

            sb.Draw(Texture, dest, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, float.Parse(rotation.ToString()), new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
            base.Draw(gameTime, sb);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }

        public int Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
    }
}
