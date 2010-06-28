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
using Todesesser.WeaponEngine.WeaponTypes;

namespace Todesesser.WeaponEngine.Weapons
{
    public class USP : SemiAuto 
    {
        public USP()
        {
            this.Ammo = 12;
            this.MaxClip = 12;
            this.FireRate = 0;
            this.Damage = 50;
            this.DefaultDamage = 50;
            this.ReloadTime = 1.9;
            this.ShootType = GunShootTypes.SemiAutomatic;
            this.Type = GunTypes.Pistol;
            this.Name = "USP";
            this.BulletSpeed = 15;
            this.Bullets = new List<ObjectBullet>();
            this.Penetration = 2;
            this.PenetrationScale = 0.5f;
        }
    }
}
