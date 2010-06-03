using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.WeaponEngine;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectBullet : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private int fromX;
        private int fromY;
        private float rotation;
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
            //TODO: Fix this to move in direction of Rotation.
            this.Position = new Vector2(this.Position.X, this.Position.Y - weapon.BulletSpeed);
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

        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public WeaponBase Weapon
        {
            get { return this.weapon; }
            set { this.weapon = value; }
        }
    }
}
