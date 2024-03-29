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
        public enum ObjectTypes { Player, Cursor, Button, Weapon, Bullet, DebugPoint, Enemy, AchivementShelf, Ground, EnemySpawner };

        public ObjectPool(ContentPool content)
        {
            Content = content;
            objectTable = new Hashtable();
        }

        public void RemoveObjectsStartingWith(string startingwith)
        {

        }

        public void RemoveObject(string Key)
        {
            objectTable.Remove(Key);
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
                    case ObjectTypes.Enemy:
                        objectTable.Add(Key, new ObjectEnemy(Key, Type, ContentKey, Content));
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
                    case ObjectTypes.DebugPoint:
                        objectTable.Add(Key, new ObjectDebugPoint(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.AchivementShelf:
                        objectTable.Add(Key, new ObjectAchivementShelf(Key, Type, ContentKey, Content));
                        break;
                    case ObjectTypes.Ground:
                        objectTable.Add(Key, new ObjectGround(Key, Type, ContentKey, Content));
                        break;

                    case ObjectTypes.EnemySpawner:
                        objectTable.Add(Key, new ObjectEnemySpawner(Key, Type, ContentKey, Content));
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

        public ObjectBase GetObject(string Key)
        {
            return (ObjectBase)objectTable[Key];
        }

        public int Count
        {
            get { return this.objectTable.Count; }
        }
    }
}
