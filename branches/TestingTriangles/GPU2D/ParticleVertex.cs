using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D
{
    struct ParticleVertex
    {
        public Vector3 Position;

        public Vector3 Velocity;

        public Vector2 Offset;

        public Color Random;

        public float Time;

        public static readonly VertexElement[] VertexElements = {
                                                                    new VertexElement(0, VertexElementFormat.Vector3,
                                                                        VertexElementUsage.Position, 0),

                                                                    new VertexElement(12, VertexElementFormat.Vector3,
                                                                        VertexElementUsage.Normal, 0),

                                                                    new VertexElement(24, VertexElementFormat.Vector2,
                                                                        VertexElementUsage.PointSize, 0),

                                                                    new VertexElement(32, VertexElementFormat.Color,
                                                                        VertexElementUsage.Color, 0),
                                                                    new VertexElement(36, VertexElementFormat.Single,
                                                                        VertexElementUsage.TextureCoordinate, 0),
                                                                };

        public const int SizeInBytes = 40;
    }
}
