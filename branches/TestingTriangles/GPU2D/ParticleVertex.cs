#region File Description
//-----------------------------------------------------------------------------
// ParticleVertex.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GPU2D
{
    /// <summary>
    /// Custom vertex structure for drawing point sprite particles.
    /// </summary>
    struct ParticleVertex
    {
        // Stores the starting position of the particle.
        public Vector3 Position;

        // Stores the starting velocity of the particle.
        public Vector3 Velocity;

        public Vector2 Offset;

        // Four random values, used to make each particle look slightly different.
        public Color Random;

        // The time (in seconds) at which this particle was created.
        public float Time;

        // Convert4.0 - essentiall the same as the old, but the VertexElementMethod and Offset
        // have been removed. Also note that I added a PointSize field to keep track of
        // which of the 4 quad vertices this one is.
        // Describe the layout of this vertex structure.
        public static readonly VertexElement[] VertexElements =
        {
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


        // Describe the size of this vertex structure.
        public const int SizeInBytes = 40;
    }
}
