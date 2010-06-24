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

        VertexPositionColor[] vertices;
        short[] indices;

        Matrix View;
        Matrix Projection;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        void SetUpCamera()
        {
            //TODO: Fix Camera for true Projection Scaling.
            View = Matrix.CreateLookAt(new Vector3(0, 5, 0), new Vector3(0, -5, 0), Vector3.UnitZ);
            Projection = Matrix.CreatePerspectiveFieldOfView(1, 16 / 10, 1, 100);
        }

        void InitializeVertices()
        {
            vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(0, 0.5f, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1, 0.5f, 0), Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(0, 0.5f, 1), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(1, 0.5f, 1), Color.Blue);
        }

        void SetUpIndices()
        {
            indices = new short[6];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 3;
            indices[4] = 2;
            indices[5] = 1;
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
            GraphicsDevice.Clear(Color.Black);

            shader.Parameters["ViewProjection"].SetValue(View * Projection);

            shader.CurrentTechnique.Passes[0].Apply();

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);

            base.Draw(gameTime);
        }
    }
}
