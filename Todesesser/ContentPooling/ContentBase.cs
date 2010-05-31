using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentPooling
{
    public class ContentBase
    {
        private string key;
        private string type;

        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
