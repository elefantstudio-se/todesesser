using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D
{
    public class G2ParticleSettings
    {
        public string textureName = null;
        
        public int maxParticles = 100;

        public TimeSpan duration = TimeSpan.FromSeconds(1);

        public float durationRandomness = 0;

        public float emitterVelocitySensitivity = 1;

        public float minHorizontalVelocity = 0;
        public float maxHorizontalVelocity = 0;

        public float minVerticalVelocity = 0;
        public float maxVerticalVelocity = 0;

        public Vector3 gravity = Vector3.Zero;

        public float endVelocity = 1;

        public Color minColour = Color.White;
        public Color maxColour = Color.White;

        public float minRotateSpeed = 0;
        public float maxRotateSpeed = 0;

        public float minStartSize = 100;
        public float maxStartSize = 100;

        public float minEndSize = 100;
        public float maxEndSize = 100;

        public BlendState blend = BlendState.AlphaBlend;
    }
}