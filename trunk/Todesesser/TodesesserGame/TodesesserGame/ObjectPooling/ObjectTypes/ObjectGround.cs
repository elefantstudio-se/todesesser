using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Todesesser.Core;
using Todesesser.Map;
using ContentPooling;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectGround : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private bool originalOffsetSet = false;
        private Vector2 originalOffset;

        public ObjectGround(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            this.BoundingRectangle = new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height);
        }

        public override void Update(GameTime gameTime, ObjectPlayer player, MapBase map)
        {
            if (Math.Abs(Convert.ToInt32(Position.Y - map.Offset.Y - originalOffset.Y)) > Texture.Width)
            {
                Position = new Vector2(Position.X, Convert.ToInt32(map.Offset.Y) + originalOffset.Y);
            }
            if (Math.Abs(Convert.ToInt32(Position.X - map.Offset.X - originalOffset.X)) > Texture.Height)
            {
                Position = new Vector2(Convert.ToInt32(map.Offset.X) + originalOffset.X, Position.Y);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), Color.White);
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {
            sb.Draw(Texture, new Rectangle(Convert.ToInt32(this.Position.X) - Convert.ToInt32(offset.X), Convert.ToInt32(this.Position.Y) - Convert.ToInt32(offset.Y), Texture.Width, Texture.Height), null, Color.White, float.Parse(this.Rotation.ToString()), new Vector2(0,0), SpriteEffects.None, 1);
            base.Draw(gameTime, sb, offset);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                if (originalOffsetSet == false)
                {
                    originalOffset = value;
                    originalOffsetSet = true;
                }
                base.Position = value;
            }
        }
    }
}
