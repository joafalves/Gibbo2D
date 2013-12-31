using System;

namespace Gibbo.Library
{
    /// <summary>
    /// Attribute to determine if a component should have only 1 instance per object (explicit or assignable from)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class Unique : System.Attribute
    {
        /// <summary>
        /// 'Explicit' is the component type itself.
        /// 'AssinableFrom' can be assigned from parent too (for example, is subclass of the component).
        /// </summary>
        public enum UniqueOptions { Explicit, AssignableFrom }

        public UniqueOptions Options
        {
            get
            {
                return options;
            }
            set
            {

                options = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Unique() 
        {
            this.Options = UniqueOptions.Explicit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public Unique(UniqueOptions options) 
        {
            this.Options = options;
        }

        private UniqueOptions options;
    }
}
