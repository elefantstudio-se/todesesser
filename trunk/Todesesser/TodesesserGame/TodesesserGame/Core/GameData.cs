using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Todesesser.Core
{
    static class GameData
    {
        public enum GameStates { Invalid, Menu, Playing, Paused, Exiting, Splash };
        private static GameStates gameState = GameStates.Invalid;
        private static Rectangle mouseRect = new Rectangle();
        private static ConPlug.ConPlug settings;
        private static ConPlug.ConPlug profile;
        private static Viewport screenSize;

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

        public static ConPlug.ConPlug Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public static ConPlug.ConPlug Profile
        {
            get { return profile; }
            set { profile = value; }
        }

        public static Viewport ScreenSize
        {
            get { return screenSize; }
            set { screenSize = value; }
        }
    }
}
