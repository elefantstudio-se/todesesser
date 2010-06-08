using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;

namespace Todesesser.Map.Maps
{
    public class MapTest : MapBase
    {
        private ContentPool Content;
        public MapTest(SpriteBatch batch, ContentPool Content, ObjectPool Objects)
        {
            this.ObjectPool = Objects;
            this.Content = Content;
            this.Batch = batch;
            this.Width = 1000;
            this.Height = 1000;
        }

        public override void LoadContent()
        {
            //Load Objects
            Content.AddTexture2D("Objects\\Walls\\Test", "Objects-Walls-Test");

            base.LoadContent();
        }

        public override void Initialize()
        {
            //Add Objects to Map
            AddObject("tw1", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "tw1", "Objects-Walls-Test"));

            base.Initialize();
        }
    }
}
