using Gibbo.Library;

namespace Gibbo.Editor.Model
{
    public class RotateCommand : ICommand
    {
        #region fields

        private float _change;
        private float _beforeChange;
        private GameObject _element;

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        /// <param name="element"></param>
        public RotateCommand(float change, float before, GameObject element)
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
            _element.Transform.Rotation = _change;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnExecute()
        {
            _element.Transform.Rotation = _beforeChange;
        }

        #endregion
    }
}
