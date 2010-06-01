﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentPooling;
using System.Collections;
using Todesesser.ObjectPooling.ObjectTypes;

namespace Todesesser.ObjectPooling
{
    public class ObjectPool
    {
        private ContentPool Content;
        private Hashtable objectTable;
        public enum ObjectTypes { Player };

        public ObjectPool(ContentPool content)
        {
            Content = content;
            objectTable = new Hashtable();
        }

        public ObjectBase AddObject(ObjectTypes Type, string Key, string ContentKey)
        {
            if (objectTable.ContainsKey(Key) == false)
            {
                switch (Type)
                {
                    case ObjectTypes.Player:
                        objectTable.Add(Key, new ObjectPlayer(Key, Type, ContentKey, Content));
                        break;
                    default:
                        break;
                }
                return (ObjectBase)objectTable[Key];
            }
            else
            {
                return null;
            }
        }
    }
}