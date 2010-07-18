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
using GPU2D;
using GPU2D.Particles;
using GPU2D.VParticle;

namespace TodParticles
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        //GPU2D:
        GPU2DEngine gpu2d;

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 853;
            graphics.PreferredBackBufferHeight = 480;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            gpu2d = new GPU2DEngine(Content.Load<Effect>("Basic"), graphics);
            //gpu2d.AddParticle(new G2VSmokePlume(this, Content, new Vector2(200, 200)));
            //gpu2d.AddParticle(new G2VFire(this, Content, new Vector2(100,100)));
            //gpu2d.AddParticle(new G2VExplosion(this, Content, new Vector2(300, 300)));
            //gpu2d.AddParticle(new G2VFireRing(this, Content, new Vector2(400, 400), 40, 30));
            gpu2d.AddParticle(new G2VFireDirectional(this, Content, new Vector2(100, 100), new Vector2(200, 200), 5));
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Allows the game to run logic.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            gpu2d.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;

            device.Clear(Color.CornflowerBlue);

            gpu2d.Draw(gameTime, graphics);

            base.Draw(gameTime);
        }

        #endregion
    }
}
