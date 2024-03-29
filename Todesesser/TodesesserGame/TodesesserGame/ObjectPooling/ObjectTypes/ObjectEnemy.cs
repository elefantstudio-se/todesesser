﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;
using Todesesser.WeaponEngine;
using Todesesser.Map;
using Todesesser.Core;
using FarseerGames.FarseerPhysics.Dynamics;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectEnemy : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private Color colour = Color.White;
        private int health = 1;
        private double scale = 0.5;
        private Body physicsBody;

        public ObjectEnemy(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(50, 50);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
            this.BoundingRectangle = new Rectangle(Convert.ToInt32(this.Position.X - ((Texture.Width * scale) / 2)), Convert.ToInt32(this.Position.Y - ((Texture.Height * scale) / 2)), Convert.ToInt32((Texture.Width * scale) - ((Texture.Width * scale) / 2)), Convert.ToInt32((Texture.Height * scale) - ((Texture.Height * scale) / 2)));
        }

        public override void Update(GameTime gameTime, ObjectPlayer player, MapBase map)
        {
            if (health > 0)
            {
                Vector2 releventPlayerPosition = new Vector2(player.Position.X + Convert.ToInt32(map.Offset.X), player.Position.Y + Convert.ToInt32(map.Offset.Y));

                this.Rotation = float.Parse(GameFunctions.GetAngle(this.Position, releventPlayerPosition).ToString());

                int rot = Convert.ToInt32(MathHelper.ToDegrees(float.Parse(this.Rotation.ToString()))) - 90;
                double xs = Math.Cos((rot * Math.PI) / 180);
                double xy = Math.Sin((rot * Math.PI) / 180);
                Vector2 OldPos = this.Position;
                this.Position = new Vector2(this.Position.X - float.Parse(xs.ToString()), this.Position.Y - float.Parse(xy.ToString()));
                this.BoundingRectangle = new Rectangle(Convert.ToInt32(this.Position.X - ((Texture.Width * scale) / 2)), Convert.ToInt32(this.Position.Y - ((Texture.Height * scale) / 2)), Convert.ToInt32(Texture.Width * scale), Convert.ToInt32(Texture.Height * scale));

                //Hit Test!
                float dis = Vector2.Distance(this.Position, releventPlayerPosition);
                Vector2 MV = (this.Position - releventPlayerPosition) * .05f;// / dis;
                if (dis < 40)
                {
                    this.Position = OldPos;
                    this.Position = this.Position + MV;
                    //player.Position = player.Position - MV;
                    player.Health -= 1;
                }
            }
            base.Update(gameTime);
        }

        public override void OnHit(WeaponBase weapon, Vector2 from)
        {
            health -= weapon.Damage;
            if (health <= 0)
            {
                //Update Stats
                GameStats.AppendStat("KilledEnemies", 1);
            }
            base.OnHit(weapon, from);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (health > 0)
            {
                if (physicsBody == null)
                {
                    sb.Draw(Texture, new Rectangle(Convert.ToInt32(this.Position.X), Convert.ToInt32(this.Position.Y), Texture.Width, Texture.Height), colour);
                }
                else
                {
                    sb.Draw(Texture, new Rectangle(Convert.ToInt32(physicsBody.Position.X), Convert.ToInt32(physicsBody.Position.Y), Texture.Width, Texture.Height), colour);
                }
                base.Draw(gameTime, sb);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, Vector2 offset)
        {
            if (health > 0)
            {
                if (physicsBody == null)
                {
                    sb.Draw(Texture, new Rectangle(Convert.ToInt32(this.Position.X) - Convert.ToInt32(offset.X), Convert.ToInt32(this.Position.Y) - Convert.ToInt32(offset.Y), Convert.ToInt32(Texture.Width * scale), Convert.ToInt32(Texture.Height * scale)), null, colour, float.Parse(this.Rotation.ToString()) - MathHelper.ToRadians(180), new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
                }
                else
                {
                    sb.Draw(Texture, new Rectangle(Convert.ToInt32(physicsBody.Position.X) - Convert.ToInt32(offset.X), Convert.ToInt32(physicsBody.Position.Y) - Convert.ToInt32(offset.Y), Convert.ToInt32(Texture.Width * scale), Convert.ToInt32(Texture.Height * scale)), null, colour, physicsBody.Rotation - MathHelper.ToRadians(180), new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
                }
                base.Draw(gameTime, sb, offset);
            }
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }

        public int Health
        {
            get { return this.health; } 
        }

        public Body PhysicsBody
        {
            get
            {
                return this.physicsBody;
            }
            set
            {
                this.physicsBody = value;
            }
        }
    }
}
