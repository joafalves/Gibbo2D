using System;

namespace Gibbo.Library
{
    /// <summary>
    /// Attribute that adds one or more component requirements to the object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class RequireComponent : System.Attribute
    {
        public readonly string ComponentsTypeNames;
        public readonly bool requireAll;

        /// <summary>
        /// Requirement based on the type name(s)
        /// </summary>
        /// <param name="componentTypeNames">The type name, if you want to apply multiple requirements use '|' to split them.</param>
        /// <param name="requireAll">If true every component is required</param>
        public RequireComponent(string componentTypeNames, bool requireAll = true)
        {
            this.ComponentsTypeNames = componentTypeNames;
            this.requireAll = requireAll;
        }
    }
}
