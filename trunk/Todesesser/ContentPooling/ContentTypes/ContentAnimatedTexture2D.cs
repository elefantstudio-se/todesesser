using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ContentPooling.ContentTypes
{
    public class ContentAnimatedTexture2D : ContentBase
    {
        private List<Texture2D> textures;
        private int currentTextureIndex = 0;

        public ContentAnimatedTexture2D(string Key)
        {
            this.Key = Key;
        }

        public void Draw(SpriteBatch sb, int x, int y, Color colour)
        {
            sb.Draw(textures[currentTextureIndex], new Rectangle(x, y, textures[currentTextureIndex].Width, textures[currentTextureIndex].Height), colour);
            currentTextureIndex += 1;
        }

        public void DrawFrame(SpriteBatch sb, int x, int y, Color colour, int frameIndex)
        {
            sb.Draw(textures[frameIndex], new Rectangle(x, y, textures[frameIndex].Width, textures[frameIndex].Height), colour);
        }

        public List<Texture2D> Textures
        {
            get { return this.textures; }
            set { this.textures = value; }
        }
    }
}
