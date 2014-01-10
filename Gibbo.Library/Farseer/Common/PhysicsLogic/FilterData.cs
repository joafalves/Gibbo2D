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
using FarseerPhysics.Dynamics;

namespace FarseerPhysics.Common.PhysicsLogic
{
    /// <summary>
    /// Contains filter data that can determine whether an object should be processed or not.
    /// </summary>
    public abstract class FilterData
    {
        /// <summary>
        /// Disable the logic on specific categories.
        /// Category.None by default.
        /// </summary>
        public Category DisabledOnCategories = Category.None;

        /// <summary>
        /// Disable the logic on specific groups
        /// </summary>
        public int DisabledOnGroup;

        /// <summary>
        /// Enable the logic on specific categories
        /// Category.All by default.
        /// </summary>
        public Category EnabledOnCategories = Category.All;

        /// <summary>
        /// Enable the logic on specific groups.
        /// </summary>
        public int EnabledOnGroup;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public virtual bool IsActiveOn(Body body)
        {
            if (body == null || !body.Enabled || body.IsStatic)
                return false;

            if (body.FixtureList == null)
                return false;

            foreach (Fixture fixture in body.FixtureList)
            {
                //Disable
                if ((fixture.CollisionGroup == DisabledOnGroup) && fixture.CollisionGroup != 0 && DisabledOnGroup != 0)
                    return false;

                if ((fixture.CollisionCategories & DisabledOnCategories) != Category.None)
                    return false;

                if (EnabledOnGroup != 0 || EnabledOnCategories != Category.All)
                {
                    //Enable
                    if ((fixture.CollisionGroup == EnabledOnGroup) && fixture.CollisionGroup != 0 && EnabledOnGroup != 0)
                        return true;

                    if ((fixture.CollisionCategories & EnabledOnCategories) != Category.None &&
                        EnabledOnCategories != Category.All)
                        return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddDisabledCategory(Category category)
        {
            DisabledOnCategories |= category;
        }

        /// <summary>
        /// Removes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void RemoveDisabledCategory(Category category)
        {
            DisabledOnCategories &= ~category;
        }

        /// <summary>
        /// Determines whether this body ignores the the specified controller.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// 	<c>true</c> if the object has the specified category; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInDisabledCategory(Category category)
        {
            return (DisabledOnCategories & category) == category;
        }

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddEnabledCategory(Category category)
        {
            EnabledOnCategories |= category;
        }

        /// <summary>
        /// Removes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void RemoveEnabledCategory(Category category)
        {
            EnabledOnCategories &= ~category;
        }

        /// <summary>
        /// Determines whether this body ignores the the specified controller.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// 	<c>true</c> if the object has the specified category; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInEnabledInCategory(Category category)
        {
            return (EnabledOnCategories & category) == category;
        }
    }
}