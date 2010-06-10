using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectDebugPoint : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;

        public ObjectDebugPoint(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            this.Position = new Vector2(0 - (Texture.Width / 2), 0 - (Texture.Height / 2));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), Color.White);
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()) - int.Parse(offset.X.ToString()), int.Parse(this.Position.Y.ToString()) - int.Parse(offset.Y.ToString()), Texture.Width, Texture.Height), null, Color.White);
            base.Draw(gameTime, sb, offset);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }
    }
}
