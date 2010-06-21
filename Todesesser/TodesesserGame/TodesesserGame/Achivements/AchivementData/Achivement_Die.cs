using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Todesesser.Core;

namespace Todesesser.Achivements.AchivementData
{
    public class Achivement_Die : AchivementBase
    {
        public Achivement_Die()
        {
            this.Name = "Die";
            this.Active = true;
        }

        public override void Update(GameTime gameTime)
        {
            //Check if Achivement has been completed
            if (GameStats.GetStat("DeadCount") >= 1)
            {
                this.Active = false;
                this.Completed();
            }
            //Update Base
            base.Update(gameTime);
        }
    }
}
