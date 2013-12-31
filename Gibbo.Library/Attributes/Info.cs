using System;

namespace Gibbo.Library
{
    /// <summary>
    /// Attribute that adds an information attribute to the component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class Info : System.Attribute
    {
        public readonly string Value;

        /// <summary>
        /// Creates a new Info
        /// </summary>
        /// <param name="info">The information you want to be displayed</param>
        public Info(string info)
        {
            this.Value = info;
        }
    }
}
