using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ContentPooling;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Todesesser.Core;
using System.Diagnostics;

namespace Todesesser.ObjectPooling.ObjectTypes
{
    public class ObjectPlayer : ObjectBase
    {
        private string contentKey;
        private ContentPool Content;
        private int speed = 2;
        private int stamina = 1000;
        private const int RUN = 5;
        private const int WALK = 3;
        //TODO FIX THE DAM NAME
        private const int STAMINATHRESHOLD = 150;

        public ObjectPlayer(string Key, ObjectPool.ObjectTypes Type, string ContentKey, ContentPool contentPool)
        {
            this.Position = new Vector2(0, 0);
            this.contentKey = ContentKey;
            this.Key = Key;
            this.Type = Type;
            this.Content = contentPool;
        }

        public override void Update(GameTime gameTime)
        {
            movement();

            this.Rotation = double.Parse(GameFunctions.GetAngle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Position).ToString());

            base.Update(gameTime);
        }

        private bool run = false;
        private bool usedRun = false;
        private void movement()
        {
            //Debug.WriteLine(stamina);
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                run = true;
            else
                run = false;

            if (run == true && stamina > 5 && usedRun == false)
            {
                speed = RUN;
                stamina -= 5;
                if (stamina == 0)
                    usedRun = true;
            }
            else
            {
                speed = WALK;
                if (stamina < 998)
                    stamina += 2;
                if (stamina >= STAMINATHRESHOLD)
                    usedRun = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height), Color.White);
            base.Draw(gameTime, sb);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb, double rotation)
        {
            Rectangle dest = new Rectangle(int.Parse(this.Position.X.ToString()), int.Parse(this.Position.Y.ToString()), Texture.Width, Texture.Height);

            sb.Draw(Texture, dest, new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
            base.Draw(gameTime, sb, 0);
        }

        public Texture2D Texture
        {
            get { return this.Content.GetTexture2D(contentKey).Texture; }
        }

        public int Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
    }
}
