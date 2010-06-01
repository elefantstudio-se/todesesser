using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.ObjectPooling
{
    public class ObjectBase
    {
        private string key;
        private ObjectPool.ObjectTypes type;
        private Vector2 position;

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {

        }

        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public ObjectPool.ObjectTypes Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
    }
}
