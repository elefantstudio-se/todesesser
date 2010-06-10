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
            Content.AddTexture2D("Misc\\debugPoint", "Objects-DebugPoint");

            base.LoadContent();
        }

        public override void Initialize()
        {
            //Add Objects to Map
            AddObject("tw1", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "tw1", "Objects-Walls-Test"));
            AddObject("tdp1", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.DebugPoint, "tdp1", "Objects-DebugPoint"));
            //Setup Objects
            ObjectBase b = (ObjectBase)this.Objects["tdp1"];
            b.Position = new Vector2(25, 25);
            UpdateObject("tdp1", b);

            base.Initialize();
        }
    }
}
