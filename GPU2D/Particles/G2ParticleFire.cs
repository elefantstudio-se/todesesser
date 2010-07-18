#region File Description
//-----------------------------------------------------------------------------
// FireParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GPU2D.Particles
{
    /// <summary>
    /// Custom particle system for creating a flame effect.
    /// </summary>
    public class G2ParticleFire : G2ParticleBase
    {
        public G2ParticleFire(Game game, ContentManager content) : base(game, content)
        {
            this.type = ParticleTypes.Fire;
        }

        protected override void InitializeSettings(ParticleSettings settings)
        {
            settings.TextureName = "fire";

            settings.MaxParticles = 2400;

            settings.Duration = TimeSpan.FromSeconds(1.8);

            settings.DurationRandomness = 1;

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 15;

            settings.MinVerticalVelocity = -10;
            settings.MaxVerticalVelocity = 10;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, 15, 0);

            settings.MinColor = new Color(255, 255, 255, 10);
            settings.MaxColor = new Color(255, 255, 255, 40);
            settings.MinColor = new Color(255, 255, 255, 255);
            settings.MaxColor = new Color(255, 255, 255, 255);

            settings.MinStartSize = 10;
            settings.MaxStartSize = 20;

            settings.MinEndSize = 10;
            settings.MaxEndSize = 60;

            // Use additive blending.
            // Convert4.0 - Blend state set as an object rather than individual properties.
            settings.Blend = BlendState.Additive;
        }
    }
}
