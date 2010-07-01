using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GPU2D.Particles;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D.VParticle
{
    public class G2VFire : G2VBase
    {
        private G2ParticleBase fireParticles;
        private GraphicsDevice device;
        private Vector2 position;

        public G2VFire(Game game, ContentManager content, Vector2 position)
        {
            fireParticles = new G2ParticleFire(game, content);
            device = game.GraphicsDevice;
            this.position = position;
        }

        public override void Initialize()
        {
            fireParticles.Initialize();
            fireParticles.LoadContent();
            base.Initialize();
        }

        public override void  Update(GameTime gameTime)
        {
            fireParticles.AddParticle(Vector3.Zero, Vector3.Zero);
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
                                  Vector3.UnitZ) * Matrix.CreateTranslation(position.X - (device.Viewport.Width / 2), (device.Viewport.Height / 2) - position.Y, 0);
            Matrix Projection = Matrix.CreateOrthographic(device.Viewport.Width,
                                                          device.Viewport.Height,
                                                          1,
                                                          10000) *
                                Matrix.CreateScale(1);
            fireParticles.SetCamera(View, Projection);
            fireParticles.Draw(gameTime);
            base.Draw(gameTime, view, projection);
        }
    }
}
