using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.Core;
using ContentPooling;

namespace Todesesser.Map
{
    public class MapBase
    {
        private Hashtable objects;
        private Hashtable groundObjects;
        private SpriteBatch batch;
        private ObjectPool objectPool;
        public Vector2 Offset;
        private List<ObjectBullet> playerBullets;
        private int width;
        private int height;
        private string groundContentKey;
        private ContentPool content;

        public MapBase()
        {
            this.objects = new Hashtable();
            this.Offset = new Vector2(0, 0);
            this.playerBullets = new List<ObjectBullet>();
            this.groundContentKey = "ground";
        }

        public void Initializer()
        {
            this.groundObjects = new Hashtable();
            //Load Ground
            Content.AddTexture2D("Enviroment\\Ground", groundContentKey);
            //Create Enviroment Ground Objects to cover the screen
            //TODO: Finish Ground Updating!
            //groundObjects.Add("eg-0", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-0", groundContentKey));
            //((ObjectGround)groundObjects["eg-0"]).Position = new Vector2(0, -512);
            //groundObjects.Add("eg-1", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-1", groundContentKey));
            //((ObjectGround)groundObjects["eg-1"]).Position = new Vector2(0, -256);
            //groundObjects.Add("eg-2", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-2", groundContentKey));
            //((ObjectGround)groundObjects["eg-2"]).Position = new Vector2(0, 0);
            //groundObjects.Add("eg-3", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-3", groundContentKey));
            //((ObjectGround)groundObjects["eg-3"]).Position = new Vector2(0, 256);
            //groundObjects.Add("eg-4", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-4", groundContentKey));
            //((ObjectGround)groundObjects["eg-4"]).Position = new Vector2(0, 512);
        }

        public virtual void LoadContent()
        {

        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(GameTime gameTime, MapBase map, ObjectPlayer player)
        {
            //Update Enviroment Ground Objects
            foreach (string Key in this.groundObjects.Keys)
            {
                ObjectBase objbase = (ObjectBase)this.groundObjects[Key];
                objbase.Update(gameTime, player, map);

            }
            //Update Bullets
            foreach (ObjectBullet bullet in this.playerBullets)
            {
                bullet.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            //Draw Ground
            foreach (string Key in this.groundObjects.Keys)
            {
                ObjectBase objbase = (ObjectBase)this.groundObjects[Key];
                objbase.Draw(gameTime, batch, Offset);

            }
            //Draw Objects
            foreach (string Key in this.objects.Keys)
            {
                ObjectBase objbase = (ObjectBase)this.objects[Key];
                if (objbase.Type == ObjectPooling.ObjectPool.ObjectTypes.Enemy)
                {
                    System.Diagnostics.Debug.WriteLine("Drawing " + objbase.Type.ToString());
                }
                if (objbase.Type != ObjectPooling.ObjectPool.ObjectTypes.Bullet)
                {
                    objbase.Draw(gameTime, batch, Offset);
                }
                else
                {
                    ObjectBullet b = (ObjectBullet)objbase;
                    objbase.Draw(gameTime, batch, Offset, b.Rotation);
                }
            }
        }

        public virtual void RemoveObject(string Key)
        {
            this.objects.Remove(Key);
        }

        public virtual void RemoveObjectsStartingWith(string startingwith)
        {
            List<string> pendingremove = new List<string>();
            foreach (string key in this.objects.Keys)
            {
                if (key.StartsWith(startingwith) == true)
                {
                    pendingremove.Add(key);
                }
            }

            foreach (string key in pendingremove)
            {
                this.objects.Remove(key);
                this.ObjectPool.RemoveObject(key);
            }
        }

        public virtual void AddObject(string Key, ObjectBase Object)
        {
            this.objects.Add(Key, Object);
        }

        public virtual ObjectBase GetObject(string Key)
        {
            return (ObjectBase)this.objects[Key];
        }

        public virtual void UpdateObject(string Key, ObjectBase Object)
        {
            this.objects[Key] = Object;
        }

        #region Properties

        public Hashtable Objects
        {
            get { return this.objects; }
            set { this.objects = value; }
        }

        public SpriteBatch Batch
        {
            get { return this.batch; }
            set { this.batch = value; }
        }

        public ObjectPool ObjectPool
        {
            get { return this.objectPool; }
            set { this.objectPool = value; }
        }

        public List<ObjectBullet> PlayerBullets
        {
            get { return this.playerBullets; }
            set { this.playerBullets = value; }
        }

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public ContentPool Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
        #endregion
    }
}
