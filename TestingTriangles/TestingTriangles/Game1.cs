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
        Texture2D scaletest;

        VertexPositionColor[] vertices;
        short[] indices;

        Matrix View;
        Matrix Projection;
        int cameraHeight = 5;
        int planeHeight = -5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        void SetUpCamera()
        {
            //TODO: Fix Camera for true Projection Scaling.
            View = Matrix.CreateLookAt(new Vector3(graphics.PreferredBackBufferWidth / 2, cameraHeight, graphics.PreferredBackBufferHeight / 2), new Vector3(graphics.PreferredBackBufferWidth / 2, planeHeight, graphics.PreferredBackBufferHeight / 2), Vector3.UnitZ);
            Projection = Matrix.CreateOrthographic(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1, 100) * Matrix.CreateScale(1) * Matrix.CreateRotationZ(MathHelper.Pi);
        }

        void InitializeVertices()
        {
            vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(325, 0.5f, 300), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(350, 0.5f, 300), Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(325, 0.5f, 325), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(350, 0.5f, 325), Color.Red);
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
            scaletest = Content.Load<Texture2D>("scaletest");
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

            spriteBatch.Begin();
            spriteBatch.Draw(scaletest, new Rectangle(300,300,25,25), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
