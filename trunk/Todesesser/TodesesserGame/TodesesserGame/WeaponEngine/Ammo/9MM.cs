using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todesesser.WeaponEngine.Ammo
{
    public class _9MM : AmmoBase
    {
        public _9MM(int ammount)
        {
            this.Remaining = ammount;
            this.Name = "9MM";
        }
    }
}
