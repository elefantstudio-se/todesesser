using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling.ObjectTypes;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

namespace Todesesser.Map.Maps
{
    public class MapTest : MapBase
    {
        private PhysicsSimulator simulator;
        private Body testPhysicsBody;
        private ObjectEnemy testPhysicsObject;

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
            for (int i = 0; i < Objects.Keys.Count;i++)
            {
                string[] arrayShit = new string[Objects.Keys.Count];
                Objects.Keys.CopyTo(arrayShit,0);
                string ObjectKey = arrayShit[i];
                ObjectBase Object = (ObjectBase)Objects[ObjectKey];
                switch (Object.Type)
                {
                    case ObjectPooling.ObjectPool.ObjectTypes.Enemy:
                        Object.Update(gameTime, player, map);
                        break;

                    case ObjectPooling.ObjectPool.ObjectTypes.EnemySpawner:
                        Object.Update(gameTime, map, ObjectPool);
                        break;
                }
            }
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
            Random random = new Random();
            for (int i = 0; i < 25; i++)
            {
                AddObject("w" + i, ObjectPool.AddObject(ObjectPool.ObjectTypes.EnemySpawner, "w" + i, "1x1white"));
                GetObject("w" + i).Position = new Vector2(random.Next(-1000, 2000), random.Next(-1000, 2000));
                ObjectEnemySpawner spawner = GetObject<ObjectEnemySpawner>("w" + i);
                spawner.SpawnTime = 1;
                spawner.EType = "basic";
                spawner.MaxSpawned = 4;
            }

            simulator = new PhysicsSimulator(new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, -5));
            testPhysicsBody = BodyFactory.Instance.CreateRectangleBody(5, 5, 10);
            testPhysicsBody.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(250, 0);
            AddObject("phy1", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Enemy, "phy1", "1x1white"));
            testPhysicsObject = GetObject<ObjectEnemy>("phy1");
            testPhysicsObject.PhysicsBody = testPhysicsBody;

            base.Initialize();
        }
    }
}
