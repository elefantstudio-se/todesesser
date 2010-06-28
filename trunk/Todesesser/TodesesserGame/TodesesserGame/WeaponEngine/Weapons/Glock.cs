using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todesesser.WeaponEngine.Ammo;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Map;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.WeaponEngine.WeaponTypes;

namespace Todesesser.WeaponEngine.Weapons
{
    public class Glock : Automatic
    {
        public Glock()
        {
            this.Ammo = 20;
            this.FireRate = 0.3;
            this.MaxClip = 20;
            this.Damage = 40;
            this.DefaultDamage = 40;
            this.ReloadTime = 1.2;
            this.ShootType = GunShootTypes.Automatic;
            this.Type = GunTypes.Pistol;
            this.Name = "Glock";
            this.BulletSpeed = 15;
            this.Bullets = new List<ObjectBullet>();
            this.Penetration = 2;
            this.PenetrationScale = 0.5f;
        }
    }
}
