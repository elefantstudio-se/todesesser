using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Lighting.LightingTypes;
using Microsoft.Xna.Framework.Content;

namespace Lighting
{
    public class BaseHull
    {
        public virtual void Draw(SpriteBatch spriteBatch, ContentManager content)
        {

        }

        public virtual void Update(GameTime gameTime, AmbientLight[] lights)
        {

        }

        public virtual Vector2 Position
        {
            get { return Vector2.Zero; }
            set { }
        }

        public virtual Rectangle Size
        {
            get { return new Rectangle(); }
            set { }
        }
    }
}
