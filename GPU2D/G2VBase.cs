using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GPU2D
{
    public class G2VBase
    {
        public enum ParticleTypes { SmokePlume };

        private ParticleTypes particleType;

        public ParticleTypes ParticleType
        {
            get { return this.particleType; }
            set { this.particleType = value; }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

        }
    }
}
