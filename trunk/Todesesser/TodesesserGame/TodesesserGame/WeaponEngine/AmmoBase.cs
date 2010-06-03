using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todesesser.WeaponEngine
{
    public class AmmoBase
    {
        private int remaining;
        private string name;

        public int Remaining
        {
            get { return this.remaining; }
            set { this.remaining = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
