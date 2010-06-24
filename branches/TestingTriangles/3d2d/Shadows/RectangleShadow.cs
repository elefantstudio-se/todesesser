using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace _3d2d.Shadows
{
    public class RectangleShadow
    {
        VertexPositionColor[] vertices;
        short[] indices;

        public RectangleShadow(Vector2 TopLeft, Vector2 TopRight, Vector2 BottomLeft, Vector2 BottomRight, Color colour)
        {
            Debug.WriteLine("RectangleShadow->__CONSTRUCT__");
            //Vertices
            vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), colour);
            vertices[1] = new VertexPositionColor(new Vector3(0, 0, 25), colour);
            vertices[2] = new VertexPositionColor(new Vector3(25, 0, 0), colour);
            vertices[3] = new VertexPositionColor(new Vector3(25, 0, 25), colour);
            //Indices
            indices = new short[6];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;
        }

        public void Draw(GraphicsDevice device)
        {
            Debug.WriteLine("RectangleShadow->Draw");
            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
        }

        public Vector2 TopLeft
        {
            get { return new Vector2(vertices[0].Position.X, vertices[0].Position.Y); }
            set { vertices[0].Position = new Vector3(value.X, 0, value.Y); }
        }

        public Vector2 TopRight
        {
            get { return new Vector2(vertices[1].Position.X, vertices[1].Position.Y); }
            set { vertices[1].Position = new Vector3(value.X, 0, value.Y); }
        }

        public Vector2 BottomLeft
        {
            get { return new Vector2(vertices[2].Position.X, vertices[2].Position.Y); }
            set { vertices[2].Position = new Vector3(value.X, 0, value.Y); }
        }

        public Vector2 BottomRight
        {
            get { return new Vector2(vertices[3].Position.X, vertices[3].Position.Y); }
            set { vertices[3].Position = new Vector3(value.X, 0, value.Y); }
        }
    }
}
