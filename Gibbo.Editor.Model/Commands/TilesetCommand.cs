using Gibbo.Library;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Gibbo.Editor.Model
{
    public class TilesetCommand : ICommand
    {
        #region fields

        private Tile[,] _change;
        private Tile[,] _beforeChange;
        private Tileset _element;

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        /// <param name="element"></param>
        public TilesetCommand(Tile[,] change, Tile[,] before, Tileset element)
        {
            _change = change;
            _element = element;
            _beforeChange = before;
        }

        #endregion

        #region ICommand methods

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            _element.Tiles = (_change);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnExecute()
        {
            _element.Tiles = (_beforeChange);
        }

        #endregion
    } 
}
