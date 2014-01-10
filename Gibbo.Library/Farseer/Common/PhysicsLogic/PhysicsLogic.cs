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
using FarseerPhysics.Dynamics;

namespace FarseerPhysics.Common.PhysicsLogic
{
    [Flags]
    public enum PhysicsLogicType
    {
        Explosion = (1 << 0)
    }

    public struct PhysicsLogicFilter
    {
        public PhysicsLogicType ControllerIgnores;

        /// <summary>
        /// Ignores the controller. The controller has no effect on this body.
        /// </summary>
        /// <param name="type">The logic type.</param>
        public void IgnorePhysicsLogic(PhysicsLogicType type)
        {
            ControllerIgnores |= type;
        }

        /// <summary>
        /// Restore the controller. The controller affects this body.
        /// </summary>
        /// <param name="type">The logic type.</param>
        public void RestorePhysicsLogic(PhysicsLogicType type)
        {
            ControllerIgnores &= ~type;
        }

        /// <summary>
        /// Determines whether this body ignores the the specified controller.
        /// </summary>
        /// <param name="type">The logic type.</param>
        /// <returns>
        /// 	<c>true</c> if the body has the specified flag; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPhysicsLogicIgnored(PhysicsLogicType type)
        {
            return (ControllerIgnores & type) == type;
        }
    }

    public abstract class PhysicsLogic : FilterData
    {
        private PhysicsLogicType _type;
        public World World;

        public override bool IsActiveOn(Body body)
        {
            if (body.PhysicsLogicFilter.IsPhysicsLogicIgnored(_type))
                return false;

            return base.IsActiveOn(body);
        }

        public PhysicsLogic(World world, PhysicsLogicType type)
        {
            _type = type;
            World = world;
        }
    }
}