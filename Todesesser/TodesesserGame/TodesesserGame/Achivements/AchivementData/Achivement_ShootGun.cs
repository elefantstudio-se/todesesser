using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Todesesser.Core;

namespace Todesesser.Achivements.AchivementData
{
    public class Achivement_ShootGun : AchivementBase
    {
        public Achivement_ShootGun()
        {
            this.Name = "Shoot Gun";
            this.Active = true;
        }

        public override void LoadContent(ContentPooling.ContentPool Content)
        {
            Content.AddTexture2D("AchivementIcons\\aIconShootGun", "aIconShootGun");
            this.ContentKey = "aIconShootGun";
            this.ContentLoaded = true;
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            //Check if Achivement has been completed
            if (GameStats.GetStat<Int32>("FiredBullets") >= 1)
            {
                this.Active = false;
                this.Completed();
            }
            //Update Base
            base.Update(gameTime);
        }
    }
}
