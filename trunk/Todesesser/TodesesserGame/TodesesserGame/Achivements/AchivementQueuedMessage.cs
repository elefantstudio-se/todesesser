using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.Achivements
{
    public class AchivementQueuedMessage
    {
        private string message;
        private string contentKey;

        public AchivementQueuedMessage(string message, string contentKey)
        {
            this.message = message;
            this.contentKey = contentKey;
        }

        public string Message
        {
            get { return this.message; }
        }

        public string ContentKey
        {
            get { return this.contentKey; }
        }
    }
}
