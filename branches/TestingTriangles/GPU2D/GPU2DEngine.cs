//Todesesser, XNA 4.0 C# Game.
//Copyright (C) 2010  Dean Gardiner and Taylor Lodge.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Email: gardiner91@gmail.com.
//
//Full Licence can be found at http://www.gnu.org/licenses/gpl-3.0.txt.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using GPU2D.Primitives;

namespace GPU2D
{
    public class GPU2DEngine
    {
        private List<G2PrimitiveBase> primitives;
        private List<G2VBase> particles;
        private Effect shader;

        Matrix View;
        Matrix Projection;
        int cameraHeight = 5;
        int planeHeight = -5;

        private GraphicsDeviceManager graphics;

        public GPU2DEngine(Effect shader, GraphicsDeviceManager graphics)
        {
            primitives = new List<G2PrimitiveBase>();
            particles = new List<G2VBase>();
            this.shader = shader;
            this.graphics = graphics;
        }

        private void InitializeShader()
        {
            View = Matrix.CreateLookAt(new Vector3(graphics.PreferredBackBufferWidth / 2, cameraHeight, graphics.PreferredBackBufferHeight / 2), new Vector3(graphics.PreferredBackBufferWidth / 2, planeHeight, graphics.PreferredBackBufferHeight / 2), Vector3.UnitZ);
            Projection = Matrix.CreateOrthographic(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1, 100) * Matrix.CreateScale(1) * Matrix.CreateRotationZ(MathHelper.Pi);
        }

        public void Update(GameTime gameTime)
        {
            InitializeShader();

            //Update Particles
            foreach (G2VBase particle in particles)
            {
                particle.Update(gameTime);
            }
        }

        public void DrawPrimitive(G2PrimitiveBase primitive)
        {
            primitives.Add(primitive);
        }

        public int AddParticle(G2VBase particle)
        {
            particle.Initialize();
            particles.Add(particle);
            return particles.IndexOf(particle);
        }

        public void RemoveParticle(int index)
        {
            particles.RemoveAt(index);
        }

        public void Draw(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            shader.Parameters["ViewProjection"].SetValue(View * Projection);

            shader.CurrentTechnique.Passes[0].Apply();

            List<G2PrimitiveBase> removePrimitives = new List<G2PrimitiveBase>();

            foreach (G2PrimitiveBase primitive in primitives)
            {
                primitive.Draw(graphics);
                removePrimitives.Add(primitive);
            }

            foreach (G2PrimitiveBase primitive in removePrimitives)
            {
                primitives.Remove(primitive);
            }

            foreach (G2VBase particle in particles)
            {
                particle.Draw(gameTime, View, Projection);
            }
        }
    }
}
