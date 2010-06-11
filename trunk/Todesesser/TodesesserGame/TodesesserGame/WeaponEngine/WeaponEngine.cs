using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework.Input;
using Todesesser.WeaponEngine.Weapons;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.Map;

namespace Todesesser.WeaponEngine
{
    public class WeaponEngine
    {
        private ContentPool Content;
        private ObjectPool Objects;
        private List<WeaponBase> availableWeapons;
        private WeaponBase currentWeapon;

        private bool tabActivated = false;

        public WeaponEngine(ContentPool Content, ObjectPool Objects)
        {
            this.Content = Content;
            this.Objects = Objects;
            this.availableWeapons = new List<WeaponBase>();
            this.currentWeapon = null;
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime, int playerX, int playerY, MapBase map, double rotation, double aimX, double aimY)
        {
            KeyboardState kybd = Keyboard.GetState();
            if (kybd.IsKeyDown(Keys.Tab) && tabActivated == false)
            {
                if (currentWeapon != null)
                {
                    if (availableWeapons.IndexOf(currentWeapon) + 1 < availableWeapons.Count)
                    {
                        currentWeapon = availableWeapons[availableWeapons.IndexOf(currentWeapon) + 1];
                    }
                    else
                    {
                        if (availableWeapons.Count >= 1)
                        {
                            currentWeapon = availableWeapons[0];
                        }
                    }
                }
                else
                {
                    if (availableWeapons.Count >= 1)
                    {
                        currentWeapon = availableWeapons[0];
                    }
                }
                tabActivated = true;
            }
            if (kybd.IsKeyUp(Keys.Tab) && tabActivated == true)
            {
                tabActivated = false;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                currentWeapon.Shoot(rotation, playerX + int.Parse(map.Offset.X.ToString()), playerY + int.Parse(map.Offset.Y.ToString()), map, aimX, aimY);
            }
            currentWeapon.Update(gameTime, playerX, playerY, rotation);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            currentWeapon.Draw(gameTime, sb, rotation);
        }

        public void AddAvailableWeapon(WeaponBase weapon)
        {
            availableWeapons.Add(weapon);
            availableWeapons[availableWeapons.Count - 1].LoadContent(Content, Objects);
            if (this.availableWeapons.Count == 1)
            {
                currentWeapon = availableWeapons[0];
            }
        }

        #region Properties

        public WeaponBase CurrentWeapon
        {
            get { return this.currentWeapon; }
            set { this.currentWeapon = value; }
        }

        #endregion
    }
}
