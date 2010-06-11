using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.WeaponEngine;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectWall : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private Color colour = Color.Green;
        private int health = 10000;
        private int ht_ok = 10000;
        private int ht_damaged = 5000;
        private int ht_critical = 2500;

        public ObjectWall(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(50, 50);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            this.BoundingRectangle = new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnHit(WeaponBase weapon, Vector2 from)
        {
            health -= weapon.Damage;
            if (health <= 0)
            {
                //TODO: Probably shouldn't just hide the wall if its dead ;)
                colour = Color.White;
            }
            else if (health < ht_critical)
            {
                colour = Color.Red;
            }
            else if (health < ht_damaged)
            {
                colour = Color.Orange;
            }
            else if (health <= ht_ok)
            {
                colour = Color.Green;
            }
            base.OnHit(weapon, from);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), colour);
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()) - int.Parse(offset.X.ToString()), int.Parse(this.Position.Y.ToString()) - int.Parse(offset.Y.ToString()), Texture.Width, Texture.Height), null, colour);
            base.Draw(gameTime, sb, offset);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }
    }
}
