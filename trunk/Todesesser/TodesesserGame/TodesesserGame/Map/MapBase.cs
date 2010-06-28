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
            //==============================
            //Y = -256
            //==============================
            groundObjects.Add("eg-2", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-2", groundContentKey));
            ((ObjectGround)groundObjects["eg-2"]).Position = new Vector2(-256, -256);
            groundObjects.Add("eg-3", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-3", groundContentKey));
            ((ObjectGround)groundObjects["eg-3"]).Position = new Vector2(0, -256);
            groundObjects.Add("eg-4", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-4", groundContentKey));
            ((ObjectGround)groundObjects["eg-4"]).Position = new Vector2(256, -256);
            groundObjects.Add("eg-5", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-5", groundContentKey));
            ((ObjectGround)groundObjects["eg-5"]).Position = new Vector2(512, -256);
            groundObjects.Add("eg-6", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-6", groundContentKey));
            ((ObjectGround)groundObjects["eg-6"]).Position = new Vector2(768, -256);
            groundObjects.Add("eg-7", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-7", groundContentKey));
            ((ObjectGround)groundObjects["eg-7"]).Position = new Vector2(1024, -256);
            //==============================
            //Y = 0
            //==============================
            groundObjects.Add("eg-8", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-8", groundContentKey));
            ((ObjectGround)groundObjects["eg-8"]).Position = new Vector2(-256, 0);
            groundObjects.Add("eg-9", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-9", groundContentKey));
            ((ObjectGround)groundObjects["eg-9"]).Position = new Vector2(0, 0);
            groundObjects.Add("eg-10", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-10", groundContentKey));
            ((ObjectGround)groundObjects["eg-10"]).Position = new Vector2(256, 0);
            groundObjects.Add("eg-11", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-11", groundContentKey));
            ((ObjectGround)groundObjects["eg-11"]).Position = new Vector2(512, 0);
            groundObjects.Add("eg-12", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-12", groundContentKey));
            ((ObjectGround)groundObjects["eg-12"]).Position = new Vector2(768, 0);
            groundObjects.Add("eg-13", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-13", groundContentKey));
            ((ObjectGround)groundObjects["eg-13"]).Position = new Vector2(1024, 0);
            //==============================
            //Y = 256
            //==============================
            groundObjects.Add("eg-14", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-14", groundContentKey));
            ((ObjectGround)groundObjects["eg-14"]).Position = new Vector2(-256, 256);
            groundObjects.Add("eg-15", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-15", groundContentKey));
            ((ObjectGround)groundObjects["eg-15"]).Position = new Vector2(0, 256);
            groundObjects.Add("eg-16", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-16", groundContentKey));
            ((ObjectGround)groundObjects["eg-16"]).Position = new Vector2(256, 256);
            groundObjects.Add("eg-17", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-17", groundContentKey));
            ((ObjectGround)groundObjects["eg-17"]).Position = new Vector2(512, 256);
            groundObjects.Add("eg-18", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-18", groundContentKey));
            ((ObjectGround)groundObjects["eg-18"]).Position = new Vector2(768, 256);
            groundObjects.Add("eg-19", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-19", groundContentKey));
            ((ObjectGround)groundObjects["eg-19"]).Position = new Vector2(1024, 256);
            //==============================
            //Y = 512
            //==============================
            groundObjects.Add("eg-20", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-20", groundContentKey));
            ((ObjectGround)groundObjects["eg-20"]).Position = new Vector2(-256, 512);
            groundObjects.Add("eg-21", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-21", groundContentKey));
            ((ObjectGround)groundObjects["eg-21"]).Position = new Vector2(0, 512);
            groundObjects.Add("eg-22", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-22", groundContentKey));
            ((ObjectGround)groundObjects["eg-22"]).Position = new Vector2(256, 512);
            groundObjects.Add("eg-23", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-23", groundContentKey));
            ((ObjectGround)groundObjects["eg-23"]).Position = new Vector2(512, 512);
            groundObjects.Add("eg-24", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-24", groundContentKey));
            ((ObjectGround)groundObjects["eg-24"]).Position = new Vector2(768, 512);
            groundObjects.Add("eg-25", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-25", groundContentKey));
            ((ObjectGround)groundObjects["eg-25"]).Position = new Vector2(1024, 512);
            //==============================
            //Y = 768
            //==============================
            groundObjects.Add("eg-26", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-26", groundContentKey));
            ((ObjectGround)groundObjects["eg-26"]).Position = new Vector2(-256, 768);
            groundObjects.Add("eg-27", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-27", groundContentKey));
            ((ObjectGround)groundObjects["eg-27"]).Position = new Vector2(0, 768);
            groundObjects.Add("eg-28", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-28", groundContentKey));
            ((ObjectGround)groundObjects["eg-28"]).Position = new Vector2(256, 768);
            groundObjects.Add("eg-29", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-29", groundContentKey));
            ((ObjectGround)groundObjects["eg-29"]).Position = new Vector2(512, 768);
            groundObjects.Add("eg-30", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-30", groundContentKey));
            ((ObjectGround)groundObjects["eg-30"]).Position = new Vector2(768, 768);
            groundObjects.Add("eg-31", ObjectPool.AddObject(ObjectPooling.ObjectPool.ObjectTypes.Ground, "eg-31", groundContentKey));
            ((ObjectGround)groundObjects["eg-31"]).Position = new Vector2(1024, 768);
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

            /*foreach (string objkey in this.objects.Keys)
            {
                ObjectBase obj = (ObjectBase)this.objects[objkey];

                Batch.Draw(Content.GetTexture2D("1x1white").Texture, new Rectangle(obj.BoundingRectangle.X - Convert.ToInt32(this.Offset.X), obj.BoundingRectangle.Y - Convert.ToInt32(this.Offset.Y), obj.BoundingRectangle.Width, obj.BoundingRectangle.Height), Color.Black);
            }*/
            //Draw Objects
            foreach (string Key in this.objects.Keys)
            {
                ObjectBase objbase = (ObjectBase)this.objects[Key];
                if (objbase.Type == ObjectPooling.ObjectPool.ObjectTypes.Enemy)
                {
                    objbase.Draw(gameTime, batch, Offset);
                    Random rand = new Random();

                    if(rand.Next(2)<1f)
                    {
                        foreach (string obj in this.objects.Keys)
                        {
                            ObjectBase bob = this.objects[obj] as ObjectBase;
                            if (objbase.Type == ObjectPooling.ObjectPool.ObjectTypes.Enemy &&
                                bob.Type == ObjectPooling.ObjectPool.ObjectTypes.Enemy)
                            {
                                float dis = Vector2.Distance(bob.Position,objbase.Position);
                                if (dis < 45) 
                                {
                                    Vector2 MV = (bob.Position - objbase.Position) * .04f;
                                    bob.Position = bob.Position + MV;
                                    objbase.Position = objbase.Position - MV;
                                    if (dis < 20)
                                    {
                                        MV *= 4;
                                        bob.Position = bob.Position + MV;
                                        objbase.Position = objbase.Position - MV;
                                    }
                                }
                            }
                        }
                    }
                }
                else if(objbase.Type != ObjectPooling.ObjectPool.ObjectTypes.Bullet)
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
