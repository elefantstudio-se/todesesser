using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling.ObjectTypes;

namespace Todesesser.Map.Maps
{
    public class MapTest : MapBase
    {
        public MapTest(SpriteBatch batch, ContentPool Content, ObjectPool Objects)
        {
            this.ObjectPool = Objects;
            this.Content = Content;
            this.Batch = batch;
            this.Width = 1000;
            this.Height = 1000;
        }

        public override void Update(GameTime gameTime, MapBase map, ObjectPlayer player)
        {
            GetObject("w1").Update(gameTime, player, map);
            base.Update(gameTime, map, player);
        }

        public override void LoadContent()
        {
            //Load Objects
            Content.AddTexture2D("Enemies\\Zombie", "Enemies-Zombie");
            Content.AddTexture2D("Misc\\debugPoint", "Objects-DebugPoint");

            base.LoadContent();
        }

        public override void Initialize()
        {
            //Add Objects to Map
            AddObject("w1", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "w1", "Enemies-Zombie"));

            base.Initialize();
        }
    }
}
