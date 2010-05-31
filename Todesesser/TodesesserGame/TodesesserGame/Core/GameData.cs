using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todesesser.Core
{
    static class GameData
    {
        public enum GameStates { Invalid, Menu, Playing, Paused };
        private static GameStates gameState = GameStates.Invalid;

        public static GameStates GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }
    }
}
