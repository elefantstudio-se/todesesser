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
using Microsoft.Xna.Framework.Input;

namespace Todesesser.WeaponEngine.WeaponTypes
{
    public class SemiAuto : WeaponBase
    {
        public bool canFire = true;
        public override void Shoot(double rotation, int fromX, int fromY, MapBase map, double aimX, double aimY)
        {
            if(canFire == true)
                base.Shoot(rotation, fromX, fromY, map, aimX, aimY);
            canFire = false;
        }

        public override void LoadContent(ContentPool Content, ObjectPool Objects)
        {
            base.LoadContent(Content, Objects);
        }

        public override void Update(GameTime gameTime, int attachX, int attachY, double rotation)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released)
                canFire = true;
            base.Update(gameTime, attachX, attachY, rotation);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            base.Draw(gameTime, sb, rotation);
        }
    }
}
