using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GPU2D.Particles;

namespace GPU2D.VParticle
{
    public class G2VExplosion : G2VBase
    {
        private G2ParticleBase explosionParticles;
        private G2ParticleBase explosionSmokeParticles;
        private G2ParticleBase projectileTrailParticles;
        private GraphicsDevice device;
        private Vector2 position;
        private List<Projectile> projectiles = new List<Projectile>();
        private TimeSpan timeToNextProjectile = TimeSpan.Zero;

        public G2VExplosion(Game game, ContentManager content, Vector2 position)
        {
            explosionParticles = new G2ParticleExplosion(game, content);
            explosionSmokeParticles = new G2ParticleExplosionSmoke(game, content);
            projectileTrailParticles = new G2ProjectileTrail(game, content);
            device = game.GraphicsDevice;
            this.position = position;
        }

        public override void Initialize()
        {
            explosionParticles.Initialize();
            explosionParticles.LoadContent();
            explosionSmokeParticles.Initialize();
            explosionSmokeParticles.LoadContent();
            projectileTrailParticles.Initialize();
            projectileTrailParticles.LoadContent();
            base.Initialize();
        }

        public override void  Update(GameTime gameTime)
        {
            timeToNextProjectile -= gameTime.ElapsedGameTime;

            if (timeToNextProjectile <= TimeSpan.Zero)
            {
                // Create a new projectile once per second. The real work of moving
                // and creating particles is handled inside the Projectile class.
                projectiles.Add(new Projectile(explosionParticles,
                                               explosionSmokeParticles,
                                               projectileTrailParticles));
                timeToNextProjectile += TimeSpan.FromSeconds(1);
            }

            int i = 0;

            while (i < projectiles.Count)
            {
                if (!projectiles[i].Update(gameTime))
                {
                    // Remove projectiles at the end of their life.
                    projectiles.RemoveAt(i);
                }
                else
                {
                    // Advance to the next projectile.
                    i++;
                }
            }

            explosionParticles.Update(gameTime);
            explosionSmokeParticles.Update(gameTime);
            projectileTrailParticles.Update(gameTime);
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
            explosionParticles.SetCamera(View, Projection);
            explosionParticles.Draw(gameTime);
            explosionSmokeParticles.SetCamera(View, Projection);
            explosionSmokeParticles.Draw(gameTime);
            projectileTrailParticles.SetCamera(View, Projection);
            projectileTrailParticles.Draw(gameTime);
            base.Draw(gameTime, view, projection);
        }
    }
}
