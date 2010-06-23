using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lighting.LightingTypes;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Lighting.ObjectTypes
{
    public class RectangleHull : BaseHull
    {
        private Vector2 position;
        private Rectangle size;

        public RectangleHull(Vector2 position, int width, int height)
        {
            this.position = position;
            this.size = new Rectangle(0, 0, width, height);
        }

        public override void Update(GameTime gameTime, AmbientLight[] lights)
        {
            base.Update(gameTime, lights);
        }

        public override void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            //Draw Rectangle
            spriteBatch.Draw(content.Load<Texture2D>("tile"), new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height), Color.White);
            base.Draw(spriteBatch, content);
        }

        public override Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public override Rectangle Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }
    }
}
