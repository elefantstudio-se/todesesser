using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Collections;
using ContentPooling.ContentTypes;

namespace ContentPooling
{
    public class ContentPool
    {
        private Hashtable contentTable;
        private ContentManager Content;

        public ContentPool(ContentManager contentManager)
        {
            contentTable = new Hashtable();
            Content = contentManager;
        }

        # region Add

        /// <summary>
        /// Add's new Texture2D to ContentPool.
        /// </summary>
        /// <param name="Texture">Texture2D to Pool</param>
        /// <param name="Key">Unique Key of Content</param>
        /// <returns>True = Sucessfully Added, False = Key Already Exists</returns>
        public bool AddTexture2D(Texture2D Texture, string Key)
        {
            if (contentTable.ContainsKey(Key) == false)
            {
                contentTable.Add(Key, new ContentTexture2D(Texture, Key));
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddTexture2D(string AssetName, string Key)
        {
            Texture2D t = null;
            try
            {
                t = Content.Load<Texture2D>(AssetName);
            }
            catch (ContentLoadException cle)
            {
                throw ContentTexture2DLoadException.FromContentLoadException(cle);
            }
            return AddTexture2D(t, Key);
        }

        /// <summary>
        /// Add's new Texture2D to ContentPool.
        /// </summary>
        /// <param name="Texture">Texture2D to Pool</param>
        /// <param name="Key">Unique Key of Content</param>
        /// <returns>True = Sucessfully Added, False = Key Already Exists</returns>
        public bool AddSpriteFont(SpriteFont Font, string Key)
        {
            if (contentTable.ContainsKey(Key) == false)
            {
                contentTable.Add(Key, new ContentSpriteFont(Font, Key));
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddSpriteFont(string AssetName, string Key)
        {
            SpriteFont t = null;
            try
            {
                t = Content.Load<SpriteFont>(AssetName);
            }
            catch (ContentLoadException cle)
            {
                throw ContentTexture2DLoadException.FromContentLoadException(cle);
            }
            return AddSpriteFont(t, Key);
        }

        #endregion

        #region Get

        /// <summary>
        /// Get's Texture2D from ContentPool
        /// </summary>
        /// <param name="Key">Unqiue Key of Texture2D Content</param>
        /// <returns>ContentTexture2D, Or null if doesn't exist</returns>
        public ContentTexture2D GetTexture2D(string Key)
        {
            if (contentTable.ContainsKey(Key) == true)
            {
                return (ContentTexture2D)contentTable[Key];
            }
            else
            {
                return null;
            }
        }

        public ContentSpriteFont GetSpriteFont(string Key)
        {
            if (contentTable.ContainsKey(Key) == true)
            {
                return (ContentSpriteFont)contentTable[Key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get's Type of Content
        /// </summary>
        /// <param name="Key">Unqiue Key of Content</param>
        /// <returns>Content Type</returns>
        public string GetType(string Key)
        {
            if (contentTable.ContainsKey(Key) == true)
            {
                ContentBase b = (ContentBase)contentTable[Key];
                return b.Type;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
