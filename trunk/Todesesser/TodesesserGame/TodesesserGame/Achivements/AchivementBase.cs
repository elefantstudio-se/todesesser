using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ContentPooling;

namespace Todesesser.Achivements
{
    public class AchivementBase
    {
        private string name;
        private bool active;
        public delegate void OnCompletedDelegate(AchivementBase Achivement);
        public event OnCompletedDelegate OnAchivementCompleted;
        private string contentKey;
        private bool contentLoaded = false;

        public virtual void LoadContent(ContentPool Content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public void Completed()
        {
            if (OnAchivementCompleted != null)
            {
                OnAchivementCompleted(this);
            }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        public string ContentKey
        {
            get { return this.contentKey; }
            set { this.contentKey = value; }
        }

        public bool ContentLoaded
        {
            get { return this.contentLoaded; }
            set { this.contentLoaded = value; }
        }
    }
}
