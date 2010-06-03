using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todesesser.WeaponEngine.Ammo;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.WeaponEngine.Weapons
{
    public class Glock : WeaponBase
    {
        public Glock()
        {
            this.Ammo = (AmmoBase)new _9MM();
            this.MaxClip = 20;
            this.Damage = 40;
            this.DefaultDamage = 40;
            this.ReloadTime = 1.6;
            this.ShootType = GunShootTypes.Automatic;
            this.Type = GunTypes.Pistol;
            this.Name = "Glock";
            this.BulletSpeed = 15;
        }

        public override void LoadContent(ContentPool Content, ObjectPool Objects)
        {
            base.LoadContent(Content, Objects);
        }

        public override void Update(GameTime gameTime, int attachX, int attachY)
        {
            base.Update(gameTime, attachX, attachY);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            base.Draw(gameTime, sb);
        }
    }
}
