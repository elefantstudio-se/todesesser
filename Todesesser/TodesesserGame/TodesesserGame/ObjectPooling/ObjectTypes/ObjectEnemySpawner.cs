using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Core;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    class ObjectEnemySpawner : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private DateTime timeSinceLastSpawn;
        private float spawnTime;
        private string eType;
        private int maxSpawned = 5;
        private int spawned = 0;
        public ObjectEnemySpawner(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            timeSinceLastSpawn = DateTime.Now;
        }

        public override void Update(GameTime gameTime, Map.MapBase map, ObjectPool objectPool)
        {
            TimeSpan spawnCheck = DateTime.Now - timeSinceLastSpawn;
            if ((spawnCheck.TotalMilliseconds / 1000) > spawnTime && spawned < maxSpawned)
            {
                spawned++;
                switch (eType)
                {
                    case "basic":
                            string guid = Guid.NewGuid().ToString();
                            map.AddObject("w" + guid, objectPool.AddObject(ObjectPool.ObjectTypes.Enemy, "w" + guid, "Enemies-Zombie"));
                            map.GetObject("w" + guid).Position = this.Position;
                        break;
                }
                timeSinceLastSpawn = DateTime.Now;
            }
            base.Update(gameTime);
        }

        public string EType
        {
            get{ return eType; }
            set { eType = value; }
        }

        public float SpawnTime
        {
            get{ return spawnTime; }
            set{ spawnTime = value; }
        }

        public int MaxSpawned
        {
            get { return maxSpawned; }
            set { spawnTime = value; }
        }
    }
}
