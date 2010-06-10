using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Microsoft.Xna.Framework;

namespace Todesesser.Debug
{
    public class DebugVar
    {
        private Hashtable vars;
        private SpriteFont font;
        private ContentPool Content;

        public DebugVar(string FontContentKey, ContentPool content)
        {
            Content = content;
            vars = new Hashtable();
            font = content.GetSpriteFont(FontContentKey).SpriteFont;
        }

        public void Add(string value, string name)
        {
            vars.Add(name, value);
        }

        public void Add(int value, string name)
        {
            vars.Add(name, value);
        }

        public void Add(double value, string name)
        {
            vars.Add(name, value);
        }

        public void Update(string value, string name)
        {
            vars[name] = value;
        }

        public void Update(int value, string name)
        {
            vars[name] = value;
        }

        public void Update(double value, string name)
        {
            vars[name] = value;
        }

        public void Draw(SpriteBatch sb)
        {
            string[] keys = new string[vars.Keys.Count];
            vars.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            int nextY = 20;
            sb.Draw(Content.GetTexture2D("debugdialog").Texture, new Rectangle(0, 0, Content.GetTexture2D("debugdialog").Texture.Width, Content.GetTexture2D("debugdialog").Texture.Height), Color.White);
            foreach (string name in keys)
            {
                sb.DrawString(font, name + ": " + vars[name].ToString(), new Vector2(20, nextY), Color.Black);
                nextY += 20;
            }
        }
    }
}
