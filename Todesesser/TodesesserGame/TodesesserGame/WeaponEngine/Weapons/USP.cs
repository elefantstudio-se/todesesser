using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todesesser.WeaponEngine.Ammo;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Todesesser.ObjectPooling.ObjectTypes;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Map;

namespace Todesesser.WeaponEngine.Weapons
{
    //TODO: Move Most of the Functions to the Super (WeaponBase).
    public class USP : WeaponBase
    {
        public USP()
        {
            this.Ammo = (AmmoBase)new _9MM();
            this.MaxClip = 15;
            this.FireRate = 0;
            this.Damage = 50;
            this.DefaultDamage = 50;
            this.ReloadTime = 1.9;
            this.ShootType = GunShootTypes.SemiAutomatic;
            this.Type = GunTypes.Pistol;
            this.Name = "USP";
            this.BulletSpeed = 15;
            this.Bullets = new List<ObjectBullet>();
        }

        public override void Shoot(float rotation, int fromX, int fromY, MapBase map)
        {
            ObjectBullet b = new ObjectBullet("USP_bullet_" + this.Bullets.Count, ObjectPool.ObjectTypes.Bullet, "USP_bullet", this.Content);
            b.Rotation = rotation;
            b.FromX = fromX + 32;
            b.FromY = fromY;
            b.Position = new Vector2(fromX + 32, fromY);
            b.Weapon = this;
            this.Bullets.Add(b);
            map.AddObject("USP_bullet_" + this.Bullets.Count, b);
            base.Shoot(rotation, fromX, fromY, map);
        }

        public override void LoadContent(ContentPool Content, ObjectPool Objects)
        {
            this.Content = Content;
            Content.AddTexture2D("Bullets\\bullet", "USP_bullet");
            Content.AddTexture2D("Weapons\\debug", "USP_gun");
            this.ObjWeapon = (ObjectWeapon)Objects.AddObject(ObjectPool.ObjectTypes.Weapon, "PlayerUSP", "USP_gun");

            base.LoadContent(Content, Objects);
        }

        public override void Update(GameTime gameTime, int attachX, int attachY)
        {
            foreach (ObjectBullet b in this.Bullets)
            {
                b.Update(gameTime);
            }
            this.ObjWeapon.Update(gameTime);
            this.ObjWeapon.Position = new Vector2(attachX + 30, attachY);
            base.Update(gameTime, attachX, attachY);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            this.ObjWeapon.Draw(gameTime, sb);
            base.Draw(gameTime, sb);
        }
    }
}
