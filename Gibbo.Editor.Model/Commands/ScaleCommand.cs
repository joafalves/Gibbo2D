using Gibbo.Library;
using Microsoft.Xna.Framework;

namespace Gibbo.Editor.Model
{
    public class ScaleCommand : ICommand
    {
        #region fields

        private Vector2 _change;
        private Vector2 _beforeChange;
        private GameObject _element;

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        /// <param name="element"></param>
        public ScaleCommand(Vector2 change, Vector2 before, GameObject element)
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
            _element.Transform.Scale = _change;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnExecute()
        {
            _element.Transform.Scale = _beforeChange;
        }

        #endregion
    }
}
