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

using System.Collections.Generic;
using Gibbo.Library;
using Microsoft.Xna.Framework;

namespace Gibbo.Editor.Model
{
    public class UndoRedo
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
        public void InsertInUnDoRedoForScale(Vector2 scale, Vector2 oldScale, GameObject element)
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
