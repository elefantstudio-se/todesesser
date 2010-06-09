using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.WeaponEngine;
using Todesesser.Map;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectBullet : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private int fromX;
        private int fromY;
        private WeaponBase weapon;

        public ObjectBullet(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(0, 0);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
        }

        public override void Update(GameTime gameTime)
        {
            int rot = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(Rotation.ToString()))) - 90;
            double xs = 10 * Math.Cos((rot * Math.PI) / 180);
            double xy = 10 * Math.Sin((rot * Math.PI) / 180);
            this.Position = new Vector2(this.Position.X + float.Parse(xs.ToString()), this.Position.Y + float.Parse(xy.ToString()));
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

        public override void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            Rectangle dest = new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height);

            sb.Draw(Texture, dest, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, float.Parse(rotation.ToString()), new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
            base.Draw(gameTime, sb, rotation);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset, double rotation)
        {
            Rectangle dest = new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height);

            sb.Draw(Texture, new Rectangle(Convert.ToInt32(this.Position.X) - Convert.ToInt32(offset.X), Convert.ToInt32(this.Position.Y) - Convert.ToInt32(offset.Y), Texture.Width, Texture.Height), null, Color.White, float.Parse(rotation.ToString()), new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
            base.Draw(gameTime, sb, offset, rotation);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }

        public int FromX
        {
            get { return this.fromX; }
            set { this.fromX = value; }
        }

        public int FromY
        {
            get { return this.fromY; }
            set { this.fromY = value; }
        }

        public WeaponBase Weapon
        {
            get { return this.weapon; }
            set { this.weapon = value; }
        }
    }
}
