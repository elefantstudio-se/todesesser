using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todesesser.WeaponEngine.Ammo
{
    class Clip
    {
        private int remaining;
        private string clipType;
        public Clip(string type, int size)
        {
            remaining = size;
            clipType = type;
        }

        public int Remaining
        {
            get {return remaining;}
            set {remaining = value;}
        }

        public string ClipType
        {
            get {return clipType; }
        }
    }
}
