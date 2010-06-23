using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace TestingTriangles
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        Effect shader;

        Texture2D spriteScaleTest;

        VertexPositionColor[] vertices;

        Matrix View;
        Matrix Projection;

        short[] indices;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        void SetUpCamera()
        {
            View = new Matrix(1.0f, 0, 0, 0, 0, 0.019996f, 0.9998f, 0, 0, -0.9998f, 0.019996f, 0, 0, 0.00000000186234717f, -5.001f, 1.0f);
            Projection = new Matrix(1.83048773f, 0, 0, 0, 0, 1.83048773f, 0, 0, 0, 0, -1.010101f, -1.0f, 0, 0, -1.010101f, 0);
        }
        void InitializeVertices()
        {
            vertices = new VertexPositionColor[4];

            vertices[0] = new VertexPositionColor(new Vector3(0, 0.5f, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(25, 0.5f, 0), Color.Red);

            vertices[2] = new VertexPositionColor(new Vector3(0, 0.5f, 25), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(25, 0.5f, 25), Color.Red);
        }
        void SetUpIndices()
        {
            indices = new short[6];

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            InitializeVertices();
            SetUpIndices();
            SetUpCamera();
            shader = Content.Load<Effect>("Basic");
            spriteScaleTest = Content.Load<Texture2D>("scaletest");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            SetUpCamera();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(spriteScaleTest, new Rectangle(300, 300, 25, 25), Color.White);
            spriteBatch.End();

            shader.Parameters["ViewProjection"].SetValue(View * Projection);

            shader.CurrentTechnique.Passes[0].Apply();

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);

            base.Draw(gameTime);
        }
    }
}
