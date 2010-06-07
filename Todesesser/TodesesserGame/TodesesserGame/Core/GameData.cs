using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Todesesser.Core
{
    static class GameData
    {
        public enum GameStates { Invalid, Menu, Playing, Paused, Exiting, Splash };
        private static GameStates gameState = GameStates.Invalid;

        private static Rectangle mouseRect = new Rectangle();

        public static GameStates GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public static Rectangle MouseRectangle
        {
            get { return mouseRect; }
            set { mouseRect = value; }
        }
    }
}
