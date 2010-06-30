using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.WeaponEngine;
using Todesesser.Map;
using Todesesser.ObjectPooling.ObjectTypes;

namespace Todesesser.ObjectPooling
{
    public class ObjectBase
    {
        private string key;
        private ObjectPool.ObjectTypes type;
        private Vector2 position;
        private Vector2 fixedOffset;
        private double rotation;
        private Rectangle boundingRectangle;

        public virtual void Update(GameTime gameTime, ObjectPlayer player)
        {

        }

        public virtual void Update(GameTime gameTime, ObjectPlayer player, MapBase map)
        {

        }

        public virtual void Update(GameTime gameTime, MapBase map)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Update(GameTime gameTime, Map.MapBase map, ObjectPool objectPool)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, double rotation, Vector2 originOffset, Vector2 offset)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset, double rotation)
        {

        }

        public virtual void OnHit(WeaponBase weapon, Vector2 from)
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

        public virtual Vector2 Position
        {
            get { return this.position; }
            set { this.position = new Vector2(value.X + fixedOffset.X, value.Y + fixedOffset.Y); }
        }

        public Vector2 FixedOffset
        {
            get { return this.fixedOffset; }
            set { this.fixedOffset = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public Rectangle BoundingRectangle
        {
            get { return this.boundingRectangle; }
            set { this.boundingRectangle = value; }
        }
    }
}
