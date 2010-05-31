using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;

namespace Todesesser.ObjectPooling
{
    public class ObjectPool
    {
        private ContentPool Content;

        public ObjectPool(ContentPool content)
        {
            Content = content;
        }
    }
}
