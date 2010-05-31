using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace ContentPooling
{
    public class ContentTexture2DLoadException : Exception
    {

        public ContentTexture2DLoadException(string hl, Exception ie, string msg, string source, string stackTrace)
        {
            this.HelpLink = hl;
        }

        public static ContentTexture2DLoadException FromContentLoadException(ContentLoadException cle)
        {
            return new ContentTexture2DLoadException(cle.HelpLink, cle.InnerException, cle.Message, cle.Source, cle.StackTrace);
        }
    }
}
