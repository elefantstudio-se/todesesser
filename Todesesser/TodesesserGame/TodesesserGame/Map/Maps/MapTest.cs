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
            Initializer();
        }

        public override void Update(GameTime gameTime, MapBase map, ObjectPlayer player)
        {
            GetObject("w1").Update(gameTime, player, map);
            GetObject("w2").Update(gameTime, player, map);
            GetObject("w3").Update(gameTime, player, map);
            GetObject("w4").Update(gameTime, player, map);
            GetObject("w5").Update(gameTime, player, map);
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
            GetObject("w1").Position = new Vector2(0, 0);
            AddObject("w2", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "w2", "Enemies-Zombie"));
            GetObject("w2").Position = new Vector2(100, 0);
            AddObject("w3", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "w3", "Enemies-Zombie"));
            GetObject("w3").Position = new Vector2(200, 0);
            AddObject("w4", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "w4", "Enemies-Zombie"));
            GetObject("w4").Position = new Vector2(300, 0);
            AddObject("w5", ObjectPool.AddObject(ObjectPool.ObjectTypes.Wall, "w5", "Enemies-Zombie"));
            GetObject("w5").Position = new Vector2(400, 0);

            base.Initialize();
        }
    }
}
