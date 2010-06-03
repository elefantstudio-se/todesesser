using System;
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
        public enum ObjectTypes { Player, Wall, Cursor, Button, Weapon, Bullet };

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
                    case ObjectTypes.Wall:
                        objectTable.Add(Key, new ObjectWall(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.Cursor:
                        objectTable.Add(Key, new ObjectCursor(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.Button:
                        objectTable.Add(Key, new ObjectButton(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.Weapon:
                        objectTable.Add(Key, new ObjectWeapon(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.Bullet:
                        objectTable.Add(Key, new ObjectBullet(Key, Type, ContentKey, Content));
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

        public int Count
        {
            get { return this.objectTable.Count; }
        }
    }
}
