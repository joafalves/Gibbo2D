#region Copyrights
/*
Gibbo2D - Copyright - 2013 Gibbo2D Team
Founders - Joao Alves <joao.cpp.sca@gmail.com> and Luis Fernandes <luisapidcloud@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. 
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;

using System.Runtime.Serialization;

namespace Gibbo.Library
{
    /// <summary>
    /// The project settings
    /// </summary>
#if WINDOWS
    [Serializable, TypeConverter(typeof(ExpandableObjectConverter))]
#endif
    [DataContract]
    public class GibboProjectSettings
    {
        #region fields
        [DataMember]
        private bool defaultCollisionDetectionEnabled = true;
        [DataMember]
        private bool highlightActiveTilesetInEditor = true;
        [DataMember]
        private bool highlightActiveTilesetInGame = false;

        #endregion

        #region properties

        /// <summary>
        /// Determines if the active tileset is going to be highlighted in the editor
        /// </summary>
#if WINDOWS
        [DisplayName("Highlight Active Tileset in the Editor"), Description("Determines if the active tileset is going to be highlighted in the editor")]
#endif
        public bool HighlightActiveTilesetInEditor
        {
            get { return highlightActiveTilesetInEditor; }
            set { highlightActiveTilesetInEditor = value; }
        }

        /// <summary>
        /// Determines if the active tileset is going to be highlighted in the game
        /// </summary>
#if WINDOWS
        [DisplayName("Highlight Active Tileset in the Game"), Description("Determines if the active tileset is going to be highlighted in the game")]
#endif
        public bool HighlightActiveTilesetInGame
        {
            get { return highlightActiveTilesetInGame; }
            set { highlightActiveTilesetInGame = value; }
        }

        /// <summary>
        /// Determines if the engine will use the default collision engine
        /// </summary>
#if WINDOWS
        [DisplayName("Default Collision Engine"), Description("Determines if the engine uses the default collision engine")]
#endif
        public bool DefaultCollisionDetectionEnabled
        {
            get { return defaultCollisionDetectionEnabled; }
            set { defaultCollisionDetectionEnabled = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
        }

        #endregion
    }
}
