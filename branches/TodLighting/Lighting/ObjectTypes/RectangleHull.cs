//Todesesser, XNA 4.0 C# Game.
//Copyright (C) 2010  Dean Gardiner and Taylor Lodge.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Email: gardiner91@gmail.com.
//
//Full Licence can be found at http://www.gnu.org/licenses/gpl-3.0.txt.

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
