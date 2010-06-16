using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Todesesser.Achivements.AchivementData;
using Todesesser.ObjectPooling.ObjectTypes;
using Todesesser.ObjectPooling;
using Microsoft.Xna.Framework;
using ContentPooling;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.Achivements
{
    public class AchivementEngine
    {
        private Hashtable achivementTable;
        private ObjectAchivementShelf shelf;
        private List<AchivementQueuedMessage> messages;
        private bool waitingForFinish = false;

        public AchivementEngine()
        {
            //Initialize Variables
            this.achivementTable = new Hashtable();
            this.messages = new List<AchivementQueuedMessage>();
            //Load Achivements
            AddAchivement(new Achivement_StartGame());
            AddAchivement(new Achivement_ShootGun());
            AddAchivement(new Achivement_KillZombie());
        }

        public void AddAchivement(AchivementBase Achivement)
        {
            Achivement.OnAchivementCompleted += new AchivementBase.OnCompletedDelegate(AchivementEngine_OnAchivementCompleted);
            this.achivementTable.Add(Achivement.Name, Achivement);
        }

        void AchivementEngine_OnAchivementCompleted(AchivementBase Achivement)
        {
            messages.Add(new AchivementQueuedMessage("Unlocked Achivement\n\n" + Achivement.Name, Achivement.ContentKey));
        }

        public void LoadContent(ObjectPool Objects, ContentPool Content)
        {
            foreach (string key in achivementTable.Keys)
            {
                AchivementBase b = (AchivementBase)achivementTable[key];
                b.LoadContent(Content);
            }
            Content.AddTexture2D("Misc\\achivementShelf", "achivementShelf");
            shelf = (ObjectAchivementShelf)Objects.AddObject(ObjectPool.ObjectTypes.AchivementShelf, "achivementShelf", "achivementShelf");
            shelf.OnMessageFinished += new ObjectAchivementShelf.OnMessageFinishedDelegate(shelf_OnMessageFinished);
        }

        void shelf_OnMessageFinished()
        {
            waitingForFinish = false;
        } 

        public void Update(GameTime gameTime)
        {
            //Display Next Message in Queue
            if (messages.Count >= 1 && waitingForFinish == false)
            {
                shelf.Show(messages[0].Message, messages[0].ContentKey, 6, 2);
                messages.Remove(messages[0]);
                waitingForFinish = true;
            }
            //Update Achivements
            foreach (string key in achivementTable.Keys)
            {
                AchivementBase a = (AchivementBase)achivementTable[key];
                if (a.Active == true)
                {
                    a.Update(gameTime);
                }
            }
            //Update Shelf
            shelf.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Draw Shelf
            shelf.Draw(gameTime, sb);
        }
    }
}
