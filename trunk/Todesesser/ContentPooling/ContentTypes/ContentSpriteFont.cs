using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ContentPooling.ContentTypes
{
    public class ContentSpriteFont : ContentBase
    {
        private SpriteFont spriteFont;

        public ContentSpriteFont(SpriteFont Font, string Key)
        {
            this.spriteFont = Font;
            this.Key = Key;
        }

        public void Draw(SpriteBatch sb, int x, int y, Color colour, string text)
        {
            sb.DrawString(spriteFont, text, new Microsoft.Xna.Framework.Vector2(x, y), colour);
        }

        public SpriteFont SpriteFont
        {
            get { return this.spriteFont; }
            set { this.spriteFont = value; }
        }
    }
}
