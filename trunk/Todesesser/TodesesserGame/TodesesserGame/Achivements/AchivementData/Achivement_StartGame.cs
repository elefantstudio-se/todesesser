using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Todesesser.Core;

namespace Todesesser.Achivements.AchivementData
{
    public class Achivement_StartGame : AchivementBase
    {
        public Achivement_StartGame()
        {
            this.Name = "Start Game";
            this.Active = true;
        }

        public override void LoadContent(ContentPooling.ContentPool Content)
        {
            Content.AddTexture2D("AchivementIcons\\aiconStartGame", "aIconStartGame");
            this.ContentKey = "aIconStartGame";
            this.ContentLoaded = true;
            base.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            //Check if Achivement has been completed
            if (GameData.GameState == GameData.GameStates.Playing)
            {
                this.Active = false;
                this.Completed();
            }
            //Update Base
            base.Update(gameTime);
        }
    }
}
