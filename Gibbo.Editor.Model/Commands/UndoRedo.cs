#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

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
