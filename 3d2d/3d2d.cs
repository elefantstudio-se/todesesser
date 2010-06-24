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
using _3d2d.Shadows;
using System.Diagnostics;

namespace _3d2d
{
    public class _3d2d
    {
        private List<RectangleShadow> rectShadows;
        private Effect shader;
        private ContentManager Content;
        private GraphicsDeviceManager graphics;

        Matrix View;
        Matrix Projection;
        Vector3 CameraPosition;
        Vector3 CameraTarget;

        public _3d2d(Game game, ContentManager Content, GraphicsDeviceManager graphics)
        {
            Debug.WriteLine("3d2d->__CONSTRUCT__");
            rectShadows = new List<RectangleShadow>();
            this.Content = Content;
            this.graphics = graphics;

            //CameraPosition = new Vector3(-375, 5, -275);
            //CameraTarget = new Vector3(-375, -5, -275);
        }
        public void Initialize()
        {
            //Debug.WriteLine("3d2d->Initialize");

            //rectShadows.Add(new RectangleShadow(new Vector2(0,0), new Vector2(0,25), new Vector2(25,0), new Vector2(25,25), Color.Blue));
        }

        public void LoadContent()
        {
            //Debug.WriteLine("3d2d->LoadContent");
            //shader = Content.Load<Effect>("Basic");
        }

        public void UnloadContent()
        {
            //Debug.WriteLine("3d2d->UnloadContent");
        }

        public void Update(GameTime gameTime)
        {
            //Debug.WriteLine("3d2d->Update");
            //View = Matrix.CreateLookAt(CameraPosition, CameraTarget, Vector3.UnitZ);
            //Projection = Matrix.CreateOrthographic(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1, 100);
        }

        public void Draw(GameTime gameTime, GraphicsDevice device)
        {
            //Debug.WriteLine("3d2d->Draw");
            //shader.Parameters["ViewProjection"].SetValue(View * Projection);

            //shader.CurrentTechnique.Passes[0].Apply();



            //foreach (RectangleShadow shadow in rectShadows)
            //{
            //    shadow.Draw(device);
            //}
        }
    }
}
