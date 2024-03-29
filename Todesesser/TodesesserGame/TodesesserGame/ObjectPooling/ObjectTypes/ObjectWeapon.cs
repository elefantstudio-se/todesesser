﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectWeapon : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;

        public ObjectWeapon(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.FixedOffset = new Vector2(25, 0);
            this.Position = new Vector2(0, 0);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
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

        public override void Draw(GameTime gameTime, SpriteBatch sb, double rotation, Vector2 originOffset, Vector2 offset)
        {
            Rectangle dest = new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height);

            sb.Draw(Texture, dest, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, float.Parse(rotation.ToString()), new Vector2((Texture.Width / 2) + originOffset.X, (Texture.Height / 2) + originOffset.Y), SpriteEffects.None, 1);
            base.Draw(gameTime, sb, rotation);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }
    }
}
