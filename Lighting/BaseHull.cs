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
