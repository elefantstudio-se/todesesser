using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D
{
    public class G2ParticleBase : DrawableGameComponent
    {
        private G2ParticleSettings settings = new G2ParticleSettings();

        private ContentManager content;

        private Effect particleEffect;

        private EffectParameter effectViewParameter;
        private EffectParameter effectProjectionParameter;
        private EffectParameter effectViewportHeightParameter;
        private EffectParameter effectAspectParameter;
        private EffectParameter effectTimeParameter;

        private ParticleVertex[] particles;
        private ParticleVertex[] partVerts;

        DynamicVertexBuffer vertexBuffer;

        IndexBuffer indexBuffer;

        VertexDeclaration vertexDeclaration;

        int firstActiveParticle;
        int firstNewParticle;
        int firstFreeParticle;
        int firstRetiredParticle;

        float currentTime;

        int drawCounter;

        static Random random = new Random();

        #region Initialize

        protected G2ParticleBase(Game game, ContentManager content)
            : base(game)
        {
            this.content = content;
        }

        public override void Initialize()
        {
            InitializeSettings(settings);

            particles = new ParticleVertex[settings.maxParticles];
            partVerts = new ParticleVertex[particles.Length * 4];
            
            base.Initialize();
        }

        protected abstract void InitializeSettings(G2ParticleSettings settings);

        protected override void LoadContent()
        {
            LoadParticleEffect();

            vertexDeclaration = new VertexDeclaration(ParticleVertex.VertexElements);

            int numVertices = partVerts.Length;
            vertexBuffer = new DynamicVertexBuffer(GraphicsDevice,
                vertexDeclaration,
                numVertices,
                BufferUsage.WriteOnly);

            MakeIndexBuffer(particles.Length);
        }

        private void MakeIndexBuffer(int numParticles)
        {
            short[] indices = new short[numParticles * 6];

            for (int i = 0; i < numParticles; ++i)
            {
                indices[i * 6 + 0] = (short)(i * 4 + 0);
                indices[i * 6 + 1] = (short)(i * 4 + 1);
                indices[i * 6 + 2] = (short)(i * 4 + 2);

                indices[i * 6 + 3] = (short)(i * 4 + 2);
                indices[i * 6 + 4] = (short)(i * 4 + 1);
                indices[i * 6 + 5] = (short)(i * 4 + 3);
            }

            indexBuffer = new IndexBuffer(GraphicsDevice,
                typeof(short),
                numParticles * 6,
                BufferUsage.WriteOnly);

            indexBuffer.SetData<short>(indices);
        }

        private void LoadParticleEffect()
        {
            {
                Effect effect = content.Load<Effect>("ParticleEffect");

                particleEffect = effect.Clone();
            }

            EffectParameterCollection parameters = particleEffect.Parameters;

            effectViewParameter = parameters["View"];
            effectProjectionParameter = parameters["Projection"];
            effectViewportHeightParameter = parameters["ViewportHeight"];
            effectAspectParameter = parameters["Aspect"];
            effectTimeParameter = parameters["CurrentTime"];


        }

        #endregion
    }
}
