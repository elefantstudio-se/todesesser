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
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D.Primitives
{
    public class G2Rectangle : G2PrimitiveBase
    {
        private Vector2 topLeft;
        private Vector2 topRight;
        private Vector2 bottomLeft;
        private Vector2 bottomRight;

        private VertexPositionColor[] vertices;
        private short[] indices;

        private Color colour;

        public G2Rectangle(Vector2 TopLeft, Vector2 TopRight, Vector2 BottomLeft, Vector2 BottomRight, Color Colour, float height)
        {
            this.topLeft = TopLeft;
            this.topRight = TopRight;
            this.bottomLeft = BottomLeft;
            this.bottomRight = BottomRight;

            this.colour = Colour;

            this.vertices = new VertexPositionColor[4];
            this.vertices[0] = new VertexPositionColor(new Vector3(TopLeft.X, height, TopLeft.Y), Colour);
            this.vertices[1] = new VertexPositionColor(new Vector3(TopRight.X, height, TopRight.Y), Colour);
            this.vertices[2] = new VertexPositionColor(new Vector3(BottomLeft.X, height, BottomLeft.Y), Colour);
            this.vertices[3] = new VertexPositionColor(new Vector3(BottomRight.X, height, BottomRight.Y), Colour);
            
            this.indices = new short[6];
            this.indices[0] = 0;
            this.indices[1] = 1;
            this.indices[2] = 2;
            this.indices[3] = 3;
            this.indices[4] = 2;
            this.indices[5] = 1;
        }

        public G2Rectangle(Rectangle rectangle, Color Colour, float height)
        {
            this.topLeft = new Vector2(rectangle.X, rectangle.Y);
            this.topRight = new Vector2(rectangle.X + rectangle.Width, rectangle.Y);
            this.bottomLeft = new Vector2(rectangle.X, rectangle.Y + rectangle.Height);
            this.bottomRight = new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);

            this.colour = Colour;

            this.vertices = new VertexPositionColor[4];
            this.vertices[0] = new VertexPositionColor(new Vector3(TopLeft.X, height, TopLeft.Y), Colour);
            this.vertices[1] = new VertexPositionColor(new Vector3(TopRight.X, height, TopRight.Y), Colour);
            this.vertices[2] = new VertexPositionColor(new Vector3(BottomLeft.X, height, BottomLeft.Y), Colour);
            this.vertices[3] = new VertexPositionColor(new Vector3(BottomRight.X, height, BottomRight.Y), Colour);

            this.indices = new short[6];
            this.indices[0] = 0;
            this.indices[1] = 1;
            this.indices[2] = 2;
            this.indices[3] = 3;
            this.indices[4] = 2;
            this.indices[5] = 1;
        }

        public override void Draw(GraphicsDeviceManager graphics)
        {
            graphics.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
        }

        #region Properties

        public Vector2 TopLeft
        {
            get { return this.topLeft; }
            set
            {
                this.vertices[0] = new VertexPositionColor(new Vector3(value.X, this.vertices[0].Position.Y, value.Y), colour);
                this.topLeft = value;
            }
        }

        public Vector2 TopRight
        {
            get { return this.topRight; }
            set
            {
                this.vertices[1] = new VertexPositionColor(new Vector3(value.X, this.vertices[1].Position.Y, value.Y), colour);
                this.topRight = value;
            }
        }

        public Vector2 BottomLeft
        {
            get { return this.bottomLeft; }
            set
            {
                this.vertices[2] = new VertexPositionColor(new Vector3(value.X, this.vertices[2].Position.Y, value.Y), colour);
                this.bottomLeft = value;
            }
        }

        public Vector2 BottomRight
        {
            get { return this.bottomRight; }
            set
            {
                this.vertices[3] = new VertexPositionColor(new Vector3(value.X, this.vertices[3].Position.Y, value.Y), colour);
                this.bottomRight = value;
            }
        }

        public Color Colour
        {
            get { return this.colour; }
            set { this.colour = value; }
        }

        #endregion
    }
}
