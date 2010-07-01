using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPU2D.Particles;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D.VParticle
{
    public class G2VFireRing : G2VBase
    {
        private G2ParticleBase fireParticles;
        private GraphicsDevice device;
        private Vector2 position;
        private Random random = new Random();
        private int Height;
        private int Radius;

        public G2VFireRing(Game game, ContentManager content, Vector2 position, int height, int radius)
        {
            fireParticles = new G2ParticleFire(game, content);
            device = game.GraphicsDevice;
            this.position = position;
            this.Height = height;
            this.Radius = radius;
        }

        public override void Initialize()
        {
            fireParticles.Initialize();
            fireParticles.LoadContent();
            base.Initialize();
        }

        public override void  Update(GameTime gameTime)
        {
            const int fireParticlesPerFrame = 20;

            // Create a number of fire particles, randomly positioned around a circle.
            for (int i = 0; i < fireParticlesPerFrame; i++)
            {
                fireParticles.AddParticle(RandomPointOnCircle(), Vector3.Zero);
            }
            fireParticles.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            Matrix View = Matrix.CreateLookAt(new Vector3(0,
                                              -5f,
                                              0),
                                  new Vector3(0,
                                              0,
                                              0),
                                  Vector3.UnitZ) * 
                          Matrix.CreateTranslation(
                                  position.X - (device.Viewport.Width / 2), 
                                  (device.Viewport.Height / 2) - position.Y, 
                                  0);
            Matrix Projection = Matrix.CreateOrthographic(device.Viewport.Width,
                                                          device.Viewport.Height,
                                                          1,
                                                          10000) *
                                Matrix.CreateScale(1);
            fireParticles.SetCamera(View, Projection);
            fireParticles.Draw(gameTime);
            base.Draw(gameTime, view, projection);
        }

        /// <summary>
        /// Helper used by the UpdateFire method. Chooses a random location
        /// around a circle, at which a fire particle will be created.
        /// </summary>
        Vector3 RandomPointOnCircle()
        {
            double angle = random.NextDouble() * Math.PI * 2;

            float x = (float)Math.Cos(angle);
            float y = (float)Math.Sin(angle);

            return new Vector3(x * Radius, 0, y * Radius + Height);
        }
    }
}
