using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPU2D.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D.VParticle
{
    public class G2VSmokePlume : G2VBase
    {
        private G2ParticleBase smokePlumeParticles;
        private GraphicsDevice device;
        private Vector2 position;

        public G2VSmokePlume(Game game, ContentManager content, Vector2 position)
        {
            smokePlumeParticles = new G2SmokePlume(game, content);
            device = game.GraphicsDevice;
            this.position = position;
        }

        public override void Initialize()
        {
            smokePlumeParticles.Initialize();
            smokePlumeParticles.LoadContent();
            base.Initialize();
        }

        public override void  Update(GameTime gameTime)
        {
            smokePlumeParticles.AddParticle(Vector3.Zero, Vector3.Zero);
            smokePlumeParticles.Update(gameTime);
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
            smokePlumeParticles.SetCamera(View, Projection);
            smokePlumeParticles.Draw(gameTime);
            base.Draw(gameTime, view, projection);
        }
    }
}
