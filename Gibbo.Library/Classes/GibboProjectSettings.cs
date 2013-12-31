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
