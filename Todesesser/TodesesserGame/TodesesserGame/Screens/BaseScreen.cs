using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ContentPooling;

namespace Todesesser.Screens
{
    public class BaseScreen
    {
        private GraphicsDevice graphicsDevice;
        private SpriteBatch graphicsBatch;
        private string screenName;

        public virtual void LoadContent()
        {
            
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime)
        {
            
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return this.graphicsDevice; }
            set { this.graphicsDevice = value; }
        }

        public SpriteBatch Batch
        {
            get { return this.graphicsBatch; }
            set { this.graphicsBatch = value; }
        }

        public string ScreenName
        {
            get { return this.screenName; }
            set { this.screenName = value; }
        }
    }
}
