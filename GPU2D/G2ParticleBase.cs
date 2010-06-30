using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GPU2D
{
    public abstract class G2ParticleBase
    {
        #region Variables

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

#endregion

        GraphicsDevice device;

        #region Initialize

        public G2ParticleBase(Game game, ContentManager content, GraphicsDevice device, Effect particleEffect)
        {
            this.particleEffect = particleEffect;
            this.content = content;
            this.device = device;
        }

        public void Initialize()
        {
            InitializeSettings(settings);

            particles = new ParticleVertex[settings.maxParticles];
            partVerts = new ParticleVertex[particles.Length * 4];
        }

        public abstract void InitializeSettings(G2ParticleSettings settings);

        public void LoadContent()
        {
            LoadParticleEffect();

            vertexDeclaration = new VertexDeclaration(ParticleVertex.VertexElements);

            int numVertices = partVerts.Length;
            vertexBuffer = new DynamicVertexBuffer(device,
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

            indexBuffer = new IndexBuffer(device,
                typeof(short),
                numParticles * 6,
                BufferUsage.WriteOnly);

            indexBuffer.SetData<short>(indices);
        }

        private void LoadParticleEffect()
        {

            EffectParameterCollection parameters = particleEffect.Parameters;

            effectViewParameter = parameters["View"];
            effectProjectionParameter = parameters["Projection"];
            effectViewportHeightParameter = parameters["ViewportHeight"];
            effectAspectParameter = parameters["Aspect"];
            effectTimeParameter = parameters["CurrentTime"];

            parameters["Duration"].SetValue((float)settings.duration.TotalSeconds);
            parameters["DurationRandomness"].SetValue(settings.durationRandomness);
            parameters["Gravity"].SetValue(settings.gravity);
            parameters["EndVelocity"].SetValue(settings.endVelocity);
            parameters["MinColor"].SetValue(settings.minColour.ToVector4());
            parameters["MaxColor"].SetValue(settings.maxColour.ToVector4());

            parameters["RotateSpeed"].SetValue(new Vector2(settings.minRotateSpeed, settings.maxRotateSpeed));

            Vector2 sz = parameters["StartSize"].GetValueVector2();
            parameters["StartSize"].SetValue(
                new Vector2(settings.minStartSize, settings.maxStartSize));
            sz = parameters["StartSize"].GetValueVector2();

            parameters["EndSize"].SetValue(new Vector2(settings.minEndSize, settings.maxEndSize));

            Texture2D texture = content.Load<Texture2D>(settings.textureName);

            parameters["Texture"].SetValue(texture);

            string techniqueName;

            techniqueName = "Particles";

            particleEffect.CurrentTechnique = particleEffect.Techniques[techniqueName];
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException("gameTime");

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            RetireActiveParticles();
            FreeRetiredParticles();

            if (firstActiveParticle == firstFreeParticle)
                currentTime = 0;

            if (firstRetiredParticle == firstActiveParticle)
                drawCounter = 0;
        }

        private void RetireActiveParticles()
        {
            float particleDuration = (float)settings.duration.TotalSeconds;

            while (firstActiveParticle != firstNewParticle)
            {
                float particleAge = currentTime - particles[firstActiveParticle].Time;

                if (particleAge < particleDuration)
                    break;

                particles[firstActiveParticle].Time = drawCounter;

                firstActiveParticle++;

                if (firstActiveParticle >= particles.Length)
                    firstActiveParticle = 0;
            }
        }

        private void FreeRetiredParticles()
        {
            while (firstRetiredParticle != firstActiveParticle)
            {
                int age = drawCounter - (int)particles[firstRetiredParticle].Time;

                if (age < 3)
                    break;

                firstRetiredParticle++;

                if (firstRetiredParticle >= particles.Length)
                    firstRetiredParticle = 0;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (vertexBuffer.IsContentLost)
            {
                vertexBuffer.SetData(partVerts);
            }

            if (firstNewParticle != firstFreeParticle)
            {
                AddNewParticlesToVertexBuffer();
            }

            if (firstActiveParticle != firstFreeParticle)
            {
                SetParticleRenderStates();

                effectViewportHeightParameter.SetValue(device.Viewport.Height);
                float aspect = ((float)device.Viewport.Height) / device.Viewport.Width;
                effectAspectParameter.SetValue(aspect);

                Vector2 sz = particleEffect.Parameters["EndSize"].GetValueVector2();

                effectTimeParameter.SetValue(currentTime);

                device.SetVertexBuffer(vertexBuffer);
                device.Indices = indexBuffer;

                foreach (EffectPass pass in particleEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    if (firstActiveParticle < firstFreeParticle)
                    {
                        device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                            0,
                            firstActiveParticle * 4,
                            (firstFreeParticle - firstActiveParticle) * 4,
                            firstActiveParticle * 6,
                            (firstFreeParticle - firstActiveParticle) * 2);
                    }
                    else
                    {
                        device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                            0,
                            firstActiveParticle * 4,
                            (particles.Length - firstActiveParticle) * 4,
                            firstActiveParticle * 6,
                            (particles.Length - firstActiveParticle) * 2);

                        if (firstActiveParticle > 0)
                        {
                            device.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                                0,
                                0,
                                firstFreeParticle * 4,
                                0,
                                firstFreeParticle * 2);
                        }
                    }
                }
            }

            drawCounter++;
        }

        private ParticleVertex[] ParticlesToVertices(ParticleVertex[] particles)
        {
            for (int i = 0; i < particles.Length; ++i)
            {
                partVerts[i * 4 + 0] = particles[i];
                partVerts[i * 4 + 0].Offset = new Vector2(-1, -1);

                partVerts[i * 4 + 1] = particles[i];
                partVerts[i * 4 + 1].Offset = new Vector2(-1, 1);

                partVerts[i * 4 + 2] = particles[i];
                partVerts[i * 4 + 2].Offset = new Vector2(1, -1);

                partVerts[i * 4 + 3] = particles[i];
                partVerts[i * 4 + 3].Offset = new Vector2(1, 1);
            }
            return partVerts;
        }

        private void AddNewParticlesToVertexBuffer()
        {
            int stride = ParticleVertex.SizeInBytes;

            partVerts = ParticlesToVertices(particles);

            if (firstNewParticle < firstFreeParticle)
            {
                vertexBuffer.SetData(firstNewParticle * 4 * stride,
                                     partVerts,
                                     firstNewParticle * 4,
                                     (firstFreeParticle - firstNewParticle) * 4,
                                     stride, SetDataOptions.NoOverwrite);
            }
            else
            {
                vertexBuffer.SetData(firstNewParticle * 4 * stride,
                                     partVerts,
                                     firstNewParticle * 4,
                                     (particles.Length - firstNewParticle) * 4,
                                     stride, SetDataOptions.NoOverwrite);

                if (firstFreeParticle > 0)
                {
                    vertexBuffer.SetData(0,
                                         partVerts,
                                         0,
                                         firstFreeParticle * 4,
                                         stride, SetDataOptions.NoOverwrite);
                }
            }

            firstNewParticle = firstFreeParticle;
        }

        private void SetParticleRenderStates()
        {
            device.BlendState = settings.blend;
            device.DepthStencilState = DepthStencilState.DepthRead;
        }

        #endregion

        #region Public Methods

        public void SetCamera(Matrix view, Matrix projection)
        {
            effectViewParameter.SetValue(view);
            effectProjectionParameter.SetValue(projection);
        }

        public void AddParticle(Vector3 position, Vector3 velocity)
        {
            int nextFreeParticle = firstFreeParticle + 1;

            if (nextFreeParticle >= particles.Length)
                nextFreeParticle = 0;

            if (nextFreeParticle == firstRetiredParticle)
                return;

            velocity *= settings.emitterVelocitySensitivity;

            float horizontalVelocity = MathHelper.Lerp(settings.minHorizontalVelocity,
                settings.maxHorizontalVelocity,
                (float)random.NextDouble());

            double horizontalAngle = random.NextDouble() * MathHelper.TwoPi;

            velocity.X += horizontalVelocity * (float)Math.Cos(horizontalAngle);
            velocity.Y += horizontalVelocity * (float)Math.Sin(horizontalAngle);

            velocity.Y += MathHelper.Lerp(settings.minVerticalVelocity,
                settings.maxVerticalVelocity,
                (float)random.NextDouble());

            Color randomValues = new Color((byte)random.Next(255),
                (byte)random.Next(255),
                (byte)random.Next(255),
                (byte)random.Next(255));

            particles[firstFreeParticle].Position = position;
            particles[firstFreeParticle].Velocity = velocity;
            particles[firstFreeParticle].Random = randomValues;
            particles[firstFreeParticle].Time = currentTime;

            firstFreeParticle = nextFreeParticle;
        }

        #endregion
    }
}
