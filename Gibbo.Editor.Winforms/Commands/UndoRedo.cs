using System.Collections.Generic;
using Gibbo.Library;
using Microsoft.Xna.Framework;

namespace Gibbo.Editor
{
    class UndoRedo
    {
        #region fields

        private Stack<ICommand> _Undocommands = new Stack<ICommand>();
        private Stack<ICommand> _Redocommands = new Stack<ICommand>();

        #endregion

        #region methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Redocommands.Count != 0)
                {
                    ICommand command = _Redocommands.Pop();
                    command.Execute();
                    _Undocommands.Push(command);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Undocommands.Count != 0)
                {
                    ICommand command = _Undocommands.Pop();
                    command.UnExecute();
                    _Redocommands.Push(command);
                }

            }
        }

        #endregion

        #region helper functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        public void InsertUndoRedo(ICommand cmd)
        {
            _Undocommands.Push(cmd);
            _Redocommands.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="before"></param>
        /// <param name="element"></param>
        public void InsertInUnDoRedoForMove(Vector2 position, Vector2 oldPosition, GameObject element)
        {
            ICommand cmd = new MoveCommand(position, oldPosition, element);
            _Undocommands.Push(cmd);
            _Redocommands.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="oldScale"></param>
        /// <param name="element"></param>
        public void InsertInUnDoRedoForScale(float scale, float oldScale, GameObject element)
        {
            ICommand cmd = new ScaleCommand(scale, oldScale, element);
            _Undocommands.Push(cmd);
            _Redocommands.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotate"></param>
        /// <param name="oldRotate"></param>
        /// <param name="element"></param>
        public void InsertInUnDoRedoForRotate(float rotate, float oldRotate, GameObject element)
        {
            ICommand cmd = new RotateCommand(rotate, oldRotate, element);
            _Undocommands.Push(cmd);
            _Redocommands.Clear();
        }

        #endregion
    } 
}
