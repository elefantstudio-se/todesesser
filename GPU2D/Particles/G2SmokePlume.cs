using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D.Particles
{
    public class G2SmokePlume : G2ParticleBase
    {
        public G2SmokePlume(Game game, ContentManager content, GraphicsDevice device, Effect particleEffect) : base(game, content, device, particleEffect)
        {
            
        }

        public override void  InitializeSettings(G2ParticleSettings settings)
        {
            settings.textureName = "smoke";

            settings.maxParticles = 600;

            settings.duration = TimeSpan.FromSeconds(10);

            settings.minHorizontalVelocity = 0;
            settings.maxHorizontalVelocity = 15;

            settings.minVerticalVelocity = 10;
            settings.maxVerticalVelocity = 20;

            settings.gravity = new Vector3(-20, -5, 0);

            settings.endVelocity = 0.75f;

            settings.minRotateSpeed = -1;
            settings.maxRotateSpeed = 1;

            settings.minStartSize = 5;
            settings.maxStartSize = 10;

            settings.minEndSize = 50;
            settings.maxEndSize = 200;

            settings.blend = BlendState.AlphaBlend;
        }
    }
}
