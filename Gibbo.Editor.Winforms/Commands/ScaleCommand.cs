using Gibbo.Library;

namespace Gibbo.Editor
{
    class ScaleCommand : ICommand
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
        public ScaleCommand(float change, float before, GameObject element)
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
