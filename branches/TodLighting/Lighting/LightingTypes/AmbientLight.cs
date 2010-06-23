using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TodLighting;
using System.Diagnostics;
using System.Collections;
using Microsoft.Xna.Framework.Input;

namespace Lighting.LightingTypes
{
    public class AmbientLight
    {
        //Variables
        private Vector2 position;
        private float range;
        private Color colour;
        private Texture2D lightTexture;

        private Vector2 hullAngles;
        private Texture2D lineTexture;

        private double rotation = 0;

        private VertexPositionColor[] vertices;
        private Int16[] indices;

        private IndexBuffer iBuffer;
        private VertexBuffer vBuffer;

        private Effect basicEffect;

        //Methods
        public AmbientLight(GraphicsDevice device, Texture2D texture, Effect basicEffect, Color colour, float range, Vector2 position, Texture2D lineTexture)
        {
            this.basicEffect = basicEffect;
            this.position = position;
            this.range = range;
            this.colour = colour;
            this.lightTexture = texture;
            this.lineTexture = lineTexture;

            vertices = new VertexPositionColor[5];
            vertices[0].Position = new Vector3(0f, 0f, 0f);
            vertices[0].Color = Color.White;
            vertices[1].Position = new Vector3(5f, 0f, 0f);
            vertices[1].Color = Color.White;
            vertices[2].Position = new Vector3(10f, 0f, 0f);
            vertices[2].Color = Color.White;
            vertices[3].Position = new Vector3(5f, 0f, -5f);
            vertices[3].Color = Color.White;
            vertices[4].Position = new Vector3(10f, 0f, -5f);
            vertices[4].Color = Color.White;
            vBuffer = new VertexBuffer(device, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vBuffer.SetData<VertexPositionColor>(vertices);

            indices = new Int16[6];
            indices[0] = 3;
            indices[1] = 1;
            indices[2] = 0;
            indices[3] = 4;
            indices[4] = 2;
            indices[5] = 1;
            iBuffer = new IndexBuffer(device,typeof(Int16), indices.Length, BufferUsage.WriteOnly);
            iBuffer.SetData<Int16>(indices, 0, indices.Length);
        }

        public void Update(GameTime gameTime, BaseHull[] hulls)
        {
            if (rotation < 360)
            {
                rotation += MathHelper.ToRadians(30);
            }
            else
            {
                rotation = MathHelper.ToRadians(0);
            }

            foreach (BaseHull hull in hulls)
            {
                //Get Angle from light to hull.
                double angle = GameFunctions.GetAngle(Position, hull.Position);
                hullAngles = hull.Position;
            }
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch, BaseHull[] hulls)
        {
            Vector2 center = new Vector2(lightTexture.Width / 2, lightTexture.Height / 2);
            float scale = range / ((float)lightTexture.Width / 2.0f);
            spriteBatch.Draw(lightTexture, position, null, colour, 0, center, scale, SpriteEffects.None, 1);

            if (hulls.Length >= 1)
            {
                foreach (BaseHull hull in hulls)
                {
                    GameFunctions.DrawLine(spriteBatch, lineTexture, Position, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), Color.Red);
                    GameFunctions.DrawLine(spriteBatch, lineTexture, Position, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), Color.Green);

                    double angle1 = GameFunctions.GetAngle(Position, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height));
                    double angle2 = GameFunctions.GetAngle(Position, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y));

                    int rot1 = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(angle1.ToString())));
                    rot1 = 0 - rot1;
                    int rot2 = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(angle2.ToString())));
                    rot2 = 0 - rot2;

                    Vector2 projection1 = GameFunctions.GetVectorProjection(new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), rot1, 100);
                    Vector2 projection2 = GameFunctions.GetVectorProjection(new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), rot2, 100);

                    GameFunctions.DrawLine(spriteBatch, lineTexture, new Vector2(hull.Position.X, hull.Position.Y + hull.Size.Height), projection1, Color.Red);
                    GameFunctions.DrawLine(spriteBatch, lineTexture, new Vector2(hull.Position.X + hull.Size.Width, hull.Position.Y), projection2, Color.Green); 
                }
            }

            foreach (EffectTechnique technique in basicEffect.Techniques)
            {
                foreach (EffectPass pass in technique.Passes)
                {
                    pass.Apply();
                    device.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
                    Debug.WriteLine("Draw Primitives:");
                }
            }
        }

        //Properties
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public float Range
        {
            get { return this.range; }
            set { this.range = value; }
        }
        public Color Colour
        {
            get { return this.colour; }
            set { this.colour = value; }
        }
        public Texture2D LightTexture
        {
            get { return this.lightTexture; }
            set { this.lightTexture = value; }
        }
    }
}
