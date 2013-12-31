using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbo.Editor.Model
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    } 
}
