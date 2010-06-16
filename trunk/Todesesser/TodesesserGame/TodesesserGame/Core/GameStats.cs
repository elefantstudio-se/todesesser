using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Todesesser.Core
{
    /// <summary>
    /// Holds Statistics about Gameplay.
    /// </summary>
    public static class GameStats
    {
        private static Hashtable statTable = new Hashtable(2); //Set to Amount of Statistic Types.

        public static void UpdateStat<T>(string key, T value)
        {
            if (statTable.ContainsKey(key) == true)
            {
                statTable[key] = value;
            }
        }

        public static void AppendStat<T>(string key, T value)
        {
            if (statTable.ContainsKey(key) == true)
            {
                if (typeof(T) == typeof(Int32))
                {
                    //Int Appending
                    Int32 prev = (Int32)statTable[key];
                    statTable[key] = prev + Convert.ToInt32(value);
                }
            }
        }

        public static void CreateStat<T>(string key, T value)
        {
            if (statTable.ContainsKey(key) == false)
            {
                statTable.Add(key, value);
            }
        }

        public static T GetStat<T>(string key)
        {
            return (T)statTable[key];
        }
    }
}
