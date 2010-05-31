using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ContentPooling.ContentTypes
{
    public class ContentTexture2D : ContentBase
    {
        private Texture2D texture;

        public ContentTexture2D(Texture2D Texture, string Key)
        {
            this.texture = Texture;
            this.Key = Key;
        }

        public void Draw(SpriteBatch sb, int x, int y, Color colour)
        {
            sb.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), colour);
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
    }
}
