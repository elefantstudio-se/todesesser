using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Todesesser.Core;

namespace Todesesser.Achivements.AchivementData
{
    public class Achivement_KillZombie : AchivementBase
    {
        public Achivement_KillZombie()
        {
            this.Name = "Kill Zombie";
            this.Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if Achivement has been completed
            if (GameStats.GetStat<Int32>("KilledEnemies") >= 1)
            {
                this.Active = false;
                this.Completed();
            }
            //Update Base
            base.Update(gameTime);
        }
    }
}
