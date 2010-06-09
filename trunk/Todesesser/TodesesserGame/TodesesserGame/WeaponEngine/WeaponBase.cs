﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling.ObjectTypes;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Map;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;

namespace Todesesser.WeaponEngine
{
    public class WeaponBase
    {
        public enum GunShootTypes { SemiAutomatic, Automatic };
        public enum GunTypes { SniperRifle, MachineGun, SubMachineGun, Pistol, Shotgun, Melee };
        private AmmoBase ammo;
        private int maxClip;
        private int fireRate;
        private int damage;
        private int defaultDamage;
        private double reloadTime;
        private GunShootTypes gunShootType;
        private GunTypes gunType;
        private string name;
        private int bulletSpeed;

        private ObjectWeapon objWeapon;
        private List<ObjectBullet> bullets;
        private ContentPool contentPool;

        public virtual void LoadContent(ContentPool Content, ObjectPool Objects)
        {
            this.Content = Content;
            Content.AddTexture2D("Bullets\\bullet", this.name + "_bullet");
            Content.AddTexture2D("Weapons\\debug", this.name + "_gun");
            this.ObjWeapon = (ObjectWeapon)Objects.AddObject(ObjectPool.ObjectTypes.Weapon, "Player" + this.name, this.name + "_gun");
        }

        public virtual void Update(GameTime gameTime, int attachX, int attachY, double rotation)
        {
            if (this.Bullets != null)
            {
                this.ObjWeapon.Update(gameTime);
                this.ObjWeapon.Position = new Vector2(attachX, attachY);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            this.ObjWeapon.Draw(gameTime, sb, 0, new Vector2(0, 0), new Vector2(0, 0));
        }
        
        public virtual void Shoot(double rotation, int fromX, int fromY, MapBase map)
        {
            if (Content != null && this.Bullets != null)
            {
                //TODO: Ray Trace & Muzzle Flash Goes Here
                Ray2D ray = new Ray2D(new Vector2(fromX, fromY), new Vector2(fromX, fromY) * 2);
                
            }
        }

        #region Properties

        public AmmoBase Ammo
        {
            get { return this.ammo; }
            set { this.ammo = value; }
        }

        public int MaxClip
        {
            get { return this.maxClip; }
            set { this.maxClip = value; }
        }

        public int FireRate
        {
            get { return this.fireRate; }
            set { this.fireRate = value; }
        }

        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public int DefaultDamage
        {
            get { return this.defaultDamage; }
            set { this.defaultDamage = value; }
        }

        public double ReloadTime
        {
            get { return this.reloadTime; }
            set { this.reloadTime = value; }
        }

        public GunShootTypes ShootType
        {
            get { return this.gunShootType; }
            set { this.gunShootType = value; }
        }

        public GunTypes Type
        {
            get { return this.gunType; }
            set { this.gunType = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int BulletSpeed
        {
            get { return this.bulletSpeed; }
            set { this.bulletSpeed = value; }
        }

        public ObjectWeapon ObjWeapon
        {
            get { return this.objWeapon; }
            set { this.objWeapon = value; }
        }

        public List<ObjectBullet> Bullets
        {
            get { return this.bullets; }
            set { this.bullets = value; }
        }

        public ContentPool Content
        {
            get { return this.contentPool; }
            set { this.contentPool = value; }
        }

        #endregion
    }
}
